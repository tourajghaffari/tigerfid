# Friction log — Sprint 3, "Ready for the sitting"

This log records what got in the way while Sprint 3 was proposed and run:
the friction itself, what it cost, and what would prevent it. It is public,
like everything in this repo, and written for a stranger. The sprint block
is in `docs/project-plan.md` (§ Sprint 3) while the sprint is live, and in
`docs/archives/project-plan-completed.md` once it has run and been reviewed.

Each leg appends one section with:
- its context checkpoint: the leg plan's task count, and how many
  compactions, forced re-reads and lost-context defects it met;
- each friction, with its cost and its suggested fix;
- each guard event, with the guard's own banner line quoted whole and its
  register class id beside the word "seat" (the block's § Friction log).

## The proposal (2026-10-28)

Proposed by the hub-rooted Windows session "tigerfid Sprint 3 proposal"
(session `4ef9427a-246a-42f9-98aa-be892d938900`), fresh by `/clear` of the
session "tigerfid Sprint 2 review" (background-task path id
`2a0782f7-f632-4bab-b967-a9cfbfe1ba93`), in one sitting. Not elevated.

**Context checkpoint:** no compaction, no forced re-read, no lost-context
defect.

- **F1 — The orders arrived after the work began.** Omid opened the session
  with the proposal's intent at the terminal's `date -u` read of 10:43:54Z.
  Air Traffic Control's orders 18 were sent at 10:49:58Z, after the inputs
  were read and the findings re-measured. They matched the work and added
  four pre-flight rows (KB `plan-authoring-for-cold-execution` item 53, a
  test-arming read per leg, § Roles' "flagged" wording, the touched-file
  ledger), which the block carries. Cost: none; the rows were added before
  the block was written.
- **F2 — A test-arming read needs a script.** The docs-only classifier
  exports `classify()` and `repoOptions()`, but no command prints the verdict
  for a change set that does not exist yet. The proposal wrote a scratch
  script that feeds each leg's ledger to them (the block's § Verified-live).
  Cost: one short script. Suggested fix: a `--paths <p,…>` flag on
  `scripts/ci-docs-only.mjs` that classifies a given list (hub-owned: the
  script is a synced canonical).
- **F3 — The close runner's review-seating line is not this proposal's.**
  The runner printed "REVIEW SEATING MISSING (Story 147.1.3 — KB
  epic-close-protocol): Sprint 2's review appended no register seat and
  records no none-qualified line". The review sent its seats to Air Traffic
  Control as text. The block hands the census to the closer (Story 2.1.8).

**Guard events:**
- `piped-checker-exit` seat: this proposal's first `npm test` read was piped
  into `tail` and drew the guard, "piped-checker guard (warn, not a block)".
  Its `PIPESTATUS[0]` read 0, and the run was repeated unpiped into a scratch
  file: exit 0, 17 of 17.
- `piped-checker-exit` seat: the session's first `git fetch origin` in this
  repo was piped into `tail`, with no banner. The fetch was read after it by
  `git rev-list --count HEAD..origin/main`: 0.
- `cwd-drift` seat: eight calls that opened with a `cd` into this repo or its
  `docs/study/` each drew "cwd-drift guard (warn, not a block)", and the calls
  after them drew its second arm. Every git command carried `git -C` with the
  literal path or ran after a `cd` in the same call; none ran in the wrong
  repository.
