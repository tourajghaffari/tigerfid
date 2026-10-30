#!/usr/bin/env node
// wrap-session.mjs — session handoff helper. CANONICAL COPY lives in the hub
// (scripts/wrap-session.mjs) and is synced verbatim into each repo under projects/
// by scripts/sync-session-wrap.mjs — do NOT hand-edit a synced copy; edit the hub
// canonical and re-run the sync. Every wrap lands the note on its own docs-only
// session-notes/<stamp> branch and self-lands it (merge if green, else arm GitHub
// auto-merge) in every repo; the old --merge flag is now a no-op.
//
// What it does:
//   wrap   Trim docs/notes.md to the 3 most recent "## Session" blocks, then land them
//          on main via their OWN throwaway branch — decoupled from feature work so a
//          wrap note can never be stranded on an unmerged feature PR. The commit itself
//          happens in an EPHEMERAL WORKTREE off origin/main (Initiative 11 Pillar A):
//          your session's checkout, index, and working tree are never touched. Every
//          wrap, in every repo:
//            1. Composes the note against origin/main's CURRENT notes.md (fetch first;
//               this session's new block(s) prepended to origin's), NOT the possibly-stale
//               local file — so a note another session merged concurrently is never
//               clobbered (the T3 fix, Decision D2). If origin has notes this local file
//               never saw, the wrap refuses ("BEHIND origin — git pull --ff-only") rather
//               than wrap stale. If the local file EDITS a block that already exists on
//               origin (same heading, different content), the wrap refuses too — pass
//               --amend to deliberately land the local wording in place.
//            2. Commits the trimmed notes on session-notes/<stamp> — a fresh branch cut in
//               an isolated worktree, never the session's own checkout — pushes, and opens
//               a docs-only PR.
//            3. Lands it: merges if GREEN (the protect-main ruleset allows 0-approval
//               solo merges), else — if the only blocker is checks still running — arms
//               GitHub auto-merge so it lands server-side when they pass. Anything else
//               (conflicts, failing checks, or any other unresolved merge blocker) leaves
//               the PR open with a loud HANDOFF INCOMPLETE banner; the notes are safely
//               pushed either way. A merge call that exits non-zero is followed by ONE
//               re-read of the PR: MERGED is a landing (gh's post-merge branch delete can
//               fail after the merge itself succeeded — Story 236.1.1), reported with gh's
//               error and the hand delete; any other read keeps the banner.
//          Because the branch is docs-only, landing it is always safe — so this happens
//          in every repo, hub and site alike (the old --merge opt-in is now a no-op).
//          Wrap touches ONLY docs/notes.md, and only in the isolated worktree: other
//          tracked, uncommitted changes in your session physically cannot ride onto the
//          notes branch, so they no longer block the wrap — an informational note lists
//          them instead (commit finished work on its own branch when convenient). An
//          UNMERGED docs/notes.md (conflict markers) still refuses — that's the one thing
//          isolation can't fix. Your checkout is never switched. After a CONFIRMED land
//          the wrap reconciles exactly one path — docs/notes.md, discarded only when the
//          F3 predicate proves every uncommitted added line is on origin/main — and
//          fast-forwards local main (12.1.2); the armed path stays hands-off in-session
//          (the note is not on origin/main yet, so the discard would be unproven) —
//          `reconcile` closes that window later. It no longer merges your feature PR —
//          do that yourself.
//          Refuses to wrap while a DRAFT block tops the notes: a draft is an uncommitted
//          pause checkpoint the real wrap note must replace first.
//   recall Print the most recent session block from docs/notes.md.
//   reconcile  Close the armed-wrap window after the fact: when a landed wrap left
//          docs/notes.md dirty (the leftover that blocks `git pull --ff-only` — KB
//          post-wrap-notes-dirty-not-lost), prove via the same F3 predicate that every
//          uncommitted added line is on origin/main, then discard the leftover and
//          fast-forward local main. Refuses (leaves the file alone) when any line is
//          missing from origin — so a pause DRAFT is never touched. The hub's
//          SessionStart freshness hook runs this automatically at session start.
//
// How to run:
//   node scripts/wrap-session.mjs wrap [-m "commit message"] [--merge] [--amend] [--dry-run]
//                                      [--marker <ref>] [--carry-siblings] [--session-id <uuid>]
//                                      [--tombstone-only]
//   Story 208.1.1: the wrap lands and stamps only the block carrying ITS OWN session id
//   (CLAUDE_CODE_SESSION_ID, or --session-id); a sibling's block at the top refuses by
//   name; blocks below your own ride only on --carry-siblings.
//   Story 236.2.1: the commit's title is that block's heading (never the top by position);
//   a compose that would land no block of yours — only a trim — refuses, nothing pushed;
//   and your presence marker is tombstoned only once the wrap branch is pushed (or the
//   notes read already current), so a refused wrap leaves it LIVE for the re-run.
//   Story 212.1.1: --tombstone-only renames this session's presence marker to its
//   session-done/* tombstone and does nothing else (no note, no heartbeat, no fetch, no
//   sweep, no PR) — for a session whose block already rides main by another PR; honours
//   --marker and --dry-run; a refusal exits 1 (the marker is the run's whole job).
//   Story 220.1.2: the hub-mode --dry-run reuses a heartbeat stamp younger than ten minutes
//   (check-fleet.mjs writes <FGL_HOME or ~/.fgl>/heartbeat-last.json after every full run):
//   the stamp's lines with "(reused from HH:MMZ)" on the summary line and no heartbeat
//   spawned; older, absent, another root or unparsable → the run as before, loud about why.
//   The real wrap always runs its own heartbeat (its stamp is the note's evidence).
//   node scripts/wrap-session.mjs recall
//   node scripts/wrap-session.mjs reconcile [--dry-run]
//   Story 244.1.5: `reconcile --dry-run` is the preview that never acts — it fetches
//   origin/main (a remote-tracking ref only; the verdict needs origin's current notes),
//   prints the verdict and each step it would run (the notes restore; the fast-forward
//   of local main), and leaves the working tree, the index and local main unchanged.
//   Each subcommand takes only its own flags — wrap the eight above, reconcile only
//   --dry-run, recall none — and an unknown one is refused with the usage, exit 2,
//   before any repo access; an unknown subcommand keeps its exit 1.
//   (--merge is accepted but a no-op — landing is the default everywhere now.)
//
// The session notes themselves are written by Claude (see CLAUDE.md "Session
// handoff") before this script runs; this script only trims, commits, pushes.

import { readFileSync, writeFileSync, existsSync, mkdirSync, realpathSync, readdirSync } from 'node:fs';
import { execFileSync } from 'node:child_process';
import os from 'node:os';
import path from 'node:path';
import { fileURLToPath } from 'node:url';
import { withWriteWorktree, wtGit, fetchOriginMain, isPushReject, branchHeldByWorktree, parseAddedLines, safeDirtyVerdict, notesReconcileVerdict } from './lib/hub-write-worktree.mjs';
import { forgeFor } from './lib/forge.mjs';

const KEEP = 3;
const repoRoot = path.resolve(path.dirname(fileURLToPath(import.meta.url)), '..');
const notesPath = path.join(repoRoot, 'docs', 'notes.md');
const notesRel = 'docs/notes.md';

// Story 8.1.1 (CONFIRMED high): git/gh run over execFileSync argv — NO shell —
// so branch/PR-derived values (legal refs may contain `$(...)`, backticks, `;`,
// `&&`, `|`) reach the program as inert literal tokens and can't command-inject
// at wrap time. Every caller passes an argument ARRAY, never a command string.
// This is the same standard as the lib's wtGit; do NOT reintroduce a shell
// (execSync / a `git ${x}` string) for any git call in this file — the few
// direct execFileSync calls elsewhere (the porcelain/show probes) follow
// the same argv rule even though their commands are fixed and take no input.
// gh calls follow the same standard INSIDE the forge adapter (below).
export const git = (args) =>
  execFileSync('git', args, { cwd: repoRoot, encoding: 'utf8' }).trim();
// Like git() but keeps stderr off the terminal — for the belt-and-suspenders
// branch-delete backups whose failure is the ROUTINE case now that the lib's
// 13.1.1 cleanup deletes the wrap branch on every clean wrap (execFileSync's
// default stdio sends child stderr straight to the terminal, so the caught
// error still printed `error: branch … not found` mid-wrap). stderr is PIPED,
// not ignored, mirroring the forge adapter's quiet ops: a real failure still
// carries err.stderr.
const gitQuiet = (args) =>
  execFileSync('git', args, {
    cwd: repoRoot,
    encoding: 'utf8',
    stdio: ['ignore', 'pipe', 'pipe'],
  }).trim();
// Forge calls route through the forge adapter (Story 17.1.3,
// scripts/lib/forge.mjs). CANONICAL-set decision, recorded per the story: the
// adapter JOINS the CANONICALS set as this file's companion — the same
// arrangement as lib/hub-write-worktree.mjs — so every fleet-synced copy of
// this file finds it at the same relative path, and a change to either lands
// via a fleet-sync round. The adapter owns the gh argv, the WRAP_SESSION_GH
// e2e seam (13.1.5, win32-safe — no PATH shadowing), and the quiet-vs-loud
// stdio semantics (review fix 8.1.6: quiet ops PIPE stderr, never ignore it,
// so err.stderr still lets classifyPrLookupFailure distinguish "no PR" from a
// real gh failure). Ops default loud; pass QUIET where a failure is a routine
// probe outcome whose diagnostic would be noise.
const forge = forgeFor(repoRoot);
const QUIET = { quiet: true };

// GitHub rejects PR titles over 256 chars, and `gh pr create --fill` copies a
// single-commit branch's subject into the PR title — so a full session heading
// as the commit subject breaks the standalone-PR path (and a squash merge of a
// single-commit PR inherits the subject, flooding `git log --oneline`). Cap
// the subject and carry the full heading in the commit body; --fill turns the
// body into the PR body, so the heading still reads in full on the PR.
const SUBJECT_MAX = 150;
// The wrap title is the top session block's heading line. \r?-tolerant split:
// on a Windows autocrlf worktree notes.md arrives CRLF, and a bare '\n' split
// would ride a trailing \r into the wrap commit subject/body and PR title.
// Pause-draft guard predicate (hub Story 67.1.1): a session block is a PAUSE DRAFT
// iff its HEADING line carries the pause marker token `DRAFT:` — the form the
// pause protocol prescribes (`## Session: <date> — DRAFT: <title>`, hub CLAUDE.md
// "Session handoff") and the notes header spells. A bare DRAFT word is prose:
// tigerian's spec cycles name every unapproved spec "DRAFT vN" and wrap headings
// cite them, so the old any-word-boundary DRAFT guard refused a real wrap note
// (2026-09-01, Sprint 25 leg 5; hub inbox tigerian-wrap-draft-guard-false-positive).
export function isPauseDraft(sessionBlock) {
  return /^## Session[^\n]*\bDRAFT:/.test(sessionBlock ?? '');
}

