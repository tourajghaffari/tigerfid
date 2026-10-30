# Session Notes

Cross-machine handoff notes so any Claude Code session can pick up where the
last one stopped. Newest session first; only the 3 most recent sessions are
kept (older ones are trimmed by `scripts/wrap-session.mjs` — full history
lives in git). Written at wrap-up per the "Session handoff" section of
CLAUDE.md. These notes are public, like everything in this repo (CLAUDE.md
§ Writing in public).

## Session: 2026-10-30 — Story 2.1.15 reviewed (PASS WITH FINDINGS); Story 2.1.16 filed

- **Landed:**
  - #69 → `0baa9e4f` (GitHub's merge stamp 09:34:37Z), squash with
    `--match-head-commit ba9ae489…` once `watch-pr.mjs` read it GREEN.
    - Story 2.1.15 carries its `**Reviewed:**` record: PASS WITH FINDINGS,
      none Important. One script rebuilt 110 figures #67 prints from the
      studies' inputs, and none differs. #67's fourteen greps reproduce, and
      R&L's and TI's pages read as the studies quote them.
    - Story 2.1.16 is filed, anywhere before 2.1.8: three Minors (the EU's
      channel at a Reader's edge; [21] against the count's 5-minute wait;
      the conference hall's second Reader's network) and three nits. The
      manifest gains `2.1.16` and the prose check `story2115-review-fixes`.
- **Next:**
  1. Story 2.1.7, Omid's sitting. Nothing in this review holds it: no
     finding moves a margin it chooses a cell from.
  2. Story 2.1.16 (a fresh executor), anywhere before 2.1.8. If the sitting
     decides Minor A or C in the rebuild spec first, the story writes the
     spec's answer into the study.
  3. Story 2.1.8, a FRESH closer, who also adjudicates GNU make's INFERRED
     chain under `three-platform-claims-sourced`.
- **Surprises / decisions:**
  - **The record's place:** 2.1.15 ran as a lone story, with no sprint
    block, so its `**Reviewed:**` record sits under its Outcome in the plan.
    The epic close cuts it with the story.
  - **The hub's Item-8 check read nothing on #67.** #67's grep section is
    titled "Each item's grep", not "Item-8 anchor grep", so this pass read
    the dispositions by hand. A PR that wants the check to read it uses the
    check's heading.
  - **The register seats** went as text in #69's body: #67's four and this
    pass's four. This wrap's own `cd` into the clone, which the wrap needs,
    draws the cwd-drift arm once more.
  - **The merge word:** Omid, first-hand in this terminal between its reads
    of 09:09:46Z and 09:18:03Z: "Please merge PRs you open during this
    session when ready and in the right order."
- **Provenance:** Session id `26c5a341-81ee-4645-b03a-779448772d5d`, the
  hub-rooted reviewer seat "Mac 2", fresh by `/clear`: its background-task
  path id, `8cac88a9-…`, is an earlier Mac session's, which had wrapped. Not
  elevated.

## Session: 2026-10-30 — Story 2.1.15: the warehouse at a Reader's edge, and the figures beside it

- **Landed:**
  - #67 → `ce56ceeb` (GitHub's merge stamp 08:46:54Z), squash with
    `--match-head-commit c8a9d558…` once `watch-pr.mjs` read it GREEN.
    Story 2.1.15 is ticked; the manifest gains its `reviewedFor` line and
    `reviewed` reads 2026-10-30.
    - Finding 1: a second Field Generator in the bay across each Reader's
      edge follows the near Reader's network, so [27] holds at every bay:
      1.21 and 1.10 of five years on a CR2032 at 12 s, with the count's
      wakes priced at their bound (one step in 5 minutes). The route
      without new hardware is a second wake every fifth listen (1.16 and
      1.05).
