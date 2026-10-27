// scripts/lib/hub-write-worktree.mjs — ephemeral write-worktrees for hub-writer
// scripts (Initiative 11 Pillar A; spec docs/superpowers/specs/
// 2026-07-10-same-machine-hub-write-isolation-design.md).
//
// withWriteWorktree gives a writer its own private working tree + index on a
// fresh branch off `base`, so the write NEVER touches the calling session's
// checkout — two same-machine sessions can write concurrently with no
// filesystem-layer race (the pilot's T2/T4 class).
//
// F1 (empirical, PR #289 review): worktrees share ONE mutable file —
// .git/config — and two DEFAULT git behaviors write it: tracking auto-setup
// when branching off a remote-tracking ref, and `push -u`. Concurrent writers
// then contend on config.lock (6/8 failed loud in an 8-way burst; 12/12 clean
// with the fix). NORMATIVE: this module branches --no-track, and callers push
// WITHOUT -u. See KB git-worktree-shared-config-contention.
//
// Synced fleet-wide as a canonical alongside wrap-session.mjs (which imports
// it); also used by the hub-resident kb.mjs, hub-doc.mjs, and inbox.mjs.
//
// Story 199.1.1 (2026-10-12): fetchOriginMain retries ONCE on two transient
// signatures — a ref lock (immediately) and `did not send all necessary
// objects` (after a short back-off) — through an injectable { run, sleep }
// seam; see the function.

import { execFileSync } from 'node:child_process';
import { mkdtempSync, rmSync } from 'node:fs';
import { tmpdir } from 'node:os';
import path from 'node:path';

// argv exec, no shell (the 8.1.1 injection standard), pinned to `dir`.
export const wtGit = (dir, args, opts = {}) =>
  execFileSync('git', ['-C', dir, ...args], { encoding: 'utf8', ...opts }).trim();

// Story 11.1.6: classify a git-fetch failure as transient ref-lock contention
// (two same-machine writers fetching a just-moved origin/main at the same
// instant — the other fetch already updated the ref, so an immediate retry
// succeeds). Real samples: "cannot lock ref 'refs/remotes/origin/main'",
// "error: Unable to create '<path>/refs/remotes/origin/main.lock': File
// exists", "error: unable to update local ref".
export function isTransientRefLock(errText) {
  return /cannot lock ref|unable to update local ref|Unable to create '[^']*\.lock'/i.test(errText ?? '');
}

// Story 199.1.1: classify a git-fetch failure as a transient object-transfer
// loss — the fetch read a ref the other side was still pushing objects for
// (three hub `main` push runs failed the 11.4.6 overlap case on this text,
// 2026-10-05 and 2026-10-11 twice; register class
// hub-e2e-plan-tick-inbox-drop-overlap-flake). Real sample: "error:
// /tmp/hubdoc-…/origin.git did not send all necessary objects". The push
// completes within a moment, so one retry after a short back-off succeeds.
// Case-sensitive: the git wording, verbatim.
export function isTransientObjectTransfer(errText) {
  return /did not send all necessary objects/.test(errText ?? '');
}

// The back-off before the object-transfer retry: synchronous (the function
// stays synchronous for its eight call sites in four files), no timer, no
// promise — Atomics.wait on a one-element Int32Array over a SharedArrayBuffer
// returns `timed-out` after the wait (block D5; the executor's call: 250 ms).
export const FETCH_RETRY_BACKOFF_MS = 250;
const backOff = (ms) => { Atomics.wait(new Int32Array(new SharedArrayBuffer(4)), 0, 0, ms); };

