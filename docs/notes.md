# Session Notes

Cross-machine handoff notes so any Claude Code session can pick up where the
last one stopped. Newest session first; only the 3 most recent sessions are
kept (older ones are trimmed by `scripts/wrap-session.mjs` — full history
lives in git). Written at wrap-up per the "Session handoff" section of
CLAUDE.md. These notes are public, like everything in this repo (CLAUDE.md
§ Writing in public).

## Session: 2026-10-30 — Story 2.1.16: the EU edge's channels, the inventory's wait and the hall's second Reader

- **Landed:**
  - #72 → `f59888ff` (GitHub's merge stamp 12:15:14Z), squash with
    `--match-head-commit 807ab911…` once `watch-pr.mjs` read it GREEN.
    - Story 2.1.16 is ticked, and the manifest gains its `reviewedFor` line.
      The Story 2.1.15 review's three Minors and three nits are corrected in
      `radio-and-location.md` and `reference-research.md`. No requirement
      number moved, and no printed figure changed: each figure added
      reproduces from the studies' inputs by a script.
    - The sitting had not run, so the study writes its own answers to Minors
      A and C. In the EU, Readers beside each other take different channels,
      four of the five in a 2 × 2 pattern. [2]'s second Reader in the hall
      listens on the first Reader's network, so the hall stays one block and
      [9] keeps its 22 %.
- **Next:**
  1. Story 2.1.7, Omid's sitting. Story 2.1.16 moved no figure it reads. It
     gains two answers to confirm or overrule in the rebuild spec: the EU
     channel rule (Minor A) and the hall's second Reader (Minor C).
  2. A review of Story 2.1.16 by a FRESH seat, as Story 2.1.15 had, if the
     coordinator orders one.
  3. Story 2.1.8, Epic 2.1's close, by a FRESH closer.
- **Surprises / decisions:**
  - **What declines the hall's own network** is a cost the review did not
    name. A worn badge that crosses a Reader's edge in the hall rejoins the
    other Reader each time, up to one hop cycle in the US: 61.7 mAh a year
    at ten crossings a day (ASSUMED), 0.81 years even with six more Field
    Generators. The review's 9 % margin beside the edge is the second
    reason.
  - **A lost tag in the EU** must now try the other channels, one per
    search: four searches, about 15 minutes. This follows from Minor A's
    rule, and the study says so.
  - **The hub's Item-8 check read this PR** because its grep section uses
    the check's heading: 29 dispositions, 14 current, none named.
- **Provenance:** Session id `fbb2b4f5-d3b5-43b1-a0d4-180396d4e44f`, a
  hub-rooted executor seat, freshly launched: its background-task path id
  equals its session id. Not elevated. The merge word is Omid's in-session
  delegation, relayed by the coordinator's orders: "Please merge any PRs
  you open during this session when ready and in the right order."

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

