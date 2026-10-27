# Session Notes

Cross-machine handoff notes so any Claude Code session can pick up where the
last one stopped. Newest session first; only the 3 most recent sessions are
kept (older ones are trimmed by `scripts/wrap-session.mjs` — full history
lives in git). Written at wrap-up per the "Session handoff" section of
CLAUDE.md. These notes are public, like everything in this repo (CLAUDE.md
§ Writing in public).

## Session: 2026-10-27 — the Sprint 1 reviewer pass (every leg PASS)

- **Landed:** both merged on Omid's in-session merge instruction, each pinned
  to its green head.
  - #23 → `a647e11b` (15:28:02Z). The archive's Sprint 1 `**Reviewed:**`
    record is the adjudication: every leg PASS, five findings. #23 also
    refreshes the two restated PENDING copies, adds one dated Context
    sentence to each of Stories 2.1.3, 2.1.4, 2.1.5 and 2.1.7, and corrects
    `docs/close/2.1.json`'s `rebuild-spec-approved` run line, with one
    `reviewedFor` entry.
  - #24 → `3c2cdbbf` (15:31:22Z) corrects the record's register-seats
    bullet. The Epic 1.1 close's two seats had landed in hub #4446 before
    #23 opened.
- **Next:**
  - The Sprint 2 proposal, on Omid's word, from Story 2.1.3. The Context
    sentences on 2.1.3–2.1.7 are its inputs.
  - Its first study PR also carries the review's fix-up to
    `docs/study/original-system.md` (findings 3–4 in the Sprint 1 record):
    the spec corrections are three, not two, and the Outlook-message
    sentence still waits on A1, which is answered.
- **Surprises / decisions:**
  - **A born tooth that could never pass.** `rebuild-spec-approved`'s regex
    failed to parse on every input (`SyntaxError`). MEASURED on fixtures
    under Git Bash. INFERRED: the same lost-backslash mechanism as 1.1's
    `build-inputs-name-notices`. A born-RED tooth hides this kind of defect,
    because its red reads as "not yet".
  - **A shared record moved during a wait.** Omid's answer on the #6–#8
    merges came between this terminal's `date -u` reads of 13:21:53Z and
    15:22:47Z. The hub register gained the close's two seats in between, so
    the record written from the first read was stale, and #24 corrects it.
  - **A CR check that misread.** On this machine, `grep -c $'\r'` over
    `git show` output counted every line, on `origin/main`'s files too. Only
    `git cat-file -p … | od -c` showed the blobs are LF. MEASURED.
  - **The fresh-session read:** session `f08270bf-1a7d-43fc-8c97-df28f66d8ebc`.
    Its background-task path id was `c59c545c-e735-4344-b0b8-697d9f897dca`,
    leg 4's process, which leg 5's session `c40e0637-…` also ran in. So a
    `/clear` came before it. It ran without elevation.

## Session: 2026-10-27 — Story 1.1.6: the Epic 1.1 close (Initiative 1 archived)

- **Landed:** #19 → `25c0f972` (13:02:01Z), merged on Omid's word, pinned
  to its green head. The archive is the record: Initiative 1 whole, with the
  Epic 1.1 close evidence and A1 beside it, and § Executed sprints › Sprint 1.
- **Next:**
  - The Sprint 1 reviewer pass, in a fresh session. The block now lives in
    the archive, not the plan. The review PR refreshes its `**Review:** PENDING`
    line and the Epic 1.1 evidence's "Sprint 1's review had not run" bullet;
    the PENDING line names both.
  - Sprint 2 needs proposing (Stories 2.1.3 onward).
- **Surprises / decisions:**
  - **The Sprint 1 block moved with this close.** Once #16 ticked leg 5, the
    close runner printed its RELOCATE banner: the legs span Epics 1.1 and
    2.1, every leg read ticked, so the first close owns the move (hub Story
    89.1.1). #17 had stamped the block in place. This close moved it whole, on
    Air Traffic Control's grant. Legs 4–5 (Stories 2.1.1, 2.1.2) stay ticked
    in the plan until Epic 2.1's close archives them.
  - **CLAUDE.md § CI corrected:** since #8, `npm test` runs four commands,
    not two.
  - **Story 1.1.3's tick has a dated correction.** The live site was frozen
    to static HTML at 20:04:41Z on 2026-10-26, before #6 opened. The capture
    predates the freeze, so its dating holds.
  - **The conduct alias works.** #7 required `conduct@activewaveinc.com` to
    exist before its merge, and no record showed it. Omid confirmed it at
    this close: "Yes, it works".
  - **A close's Completed line names its PR number once the PR is open.**
    `scope`'s `check-tick-merge-claim` refused "this PR" on #19's first head.
    A second commit named #19 and the pre-tick head.
  - **The wrap refused once, rightly.** #20 amended the Story 2.1.2 block on
    origin while this block was being written. The block was re-laid on
    origin's notes, never `--amend`ed over them.
  - **The fresh-session read:** session `ce689038-d1e9-483f-9350-492b8e1ff75f`.
    Its background-task path id was the same, so no `/clear` came before it.
    It ran without elevation.

## Session: 2026-10-27 — Story 2.1.2: how the original worked (Sprint 1 leg 5); Sprint 1 EXECUTED

- **Landed:**
  - #16 is Story 2.1.2 (`d476e556`). It holds `docs/study/original-system.md`,
    the plan's tick, the 2.1 manifest's `reviewedFor` entry and a pointer in
    the research log's §1.
  - #17 stamps Sprint 1 EXECUTED with the five legs' PRs and shas
    (`3da6253b`, auto-merged at 12:55:50Z). **Review:** reads PENDING.
  - Hub #4436 (`62066244`, auto-merged at 13:00:04Z) is the inbox fragment
    `docs/inbox/tigerfid-onboarding-spec-corrections.md`, filed on Omid's
    go. It asks a hub seat for a dated correction note in the onboarding
    spec, covering the three corrections below. The hub seat that triages
    it does the work; tigerfid needs no change.
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
    A PR reading `CLEAN` with auto-merge armed but still OPEN is only lag.
    MEASURED on hub #4436: auto-merge fired at 13:00:04Z, and a direct merge
    sent seconds later answered "already merged".
  - **The fresh-session read:** session
    `c40e0637-2dc0-4ae7-bea5-428bee160ea7`. Its background-task path id was
    `c59c545c-e735-4344-b0b8-697d9f897dca`, leg 4's process, so a `/clear`
    came between the two. Elevation was not read on win32.

