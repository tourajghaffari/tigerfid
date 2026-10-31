# Project plan — tigerfid

TigeRFID: the ActiveWave active-RFID system — boards, firmware and software
built from 2001 to 2013 — that Touraj Ghaffari's sons open-sourced under
Apache-2.0 in 2017–2018 (the dedication in `readme.md`), and its rebuild
from the ground up. The 2018 state is kept byte for byte at the annotated
tag `activewave-2018` (`f25ff13f3fb672d8ec91f1fadc8894d69f781ed6`) and its
GitHub release; nothing in the history is rewritten. Initiatives 1 and 2 are
closed, and their records are in `docs/archives/project-plan-completed.md`:
Initiative 1 joined the repo to the fleet and cleared the current tree for
the rebuild (2026-10-27), and Initiative 2, the design study, decided the new
system in Omid's decision sitting of 2026-10-30 (closed 2026-10-31; the rebuild's design spec,
`docs/superpowers/specs/2026-10-30-rebuild-design.md`). Its close filed the
build as Initiatives 3–7: the hardware, the firmware, the host software, the
guides and milestone 1, one Reader, one Field Generator and three tags across
two zones, which a complete beginner can follow on Windows, macOS or Linux
with free tools only.
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

## ASK-OMID register

No open rows. A1, answered, moved to the archive with Initiative 1 (KB
`register-relocation-at-close`, 2026-10-27).

## Initiative 3: The hardware, in KiCad 10

**Objective:** the bench set up first (spec D9), then the Reader, the Field
Generator and the tag core drawn in KiCad 10, built, brought up and measured
by Omid's hand, from the rebuild's design spec
(`docs/superpowers/specs/2026-10-30-rebuild-design.md`, § 5.4, § 5.5, § 6.2,
§ 8 and § 9). Filed 2026-10-30 by Epic 2.1's close (Story 2.1.8), with
Initiatives 4–7.
**The build's order (all five initiatives):** Story 3.1.1, the bench, comes
first. The firmware (Initiative 4) starts on the bench's development board,
and the host software (Initiative 5) on recorded or simulated frames, while
gate G1 waits on TI. No board is drawn before gates G1 and G2 clear
(Story 3.1.2), no board is ordered before gates G3 and G4 (Stories 3.1.3 and
3.1.9), and no firmware image is published before gate G6 (Story 4.1.1).
The guides (Initiative 6) are written from each story's records, and
milestone 1 (Initiative 7) is the acceptance.
**Evidence and gate records (every build initiative):**
- **Evidence:** a story proven by Omid's hand commits its evidence under
  `docs/evidence/<story id>/`: the photos, measurements and the receiver's
  captures, with a `README.md` naming each file, what it shows, the date it
  was taken and the instrument. The evidence is public, so it shows no face,
  no address or hostname, and no screen beyond the measurement (CLAUDE.md
  § Writing in public).
