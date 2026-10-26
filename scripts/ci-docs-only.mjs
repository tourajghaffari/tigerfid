#!/usr/bin/env node
// scripts/ci-docs-only.mjs — the docs-only fast path (hub Story 19.1.3) and
// its third tier (hub Story 19.2.1).
//
// WHAT: classifies a CI run's change set three ways. (1) documentation ONLY
// (`docs_only=true`): the workflow's real jobs carry
// `if: needs.scope.outputs.docs_only != 'true'`, so a docs-only PR skips them
// and bills nothing, while the workflow still RUNS and its required contexts
// still REPORT (a skipped required check counts as passing — KB
// ci-gating-standard rule 6). This is why the fast path must NOT be a
// `paths:` filter: a filtered-out workflow never reports at all and would
// block every docs PR forever. (2) CI-VALIDATED docs only (`lint_only=true`,
// Story 19.2.1): every changed path is documentation but some of it is
// exactly what a CI check validates (plan/close-manifest edits, policed by
// close-lint) — in repos wired for the tier (REPO_VALIDATOR_JOBS below), the
// validator job runs and everything else skips, so a plan edit stops buying
// an OS matrix to run a lint. (3) anything else: full suite.
//
// HOW TO RUN: `node scripts/ci-docs-only.mjs` from the repo root. In CI it is
// the `scope` job's only real step; locally it prints the same verdict against
// your current branch vs origin/main, which is how you debug a surprise.
//
// FAIL-CLOSED, ALWAYS. Every uncertainty resolves to docs_only=false (run the
// full suite): unknown event, missing/!unresolvable base sha, a git failure, a
// zero-length change set, or any path the glob set does not positively
// classify as docs. Being wrong in the other direction merges untested code
// behind a green required check, so doubt must always cost minutes, never
// coverage.
//
// Canonical: synced to every repo by scripts/sync-session-wrap.mjs (as a PAIR
// with scripts/lib/ci-forge.mjs — the forge shim below). Edit HERE.
//
// FORGE-NEUTRAL CORE (Story 43.1.1): every provider read — event name, event
// payload, diff range, verdict publication — goes through the shim
// (scripts/lib/ci-forge.mjs, the scripts/lib/forge.mjs pattern). A forge move
// rewrites only the shim; an unrecognized CI provider fails LOUDLY below
// rather than sitting silently inert on the local-debug fallback.

import { execFileSync } from 'node:child_process';
import { realpathSync } from 'node:fs';
import { fileURLToPath } from 'node:url';
import { detectProvider, eventName, readEventPayload, resolveRange, publishVerdict } from './lib/ci-forge.mjs';

// --- the glob sets (Story 19.1.3 requires these be recorded in the script) ---

// Documentation by default, fleet-wide.
export const DOCS_GLOBS = ['docs/**', '**/*.md'];

// Paths that LOOK like docs but that CI itself validates, so they must always
// buy a full run. All 11 fleet repos run close-lint, which enforces manifest
// lockstep: a plan story-line edit must also touch docs/close/<epic>.json.
// Both files live under docs/, so without this exclusion a plan+manifest PR
// would skip the only check that validates it and lockstep would quietly stop
// being enforced. Costs ~28 of the hub's ~230 monthly docs PRs (July: close/
// 16 + plan/ 12, against kb/ 79 + docs/ 44 + session-notes/ 43 + inbox/ 36).
// docs/provenance/** (hub Story 91.1.1): the record-keyed provenance sidecar,
// validated by the provenance checker (scripts/check-provenance.mjs) in the
// adopter's validator job — so a provenance-only PR must never take the
// docs-only skip (KB fast-path-must-not-skip-its-validator: the trigger set
// and the validator's scope are the same files). Cost: a wired repo
// (REPO_VALIDATOR_JOBS) runs only its validator job; an unwired one runs the
// full suite (fail-closed); a repo with no such paths is unchanged.
export const ALWAYS_FULL_GLOBS = ['docs/project-plan.md', 'docs/close/**', 'docs/provenance/**'];

