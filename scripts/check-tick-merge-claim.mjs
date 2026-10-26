#!/usr/bin/env node
// scripts/check-tick-merge-claim.mjs — hub Story 206.1.1: a tick that rides
// its own leg PR cannot claim its merge.
//
// WHAT: reads the PR's own diff over the two plan files (docs/project-plan.md
// and docs/archives/project-plan-completed.md) and REFUSES (exit 1) any ADDED
// line that begins `**Completed` — the tick's label — when it names THIS PR's
// number and says how that PR merged. The record shape it enforces is KB
// sprint-proposal-standard § Roles: a tick that rides its own leg PR records
// the PR's number and head and nothing about its merge; the merge is recorded
// where it is a fact, in the next leg's landing record and the block's
// **Executed:** record. Register class tick-in-the-leg-pr-predicts-its-own-merge
// (thirteen ticks across Hub Sprints 48, 53 and 54, every claim written before
// the merge it describes) is the reason — a prediction in a record's voice.
//
// HOW TO RUN:
//   in CI (the hub `scope` job, after check-recurrences.mjs): bare —
//     node scripts/check-tick-merge-claim.mjs
//   Since hub Story 215.1.3 (Hub Sprint 56 leg 4) this file is a SYNCED CANONICAL
//   (scripts/sync-session-wrap.mjs CANONICALS): Story 215.1.6's round puts it in every
//   fleet repo, and the sweep module scripts/fleet/sweeps/tick-merge-claim-wiring.mjs
//   then adds the same bare line to each repo's `scope` job after ci-docs-only.mjs —
//   so a tick in any repo meets the arm (the hub's ten cases stay the single pin).
//   the PR number comes from the pull_request event (GITHUB_EVENT_PATH,
//   `.number`), the range from the merge ref the job checks out (HEAD^1...HEAD,
//   the same read scripts/ci-docs-only.mjs makes — Story 98.1.1's, through
//   scripts/lib/ci-forge.mjs resolveRange).
//   by hand: node scripts/check-tick-merge-claim.mjs --pr <n> [--opened <ISO-8601>] [--ready|--draft] [--repo <path>] --base <rev> --head <rev>
//   (docs/close/206.1.json's arm-refuses-the-face tooth is the historical
//   plant: --pr 3637 --base a2810c87^ --head a2810c87 — Hub Sprint 54 leg 1's
//   own tick, which says "Merged by the executor …" of #3637 inside #3637;
//   arm-refuses-the-date-face is the second plant: --pr 3782 --opened
//   2026-10-19T00:07:04Z --base a383f8ab^ --head a383f8ab — Sprint 55 tick 5,
//   dated 2026-10-18 in a PR that opened on the 19th).
//
// THE PREDICATE (the executor's call, stated here): an added Completed line is
// refused when a merge verb on it — `merged`, `squash-merged`, `auto-merged`,
// `landed` (any case) — has THIS PR's `#<n>` as its NEAREST PR reference on
// the line, by character distance either side. The nearest-reference rule is
// what lets a tick cite another PR's merge on the same line ("deccentral.com
// #320 (merged …); this hub tick rides hub #3749") and still be refused when
// the verb is about itself ("hub #3637 (…). Merged by the executor on its green
// run"). A line naming its own PR with no merge verb passes; a line with a
// merge verb and no PR reference passes (nothing claims THIS merge); a line
// that is not ADDED in this diff is never read. Accepted limits: the noun
// `merge` ("its merge waits on Omid") is not a verb here; a claim phrased
// without any of the four verbs ("in main since 10:39Z") passes — the
// reviewer's read, not this arm's.
//
// THE SECOND FACE (the Hub Sprint 55 reviewer pass's widening, 2026-10-19 —
// register class tick-date-is-the-authoring-day-not-the-utc-landing-day, seat
// 4: hub #3782 opened 2026-10-19T00:07:04Z with its Completed line dated
// 2026-10-18): an added Completed line whose leading date — the `YYYY-MM-DD`
// right after the label, or the dated-label variant's `**Completed
// (YYYY-MM-DD):**` when no date follows — is EARLIER than the UTC day the PR
// opened is refused. The open instant is `pull_request.created_at` from the
// event, or `--opened <ISO-8601>` by hand; its UTC day is the bar. A line dated
// the open day or later passes (a tick pushed on a later day is dated that
// day); an undated line meets the first face only; with no open instant known
// the date face is not read and the `ok:` line says so.
//
// THE MOVE FACE (hub Initiative 232, 2026-10-22 — register class
// tick-merge-date-face-reads-a-moved-tick-as-added, seats hub #3966 and
// tigerian #111): an epic close's archival cut deletes every ticked block from
// docs/project-plan.md and adds it unchanged to the archive, so each moved tick
// is an ADDED line to the diff reader — and the date face refused seven of
// tigerian #111's, dated 2026-10-12…21 in a PR opened on the 22nd. The deleted
// `**Completed` lines of the two plan files are collected from the same diff
// (removedCompletedTexts); an added line whose text EQUALS a deleted one is a
// MOVE: the date face is not read on it (the line pre-existed; its date is its
// own PR's), the merge-claim face still is (a moved line cannot truthfully say
// how THIS PR merged), and the `ok:` line counts the moves. Exact text on
// purpose — the archive keeps a tick's line byte for byte; a re-dated or edited
// line is a new line the date face must still judge.
//
// THE SUBJECT RULE (hub Story 234.1.3, Initiative 234 — register class
// tick-merge-claim-arm-reads-another-legs-merge-word-as-this-ticks-claim, seat
// 1: Hub Sprint 59 leg 2's tick went RED on `scope` for the words "leg 1's
// merged canonical", a truthful cross-reference to a sibling leg's merged
// canonical, because the nearest `#<n>` to that verb was this PR's). A merge
// word has a SUBJECT: the clause it sits in — from the last clause delimiter
// before it (a sentence end `. `, `; `, `: `, a bracket, `, `, an em dash) to
// the verb. A clause that names ANOTHER unit — another PR's `#<n>`, `leg N`,
// `Story N.M.K`, `Epic N.M` — is about that unit, never this tick's claim
// ("… reads leg 1's merged canonical", "cut after hub #3955 merged"). A clause
// that names THIS PR — its `#<n>`, or the words "this PR" — is a claim ("this
// PR merged on its green run"). A clause naming nothing falls back to the
// nearest-reference rule over the whole line ("hub #3637 (…). Merged by the
// executor on its green run": a new sentence about this PR — still refused).
// "this PR" counts as a reference to this PR in both rules.
// Two narrowings (hub Story 241.1.2, Hub Sprint 66 leg 3 — the same class, seats
// 3 and 4, each after this rule landed): (a) a BARE story id `N.M.K` is another
// unit, as `Story N.M.K` is (seat 3, hub #4097: "after Windows Hub 1's 221.1.2
// tick and the KB PR landed"); a three-part version string (`2.56.0`) reads as
// one too — accepted, it is never a PR; (b) a merge word DIRECTLY after `a`,
// `an` or `the` is attributive and never read (seat 4, hub #4177: "a merged PR
// whose post-merge branch delete failed"); `each`/`every` are not articles
// ("each merged before the next" is a verb). The census over every fleet plan and
// archive (1,221 Completed lines with a #N, each line's own PR taken as its first
// #N) moved 204 refusals to 202 — two lines cleared by (b), "rebased onto the
// merged leg 2" and "with the merged head filled into the field", and NONE newly
// refused. The accepted limit (seat 5, expeal-app #870): a clause naming nothing
// whose nearest visible reference is this PR's own number AFTER the verb ("…
// MERGED to the hub's `main` …; both rows read this story DONE in #870") stays
// refused — the census rejected both general rules that would clear it (the
// fallback looking before the verb first newly refused 16 legal round records;
// stopping its forward look at the sentence end dropped true self-claims). A
// refusal reached through the fallback prints its own fix line (FIX_FALLBACK):
// name the other PR in the merge word's own clause.
//
// THE THIS-PR FACE (hub Story 234.1.3 — register class
// tick-names-this-pr-without-its-number-and-head, seat 1: four Hub Sprint 57
// ticks read "(this PR, `sprint-57/tick-N`, docs-only)" — no number, no head;
// the numbers survived only in the squash subjects): when the PR is NOT a draft
// (`pull_request.draft` false in the event; `--ready` by hand), an added
// Completed line that says "this PR" and names NO `#<n>` at all is refused —
// the number exists once the PR is open, and § Roles asks for the number and
// the pre-tick head in a second commit. A draft's first commit may say "this
// PR" (`draft` true; `--draft` by hand); a line naming any number passes; with
// the draft state unknown (by hand without a flag) the face is not read; a
// MOVED line is never read by it.
//
// THE MANIFEST FACE (hub Story 235.1.1, Initiative 235 — register class
// tick-in-the-leg-pr-predicts-its-own-merge, seats 15–16: tigerdash #337 and
// #344 each wrote "merged under Omid's first-hand delegation to that/this seat"
// into their OWN Story 4.4.5 / 4.4.7 reviewedFor entry of docs/close/4.4.json
// before the merge, while their Completed lines named no merge verb — close-lint's
// lockstep arm puts a tick's record into BOTH places in the SAME PR, so the face
// is structural): every docs/close/*.json the range touches is read at the merge
// base and at the head (`git show <rev>:<path>`, spawned without a shell), and an
// ADDED reviewedFor entry — a head string whose exact text is not in the base
// array (an edited entry is a new text; a reordered one is not; a manifest absent
// at base has every entry added) — is judged by the same claimsOwnMerge rule as a
// Completed line and refused with the manifest's file and the entry's new-file
// line (`?` when its JSON encoding is not on one line). A head copy that does not
// parse is named on a `note:` line and read past (close-lint's red, not this
// arm's). Accepted limits: the date face and the this-PR face are not read on an
// entry (no seat; a born entry says "its filing PR" by convention before its
// number exists); manifests are never cut to the archive, so the move face has no
// manifest analogue. The `ok:` line adds the entries' count only when the range
// touches a manifest, so a plan-only PR's line stays byte-identical to the
// pre-235 shape.
//
// THE VISIBLE-REFERENCE RULE (hub Story 235.1.1 — MEASURED at the Initiative 235
// filing, hub #4077: fed the two seated entries, the fallback returned null for
// both, because the nearest `#<n>` to each `merged` was the worktree-lineage
// parenthetical's PR — `(#336); merged under` seven characters away, the entry's
// own `PR #337` 256 back): in the nearest-reference fallback a candidate is a
// reference the verb can SEE — at the verb's bracket depth or shallower, with no
// bracket of the reference's own group closing between them (`()` and `[]`; a
// bracket inside backticks counts). `(#336)` closed before `merged`, so `PR #337`
// is the nearest; `(#343, a fleet canonicals sync that landed …)` keeps `landed`
// about #343 (one group, one depth); "deccentral.com #320 (merged …)" still finds
// #320 (the verb's own group opened after it). A line whose brackets do not
// balance is read as before, by plain distance. The same hole sat on a Completed
// line carrying the lineage parenthetical ("cut from `origin/main` `c310a71c` (the
// Initiative 234 filing, hub #4010); merged by …") — covered by the same rule.
//
// --repo <path> (hub Story 235.1.1): the repository to read, so a live reading
// over a sibling clone needs no `cd` (KB bash-cwd-drift-four-modes); the cwd
// otherwise, as before.
//
// EXIT: 0 — no added Completed line or reviewedFor entry claims this PR's merge,
// and no added Completed line predates its
// open day (an `ok:` line with the count), or no PR number is known (a push to
// main runs the scope job too; the arm is PR-scoped, so it prints `skipped:`
// and stands down); 1 — refused, each line quoted with its file and new-file
// line number, then the § Roles sentence as the fix (one per face); 2 — the
// range or the git read failed (loud).
//
// Hub-only at this filing (every Completed-line seat is the hub's; a canonical
// round is a later story only if a second repo seats the face). The pure reads
// are exported for test/check-tick-merge-claim.test.mjs; the entry is
// isMain-gated (scripts/lib/is-main.mjs). The git execs: the plan diff is
// content-bearing (`diff` without --name-only), so it names its maxBuffer (hub
// Story 97.1.4's census — the archive is several MB and a close PR's diff can
// carry most of it); since Story 235.1.1 a `diff --name-only` over docs/close/,
// one `merge-base`, and one `show` per touched manifest per side, each with the
// same maxBuffer.

