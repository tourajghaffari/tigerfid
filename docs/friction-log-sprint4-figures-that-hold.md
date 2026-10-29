# Friction log — Sprint 4, "Figures that hold for the sitting"

This log records what got in the way while Sprint 4 was proposed and run:
the friction itself, what it cost, and what would prevent it. It is public,
like everything in this repo, and written for a stranger. The sprint block
is in `docs/project-plan.md` (§ Sprint 4) while the sprint is live, and in
`docs/archives/project-plan-completed.md` once it has run and been reviewed.

Each leg appends one section with:
- its context checkpoint: the leg plan's task count, and how many
  compactions, forced re-reads and lost-context defects it met;
- each friction, with its cost and its suggested fix;
- each guard event, with the guard's own banner line quoted whole and its
  register class id beside the word "seat" (the block's § Friction log).

The reviewer pass's seat check names this sprint by the register's own
wording: `check-register-seats.mjs --sprint 'tigerfid — Sprint 4'` (Sprint
3's friction R4).

## The proposal (2026-10-29)

Proposed by the hub-rooted Windows session "tigerfid Sprint 4 proposal"
(session `cb189c7f-0ffb-4dea-bea2-8485d49ee215`), on the Mac coordinator's
orders 19, in one sitting. **The fresh-session read:** fresh by `/clear`.
The background-task path id is `13efb2b1-ddbf-4cd6-824f-74b58f5302ce`, the
session id of "Windows 4", Sprint 3's leg 3 executor; that process last held
the Sprint 3 reviewer pass (`f050a5d6-…`, wrapped at #54), which this
`/clear` ended. Not elevated: a Windows administrator-role read, `False`, at
00:42:54Z.

**Context checkpoint:** no compaction, no forced re-read, no lost-context
defect.

**The approval:** Omid, first-hand in this terminal between its `date -u`
reads of 00:50:57Z and 06:25:22Z: "Approve as proposed (Recommended)". The
question laid out the one call a reader might change, the split of Story
2.1.12, with its cost, and D2's clause for a baby's band.

- **F1 — The hub checkout's sprint parser is not origin/main's.** The close
  runner printed "hub tooling differs from origin/main:
  scripts/fleet-view/check-sprint-block.mjs,
  scripts/fleet-view/sprint-blocks.mjs,
  scripts/fleet/check-item8-dispositions.mjs, scripts/fleet/watch-pr.mjs".
  The hub checkout is shared by other live seats, so it is read at
  origin/main and never pulled. The proposal extracted origin/main's
  `scripts/fleet-view/` and `scripts/lib/` (`702c116c`) into its scratchpad
  with `git archive` and ran the parser from there, leaving the checkout
  untouched. The close runner's teeth are this repo's own run lines, so its
  PASS and FAIL readings do not depend on that difference. Cost: one
  extraction. Suggested fix, hub-owned: a parser command that runs
  origin/main's copy by itself.
- **F2 — A test-arming read still needs a script** (Sprint 3's F2, still
  open). `scripts/ci-docs-only.mjs` exports `classify()` and
  `repoOptions()`, and no command classifies a change set that does not
  exist yet. The proposal wrote a scratch script again. Cost: one short
  script. Suggested fix, hub-owned (the script is a synced canonical): a
  `--paths <p,…>` flag.
- **F3 — A needle split by a line break, caught.** The anchor grep for
  finding 6, `grep -n 'except the Arm'`, read 0 on
  `docs/study/parts-and-toolchains.md`, because the sentence wraps after
  "for every tool except two." The anchor was read whole at l.242–246
  before the block cited it. The block's merge-loop line carries Sprint 3's
  F7 rule (the shortest needle a line break cannot split).
  `wrapped-sentence-needle-undercount` seat, caught.
- **F4 — `main` moved while the approval waited.** The question went out
  after the read of 00:50:57Z, and the answer came before the read of
  06:25:22Z. In between, `main` took a fleet sync round (#56: the
  `block-secrets`, `close-lint` and `check-tick-merge-claim` canonicals). The
  branch took `main` by a merge, not a rebase, so its push stays a fast
  forward, and the sign-off commit re-ran the synced `close-lint`. Cost: one
  merge. No fix needed: the squash merge flattens it.

**Guard events:**
- `cwd-drift` seat: the calls that opened with a `cd` into this repo drew
  "cwd-drift guard (warn, not a block)", and the calls after them drew its
  second arm. Every git command carried `git -C` with the literal path; none
  ran in the wrong repository.
- `truncated-read-taken-as-source-extent` seat, caught: the close runner's
  verdict lines on this PR's tree were first printed through `cut -c1-90`,
  with their count (5) beside them. The output file was read again whole
  before the block recorded the verdicts, and they matched.
- The marker hook, class id not known: the sign-off's first edit, after the
  read of 06:25:22Z, drew "Widen the marker so concurrent sessions can see
  this work. Run from <hub>: node scripts/fleet-view.mjs --declare tigerfid"
  (the hub's path written here as `<hub>`), on a marker opened with
  `--repos tigerfid`, the face Sprint 3's reviewer pass recorded on its legs
  2 and 3. The command ran and printed "marker declared: … repos: tigerfid"
  before the read of 06:26:05Z. The proposal's earlier edits, between the
  reads of 00:42:54Z and 00:48:58Z, drew no such line.
- `semicolon-chained-checker-does-not-gate-commit` seat, the guard's
  refusal: the sign-off's first commit command ran `close-lint` and `npm
  test` in an `&&` chain to `git commit`, then an `echo` after a `;`, then
  `git push`. The guard refused it before it ran: "piped-checker guard
  (BLOCKED — this command did not run)". It was re-issued as one `&&` chain
  to the push, with the exit read after it.