// Per-repo build inputs that match a docs glob but are BUILD INPUTS, not docs
// — the risk Story 19.1.3 pre-named. Verified per repo at execution
// (2026-07-25); repos absent from this table were confirmed to have no
// markdown build inputs. Repos born AFTER a verification date need their own
// entry assessed at birth — the 2026-08-03 first audits of the two repos born
// 2026-07-31/2026-08-01 found exactly that gap (ghaffaritabrizi.com PR #39,
// bridgetandomid.com PR #17: both CONFIRMED medium, reproduced via classify()).
export const REPO_BUILD_INPUTS = {
  // src/ IS the Eleventy input dir (eleventy.config.js `dir.input: "src"`), and
  // src/menus/*.md are 119 rendered pages carrying front matter. Treating a
  // menu edit as docs-only would ship an unbuilt/broken page behind a green check.
  'kitchenbridget.com': ['src/**'],
  // src/ IS the Eleventy input dir and markdownTemplateEngine is "njk", so
  // every .md under src/ is an executable Nunjucks build input; the config
  // header anticipates .md content pages. Latent today (only src/assets/
  // README.md, ignored as a template) — the first real content .md must not
  // merge behind a skipped build (lawcuments.com Story 4.7.1, B1-F51/B1-F62).
  'lawcuments.com': ['src/**'],
  // tigerian (`packages/**`, `docs/rules/**` — the compiled-rulebook class,
  // hub Story 19.2.7) left this map 2026-09-14: the repo was deleted
  // 2026-09-13 (hub CLAUDE.md § projects/). The class's shape lives on as a
  // literal fixture in test/ci-docs-only.test.mjs.
  // The successor repo's own entry (2026-10-12) sits at the end of this map.
  // src/ IS the Eleventy input dir with markdownTemplateEngine "njk" (the
  // lawcuments.com class), AND three manifest-covered vendored-payload
  // READMEs live under src/ and SHIP via passthrough — a docs-only
  // classification skips the required test job's vendored-payload re-hash,
  // so a payload .md edit could merge behind a green skipped check
  // (2026-08-03 first audit, ghaffaritabrizi.com PR #39; reachable today).
  'ghaffaritabrizi.com': ['src/**'],
  // Same class, born one day later with the same shape (2026-08-03 first
  // audit, bridgetandomid.com PR #17): njk-preprocessed .md under src/ is
  // an executable build input, and the vendored fonts README ships.
  'bridgetandomid.com': ['src/**'],
  // Born 2026-10-11 (hub Story 196.1.1; assessed at birth per KB
  // new-repo-standard item 11): njk pages and the vendored payload's READMEs
  // under src/ are build inputs. Same class as ghaffaritabrizi.com /
  // bridgetandomid.com.
  'gtllclaw.com': ['src/**'],
  // Born 2026-09-05 (hub Story 75.1.2; spec 2026-09-05-tigerdash-design §7.2):
  // src/ IS the Eleventy input dir, and the vendored @fgl/vituno payload lands
  // READMEs under src/ (gates/, icons/, fonts/) — a payload edit classified
  // docs-only would skip the required test job's egress gate and payload
  // re-hash. Same class as ghaffaritabrizi.com / bridgetandomid.com.
  tigerdash: ['src/**'],
  // tigerian returned 2026-10-12 with a different shape (the successor repo,
  // born 2026-09-15 — hub CLAUDE.md § projects/): no markdown feeds its
  // build, but its two root `*_NOTICES.md` files are VALIDATED OUTPUTS of
  // its `test` job — the dependency notices (`node scripts/rust-ci.mjs all`
  // runs `about --check`) and the third-party notices (`npm run
  // check:licenses`). A notices-only PR classified docs-only would skip the
  // one job that polices them (KB fast-path-must-not-skip-its-validator, the
  // new-validator face; found by the tigerian Story 3.1.2 review, tigerian
  // #50; hub inbox fragment tigerian-notices-files-skip-their-validator,
  // triaged 2026-10-12). ONE root glob, never the file names spelled out:
  // tigerian's copy-with-trail gate (`scripts/check-licenses.mjs`) scans
  // every tracked non-.md file — this synced canonical included — and reads
  // any mention of its third-party notices file that is not a derived-work
  // marker as a finding; the first round that carried the two names (hub
  // #3607 → tigerian #62, 2026-10-12) redded tigerian's `test` job on
  // exactly that (test/ci-docs-only.test.mjs pins the literal's absence;
  // hub #3617, folded by Story 201.1.1). The matcher anchors `^…$` and `*`
  // stays inside one segment, so the glob names root files only — a
  // same-named file under docs/ stays docs. The SECOND entry is the asset
  // tree: the gate's asset arm requires a row in `assets/MANIFEST.tsv` for
  // every tracked file under `assets/`, so a kit's LICENSE.md (or the
  // manifest itself) is a build input — `**` crosses segments, and the
  // gate's own exemptions (the manifest, `assets/README.md`) are not the
  // classifier's: an edit there must run the gate (Story 201.1.1).
  tigerian: ['*_NOTICES.md', 'assets/**'],
};

