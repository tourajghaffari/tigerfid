# Session Notes

Cross-machine handoff notes so any Claude Code session can pick up where the
last one stopped. Newest session first; only the 3 most recent sessions are
kept (older ones are trimmed by `scripts/wrap-session.mjs` — full history
lives in git). Written at wrap-up per the "Session handoff" section of
CLAUDE.md. These notes are public, like everything in this repo (CLAUDE.md
§ Writing in public).

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

## Session: 2026-10-30 — Story 2.1.18 reviewed (PASS WITH FINDINGS); Story 2.1.20 filed

- **Landed:**
  - #89 → `e0fae75d` (GitHub's merge stamp 22:40:34Z), squash with
    `--match-head-commit ff0a9d61…` once `watch-pr.mjs` read it GREEN.
    - Story 2.1.18 carries its `**Reviewed:**` record: PASS WITH FINDINGS.
      #85's 48 figures reproduce from the studies' inputs, no requirement
      number or printed figure moved, and its greps reproduce. It writes
      the rebuild spec's 3b and 3c into the study as § 5.3 states them.
    - Story 2.1.20 is filed with the review's Minor H, and the manifest
      gains its `reviewedFor` line, `2.1.20` in `stories` and the prose
      check `story2118-review-fixes`.
- **Next:**
  1. Story 2.1.20, before Story 2.1.8: what starts the wide search, and
     which network a carried tag joins. On the study's own budget a tag
     carried one or two bays past its Reader's edge likely still hears its
     old Reader's beacon but none of its Field Generators, so it stays
     unplaced and the wide search never runs (INFERRED).
  2. The Story 2.1.19 review (Mac Reviewer's next order).
  3. Story 2.1.8, a FRESH closer, once Stories 2.1.19 and 2.1.20 are done
     and reviewed.
- **Surprises / decisions:**
  - **This pass filed Story 2.1.18** (as the Story 2.1.17 review) and
    reviewed it, so it held #85 to the written Done-when and § 5.3 and
    re-measured every figure.
  - **The register seats** (#85's four wrapped hits, each correct; this
    pass's one, a reading `node -e` that also wrote the manifest, caught
    before the commit) go to the coordinator as text in #89's body.
  - **The merge word:** Omid, first-hand in this terminal between its reads
    of 20:17:04Z and 20:20:02Z: "Please merge any PRs you open during this
    session when ready and in the right order."
- **Provenance:** Session id `86ac7523-9bad-4199-8c93-a5a8b64ce65b`, "Mac
  Reviewer", a reviewer seat on the fleet coordinator's orders, a fresh
  launch: its background-task path id equals its session id. Not elevated.
  It did none of Story 2.1.18's work.

