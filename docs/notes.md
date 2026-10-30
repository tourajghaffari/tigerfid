# Session Notes

Cross-machine handoff notes so any Claude Code session can pick up where the
last one stopped. Newest session first; only the 3 most recent sessions are
kept (older ones are trimmed by `scripts/wrap-session.mjs` — full history
lives in git). Written at wrap-up per the "Session handoff" section of
CLAUDE.md. These notes are public, like everything in this repo (CLAUDE.md
§ Writing in public).

## Session: 2026-10-30 — Story 2.1.20 reviewed (PASS); Epic 2.1 close-ready

- **Landed:**
  - #95 → `2c954988` (GitHub's merge stamp 23:01:10Z), squash with
    `--match-head-commit 6e7a7863…` once `watch-pr.mjs` read it GREEN.
    - Story 2.1.20 carries its `**Reviewed:**` record: PASS. The wide
      search now starts on a whole-block listen that hears none of the
      network's Field Generators, beacon or not, and the tag joins the
      network whose Field Generator it hears. #93's figures, needle census
      and numbers census reproduce, and the manifest parses with 32
      `reviewedFor` entries, none empty.
- **Next:**
  1. Story 2.1.8, Epic 2.1's close, by a FRESH closer (Windows Closer):
     every story before it is done and reviewed.
- **Surprises / decisions:**
  - **One observation for the build initiatives** (in the record, not a
    finding): the two wide searches re-arm, by the cost bound's "for each
    carry", when the tag next hears a Field Generator, not at each
    whole-block listen.
  - **This pass filed Story 2.1.20** (as the Story 2.1.18 review) and
    reviewed it, so it held #93 to the written Done-when and its prose
    check, and re-measured every figure and count.
  - **The merge word:** Omid, first-hand in this terminal between its reads
    of 20:17:04Z and 20:20:02Z: "Please merge any PRs you open during this
    session when ready and in the right order."
- **Provenance:** Session id `86ac7523-9bad-4199-8c93-a5a8b64ce65b`, "Mac
  Reviewer", a reviewer seat on the fleet coordinator's orders, a fresh
  launch: its background-task path id equals its session id. Not elevated.
  It did none of Story 2.1.20's work.

## Session: 2026-10-30 — Story 2.1.20: the wide search starts when a tag hears none of its Field Generators (#93)

**(A hub-rooted executor session on the Mac, "Mac 3", session `7d281368-21e6-410c-982e-c3db4c0cac0d`, in a scratch worktree of
this clone. A `/clear` of the process `c5a37a49-…`, whose earlier contexts did other repos' work and none of Epic 2.1's. Seated
by the fleet coordinator's orders; Omid's merge sentence first-hand in this terminal: "Please merge PRs you open when ready and
in the right order.")**

- **In flight:** nothing. #93 merged → `c2eb62be73e2602ee44bd4fe878ad570c3d41cec` (2026-10-30T22:54:52Z, squashed pinned to head `5bfa3ac2` after CI run 41141155014 read green).
- **Read first, next session:** Story 2.1.20's tick in `docs/project-plan.md`, and `docs/study/radio-and-location.md` § Zones, A
  tag carried past a Reader's edge.
- **Next (pointers — re-verify after a fetch):** Story 2.1.8, Epic 2.1's close, by a fresh closer: every work story is ticked.
  Its prose check `story2118-review-fixes` reads #93's body for the item's grep.
- **Surprises / decisions:**
  - The fix touched two sentences beyond the trigger, since they described the old rule: the US wide listen "stops at the
    first beacon it hears" now stops at the first frame whose block carries a Field Generator it hears, and a tag neither wide
    search finds is split by whether it still hears a beacon ([25]'s case or not).
  - The same-frame join rests on one ASSUMED order: a frame's joining window follows the block, as § Anti-collision lists a
    frame's shares. Were it before, the join comes one frame (0.25 s) later, still inside [22]. The build measures the frame.
  - The review's Minor H priced the old beacon at 35 m (about 23 dB). The study takes the far end of one or two bays past the
    edge, 36.5 m (22.7 dB), so the figure holds for the whole span.

## Session: 2026-10-30 — Story 2.1.19 reviewed (PASS); the research log's entry for its reads

- **Landed:**
  - #91 → `a0526488` (GitHub's merge stamp 22:48:35Z), squash with
    `--match-head-commit a2307bb8…` once `watch-pr.mjs` read it GREEN.
    - Story 2.1.19 carries its `**Reviewed:**` record: PASS. #86's 32 EU
      figures reproduce from the US figures and the read prices, no US
      figure moved, the section's grep reproduces, and the manifest holds
      both #85's and #86's DONE lines.
    - One PATCH: `docs/reference-research.md` gains Story 2.1.19's § 4 entry
      and its § Searches already run line, written from the story's own
      read record (`tiered-cost.md` § Read on 2026-10-30, for the EU build).
- **Next:**
  1. Story 2.1.20 (Mac 3), the wide search's trigger, then its review.
  2. Story 2.1.8, a FRESH closer, once Story 2.1.20 is done and reviewed.
- **Surprises / decisions:**
  - **The research log lacks lines** for Stories 2.1.17 and 2.1.18 (each PR
    states no source read and no search run) and for Story 2.1.7's reads
    of the two FCC grants (the spec cites them by URL and SHA-256). An
    observation in the record for the close's prose checks, not a finding.
  - **This PR's number was fixed before its citation:** the PR opened as a
    draft after its first commit, so the record cites #91, not a guess.
  - **The merge word:** Omid, first-hand in this terminal between its reads
    of 20:17:04Z and 20:20:02Z: "Please merge any PRs you open during this
    session when ready and in the right order."
- **Provenance:** Session id `86ac7523-9bad-4199-8c93-a5a8b64ce65b`, "Mac
  Reviewer", a reviewer seat on the fleet coordinator's orders, a fresh
  launch: its background-task path id equals its session id. Not elevated.
  It did none of Story 2.1.19's work.

