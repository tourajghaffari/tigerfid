#!/usr/bin/env node
// check-template-antipatterns.mjs — guard against the two grep-able Nunjucks
// silent-render traps that pass a green build (hub KB
// nunjucks-eleventy-silent-render-traps). Both fail the check regardless of
// whether anyone recalls the KB. (Trap #3, layout-chain `{% set %}` scope, isn't
// detectable by pattern and stays KB-only.)
//
//   Trap #2 — `{% for x in seq if cond %}` is Jinja2-only. Nunjucks parses it as
//   `for x in (seq if cond)`; the sequence-conditional evaluates in the outer scope
//   and the intended per-item filter silently does nothing (shipped once: lawcuments
//   Story 1.4.1, PR #29 — green suite, zero iterations).
//
//   Trap #1 — a chained inline conditional `{{ A if c1 else B if c2 }}` (an `else`
//   whose value is itself an `if`). Nunjucks mis-evaluates the chain and can collapse
//   the enclosing include. A single-level ternary `{{ A if c else B }}` is fine;
//   precompute multi-branch logic in a guarded `{% set %}`.
//
// Comment-aware by design: Eleventy runs Nunjucks BEFORE markdown, so in a .md file
// even a backticked `{% … %}` is live template code — the only inert ways to WRITE
// the pattern (to document it) are a {# … #} comment or a {% raw %}…{% endraw %}
// block. We strip those two, preserving line numbers, then scan what's left. So
// warning comments that document this trap don't trip the check.
//
// This is the hub-canonical INTERIM form of the guard (hub Story 2.5.1), synced
// fleet-wide via scripts/sync-session-wrap.mjs — a bridge until the sites adopt
// @fgl/vituno, whose Story 1.3.2 is the durable home (each site's copy
// retires at that site's framework adoption — the framework promotion rule).
// Sourced from the lawcuments reference (test/template-antipatterns.test.js,
// lawcuments PR #32) with the semantics preserved exactly; self-tested at the hub
// source (test/check-template-antipatterns.test.mjs + test/fixtures/).
//
// How to run (from a site repo):
//   node scripts/check-template-antipatterns.mjs             # scans src/
//   node scripts/check-template-antipatterns.mjs --dir path  # scan another tree
//   node scripts/check-template-antipatterns.mjs --skip assets,vendor
// Exit codes: 0 clean · 1 offenders found · 2 usage/missing dir (fails closed).

import { readdirSync, readFileSync, statSync, existsSync, realpathSync } from 'node:fs';
import path from 'node:path';
import { fileURLToPath } from 'node:url';

const SCAN_EXT = new Set(['.njk', '.md']);
// src/assets/ is passthrough-copied, not Eleventy input, on every FGL site.
const DEFAULT_SKIP_DIRS = ['assets'];