- **Gates:** each gate of the spec's § 8 is recorded in `docs/build/gates.md`
  on a line of its own, `**G<n>:** CLEARED (<date>)` or
  `**G<n>:** RETURNED TO OMID (<date>)`, with its source beside it (TI's
  written answer, the rule read, Omid's ruling). A gate story ends at the
  decision and its record (hub KB `plan-authoring-for-cold-execution`
  item 14).
**Done when:** the Reader, the Field Generator and the tag core with its
CR2450 item variant are drawn in KiCad 10, built and measured, with Omid's
evidence committed, and Epic 3.1 is archived with close evidence
(initiative-final while Epic 3.1 is the initiative's only epic).

### Epic 3.1: The bench, the gates and the boards

**Goal:** a bench that sees what the boards transmit, the gates cleared,
and the five devices' boards drawn, built, matched and measured.
**Ordering:** 3.1.1 → 3.1.2 and 3.1.3 (asked at once; G1's answer gates
every drawing) → 3.1.4 → 3.1.5, 3.1.6, 3.1.7 and 3.1.8 → 3.1.9 → 3.1.10 →
3.1.11 → 3.1.13, with 3.1.12 after Story 7.1.1's range test, before the
close. 3.1.4 may start before G1 clears: it reads datasheets and draws
nothing.
Manifest: `docs/close/3.1.json` (born FAILING; each story's evidence tooth
stays RED until its evidence lands).
**Done when:** every tooth in `docs/close/3.1.json` passes from the repo root
and its prose checks are adjudicated.

#### Story 3.1.1: Set up the bench

**Story:** As the first builder, I want the bench bought, set up and seen to
work, so that every later board is built and checked with tools that can
see what it transmits.
**Context:** spec D9 (a tool tier plus a cheap radio receiver), decision 8a
(the advanced tier; the spec's § 6.2) and § 9's cost-study items
(`docs/study/tiered-cost.md` § The bench, § The tool tiers, § What the
choice leaves to later stories).
- **Prices read again before anything is bought** (gate G4's arm for the
  bench): every price this story pays is read again on its seller's page
  and dated, and a changed price is written beside the old one in
  `tiered-cost.md`.
- **The receiver:** the tinySA, or the RTL-SDR Blog V4L once its datasheet
  shows it covers 902–928 MHz; the choice and its reason recorded.
- **The development board** (LP-EM-CC1312PSIP) and the LP-XDS110ET, each
  with the cable and the pogo-pin clip the tiers name; the development
  board's FCC status read (the spec's § 9, open for the build), and TI's
  evaluation terms kept in view: the board is a bench tool, never part of
  a device (§ 6.2).
- **The first capture:** the development board transmits a TI SDK example
  (fetched from TI, never copied in), and the receiver shows it in
  902–928 MHz. The capture is committed.
- **For the hand-solder route's guides:** the stencil, the paste against the
  module's reflow profile, and how a tag's board meets the VNA, each read
  and recorded for Story 6.1.3. Omid's own build is fully assembled
  (decision 8a).
**Done when:** the bench's photo, the receiver's first capture and the
re-read prices are committed under `docs/evidence/3.1.1/` with their
README, the receiver's choice and the hand-solder items are recorded there,
and Omid has confirmed the bench works (manifest tooth `bench-evidence` and
prose check `bench-by-omids-hand`).

#### Story 3.1.2: Gates G1 and G2: TI's written answer

**Story:** As the build, I want TI's written answer on which FCC grant
covers the 300 kbit/s PHY, and where its trace-reference-design
instructions are, so that no board is drawn on a radio mode or a layout the
grant does not cover.
**Context:** decisions 2 and 12, and the spec's § 5.2 and § 8 (gates G1 and
G2). Omid posts the spec's question to TI, its four parts as written in
§ 5.2, on TI's public E2E forum or through TI's support; the post and every
reply are linked from `docs/build/gates.md`.
- **Posted, 2026-10-30:** "I posted it on E2E with CC1312PSIP" [Omid's
  words as relayed by the session "Windows 5", which had them first-hand
  in its terminal before its `date -u` read of 22:41:19Z]. The question is
  on TI's E2E forum under the part number CC1312PSIP. No e-mail route
  exists for it: ti.com/contact lists none for design questions [the same
  session's read, relayed]. The post's link is this story's to record.
- **G1 clears** on TI's written answer naming the FCC ID whose grant covers
  Wi-SUN Mode #5 at 300 kbit/s, from a TI employee; the exhibit TI names is
  read. If no clear answer has come when Story 3.1.1 is done, or TI cannot
  show Mode #5 on a grant, the gate goes back to Omid with § 5.2's two
  fallbacks (mode #2a at 100 kbit/s, or a second vendor's module), and the
  plan's boards wait on his ruling.
- **G2 clears** when TI's instructions for the -3 grant's "trace reference
  design" are read. If they bind a board to TI's own geometry, publishing
  that board goes to the ASK-OMID register first, since TI's terms are not
  on the licence lane's allowlist (`THIRD_PARTY_NOTICES.md`).
- The label ID follows TI's answer (the spec's § 11 call 2).
**Done when:** `docs/build/gates.md` records G1 and G2, each CLEARED with
TI's written word linked, or RETURNED TO OMID with his ruling (manifest
teeth `gate-g1-recorded` and `gate-g2-recorded`, prose check
`gates-g1-g2-sourced`, which needs Omid's word).

#### Story 3.1.3: Gate G3: the spare boards under §15.23

**Story:** As the build, I want §15.23's count read for spare boards that
are never powered, so that the assembled order never puts a sixth radio in
the house.
**Context:** decision 8b and the spec's § 6.2 and § 8 (gate G3). JLCPCB
assembles at least two boards a design, so an assembled order brings a
second Reader and a second Field Generator: seven radio boards against
§15.23's "quantities of five or less" (`docs/study/radio-and-location.md`
§ What the law decides before any radio is compared). The story reads
§15.23 and any FCC guidance on it at the version read, dated, and records
whether never-powered spares count. If they count, the Reader's and the
Field Generator's boards are ordered assembled without the module, and Omid
places one module on each with the hot plate (the spec's § 11 call 11).
This is the project's reading, not legal advice (the spec's preamble).
**Done when:** `docs/build/gates.md` records G3 CLEARED with the rule's text
cited at its version and date, and the order route it sets (manifest tooth
`gate-g3-recorded` and prose check `gate-g3-sourced`).

#### Story 3.1.4: The parts chosen from their datasheets

**Story:** As the board's designer, I want every part the studies left
representative chosen from its maker's datasheet, so that the boards are
drawn around real parts with real footprints.
**Context:** the spec's § 9 (the parts study's and the cost study's items)
and § 10. It draws nothing, so it may run before G1 clears.
- **The USB bridge:** a CDC-class part needing no driver on any of the three
  platforms, chosen from its datasheet, with its baud limit read for
  Story 4.1.4 (`docs/study/software-stack.md` § On the wire).
- **The chip antennas' land patterns and matching:** the Johanson
  0915AT43A0026's for the US build, and the 0868AT43A0020001E's for the EU
  build (Story 2.1.19, `tiered-cost.md` § The EU build and § What the choice
  leaves to later stories), each from its maker's datasheet
  (`docs/study/parts-and-toolchains.md` § The tags).
- **Every part marked representative:** the regulator, the relays and their
  driver, the connectors, the passives, the cells' holders, the enclosures
  and the seal's band and loop (`tiered-cost.md` § The devices).
- Each choice names its datasheet, pinned under the reference corpus's
  `sources/` where its licence allows, and its price read and dated in
  `tiered-cost.md`.
**Done when:** each part above is named with its datasheet and its dated
price, and every "representative" mark it replaces is refreshed in the
studies (manifest prose check `parts-from-datasheets`).

#### Story 3.1.5: The Reader's board in KiCad 10

**Story:** As a builder, I want the Reader's board drawn in KiCad 10, so
that I can order it and build it.
**Context:** the spec's § 5.4 (the Reader) and decision 11; waits on G1 and
G2 (Story 3.1.2) and the parts (Story 3.1.4). The module at 17 dBm on the
20 dBm port, the W5017 on an SMA connector, the USB bridge, the 3.3 V
regulator, two relays and two supervised inputs, a 10-pin debug header, and
an unfitted footprint for a wired network link that keeps clear of the
module's RF layout rules (SWRA797 §3.1). Drawn fresh from the written
layout rules, nothing copied from TI's reference designs (§ 4 item 8).
**Done when:** the Reader's KiCad 10 project is committed under `hardware/`
with its electrical and design rule checks clean, and `docs/evidence/3.1.5/`
holds the checks' reports and a render of the board, read by Omid
(manifest teeth `kicad-projects` and `reader-board-evidence`, prose check
`reader-board-by-omid`).

#### Story 3.1.6: The Field Generator's board in KiCad 10

**Story:** As a builder, I want the Field Generator's board drawn in
KiCad 10, so that I can build the device that makes a zone.
**Context:** the spec's § 5.4 (the Field Generator); waits on Stories 3.1.2
and 3.1.4. The same module and whip at a power set for its room, a USB wall
supply's 5 V through a 3.3 V regulator, and no host link. The exit relay and
the motion input are options, not in milestone 1.
**Done when:** the Field Generator's KiCad 10 project is committed under
`hardware/` with its electrical and design rule checks clean, and
`docs/evidence/3.1.6/` holds the reports and a render, read by Omid
(manifest tooth `field-generator-board-evidence`, prose check
`field-generator-board-by-omid`).

#### Story 3.1.7: The tag core and its CR2450 variant in KiCad 10

**Story:** As a builder, I want one tag core drawn in KiCad 10, with the
warehouse's CR2450 variant from the same schematic, so that every tag style
is one board.
**Context:** decisions 4 and 5a and the spec's § 5.4 (one tag core); waits
on Stories 3.1.2 and 3.1.4. One schematic and one four-layer layout sized
for the fob, carrying the module at 12.0 dBm on the 14 dBm port, the chip
antenna on its land pattern, the CR2032 with its buffer capacitor in a
tool-opened compartment, and debug pads within 25 mm of an edge for the
pogo-pin clip. Fitting options: the TMP117, the tamper loop on a pin that
wakes the module, the badge's buttons, and the unfitted NFC footprint (an
NFC memory, its I²C link and two antenna pads at the board's edge). The
CR2450 item variant shares the schematic with a larger holder. The EU
build's 868 MHz chip antenna (Story 3.1.4) takes the same layout if its
land pattern fits; otherwise the EU core is a variant of the same
schematic, drawn here.
**Done when:** the core's KiCad 10 project and its CR2450 variant are
committed under `hardware/` with their electrical and design rule checks
clean, and `docs/evidence/3.1.7/` holds the reports and renders, read by
Omid (manifest tooth `tag-core-evidence`, prose check `tag-core-by-omid`).

#### Story 3.1.8: The enclosures: the fob, the card and the seal

**Story:** As a builder, I want each tag style's enclosure chosen and the
seal's band designed, so that the one core becomes three tags.
**Context:** the spec's § 5.4 (the sealTag, the compactTag and the cardTag)
and the software study's NFC item (`software-stack.md` § Decided ahead of
the sitting). The seal is a new design: a band or a seal whose loop is
wired to the core's wake pin, reporting in the next frame (about 1.5 s
against [14]'s 5 s, INFERRED). The coil's place in the seal's enclosure,
off the core and away from the module's antenna and ground plane
(SWRA797 §3.1.2), is drawn; milestone 1 fits no coil. Each enclosure is
chosen from its maker's drawing, its price dated in `tiered-cost.md`.
**Done when:** each enclosure is named with its drawing and price, the
seal's band, loop and coil place are drawn, and `docs/evidence/3.1.8/` holds
a photo or render of each, read by Omid (manifest tooth
`enclosures-evidence`, prose check `enclosures-by-omid`).

