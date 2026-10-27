# Friction log — Sprint 2, "The study makes its choices"

This log records what got in the way while Sprint 2 was proposed and run:
the friction itself, what it cost, and what would prevent it. It is public,
like everything in this repo, and written for a stranger. The sprint block
is in `docs/project-plan.md` (§ Sprint 2) while the sprint is live, and in
`docs/archives/project-plan-completed.md` once it has run and been reviewed.

Each leg appends one section with:
- its context checkpoint: the leg plan's task count, and how many
  compactions, forced re-reads and lost-context defects it met;
- each friction, with its cost and its suggested fix.

## The proposal (2026-10-27)

Proposed by the repo-rooted session "tigerfid Sprint 2 Proposal" (session
`49c256bc-78bf-445e-b2a6-f970dfb1f707`), fresh by `/clear`, in one sitting.

- **F1 — A review finding's example covered one of three places.** Finding 4
  of the Sprint 1 review named one stale sentence: the Outlook message
  "stays closed until Omid rules on it" in `docs/study/original-system.md`.
  A grep for the finding's predicate, every place that still says the message
  waits on ASK-OMID A1, found two more: `docs/reference-research.md` § 1 and
  `docs/reference-repos.md` reading rule 7. Story 2.1.9 carries all three and
  bakes the grep in, so the leg fixes the predicate, not the example. Cost: none
  landed wrong, because the proposal ran the grep before writing the story.
- **F2 — A manifest check asked for something its epic cannot produce.**
  `free-tools-three-platforms` in `docs/close/2.1.json`, born at adoption,
  asked the closer to confirm "the chosen boards are drawn in KiCad 10". The
  study draws no board: drawing is the build initiatives' work, filed by the
  epic's close. Read cold, the check would have failed at the close, or left a
  closer to invent its own reading. The proposal's staleness sweep caught it,
  and it now asks that every board the study plans is planned in KiCad 10.
  Suggested fix, for whoever writes the next study manifest: read each prose
  check against the epic's own Done-when, and ask whether the epic's stories
  can produce what the check names.
- **F3 — The proposal standard is long.** `sprint-proposal-standard` runs to
  4,338 lines, and its friction log to 2,250. The proposal read its steps 0–8
  and the log's newest entries, and modelled the block on the newest approved
  block in a sibling repo, rather than reading every clause. Cost: reading
  time, and the risk that a clause the proposal skipped binds this block. The
  reviewer pass is the check on that risk.
- **F4 — The sprint-block parser read the working-tree plan as having no
  block.** The fleet's parser printed `found: false` over
  `docs/project-plan.md` as checked out, with the block's heading in place.
  - **Cause, MEASURED:** this repo has no `.gitattributes`, and Git for
    Windows' default `core.autocrlf=true` checks text out with CRLF line
    endings. The parser's heading pattern does not match a line that ends in
    a carriage return.
  - **The workaround:** the proposal parsed the staged blob instead, the LF
    text that lands, and read all four legs.
  - **The wider gap:** the fleet's other repos pin LF line endings in a
    `.gitattributes` file, so this repo is the one without it. Filed to the
    hub as inbox #4452: tigerfid's missing pin, the adoption standard's
    missing row for it, and the parser's missing normalisation.
  - **Cost:** a few minutes, and a first reading that the heading was wrong.
- **Guard events, each caught before it changed anything.**
  - The cwd-drift guard warned twice on a `cd` out of this repo, once for a
    read of a sibling clone at `origin/main`, once for a loop over four
    siblings' `.gitattributes`.
  - The piped-checker guard warned once on a `$?` read after a grep, which
    was not used as proof.

## Leg 1 — Story 2.1.9 (2026-10-27)

Run by the repo-rooted session "tigerfid Sprint 2 Executor" (session
`014c145a-45db-4c1d-b7ff-a2e9bcbcb0c4`), fresh by `/clear`. Its
background-task path id, `c59c545c-e735-4344-b0b8-697d9f897dca`, is the
process the Sprint 2 proposal used last. Not elevated. Before the leg, the
same session landed the line-ending pin as its own PR (#28), on Air Traffic
Control's orders.

**Context checkpoint:** a plan of six tasks (the three files, the greps,
the tick with its manifest line and this section, the checks and the merge),
under D9's tripwire of eight. No compaction, no forced re-read, no
lost-context defect.

- **F5 — F4's cause, fixed before the leg.** #28 adds a root
  `.gitattributes` (LF stored and checked out). Every blob was LF already, so
  it changed no stored byte. This clone's 52 CRLF working files were repaired
  by the delete-and-`checkout-index -f -u` recipe, never a renormalize. Over
  the repaired working tree, the sprint parser now reads the block,
  `found: true`, four legs, exit 0. Cost: one small PR before the leg.
- **F6 — An order's literal scope would have rebuilt a known trap.** The
  orders asked that "the originals' tree" be marked `-text`. Read literally,
  that names the tag's 2018 folders, `software/` among them, and the
  rebuild's starting layout has a `software/` of its own. A rule on those
  names would have caught the rebuild's files, the same way the retired
  sparse view hid them. The pin marks only the 2018 files still on `main`
  (`license` and `_assets/`). It was measured that a `.gitattributes` on
  `main` cannot reach the tag: `git archive` of the tag ignored it. #28's body
  records the reading. Cost: none landed wrong.
- **F7 — An edit missed on a re-wrapped line.** One edit's search text began
  mid-line where the file wraps differently, so it matched nothing. Re-cut
  from the line's start, it applied. Cost: one retry.
- **Guard events, each a read and not a proof.** The piped-checker guard
  warned on a `git pull` piped into `tail` and on a re-verify read that piped
  `npm test` into `grep` after a `;`-chained `close-lint`. Each state was
  re-read unpiped, and every commit ran behind `close-lint && npm test &&`.
