# Session Notes

Cross-machine handoff notes so any Claude Code session can pick up where the
last one stopped. Newest session first; only the 3 most recent sessions are
kept (older ones are trimmed by `scripts/wrap-session.mjs` — full history
lives in git). Written at wrap-up per the "Session handoff" section of
CLAUDE.md. These notes are public, like everything in this repo (CLAUDE.md
§ Writing in public).

## Session: 2026-10-29 — Sprint 4 leg 1 (Story 2.1.12, the listening window)

- **Landed:** #58 → `cf580b05` (GitHub's merge stamp 10:05:48Z), squash
  with `--match-head-commit 64c09fb0…` once `watch-pr.mjs` read it GREEN
  (07:50:38Z).
  - The radio study states the layout of a listen: the beacon skipped on
    most listens. Every battery figure is re-priced, and Sprint 3's D2 is
    re-run: [18] by 24 %, [9] by 22 %, [27] by 22 % and 11 % on cold
    chain. All are met on paper, and no ASK-OMID row is filed.
  - The conference's block is computed (1,308 reply slots a second), and
    GNU make is named among the tools not cited (D4's default).
  - The tick, the manifest's `reviewedFor` line and the friction log's
    leg 1 section ride the same PR.
- **Next:**
  1. **Sprint 4 leg 2, Story 2.1.14,** in a FRESH executor seat, by the
     block's kickoff. It reads `tiered-cost.md` anchors at `cf580b05`.
     `tiered-cost.md` § Both cells needs no sentence moved (#58's body,
     § For leg 2).
  2. Then leg 3 (Story 2.1.13, merged by Omid's word), the Sprint 4
     review, Story 2.1.7's sitting and Story 2.1.8's close.
- **Surprises / decisions:**
  - **The classifier refused the relayed merge delegation** ("Merge
    Without Review"), before the read of 07:51:08Z. Omid answered
    first-hand in this terminal, "Merge #58 (Recommended)", between that
    read and 10:05:45Z. He then gave this session its own delegation:
    "Please merge any PRs you open during this session when ready and in
    the right order."
  - **For the reviewer:** the block's order was a second hidden
    assumption (friction L1); reproduce slot counts at 11/15 ms (L2). The
    leg's script is in #58's body.
- **Provenance:** Session id `6fb033a2-2fc2-4f7a-8d61-8918850b976e`, the
  hub-rooted executor seat "tigerfid Sprint 4 leg 1", fresh by `/clear` of
  "Windows 4"'s process, which last held the Sprint 4 proposal (the friction
  log's fresh-session read).

## Session: 2026-10-29 — Sprint 4 proposal (approved, three legs; Story 2.1.12 split, Story 2.1.14 born)

- **Landed:** #55 → `bfead197` (GitHub's merge stamp 06:27:28Z), squash by
  auto-merge armed with `--match-head-commit 432aaa00…` once `watch-pr.mjs`
  read it GREEN.
  - `docs/project-plan.md` § Sprint 4, three legs, serial: 2.1.12 (the
    listening window), 2.1.14 (the tiers) and 2.1.13 (the folder's
    `sources/` route). Omid approved it first-hand in this terminal, between
    its `date -u` reads of 00:50:57Z and 06:25:22Z: "Approve as proposed
    (Recommended)".
  - Story 2.1.12 is narrowed and Story 2.1.14 born, with
    `docs/close/2.1.json` in lockstep (`sprint3-review-tier-fixes` born).
  - The sprint's friction log, `docs/friction-log-sprint4-figures-that-hold.md`,
    has § The proposal (F1–F4 and the guard events).
- **Next:**
  1. **Sprint 4 leg 1, Story 2.1.12,** in a FRESH executor seat, by the
     block's kickoff. D2's clause for a baby's band may need Omid's
     first-hand word in that leg's terminal.
  2. Then leg 2 (Story 2.1.14) and leg 3 (Story 2.1.13, merged by Omid's
     word). After them come the Sprint 4 review, Story 2.1.7's sitting and
     Story 2.1.8's close.
- **Surprises / decisions:**
  - **The split.** As filed, Story 2.1.12 would plan ten or eleven tasks,
    above the eight-task tripwire. It was split along its own two clauses
    by the session-fit test (the block's § The sprint's shape).
  - **`main` moved while the approval waited:** #56, a fleet sync round,
    merged into the branch (F4).
  - **The hub checkout's sprint parser is behind origin/main.** The
    proposal ran origin/main's copy from its scratchpad (F1).
- **Provenance:** Session id `cb189c7f-0ffb-4dea-bea2-8485d49ee215`, the
  hub-rooted proposer seat "tigerfid Sprint 4 proposal", fresh by `/clear` of
  "Windows 4"'s process (the friction log's fresh-session read).

## Session: 2026-10-29 — Sprint 3 review (PASS WITH FINDINGS; Stories 2.1.12 and 2.1.13 filed)

- **Landed:** #53 → `5570ecee` (GitHub's merge stamp 00:14:32Z), squash on
  green, `--match-head-commit 8f6f15a0…`.
  - The Sprint 3 block is in the archive's § Executed sprints, with its
    `**Reviewed:**` record.
  - The plan files Stories 2.1.12 and 2.1.13. `docs/close/2.1.json` carries
    both, with the new tooth `stub-names-sources` (born RED) and two prose
    checks.
  - The friction log has § Reviewer pass (R1–R5).
- **Next:**
  1. **Story 2.1.12, the Sprint 3 review's fixes to the studies,** before
     Omid's sitting: a Sprint 4 proposal, in a FRESH seat.
     - Its finding 1, the listening window, moves the [9], [18] and [27]
       margins that Sprint 3's D2 judged.
     - D2 may need Omid's word if no route meets [18] in a baby's band.
  2. Then Story 2.1.7's sitting (Omid's), and Story 2.1.8, Epic 2.1's close
     (a FRESH closer). Story 2.1.13, the reference folder's `sources/` route,
     touches `scripts/` and goes any time before the close.
- **Surprises / decisions:**
  - **The sitting waits for Story 2.1.12, by the review's routing.** Omid
    may sit first. The cost: he would decide the cells on margins that do
    not hold as printed.
  - **The register seats went to the Mac coordinator as text** (the friction
    log's § Reviewer pass). Until a pen seats them as the reviewer pass's,
    the close runner prints "REVIEW SEATING MISSING" for Sprint 3. Its
    Sprint 2 line is a stale read of the hub checkout (R5).
  - **The hub's Item-8 reader reads a table as empty** (R3). A reviewer
    reads a table-form section by eye.
  - **For this repo's logs, `check-register-seats.mjs` needs
    `--sprint 'tigerfid — Sprint N'`** (R4).
- **Provenance:** Session id `f050a5d6-6c6f-403f-878d-a9c23140108a`, the
  hub-rooted reviewer seat "tigerfid Sprint 3 review", fresh by `/clear` of
  "Windows 4"'s process (the friction log's fresh-session read).