#### Story 3.1.9: The quote and the order (gate G4)

**Story:** As the first builder, I want the drawn boards quoted, re-priced
and ordered, so that the build buys what the drawings need at prices read
on the day.
**Context:** gate G4 and decision 5b (the spec's § 8, § 5.5); waits on
Stories 3.1.3 and 3.1.5–3.1.8. The boards' real sizes re-price them; the
JLCPCB quote for the drawn boards, the module's sourcing there, the five
prices written not read (`tiered-cost.md` § What was read), and every price
read again. The order follows G3's route. Milestone 1 buys Energizer cells
(5b), their price read at purchase.
**Done when:** `tiered-cost.md` carries the quote and every price read
again, dated, with the totals summed again; `docs/build/gates.md` records
G4 CLEARED; and `docs/evidence/3.1.9/` holds the order's public-safe record
(manifest teeth `gate-g4-recorded` and `order-evidence`, prose check
`order-by-omid`).

#### Story 3.1.10: Bring-up and the antenna match

**Story:** As the first builder, I want each board powered, its first
transmission seen and its antenna matched, so that every device sends what
the design says before any firmware is trusted on it.
**Context:** the spec's § 6.2 (the NanoVNA-H4: "Tuning of the antenna
impedance matching network is recommended after manufacturing of the PCB",
PSIP p57) and spec D9. Each board is powered from its supply and current
read, flashed with a test image, and its transmission seen on the receiver
in 902–928 MHz; each antenna's match is read on the NanoVNA-H4 and tuned.
The spare boards stay unpowered (decision 8b).
**Done when:** `docs/evidence/3.1.10/` holds, for each board, its first
power-up current, its receiver capture and its match before and after
tuning, read by Omid (manifest tooth `bring-up-evidence`, prose check
`bring-up-by-omid`).

#### Story 3.1.11: The tags' currents, and the worn tags' rule

