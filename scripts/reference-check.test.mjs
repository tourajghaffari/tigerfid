// scripts/reference-check.test.mjs — tests for scripts/reference-check.mjs
// (plan Story 1.1.3). Each case builds its own throwaway git repo and folder
// under the OS temp directory, so nothing here reads the real corpus.
// How to run: node --test scripts/reference-check.test.mjs
import { execFileSync } from 'node:child_process';
import { appendFileSync, mkdirSync, mkdtempSync, readFileSync, rmSync, writeFileSync } from 'node:fs';
import { tmpdir } from 'node:os';
import { join } from 'node:path';
import { after, test } from 'node:test';
import assert from 'node:assert/strict';
import { TarExtractor, blobId, main, nameProblem, parseTsv, sha256 } from './reference-check.mjs';

const made = [];
after(() => { for (const d of made) rmSync(d, { recursive: true, force: true }); });
const temp = () => { const d = mkdtempSync(join(tmpdir(), 'reference-check-')); made.push(d); return d; };
const git = (args, cwd) => execFileSync('git', ['-c', 'user.name=test', '-c', 'user.email=test@example.invalid', '-c', 'commit.gpgsign=false', '-c', 'tag.gpgsign=false', ...args], { cwd, stdio: ['ignore', 'pipe', 'pipe'] }).toString('utf8').trim();
const STUB = '# tigerfid-reference — STOP before reading\n\nA test stub.\n';

// A repo whose one commit holds an LF text file and a nested binary file,
// committed with core.autocrlf off, then configured autocrlf=true, so a
// recreate that let the setting through would write CRLF and fail the check.
function fixture() {
  const repo = temp();
  git(['init', '-q', '-b', 'main'], repo);
  git(['config', 'core.autocrlf', 'false'], repo);
  mkdirSync(join(repo, 'deep', 'er'), { recursive: true });
  writeFileSync(join(repo, 'notes.txt'), 'one\ntwo\n');
  writeFileSync(join(repo, 'deep', 'er', 'blob.bin'), Buffer.from([0, 1, 2, 13, 10, 255]));
  git(['add', '-A'], repo);
  git(['commit', '-q', '-m', 'the original'], repo);
  const pin = git(['rev-parse', 'HEAD'], repo);
  git(['tag', '-a', 'orig', '-m', 'the original', pin], repo);
  git(['config', 'core.autocrlf', 'true'], repo);
  const folder = join(temp(), 'ref');
  mkdirSync(join(folder, 'media'), { recursive: true });
  writeFileSync(join(folder, 'media', 'page.html'), '<p>kept</p>\n');
  mkdirSync(join(repo, 'docs'));
  writeFileSync(join(repo, 'docs', 'reference-readme-stub.md'), STUB);
  const rows = [
    '# a comment line',
    'kind\tname\turl\tpin\tlicense\tverdict',
    `tree\torig\t-\t${pin}\tApache-2.0\toriginal`,
    `file\tmedia/page.html\thttps://example.invalid/page\t${sha256(join(folder, 'media', 'page.html'))}\tthird-party\tstudy-only`,
  ];
  writeFileSync(join(repo, 'docs', 'reference-corpus.tsv'), `${rows.join('\n')}\n`);
  return { repo, folder, pin };
}

const run = async (argv, root) => { const lines = []; const code = await main(argv, { root, log: (l) => lines.push(l) }); return { code, out: lines.join('\n') }; };
const lineOf = (out, needle) => out.split('\n').find((l) => l.includes(needle));

// Adds a sources/ row to a fixture: a study source, which the corpus lets a
// builder fetch again from its URL (docs/reference-corpus.tsv's header).
function withSource({ repo, folder }, name = 'sources/datasheet.pdf', url = 'https://example.invalid/datasheet.pdf') {
  mkdirSync(join(folder, 'sources'), { recursive: true });
  writeFileSync(join(folder, name), `the pinned bytes of ${name}\n`);
  appendFileSync(join(repo, 'docs', 'reference-corpus.tsv'), `file\t${name}\t${url}\t${sha256(join(folder, name))}\tthird-party\tstudy-only\n`);
}

test('parseTsv skips comments and maps the header', () => {
  assert.deepEqual(parseTsv('# c\nkind\tname\r\ntree\ta\n'), [{ kind: 'tree', name: 'a' }]);
  assert.throws(() => parseTsv('# only a comment\n'), /no header row/);
});