// Story 11.4.1 finding 2: classify a `git worktree add` failure as a transient
// worktree-admin-dir race — a concurrent same-machine writer's `worktree prune`
// or `worktree remove` mutating `.git/worktrees/` while THIS add reads a
// sibling's admin file. Real sample (macOS, N=12 stress): "fatal: failed to read
// .git/worktrees/wt3/commondir: Undefined error: 0". Retryable (a re-run of the
// add succeeds once the sibling's transient state settles); a branch-name
// collision ("reference already exists") is deterministic, NOT this.
// Story 242.1.2 (2026-10-26): a second face of the same race on the shared Linux
// runner (hub #4102's test, run 39959199535 — register class
// concurrent-worktree-add-admin-dir-race-outside-the-transient-classifier):
// "fatal: not a git repository: /tmp/…/repo/.git/worktrees/wt", anchored on the
// .git/worktrees/NAME path (the bare text and git's outside-a-repo line stay NOT
// transient); it fires the one retry, and classifyAddError labels a second
// failure on it WORKTREE_RACE. Corrected by Story 244.1.13 (the Hub Sprint 67
// review's finding 2, hub #4360, measured before its battery's GO at 19:55:28Z
// 2026-10-26): Story 242.1.2 recorded the run's two lines as ONE stderr thrown at
// addWorktree; they were read off the test's console. The second line, "fatal:
// '/tmp/hub-write-…/wt' is not a working tree", is the `finally`'s cleanup echo —
// `worktree remove --force` on the dir the failed add never created, its stderr
// inherited by the console — git's permanent wrong-path error, never an add text.
// It is not transient, and the cleanup now runs with stdio 'pipe' (below).
export function isTransientWorktreeError(errText) {
  const t = errText ?? '';
  return (
    /worktrees\/[^/\s]+\/(commondir|gitdir)/i.test(t) ||
    /not a git repository: \S*\.git[\\/]worktrees[\\/][^\\/\s]+/i.test(t)
  );
}

// Fetch origin/main with a single retry on a transient failure: ref-lock
// contention (spec Pillar A: "loud + self-healing — retry once"; Story 11.1.6,
// retried immediately) or, since Story 199.1.1 (2026-10-12), an object-transfer
// loss (`did not send all necessary objects`, retried after a short back-off —
// the inbox drop's fetch against an origin whose push is still in flight).
// The retry is ONE for either signature. Anything else — offline, auth, a
// SECOND failure — propagates to the caller's friendly failure. `run` and
// `sleep` are the seam the planted cases inject (block D5 supersedes the
// 13.1.5 review's "a git seam is too invasive" judgment, dated here); the
// defaults are the real git and the real back-off.
// Note (11.4.7): concurrent fetches also write the shared .git/FETCH_HEAD
// unserialized — benign ONLY because nothing programmatic reads FETCH_HEAD
// (human recovery recipes aside). Do not add a FETCH_HEAD consumer.
export function fetchOriginMain(repoRoot, { run = (args) => wtGit(repoRoot, args), sleep = backOff } = {}) {
  const FETCH = ['fetch', 'origin', 'main'];
  try {
    run(FETCH);
  } catch (e) {
    const text = `${e?.stderr ?? ''}\n${e?.message ?? ''}`;
    if (isTransientObjectTransfer(text)) sleep(FETCH_RETRY_BACKOFF_MS);
    else if (!isTransientRefLock(text)) throw e;
    run(FETCH); // retry once; a second failure propagates
  }
}

// Push-reject (same-slug twin) signatures vs offline/auth noise — the regex
// wrap-session used inline, shared here (11.4.4 review): kb/inbox must not
// diagnose EVERY push failure as a concurrent twin, or the operator is sent to
// the wrong recovery. `stale info` is --force-with-lease's reject wording.
export function isPushReject(errText) {
  return /\[rejected\]|non-fast-forward|fetch first|failed to push some refs|stale info/i.test(errText ?? '');
}

// The worktree (if any) holding `branch` checked out — live or crashed. After
// a SIGKILL mid-write the dead worktree's registration still holds the branch,
// so the `git branch -D` every stranded message prints refuses ("used by
// worktree at …"); callers use this to print the worktree-aware recovery
// instead (11.4.4 review). The MAIN checkout (always listed first) is skipped:
// it is not removable, and the plain-checkout advice already covers it.
export function branchHeldByWorktree(repoRoot, branch) {
  let out;
  try {
    out = wtGit(repoRoot, ['worktree', 'list', '--porcelain']);
  } catch {
    return null; // listing failed — callers fall back to the generic advice
  }
  for (const block of out.split(/\n\s*\n/).slice(1)) {
    const wtPath = /^worktree (.+)$/m.exec(block)?.[1];
    const held = /^branch refs\/heads\/(.+)$/m.exec(block)?.[1];
    if (wtPath && held === branch) return wtPath;
  }
  return null;
}

// A branch-name collision at `worktree add -b` — a truly concurrent same-slug
// twin (both writers past the caller's localBranchExists pre-check, a TOCTOU) or
// a leftover branch. Deterministic, NOT a retryable transient.
function isBranchExists(errText) {
  return /already exists|already used by worktree|already checked out/i.test(errText ?? '');
}

