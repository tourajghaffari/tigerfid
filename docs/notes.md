# Session Notes

Cross-machine handoff notes so any Claude Code session can pick up where the
last one stopped. Newest session first; only the 3 most recent sessions are
kept (older ones are trimmed by `scripts/wrap-session.mjs` — full history
lives in git). Written at wrap-up per the "Session handoff" section of
CLAUDE.md. These notes are public, like everything in this repo (CLAUDE.md
§ Writing in public).

## Session: 2026-10-28 — Sprint 3 leg 2 (Story 2.1.6, the tiered cost)

- **Landed:** #48 → `3d27bb01` (GitHub's merge stamp 21:39:52Z), squash on
  green, `--match-head-commit 208db34e…`. `docs/study/tiered-cost.md` is the
  state: Story 2.1.6 is ticked in the plan, `docs/close/2.1.json` carries its
  `reviewedFor` line, and the friction log's § Leg 2 holds F12–F16 and the
  fresh-session read.
- **Next:**
  1. **Leg 3, Story 2.1.11**, the reference check's `sources/` message, in a
     FRESH executor seat. It touches `scripts/` and merges by Omid's word.
     As the last leg, it stamps the sprint EXECUTED.
  2. Then the Sprint 3 review, Story 2.1.7's sitting and Epic 2.1's close.
- **Surprises / decisions:**
  - **Five prices are not read, by Omid's word.** First-hand in this
    terminal, between its `date -u` reads of 14:42:21Z and 15:59:59Z: "Write
    all as not read (Recommended)". They are the W5017 whips, JLCPCB's
    boards, the module's sourcing at JLCPCB, the pro iron and the tag
    enclosures. Every total names them, and the sitting may read them in a
    browser.
  - **Eight sellers refuse a plain request** (MEASURED, HTTP 403): Digi-Key,
    Mouser, Arrow, Newark, Hakko USA, Jameco, Chip Quik and JBC. LCSC
    answered a quick run with a challenge page, and every page at 15 s
    apart. A later price read can start from those two facts (friction F12,
    F14).
  - **The relayed merge delegation was refused again; a first-hand word
    passed.** The auto-mode classifier refused `gh pr merge` [Merge Without
    Review] at the 16:14:10Z read. The leg held and told Air Traffic Control.
    Omid answered "Merge it now (Recommended)" between this terminal's reads
    of 16:14:30Z and 21:39:50Z, and the same command merged. MEASURED twice
    now (legs 1 and 2), so leg 3's Omid-word merge needs his line in its own
    terminal.
  - **The bench's receiver is the tinySA.** RTL-SDR Blog's store page no
    longer lists the plain V4, and the V4L's page prints no upper range
    (F15).
- **Provenance:** Session id `293fce01-04ef-47ce-b16f-5288623c6019`, the
  executor seat that followed a `/clear` of the leg-1 executor's process
  (the friction log's fresh-session read).

## Session: 2026-10-28 — Sprint 3 leg 1 (Story 2.1.10, the studies' review fixes)

- **Landed:** #45 → `4bd9694a` (GitHub's merge stamp 13:35:25Z), squash on
  green, `--match-head-commit 678b0c1c…`. Story 2.1.10 is ticked in the
  plan, `docs/close/2.1.json` carries its `reviewedFor` line, and the
  friction log's § Leg 1 holds F5–F11 and the fresh-session read.
- **Next:**
  1. **Leg 2, Story 2.1.6**, the tiered cost, in a FRESH executor seat. Its
     D7 message to Omid (the refused price pages) comes early in the leg.
  2. **Leg 3, Story 2.1.11**, which touches `scripts/` and merges by Omid's
     word. As the last leg, it stamps the sprint EXECUTED.
  3. Then the Sprint 3 review, Story 2.1.7's sitting and Epic 2.1's close.
- **Surprises / decisions:**
  - **A relayed merge delegation was refused; a first-hand one passed.**
    With the delegation relayed by the coordinator, the auto-mode
    classifier refused `gh pr merge` [Merge Without Review] at the 11:55:00Z
    read, and the leg held per the block's D12. Omid then gave the word in
    this terminal himself, and the same command merged at 13:35:25Z. Each
    face is MEASURED once; why the classifier tells them apart is not read.
  - **Two requirements are unmet on paper, each with a route that meets
    it.** [18] fails beyond 227 h a year in the corridor; [27] fails for a
    cold-chain item tag on a CR2032. D2 applied: the studies recommend a
    route (measure first, then a CR2450; a CR2450 on every cold-chain item
    tag), so no ASK-OMID row. Story 2.1.7's sitting reads both.
  - **The orders' leg-1 line cited plan line 246, the D9 seam, not the leg
    line.** The whole story was executed (friction F5).
- **Provenance:** Session id `0082985c-c964-4b27-a5c7-4592056265f0`, the
  executor seat that followed a `/clear` of the Sprint 3 proposer's process
  (the friction log's fresh-session read).

## Session: 2026-10-28 — the Sprint 3 proposal (approved, three legs)

- **Landed:** #43 → `88d54034` (GitHub's merge stamp 11:02:31Z), squash by
  hand on green under the session's merge delegation, `--match-head-commit
  10906b17…`. The Sprint 3 block is the state: `docs/project-plan.md`
  § Sprint 3, approved by Omid first-hand between this terminal's `date -u`
  reads of 10:59:30Z and 11:00:38Z ("Approve as proposed (Recommended)").
- **Next:**
  1. **Leg 1, Story 2.1.10**, the Sprint 2 review's fixes to the three
     studies, in a FRESH executor seat. The block's kickoff is baked; read
     the touched-file ledger for the grants.
  2. **Leg 2, Story 2.1.6**, the tiered cost, after leg 1 merges. Its D7
     message to Omid (the refused price pages) comes early in the leg.
  3. **Leg 3, Story 2.1.11**, the reference check's `sources/` message. It
     touches `scripts/`, so it merges by Omid's word.
  4. Then the Sprint 3 review, Story 2.1.7's sitting and Epic 2.1's close.
- **Surprises / decisions:**
  - **`close-lint` lints the repo it is run from.** Run by absolute path
    from the hub root, it linted the hub ("adopted 2026-07-06"). `npm
    --prefix <F> test` linted this repo. The block's § Paths and habits says
    so (friction F4).
  - **The commit guard cannot resolve `git -C "$VAR"`** and judged the hub's
    `main`; a literal path committed.
  - **The close runner prints "REVIEW SEATING MISSING" for Sprint 2's
    review.** It is the closer's census (Story 2.1.8), noted in the block.
  - **The fresh-session read:** session
    `4ef9427a-246a-42f9-98aa-be892d938900`. Its background-task path id,
    `2a0782f7-…`, was the Sprint 2 reviewer pass's process, so a `/clear`
    came before it. It ran without elevation.