// The third tier's per-repo validator job (Story 19.2.1): the ONE workflow
// job that runs when the change set is CI-validated docs only. An entry here
// is a CONTRACT with that repo's workflow — the named job must gate on
// `docs_only != 'true'` alone while every other real job also gates on
// `lint_only != 'true'` — so a repo joins the tier by landing BOTH the entry
// and the workflow gate edits in the same round. Absent repo = tier not
// wired = lint_only always false = full suite (fail-closed, like every other
// uncertainty in this file).
export const REPO_VALIDATOR_JOBS = {
  // tigerian (`close-lint`, extracted from its `suite` matrix at Story
  // 19.2.1) left this map 2026-09-14: the repo was deleted 2026-09-13.
  // `lint` already contains close-lint beside rubocop — required on main, no
  // extraction needed.
  'deccentral-app': 'lint',
  // close-lint extracted from build-and-check's trailing step into its own
  // `lint` job (hub Story 19.2.8, expeal.com #512 — the third tier's seat;
  // every expeal.com plan / manifest / close PR paid the 3½-minute build for a
  // lint). The provenance checker joins that job at Initiative 91's promotion
  // — the entry does not change then (one job per repo).
  'expeal.com': 'lint',
};

// Per-repo paths under a docs glob that THIS repo's CI validates, merged into
// ALWAYS_FULL_GLOBS for that repo only (hub Story 157.1.1; KB
// fast-path-must-not-skip-its-validator: the trigger set and the validator's
// scope are the same files). Per repo, not fleet-wide: only a repo that has the
// check should pay for it. CONTRACT: an entry is safe only beside a
// REPO_VALIDATOR_JOBS entry whose job runs the check — without one the matched
// paths fall to the full suite (fail-closed: correct, but every edit buys the
// whole suite). test/ci-docs-only.test.mjs pins that every key here is wired.
export const REPO_ALWAYS_VALIDATED_GLOBS = {
  // test/plan-docs.test.js (expeal.com Story 19.3.18, #816; run in the `lint`
  // job since #820) fails a plan document carrying an assembly marker inside
  // a code block — the #762 corruption, which merged green as docs-only.
  'expeal.com': ['docs/superpowers/plans/**'],
};

// The per-repo options main() hands classify — ONE read, exported so the tests
// call the seam main() uses, never a hand-copied merge (hub Story 157.1.1).
export function repoOptions(key) {
  return {
    buildInputs: REPO_BUILD_INPUTS[key] ?? [],
    validatorJob: REPO_VALIDATOR_JOBS[key] ?? null,
    alwaysFull: [...ALWAYS_FULL_GLOBS, ...(REPO_ALWAYS_VALIDATED_GLOBS[key] ?? [])],
  };
}

// --- pure core (unit-tested) ---

// Glob subset: `**` (any path segments), `*` (within a segment), `?` (one char).
// Mirrors the close-manifest matcher's subset — no extglob, no braces.
export function globToRegExp(glob) {
  let re = '';
  for (let i = 0; i < glob.length; i++) {
    const c = glob[i];
    if (c === '*') {
      if (glob[i + 1] === '*') {
        // `**/` consumes zero or more full segments; a trailing `**` takes the rest.
        if (glob[i + 2] === '/') { re += '(?:[^/]+/)*'; i += 2; } else { re += '.*'; i += 1; }
      } else re += '[^/]*';
    } else if (c === '?') re += '[^/]';
    else re += c.replace(/[.+^${}()|[\]\\]/g, '\\$&');
  }
  return new RegExp(`^${re}$`);
}

export const matchesAny = (p, globs) => globs.some((g) => globToRegExp(g).test(p));

// The verdict. Returns { docsOnly, lintOnly, reason, offenders } — never
// throws. The tiers are mutually exclusive; every uncertainty (empty set, a
// build input, any non-doc path, no recorded validator job) resolves to BOTH
// false — the full suite.
export function classify(changedPaths, { docsGlobs = DOCS_GLOBS, alwaysFull = ALWAYS_FULL_GLOBS, buildInputs = [], validatorJob = null } = {}) {
  const full = (reason, offenders = []) => ({ docsOnly: false, lintOnly: false, reason, offenders });
  if (!Array.isArray(changedPaths) || changedPaths.length === 0) {
    return full('empty change set — cannot prove docs-only, running full suite');
  }
  const build = changedPaths.filter((p) => matchesAny(p, buildInputs));
  if (build.length) {
    return full('changed a markdown BUILD INPUT for this repo, not documentation', build);
  }
  const nonDocs = changedPaths.filter((p) => !matchesAny(p, docsGlobs));
  if (nonDocs.length) {
    return full('changed non-documentation paths', nonDocs);
  }
  const forced = changedPaths.filter((p) => matchesAny(p, alwaysFull));
  if (forced.length) {
    // The third tier (Story 19.2.1): everything changed is documentation and
    // the CI-validated part has a recorded validator job — run just that job.
    if (validatorJob) {
      return {
        docsOnly: false,
        lintOnly: true,
        reason: `all ${changedPaths.length} changed path(s) are documentation; the CI-validated ones run only their validator job '${validatorJob}'`,
        offenders: forced,
      };
    }
    return full('changed a CI-validated doc (close manifest / project plan / provenance); no validator job recorded for this repo', forced);
  }
  return { docsOnly: true, lintOnly: false, reason: `all ${changedPaths.length} changed path(s) are documentation`, offenders: [] };
}