import { execFileSync } from 'node:child_process';
import process from 'node:process';
import path from 'node:path';
import { realpathSync } from 'node:fs';
import { fileURLToPath } from 'node:url';
import { readEventPayload, resolveRange } from './lib/ci-forge.mjs';

export const PLAN_FILES = ['docs/project-plan.md', 'docs/archives/project-plan-completed.md'];
// The tick's label at the start of the line: `**Completed:**` and the dated
// variant `**Completed (2026-10-13):**`; a list item (`- **Completed:**`) or a
// different label (`**Closer's completion …`) is not a tick.
export const COMPLETED_RE = /^\*\*Completed\b/;
export const MERGE_VERB_RE = /\b(?:squash-|auto-)?merged\b|\blanded\b/gi;
const PR_REF_RE = /#(\d+)\b/g;
// "this PR" names this PR without its number (hub Story 234.1.3).
const THIS_PR_RE = /\bthis PR\b/g;
// Another unit a merge word can be about — a leg, a story (with its prefix, or a bare
// `N.M.K` id since hub Story 241.1.2), an epic; another PR's `#<n>` is read through
// PR_REF_RE (hub Story 234.1.3, THE SUBJECT RULE).
const OTHER_UNIT_RE = /\b(?:[Ll]eg \d+|Story \d+(?:\.\d+)+|\d+\.\d+\.\d+|Epic \d+(?:\.\d+)?)\b/g;
// The clause a merge word sits in begins after the last of these before it.
const CLAUSE_DELIM_RE = /\. |; |: |[()[\]]|, | — /g;
// A merge word directly after an article is attributive ("a merged PR"), never a verb
// (hub Story 241.1.2); `each`/`every` are not articles ("each merged before the next").
const ARTICLE_BEFORE_RE = /\b(?:a|an|the)\s+$/i;
export const FIX =
  "fix: record the PR's number and head; the merge is recorded in the next leg's landing record and the **Executed:** record (KB sprint-proposal-standard § Roles).";
