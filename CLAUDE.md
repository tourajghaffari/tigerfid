# tigerfid

**Working outside the Fields Ghaffari Labs hub?** The hub references in this repo (KB slugs, hub stories, hub specs) will not resolve for you: read `readme.md`, and `guides/` once the build guides exist.

This repo lives nested inside the `fields-ghaffari-labs` hub checkout, so every session here — repo-rooted or hub-rooted — loads the hub's CLAUDE.md and KB as ancestors (hub-as-parent; see hub KB `hub-as-parent-onboarding` if you cloned this repo standalone). Fleet workflow rules live in the hub file; PR-only merges are enforced by this repo's own ruleset and hooks regardless.

## What this repo is

TigeRFID: the ActiveWave active-RFID system that Touraj Ghaffari's company
built from 2001, which his sons open-sourced under Apache-2.0 in 2017–2018
in honor of his love of spreading knowledge (the dedication in
`readme.md`). It is being rebuilt from the ground up: a design study decides
a new system against three acceptance scenarios, then a first build — one
Reader, one Field Generator and three tags across two zones — that a
complete beginner can follow on Windows, macOS or Linux with free tools
only. The plan is `docs/project-plan.md`. Adopted into the FGL fleet
2026-10-26 (hub spec `2026-10-26-tigerfid-onboarding-design`); the first
public repo the fleet holds.

Its sibling `activewaveinc.com` (a separate fleet repo) is the marketing
site. This repo is where the system is built and documented: no file is
shared by the two, and nothing here edits the site.

## The originals

- The 2018 state lives at the annotated tag `activewave-2018` →
  `f25ff13f3fb672d8ec91f1fadc8894d69f781ed6`, with its GitHub release
  "ActiveWave, as open-sourced (2017–2018)". The tag is never moved,
  deleted or re-pointed; the history is never rewritten or force-pushed —
  the forks depend on it.
- Read an original at the tag (`git show activewave-2018:<path>`), or in the
  reference folder recreated from it (plan Story 1.1.3).
- Since the restructure (plan Story 1.1.2) the current tree holds the
  rebuild plus the 2018 `license`, `contributing.md`, `readme.md` and
  `_assets/`; every other 2018 path is read at the tag. The README's
  dedication sections (Introduction, Fun Touraj Fact, Thank You) stay
  verbatim.

## CI

- Every job runs on GitHub-hosted `runs-on: ubuntu-latest` — never
  `vars.FLEET_RUNNER` (spec C2). This repo is public; the fleet's
  self-hosted runner group refuses public repos and must keep refusing,
  because opening it would let any fork's pull request run code on Omid's
  machines. Hosted minutes are free for public repos.
- Checkouts are blobless (`fetch-depth: 0` + `filter: blob:none`, spec C3):
  full history for the merge-base reads, never the 2018 blobs. They are not
  sparse: Story 1.1.2 moved the 2018 folders off `main` and retired the
  interim sparse view, whose excluded names (`software/`, `api/`) would
  otherwise hide the rebuild's own folders from CI and from the secret scan.
  Local clones are partial (`--filter=blob:none`) and not sparse either.
- Two required checks, `scope` and `test`; extend `test`, never rename it.
  The commands are recorded in `docs/ci-contract.md`.
- `npm test` runs what CI's `test` job runs: the close-protocol lint
  (`node scripts/close-lint.mjs`) and the reference-check tests. No npm
  dependencies exist; the repo-root `.npmrc` guard (`ignore-scripts=true`)
  stays when the first one arrives.

## The reference corpus

- `../tigerfid-reference/` holds the original tree recreated from the tag,
  the old website's capture and the study's comparisons. It is a plain
  folder, never a repo, never committed.
- Read `docs/reference-repos.md` before reading anything in it (the dial,
  the reading rules, the licence audit), and `docs/reference-research.md`
  before any search for sources.
- `npm run setup` verifies it (`node scripts/reference-check.mjs`);
  `--recreate` rebuilds the original tree from the tag.
- The pins live in `docs/reference-corpus.tsv` and nowhere else.

## Writing in public

Everything here — plan, specs, notes, close records, commits, PR bodies —
is public and written for a stranger (spec D8).

- **Never:** credentials, hostnames, IP addresses or machine names; personal
  or family words beyond what `readme.md` already publishes; anyone else's
  data.
- **Never** a medical claim: no page, guide or README calls the system a
  medical device or a monitor of vital signs (spec C8).
- **Allowed:** session ids, hub PR and story numbers, KB slugs — opaque
  pointers that resolve only for the fleet.
- Commits use Omid's GitHub noreply address, set per clone with
  `git config user.email` before the clone's first commit.
- Session-presence markers and wrap tombstones go to the hub's origin,
  never to this repo's (hub Story 243.1.1).

## Contributions and strangers' pull requests

- Issues and questions are welcome now; pull requests once the design study
  has set the direction (spec D10; the README and `contributing.md` say so).
  Conduct reports go to the address `contributing.md` names (Contributor
  Covenant 3.0, linked, not copied).
- **A fleet session never checks out a stranger's PR branch on Omid's
  machines:** that branch's `.claude/`, `scripts/` and `.githooks/` would run
  beside the machine's credentials. Review it by `gh pr diff <n>` (and
  `gh pr view <n>`) only.
- A stranger's PR merges like any other: through the required checks, by
  Omid's merge.

## Git workflow

- Never commit directly to `main`: branch → push → PR. The `protect-main`
  ruleset enforces PR-only merges with the required `test` + `scope` checks;
  the repo squash-merges only. `scripts/block-commit-on-main.mjs` (a
  PreToolUse hook in `.claude/settings.json`) blocks local commits on
  `main`.
- After cloning: `git config core.hooksPath .githooks` arms the pre-commit
  secret scan.
- `gh pr merge --auto` waits until `protect-main` requires a check (without
  one it merges instantly); from then on, docs-only PRs may arm it. Other PRs
  merge by Omid's word.

## Session handoff

This repo's cross-machine session memory is `docs/notes.md` — public, like
everything here — wrapped by `scripts/wrap-session.mjs`, a canonical synced
from the hub: edit the hub's copy and re-sync, never hand-edit this one.
