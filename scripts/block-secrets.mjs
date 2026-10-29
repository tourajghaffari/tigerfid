#!/usr/bin/env node
// block-secrets.mjs — PreToolUse hook: deny a `git commit` that would stage a
// provider credential (GitHub token, AWS key, Stripe live key, Google key, etc.).
//
// Wired from a repo's .claude/settings.json (PreToolUse, matcher "Bash" —
// "Bash|PowerShell" once Story 73.1.2 lands, exec form: "command":"node" +
// args — no shell on any platform). Receives the PreToolUse payload on stdin;
// when the shell-tool command (Bash, or the PowerShell tool — Story 73.1.1:
// only the tool-name gate widened, the commit detection and the staged-diff
// scan are shell-agnostic) is a git commit, it scans the STAGED diff (plus
// the tracked working-tree diff for `commit -a`) for known-provider secret
// patterns and emits permissionDecision "deny" with the finding REDACTED.
// Sibling of block-commit-on-main.mjs; same wiring model.
//
// Detection is deliberately provider-pattern-based (ghp_/AKIA/sk_live_/AIza/…),
// not entropy-based, to keep false positives near zero on Rails/JS repos. Files
// whose basename is a .env EXAMPLE/SAMPLE/TEMPLATE are skipped — those are meant
// to be committed with placeholder values. A line carrying the marker
// `secret-scan:allow` is intentionally exempt (test fixtures, a third-party
// public key, a documented example) — the deliberate opt-out.
//
// Known limits (accepted, same spirit as block-commit-on-main.mjs): it scans the
// state git can see when the hook fires, so a secret staged *inside the same
// compound command* (`git add x && git commit`) isn't visible yet; and it only
// covers Claude Code sessions. The unbypassable complements are a native git
// pre-commit hook (covers every commit) and GitHub push protection (server-side).
// `findSecrets`/`scanDiff`/`scanStaged` are exported so a pre-commit shim can reuse the core.
//
// Three modes: PreToolUse stdin hook (default), `--pre-commit` (native git
// hook; fails CLOSED on git failure — Story 26.1.1), and `--repo-scan` (CI
// backstop: scans all tracked files' CONTENTS via git ls-files, catching what
// an unconfigured clone already committed — Story 26.1.2).
//
// Test manually (stage a fake secret first, then):
//   echo '{"tool_name":"Bash","cwd":".","tool_input":{"command":"git commit -m x"}}' \
//     | node scripts/block-secrets.mjs
//   The PowerShell tool gets the same deny (Story 73.1.1):
//   echo '{"tool_name":"PowerShell","cwd":".","tool_input":{"command":"git commit -m x"}}' \
//     | node scripts/block-secrets.mjs

import { readFileSync, realpathSync } from 'node:fs';
import { execFileSync } from 'node:child_process';
import { fileURLToPath } from 'node:url';

// Provider-specific patterns (low false-positive). Order is display order.
const PATTERNS = [
  ['github-token', /\b(?:ghp|gho|ghu|ghs|ghr)_[A-Za-z0-9]{36}\b/g],
  ['github-pat', /\bgithub_pat_[A-Za-z0-9_]{40,}\b/g],
  ['aws-access-key', /\b(?:AKIA|ASIA)[0-9A-Z]{16}\b/g],
  ['stripe-live-key', /\b(?:sk|rk)_live_[A-Za-z0-9]{20,}\b/g],
  ['stripe-test-key', /\bsk_test_[A-Za-z0-9]{20,}\b/g],
  ['google-api-key', /\bAIza[0-9A-Za-z_-]{35}\b/g],
  ['google-oauth-secret', /\bGOCSPX-[A-Za-z0-9_-]{20,}\b/g],
  ['slack-token', /\bxox[baprs]-[A-Za-z0-9-]{10,}\b/g],
  ['private-key-block', /-----BEGIN (?:[A-Z]+ )?PRIVATE KEY-----/g],
  ['basic-auth-url-cred', /\b[a-z][a-z0-9+.-]*:\/\/[A-Za-z0-9._~%-]+:[^@\s/]{6,}@/g],
];

