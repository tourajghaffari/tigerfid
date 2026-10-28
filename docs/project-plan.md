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

**Last staleness sweep:** 2026-10-27 (tigerfid #26, the Sprint 2 proposal's
step-2 sweep, KB `plan-staleness-sweep`: scoped to the sprint's inputs, which
were the whole live plan, `docs/close/2.1.json` and the records its stories
cite).

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
2.1.5 (Sprint 2) → 2.1.6 (proposed after Sprint 2's review) → 2.1.7 → 2.1.8.
Serial since the Sprint 2 proposal (2026-10-27): 2.1.4 names parts for the
radio 2.1.3 chooses, and 2.1.5's reader abstraction follows 2.1.3's link.
The Sprint 2 review (2026-10-28) routes a fix-up of the three studies to the
Sprint 3 proposal, ahead of 2.1.6: its findings can move a cell and the tags'
listening budget (the archive's § Executed sprints › Sprint 2).
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

#### Story 2.1.6: The tiered cost of milestone 1

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
browser read, or written as not read.
**Done when:** `docs/study/tiered-cost.md` carries the tables with a date on
every price and a total per tier.

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
  §3.1.2). Milestone 1 fits none of it. The study's Decided line still reads
  "no space reserved" until the Sprint 3 fix-up corrects it.
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