export function sessionTitle(sessionBlock) {
  return sessionBlock.split(/\r?\n/)[0].replace(/^## Session:?\s*/, '');
}

export function wrapCommitMessage(title) {
  const full = `Session notes: wrap up (${title})`;
  if (full.length <= SUBJECT_MAX) return full;
  // room left for the heading inside "Session notes: wrap up (<cut>…)"
  const budget = SUBJECT_MAX - (full.length - title.length) - '…'.length;
  let cut = title.slice(0, budget);
  if (/[\uD800-\uDBFF]$/.test(cut)) cut = cut.slice(0, -1); // never split a surrogate pair
  return `Session notes: wrap up (${cut}…)\n\n${title}`;
}

// GitHub rejects `gh pr merge --merge` outright in repos that only allow squash
// (or rebase) merges, so the wrap must pick a method the repo permits. Prefer a
// true merge commit (the historical behavior) and fall back in the order GitHub
// lists the settings. A failed lookup (null) keeps 'merge' — same as before this
// check existed, and the merge attempt's own error message stays intelligible.
//
// A METHOD IS USABLE ONLY IF BOTH the repo-level setting allows it AND — when a
// branch ruleset pins `allowed_merge_methods` — the ruleset lists it. The ruleset
// can be STRICTER than the repo settings, and GitHub enforces the ruleset: until
// 2026-10-01 lawcuments allowed all three at the repo level while its protect-main
// ruleset pinned allowed_merge_methods to ["squash"], so a repo-only check picked
// --merge and GitHub rejected the auto-merge arming with "Merge method merge commits
// are not allowed" (history). Since that day every fleet repo reads
// squash-only on both layers (hub Story 136.1.1; re-measured 2026-10-10, hub Story
// 181.1.1); the ruleset read stays, because a ruleset can be stricter than the settings.
// `rulesetMethods` is that list (lowercase 'merge'/'squash'/'rebase'); null or empty
// means "no ruleset restriction on this branch" — the repo settings alone decide.
export function pickMergeMethod(perms, rulesetMethods = null) {
  const rulesetOk = (m) =>
    !Array.isArray(rulesetMethods) || rulesetMethods.length === 0 || rulesetMethods.includes(m);
  if (perms?.mergeCommitAllowed && rulesetOk('merge')) return 'merge';
  if (perms?.squashMergeAllowed && rulesetOk('squash')) return 'squash';
  if (perms?.rebaseMergeAllowed && rulesetOk('rebase')) return 'rebase';
  return 'merge';
}

// Does the session's cwd belong to the same repo this script copy wraps? The
// projects nest physically inside the hub, so a path-prefix check cannot tell
// them apart — only comparing the two git toplevels can. Paths on macOS and
// Windows filesystems are case-insensitive (a session can sit in a lowercase
// spelling of the same directory), so compare case-folded there.
export function sameRepoRoot(a, b, platform = process.platform) {
  if (!a || !b) return false;
  const fold = platform === 'win32' || platform === 'darwin';
  const norm = (p) => (fold ? path.resolve(p).toLowerCase() : path.resolve(p));
  return norm(a) === norm(b);
}

// The wrap commits docs/notes.md from an ISOLATED worktree (Pillar A), so other
// tracked, uncommitted changes physically cannot ride onto the notes branch —
// but they are still worth surfacing (finished work should be committed on its
// branch before a handoff), so return those paths for an informational note.
// Untracked files ('??') are left out. An UNMERGED status (conflict in
// progress: UU/AA/DD/AU/UA/UD/DU) is returned even for notes.md itself — the
// file holds conflict markers and the CALLER refuses that case (committing
// markers to origin/main is the one thing isolation can't fix).
const UNMERGED_XY = new Set(['DD', 'AU', 'UD', 'UA', 'DU', 'AA', 'UU']);
export function offendingChanges(porcelainText, notesRel) {
  return porcelainText
    .split('\n')
    .filter(Boolean)
    .filter((line) => !line.startsWith('??'))
    .map((line) => ({ xy: line.slice(0, 2), path: line.slice(3).trim() }))
    .filter(({ xy, path: p }) => p && (p !== notesRel || UNMERGED_XY.has(xy)))
    .map(({ path: p }) => p);
}

// The throwaway branch that carries a wrap's note: session-notes/<YYYYMMDD-HHMM>.
export function notesBranchName(date) {
  const stamp = date.toISOString().replace(/[-:]/g, '').replace(/T(\d{4}).*/, '-$1');
  return `session-notes/${stamp}`;
}

// Split a notes file into its header (everything before the first block) and the
// list of "## Session" blocks. parts[0] is the header; each later part starts with
// "## Session". Used for both the local file and origin/main's copy. A HEADERLESS
// file (starts with "## Session") is special-cased: the zero-width split never
// fires at position 0, so parts[0] would otherwise classify the first session as
// the header — immortal (never trimmed), invisible to the DRAFT guard and the
// title derivation (Story 10.2.3 item 7).
// "[:\s]" keeps prefix-lookalike headings ("## Sessions of yore") out of the
// block list — real block headings are always "## Session: …" (10.2.3 LOW).
export function splitNotes(text) {
  const t = text ?? '';
  const parts = t.split(/^(?=## Session[:\s])/m);
  if (/^## Session[:\s]/.test(t)) return { header: '', sessions: parts };
  return { header: parts[0], sessions: parts.slice(1) };
}

// Compose the wrap note against fetched ORIGIN/main content, not the stale local
// file (Decision D2 / Story 10.2.2 — the T3 clobber fix). The session's NEW blocks
// (the prefix of the local file's blocks whose heading isn't already on origin) are
// prepended to origin's blocks, then the whole thing is trimmed to `keep`. So a note
// merged concurrently by another session (present on origin, absent from this stale
// local file) is preserved instead of being silently overwritten. `missingFromLocal`
// is origin's blocks the local file lacks entirely — a non-empty value means the local
// notes are BEHIND origin (the caller refuses and tells you to `git pull --ff-only`).
// `diverged` lists blocks present on BOTH sides under the same heading but with
// different content (an in-place edit — e.g. amending the just-landed top note). By
// default origin's copy wins and the caller refuses loudly: silently preferring either
// side would clobber someone — local-wins re-opens the T3 content clobber, origin-wins
// silently discards the amendment. `amend: true` is the deliberate path — the local
// copy replaces origin's, in place. Trailing-whitespace-only differences don't count
// as divergence — and neither do line endings alone (13.1.4): a CRLF checkout that
// predates a repo's `.gitattributes eol=lf` adoption (`git ls-files --eol`:
// i/lf w/crlf) reads every untouched block as CRLF while origin's blob is LF, and
// the git clean-filter would never even commit those endings. Both sides are
// CRLF→LF-normalized before comparing (same class as the sync-side lesson Story
// 8.3.3 encoded in sync-session-wrap.mjs). The composed text always ends with a
// newline (heals a file whose final newline was stripped).
const headingKey = (block) => block.split('\n', 1)[0].trim();
const eolNormalized = (block) => block.replace(/\r\n/g, '\n');

// The BEHIND refusal's full message (Story 2.1.9 arm B — pure so the contract
// test can pin it). Two hypotheses, stated unconditionally: (1) the local
// clone is genuinely stale → pull; (2) a LANDED block's HEADING was edited in
// place → that block's origin heading no longer matches any local key, so it
// reads as "missing" here, and the pull fixes NOTHING (the file is ahead,
// incompatibly) — the recovery is the append-only contract (KB
// wrap-notes-append-only): restore the origin heading verbatim and write the
// update as a NEW `## Session:` block. (The body-edit sibling case is already
// served by the DIVERGED refusal's sanctioned --amend path.)
export function behindRefusal({ notesRel, missingFromLocal }) {
  return (
    `Refusing to wrap: origin/main has ${missingFromLocal.length} session note(s) your local\n` +
    `${notesRel} doesn't — your local notes are BEHIND origin. Your wrap note is safe\n` +
    '(uncommitted in the working tree). Re-sync, then re-run the wrap:\n' +
    '  git pull --ff-only origin main\n' +
    `(reconcile your new block onto the freshly pulled ${notesRel} if the pull conflicts).\n` +
    'Second hypothesis: if a landed block\'s HEADING was edited in place in your local file,\n' +
    'it reads as "missing" here and the pull will NOT fix it — the notes are append-only\n' +
    '(KB wrap-notes-append-only): restore the origin heading verbatim and write your update\n' +
    'as a NEW `## Session:` block on top instead.\n' +
    'Newest origin note(s) you are missing:\n' +
    missingFromLocal.map((s) => `  ${headingKey(s)}`).join('\n')
  );
}

export function composeNotes({ header, localSessions, originSessions, keep, amend = false }) {
  const originKeys = new Set(originSessions.map(headingKey));
  const localByKey = new Map(localSessions.map((s) => [headingKey(s), s]));
  const newBlocks = [];
  for (const s of localSessions) {
    if (originKeys.has(headingKey(s))) break; // reached a block already on origin
    newBlocks.push(s);
  }
  const missingFromLocal = originSessions.filter((s) => !localByKey.has(headingKey(s)));
  // Local-only blocks BELOW the new-block prefix (e.g. a hand-restored old session):
  // the compose builds from origin's blocks, so these would be silently dropped from
  // what lands on origin/main. Report them so the caller can refuse loudly instead.
  const stranded = localSessions
    .slice(newBlocks.length)
    .filter((s) => !originKeys.has(headingKey(s)))
    .map(headingKey);
  const diverged = [];
  const base = originSessions.map((o) => {
    const local = localByKey.get(headingKey(o));
    if (local === undefined || eolNormalized(local).trimEnd() === eolNormalized(o).trimEnd()) return o;
    diverged.push(headingKey(o));
    return amend ? local : o;
  });
  const composed = [...newBlocks, ...base];
  const kept = composed.slice(0, keep);
  let text = header + kept.join('');
  if (text && !text.endsWith('\n')) text += '\n';
  return {
    text,
    newCount: newBlocks.length,
    keptCount: kept.length,
    dropped: composed.length - kept.length,
    missingFromLocal,
    stranded,
    diverged,
  };
}

// The --dry-run description of the single decoupled path (no side effects).
export function dryRunPlan({ startBranch, sessions, dropped, wrapBranch, offending }) {
  const lines = [];
  if (offending.length) {
    lines.push(
      `[dry-run] note: uncommitted changes outside ${notesRel} stay in your tree (isolated worktree) — commit finished work on its branch:`
    );
    for (const p of offending) lines.push(`            ${p}`);
  }
  lines.push(`[dry-run] branch=${startBranch}, sessions=${sessions}, would drop ${dropped} oldest, then:`);
  lines.push(`            commit the trimmed notes on ${wrapBranch} off origin/main in an EPHEMERAL worktree,`);
  lines.push(`            push, open a docs-only PR, and land it (merge if green, else arm auto-merge).`);
  lines.push(`            Your checkout is never touched.`);
  return lines.join('\n');
}

// The auto-merge gate (Story 8.1.5): may this PR be merged right now? Green
// only when it is open, conflict-free, AND its checks pass — CLEAN, or
// HAS_HOOKS (merge hooks still settling). Everything else defers: BLOCKED
// (required checks failing/pending), UNSTABLE (a reporting check failing),
// BEHIND (up-to-date enforced), DRAFT, UNKNOWN — and any state this function
// doesn't recognize fails closed, because merging past a failing check is the
// exact promise the header makes ("deferred on failing checks").
export function mergeGate(pr) {
  if (pr?.ghFailed) return { green: false, why: `gh failed — carrier state unknown (${pr.detail})` };
  if (!pr) return { green: false, why: 'no open PR was found for this branch' };
  if (pr.state !== 'OPEN') return { green: false, why: `already ${pr.state}` };
  if (pr.mergeable === 'CONFLICTING') return { green: false, why: 'it has merge conflicts' };
  if (pr.mergeable === 'UNKNOWN')
    return { green: false, why: 'GitHub has not finished computing its mergeability' };
  if (pr.mergeable !== 'MERGEABLE')
    return { green: false, why: `its mergeability is ${pr.mergeable}` };
  switch (pr.mergeStateStatus) {
    case 'CLEAN':
    case 'HAS_HOOKS':
      return { green: true, why: '' };
    case 'BLOCKED':
      return { green: false, why: 'required checks are failing or still pending (BLOCKED)' };
    case 'UNSTABLE':
      return { green: false, why: 'a reporting check is failing or still pending (UNSTABLE)' };
    case 'BEHIND':
      return { green: false, why: 'it is behind main and up-to-date is enforced (BEHIND)' };
    case 'DRAFT':
      return { green: false, why: 'it is a draft PR' };
    default:
      return {
        green: false,
        why: `its merge state is ${pr.mergeStateStatus || 'not reported'}`,
      };
  }
}

// Classify a PR's check rollup (Story 8.1.7): 'pending' | 'failing' |
// 'passing' | 'none'. Drives the arm-auto-merge decision when mergeGate
// defers on BLOCKED/UNSTABLE: pending-only checks arm `gh pr merge --auto`
// (the merge fires server-side the moment they pass); failing checks keep
// the loud banner; 'none' keeps the banner too — arming --auto on a required
// context nothing reports would wait forever (the rename trap). Items are
// gh's statusCheckRollup shapes: CheckRun {status, conclusion} or commit
// StatusContext {state}. Unrecognized shapes count as failing (fail closed).
export function classifyChecks(rollup) {
  if (!rollup || rollup.length === 0) return 'none';
  const verdictOf = (c) => {
    if (c.state) {
      if (c.state === 'SUCCESS') return 'passing';
      if (c.state === 'PENDING' || c.state === 'EXPECTED') return 'pending';
      return 'failing'; // FAILURE, ERROR
    }
    if (c.status && c.status !== 'COMPLETED') return 'pending'; // QUEUED, IN_PROGRESS, ...
    if (['SUCCESS', 'NEUTRAL', 'SKIPPED'].includes(c.conclusion)) return 'passing';
    return 'failing'; // FAILURE, CANCELLED, TIMED_OUT, ACTION_REQUIRED, unknown shapes
  };
  const verdicts = rollup.map(verdictOf);
  if (verdicts.includes('failing')) return 'failing';
  if (verdicts.includes('pending')) return 'pending';
  return 'passing';
}

// Refuse to wrap a repo the session isn't sitting in. Every repo carries its own
// copy of this script, and the copy determines the target (repoRoot above) — so
// invoking ANOTHER repo's copy (e.g. the hub's, via `node ../../scripts/
// wrap-session.mjs` from inside a project) would silently wrap that other repo:
// notes, carrier branch, and PR all land where the session isn't looking. An
// inconclusive cwd (not in any git repo) warns and proceeds — this is a misfire
// net for the nested hub/projects layout, not a security boundary.
function guardCwdRepo() {
  let cwdTop;
  try {
    // Deliberately NOT the git() helper: this one must run in the session's cwd,
    // not repoRoot — the whole point is to compare the two. Argv form (no shell),
    // same 8.1.1 standard; the command is fixed and takes no external input.
    cwdTop = execFileSync('git', ['rev-parse', '--show-toplevel'], { encoding: 'utf8' }).trim();
  } catch {
    console.error(
      `note: cannot tell which repo the current directory belongs to; proceeding to wrap ${repoRoot}.`
    );
    return;
  }
  // realpath both sides so symlinked paths (macOS /tmp) and on-disk casing agree.
  const real = (p) => {
    try {
      return realpathSync.native(p);
    } catch {
      return p;
    }
  };
  if (!sameRepoRoot(real(cwdTop), real(repoRoot))) {
    console.error(
      `This copy of wrap-session.mjs wraps:  ${repoRoot}\n` +
        `but the current directory is inside: ${cwdTop}\n` +
        'Refusing — wrap the repo you are in:\n' +
        '  node scripts/wrap-session.mjs wrap\n' +
        `(or cd into ${repoRoot} first if that repo really is the one to wrap).`
    );
    process.exit(1);
  }
}

// The flag `gh pr merge` gets. Squash-only repos (GitHub setting) hard-reject
// `--merge`, so ask the repo which methods it allows and use pickMergeMethod's
// preference. Cached: the setting can't change mid-wrap, and this is consulted
// for hint text as well as the merge itself.
let mergeFlagCache = null;
function repoMergeFlag() {
  if (!mergeFlagCache) {
    let perms = null;
    let rulesetMethods = null;
    try {
      perms = forge.repoViewJson('mergeCommitAllowed,squashMergeAllowed,rebaseMergeAllowed,nameWithOwner,defaultBranchRef', QUIET);
      // A branch ruleset can restrict merge methods below the repo settings, and GitHub
      // enforces the ruleset — so ask for the EFFECTIVE rules on the default branch and
      // honor any pull_request rule's allowed_merge_methods. Best-effort: no ruleset, no
      // pull_request rule, or an older gh all leave rulesetMethods null (repo settings decide).
      const branch = perms?.defaultBranchRef?.name || 'main';
      if (perms?.nameWithOwner) {
        try {
          const rules = forge.apiJson(`repos/${perms.nameWithOwner}/rules/branches/${branch}`, QUIET);
          const pr = Array.isArray(rules) && rules.find((r) => r?.type === 'pull_request');
          const methods = pr?.parameters?.allowed_merge_methods;
          if (Array.isArray(methods) && methods.length) rulesetMethods = methods;
        } catch {
          // no ruleset / no branch-rules endpoint / offline: leave rulesetMethods null.
        }
      }
    } catch {
      // gh missing/offline: pickMergeMethod(null) keeps the historical --merge.
    }
    mergeFlagCache = `--${pickMergeMethod(perms, rulesetMethods)}`;
  }
  return mergeFlagCache;
}

// A merge/rebase/cherry-pick/revert in progress means the working tree is
// mid-operation: notes.md may hold conflict markers (even a resolved+staged
// notes.md doesn't prove the operation is DONE), and committing that content
// to main mid-operation is never safe (Story 10.2.3 item 1). Resolve the git
// dir via rev-parse — .git is a FILE in a linked worktree, so a hardcoded
// .git/ path would silently disarm this guard.
function inProgressGitOp() {
  let gitDir;
  try {
    gitDir = path.resolve(repoRoot, git(['rev-parse', '--git-dir']));
  } catch {
    return null; // not a git repo — later plumbing will surface its own error
  }
  const has = (p) => existsSync(path.join(gitDir, p));
  if (has('MERGE_HEAD')) return 'a merge is in progress (MERGE_HEAD exists)';
  if (has('rebase-merge') || has('rebase-apply')) return 'a rebase is in progress';
  if (has('CHERRY_PICK_HEAD')) return 'a cherry-pick is in progress';
  if (has('REVERT_HEAD')) return 'a revert is in progress';
  return null;
}

function loadNotes() {
  if (!existsSync(notesPath)) {
    console.error(`${notesRel} not found — nothing to do.`);
    process.exit(1);
  }
  return splitNotes(readFileSync(notesPath, 'utf8'));
}

function hasGh() {
  return forge.available();
}

// Synchronous sleep so we can poll gh without going async (the whole script
// runs git/gh synchronously). Backed by a SharedArrayBuffer so it works on any platform.
function sleepMs(ms) {
  Atomics.wait(new Int32Array(new SharedArrayBuffer(4)), 0, 0, ms);
}

// gh's stderr distinguishes "this branch has no PR" from every other failure
// (network, auth, rate limit). Only the former may be reported as "no open
// PR" — conflating them mis-steers the HANDOFF banner and the handoff note's
// next-steps on a transient outage when a PR exists (8.1.6, last box).
export function classifyPrLookupFailure(stderrText) {
  return /no pull requests? found/i.test(stderrText ?? '') ? 'no-pr' : 'unknown';
}

// A gh failure that is NOT "no PR": callers (mergeGate/landOnMain) treat this
// as carrier-state-unknown — loud banner, never auto-merge arming. Classifies
// on the FULL stderr (gh may prefix warning lines); the first line is only the
// display detail. Exported for the tests that pin this exact seam — the
// original version classified the already-split first line, which hid a bug
// where an ignored-stderr err carried only "Command failed: …".
export const prLookupFailed = (err) => {
  const full = `${err?.stderr || err?.message || err || ''}`.trim();
  if (classifyPrLookupFailure(full) === 'no-pr') return null;
  return { ghFailed: true, detail: full.split('\n')[0] || 'no stderr detail' };
};

// GitHub computes a PR's mergeability asynchronously, so right after a push
// `gh pr view` often returns mergeable=UNKNOWN (and mergeStateStatus=UNKNOWN)
// for a beat. Poll until both settle (or we give up) — the merge gate treats
// UNKNOWN as not-green, so returning too early would defer merges that were
// a second from settling. Returns the PR object; null if there is genuinely
// no PR; or a { ghFailed, detail } sentinel when gh itself failed. The head
// rides along for the clean-arm merge's pin (Story 171.1.1).
const PR_FIELDS = 'number,state,mergeable,mergeStateStatus,headRefOid';
function resolveMergeable(branch, attempts = 5) {
  for (let attempt = 0; attempt < attempts; attempt++) {
    let pr;
    try {
      pr = forge.prViewJson(branch, PR_FIELDS, QUIET);
    } catch (err) {
      return prLookupFailed(err);
    }
    if (pr.state !== 'OPEN') return pr;
    if (pr.mergeable !== 'UNKNOWN' && pr.mergeStateStatus !== 'UNKNOWN') return pr;
    sleepMs(1500);
  }
  // Last look — caller treats a still-UNKNOWN result as "not green".
  try {
    return forge.prViewJson(branch, PR_FIELDS);
  } catch (err) {
    return prLookupFailed(err);
  }
}

// How long an EMPTY rollup is waited on before 'none' is believed (Story
// 166.1.1). GitHub creates a PR's workflow run asynchronously: 45–88 s after
// the PR on 2026-10-06 (deccentral.com #280/#282, hub inbox #3270), ~10–20×
// the old ~6 s window, so four ordinary wraps that day bannered and were armed
// by hand. WRAP_SESSION_RUN_WAIT_SEC overrides it in whole seconds (the e2e
// fixtures set 0); anything else falls back to the default, never to NaN.
export const RUN_CREATION_WAIT_SEC = 150;
export function runCreationWaitSec(env = process.env) {
  const raw = env.WRAP_SESSION_RUN_WAIT_SEC;
  return /^\d+$/.test(raw ?? '') ? Number(raw) : RUN_CREATION_WAIT_SEC;
}

// Poll `read()` — a classifyChecks verdict, or 'unknown' on a gh failure —
// while it says 'none', until a check registers or `waitMs` has passed. Any
// other verdict returns at once. Pure (reads, sleep and clock are arguments),
// exported for tests. `onWait(seconds)` fires once, on the first empty read.
export function pollRollup(read, { waitMs, intervalMs = 5000, sleep = sleepMs, now = Date.now, onWait = () => {} }) {
  const deadline = now() + waitMs;
  for (let looks = 0; ; looks++) {
    const state = read();
    if (state !== 'none') return state;
    const left = deadline - now();
    if (left <= 0) return 'none';
    if (looks === 0) onWait(Math.round(waitMs / 1000));
    sleep(Math.min(intervalMs, left));
  }
}

// Fetch and classify the branch PR's check rollup. 'unknown' (gh failure)
// keeps the caller on the loud-banner path — fail closed. An empty rollup is
// polled for `waitMs` (about 30 looks, 0.25–5 s apart): right after a push
// GitHub can take a minute or more to create the check run, and 'none' must
// mean "nothing will ever report" (skip-ci marker, required-context rename
// trap) rather than "not created yet".
function checkState(branch, waitMs) {
  return pollRollup(
    () => {
      try {
        const { statusCheckRollup } = forge.prViewJson(branch, 'statusCheckRollup', QUIET);
        return classifyChecks(statusCheckRollup);
      } catch {
        return 'unknown';
      }
    },
    {
      waitMs,
      intervalMs: Math.min(5000, Math.max(250, Math.floor(waitMs / 30))),
      onWait: (s) =>
        console.log(`${branch}: no check has registered yet — waiting up to ${s} s for GitHub to create the CI run…`),
    }
  );
}

// The recovery for a wrap PR that flipped CONFLICTING (the T3b loser): its note
// exists ONLY in the wrap-branch commit, and `gh pr merge` can never work on a
// conflicted PR. The recipe reads the note from ORIGIN (the armed path deletes
// the local branch) and lifts the top block onto the freshly PULLED file — a
// bare `git checkout <branch> -- docs/notes.md` would resurrect the loser's
// whole stale composition and trip the BEHIND refuse (verified e2e; 10.2.3
// item 3). Exported for tests.
// The same-minute leftover-branch refusal (item 5a), extracted so the message
// can be worktree-aware (11.4.4 review, crash recovery): after a SIGKILLed
// wrap the dead worktree's registration still holds the branch, and the plain
// `git branch -D` advice below REFUSES ("used by worktree at …").
export function leftoverBranchMessage(branch, heldAt = null) {
  const lines = [
    `Refusing to wrap: a local branch ${branch} already exists — a leftover from`,
    'an earlier wrap this same minute (or a crashed one). Your notes edit is untouched.',
  ];
  if (heldAt) {
    lines.push(
      `The branch is still checked out in a (possibly crashed) worktree at:`,
      `  ${heldAt}`,
      '`git branch -D` refuses while that worktree holds it. Free it first:',
      `  git worktree remove --force ${heldAt}`,
      `  git worktree prune`,
    );
  }
  lines.push(
    `If that branch's note already landed (check: git log -1 ${branch}), delete it`,
    `and re-run:  git branch -D ${branch}`,
    'Otherwise wait a minute and re-run — the wrap picks a fresh branch name.',
  );
  return lines.join('\n');
}

export function conflictRecovery({ branch, prNumber }) {
  const pr = prNumber ?? '<PR number>';
  return (
    `Recover ${branch}'s note (committed on that branch — held, NOT lost):\n` +
    `    1. gh pr close ${pr}\n` +
    '    2. git checkout main && git pull --ff-only origin main\n' +
    `    3. git fetch origin ${branch} && git show FETCH_HEAD:docs/notes.md\n` +
    "       — copy YOUR '## Session' block (the top one) onto the TOP of docs/notes.md\n" +
    `    4. git push origin --delete ${branch}   (and git branch -D ${branch} if it exists)\n` +
    '    5. re-run: node scripts/wrap-session.mjs wrap'
  );
}

function loudIncomplete(branch, pr, gate = mergeGate(pr)) {
  const ref = pr?.number ? `PR #${pr.number}` : `the PR for ${branch}`;
  const cmd = pr?.number
    ? `gh pr merge ${pr.number} ${repoMergeFlag()} --delete-branch`
    : `gh pr merge ${branch} ${repoMergeFlag()} --delete-branch`;
  const { why } = gate;
  // A conflicted PR gets the recovery that works — advising `gh pr merge` on it
  // would send the operator down a command that can never succeed (item 3).
  const advice =
    pr?.mergeable === 'CONFLICTING'
      ? `  A conflicted wrap PR can never be merged as-is.\n  ${conflictRecovery({ branch, prNumber: pr?.number }).split('\n').join('\n  ')}\n`
      : '  main does NOT have this session’s notes/docs until you\n' +
        '  resolve that and merge:\n' +
        `    ${cmd}\n`;
  console.error(
    '\n============================================================\n' +
      '  HANDOFF INCOMPLETE\n' +
      `  Notes are committed and pushed on ${branch}, but ${ref}\n` +
      `  was NOT merged (${why}).\n` +
      advice +
      '============================================================\n'
  );
}

// T3b preflight (10.2.3 item 3): a wrap that arms auto-merge exits before the
// merge fires — if the PR then flips CONFLICTING (another wrap landed first),
// it sits OPEN forever with its note existing only on the wrap branch, and
// nothing used to detect it. Sweep the open session-notes/* PRs at wrap start
// and flag any stuck one loudly, with the recovery that works. Best-effort:
// offline or a gh hiccup skips the sweep rather than blocking the wrap.
function sweepStrandedWrapPrs() {
  if (!hasGh()) return;
  let prs;
  try {
    prs = forge.prListJson({ state: 'open', limit: 50, json: 'number,headRefName,mergeable,mergeStateStatus' }, QUIET);
  } catch {
    return;
  }
  // 11.4.5: the sweep also watches hub-doc/* — an aborted or timed-out land
  // can leave an OPEN+CONFLICTING hub-doc PR that nothing else ever surfaces
  // (the exact silent-but-held mode Pillar B exists to remove).
  const stuck = (Array.isArray(prs) ? prs : []).filter(
    (p) =>
      (p?.headRefName?.startsWith('session-notes/') || p?.headRefName?.startsWith('hub-doc/')) &&
      (p.mergeable === 'CONFLICTING' || p.mergeStateStatus === 'DIRTY')
  );
  for (const p of stuck) {
    if (p.headRefName.startsWith('hub-doc/')) {
      console.error(
        '\n============================================================\n' +
          `  STRANDED HUB-DOC PR: #${p.number} (${p.headRefName}) is OPEN\n` +
          '  but CONFLICTING — its auto-merge will never fire. The delta is\n' +
          "  NOT lost (it stayed in the landing operator's working tree), but\n" +
          '  a conflicted PR can never merge as-is. Close the zombie and\n' +
          '  re-land the edit:\n' +
          `    gh pr close ${p.number}\n` +
          '    node scripts/hub-doc.mjs land <file> --title "…"   (re-run)\n' +
          '============================================================\n'
      );
      continue;
    }
    console.error(
      '\n============================================================\n' +
        `  STRANDED WRAP PR: #${p.number} (${p.headRefName}) is OPEN but\n` +
        '  CONFLICTING — its auto-merge will never fire, and its session\n' +
        '  note exists only on that branch.\n' +
        `  ${conflictRecovery({ branch: p.headRefName, prNumber: p.number }).split('\n').join('\n  ')}\n` +
        '============================================================\n'
    );
  }
}

// Bring local `main` up to date WITHOUT touching the session's checkout
// (Pillar A): when the session happens to be ON main with a quiet tree, a
// plain ff-pull; otherwise fast-forward the ref directly (fails harmlessly if
// main is in use elsewhere or has diverged — fetch-before-trust covers readers).
// The refusal's line, shared with fastForwardPlan's preview of a diverged main (Story 248.1.5).
const NOT_FAST_FORWARDED = 'note: local main not fast-forwarded — `git pull --ff-only origin main` when convenient.';
function refreshLocalMain() {
  try {
    const before = git(['rev-parse', 'main']);
    if (git(['rev-parse', '--abbrev-ref', 'HEAD']) === 'main') {
      if (execFileSync('git', ['status', '--porcelain'], { cwd: repoRoot, encoding: 'utf8' }).trim() === '') {
        git(['pull', '--ff-only', 'origin', 'main']);
      } else {
        console.log('note: local main may be behind origin — pull when your tree is quiet.'); // 11.4.7: not verified, only possible
        return;
      }
    } else {
      git(['fetch', 'origin', 'main:main']);
    }
    // Say so when main actually moved: this runs from session-start plumbing
    // too (`reconcile` via the hub freshness hook), where a silent mutation
    // would be invisible.
    if (git(['rev-parse', 'main']) !== before) console.log('local main fast-forwarded to origin/main.');
  } catch {
    console.log(NOT_FAST_FORWARDED);
  }
}

// 12.1.2: after a CONFIRMED land (merged PR — or notes already current on
// origin), reconcile the session clone so a successful wrap doesn't read as
// data loss (KB post-wrap-notes-dirty-not-lost): discard the now-redundant
// docs/notes.md edit — ONLY when the F3 predicate proves every uncommitted
// added line is on origin/main — then fast-forward local main (which the
// dirty notes file was blocking). Belt-and-suspenders by design: the compose
// guards refuse most loss cases pre-push, but not the trim edge (>KEEP new
// blocks — the oldest new block is silently trimmed; a blind discard would
// lose it). Best-effort honesty: no failure here changes the wrap's exit
// code, and every failure direction points away from `checkout --`.
function reconcileAfterLand() {
  let verified = false;
  try {
    fetchOriginMain(repoRoot);
    // The two-probe union verdict lives in the lib (notesReconcileVerdict) so
    // this green path, the standalone `reconcile` command, and the hub's
    // SessionStart hook all prove the SAME scope the restore destroys.
    const { added, verdict } = notesReconcileVerdict(repoRoot, notesRel);
    if (verdict === 'safe') {
      verified = true;
    } else {
      console.log(
        `note: ${notesRel} left dirty: some of its ${added.length} uncommitted added line(s) are NOT on origin/main — do NOT discard\n` +
          'by hand; land the missing content separately (it was trimmed from the wrapped note or never composed).'
      );
    }
  } catch {
    console.log(
      `note: could not re-verify ${notesRel} against origin (offline?) — left as-is. Reconcile later,\n` +
        `AFTER confirming your note landed (git show refs/remotes/origin/main:${notesRel} — your block on top):\n` +
        `  git checkout HEAD -- ${notesRel} && git pull --ff-only`
    );
  }
  if (verified) {
    try {
      // checkout HEAD -- (not `checkout --`): restores BOTH index and working
      // tree. A bare `checkout -- <path>` restores from the INDEX, so a staged
      // notes.md would silently survive and falsify the "reconciled" message.
      // The verdict above covers the UNION of the worktree and index deltas —
      // exactly the content this restore destroys — so scope proven == scope
      // restored.
      git(['checkout', 'HEAD', '--', notesRel]);
      console.log(`${notesRel} reconciled (your note is on origin/main).`);
    } catch {
      console.log(`note: could not restore ${notesRel} — reconcile by hand: git checkout HEAD -- ${notesRel}`);
    }
  }
  refreshLocalMain();
}

// The armed-window closer (the gap 12.1.2 left on purpose): an armed wrap
// exits before GitHub's auto-merge fires — at that instant the note is NOT yet
// on origin/main, so a discard would be unproven — and nothing ever re-ran the
// reconcile. The next session on that machine then inherits a dirty notes.md
// that blocks `git pull --ff-only` (KB post-wrap-notes-dirty-not-lost).
// Standalone surface for that moment: same F3 proof, same discard — run by
// hand (`wrap-session.mjs reconcile`) or from the hub's SessionStart freshness
// hook (which imports it with quietWhenClean). A pause DRAFT is refused by
// construction: its lines are not on origin/main, so the verdict is 'unsafe'.
// Never throws and never exits non-zero — session-start plumbing must not
// block. Returns the outcome for callers/tests.
export function reconcile({ fetch = true, quietWhenClean = false, dryRun = false } = {}) {
  let v;
  try {
    if (fetch) fetchOriginMain(repoRoot);
    v = notesReconcileVerdict(repoRoot, notesRel);
  } catch {
    if (dryRun) {
      console.log(`reconcile --dry-run: nothing written — ${notesRel} could not be verified against origin (offline?); a real reconcile would leave it as-is.`);
      return 'unverified';
    }
    console.log(
      `note: could not verify ${notesRel} against origin (offline?) — left as-is. Reconcile later,\n` +
        `AFTER confirming your note landed (git show refs/remotes/origin/main:${notesRel} — your block on top):\n` +
        `  git checkout HEAD -- ${notesRel} && git pull --ff-only`
    );
    return 'unverified';
  }
  if (dryRun) return reconcilePreview(v);
  let state;
  if (!v.dirty) {
    state = 'clean';
    if (!quietWhenClean) console.log(`${notesRel} is clean — nothing to reconcile.`);
  } else if (v.verdict !== 'safe') {
    state = 'unsafe';
    console.log(
      `note: ${notesRel} has ${v.added.length} uncommitted added line(s) NOT on origin/main — left untouched.\n` +
        'Normal for a pause DRAFT (just resume it); otherwise land the content first (wrap, or a docs\n' +
        'PR) — the discard only fires once every line is provably on origin/main.'
    );
  } else {
    try {
      // Same restore as the green path: checkout HEAD -- covers BOTH index and
      // working tree — exactly the union the verdict just proved redundant.
      git(['checkout', 'HEAD', '--', notesRel]);
      state = 'reconciled';
      console.log(`${notesRel} reconciled (the landed wrap note is on origin/main).`);
    } catch {
      console.log(`note: could not restore ${notesRel} — reconcile by hand: git checkout HEAD -- ${notesRel}`);
      return 'restore-failed';
    }
  }
  refreshLocalMain();
  return state;
}

// Story 244.1.5: `reconcile --dry-run` — the verdict and the steps a real reconcile
// would run, read-only (rev-parse, status, rev-list; no checkout, no pull, no ref
// write). Returns 'would-reconcile', 'clean' or 'unsafe'.
function reconcilePreview(v) {
  const lines = [`reconcile --dry-run: nothing written (the working tree, the index and local main are left as they are).`];
  let state;
  if (!v.dirty) {
    state = 'clean';
    lines.push(`verdict: clean — ${notesRel} has no uncommitted change; nothing to restore.`);
  } else if (v.verdict !== 'safe') {
    state = 'unsafe';
    lines.push(`verdict: unsafe — ${v.added.length} uncommitted added line(s) of ${notesRel} are NOT on origin/main; a real reconcile leaves it untouched.`);
  } else {
    state = 'would-reconcile';
    lines.push(`verdict: safe — every uncommitted added line of ${notesRel} is on origin/main.`);
    lines.push(`would run: git checkout HEAD -- ${notesRel}`);
  }
  lines.push(fastForwardPlan({ notesRestored: state === 'would-reconcile' }));
  for (const l of lines) console.log(l);
  return state;
}

// What refreshLocalMain would do now, as one line (read-only). The tree is judged as it
// would stand after the notes restore, so a dry run reports the real run's step. Story
// 248.1.5: a main AHEAD of origin/main as well as behind cannot fast-forward — the real
// run's pull --ff-only (or fetch origin main:main) refuses and prints NOT_FAST_FORWARDED,
// so the preview prints that line, never a pull it cannot make. A main that is only ahead
// is not behind, and the real run's pull is a no-op.
function fastForwardPlan({ notesRestored }) {
  try {
    const behind = Number(git(['rev-list', '--count', 'main..refs/remotes/origin/main']));
    if (!behind) return 'would run: nothing more — local main is not behind origin/main.';
    const n = `${behind} commit(s) behind origin/main`;
    const onMain = git(['rev-parse', '--abbrev-ref', 'HEAD']) === 'main';
    if (onMain) {
      const others = execFileSync('git', ['status', '--porcelain'], { cwd: repoRoot, encoding: 'utf8' })
        .split('\n').filter(Boolean).filter((l) => !(notesRestored && l.slice(3) === notesRel));
      if (others.length) return `would run: no fast-forward — local main is ${n}, and the tree is not quiet (${others.length} other change(s)); pull when it is.`;
    }
    const ahead = Number(git(['rev-list', '--count', 'refs/remotes/origin/main..main']));
    if (ahead) {
      return `would print: ${NOT_FAST_FORWARDED} — local main is ${ahead} commit(s) ahead of and ${behind} behind origin/main (diverged), so the real run's ${onMain ? 'git pull --ff-only origin main' : 'git fetch origin main:main'} refuses.`;
    }
    return onMain ? `would run: git pull --ff-only origin main (local main is ${n})` : `would run: git fetch origin main:main (local main is ${n})`;
  } catch {
    return 'would run: the fast-forward, if it can — local main\'s distance from origin/main could not be read.';
  }
}

// Story 244.1.5 (register class cli-ignores-an-unknown-flag-and-exits-0, form (a)): each
// subcommand's own flags — the census of the fleet's invocations (the Hub Sprint 70
// block's finding 4): wrap's eight, reconcile's --dry-run, recall none. A value flag
// consumes the next word (a missing or flag-like value is the dispatcher's own exit 1) —
// unless that word is one of the subcommand's OWN flags (Story 248.1.5): then the flag is
// left to be read, and the dispatcher names the value flag's missing value, never the
// word after the flag it would have swallowed (`wrap --marker -m "my message"`).
export const SUBCOMMAND_FLAGS = {
  wrap: { bool: ['--dry-run', '--merge', '--amend', '--carry-siblings', '--tombstone-only'], value: ['-m', '--marker', '--session-id'] },
  reconcile: { bool: ['--dry-run'], value: [] },
  recall: { bool: [], value: [] },
};

// The words of `args` that `command` does not take: [] when every one parses; null for
// an unknown subcommand (the dispatcher's usage and exit 1).
export function unknownFlags(command, args) {
  const spec = Object.hasOwn(SUBCOMMAND_FLAGS, command) ? SUBCOMMAND_FLAGS[command] : null;
  if (!spec) return null;
  const bad = [];
  const own = (w) => spec.bool.includes(w) || spec.value.includes(w);
  for (let i = 0; i < args.length; i++) {
    if (spec.bool.includes(args[i])) continue;
    if (spec.value.includes(args[i])) { if (!own(args[i + 1])) i++; continue; }
    bad.push(args[i]);
  }
  return bad;
}

// gh's first error line, for a banner's reason — never a raw stack.
const ghErrorLine = (err) => `${err?.stderr ?? err?.message ?? err}`.trim().split('\n')[0];

// GitHub refuses an auto-merge arm on a PR that is already mergeable: "Pull
// request Pull request is in clean status (enablePullRequestAutoMerge)". A
// docs-only notes PR's checks can pass inside the arm's own window — hub #3317,
// 2026-10-07, bannered HANDOFF INCOMPLETE over a CLEAN PR (Story 171.1.1).
export const isCleanArmRefusal = (text) => /pull request is in clean status/i.test(text ?? '');

// A PR this wrap merged: reconcile the clone and say so. `deleted: false` (Story
// 236.1.1) is the landing whose remote branch delete is UNCONFIRMED — the line
// must not claim it.
function reportLanded(branch, pr, { deleted = true } = {}) {
  reconcileAfterLand();
  try {
    // Quiet (13.1.5): the 13.1.1 lib cleanup — or gh itself — has usually
    // already deleted the local branch; that routine miss must not print.
    gitQuiet(['branch', '-d', branch]);
  } catch {
    /* local branch already removed */
  }
  console.log(
    deleted
      ? `Merged PR #${pr.number} into main and deleted ${branch}.`
      : `Merged PR #${pr.number} into main (the remote delete of ${branch} is unconfirmed — see below).`
  );
  console.log(`origin/main is updated.`);
}

// Story 236.1.1 (hub Initiative 236): gh's `pr merge --delete-branch` deletes the
// remote branch AFTER the merge and exits non-zero when that delete is refused
// (every status but 422; gh 2.92.0) — so a PR that had MERGED threw out of
// forge.prMerge and bannered "was NOT merged" (hub #4107, 2026-10-23: the
// post-merge delete got HTTP 403 under an exhausted App installation pool; the
// notes were already on main). After a merge error the PR is read ONCE more
// (resolveMergeable returns a non-OPEN PR on its first look): MERGED is a
// landing; anything else — OPEN, gh failed, no PR — is null and the caller keeps
// the banner with the why it had. The 403's cause is not needed for the remedy.
function mergedDespiteError(branch) {
  const again = resolveMergeable(branch);
  return again && !again.ghFailed && again.state === 'MERGED' ? again : null;
}

// The landed report when gh exited non-zero after the merge: the merge is a fact
// (the re-read says MERGED), the branch delete is not — name gh's error and the
// one hand command, harmless when the repo's delete_branch_on_merge already
// removed the branch (the hub reads that setting true).
function reportLandedDespiteError(branch, pr, err) {
  reportLanded(branch, pr, { deleted: false });
  console.log(
    `gh pr merge exited non-zero AFTER the merge (${ghErrorLine(err)}) — the PR re-read as MERGED, so the notes ARE on main; ` +
      `only the post-merge branch delete is unconfirmed. If origin still has the branch: git push origin --delete ${branch}`
  );
}

// After the clean refusal (Story 171.1.1): re-read the PR, and merge it only
// when the gate is green, pinned to the head just read — a head that moves
// before the merge is refused by GitHub, and banners. A re-read that is not
// green, or names no head, banners too.
function mergeCleanAfterArm(branch, armedPr) {
  const now = resolveMergeable(branch);
  const pr = now && !now.ghFailed ? now : armedPr;
  const gate = mergeGate(now);
  if (!gate.green || !now.headRefOid) {
    loudIncomplete(branch, pr, gate.green ? { green: false, why: 'its head could not be read' } : gate);
    return false;
  }
  console.log(
    `PR #${now.number}: GitHub refused the auto-merge arm because the PR is already clean — ` +
      `merging it now, pinned to its head ${now.headRefOid}.`
  );
  try {
    forge.prMerge(branch, { methodFlag: repoMergeFlag(), matchHeadCommit: now.headRefOid });
  } catch (err) {
    // Story 236.1.1: a MERGED re-read is a landing whose branch delete failed, not a failed merge.
    const merged = mergedDespiteError(branch);
    if (merged) {
      reportLandedDespiteError(branch, merged, err);
      return true;
    }
    loudIncomplete(branch, now, { green: false, why: `the merge pinned to ${now.headRefOid} failed: ${ghErrorLine(err)}` });
    return false;
  }
  reportLanded(branch, now);
  return true;
}

// Land an already-pushed branch on main: merge its PR if green, else warn.
// `knownPr` lets callers that already created the PR distinguish a transient gh
// blip (we KNOW the PR exists — banner with its number) from true PR-absence
// (10.2.3 LOW). A banner-bound verdict is re-checked once before printing: the
// PR's state can flip between our looks (the arm race), and the banner should
// report a CURRENT reason — or discover the deferral already resolved.
function landOnMain(branch, knownPr) {
  if (!hasGh()) {
    console.error(
      `gh CLI not found — notes are pushed on ${branch}, but the PR cannot be\n` +
        'merged automatically. Merge it manually (or install gh) to update main.'
    );
    return false;
  }
  let runWaited = false;
  for (let attempt = 0; ; attempt++) {
    // A gh-failure sentinel is WORSE information than the PR we just created
    // (knownPr) — prefer knownPr then; otherwise let the sentinel reach
    // mergeGate for the carrier-state-unknown banner (8.1.6, last box).
    const resolved = resolveMergeable(branch);
    const pr = resolved?.ghFailed ? (knownPr ?? resolved) : (resolved ?? knownPr ?? null);
    const gate = mergeGate(pr);
    // When we fell back to knownPr, the gate's reason would misattribute the
    // gh outage to GitHub's async mergeability — say what actually happened.
    if (!gate.green && resolved?.ghFailed && pr === knownPr) {
      gate.why += ` (and the gh lookup itself failed: ${resolved.detail})`;
    }
    if (gate.green) {
      try {
        forge.prMerge(branch, { methodFlag: repoMergeFlag() });
      } catch (err) {
        // The gate said green a moment ago, but the merge itself failed — e.g.
        // the PR flipped BLOCKED between gate and merge (the two-hub-writers
        // race), or gh/network died. Banner + false, never a raw stack
        // (10.2.3 item 2) — unless the PR re-reads MERGED: gh's post-merge
        // branch delete failing after a successful merge is the measured face
        // (Story 236.1.1, hub #4107), and that is a landing.
        const merged = mergedDespiteError(branch);
        if (merged) {
          reportLandedDespiteError(branch, merged, err);
          return true;
        }
        loudIncomplete(branch, pr, { green: false, why: `the merge attempt itself failed: ${ghErrorLine(err)}` });
        return false;
      }
      reportLanded(branch, pr);
      return true;
    }
    // Deferred only because checks haven't finished? The wrap's own notes push
    // restarts the required check, so with a gated main this is the COMMON
    // case, not the exception (Story 8.1.7). Arm GitHub's server-side
    // auto-merge: the PR lands the moment the checks pass, with the ruleset
    // still enforcing them. Failing checks — and BLOCKED with no checks
    // reporting at all (the required-context rename trap, where --auto would
    // wait forever) — keep the loud banner. The run-creation window is waited
    // out ONCE per wrap (Story 166.1.1); a later look reads the rollup once.
    const armable =
      pr?.state === 'OPEN' &&
      pr?.mergeable === 'MERGEABLE' &&
      (pr?.mergeStateStatus === 'BLOCKED' || pr?.mergeStateStatus === 'UNSTABLE');
    const firstWait = armable && !runWaited;
    const checks = armable ? checkState(branch, firstWait ? runCreationWaitSec() * 1000 : 0) : null;
    if (armable) runWaited = true;
    const pending = checks === 'pending';
    if (pending) {
      try {
        // --delete-branch removes the remote session-notes/* branch when the merge fires
        // (kill 1: no leftover branch for a later same-stamp wrap to fast-forward onto).
        forge.prMerge(branch, { auto: true, methodFlag: repoMergeFlag() });
      } catch (err) {
        // GitHub refuses the arm on a PR that turned CLEAN since our read (Story
        // 171.1.1) — that one refusal merges it instead. Any other keeps the
        // banner: e.g. auto-merge disabled on this repo.
        if (isCleanArmRefusal(`${err?.stderr ?? ''}\n${err?.message ?? ''}`)) return mergeCleanAfterArm(branch, pr);
        loudIncomplete(branch, pr, gate);
        return false;
      }
      refreshLocalMain();
      try {
        // The branch lives on as the PR head on origin; a leftover LOCAL copy
        // would collide with a same-minute re-wrap (10.2.3 item 5). Quiet
        // (13.1.5): the 13.1.1 lib cleanup usually already deleted it.
        gitQuiet(['branch', '-D', branch]);
      } catch {
        /* already gone, or we are still on it (checkout above failed) */
      }
      console.log(
        `PR #${pr.number}: checks still running — armed GitHub auto-merge (${repoMergeFlag()}). ` +
          'The notes land on main automatically when the checks pass.'
      );
      console.log(
        `If a check FAILS instead, the PR stays open: fix it, then gh pr merge ${pr.number} ${repoMergeFlag()} --delete-branch`
      );
      // T3b hint: a wrap that armed auto-merge exits before the merge fires, so a loser that
      // silently flips to CONFLICTING would sit OPEN forever. Give the operator the one command
      // to confirm it actually landed.
      console.log(
        `Confirm it landed (not stuck OPEN/CONFLICTING): gh pr view ${pr.number} --json state,mergeStateStatus,mergedAt`
      );
      console.log(
        `Until PR #${pr.number} merges, your clone will look behind and ${notesRel} dirty —\n` +
          "that's the wrap's isolation, not data loss. After it merges:\n" +
          `  git checkout HEAD -- ${notesRel} && git pull --ff-only`
      );
      return true;
    }
    // One more look before the banner (arm race, 10.2.3 LOW) — and after a
    // run-creation wait whose run registered AND finished green inside the
    // window, since the PR state was read before the wait (Story 166.1.1).
    if (attempt === 0 || (firstWait && checks === 'passing')) {
      sleepMs(2000);
      continue;
    }
    loudIncomplete(branch, pr, gate);
    return false;
  }
}

// --- Story 13.1.2: wrap-time sweep of provably-merged [gone] branches ---
// The wrap is the one moment every machine runs the same script with the
// remote freshly consulted, so post-merge branch debris is cured HERE: a
// local branch whose upstream is GONE and whose tip PROVABLY merged via a
// PR is deleted; everything else is KEPT and reported. `[gone]` alone is
// never proof — the fleet squash-merges (so `git branch -d` cannot detect a
// merge) and a remote branch also vanishes when its PR closes UNMERGED. The
// proof standard (a merged PR whose headRefOid equals the local tip) mirrors
// fleet/round.mjs's reconcile. Upstream-less orphans are out of scope BY
// DESIGN — nothing can be proven about a branch with no upstream; Story
// 13.1.1 prevents that class at the source instead. Failure direction is
// always KEEP: a gh failure skips the WHOLE sweep loudly (deleting on a
// flaky gh could destroy unmerged work), and no sweep outcome ever changes
// the wrap's own flow.

// Pure: parse `git for-each-ref refs/heads` lines of the form
// "<short>\t<upstream:short>\t<upstream:track>" into the [gone] subset. A
// branch with NO configured upstream has an empty second field — never
// [gone], so 13.1.1's orphan class is structurally excluded here.
export function parseGoneBranches(forEachRefText) {
  const gone = [];
  for (const line of (forEachRefText ?? '').split('\n')) {
    if (!line.trim()) continue;
    const [branch, upstream, track] = line.split('\t');
    if (branch && upstream && (track ?? '').trim() === '[gone]') gone.push(branch);
  }
  return gone;
}

export function sweepMergedGoneBranches({ dryRun = false } = {}) {
  if (!dryRun) {
    // The sweep's own remote consult: --prune is what marks a deleted
    // upstream [gone]. (The wrap's earlier fetch is main-only, no prune.)
    // Dry-run stays fetch-free like the rest of the wrap's dry run — it
    // previews from the CURRENT tracking state.
    try {
      git(['fetch', '--prune', 'origin']);
    } catch {
      console.log('sweep: skipped — `git fetch --prune` failed (offline?); no branches deleted.');
      return;
    }
  }
  let refs;
  try {
    refs = git(['for-each-ref', 'refs/heads', '--format', '%(refname:short)\t%(upstream:short)\t%(upstream:track)']);
  } catch {
    console.log('sweep: skipped — could not list local branches; no branches deleted.');
    return;
  }
  const gone = parseGoneBranches(refs);
  if (gone.length === 0) {
    console.log(
      dryRun
        ? '[dry-run] sweep: no local branches with a gone upstream (from current tracking state — the real wrap runs `fetch --prune` first).'
        : 'sweep: no local branches with a gone upstream.'
    );
    return;
  }
  let current = null;
  try {
    current = git(['rev-parse', '--abbrev-ref', 'HEAD']);
  } catch {
    /* detached or unreadable — nothing to exclude */
  }
  for (const branch of gone) {
    if (branch === current) {
      console.log(`sweep: kept ${branch} — upstream gone but it is checked out here.`);
      continue;
    }
    let prs;
    try {
      // (17.1.3 note: the old `|| '[]'` empty-stdout guard is gone — an empty
      // success now takes the SKIPPED path below, the keep direction.)
      prs = forge.prListJson({ head: branch, state: 'merged', limit: 100, json: 'number,headRefOid' }, QUIET);
    } catch (err) {
      const detail = `${err?.stderr ?? err?.message ?? err}`.trim().split('\n')[0];
      console.log(`sweep: SKIPPED — gh unavailable (${detail}); no branches deleted (the failure direction is keep).`);
      return;
    }
    let localTip = null;
    try {
      localTip = git(['rev-parse', `refs/heads/${branch}`]);
    } catch {
      /* unreadable ref — falls through to keep */
    }
    const proof = localTip ? prs.find((p) => p.headRefOid === localTip) : undefined;
    if (proof) {
      if (dryRun) {
        console.log(`[dry-run] sweep: would delete ${branch} (merged PR #${proof.number}, tip matches, upstream gone).`);
      } else {
        try {
          git(['branch', '-D', branch]);
          console.log(`sweep: deleted ${branch} (merged PR #${proof.number}, tip matches, upstream gone).`);
        } catch (err) {
          console.log(`sweep: kept ${branch} — delete failed (${`${err?.stderr ?? err?.message ?? err}`.trim().split('\n')[0]}).`);
        }
      }
    } else if (prs.length) {
      console.log(`sweep: kept ${branch} — a merged PR exists but its head != local tip (branch-name reuse? inspect by hand).`);
    } else {
      console.log(`sweep: kept ${branch} — upstream gone but no merged PR found (closed unmerged, or never PR'd).`);
    }
  }
  // S2-D2 (executor-decided at implementation: include): pair the sweep with
  // self-maintaining tracking refs between wraps — one hint, only when unset.
  try {
    git(['config', '--get', 'fetch.prune']);
  } catch {
    console.log('sweep hint: `git config fetch.prune true` keeps deleted upstreams pruned between wraps.');
  }
}

// --- Story 34.1.1: machine-local marker-registry accessors, INLINED ---
// This file is a fleet-synced canonical; scripts/fleet-view/ is hub-only, so
// the marker arm's registry access lives HERE (the Story 33.1.7 single-file
// precedent) instead of loading the fleet-view registry module — which site
// copies cannot resolve (the pre-34.1.1 behavior: every site wrap skipped
// the tombstone and left its marker dangling). Same registry path, same
// read/remove semantics as that module's exports;
// test/wrap-marker-registry.test.mjs pins the two against each other so the
// duplication can't drift.
const fglHome = () => process.env.FGL_HOME || path.join(os.homedir(), '.fgl');
export const markerRegistryPath = () => path.join(fglHome(), 'session-markers.json');
export function readMarkerRegistry() {
  try { return JSON.parse(readFileSync(markerRegistryPath(), 'utf8')); } catch { return { markers: [] }; }
}
// Story 53.1.1: the registry KEY is the worktree-SHARED git dir — the inlined
// twin of the fleet-view module's gitDirKey (the pin test holds the two
// equal from a main checkout AND from a worktree of it). A wrap run from a
// worktree's synced script copy (repoRoot = the worktree path) still finds
// the marker the main checkout's --start registered. Absolute path format
// (git ≥ 2.31) because the bare form returns a relative `.git` from a main
// checkout on Windows; resolved against repoRoot, then the same norm() as
// the writer. null on any failure — the legacy repoRoot match still applies.
const markerNorm = (p) => { try { return realpathSync(p); } catch { return p; } };
export function markerGitDir(repoRoot, gitFn) {
  try {
    const out = String(gitFn(['rev-parse', '--path-format=absolute', '--git-common-dir'])).trim();
    return out ? markerNorm(path.resolve(repoRoot, out)) : null;
  } catch { return null; }
}
// The match rule, mirrored from the module's matchesRegistryKey: gitDir
// equality; else a legacy (repoRoot-only) entry whose <repoRoot>/.git IS the
// caller's git dir; else repoRoot equality (the pre-53.1.1 lookup).
function markerMatchesKey(m, { repoRoot, gitDir = null }) {
  if (gitDir) {
    if (m.gitDir) { if (markerNorm(m.gitDir) === markerNorm(gitDir)) return true; }
    else if (m.repoRoot && markerNorm(path.join(m.repoRoot, '.git')) === markerNorm(gitDir)) return true;
  }
  return Boolean(m.repoRoot && repoRoot) && markerNorm(m.repoRoot) === markerNorm(repoRoot);
}

// Story 42.1.2: the registry is an append-list (same-checkout siblings
// register beside each other). Bare removal only when unambiguous —
// mirrors the fleet-view markers module's removeMarker; the pin test
// (test/wrap-marker-registry.test.mjs) holds the two in agreement. With a
// ref: under the key first, else anywhere in the registry (the 53.1.1
// --marker override).
export function removeMarkerFromRegistry(repoRoot, ref = null, { gitDir = null } = {}) {
  const reg = readMarkerRegistry();
  const mine = reg.markers.filter((m) => markerMatchesKey(m, { repoRoot, gitDir }));
  const found = ref !== null ? mine.find((m) => m.ref === ref) ?? reg.markers.find((m) => m.ref === ref) ?? null
    : mine.length === 1 ? mine[0] : null;
  if (!found) return null;
  reg.markers = reg.markers.filter((m) => m !== found);
  mkdirSync(fglHome(), { recursive: true });
  writeFileSync(markerRegistryPath(), JSON.stringify(reg, null, 2));
  return found;
}

// Rename this session's presence marker to a session-done/* tombstone
// (fleet-view --start's counterpart; spec docs/superpowers/specs/
// 2026-08-12-fleet-coordination-design.md, Story 33.2.1). A bare delete
// removed the only attribution record while the session's activity stayed
// fresh for the full TTL — every wrap manufactured UNDECLARED lines. The
// tombstone carries the final manifest + a `wrapped:` stamp; the rename is
// ONE push with two refspecs (create + delete), so no crash window leaves
// both refs or neither. Registry-linked: only OWN marker, matched by the
// worktree-shared git dir (Story 53.1.1: gitDir = markerGitDir; a legacy
// repoRoot-only entry still matches). Expired markers/tombstones belong to
// --start's sweep (spec Amendment 2026-08-17), never the wrap.
// Story 42.1.2 (spec decision 6): with same-checkout siblings registered,
// an unnamed wrap tombstones NOTHING — newest/oldest guessing tombstones a
// live session's marker in half the interleavings; the caller passes
// --marker <ref> to name its own. Story 53.1.1: a named ref is an explicit
// OVERRIDE — it resolves anywhere in this machine's registry when the key
// misses, and fails loudly when registered nowhere. Story 57.1.1: an
// unnamed wrap under siblings first resolves by the sessionId handshake —
// --start records the harness session's id on the row, and exact equality
// against this process's own id NAMES the row (never guesses: null matches
// nothing, and anything but exactly one match still refuses).
// Story 215.1.4 (Hub Sprint 56 D7): the SESSION ID resolves first. A retry wrap, its
// own marker already tombstoned, found ONE row on the checkout — a sibling's — and
// tombstoned it (hub #3819 §3; candidate class wrap-tombstones-a-sibling-marker-
// when-its-own-is-already-done), because the lone-row branch preceded the handshake.
// Now: a named --marker first (unchanged); then, with an id known, the row whose
// sessionId equals it (exactly one → own); a lone row is taken only when it carries
// no id AND no id is known, or when its id is this session's; a lone row carrying a
// DIFFERENT id — or none while an id is known — is refused with the banner naming
// it; several rows still refuse as before.
// Story 212.1.1: `dryRun` resolves the own marker by the same rules and returns
// `{ tombstoned: false, dryRun: true, ref, doneRef }` with NO git call.
// Story 243.1.1 (hub Initiative 243, spec D8): a row whose marker is held in
// another checkout's origin — the HUB's, for a public repo; fleet-view --start
// records that checkout as the row's `home` — is renamed THERE: every git call
// below runs as `git -C <home> …`, so the public origin never sees the marker or
// its tombstone. A row without a home keeps the argv byte for byte. `home` rides
// the result (the dry run's line names it).
export function tombstoneSessionMarker({ repoRoot, gitDir = null, registry, gitFn, now = new Date(), ref: namedRef = null,
  sessionId = process.env.CLAUDE_CODE_SESSION_ID ?? null, dryRun = false }) {
  if (sessionId === '') sessionId = null; // Story 215.1.4: a blank harness var is no id
  const mine = registry.markers.filter((m) => markerMatchesKey(m, { repoRoot, gitDir }));
  const sidMine = sessionId !== null ? mine.filter((m) => (m.sessionId ?? null) === sessionId) : [];
  const own = namedRef !== null ? mine.find((m) => m.ref === namedRef) ?? registry.markers.find((m) => m.ref === namedRef) ?? null
    : sessionId !== null ? (sidMine.length === 1 ? sidMine[0] : null)
    : mine.length === 1 && (mine[0].sessionId ?? null) === null ? mine[0] : null;
  if (!own) {
    if (namedRef !== null) return { tombstoned: false, reason: `no registry entry ${namedRef} for this repo` };
    // Story 248.1.9 (Hub Sprint 73 leg 4): a refusal carries each candidate row's session id beside the unchanged
    // refs (`candidateRows`), so the banner offers a command only for this session's row or an id-less one; and,
    // when no row here is this session's, the rows carrying its id keyed to ANOTHER checkout (`elsewhere`, each
    // with its repoRoot — a hub-rooted seat's site wrap, whose own row is keyed to the hub checkout). `elsewhere`
    // rides only a refusal and only when non-empty, so no other result changes shape.
    const rowsOf = (ms) => ms.map((m) => ({ ref: m.ref, sessionId: m.sessionId ?? null }));
    const away = sessionId !== null && sidMine.length === 0
      ? registry.markers.filter((m) => (m.sessionId ?? null) === sessionId && !markerMatchesKey(m, { repoRoot, gitDir }))
        .map((m) => ({ ref: m.ref, repoRoot: m.repoRoot ?? null }))
      : [];
    const elsewhere = away.length ? { elsewhere: away } : {};
    if (mine.length === 0) return { tombstoned: false, reason: 'no registry entry for this repo', ...elsewhere };
    if (mine.length === 1 && sessionId === null && (mine[0].sessionId ?? null) !== null) {
      return { tombstoned: false, candidates: [mine[0].ref], candidateRows: rowsOf(mine),
        reason: `the one row on this checkout is session ${mine[0].sessionId}'s and this wrap knows no session id — pass --session-id <uuid> or name yours with --marker <ref>` };
    }
    if (mine.length === 1 && sessionId !== null && sidMine.length === 0) {
      const other = mine[0].sessionId ?? null;
      return { tombstoned: false, candidates: [mine[0].ref], candidateRows: rowsOf(mine), ...elsewhere,
        reason: other === null
          ? `the one row on this checkout carries no session id and this session has one (${sessionId}) — name yours with --marker <ref>`
          : `the one row on this checkout is session ${other}'s, not this session's — name yours with --marker <ref>` };
    }
    return { tombstoned: false, reason: 'multiple session markers registered on this checkout — name yours with --marker <ref>',
      candidates: mine.map((m) => m.ref), candidateRows: rowsOf(mine), ...elsewhere };
  }
  const ref = own.ref;
  const doneRef = ref.replace(/^session\//, 'session-done/');
  const home = own.home ?? null;
  const where = home ? { home } : {};
  if (dryRun) return { tombstoned: false, dryRun: true, ref, doneRef, ...where };
  const g = home ? (argv) => gitFn(['-C', home, ...argv]) : gitFn;
  const tmpRef = `refs/wrap-tombstone/${ref}`;
  g(['fetch', '--quiet', 'origin', `+refs/heads/${ref}:${tmpRef}`]);
  const manifest = String(g(['log', '-1', '--format=%B', tmpRef])).trimEnd();
  const tree = String(g(['rev-parse', `${tmpRef}^{tree}`])).trim();
  const parent = String(g(['rev-parse', tmpRef])).trim();
  const sha = String(g(['commit-tree', tree, '-p', parent, '-m',
    `${manifest}\nwrapped: ${new Date(now).toISOString()}`])).trim();
  g(['push', 'origin', `${sha}:refs/heads/${doneRef}`, `:refs/heads/${ref}`]);
  return { tombstoned: true, ref, doneRef, ...where };
}

// Story 57.1.1: the ambiguity refusal was a one-line console.log — measured
// missed on three consecutive Mac hub wraps (the stranded-LIVE-marker class,
// inbox #1777) while the wrap completed cleanly around it. The unresolvable
// path keeps the HANDOFF-banner register: bordered, on stderr, each candidate
// with its exact re-run command.
// Story 212.1.1: `flags` prefixes each re-run command (the --tombstone-only arm
// names itself, so the printed command is the one to re-run).
// Story 248.1.9 (register class wrap-banner-names-a-siblings-marker-for-the-tombstone): a
// command is offered only for a row whose session id is this session's (`sessionId`) or
// unknown; a row carrying ANOTHER id is named as a sibling's, with no command — five seats in
// one night pasted or met a command that was not theirs. With no id known, a row carrying
// one keeps its command and names its id (the caller cannot tell — the implementer's call,
// named). The session's rows keyed to another checkout (`elsewhere`) print with their
// checkout and the --tombstone-only command to run there. A result without `candidateRows`
// (the pre-248.1.9 shape) reads every candidate as id-less.
export function markerRefusalBanner({ reason, candidates, candidateRows = null, elsewhere = [] }, flags = '', { sessionId = null } = {}) {
  const rows = candidateRows ?? candidates.map((ref) => ({ ref, sessionId: null }));
  const yours = [];
  const siblings = [];
  for (const r of rows) {
    const id = r.sessionId ?? null;
    const cmd = `    node scripts/wrap-session.mjs wrap ${flags}--marker ${r.ref}`;
    if (id === null) yours.push(`${cmd}\n`);
    else if (sessionId === null) yours.push(`${cmd}   (session ${id}'s row — yours only if this session is ${id})\n`);
    else if (id === sessionId) yours.push(`${cmd}   (this session's row)\n`);
    else siblings.push(`    ${r.ref} — session ${id}'s row, a sibling's: never name it\n`);
  }
  return (
    '\n============================================================\n' +
    '  MARKER NOT TOMBSTONED\n' +
    `  ${reason}\n` +
    (yours.length ? '  This session\'s marker stays LIVE until you name yours:\n' + yours.join('') : '') +
    (siblings.length ? '  Not this session\'s (no command):\n' + siblings.join('') : '') +
    (elsewhere.length
      ? '  This session\'s row on another checkout — run there:\n' +
        elsewhere.map((e) => `    (in ${e.repoRoot ?? 'another checkout'}) node scripts/wrap-session.mjs wrap --tombstone-only --marker ${e.ref}\n`).join('')
      : '') +
    (!yours.length && !elsewhere.length ? '  No row here is this session\'s: its marker may already be tombstoned, or registered under another key.\n' : '') +
    '============================================================\n'
  );
}

// Story 248.1.9: the reason-only path's line for a session row keyed to another checkout.
export const elsewhereLine = (e) =>
  `elsewhere: this session's row ${e.ref} is keyed to ${e.repoRoot ?? 'another checkout'} — run there: node scripts/wrap-session.mjs wrap --tombstone-only --marker ${e.ref}`;

// Story 216.1.7: the marker arm's lines — one pure helper for the full wrap's
// arm and its dry run, so a reason-only result is never silent. tombstoned →
// the one line as before; candidates → the refusal banner as before; a dry run
// → what it would do; reason-only → the reason and, when a ref was NAMED (a
// truncated or mistyped --marker: Mac Hub's wrap of 2026-10-20 passed `…-mac`
// for `…-mac-40c07a` and its marker stayed LIVE in silence), the refs
// registered on this checkout and the recovery command for each. The call
// sites print; the exit stays the handoff's (a wrap never fails on its marker).
export function markerCleanupLines(res, { namedRef = null, registeredRefs = [], sessionId = null, dryRun = false } = {}) {
  if (res.tombstoned) return [`session marker → tombstone: ${res.doneRef}`];
  if (res.candidates) return [markerRefusalBanner(res, '', { sessionId })];
  if (res.dryRun) return [`would tombstone ${res.ref} → ${res.doneRef}${res.home ? ` in ${res.home}'s origin` : ''} and remove its registry row`];
  const lines = [`marker not tombstoned: ${res.reason}`];
  for (const e of res.elsewhere ?? []) lines.push(elsewhereLine(e)); // Story 248.1.9: the --repos face
  if (namedRef !== null) {
    // Story 220.1.2 (Hub Sprint 59 leg 1, D4 — the two Sprint 57 arm nits): a row is a ref
    // string (the pre-220.1.2 shape) or { ref, sessionId }; the row whose sessionId is this
    // process's is marked, siblings' rows stay unmarked; under --dry-run nothing has wrapped yet,
    // so the recovery named is the REAL wrap (`wrap --marker` with the ref), never --tombstone-only.
    const rows = registeredRefs.map((r) => (typeof r === 'string' ? { ref: r, sessionId: null } : r));
    lines.push(`registered on this checkout: ${rows.length ? rows.map((r) => r.ref).join(', ') : 'none'}`);
    const verb = dryRun ? 'wrap --marker' : 'wrap --tombstone-only --marker';
    for (const r of rows) {
      const own = sessionId !== null && (r.sessionId ?? null) === sessionId ? " (this session's row)" : '';
      lines.push(`recover: node scripts/wrap-session.mjs ${verb} ${r.ref}${own}`);
    }
  }
  return lines;
}

// --- Story 24.3.2: the hub wrap runs its own fleet heartbeat ---
// Habit→contract: a hub wrap no longer relies on protocol memory to run
// check-fleet.mjs first — the wrap invokes it itself and stamps the summary
// into the note it lands, so no hub wrap ships without a heartbeat verdict.
// Hub mode is a CAPABILITY probe: this file is fleet-synced verbatim
// (CANONICALS), but scripts/check-fleet.mjs is hub-only — never in the sync's
// canonical set — so a synced site copy finds no heartbeat script and behaves
// exactly as before. The stamp is a RECORD, never a gate (the D1 precedent:
// report-level only): a red heartbeat lands loudly in the note, the wrap
// still lands the handoff, and the wrap's exit code keeps tracking the
// handoff alone.
export function fleetHeartbeatScript(root) {
  const p = path.join(root, 'scripts', 'check-fleet.mjs');
  return existsSync(p) ? p : null;
}

// Run the heartbeat and capture its output + exit code. execFileSync argv (no
// shell — the 8.1.1 standard); the exit code is the child's own (err.status),
// never a pipeline's. stderr is piped, not ignored, so a crash still carries
// its diagnostic.
export function runFleetHeartbeat(root, scriptPath) {
  try {
    const output = execFileSync(process.execPath, [scriptPath], {
      cwd: root,
      encoding: 'utf8',
      stdio: ['ignore', 'pipe', 'pipe'],
      maxBuffer: 16 * 1024 * 1024,
    });
    return { exitCode: 0, output };
  } catch (err) {
    if (typeof err?.status === 'number') {
      return { exitCode: err.status, output: `${err.stdout ?? ''}` };
    }
    return { exitCode: null, output: '', error: `${err?.message ?? err}`.trim().split('\n')[0] };
  }
}

// Story 220.1.2 (Hub Sprint 59 leg 1, D3): the dry run's reuse of a young heartbeat stamp.
// check-fleet.mjs writes `heartbeat-last.json` under fglHome() after every FULL run it completes
// ({ clock, root, exitCode, lines }); the hub-mode dry run reads it here through the same home
// seam as the marker registry (fglHome — no new import into this canonical). Reusable iff the
// stamp parses, its root is this run's root (sameRepoRoot: case-folded on win32/darwin — a
// worktree's or a fixture hub's stamp never serves the checkout) and its clock is within
// HEARTBEAT_STAMP_REUSE_MS; anything else re-runs, loud about why. The REAL wrap never reads
// it — its stamp is the note's evidence.
export const HEARTBEAT_STAMP_REUSE_MS = 10 * 60 * 1000;
const hhmmZ = (ms) => `${new Date(ms).toISOString().slice(11, 16)}Z`;
export function readReusableHeartbeatStamp({ root, now = Date.now(), home = fglHome() }) {
  const file = path.join(home, 'heartbeat-last.json');
  if (!existsSync(file)) return { reuse: false, reason: 'absent' };
  let stamp;
  try { stamp = JSON.parse(readFileSync(file, 'utf8')); } catch { return { reuse: false, reason: 'unparsable' }; }
  if (!stamp || typeof stamp !== 'object' || !Array.isArray(stamp.lines) || typeof stamp.root !== 'string') return { reuse: false, reason: 'unparsable' };
  const at = Date.parse(stamp.clock);
  if (Number.isNaN(at)) return { reuse: false, reason: 'clock unreadable' };
  if (!sameRepoRoot(stamp.root, root)) return { reuse: false, reason: `another root (${stamp.root})` };
  const age = now - at;
  // Story 234.1.3 (hub Initiative 234): the reason names the face it measured — a stamp
  // ahead of the clock is "in the FUTURE", one past the window "old" (the Hub Sprint 59
  // review measured `future +5m → older than ten minutes (18:05Z)`: the refusal right, the
  // reason wrong). The refusal itself is Story 220.1.2's rule, unchanged.
  if (age < 0 || age > HEARTBEAT_STAMP_REUSE_MS) {
    const span = (ms) => { const m = Math.floor(ms / 60_000); const s = Math.floor((ms % 60_000) / 1000); return `${m} min${s ? ` ${s} s` : ''}`; };
    const face = age < 0 ? `${span(-age)} in the FUTURE` : `${span(age)} old`;
    return { reuse: false, reason: `${face} — outside the ten-minute window (${hhmmZ(at)})` };
  }
  return { reuse: true, stamp, clock: hhmmZ(at) };
}

// The dry run's heartbeat step: prints through `log`, returns { reused, lines }. `run` is the
// heartbeat spawn (runFleetHeartbeat by default; injectable for the test, which never spawns).
export function dryRunHeartbeat({ root, scriptPath, run = runFleetHeartbeat, now = Date.now(), home = fglHome(), log = console.log }) {
  const r = readReusableHeartbeatStamp({ root, now, home });
  let stamp;
  if (r.reuse) {
    log(`[dry-run] hub wrap: reusing the heartbeat stamp from ${r.clock} (younger than ten minutes; the real wrap runs its own)`);
    stamp = heartbeatStampLines({ exitCode: r.stamp.exitCode, output: `\n${r.stamp.lines.join('\n')}\n` });
    stamp[0] = `${stamp[0]} (reused from ${r.clock})`;
  } else {
    log(`[dry-run] hub wrap: no reusable heartbeat stamp (${r.reason}) — running the fleet heartbeat (scripts/check-fleet.mjs)…`);
    stamp = heartbeatStampLines(run(root, scriptPath));
  }
  log('[dry-run] would stamp into the top note block:');
  for (const l of stamp) log(`            ${l}`);
  return { reused: r.reuse, lines: stamp };
}

// Pure: the stamp lines for a heartbeat result — the `Fleet:` summary plus
// every non-ok detail line (EXPECTED/FAIL/MISSING/flaky/pending and the
// unused-waiver notes); plain `ok` lines are noise the summary already
// counts. A heartbeat that crashed (or printed no summary) stamps a loud
// FAILED line — the note then honestly records the missing verdict instead
// of silently shipping without one.
export function heartbeatStampLines({ exitCode, output, error }) {
  const label = '**Fleet heartbeat at wrap (auto-stamped by wrap-session):**';
  if (error != null) {
    return [
      `${label} FAILED to run — ${error}. No verdict; run ` +
        '`node scripts/check-fleet.mjs` by hand and record the summary.',
    ];
  }
  const lines = (output ?? '').split(/\r?\n/);
  // Story 220.1.1 (hub Initiative 220): a heartbeat that could not afford its GitHub REST
  // reads prints ONE line — the quota, the reset — and exits 2 with no arm run; the stamp
  // carries it verbatim, never a FAILED-TO-DERIVE stack and never the no-summary notice. A
  // skipped heartbeat is a record, not a blocker: the wrap lands, the next stamp measures.
  const skipped = lines.find((l) => /^gh quota: .* — heartbeat skipped\b/.test(l));
  if (skipped) return [`${label} ${skipped.trim()}`];
  let fleetIdx = -1;
  for (let i = lines.length - 1; i >= 0; i--) {
    if (lines[i].startsWith('Fleet: ')) {
      fleetIdx = i;
      break;
    }
  }
  if (fleetIdx === -1) {
    return [
      `${label} ran (exit ${exitCode}) but printed no \`Fleet:\` summary line — ` +
        'inspect `node scripts/check-fleet.mjs` by hand.',
    ];
  }
  // Only the summary SECTION feeds the stamp: check-fleet prints a blank
  // line, then one line per outcome, then the `Fleet:` total — so the
  // contiguous non-empty run ending at the total is the section, and each
  // checker's RELAYED output above it (which repeats FAIL-shaped lines on
  // the confirmation retry) never reaches the note.
  let start = fleetIdx;
  while (start > 0 && lines[start - 1].trim() !== '') start--;
  const detail = lines
    .slice(start, fleetIdx)
    .filter((l) => /^(EXPECTED|FAIL|MISSING|flaky→ok|pending|note)\s/.test(l));
  const head = `${label} ${lines[fleetIdx]}` + (exitCode === 0 ? '' : ' **NEW reds — explain them above.**');
  return [head, ...detail.map((l) => `- \`${l.trim()}\``)];
}

// Pure: append the stamp to the END of the TOP session block, preserving the
// block's exact trailing-whitespace run (block boundaries are significant to
// splitNotes). The CALLER decides when to stamp — a wrap landing no new block
// must never mutate an already-landed note.
export function stampTopBlock(text, stampLines) {
  return stampBlockAt(text, stampLines, 0);
}

// Pure: stamp the END of the session block at index i (same trailing-run rule).
function stampBlockAt(text, stampLines, i) {
  const { header, sessions } = splitNotes(text);
  if (!sessions.length || !stampLines.length || i < 0 || i >= sessions.length) return text;
  const target = sessions[i];
  const trailing = target.match(/\s*$/)[0];
  const stamped =
    target.slice(0, target.length - trailing.length) + '\n\n' + stampLines.join('\n') + (trailing || '\n');
  return header + sessions.slice(0, i).join('') + stamped + sessions.slice(i + 1).join('');
}

// --- Story 208.1.1 (hub Initiative 208): a wrap lands and stamps only its own block ---
// MEASURED 2026-10-14 00:38Z: composeNotes prepends every local block origin lacks and
// the hub stamp landed in the TOP block whoever wrote it — hub #3675 carried Sprint 53's
// not-yet-wrapped block out with Sprint 54's heartbeat, and #3676 CONFLICTED. The wrap
// now reads its own session id (CLAUDE_CODE_SESSION_ID, as fleet-view's role declaration
// does; --session-id <uuid> the printed escape) and judges the compose's NEW blocks.
export const OWN_BLOCK_REFUSAL = "wrap refused: the top block is not this session's";

// A block CARRIES a session id when its text holds the full uuid, or the id's first
// eight hex characters followed by an ellipsis — the notes' `26aabc44-…` provenance form.
export function blockCarriesSessionId(block, sessionId) {
  if (typeof block !== 'string' || typeof sessionId !== 'string' || sessionId.length < 8) return false;
  if (block.includes(sessionId)) return true;
  const short = sessionId.slice(0, 8);
  if (!/^[0-9a-f]{8}$/i.test(short)) return false;
  return new RegExp(`${short}-?(…|\\.\\.\\.)`).test(block);
}

// Pure: judge the compose's NEW blocks (its prefix of the local file) against this session's
// id. The top new block must carry it; more than one new block refuses unless carrySiblings;
// no id refuses (never guessed). → { ok, ownIndex, carried } | { ok: false, reason }.
export function ownBlockGuard({ newBlocks, sessionId, carrySiblings = false }) {
  if (!newBlocks || newBlocks.length === 0) return { ok: true, ownIndex: -1, carried: [] };
  const top = headingKey(newBlocks[0]);
  if (!sessionId) {
    return {
      ok: false,
      reason:
        `${OWN_BLOCK_REFUSAL} — no session id to match it against: CLAUDE_CODE_SESSION_ID is not set in this\n` +
        `shell, and the guard never guesses. Top new block: ${top}. Pass --session-id <uuid> (your harness session id).`,
    };
  }
  if (!blockCarriesSessionId(newBlocks[0], sessionId)) {
    const own = newBlocks.findIndex((b) => blockCarriesSessionId(b, sessionId));
    return {
      ok: false,
      reason:
        `${OWN_BLOCK_REFUSAL} — the top new block ${top} does not carry session id ${sessionId}` +
        (own > 0 ? ` (this session's block is ${headingKey(newBlocks[own])}, below it)` : '') +
        `.\nA sibling's note never rides a wrap unasked (hub Initiative 208). Either add \`Session id \\\`${sessionId}\\\`\`\n` +
        'to your own block\'s provenance line and put it on top, or move the sibling\'s block to that session\'s\n' +
        'scratchpad; --carry-siblings lands the blocks below YOUR top block with it, never a foreign top.',
    };
  }
  // Blocks below the top that carry THIS session's id are this session's own (a session may
  // write more than one block — the 12.1.2 trim-edge e2e writes four); only the others are siblings.
  const siblings = newBlocks.slice(1).filter((b) => !blockCarriesSessionId(b, sessionId)).map(headingKey);
  if (siblings.length && !carrySiblings) {
    return {
      ok: false,
      reason:
        `${OWN_BLOCK_REFUSAL} alone — ${siblings.length} other new block(s) not carrying this session's id sit below it in the local notes and would\n` +
        `ride to main inside this wrap: ${siblings.join(' | ')}.\nMove them to their sessions' scratchpads, or pass --carry-siblings to carry them on purpose (hub Initiative 208).`,
    };
  }
  return { ok: true, ownIndex: 0, carried: siblings };
}

// Pure: stamp the END of the first session block that carries sessionId — this session's
// own block, never the top by position. No carrying block → text unchanged (the caller
// has already refused by then; a stamp never lands in a block it cannot name).
export function stampSessionBlock(text, stampLines, sessionId) {
  const { sessions } = splitNotes(text);
  const i = sessions.findIndex((b) => blockCarriesSessionId(b, sessionId));
  return i === -1 ? text : stampBlockAt(text, stampLines, i);
}

// --- Story 236.2.1 (hub Initiative 236, Hub Sprint 64 D4): a wrap lands only a block of its own ---
// MEASURED 2026-10-24 from the script's shape: the commit subject was the local TOP block's heading
// by position (208.1.1 moved the stamp to the id-carrying block, not the title), and a compose that
// lands no new block still lands a diff when it trims. The face (RELAYED from two first-hand notes):
// Mac Overflow's re-run of 2026-10-23 landed hub #4132 → 9548d6f8, +0/−24 — a three-block trim and
// nothing else, under Windows Hub 3's heading.
export const LANDING_REFUSAL = 'wrap refused: no block carrying session id';

// Pure: the heading of the block carrying sessionId among the blocks that LAND — the compose's new
// blocks (the local prefix origin lacks) and, under --amend, the local copies of the diverged ones.
// → { ok: true, title } | { ok: false, reason }. The CALLER applies the refusal only when the compose
// would still change docs/notes.md: an empty diff stays "already current" (exit 0).
export function landingTitle({ sessions, composed, sessionId, amend = false }) {
  const landing = sessions.slice(0, composed.newCount);
  if (amend && composed.diverged.length) {
    const diverged = new Set(composed.diverged);
    landing.push(...sessions.slice(composed.newCount).filter((b) => diverged.has(headingKey(b))));
  }
  const own = sessionId ? landing.find((b) => blockCarriesSessionId(b, sessionId)) : undefined;
  if (own) return { ok: true, title: sessionTitle(own) };
  const id = sessionId || '(none — CLAUDE_CODE_SESSION_ID is not set in this shell; pass --session-id <uuid>)';
  const only = composed.dropped > 0 ? `a trim of ${composed.dropped} block(s)` : 'changes to blocks this session did not write';
  return {
    ok: false,
    reason:
      `${LANDING_REFUSAL} ${id} is landing — the compose would land only ${only}; a wrap lands a session note, never a bare trim; nothing pushed.\n` +
      'Your local notes are untouched. Either restore your block to the top of docs/notes.md (its provenance line carrying\n' +
      'your session id) and re-run, or let the trim ride your next real wrap.',
  };
}

function wrap({ message, dryRun, merge, amend, marker = null, carrySiblings = false, sessionIdArg = null, tombstoneOnly = false }) {
  guardCwdRepo();
  // Story 208.1.1: this session's id — the env var the harness hands every session, or the
  // printed escape; null refuses in the compose (never guessed).
  const sessionId = sessionIdArg ?? process.env.CLAUDE_CODE_SESSION_ID ?? null;
  const opInProgress = inProgressGitOp();
  if (opInProgress) {
    console.error(
      `Refusing to wrap: ${opInProgress} in this repo — ${notesRel} may hold\n` +
        'conflict markers mid-operation, and the wrap would commit them to main.\n' +
        'Finish or abort the operation first (commit it, git merge --abort, or\n' +
        'git rebase --abort), then re-run the wrap.'
    );
    process.exit(1);
  }
  // A detached HEAD has no branch name — remember the SHA instead so the
  // --dry-run summary (the only place startBranch is displayed now that the
  // wrap never switches branches) shows something meaningful. The read is
  // the wrap's first touch of this repo's git state, so it doubles as the
  // guarded sentinel for "not a repo / git broken".
  let startBranch;
  try {
    startBranch = git(['rev-parse', '--abbrev-ref', 'HEAD']);
  } catch (err) {
    const detail = `${err?.stderr ?? err?.message ?? err}`.trim().split('\n')[0];
    console.error(
      `Cannot read this repo's git state (${repoRoot}):\n  ${detail}\n` +
        'Is git installed, and is this actually a git repo? Nothing was changed.'
    );
    process.exit(1);
  }
  if (startBranch === 'HEAD') startBranch = git(['rev-parse', 'HEAD']); // detached: display the SHA

  // Story 212.1.1: --tombstone-only — the marker arm ALONE, for a session whose
  // note block already rides main by another PR (a hub-rooted sub-repo session
  // whose hub wrap refused by design because the top block was a sibling's —
  // register class wrap-leaves-other-repo-marker-live, seat 2, 2026-10-17). No
  // notes read, no heartbeat, no fetch of main, no sweep, no PR: the marker
  // stops reading LIVE in fleet-view instead of aging out over its 8 h TTL.
  // Resolution is the wrap's own (one registered row, the sessionId handshake,
  // or --marker under siblings — never a guess). A refusal is LOUD here (exit
  // 1): the tombstone is this run's whole job, where the full wrap contains
  // marker trouble — and, since Story 216.1.7, SAYS so (markerCleanupLines on
  // stderr, the exit unchanged) — so a handoff never fails on it. Runs BEFORE the notes read —
  // this path must not need a docs/notes.md at all.
  if (tombstoneOnly) {
    const gitDir = markerGitDir(repoRoot, (argv) => git(argv));
    const res = tombstoneSessionMarker({
      repoRoot, gitDir, registry: readMarkerRegistry(), gitFn: (argv) => git(argv), ref: marker, sessionId, dryRun,
    });
    if (res.tombstoned) {
      removeMarkerFromRegistry(repoRoot, res.ref, { gitDir });
      console.log(`session marker → tombstone: ${res.doneRef}`);
      return;
    }
    if (res.dryRun) {
      console.log(`[dry-run] would tombstone ${res.ref} → ${res.doneRef}${res.home ? ` in ${res.home}'s origin` : ''} and remove its registry row — nothing else (no note, no heartbeat, no fetch, no sweep, no PR)`);
      return;
    }
    // Story 248.1.9: the banner offers only this session's rows; the reason-only line is followed by
    // this session's rows keyed to another checkout, each with its --tombstone-only command, run there.
    if (res.candidates) console.error(markerRefusalBanner(res, '--tombstone-only ', { sessionId }));
    else for (const line of markerCleanupLines(res, { sessionId })) console.error(line);
    process.exit(1);
  }

  const { header, sessions } = loadNotes();
  if (sessions.length === 0) {
    console.error(`No "## Session" blocks in ${notesRel} — write the notes first.`);
    process.exit(1);
  }
  if (isPauseDraft(sessions[0])) {
    console.error(
      `The newest block in ${notesRel} is a DRAFT (pause checkpoint). Replace it\n` +
        'with the real wrap note first — wrapping over it would commit the draft\n' +
        'as-is and could trim away a real session with no way to recover it.'
    );
    process.exit(1);
  }

  const wrapBranch = notesBranchName(new Date());
  // Read porcelain RAW — not via git(), which .trim()s the whole output and would strip
  // the leading status-column space off the first line, corrupting its path when
  // offendingChanges slices the fixed 3-char prefix (a notes-only dirty tree — the normal
  // wrap case — would then look "offending" and the wrap would refuse every run).
  const offending = offendingChanges(
    execFileSync('git', ['status', '--porcelain'], { cwd: repoRoot, encoding: 'utf8' }),
    notesRel
  );

  // Pillar A: the wrap commits from an ISOLATED worktree, so dirty files cannot
  // ride onto the notes branch. Only notes.md-mid-merge (conflict markers) must
  // still refuse; everything else is an informational nudge. 11.4.7: this
  // refusal runs BEFORE the dry-run early-return — a dry-run of a wrap that
  // cannot run must say so, not print a happy plan.
  if (offending.includes(notesRel)) {
    console.error(
      `Refusing to wrap: ${notesRel} is UNMERGED (conflict markers) — resolve the\n` +
        'conflict first, then re-run the wrap.'
    );
    process.exit(1);
  }

  if (dryRun) {
    // dropped is estimated locally here — the real value comes from composing against
    // origin/main, which a side-effect-free dry run does not fetch.
    const est = Math.max(0, sessions.length - KEEP);
    console.log(dryRunPlan({ startBranch, sessions: sessions.length, dropped: est, wrapBranch, offending }));
    // 13.1.2: preview the sweep too (verdicts only, nothing deleted). Contained:
    // a sweep failure never changes the dry run's outcome.
    try {
      sweepMergedGoneBranches({ dryRun: true });
    } catch (err) {
      console.log(`sweep: skipped (${`${err?.message ?? err}`.split('\n')[0]}); no branches deleted.`);
    }
    // Story 216.1.7: the dry run resolves the marker it would take (dryRun —
    // no git call) and prints the helper's lines, so a named ref registered
    // nowhere shows its reason, the registered refs and the recovery command
    // here too. Contained like the sweep: a marker failure never changes the
    // dry run's outcome. Story 220.1.2 (D4): the rows carry their session ids so
    // the recover lines mark this session's row, and the recovery named is the
    // REAL wrap --marker (nothing has wrapped yet), never --tombstone-only.
    try {
      const gitDir = markerGitDir(repoRoot, (argv) => git(argv));
      const registry = readMarkerRegistry();
      // Story 248.1.9: the session id rides here and in retireMarker as it does in the
      // --tombstone-only arm, so the refusal's `--session-id` advice names a flag the wrap reads.
      const res = tombstoneSessionMarker({ repoRoot, gitDir, registry, gitFn: (argv) => git(argv), ref: marker, sessionId, dryRun: true });
      const registeredRefs = registry.markers.filter((m) => markerMatchesKey(m, { repoRoot, gitDir })).map((m) => ({ ref: m.ref, sessionId: m.sessionId ?? null }));
      for (const line of markerCleanupLines(res, { namedRef: marker, registeredRefs, sessionId, dryRun: true })) console.log(`[dry-run] ${line}`);
    } catch (err) {
      console.log(`[dry-run] marker: skipped (${`${err?.message ?? err}`.split('\n')[0]})`);
    }
    // Story 208.1.1: the own-block verdict on the LOCAL top block (the real wrap judges the
    // compose's new blocks against origin, which a side-effect-free dry run does not fetch).
    const ownDry = ownBlockGuard({ newBlocks: [sessions[0]], sessionId, carrySiblings });
    console.log(ownDry.ok ? `top block carries this session's id (${sessionId}) — the wrap would land and stamp it` : `[dry-run] would refuse — ${ownDry.reason.split('\n')[0]}`);
    // 24.3.2: the hub dry run demonstrates the stamp — run the real heartbeat
    // (read-only) and preview the exact lines the wrap would append to the
    // top note block. Site copies: no heartbeat script, no output.
    // Story 220.1.2: a stamp younger than ten minutes from THIS root is reused (no heartbeat
    // spawned — one heartbeat per wrap, not two); otherwise the run as before. dryRunHeartbeat
    // prints which, and why.
    const dryHbScript = fleetHeartbeatScript(repoRoot);
    if (dryHbScript) dryRunHeartbeat({ root: repoRoot, scriptPath: dryHbScript });
    return;
  }
  if (offending.length) {
    console.log(
      `note: uncommitted changes outside ${notesRel} — they stay in your working tree\n` +
        '(the wrap writes from an isolated worktree). Commit finished work on its branch:\n' +
        offending.map((p) => `  ${p}`).join('\n')
    );
  }

  if (merge) {
    console.log(
      'note: --merge is now the default — the note always self-lands on its own docs ' +
        'branch, so the flag is no longer needed.'
    );
  }

  // Fetch origin/main: needed both to branch the note off it AND to compose the note
  // against origin's CURRENT notes rather than this session's possibly-stale local file —
  // the T3 clobber fix (Decision D2 / Story 10.2.2).
  try {
    fetchOriginMain(repoRoot);
  } catch {
    console.error(
      `Could not fetch origin/main (offline?). Your notes edit is intact and uncommitted —\n` +
        'retry the wrap when you are back online.'
    );
    process.exit(1);
  }

  // T3b preflight: loudly flag any earlier wrap PR stuck OPEN+CONFLICTING —
  // its note is stranded on its branch and nothing else will ever surface it.
  sweepStrandedWrapPrs();

  // 13.1.2: cure post-merge [gone] branch debris while the remote is fresh.
  // Contained: no sweep outcome — including a crash — changes the wrap's own
  // notes flow (the epic's "untouched by a sweep failure" clause).
  try {
    sweepMergedGoneBranches({ dryRun: false });
  } catch (err) {
    console.log(`sweep: skipped (${`${err?.message ?? err}`.split('\n')[0]}); no branches deleted.`);
  }

  // Compose the note against origin/main's notes.md, NOT the stale local file (kill 2):
  // prepend this session's new block(s) to origin's blocks and trim — so a note merged
  // concurrently by another session (on origin, absent from this local file) survives.
  // Read RAW — not via git(), whose .trim() strips the file's trailing newline and
  // corrupts the composed content (phantom whitespace diffs that also defeat the
  // "already current" check below).
  // Story 148.1.1: every read goes THROUGH a recorded sha of origin/main, and the
  // write worktree is cut from the sha the final compose read — so a move after
  // that read surfaces as a real conflict on the wrap PR, never a silent
  // whole-file overwrite.
  const readOriginNotes = () => {
    const sha = git(['rev-parse', 'origin/main']);
    try {
      // maxBuffer (hub Story 97.1.4): a content read through the child-process
      // default 1 MiB cap dies ENOBUFS and lands in the catch as "not on
      // origin" — 64 MiB, the family's cap.
      return { sha, text: execFileSync('git', ['show', `${sha}:docs/notes.md`], { cwd: repoRoot, encoding: 'utf8', maxBuffer: 64 * 1024 * 1024 }) };
    } catch {
      return { sha, text: '' }; // notes.md not yet on origin (brand-new repo) — compose from local alone
    }
  };
  const composeAgainst = (originText) =>
    composeNotes({
      header,
      localSessions: sessions,
      originSessions: splitNotes(originText).sessions,
      keep: KEEP,
      amend,
    });

  // The compose's refusals, run on EVERY compose (Story 148.1.1: the re-compose
  // re-runs them against the moved origin). A block missing from the local file
  // refuses only at the first read; one that arrived after it is kept (below).
  const refuseUnsafeCompose = ({ newCount, missingFromLocal, stranded, diverged }, { firstRead }) => {
    // The pre-fetch guard above catches a DRAFT on top; a DRAFT further down can
    // still be NEW vs origin (e.g. [real note, stale draft, …]) and would ride to
    // main inside the composed note. Check every new block (10.2.3 item 6).
    const draftBelowTop = sessions
      .slice(0, newCount)
      .filter((s) => isPauseDraft(s))
      .map(headingKey);
    if (draftBelowTop.length) {
      console.error(
        `Refusing to wrap: ${draftBelowTop.length} DRAFT block(s) sit among the NEW session note(s)\n` +
          `in ${notesRel} — wrapping would commit the draft(s) to main:\n` +
          draftBelowTop.map((h) => `  ${h}`).join('\n') +
          '\nFold each draft into the real wrap note (or delete it), then re-run the wrap.'
      );
      process.exit(1);
    }

    // Story 208.1.1: the top new block must be THIS session's; a sibling's block below it
    // rides only on --carry-siblings; no session id refuses (never guessed).
    const own = ownBlockGuard({ newBlocks: sessions.slice(0, newCount), sessionId, carrySiblings });
    if (!own.ok) {
      console.error(own.reason);
      process.exit(1);
    }
    if (firstRead) for (const h of own.carried) console.log(`wrap: carrying sibling block ${h} (--carry-siblings)`);

    // Kill 3: if origin/main carries session notes this local file never saw, the local
    // notes are BEHIND — refuse loudly so a stale-clone wrap can't proceed on an outdated
    // base (the T3 window). The composed note above already avoids data loss; stopping here
    // keeps the human working from current state.
    if (firstRead && missingFromLocal.length) {
      console.error(behindRefusal({ notesRel, missingFromLocal }));
      process.exit(1);
    }

    // A local-only block BELOW the new-block prefix would be silently dropped by the
    // compose (it builds from origin's blocks) — absent from what lands on origin/main.
    // Refuse loudly — the wrap only prepends new TOP block(s) to origin's current notes.
    if (stranded.length) {
      console.error(
        `Refusing to wrap: ${stranded.length} local session block(s) below your new note are NOT\n` +
          `on origin/main — the wrap would silently drop them from ${notesRel}:\n` +
          stranded.map((h) => `  ${h}`).join('\n') +
          '\nYour file is untouched. The wrap only prepends new top block(s) to origin\'s current\n' +
          'notes: remove the stranded block(s), or land them via a normal docs PR instead.'
      );
      process.exit(1);
    }

    // An in-place edit to a block that already exists on origin (same heading, different
    // content — e.g. amending the just-landed top note) is never resolved silently:
    // origin-wins would discard the amendment, local-wins would re-open the T3 content
    // clobber. Refuse loudly by default; --amend is the deliberate local-wins path.
    if (diverged.length && !amend) {
      console.error(
        `Refusing to wrap: your local ${notesRel} EDITS ${diverged.length} session note(s) that\n` +
          'already exist on origin/main under the same heading(s):\n' +
          diverged.map((h) => `  ${h}`).join('\n') +
          '\nYour edit is safe (uncommitted in the working tree). If you are deliberately\n' +
          'amending the note(s) in place, re-run with --amend to land your local wording.\n' +
          'If this is unexpected, another session may have amended the note on origin —\n' +
          `compare first: git diff origin/main -- ${notesRel}`
      );
      process.exit(1);
    }
  };

  let read = readOriginNotes();
  let composed = composeAgainst(read.text);
  refuseUnsafeCompose(composed, { firstRead: true });

  // 24.3.2: hub mode runs the fleet heartbeat and stamps its summary into the
  // note being landed — but only when a NEW top block is landing (newCount>0):
  // a re-land or --amend of an already-landed note is never mutated by the
  // stamp (mutating it would diverge the next wrap's compose).
  let stamp = null;
  const hbScript = fleetHeartbeatScript(repoRoot);
  if (hbScript && composed.newCount > 0) {
    console.log('hub wrap: running the fleet heartbeat (scripts/check-fleet.mjs)…');
    stamp = heartbeatStampLines(runFleetHeartbeat(repoRoot, hbScript));
    for (const l of stamp) console.log(l);
  } else if (hbScript) {
    console.log('hub wrap: no new session block is landing — heartbeat stamp skipped.');
  }

  // Story 148.1.1: origin moves during the wrap — the hub heartbeat runs for
  // minutes and its own check-ci-contract fetch moves origin/main (hub #3064
  // deleted a note that landed in that window). Re-fetch just before the write
  // (hub: after the heartbeat; site: no heartbeat, a seconds-wide window); if
  // origin moved, re-compose against it so every block it holds lands, each one
  // that arrived mid-wrap named. The heartbeat is NOT re-run: its stamp is
  // re-applied to the re-composed top block below.
  try {
    fetchOriginMain(repoRoot);
    const again = readOriginNotes();
    if (again.sha !== read.sha) {
      const recomposed = composeAgainst(again.text);
      refuseUnsafeCompose(recomposed, { firstRead: false });
      for (const s of recomposed.missingFromLocal) {
        console.log(`wrap: origin moved during the wrap — kept ${headingKey(s)}`);
      }
      read = again;
      composed = recomposed;
    }
  } catch {
    console.log(
      'wrap: could not re-fetch origin/main before the write — writing from the sha the compose\n' +
        'read; a move since then surfaces as a conflict on the wrap PR, never an overwrite.'
    );
  }
  if (composed.diverged.length && amend) {
    console.log(`Amending ${composed.diverged.length} session note(s) in place (--amend): ${composed.diverged.join(' | ')}`);
  }
  const { dropped, keptCount } = composed;
  // Story 208.1.1: the stamp goes into THIS session's block (the one the id matched), never the top by position.
  const landText = stamp && composed.newCount > 0 ? stampSessionBlock(composed.text, stamp, sessionId) : composed.text;

  // Story 236.2.1 (D4): the title is the heading of the block carrying THIS session's id among the
  // blocks that land, never the local top block by position. No such block → `landing.reason`, applied
  // in the worktree below only when the compose would still change the file (a bare trim).
  const landing = landingTitle({ sessions, composed, sessionId, amend });
  const title = landing.ok ? landing.title : null;
  // Passed verbatim to wtGit(wt, ['commit', '-m', msg]) — no shell, so no escaping needed.
  const msg = message ?? (title === null ? null : wrapCommitMessage(title));

  // A leftover local wrap branch — a same-minute re-wrap after a crash, or an
  // armed wrap from before the arm path deleted its local branch — would crash
  // the worktree's `git worktree add -b` below raw (a branch name can't be
  // reused). Refuse with the recovery instead (10.2.3 item 5). Nothing
  // destructive has happened yet: the notes edit is untouched.
  let leftoverBranch = false;
  try {
    git(['rev-parse', '--verify', '--quiet', `refs/heads/${wrapBranch}`]);
    leftoverBranch = true;
  } catch {
    /* no such branch — the normal case */
  }
  if (leftoverBranch) {
    console.error(leftoverBranchMessage(wrapBranch, branchHeldByWorktree(repoRoot, wrapBranch)));
    process.exit(1);
  }

  // Story 33.1.6 (reshaped by 33.2.1): rename this session's own presence
  // marker to its session-done/* tombstone (fleet-view --start's
  // counterpart), so the wrapped session's trailing activity stays
  // attributed for the TTL instead of rendering UNDECLARED. Contained like
  // the sweep — marker trouble never fails a wrap. The registry accessors
  // are the INLINED ones above (Story 34.1.1): the arm runs identically
  // from the hub and from every synced site copy — no hub-only module in
  // the path, so a site wrap tombstones its own marker too.
  // Story 53.1.1: keyed by the worktree-shared git dir, so a wrap run from a
  // worktree of the checkout that --started still tombstones its own marker.
  // Story 216.1.7: every outcome prints — the tombstone on stdout as before;
  // the refusal banner (spec decision 6: surfaced, never guessed — the
  // sessionId handshake resolves the common sibling case, Story 57.1.1) and a
  // reason-only result on stderr, the latter naming the registered refs and
  // the recovery command when a ref was named. The exit is UNCHANGED: a
  // handoff never fails on its marker.
  // Story 236.2.1 (D3): CALLED only past the last refusal — once the wrap
  // branch is pushed, or the notes read already current — so a refused wrap
  // (a sibling's block, a DRAFT, BEHIND, stranded, diverged, a leftover
  // branch, a bare trim, a failed write or push) leaves the marker and its
  // registry row LIVE for the re-run. Until 2026-10-24 it ran before the
  // first compose, and every refusal exited with the marker already gone.
  const retireMarker = () => {
    try {
      const gitDir = markerGitDir(repoRoot, (argv) => git(argv));
      const registry = readMarkerRegistry();
      const res = tombstoneSessionMarker({ repoRoot, gitDir, registry, gitFn: (argv) => git(argv), ref: marker, sessionId });
      const registeredRefs = registry.markers.filter((m) => markerMatchesKey(m, { repoRoot, gitDir })).map((m) => ({ ref: m.ref, sessionId: m.sessionId ?? null }));
      const lines = markerCleanupLines(res, { namedRef: marker, registeredRefs, sessionId });
      if (res.tombstoned) { removeMarkerFromRegistry(repoRoot, res.ref, { gitDir }); console.log(lines.join('\n')); }
      else console.error(lines.join('\n'));
    } catch (e) { console.log(`marker cleanup: skipped (${e.message.split('\n')[0]})`); }
  };

  // Pillar A (Initiative 11): the note is committed in an EPHEMERAL WORKTREE
  // off the sha the final compose read (Story 148.1.1 — not the live
  // origin/main ref, which a concurrent fetch can move after the read) — the
  // session's checkout, index, and working tree are never touched, so there
  // is nothing to snapshot or restore: on ANY failure below the session note
  // still sits untouched in the local docs/notes.md.
  // F1: the worktree branch is --no-track and the push has no -u, so shared
  // .git/config is never written (KB git-worktree-shared-config-contention).
  let nothingToCommit = false;
  let bareLanding = false;
  let pushErr = null;
  try {
    withWriteWorktree({ repoRoot, branch: wrapBranch, base: read.sha }, (wt) => {
      writeFileSync(path.join(wt, notesRel), landText);
      wtGit(wt, ['add', notesRel]);
      try {
        wtGit(wt, ['diff', '--cached', '--quiet']); // exit 0 = nothing staged
        nothingToCommit = true;
        return;
      } catch {
        /* staged changes exist — proceed to commit */
      }
      // Story 236.2.1 (D4): a diff with no block of this session's landing is a bare trim — never committed.
      if (!landing.ok) {
        bareLanding = true;
        return;
      }
      wtGit(wt, ['commit', '-m', msg]);
      try {
        wtGit(wt, ['push', 'origin', wrapBranch]); // no -u (F1)
      } catch (err) {
        pushErr = err; // classified below, after the worktree is cleaned up
      }
    });
  } catch (err) {
    // 11.4.4 review, I2: a twin claiming the branch AFTER the pre-check (TOCTOU)
    // surfaces here as a classified BRANCH_EXISTS. The branch is the TWIN's —
    // its commit is held-not-lost; deleting it would silently falsify the
    // twin's own recovery message. Refuse loud and leave it alone.
    if (err?.code === 'BRANCH_EXISTS') {
      console.error(
        `Refusing to wrap: branch ${wrapBranch} was claimed by a concurrent same-minute wrap\n` +
          `(a twin got there between the pre-check and the write). Your session note is NOT\n` +
          `lost — it is untouched in your local ${notesRel}. The twin's branch is left alone.\n` +
          'Wait a minute and re-run — the wrap picks a fresh branch name.'
      );
      process.exit(1);
    }
    console.error(
      `WRAP FAILED before anything was pushed — your session note is NOT lost:\n` +
        `it is untouched in your local ${notesRel}. Fix the error and re-run:\n` +
        `${(err?.stderr || err?.message || err).toString().trim()}`
    );
    try { git(['branch', '-D', wrapBranch]); } catch { /* not created — fine */ }
    process.exit(1);
  }
  if (bareLanding) {
    console.error(landing.reason);
    try { git(['branch', '-D', wrapBranch]); } catch { /* not created — fine */ }
    process.exit(1);
  }
  if (dropped > 0) console.log(`Trimmed ${dropped} old session(s); ${keptCount} kept.`);
  if (nothingToCommit) {
    console.log(`${notesRel} already current on origin/main — nothing to commit.`);
    git(['branch', '-D', wrapBranch]);
    retireMarker(); // Story 236.2.1: the notes are already current — past the last refusal
    reconcileAfterLand();
    return;
  }
  if (pushErr) {
    const errText = `${pushErr?.stderr ?? ''}\n${pushErr?.message ?? ''}`;
    if (isPushReject(errText)) {
      console.error(
        `Push REJECTED (non-fast-forward): origin already has ${wrapBranch} with different\n` +
          'content — most likely a same-minute twin wrap from another session. Your note is\n' +
          `committed locally on ${wrapBranch} — held, NOT lost (and still in your working\n` +
          `tree). Clean up and retry next minute (fresh branch name):\n` +
          `  git branch -D ${wrapBranch}`
      );
    } else {
      console.error(
        `Notes committed on ${wrapBranch}, but the push failed (offline?). Push it when online:\n` +
          `  git push origin ${wrapBranch}`
      );
    }
    process.exit(1);
  }
  console.log(`Committed and pushed ${notesRel} on ${wrapBranch}.`);
  retireMarker(); // Story 236.2.1: the branch is pushed — past the last refusal

  if (!hasGh()) {
    console.error(
      `gh CLI not found — branch ${wrapBranch} is pushed, but the PR was not opened.\n` +
        'Open and merge it manually (or install gh) to update main.'
    );
    process.exit(1);
  }
  // Remember the PR we just created: if every later look at it fails (a gh
  // blip), landOnMain can still banner with the real number instead of the
  // misleading "no open PR was found" (10.2.3 LOW). UNKNOWN merge fields keep
  // the gate deferring, never merging on a blip.
  // Title/body are passed EXPLICITLY (field regression, first live sweep run,
  // 2026-07-14): `--fill` computes them from `git log origin/main...<branch>`,
  // which needs the LOCAL branch ref — and the 13.1.1 lib behavior deletes it
  // the moment the push provably lands, so --fill died with "unknown revision"
  // AFTER the push (loud banner, notes safe, but every wrap needed a manual
  // PR). The wrap already holds the exact content --fill would derive: msg's
  // subject line and body.
  const msgNl = msg.indexOf('\n\n');
  const prTitle = msgNl === -1 ? msg : msg.slice(0, msgNl);
  const prBody = msgNl === -1 ? title : msg.slice(msgNl + 2);
  let knownPr = null;
  try {
    const prUrl = forge.prCreate({ head: wrapBranch, title: prTitle, body: prBody });
    console.log(`Opened ${prUrl}`);
    const num = prUrl.match(/\/pull\/(\d+)/)?.[1];
    if (num) knownPr = { number: Number(num), state: 'OPEN', mergeable: 'UNKNOWN', mergeStateStatus: 'UNKNOWN' };
  } catch (err) {
    console.error(
      `Could not open a PR for ${wrapBranch}: ${err.message}\n` +
        'The notes are pushed — open and merge the PR manually to update main.'
    );
    process.exit(1);
  }

  // A docs-only branch is always safe to land, so we always do — merge if green, else arm
  // GitHub auto-merge (unchanged landOnMain logic). No --merge gate anymore.
  // The session's checkout is untouched throughout — nothing to restore.
  // 11.4.7: a HANDOFF INCOMPLETE banner means main does NOT have the notes —
  // exit non-zero so calling automation sees the failure (armed-and-waiting
  // returns true and stays a success).
  if (!landOnMain(wrapBranch, knownPr)) process.exitCode = 1;

  // F6: a hub clone with pending escalations reminds every wrap that lands or
  // arms a note (the nothing-to-commit early return above skips it;
  // check-portfolio's hub lint is the backstop that keeps escalations
  // never-unseen). Harmless in project repos — they have no docs/inbox.
  const inboxDir = path.join(repoRoot, 'docs', 'inbox');
  if (existsSync(inboxDir)) {
    const pending = readdirSync(inboxDir).filter((f) => f.endsWith('.md'));
    if (pending.length) {
      console.log(`note: docs/inbox has ${pending.length} pending escalation(s) — a hub session should triage them into the plan.`);
    }
  }
}

function recall() {
  const { sessions } = loadNotes();
  if (sessions.length === 0) {
    console.log(`No sessions recorded yet in ${notesRel}.`);
    return;
  }
  console.log(sessions[0].trim());
}

// Only dispatch when run directly. The importer is the HUB's test suite
// (test/wrap-session.test.mjs — the hub owns this canonical); site repos have
// no wrap-session test, and their copies carry this guard verbatim because
// the sync is byte-identical (scripts/sync-session-wrap.mjs). (Story 1.9.5)
// Hardened isMain (Story 26.1.4): realpath the argv side — a silently
// no-opping wrap would look exactly like a wrap that ran and had nothing to
// say (KB macos-tmpdir-symlink-spawn-tests).
const argvRealMain = (() => { try { return process.argv[1] && realpathSync(process.argv[1]); } catch { return null; } })();
if (argvRealMain === fileURLToPath(import.meta.url)) {
  const [, , command, ...rest] = process.argv;
  const usage = 'Usage: node scripts/wrap-session.mjs <wrap|recall|reconcile> [-m msg] [--merge] [--amend] [--dry-run] [--marker <ref>] [--carry-siblings] [--session-id <uuid>] [--tombstone-only]';
  // Story 244.1.5: a flag the subcommand does not take is refused before any repo
  // access — an ignored flag made `reconcile --dry-run` reconcile (the Hub Sprint 67
  // review's measurement). An unknown subcommand falls through to the exit 1 below.
  const badFlags = unknownFlags(command, rest);
  if (badFlags?.length) {
    const { bool, value } = SUBCOMMAND_FLAGS[command];
    const takes = [...value, ...bool];
    console.error(
      `wrap-session: ${command} does not take ${badFlags.join(' ')} — ${command} takes ${takes.length ? takes.join(' ') : 'no flag'}.`
    );
    console.error(usage);
    process.exit(2);
  }
  const dryRun = rest.includes('--dry-run');
  const merge = rest.includes('--merge');
  const amend = rest.includes('--amend');
  const mIdx = rest.indexOf('-m');
  const message = mIdx !== -1 ? rest[mIdx + 1] : undefined;
  // Story 42.1.2: --marker <ref> names this session's own marker when
  // same-checkout siblings are registered (the ambiguous wrap refuses).
  const markerIdx = rest.indexOf('--marker');
  const marker = markerIdx !== -1 ? rest[markerIdx + 1] : null;
  if (markerIdx !== -1 && (marker === null || marker === undefined || marker.startsWith('-'))) {
    console.error('--marker requires a session/<...> ref argument.');
    process.exit(1);
  }
  // Story 208.1.1: --carry-siblings lands the new blocks below this session's own top block;
  // --session-id <uuid> names this session when CLAUDE_CODE_SESSION_ID is not in the shell.
  const carrySiblings = rest.includes('--carry-siblings');
  // Story 212.1.1: --tombstone-only runs the marker arm alone (see wrap()).
  const tombstoneOnly = rest.includes('--tombstone-only');
  const sidIdx = rest.indexOf('--session-id');
  const sessionIdArg = sidIdx !== -1 ? rest[sidIdx + 1] : null;
  if (sidIdx !== -1 && (sessionIdArg === undefined || sessionIdArg.startsWith('-'))) {
    console.error('--session-id requires a uuid argument (this session\'s harness id).');
    process.exit(1);
  }
  // A missing or flag-like -m value used to be silently ignored (the wrap fell
  // back to the derived message) — refuse loudly instead (10.2.3 LOW).
  if (mIdx !== -1 && (message === undefined || message.startsWith('-'))) {
    console.error(
      message === undefined
        ? '-m requires a message argument (none given).'
        : `-m's value looks like a flag (${message}) — quote a real message or drop -m.`
    );
    process.exit(1);
  }

  if (command === 'wrap') wrap({ message, dryRun, merge, amend, marker, carrySiblings, sessionIdArg, tombstoneOnly });
  else if (command === 'recall') recall();
  else if (command === 'reconcile') reconcile({ dryRun });
  else {
    console.error(usage);
    process.exit(1);
  }
}
