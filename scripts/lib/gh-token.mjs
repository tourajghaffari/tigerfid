// scripts/lib/gh-token.mjs — the GitHub App token resolver (hub Story 221.1.2, Initiative 221):
// the child env for a script-side `gh` call, with GH_TOKEN set to this box's App INSTALLATION
// token for the TARGET org — minted and cached by scripts/gh-app-token.mjs (Story 221.1.1) — so
// the fleet's scripts spend the App's 5,000 an hour per installation instead of the user's one
// budget shared by every session on both boxes (KB gh-rest-quota-shared-across-sessions). A
// session's own hand `gh` calls never come through here: they stay on the user's login.
//
//   ghEnvFor(org, { env, home, mint, now, memo, warn }) → the env for the gh child
//   orgOfCwd(cwd)                                      → the clone's org from its origin, or null
//
// The ONE consumer is scripts/lib/forge.mjs (the hub's single home for a gh exec), which derives
// the target org per call from the argv and falls back to orgOfCwd, then passes the env it gets
// back to the exec. Resolution, in order:
//   1. no org (a call naming no repo, in a cwd that is no clone)              → env unchanged
//   2. env.FGL_GH_APP === 'off' — the hand switch back to the session's login  → env unchanged
//   3. env.FGL_GH_BIN or env.WRAP_SESSION_GH set — forge's gh STUB seams (the e2e suites): a stub
//      spends no budget and must never receive a real token                    → env unchanged
//   4. no <home>/apps/default — a box with no App (the fallback that keeps every script working;
//      <home> is FGL_HOME or ~/.fgl, the helper's own root)                     → env unchanged
//   5. otherwise { ...env, GH_TOKEN: <the helper's token for org> }: the helper is asked once per
//      org per MEMO_TTL_MS (four minutes — it re-mints under five minutes to expiry, so a long run
//      never holds a token past its hour) and its answer memoized for the process; a helper that
//      refuses (exit 1 or 2) is memoized too, ONE stderr line names the org and the reason, and
//      the env comes back unchanged — fail-open, the budget spent aloud, never silently.
// `env` is never mutated and process.env is never written: the caller gets a new object.
//
// CANONICAL: rides sync-session-wrap.mjs's roster because forge.mjs (synced) imports it. Node
// builtins only, no repo-local import. The helper is found by walking UP from this file for
// scripts/gh-app-token.mjs — two levels in the hub, four from a site's synced copy
// (<hub>/projects/<site>/scripts/lib/) — so a site's own wrap resolves through the hub's helper
// and its PR is the bot's too. This module never reads the key or the config: the helper does,
// in its own process, and only the token crosses the pipe.

import { existsSync } from 'node:fs';
import { spawnSync } from 'node:child_process';
import os from 'node:os';
import path from 'node:path';
import { fileURLToPath } from 'node:url';

export const MEMO_TTL_MS = 4 * 60 * 1000;

const HERE = path.dirname(fileURLToPath(import.meta.url));
const HELPER_REL = path.join('scripts', 'gh-app-token.mjs');
const MAX_WALK = 8;

const fglHome = (env) => env.FGL_HOME || path.join(os.homedir(), '.fgl');

// The hub's helper, by walking up from this file's directory (the hub two levels up; a site's
// synced copy four).
export function findHelper(from = HERE) {
  let dir = from;
  for (let i = 0; i < MAX_WALK; i++) {
    const candidate = path.join(dir, HELPER_REL);
    if (existsSync(candidate)) return candidate;
    const parent = path.dirname(dir);
    if (parent === dir) break;
    dir = parent;
  }
  return null;
}

// The default mint: the helper in its own process against `home`; its stdout is the token, its
// one stderr line the reason when it refuses (the `gh-app-token: ` prefix dropped — ours is added).
function spawnHelper(org, home) {
  const helper = findHelper();
  if (!helper) throw new Error(`no ${HELPER_REL} above ${HERE}`);
  const r = spawnSync(process.execPath, [helper, org], {
    encoding: 'utf8',
    env: { ...process.env, FGL_HOME: home },
    stdio: ['ignore', 'pipe', 'pipe'],
    timeout: 60_000,
  });
  if (r.error) throw r.error;
  if (r.status !== 0 || !r.stdout) {
    const line = (r.stderr || '').trim().split(/\r?\n/)[0] || `helper exit ${r.status}`;
    throw new Error(line.replace(/^gh-app-token: /, ''));
  }
  return r.stdout.trim();
}

// The three origin forms the fleet's clones carry (KB github-org-per-repo-derive-from-remote):
// https://github.com/o/r[.git], git@github.com:o/r[.git], ssh://git@github.com/o/r[.git].
const GITHUB_ORIGIN = /^(?:https:\/\/github\.com\/|ssh:\/\/git@github\.com\/|git@github\.com:)([^/]+)\//;
const ORIGIN_ORG = new Map();

// The cwd's org from its origin — one git read per cwd per process (`remote get-url` is a
// listing verb: no maxBuffer owed, test/git-exec-census.test.mjs); null when the cwd is no clone
// or its origin is not github.com.
export function orgOfCwd(cwd) {
  if (!ORIGIN_ORG.has(cwd)) {
    const r = spawnSync('git', ['-C', cwd, 'remote', 'get-url', 'origin'], {
      encoding: 'utf8',
      timeout: 10_000,
      stdio: ['ignore', 'pipe', 'ignore'],
    });
    ORIGIN_ORG.set(cwd, r.status === 0 ? (GITHUB_ORIGIN.exec(r.stdout.trim())?.[1] ?? null) : null);
  }
  return ORIGIN_ORG.get(cwd);
}

const MEMO = new Map(); // `${home}\0${org}` → { token, until } | { failed: true }

export function ghEnvFor(org, {
  env = process.env,
  home = fglHome(env),
  mint = spawnHelper,
  now = Date.now,
  memo = MEMO,
  warn = (line) => process.stderr.write(`${line}\n`),
} = {}) {
  if (!org) return env;
  if (env.FGL_GH_APP === 'off') return env;
  if (env.FGL_GH_BIN || env.WRAP_SESSION_GH) return env;
  if (!existsSync(path.join(home, 'apps', 'default'))) return env;
  const key = `${home}\u0000${org}`;
  const t = now();
  let entry = memo.get(key);
  if (!entry || (entry.token && t >= entry.until)) {
    try {
      entry = { token: mint(org, home), until: t + MEMO_TTL_MS };
    } catch (e) {
      entry = { failed: true };
      warn(`gh-token: ${org}: ${e.message} — running on the session’s gh login`);
    }
    memo.set(key, entry);
  }
  return entry.token ? { ...env, GH_TOKEN: entry.token } : env;
}