// Blank out inert regions (keeping newlines so line numbers stay accurate) so that
// documentation OF the anti-pattern isn't mistaken for a use of it.
export function stripInert(source) {
  const blank = (m) => m.replace(/[^\n]/g, ' ');
  return source
    .replace(/\{#[\s\S]*?#\}/g, blank) // {# … #} Nunjucks comments
    .replace(/\{%-?\s*raw\s*-?%\}[\s\S]*?\{%-?\s*endraw\s*-?%\}/g, blank); // {% raw %}…{% endraw %}
}

// A `{% for … if … %}` tag. `[^%]+?` keeps the match inside a single tag, so a
// for-tag and a separate {% if %} tag on the same line are NOT falsely joined.
const FOR_IF = /\{%-?\s*for\s+[^%]+?\sif\s[^%]+?-?%\}/;

// A `{{ … }}` output expression, and the chained-conditional tell within one: an
// `else` whose value is itself an `if` (`A if c1 else B if c2`). A single ternary
// (`A if c else B`) has no `if` after its `else`, so it is not flagged.
const OUTPUT = /\{\{-?[\s\S]*?-?\}\}/g;
const CHAINED_INLINE_IF = /\belse\b[\s\S]*?\bif\b/;

function lineOf(source, index) {
  return source.slice(0, index).split('\n').length;
}

// Scan one template source. Returns { forIf: [{line, text}], chainedIf: [{line, text}] }.
export function scanSource(source) {
  const live = stripInert(source);
  const forIf = [];
  live.split('\n').forEach((line, i) => {
    if (FOR_IF.test(line)) forIf.push({ line: i + 1, text: line.trim() });
  });
  const chainedIf = [];
  for (const m of live.matchAll(OUTPUT)) {
    if (CHAINED_INLINE_IF.test(m[0])) {
      chainedIf.push({ line: lineOf(live, m.index), text: m[0].replace(/\s+/g, ' ').trim() });
    }
  }
  return { forIf, chainedIf };
}

export function templateFiles(dir, skipDirs = DEFAULT_SKIP_DIRS) {
  const skip = new Set(skipDirs);
  const out = [];
  for (const name of readdirSync(dir)) {
    const p = path.join(dir, name);
    if (statSync(p).isDirectory()) {
      if (!skip.has(name)) out.push(...templateFiles(p, skipDirs));
    } else if (SCAN_EXT.has(path.extname(name))) {
      out.push(p);
    }
  }
  return out;
}

// Scan a tree. Returns [{file, line, trap: 'for-if'|'chained-inline-if', text}].
export function scanTree(dir, skipDirs = DEFAULT_SKIP_DIRS) {
  const offenders = [];
  for (const file of templateFiles(dir, skipDirs)) {
    const { forIf, chainedIf } = scanSource(readFileSync(file, 'utf8'));
    for (const o of forIf) offenders.push({ file, line: o.line, trap: 'for-if', text: o.text });
    for (const o of chainedIf)
      offenders.push({ file, line: o.line, trap: 'chained-inline-if', text: o.text });
  }
  return offenders;
}

const GUIDANCE = {
  'for-if':
    "Nunjucks does not support Jinja2's `{% for x in seq if cond %}` — it silently iterates " +
    'zero times. Filter the sequence instead: `seq | selectattr("attr")` or an inner `{% if %}`. ' +
    'See hub KB nunjucks-eleventy-silent-render-traps (trap #2).',
  'chained-inline-if':
    'Nunjucks mis-evaluates a chained inline conditional `{{ A if c1 else B if c2 }}` and can ' +
    'collapse the enclosing include. Keep inline `{{ if }}` single-level and precompute ' +
    'multi-branch logic in a guarded `{% set %}`. ' +
    'See hub KB nunjucks-eleventy-silent-render-traps (trap #1).',
};

// --- CLI ---

// Hardened isMain (Story 26.1.4): realpath the argv side so symlink-routed
// invocation (macOS tmpdir, symlinked checkouts) can't turn the checker into
// a silent exit 0 (KB macos-tmpdir-symlink-spawn-tests).
const argvReal = (() => { try { return process.argv[1] && realpathSync(process.argv[1]); } catch { return null; } })();
const isMain = argvReal === fileURLToPath(import.meta.url);
if (isMain) {
  const args = process.argv.slice(2);
  let dir = 'src';
  let skipDirs = DEFAULT_SKIP_DIRS;
  for (let i = 0; i < args.length; i++) {
    if (args[i] === '--dir' && args[i + 1]) dir = args[++i];
    else if (args[i] === '--skip' && args[i + 1]) skipDirs = args[++i].split(',').filter(Boolean);
    else {
      console.error(`check-template-antipatterns: unknown argument ${args[i]}`);
      console.error('usage: node scripts/check-template-antipatterns.mjs [--dir <path>] [--skip <dir,dir>]');
      process.exit(2);
    }
  }

  if (!existsSync(dir)) {
    // Fail closed: a missing input dir means the check is miswired, not that the
    // templates are clean.
    console.error(`check-template-antipatterns: scan dir does not exist: ${dir}`);
    process.exit(2);
  }

  const offenders = scanTree(dir, skipDirs);
  if (offenders.length === 0) {
    console.log(`check-template-antipatterns: clean (${dir})`);
    process.exit(0);
  }
  for (const trap of ['for-if', 'chained-inline-if']) {
    const hits = offenders.filter((o) => o.trap === trap);
    if (hits.length === 0) continue;
    console.error(GUIDANCE[trap]);
    for (const o of hits) console.error(`  ${o.file}:${o.line}: ${o.text}`);
  }
  process.exit(1);
}