test('nameProblem refuses names that would leave the folder', () => {
  assert.equal(nameProblem('activewaveinc/rendered/index.html'), null);
  for (const bad of ['', '../x', 'a/../b', 'a//b', 'a\\b', '/abs', 'C:/x', './a']) assert.notEqual(nameProblem(bad), null, bad);
});

test('blobId is git\'s object id for the raw bytes', () => {
  const d = temp();
  const p = join(d, 'crlf.txt');
  writeFileSync(p, 'a\r\nb\r\n');
  assert.equal(blobId(p), execFileSync('git', ['hash-object', '--no-filters', p]).toString().trim());
});

test('a missing tree is recreated byte-true, the README stubbed, and the folder verifies', async () => {
  const { repo, folder } = fixture();
  writeFileSync(join(folder, 'README.md'), '# STOP — reference-only folder for tigerfid\n\nthe adoption-era text\n');
  const first = await run(['--folder', folder], repo);
  assert.equal(first.code, 1);
  assert.match(first.out, /MISSING tree {3}orig — 2 files/);
  assert.match(first.out, /README  readme adoption/);
  const fixed = await run(['--folder', folder, '--recreate', '--write-stub'], repo);
  assert.equal(fixed.code, 0, fixed.out);
  assert.match(fixed.out, /recreated tree orig — 2 files/);
  assert.match(fixed.out, /ok {6}tree {3}orig .* 2 files at their blob ids, none extra/);
  assert.match(fixed.out, /ok {6}files {2}media\/ {2}1 of 1 at their sha256/);
  assert.equal(readFileSync(join(folder, 'orig', 'notes.txt'), 'utf8'), 'one\ntwo\n'); // LF, despite autocrlf=true
  assert.equal(readFileSync(join(folder, 'README.md'), 'utf8'), STUB);
});

test('drift, extras, unpinned files and sidecars are each a finding', async () => {
  const { repo, folder } = fixture();
  writeFileSync(join(folder, 'README.md'), STUB);
  assert.equal((await run(['--folder', folder, '--recreate'], repo)).code, 0);
  appendFileSync(join(folder, 'orig', 'notes.txt'), 'three\n');
  writeFileSync(join(folder, 'orig', 'stray.txt'), 'x');
  writeFileSync(join(folder, 'media', 'new.html'), 'y');
  writeFileSync(join(folder, 'media', '.DS_Store'), 'z');
  const r = await run(['--folder', folder], repo);
  assert.equal(r.code, 1);
  assert.match(r.out, /changed notes\.txt/);
  assert.match(r.out, /extra {3}stray\.txt/);
  assert.match(r.out, /UNPINNED file {2}media\/new\.html/);
  assert.match(r.out, /SIDECARS 1/);
});

test('a moved tag and a present tree are never written over', async () => {
  const { repo, folder, pin } = fixture();
  writeFileSync(join(repo, 'later.txt'), 'later\n');
  git(['add', 'later.txt'], repo);
  git(['commit', '-q', '-m', 'later'], repo);
  git(['tag', '-f', '-a', 'orig', '-m', 'moved'], repo);
  const r = await run(['--folder', folder], repo);
  assert.match(r.out, new RegExp(`MOVED {3}tag {4}orig .* pin ${pin.slice(0, 12)}`));
  mkdirSync(join(folder, 'orig'));
  const again = await run(['--folder', folder, '--recreate'], repo);
  assert.equal(again.code, 1);
  assert.match(again.out, /DRIFT {3}tree {3}orig/);
});

