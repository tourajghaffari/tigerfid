// Per-repo close-protocol lint (KB epic-close-protocol; spec 2026-07-05 §2.3/§2.5).
// SYNCED CANONICAL: this file is distributed verbatim to every repo under projects/
// and must stay single-file and dependency-free. It lints THIS repo only:
//   - docs/close/protocol.json absent  -> not adopted, exit 0 (adoption-gated)
//   - adopted -> every open epic has a "Close out Epic N.M" story and a parsing
//     manifest whose `stories` resolve against plan ∪ archive ∪ Accepted-risks
//     register; duplicate check ids flagged; post-adoption archived epics must
//     carry a "**Close evidence (" block.
//   - WARN arm (Story 215.1.2): a manifest `run` string that pipes a PRODUCER into
//     anything without gating its exit prints `close-lint WARN:` lines and the summary
//     counts them — the exit is unchanged (a warn is a reading, never a gate).
// How to run: node scripts/close-lint.mjs   (exit 1 on any failure; warnings never fail)

import { readFileSync, readdirSync, existsSync, realpathSync, statSync } from 'node:fs';
import { execFileSync } from 'node:child_process';
import path from 'node:path';
import { fileURLToPath } from 'node:url';

const EPIC_RE = /^### Epic (\d+(?:\.\d+)*): (.+)$/;
const STORY_RE = /^#### Story (\d+(?:\.\d+)*)[: ](.*)$/;
const RISK_RE = /^#### Accepted risk: Story (\d+(?:\.\d+)*)/;
const INITIATIVE_RE = /^## Initiative (\d+)\b/;

export function parsePlan(planText) {
  const epics = [];
  const initiatives = [];
  const registerStories = new Set();
  let cur = null;
  let curInit = null;
  // \r?-tolerant split: on a Windows autocrlf worktree the plan arrives CRLF; a
  // bare '\n' split leaves \r on every line, the $-anchored heading regexes then
  // match ZERO epics/stories, and the lint reports ok having verified nothing.
  for (const line of (planText ?? '').split(/\r?\n/)) {
    if (/^## /.test(line)) {
      cur = null;
      const im = line.match(INITIATIVE_RE);
      curInit = im ? { num: im[1], epics: [] } : null;
      if (curInit) initiatives.push(curInit);
      continue;
    }
    const e = line.match(EPIC_RE);
    if (e) {
      cur = { num: e[1], title: e[2].trim(), stories: [], hasCloseStory: false };
      epics.push(cur);
      if (curInit) curInit.epics.push(e[1]);
      continue;
    }
    const r = line.match(RISK_RE);
    if (r) { registerStories.add(r[1]); continue; }
    const s = line.match(STORY_RE);
    if (s && cur) {
      const tombstone = /➡ RELOCATED/.test(s[2]);
      const isClose = s[2].includes(`Close out Epic ${cur.num}`);
      cur.stories.push({ num: s[1], tombstone, isClose });
      if (isClose) cur.hasCloseStory = true;
    }
  }
  return { epics, initiatives, registerStories };
}