**Story:** As the design, I want the tags' currents measured, so that every
battery figure rests on a measurement and decision 6's rule picks each worn
tag's cell.
**Context:** the spec's § 5.5 (decision 6) and § 9 (the parts study's
items). The LP-XDS110ET measures the three ASSUMED inputs, the 0.3 ms
set-up, the 1.0 ms beacon and the 0.4 ms slot, and each tag style's current
at its listen interval. Whether the 12 dBm setting uses the boost mode that
needs 2.1 V is read on the bench (the spec's § 9, open for the build). Then
decision 6's rule runs in its order: an adult's fob left short takes a
CR2450; a baby's band left short takes one conversion per reading, then
shorter corridor stretches; a conference badge left short takes a 3 s
listen; anything still short comes back to Omid. No requirement number is
lowered (`requirements.md` § Ground rules). Each battery figure in
`parts-and-toolchains.md` is re-priced from the measurements, each MEASURED
with its date.
**Done when:** `docs/evidence/3.1.11/` holds the current traces, the
studies' battery figures carry the measured inputs, and the rule's outcome
for [9], [18] and [27] is recorded, read by Omid (manifest tooth
`currents-evidence`, prose check `currents-by-omid`).

#### Story 3.1.12: The card-sized badge, if the range test asks

**Story:** As the conference's badge, I want a card-sized variant only if
[2]'s range test shows the small core short, so that the badge reaches
30 m worn without a board nobody needs.
**Context:** decision 4 and the spec's § 5.4 and § 11 call 5; waits on
Story 7.1.1's range test. A board shorter than a quarter wave, 8.2 cm at
915 MHz, loses efficiency (SWRA797 p9), and a card-sized board recovers some
of it. If the test meets [2] on the small core, this story records that
and draws nothing.
**Done when:** the range test's verdict is recorded here; if the badge was
short, the card-sized variant is drawn in KiCad 10 from the core's
schematic and measured against [2], with `docs/evidence/3.1.12/` read by
Omid (manifest tooth `card-variant-evidence`, prose check
`card-variant-by-omid`).

#### Story 3.1.13: Close out Epic 3.1

**Story:** As the fresh-session closer, I want Epic 3.1 closed per KB
`epic-close-protocol`, so that the archive carries the hardware's evidence.
**Context:** `node ../../scripts/close/close-epic.mjs --epic 3.1` from the
repo root, in a session that did none of the epic's work. Initiative-final
while Epic 3.1 is the initiative's only epic.
**Done when:** every manifest check passes or is adjudicated and the archive
entry carries the evidence block.

## Initiative 4: The firmware

**Objective:** one firmware tree for the one module, with the Reader's, the
Field Generator's and the tag's images sharing a protocol library, the tag
style a setting (the spec's § 5.7), built with the free toolchains on
Windows, macOS and Linux, and TI's SDK fetched by each builder, never copied
in (`docs/study/parts-and-toolchains.md` § The licences of the reference
designs read). Filed 2026-10-30 by Epic 2.1's close.
**Done when:** the three images run milestone 1's devices from the protocol
the spec states, built on all three platforms, and Epic 4.1 is archived with
close evidence (initiative-final while Epic 4.1 is the initiative's only
epic).

### Epic 4.1: The protocol and the three images

**Goal:** the protocol library and the three images, each on the
development board first and on its own board once Story 3.1.10 brings it
up.
**Ordering:** 4.1.1 → 4.1.2 → 4.1.3 → 4.1.4, 4.1.5 and 4.1.6 → 4.1.7 →
4.1.8 → 4.1.9. No image is published before G6 (Story 4.1.1); 4.1.2–4.1.7
are written and built on the development board meanwhile.
Manifest: `docs/close/4.1.json` (born FAILING).
**Done when:** every tooth in `docs/close/4.1.json` passes from the repo root
and its prose checks are adjudicated.

#### Story 4.1.1: Gate G6: TI's SDK licence

**Story:** As the project, I want TI's SDK licence ruled on before any
firmware image is published, so that nothing ships on terms the licence
lane has not cleared.
**Context:** the spec's § 8 (gate G6). The SDK's licence reads "for use only
with TI Devices" and is not on the allowlist (`THIRD_PARTY_NOTICES.md`).
The story files one ASK-OMID row: what may be published (source that
fetches the SDK, compiled images, both), with the licence's text quoted at
its version and date, a recommendation and its default (CLAUDE.md § The
licence lane; spec C5).
**Done when:** the ASK-OMID row is answered by Omid and `docs/build/gates.md`
records G6 with his answer (manifest tooth `gate-g6-recorded`, prose check
`gate-g6-ruled`, which needs Omid's word).

#### Story 4.1.2: The firmware tree, built on three platforms

**Story:** As a builder on any of the three platforms, I want the firmware
to build with free tools on my own machine, so that I can flash my devices
without a paid tool or a vendor's cloud.
**Context:** the spec's § 4 item 7 and § 5.7. The tree under `firmware/`
builds with CMake and TI Arm Clang or the Arm GNU route, against TI's
SimpleLink Low Power F2 SDK fetched by a script at a pinned version, never
committed; SysConfig sets the radio at the SDK's default PHY settings
(SWRA797 p6). The build is MEASURED on Windows, macOS and Linux, each with
the machine's operating system, the date and each tool's printed version.
At the first install, the licence terms the study left unread are read and
cited at their version and date: SysConfig's and TI Arm Clang's (the study
found no price on their pages, `parts-and-toolchains.md` § The toolchains),
the Arm GNU route's, and the terms of TI's download page. Routed here by
Epic 2.1's close on Omid's verdict on `free-tools-three-platforms` ("PASS,
route reads"). A term that is not free goes to the ASK-OMID register beside
gate G6.
**Done when:** `firmware/` holds the build and the SDK-fetch script, a
development-board image builds on all three platforms as measured, and the
build's commands are in `docs/ci-contract.md` if CI runs them (manifest
tooth `firmware-build-files` and prose check `firmware-three-platforms`).

#### Story 4.1.3: The protocol library: the frame, the count and joining

**Story:** As the three images, I want one protocol library for the frame,
so that the Reader, the Field Generators and the tags keep one timing.
**Context:** the spec's § 4 items 1, 2 and 5 and § 5.1; the radio study's
build items (`radio-and-location.md` § Anti-collision, § Channels, § Zones).
The 250 ms frame: the Reader's beacon carrying commands and the count, the
Field Generators' block, the joining window, the downlink slot and the
reply slots (0.7333 ms each). The count each Field Generator's slot repeats.
The hop order a US tag is given when it joins (42 channels, 10.5 s a
cycle), and in the EU the site's channels, named by a Reader at joining.
Host-run tests pin each layout figure against the study's.
**Done when:** the library and its tests are committed under `firmware/`,
the tests pass on the host, and every frame figure they pin cites the study
line it is read from (manifest prose check `protocol-library-figures`).

#### Story 4.1.4: The Reader's image and its wire

**Story:** As the host, I want the Reader to run the frame and send its
events as binary frames at the bridge's limit, so that every reply reaches
the floorplan.
**Context:** the spec's § 5.1, § 5.4 (the Reader's own zone slot, milestone
1's second zone, § 11 call 9) and § 5.6; the software study's build items
(`software-stack.md` § On the wire). The beacon, the roll calls, the Reader's
zone slot, and the compact binary frame closed by a CRC on the USB bridge at
the baud limit Story 3.1.4 read, in place of the ASSUMED 921,600. Each event
carries the fields of the event schema (Story 5.1.1).
**Done when:** the Reader's image runs the frame on the development board,
its frames decode against the schema in a host-run test, and the wire's baud
cites the bridge's datasheet (manifest prose check `reader-image-wire`).

#### Story 4.1.5: The Field Generator's image

**Story:** As a zone, I want the Field Generator to send its ID in its slot
at its room's power, so that a tag knows the room it is in.
**Context:** the spec's § 5.1 (zones) and § 5.4. Its slot in the Reader's
block, timed from the beacon; its ID and the count repeated; its power set
for its room; an exit's Field Generator also listens, for the pairing
alarm.
**Done when:** the image runs on the development board in the Reader's frame,
its slot and power settable, and a host-run test pins its slot's timing
(manifest prose check `field-generator-image`).

#### Story 4.1.6: The tag's image

**Story:** As a tag, I want to listen to my set, check in and report my
sensor and tamper loop, so that I last my battery's years and am found in
my zone.
**Context:** the spec's § 4 items 1, 2 and 4, § 5.4 and § 5.5. The listen
set the Reader names, the beacon skipped on most listens with time kept on
the 32 kHz crystal, check-ins at each style's interval (2 s, 1 s, 12 s), the
style a setting, the tamper loop's wake, and the TMP117's averaging a per-tag
setting, eight averages by default (decision 7).
**Done when:** the image runs on the development board in the Reader's frame,
each style settable, and host-run tests pin the listen layout and the check-in
intervals (manifest prose check `tag-image`).

#### Story 4.1.7: A lost tag's search, and a tag carried past a Reader's edge

**Story:** As a lost tag, I want to search the way the study designs it, so
that I am found again within [22]'s 5 minutes and raise no false [25] alarm.
**Context:** decisions 3b and 3c (the spec's § 5.3) and the design Stories
2.1.17, 2.1.18 and 2.1.20 write into `radio-and-location.md` § Channels and
§ Zones: a lost tag's window, its searches held at 6.6 µA, its own Reader's
channel first, then its site's channels in turn, the spare left out; at a
site of more than one network, the wide search a carried tag starts (a
frame on each of the site's four channels in the EU, one channel for a hop
cycle in the US), shown in about 50 s or 60 s against [22]'s 5 minutes, and
the trigger and the network it joins as Story 2.1.20 states them; in the US,
the word a tag is given when it joins that its site has other networks; and
what re-arms the two wide searches for a tag that stays unplaced beside its
old Reader (INFERRED by Story 2.1.20's review: when it next hears a Field
Generator, not at each whole-block listen). The study's § What the choice
leaves to later stories lists these items. The CR2032
core's US limit, one carry every 2.4 days (4.7 on cold chain), stands on
Omid's ruling "State the limit; CR2450 if carried" (Story 2.1.18's Outcome).
**Done when:** the search runs in the tag's image as the study states it,
and host-run tests pin its search order and timing against the study's
figures (manifest prose check `lost-tag-search`).

#### Story 4.1.8: The frame's timing measured on air

**Story:** As the design, I want the beacon's and a Field Generator slot's
lengths measured, so that the listen windows rest on what the boards send.
**Context:** the spec's § 9 (the radio study's first item); waits on Story
3.1.10. The 1.0 ms beacon and the 0.4 ms slot are ASSUMED in every battery
figure (`radio-and-location.md` § Zones). Measured on the receiver or a
logic capture on the brought-up boards, then written into the studies,
MEASURED with the date.
**Done when:** `docs/evidence/4.1.8/` holds the captures and the studies
carry the measured lengths, read by Omid (manifest tooth `timing-evidence`,
prose check `timing-by-omid`).

#### Story 4.1.9: Close out Epic 4.1

**Story:** As the fresh-session closer, I want Epic 4.1 closed per KB
`epic-close-protocol`, so that the archive carries the firmware's evidence.
**Context:** `node ../../scripts/close/close-epic.mjs --epic 4.1` from the
repo root, in a session that did none of the epic's work. Initiative-final
while Epic 4.1 is the initiative's only epic.
**Done when:** every manifest check passes or is adjudicated and the archive
entry carries the evidence block.

## Initiative 5: The host software

**Objective:** the small Python 3 program the spec's § 5.6 states, on the
owner's own Windows, macOS or Linux machine, offline, with its page in the
owner's browser, taking events from several Readers; then the LAN tier,
after milestone 1. Filed 2026-10-30 by Epic 2.1's close.
**Done when:** Epic 5.1 (milestone 1's host) and Epic 5.2 (the LAN tier)
are archived with close evidence (initiative-final at Epic 5.2's close).

### Epic 5.1: The host for milestone 1

**Goal:** the host that shows milestone 1's tags on the floorplan, raises
its alarms and pairs people with things, measured on all three platforms.
**Ordering:** 5.1.1 → 5.1.2 → 5.1.3 → 5.1.4, 5.1.5 and 5.1.6 → 5.1.7 →
5.1.8. 5.1.1–5.1.6 run on recorded or simulated frames; 5.1.7 needs a Reader
(Story 4.1.4).
Manifest: `docs/close/5.1.json` (born FAILING).
**Done when:** every tooth in `docs/close/5.1.json` passes from the repo root
and its prose checks are adjudicated.

#### Story 5.1.1: The event schema, published

**Story:** As anyone writing a reader, I want the event schema published as a
JSON Schema file, so that any device that sends the same events can fill the
"reader" layer.
**Context:** the spec's § 5.6 and `software-stack.md` § The event schema the
readers report: `v`, `tag`, `reader`, `via`, `rssi_dbm`, `time` in UTC from
the Reader's frame clock, `seq` and `payload`. The Chorus four layers' schema
(ADOPTED, `software-stack.md` § The Chorus comparison, software side).
**Done when:** a `.schema.json` file under `software/` names exactly those
eight fields, and a host test validates sample events against it (manifest
tooth `event-schema-fields` and prose check `event-schema-exact`).

#### Story 5.1.2: The host's core: storage, the page and security

**Story:** As an owner, I want the host to run on my machine alone, keep its
data in one file and answer only me, so that nothing leaves my machine.
**Context:** the spec's § 5.6 and `software-stack.md` § The choice, § Security
on the user's machine. Python 3's standard library (`sqlite3`,
`ThreadingHTTPServer`), a page with no build step and nothing fetched from
the internet, listening on the owner's machine only, answering only requests
addressed to it by that name, a token on every change, its own secrets made
on first run, one data file backed up through SQLite's backup interface.
Retention: raw check-ins 7 days by default, the history until the owner
deletes it, the setting on screen beside the disk use (decided ahead of the
sitting). pyserial is either copied in with its trail (BSD-3-Clause is on
the allowlist, `THIRD_PARTY_NOTICES.md`) or installed by `pip`; the story
chooses and says why. Python's and SQLite's licences, asserted free by the
software study and never cited, are cited at their version and date (routed
here by Epic 2.1's close, as Story 4.1.2's reads are).
**Done when:** the host starts, serves its page and stores events with the
rules above, each rule pinned by a host test that CI's `test` job runs, and
pyserial's route is recorded (manifest tooth `host-tests` and prose check
`host-core-rules`).

#### Story 5.1.3: The Reader adapter, for several Readers

**Story:** As the host, I want to read every Reader's frames as events and
keep one copy of each reply, so that a site's Readers, or the hall's second
Reader, show each tag once.
**Context:** decision 11 and 3a (the spec's § 5.3, § 5.6). The adapter turns
the Reader's binary frames (Story 4.1.4) into schema events, takes events
from several Readers, keeps one copy of each reply by the tag's `seq`, and
sets each Reader's clock (`software-stack.md` § Time, from the Reader's
clock).
**Done when:** host tests on recorded frames from two Readers show one copy
per `seq` and each Reader's clock set (manifest prose check
`reader-adapter-several`).

#### Story 5.1.4: The floorplan, its neighbour links and the block's order

**Story:** As an owner, I want to draw my floorplan by zone with its
neighbour links, so that each tag listens to the right Field Generators.
**Context:** the spec's § 4 items 1 and 2 and § 9 (the radio study's items):
the floorplan draws each zone's neighbours, from which the Reader names a
placed tag's listen set, and the host orders the block along the floorplan
so that a set is one run (`radio-and-location.md` § Zones). The page draws
the floorplan in SVG, live by Server-Sent Events (`software-stack.md`
§ The floorplan).
**Done when:** host tests pin the listen sets and the block's order computed
from a floorplan with neighbour links, and the page shows a tag's zone live
from recorded events (manifest prose check `floorplan-links-order`).

#### Story 5.1.5: The alarms

**Story:** As staff, I want the four alarms and Chorus's rules raised and
acknowledged by name, so that nothing that matters passes unseen.
**Context:** the spec's § 5.6 and `software-stack.md` § The alarms: tamper,
temperature, missed check-in and pairing, with dwell, left-a-zone, inputs,
low battery and a Reader offline, each set on screen and acknowledged by a
named user. A temperature alarm means a tag's reading crossed a limit someone
set (spec C8), never a medical claim.
**Done when:** host tests raise and acknowledge each alarm from recorded
events within its requirement's latency budget on the host, and no page or
message calls the system a medical device (manifest prose checks
`alarms-raised-acknowledged` and `host-no-medical-claim`).

#### Story 5.1.6: Scan-and-scan pairing, with the QR decoder

**Story:** As staff, I want to pair a person with a thing by scanning two
codes, so that pairing takes seconds.
**Context:** the spec's § 5.6 and § 9 (the software study's QR decoder).
Every tag carries its ID as a number and a QR code; the scan is at the host
machine in milestone 1, by a USB scanner typing the number or a camera
decoded on the host. The decoder is chosen, its licence on the allowlist or
installed beside the host (CLAUDE.md § The licence lane).
**Done when:** pairing by two scans works in a host test with both the typed
number and a decoded code, and the decoder's licence and route are recorded
(manifest prose check `pairing-qr`).

