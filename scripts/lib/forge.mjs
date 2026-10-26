// scripts/lib/forge.mjs — the forge adapter (hub Story 17.1.3): the ONE module
// that owns every forge (GitHub-today) invocation in the hub's scripts, so a
// forge move is this file's rewrite, not five scripts'. Consumers:
// wrap-session.mjs, kb.mjs, inbox.mjs, hub-doc.mjs, fleet/round.mjs,
// hub-status.mjs, check-ci-gating.mjs, the heartbeat's arms under fleet/, the
// sweep and train drivers, gh-quota.mjs. A bare `gh` call anywhere else in
// scripts/ fails test/forge-boundary.test.mjs (hub CI) by design — new forge
// operations are added HERE, never inline.
//
// CANONICAL: like scripts/lib/hub-write-worktree.mjs, this file rides the
// CANONICALS set (sync-session-wrap.mjs) because the fleet-synced
// wrap-session.mjs imports it — every repo under projects/ receives the set
// at the same relative paths. Node builtins plus ./gh-token.mjs (a roster
// sibling, below); no other repo-local import.
//
// The token (hub Story 221.1.2, Initiative 221): every exec here runs under the
// env scripts/lib/gh-token.mjs's ghEnvFor returns for the call's TARGET org —
// GH_TOKEN set to this box's GitHub App installation token for that org, so the
// fleet's scripts spend the App's budget, not the user's one shared 5,000 an
// hour (KB gh-rest-quota-shared-across-sessions). One instance bound to the hub
// serves BOTH orgs (check-ci-gating's fifteen slugs, the runner census's two
// orgs), so the org is read per CALL from the argv by targetOrg below — the
// --repo flag, an api path's repos/<o>/, orgs/<o>/ or organizations/<o>/
// segment, a pr view URL — and only then from the cwd's origin (wrap-session's
// own repo; gh's {owner}/{repo} placeholder is the cwd face too). `defaults.env`
// is the base env (process.env unless given); `defaults.tokenEnv` the resolver
// (ghEnvFor unless given — the tests inject one). Without an App on the box, or
// under the gh stub seams, ghEnvFor hands the base env back unchanged.
//
// Exec standard (Story 8.1.1): argv execFileSync only — NO shell, ever.
// Test seam (the 13.1.5 pattern): when WRAP_SESSION_GH or FGL_GH_BIN is set
// (only the e2e suites set them — wrap-session's suite the former, the
// kb/inbox/hub-doc/round suites the latter), gh runs as `node <that script>
// <args>` instead; PATH-shadowed stubs are POSIX-only (KB
// win32-path-shim-stubs). Unset in production: plain `gh`.
// Failure contract (review fix 8.1.6): quiet ops pipe stderr — never 'ignore'
// it — so a thrown error's .stderr carries gh's message and callers can
// classify "no PR for this branch" apart from a real gh failure.
//
// The adapter contract, with the GitLab mapping sketched (per KB
// ci-portability-standard surfaces 3–4; vocabulary caveat below):
//   forgeFor(cwd, defaults) → ops bound to that repo checkout
//   prCreate({head,title,body} | {fill}) → PR URL
//       GitLab: `glab mr create` / POST /projects/:id/merge_requests
//   prMerge(branch, {auto, methodFlag, deleteBranch, matchHeadCommit})  — --delete-branch unless deleteBranch: false
//       GitLab: `glab mr merge` — --auto ↔ MWPS (merge-when-pipeline-succeeds);
//       squash policy is a project setting rather than a per-merge flag
//   prClose(branch)
//       GitLab: `glab mr close` / PUT state_event=close
//   prViewJson(ref, fields, {quiet}) → parsed PR
//       GitLab: GET /merge_requests/:iid — field vocabulary DIFFERS
//       (mergeStateStatus ↔ detailed_merge_status, statusCheckRollup ↔
//       head_pipeline); see caveat
//   prListJson({repo,head,state,search,limit,json}) → parsed array
//       GitLab: GET /merge_requests?source_branch=&state= (state names differ:
//       merged/opened/closed)
//   apiJson(path) → parsed REST GET; apiJsonAsync(path) → the same, as a promise
//       (Story 153.1.1 — a caller that fans reads out)
//       GitLab: GET equivalents — rulesets/protection ↔ protected branches +
//       approval rules endpoints
//   repoViewJson(fields) → parsed repo settings
//       GitLab: GET /projects/:id (merge-method fields ↔ merge_method)
//   available() / authOk() → boolean probes
//       GitLab: `glab --version` / `glab auth status`
// Vocabulary caveat: ops return the forge's OWN JSON fields (GitHub names
// today). Consumers that branch on field values (wrap-session's merge-state
// classifiers) would translate at this boundary in a real move — that
// translation lands here, not in the consumers.

