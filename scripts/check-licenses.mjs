// scripts/check-licenses.mjs — the copy-with-trail gate (plan Story 1.1.4;
// hub spec 2026-10-26-tigerfid-onboarding-design, C5), in tigerian's
// two-halves form: every marker in the scan set needs an entry in the
// notices file's `## Entries` section and every entry needs a marker; a
// mention of the notices file that is not a well-formed marker is a finding;
// a marker's licence and author must equal its entry's. Each entry carries
// its five lines once each, in order (Source, Pin, License, Taken, Landed);
// its SPDX id is on the allowlist (tigerian's, widened for hardware sources
// with CERN-OHL-P-2.0 and CC-BY-4.0); its Pin is a commit URL with a full
// 40-hex revision or `sha256:<64 hex>`; and its Source is never a source the
// reference corpus (docs/reference-corpus.tsv) marks study-only, nor the
// original, which is this repo's own work and needs no entry.
// The scan set is `git ls-files` minus docs/**, every *.md file, the notices
// file, this file and its test, the hub-synced canonicals that
// scripts/sync-provenance.json lists (a path list, never a heuristic), and
// binaries. A file that cannot carry a comment carries its marker in a
// `<file>.license` text file beside it, which the scan reads like any other.
// How to run (from the repo root): node scripts/check-licenses.mjs
//   (CI's test job runs it; so does `npm test`)
// Exit 0 with no finding, 1 otherwise. Never writes.
import { execFileSync } from 'node:child_process';
import { closeSync, openSync, readFileSync, readSync, realpathSync } from 'node:fs';
import { dirname, join } from 'node:path';
import { fileURLToPath } from 'node:url';
import { parseTsv, TSV } from './reference-check.mjs';

const REPO_ROOT = join(dirname(fileURLToPath(import.meta.url)), '..');
export const NOTICES_FILE = 'THIRD_PARTY_NOTICES.md';
export const MARKER_RE = /Derived from (.+?) \((\S+) © (.+?)\) — see THIRD_PARTY_NOTICES\.md/;
export const ALLOWLIST = new Set([
  'MIT', 'Apache-2.0', 'BSD-2-Clause', 'BSD-3-Clause', 'ISC', '0BSD', 'Unlicense', 'CC0-1.0',
  'CERN-OHL-P-2.0', 'CC-BY-4.0',
]);
const ENTRY_FIELDS = ['Source', 'Pin', 'License', 'Taken', 'Landed'];
const LICENSE_RE = /^(\S+) \(© (.+)\)$/;
const PIN_RE = /^(https?:\/\/\S*[0-9a-f]{40}\S*|sha256:[0-9a-f]{64})$/;
const EXCLUDED_PREFIXES = ['docs/'];
const EXCLUDED_FILES = new Set([NOTICES_FILE, 'scripts/check-licenses.mjs', 'scripts/check-licenses.test.mjs']);
export const SYNC_PROVENANCE = 'scripts/sync-provenance.json';

// The hub-synced canonicals are hub text, never a derived work here, so the
// gate skips them by path. A missing or malformed list exempts nothing.
export function syncedCanonicals(files, readFile) {
  if (!files.includes(SYNC_PROVENANCE)) return new Set();
  try { return new Set(Object.keys(JSON.parse(readFile(SYNC_PROVENANCE)).files ?? {})); } catch { return new Set(); }
}

export function isExcluded(file) {
  return EXCLUDED_FILES.has(file) || file.endsWith('.md') || EXCLUDED_PREFIXES.some((p) => file.startsWith(p));
}

export function isBinary(root, file) {
  const fd = openSync(join(root, file), 'r');
  try {
    const buf = Buffer.alloc(8192);
    const n = readSync(fd, buf, 0, 8192, 0);
    return buf.subarray(0, n).includes(0);
  } finally { closeSync(fd); }
}

