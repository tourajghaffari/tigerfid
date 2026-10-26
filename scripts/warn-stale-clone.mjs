// scripts/warn-stale-clone.mjs — PreToolUse WARN guard (never blocks).
// Warns when an Edit/Write touches a coordination surface (project-plan,
// notes, docs/close/*) in a repo whose CACHED refs say local main is behind
// origin/main, or whose last fetch is stale. No network in the hook.
// Canonical hub copy — fleet-synced (do NOT hand-edit synced copies).
// Manual test: node scripts/warn-stale-clone.mjs < payload.json
import { existsSync, statSync } from 'node:fs';
import { execFileSync } from 'node:child_process';
import path from 'node:path';
// Inlined from scripts/fleet-view/constants.mjs — hooks are fleet-synced
// SINGLE-FILE canonicals (site repos have no scripts/fleet-view/); keep the
// two values in agreement when retuning.
const FETCH_FRESH_MINUTES = 60;

if (process.stdin.isTTY) { console.error('warn-stale-clone.mjs expects a PreToolUse JSON payload on stdin.'); process.exit(1); }
let payload; try { payload = JSON.parse(await readStdin()); } catch { payload = null; }
if (typeof payload !== 'object' || payload === null || Array.isArray(payload)) process.exit(0);
if (payload.tool_name !== 'Edit' && payload.tool_name !== 'Write') process.exit(0);

const msysDrive = (p) => { if (process.platform !== 'win32' || !p) return p; const m = /^\/([A-Za-z])(\/.*)?$/.exec(p); return m ? `${m[1].toUpperCase()}:${m[2] ?? '/'}` : p; };
const file = msysDrive(payload.tool_input?.file_path ?? '');
if (!file) process.exit(0);
const rel = file.replace(/\\/g, '/');
if (!/docs\/(project-plan\.md|notes\.md|close\/)/.test(rel)) process.exit(0);

let repo = path.dirname(path.resolve(file));
while (repo && !existsSync(path.join(repo, '.git'))) { const up = path.dirname(repo); if (up === repo) process.exit(0); repo = up; }

const git = (argv) => execFileSync('git', argv, { cwd: repo, encoding: 'utf8', timeout: 5_000 }).trim();
// Story 37.1.3 (the worktree face): judge the CURRENT checkout — HEAD against
// origin/main — never the repo's shared local `main`: in a `.claude/worktrees/`
// worktree (or any feature-branch checkout) `main` is the parent checkout's
// ref, which the peer-session housekeeping rule deliberately leaves behind, so
// the old `main..origin/main` count warned on a branch whose base EQUALED
// origin/main (expeal.com Sprint 8, twice). On a `main` checkout HEAD == main,
// so the pre-37.1.3 shape is unchanged. Story 207.1.1: git keeps FETCH_HEAD
// PER WORKTREE — `git rev-parse --git-path FETCH_HEAD` names .git/FETCH_HEAD on
// the main checkout and .git/worktrees/<id>/FETCH_HEAD in a linked one, and a
// fetch run in a worktree writes only that file (MEASURED 2026-10-14) — so
// freshness reads the CURRENT checkout's own copy and falls back to the common
// dir's (the main checkout's last fetch) by name when this worktree never fetched.
let behind = 0; let fetchAgeMin = null; let head = 'HEAD';
try { behind = Number(git(['rev-list', '--count', 'HEAD..origin/main'])) || 0; } catch { process.exit(0); }
try { head = git(['rev-parse', '--abbrev-ref', 'HEAD']) || 'HEAD'; } catch { /* detached or unreadable — keep 'HEAD' */ }
let fetchFrom = 'own'; // 'own' | 'common' — which FETCH_HEAD the age below was read from
const ageOf = (p) => (Date.now() - statSync(p).mtimeMs) / 60_000;
const absGitPath = (p) => (path.isAbsolute(p) ? p : path.join(repo, p));
try {
  const own = absGitPath(git(['rev-parse', '--git-path', 'FETCH_HEAD']));
  if (existsSync(own)) fetchAgeMin = ageOf(own);
  else {
    const common = absGitPath(path.join(git(['rev-parse', '--git-common-dir']), 'FETCH_HEAD'));
    if (existsSync(common)) { fetchAgeMin = ageOf(common); fetchFrom = 'common'; }
  }
} catch { /* freshness unknown stays null */ }

const warn = (msg, advice) => { console.log(JSON.stringify({ systemMessage: `stale-clone guard: ${msg}`, hookSpecificOutput: { hookEventName: 'PreToolUse', additionalContext: advice } })); process.exit(0); };

// Story 239.1.2 (hub Sprint 66 leg 1): on `main` the remedy no longer says to
// pull — the guard cannot know whether this seat is the one rooted in REPO, and
// a pull by any other seat moves a checkout under that seat's running process
// (the 2026-10-24 21:56:19Z hub reflog face). It says the fast-forward rule
// instead (hub Story 239.1.1; KB parallel-sessions-one-checkout).
if (behind > 0) {
  const remedy = head === 'main'
    ? `Before editing coordination surfaces, read it at origin/main (git -C "${repo}" show origin/main:FILE), or let that repo's own live seat fast-forward it — never pull a checkout another seat is rooted in (KB parallel-sessions-one-checkout)`
    : `Run: git -C "${repo}" fetch origin && git -C "${repo}" rebase origin/main (or merge it) on ${head} before editing coordination surfaces`;
  warn(`${path.basename(repo)} checkout (${head}) is behind origin/main by ${behind} (warn only)`, `Cached refs say origin/main is ahead of this checkout. ${remedy} (KB dispatch-lines-are-pointers; fleet-view is the re-verification tool).`);
}
const fallbackNote = fetchFrom === 'common' ? " (freshness from the main checkout's fetch — this worktree has not fetched)" : '';
if (fetchAgeMin === null || fetchAgeMin > FETCH_FRESH_MINUTES) warn(`${path.basename(repo)} fetch freshness unknown/stale (warn only)${fallbackNote}`, `Last fetch ${fetchAgeMin === null ? 'not found' : Math.round(fetchAgeMin) + ' min ago'}${fallbackNote}. Run: git -C "${repo}" fetch origin before trusting clone state.`);
process.exit(0);

// Story 128.1.8 (register class stdin-readfilesync-eagain-on-nonblocking-pipe):
// stdin through the async iterator, never readFileSync(0) — a 1.1 MB payload on a
// non-blocking pipe read as unreadable on Linux CI (Node 24, hub #3034's first run).
async function readStdin() {
  const chunks = [];
  for await (const chunk of process.stdin) chunks.push(chunk);
  return Buffer.concat(chunks).toString('utf8');
}