export const FIX_DATE =
  "fix: a tick's date is the UTC day its PR opens; a sprint that crosses 00:00Z re-dates its dated fields before the open (KB sprint-proposal-standard § Roles).";
// Printed beside FIX when a refusal was reached through the nearest-reference fallback
// (the merge word's clause named nothing — hub Story 241.1.2, seat 5's face).
export const FIX_FALLBACK =
  "fix: the merge word's clause names no PR; if it is another PR's merge, name that PR in the same clause (`hub #4224 merged …`).";
export const FIX_THIS_PR =
  'fix: name the PR\'s number and pre-tick head in a second commit once the PR is open — "this PR" and the branch alone leave the number in the squash subject only (KB sprint-proposal-standard § Roles; register class tick-names-this-pr-without-its-number-and-head).';
// The tick's leading date: `**Completed:** 2026-10-18 —`, or the dated-label
// variant `**Completed (2026-10-13):**` when no date follows the label.
const TICK_DATE_RE = /^\*\*Completed(?: \((\d{4}-\d{2}-\d{2})\))?:\*\*\s*(\d{4}-\d{2}-\d{2})?/;

export function tickDateOf(text) {
  const m = TICK_DATE_RE.exec(String(text));
  if (!m) return null;
  return m[2] ?? m[1] ?? null;
}

