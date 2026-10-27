// scripts/reference-check.mjs — verifies the reference corpus beside this repo
// (../tigerfid-reference/) against docs/reference-corpus.tsv: each tree row
// recreated from its pinned commit (every file at its git blob id, nothing
// extra), each file row at its sha256 (nothing unpinned beside it), no macOS
// sidecars, and the folder README byte-equal to docs/reference-readme-stub.md.
// Plan Story 1.1.3 (hub spec 2026-10-26-tigerfid-onboarding-design, C4); the
// shape is tigerian's and tigerdash's. The last step of `npm run setup`.
// How to run (from the repo root):
//   node scripts/reference-check.mjs               verify + report
//   node scripts/reference-check.mjs --recreate    + recreate every MISSING tree row from its pin by git archive
//   node scripts/reference-check.mjs --write-stub  + replace an absent, adoption-era or stale README with the stub
//   --folder <path>                                override the sibling folder (tests only)
// Exit 0 when every row is ok, no sidecar, README the stub; 1 on any finding;
// 2 when the folder is absent (without --recreate) or the usage is wrong.
// File rows are copy-only: the tool never fetches one, because the pages they
// were captured from change. It never modifies or deletes a present file and
// writes nothing without --recreate / --write-stub. --recreate first fetches
// the pin's missing blobs in ONE batch (a blobless clone would otherwise fetch
// them one by one), then extracts `git archive` with core.autocrlf off into
// <name>.partial/ and renames it into place only once the whole archive is
// written and its commit id matches the pin.
import { execFileSync, spawn } from 'node:child_process';
import { createHash } from 'node:crypto';
import {
  closeSync, existsSync, mkdirSync, openSync, readFileSync, readSync, readdirSync,
  realpathSync, renameSync, statSync, utimesSync, writeFileSync, writeSync,
} from 'node:fs';
import { dirname, isAbsolute, join, resolve } from 'node:path';
import { fileURLToPath } from 'node:url';

const REPO_ROOT = join(dirname(fileURLToPath(import.meta.url)), '..');
export const TSV = 'docs/reference-corpus.tsv';
export const STUB = 'docs/reference-readme-stub.md';
const STUB_MARK = '# tigerfid-reference — STOP before reading';
const ADOPTION_MARK = '# STOP — reference-only folder for tigerfid';
const SIDECAR_RE = /^(\._.*|\.DS_Store)$/s;
const HEX40 = /^[0-9a-f]{40}$/;
const HEX64 = /^[0-9a-f]{64}$/;
const norm = (t) => t.replace(/\r\n/g, '\n');

export function parseTsv(text) {
  const [head, ...rows] = text.split(/\r?\n/).filter((l) => l.length && !l.startsWith('#'));
  if (head === undefined) throw new Error(`${TSV} holds no header row`);
  const cols = head.split('\t');
  return rows.map((r) => Object.fromEntries(r.split('\t').map((v, i) => [cols[i], v])));
}

function git(args, cwd, opts = {}) {
  return execFileSync('git', args, { cwd, stdio: ['ignore', 'pipe', 'pipe'], maxBuffer: 1 << 28, ...opts }).toString('utf8');
}

// Why a row name (or an archive entry) would leave the folder once joined
// under it, or null. Names are `/`-separated relative paths on every platform.
export function nameProblem(name) {
  if (!name) return 'is empty';
  if (name.includes('\\')) return 'carries a backslash';
  if (isAbsolute(name) || /^[A-Za-z]:/.test(name)) return 'is absolute';
  if (name.split('/').some((s) => s === '' || s === '.' || s === '..')) return 'carries an empty, . or .. segment';
  return null;
}

// A file's digest, read in 1 MiB slices so a large original never sits whole
// in memory. `blobId` is git's object id for the bytes (sha1 of
// "blob <size>\0" + bytes), the same id `git ls-tree` prints, with no filter
// between the disk and the hash.
function digest(path, algo, prefix) {
  const h = createHash(algo);
  if (prefix) h.update(prefix);
  const fd = openSync(path, 'r');
  const buf = Buffer.allocUnsafe(1 << 20);
  try {
    let n;
    while ((n = readSync(fd, buf, 0, buf.length, null)) > 0) h.update(buf.subarray(0, n));
  } finally { closeSync(fd); }
  return h.digest('hex');
}
export const sha256 = (path) => digest(path, 'sha256');
export const blobId = (path) => digest(path, 'sha1', `blob ${statSync(path).size}\0`);