// Map a `worktree add` failure to what the caller should see: a same-slug branch
// collision becomes a classified BRANCH_EXISTS (finding 1 — the caller prints its
// fails-closed message instead of a raw stack); anything else propagates as-is.
function classifyAddError(errText, branch, raw) {
  if (isBranchExists(errText)) {
    const err = new Error(`worktree branch ${branch} already exists (claimed by a concurrent same-machine write, or a leftover)`);
    err.code = 'BRANCH_EXISTS';
    return err;
  }
  // 11.4.7: a SECOND transient admin failure (retry exhausted) used to
  // propagate raw — loud and pre-commit, but it read as a tool crash where
  // every sibling failure prints fails-closed advice. Classify it.
  if (isTransientWorktreeError(errText)) {
    const err = new Error(
      'transient worktree-admin contention persisted after a retry (a concurrent same-machine\n' +
        'writer kept racing this write) — nothing was written or committed. Re-run.\n' +
        `(git: ${String(errText ?? '').trim().split('\n')[0]})`
    );
    err.code = 'WORKTREE_RACE';
    return err;
  }
  return raw;
}

// `worktree add`, hardened (Story 11.4.1 live-run findings). Retries ONCE on a
// transient worktree-admin race (finding 2 — a concurrent writer's prune/remove
// racing this add over the shared .git/worktrees/ dir; same self-healing contract
// as fetchOriginMain). A mid-flight failure can leave a partial branch, dir, or
// registration behind (git creates the branch before the admin write that raced),
// so the retry clears all three first — otherwise it self-collides on "a branch
// named … already exists". A branch collision on the FIRST attempt is a concurrent
// twin → classified BRANCH_EXISTS, not retried.
function addWorktree(repoRoot, branch, dir, base) {
  // --no-track is load-bearing (F1): without it, branching off a remote-tracking
  // ref writes the SHARED .git/config and concurrent writers contend on config.lock.
  const args = ['worktree', 'add', '--no-track', '-b', branch, dir, base];
  try {
    wtGit(repoRoot, args);
    return;
  } catch (e) {
    const txt = `${e?.stderr ?? ''}\n${e?.message ?? ''}`;
    if (!isTransientWorktreeError(txt)) throw classifyAddError(txt, branch, e);
    // Clear the failed attempt's partial dir → registration → branch, then retry.
    try { rmSync(dir, { recursive: true, force: true }); } catch { /* best effort */ }
    try { wtGit(repoRoot, ['worktree', 'prune']); } catch { /* best effort */ }
    // 11.4.4 review, I1: only delete the branch if it still points AT base — a
    // partial add's branch always does. Under a same-slug race the name can
    // belong to ANOTHER writer's held-not-lost commit (tip ≠ base); leave that
    // alone and let the retry surface it as BRANCH_EXISTS instead.
    try {
      const branchTip = wtGit(repoRoot, ['rev-parse', '--verify', '--quiet', `refs/heads/${branch}`]);
      const baseTip = wtGit(repoRoot, ['rev-parse', '--verify', '--quiet', `${base}^{commit}`]);
      if (branchTip === baseTip) wtGit(repoRoot, ['branch', '-D', branch]);
    } catch { /* branch absent — nothing to delete */ }
  }
  try {
    wtGit(repoRoot, args); // retry once; a second failure propagates
  } catch (e) {
    throw classifyAddError(`${e?.stderr ?? ''}\n${e?.message ?? ''}`, branch, e);
  }
}

