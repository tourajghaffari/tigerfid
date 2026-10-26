# CI contract — tigerfid

Per hub KB `ci-portability-standard`: the commands that must pass for a
change to merge. The adapter — `.github/workflows/ci.yml` (GitHub Actions) —
is THIN: a forge or CI-provider move rewrites the adapter to invoke the same
commands, never the commands or any CI logic.

## The contract

Runtime: Node 24 (the fleet pin), on GitHub-hosted `ubuntu-latest` runners
only — this repo is public, and the fleet's self-hosted runners never run a
public repo's jobs (hub spec `2026-10-26-tigerfid-onboarding-design`, C2).
No dependencies are installed. Both jobs need a **full-history, blobless
checkout** (`fetch-depth: 0` + `filter: blob:none`, spec C3): the fast path
diffs the merge ref against its first parent, and `scripts/close-lint.mjs`'s
manifest-lockstep check resolves `merge-base(origin/main, HEAD)` — a shallow
clone makes that check skip (loudly, but unenforced) — while no run
downloads the 2018 history's blobs.

Required context `scope` (the docs-only fast path, hub Story 19.1.3 — it
decides whether the `test` job below runs; fail-closed, so any uncertainty
buys the full run — plus the tick-merge-claim arm and the secret-scan
backstop, which run on every PR, docs-only ones included):

```
node scripts/ci-docs-only.mjs
node scripts/check-tick-merge-claim.mjs
node scripts/block-secrets.mjs --repo-scan
```

Required context `test` (the lint leg; with `scope`, one of the two required
checks):

```
node scripts/close-lint.mjs
```

The context NAMES are load-bearing — required checks match by name (hub KB
`required-status-check-rename`); keep `scope` and `test` stable. `test`
grows as the rebuild brings checks to run — the licence gate first (plan
Story 1.1.4), then hardware, firmware and software checks as they come to
exist — and each one joins this contract in the PR that wires it.

## Platform surface (inventoried hub-side with exit paths)

Everything platform-specific this repo carries appears in hub KB
`ci-portability-standard`'s coupling inventory:

- `ci.yml` — the adapter (exit: rewrite it per forge, invoking the same
  documented commands above).
- `protect-main` ruleset + the required `scope` and `test` — branch
  protection (exit: the next forge's protected branches + must-pass
  pipelines).
- Auto-merge reliance and the `gh` CLI in the hub-synced session scripts —
  exits through the hub's forge adapter, `scripts/lib/forge.mjs`
  (canonical-synced into this repo).
- `.github/dependabot.yml` — Actions-pin currency (exit: Renovate).

Adapter thinness is machine-checked hub-side by
`scripts/fleet/check-workflow-thinness.mjs`, and this document against the
live workflow by `scripts/fleet/check-ci-contract.mjs` (both in the hub's
wrap heartbeat).

## Docs-only fast path

`ci.yml` runs the cheap `scope` job first; `test` carries
`if: needs.scope.outputs.docs_only != 'true'` and is **skipped** when the
change set is documentation only. The workflow still runs and the required
contexts still report — a skipped required check counts as passing. This is
deliberately **not** a `paths:` filter: a filtered-out workflow never reports
at all, so a required context would block every docs PR forever (hub KB
`ci-gating-standard`).

The decision lives in the hub canonical `scripts/ci-docs-only.mjs` (edit the
hub copy and re-sync — never hand-edit the synced copy here). This repo uses
the fleet default:

- **documentation** — `docs/**`, `**/*.md`
- **never documentation, fleet-wide** — `docs/project-plan.md`,
  `docs/close/**` (CI's own `close-lint` validates these, so they always buy
  a full run)
- **no markdown build inputs** (`REPO_BUILD_INPUTS`, assessed 2026-10-26: no
  build exists; re-assessed if a docs site or a generated guide ever reads
  markdown)