// The UTC day of an ISO-8601 instant (any offset), or null when it does not parse.
export function openDayOf(iso) {
  if (!iso) return null;
  const t = Date.parse(String(iso));
  if (Number.isNaN(t)) return null;
  return new Date(t).toISOString().slice(0, 10);
}

// Unified diff → the ADDED lines that begin with the tick's label inside the
// two plan files, each with its file and NEW-file line number (from the hunk
// headers). Deleted lines and context advance nothing / one line; a
// "\ No newline at end of file" marker advances nothing.
export function addedCompletedLines(diffText) {
  const out = [];
  let file = null;
  let inPlan = false;
  let line = 0;
  for (const raw of String(diffText).split('\n')) {
    if (raw.startsWith('diff --git ')) { file = null; inPlan = false; continue; }
    if (raw.startsWith('+++ ')) {
      const named = raw.slice(4).trim();
      file = named === '/dev/null' ? null : named.replace(/^b\//, '');
      inPlan = file != null && PLAN_FILES.includes(file);
      continue;
    }
    if (raw.startsWith('--- ')) continue;
    const hunk = /^@@ -\d+(?:,\d+)? \+(\d+)(?:,\d+)? @@/.exec(raw);
    if (hunk) { line = Number(hunk[1]); continue; }
    if (file == null) continue;
    if (raw.startsWith('\\')) continue;
    if (raw.startsWith('+')) {
      const text = raw.slice(1);
      if (inPlan && COMPLETED_RE.test(text)) out.push({ file, line, text });
      line += 1;
      continue;
    }
    if (raw.startsWith('-')) continue;
    line += 1;
  }
  return out;
}

// Unified diff → the TEXT of every DELETED line that begins with the tick's
// label inside the two plan files — a move's other half (hub Initiative 232).
// The file headers (`--- a/…`, `+++ b/…`) are read only between `diff --git`
// and the first hunk, so a deleted line that happens to begin `-- ` inside a
// hunk is a deletion, never a header.
export function removedCompletedTexts(diffText) {
  const out = new Set();
  let inPlan = false;
  let inHeader = false;
  for (const raw of String(diffText).split('\n')) {
    if (raw.startsWith('diff --git ')) { inPlan = false; inHeader = true; continue; }
    if (inHeader && raw.startsWith('--- ')) {
      const named = raw.slice(4).trim();
      inPlan = named !== '/dev/null' && PLAN_FILES.includes(named.replace(/^a\//, ''));
      continue;
    }
    if (inHeader && raw.startsWith('@@ ')) inHeader = false;
    if (inHeader || !inPlan) continue;
    if (raw.startsWith('-') && !raw.startsWith('--- ')) {
      const text = raw.slice(1);
      if (COMPLETED_RE.test(text)) out.add(text);
    }
  }
  return out;
}

// Bracket depth before each index over `()` and `[]` (the closing depth at
// s.length); null when the line's brackets do not balance — THE VISIBLE-REFERENCE
// RULE then stands down and the fallback reads plain distance (hub Story 235.1.1).
export function bracketDepths(text) {
  const s = String(text);
  const out = new Array(s.length + 1);
  let depth = 0;
  for (let i = 0; i < s.length; i += 1) {
    out[i] = depth;
    const c = s[i];
    if (c === '(' || c === '[') depth += 1;
    else if (c === ')' || c === ']') {
      depth -= 1;
      if (depth < 0) return null;
    }
  }
  out[s.length] = depth;
  return depth === 0 ? out : null;
}

// Can the verb at `verbAt` see the reference at `refAt`? Only at the verb's depth
// or shallower, and only when no bracket of the reference's own group closes
// between them (the lineage parenthetical `(#336)` closed before `merged`).
function visibleFrom(depths, refAt, verbAt) {
  const dr = depths[refAt];
  if (dr > depths[verbAt]) return false;
  const lo = Math.min(refAt, verbAt);
  const hi = Math.max(refAt, verbAt);
  for (let i = lo; i <= hi; i += 1) if (depths[i] < dr) return false;
  return true;
}

// The claim on one line: the first merge verb that is about THIS PR — { verb, ref, at,
// via } — or null. THE SUBJECT RULE (hub Story 234.1.3) reads the verb's clause first: a
// clause naming another unit is about that unit; one naming this PR (`#<n>` or "this
// PR") is a claim (`via: 'clause'`); one naming nothing falls back to the nearest
// reference the verb can SEE on the line (the 206.1 rule under THE VISIBLE-REFERENCE
// RULE, hub Story 235.1.1), "this PR" counted as a reference to this PR (`via:
// 'fallback'`). A merge word directly after an article is attributive and never read
// (hub Story 241.1.2).
export function claimsOwnMerge(text, pr) {
  const own = Number(pr);
  const s = String(text);
  const refs = [...s.matchAll(PR_REF_RE)].map((m) => ({ at: m.index, own: Number(m[1]) === own }));
  for (const m of s.matchAll(THIS_PR_RE)) refs.push({ at: m.index, own: true });
  if (refs.length === 0) return null;
  const others = [...s.matchAll(OTHER_UNIT_RE)].map((m) => m.index);
  const depths = bracketDepths(s);
  for (const m of s.matchAll(MERGE_VERB_RE)) {
    if (ARTICLE_BEFORE_RE.test(s.slice(0, m.index))) continue;
    let start = 0;
    for (const d of s.slice(0, m.index).matchAll(CLAUSE_DELIM_RE)) start = d.index + d[0].length;
    const inClause = (at) => at >= start && at < m.index;
    if (refs.some((r) => !r.own && inClause(r.at)) || others.some(inClause)) continue;
    if (refs.some((r) => r.own && inClause(r.at))) return { verb: m[0], ref: own, at: m.index, via: 'clause' };
    let nearest = null;
    for (const r of refs) {
      if (depths && !visibleFrom(depths, r.at, m.index)) continue;
      const d = Math.abs(r.at - m.index);
      if (nearest == null || d < nearest.d) nearest = { d, own: r.own };
    }
    if (nearest && nearest.own) return { verb: m[0], ref: own, at: m.index, via: 'fallback' };
  }
  return null;
}

// The faces over the added lines; openDay null leaves the date face unread.
// `removedTexts` (hub Initiative 232) is the set removedCompletedTexts read
// from the same diff: an added line whose text is in it is a MOVE — counted,
// its date face and its this-PR face not read, its merge-claim face read as any
// other. `draft` (hub Story 234.1.3): false reads THE THIS-PR FACE; true or
// null (unknown) leaves it unread. `entries` (hub Story 235.1.1) are the ADDED
// reviewedFor entries of the touched manifests, { file, line, text } each — read
// by the merge-claim face alone (kind `manifest-claim`).
export function judge(added, pr, openDay = null, removedTexts = new Set(), { draft = null, entries = [] } = {}) {
  const refusals = [];
  let moved = 0;
  for (const a of added) {
    const claim = claimsOwnMerge(a.text, pr);
    if (claim) refusals.push({ ...a, kind: 'merge-claim', verb: claim.verb, via: claim.via });
    const isMove = removedTexts.has(a.text);
    if (isMove) moved += 1;
    const date = openDay && !isMove ? tickDateOf(a.text) : null;
    if (date && date < openDay) refusals.push({ ...a, kind: 'date', date, openDay });
    if (draft === false && !isMove && /\bthis PR\b/.test(a.text) && !/#\d+\b/.test(a.text)) refusals.push({ ...a, kind: 'this-pr' });
  }
  for (const e of entries) {
    const claim = claimsOwnMerge(e.text, pr);
    if (claim) refusals.push({ ...e, kind: 'manifest-claim', verb: claim.verb, via: claim.via });
  }
  return { read: added.length, moved, refusals, entriesRead: entries.length };
}

export function formatRefusal(r, pr) {
  const text = r.text.length > 240 ? `${r.text.slice(0, 240)}…` : r.text;
  if (r.kind === 'manifest-claim') {
    return `refused: ${r.file}:${r.line} — an added reviewedFor entry names #${pr} and says how it merged ("${r.verb}"): ${text}`;
  }
  if (r.kind === 'date') {
    return `refused: ${r.file}:${r.line} — an added **Completed:** line is dated ${r.date}, earlier than the UTC day its PR opened (${r.openDay}): ${text}`;
  }
  if (r.kind === 'this-pr') {
    return `refused: ${r.file}:${r.line} — an added **Completed:** line names "this PR" with no #<n> on a PR that is no longer a draft (the number exists once the PR is open): ${text}`;
  }
  return `refused: ${r.file}:${r.line} — an added **Completed:** line names #${pr} and says how it merged ("${r.verb}"): ${text}`;
}

// --pr / --opened / --ready | --draft / --repo / --base / --head by hand; the rest from the event.
export function parseArgs(argv) {
  const out = { pr: null, opened: null, base: null, head: null, ready: null, repo: null };
  for (let i = 0; i < argv.length; i += 1) {
    const a = argv[i];
    if (a === '--pr') out.pr = argv[++i] ?? null;
    else if (a === '--opened') out.opened = argv[++i] ?? null;
    else if (a === '--base') out.base = argv[++i] ?? null;
    else if (a === '--head') out.head = argv[++i] ?? null;
    else if (a === '--repo') out.repo = argv[++i] ?? null;
    else if (a === '--ready') out.ready = true;
    else if (a === '--draft') out.ready = false;
    else if (a.startsWith('--pr=')) out.pr = a.slice(5);
    else if (a.startsWith('--opened=')) out.opened = a.slice(9);
    else if (a.startsWith('--base=')) out.base = a.slice(7);
    else if (a.startsWith('--head=')) out.head = a.slice(7);
    else if (a.startsWith('--repo=')) out.repo = a.slice(7);
  }
  return out;
}

export function prNumberFrom(args, payload) {
  if (args.pr != null && /^\d+$/.test(String(args.pr))) return Number(args.pr);
  const n = payload?.number ?? payload?.pull_request?.number;
  return Number.isInteger(n) ? n : null;
}

// The PR's open day (UTC) from --opened, else the event's created_at; null when neither is known.
export function openDayFrom(args, payload) {
  return openDayOf(args.opened ?? payload?.pull_request?.created_at ?? null);
}

// The PR's draft state (hub Story 234.1.3): --ready → false, --draft → true, else the
// event's pull_request.draft when it is a boolean; null when unknown (the face is not read).
export function draftFrom(args, payload) {
  if (args.ready === true) return false;
  if (args.ready === false) return true;
  const d = payload?.pull_request?.draft;
  return typeof d === 'boolean' ? d : null;
}

// Every content-bearing git exec names its maxBuffer AT THE CALL SITE — the Story
// 97.1.4 census (test/git-exec-census.test.mjs) reads the literal options object,
// so a spread constant reads as "no maxBuffer" (this story's battery, 12:29Z).
export function readDiff(base, head, cwd = process.cwd()) {
  return execFileSync('git', ['diff', `${base}...${head}`, '--', ...PLAN_FILES], {
    cwd,
    encoding: 'utf8',
    maxBuffer: 64 * 1024 * 1024,
    stdio: ['ignore', 'pipe', 'pipe'],
  });
}

// THE MANIFEST FACE's git reads (hub Story 235.1.1): every docs/close/<N.M>.json the
// range touches, with its text at the merge base (null when absent there — a born
// manifest) and at the head (a manifest deleted at head is not read). The colon
// `rev:path` form goes to git.exe directly — no shell, so no MSYS conversion (KB
// windows-git-bash-cli-gotchas §6 is the Bash tool's layer, not this one).
export function readManifestChanges(base, head, cwd = process.cwd()) {
  const git = (args) => execFileSync('git', args, {
    cwd,
    encoding: 'utf8',
    maxBuffer: 64 * 1024 * 1024,
    stdio: ['ignore', 'pipe', 'pipe'],
  });
  const names = git(['diff', '--name-only', `${base}...${head}`, '--', 'docs/close/'])
    .split('\n')
    .map((s) => s.trim())
    .filter((f) => /^docs\/close\/[^/]+\.json$/.test(f));
  if (names.length === 0) return [];
  let mergeBase;
  try { mergeBase = git(['merge-base', base, head]).trim() || base; } catch { mergeBase = base; }
  const show = (rev, file) => { try { return git(['show', `${rev}:${file}`]); } catch { return null; } };
  return names
    .map((file) => ({ file, baseText: show(mergeBase, file), headText: show(head, file) }))
    .filter((m) => m.headText != null);
}

// One touched manifest → its ADDED reviewedFor entries: each head string whose exact
// text is not in the base array (an edited entry is a new text; a reordered one is
// not; no base — every entry), with the entry's new-file line (the line holding its
// JSON encoding; `?` when the encoding is not on one line). A head that does not
// parse returns `error` and no entries — close-lint's red, named by the caller; an
// unparsable base reads as empty (every head entry judged — a claim about THIS PR
// in an old entry cannot exist, so nothing false is refused by it).
export function addedReviewedForEntries({ file, baseText, headText }) {
  let head;
  try {
    head = JSON.parse(headText);
  } catch (e) {
    return { file, entries: [], error: e?.message || String(e) };
  }
  const headList = Array.isArray(head?.reviewedFor) ? head.reviewedFor.filter((s) => typeof s === 'string') : [];
  let baseSet = new Set();
  if (baseText != null) {
    try {
      const b = JSON.parse(baseText);
      if (Array.isArray(b?.reviewedFor)) baseSet = new Set(b.reviewedFor.filter((s) => typeof s === 'string'));
    } catch { /* an unparsable base: every head entry reads as added */ }
  }
  const lines = String(headText).split('\n');
  const entries = [];
  for (const text of headList) {
    if (baseSet.has(text)) continue;
    const needle = JSON.stringify(text);
    const idx = lines.findIndex((l) => l.includes(needle));
    entries.push({ file, line: idx >= 0 ? idx + 1 : '?', text });
  }
  return { file, entries, error: null };
}

export function main({ argv = process.argv.slice(2), env = process.env, cwd = process.cwd(), log = console.log, error = console.error } = {}) {
  const args = parseArgs(argv);
  const payload = readEventPayload(env);
  const pr = prNumberFrom(args, payload);
  if (pr == null) {
    log(`skipped: tick-merge-claim: no pull request number (event '${env.GITHUB_EVENT_NAME || '(none)'}') — the arm is PR-scoped; pass --pr <n> by hand`);
    return 0;
  }
  // --repo <path> (hub Story 235.1.1): the repository to read; the cwd otherwise.
  const root = args.repo ? path.resolve(cwd, args.repo) : cwd;
  let range;
  if (args.base && args.head) range = { base: args.base, head: args.head };
  else {
    range = resolveRange(env, payload, { revParse: (ref) => gitRevParseIn(ref, root) });
    if (range.error) {
      error(`tick-merge-claim: cannot resolve the diff range — ${range.error}; pass --base <rev> --head <rev>`);
      return 2;
    }
  }
  let diff;
  try {
    diff = readDiff(range.base, range.head, root);
  } catch (e) {
    error(`tick-merge-claim: git diff ${range.base}...${range.head} failed — ${e?.stderr?.toString?.().trim() || e?.message || e}`);
    return 2;
  }
  // THE MANIFEST FACE (hub Story 235.1.1): the touched manifests and their added entries.
  let manifests;
  try {
    manifests = readManifestChanges(range.base, range.head, root);
  } catch (e) {
    error(`tick-merge-claim: git read over docs/close/ for ${range.base}...${range.head} failed — ${e?.stderr?.toString?.().trim() || e?.message || e}`);
    return 2;
  }
  const parsed = manifests.map(addedReviewedForEntries);
  for (const p of parsed) {
    if (p.error) log(`note: tick-merge-claim: ${p.file} at ${range.head} does not parse (${p.error}) — close-lint's red; its reviewedFor entries not read`);
  }
  const entries = parsed.flatMap((p) => p.entries);
  const openDay = openDayFrom(args, payload);
  const verdict = judge(addedCompletedLines(diff), pr, openDay, removedCompletedTexts(diff), { draft: draftFrom(args, payload), entries });
  if (verdict.refusals.length > 0) {
    for (const r of verdict.refusals) log(formatRefusal(r, pr));
    if (verdict.refusals.some((r) => r.kind === 'merge-claim' || r.kind === 'manifest-claim')) log(FIX);
    if (verdict.refusals.some((r) => r.via === 'fallback')) log(FIX_FALLBACK);
    if (verdict.refusals.some((r) => r.kind === 'date')) log(FIX_DATE);
    if (verdict.refusals.some((r) => r.kind === 'this-pr')) log(FIX_THIS_PR);
    return 1;
  }
  const dateFace = openDay ? `or predates its open day (${openDay})` : '(open day unknown — the date face is not read; pass --opened <ISO-8601>)';
  // With no move the line is byte-identical to the pre-232 shape (the 206.1 cases' regexes).
  const movedNote = verdict.moved > 0 ? ` (${verdict.moved} moved — deleted and re-added verbatim in the same diff, an archival cut; the date face not read on them)` : '';
  // With no manifest touched the line is byte-identical to the pre-235 shape; a touched manifest names the read, count 0 included.
  const manifestNote = manifests.length > 0 ? ` and ${verdict.entriesRead} added reviewedFor entr${verdict.entriesRead === 1 ? 'y' : 'ies'} read over ${manifests.length} manifest(s) in docs/close/` : '';
  log(`ok: tick-merge-claim: ${verdict.read} added **Completed:** line(s) read${movedNote}${manifestNote}, none claims #${pr}'s merge ${dateFace} (${range.base}...${range.head} over ${PLAN_FILES.join(', ')})`);
  return 0;
}

// A ref's sha in `cwd`, or null — the shim's read, with the cwd pinned.
function gitRevParseIn(ref, cwd) {
  try {
    const out = execFileSync('git', ['rev-parse', '--verify', '--quiet', ref], { cwd, encoding: 'utf8', stdio: ['ignore', 'pipe', 'ignore'] }).trim();
    return out || null;
  } catch {
    return null;
  }
}

// Story 215.1.6 (the sweep canary's find): this file is a SYNCED CANONICAL since Story
// 215.1.3, and scripts/lib/is-main.mjs is hub-only by its own contract ("the synced
// single-file canonicals keep their inline guard and never import") — bridgetandomid.com's
// scope job failed ERR_MODULE_NOT_FOUND on the first live run. The guard is close-lint.mjs's
// shape: Node realpaths the main module's URL, so the argv side is realpathed to match.
const argvReal = (() => { try { return process.argv[1] && realpathSync(process.argv[1]); } catch { return null; } })();
if (argvReal === fileURLToPath(import.meta.url)) process.exit(main());