// Every file below `dir`, as `/`-joined paths relative to it.
export function walkFiles(dir, base = '') {
  const out = [];
  for (const e of readdirSync(dir, { withFileTypes: true })) {
    const rel = base ? `${base}/${e.name}` : e.name;
    if (e.isDirectory()) out.push(...walkFiles(join(dir, e.name), rel));
    else out.push(rel);
  }
  return out;
}

// The pin's tree as path → blob id (`ls-tree -z`, untrimmed). Trees and
// commits are always present in a blobless clone, so this fetches nothing.
export function lsTree(pin, root) {
  const want = new Map();
  for (const rec of git(['ls-tree', '-r', '-z', '--full-tree', pin], root).split('\0')) {
    if (!rec) continue;
    const tab = rec.indexOf('\t');
    const [mode, type, oid] = rec.slice(0, tab).split(' ');
    if (type !== 'blob' || mode === '120000') throw new Error(`unsupported entry ${mode} ${type} at ${rec.slice(tab + 1)}`);
    want.set(rec.slice(tab + 1), oid);
  }
  return want;
}

export function readmeState(folder, stubText) {
  const p = join(folder, 'README.md');
  if (!existsSync(p)) return 'absent';
  const t = norm(readFileSync(p, 'utf8'));
  if (t === norm(stubText)) return 'stub';
  if (t.startsWith(STUB_MARK)) return 'stale-stub';
  if (t.startsWith(ADOPTION_MARK)) return 'adoption';
  return 'unknown';
}

function verifyTree(row, folder, root, log, findings, missingTrees) {
  const dir = join(folder, row.name);
  let tagAt = null;
  try { tagAt = git(['rev-parse', '--verify', '-q', `refs/tags/${row.name}^{commit}`], root).trim(); } catch { /* no local tag: the pin alone */ }
  if (tagAt && tagAt !== row.pin) { log(`MOVED   tag    ${row.name} peels to ${tagAt.slice(0, 12)}, pin ${row.pin.slice(0, 12)} — the tag is never moved; stop and ask`); findings.push(`tag:${row.name}`); }
  let want;
  try { want = lsTree(row.pin, root); } catch (e) { log(`NOPIN   tree   ${row.name} — ${row.pin.slice(0, 12)} is not readable in this clone (git fetch origin tag ${row.name}): ${String(e.message).split('\n')[0]}`); findings.push(row.name); return; }
  if (!existsSync(dir)) { log(`MISSING tree   ${row.name} — ${want.size} files; next: node scripts/reference-check.mjs --recreate`); findings.push(row.name); missingTrees.push(row); return; }
  const have = new Set(walkFiles(dir));
  const bad = [];
  for (const [p, oid] of want) {
    if (!have.has(p)) bad.push(`missing ${p}`);
    else if (blobId(join(dir, p)) !== oid) bad.push(`changed ${p}`);
  }
  for (const p of have) if (!want.has(p)) bad.push(`extra   ${p}`);
  if (bad.length === 0) { log(`ok      tree   ${row.name}  ${row.pin.slice(0, 12)} — ${want.size} files at their blob ids, none extra`); return; }
  log(`DRIFT   tree   ${row.name}  ${row.pin.slice(0, 12)} — ${bad.length} path(s):`);
  for (const b of bad) log(`          ${b}`);
  findings.push(row.name);
}