// Obvious non-secrets: docs placeholders and example values.
const PLACEHOLDER = /x{6,}|X{6,}|REDACTED|EXAMPLE|placeholder|your[-_]?|dummy|\.\.\.|<[A-Za-z]/;

// A .env EXAMPLE/SAMPLE/TEMPLATE file is meant to be committed with placeholders.
const EXAMPLE_ENV = /(^|\/)\.env\.(example|sample|template|dist)$/i;

// Inline escape hatch: a line carrying this marker is intentionally exempt
// (test fixtures, a third-party public key, a documented example). Deliberate
// opt-out, mirroring gitleaks:allow / detect-secrets' allowlist pragma.
const ALLOW = /secret-scan:allow\b/;

function redact(secret) {
  const head = secret.startsWith('-----BEGIN') ? 'PRIVATE KEY block' : secret.slice(0, 8);
  return `${head}…[REDACTED len=${secret.length}]`;
}

// Scan a blob of text for provider secrets. Returns [{type, redacted}].
// The ALLOW marker exempts ONLY the line that carries it — applied per line
// (the documented semantics), so one marker never exempts a whole blob. This
// matters because the core is exported: a pre-commit shim scanning whole
// files would otherwise fail open on the first fixture marker
// (lawcuments.com Story 4.6.4 → hub routing).
export function findSecrets(text) {
  const out = [];
  for (const line of text.split('\n')) {
    if (ALLOW.test(line)) continue;
    for (const [type, re] of PATTERNS) {
      re.lastIndex = 0;
      let m;
      while ((m = re.exec(line))) {
        const s = m[0];
        if (PLACEHOLDER.test(s)) continue;
        out.push({ type, redacted: redact(s) });
      }
    }
  }
  return out;
}

