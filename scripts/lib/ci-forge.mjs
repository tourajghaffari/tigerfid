// scripts/lib/ci-forge.mjs — the CI provider shim for the docs-only fast path
// (hub Story 43.1.1, on the scripts/lib/forge.mjs pattern): the ONE module that
// owns ci-docs-only.mjs's provider IO — event vocabulary, event payload, diff
// range, and verdict publication — so the canonical's core is forge-neutral
// and a forge move rewrites only this file. forge.mjs owns `gh` invocations;
// this file owns the CI runner's env/file surfaces. GitHub today; the GitLab
// mapping is sketched per op.
//
// CANONICAL: rides the CANONICALS set (sync-session-wrap.mjs) because the
// fleet-synced ci-docs-only.mjs imports it — every repo under projects/
// receives the pair at the same relative paths. Node builtins only; no
// repo-local imports.

import { execFileSync } from 'node:child_process';
import { appendFileSync, readFileSync } from 'node:fs';

// Non-GitHub CI fingerprints. GitHub Actions also sets CI=true, so the GitHub
// probe runs first; a signal here without a GitHub var means SOME runner is
// executing us that this shim cannot speak to — the caller must fail loudly
// (the Initiative 43 defect was exactly this shape sitting silently inert).
const FOREIGN_CI_SIGNALS = ['CI', 'GITLAB_CI', 'BUILDKITE', 'CIRCLECI', 'JENKINS_URL', 'TF_BUILD', 'TEAMCITY_VERSION'];

// 'github' | 'unknown-ci' | 'local'.
//   GitLab: a GITLAB_CI/CI_PIPELINE_SOURCE probe would slot in before the
//   foreign-signal sweep.
export function detectProvider(env) {
  if (env.GITHUB_ACTIONS === 'true' || env.GITHUB_EVENT_NAME) return 'github';
  if (FOREIGN_CI_SIGNALS.some((k) => env[k])) return 'unknown-ci';
  return 'local';
}

// The provider's event name, or null outside a provider event.
//   GitLab: CI_PIPELINE_SOURCE (vocabulary differs: merge_request_event/push).
export const eventName = (env) => env.GITHUB_EVENT_NAME || null;

// The provider's event payload, parsed; null when absent or unreadable —
// resolveRange treats a null payload as an undiffable event (fail-closed).
//   GitLab: no payload file — CI_MERGE_REQUEST_DIFF_BASE_SHA et al. are env.
export function readEventPayload(env, readFileSyncFn = readFileSync) {
  if (!env.GITHUB_EVENT_PATH) return null;
  try { return JSON.parse(readFileSyncFn(env.GITHUB_EVENT_PATH, 'utf8')); } catch { return null; }
}

// The shim's one git read (hub Story 98.1.1): a ref's commit sha, or null when
// it does not resolve. A sha is a listing, not content — no maxBuffer owed
// (the Story 97.1.4 census's not-content class: rev-parse). Injectable through
// resolveRange's `io` so the payload-only cases stay pure.
export function gitRevParse(ref, cwd = process.cwd()) {
  try {
    const out = execFileSync('git', ['rev-parse', '--verify', '--quiet', ref], { cwd, encoding: 'utf8', stdio: ['ignore', 'pipe', 'ignore'] }).trim();
    return out || null;
  } catch { return null; }
}

// Is HEAD this PR's merge ref? Under actions/checkout on a `pull_request`
// event HEAD is refs/pull/N/merge — a merge commit whose FIRST parent is the
// base branch's tip at build time and whose SECOND parent is the PR head. The
// second-parent identity is the test: a merge commit that is not this PR's
// (a base checkout under pull_request_target whose tip happens to be a merge,
// a workflow checking out head.sha) keeps the payload range below.
function headIsMergeRefOf(payload, io) {
  const prHead = payload?.pull_request?.head?.sha;
  if (!prHead) return false;
  const second = io.revParse('HEAD^2');
  return Boolean(second) && second === prHead;
}

// Which two commits to diff, from the provider event. Returns {base, head} or
// {error} — pure over an env + parsed event payload except for the one
// injectable git read above. (Moved verbatim from ci-docs-only.mjs at the
// Story 43.1.1 extraction; the merge-ref branch added by hub Story 98.1.1.)
//
// pull_request (Story 98.1.1, fix candidate (a) of hub Initiative 98): the
// payload's base.sha is the base tip FROZEN at PR creation, while HEAD is the
// merge ref carrying CURRENT main — so `base.sha...HEAD` charged a docs-only
// PR for every non-doc commit main gained after the branch point (tigerian
// #583 read FULL SUITE over three sweep files its branch never touched). When
// HEAD is this PR's merge ref, diff `HEAD^1...HEAD`: HEAD^1 is the base tip
// at merge-ref build time, so the three-dot diff is exactly the PR's own
// commits; no extra fetch (every fleet ci.yml checks out fetch-depth: 0), no
// ref name. Otherwise the payload sha as before. pull_request_target checks
// out the BASE, never a merge ref — it keeps the payload sha (no fleet
// workflow uses the event; recorded, not exercised).
//   GitLab: merge_request_event ↔ CI_MERGE_REQUEST_DIFF_BASE_SHA, push ↔
//   CI_COMMIT_BEFORE_SHA (same all-zero new-branch caveat).
export function resolveRange(env, payload, io = { revParse: gitRevParse }) {
  const event = env.GITHUB_EVENT_NAME;
  if (event === 'pull_request' || event === 'pull_request_target') {
    const base = payload?.pull_request?.base?.sha;
    if (!base) return { error: 'pull_request payload carried no base.sha' };
    if (event === 'pull_request' && headIsMergeRefOf(payload, io)) return { base: 'HEAD^1', head: 'HEAD' };
    return { base, head: 'HEAD' };
  }
  if (event === 'push') {
    const before = payload?.before;
    if (!before || /^0+$/.test(before)) return { error: 'push payload had no usable before sha (new branch or force-push)' };
    return { base: before, head: payload?.after || 'HEAD' };
  }
  return { error: `event '${event ?? '(none)'}' is not a diffable change set` };
}

// Publish the verdict on the provider's output channel. Returns whether a
// channel existed — the caller decides what an unpublished verdict means for
// its provider. Both outputs, always, as explicit booleans: a wired workflow
// reading a missing output would compare against the empty string.
//   GitLab: dotenv artifact (a written report file) instead of GITHUB_OUTPUT.
export function publishVerdict(env, verdict, appendFileSyncFn = appendFileSync) {
  if (!env.GITHUB_OUTPUT) return false;
  appendFileSyncFn(env.GITHUB_OUTPUT, `docs_only=${verdict.docsOnly}\nlint_only=${verdict.lintOnly === true}\n`);
  return true;
}