// The entries are the `### <project>` headings under `## Entries`, up to the
// next `## ` heading; a `###` elsewhere (the standing notes) is prose.
export function parseNotices(text) {
  const entries = new Map();
  const problems = [];
  let inEntries = false;
  let current = null;
  text.split(/\r?\n/).forEach((line, i) => {
    if (/^## /.test(line)) { inEntries = line.trim() === '## Entries'; current = null; return; }
    if (!inEntries) return;
    const h = /^### (.+)$/.exec(line);
    if (h) {
      current = { project: h[1].trim(), line: i + 1, fields: {}, order: [] };
      if (entries.has(current.project)) problems.push(`${NOTICES_FILE}:${i + 1} — duplicate entry "${current.project}"`);
      entries.set(current.project, current);
      return;
    }
    if (!current) return;
    const f = /^- (Source|Pin|License|Taken|Landed): (.*)$/.exec(line);
    if (!f) return;
    // A repeated line is a finding and the FIRST value stands.
    if (f[1] in current.fields) { problems.push(`${NOTICES_FILE}:${i + 1} — entry "${current.project}" repeats its "${f[1]}:" line`); return; }
    current.fields[f[1]] = f[2].trim();
    current.order.push(f[1]);
  });
  for (const e of entries.values()) {
    for (const k of ENTRY_FIELDS) if (!e.fields[k]) problems.push(`${NOTICES_FILE}:${e.line} — entry "${e.project}" lacks its "${k}:" line`);
    if (e.order.join() !== ENTRY_FIELDS.filter((k) => e.order.includes(k)).join()) problems.push(`${NOTICES_FILE}:${e.line} — entry "${e.project}": its lines are out of order — expected ${ENTRY_FIELDS.join(', ')}`);
    if (e.fields.Pin && !PIN_RE.test(e.fields.Pin)) problems.push(`${NOTICES_FILE}:${e.line} — entry "${e.project}": Pin must be a commit URL carrying a 40-hex revision, or sha256:<64 hex>`);
    if (e.fields.License) {
      const m = LICENSE_RE.exec(e.fields.License);
      if (m) { e.license = m[1]; e.author = m[2]; } else problems.push(`${NOTICES_FILE}:${e.line} — entry "${e.project}": License must read "<SPDX id> (© <author>)"`);
      if (m && !ALLOWLIST.has(m[1])) problems.push(`${NOTICES_FILE}:${e.line} — entry "${e.project}": ${m[1]} is not on the allowlist (${[...ALLOWLIST].join(', ')}) — a source on other terms goes to ASK-OMID`);
    }
  }
  return { entries, problems };
}

const sameUrl = (u) => u.trim().toLowerCase().replace(/\/+$/, '').replace(/\.git$/, '');

export function checkLicenses({ files, noticesText, corpusRows = [], readFile, binary }) {
  const { entries, problems } = parseNotices(noticesText);
  const findings = [...problems];
  for (const e of entries.values()) {
    if (!e.fields.Source) continue;
    const row = corpusRows.find((r) => r.url && r.url !== '-' && sameUrl(r.url) === sameUrl(e.fields.Source));
    if (row && row.verdict === 'study-only') findings.push(`${NOTICES_FILE}:${e.line} — entry "${e.project}": its Source is the corpus row ${row.name}, verdict study-only — never a copy source`);
    if (row && row.verdict === 'original') findings.push(`${NOTICES_FILE}:${e.line} — entry "${e.project}": its Source is the original (${row.name}), this repo's own Apache-2.0 work — it needs no entry`);
  }
  const seen = new Set();
  const synced = syncedCanonicals(files, readFile);
  let scanned = 0;
  for (const file of files) {
    if (isExcluded(file) || synced.has(file) || binary(file)) continue;
    scanned++;
    readFile(file).split(/\r?\n/).forEach((line, idx) => {
      if (!line.includes(NOTICES_FILE)) return;
      const m = MARKER_RE.exec(line);
      if (!m) { findings.push(`${file}:${idx + 1} — mentions ${NOTICES_FILE} but is not a well-formed marker; expected: Derived from <project> (<licence> © <author>) — see ${NOTICES_FILE}`); return; }
      const [, project, license, author] = m;
      seen.add(project);
      const e = entries.get(project);
      if (!e) { findings.push(`${file}:${idx + 1} — marker for "${project}" has no ### entry under ## Entries in ${NOTICES_FILE}`); return; }
      if (e.license && (e.license !== license || e.author !== author)) findings.push(`${file}:${idx + 1} — marker says "${license} © ${author}" but the ${NOTICES_FILE} entry says "${e.license} © ${e.author}"`);
    });
  }
  for (const p of entries.keys()) if (!seen.has(p)) findings.push(`${NOTICES_FILE} — entry "${p}" has no marker anywhere in the scan set`);
  return { findings, entries: entries.size, markers: seen.size, scanned };
}

export function main({ root = REPO_ROOT, log = console.log } = {}) {
  const files = execFileSync('git', ['ls-files', '-z'], { cwd: root, encoding: 'utf8', maxBuffer: 1 << 26 }).split('\0').filter(Boolean);
  const readFile = (f) => readFileSync(join(root, f), 'utf8');
  if (!files.includes(NOTICES_FILE)) { log(`check-licenses: ${NOTICES_FILE} is not tracked — the lane needs it before any copy`); return 1; }
  const corpusRows = files.includes(TSV) ? parseTsv(readFile(TSV)) : [];
  const r = checkLicenses({ files, noticesText: readFile(NOTICES_FILE), corpusRows, readFile, binary: (f) => isBinary(root, f) });
  for (const f of r.findings) log(f);
  if (r.findings.length) { log(`check-licenses: ${r.findings.length} finding(s)`); return 1; }
  log(`check-licenses: ok — ${r.entries} entr${r.entries === 1 ? 'y' : 'ies'}, ${r.markers} marker(s), ${r.scanned} files scanned`);
  return 0;
}

const isMain = (() => { try { return realpathSync(process.argv[1]) === realpathSync(fileURLToPath(import.meta.url)); } catch { return false; } })();
if (isMain) process.exit(main());
