# Session Notes

Cross-machine handoff notes so any Claude Code session can pick up where the
last one stopped. Newest session first; only the 3 most recent sessions are
kept (older ones are trimmed by `scripts/wrap-session.mjs` — full history
lives in git). Written at wrap-up per the "Session handoff" section of
CLAUDE.md. These notes are public, like everything in this repo (CLAUDE.md
§ Writing in public).

## Session: 2026-10-31 — Windows Closer: Epic 2.1 closed, Initiative 2 archived, the build filed as Initiatives 3–7

**The session:** "Windows Closer", `855e7ac0-8251-4a5c-8c32-b91a310b4834`, hub-rooted, fresh by `/clear`. Its presence marker and tombstone are on the hub's origin.

**Landed:** #98 → `f2bbcb31` (GitHub's merge stamp 09:47:24Z), squash with `--match-head-commit 925d5b0b…` once `watch-pr.mjs` read it GREEN. It is Story 2.1.8, initiative-final. The evidence is in the archive under Story 2.1.8; Initiatives 3–7 are the plan's live work.

**Next, read first:**
- **The build's order.** Story 3.1.1 (the bench) comes first. Gate G1 (Story 3.1.2) holds every board drawing until TI answers in writing. The firmware (Initiative 4) and the host (Initiative 5) may start on the development board and recorded frames meanwhile.
- **No sprint is proposed yet.** The next proposer assembles Sprint 5 from Initiatives 3–7 per hub KB `sprint-proposal-standard`.

**Facts not in the plan, or recorded only here:**
- **G1's question is posted** on TI's E2E forum under part number CC1312PSIP: "I posted it on E2E with CC1312PSIP" [Omid's words as relayed by the session "Windows 5", which had them first-hand before its `date -u` read of 22:41:19Z on 2026-10-30]. ti.com/contact lists no e-mail route for design questions [the same session's read, relayed]. The post's link is not yet recorded; Story 3.1.2 records it. Story 3.1.2's Context carries the dated line.
- **The contribution policy.** The README's and `contributing.md`'s "pull requests are welcome once the design study has set the direction" now reads as met (spec D10). They are unchanged; whether to say so plainly is Omid's call.
- **The research log** has no line for Story 2.1.7's FCC grant reads (the spec's § Sources records them by URL and SHA-256), nor for Stories 2.1.17 and 2.1.18, which read no outside source. No check asked for one.
- **Register seats.** The 2.1.17, 2.1.18 and 2.1.19 reviews' seats, and this close's four (all in decided classes, each caught), went to the fleet coordinator as text. The hub register at `1bc6cff9` did not yet hold them.

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