// Repo identity for the per-repo table: the origin remote's basename, so a
// renamed working directory can't silently drop a repo's build-input rules.
export function repoKeyFromRemote(remoteUrl, fallback = '') {
  if (!remoteUrl) return fallback;
  // A backslash separates too: on win32 a local-path origin prints verbatim as
  // `C:\…\repo.git` (hub Story 157.1.3).
  const m = String(remoteUrl).trim().replace(/\.git$/, '').match(/([^/\\:]+)$/);
  return m ? m[1] : fallback;
}

// --- IO ---

const git = (args) => execFileSync('git', args, { encoding: 'utf8', stdio: ['ignore', 'pipe', 'pipe'] });

export function changedPathsFor(base, head) {
  // Three-dot: everything the branch introduced relative to the merge base.
  const out = git(['diff', '--name-only', `${base}...${head}`]);
  return out.split('\n').map((s) => s.trim()).filter(Boolean);
}

function main() {
  const env = process.env;
  const provider = detectProvider(env);
  if (provider === 'unknown-ci') {
    // The Initiative 43 defect, closed: a foreign forge used to take the
    // local-debug fallback and never publish a verdict — silently inert
    // behind a green job. Fail-closed here means fail LOUDLY: this exit is
    // the one deliberate non-zero in the file.
    console.error('ci-docs-only: running under an unrecognized CI provider — no forge shim is wired for it (scripts/lib/ci-forge.mjs). Refusing to sit silently inert: run the full suite unconditionally, or port the shim.');
    process.exit(1);
  }
  let verdict;
  try {
    let range = resolveRange(env, readEventPayload(env));
    if (range.error && !eventName(env)) {
      // Local debugging convenience: compare against origin/main.
      range = { base: 'origin/main', head: 'HEAD' };
    }
    if (range.error) {
      verdict = { docsOnly: false, lintOnly: false, reason: range.error, offenders: [] };
    } else {
      const key = repoKeyFromRemote(git(['remote', 'get-url', 'origin']).trim());
      const paths = changedPathsFor(range.base, range.head);
      verdict = classify(paths, repoOptions(key));
      verdict.repo = key;
      verdict.count = paths.length;
    }
  } catch (err) {
    verdict = { docsOnly: false, lintOnly: false, reason: `could not compute the change set (${err.message.split('\n')[0]})`, offenders: [] };
  }

  const decision = verdict.docsOnly
    ? 'DOCS-ONLY — skipping the suite'
    : verdict.lintOnly
      ? 'VALIDATED-DOCS — running only the validator job'
      : 'FULL SUITE';
  console.log(`ci-docs-only: ${decision}`);
  console.log(`  why: ${verdict.reason}`);
  if (verdict.offenders?.length) {
    console.log(`  triggered by: ${verdict.offenders.slice(0, 10).join(', ')}${verdict.offenders.length > 10 ? ` (+${verdict.offenders.length - 10} more)` : ''}`);
  }
  // Publication is the shim's op (both outputs, always, explicit booleans —
  // see publishVerdict). No channel = the local-debug shape; stdout above is
  // the verdict's home there.
  publishVerdict(env, verdict);
  process.exit(0); // Reporting only — never fail the gate job itself.
}

// Hardened isMain (Story 26.1.4): the old `file://${argv[1]}` concat never
// matched Windows drive paths (backslashes) or symlink-routed invocations, so
// CI's own shape (`node scripts/ci-docs-only.mjs`) silently no-oped there.
// Realpath the argv side vs the resolved module URL — the block-secrets /
// close-lint sibling idiom (KB macos-tmpdir-symlink-spawn-tests).
const argvReal = (() => { try { return process.argv[1] && realpathSync(process.argv[1]); } catch { return null; } })();
if (argvReal === fileURLToPath(import.meta.url)) main();