export function archiveStoryNums(archiveText) {
  return new Set([...(archiveText ?? '').matchAll(/^#### Story (\d+(?:\.\d+)*)/gm)].map((m) => m[1]));
}

export function archiveEpicBlocks(archiveText) {
  const out = [];
  const parts = (archiveText ?? '').split(/^(?=### Epic )/m);
  for (const part of parts) {
    const h = part.match(/^### Epic (\d+(?:\.\d+)*): /);
    if (!h) continue;
    const body = part.split(/^## /m)[0];
    const completed = body.match(/\*\*Completed:\*\* (\d{4}-\d{2}-\d{2})/)?.[1] ?? null;
    out.push({ num: h[1], completed, body });
  }
  return out;
}

// --- Story 24.3.8: the ONE close-manifest dialect, schema-enforced ---
// The dialect (executor-decided 2026-07-30 against divergence-register theme
// 4's inventory + the resumed pre-flight's corpus census, 273/4/11): the four
// observed top-level key-set dialects were OPTIONALITY of the two dated
// fields, and the 11 check shapes were five kinds + annotations — so ONE
// schema legalizes the whole corpus and the fleet round normalizes FORMAT
// (key order + serialization), never semantics (a `reviewed` date can't be
// invented). The expect vocabularies are quoted from runCheck's own contract
// (close-epic.mjs — item 18e: the runner, not a corpus paraphrase), which is
// why glob allows `exclude` (runner-supported, corpus-unused) and prose takes
// `requires` ('omid' routes to blocked-on-omid). `note` is a pure annotation
// runCheck never reads — allowed on every kind. Enforced here (close-lint is
// each repo's lint gate) for EVERY docs/close/<epic>.json, open or archived.
export const MANIFEST_SCHEMA = {
  topLevel: { required: ['epic', 'planRef', 'stories', 'checks'], optional: ['reviewed', 'reviewedFor'] },
  kinds: {
    prose: { expect: null, extras: ['requires'] },
    run: { expect: ['exit', 'stdoutIncludes', 'stdoutMatches'], extras: ['timeoutSec'] },
    file: { expect: ['exists', 'contains', 'absent'], extras: [] },
    glob: { expect: ['count'], extras: ['exclude'] },
    http: { expect: ['status', 'bodyIncludes'], extras: [] },
  },
};

const isoDate = (s) => typeof s === 'string' && /^\d{4}-\d{2}-\d{2}$/.test(s);

// `liveEpics` (Story 86.1.3): the plan's live-epic set — lintRepo hands
// parsePlan's in — so the heading-needle rule fires on LIVE epics' manifests
// only; the bare call (no plan context) validates the schema alone.
export function validateManifest(m, label = 'manifest', { liveEpics = null } = {}) {
  if (typeof m !== 'object' || m === null || Array.isArray(m)) return [`${label}: not a JSON object`];
  const fails = [];
  const { required, optional } = MANIFEST_SCHEMA.topLevel;
  for (const k of required) if (m[k] === undefined) fails.push(`${label}: missing required top-level "${k}"`);
  for (const k of Object.keys(m))
    if (!required.includes(k) && !optional.includes(k)) fails.push(`${label}: unknown top-level key "${k}"`);
  if (m.epic !== undefined && typeof m.epic !== 'string') fails.push(`${label}: "epic" must be a string`);
  if (m.planRef !== undefined && typeof m.planRef !== 'string') fails.push(`${label}: "planRef" must be a string`);
  if (m.stories !== undefined && (!Array.isArray(m.stories) || m.stories.some((s) => typeof s !== 'string')))
    fails.push(`${label}: "stories" must be an array of strings`);
  if (m.reviewed !== undefined && !isoDate(m.reviewed)) fails.push(`${label}: "reviewed" must be "YYYY-MM-DD"`);
  // reviewedFor is an ARRAY of free-text re-affirmation notes (story +
  // context), NOT a story->date map — verified against the live corpus (65
  // instances, e.g. hub 13.1.json), where a first-cut census misread the
  // array as an object via Object.entries.
  if (m.reviewedFor !== undefined && (!Array.isArray(m.reviewedFor) || m.reviewedFor.some((s) => typeof s !== 'string')))
    fails.push(`${label}: "reviewedFor" must be an array of strings (free-text re-affirmation notes)`);
  if (m.checks !== undefined && !Array.isArray(m.checks)) {
    fails.push(`${label}: "checks" must be an array`);
    return fails;
  }
  for (const [i, c] of (m.checks ?? []).entries()) {
    const where = `${label} check[${i}]${c && typeof c.id === 'string' ? ` (${c.id})` : ''}`;
    if (typeof c !== 'object' || c === null || Array.isArray(c)) {
      fails.push(`${where}: not an object`);
      continue;
    }
    if (typeof c.id !== 'string') fails.push(`${where}: missing string "id"`);
    const kinds = Object.keys(MANIFEST_SCHEMA.kinds).filter((k) => c[k] !== undefined);
    if (kinds.length !== 1) {
      fails.push(`${where}: exactly one kind field of prose/run/file/glob/http (found ${kinds.length ? kinds.join('+') : 'none'})`);
      continue;
    }
    const kind = kinds[0];
    const spec = MANIFEST_SCHEMA.kinds[kind];
    if (typeof c[kind] !== 'string') fails.push(`${where}: "${kind}" must be a string`);
    const allowed = new Set(['id', kind, 'note', ...spec.extras, ...(spec.expect ? ['expect'] : [])]);
    for (const k of Object.keys(c)) if (!allowed.has(k)) fails.push(`${where}: key "${k}" not allowed on a ${kind} check`);
    if (spec.expect) {
      if (c.expect === undefined) fails.push(`${where}: a ${kind} check requires "expect"`);
      else if (typeof c.expect !== 'object' || c.expect === null || Array.isArray(c.expect))
        fails.push(`${where}: "expect" must be an object`);
      else {
        const ekeys = Object.keys(c.expect);
        if (ekeys.length === 0) fails.push(`${where}: "expect" is empty`);
        for (const k of ekeys)
          if (!spec.expect.includes(k))
            fails.push(`${where}: expect key "${k}" not allowed on a ${kind} check (allowed: ${spec.expect.join(', ')})`);
        // Story 77.1.1 arm 3: `absent` asserts the FILE is missing — boolean
        // true, nothing else. runCheck reads any value as "must not exist", so
        // `absent: "<text>"` (tigerdash Epic 1.1 close, 2026-09-06 — meant
        // "this text must be gone") is a check born permanently red.
        if (kind === 'file' && 'absent' in c.expect && c.expect.absent !== true)
          fails.push(`${where}: file check absent must be true (boolean), not text — a text-must-be-gone check is a run grep (got ${JSON.stringify(c.expect.absent)}; KB epic-close-protocol § validator rules)`);
        // Story 86.1.3: a heading-literal `contains` needle on a LIVE epic's
        // manifest is refused — prose that names the section satisfies it
        // (register class needle-satisfied-by-prose, seats 2 and 4: the 82.1
        // reviewer-adjudication and the 84.1 sweep-record teeth, both green
        // on a header sentence quoting the heading). A closed epic's manifest
        // is a frozen record and passes; the bare call is unchanged.
        if (kind === 'file' && typeof c.expect.contains === 'string' && /^#{1,6} /.test(c.expect.contains) && liveEpics != null && liveEpics.has(m.epic))
          fails.push(`${where}: file check contains needle ${JSON.stringify(c.expect.contains)} is a Markdown heading literal — prose that quotes the heading satisfies it (KB falsifiable-machine-checks mode seventeen rule (e)); pin a heading with a run check in the line-start form: split the file on LF (String.fromCharCode(10)), count the lines that startsWith the heading, expect exactly one`);
      }
    }
    if (c.timeoutSec !== undefined && typeof c.timeoutSec !== 'number') fails.push(`${where}: "timeoutSec" must be a number`);
    if (c.note !== undefined && typeof c.note !== 'string') fails.push(`${where}: "note" must be a string`);
    if (c.requires !== undefined && typeof c.requires !== 'string') fails.push(`${where}: "requires" must be a string`);
    if (c.exclude !== undefined && typeof c.exclude !== 'string') fails.push(`${where}: "exclude" must be a string`);
  }
  return fails;
}

// --- Story 32.1.3: run strings must deliver the SAME program text under every
// shell (KB run-strings-shell-backslash-divergence). close-epic runs `run`
// checks with shell:true — /bin/sh on POSIX, cmd.exe on win32 (until Story
// 32.1.1's Git Bash runner; cmd.exe remains its loud no-bash fallback) — and
// the two transform escape sequences differently, so one manifest can be green
// on one OS and false-red on the other (ghaffaritabrizi.com 6.3.json, the
// first live case). Three sequences differ in delivered text by PRESENCE:
//   \\  sh collapses to one backslash inside "…"; Windows argv keeps both
//   \$  sh delivers $; Windows keeps \$
//   `   sh = command substitution; Windows = a literal backtick
// The fourth, \", is convergent by itself (sh and the Windows argv rules both
// deliver ") — its hazard is cmd.exe's OWN quote parity: cmd toggles its quote
// state on every ", the escaped one included, and interprets ^ & | < > in the
// spans it then reads as unquoted while sh keeps them quoted. So the \" face is
// EXPOSURE-based: a dual quote-state walk (sh: \ escapes, '…' and "…" quote;
// cmd: every " toggles, ^ escapes when unquoted) flags a shell-special where the
// two states disagree. Static, never executed — the plants in
// test/close-lint.test.mjs red for each sequence's own reason.
const SH_SPECIALS = '^&|<>';

// Story 32.1.4: archived epics whose `**Completed:**` date is on/after this
// cutoff are linted for run-string divergence like live ones — it closes the
// born-archived hole (a close PR adds checks and archives in one diff) without
// re-litigating the sweep-era records: every epic closed on/before 2026-08-16
// was classified by the Sprint 22 sweep + review (10 divergent strings, all
// POSIX-authored, record-and-leave) and stays exempt. ISO dates compare
// lexically. Retune point if a repo ever needs a later floor.
export const RUN_STRING_ARCHIVE_CUTOFF = '2026-08-17';

export function runStringDivergence(run) {
  const s = String(run ?? '');
  const out = [];
  if (s.includes('\\\\')) out.push('`\\\\` (double backslash): sh collapses it to one backslash inside double quotes, Windows argv keeps both — the child receives different program text per shell; build a literal backslash with String.fromCharCode(92) or use a character class');
  if (s.includes('\\$')) out.push('`\\$`: sh delivers `$`, Windows keeps `\\$` — different program text per shell');
  if (s.includes('`')) out.push('backtick: sh treats it as command substitution, Windows as a literal — different program text per shell');
  if (s.includes('\\"')) {
    let shDq = false, shSq = false, shEsc = false, cmdQ = false, cmdEsc = false;
    const exposed = [];
    for (let i = 0; i < s.length; i++) {
      const ch = s[i];
      let shQuoted = shDq || shSq;
      if (shEsc) shEsc = false;
      else if (shSq) { if (ch === "'") shSq = false; }
      else if (ch === '\\') shEsc = true;
      else if (shDq) { if (ch === '"') { shDq = false; shQuoted = true; } }
      else if (ch === '"') { shDq = true; shQuoted = true; }
      else if (ch === "'") { shSq = true; shQuoted = true; }
      let cmdQuoted = cmdQ;
      if (cmdEsc) { cmdEsc = false; cmdQuoted = true; }
      else if (ch === '"') { cmdQ = !cmdQ; cmdQuoted = true; }
      else if (ch === '^' && !cmdQ) { cmdEsc = true; cmdQuoted = false; }
      if (SH_SPECIALS.includes(ch) && shQuoted !== cmdQuoted) exposed.push(`'${ch}' at offset ${i}`);
    }
    if (exposed.length) out.push(`\`\\"\` exposes ${exposed.join(', ')} to cmd.exe: cmd toggles its quote state on the escaped quote and reads the character unquoted where sh keeps it quoted — different program text per shell; write the quote shell-invariantly (\\x22 in JS source) or restructure`);
  }
  return out;
}

// --- Story 215.1.2: a manifest `run` that pipes a producer drops the producer's exit ---
// Register class manifest-run-pipe-drops-the-checker-exit (2 seats): expeal.com
// 1.10.json `approvals-launched` and 19.5.json `approved` each piped a checker into
// `grep -q`, so the tooth PASSED on the very FAIL line it was built to catch — the pipe
// hands the chain the CONSUMER's exit. Decided machine-check 2026-10-19 (hub #3805):
// a WARN, never a FAIL (Hub Sprint 56 D5 — the FAIL face is Omid's word after the
// fleet census read from the round's CI logs). The read: every `run` string the
// 32.1.3 / 32.1.4 passes already walk (live epics; archived epics completed on/after
// PIPE_WARN_ARCHIVE_CUTOFF — this story's own floor, so sweep-era records never warn),
// split at an sh-UNQUOTED single `|` (never `||`, never a `|` inside quotes). The
// predicate: the LEFT stage's command word — the first word after `VAR=x` assignments,
// `env`/`time`/`nohup`, `mise exec --` and `npx` — is not a pure READER, and no `pipefail`
// appears on the line. The reader census (KB regex-gate-syntax-form-census, one fixture
// per form in test/close-lint.test.mjs): cat, echo, printf, ls, find, head, tail, sed,
// grep, wc, sort, uniq, cut, tr; `git show|log|diff|ls-tree|ls-files|rev-parse|cat-file|`
// `status|remote|branch|tag|for-each-ref|ls-remote`; `gh api` and `gh <pr|run|release|`
// `repo|issue> <view|list|checks|diff|status>`. The warn names the producer, the consumer,
// the gate (`<producer> > <file> && <consumer> <file>`, or `set -o pipefail`), the story
// and the class. Static, never executed.
export const PIPE_WARN_ARCHIVE_CUTOFF = '2026-10-19';
const PIPE_READERS = new Set(['cat', 'echo', 'printf', 'ls', 'find', 'head', 'tail', 'sed', 'grep', 'wc', 'sort', 'uniq', 'cut', 'tr']);
const PIPE_GIT_READS = new Set(['show', 'log', 'diff', 'ls-tree', 'ls-files', 'rev-parse', 'cat-file', 'status', 'remote', 'branch', 'tag', 'for-each-ref', 'ls-remote']);
const PIPE_GH_READS = new Set(['view', 'list', 'checks', 'diff', 'status']);
const PIPE_WRAPPERS = new Set(['env', 'time', 'nohup', 'command', 'exec']);
// Shell keywords that open a stage without being its command (`do if node x | grep`).
const PIPE_KEYWORDS = new Set(['if', 'then', 'else', 'elif', 'fi', 'do', 'done', 'while', 'until', 'for', 'case', 'esac', '!', '{', '}']);

// The command word of a stage: shell keywords, assignments, wrappers, `mise exec --` and `npx` skipped;
// the basename of what remains (quotes stripped). Returns null for an empty stage.
function pipeCommandWord(stage) {
  const words = stage.trim().split(/\s+/).filter(Boolean).map((w) => w.replace(/^['"]|['"]$/g, ''));
  let i = 0;
  while (i < words.length && (/^[A-Za-z_][A-Za-z0-9_]*=/.test(words[i]) || PIPE_WRAPPERS.has(words[i]) || PIPE_KEYWORDS.has(words[i]))) i++;
  if (words[i] === 'mise' && words[i + 1] === 'exec') {
    const dash = words.indexOf('--', i + 2);
    if (dash === -1) return null;
    i = dash + 1;
  }
  if (words[i] === 'npx') i++;
  if (i >= words.length) return null;
  const base = words[i].slice(Math.max(words[i].lastIndexOf('/'), words[i].lastIndexOf('\\')) + 1);
  return { word: base, rest: words.slice(i + 1) };
}

function pipeIsReader({ word, rest }) {
  if (PIPE_READERS.has(word)) return true;
  if (word === 'git' || word === 'git.exe') {
    const sub = rest.find((w) => !w.startsWith('-') && w !== '-C' && !/^[A-Za-z]:|^\/|^\./.test(w)) ?? '';
    const idx = rest.indexOf('-C');
    const after = idx === -1 ? rest : [...rest.slice(0, idx), ...rest.slice(idx + 2)];
    const first = after.find((w) => !w.startsWith('-')) ?? sub;
    return PIPE_GIT_READS.has(first);
  }
  // A word test, never an exec target: test/forge-boundary pins every standalone quoted gh
  // literal under scripts/ as a bare forge call, and this census only reads a command word.
  if (/^gh$/.test(word)) {
    const a = rest.filter((w) => !w.startsWith('-'));
    return a[0] === 'api' || (['pr', 'run', 'release', 'repo', 'issue'].includes(a[0]) && PIPE_GH_READS.has(a[1]));
  }
  return false;
}

// Returns [{ producer, consumer }] for every sh-unquoted single pipe whose left stage
// is not a pure reader, or [] when the line carries `pipefail`.
export function runStringPipes(run) {
  const s = String(run ?? '');
  if (/\bpipefail\b/.test(s)) return [];
  const out = [];
  let sq = false, dq = false, esc = false, start = 0;
  for (let i = 0; i < s.length; i++) {
    const ch = s[i];
    if (esc) { esc = false; continue; }
    if (ch === '\\' && !sq) { esc = true; continue; }
    if (sq) { if (ch === "'") sq = false; continue; }
    if (dq) { if (ch === '"') dq = false; continue; }
    if (ch === "'") { sq = true; continue; }
    if (ch === '"') { dq = true; continue; }
    if (ch === '|') {
      if (s[i + 1] === '|') { i++; start = i + 1; continue; }
      const left = pipeCommandWord(s.slice(start, i));
      const rightEnd = s.slice(i + 1).search(/[|;&\n()]/);
      const right = pipeCommandWord(s.slice(i + 1, rightEnd === -1 ? s.length : i + 1 + rightEnd));
      if (left && !pipeIsReader(left)) out.push({ producer: left.word, consumer: right?.word ?? '(nothing)' });
      start = i + 1;
      continue;
    }
    if (ch === '&') {
      // A redirection's ampersand (`2>&1`, `>&`, `&>`) joins its stage; `&&` and a lone `&` split.
      if (s[i - 1] === '>' || s[i - 1] === '<' || s[i + 1] === '>') continue;
      if (s[i + 1] === '&') i++;
      start = i + 1;
      continue;
    }
    if (ch === ';' || ch === '\n' || ch === '(' || ch === ')') start = i + 1;
  }
  return out;
}

function pipeWarning(num, c, p, tail = '') {
  return `docs/close/${num}.json check "${c.id}": run pipes a producer (${p.producer}) into ${p.consumer} — the pipe drops the producer's exit; gate it (${p.producer} > <file> && ${p.consumer} <file>, or set -o pipefail)${tail} (Story 215.1.2; register class manifest-run-pipe-drops-the-checker-exit)`;
}

// --- Story 77.1.1: the manifest cannot contradict the plan ---
// A `reviewedFor` entry is a record a fresh closer reads as evidence, yet the
// lockstep arm asked only that the manifest be TOUCHED: tigerian #507 (a
// multi-session leg that crossed a midnight) ticked two stories
// `✅ DONE (2026-09-06)` while both manifests recorded `DONE (2026-09-05, …)`
// and left `reviewed` at 2026-09-05 — the lint passed, a fresh closer's
// double-check caught it. Two arms, each naming the two values it compared:
//   arm 1 (agreement): an entry in the DONE / TICKED / COMPLETED family — the
//     verbs that assert a plan tick (fleet verb census at filing: EXECUTED,
//     FILED, TICKED, DONE, BORN, COMPLETED) — must name the date the story's
//     heading tick carries in plan ∪ archive, and a story no heading ticks is
//     written ahead of its tick; an undated entry has nothing to compare.
//   arm 2 (monotone): `reviewed` is never older than any dated entry, whatever
//     its verb — a sitting that appends today's entry bumps the stamp. An entry's
//     date for this arm is the NEWER of its dated records and its dated lead
//     (Story 244.1.22, register class lockstep-entry-added-without-the-reviewed-bump:
//     expeal.com #1066's `2026-10-27 — Story 1.10.6 RE-VERSIONED (… Omid's ruling of
//     2026-10-26 …)` was dated by its record's quoted 2026-10-26 alone, and a
//     `reviewed` left at 2026-10-26 reached main); reviewedForRecords below is
//     unchanged, and reviewedForFailures reads the lead beside it.
// A "dated entry" is the `Story X VERB (…)` shape with an ISO date inside the
// parenthesis (the first one) — or, for an entry with no such record, the
// entry's LEAD carrying an ISO date (Story 175.1.1): its text up to the first
// top-level `: `, ` — ` or `; `, its first date the entry's own. The fleet
// writes most entries in other wordings (`Story X ticked DATE …`, `Swept DATE
// by …`, `Born WITH the Story X tick (DATE, …`), and the `Story X VERB (…)`
// reader alone saw 7% of dated entries at filing. Prose dates after the lead
// (a sitting named ahead) never count. Lead records feed arm 2 only: they
// carry no verb, so arm 1 never reads them. Classifier: live epics' manifests,
// plus archived epics completed on/after the cutoff — the 32.1.4 shape, so a
// close PR's born-archived entry is covered without retro-editing sweep-era
// records.
export const REVIEWEDFOR_ARCHIVE_CUTOFF = '2026-09-06';
const TICK_VERBS = new Set(['DONE', 'TICKED', 'COMPLETED']);
const ENTRY_RE = /\bStory (\d+(?:\.\d+)*) ([A-Z][A-Z-]*) \(([^)]*)\)/g;
const ISO_RE = /\d{4}-\d{2}-\d{2}/;
const LEAD_STOPS = [': ', ' — ', '; '];

// An entry's lead: its text up to the first `: `, ` — ` or `; ` outside any
// parenthesis (a parenthesis's own clauses stay inside the lead).
export function reviewedForLead(entry) {
  let depth = 0;
  for (let i = 0; i < entry.length; i++) {
    const c = entry[i];
    if (c === '(') depth++;
    else if (c === ')') depth = Math.max(0, depth - 1);
    else if (depth === 0 && LEAD_STOPS.some((s) => entry.startsWith(s, i))) return entry.slice(0, i);
  }
  return entry;
}

// Every `Story X VERB (…)` record across a manifest's reviewedFor entries, and
// a lead record (verb null) for each entry that has no DATED such record but
// whose lead is dated (`2026-08-04 Story X EXECUTED (Sprint 5 leg 3)` included).
export function reviewedForRecords(reviewedFor) {
  const out = [];
  for (const e of Array.isArray(reviewedFor) ? reviewedFor : []) {
    if (typeof e !== 'string') continue;
    const before = out.length;
    for (const m of e.matchAll(ENTRY_RE)) out.push({ story: m[1], verb: m[2], date: m[3].match(ISO_RE)?.[0] ?? null });
    if (out.slice(before).some((r) => r.date)) continue;
    const lead = reviewedForLead(e);
    const date = lead.match(ISO_RE)?.[0];
    if (date) out.push({ story: lead.match(/\bStory (\d+(?:\.\d+)*)/)?.[1] ?? null, verb: null, date, lead });
  }
  return out;
}

// Story heading ticks across plan ∪ archive: num -> { ticked, date }. The tick
// is the ✅ marker; its date is the first ISO date after it (`✅ DONE
// (2026-09-06)`, `✅ DONE (2026-09-06, fixed-by-evolution)`, `✅ RETIRED (…)`
// — every fleet heading shape at authoring puts the date first). A story
// heading that appears twice (a plan tombstone + the archived original)
// reads as ticked if any copy is. Story 241.1.3 (hub inbox #4259): a story a
// closer relocated to `## Accepted risks` as `#### Accepted risk: Story N.M.K:
// …` is read with the same ✅ and first-ISO-date rule — so a stamped one
// carries its tick without a tombstone `#### Story` copy, and an unstamped one
// reads not ticked (close-lint never invents a tick).
export function storyTicks(...texts) {
  const map = new Map();
  for (const t of texts) {
    for (const line of (t ?? '').split(/\r?\n/)) {
      const s = line.match(STORY_RE);
      const r = s ? null : line.match(RISK_RE);
      if (!s && !r) continue;
      const num = (s ?? r)[1];
      const rest = s ? s[2] : line.slice(r[0].length);
      const at = rest.indexOf('✅');
      const ticked = at !== -1;
      const date = ticked ? (rest.slice(at).match(ISO_RE)?.[0] ?? null) : null;
      const prev = map.get(num);
      if (!prev || (!prev.ticked && ticked)) map.set(num, { ticked, date });
    }
  }
  return map;
}

function reviewedForFailures(num, m, ticks, why = '') {
  const tail = `${why ? ` (${why} — ` : ' ('}Story 77.1.1)`;
  const out = [];
  const reviewed = isoDate(m?.reviewed) ? m.reviewed : null;
  for (const rec of reviewedForRecords(m?.reviewedFor)) {
    if (TICK_VERBS.has(rec.verb)) {
      const t = ticks.get(rec.story);
      if (!t?.ticked) {
        out.push(`docs/close/${num}.json: reviewedFor entry names Story ${rec.story} as ${rec.verb} but no heading carries the tick in plan ∪ archive — written ahead of its tick${tail}`);
      } else if (rec.date && t.date && rec.date !== t.date) {
        out.push(`docs/close/${num}.json: reviewedFor entry for Story ${rec.story} says ${rec.verb} (${rec.date}) but the plan tick reads (${t.date})${tail}`);
      }
    }
    if (reviewed && rec.date && rec.date > reviewed) {
      const which = rec.lead ? `"${rec.lead.length > 80 ? `${rec.lead.slice(0, 80)}…` : rec.lead}"` : `Story ${rec.story} ${rec.verb}`;
      out.push(`docs/close/${num}.json: reviewed (${reviewed}) is older than its reviewedFor entry (${rec.date}) — ${which}${tail}`);
    }
  }
  // Story 244.1.22: arm 2 dates an entry by the NEWER of its dated records and its dated lead. reviewedForRecords
  // reads the lead only for an entry with no dated record (Story 175.1.1's fallback), so an entry whose record quotes
  // an earlier ruling's date beside a later lead reached this arm by the record alone. A lead no newer than the
  // entry's newest record adds nothing (that record was compared above), and arm 1 never reads a lead.
  if (reviewed) {
    for (const e of Array.isArray(m?.reviewedFor) ? m.reviewedFor : []) {
      if (typeof e !== 'string') continue;
      const recDates = [...e.matchAll(ENTRY_RE)].map((x) => x[3].match(ISO_RE)?.[0]).filter(Boolean);
      if (recDates.length === 0) continue;
      const lead = reviewedForLead(e);
      const leadDate = lead.match(ISO_RE)?.[0];
      if (!leadDate || leadDate <= reviewed || recDates.some((d) => d >= leadDate)) continue;
      out.push(`docs/close/${num}.json: reviewed (${reviewed}) is older than its reviewedFor entry (${leadDate}) — "${lead.length > 80 ? `${lead.slice(0, 80)}…` : lead}"${tail}`);
    }
  }
  return out;
}

// Canonical serialization — the normalization round's mechanism. Key order is
// fixed (top-level, per-check, expect), 2-space indent, trailing newline;
// unknown keys (schema failures) are preserved in original order after the
// canonical ones so a failing manifest never loses data on normalize.
const orderKeys = (obj, order) => {
  const out = {};
  for (const k of order) if (obj[k] !== undefined) out[k] = obj[k];
  for (const k of Object.keys(obj)) if (!(k in out)) out[k] = obj[k];
  return out;
};

export function normalizeManifest(m) {
  const out = orderKeys(m, ['epic', 'planRef', 'reviewed', 'reviewedFor', 'stories', 'checks']);
  if (Array.isArray(out.checks)) {
    out.checks = out.checks.map((c) => {
      if (typeof c !== 'object' || c === null || Array.isArray(c)) return c;
      const n = orderKeys(c, ['id', 'prose', 'run', 'file', 'glob', 'http', 'exclude', 'expect', 'timeoutSec', 'note', 'requires']);
      if (n.expect && typeof n.expect === 'object' && !Array.isArray(n.expect))
        n.expect = orderKeys(n.expect, ['exit', 'stdoutIncludes', 'stdoutMatches', 'exists', 'contains', 'absent', 'count', 'status', 'bodyIncludes']);
      return n;
    });
  }
  return out;
}

export const serializeManifest = (m) => JSON.stringify(normalizeManifest(m), null, 2) + '\n';

export function lintRepo({ planText, archiveText, manifests, protocolText }) {
  if (protocolText == null) return { adopted: null, failures: [], ok: true };
  const failures = [];
  const warnings = []; // Story 215.1.2: readings, never a verdict
  let adopted = null;
  let proto = null;
  try { proto = JSON.parse(protocolText); adopted = proto.adopted ?? null; }
  catch { return { adopted: null, failures: ['docs/close/protocol.json does not parse'], ok: false }; }
  if (!/^\d{4}-\d{2}-\d{2}$/.test(adopted ?? '')) failures.push('protocol.json missing valid "adopted" date');

  // 24.3.8: schema-validate EVERY manifest (open or archived epic) — the
  // dialect is one; drift now fails the repo's own lint gate. Story 86.1.3:
  // the pass reads the plan's live-epic set (parsed once, here) so the
  // heading-needle rule fires on live epics' manifests only.
  const { epics, initiatives, registerStories } = parsePlan(planText);
  const liveEpicNums = new Set(epics.map((e) => e.num));
  for (const [num, raw] of Object.entries(manifests)) {
    let parsed;
    try {
      parsed = JSON.parse(raw);
    } catch {
      failures.push(`docs/close/${num}.json does not parse`);
      continue;
    }
    failures.push(...validateManifest(parsed, `docs/close/${num}.json`, { liveEpics: liveEpicNums }));
  }

  const archived = archiveStoryNums(archiveText);
  const ticks = storyTicks(planText, archiveText);
  for (const epic of epics) {
    if (!epic.hasCloseStory) failures.push(`Epic ${epic.num}: no close story ("Close out Epic ${epic.num}")`);
    const raw = manifests[epic.num];
    if (raw == null) { failures.push(`Epic ${epic.num}: missing manifest docs/close/${epic.num}.json`); continue; }
    let m;
    try { m = JSON.parse(raw); }
    catch { continue; } // parse failure already flagged by the schema pass above
    if (m.epic !== epic.num) {
      failures.push(typeof m.epic === 'number'
        ? `Epic ${epic.num}: manifest epic field is the bare JSON number ${m.epic} — quote it as a string ("${epic.num}")`
        : `Epic ${epic.num}: manifest epic field is "${m.epic}"`);
    }
    const listed = new Set(m.stories ?? []);
    for (const s of epic.stories) {
      if (!s.tombstone && !s.isClose && !listed.has(s.num))
        failures.push(`Epic ${epic.num}: story ${s.num} in plan but not in manifest stories`);
    }
    for (const num of listed) {
      const inPlan = epic.stories.some((s) => s.num === num && !s.tombstone);
      if (!inPlan && !archived.has(num) && !registerStories.has(num))
        failures.push(`Epic ${epic.num}: manifest story ${num} resolves nowhere (plan ∪ archive ∪ register)`);
    }
    const ids = (m.checks ?? []).map((c) => c.id);
    for (const d of new Set(ids.filter((x, i) => ids.indexOf(x) !== i)))
      failures.push(`Epic ${epic.num}: duplicate check id "${d}"`);
    // 32.1.3 run-string divergence arm — LIVE epics' manifests. The first
    // classifier is liveness: an archived epic's manifest is a record the
    // protocol never re-runs (no post-close re-verification), and its strings
    // are records; a manifest is linted through its whole live life. Census of
    // record (`node scripts/close/run-string-census.mjs`, origin/main): 9
    // divergent strings at 2026-08-15, 10 at 2026-08-16 — all in closed
    // epics, all POSIX-authored — record-and-leave (Sprint 22 leg 1, hub
    // friction log F3, upheld at review). The second classifier (Story
    // 32.1.4) runs below over ARCHIVED epics.
    for (const c of m.checks ?? []) {
      if (typeof c?.run !== 'string') continue;
      for (const why of runStringDivergence(c.run))
        failures.push(`docs/close/${epic.num}.json check "${c.id}": run-string divergence — ${why} (Story 32.1.3; KB run-strings-shell-backslash-divergence)`);
      for (const p of runStringPipes(c.run)) warnings.push(pipeWarning(epic.num, c, p));
    }
    // 77.1.1 arms 1–2 over LIVE epics' manifests (the archived-on/after-cutoff
    // pass runs below, beside 32.1.4's).
    failures.push(...reviewedForFailures(epic.num, m, ticks));
  }
  // 77.1.1 — archived epics completed on/after REVIEWEDFOR_ARCHIVE_CUTOFF are
  // linted for reviewedFor agreement + monotone `reviewed` like live ones; a
  // sweep-era record (closed before the cutoff) or an undated block is exempt —
  // never retro-edited to satisfy a lint born after it. (liveEpicNums is the
  // set parsed above the schema pass.)
  for (const b of archiveEpicBlocks(archiveText)) {
    if (liveEpicNums.has(b.num) || !b.completed || b.completed < REVIEWEDFOR_ARCHIVE_CUTOFF) continue;
    const raw = manifests[b.num];
    if (raw == null) continue;
    let m;
    try { m = JSON.parse(raw); } catch { continue; }
    failures.push(...reviewedForFailures(b.num, m, ticks, `archived epic completed ${b.completed}, on/after the ${REVIEWEDFOR_ARCHIVE_CUTOFF} cutoff`));
  }
  // 32.1.4 — the born-archived hole: a close PR adds machine checks to the
  // manifest AND cuts the epic to the archive in ONE diff, so a divergent
  // string authored by a close PR is never live at any lint (lawcuments.com
  // 6.4 `hero-semantics`, its close PR #324, 2026-08-16 — the Sprint 22
  // reviewer's remainder). Second classifier, archive-date: an ARCHIVED
  // epic whose `**Completed:**` date is on/after RUN_STRING_ARCHIVE_CUTOFF is
  // linted like a live one; every sweep-era record (closed on/before
  // 2026-08-16) and any undated archive block stays an exempt record.
  for (const b of archiveEpicBlocks(archiveText)) {
    if (liveEpicNums.has(b.num) || !b.completed || b.completed < RUN_STRING_ARCHIVE_CUTOFF) continue;
    const raw = manifests[b.num];
    if (raw == null) continue;
    let m;
    try { m = JSON.parse(raw); } catch { continue; }
    for (const c of m.checks ?? []) {
      if (typeof c?.run !== 'string') continue;
      for (const why of runStringDivergence(c.run))
        failures.push(`docs/close/${b.num}.json check "${c.id}": run-string divergence — ${why} (archived epic completed ${b.completed}, on/after the ${RUN_STRING_ARCHIVE_CUTOFF} cutoff — Story 32.1.4; KB run-strings-shell-backslash-divergence)`);
      if (b.completed >= PIPE_WARN_ARCHIVE_CUTOFF)
        for (const p of runStringPipes(c.run)) warnings.push(pipeWarning(b.num, c, p, ` (archived epic completed ${b.completed}, on/after the ${PIPE_WARN_ARCHIVE_CUTOFF} floor)`));
    }
  }
  // --- Story 24.3.19: two OPT-IN teeth, declared per repo in protocol.json.
  // Opt-in is deliberate: an ungated rollout measured 44 instant CI reds
  // across 10 repos (2026-07-31) — a repo declares its bar once, the lint
  // holds it mechanically from then on (the tigercity #162/#163 class:
  // manifests authored before a check existed drift silently forever).
  // (1) requiredChecks: ["<id>", ...] — every live-plan epic manifest must
  //     carry each declared check id (machine-check parity).
  if (proto?.requiredChecks !== undefined) {
    const rc = proto.requiredChecks;
    if (!Array.isArray(rc) || rc.some((s) => typeof s !== 'string')) {
      failures.push('protocol.json "requiredChecks" must be an array of check-id strings');
    } else {
      for (const epic of epics) {
        const raw = manifests[epic.num];
        if (raw == null) continue; // missing manifest already flagged above
        let m;
        try { m = JSON.parse(raw); } catch { continue; }
        const ids = new Set((m.checks ?? []).map((c) => c?.id));
        for (const id of rc) {
          if (!ids.has(id))
            failures.push(`Epic ${epic.num}: manifest missing required check "${id}" (protocol.json requiredChecks parity, Story 24.3.19)`);
        }
      }
    }
  }
  // (2) initiativeFinal: true — the LAST live epic of each live initiative
  //     (document order; the derivation moves if a later epic files) must
  //     carry an "initiative-<N>-final" check, so closing it adjudicates the
  //     initiative's own Done-when instead of archiving it silently.
  if (proto?.initiativeFinal !== undefined && typeof proto.initiativeFinal !== 'boolean') {
    failures.push('protocol.json "initiativeFinal" must be a boolean');
  } else if (proto?.initiativeFinal === true) {
    for (const init of initiatives) {
      if (!init.epics.length) continue;
      const last = init.epics[init.epics.length - 1];
      const raw = manifests[last];
      if (raw == null) continue; // missing manifest already flagged above
      let m;
      try { m = JSON.parse(raw); } catch { continue; }
      const want = `initiative-${init.num}-final`;
      if (!(m.checks ?? []).some((c) => c?.id === want))
        failures.push(`Epic ${last}: last live epic of Initiative ${init.num} without an "${want}" check (protocol.json initiativeFinal, Story 24.3.19)`);
    }
  }
  if (adopted) {
    for (const b of archiveEpicBlocks(archiveText)) {
      if (b.completed && b.completed >= adopted && !/\*\*Close evidence \(/.test(b.body))
        failures.push(`Archive Epic ${b.num}: completed ${b.completed} (post-adoption) without a Close evidence block`);
    }
  }
  return { adopted, failures, warnings, ok: failures.length === 0 };
}

// --- manifest lockstep (diff mode) ---
// KB epic-close-protocol: "The PR that edits an epic's stories/done-whens
// updates its manifest." Convention-only until 2026-07-12, when one PR
// (expeal-app #84) re-scoped/ticked stories in two epics and left both
// manifests stale — each close then hit checks encoding an unshipped
// mechanism. This check enforces the rule at the PR: a diff that edits
// mechanism-relevant story lines inside an epic's plan section must also
// touch docs/close/<epic>.json. An epic whose heading is GONE from the new
// plan is exempt (the archival cut is the close protocol's own, evidenced
// path). A manifest that is genuinely still accurate is re-affirmed by
// setting a top-level "reviewed": "YYYY-MM-DD" field — a real, reviewable
// diff artifact instead of a junk edit. **Update lines are relevant too
// (2026-07-21, hub Story 18.2.1): two v0.3.1-batch PRs landed substantive
// state changes as Update-only edits — expeal-app #174 declared a gate
// DISCHARGED — and never fired this check.
const LOCKSTEP_RELEVANT_RE = /^(#### Story |\*\*Done when:|\*\*Re-scoped|\*\*Outcome:|\*\*Completed:|\*\*Update)/;

// 1-based line number -> enclosing epic num (null outside epic sections;
// "## " headings reset, mirroring parsePlan).
export function epicLineMap(planText) {
  // \r?-tolerant like parsePlan: a CRLF plan otherwise maps every line to no
  // epic and lockstep never asks for the manifest edit.
  const lines = (planText ?? '').split(/\r?\n/);
  const map = new Array(lines.length + 1).fill(null);
  let cur = null;
  for (let i = 0; i < lines.length; i++) {
    if (/^## /.test(lines[i])) cur = null;
    const e = lines[i].match(EPIC_RE);
    if (e) cur = e[1];
    map[i + 1] = cur;
  }
  return map;
}

// Parse a --unified=0 diff of ONE file into added/removed lines with their
// new-file/old-file line numbers respectively. Each entry carries its 0-based
// `hunk` index — the slide exemption below reasons per hunk.
export function parseHunks(diffText) {
  const added = [];
  const removed = [];
  let o = 0;
  let n = 0;
  let hunk = -1;
  for (const raw of (diffText ?? '').split('\n')) {
    const h = raw.match(/^@@ -(\d+)(?:,\d+)? \+(\d+)(?:,\d+)? @@/);
    if (h) { o = +h[1]; n = +h[2]; hunk++; continue; }
    if (/^(diff |index |--- |\+\+\+ )/.test(raw)) continue;
    if (raw.startsWith('+')) { added.push({ line: n, text: raw.slice(1), hunk }); n++; }
    else if (raw.startsWith('-')) { removed.push({ line: o, text: raw.slice(1), hunk }); o++; }
  }
  return { added, removed };
}

export function lockstepFailures({ changedFiles, planDiffText, newPlanText, oldPlanText }) {
  if (!changedFiles.includes('docs/project-plan.md')) return [];
  const { added, removed } = parseHunks(planDiffText);
  const newMap = epicLineMap(newPlanText);
  const oldMap = epicLineMap(oldPlanText);
  const currentEpics = new Set(parsePlan(newPlanText).epics.map((e) => e.num));
  const affected = new Set();
  for (const { line, text } of added) {
    if (LOCKSTEP_RELEVANT_RE.test(text) && newMap[line]) affected.add(newMap[line]);
  }
  // Story 24.3.18: archival-cut hunk-slide exemption. Close stories carry
  // byte-identical boilerplate, so git can express a whole-epic deletion with
  // the window slid upward — removing the PRECEDING epic's copy and keeping
  // the cut epic's identical copy as context. Old-map attribution then names
  // a live epic the change never touched (lawcuments.com #213). Per hunk:
  // when the hunk also deletes an entire epic (its heading removed, the epic
  // gone from the new plan), a removed line attributed to a live epic is
  // forgiven if the deleted epic's old block holds an unclaimed byte-identical
  // copy — copies the hunk removed under the deleted epic's own attribution
  // claim their budget first, so a genuine same-hunk deletion in the
  // surviving epic (both copies removed) still fails.
  const oldLines = (oldPlanText ?? '').split(/\r?\n/);
  const exempt = new Set();
  for (const h of new Set(removed.map((r) => r.hunk))) {
    const inHunk = removed.filter((r) => r.hunk === h);
    const deleted = new Set();
    for (const r of inHunk) {
      const e = r.text.match(EPIC_RE);
      if (e && !currentEpics.has(e[1])) deleted.add(e[1]);
    }
    if (!deleted.size) continue;
    const budget = new Map();
    for (let i = 0; i < oldLines.length; i++) {
      if (deleted.has(oldMap[i + 1])) budget.set(oldLines[i], (budget.get(oldLines[i]) ?? 0) + 1);
    }
    for (const r of inHunk) {
      if (deleted.has(oldMap[r.line])) budget.set(r.text, (budget.get(r.text) ?? 0) - 1);
    }
    for (const r of inHunk) {
      const epic = oldMap[r.line];
      if (!epic || !currentEpics.has(epic) || !LOCKSTEP_RELEVANT_RE.test(r.text)) continue;
      if ((budget.get(r.text) ?? 0) > 0) { budget.set(r.text, budget.get(r.text) - 1); exempt.add(r); }
    }
  }
  for (const r of removed) {
    if (exempt.has(r)) continue;
    if (LOCKSTEP_RELEVANT_RE.test(r.text) && oldMap[r.line]) affected.add(oldMap[r.line]);
  }
  const failures = [];
  for (const epic of [...affected].sort()) {
    if (!currentEpics.has(epic)) continue;
    if (changedFiles.includes(`docs/close/${epic}.json`)) continue;
    failures.push(`Epic ${epic}: this diff edits story/done-when lines but not docs/close/${epic}.json — `
      + 'the PR that edits an epic\'s stories updates its manifest (KB epic-close-protocol). '
      + 'If the manifest is still accurate, re-affirm it with a top-level "reviewed": "YYYY-MM-DD" field.');
  }
  return failures;
}

// Git glue for the CLI: diff merge-base(origin/main, HEAD) against the
// WORKING TREE (uncommitted plan edits get local feedback too; in CI the
// tree is the checkout, so this equals the PR diff). Returns { skip } when
// no base is resolvable (not a git repo, no origin/main, shallow clone) —
// the lint must degrade loudly-but-green rather than fail on plumbing.
export function gitDiffInputs(repoRoot) {
  // maxBuffer (hub Story 97.1.4): the plan diff and the `show <base>:plan`
  // below return CONTENT — a hub-sized plan through the 1 MiB default would
  // die ENOBUFS and read as "plan new at base". 64 MiB, the family's cap.
  const git = (...a) => execFileSync('git', ['-C', repoRoot, ...a], {
    encoding: 'utf8', timeout: 15000, maxBuffer: 64 * 1024 * 1024, stdio: ['ignore', 'pipe', 'pipe'],
  });
  let base;
  try { base = git('merge-base', 'origin/main', 'HEAD').trim(); }
  catch { return { skip: 'no merge-base with origin/main (not a git repo, or a shallow/originless clone)' }; }
  try {
    const changedFiles = git('diff', '--name-only', base).split('\n').filter(Boolean);
    // Untracked manifests count as touched — a brand-new docs/close/<epic>.json
    // shouldn't fail lockstep just because it isn't staged yet.
    for (const f of git('ls-files', '--others', '--exclude-standard', 'docs/close').split('\n').filter(Boolean)) {
      changedFiles.push(f);
    }
    if (!changedFiles.includes('docs/project-plan.md')) return { changedFiles, planDiffText: '', oldPlanText: '' };
    const planDiffText = git('diff', '--unified=0', base, '--', 'docs/project-plan.md');
    let oldPlanText = '';
    try { oldPlanText = git('show', `${base}:docs/project-plan.md`); } catch { /* plan new at base */ }
    return { changedFiles, planDiffText, oldPlanText };
  } catch (e) {
    return { skip: `git diff failed (${e.message?.split('\n')[0] ?? e})` };
  }
}

// `overrides` lets a caller thread in texts it already read (check-portfolio
// reads the plan itself) so a fleet run doesn't read every file twice.
export function collectRepoInputs(repoRoot, overrides = {}) {
  const read = (p) => (existsSync(path.join(repoRoot, p)) ? readFileSync(path.join(repoRoot, p), 'utf8') : null);
  const closeDir = path.join(repoRoot, 'docs', 'close');
  const manifests = {};
  if (existsSync(closeDir)) {
    for (const f of readdirSync(closeDir)) {
      const m = f.match(/^(\d+(?:\.\d+)*)\.json$/);
      // A directory named N.M.json reads as a missing manifest (a visible
      // lint failure), never an EISDIR crash that kills a whole fleet run.
      if (m && statSync(path.join(closeDir, f)).isFile()) manifests[m[1]] = readFileSync(path.join(closeDir, f), 'utf8');
    }
  }
  return {
    planText: read(path.join('docs', 'project-plan.md')),
    archiveText: read(path.join('docs', 'archives', 'project-plan-completed.md')),
    manifests,
    protocolText: read(path.join('docs', 'close', 'protocol.json')),
    ...overrides,
  };
}

// --- CLI ---
// argv[1] may not exist as a path when this module is IMPORTED by a script
// run via `node -e` (argv[1] is that script's first argument) — never crash
// the importer just deciding whether we're the CLI.
const argvReal = (() => { try { return process.argv[1] && realpathSync(process.argv[1]); } catch { return null; } })();
const isMain = argvReal === fileURLToPath(import.meta.url);
if (isMain) {
  const args = process.argv.slice(2);
  const repoIdx = args.indexOf('--repo');
  if (repoIdx !== -1 && !args[repoIdx + 1]) {
    console.error('usage: node scripts/close-lint.mjs [--repo <path>]');
    process.exit(2);
  }
  const root = repoIdx === -1 ? process.cwd() : path.resolve(args[repoIdx + 1]);
  // Story 71.1.1: fail CLOSED off-root. "Not adopted → exit 0" is right for a
  // real repo root without the protocol and wrong for /tmp, the hub while
  // linting a site, or wherever the harness reset the cwd — four relocation
  // legs recorded that false green on 2026-09-03 (KB close-lint-cwd-default
  // is the same trap one step earlier). A root with no docs/project-plan.md
  // is not a repo root: refuse with exit 2 (the usage-error code) and name it.
  if (!existsSync(path.join(root, 'docs', 'project-plan.md'))) {
    console.error(`close-lint: ${root} is not a repo root (no docs/project-plan.md) — run from the repo or pass --repo <path>`);
    process.exit(2);
  }
  // 24.3.8 --normalize: rewrite this repo's manifests to the canonical
  // serialization (key order + 2-space indent). The fleet normalization
  // round's mechanism — deliberate and PR-visible, never run by the lint
  // itself. Validation still runs after, so a normalize never masks drift.
  if (args.includes('--normalize')) {
    const { writeFileSync } = await import('node:fs');
    const closeDir = path.join(root, 'docs', 'close');
    let rewrote = 0;
    if (existsSync(closeDir)) {
      for (const f of readdirSync(closeDir)) {
        if (!/^\d+(?:\.\d+)*\.json$/.test(f) || !statSync(path.join(closeDir, f)).isFile()) continue;
        const p = path.join(closeDir, f);
        const raw = readFileSync(p, 'utf8');
        let parsed;
        try { parsed = JSON.parse(raw); }
        catch { console.error(`normalize: ${f} does not parse — left as-is`); continue; }
        const canonical = serializeManifest(parsed);
        if (canonical !== raw) { writeFileSync(p, canonical); rewrote++; console.log(`normalized docs/close/${f}`); }
      }
    }
    console.log(`normalize: ${rewrote} manifest(s) rewritten.`);
  }
  const inputs = collectRepoInputs(root);
  const r = lintRepo(inputs);
  if (r.adopted == null && r.ok) { console.log('close-lint: not adopted (no docs/close/protocol.json) — nothing to check'); process.exit(0); }
  if (r.adopted != null) {
    const d = gitDiffInputs(root);
    if (d.skip) console.log(`close-lint: lockstep skipped — ${d.skip}`);
    else r.failures.push(...lockstepFailures({ ...d, newPlanText: inputs.planText }));
  }
  r.ok = r.failures.length === 0;
  for (const w of r.warnings ?? []) console.error(`close-lint WARN: ${w}`);
  for (const f of r.failures) console.error(`close-lint FAIL: ${f}`);
  const warnTail = r.warnings?.length ? ` — ${r.warnings.length} warning(s)` : '';
  console.log(r.ok ? `close-lint: ok (adopted ${r.adopted})${warnTail}` : `close-lint: ${r.failures.length} failure(s)${warnTail}`);
  process.exit(r.ok ? 0 : 1);
}