- **Next:**
  1. The review of Story 2.1.15 (Mac Reviewer's, a FRESH pass), then
     Story 2.1.7, Omid's sitting, then Story 2.1.8, a FRESH closer, who also
     adjudicates GNU make's INFERRED chain under
     `three-platform-claims-sourced`.
  2. The close runner prints "REVIEW SEATING MISSING" for Sprint 4 until
     the coordinator's register fold lands. The seats went as text in #65's
     and #67's bodies.
- **Surprises / decisions:**
  - **The 2026-10-29 note's Next 3 is closed, MEASURED.** The Mac's
    reference check exited 1 (`README stale-stub`) between this terminal's
    reads of 08:42:43Z and 08:47:00Z. `--write-stub` (exit 0) and the
    re-check (exit 0, "299 rows verified, README the stub") ran between
    08:47:15Z and 08:47:22Z, on the coordinator's GRANT.
  - **Declined in the study: one Field Generator in both Readers' blocks.**
    Two unsynchronised frames slide past each other, and two crystals that
    agree closely keep its two slots overlapping for as long as they agree,
    so nothing bounds the loss. It was this seat's first pick, caught on
    re-reading the slide arithmetic before the first commit.
  - **§ [27] counts 365-day years;** the Sprint 4 review's edge figures
    counted 365.25. A script that reproduced the printed table (22 of 24
    cells exactly) set the convention before any new figure was written.
  - **The merge word:** Omid, first-hand in this terminal between its reads
    of 08:31:29Z and 08:42:43Z: "Yes, push/edit/ready/merge (Recommended)".
  - **The guard events** went as text in #67's body: two
    `piped-checker-exit` warnings, the cwd-drift arm, and a needle split by
    a line wrap.
  - **The orders' wrap path:** `node ../../scripts/wrap-session.mjs` (the
    hub's copy) refuses from the clone, exit 1, and names the clone's own
    `scripts/wrap-session.mjs`, which this wrap ran.
- **Provenance:** Session id `ba5a50fc-7f22-4257-944a-a4b3b875897c`, the
  hub-rooted executor seat "Mac 3", fresh by launch: its background-task
  path id equals the session id. Not elevated.

## Session: 2026-10-29 — Sprint 4 reviewed (PASS WITH FINDINGS); Story 2.1.15 filed

- **Landed:**
  - #65 → `9940597e` (GitHub's merge stamp 00:00:55Z, 2026-10-30), squash
    with `--match-head-commit 82e2ca46…` once `watch-pr.mjs` read it GREEN.
    - The Sprint 4 block is cut whole to the archive's § Executed sprints,
      with its `**Reviewed:**` record: PASS WITH FINDINGS, each leg MET.
    - Story 2.1.15 is filed ahead of Story 2.1.7. It carries finding 1,
      Important: the warehouse's "one run of three" does not hold at a
      Reader's edge. INFERRED: a CR2032 lasts 0.92 of five years there, and
      the cold-chain tag 0.85, while the recommended CR2450 still meets
      [27]. It also carries four Minors and five nits.
    - The manifest gains `2.1.15` and the prose check
      `sprint4-review-fixes`.
- **Next:**
  1. **A Sprint 5 proposal for Story 2.1.15** (a FRESH proposer), then its
     leg and its review. Omid may sit first, at the cost the record names:
     choosing the item tag's cell from a CR2032 margin that does not hold at
     a Reader's edge.
  2. Story 2.1.7, Omid's sitting; then Story 2.1.8, a FRESH closer, who
     also adjudicates GNU make's INFERRED chain under
     `three-platform-claims-sourced` (the record's note for the closer).
  3. **The Mac's reference folder** still reads `README stale-stub`. MEASURED
     by this pass: it is byte for byte the stub before leg 3. The next
     tigerfid session on the Mac runs `node scripts/reference-check.mjs
     --write-stub` (D13; the friction log's V1).
- **Surprises / decisions:**
  - **The register seats** of legs 2 and 3, and this pass's own, went as
    text in #65's body for the coordinator's fold. Until that fold lands,
    the close runner prints "REVIEW SEATING MISSING" for Sprint 4.
  - **The merge word:** Omid, first-hand in this terminal between its reads
    of 23:36:35Z and 23:40:19Z: "Yes, push/edit/ready/merge
    (Recommended)". The merge did not hold.
- **Provenance:** Session id `6c5fdfae-3e60-414d-bac8-80781e57d20e`, the
  hub-rooted reviewer seat "Mac 3", fresh by `/clear` of the process
  `5c057329-…` (the friction log's fresh-session read).