export function verify({ folder, rows, root = REPO_ROOT, stubText, log = console.log }) {
  const findings = [];
  const missingTrees = [];
  const pinned = new Map(); // top folder → Set of file-row names under it
  const counts = new Map(); // top folder → { ok, total }
  for (const r of rows) {
    const unsafe = nameProblem(r.name);
    if (unsafe) { log(`UNSAFE  ${r.kind}  ${JSON.stringify(r.name)} — the name ${unsafe}; never read or written`); findings.push(`unsafe:${r.name}`); continue; }
    if (r.kind === 'tree') {
      if (!HEX40.test(r.pin ?? '')) { log(`BADPIN  tree   ${r.name} — a tree pin is a full 40-hex commit id`); findings.push(r.name); continue; }
      verifyTree(r, folder, root, log, findings, missingTrees);
    } else if (r.kind === 'file') {
      if (!HEX64.test(r.pin ?? '')) { log(`BADPIN  file   ${r.name} — a file pin is a 64-hex sha256`); findings.push(r.name); continue; }
      const top = r.name.split('/')[0];
      if (!pinned.has(top)) { pinned.set(top, new Set()); counts.set(top, { ok: 0, total: 0 }); }
      pinned.get(top).add(r.name);
      counts.get(top).total++;
      const p = join(folder, r.name);
      if (!existsSync(join(folder, top))) { findings.push(r.name); continue; } // one line per absent folder, below
      if (!existsSync(p) || !statSync(p).isFile()) { log(`MISSING file   ${r.name} (copy-only — copy it from the first machine)`); findings.push(r.name); }
      else if (sha256(p) !== r.pin) { log(`DRIFT   file   ${r.name} — its sha256 is not the pin`); findings.push(r.name); }
      else counts.get(top).ok++;
    } else { log(`UNKNOWN kind   ${JSON.stringify(r.kind)} for ${r.name}`); findings.push(r.name); }
  }
  for (const [top, names] of pinned) {
    const c = counts.get(top);
    if (!existsSync(join(folder, top))) { log(`MISSING folder ${top}/ — ${c.total} files, copy-only: copy the folder whole from the first machine`); continue; }
    const unpinned = walkFiles(join(folder, top), top).filter((p) => !names.has(p) && !SIDECAR_RE.test(p.split('/').at(-1)));
    for (const p of unpinned) log(`UNPINNED file  ${p} — in the folder, not in ${TSV}`);
    findings.push(...unpinned.map((p) => `unpinned:${p}`));
    log(`${c.ok === c.total ? 'ok     ' : 'FILES  '} files  ${top}/  ${c.ok} of ${c.total} at their sha256${unpinned.length ? `, ${unpinned.length} unpinned` : ''}`);
  }
  const sidecars = walkFiles(folder).filter((p) => SIDECAR_RE.test(p.split('/').at(-1)));
  if (sidecars.length) {
    log(`SIDECARS ${sidecars.length} — macOS metadata files; strip them from the folder root: find . \\( -name '._*' -o -name '.DS_Store' \\) -type f -print -delete`);
    for (const p of sidecars) log(`          ${p}`);
    findings.push('sidecars');
  } else log('ok      sidecars none');
  const readme = readmeState(folder, stubText);
  log(`${readme === 'stub' ? 'ok     ' : 'README '} readme ${readme}`);
  if (readme !== 'stub') findings.push(`readme:${readme}`);
  return { findings, missingTrees, readme };
}

// A streaming reader for the tar `git archive` writes (ustar, with pax
// headers for the commit id and for long names): each regular file is
// written under `dest` as it streams, never held whole. Anything but a
// directory or a regular file is refused, as is a name that would leave
// `dest`.
export class TarExtractor {
  constructor(dest) {
    this.dest = dest; this.buf = Buffer.alloc(0); this.state = 'header';
    this.left = 0; this.pad = 0; this.fd = null; this.entry = null;
    this.meta = []; this.pax = {}; this.globalPax = {}; this.files = 0;
  }

  push(chunk) {
    this.buf = this.buf.length ? Buffer.concat([this.buf, chunk]) : chunk;
    for (;;) {
      if (this.state === 'header') {
        if (this.buf.length < 512) return;
        const h = this.buf.subarray(0, 512);
        this.buf = this.buf.subarray(512);
        if (h.every((b) => b === 0)) { this.state = 'end'; continue; }
        this.header(h);
      } else if (this.state === 'data' || this.state === 'meta') {
        if (this.left > 0) {
          if (this.buf.length === 0) return;
          const n = Math.min(this.left, this.buf.length);
          const part = this.buf.subarray(0, n);
          this.buf = this.buf.subarray(n);
          this.left -= n;
          if (this.state === 'data') writeSync(this.fd, part);
          else this.meta.push(Buffer.from(part));
          if (this.left > 0) return;
        }
        this.finishEntry();
      } else if (this.state === 'pad') {
        const n = Math.min(this.pad, this.buf.length);
        this.buf = this.buf.subarray(n);
        this.pad -= n;
        if (this.pad > 0) return;
        this.state = 'header';
      } else { this.buf = Buffer.alloc(0); return; } // 'end': the trailing zero blocks
    }
  }