// Story 2.1.11: a sources/ file may be fetched again from its row's URL, so
// the check names that route first and the copy as the alternative.
test('a missing sources/ file names its row\'s URL, the sha256 rule and the copy route', async () => {
  const f = fixture();
  withSource(f);
  rmSync(join(f.folder, 'sources', 'datasheet.pdf'));
  const r = await run(['--folder', f.folder], f.repo);
  assert.equal(r.code, 1);
  const line = lineOf(r.out, 'MISSING file   sources/datasheet.pdf');
  assert.ok(line, r.out);
  assert.match(line, /fetch it from its row's URL, https:\/\/example\.invalid\/datasheet\.pdf/);
  assert.match(line, /counts only once its sha256 matches the pin/);
  assert.match(line, /copy it, never move it, from a machine whose folder verifies/);
  assert.doesNotMatch(line, /copy-only/);
});

test('an absent sources/ folder names the fetch route for its files', async () => {
  const f = fixture();
  withSource(f);
  withSource(f, 'sources/note.pdf', 'https://example.invalid/note.pdf');
  withSource(f, 'sources/unlinked.pdf', '-');
  rmSync(join(f.folder, 'sources'), { recursive: true });
  const r = await run(['--folder', f.folder], f.repo);
  assert.equal(r.code, 1);
  const line = lineOf(r.out, 'MISSING folder sources/');
  assert.ok(line, r.out);
  assert.match(line, /sources\/ — 3 files; fetch each from its row's URL \(below\)/);
  assert.match(line, /counts only once its sha256 matches the pin/);
  assert.match(line, /copy the folder whole, never move it, from a machine whose folder verifies/);
  assert.match(r.out, /^ {10}sources\/datasheet\.pdf {2}https:\/\/example\.invalid\/datasheet\.pdf$/m);
  assert.match(r.out, /^ {10}sources\/note\.pdf {2}https:\/\/example\.invalid\/note\.pdf$/m);
  assert.match(r.out, /^ {10}sources\/unlinked\.pdf {2}no URL in its row: copy it$/m);
});

test('a missing file elsewhere, or a sources/ row with no URL, keeps the copy-only line', async () => {
  const f = fixture();
  withSource(f, 'sources/unlinked.pdf', '-');
  rmSync(join(f.folder, 'media', 'page.html'));
  rmSync(join(f.folder, 'sources', 'unlinked.pdf'));
  const r = await run(['--folder', f.folder], f.repo);
  assert.equal(r.code, 1);
  assert.equal(lineOf(r.out, 'media/page.html'), 'MISSING file   media/page.html (copy-only — copy it, never move it, from a machine whose folder verifies)');
  assert.equal(lineOf(r.out, 'sources/unlinked.pdf'), 'MISSING file   sources/unlinked.pdf (copy-only — copy it, never move it, from a machine whose folder verifies)');
  rmSync(join(f.folder, 'media'), { recursive: true });
  const gone = await run(['--folder', f.folder], f.repo);
  assert.equal(lineOf(gone.out, 'MISSING folder media/'), 'MISSING folder media/ — 1 files, copy-only: copy the folder whole, never move it, from a machine whose folder verifies');
});

test('the usage and an absent folder exit 2', async () => {
  const { repo, folder } = fixture();
  assert.equal((await run(['--nope'], repo)).code, 2);
  assert.equal((await run(['--folder'], repo)).code, 2);
  assert.equal((await run(['--folder', join(folder, 'absent')], repo)).code, 2);
});

// A one-entry ustar block with a correct checksum.
function tarHeader(name, size, type = '0') {
  const h = Buffer.alloc(512);
  h.write(name, 0, 100, 'utf8');
  h.write('0000644\0', 100); h.write('0000000\0', 108); h.write('0000000\0', 116);
  h.write(`${size.toString(8).padStart(11, '0')}\0`, 124);
  h.write('00000000000\0', 136);
  h.write(type, 156);
  h.write('ustar\0', 257); h.write('00', 263);
  h.fill(32, 148, 156);
  let sum = 0;
  for (const b of h) sum += b;
  h.write(`${sum.toString(8).padStart(6, '0')}\0 `, 148);
  return h;
}

test('the tar reader refuses an entry that would leave its folder, and a damaged header', () => {
  assert.throws(() => new TarExtractor(temp()).push(tarHeader('../escape.txt', 0)), /refused/);
  const bad = tarHeader('ok.txt', 0);
  bad[0] ^= 1;
  assert.throws(() => new TarExtractor(temp()).push(bad), /checksum/);
  assert.throws(() => new TarExtractor(temp()).push(tarHeader('link', 0, '2')), /only directories and regular files/);
});

test('the tar reader writes a file split across chunks and needs the end blocks', () => {
  const d = temp();
  const body = Buffer.from('hello, split world\n');
  const tar = Buffer.concat([tarHeader('a/b.txt', body.length), body, Buffer.alloc(512 - body.length), Buffer.alloc(1024)]);
  const ex = new TarExtractor(d);
  for (let i = 0; i < tar.length; i += 7) ex.push(tar.subarray(i, i + 7));
  ex.end();
  assert.equal(readFileSync(join(d, 'a', 'b.txt'), 'utf8'), body.toString());
  const short = new TarExtractor(temp());
  short.push(tar.subarray(0, 512 + 512));
  assert.throws(() => short.end(), /ended early/);
});
