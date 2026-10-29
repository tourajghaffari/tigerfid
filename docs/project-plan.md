# Project plan — tigerfid

TigeRFID: the ActiveWave active-RFID system — boards, firmware and software
built from 2001 to 2013 — that Touraj Ghaffari's sons open-sourced under
Apache-2.0 in 2017–2018 (the dedication in `readme.md`), and its rebuild
from the ground up. The 2018 state is kept byte for byte at the annotated
tag `activewave-2018` (`f25ff13f3fb672d8ec91f1fadc8894d69f781ed6`) and its
GitHub release; nothing in the history is rewritten. Initiative 1 joined the
repo to the fleet and cleared the current tree for the rebuild (closed
2026-10-27; its record is in `docs/archives/project-plan-completed.md`); Initiative 2
is the design study, which decides the new system and ends in Omid's
decision sitting. Its close files the build: one Reader, one Field Generator
and three tags across two zones, which a complete beginner can follow on
Windows, macOS or Linux with free tools only (milestone 1, Initiative 2).
Everything in this plan is public, written for a stranger (CLAUDE.md
§ Writing in public).

**Provenance / re-review note:** this plan was born at adoption, not audited — authored 2026-10-26 by hub Story 243.1.2 per KB `new-repo-standard`'s authored-plan template, from the approved design spec (hub spec `2026-10-26-tigerfid-onboarding-design`, Omid 2026-10-26, hub PR #4355). No `/audit-project` run yet; the spec's decisions D1–D12 and calls C1–C11 are the re-review record until the first audit.

**Last staleness sweep:** 2026-10-29 (the Sprint 4 proposal's step-2 sweep,
KB `plan-staleness-sweep`: scoped to the sprint's inputs, which were Epic
2.1, the archive's Sprint 3 record and its friction log's § Reviewer pass,
`docs/close/2.1.json`, the anchors the review's findings cite, and, for
Story 2.1.13, the folder's stub, `scripts/reference-check.mjs` and
`docs/reference-repos.md`).

## Plan format

Three levels, fleet-standard:

## Initiative N: <name>
### Epic N.M: <name>
#### Story N.M.K: <name>

Completed stories are ticked ✅ DONE in place by the completing PR; the
physical cut to `docs/archives/project-plan-completed.md` happens at epic
close only, never per story — the close PR is the archival PR (the archival
clause; fleet convention, Omid 2026-07-07). Every epic ends with a
`Close out Epic N.M` story and has a manifest in `docs/close/` (KB
`epic-close-protocol`); the PR that edits an epic's stories updates its
manifest.