  header(h) {
    const str = (o, n) => { const s = h.subarray(o, o + n); const z = s.indexOf(0); return s.subarray(0, z < 0 ? n : z).toString('utf8'); };
    const oct = (o, n) => parseInt(str(o, n).trim() || '0', 8);
    let sum = 0;
    for (let i = 0; i < 512; i++) sum += i >= 148 && i < 156 ? 32 : h[i];
    if (sum !== oct(148, 8)) throw new Error('a tar header fails its checksum — the archive is damaged');
    const size = oct(124, 12);
    const type = h[156] === 0 ? '0' : String.fromCharCode(h[156]);
    const prefix = str(257, 5) === 'ustar' ? str(345, 155) : '';
    const name = (this.pax.path ?? (prefix ? `${prefix}/${str(0, 100)}` : str(0, 100))).replace(/\/$/, '');
    this.left = size;
    this.pad = (512 - (size % 512)) % 512;
    if (type === 'g' || type === 'x') { this.state = 'meta'; this.entry = { type }; this.meta = []; return; }
    this.pax = {};
    const unsafe = nameProblem(name);
    if (unsafe) throw new Error(`the archive entry ${JSON.stringify(name)} ${unsafe} — refused`);
    const path = join(this.dest, name);
    if (type === '5') { mkdirSync(path, { recursive: true }); this.entry = null; this.state = 'data'; return; }
    if (type !== '0') throw new Error(`the archive entry ${name} has tar type ${JSON.stringify(type)} — only directories and regular files are expected`);
    mkdirSync(dirname(path), { recursive: true });
    this.fd = openSync(path, 'wx');
    this.entry = { type, path, mtime: oct(136, 12) };
    this.state = 'data';
  }

  finishEntry() {
    const e = this.entry;
    if (e && (e.type === 'g' || e.type === 'x')) {
      const rec = {};
      const text = Buffer.concat(this.meta).toString('utf8');
      for (let i = 0; i < text.length;) {
        const sp = text.indexOf(' ', i);
        const len = parseInt(text.slice(i, sp), 10);
        if (!(len > 0)) break;
        const kv = text.slice(sp + 1, i + len - 1);
        const eq = kv.indexOf('=');
        rec[kv.slice(0, eq)] = kv.slice(eq + 1);
        i += len;
      }
      if (e.type === 'g') Object.assign(this.globalPax, rec); else this.pax = rec;
    } else if (e) {
      closeSync(this.fd);
      this.fd = null;
      utimesSync(e.path, e.mtime, e.mtime);
      this.files++;
    }
    this.entry = null;
    this.state = this.pad > 0 ? 'pad' : 'header';
  }

  end() {
    if (this.fd !== null) { closeSync(this.fd); this.fd = null; }
    if (this.state !== 'end') throw new Error('the archive ended early — no end-of-archive blocks');
  }
}

// The pin's blobs this clone does not hold, without fetching any of them.
export function missingBlobs(pin, root) {
  return git(['rev-list', '--objects', '--no-walk', '--missing=print', pin], root)
    .split('\n').filter((l) => l.startsWith('?')).map((l) => l.slice(1).trim());
}

