# Session Notes

Cross-machine handoff notes so any Claude Code session can pick up where the
last one stopped. Newest session first; only the 3 most recent sessions are
kept (older ones are trimmed by `scripts/wrap-session.mjs` — full history
lives in git). Written at wrap-up per the "Session handoff" section of
CLAUDE.md. These notes are public, like everything in this repo (CLAUDE.md
§ Writing in public).

## Session: 2026-10-27 — Story 2.1.2: how the original worked (Sprint 1 leg 5); Sprint 1 EXECUTED

- **Landed:**
  - #16 is Story 2.1.2 (`d476e556`). It holds `docs/study/original-system.md`,
    the plan's tick, the 2.1 manifest's `reviewedFor` entry and a pointer in
    the research log's §1.
  - #17 stamps Sprint 1 EXECUTED with the five legs' PRs and shas
    (`3da6253b`, auto-merged at 12:55:50Z). **Review:** reads PENDING.
- **Next:**
  - The Sprint 1 reviewer pass, in a fresh session (KB
    `sprint-proposal-standard` § Roles).
  - Story 1.1.6, the Epic 1.1 close, is not this terminal's. Air Traffic
    Control released it to the Mac's hub closer, who opens a tigerfid-rooted
    terminal for it on Omid's hand.
  - Stories 2.1.3 and 2.1.4 have no sprint yet; Sprint 2 needs proposing.
- **Surprises / decisions:**
  - **Three corrections to the onboarding spec's Context and Appendix B**,
    read from the records at the tag. The study document records them; the
    hub spec itself is unchanged (it is the hub's file):
    - the compactTag is Tag 102; Tag 103 is the miniTag;
    - the Reader's radio is the TRX-101 daughterboard;
    - the 2012 sealTag is an ISO 15693 logging seal, with no active radio
      and no temperature sensor.
  - **So milestone 1's sealTag is a new design.** Story 2.1.4 weighs it and
    Story 2.1.7 decides it (the document's last section).
  - **No record states the radio's data rate.** The requirements'
    250 kbit/s stays ASSUMED for Story 2.1.3 to replace.
  - **Forks cannot write files.** The harness refused their Write to the
    scratchpad ("Subagents should return findings as text"), so the three
    research forks returned their findings in their replies. Every quote the
    document uses was then checked against its source by a script.
  - **`gh pr merge --auto` raced the checks on #16.** Armed right after the
    push, it failed with "2 of 2 required status checks are expected". Once
    the checks were green, `--auto` refused a PR "in clean status", and a
    direct `--squash --match-head-commit` merged it. MEASURED: both errors.
    INFERRED cause: the call landed before the new head's checks registered.
  - **The fresh-session read:** session
    `c40e0637-2dc0-4ae7-bea5-428bee160ea7`. Its background-task path id was
    `c59c545c-e735-4344-b0b8-697d9f897dca`, leg 4's process, so a `/clear`
    came between the two. Elevation was not read on win32.

## Session: 2026-10-27 — Story 2.1.1: the requirements, with numbers (Sprint 1 leg 4)

- **Landed:** #14 is Story 2.1.1. The record is the plan's tick and
  `docs/study/requirements.md`: 27 numbers, each with Omid's answer
  recorded first-hand. He accepted 24 as recommended and set three himself:
  [3], [12] and [13].
- **Next:**
  - Sprint 1 leg 5, Story 2.1.2 (`docs/study/original-system.md`), by
    Omid's `/clear` in this terminal. Its tick meets the same lockstep
    check as #14: one `reviewedFor` entry in `docs/close/2.1.json` plus
    the `reviewed` date, in the form #14 used.
  - Story 1.1.6, the Epic 1.1 close, in a fresh session.
- **Surprises / decisions:**
  - **Omid raised the conference's throughput tenfold:** 1,000 tags/s per
    reader where the study recommended 100. By the document's arithmetic
    (INFERRED, not measured), the original's random-delay anti-collision
    cannot reach that. It needs a fast link (about 1 Mbit/s) with short
    replies and reader-assigned slots, or several channels per reader, at
    some cost in range. Story 2.1.3 weighs [3] against [2] (30 m, tag worn)
    first. If the radio cannot meet it, the number goes back to Omid with
    its cost, as the document's ground rule says.
  - **Story 2.1.2 should read the original's data rate.** The
    requirements' channel arithmetic assumes 250 kbit/s and leaves the
    original's real rate for 2.1.2 to read.
  - **The fresh-session read:** session `c59c545c-e735-4344-b0b8-697d9f897dca`.
    Its background-task path id was the same, so no `/clear` came before
    it, and it ran without elevation.

## Session: 2026-10-27 — Story 1.1.5: the second machine's clone and reference folder

- **Landed:** #11 is Story 1.1.5. The plan's tick and the PR body are the
  record: the second machine's verify run (291 of 291, exit 0) and its
  noreply identity read-back.
- **Next:**
  - Story 1.1.6, the Epic 1.1 close, in a fresh session. It waits on
    `build-inputs-name-notices` going green, which happens when the fleet's
    sync round (hub Sprint 68) brings Story 1.1.4's hub companion (hub inbox
    #4410) here.
  - Sprint 1 legs 4–5, Stories 2.1.1 and 2.1.2. The corpus now verifies on
    both machines, so either one can run them.
- **Surprises / decisions:**
  - **A tick-only PR still touches its manifest.** `close-lint`'s lockstep
    check fails a diff that edits a story's lines but not
    `docs/close/<epic>.json`. Its suggested remedy, a `"reviewed"` date bump,
    does nothing when the field already reads today's date. #11 re-affirmed
    `1.1.json` with a `reviewedFor` entry in the form
    `Story X (PR #n, date): … No run, expect or prose changed.` Legs 4–5
    will meet the same check for `2.1.json`.
  - **The second machine's clone is partial,** as the plan asks:
    `--filter=blob:none`, full history, not sparse. Its pack is 758 MiB,
    because `--recreate` fetched the 2018 blobs.
  - **The fresh-session read:** session `54985f54-8bd1-4ecb-8f31-b4a125beafb4`.
    Its background-task path id was the same, so no `/clear` came before it.
    It ran without elevation.

