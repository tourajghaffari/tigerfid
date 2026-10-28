# Session Notes

Cross-machine handoff notes so any Claude Code session can pick up where the
last one stopped. Newest session first; only the 3 most recent sessions are
kept (older ones are trimmed by `scripts/wrap-session.mjs` — full history
lives in git). Written at wrap-up per the "Session handoff" section of
CLAUDE.md. These notes are public, like everything in this repo (CLAUDE.md
§ Writing in public).

## Session: 2026-10-28 — Sprint 3 leg 3 (Story 2.1.11, the sources' fetch route) and the sprint executed

- **Landed:**
  - #50 → `780d1483` (GitHub's merge stamp 23:11:36Z), squash on green,
    `--match-head-commit 23b26c7d…`. `scripts/reference-check.mjs` names
    the fetch route for a missing `sources/` file or folder. Story 2.1.11 is
    ticked, `docs/close/2.1.json` carries its `reviewedFor` line, and the
    friction log's § Leg 3 holds F17–F20 and the fresh-session read.
  - #51 → `cf55d27c` (GitHub's merge stamp 23:13:47Z), auto-merged: the
    Sprint 3 block is stamped EXECUTED, with its Executed record and
    `**Review:** PENDING`.
- **Next:**
  1. **The Sprint 3 review,** in a FRESH seat. The block's § Independent
     checks name leg 3's tests run against `main`'s script before the leg:
     two of the three are red there, and the third pins the unchanged lines
     (F18).
  2. Then Story 2.1.7's decision sitting (Omid's) and Epic 2.1's close
     (Story 2.1.8, a FRESH closer).
- **Surprises / decisions:**
  - **Omid's first-hand word came at the leg's start, so the merge did not
    hold.** He wrote in this terminal, after its UTC read of 23:02:01Z and
    before its `date -u` read of 23:03:44Z: "Please merge any PRs you open
    during this session when ready and in the riught order". The `gh pr
    merge --squash --match-head-commit` that held in legs 1 and 2 passed
    at once.
  - **The folder README's stub is stale** (F17). It still says "the other
    two folders travel by copy only", though Story 2.1.3 added `sources/`.
    It is left for the review with its cost: editing the stub makes every
    machine's folder README a finding until `--write-stub` runs there.
  - **A `sources/` file fetched at a newer revision** reads only `DRIFT
    file` (F20). Recommended for the build initiatives' first story that
    adds `sources/` rows.
- **Provenance:** Session id `13efb2b1-ddbf-4cd6-824f-74b58f5302ce`, the
  hub-rooted executor seat "Windows 4", fresh by launch (the friction log's
  fresh-session read).

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