// Run fn inside an ephemeral worktree on the NEW branch `branch` cut from
// `base` (default origin/main — callers fetch first, with their own error UX).
// The worktree lives under the OS tmpdir, never inside the repo. Cleanup is
// guaranteed even when fn throws. Branch lifetime (Story 13.1.1): once the
// push PROVABLY landed — refs/heads/<branch> equals refs/remotes/origin/<branch>,
// which a successful `git push origin <branch>` updates even without -u — the
// local ref is deleted, so a clean capture leaves the local ref-space
// untouched. On ANY doubt (fn threw, push failed or was swallowed by the
// caller, tips differ, the branch predates this run) the branch is KEPT: it
// holds the commit (held-not-lost recovery), and the caller owns its lifetime.
export function withWriteWorktree({ repoRoot, branch, base = 'origin/main' }, fn) {
  // Crashed-run leftovers (a reboot can empty tmpdir without `worktree remove`)
  // would otherwise accumulate stale registrations; prune is cheap.
  wtGit(repoRoot, ['worktree', 'prune']);
  const parent = mkdtempSync(path.join(tmpdir(), 'hub-write-'));
  const dir = path.join(parent, 'wt'); // non-existent path: git creates it
  // Deletion below is gated on THIS run having created the branch: under a
  // BRANCH_EXISTS collision the ref belongs to a concurrent twin whose
  // recovery message names it — never touch another writer's ref.
  let created = false;
  try {
    addWorktree(repoRoot, branch, dir, base);
    created = true;
    return fn(dir);
  } finally {
    // Windows can hold the dir briefly; the unconditional prune below then sweeps
    // the stale metadata so it never blocks the next writer. Story 244.1.13: stdio
    // 'pipe' — after a failed add there is no dir, and the remove's "is not a working
    // tree" printed beside the add's own error read as an add face (Story 242.1.2's
    // record); its error is discarded here, so quieting it hides nothing actionable.
    try { wtGit(repoRoot, ['worktree', 'remove', '--force', dir], { stdio: 'pipe' }); } catch { /* prune sweeps it */ }
    // 11.4.7 (review M1): delete the dir BEFORE pruning — when `worktree remove`
    // failed, prune can only sweep a registration whose dir is gone, so this
    // order makes the cleanup self-contained instead of leaning on the next
    // writer's entry prune.
    try { rmSync(parent, { recursive: true, force: true }); } catch { /* best effort */ }
    // Always prune (Story 11.4.1): under high concurrency a racing remove, or a
    // retried add (addWorktree), can leave a stale registration whose dir is gone.
    // Sweeping here returns `worktree list` to 1 promptly. Any concurrent live
    // worktree has its dir, so prune never touches it (pinned by the 11.4.6
    // barrier e2e); a prune-induced add race self-heals via addWorktree.
    try { wtGit(repoRoot, ['worktree', 'prune']); } catch { /* best effort */ }
    // Story 13.1.1: drop the local ref ONCE THE PUSH PROVABLY LANDED — tip
    // equality with the remote-tracking ref is the proof (a failed or skipped
    // push leaves it absent or stale, so every doubt path lands in the catch
    // or fails the equality and KEEPS the branch). Runs after `worktree
    // remove` — git refuses to delete a branch a live worktree still holds.
    if (created) {
      try {
        const localTip = wtGit(repoRoot, ['rev-parse', '--verify', '--quiet', `refs/heads/${branch}`]);
        const remoteTip = wtGit(repoRoot, ['rev-parse', '--verify', '--quiet', `refs/remotes/origin/${branch}`]);
        if (localTip && localTip === remoteTip) wtGit(repoRoot, ['branch', '-D', branch]);
      } catch { /* no proof — held-not-lost; the caller owns recovery */ }
    }
  }
}

// --- F3 safe-dirty predicate (Story 12.1.2 relocation; born in 12.1.1) ---
// Lives HERE, not in hub-status.mjs, because the wrap (a synced canonical)
// consumes it and may only import from its synced companion — this lib.
// hub-status re-exports from here. The IO recipe travels with the predicate:
// feed parseAddedLines the output of
//   git diff --no-ext-diff --no-color HEAD -- <path>
// (external/colorized diff output is not unified format — it would parse as
// zero additions and F3 would report false-'safe').

// Added lines of a `git diff` — the uncommitted delta. Blank additions are
// noise for containment; trailing \r tolerates CRLF checkouts.
export function parseAddedLines(diffText) {
  const out = [];
  for (const line of (diffText ?? '').split('\n')) {
    if (!line.startsWith('+') || /^\+\+\+ (b\/|\/dev\/null)/.test(line)) continue;
    const content = line.slice(1).replace(/\r$/, '');
    if (content.trim() === '') continue;
    out.push(content);
  }
  return out;
}

// The lines of a notes text as a Set — the F3 normalization (trailing \r
// tolerates CRLF checkouts), shared by the tip proof and the history probe.
function lineSet(text) {
  return new Set(text.split('\n').map((l) => l.replace(/\r$/, '')));
}

// F3: would discarding the local edit lose anything not on origin?
// Safe iff every uncommitted ADDED line already appears in origin's copy.
// Line-level on purpose — stricter than block-level, fails toward caution.
// NOT file⊆file: origin legitimately trims old blocks (live counterexample
// 2026-07-11).
export function safeDirtyVerdict(addedLines, originText) {
  if (originText == null) return 'unsafe';
  const originLines = lineSet(originText);
  for (const l of addedLines ?? []) {
    if (!originLines.has(l)) return 'unsafe';
  }
  return 'safe';
}