// Scan a unified diff: only ADDED lines (`+`), tracking the current file so
// .env example files can be skipped. Returns [{type, redacted, file}], deduped.
export function scanDiff(diffText) {
  const seen = new Set();
  const out = [];
  let file = '';
  for (const line of diffText.split('\n')) {
    if (line.startsWith('+++ ')) {
      file = line.slice(4).replace(/^b\//, '').trim();
      continue;
    }
    if (!line.startsWith('+') || line.startsWith('+++')) continue;
    if (EXAMPLE_ENV.test(file)) continue;
    for (const hit of findSecrets(line.slice(1))) {
      const key = `${hit.type}|${file}|${hit.redacted}`;
      if (seen.has(key)) continue;
      seen.add(key);
      out.push({ ...hit, file });
    }
  }
  return out;
}

// Shared: scan the staged diff at `cwd`. Returns the hits array. With
// failClosed, a git failure THROWS instead of degrading to an empty diff —
// the pre-commit layer must block when it cannot scan (an index lock, a
// maxBuffer overflow, a broken git binary; Story 26.1.1). The PreToolUse
// hook path stays permissive by design: it is the warn-level layer and the
// native pre-commit hook is the one whose header promises every commit — so
// an overflow there degrades to an empty scan, and the pre-commit read is the
// one that refuses.
// Story 244.1.20: both diffs take --diff-filter=d, so a DELETED file adds no
// line to the buffer — scanDiff reads `+` lines only, and a vendored tree
// removed in one commit (tigerfid's 9,472 paths, a ~233 MB diff of `-` lines)
// overflowed the 64 MiB buffer and was refused UNSCANNED for bytes the scan
// never reads. An ADDED change larger than the buffer still fails closed.
// `maxBuffer` is injectable for the tests only — there is no environment knob:
// a security hook keeps none.
export function scanStaged(cwd, includeUnstagedTracked, { failClosed = false, maxBuffer = 64 * 1024 * 1024 } = {}) {
  const git = (args) => {
    try {
      return execFileSync('git', ['-C', cwd, ...args], { encoding: 'utf8', maxBuffer });
    } catch (err) {
      if (failClosed) throw err;
      return '';
    }
  };
  // Story 244.1.39: the diff git prints with no colour, no external driver and no textconv —
  // under `color.ui=always` (or `color.diff=always`) every added line read `\u001b[32m+…`, so
  // scanDiff's `+`-line read matched none and a staged credential passed (the Hub Sprint 70
  // review's finding 1, MEASURED). One dotfile line must never turn the scan off.
  const plain = ['--no-color', '--no-ext-diff', '--no-textconv'];
  let diff = git(['diff', ...plain, '--cached', '--unified=0', '--diff-filter=d']);
  if (includeUnstagedTracked) diff += '\n' + git(['diff', ...plain, '--unified=0', '--diff-filter=d']);
  return scanDiff(diff);
}

// ---- native git pre-commit mode: `node block-secrets.mjs --pre-commit` ----
// Covers every commit (human or AI), unlike the PreToolUse hook. Enable per
// clone with `git config core.hooksPath .githooks`.
function preCommit() {
  let hits;
  try {
    hits = scanStaged(process.cwd(), false, { failClosed: true });
  } catch (err) {
    // First line only: execFileSync's message embeds the child's whole stderr
    // (git's usage dump), and the child's stderr already passed through above.
    const firstLine = String(err?.message || err).split('\n')[0].trim();
    console.error(
      'block-secrets: git failed — refusing to commit UNSCANNED changes (fail closed):\n' +
        `  ${firstLine}\n` +
        'Fix the git error and retry; the scan must actually run to clear a commit.'
    );
    process.exit(1);
  }
  if (hits.length === 0) process.exit(0);
  const list = hits.map((h) => `  • ${h.type} in ${h.file} (${h.redacted})`).join('\n');
  console.error(
    'block-secrets: refusing to commit — the staged changes contain what looks like a live credential:\n' +
      list +
      "\nMove it to a gitignored .env / env var and unstage it. If it's a false positive " +
      '(placeholder or third-party public key), add a `secret-scan:allow` comment on the line, ' +
      'or bypass this one commit with `git commit --no-verify`.'
  );
  process.exit(1);
}

// ---- CI backstop mode: `node block-secrets.mjs --repo-scan` ----
// Scans the CONTENTS of every tracked file (git ls-files), not a diff — only
// this shape catches what an unconfigured clone already committed (hooksPath
// never set, so neither hook layer above ever ran; lawcuments.com Story 6.2.5
// → hub Story 26.1.2). Detection semantics stay identical to the other modes
// by construction: the findSecrets core + PATTERNS table, the .env
// EXAMPLE/SAMPLE/TEMPLATE basename skip, the per-line secret-scan:allow
// pragma (carrying line only). Findings print REDACTED; exit 1 on any
// finding, 0 clean; no network; git failure fails closed.
function repoScan() {
  let listing;
  try {
    listing = execFileSync('git', ['ls-files', '-z'], { encoding: 'utf8', maxBuffer: 64 * 1024 * 1024 });
  } catch (err) {
    const firstLine = String(err?.message || err).split('\n')[0].trim();
    console.error(`block-secrets --repo-scan: git ls-files failed — cannot scan (fail closed): ${firstLine}`);
    process.exit(1);
  }
  const out = [];
  for (const file of listing.split('\0').filter(Boolean)) {
    if (EXAMPLE_ENV.test(file)) continue;
    let text;
    try {
      text = readFileSync(file, 'utf8');
    } catch (err) {
      if (err?.code === 'ENOENT') continue; // tracked but deleted from the worktree: nothing on disk to scan
      console.error(`block-secrets --repo-scan: cannot read tracked file ${file} (fail closed): ${err?.code || err}`);
      process.exit(1);
    }
    for (const hit of findSecrets(text)) out.push({ ...hit, file });
  }
  if (out.length === 0) process.exit(0);
  const list = out.map((h) => `  • ${h.type} in ${h.file} (${h.redacted})`).join('\n');
  console.error(
    `block-secrets --repo-scan: ${out.length} finding(s) in tracked file contents:\n` +
      list +
      '\nRotate/revoke anything real, purge it from the file, and re-run. False positive? ' +
      'Add a `secret-scan:allow` comment on the flagged line.'
  );
  process.exit(1);
}

// ---- CLI (PreToolUse hook) ----
async function main() {
  if (process.argv.includes('--pre-commit')) return preCommit();
  if (process.argv.includes('--repo-scan')) return repoScan();
  if (process.stdin.isTTY) {
    console.error('block-secrets.mjs expects a PreToolUse JSON payload on stdin.');
    process.exit(1);
  }
  let payload;
  try {
    payload = JSON.parse(await readStdin());
  } catch {
    process.exit(0); // unparseable input: stay out of the way
  }
  if (!payload || typeof payload !== 'object') process.exit(0); // e.g. JSON `null`
  // Story 73.1.1: the harness's shell tools — Bash on both machines,
  // PowerShell on the Windows box (its primary shell). Only the gate widens:
  // the commit detection and the staged-diff scan are shell-agnostic.
  const SHELL_TOOLS = new Set(['Bash', 'PowerShell']);
  if (!SHELL_TOOLS.has(payload.tool_name)) process.exit(0);
  const command = payload.tool_input?.command ?? '';
  if (!/\bgit\b[^|&;<>\n]*\bcommit\b/.test(command)) process.exit(0);

  const cwd = payload.cwd || process.cwd();
  const commitAll = /\bcommit\b[^|&;<>\n]*(-a\b|--all\b|-\w*a)/.test(command);
  const hits = scanStaged(cwd, commitAll);
  if (hits.length === 0) process.exit(0);

  const list = hits.map((h) => `  • ${h.type} in ${h.file} (${h.redacted})`).join('\n');
  console.log(
    JSON.stringify({
      hookSpecificOutput: {
        hookEventName: 'PreToolUse',
        permissionDecision: 'deny',
        permissionDecisionReason:
          'Secret guardrail: the staged changes contain what looks like a live credential — ' +
          'committing it would leak it into git history (see project-plan Epic 8.8). ' +
          'Move the value to an environment variable / gitignored .env and unstage it:\n' +
          list +
          '\nTo proceed: unstage the flagged file (`git restore --staged <path>`) and commit ' +
          'without it. If this looks like a false positive (a placeholder or a third-party ' +
          'public key), STOP and ask the user — only with their explicit confirmation add a ' +
          '`secret-scan:allow` comment on the flagged line and re-stage it.',
      },
    })
  );
}

// Run as a hook only when invoked directly, so tests can import the core.
// Path-compare via realpath + fileURLToPath (the close-lint sibling pattern):
// the old naive `file://` + argv concatenation never matches Windows drive
// paths or percent-encoded paths (spaces), leaving both hook layers silently
// inert — fail-open on exactly the platforms the guard exists for
// (deccentral.com audit Story 4.5.1; reproduced with a spaced checkout).
const argvReal = (() => { try { return process.argv[1] && realpathSync(process.argv[1]); } catch { return null; } })();
if (argvReal === fileURLToPath(import.meta.url)) main();

// Story 128.1.8 (register class stdin-readfilesync-eagain-on-nonblocking-pipe):
// stdin through the async iterator, never readFileSync(0) — a 1.1 MB payload on a
// non-blocking pipe read as unreadable on Linux CI (Node 24, hub #3034's first run).
async function readStdin() {
  const chunks = [];
  for await (const chunk of process.stdin) chunks.push(chunk);
  return Buffer.concat(chunks).toString('utf8');
}