Sprint orchestration blocks (kept in this file while live) are cut the same
way once every leg story is ✅ DONE **and** the sprint's review has landed —
into the archive's `## Executed sprints` section, newest first, the heading
stamped `— ✅ EXECUTED (date)` plus an **Executed:**/**Reviewed:** record.
Keep that section above the first `## Initiative` block in the archive.

## Sprint 4 — "Figures that hold for the sitting: a listen every tag can keep, tiers that run what they buy, and a reference folder that names the sources' route" (proposed 2026-10-29; 3 legs, serial) — ✅ APPROVED (2026-10-29) — ✅ EXECUTED (2026-10-29)

**Executed:** 2026-10-29, 3/3 legs serial, each by a fresh hub-rooted
Windows executor seat. None was elevated.
- **The seats** (each leg's friction-log section has its fresh-session read):
  - **leg 1:** "tigerfid Sprint 4 leg 1" `6fb033a2-…`, on the Mac
    coordinator's orders 24;
  - **leg 2:** "tigerfid Sprint 4 leg 2" `7a8f4e54-…`, on its orders 28.

    Each of these two was a `/clear` of the process `13efb2b1-…`.
  - **leg 3:** "Windows 3" `70fda9ed-…`, fresh by launch, on the Mac
    coordinator's orders 7.
- **The legs,** each with its full merge sha and GitHub's merge stamp, read
  from the API in one call between leg 3's `date -u` reads of 23:03:13Z and
  23:03:14Z:
  - **Leg 1, Story 2.1.12 — #58 → `cf580b05b9f596a2106897f1456f42c2275ad991`
    (10:05:48Z):** the listening window. A tag skips the Reader's beacon on
    most listens and hears its set's slots alone. Every battery figure is
    re-priced and Sprint 3's D2 re-run: [18] holds by 24 % for a room's
    tag, [9] by 22 %, and [27] by 22 %, or 11 % on cold chain.
    - D2's clause for a baby's band did not fire, and no ASK-OMID row was
      filed. The conference's block of Field Generator slots is computed,
      and GNU make is named among the tools not cited.
  - **Leg 2, Story 2.1.14 — #60 → `b707bf1a6042aa8bebd380e23be1e4059f0d601f`
    (10:44:46Z):** the tiers run what they buy: the hot plate's supply, the
    debugger's cable and the tags' clip, with JLCPCB's minimum of two boards
    a design. The totals are 602.25, 1,449.92 and 1,996.03 US dollars for
    the starter, advanced and pro tiers.
    - No price is written not read, and no D7 message went.
  - **Leg 3, Story 2.1.13 — #62 → `d149ade61144989f58fa0d0624a408efba78cd4c`
    (23:02:54Z):** the reference folder gives the `sources/` route: the
    stub names `sources/`, a drifted `sources/` file's line names the newer
    revision and the copy route, and reading rule 8 names `pdftotext
    -layout`. The tool still never fetches (D11).
    - `--write-stub` ran on leg 3's machine. The other machine's folder
      README reads `README stale-stub` until that command runs there (D13).
- **The merges** (D12), each by Omid's first-hand word in the leg's own
  terminal. Leg 1 held when the auto-mode classifier refused the relayed
  delegation at `gh pr merge`, then merged on his "Merge #58
  (Recommended)". The words for legs 2 and 3 came early in each leg, and
  neither merge held.
- **The manifest,** `docs/close/2.1.json`, flipped as mapped:
  - legs 1 and 2 flipped no tooth;
  - leg 3 flipped `stub-names-sources`.

  `rebuild-spec-approved` stays RED until Story 2.1.7, as the block said.
- **No ASK-OMID row** was filed by any leg. **No D9 split:** the legs
  planned 7, 7 and 6 tasks (leg 3's sixth is this stamp).
- **The record:** `docs/friction-log-sprint4-figures-that-hold.md` (the
  proposal's F1–F4, leg 1's L1–L5, leg 2's T1–T8 and leg 3's R1–R4), each
  leg's guard events written with the class id beside "seat".
- **Open for the review** (each a deferred Minor or a routed item):
  - **The proposal:** F1, a parser command that runs origin/main's copy of
    the hub's sprint parser; F2, a `--paths` flag for the test-arming read.
  - **Leg 1:** L2, reproduce slot counts at 11/15 ms; L3, the old [27]
    table's four cells 0.1 above the script; L5, `software-stack.md`'s raw
    slot capacity, dispositioned current.
  - **Leg 2:** T2, reproduce the fees from the stated fully assembled
    joints; T3, the breakout's 0.1" header INFERRED; T7, a CLAIM outside
    the ledger, granted.
  - **Leg 3:** R2, the folder README changed on its branch before `main`;
    R3, a grant that named one of the ledger's three sections.
  - **The register seats:** each leg's § Guard events in the friction log.
    Leg 3 also caught an `inferred-cause-recorded-as-measured` seat (R1).

**Review:** PENDING: the Sprint 4 reviewer pass, a FRESH seat.

**Approval:** approved 2026-10-29 (Omid, first-hand in the proposer's
terminal "tigerfid Sprint 4 proposal", between its `date -u` reads of
00:50:57Z and 06:25:22Z: "Approve as proposed (Recommended)"): the block as
proposed, three legs with Story 2.1.12 split, D1–D13 at their defaults. The
question laid out the split with its cost, and D2's clause for a baby's
band. The sign-off edit is the proposer's, on the proposal PR's branch
(tigerfid #55).

**Proposed:** 2026-10-29 by the hub-rooted Windows session "tigerfid Sprint 4
proposal" (session `cb189c7f-0ffb-4dea-bea2-8485d49ee215`), on the Mac
coordinator's orders 19, composed after its `date -u` read of 00:28:18Z. The
session was fresh by `/clear`: its background-task path id,
`13efb2b1-ddbf-4cd6-824f-74b58f5302ce`, is the session id of "Windows 4",
Sprint 3's leg 3 executor. That process last held the Sprint 3 reviewer pass
(`f050a5d6-…`, wrapped at #54), which this `/clear` ended. Role proposer; not
elevated (a Windows administrator-role read, `False`, at 00:42:54Z).

**The outcome:** the figures Omid's decision sitting (Story 2.1.7) decides
from hold as printed:
- every tag's battery figure rests on a listening layout every tag can keep,
  with Sprint 3's D2 re-run for each margin the layout moves (Story 2.1.12);
- every tier's total buys what its tools need to run: the hot plate's
  supply, the debugger's cable and the tags' fixture, with JLCPCB's minimum
  order named (Story 2.1.14, split from 2.1.12 by this proposal);
- the reference folder's README, its drift line and the reading rules give
  the `sources/` route (Story 2.1.13).

**Admission (the meta-work test):** legs 1 and 2 correct public documents
that a stranger reads on GitHub this month and that the sitting decides
from. Leg 3 changes what `npm run setup` prints, and the README it writes,
for every stranger who sets up the reference folder.

**For the sitting: what it needs corrected first, and what can wait.**
- **First,** since each moves a figure the sitting decides from:
  - finding 1, the listening window: the margins of [9], [18] and [27], from
    which the sitting chooses the nursery tag's and the item tag's cells
    (leg 1);
  - findings 2 and 3: the advanced and pro tiers' totals (the hot plate's
    supply), and every tier's total, the recommended starter tier's
    included (the debugger's 10-pin cable and the tags' fixture) (leg 2);
  - finding 4: JLCPCB's minimum assembled quantity, missing from the Lacks
    of both assembled routes, the starter tier's among them (leg 2);
  - finding 5: what three tag boards cost against one core on the
    hand-solder route, a choice the sitting makes (leg 2).
- **Can wait,** since the sitting decides nothing from them. Each rides the
  leg that already edits its section, which costs less than a second pass:
  - finding 6, GNU make's platforms: the closer's
    `three-platform-claims-sourced` reads it (leg 1);
  - the four nits in `tiered-cost.md`: a rounding and three sentences
    (leg 2);
  - the anti-collision nit: the conference's own block of Field Generator
    slots. INFERRED at proposal, from `radio-and-location.md`'s own inputs:
    one Reader keeps [3]'s 1,000 reply slots a second with up to about 164
    Field Generators in its block (a 250 ms frame, less 250 slots of
    0.733 ms, less the 1.0 ms beacon, over 0.4 ms a slot), against the
    ward's 27. It rides leg 1 because the layout leg 1 chooses sets the
    block's length;
  - Story 2.1.13: it gates nothing in the study (leg 3).
- **The order after the sprint:** the Sprint 4 review, then the sitting. The
  sitting waits for this sprint because Sprint 3's figures did not hold as
  printed; Omid may sit once leg 2 merges, at the same cost the Sprint 3
  review named, figures no review has read. Leg 3 does not gate the sitting.

**Inputs swept 2026-10-29.** The scope is Epic 2.1 (its Ordering line and
Stories 2.1.12, 2.1.13, 2.1.7 and 2.1.8), the archive's Sprint 3 record (its
`**Reviewed:**` findings, nits and routed list) and its friction log's
§ Reviewer pass, `docs/close/2.1.json`, each anchor a finding cites, and, for
Story 2.1.13, `docs/reference-readme-stub.md`, `scripts/reference-check.mjs`
and `docs/reference-repos.md`.
- **The events digest,** `git log origin/main --oneline --grep '<id>'` at
  `de0657f`:
  - 2.1.12 and 2.1.13: #53, which bore them, and #54, its wrap note;
  - 2.1.7: #23, #39, #41, #43 and #45, each a Context edit;
  - 2.1.8 and 2.1.14: no hit, so the id 2.1.14 is free.

  `docs/notes.md` and the archive name 2.1.12 and 2.1.13 only as forward
  work. `node <hub>/scripts/fleet/open-prs.mjs --repos tigerfid` read 0 open
  PRs, and `git ls-remote --heads origin` lists `main` alone. Every story is
  CURRENT; none has run.
- **Each anchor, re-read on `main` (`de0657f`)** before a story cites it:
  - finding 1: `radio-and-location.md` l.521 ("The slots sit in one block
    after the Reader's beacon") and l.525 ("its set in one window");
    `parts-and-toolchains.md` l.381 (the Listening row, "The window is the
    Reader's beacon, 1.0 ms, and 0.4 ms for each Field Generator slot"),
    l.422 (the room tag's 147.4 mAh), l.432 ("by 2.3 %"), l.441 ("holds its
    year by 1.8 %"), l.497 and l.522 ([27]'s 143.7 and "a margin of 4 %");
    `requirements.md` l.272–276, [18]'s dated pointer line, which quotes the
    1.8 %;
  - finding 2: `tiered-cost.md` l.272 ("Each tier holds what its route
    needs") and l.281 (the MHP50-B5 in the advanced and pro columns);
    `grep -c -i 'USB.PD'` reads 0 there;
  - finding 3: `tiered-cost.md` l.286 (the LP-XDS110ET in every tier, and
    nothing beside it); `parts-and-toolchains.md` l.170 (the tags' "adapter
    or a pogo-pin fixture") and l.309–310 (the "10-pin XDS110 out
    connector"); `grep -c -i 'pogo'` reads 0 in `tiered-cost.md`;
  - finding 4: `tiered-cost.md` l.248–249 ("JLCPCB's minimum assembled
    quantity is not read"), and the Lacks at l.263–264 and l.343–344, which
    do not name it;
  - finding 5: `tiered-cost.md` l.77–78 ("boards cost about the same, since
    OSH Park prices by area") beside l.212 ("includes three copies of your
    design");
  - finding 6: `parts-and-toolchains.md` l.242–246 ("for every tool except
    two") and l.260 (GNU make's row, its platforms INFERRED). The needle
    "except the Arm" reads 0, because the line wraps after "two." (Sprint 3's
    F7 face, met by this proposal's own grep and caught);
  - the nits: `tiered-cost.md` l.369 ("would save 19.13"), l.38 ("Each page
    was fetched once") beside l.61–62 (LCSC's pages read again), and l.282
    (the ceramic tweezers "with" the ESD pair) beside l.287's 991.62;
    `parts-and-toolchains.md` l.301 ("Story 2.1.6 prices the rest and dates
    every price again") beside `tiered-cost.md`'s one debugger row, the
    LP-XDS110ET at l.286; `radio-and-location.md` l.272–273 and l.417–418
    ("1,000 tags take 0.73 s");
  - Story 2.1.13: `docs/reference-readme-stub.md` l.25 ("the other two",
    wrapping to "folders"; `grep -c 'sources'` reads 0);
    `scripts/reference-check.mjs` l.170, the file DRIFT line; and
    `docs/reference-repos.md` § Reading rules (l.55), where `grep -c
    'pdftotext'` reads 0.
- **Refreshed in this PR:** Epic 2.1's Ordering line; Story 2.1.12, narrowed
  to the listening window; Story 2.1.14, born (§ The sprint's shape); the
  manifest's `stories`, its `sprint3-review-fixes` narrowed with the story
  and `sprint3-review-tier-fixes` born; the plan's staleness stamp.
- **Observed, the closer's (Story 2.1.8):** the close runner prints "REVIEW
  SEATING MISSING" for Sprints 2 and 3 (its read after 00:41:23Z). Sprint
  3's review sent its seats to the Mac coordinator as text, and its R5 found
  the runner reading a hub checkout that was behind. The closer censuses the
  register at the hub's origin/main.

The standard (hub KB `sprint-proposal-standard`) was read by its steps 0–8
at their rule level, and its friction log's newest entry, P74, the one
Sprint 3's proposal read last. Neither it nor KB
`plan-authoring-for-cold-execution` changed on the hub's origin/main since
that read; the one tool that did is the sprint parser (hub #4543, a relative
plan path and a CRLF plan), whose origin/main copy this proposal ran
(§ Verified-live). The Sprint 3 review's advice to this proposal is carried:
R1 and R4 in § Independent checks, R3 and F7 in the merge-loop line, F12
and F13 in D7, and F18 in leg 3's gate.

**Why this order (step 2):**
- **Leg 1** goes first. Finding 1 is the review's first Important finding,
  and the only one whose D2 may need Omid's word before the sitting (D2), so
  it gets the most time ahead of it. Its outcome may move a sentence of
  `tiered-cost.md` § Both cells, which leg 2 re-reads.
- **Leg 2** follows. Its prices are dated as near the sitting as the sprint
  allows, and it reads leg 1's cell recommendations. It shares
  `parts-and-toolchains.md` with leg 1 (§ The debugger here; § The battery
  and § The toolchains there), so serial order spares the file a conflict.
- **Leg 3** goes last. It gates nothing in the study, and it touches
  `scripts/`, so it merges by Omid's word; placing it last keeps the
  sitting's path from waiting on that merge. It shares only the plan, the
  manifest and the friction log with legs 1 and 2.
- **Stories 2.1.7 and 2.1.8 stay outside the sprint.** The sitting is
  Omid's, and no executor seat can hold it. The close needs the sitting's
  spec and a closer who did none of the epic's work.

**The sprint's shape (step 3).** Three legs, under the cap.
- **Story 2.1.12 is split**, by the standard's session-fit test. As filed,
  it names about eight mechanisms with checks of their own: the listening
  layout; the battery re-pricing with D2; the plate's supply; the debugger's
  cable and the tags' fixture; JLCPCB's minimum; the three-board arithmetic;
  GNU make's citation; and the conference's block. That is above the test's
  six or so, and one leg's plan would come to ten or eleven tasks, above
  D9's tripwire of eight. The seam is the story's own two clauses:
  - **2.1.12 keeps the listening window,** "the tags' battery figures … on a
    listening layout": finding 1, finding 6 and the anti-collision nit, in
    two of the three studies.
  - **2.1.14 takes the tiers,** "the tiers' totals on a budget that can
    build what it lists": findings 2–5 and the other four nits, in
    `tiered-cost.md` and one pointer line of `parts-and-toolchains.md`
    § The debugger.

  Each item moves as the review wrote it, with what it MEASURED and what it
  INFERRED, and the manifest moves in lockstep.
- **Weighed and declined:**
  - **2.1.12 whole, in one leg, with D9's seam named.** Its estimate is
    above the tripwire, so the executor would stop before its first task and
    split it there. The remainder would then be a new story outside this
    sprint, and a second sprint ahead of the sitting.
  - **Story 2.1.13 in a later sprint.** It is ready and small, and it holds
    the manifest's one RED tooth besides the sitting's. A later sprint costs
    a proposal and a review for a five-task leg.
  - **Legs 1 and 2 at once, on two boxes.** They share no study section, but
    each ticks the same plan and appends its `reviewedFor` line to the same
    place in `docs/close/2.1.json`, so the second leg would rebase by hand.
    It saves one leg's wall-clock, which the sitting's date does not hang
    on.

1. **Leg 1 — Story 2.1.12** — the listening window, in
   `docs/study/radio-and-location.md` and `docs/study/parts-and-toolchains.md`:
   finding 1's layout chosen and stated (D3), its cost carried into every
   battery figure it moves, and Sprint 3's D2 re-run for [9], [18] and [27]
   (D2); the conference's block of Field Generator slots computed beside the
   ward's (the anti-collision nit); GNU make's platforms cited, or named
   among the tools not cited (finding 6, D4). Gate: the story's greps quoted
   in the PR body, each hit's disposition beside it, and a claims audit over
   every changed figure. Flip map: no tooth flips; the five study teeth stay
   PASS. Prose for the closer: `sprint3-review-fixes`, with
   `three-platform-claims-sourced` and `no-medical-claim` read again where
   the leg edits. Item-8 scope: the two studies; `docs/study/requirements.md`'s
   dated pointer lines (D1); `tiered-cost.md` § Both cells, read and left to
   leg 2; and Story 2.1.7's Context, only if Omid rules under D2.
2. **Leg 2 — Story 2.1.14** — the tiers, in `docs/study/tiered-cost.md`: the
   hot plate's USB-PD supply (finding 2), the debugger's cable and the tags'
   adapter or fixture (finding 3), each priced in every tier that needs it
   or named in its Lacks (D5–D7); JLCPCB's minimum assembled quantity, read
   or named in each assembled route's Lacks (finding 4); the three-board
   estimate, by the document's own arithmetic (finding 5); the four nits;
   and the totals summed again. Gate: as leg 1's. Flip map: no tooth flips;
   `tiered-cost-doc` stays PASS. Prose for the closer:
   `sprint3-review-tier-fixes`, with `tiers-dated` and `no-medical-claim`
   read again. Item-8 scope: every line that quotes a total or a figure the
   leg changes, in the study and outside it; § Both cells, against leg 1's
   outcome; and `parts-and-toolchains.md` § The debugger's pointer line.
3. **Leg 3 — Story 2.1.13** — the reference folder's `sources/` route: the
   stub names `sources/` and its route; a drifted `sources/` file's line
   names the newer revision and the copy route; the reading rules name
   `pdftotext -layout` as the Windows route to a PDF's pages; and
   `--write-stub` runs on the leg's box (D11, D13). Gate: the red-first line
   names its two tests: the drift test is seen red against `main`'s script,
   and the pin (another folder's drift line unchanged) green there by design
   (Sprint 3's F18); then both green, and `npm test` green. Flip map:
   `stub-names-sources` FAIL → PASS. Prose for the closer:
   `reference-folder-sources-route`. Item-8 scope: `docs/reference-repos.md`,
   `docs/reference-corpus.tsv`'s header comment and
   `docs/reference-research.md`, where they describe the stub, the drift
   line or a PDF's pages.

After the sprint, `rebuild-spec-approved` (Story 2.1.7) stays RED.

**Decisions (step 5) — no decision currently blocks execution on Omid.** Each
is the executor's at its default unless Omid says otherwise at the approval.

- **D1 — A study chooses; the sitting decides** (Sprints 2 and 3's D1,
  unchanged). A number Omid set or accepted in `docs/study/requirements.md`
  changes only by his word. A figure that a dated pointer line there quotes,
  and that this sprint changes, gets a new dated pointer line beside it; the
  text stays otherwise unchanged.
- **D2 — A margin that fails on paper after finding 1's layout.** If [9],
  [18], [26] or [27] is unmet on paper at the planning capacity:
  - the leg records it, names the routes with their costs (measure first, a
    larger cell, a slower listen where a latency allows it, one conversion
    per reading, the fallback in which the Field Generators listen) and
    recommends one, and the sitting decides. No requirement is lowered.
  - **A baby's band.** The nursery's room tag is a baby's tag ([16], [17]),
    and the study says a baby's band "may not" take a CR2450
    (`parts-and-toolchains.md` l.457). So if [18] fails for the room tag and
    the larger cell is the only route that meets it on paper, no route meets
    [18] in a baby's band. The leg then puts it to Omid first-hand in its
    terminal, framed by the goal [18] serves (a tag changed at a planned
    yearly service, never mid-stay: `requirements.md` [18]) with the leg's
    recommendation for that goal, and records his words with the terminal's
    two bracketing `date -u` reads. If he cannot be reached in the leg, it
    files ASK-OMID A2 with that question.
  - Default: the number stands, and the study recommends the closest route.
    The leg proceeds on the default, and its tick names A2 if it filed one.
  - INFERRED at proposal, for the leg to check and not a finding: a tag that
    hears only its set's slots, skipping the beacon, would listen for about
    8.1 µA at 1 s (2.795 µC + 5.8 mA × 0.92 ms, once a second), against the
    13.9 µA printed and the review's 16.73 µA for a second wake. If that
    layout's costs hold, the clause above may not fire.
- **D3 — Finding 1's layout is the leg's choice.** The options the review
  names, and one it implies:
  - a tag that skips the Reader's beacon on most listens, keeping time on its
    32 kHz crystal and re-syncing on its own zone's slot. Its cost: what the
    beacon carries (the downlink that names a listen set; in the US, the hop
    to the next channel) waits for the next beacon the tag hears;
  - a short sync before each group of slots, so that each zone's set follows
    a sync. Its cost is frame time for each group, which the anti-collision
    figure carries;
  - the second wake, charged: the review's 16.73 µA for the room tag;
  - another the leg finds.

  The leg weighs each on its listening cost, on [13]'s 5 s, [17]'s 5 s and
  [22]'s 5 min, on the frame time [3] needs, and on the downlink that names
  a set. It states the chosen one in `radio-and-location.md` § How a tag
  hears a Field Generator it has not heard before, and carries its cost into
  `parts-and-toolchains.md` § The battery's Listening row, § Each worn tag's
  year and § [27].
- **D4 — Finding 6: cite it or name it.** GNU make's platforms are cited
  from a page that states them, with its URL and fetch date, or the sentence
  above the table names GNU make beside the Arm GNU Toolchain and the SDK as
  not cited. GNU's own make page reset the connection twice for Story 2.1.10
  (`docs/reference-research.md` § 4); if it refuses again, the sentence is
  the default. Either way, the closer's `three-platform-claims-sourced`
  reads one true sentence.
- **D5 — Prices** (Sprint 3's D5, unchanged). In US dollars, each from a
  seller's public page, with the date it was read, the quantity it is for
  and the page's URL; cited, never pinned; shipping and tax left out, and
  the document says so. Each tier's total states what it is made of.
- **D6 — A part the studies leave unnamed** (the 100 W USB-PD supply, the
  debugger's 10-pin cable, the tags' adapter or pogo-pin fixture) is priced
  as a representative part the leg names, marked "representative, for
  pricing" with the reason. It is never presented as a choice: the build
  initiatives choose it.
- **D7 — A page that refuses a plain request** is put to Omid in ONE message
  in the leg's first minutes, listing every such URL with what each price is
  for and the leg's default if it stays unread. The research log's § 4 names
  the sellers that refused Sprint 3 and LCSC's pace (15 s between requests),
  so the list is drawn before the first price is read (the review's
  disposition of F12 and F13). His answer is recorded first-hand, with the
  terminal's two bracketing `date -u` reads. Default, if he cannot be
  reached in the leg: each is written not read and named in its tier's
  Lacks, and no total counts it as zero; that was his answer for Sprint 3's
  five ("Write all as not read (Recommended)").
- **D8 — Milestone 1 is priced for the US build** (Sprint 3's D8,
  unchanged). The parts that differ for the EU build are named, not priced.
- **D9 — A leg is a session.** The tripwire: an executor's plan above eight
  tasks stops before its first task, narrows the leg in story order, splits
  the remainder off as a new story under the same step, and logs the miss
  against this proposal. The seams:
  - **Leg 1:** finding 6 becomes the remainder. Finding 1 and the
    anti-collision nit stay, since the layout sets the block the nit
    computes.
  - **Leg 2:** finding 5 and the four nits become the remainder. Findings
    2–4 stay, since they move the totals the sitting reads.
  - **Leg 3:** none; it is small.

  A session limit mid-leg pushes the branch and leaves a notes DRAFT, and
  the resumed sitting re-runs the re-verify set first.
- **D10 — Every claim is sourced or marked** (Sprints 2 and 3's D10,
  unchanged): every number and every "the rule allows" or "the part
  supports" sentence names its source, is MEASURED with the read, or is
  INFERRED with its arithmetic. Also binding: no figure from the 2005/2017
  datasheets becomes a requirement (spec Appendix B); no medical claim (spec
  C8); no third-party text, figure or file is copied into the repo (the
  licence lane); nothing from the reference folder is committed; the Outlook
  message stays unread (reading rule 7).
- **D11 — The tool still never fetches** (Sprint 3's D11). Leg 3 changes the
  drift line, the stub and the reading rules; `docs/reference-repos.md`'s
  "The tool never fetches a file row" stands.
- **D12 — Merges.**
  - **Legs 1 and 2 are docs-only** (`docs/**`). Each merges by `gh pr merge
    --squash --match-head-commit <full sha>` on green, under the session's
    merge delegation as it stands, or by Omid's word.
  - **Leg 3 touches `scripts/`.** It merges by Omid's word, which his
    in-session delegation sentence is (this repo's `CLAUDE.md` § Git
    workflow).
  - **A relayed delegation can be refused** by the auto-mode classifier. The
    leg then holds and tells the coordinator the PR and its head sha, and
    Omid's own `!` in the leg's terminal is the pass.
- **D13 — The stub's cost** (Sprint 3's F17). The check compares each
  machine's folder README with the stub byte for byte, so once leg 3 merges,
  every other machine reads `README stale-stub` until `node
  scripts/reference-check.mjs --write-stub` runs there. Leg 3 runs it on its
  own box. The other box's README is left to the check's own prompt, which
  names that command (the story's Done-when), and the next session there runs
  it when the check prints it.

**The touched-file ledger** (Sprint 2's F21). Every file each leg may edit,
so the coordinator's grants name them all. Every leg edits
`docs/project-plan.md` (its tick), `docs/close/2.1.json` (its `reviewedFor`
line) and this sprint's friction log.
- **Leg 1:**
  - `docs/study/radio-and-location.md` (§ Zones, with its § How a tag hears
    a Field Generator it has not heard before, and § Anti-collision's
    figures);
  - `docs/study/parts-and-toolchains.md` (§ The battery, § Each worn tag's
    year, § [27] and § The toolchains);
  - `docs/study/requirements.md`, only for a dated pointer line (D1);
  - `docs/reference-research.md` § 4, the entry for the leg's reads;
  - `docs/reference-corpus.tsv` and `docs/reference-repos.md` § `sources/`,
    only if the leg pins a source a choice rests on (Sprint 2's D8);
  - Story 2.1.7's Context and the ASK-OMID register, only for Omid's word or
    A2 under D2.
- **Leg 2:**
  - `docs/study/tiered-cost.md`;
  - `docs/study/parts-and-toolchains.md`, only § The debugger's pointer line
    and § The tags' Debug row, where it names the fixture the leg prices;
  - `docs/reference-research.md` § 4, the price reads' entry;
  - the corpus files only if it pins a source (none is expected, D5).
- **Leg 3:**
  - `scripts/reference-check.mjs` and `scripts/reference-check.test.mjs`;
  - `docs/reference-readme-stub.md`;
  - `docs/reference-repos.md` (the reading rules, and § Verifying a transfer
    and § `sources/` where they describe the drift line);
  - outside the repo, the folder's README on the leg's box, by
    `--write-stub`, never committed.

  `docs/ci-contract.md` does not change: `test` already runs the test file.

**Pre-flight (step 4), per leg** (KB `plan-authoring-for-cold-execution`
item 53's two halves, and a test-arming read):
- **Leg 1:**
  - **narrowing half:** each item 2.1.12 keeps (finding 1, finding 6, the
    anti-collision nit) maps to one Context bullet, and the Done-when names
    each. Nothing is narrowed beyond the split; finding 1's two layout
    examples stay, because the review measured the text they correct.
  - **forward-seam half:** leg 2 reads leg 1's cell recommendations through
    § Both cells and prices both cells already, so no leg 1 outcome moves a
    price. Leg 3 shares no file with leg 1 but the plan, the manifest and
    the log.
  - **test arming, MEASURED** through `scripts/ci-docs-only.mjs`'s
    `classify()` with `repoOptions('tigerfid')`: `test` runs in full,
    because the plan and the manifest are CI-validated docs and this repo
    records no validator job.
  - **estimate:** seven tasks (re-verify; the layout weighed and stated; the
    worn tags' listening and years priced again, by a script; [27] priced
    again; D2 re-run for [9], [18] and [27], with the pointer lines and any
    message to Omid; the anti-collision nit and finding 6; the greps, the
    audit, the research-log entry, the tick and the PR).
- **Leg 2:**
  - **narrowing half:** each item 2.1.14 carries (findings 2–5, the four
    nits) maps to one Context bullet, and its Done-when names each.
    `sprint3-review-tier-fixes` reads every tier's Lacks for the parts the
    tools need.
  - **forward-seam half:** Story 2.1.7's sitting reads the totals, and the
    build initiatives' bench story reads the tool tiers. Leg 3 touches
    neither.
  - **test arming, MEASURED** the same way: `test` runs in full.
  - **estimate:** seven tasks (re-verify, with leg 1's outcome and § Both
    cells; the price list and the D7 message in the first minutes; the
    supply; the cable and the fixture; JLCPCB's minimum and the Lacks; the
    three boards, the four nits and the totals summed again; the greps, the
    audit, the research-log entry, the tick and the PR).
- **Leg 3:**
  - **narrowing half:** F17 maps to the stub, F20 to the drift line, F6 to
    the reading rules, and the stub's cost to the `--write-stub` run; the
    Done-when names all four.
  - **forward-seam half:** no later leg in this sprint. The build
    initiatives' new `sources/` rows read the same lines, and the other box
    reads `README stale-stub` once (D13).
  - **test arming, MEASURED** the same way: `test` runs in full, since
    `scripts/` is not documentation. The new tests run in `npm test` and in
    CI's `node --test scripts/reference-check.test.mjs`.
  - **estimate:** five tasks (the red test and the pin; the drift line and
    the header comment; the stub and the PDF route; `--write-stub`, the
    check and the item-8 grep over the corpus documents; the tick and the
    PR), with the sprint's EXECUTED stamp as a sixth.

**Checks per leg:**
- the flip map, through `node <hub>/scripts/close/close-epic.mjs --epic 2.1
  --repo <F> --only <ids>`;
- `node scripts/close-lint.mjs` and `npm test` (the `test` job's commands),
  unpiped, on every leg;
- `node scripts/reference-check.mjs` on leg 3, before and after
  `--write-stub`, and on any leg that adds corpus rows.

**Gate line (every leg):** the required set is `scope` + `test` (the
`protect-main` ruleset).
- **`test` runs in full on every leg**, by the test-arming reads above.
- **`scope` also gates the tick and secrets.** It runs
  `check-tick-merge-claim.mjs` (a tick never claims its own PR's merge) and
  the secret scan.

**Merge-loop line:** each leg's PR:
1. ticks its story in place: the heading gains `— ✅ DONE (date)`, with
   `**Completed:**` (the date, `tigerfid #N`, the branch and the pre-tick
   head, never the merge) and `**Outcome:**` directly under it;
2. appends its `reviewedFor` line to `docs/close/2.1.json` in the same PR
   (lockstep);
3. runs the item-8 anchor grep, `grep -n "Story 2.1.<K>"
   docs/project-plan.md docs/close/2.1.json`, and the document-name grep,
   `grep -rn "<document>.md" docs/`, under a PR-body heading titled exactly
   "Item-8 anchor grep". Each hit's disposition (refreshed, current, or
   frozen in a DONE story) stands beside it as a bullet, never a table row,
   since the hub's `check-item8-dispositions.mjs` reads bullets only (Sprint
   3's R3); each `current` one quotes its line. A predicate grep uses the
   shortest needle a line break cannot split (Sprint 3's F7);
4. runs the C8 grep, `grep -n -i -E 'medical|vital' docs/study/<document>.md`
   (each hit a negation), and reads the diff for a hostname, an IP address, a
   machine name, a private path, or any person beyond those `readme.md`
   names;
5. carries a CLAIMS AUDIT in its body: every number and every rule or part
   claim beside its source, its measurement or its INFERRED arithmetic (D10);
6. runs `node scripts/close-lint.mjs` and `npm test` unpiped, gated by `&&`
   all the way to `git commit`;
7. rebases on the fetched `main` and merges per D12, after `node
   <hub>/scripts/fleet/watch-pr.mjs <N>` from inside the clone.

No CHANGELOG.

**Kickoff (step 6, baked):** a FRESH executor session per leg, on either box:
1. **Open:** from the hub root, `node scripts/fleet-view.mjs --start
   "tigerfid Sprint 4 leg <N> — Story 2.1.<K>" --role executor --repos
   tigerfid` (the marker goes to the hub's origin by itself). It fetches and
   fast-forwards what it may. When it prints a read-at-origin line for the
   hub, the hub's tools are read at `origin/main` and the hub is never
   pulled.
2. **Fetch this repo:** on `main`, `git -C <F> fetch origin` and `git -C <F>
   merge --ff-only origin/main`, each unpiped, its exit read.
3. **Clocks:** `date -u` before every clock written.
4. **Read, before any search:**
   - this block and the leg's story, with its checks in
     `docs/close/2.1.json`;
   - `CLAUDE.md` (this repo's), § Writing in public above all; a hub-rooted
     seat reads it by hand, since it does not load there;
   - `docs/reference-repos.md`, for the reading rules, and
     `docs/reference-research.md`, for § 4's reads and refusals;
   - the study documents the story cites, the archive's Sprint 3
     `**Reviewed:**` record and its friction log's § Reviewer pass (legs 1
     and 2);
   - the previous leg's PR body and this sprint's friction log.
5. **Re-verify first:** the set below.
6. **Branch:** `story-2.1.<K>-<slug>`, from the fetched, fast-forwarded
   `main`.

Paths and habits:
- `<hub>` is the hub root's absolute path on the executor's box, `<F>` is
  `<hub>/projects/tigerfid`, and `<SCRATCH>` is the session scratchpad
  (`mkdir -p` first; on Windows `$TMPDIR` is unset).
- Checkers run unpiped, and a census never reads through `head`, `tail` or
  `cut`. Long files are written with the Write tool. `git -C` takes the
  literal path, never a shell variable (the commit guard cannot resolve one
  and judges the session's own checkout). `npm --prefix <F> test` runs the
  repo's checks without a `cd`. `close-lint` lints the repo it is run FROM:
  `node <F>/scripts/close-lint.mjs` from the hub root lints the hub and
  prints the hub's adoption date, 2026-07-06, not this repo's 2026-10-26.
  `npm test` here is light and takes no battery lock.
- On Windows, a colon path through git takes `MSYS2_ARG_CONV_EXCL='*'`, with
  the repo's path written `C:/…`, never `/c/…`, in the same command (the
  variable stops git from reading the POSIX form); no interpreter is fed by
  stdin.
- On the Mac, the lid stays open while any fleet job runs (KB
  `mac-standby-sleep-abandons-jobs`).
- CI runs on GitHub-hosted runners only. No leg edits `.github/`, and only
  leg 3 edits `scripts/`. A stranger's PR is never checked out.

**Friction log:** `docs/friction-log-sprint4-figures-that-hold.md`. Each leg
appends its section, written for a stranger:
- its context checkpoint: the plan's task count, compactions, forced
  re-reads and lost-context defects;
- each piece of friction it met;
- each guard event with the guard's own banner line quoted whole and its
  register class id beside the word "seat", so the hub's
  `check-register-seats.mjs` can pair it. A seat note says "flagged" or
  "drew the arm", never a verdict word, when a census line precedes the
  banner in its row (§ Roles).

**Re-verify at execution** (each leg, before its first edit):
- `git -C <F> log --oneline -3 origin/main` shows the previous leg's merge;
- `git -C <F> config user.email` is Omid's GitHub noreply address;
- `node scripts/close-lint.mjs` exits 0 and `npm test` is green on `main`;
- `node scripts/reference-check.mjs` exits 0 on this box. A `MISSING file`
  under `sources/` is fetched from its row's URL or copied (Sprint 2's D8);
- the flip map's teeth read as the block says through the close runner
  before the work;
- the ASK-OMID register: any row an earlier leg filed, with its answer if
  any;
- legs 1 and 2: each anchor the leg's story cites still reads as quoted in
  § Inputs swept. Leg 2 reads its `tiered-cost.md` anchors at leg 1's merge,
  and leg 1's edits move the `parts-and-toolchains.md` line numbers.

**Independent checks for the reviewer pass** (as Sprint 3's, with its R1
and R4):
- **leg 1's arithmetic:** every re-priced margin, and each scenario's block
  of Field Generator slots in its frame, reproduces from the study's own
  inputs, by a script;
- **leg 1's layout against its sources:** PSIP p23 and p24 for the clocks,
  and its downlink and hop claims against `radio-and-location.md`'s own
  § Channels and § Zones;
- **the Chorus adopt-and-decline list** of `radio-and-location.md`,
  unchanged by leg 1 unless the leg says why;
- **leg 2's prices:** a sample re-read at their URLs, any change since the
  read date named as the date's, not as an error; and every total summed
  again by a script from its rows;
- **leg 3's tests,** run against `main`'s script before the leg: the drift
  test red, the pin green;
- **the hub's two checks:** `check-register-seats.mjs` takes `--sprint
  'tigerfid — Sprint 4'`, the register's own wording (Sprint 3's R4), and
  `check-item8-dispositions.mjs` reads the legs' bullets. A reader's brief
  names each repo file as `git show <sha>:<path>` whenever the pass edits
  the tree while its readers run (Sprint 3's R1).

**Verified-live at proposal** (2026-10-29, the session above, after its
`date -u` read of 00:33:54Z, on Node v24.22.1):
- **The battery, under the Windows box's lock** (`battery-lock.mjs run`):
  `npm test` held 00:40:39Z–00:40:41Z, exit 0, with `close-lint: ok
  (adopted 2026-10-26)`, `check-licenses: ok` and 20 of 20; `node
  scripts/reference-check.mjs` held 00:40:49Z–00:40:55Z, "299 rows verified,
  README the stub", exit 0.
- **The close runner** at `de0657f` over the eleven teeth, after the read of
  00:41:23Z: nine PASS; `stub-names-sources` FAIL and `rebuild-spec-approved`
  FAIL (exit 1), each for its right reason (the stub names no `sources/`; no
  rebuild spec exists).
- **The test-arming reads**, through `classify()` over each leg's ledger:
  the proposal and legs 1–3 read `docsOnly=false lintOnly=false`; a
  notes-only wrap reads `docsOnly=true`.
- **The parser** at the hub's origin/main (`702c116c`, its `scripts/fleet-view/`
  and `scripts/lib/` extracted to the scratchpad, friction F1), `node
  check-sprint-block.mjs <hub> <plan> 4` over this block as proposed, after
  the read of 00:48:58Z, exit 0:
  `{"found":true,"approvedDate":null,"executed":false,"reviewed":false,"declaredLegs":3,"legs":["2.1.12","2.1.14","2.1.13"],"legsSource":"structured","ambiguous":false,"sweptDate":"2026-10-29","problems":[]}`;
  its `all` mode, `{"blocks":1,"problems":[]}`, exit 0.
- **The parser, as approved:** the same command over a scratch copy with an
  approval pasted into the heading and the label line, exit 0:
  `{"found":true,"approvedDate":"2026-10-29","executed":false,"reviewed":false,"declaredLegs":3,"legs":["2.1.12","2.1.14","2.1.13"],"legsSource":"structured","ambiguous":false,"sweptDate":"2026-10-29","problems":[]}`.
- **This PR's tree,** after the read of 00:49:21Z: `node
  scripts/close-lint.mjs`, `close-lint: ok (adopted 2026-10-26)`; `npm test`
  under the lock (held 00:49:21Z–00:49:24Z), 20 of 20, exit 0; the close
  runner over 5 of the manifest's 28 checks: `lint`, `tiered-cost-doc` and
  `fcc-ids-both-named` PASS, `stub-names-sources` and
  `rebuild-spec-approved` FAIL (exit 1), as above.

**After the sprint:** the last leg's executor stamps the block as executed:
the heading's stamp, the Executed record, and a line saying the review is
pending. It then wraps. Then, in order:
1. the Sprint 4 reviewer pass (FRESH);
2. the decision sitting (Story 2.1.7), Omid's;
3. Epic 2.1's close (Story 2.1.8, a FRESH closer).

## ASK-OMID register

No open rows. A1, answered, moved to the archive with Initiative 1 (KB
`register-relocation-at-close`, 2026-10-27).

## Initiative 2: The design study

**Objective:** turn spec D1–D7 into a design a builder can execute, decided
bottom-up against three acceptance scenarios: a conference where every
attendee wears a tag and the software shows each person on the floorplan; a
hospital nursery where each baby's tag carries a temperature sensor, so
staff can find each baby and read the sensor remotely; and a warehouse
inventorying 100,000+ items in minutes. The study answers spec §3: the
requirements with numbers, how the original worked, the radio and location
architecture with its legality, the parts and free toolchains, the software
stack for Windows, macOS and Linux, and the tiered cost — then Omid's
decision sitting writes the rebuild's design spec in this repo (the tigerian
shape: a research initiative with the design sitting at its end). The
anti-goals bind every answer: nothing a beginner cannot build or debug, no
paid tool or single vendor's cloud, no medical claim, no transmitter a
beginner cannot legally run.
**Milestone 1 (filed by this study's close — spec §4):** a builder with no
experience follows `guides/` and ends with one Reader, one Field Generator
and a sealTag, a compactTag and a cardTag built; the host software running
on their own Windows, macOS or Linux machine; all three tags read together
(anti-collision); a tag carried between two zones, the move showing on the
floorplan; a tamper alarm and a temperature reading arriving; and a tag
paired with a person raising an alarm when it leaves without them. Every
shopping list is tiered (D7). Omid is the first builder (D9): milestone 1 is
accepted when he has built it from the guides, and the guides are revised
from what his build taught before anyone else is pointed at them.
**Done when:** Epic 2.1 is archived with close evidence (initiative-final —
Epic 2.1 is its only epic), and its close has filed the build initiatives.

### Epic 2.1: The study, the decision sitting and the rebuild spec

**Goal:** six study documents under `docs/study/`, each answering one part
of spec §3, then the decision sitting and the approved rebuild spec.
**Ordering:** 2.1.1 → 2.1.2 (Sprint 1 legs 4–5) → 2.1.9 → 2.1.3 → 2.1.4 →
2.1.5 (Sprint 2) → 2.1.10 → 2.1.6 → 2.1.11 (Sprint 3) → 2.1.12 → 2.1.14
(Sprint 4 legs 1–2) → 2.1.7 → 2.1.8, with 2.1.13 (Sprint 4 leg 3) anywhere
before 2.1.8.
Serial since the Sprint 2 proposal (2026-10-27): 2.1.4 names parts for the
radio 2.1.3 chooses, and 2.1.5's reader abstraction follows 2.1.3's link.
The Sprint 2 review (2026-10-28) routes a fix-up of the three studies,
Story 2.1.10, ahead of 2.1.6: its findings can move a cell and the tags'
listening budget (the archive's § Executed sprints › Sprint 2). It routes
F11's message fix, Story 2.1.11, after them.
The Sprint 3 review (2026-10-29) routes a second fix-up, Story 2.1.12,
ahead of the sitting: its findings move the tags' listening budget and two
tiers' totals, which the sitting decides from. It files the reference
folder's fixes, Story 2.1.13, which gate nothing in the study (the
archive's § Executed sprints › Sprint 3).
The Sprint 4 proposal (2026-10-29) splits Story 2.1.12 by the session-fit
test: 2.1.12 keeps the listening window, and Story 2.1.14 takes the tiers'
fixes (§ Sprint 4, The sprint's shape).
Manifest: `docs/close/2.1.json` (born FAILING; each study document's tooth
stays RED until its document lands).
**Done when:** every tooth in `docs/close/2.1.json` passes from the repo root
and its prose checks are adjudicated.

#### Story 2.1.1: The requirements, from the three scenarios, with numbers — ✅ DONE (2026-10-27)

**Completed:** 2026-10-27 — tigerfid #14 (branch `story-2.1.1-requirements`).
**Outcome:** `docs/study/requirements.md` carries 27 numbers. They are the five measures for each of the three scenarios, plus each scenario's size as an assumption, and each has its reasoning and Omid's answer recorded first-hand beside it.
- **The answers.** The list went out after the session terminal's `date -u` read of 11:57:23Z, and his one-message answer came before the read of 12:05:05Z. He accepted 24 numbers as recommended and set three himself:
  - [3]: 1,000 tags/s per reader;
  - [12]: 100 tags/s in the nursery;
  - [13]: a nursery move shown within 5 s.
- **His question on [3]** ("Can we do that?") is answered in the document by the arithmetic, marked INFERRED. It needs a fast link with short replies and reader-assigned slots, or several channels per reader; the original's random delay cannot do it.
- **For Stories 2.1.3–2.1.4:** every input to the arithmetic is marked ASSUMED or INFERRED, for those stories to replace with real parts' figures.
- **The ground rules hold.** No datasheet figure is copied (spec Appendix B). The nursery is framed as finding tags and reading a sensor; every "medical" or "vital" mention is a negation (C8).
- **Story 2.1.3 answers first** the hardest pairing: [3]'s 1,000 replies a second against [2]'s 30 m with the tag worn.

**Story:** As the study, I want each scenario turned into measurable
requirements, so that every design option is judged against numbers, not
impressions.
**Context:** spec §3.1. A number for each of: indoor read range; tags per
second through anti-collision (the warehouse's 100,000+ items in minutes
implies hundreds per second — INFERRED from the arithmetic; the study sets
the number); zone accuracy; alarm latency for tamper, temperature, missed
check-in and pairing; battery life. Each number is put to Omid and his
answer recorded beside it. The old datasheets' figures conflict with each
other (spec Appendix B) and are never copied as requirements. The nursery
scenario proves "find each tag and read its temperature sensor" — never a
medical device or a monitor of vital signs (spec C8).
**Done when:** `docs/study/requirements.md` carries, per scenario, a number,
its reasoning and Omid's recorded answer for each of the five measures.

#### Story 2.1.2: How the original worked — ✅ DONE (2026-10-27)

**Completed:** 2026-10-27 — tigerfid #16 (branch `story-2.1.2-original-system`).
**Outcome:** `docs/study/original-system.md` describes the 2001–2013 system from its records at the tag `activewave-2018`: the radio link, the anti-collision scheme, the zone mechanism, the alarms and I/O, and the hardware of the Reader, the Field Generator and the three tags. Each claim cites the file it was read from or is marked INFERRED, and a script found all 208 of its quoted phrases in their sources.
- **Measured:** the packets' CRC is CRC-16/X.25. It matches the records' three test vectors and their `0xF0B8` residue.
- **No record states the radio's data rate**, its modulation or any tags-per-second figure. The requirements' 250 kbit/s stays ASSUMED, and the receiver's 330 kHz IF filter suggests the original ran slower (INFERRED).
- **Three corrections to the onboarding spec** (Context and Appendix B):
  - the compactTag is Tag 102; Tag 103 is the miniTag;
  - the Reader's radio is the Transceiver daughterboard;
  - the 2012 sealTag is an ISO 15693 logging seal with no active radio and no temperature sensor.
- **For Stories 2.1.3, 2.1.4 and 2.1.7:** milestone 1's sealTag is therefore a new design. The document's last section says why: one core in a seal enclosure, per spec §3.7, is weighed by 2.1.4 and decided by 2.1.7.

**Story:** As the study, I want one written description of the 2001–2013
system, read from its own records, so that every option has a reference to
beat.
**Context:** spec §3.2 and Appendix B, read at the tag (or in the reference
folder): the protocol documents (`api/documents/Protocol20061015.doc`,
`Protocol-tg-07092008.doc`, `AW_API.pdf`, `AW_API_V39.doc`), the Reader 103
and Tag 103 schematics and BOMs, and the sealTag's STM8L firmware source —
the only firmware source in the tree (none exists for the Standard Reader
or the PIC tags). The two-way radio: the Reader or a Field Generator wakes
and commands tags at 433 MHz; tags reply at 916/927 MHz (US) or 868 MHz
(EU) with random-delay anti-collision; a zone is a Field Generator plus a
motion detector.
**Done when:** `docs/study/original-system.md` describes the radio link, the
anti-collision scheme, the zone mechanism, the alarms and I/O, and each
milestone device's hardware, citing for each claim the file at the tag it
was read from; a claim no record supports is marked INFERRED.

#### Story 2.1.9: The Sprint 1 review's two fixes to the original-system study — ✅ DONE (2026-10-27)

**Completed:** 2026-10-27 — tigerfid #29 (branch `story-2.1.9-review-fixes`).
**Outcome:** both findings fixed in the three files, and nothing else moved.
- **Finding 3.** `docs/study/original-system.md` now lists three corrections
  to the onboarding spec, and its disagreement table has a row for each. The
  new one is the Reader's radio, which is on the Transceiver daughterboard.
  § The Reader now points to the list.
  - MEASURED in the reference folder: the Transceiver's pick-and-place names
    the 916.5 MHz SAW filter, the BGA2001 and the SA636 once each. The Reader's
    Rev 6 netlist names no SAW, BGA2001, SA636, NE85619 or BFR193 part.
- **Finding 4.** Three files said the Outlook message waits on ASK-OMID A1:
  the study's § The records read, `docs/reference-research.md` § 1 and
  `docs/reference-repos.md` reading rule 7. Each now says that Omid answered
  A1 "Keep the default" at #7, and that reading rule 7 keeps the file closed.
- **The story's two greps** are quoted in the PR body with each hit's
  disposition. The headings Sprint 2's later legs cite are unchanged, and
  `original-system-doc` stays PASS.
- **Before this leg, its own PR** (#28): a root `.gitattributes` pins LF line
  endings, and this clone's CRLF working tree was repaired (hub #4452;
  friction F4).

**Story:** As a stranger reading the study, I want
`docs/study/original-system.md` to list every correction it makes to the
onboarding spec, and the reference documents to say truly why the Outlook
message stays closed, so that I can trust the records over the spec and
never wait on a ruling already made.
**Context:** born 2026-10-27 by the Sprint 2 proposal, from the Sprint 1
review's findings 3 and 4 (tigerfid #23; the archive's Sprint 1 record),
which routed both to Sprint 2's first study PR.
- **Finding 3.** The document makes three corrections to the onboarding spec
  (hub spec `2026-10-26-tigerfid-onboarding-design`). Its corrections list
  (§ The names: products and boards) and its table (§ Where the records and
  the website disagree) carry two. The third is stated in § The Reader: the
  Reader board carries no radio parts, and the radio is the Transceiver
  daughterboard, where the spec's Context lists an LNA and a SAW front end
  among the Reader 103 sheets' parts. The Story 2.1.2 tick names all three.
- **Finding 4.** Three files say the Outlook message waits on ASK-OMID A1:
  `docs/study/original-system.md` § The records read,
  `docs/reference-research.md` § 1 and `docs/reference-repos.md` reading
  rule 7. Omid answered A1 "Keep the default" at #7, and the row is in the
  archive: the file left the current tree unread and stays at the tag.
  Reading rule 7, with A1's answer, is what keeps it closed.
- **Predicates, not examples** (KB `plan-authoring-for-cold-execution` item
  10, its review face). The fix runs two greps:
  - `grep -n -i 'spec' docs/study/original-system.md`: each hit is either a
    correction, which then appears in both the list and the table, or a
    citation, which stays;
  - `grep -n -E 'A1|rules on it' docs/study/*.md docs/reference-repos.md docs/reference-research.md`:
    each hit is refreshed or named current.
- Nothing here edits the hub spec. The Sprint 1 record cites hub inbox #4436
  as already carrying the three corrections to it.
**Done when:** `docs/study/original-system.md` lists three corrections, each
with a row in its disagreement table; no study or reference document says
the message waits on a ruling; and the PR body quotes both greps with each
hit's disposition (manifest prose check `original-system-review-fixes`).

#### Story 2.1.3: Radio, location and legality — ✅ DONE (2026-10-27)

**Completed:** 2026-10-27 — tigerfid #31 (branch `story-2.1.3-radio-and-location`).
**Outcome:** `docs/study/radio-and-location.md` reads the law first, compares six options against the requirements, and chooses one sub-GHz band on a pre-certified module.
- **The law, read first, each text dated with its URL.** 47 CFR Part 15 at the eCFR's point in time of 2026-10-23: §15.3, §15.5, §15.23, §15.212, §15.231, §15.247, §15.249 and Subpart F. Also the Radio Equipment Directive (consolidated 30.05.2026), Decision 2006/771/EC (consolidated 23.01.2025) and ERC/REC 70-03 (October 2026). The three ETSI standards were read as their version listings only, and are marked so.
- **What the law decided first.** §15.231 rules out the original's 433 MHz wake field in the US for [13]'s 5 s. §15.23 covers milestone 1's five devices and no more, so the system's radio needs its own modular grant. The EU has no home-built route.
- **[3] against [2], answered first (D2):** met, with no ASK-OMID row. TI's CC1312PSIP module (FCC ID ZAT-1312PSIP-2, a single-modular grant under §15.247) runs its certified 300 kbit/s 2-GFSK PHY with reader-assigned slots. That gives about 1,360 slots a second on one channel (INFERRED). [2]'s 30 m worn is met on paper, with 21.8 dB of margin before fading at the worst body loss; the crowded hall is the unmeasured risk.
- **The rest of the choice.** Channel hopping in 902–928 MHz holds under either grant class, since the grant's own record was unread (HTTP 403). In the EU the band is 865–868 MHz at 25 mW e.r.p. under a 1 % duty cycle. Zones come from Field Generators' beacons, and a move is shown within 3 s against [13]'s 5 s (INFERRED). The chosen radio states the bit rate, modulation, output power, certification and wake signal's form that no original record does.
- **Chorus, radio side:** five items, four ADOPTED and the crowd-sourced network DECLINED. ActiveWave's room-level zones, relay outputs and wake-on-command each still win. D5's radio side: a passive HF interface needs no transmitter of its own (INFERRED from §15.3(o)).
- **For Story 2.1.4:** the module grant's record, the parts around the module, and the ASSUMED antenna, slot and start-up figures. Three TI sources are pinned under the corpus's new `sources/` (294 rows).

**Story:** As the study, I want the radio and location architecture chosen
against the scenarios, the law and a beginner's hands, so that the first
build is legal to run and possible to build.
**Context:** spec §3.3, §3.8 and C9. The options: a sub-GHz system-on-chip
in the original's split (a wake channel plus a reply channel); a
single-band design with wake-on-radio; dual-band chips (sub-GHz for
tracking, Bluetooth for phone setup); Bluetooth Long Range, LoRa and UWB as
comparisons. Pre-certified radio modules are preferred wherever they meet
the scenarios, so a beginner's build is legal without equipment
authorisation. The US rules (47 CFR Part 15 — §15.231 at 433 MHz, §15.249
and §15.247 in 902–928 MHz, §15.23 for home-built devices) and their EU
equivalent are verified before any radio is chosen. The Chorus comparison
(spec D12, §3.8), radio side: four layers (tags, readers, server,
dashboard) with "reader" an abstraction any device emitting the same event
schema can fill; simple readers and a smart server; three hardware tiers
(item tag, reusable badge or fob, combined sensor-and-reader); prototyping
on off-the-shelf modules; and, not to copy, a crowd-sourced reader network.
Each item is ADOPTED or DECLINED with its reason, and the study says whether
ActiveWave's room-level zones from a field generator, its relay outputs and
its wake-on-command still win. From the study's first two documents (the
Sprint 1 review, 2026-10-27): this story answers first [3]'s 1,000 tags/s
per reader against [2]'s 30 m with the tag worn, and meets [3] or brings it
back to Omid with its cost; it chooses reader-assigned slots, a faster link
or several channels for [3] and [21], designs how a zone move is noticed
within [13]'s 5 s, and takes from the chosen radio what no original record
states — the bit rate, modulation, output power, certification and the wake
signal's form (the Story 2.1.1 tick; `docs/study/requirements.md` § Ground
rules and items [3], [13] and [21]; `docs/study/original-system.md` § What
no record states).
**Done when:** `docs/study/radio-and-location.md` compares the options
against the requirements, cites each rule above, names the chosen
architecture, and answers its Chorus items one by one.

#### Story 2.1.4: Chips, modules and the free toolchains — ✅ DONE (2026-10-27)

**Completed:** 2026-10-27 — tigerfid #33 (branch `story-2.1.4-parts-and-toolchains`).
**Outcome:** `docs/study/parts-and-toolchains.md` names each device's parts around TI's CC1312PSIP, the free toolchain for each with its install on Windows, macOS and Linux, the debugger, and one core in three enclosures as its recommendation.
- **The parts.**
  - **The Reader:** the module at 17 dBm on a certified whip (+0.90 dBi), a USB bridge of the CDC class so that no platform needs a driver, a 3.3 V regulator, and the original's relays and inputs.
  - **The Field Generator:** the same module and antenna on a USB adapter's 5 V.
  - **The tags:** the module at 12.0 dBm on a certified chip antenna (+1.0 dBi), four layers, a CR2032 with a buffer capacitor, a tool-opened compartment, TI's TMP117 on the nursery and cold-chain tags, and the tamper loop on a wake pin.
- **The toolchains, cited from the vendors' pages, none run by this leg:**
  - KiCad 10.0.7, the SDK 8.33.00.16 from TI's public Git repository, SysConfig, TI Arm Clang, UniFlash, and CCS as an optional IDE, each on all three platforms;
  - TI's cloud tools, SmartRF Studio (Windows only) and IAR (paid) declined.
- **The debugger:** the LP-XDS110ET (59.29 on TI.com, read 2026-10-27), because it measures the current the battery tests need. TI's evaluation terms address professional users, and the sitting should know it.
- **The licences.** TI's reference design and SDK carry TI's own terms, which are not on the allowlist, so nothing is copied. The boards follow SWRA797's written layout rules.
- **The figures replaced (D1).** The cell is rated at 235 mAh, with the 150 mAh planning figure now bounded below at 117.5 mAh. A reply costs 18.09 µC. Listening costs 13.9 µA at 1 s and 1.8 µA at 12 s. Standby is 1.0 µA, and the Reader's antenna is +0.90 dBi. `requirements.md` carries the four dated pointer lines.
- **[27] (D4): the slower wake, a listen every 12 s.** It keeps five years on a CR2032 on paper, with a margin of 5–11 %, and [26] is met at 25 s of 30. A CR2450 alone lasts about 2.9 years at a 1 s wake. The CR2450 is recommended as well, for about 2.5 times the need. No ASK-OMID row.
- **[9] and [18].** The conference badge listens every 2 s and lasts 1.99 years. The nursery's tag holds [18] on paper by 6 %; measure first, then a CR2450 if short.
- **One core or three (D3):** one core, recommended, with the sealTag a new design inside it: the core in a band whose loop wakes it. The sitting decides.
- **Not read:** the module's FCC grant record. The FCC's search and two mirrors refused this leg's requests, and Omid answered "Write it as not read". TI's documents name two IDs, ZAT-1312PSIP-2 and -3, for a browser read before any part is bought. Subpart B is read: the Reader's USB and relays make it a digital device, which §15.23 covers in milestone 1.
- Five sources are pinned under `sources/` (299 rows).

**Story:** As a builder, I want each device's parts and its free firmware
toolchain named, so that I can buy, flash and debug them on my own machine.
**Context:** spec §3.4 and §3.7; D4 (KiCad 10 for the boards), D5
(Windows, macOS and Linux), and the anti-goals (no paid tool, no single
vendor's cloud). For each candidate chip or module: availability, a free
toolchain on all three platforms, a debugger a beginner can afford, and the
licence of any reference design (Story 1.1.4's lane). It weighs §3.7's
question — one electronics core in three enclosures (the seal, the fob, the
card) against three separate boards — a suggestion to weigh, not a
decision. The spec's §7 files §3.7 under 2.1.7: this story weighs it, and
2.1.7's decision sitting decides it. From the study's first two documents
(the Sprint 1 review, 2026-10-27): this story replaces the requirements'
ASSUMED cell, reply-cost and listening figures with the chosen parts'
datasheet figures, chooses for the warehouse item tag ([27]) between a slower
wake and a CR2450-size cell, and weighs milestone 1's sealTag as a new
design inside §3.7's question, because the 2012 sealTag is an ISO 15693
logging seal with no active radio (the Story 2.1.2 tick;
`docs/study/requirements.md` § The arithmetic and item [27];
`docs/study/original-system.md` § What this means for the study).
**Done when:** `docs/study/parts-and-toolchains.md` names the parts per
device, the toolchain per part with its install on each platform, and the
one-core-or-three answer with its reasons.

#### Story 2.1.5: The software stack — ✅ DONE (2026-10-28)

**Completed:** 2026-10-28 — tigerfid #36 (branch `story-2.1.5-software-stack`, pre-tick head `bcf062f5`), with the research log's Story 2.1.5 entry (`docs/reference-research.md` § 4).
**Outcome:** `docs/study/software-stack.md` reads the original host programs at the tag, chooses a small local Python program whose screen is the user's own browser, sketches the event schema and the API, and answers its Chorus items one by one.
- **The original host programs, read at the tag.** Both are Windows only:
  - **Smart Tracker:** C# on .NET Framework 4.0, the API's x86 wrapper, and MySQL through ODBC;
  - **the Programming Station:** C++Builder, building the protocol's packets itself.
  - **What they did:** zones keyed by a Reader and a Field Generator, alarm rules on screen, a history per tag, and access by group and schedule.
  - **What they lacked:** no event carried a time, so the host stamped its local arrival time (263 `DateTime.Now`, no `UtcNow`, MEASURED). There is no missed check-in alarm and no temperature in Smart Tracker's source. The floorplan program has no source in the tree, and the station's encryption keys are compiled in. The Outlook message stays unread.
- **The stack (option A).** Python 3 with its standard library's `sqlite3` and HTTP server, and pyserial (pure Python, BSD-3-Clause) for the Reader's port. The page is plain HTML, CSS and JavaScript, with SVG and Server-Sent Events, and nothing is fetched from the internet. It listens on the user's own machine only in milestone 1.
  - **Measured here, on macOS 26.6.2:** 400,000 rows at one transaction per frame in 0.36 s, and 1,000 streamed events in 16 ms.
  - **Cited:** Windows and Linux.
  - **The runner-up:** Node.js (one language; its `node:sqlite` is a release candidate, and its serial port needs a native add-on).
  - **Declined:** Go, a desktop shell, Web Serial in the page alone, and the original's stack.
- **The event schema:** `tag`, `reader` (a Reader or a listening Field Generator), `via`, `rssi_dbm`, `time` (UTC, from the Reader's frame clock), `seq` and a `payload` with the Field Generators heard. On the wire it is a binary frame, INFERRED from the UART's rate at 1,363 replies a second. The API is HTTP and JSON with a live stream.
- **Chorus, software side:** the five items ADOPTED (share links for the LAN tier, with a vendor's relay declined), both not-to-copy items DECLINED, and the four layers' schema, adopted by Story 2.1.3, written. Owned infrastructure with no subscription, and pairing people with things, still win.
- **D5's software side:** no HF interface on milestone 1's seal (recommended). Web NFC is in Chrome on Android only, and the printed QR code and the radio's tamper alarm already cover the need. The sitting decides it with the seal's design.

**Story:** As a user on any of the three platforms, I want the host
software's stack chosen, so that the floorplan, the alarms and pairing run
on my own machine without a vendor's cloud.
**Context:** spec §3.5, §3.8 and D5. The host runs on Windows, macOS and
Linux, local first (it works offline); the floorplan view; the alarms
(tamper, temperature, missed check-in, pairing); pairing people with
things; an open API. The Chorus comparison (spec D12, §3.8), software side:
an item-history timeline per tag (zones, times, sensor values); alarm rules
set on screen (temperature range, dwell, left a zone, tampered, missed
check-in); "find the nearest available X" by zone; a printed QR code and ID
on every tag, so pairing is scan-and-scan; view-only, time-limited share
links; and, not to copy, a cloud-only service, and free hardware plus a
subscription with printed battery labels. Each item is ADOPTED or DECLINED
with its reason, and the study says whether owned infrastructure with no
subscription and pairing people with things still win. From the study's
second document (the Sprint 1 review, 2026-10-27): this story reads the
original host applications' source, Smart Tracker and the Programming
Station, which Story 2.1.2 left for it (the Outlook message beside them stays
unread under `docs/reference-repos.md` reading rule 7), and, with Story
2.1.3, says whether the rebuild's seal keeps an HF interface for reading by
a phone (`docs/study/original-system.md` § The records read and § What this
means for the study).
**Done when:** `docs/study/software-stack.md` names the stack and why,
sketches the event schema the readers report (tag, reader, signal strength,
time, payload), and answers its Chorus items one by one.

#### Story 2.1.10: The Sprint 2 review's fixes to the three studies — ✅ DONE (2026-10-28)

**Completed:** 2026-10-28 — tigerfid #45 (branch `story-2.1.10-studies-review-fixes`, pre-tick head `d1923d05`), with the research log's Story 2.1.10 entry (`docs/reference-research.md` § 4).
**Outcome:** each of the Sprint 2 review's findings 1–5 and its five nits is corrected in its study, and every changed figure reproduces from the study's own inputs. No requirement number moved (D1).
- **Finding 4, the mechanism (D3): the neighbours named.** `radio-and-location.md` § Zones gains § How a tag hears a Field Generator it has not heard before:
  - the floorplan draws each zone's neighbours, and the Reader names a placed tag's listen set: its zone's slot and its neighbours';
  - a tag with no set listens to every slot until it is placed;
  - weighed against the whole block at every listen (71.9 µA in a room) and a wider listen every second time (42.9 µA). The chosen way keeps a room's tag at 13.9 µA.
  - [13]'s move stays 3.0 s for a drawn neighbour. A link the floorplan misses is shown in 5.0 s, or not while the old zone is still heard, so the guides' setup walk checks the links.
  - ASSUMED, new: a 1.0 ms beacon and a 0.4 ms slot (together the 1.8 ms the study had), and each scenario's neighbours.
- **Findings 3 and 4 in the battery (D2),** `parts-and-toolchains.md`. The sensor is charged at the eight averages its ±0.1 °C is specified at (124 ms, TMP117 p6, p12), and each tag at its listen set. These figures supersede Story 2.1.4's:
  - **[18]:** a room's tag holds by 1.8 % (it was 6 %). A tag in the corridor more than about 37 minutes a day is **unmet on paper** (0.96 years at 2 hours a day). Recommended: measure first, then the CR2450 (2.54 years).
  - **[9]:** it holds in every zone, by 2.3 % in an area of the hall with eight neighbours. A 3 s listen is the route if the measurement falls short.
  - **[27]:** the plain item tag keeps five years on a CR2032 at 12 s, by 4 % (it was 11 %). The cold-chain tag is **unmet on paper** (0.96 of five years; it was 1.05), and the CR2450 is recommended on it (2.25 times the need).
  - Each failure has a route that meets it, so no ASK-OMID row was filed.
  - `requirements.md` gains three dated pointer lines (listening, [18], [27]).
- **Finding 1,** `software-stack.md`: the Decided line and its table row carry Omid's HF ruling (an unfitted footprint on the core, the coil off it), with an amendment paragraph that records both of his answers. Web NFC reads "on Android only": Chrome, with Opera, Samsung Internet, WebView and the Quest Browser mirroring it (MDN's data, read again).
- **Finding 2:** `radio-and-location.md` names ZAT-1312PSIP-3 beside -2 (SWRA797 p14, p17) and gives the grants' read to the sitting. `fcc-ids-both-named` FAIL → PASS.
- **Finding 5:** CMake 4.4.5 (BSD-3-Clause) and GNU make 4.4.1 (GPL-3.0-or-later, from its own licence text) gain their platform and licence cells. SysConfig's and TI Arm Clang's licences are marked not read (their pages state none). The 921,600 baud is ASSUMED beside the module's 3 MBaud, and the binary frame still wins: it fits any bridge from 436,160 baud up.
- **The five nits:** `logTimestamp` (`AWI_API.h` at the tag; none of Smart Tracker's 103 `.cs` files names it); §2.803's last clause; −90 and −93 dBm on the module's own sheet (PSIP p16, p17); the radio study counted the Reader's +2 dBi; 1,363 named as the slot capacity, not [3].
- Story 2.1.7's HF sentence is refreshed. The Item-8 grep refreshed two cross-references: the radio study's § Anti-collision names the Field Generators' block (1,296 reply slots a second with the nursery's 27), and the software study's Chorus row names the CR2450. The friction log has its leg 1 section.

**Story:** As the decision sitting, I want the three technical studies to
carry the Sprint 2 review's corrections, so that I decide on figures that
hold and on the HF ruling Omid gave.
**Context:** born 2026-10-28 by the Sprint 3 proposal, from the Sprint 2
review's findings 1–5 and its five nits (tigerfid #41; the archive's
§ Executed sprints › Sprint 2, its `**Reviewed:**` record). The review
routed the studies' half of each here, ahead of Story 2.1.6. Each item says
what the review MEASURED and what changes.
- **Finding 1, `docs/study/software-stack.md`.**
  - § Decided ahead of the sitting and its table row say "no space reserved"
    for an HF interface on the seal's core. Omid's ruling in the review
    replaces it: the core keeps an unfitted footprint for an NFC memory, its
    I²C link and two antenna pads at the board's edge, and the coil lives in
    the enclosure (Story 2.1.7's Context).
  - § The seal's HF interface says "A web page reads NFC in one browser
    only". MDN's data lists Web NFC in Chrome, Opera, Samsung Internet and
    WebView on Android: still Android only, so milestone 1's no-HF decision
    stands.
  - The two "fitting option" lines are right as they stand.
- **Finding 2, `docs/study/radio-and-location.md`.** It names FCC ID
  ZAT-1312PSIP-2 alone. Its own pinned source points to ZAT-1312PSIP-3
  (SWRA797 p14) and lists the -3 filings (p17). The study gains a pointer to
  both IDs and to the browser read that Story 2.1.7 gives the sitting.
- **Finding 3, `docs/study/parts-and-toolchains.md`.** The TMP117's ±0.1 °C
  is specified at "8 averages, 1-Hz conversion cycle" (TMP117 p6), and its
  factory setting averages eight conversions, 124 ms (p12). The study charges
  one 15.5 ms conversion. At eight averages the review found [18]'s year
  holding by 1.8 %, not 6 %, and [27]'s cold-chain item tag at a 12 s listen
  by 1.4 %, not 5 %. The study either charges the setting that its cited
  accuracy needs, or names the setting it charges and the accuracy that
  setting has.
- **Finding 4, `radio-and-location.md` § Zones and
  `parts-and-toolchains.md` § The battery.** A tag listens only in "the slots
  of the Field Generators it last heard", and no sentence says how it hears
  one it has not heard before. INFERRED by the review: a tag carried into
  such a room never listens in its slot, and both the 3.0 s move and [18]'s
  battery assume it does. The study chooses the mechanism (Sprint 3's D3),
  re-checks the move against [13], and re-prices the listening budget in
  § Each worn tag's year and § [27] (Sprint 3's D2).
- **Finding 5, two figures with no source or mark** (Sprint 2's D10):
  - `parts-and-toolchains.md` § The toolchains: CMake and GNU make carry no
    platform, install or licence entry; TI Arm Clang ("TI's free compiler")
    and SysConfig carry "no price stated on the page". The sentence that
    every tool's platforms are cited except the Arm GNU Toolchain's is then
    untrue. The closer's `free-tools-three-platforms` needs this.
  - `software-stack.md`'s wire-frame table measures the JSON form "against
    921,600 baud", a figure with no source and no ASSUMED mark. The module's
    UART runs at 3 MBaud (PSIP p26), and the USB bridge's limit is unread.
    The study sources or marks the figure and says whether the binary frame
    still wins (Sprint 3's D4).
- **The five nits,** each MEASURED by the review:
  - `software-stack.md` says the original's event "carries no time". Its
    `rfTagEvent_t` has a `logTimestamp` member (`AWI_API.h` at the tag),
    which Smart Tracker's source never reads. What the host lacked was a
    time of hearing.
  - `parts-and-toolchains.md` quotes §2.803(d)(2)(iv)'s notice as ending at
    "FCC license holder". It goes on: "or must secure an experimental
    authorization under part 5 of this chapter." The conclusion stands.
  - `radio-and-location.md` calls a 500 kbit/s link "no worse by the
    tables" at −95 dBm, from the CC1312R7's sheet. The chosen module's own
    sheet prints −90 dBm under the same conditions (PSIP p16). The verdict
    stands: it is declined for its certification.
  - `parts-and-toolchains.md` says the radio study "counted only the sending
    antenna". That study's table counted the Reader's +2 dBi too; the 21.8 dB
    → 20.7 dB is that antenna's change to +0.90 dBi.
  - `software-stack.md` twice calls 1,363 a second "[3]'s". [3] is 1,000
    tags a second; 1,363 is the radio study's slot capacity.
- **Predicates, not examples** (KB `plan-authoring-for-cold-execution` item
  10, its review face). Each grep's hits are quoted in the PR body, each
  with its disposition:

  ```
  grep -n -i -E 'space reserved|reserves no space|one browser' docs/study/*.md docs/project-plan.md
  grep -n 'ZAT-1312PSIP' docs/study/*.md
  grep -n -i -E '15\.5 ms|averag' docs/study/*.md
  grep -n -E '921|222 %' docs/study/*.md
  grep -n -i -E 'CMake|GNU make|no price stated|except the Arm' docs/study/*.md
  grep -n -E "carries no time|\[3\]'s 1,363|no worse by the tables|counted only the sending|FCC license holder" docs/study/*.md
  ```

  And an old-value grep: each figure the re-pricing changes, grepped by its
  old value across `docs/study/*.md` and `docs/project-plan.md`. Each hit is
  refreshed, named current, or named frozen in a DONE story's Outcome.
- **What stays.** No requirement number moves (Sprint 3's D1). A figure that
  a dated pointer line in `requirements.md` quotes, and that this story
  changes, gets a new dated pointer line. The three studies' choices and
  Chorus verdicts stand unless a fix moves one, and the document then says
  so. The Outcomes of Stories 2.1.3–2.1.5 are records and are not edited;
  this story's tick names the figures it supersedes.
**Done when:** each of findings 1–5 and the five nits is corrected in its
document, or its sentence is shown correct with its source; finding 4's
mechanism is stated, with its listening cost carried into every battery
figure it moves; and the PR body quotes the story's greps with each hit's
disposition (manifest tooth `fcc-ids-both-named` and prose check
`studies-review-fixes`).

#### Story 2.1.6: The tiered cost of milestone 1 — ✅ DONE (2026-10-28)

**Completed:** 2026-10-28 — tigerfid #48 (branch `story-2.1.6-tiered-cost`, pre-tick head `c799d0aa`), with the research log's Story 2.1.6 entry (`docs/reference-research.md` § 4).
**Outcome:** `docs/study/tiered-cost.md` prices milestone 1 per device, per build route and per tool tier, with D9's bench and a total per tier. Every price was read on 2026-10-28 from a seller's public page and is dated; shipping and tax are left out.
- **The totals, each tier with its route:** starter with fully assembled boards, 538.83; advanced with partly assembled boards, 1,343.04; pro by hand, 1,916.23. A route-by-tier matrix gives the other pairings.
- **Five prices are not read,** by Omid's word first-hand in this leg's terminal, between its `date -u` reads of 14:42:21Z and 15:59:59Z: "Write all as not read (Recommended)". They are the two W5017 whips, JLCPCB's boards, the module's sourcing at JLCPCB, the pro tier's iron and the tag enclosures. Each total names what it lacks.
- **The devices' parts:** the Reader 39.87 and the Field Generator 28.99, each without its antenna; each tag core 13.31, without its enclosure. All devices come to 117.64 by hand, or 129.16 assembled, since an assembled order fits the sensor to every core.
- **Representative parts are marked:** the connectors, the USB bridge, the regulator, the relays and their driver, the passives, the cells and holders, the enclosures and the supplies.
- **Both cells are priced:** a CR2032 and its holder at 1.38 a tag, a CR2450 and its holder at 0.95.
- **The bench:** the tinySA (100 kHz to 960 MHz), or the Ultra Plus for pro, and the development board. The RTL-SDR V4L is the cheaper receiver, outside the totals until its range is read. The barcode scanner is optional and not read.
- **Recommended:** the starter tier with fully assembled boards. The EU build's differences are named, not priced. The three studies' hand-off lines now point to the document.

**Story:** As a beginner, I want the first build priced in starter, advanced
and pro tiers, so that I can choose what to spend before I start.
**Context:** spec §3.6, D7 and D9. Priced per device (the Reader, the Field
Generator, the three tags); per build route (hand-solder, partly assembled,
fully assembled); per tool tier (starter, advanced, pro — several ways to
the same end, for example several soldering irons); and the bench D9 needs
(a tool tier plus a cheap radio receiver to see what the boards transmit).
Every price is dated. This is the rebuild's first real budget. From the
software study (Story 2.1.5, 2026-10-28): the host software costs nothing
(Python, pyserial, SQLite and the browsers), and a USB barcode scanner for
scan-and-scan pairing is an optional bench item to price
(`docs/study/software-stack.md` § What the choice leaves to later stories).
From the radio and parts studies (the Sprint 2 review, 2026-10-28): the
build routes price the module's 0.5 mm-pitch land grid, which favours an
assembled board, and one board in larger quantity for the assembly service;
the advanced tool tier carries the vector network analyser
(`docs/study/radio-and-location.md` and `docs/study/parts-and-toolchains.md`,
each § What the choice leaves to later stories). A price page that refuses a
plain request, as the FCC's, FTDI's and Microchip's pages did for the study
(friction F8 and F13 of Sprint 2), is put to Omid early in the leg for a
browser read, or written as not read. From the Sprint 3 proposal
(2026-10-28), the block's D5–D8 bind the prices: US dollars from sellers'
public pages, each cited with its URL and read date; both cells wherever the
sitting chooses between them; a part the studies leave unnamed priced as a
representative part, marked so; the refused pages put to Omid in one
message; and the US build priced, with the parts that differ for the EU
named.
**Done when:** `docs/study/tiered-cost.md` carries the tables with a date on
every price and a total per tier.

#### Story 2.1.11: The reference check names the fetch route for the study's sources — ✅ DONE (2026-10-28)

**Completed:** 2026-10-28 — tigerfid #50 (branch `story-2.1.11-sources-fetch-route`, pre-tick head `e663b8fd`).
**Outcome:** `scripts/reference-check.mjs` names the fetch route for the study's sources. The tool still never fetches a file row (Sprint 3's D11).
- **A missing `sources/` file:** "fetch it from its row's URL, <the URL>; it counts only once its sha256 matches the pin. Or copy it, never move it, from a machine whose folder verifies".
- **An absent `sources/` folder:** the same route, then one line per file with its row's URL.
- **Every other folder keeps its copy-only line,** and so does a `sources/` row with no URL.
- **Three tests pin the cases** in `scripts/reference-check.test.mjs`. The first two failed against `main`'s script. The third pins the unchanged lines, so it passed there by design. `npm test` reads 20 of 20.
- `docs/reference-repos.md` § Verifying a transfer names the route. The folder README's stub still says "the other two folders travel by copy only", stale since Story 2.1.3 added `sources/`. It is left for the review (friction F17): editing the stub makes every machine's folder README a finding until `--write-stub` runs there.

**Story:** As a builder setting up the reference folder on a new machine, I
want the reference check to say that a missing `sources/` file may be
fetched from its row's URL, so that I follow the corpus's own rule instead of
looking for a machine to copy it from.
**Context:** born 2026-10-28 by the Sprint 3 proposal, from Sprint 2's
friction F11 (leg 2) and F18 (leg 4), which the review routed here as a
story of its own (the archive's Sprint 2 record). MEASURED on `main`
(`9cb0127`):
- **What the check prints.** `scripts/reference-check.mjs` prints "(copy-only
  — copy it, never move it, from a machine whose folder verifies)" for every
  missing file row, and "copy-only: copy the folder whole, never move it, …"
  for an absent folder. Its header comment says "File rows are copy-only:
  the tool never fetches one".
- **What the corpus's rule allows** for `sources/`:
  - the header comment of `docs/reference-corpus.tsv`: "copy-only (never
    re-fetched), except sources/, which may be fetched again from its url and
    counts only once its bytes match the pin";
  - `docs/reference-repos.md` § `sources/`: "On another machine: fetch each
    file from its row's URL, or copy it from a machine whose folder
    verifies. A fetched file counts only once its sha256 matches".
- **The fix is the messages, not a fetcher** (Sprint 3's D11). The tool
  still never fetches a file row (`docs/reference-repos.md`: "The tool never
  fetches a file row").
- **The tests,** in `scripts/reference-check.test.mjs`, written first and
  seen red against `main`'s script:
  - a missing `sources/` file names its row's URL, the sha256 rule and the
    copy route;
  - an absent `sources/` folder names the fetch route for its files;
  - a missing file in another folder keeps the copy-only line.
- `docs/ci-contract.md` needs no change: `test` already runs `node --test
  scripts/reference-check.test.mjs`.
**Done when:** the check's output for a missing `sources/` file and for an
absent `sources/` folder names the fetch route, with the sha256 rule and the
copy route as the alternative; every other file row's line is unchanged; and
the tests above pass in `npm test` after failing before the fix, the PR body
quoting the red run (manifest prose check `reference-check-sources-route`).

#### Story 2.1.12: The Sprint 3 review's fixes to the listening window — ✅ DONE (2026-10-29)

**Completed:** 2026-10-29 — tigerfid #58 (branch `story-2.1.12-listening-window`, pre-tick head `f09c8d97`), with the research log's Story 2.1.12 entry (`docs/reference-research.md` § 4).
**Outcome:** finding 1's layout is stated, its cost carried into every battery figure it moves, and Sprint 3's D2 re-run for [9], [18] and [27]: each is met on paper. No requirement number moved (D1). These figures supersede Story 2.1.10's:
- **The layout (D3): the beacon skipped on most listens** (`radio-and-location.md` § Zones, The layout of a listen):
  - a tag listens to its set's slots alone, keeping time on its crystal, with each Field Generator slot it hears setting it again;
  - it wakes once for each run of its set in a block ordered along each floorplan;
  - it hears the Reader's beacon after each check-in, when its Field Generators' slots flag news, and when it is lost.
  - Weighed against a sync before each group (no gain for its cost) and the review's second wake (16.73 µA, [18] unmet).
- **[18]:** a room's tag holds by 24 % (it was 1.8 %). A tag in the corridor more than about 5.4 hours a day is unmet on paper (it was 37 minutes). The routes stand. D2's clause for a baby's band does not fire.
- **[9]:** the hall's badge holds by 22 % (it was 2.3 %).
- **[27] at 12 s on a CR2032:** the plain item tag holds by 22 % (it was 4 %), and the cold-chain tag by 11 % (it was 0.96 of five years). The CR2450 stays recommended for margin, cold chain first. No ASK-OMID row is filed.
- **The anti-collision nit:** the conference's block of 23 Field Generators (10.2 ms) leaves 1,308 reply slots a second: 1,000 tags in about 0.76 s, beside the ward's 1,296. Any Reader keeps [3] with up to 164.
- **Finding 6 (D4):** GNU's make page reset the connection again. So the sentence above the toolchains table names GNU make, with the Arm GNU Toolchain and the SDK, among the tools whose platforms are not cited.
- `requirements.md` gains three dated pointer lines (listening, [18], [27]). A command now reaches a tag in at most 1.25 s at a 1 s listen (§ Does ActiveWave's approach still win). `tiered-cost.md` § Both cells reads true as it stands, so no sentence of it moves for leg 2. The friction log has its leg 1 section.

**Story:** As the decision sitting, I want the tags' battery figures to rest
on a listening layout that every tag can keep, so that I decide the cells
from margins that hold.
**Context:** born 2026-10-29 by the Sprint 3 review, from its findings 1–6
and its five nits (the archive's § Executed sprints › Sprint 3, its
`**Reviewed:**` record). It goes before Story 2.1.7: finding 1 moves the
margins that Sprint 3's D2 judged. Split the same day by the Sprint 4
proposal, by hub KB `sprint-proposal-standard` step 3's session-fit test:
this story keeps finding 1, finding 6 and the anti-collision nit, and Story
2.1.14 takes findings 2–5 and the other four nits, the tiers' fixes, as the
review wrote them. Each item says what the review MEASURED and what is
INFERRED.
- **Finding 1, the listening window** (`docs/study/radio-and-location.md`
  § How a tag hears a Field Generator it has not heard before;
  `docs/study/parts-and-toolchains.md` § The battery, § Each worn tag's year
  and § [27]).
  - MEASURED, the studies' text: each listen is charged one start-up and
    one window, "the Reader's beacon, 1.0 ms, and 0.4 ms for each Field
    Generator slot in the tag's listen set". The slots "sit in one block
    after the Reader's beacon", ASSUMED ordered "so that a tag hears its set
    in one window".
  - INFERRED: one block after one beacon can place only one zone's set next
    to the beacon. Every other tag wakes twice (the beacon, then its set) or
    listens across the slots between them. With the second wake timed from
    the beacon just heard, the nursery's room tag needs 171.9 mAh a year,
    0.87 years from 150 mAh, where the study prints 147.4 mAh and holds [18]
    by 1.8 %. The hall's badge ([9], 2.3 %) and the plain item tag ([27],
    4 %) lose their margins the same way.
  - The study either states a layout that keeps one window a listen (for
    example, a tag that skips the beacon on most listens, or a short sync
    before each group of slots) with its cost, or charges the second wake.
    Either way it re-runs Sprint 3's D2 for [9], [18] and [27].
- **Finding 6, GNU make's platforms** (`parts-and-toolchains.md` § The
  toolchains). MEASURED: its row marks its three platforms INFERRED, while
  the sentence above the table says every tool's platforms are cited except
  two (the Arm GNU Toolchain and the SDK). The closer's
  `three-platform-claims-sourced` reads that sentence.
- **The anti-collision nit,** MEASURED: `radio-and-location.md`
  § Anti-collision still computes "1,000 tags in 0.73 s" on 1,363 slots. The
  conference's own block of Field Generator slots is never computed. It
  stays with this story because the layout finding 1 chooses sets the
  block's length (the Sprint 4 proposal).
- **What stays.** No requirement number moves (Sprint 3's D1). A figure that
  a dated pointer line in `requirements.md` quotes, and that this story
  changes, gets a new dated pointer line ([18]'s quotes the 1.8 %). The
  Outcomes of Stories 2.1.4 and 2.1.10 are records and are not edited; this
  story's tick names the figures it supersedes.
**Done when:** findings 1 and 6 and the anti-collision nit are each corrected
in its document, or its sentence is shown correct with its source; finding
1's layout is stated, with its cost carried into every battery figure it
moves and Sprint 3's D2 re-run for every margin that fails on paper; the
conference's block of Field Generator slots is computed beside the ward's;
and the PR body quotes each finding's grep with each hit's disposition
(manifest prose check `sprint3-review-fixes`).

#### Story 2.1.13: The reference folder gives the `sources/` route — ✅ DONE (2026-10-29)

**Completed:** 2026-10-29 — tigerfid #62 (branch `story-2.1.13-sources-route`, pre-tick head `325aec17`).
**Outcome:** the reference folder's README stub, the check's drift line and the reading rules give the `sources/` route. The tool still never fetches a file row (Sprint 3's D11).
- **The stub** (`docs/reference-readme-stub.md`) lists `sources/` under "What is here" and says how each folder travels: `activewave-2018/` by `--recreate`; `activewaveinc/` and `chorus/` by copy only; each `sources/` file fetched from its row's URL, counting only once its sha256 matches the pin, or copied. "The other two folders" is gone, and `stub-names-sources` flips FAIL → PASS.
- **The drift line:** a `sources/` file with a URL whose bytes are not the pin reads "its sha256 is not the pin. If it was fetched from its row's URL, <the URL>, that URL now serves a newer revision: copy the pinned bytes instead, never move them, from a machine whose folder verifies". Every other folder's line, and a `sources/` row with no URL, is unchanged.
- **Two tests** in `scripts/reference-check.test.mjs` pin it. The drift test failed against `main`'s script; the pin (the other lines unchanged) passed there by design (Sprint 3's F18). `npm test` reads 22 of 22.
- **Reading rule 8** in `docs/reference-repos.md` names `pdftotext -layout -enc UTF-8 -f <first> -l <last> <file> -` as the Windows route to a PDF's pages. Git for Windows 2.56.0 ships `pdftotext` 4.06 and no `pdftoppm` (MEASURED 2026-10-29). § `sources/` and § Verifying a transfer name the new drift line.
- **`--write-stub` ran on this leg's machine:** the check read `README stale-stub` and printed "next: node scripts/reference-check.mjs --write-stub"; after the command, "299 rows verified, README the stub". The other machine's README reads `stale-stub` until that command runs there (D13). The friction log has its leg 3 section.

**Story:** As a builder setting up the reference folder on a new machine, I
want the folder's README, the check's drift line and the reading rules to
give the `sources/` route, so that the folder tells me what the repo does.
**Context:** born 2026-10-29 by the Sprint 3 review, from Sprint 3's friction
F17, F20 and F6, which the review routes here as one story (the archive's
Sprint 3 record). MEASURED at `dbc2c1c0`:
- **The stub.** `docs/reference-readme-stub.md` lists `activewave-2018/`,
  `activewaveinc/` and `chorus/` under "What is here", and not `sources/`.
  It ends "the other two folders travel by copy only". Story 2.1.3 added
  `sources/` (2026-10-27), whose files may be fetched from their rows' URLs
  (`docs/reference-repos.md` § `sources/`).
- **The drift line.** `scripts/reference-check.mjs` prints `DRIFT   file
  <name> — its sha256 is not the pin` for every file row. For a `sources/`
  file fetched from its URL, § `sources/` reads a mismatch as a newer
  revision behind the same URL, and copies the pinned bytes instead. The
  line says neither.
- **The PDF route.** Sprint 3's leg 1 could not open the pinned datasheets'
  pages with the harness's file reader, which needs `pdftoppm`, and read
  them with Git for Windows' `pdftotext -layout`, counting form feeds for
  the page (F6). The review MEASURED `pdftoppm` absent and `pdftotext` 4.06
  present on the Windows box. The reading rules name no route.
- **The stub's cost.** The check compares each machine's folder README with
  the stub byte for byte, so each machine reads `README stale-stub` until
  `node scripts/reference-check.mjs --write-stub` runs there. The check
  prints that next step itself.
- **The tests,** in `scripts/reference-check.test.mjs`, written first and
  seen red against the script before the fix: a `sources/` file whose bytes
  are not the pin names the newer revision and the copy route; a drifted
  file in any other folder keeps its line. The manifest tooth tests the
  stub.
- The tool still never fetches a file row (Sprint 3's D11).
**Done when:** the stub names `sources/` with its route and no longer counts
"the other two folders"; a drifted `sources/` file's line names the newer
revision and the copy route, and every other folder's drift line is
unchanged, the tests above passing in `npm test` after failing before the
fix; the reading rules name `pdftotext -layout` as the Windows route to a
PDF's pages; and `--write-stub` has run on the machine of the story's
session, the other box's README left to the check's own prompt there
(manifest tooth `stub-names-sources` and prose check
`reference-folder-sources-route`).

#### Story 2.1.14: The Sprint 3 review's fixes to the tiered cost — ✅ DONE (2026-10-29)

**Completed:** 2026-10-29 — tigerfid #60 (branch `story-2.1.14-tier-fixes`, pre-tick head `11f55d14`), with the research log's Story 2.1.14 entry (`docs/reference-research.md` § 4).
**Outcome:** findings 2–5 and the four nits are corrected in `docs/study/tiered-cost.md`, and in `parts-and-toolchains.md` § The debugger for the LP-XDS110 nit. Every total is summed again, each figure reproducing from the study's rows by a script. No requirement number moved (D1). These figures supersede Story 2.1.6's:
- **Finding 2, the hot plate's supply:** a 100 W USB-C PD supply, 50.00 (Acroname, representative), in the advanced and pro tiers. The plate's kit holds its 5 A cable and no supply (Adafruit's page, read 2026-10-29).
- **Finding 3, the debugger's connection:** 29.80 in every tier, each part representative: a 10-pin 1.27 mm cable to the Reader's and the Field Generator's headers, and for the tags' pads a pogo-pin clip, an SWD breakout board and jumper wires. TI's box list holds no cable for the 10-pin connector. The parts study's Debug row names the clip, and Story 2.1.4's "not read" sentence gains the read on the coordinator's GRANT (the sentence lay outside the ledger's pointer line).
- **Finding 4, JLCPCB's minimum:** two assembled boards a design (its capabilities page, read 2026-10-29). Each assembled route buys a second Reader and Field Generator, 27.08 partly and 33.62 fully assembled.
- **Finding 5, three tag boards:** 34.88 by hand, 23.25 more than one core (the study said "about the same"). On the assembled routes, at the minimum, they cost 109.67 and 117.12 more (it said 56.50 and 62.65).
- **The tiers:** 198.08, 1,071.42 and 1,581.42 without the pro iron (were 168.28, 991.62 and 1,501.62). **The totals:** 602.25, 1,449.92 and 1,996.03 (were 538.83, 1,343.04 and 1,916.23), with the route-by-tier matrix summed again.
- **The nits:** partly assembled boards save 25.66, the fees' 19.12 (it said 19.13) and the minimum's 6.54. "Fetched once" now names LCSC's second reads. The ceramic tweezers and the SAC305 wire read "in place of". The LP-XDS110, 35.09, is in the study, and the parts study's pointer line names what each story dated.
- **Named in the Lacks, not priced:** the Fluke 117's battery and the plain tinySA's antenna if not in their boxes, and the VNA's cable to a board. Two pages refused and were not needed (Tag-Connect, USBGear), so no D7 message went and no ASK-OMID row is filed. § Both cells reads true, as leg 1 found. The friction log has its leg 2 section.

**Story:** As the decision sitting, I want each tier's total to rest on a
budget that can build what it lists, so that I choose a tier from totals
that hold.
**Context:** born 2026-10-29 by the Sprint 4 proposal, split from Story
2.1.12 by hub KB `sprint-proposal-standard` step 3's session-fit test. It
carries the Sprint 3 review's findings 2–5 and four of its five nits (the
archive's § Executed sprints › Sprint 3, its `**Reviewed:**` record), moved
from 2.1.12 as the review wrote them. It goes before Story 2.1.7: findings
2–4 move the tiers' totals and their Lacks, and finding 5 the cost of the
choice between one core and three boards, which the sitting makes. Each
item says what the review MEASURED and what is INFERRED.
- **Finding 2, the hot plate's supply** (`docs/study/tiered-cost.md` § The
  tool tiers). MEASURED: the MHP50-B5's page says "Note that a 20V 5A USB PD
  power supply is not included!" and "You'll need to pick up a 100W 'laptop'
  power supply with USB type C PD support" (ADA-5903, read 2026-10-29). The
  advanced and pro tiers carry the plate and no such supply, where the
  document says "Each tier holds what its route needs".
- **Finding 3, the debugger's connection to the boards** (`tiered-cost.md`
  § The tool tiers; `parts-and-toolchains.md` § The debugger and § The tags).
  MEASURED: TI's LP-XDS110ET page lists "LaunchPad development kit", "USB-A
  to USB-C cable" and "Quick start guide" (read 2026-10-29). No cable for its
  "10-pin XDS110 out connector" comes in the box. No tier prices one, or the
  tags' adapter or pogo-pin fixture that the parts study names, and no Lacks
  column names either.
- **Finding 4, JLCPCB's minimum assembled quantity** (`tiered-cost.md` § The
  build routes). The document says it is not read, but no Lacks column names
  it. INFERRED: a minimum above one board adds a module to the Reader's and
  the Field Generator's assembled orders.
- **Finding 5, three tag boards on the hand-solder route** (`tiered-cost.md`
  § How the build is counted). It says three boards "cost about the same,
  since OSH Park prices by area". INFERRED from the document's own quote
  ("$10 per square inch, which includes three copies of your design"): each
  design is paid for, so three designs cost 3 × 11.63 = 34.88, not 11.63.
- **Four of the five nits,** each MEASURED:
  - `tiered-cost.md` says partly assembled boards "would save 19.13". The
    fees differ by 19.12, and the document sums before rounding.
  - It says "Each page was fetched once", and its list of refused pages
    says LCSC's were read again, 15 s apart.
  - The advanced tier's ceramic tweezers replace the ESD pair (991.62
    reproduces only so), though its row says "with".
  - `parts-and-toolchains.md` § The debugger says Story 2.1.6 read all three
    TI tools again; `tiered-cost.md` carries no LP-XDS110 price.
- **From Story 2.1.12** (the forward seam): `tiered-cost.md` § Both cells,
  where the sitting chooses, leaves the nursery tag's cell to the sitting
  "if the measurement falls short". If 2.1.12 moves that recommendation,
  this story refreshes the sentence.
- **What stays.** No requirement number moves (Sprint 3's D1). A price added
  or read again is dated, with its seller's page (Sprint 3's D5). A page that
  refuses a plain request is written not read and named in its tier's Lacks
  (D7). The Outcomes of Stories 2.1.4, 2.1.6 and 2.1.10 are records and are
  not edited; this story's tick names the figures it supersedes.
**Done when:** each of findings 2–5 and the four nits is corrected in its
document, or its sentence is shown correct with its source; every tier that
lists a tool which needs another part to run (a supply, a cable, a fixture)
prices that part or names it in its Lacks; JLCPCB's minimum assembled
quantity is read, or named in the Lacks of each assembled route; and the PR
body quotes each finding's grep with each hit's disposition (manifest prose
check `sprint3-review-tier-fixes`).

#### Story 2.1.7: The decision sitting and the rebuild spec

**Story:** As Omid, I want to decide the design in one sitting from the
study's documents, so that the rebuild has an approved spec to build from.
**Context:** spec §3.9. The six study documents are the sitting's input; its
output is the rebuild's design spec in this repo, at
`docs/superpowers/specs/<date>-rebuild-design.md`, with Omid's approval line
and milestone 1 (Initiative 2) as its acceptance target. From the study so
far (the Sprint 1 review, 2026-10-27): the sitting also decides milestone 1's
sealTag design, which Story 2.1.4 weighs (`docs/study/original-system.md`
§ What this means for the study), and the spec names the rebuild's top-level
layout, which Story 1.1.2 left to the study with a starting shape of
`hardware/`, `firmware/`, `software/`, `guides/` and `docs/` (#7; the
archive's Story 1.1.2 Context). From the software study (Story 2.1.5,
2026-10-28): its three items for the sitting were **decided by Omid ahead
of it**, first-hand in the terminal of the session "tigerfid Sprint 2 leg 4",
between its `date -u` reads of 00:39:20Z and 00:42:52Z: "I like all those
recommendations, let's go with those."
- **The host is Python.** Node.js, the runner-up, is declined.
- **Milestone 1's seal has no HF interface** for a phone (D5). Its core
  reserves an unfitted footprint for one, with the coil off the core, by
  Omid's later word below; #39 had recorded "the core reserves no space for
  one".
- **Raw check-ins are kept 7 days by default.** The history (zone moves,
  alarms and their acknowledgements, sensor readings) is kept until the owner
  deletes it, and the setting sits on screen beside the disk use.

The sitting carries the three into the rebuild spec. The study records them
with their basis (`docs/study/software-stack.md` § Decided ahead of the
sitting).

From the Sprint 2 review (2026-10-28):
- **The seal's HF footprint, decided by Omid** first-hand in the terminal of
  the session "tigerfid Sprint 2 review", between its `date -u` reads of
  10:01:36Z and 10:27:02Z: "Reserve footprint, coil off-core
  (Recommended)". The core keeps an unfitted footprint for an NFC memory, its
  I²C link and two antenna pads at the board's edge. The coil lives in the
  enclosure, away from the module's antenna and its ground plane (SWRA797
  §3.1.2). Milestone 1 fits none of it. Story 2.1.10 carried the ruling into
  the study's Decided line and its table row (2026-10-28;
  `docs/study/software-stack.md` § The seal's HF interface and § Decided
  ahead of the sitting).
- **The FCC grant read is the sitting's,** by Omid's hand in a browser: the
  grant pages of both FCC IDs in TI's documents, ZAT-1312PSIP-2 and -3,
  before the spec names an FCC ID or any part is bought
  (`docs/study/parts-and-toolchains.md` § What was read, and what could not
  be).
- **The parts study's open items are the sitting's too:** the item tag's
  cell, the nursery tag's cell if the measurement falls short, and the
  sensor's accuracy (`docs/study/parts-and-toolchains.md` § What the choice
  leaves to later stories).
- **The spec carries each study's build-initiative items** from its § What
  the choice leaves to later stories, so that the close (Story 2.1.8) files
  them.
**Done when:** the spec exists at that path and carries `**Approved by:** Omid`
with the date.

#### Story 2.1.8: Close out Epic 2.1

**Story:** As the fresh-session closer, I want Epic 2.1 closed per KB
`epic-close-protocol` and the build initiatives filed, so that the rebuild
has its next plan.
**Context:** `node ../../scripts/close/close-epic.mjs --epic 2.1` from the
repo root, in a session that did none of the epic's work. Initiative-final.
The close files the build initiatives from the approved spec (spec §4):
hardware in KiCad 10, firmware, software, guides, and milestone 1's two-zone
demonstration — with D9's "set up the bench" story first (a tool tier plus
a cheap radio receiver to see what the boards transmit). Every hardware
story is proven by Omid's hand (D9): `requires: omid` teeth plus committed,
public-safe evidence — photos, measurements and the receiver's captures.
**Done when:** every manifest check passes or is adjudicated, the archive
entry carries the evidence block, Initiative 2 is cut whole, and the build
initiatives are filed in the same PR.