// Story 83.1.1 — the trim face (KB post-wrap-notes-dirty-not-lost): a landed
// note lives in origin's HISTORY (the wrap's session-notes commit) while the
// tip has been trimmed to KEEP blocks by later wraps, so a machine whose last
// wrap is three or more wraps old carried a leftover the tip proof could not
// see. The probe is bounded by construction: the landing commit postdates the
// clone's HEAD, so `rev-list HEAD..origin/main -- <notes>` is the whole
// search (a clone whose HEAD already includes the landing shows no added
// lines for the block) — never a full-history walk, and never entered when
// the tip already proves every line. Each revision's file is read RAW.
// Line-level, like the tip proof: `missing` (the added lines the tip lacks)
// shrinks as revisions are read newest-first; proven once it is empty. The
// safety property survives: a line on NO origin revision (a pause DRAFT, an
// amendment after landing) is a line a discard would lose → still unsafe.
function provedByHistory(raw, notesRel, missing) {
  let revs;
  try {
    revs = raw(['rev-list', 'HEAD..origin/main', '--', notesRel]).split('\n').filter(Boolean);
  } catch {
    return false; // no readable ancestry → the tip verdict stands
  }
  const left = new Set(missing);
  for (const rev of revs) {
    let text;
    try {
      text = raw(['show', `${rev}:${notesRel}`]);
    } catch {
      continue; // the file absent at that revision — nothing to prove against
    }
    const lines = lineSet(text);
    for (const l of [...left]) if (lines.has(l)) left.delete(l);
    if (left.size === 0) return true;
  }
  return false;
}

// The F3 predicate's full IO recipe (12.1.2's two probes, extracted so every
// reconcile surface — the wrap's green path, `wrap-session.mjs reconcile`, the
// hub's SessionStart freshness hook, the canonical round's --finish — proves
// the SAME scope it restores): TWO diffs, verdict on the UNION. `git diff HEAD`
// sees only the HEAD→worktree delta — with a diverged index (stage A, then
// edit to B) the index-only lines are invisible to it, yet `checkout HEAD --`
// destroys them. `--cached` sees HEAD→index. Together they cover everything a
// discard can destroy. Content is read RAW — execFileSync directly, never the
// .trim()ing wtGit (KB exec-helper-trim-corrupts-content-reads). Callers fetch
// origin/main first (with their own retry/offline UX); an origin copy that
// can't be read yields 'unsafe' by construction. The tip proof runs first;
// only lines it lacks go to the bounded history probe (Story 83.1.1).
// `execOpts` reaches every git call (a caller iterating many clones passes its
// timeout); cwd and encoding are fixed here.
export function notesReconcileVerdict(repoRoot, notesRel, execOpts = {}) {
  // maxBuffer (hub Story 97.1.4): the diffs and the origin show below return
  // CONTENT; the child-process default 1 MiB dies ENOBUFS on a large
  // notes.md and would read as "absent on origin" → unsafe. 64 MiB, the
  // family's cap; a caller's execOpts may still override it.
  const raw = (args) => execFileSync('git', args, { maxBuffer: 64 * 1024 * 1024, ...execOpts, cwd: repoRoot, encoding: 'utf8' });
  const diffArgs = ['diff', '--no-ext-diff', '--no-color'];
  const worktreeDiff = raw([...diffArgs, 'HEAD', '--', notesRel]);
  const indexDiff = raw([...diffArgs, '--cached', 'HEAD', '--', notesRel]);
  const added = [...parseAddedLines(worktreeDiff), ...parseAddedLines(indexDiff)];
  let originNotes = null;
  try {
    originNotes = raw(['show', `refs/remotes/origin/main:${notesRel}`]);
  } catch {
    /* absent on origin → null → unsafe by construction */
  }
  let verdict = safeDirtyVerdict(added, originNotes);
  if (verdict === 'unsafe' && originNotes != null) {
    const tipLines = lineSet(originNotes);
    const missing = added.filter((l) => !tipLines.has(l));
    if (provedByHistory(raw, notesRel, missing)) verdict = 'safe';
  }
  return {
    dirty: worktreeDiff !== '' || indexDiff !== '',
    added,
    verdict,
  };
}