import { execFile, execFileSync } from 'node:child_process';
import { ghEnvFor, orgOfCwd } from './gh-token.mjs';

const METHOD_FLAGS = new Set(['--merge', '--squash', '--rebase']);

const seamBin = () => process.env.WRAP_SESSION_GH || process.env.FGL_GH_BIN || null;

const ghExec = (args, opts) => {
  const bin = seamBin();
  return bin
    ? execFileSync(process.execPath, [bin, ...args], opts)
    : execFileSync('gh', args, opts);
};

// Story 153.1.1 (hub #3138): ghExec's async twin, for a caller that fans reads
// out (the capacity arm's jobs, 8 at a time). Same seam and argv form; callers
// pass execOpts, so the cwd and output cap are the sync ops' own. A failure
// rejects with gh's stdout/stderr on the error, as the quiet sync path throws.
const ghExecAsync = (args, opts) => {
  const bin = seamBin();
  const [file, argv] = bin ? [process.execPath, [bin, ...args]] : ['gh', args];
  return new Promise((resolve, reject) => {
    execFile(file, argv, opts, (err, stdout, stderr) => {
      if (err) {
        err.stdout = stdout;
        err.stderr = stderr;
        reject(err);
        return;
      }
      resolve(stdout);
    });
  });
};

// Story 221.1.2: the TARGET org of one gh call, from its argv — the --repo
// flag's owner, an api path's repos/<o>/, orgs/<o>/ or organizations/<o>/
// segment, a pr view URL's owner — else null, and the caller falls back to the
// cwd's origin. gh fills its {owner}/{repo} placeholder from the cwd, so a
// placeholder owner reads as no org here (the cwd face).
const GITHUB_URL = /^https?:\/\/github\.com\/([^/]+)\//;
const ownerOf = (segment) => (segment && !/^\{.*\}$/.test(segment) ? segment : null);
const targetOrg = (args) => {
  const repoAt = args.indexOf('--repo');
  if (repoAt >= 0 && args[repoAt + 1]) {
    const value = args[repoAt + 1];
    const url = GITHUB_URL.exec(value);
    return ownerOf(url ? url[1] : value.replace(/^github\.com\//, '').split('/')[0]);
  }
  if (args[0] === 'api') {
    const apiPath = args.slice(1).find((a) => !a.startsWith('-')) ?? '';
    const m = /^\/?(?:repos|orgs|organizations)\/([^/?]+)/.exec(apiPath);
    return m ? ownerOf(m[1]) : null;
  }
  if (args[0] === 'pr' && args[1] === 'view') {
    const url = GITHUB_URL.exec(args[2] ?? '');
    return url ? url[1] : null;
  }
  return null;
};

// One adapter instance per repo checkout. `defaults` set the instance's
// baseline exec posture ({quiet, timeout}); every op takes a trailing opts
// object overriding it per call. Loud ops leave stdio unset (execFileSync
// default: stdout piped, stderr through to the terminal — today's behavior in
// every consumer); quiet ops use ['ignore','pipe','pipe'] per the 8.1.6
// failure contract above.
//
// The output cap (Story 103.1.1; register class
// child-process-default-maxbuffer-git-output, seat 4): every op's child output
// is read under the cap named in execOpts — the ONE place, so a new op
// inherits it (test/forge.test.mjs pins that every exec here takes its options
// from execOpts). Node's default is 1 MiB, and a busy org's API listing passes
// it: a run object is ~14 KB, so a 100-run page is ~1.4 MB and threw ENOBUFS
// (hub ci-capacity, 2026-09-18). 64 MiB is two orders of headroom over the
// largest page the API serves and still bounded — the fleet's precedent is
// readOriginCopy in sync-session-wrap.mjs. Past the cap the read still THROWS
// ENOBUFS: a loud failure, never a truncated parse — add no catch for it.
export function forgeFor(cwd, defaults = {}) {
  const { tokenEnv = ghEnvFor } = defaults;
  // Story 221.1.2: the env is resolved per exec for the call's target org (the argv first,
  // the cwd's origin second) — hence execOpts takes the args beside the per-call opts.
  const execOpts = ({ quiet = defaults.quiet, timeout = defaults.timeout } = {}, args = []) => ({
    cwd,
    encoding: 'utf8',
    maxBuffer: 64 * 1024 * 1024,
    env: tokenEnv(targetOrg(args) ?? orgOfCwd(cwd), { env: defaults.env ?? process.env }),
    ...(timeout ? { timeout } : {}),
    ...(quiet ? { stdio: ['ignore', 'pipe', 'pipe'] } : {}),
  });
  const gh = (args, opts) => ghExec(args, execOpts(opts, args)).trim();
  const ghJson = (args, opts) => JSON.parse(gh(args, opts));

  return {
    available(opts) {
      try {
        ghExec(['--version'], { ...execOpts(opts, ['--version']), stdio: 'ignore' });
        return true;
      } catch {
        return false;
      }
    },
    authOk(opts) {
      try {
        gh(['auth', 'status'], opts);
        return true;
      } catch {
        return false;
      }
    },
    prCreate({ head, title, body, fill } = {}, opts) {
      const args = fill
        ? ['pr', 'create', '--fill']
        : ['pr', 'create', '--head', head, '--title', title, '--body', body];
      return gh(args, opts);
    },
    // Default method is --squash: the fleet is squash-only on all 11 repos
    // (Story 21.1.1 sweep, 2026-07-29) — the old --merge default made GitHub
    // reject auto-merge arming everywhere ("Merge method merge commits are
    // not allowed"; first hit: hub PR #845, a kb.mjs capture).
    // `deleteBranch` (hub Story 81.2.9, reviewer finding I-2 — KB
    // gh-checkout-cwd-local-side-effects): gh's --delete-branch also deletes
    // the LOCAL branch of that name in its cwd and, when the checkout is ON
    // it, switches to the base and pulls. The default keeps the flag (every
    // existing consumer — wrap-session, the round, kb/inbox/hub-doc — passes
    // nothing and arms with --auto); a caller that must never touch the
    // session clone (the merge-train driver) passes false, and the repo's
    // delete_branch_on_merge setting deletes the remote branch instead.
    // `matchHeadCommit` (hub Story 171.1.1): a full sha the merge is pinned to —
    // GitHub refuses it when the PR's head is anything else. GitLab: `sha=`.
    prMerge(branch, { auto = false, methodFlag = '--squash', deleteBranch = true, matchHeadCommit } = {}, opts) {
      if (!METHOD_FLAGS.has(methodFlag)) {
        throw new Error(`forge.prMerge: unknown methodFlag ${JSON.stringify(methodFlag)}`);
      }
      gh(
        [
          'pr', 'merge', branch,
          ...(auto ? ['--auto'] : []),
          methodFlag,
          ...(deleteBranch ? ['--delete-branch'] : []),
          ...(matchHeadCommit ? ['--match-head-commit', matchHeadCommit] : []),
        ],
        opts
      );
    },
    prClose(branch, opts) {
      gh(['pr', 'close', branch], opts);
    },
    // Hub Story 81.2.2 (the merge-train driver): retarget a stacked PR to a
    // new base BEFORE its predecessor merges — the one order deterministic
    // under every branch-delete mechanism (KB github-stacked-pr-base-branch-
    // deletion, observation eleven). GitLab: PUT /merge_requests/:iid
    // target_branch=<base>.
    prEditBase(number, base, opts) {
      gh(['pr', 'edit', String(number), '--base', base], opts);
    },
    prViewJson(ref, fields, opts) {
      return ghJson(['pr', 'view', ref, '--json', fields], opts);
    },
    prListJson({ repo, head, state, search, limit, json } = {}, opts) {
      return ghJson(
        [
          'pr',
          'list',
          ...(repo ? ['--repo', repo] : []),
          ...(head ? ['--head', head] : []),
          ...(state ? ['--state', state] : []),
          ...(search ? ['--search', search] : []),
          ...(limit != null ? ['--limit', String(limit)] : []),
          '--json',
          json,
        ],
        opts
      );
    },
    apiJson(apiPath, opts) {
      return ghJson(['api', apiPath], opts);
    },
    async apiJsonAsync(apiPath, opts) {
      return JSON.parse((await ghExecAsync(['api', apiPath], execOpts(opts, ['api', apiPath]))).trim());
    },
    repoViewJson(fields, opts) {
      return ghJson(['repo', 'view', '--json', fields], opts);
    },
  };
}
