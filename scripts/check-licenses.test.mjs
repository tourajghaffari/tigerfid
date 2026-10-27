// scripts/check-licenses.test.mjs — tests for scripts/check-licenses.mjs
// (plan Story 1.1.4). Every case runs in memory: a file list, a notices text
// and a reader, so nothing here touches git or the disk.
// How to run: node --test scripts/check-licenses.test.mjs
import { test } from 'node:test';
import assert from 'node:assert/strict';
import { ALLOWLIST, checkLicenses, isExcluded, parseNotices } from './check-licenses.mjs';

const N = 'THIRD_PARTY_NOTICES.md';
const PIN = `https://github.com/example/board/tree/${'a'.repeat(40)}`;
const entry = (name, { license = 'CERN-OHL-P-2.0 (© Example Ltd)', pin = PIN, source = 'https://github.com/example/board', order } = {}) => {
  const lines = { Source: source, Pin: pin, License: license, Taken: 'the matching network', Landed: 'hardware/reader/match.kicad_sch' };
  return [`### ${name}`, ...(order ?? Object.keys(lines)).map((k) => `- ${k}: ${lines[k]}`)].join('\n');
};
const notices = (...entries) => `# Third-party notices\n\nSee ${N}.\n\n## Standing notes\n\n### The KiCad standard libraries — CC-BY-SA-4.0\n\nprose\n\n## Entries\n\n${entries.join('\n\n')}\n`;
const marker = (project, license = 'CERN-OHL-P-2.0', author = 'Example Ltd') => `# Derived from ${project} (${license} © ${author}) — see ${N}`;
const run = (files, noticesText, extra = {}) => checkLicenses({
  files: Object.keys(files), noticesText, readFile: (f) => files[f], binary: () => false, ...extra,
});

test('the allowlist is tigerian\'s, widened for hardware sources', () => {
  for (const id of ['MIT', 'Apache-2.0', 'BSD-2-Clause', 'BSD-3-Clause', 'ISC', '0BSD', 'Unlicense', 'CC0-1.0', 'CERN-OHL-P-2.0', 'CC-BY-4.0']) assert.ok(ALLOWLIST.has(id), id);
  for (const id of ['CC-BY-SA-4.0', 'GPL-3.0-only', 'CERN-OHL-S-2.0', 'CERN-OHL-W-2.0', 'MPL-2.0']) assert.ok(!ALLOWLIST.has(id), id);
});

test('an empty lane passes, and a standing note is never read as an entry', () => {
  const r = run({ 'scripts/a.mjs': 'ok\n' }, notices());
  assert.deepEqual(r.findings, []);
  assert.equal(r.entries, 0);
});

test('a marker and its entry pass both ways, a .license sidecar included', () => {
  const r = run({ 'hardware/reader/match.kicad_sch.license': marker('Example board') }, notices(entry('Example board')));
  assert.deepEqual(r.findings, []);
  assert.equal(r.markers, 1);
});

test('a marker without an entry, and an entry without a marker, each fail', () => {
  assert.match(run({ 'a.c': marker('Ghost') }, notices()).findings.join('\n'), /marker for "Ghost" has no ### entry/);
  assert.match(run({ 'a.c': 'nothing\n' }, notices(entry('Orphan'))).findings.join('\n'), /entry "Orphan" has no marker/);
});

test('a malformed mention and a licence mismatch each fail', () => {
  assert.match(run({ 'a.c': `// see ${N}` }, notices()).findings.join('\n'), /not a well-formed marker/);
  assert.match(run({ 'a.c': marker('Example board', 'MIT') }, notices(entry('Example board'))).findings.join('\n'), /marker says "MIT © Example Ltd"/);
});

test('an entry off the allowlist, with a bad pin, or out of order fails', () => {
  assert.match(parseNotices(notices(entry('Lib', { license: 'CC-BY-SA-4.0 (© KiCad)' }))).problems.join('\n'), /CC-BY-SA-4.0 is not on the allowlist/);
  assert.match(parseNotices(notices(entry('X', { pin: 'v1.2' }))).problems.join('\n'), /Pin must be/);
  assert.deepEqual(parseNotices(notices(entry('X', { pin: `sha256:${'b'.repeat(64)}` }))).problems, []);
  assert.match(parseNotices(notices(entry('X', { order: ['Pin', 'Source', 'License', 'Taken', 'Landed'] }))).problems.join('\n'), /out of order/);
});

test('a study-only corpus source and the original are refused as sources', () => {
  const rows = [
    { kind: 'file', name: 'activewaveinc/docroot/pdf/CardTagv11.pdf', url: 'https://www.activewaveinc.com/pdf/CardTagv11.pdf', verdict: 'study-only' },
    { kind: 'tree', name: 'activewave-2018', url: 'https://github.com/tourajghaffari/tigerfid', verdict: 'original' },
  ];
  const study = run({ 'a.c': marker('Card') }, notices(entry('Card', { source: 'https://www.activewaveinc.com/pdf/CardTagv11.pdf' })), { corpusRows: rows });
  assert.match(study.findings.join('\n'), /verdict study-only — never a copy source/);
  const orig = run({ 'a.c': marker('Orig') }, notices(entry('Orig', { source: 'https://github.com/tourajghaffari/tigerfid.git' })), { corpusRows: rows });
  assert.match(orig.findings.join('\n'), /needs no entry/);
});

test('docs, markdown, the notices file and the synced canonicals are outside the scan', () => {
  for (const f of ['docs/close/1.1.json', 'readme.md', N, 'scripts/check-licenses.mjs']) assert.ok(isExcluded(f), f);
  assert.ok(!isExcluded('hardware/reader/match.kicad_sch.license'));
  const files = { 'scripts/sync-provenance.json': JSON.stringify({ files: { 'scripts/ci-docs-only.mjs': {} } }), 'scripts/ci-docs-only.mjs': `// names ${N} in passing` };
  assert.deepEqual(run(files, notices()).findings, []);
});