// Recreates one tree row: the missing blobs in one batched fetch (the
// recipe git's own lazy fetch runs), then `git archive` of the pin, streamed
// into <name>.partial/ and renamed into place once complete.
export async function recreateTree({ row, folder, root = REPO_ROOT, log = console.log }) {
  const dir = join(folder, row.name);
  const tmp = `${dir}.partial`;
  if (existsSync(dir)) throw new Error(`${dir} exists — the tool never writes over a present tree`);
  if (existsSync(tmp)) throw new Error(`${tmp} exists — an earlier run stopped part-way; remove that folder by hand and run again`);
  const missing = missingBlobs(row.pin, root);
  if (missing.length) {
    log(`fetching ${missing.length} missing blob(s) of ${row.pin.slice(0, 12)} in one batch`);
    git(['-c', 'fetch.negotiationAlgorithm=noop', 'fetch', 'origin', '--no-tags', '--no-write-fetch-head', '--recurse-submodules=no', '--filter=blob:none', '--stdin'],
      root, { input: `${missing.join('\n')}\n`, stdio: ['pipe', 'pipe', 'inherit'] });
    const still = missingBlobs(row.pin, root);
    if (still.length) throw new Error(`${still.length} blob(s) still missing after the fetch; first ${still[0]}`);
  }
  mkdirSync(tmp, { recursive: true });
  const ex = new TarExtractor(tmp);
  await new Promise((done, fail) => {
    let failed = false;
    const p = spawn('git', ['-c', 'core.autocrlf=false', 'archive', '--format=tar', row.pin], { cwd: root, stdio: ['ignore', 'pipe', 'inherit'] });
    p.stdout.on('data', (c) => { if (failed) return; try { ex.push(c); } catch (e) { failed = true; p.kill(); fail(e); } });
    p.on('error', (e) => { failed = true; fail(e); });
    p.on('close', (code) => {
      if (failed) return;
      if (code !== 0) return fail(new Error(`git archive exited ${code}`));
      try { ex.end(); done(); } catch (e) { fail(e); }
    });
  });
  if (ex.globalPax.comment !== row.pin) throw new Error(`the archive names commit ${ex.globalPax.comment}, not the pin ${row.pin} — ${tmp} left for inspection`);
  renameSync(tmp, dir);
  log(`recreated tree ${row.name} — ${ex.files} files from ${row.pin.slice(0, 12)} by git archive`);
}

export function writeStub({ folder, stubText, log = console.log }) {
  const state = readmeState(folder, stubText);
  if (state === 'stub') { log('README already the stub — nothing written'); return false; }
  if (state === 'unknown') {
    const first = readFileSync(join(folder, 'README.md'), 'utf8').split(/\r?\n/)[0];
    log(`README unrecognized (first line: ${JSON.stringify(first)}) — refusing to overwrite`);
    return false;
  }
  writeFileSync(join(folder, 'README.md'), stubText);
  log(`README written as the stub (was ${state})`);
  return true;
}

function parseArgs(argv) {
  const opts = { recreate: false, writeStub: false, folder: null };
  for (let i = 0; i < argv.length; i++) {
    const a = argv[i];
    if (a === '--recreate') opts.recreate = true;
    else if (a === '--write-stub') opts.writeStub = true;
    else if (a === '--folder') {
      if (i + 1 >= argv.length || argv[i + 1].startsWith('--')) return { error: '--folder needs a path' };
      opts.folder = argv[++i];
    } else return { error: `unknown flag ${a}` };
  }
  return opts;
}

export async function main(argv = process.argv.slice(2), { root = REPO_ROOT, log = console.log } = {}) {
  const opts = parseArgs(argv);
  if (opts.error) { log(`reference-check: ${opts.error} — usage: node scripts/reference-check.mjs [--recreate] [--write-stub] [--folder <path>]`); return 2; }
  const folder = resolve(opts.folder ?? join(root, '..', 'tigerfid-reference'));
  if (!existsSync(folder)) {
    if (!opts.recreate) { log(`reference folder absent: ${folder} — run with --recreate to rebuild the original tree there, then copy the copy-only folders by hand (docs/reference-repos.md)`); return 2; }
    mkdirSync(folder, { recursive: true });
    log(`reference folder created: ${folder}`);
  }
  const rows = parseTsv(readFileSync(join(root, TSV), 'utf8'));
  const stubText = readFileSync(join(root, STUB), 'utf8');
  let result = verify({ folder, rows, root, stubText, log });
  if (opts.recreate && result.missingTrees.length) {
    for (const row of result.missingTrees) await recreateTree({ row, folder, root, log });
    result = verify({ folder, rows, root, stubText, log });
  }
  if (opts.writeStub && writeStub({ folder, stubText, log })) result = verify({ folder, rows, root, stubText, log });
  if (result.findings.length === 0) { log(`reference-check: ok — ${rows.length} rows verified, README the stub`); return 0; }
  if (result.readme !== 'stub' && !opts.writeStub && result.readme !== 'unknown') log('next: node scripts/reference-check.mjs --write-stub');
  log(`reference-check: ${result.findings.length} finding(s)`);
  return 1;
}

const isMain = (() => { try { return realpathSync(process.argv[1]) === realpathSync(fileURLToPath(import.meta.url)); } catch { return false; } })();
if (isMain) main().then((code) => process.exit(code), (e) => { console.error(`reference-check: ${e.message}`); process.exit(1); });