#### Story 5.1.7: The host measured on three platforms

**Story:** As a user on any of the three platforms, I want the host measured
on mine with a Reader attached, so that the guides promise only what was
run.
**Context:** the spec's § 9 (the software study's items) and spec D5. macOS
was measured by Story 2.1.5 (`software-stack.md` § The stack on three
platforms); Windows and Linux were cited. This story runs the host on
Windows, macOS and Linux, each with a Reader on its USB port, recording the
operating system, the date and the versions printed.
**Done when:** `docs/evidence/5.1.7/` holds each platform's run with a Reader,
and `software-stack.md` marks each platform MEASURED (manifest tooth
`host-platforms-evidence` and prose check `host-three-platforms`).

#### Story 5.1.8: Close out Epic 5.1

**Story:** As the fresh-session closer, I want Epic 5.1 closed per KB
`epic-close-protocol`, so that the archive carries the host's evidence.
**Context:** `node ../../scripts/close/close-epic.mjs --epic 5.1` from the
repo root, in a session that did none of the epic's work. Not
initiative-final: Epic 5.2 follows.
**Done when:** every manifest check passes or is adjudicated and the archive
entry carries the evidence block.

### Epic 5.2: The LAN tier, after milestone 1

**Goal:** the host shared on a site's network: HTTPS, user accounts, share
links and phone scanning (`software-stack.md` § What the choice leaves to
later stories; the spec's § 2 non-goals for milestone 1).
**Ordering:** after Story 7.1.4 (milestone 1 accepted): 5.2.1 → 5.2.2 →
5.2.3.
Manifest: `docs/close/5.2.json` (born FAILING).
**Done when:** every tooth in `docs/close/5.2.json` passes from the repo root
and its prose checks are adjudicated.

#### Story 5.2.1: HTTPS and user accounts

**Story:** As a site, I want the host served over HTTPS with user accounts,
so that staff on the site's network see it and every action carries a name.
**Context:** `software-stack.md` § Security on the user's machine and § What
the choice leaves to later stories. The LAN tier was left out of milestone 1
(the spec's § 2).
**Done when:** the host serves HTTPS on the site's network with accounts,
each pinned by a host test, and `docs/evidence/5.2.1/` holds a run on two
machines of the network (manifest tooth `lan-https-evidence`, prose check
`lan-https-accounts`).

#### Story 5.2.2: Share links and phone scanning

**Story:** As staff, I want view-only, time-limited share links and pairing
by a phone's camera, so that a visitor sees what they need and pairing works
away from the host machine.
**Context:** `software-stack.md` § The Chorus comparison, software side
(share links ADOPTED for the LAN tier, a vendor's relay declined) and § What
the choice leaves to later stories.
**Done when:** share links expire and show only what they name, pinned by
host tests, and a phone pairs two codes on the site's network
(manifest prose check `share-links-phone-scan`).

#### Story 5.2.3: Close out Epic 5.2

**Story:** As the fresh-session closer, I want Epic 5.2 closed per KB
`epic-close-protocol`, so that the archive carries the LAN tier's evidence.
**Context:** `node ../../scripts/close/close-epic.mjs --epic 5.2` from the
repo root, in a session that did none of the epic's work. Initiative-final.
**Done when:** every manifest check passes or is adjudicated, the archive
entry carries the evidence block, and Initiative 5 is cut whole.

## Initiative 6: The guides, in the US and the EU

**Objective:** a complete beginner's guides under `guides/`, for the US
build and the EU build (decision 9a, the spec's § 6.3), with every shopping
list tiered (D7), written from the build's own records. Filed 2026-10-30 by
Epic 2.1's close.
**Done when:** the guides take a beginner from the bench to milestone 1's
demonstration in both regions, and Epic 6.1 is archived with close evidence
(initiative-final while Epic 6.1 is the initiative's only epic).

### Epic 6.1: The beginner's guides for milestone 1

**Goal:** the guides milestone 1's acceptance build follows (Story 7.1.3).
**Ordering:** 6.1.1 → 6.1.2 → 6.1.3 and 6.1.4 → 6.1.5. Each guide is written
once its source stories' records exist: 6.1.3 after Stories 3.1.1 and
3.1.10, 6.1.4 after Stories 4.1.2 and 5.1.4.
Manifest: `docs/close/6.1.json` (born FAILING).
**Done when:** every tooth in `docs/close/6.1.json` passes from the repo root
and its prose checks are adjudicated.

#### Story 6.1.1: The guides' frame, both regions and the tiered lists

**Story:** As a beginner in the US or the EU, I want one place that says what
I will build, what it costs in my region and what I am responsible for, so
that I start knowing the whole road.
**Context:** the spec's § 6.1, § 6.3 and § 7, and decision 9a. The guides'
index; the US and the EU paths; the tiered shopping lists from
`tiered-cost.md` (the US build's tables, and Story 2.1.19's § The EU build
in euros at the day's ECB rate, its four prices written not read named
where the list needs them), each price with its date; and the plain
statements the spec asks for: TI's
evaluation terms for the debugger and the development board (§ 6.2), the US
builder's five-device allowance under §15.23, and that in the EU the builder
carries compliance, with no home-built allowance and no module grant (§ 7).
No page calls the system a medical device or a monitor of vital signs
(spec C8). This is the project's reading, not legal advice.
**Done when:** `guides/README.md` holds the index, both regions' paths and
the plain statements, and each region's tiered list is dated (manifest tooth
`guides-index` and prose checks `guides-both-regions` and
`guides-no-medical-claim`).

#### Story 6.1.2: The install steps, checked on each platform

**Story:** As a beginner on Windows, macOS or Linux, I want install steps
that were run on my platform, so that each step works as written.
**Context:** the spec's § 9 (the software study's item) and spec D5. The
toolchains (`parts-and-toolchains.md` § The toolchains), the SDK fetch
(Story 4.1.2) and the host (Story 5.1.2), each step run on each platform and
recorded with the operating system, the date and the versions printed.
**Done when:** the install guide's steps are each marked with the platforms
they were run on, all three for every step (manifest prose check
`install-steps-three-platforms`).

#### Story 6.1.3: The bench and the build guides

**Story:** As a beginner, I want to set up my bench and build each device
from a guide, so that I need no one beside me.
**Context:** the records of Stories 3.1.1 (the bench, the receiver, the
hand-solder route's stencil and paste), 3.1.9 (the order) and 3.1.10
(bring-up and the antenna match). Each tier's route is written; every step
that needed an advanced tool in Omid's build is named, and the guides'
first recommendation to other beginners is written from that record (the
spec's § 11 call 6). The EU build's guide adds the 868 MHz antenna's
matching and a check of the gain its maker prints, measured on the
builder's own board (`tiered-cost.md` § What the choice leaves to later
stories); Omid's acceptance build is the US build, so these steps have no
first builder's test until someone builds in the EU (the spec's § 6.3).
**Done when:** the bench and build guides cover every tier's route for each
device, each step that needed an advanced tool named (manifest prose check
`bench-build-guides`).

#### Story 6.1.4: The firmware and host guides, with the setup walk

**Story:** As a beginner, I want to flash my devices, run the host and draw
my floorplan from a guide, so that my tags show in their zones.
**Context:** Stories 4.1.2 (the build and flashing), 5.1.2 (the host) and
5.1.4 (the floorplan), and the spec's § 9 (the radio study's item): the
setup walk that checks the floorplan's neighbour links, since a link the
floorplan misses is shown in 5.0 s or not at all
(`radio-and-location.md` § Zones).
**Done when:** the guides take a beginner from flashing to a tag shown on the
floorplan, and the setup walk checks every neighbour link (manifest prose
check `firmware-host-guides`).

#### Story 6.1.5: Close out Epic 6.1

**Story:** As the fresh-session closer, I want Epic 6.1 closed per KB
`epic-close-protocol`, so that the archive carries the guides' evidence.
**Context:** `node ../../scripts/close/close-epic.mjs --epic 6.1` from the
repo root, in a session that did none of the epic's work. Initiative-final
while Epic 6.1 is the initiative's only epic.
**Done when:** every manifest check passes or is adjudicated and the archive
entry carries the evidence block.

## Initiative 7: Milestone 1, the two-zone demonstration

**Objective:** milestone 1 as Initiative 2 stated it (the spec's § 6.1):
a builder with no experience follows `guides/` and ends with one Reader, one
Field Generator, and a sealTag, a compactTag and a cardTag built; the host
running on their own machine; all three tags read together; a tag carried
between two zones, the move showing on the floorplan; a tamper alarm and a
temperature reading arriving; and a tag paired with a person raising an
alarm when it leaves without them. Its two zones are the Reader's own and
the Field Generator's, the exit (§ 5.4). It is accepted when Omid has built
it from the guides (spec D9), and the guides are revised from his build
before anyone else is pointed at them. Then, after gate G1, the requirement
tests at a site's scale. Filed 2026-10-30 by Epic 2.1's close.
**What its acceptance starts:** the product's website, activewaveinc.com,
leads with the rebuilt system, and its use cases become step-by-step recipes
from these guides (fleet KB `activewaveinc-new-system-trigger`); Story 7.1.4
starts that work.
**Done when:** Epic 7.1 (milestone 1 accepted) and Epic 7.2 (the site-scale
tests) are archived with close evidence (initiative-final at Epic 7.2's
close).

### Epic 7.1: Milestone 1, built and accepted

**Goal:** the five devices' bench tests run, Omid's acceptance build done
from the guides, and the guides revised from it.
**Ordering:** 7.1.1 and 7.1.2 (on the built devices, after Initiatives 3–5's
stories they use) → 7.1.3 (after Epic 6.1) → 7.1.4 → 7.1.5.
Manifest: `docs/close/7.1.json` (born FAILING).
**Done when:** every tooth in `docs/close/7.1.json` passes from the repo root
and its prose checks are adjudicated.

#### Story 7.1.1: The bench tests: range, zone moves and three tags at once

**Story:** As the design, I want what five devices can measure measured, so
that milestone 1 shows the system working and the site-scale tests start
from numbers.
**Context:** the spec's § 2 (milestone 1's bench measures one worn tag's
range, zone moves, the alarms' latencies and the tags' currents) and § 5.1.
One worn tag's range against [2]'s 30 m, and against [11]'s 15 m through a
wall, the result Story 3.1.12's trigger; a tag carried between the Reader's
zone and the exit's, the move's time against [13]'s 5 s, and the exit passes
the study puts at 98.8 % for 100 of 100; and all three tags read together in
one roll call.
**Done when:** `docs/evidence/7.1.1/` holds each test's method, readings and
captures, each against its requirement number, read by Omid (manifest tooth
`bench-tests-evidence`, prose check `bench-tests-by-omid`).

#### Story 7.1.2: The alarms' latencies

**Story:** As staff, I want each alarm's latency measured end to end, so that
milestone 1 shows each one inside its requirement.
**Context:** the spec's § 2 and § 6.1, and the requirements' alarm tables:
tamper ([5], [14], [23]), temperature ([15], [24]), missed check-in ([7],
[16], [25]) and pairing ([8], [17], [26]). Each is raised by its real cause on
the built devices (the seal's loop opened, the sensor warmed past a limit, a
tag shielded, a paired tag carried out of the exit without its partner) and
timed to the host's screen.
**Done when:** `docs/evidence/7.1.2/` holds each alarm's timed run against
its numbers, read by Omid (manifest tooth `alarm-latency-evidence`, prose
check `alarm-latencies-by-omid`).

#### Story 7.1.3: Omid's acceptance build, from the guides

**Story:** As Omid, the first builder, I want to build milestone 1 from the
guides alone, so that the guides are proven before anyone else follows them.
**Context:** spec D9 and the spec's § 6.1 and § 6.2: the US build, the
advanced tool tier with fully assembled boards. He follows `guides/` as a
beginner would, and every place a guide was wrong, unclear or missing is
recorded as he meets it. The demonstration runs every clause of milestone 1
(the Objective above).
**Done when:** `docs/evidence/7.1.3/` holds the demonstration's record, clause
by clause, and the list of every guide fault met, and
`docs/build/milestone-1.md` carries `**Accepted by:** Omid` with the date
(manifest teeth `acceptance-evidence` and `milestone-accepted`, prose check
`acceptance-by-omid`).

#### Story 7.1.4: The guides revised, and the website's work started

**Story:** As the next builder, I want the guides revised from Omid's build
before I am pointed at them, so that I meet none of the faults he met.
**Context:** spec D9 and the spec's § 6.1. Every fault Story 7.1.3 recorded
is fixed in the guides or answered with its reason. Milestone 1's acceptance
then starts the product website's work: the story leaves a pointer in
activewaveinc.com by that repo's own route (a hub inbox fragment for the
hub to file there, `node ../../scripts/inbox.mjs new`), never an edit in
that repo from here (hub CLAUDE.md § Stay in the repo you are rooted in;
fleet KB `activewaveinc-new-system-trigger`).
**Done when:** each fault in Story 7.1.3's list is fixed or answered, and the
website's work is filed for activewaveinc.com with its link recorded here
(manifest prose checks `guides-revised` and `website-work-started`).

#### Story 7.1.5: Close out Epic 7.1

**Story:** As the fresh-session closer, I want Epic 7.1 closed per KB
`epic-close-protocol`, so that the archive carries milestone 1's acceptance.
**Context:** `node ../../scripts/close/close-epic.mjs --epic 7.1` from the
repo root, in a session that did none of the epic's work. Not
initiative-final: Epic 7.2 follows.
**Done when:** every manifest check passes or is adjudicated and the archive
entry carries the evidence block.

### Epic 7.2: The requirement tests at a site's scale, after gate G1

**Goal:** the tests five devices cannot run (the spec's § 2 and § 9), under
the module grant that gate G1 settles.
**Ordering:** after Story 7.1.4 and with G1 CLEARED (`docs/build/gates.md`):
7.2.4 first, since its reading sets what may be built beyond five devices,
then 7.2.1, 7.2.2 and 7.2.3 → 7.2.5.
Manifest: `docs/close/7.2.json` (born FAILING).
**Done when:** every tooth in `docs/close/7.2.json` passes from the repo root
and its prose checks are adjudicated.

#### Story 7.2.1: Throughput at scale: [3] and [12]

**Story:** As the conference and the ward, I want the roll call measured at
our numbers of tags, so that 1,000 tags a second and 100 a second are shown,
not reasoned.
**Context:** the spec's § 2 and § 5.1 (1,296 reply slots a second at the
conference, 1,284 at the ward). [3]: 1,000 tags a second per Reader; [12]:
the ward's 100 tags within its figure.
**Done when:** `docs/evidence/7.2.1/` holds the method and readings against
[3] and [12], read by Omid (manifest tooth `throughput-evidence`, prose check
`throughput-by-omid`).

#### Story 7.2.2: The EU inventory on Readers that share a channel

**Story:** As an EU warehouse, I want two Readers that share a channel
tested, so that the inventory's time is measured against [21].
**Context:** decision 3b (the spec's § 5.3): the inventory is planned on
Readers that share a channel taking turns, 8.0–8.9 minutes; they roll call at
once, 5.9–6.1 minutes, only if this test shows they do not hear each other.
**Done when:** `docs/evidence/7.2.2/` holds the test and its verdict against
[21], and the study records which plan holds, read by Omid (manifest tooth
`eu-shared-channel-evidence`, prose check `eu-shared-channel-by-omid`).

#### Story 7.2.3: The crowded hall, and the second Reader's acknowledgement path

**Story:** As the conference, I want [2] measured in a crowd and the second
Reader's replies acknowledged, so that a badge is read at 30 m among people.
**Context:** decision 3a and the spec's § 5.1 and § 5.3. A crowd and indoor
fading are ASSUMED at 10–20 dB, leaving 0.7–10.7 dB; the second Reader listens
on the first Reader's network, and the acknowledgement path of a reply only it
hears is measured (were every check-in retried once, the badge would last
1.19 years).
**Done when:** `docs/evidence/7.2.3/` holds the hall test against [2] and the
acknowledgement path's measurement against [9], read by Omid (manifest tooth
`hall-evidence`, prose check `hall-by-omid`).

#### Story 7.2.4: Beyond five devices: the grant's conditions read

**Story:** As a site's builder, I want the module grant's conditions read for
a build beyond five devices, so that a site's devices run under the grant.
**Context:** the spec's § 5.2 (the grants' conditions) and § 7, and its § 9
open items: the 20 cm mobile RF-exposure condition for worn tags (the FCC's
exemption text unread), the co-location condition and whether a passive NFC
coil counts as "any other antenna", and the Reader's emissions under Part 15
Subpart B once its electronics are a digital device beyond five. Devices made
for customers stay outside this spec (decision 9b). This is the project's
reading, not legal advice.
**Done when:** each condition is read at its version and date, with what it
asks of a site's build, and what this repo may build beyond five devices is
recorded, needing Omid's word (manifest prose check `beyond-five-conditions`).

#### Story 7.2.5: Close out Epic 7.2

**Story:** As the fresh-session closer, I want Epic 7.2 closed per KB
`epic-close-protocol`, so that the archive carries the site-scale tests.
**Context:** `node ../../scripts/close/close-epic.mjs --epic 7.2` from the
repo root, in a session that did none of the epic's work. Initiative-final.
**Done when:** every manifest check passes or is adjudicated, the archive
entry carries the evidence block, and Initiative 7 is cut whole.
