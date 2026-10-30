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
**What its acceptance starts (2026-10-30):** the product's website,
activewaveinc.com, waits on it to lead with the rebuilt system. Its home page
then leads with it, and its use cases become step-by-step recipes from these
guides (fleet KB `activewaveinc-new-system-trigger`). Whoever records the
acceptance here, or Omid, starts that work.
**Done when:** Epic 2.1 is archived with close evidence (initiative-final —
Epic 2.1 is its only epic), and its close has filed the build initiatives.

### Epic 2.1: The study, the decision sitting and the rebuild spec

**Goal:** six study documents under `docs/study/`, each answering one part
of spec §3, then the decision sitting and the approved rebuild spec.
**Ordering:** 2.1.1 → 2.1.2 (Sprint 1 legs 4–5) → 2.1.9 → 2.1.3 → 2.1.4 →
2.1.5 (Sprint 2) → 2.1.10 → 2.1.6 → 2.1.11 (Sprint 3) → 2.1.12 → 2.1.14
(Sprint 4 legs 1–2) → 2.1.15 → 2.1.7 → 2.1.8, with 2.1.13 (Sprint 4 leg 3),
2.1.16, 2.1.17, 2.1.18 and 2.1.19 anywhere before 2.1.8.
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
fixes (the archive's § Executed sprints › Sprint 4, The sprint's shape).
The Sprint 4 review (2026-10-29) routes a third fix-up, Story 2.1.15, ahead
of the sitting: its Important finding moves the warehouse item tag's
margin at a Reader's edge, from which the sitting chooses the item tag's
cell (the archive's § Executed sprints › Sprint 4).
The Story 2.1.15 review (2026-10-30) files a fourth fix-up, Story 2.1.16,
which gates nothing in the sitting: none of its items moves a margin the
sitting chooses a cell from (Story 2.1.15's `**Reviewed:**` record).
The Story 2.1.16 review (2026-10-30) files a fifth, Story 2.1.17, which
gates nothing in the sitting either (Story 2.1.16's `**Reviewed:**`
record).
The Story 2.1.17 review (2026-10-30) files a sixth, Story 2.1.18, which
gates nothing in the sitting either (Story 2.1.17's `**Reviewed:**`
record).
The decision sitting (Story 2.1.7, 2026-10-30) files Story 2.1.19, the EU
build priced, on Omid's decision 9a. It goes before the close, with 2.1.18
(the rebuild spec's gate G5).
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

#### Story 2.1.15: The Sprint 4 review's fixes: the warehouse at a Reader's edge, and the figures beside it — ✅ DONE (2026-10-30)

**Completed:** 2026-10-30 — tigerfid #67 (branch `story-2.1.15-reader-edge`, pre-tick head `22533913`).
**Outcome:** finding 1's edge is stated with its cost, § [27] is priced for the costliest bay, and Sprint 4's D2 is re-run there: the 12 s listen keeps five years on a CR2032 for both item tags at every bay, a Reader's edge included. No requirement number moved (D1). These figures supersede Story 2.1.12's and Story 2.1.14's:
- **The edge** (`radio-and-location.md` § Zones, A Reader's edge): a second Field Generator in the bay across each edge follows the near Reader's network, so every item's set is one run of three in its own Reader's block. Its cost: four or six more Field Generators along a 100 m aisle, and no firmware change. A second wake every fifth listen is the route without new hardware (1.16 and 1.05 of five years). One Field Generator in both blocks is declined, since its two slots can meet without bound.
- **[27] at 12 s:** 124.2 mAh (1.21 of five years) and 136.9 (1.10) on a CR2032, and 143.5 (2.76) and 156.2 (2.54) on a CR2450; they were 123.0 (1.22), 135.6 (1.11), 142.2 (2.78) and 154.9 (2.55). The margins are 21 % and 10 % (were 22 % and 11 %). A CR2032 keeps five years from a 7.0 s or 8.8 s listen (were 6.8 s and 8.6 s), and one conversion per reading gives 131.6 mAh, 1.14 (was 130.3, 1.15). The CR2450 stays recommended for margin, and no ASK-OMID row is filed.
- **Minor 2:** the warehouse's Reader steps the count at most once in 5 minutes, priced at 288 wakes a day, 1.28 mAh over five years.
- **Minor 3:** the joining window and the downlink slot are counted (2.2 ms, ASSUMED). The conference has 324 reply slots a frame, 1,296 a second and 1,000 tags in about 0.77 s (were 327, 1,308 and 0.76 s); the ward 321 and 1,284 (were 324 and 1,296); and any one Reader up to 158 Field Generators (was 164).
- **Minor 4:** the VNA's box list was read 2026-10-30 (two SMA cables, an adapter, a calibration kit). The Lacks name a tag board's connection to the VNA.
- **Minor 5:** a lost tag's window stops at a frame (EU) or a hop cycle (US), and its searches space out to hold 6.6 µA.
- **The five nits** are corrected, and `requirements.md` [27] gains a dated pointer line. Every figure reproduces from the studies' inputs by a script, which first reproduced the printed figures it replaces.

**Reviewed:** 2026-10-30, by the Story 2.1.15 reviewer pass (tigerfid #69).
- **The pass:** the hub-rooted Mac session "Mac 2" (session
  `26c5a341-81ee-4645-b03a-779448772d5d`), on Windows Coordinator's orders
  20. It was fresh by `/clear`. Its background-task path id,
  `8cac88a9-9a28-4d13-8e6f-fca07bf925f5`, is the session id of an earlier
  Mac session, which proposed an expeal-app initiative and sprint and had
  wrapped (its tombstone on the hub's origin), whose process the clear
  ended. It was not elevated (uid 501) and did none of this story's work.
- **Verdict: PASS WITH FINDINGS.** The story meets its Done-when. No finding
  is Important, and none moves a margin the sitting chooses a cell from.
  The item tag's 1.21 and 1.10 of five years on a CR2032, at every bay,
  reproduce. Three Minors and three nits go to Story 2.1.16, which gates
  nothing in the sitting.
- **The method:** the pass read the repo at `18075a1` (#67 → `ce56ceeb`,
  then its wrap, #68) and #67's diff from `3feecef`. It rebuilt every figure
  #67 prints with one script, from the studies' stated inputs alone. It
  re-ran #67's fourteen greps at `3feecef` and censused the superseded
  figures at `18075a1`. It re-read the two pages that two items rest on.
  Each finding says what was MEASURED and what is INFERRED.
- **The merge:** #67 → `ce56ceeb2661844839fa9fa5ef1bad1936105c6b`, GitHub's
  stamp 08:46:54Z, read from the API between this pass's `date -u` reads of
  09:09:46Z and 09:18:03Z. The tick's pre-tick head, `22533913`, is #67's
  fourth commit, the one before the tick commit.
- **The inputs the script used,** each as the studies state it: a wake's
  start-up 2.795 µC; listening 5.8 mA; a Field Generator's slot 0.4 ms; the
  beacon 1.0 ms; drift 2 × 60 ppm of the time since a slot was heard; a reply
  18.09 µC; a reading 16.74 µC; standby 1.0 µA and the sensor's shutdown
  0.15 µA; 1 % a year of the rated 235 or 620 mAh; years of 365 days. The
  script checked 110 printed figures, and none differs.
- **Finding 1, a Reader's edge: MET.**
  - **[27] at 12 s:** a listen to a run of three is 2.795 + 5.8 × (1.2 +
    1.44) = 18.107 µC, 1.509 µA, so 66.09 mAh over five years. Beside it are
    standby 43.80; the check-ins and their beacons 1.31 (96 a day ×
    (18.09 + 8.769) µC); the inventory 0.01; the count's bound 1.28 (288 a
    day × 8.769 µC); and the cell's own loss 11.75. That is 124.24 mAh, and
    150 ÷ 124.24 = 1.21. Cold chain adds 6.11 for 720 readings a day and
    6.57 of shutdown: 136.92 mAh, 1.10. On a CR2450, with 31.00 of loss,
    143.49 (2.76) and 156.17 (2.54).
  - **The rest of § [27] reproduces:** all 48 cells of its table; the four
    superseded 12 s cells, without the count (123.0, 135.6, 142.2, 154.9);
    the 7.0 s and 8.8 s break-even listens; one conversion's 131.6 mAh
    (1.14); and the 535 mAh at a 1 s listen.
  - **The edge table:** the second wake's window is 2 × 20 ppm of the time
    since the far slot was heard, plus 2 × 50 ppm × 0.25 s. That is 0.505 ms
    at 12 s and 2.425 ms at 60 s. Each row adds its wake to the own run of
    two, 1.316 µA:
    - every fifth listen adds (2.795 + 5.8 × 2.825) ÷ 60 = 0.320 µA, giving
      1.64 µA, 1.16 and 1.05;
    - every listen adds 0.670, giving 1.99 µA, 1.03 and 0.95;
    - the review's wake, widened as the first, adds 1.122, giving 2.44 µA,
      0.91 and 0.84. A CR2450 then gives 2.01 on cold chain.

    The review's own four figures reproduce, as the study says, once the
    count is left out and years are 365.25 days, its second pair at
    20 ppm × 12 s: 163.8, 176.4, 138.3 and 151.0.
  - **The decline:** two Readers' crystals slide 20 ppm × 0.25 s = 5 µs a
    frame. Two 0.4 ms slots with a 0.3 ms retune between them meet while
    their starts lie within 0.7 ms: 1.4 ÷ 250 = 0.56 % of the time. That is
    1.4 ms ÷ 20 µs a second = 70 s, once in 250 ms ÷ 20 µs a second = 3.5
    hours.
  - **The geometry** follows [19], [20] and [22]: a 100 m × 100 m
    building, 9–12 Readers (one for each 29–33 m) and 10 m bays. Three or
    four Readers along an aisle make two or three edges, each with a bay on
    either side.
  - **Sprint 4's D2, re-run:** [27] holds at every bay on a CR2032, by 21 %
    and 10 % (9.55 % before rounding). No CR2450 route is needed, and the
    study keeps it recommended for margin. No ASK-OMID row is owed.
- **Minor 2, the count: MET.** 288 × 8.769 µC × 1,825 days is 1.28 mAh,
  and a step a minute is 6.40 mAh, 1.06 of five years on cold chain. The
  worn tags' 2.6 s and 2.8 s are the step rates that spend the nursery room
  tag's 29.3 mAh a year and the hall badge's 27.5. The bound's effect on
  [21] is Minor B.
- **Minor 3, the frame: MET.** The conference has (250 − 10.2 − 2.2) ÷
  11/15 = 324 reply slots a frame, 1,296 a second, and 1,000 tags in
  0.77 s. The ward has 321 and 1,284. Any Reader holds (250 − 183.3 − 1.0 −
  2.2) ÷ 0.4 = 158 Field Generators, and a warehouse block holds 525 at
  [21]'s 200 a second.
- **Minor 4, the VNA's box: MET.** MEASURED by one plain request to R&L's
  page between this pass's reads of 09:18:03Z and 09:18:04Z:
  - its "Packing list" names "NanoVNA host (with 1950mAh battery) x 1",
    "15cm SMA male to male RG316RF cable x2", an "SMA male calibration kit"
    (OPEN, SHORT and LOAD), "SMA female to female connector x1" and two
    USB-C cables;
  - its accessories list "LOAD KIT (SUPPLIED) 12.95" and "TEST JIG FOR
    TESTING YOUR OWN CIRCUIT 9.95", and the price reads $109.95.

  The Field Generator's antenna is "as the Reader's", a W5017 "on an SMA
  connector" (`parts-and-toolchains.md` § The Reader, § The Field
  Generator), so the study's INFERRED holds.
- **Minor 5, a lost tag: MET,** with nit 2.
  - The 6.6 µA is the ward's beacon and 27 slots, at a search every 12 s.
  - The window passes a frame after 0.25 s ÷ 120 ppm = 35 minutes, and a
    whole frame every 12 s is 121 µA. The US window reaches 10.5 s after
    24 hours.
  - A search comes every 3.65 minutes once the window is a frame, every
    6.5 minutes after an hour lost in the US, and every 2.6 hours once the
    window is the hop cycle.
- **The five nits: MET.**
  - TI's LP-XDS110 page, read in the same window, carries
    `data-price="35.09"` twice, with neither `priceCurrency` nor `USD`. Its
    exchange-rate table lists 22 currencies and not the dollar (EUR 0.893,
    JPY 157.4), as the parts study's INFERRED says.
  - The other four read as #67 corrected them: 1,363 a second and 0.77 s,
    each with its citation; "three times", with the research log's l.319
    and l.364–366; 3 × 11.625 = 34.875; and 11/15 ms at both lines.
- **The greps:**
  - #67's fourteen reproduce at `3feecef`, each with the count and the
    lines the PR gives.
  - A census at `18075a1` of each superseded figure (123.0, 135.6, 142.2,
    154.9, 2.78, 2.55, 1,308, 1,296, "up to 164", 327, 0.76 s, 6.8 s, 8.6 s,
    130.3, 0.733, 22 % and 11 %) finds each only in a record, a dated
    "before" line or a figure that is correct as it stands. The records are
    a story's Outcome or Context and the manifest.
  - The hub's `check-item8-dispositions.mjs` read nothing, because #67
    titles its section "Each item's grep". This pass read the dispositions
    by hand.
- **The battery** at `18075a1`, on the Mac, between the reads of 09:24:25Z
  and 09:24:27Z:
  - `close-lint: ok (adopted 2026-10-26)`;
  - `npm test`: exit 0, with `check-licenses: ok` and 22 of 22;
  - the close runner over the eleven teeth: ten PASS, and
    `rebuild-spec-approved` FAIL (exit 1), as the plan expects until Story
    2.1.7.
- **Findings, Minor** (to Story 2.1.16):
  - **A. In the EU, a Reader's edge may put two networks on one channel.**
    - MEASURED, the text: in the US each network hops on its own
      (`radio-and-location.md` l.497–500). In the EU "The band carries five
      600 kHz channels" and hopping is not required (l.511–512), "where a
      network keeps its channel" (l.568). The edge section places the far
      neighbour "in the US, on another channel" (l.765–766). No line says
      which channel each EU Reader takes.
    - INFERRED, the failure: two neighbouring Readers on one EU channel put
      the edge bay's two Field Generators on it too, unsynchronised. Their
      0.4 ms slots meet while their starts lie within 0.4 ms, 0.32 % of the
      time: about 40 s once in 3.5 hours at the crystals' worst, and longer
      as the crystals agree. That is the reason the study declines one
      Field Generator in both blocks.
    - The answer is one sentence: neighbouring EU Readers take different
      channels of the five (INFERRED: a grid of Readers, each with eight
      around it, needs four).
  - **B. The count's 5-minute wait is not checked against [21].**
    - MEASURED, the text: the warehouse's Reader "steps the count at most
      once in 5 minutes, and a command waits for the next step" (l.719–721).
      The day's inventory is a roll call, a command (§ Anti-collision,
      l.418–421). [21] asks for "100,000 items inventoried in at most 10
      minutes". "What it leaves unchanged" names [13], [17] and [22]
      (l.741–743), not [21].
    - INFERRED, it holds: 312 s for the step and a listen, then a Reader's
      share of 8,333–11,111 items at [21]'s own 200 a second, 42–56 s. That
      is about 5.9–6.1 minutes. One sentence states it.
  - **C. The conference hall's second Reader is on no stated network.**
    - MEASURED, the text: [2] puts "a second reader" in the main hall, and
      the radio study names it among [2]'s mitigations (l.479–481). § Anti-
      collision counts the conference as one block of 23 (l.434–438), and an
      inner area's set is "one run of nine slots" (l.686–687). No line says
      whether that Reader listens on the first's network, which keeps one
      block, or keeps its own, which puts a Reader's edge across the hall.
    - INFERRED, at such an edge between two columns with no Field Generator
      added: an area beside it hears six slots in its own block and three in
      the other's, (18.107 + 2.795 + 5.8 × 1.305) µC every 2 s. That is
      14.24 µA against 12.53, or 137.4 mAh a year: 1.09 years, so [9] holds
      by 9 % where the study prints 22 %. It is 0.86 years at the 117.5 mAh
      floor, where 0.96 is printed.
    - The warehouse's route would keep the 22 %, with six more Field
      Generators in the hall. One sentence chooses.
- **Nits** (each MEASURED; to Story 2.1.16):
  1. **The research log has no Story 2.1.15 entry.** #67 re-read R&L's
     page and left `docs/reference-research.md` unchanged. Every study story
     that read a source before it, 2.1.2 to 2.1.14, logged its reads there
     (`git log -- docs/reference-research.md`). Only `tiered-cost.md` records
     this read.
  2. **"about 6.5 minutes after an hour lost" is the US figure**
     (`radio-and-location.md` l.575–578). In the EU the window stops at a
     frame after about 35 minutes, so a tag lost an hour still searches
     every 3.65 minutes. The sentence names the US only in its third
     clause.
  3. **Three reasoned bullets under the edge table carry no mark**
     (l.801–822): the window's 0.505 and 2.43 ms; the decline's 5 µs,
     0.56 %, 70 s and 3.5 hours; and the CR2450's 2.01. The study's preamble
     reads "A claim marked **INFERRED** was reasoned, not read" and
     "Everything else cites the text it was read from" (l.13–16).
- **The recurrence triage** (hub KB `epic-close-protocol`): each text seat
  falls in a decided class, so none qualifies for a new decision.
  - #67's four seats are two `piped-checker-exit`, one `cwd-drift` and one
    `wrapped-sentence-needle-undercount`.
  - This pass's four are one `zsh-equals-face` deny, two `cwd-drift`
    warnings and one `ugrep-bounded-repetition-exceeds-complexity-limit`
    warning.

  The register at the hub's `94e7c1a7` holds none of the eight yet, so they
  go to the coordinator as text.
- **The merge word** for this pass: Omid, first-hand in its terminal between
  its reads of 09:09:46Z and 09:18:03Z: "Please merge PRs you open during
  this session when ready and in the right order."
- **Routed, in order:**
  1. Story 2.1.7, Omid's sitting, which need not wait for Story 2.1.16;
  2. Story 2.1.16, anywhere before Story 2.1.8;
  3. Story 2.1.8, Epic 2.1's close.

  The cost of sitting first is no margin: the sitting would decide from
  figures this pass reproduced. If it decides Minor A or C in the rebuild
  spec, Story 2.1.16 writes the spec's answer into the study.

**Story:** As the decision sitting, I want the warehouse item tag's margin
to hold at every bay, a Reader's edge included, so that I choose the item
tag's cell from a margin that holds.
**Context:** born 2026-10-29 by the Sprint 4 review, from its finding 1, its
four Minors and its five nits (the archive's § Executed sprints › Sprint 4,
its `**Reviewed:**` record). It goes before Story 2.1.7: finding 1 moves the
margin from which the sitting chooses the item tag's cell. Each item says
what the review MEASURED and what is INFERRED.
- **Finding 1, the warehouse at a Reader's edge**
  (`docs/study/radio-and-location.md` § Channels and § Zones, The layout of
  a listen; `docs/study/parts-and-toolchains.md` § [27]).
  - MEASURED, the studies' text: the warehouse has 9–12 Readers ([20]),
    each network hops on its own, and each Reader's Field Generators time
    their slots from its beacon, in a block after it. The bays are ordered
    so that "an item's set (its bay and the bay on either side) is one run
    of three", and § [27] prices every item tag so. Neither study says how
    a tag hears a neighbouring bay whose Field Generator follows another
    Reader.
  - INFERRED: at a Reader's edge that set spans two networks, so it cannot
    be one run. With a second wake widened for 12 s of drift, a CR2032
    needs 163.8 mAh (0.92 of five years) and the cold-chain tag 176.4 mAh
    (0.85). Widened for the two Readers' crystals only, they need 138.3 mAh
    (1.08) and 151.0 mAh (0.99).
  - The study states how an edge tag hears its set, or what the edge's
    neighbour link becomes, with its cost, and prices § [27] for the
    costliest bay. It re-runs Sprint 4's D2 for [27] there. If no route
    keeps five years on a CR2032, the recommended CR2450 is named the route
    (about 2.0 times the need at the edge, INFERRED).
- **Minor 2, the count's beacon wakes** (`radio-and-location.md` § The
  layout of a listen). INFERRED: the "86,400 listens" a day are the 1 s
  listen's, and a warehouse tag listens 7,200 times. The cold-chain tag's
  margin holds only while its Reader steps the count less often than about
  once in 27 s. The study bounds the count's rate, or prices it.
- **Minor 3, the frame's other users** (`radio-and-location.md`
  § Anti-collision). MEASURED: the frame counts the beacon and the block,
  not the joining window or the downlink slot that names a set. The study
  names 1,308 a second and "up to 164" as upper bounds, or counts both.
- **Minor 4, the VNA's box** (`docs/study/tiered-cost.md` § The tool tiers).
  MEASURED at R&L's page: its packing list holds two 15 cm SMA cables and
  an SMA adapter, and "LOAD KIT (SUPPLIED)" is a paid accessory. The
  sentence names what the box holds and what a board still lacks.
- **Minor 5, a tag that hears nothing** (`radio-and-location.md` § How a tag
  hears a Field Generator it has not heard before). INFERRED: the window
  grows by 2 × 60 ppm of the time since the tag last heard a slot, so a tag
  shut in a drawer passes a frame's window after about 35 minutes and may
  then listen a whole frame at each search. The study bounds the window, or
  prices the lost tag at its bound.
- **The five nits,** each MEASURED: `tiered-cost.md`'s `priceCurrency`
  sentence (l.631–632); `software-stack.md` l.212's 0.73 s beside the
  0.76 s it cites; `parts-and-toolchains.md` l.740's "twice"; `tiered-cost.md`
  l.114–115's "(3 × 11.63)"; and `radio-and-location.md` l.436–437's
  0.733 ms.
- **What stays.** No requirement number moves (Sprint 4's D1). A figure that
  a dated pointer line in `requirements.md` quotes, and that this story
  changes, gets a new dated pointer line. The Outcomes of Stories 2.1.10,
  2.1.12 and 2.1.14 are records and are not edited; this story's tick names
  the figures it supersedes.
**Done when:** finding 1, the four Minors and the five nits are each
corrected in its document, or its sentence shown correct with its source;
§ [27] is priced for the costliest bay, a Reader's edge included, with
Sprint 4's D2 re-run there; and the PR body quotes each item's grep with
each hit's disposition (manifest prose check `sprint4-review-fixes`).

#### Story 2.1.16: The Story 2.1.15 review's fixes: the EU edge, the inventory's wait and the hall's second Reader — ✅ DONE (2026-10-30)

**Completed:** 2026-10-30 — tigerfid #72 (branch `story-2.1.16-review-fixes`, pre-tick head `d98a2621`).
**Outcome:** the review's three Minors and three nits are corrected in their documents. No requirement number moved, and no printed figure changed: each figure added reproduces from the studies' inputs by a script. The sitting had not run, so the study writes its own answers to Minors A and C, for the sitting to confirm or overrule:
- **Minor A** (`radio-and-location.md` § Channels): in the EU each Reader keeps one channel, and Readers beside each other take different ones, four of the five in a 2 × 2 pattern. One channel for all is declined: the edge bay's two Field Generators would meet 0.32 % of the time, about 40 s once in 3.5 hours, without bound. A tag moving to another Reader learns its channel from the downlink that places it; a lost EU tag tries the next channel at each search, found within four, about 15 minutes.
- **Minor B** (§ Zones, The layout of a listen): the day's inventory waits for the count's step and finishes in about 5.9–6.1 of [21]'s 10 minutes.
- **Minor C** (§ Zones, The conference hall's second Reader): [2]'s second Reader listens on the first Reader's network, so the hall stays one block and [9] holds by 22 % as priced. Its own network is declined: [9] by 9 % beside the edge with no Field Generator added (137.4 mAh, 1.09 years), and a badge rejoining at each crossing (up to 61.7 mAh a year at ten a day, ASSUMED; 0.81 years).
- **The nits:** `reference-research.md` records Story 2.1.15's read of R&L's page; the lost tag's search times are split by region, the hour's 6.5 minutes under the US; the three bullets under the edge table carry INFERRED with their arithmetic.

**Reviewed:** 2026-10-30, by the Story 2.1.16 reviewer pass (tigerfid #74).
- **The pass:** a hub-rooted reviewer session, on the fleet coordinator's
  orders. It was fresh by `/clear`: its background-task path id differs
  from its session id and names the earlier session whose process the
  clear ended, a lawcuments-app Sprint 16 review that had wrapped (its
  tombstone on the hub's origin). It was not elevated and did none of this
  story's work.
- **Verdict: PASS WITH FINDINGS.** The story meets its Done-when. No finding
  is Important, and none moves a figure the sitting reads: [9]'s 22 % in
  the hall, [27]'s 1.21 and 1.10 of five years and every tier's total are
  unchanged, and they reproduce. Two Minors go to Story 2.1.17, which gates
  nothing in the sitting. **Story 2.1.7's sitting can proceed.**
- **The method:** the pass read the repo at `cdaa8e5` (#72 → `f59888ff`,
  then its wrap, #73) and #72's diff from `b0d0ba7`. The leg's study
  commit, `d98a2621`, read from the PR's head ref, holds the study and the
  research log exactly as merged. One script rebuilt every figure #72 adds,
  and the figures the sitting reads, from the studies' stated inputs alone.
  The pass re-ran #72's eight greps, censused their needles across line
  breaks, and read in the reference folder the one source the new text
  cites. Each finding says what was MEASURED and what is INFERRED.
- **The merge:** #72 → `f59888ff341fb07764e799fd8692b0a628014d31`, GitHub's
  stamp 12:15:14Z, read from the API between this pass's `date -u` reads of
  15:35:53Z and 15:37:46Z. The tick's pre-tick head, `d98a2621`, is #72's
  first commit, the one before the tick commit.
- **The sources:** `node scripts/reference-check.mjs`, read-only, between
  the reads of 15:37:46Z and 15:37:49Z: "reference-check: ok — 299 rows
  verified, README the stub".
  - The new text cites one source, PSIP p23 (`sources/ti-cc1312psip-swrs293.pdf`,
    a pinned row of `docs/reference-corpus.tsv`, its sha256 verified by that
    run). Its table gives −10 to 10 ppm for "RF Frequency accuracy including
    internal software compensated temperature drift, excluding aging, -40°
    to 65°", as Minor A cites it.
  - No pin was added. The research log's two new lines match
    `tiered-cost.md` § Read on 2026-10-30: the two clock reads of 08:31:29Z,
    and 109.95.
- **The inputs the script used,** each as the studies state it: a wake's
  start-up 2.795 µC; listening 5.8 mA; a slot 0.4 ms and a frame 250 ms;
  drift 2 × 60 ppm, the Readers' crystals ±10 ppm each and the tag's RTC
  ±50 ppm; a reply 18.09 µC, a beacon wake 8.769 µC and a reading
  16.74 µC; standby 1.0 µA and the sensor's shutdown 0.15 µA; 1 % a year of
  the rated 235 or 620 mAh, and 150 or 396 mAh usable; the hall badge's
  122.5 mAh and its check-ins' 3.9 mAh; § Range's margins; years of 365
  days. The script checked 58 printed figures, and none differs.
- **Minor A, the EU at a Reader's edge: MET.**
  - Readers beside each other take different channels, four of the five in
    a 2 × 2 pattern (`radio-and-location.md` l.518–526), so no edge bay
    holds two networks' Field Generators on one channel. Where a Reader has
    up to eight around it, four is the fewest that works.
  - The decline reproduces: 0.8 ms ÷ 250 ms = 0.32 %; 0.8 ms ÷ 20 µs a
    second = 40 s; 250 ms ÷ 20 µs a second = 12,500 s, 3.47 hours.
  - A tag moved to another Reader listens a frame there, 0.0004 mAh. A lost
    tag's search is 2.795 µC + 5.8 mA × 250 ms = 1.453 mC, 220.1 s at
    6.6 µA, and four of them take 14.7 minutes. What a lost tag's cycle
    covers is Minor D.
  - An observation, not a finding: PSIP p23's row excludes aging, which the
    same table gives as −2 to 2 ppm over five years. With it, the worst
    slide is 24 µs a second, a meeting of about 33 s once in about 2.9
    hours. Nothing bounds a meeting either way, so the decline stands.
- **Minor B, [21] against the count's wait: MET.** 300 + 12 + 0.25 =
  312.25 s, then 8,333 or 11,111 items at 200 a second, 41.7 or 55.6 s:
  353.9–367.8 s, 5.90–6.13 minutes. The sentence sits under the count
  (l.764–773) and in "What it costs" (l.786–788). Its figures assume every
  Reader's roll call at once, which is Minor E.
- **Minor C, the hall's second Reader: MET.**
  - On the first Reader's network the hall stays one block, an inner area's
    set one run of nine, and [9]'s 1.22 years (22 %) stands as priced.
  - The far corner: √(40² + 15²) = 42.72 m and 20·log₁₀(42.72 ÷ 30) =
    3.07 dB. The reply is weaker by 36.8 − 31.8 = 5 dB in the US and by
    24.4 − 22.2 = 2.2 dB in the EU, and the EU's corner keeps 24.4 − 3.1 =
    21.3 dB.
  - The declined own network reproduces: (18.107 + 10.364) µC every 2 s is
    14.24 µA, against 25.067 µC every 2 s, 12.53 µA; 122.5 + 1.70 × 8.76 =
    137.4 mAh, 1.09 years, and 0.86 at 117.5 mAh. Ten US crossings a day:
    5.8 mA × 10.5 s = 0.0169 mAh, 61.7 mAh a year, then 184.2 mAh and 0.81
    years. Every check-in retried once: 126.4 mAh, 1.19 years.
- **The three nits: MET.**
  1. `reference-research.md` § 4 has its Story 2.1.15 entry (l.392–405),
     and § Searches already run its two lines (l.458–462).
  2. The lost tag's cost is split by region (`radio-and-location.md`
     l.607–614), the hour's 6.5 minutes under the US.
  3. The three bullets under the edge table carry INFERRED (l.855, l.868,
     l.881) with their arithmetic: 0.505 ms and 2.43 ms; 0.56 %, 70 s and
     12,500 s; 40.7 mAh and 396 ÷ 196.9 = 2.01. The rows they name are the
     table's second and fourth.
- **No figure moved.**
  - Every number on a line #72 removed from the study (31 distinct) is on a
    line it added.
  - `requirements.md`, `parts-and-toolchains.md`, `tiered-cost.md`,
    `software-stack.md` and `original-system.md` have the same blob at
    `18075a1`, the Story 2.1.15 review's reading, and at `cdaa8e5`. So no
    requirement number, [27] figure or tier total has moved since that
    review reproduced them.
  - The script reproduces [27] again: 124.2 mAh (1.21) and 136.9 (1.10) on
    a CR2032, and 143.5 (2.76) and 156.2 (2.54) on a CR2450.
- **The greps:**
  - #72's eight reproduce at `cdaa8e5`, each with the count and lines the
    PR gives: 12, 6, 15, 5, 4, 1 and 3 hits, and 0 at `b0d0ba7` for nits 1
    and 3.
  - A census of the same needles across line breaks finds one more: "one
    block on one / channel" at l.895–896, in the new Minor C paragraph,
    correct as it stands.
  - The hub's `check-item8-dispositions.mjs` reads #72's body: 29
    dispositions, 14 current, 0 named.
- **The battery** at `cdaa8e5`, between the reads of 15:38:10Z and 15:38:18Z:
  - `npm test`: exit 0, with `close-lint: ok (adopted 2026-10-26)`,
    `check-licenses: ok` and 22 of 22;
  - the close runner over the eleven teeth: ten PASS, and
    `rebuild-spec-approved` FAIL (exit 1), as the plan expects until Story
    2.1.7.
- **Findings, Minor** (to Story 2.1.17):
  - **D. A lost EU tag's channel cycle is unstated, and so is its time to
    be found in its own Reader's area.**
    - MEASURED, the text: "A lost tag tries the next channel at each
      search, so it meets the Reader whose area it lies in within four
      searches" (`radio-and-location.md` l.538–540). The lost tag's cost
      gives the EU "one search in about 4 minutes once the window reaches a
      frame", and four searches, about 15 minutes, "for a tag lost in
      another Reader's area" (l.608–611). No line says which channels the
      cycle covers.
    - INFERRED, the gap: a tag that hears nothing cannot tell whose area it
      is in, so every lost EU tag cycles. One lost in its own Reader's area
      then hears its own channel once in four searches: up to 14.7 minutes
      to be found again, not one search of 3.7 minutes. A cycle over all
      five channels, the spare included, needs up to five searches, 18.3
      minutes. At the ward and the conference, one channel each, a cycle
      over the warehouse's four would cost their tags the same.
    - The answer is one sentence: what the cycle covers (for example, the
      channels the site's Readers use, named when a tag joins, so the ward
      and the conference keep their one channel and their 4 minutes), and
      the warehouse's time to be found for every lost tag. The searches
      stay at 6.6 µA however they are ordered, so no battery figure moves.
  - **E. Minor B's 5.9–6.1 minutes assume every Reader's roll call at
    once.**
    - MEASURED, the text: [21] is "200 tags/s on one radio channel" because
      "readers that share a channel share its airtime" (`requirements.md`
      l.305–309). Minor B gives each Reader's share 200 a second at once
      (`radio-and-location.md` l.768–771). Minor A's 2 × 2 pattern repeats
      a channel two Readers along (l.522–524), so some EU Readers share
      one. The US bullet says "Bays 30 m apart rarely hear each other"
      (l.504). No line says whether EU Readers on one channel roll call at
      once.
    - INFERRED, the bound: a 3 × 3 grid puts four of its nine Readers on
      one channel, and a 3 × 4 grid four of its twelve on each of two. Were
      Readers on one channel to take turns, its 44,444 or 33,333 items
      take 222 s or 167 s at 200 a second, and the inventory 534.5 s or
      478.9 s: about 8.0–8.9 minutes, still inside [21]'s 10. Those Readers
      stand two apart, about 60 m, twice [20]'s 30 m.
    - The answer is one sentence: whether they roll call at once, by the US
      bullet's reason at twice its distance, or the 8.0–8.9 minutes if they
      take turns.
- **The recurrence triage** (hub KB `epic-close-protocol`): each text seat
  falls in a decided class, so none qualifies for a new decision.
  - #72's one seat is a `wrapped-sentence-needle-undercount`: the Minor A
    grep's l.895–896 hit, above.
  - This pass's are two `piped-checker-exit` warnings (a fast-forward and
    this PR's first push, each piped into `tail`, then read unpiped), one
    `block-commit-hook-denies-a-variable-c-path-commit` deny (a commit
    through `cd` to a shell variable, re-run with the literal path), and the
    `cwd-drift` warnings of a hub-rooted seat working in the clone.

  The register at the hub's `6af670df` holds all four classes, so the seats
  go to the coordinator as text.
- **The merge word** for this pass: Omid, first-hand in its terminal between
  its reads of 15:50:00Z and 15:50:09Z, answering whether the pass may push,
  open this docs-only PR, merge it on green and land its two wraps: "Yes,
  merge when green (Recommended)".
- **Routed, in order:**
  1. Story 2.1.7, Omid's sitting, which need not wait for Story 2.1.17;
  2. Story 2.1.17, anywhere before Story 2.1.8;
  3. Story 2.1.8, Epic 2.1's close.

  The cost of sitting first is no margin: every figure the sitting reads is
  unchanged and reproduces. If the sitting decides a lost tag's channel
  search or the EU channel plan in the rebuild spec, Story 2.1.17 writes
  the spec's answer into the study.

**Story:** As a stranger reading the study, I want a Reader's edge, the
count's wait and the conference hall's second Reader stated where the study
is silent, so that the design reads whole before Epic 2.1 closes.
**Context:** born 2026-10-30 by the Story 2.1.15 reviewer pass (tigerfid
#69), from its three Minors and three nits (Story 2.1.15's `**Reviewed:**`
record). It gates nothing in the sitting. No item moves a margin the
sitting chooses a cell from, and each is one sentence or a mark. It goes
anywhere before Story 2.1.8. If the sitting runs first and decides Minor A
or C in the rebuild spec, this story writes the spec's answer into the
study. Each item says what the review MEASURED and what is INFERRED.
- **Minor A, the EU at a Reader's edge** (`docs/study/radio-and-location.md`
  § Channels; § Zones, A Reader's edge). MEASURED: the EU's band "carries
  five 600 kHz channels", a network keeps its channel, and no line says
  which channel each EU Reader takes. INFERRED: on one channel, the edge
  bay's two Field Generators meet 0.32 % of the time, unbounded as their
  crystals agree. The study says which channel each EU Reader takes, or that
  neighbouring Readers take different channels of the five.
- **Minor B, [21] against the count's wait** (`radio-and-location.md`
  § Zones, The layout of a listen). MEASURED: the warehouse's count steps at
  most once in 5 minutes, and "What it leaves unchanged" does not name [21].
  INFERRED: the day's inventory still finishes in about 5.9–6.1 of [21]'s 10
  minutes. The study states the inventory against [21], with the wait
  counted.
- **Minor C, the conference hall's second Reader** (`radio-and-location.md`
  § Range, § Anti-collision, § Zones). MEASURED: [2] puts a second Reader in
  the hall, and the study counts the conference as one block, an inner
  area's set as one run of nine. INFERRED: if that Reader keeps its own
  network, an area beside the edge holds [9] by 9 %, not 22 %, unless the
  warehouse's route adds six Field Generators. The study says whether that
  Reader listens on the first's network or keeps its own. If its own, it
  says how an area beside the edge hears its set, and re-runs [9] there.
- **The three nits,** each MEASURED:
  - `docs/reference-research.md` has no Story 2.1.15 entry for its read of
    R&L's page, in § 4 or § Searches already run;
  - "about 6.5 minutes after an hour lost" (`radio-and-location.md`
    l.575–578) is the US figure, and the sentence names the US only in its
    third clause;
  - the three reasoned bullets under the edge table (l.801–822) carry no
    INFERRED mark.
- **What stays.** No requirement number moves (Sprint 4's D1). A figure
  that a dated pointer line in `requirements.md` quotes, and that this story
  changes, gets a new dated pointer line. Story 2.1.15's Outcome and its
  `**Reviewed:**` record are records and are not edited.
**Done when:** each of Minors A–C and the three nits is corrected in its
document, or its sentence shown correct with its source; every changed
figure is sourced, MEASURED or INFERRED with its arithmetic; no requirement
number moved; and the PR body quotes each item's grep with each hit's
disposition (manifest prose check `story2115-review-fixes`).

#### Story 2.1.17: The Story 2.1.16 review's fixes: a lost EU tag's channel search, and the inventory on shared channels — ✅ DONE (2026-10-30)

**Completed:** 2026-10-30 — tigerfid #79 (branch `story-2.1.17-review-fixes`, pre-tick head `e9232732`).
**Outcome:** the review's two Minors are corrected in `radio-and-location.md`. No requirement number moved, and no printed figure changed: every figure added reproduces from the studies' inputs by a script, and `requirements.md` is unchanged, since none of its six dated pointer lines quotes a figure this story adds. The sitting had not run, so the study writes its own answers, for the sitting to confirm or overrule:
- **Minor D** (§ Channels; § Zones, the lost tag's window and cost): a lost EU tag searches its site's channels, which a Reader names when the tag joins, the spare left out (five would take 18.3 minutes). Until its window reaches a frame, after about 35 minutes lost, each search keeps its own Reader's channel, since a narrower window meets another network only by chance. After that it tries the next at each search, wherever it lies. A tag lost in its own Reader's area is found within about 4 minutes in its first 35 minutes lost, at any site. After that, it is found within about 4 minutes at the ward and the conference, and within four searches, 14.7 minutes, in the warehouse.
- **Minor E** (§ Channels; § Zones, the day's inventory): EU Readers that share a channel under the 2 × 2 pattern run their roll calls at once. They stand two bays apart, about 60 m, twice the 30 m at which bays rarely hear each other in the US (6.0 dB more free-space loss), so the inventory keeps its 5.9–6.1 minutes. If the build finds that they hear each other, they take turns (each tag still hears one beacon and sends one reply), and the inventory takes about 8.9 or 8.0 minutes, inside [21]'s 10.

**Reviewed:** 2026-10-30, by the Story 2.1.17 reviewer pass (tigerfid #81).
- **The pass:** a reviewer session on the Mac, "Mac Reviewer", on the fleet
  coordinator's orders, launched in the Mac's home folder and working from
  the hub checkout. It was a fresh launch: its background-task path id
  equals its session id, `86ac7523-9bad-4199-8c93-a5a8b64ce65b`. It was not
  elevated (uid 501) and did none of this story's work.
- **Verdict: PASS WITH FINDINGS.** The story meets its Done-when. No finding
  is Important, and none moves a figure the sitting chooses a cell from:
  [9]'s 22 % in the hall, [27]'s 1.21 and 1.10 of five years and every
  tier's total are unchanged, since the five other study documents keep
  their blobs. Two Minors go to Story 2.1.18, which gates nothing in the
  sitting. **Story 2.1.7's sitting can proceed.**
- **The method:** the pass read the repo at `6e6b6cb` (#79 → `185dc2db`,
  then its wrap, #80) and #79's diff from `9e81b48`. The leg's study
  commit, `e9232732`, read from the PR's head ref, holds the study exactly
  as merged, and the merge's tree is the PR head's (`dbb3e7fc`). One script
  rebuilt every figure #79 adds from the studies' stated inputs alone,
  censused the numbers on the lines #79 removed, re-ran #79's two greps and
  censused their needles across line breaks.
- **The merge:** #79 → `185dc2dba512861eb54417b6ba2cba1c28ffa0cd`, GitHub's
  stamp 20:28:35Z, read from the API between this pass's `date -u` reads of
  20:32:49Z and 20:40:41Z.
- **The inputs the script used,** each as the studies state it: a wake's
  start-up 2.795 µC; listening 5.8 mA; a frame 250 ms; the lost tag's
  6.6 µA; drift 2 × 60 ppm; [19]'s 100,000 items, [20]'s 9–12 Readers and
  [21]'s 200 a second; the count's wait, 300 + 12 + 0.25 s; § Range's
  margins and its free-space formula. It checked 23 printed figures, and
  none differs.
- **Minor D, a lost EU tag's channel search: MET.**
  - The search covers the site's channels, named by a Reader when the tag
    joins (`radio-and-location.md` l.560–563), as a US tag "is given the
    order when it joins" (l.494–495). The spare stays out (l.581–582).
  - The figures reproduce: 2.795 µC + 5.8 mA × 250 ms = 1.4528 mC, 220.1 s
    at 6.6 µA; 4 × 220.1 s = 14.67 minutes; 5 × 220.1 s = 18.34 minutes;
    0.25 s ÷ (2 × 60 ppm) = 2,083 s, 34.7 minutes.
  - The own-channel start holds: a tag times its window on its own
    network's slots (l.745–751), so a search narrower than a frame meets
    another network only by chance. In its first 35 minutes a search costs
    less and comes sooner (l.643–645), so "within 220 s" bounds it. What
    this leaves for a tag carried into another Reader's area is Minor G.
- **Minor E, the EU inventory on shared channels: MET.** The study says the
  Readers roll call at once and gives the time if they take turns
  (l.527–547, l.811–815, l.831–833).
  - The grid reproduces: under (row mod 2, column mod 2), a 3 × 3 grid
    puts 4, 2, 2 and 1 Readers on its four channels, and a 3 × 4 grid 4, 4,
    2 and 2. No two-dimensional grid of 9–12 Readers puts more than four
    on one channel (2 × 5 and 2 × 6 put three), and four on a channel take
    8.5 minutes among 10 Readers and 8.2 among 11, inside the range.
  - The times reproduce: 4 × 100,000 ÷ 9 = 44,444 items, 222.2 s, and
    312.25 + 222.2 = 534.5 s, 8.91 minutes; 4 × 100,000 ÷ 12 = 33,333,
    166.7 s, 478.9 s, 7.98 minutes; at once, 353.9–367.8 s, 5.90–6.13
    minutes; 20·log₁₀(60 ÷ 30) = 6.02 dB.
  - Taking turns keeps one beacon and one reply: a tag keeps the Reader's
    time on its set's slots at every listen (l.745–751), so a turn up to
    222 s after the step needs no second beacon, and § [27] stands.
  - The at-once choice's premise is Minor F.
- **What stays.**
  - No requirement number moved. `requirements.md`,
    `parts-and-toolchains.md`, `tiered-cost.md`, `software-stack.md` and
    `original-system.md` have the same blob at `cdaa8e5` (the Story 2.1.16
    review's reading), at `9e81b48` and at `185dc2db`.
  - No printed figure changed: every number on a line #79 removed from the
    study (20 distinct) is on a line it added.
  - No dated pointer line is owed. An observation, not a finding:
    `requirements.md` carries eleven dated lines, not the six #79 counts.
    The five more (l.113, l.121, l.128, l.131, l.136) sit under § The
    arithmetic behind the numbers and quote the cell, a reply and
    listening, none of which #79 changes.
  - Story 2.1.16's Outcome and `**Reviewed:**` record are untouched: #79's
    plan diff is Story 2.1.17's heading, `**Completed:**` and
    `**Outcome:**` alone.
- **The greps:**
  - #79's two reproduce at `185dc2db` with the counts its body gives:
    Minor D 10 hits (6 at `9e81b48`), Minor E 14 (7 at the base).
  - A census of the same needles across line breaks finds the two wrapped
    hits #79 names, "four searches" at l.651–652 and "share a channel" at
    l.531–532, and no other.
  - The hub's `check-item8-dispositions.mjs` reads #79's body: 24
    dispositions, 8 current, 1 named. The named one, "requirements l.309:
    **current**", quotes nothing; the line reads "because readers that
    share a channel share its airtime", current as judged.
- **The battery** at `6e6b6cb`, under the Mac's lock (GO 20:40:41Z, END
  20:40:45Z):
  - `npm test`: exit 0, with `close-lint: ok (adopted 2026-10-26)`,
    `check-licenses: ok` and 22 of 22;
  - `node scripts/reference-check.mjs`: "reference-check: ok — 299 rows
    verified, README the stub";
  - the close runner over the eleven teeth: ten PASS, and
    `rebuild-spec-approved` FAIL (exit 1), as the plan expects until Story
    2.1.7.
- **Findings, Minor** (to Story 2.1.18):
  - **F. Minor E's at-once choice rests on a premise the study's own
    figures read against.**
    - MEASURED, the text: Readers on one EU channel "stand two bays apart,
      about 60 m, twice the 30 m at which bays rarely hear each other in
      the United States (above)", and "So each runs its own roll call at
      [21]'s 200 a second" (`radio-and-location.md` l.532–537). The US
      sentence is one line with no figure: "Bays 30 m apart rarely hear
      each other" (l.504). Against it, [20] asks a Reader to read a tag at
      30 m among metal racking (`requirements.md` l.298–302), and § Range's
      EU tag-to-Reader row closes at 30 m with 22.2 dB to spare at 20 dB of
      body loss (l.472).
    - INFERRED: at 60 m, free space takes 6.0 dB of that margin and leaves
      16.2 dB, before whatever the racking beyond [20]'s 30 m costs, which
      the study does not price. A tag in the other Reader's bay can stand
      nearer, about 45 m with each Reader mid-bay: 3.5 dB, 18.7 dB left. So
      on the study's own budget two Readers on one EU channel can hear each
      other's tags, and whether their roll calls collide is a question for
      the build's test, not a conclusion. The premise was the Story 2.1.16
      review's own suggested answer ("by the US bullet's reason at twice its
      distance"), and #79 followed it.
    - The answer is one sentence: the study drops the premise and states
      the at-once choice as the build's to confirm, with 8.0–8.9 minutes as
      the EU warehouse's planning figure until [21]'s test on two Readers
      that share a channel reads otherwise, both inside [21]'s 10; or it
      prices the racking between them. The US sentence gains its basis or
      goes: that bullet's conclusion rests on its 23 % arithmetic, which
      reproduces (1 − (41/42)¹¹ = 23.3 %). No requirement number and no
      battery figure moves.
  - **G. A tag carried past a Reader's edge bay: its time to be found,
    counted from the loss, is unstated and unweighed.**
    - MEASURED, the text: an item carried across an edge joins the next
      Reader once the host places it in the far bay, from its own network's
      second Field Generator there (`radio-and-location.md` l.864–882). A
      tag that hears none of its set in two listens is lost (l.623–625).
      Its search keeps its own Reader's channel until its window reaches a
      frame, after about 35 minutes (l.563–568), and it is then found
      "within four searches" (l.579–581). [22] asks "a move shown within
      5 min" and lets the next inventory pass find a missed one
      (`requirements.md` l.317–320). [25] alarms after four missed
      check-ins at one every 15 minutes.
    - INFERRED: a tag carried more than a bay past the edge before a
      listen places it hears nothing of its own network there. In an EU
      warehouse it is then found up to 2,083 s + 4 × 220.1 s = 2,964 s,
      about 49 minutes, after it is lost. [25]'s fourth missed check-in
      falls 60 minutes after the last one made, so a loss that begins more
      than 10.6 minutes after a check-in can raise a missed-check-in alarm
      before the tag is found, at the worst search order. #79's own-channel
      start makes this explicit. Before it, "tries the next channel at each
      search" read as about 15 minutes.
    - The answer is one or two sentences: the time from the loss for a tag
      carried past the edge bay, weighed against [22] and [25]. For
      example, the first 35 minutes could interleave a frame's listen on
      the site's other channels with the narrow own-channel searches,
      inside the 6.6 µA, or the host could hold [25]'s alarm while a
      carried tag may still be searching. The US face is the same question,
      INFERRED: a lost US tag times its window on its own network's hop, so
      it meets another Reader's network only by chance until its window
      reaches the cycle, about 24 hours lost (l.637–642). The story states
      it, or leaves it to the sitting with that reason.
- **The recurrence triage** (hub KB `epic-close-protocol`): each text seat
  falls in a decided class, so none qualifies for a new decision.
  - #79's one seat is an `item-8-anchor-grep-misses-falsified-claims`: the
    Item-8 reader's named disposition, above.
  - The executor's two, read off #80's note and not measured here: a
    `piped-checker-exit` (the tick's push piped into `grep`) and the
    `cwd-drift` warnings of a hub-rooted seat in the clone.
  - This pass's are two `piped-checker-exit` (the orders' pre-read piped a
    fast-forward and four fetches into `tail`, and this pass piped its
    fetch into `grep`, each then read unpiped by `git rev-parse`), one
    `zsh-equals-face` (an `echo ==` separator, "= not found") and one
    `zsh-word-split` (a `set -- $p` loop over quoted pairs, exit 128), all
    caught.
  - None of the hub's guards fired in this session, which was launched in
    the home folder, not the hub. The `echo ==` face ran where the zsh
    guard denies it since hub #3131.

  The register at the hub's `53cb80a6` holds all five classes, so the
  seats go to the coordinator as text.
- **The merge word** for this pass: Omid, first-hand in its terminal between
  its reads of 20:17:04Z and 20:20:02Z: "Please merge any PRs you open
  during this session when ready and in the right order."
- **Routed, in order:**
  1. Story 2.1.7, Omid's sitting, which need not wait for Story 2.1.18;
  2. Story 2.1.18, anywhere before Story 2.1.8;
  3. Story 2.1.8, Epic 2.1's close.

  The cost of sitting first is no margin: every figure the sitting chooses
  a cell from is unchanged and reproduces. If the sitting decides the EU
  channel plan, a lost tag's search or [25]'s alarm rule in the rebuild
  spec, Story 2.1.18 writes the spec's answer into the study.

**Story:** As a stranger reading the study, I want a lost EU tag's channel
search and the EU inventory's shared channels stated where the study is
silent, so that the design reads whole before Epic 2.1 closes.
**Context:** born 2026-10-30 by the Story 2.1.16 reviewer pass (tigerfid
#74), from its two Minors (Story 2.1.16's `**Reviewed:**` record). It gates
nothing in the sitting: neither item moves a margin the sitting chooses a
cell from, and each is one sentence. It goes anywhere before Story 2.1.8.
If the sitting runs first and decides a lost tag's channel search or the
EU channel plan in the rebuild spec, this story writes the spec's answer
into the study. Each item says what the review MEASURED and what is
INFERRED.
- **Minor D, a lost EU tag's channel search**
  (`docs/study/radio-and-location.md` § Channels; § Zones, How a tag hears
  a Field Generator it has not heard before). MEASURED: a lost tag "tries
  the next channel at each search", and the EU's time to be found is one
  search, about 4 minutes, or four searches, about 15 minutes, "for a tag
  lost in another Reader's area". No line says which channels the cycle
  covers. INFERRED: a lost tag cannot tell whose area it is in, so one lost
  in its own Reader's area also waits up to four searches (14.7 minutes),
  or five over all five channels (18.3 minutes). The study says which
  channels a lost tag's search covers, and gives the EU's time to be found
  for a tag lost in its own Reader's area.
- **Minor E, the EU inventory on shared channels** (`radio-and-location.md`
  § Channels; § Zones, The layout of a listen; `requirements.md` [21]).
  MEASURED: [21]'s 200 a second is per channel, since "readers that share a
  channel share its airtime". Minor B's 5.9–6.1 minutes give every Reader's
  share 200 a second at once, while the EU's 2 × 2 pattern puts Readers two
  apart on one channel. INFERRED: were those Readers to take turns, the
  inventory would take about 8.0–8.9 minutes, still inside [21]'s 10. The
  study says whether EU Readers on one channel roll call at once, or gives
  the time if they take turns.
- **What stays.** No requirement number moves (Sprint 4's D1). A figure
  that a dated pointer line in `requirements.md` quotes, and that this story
  changes, gets a new dated pointer line. Story 2.1.16's Outcome and its
  `**Reviewed:**` record are records and are not edited.
**Done when:** each of Minors D and E is corrected in its document, or its
sentence shown correct with its source; every changed figure is sourced,
MEASURED or INFERRED with its arithmetic; no requirement number moved; and
the PR body quotes each item's grep with each hit's disposition (manifest
prose check `story2116-review-fixes`).

#### Story 2.1.18: The Story 2.1.17 review's fixes: the EU inventory's at-once premise, and a carried tag's time to be found

**Story:** As a stranger reading the study, I want the shared EU channel's
roll calls argued from the study's own link budget, and a tag carried past
a Reader's edge given its time to be found, so that the design reads whole
before Epic 2.1 closes.
**Context:** born 2026-10-30 by the Story 2.1.17 reviewer pass (tigerfid
#81), from its two Minors (Story 2.1.17's `**Reviewed:**` record). It gates
nothing in the sitting: neither item moves a margin the sitting chooses a
cell from. It goes anywhere before Story 2.1.8. If the sitting runs first
and decides the EU channel plan, a lost tag's search or [25]'s alarm rule
in the rebuild spec, this story writes the spec's answer into the study.
Each item says what the review MEASURED and what is INFERRED.
- **Minor F, the at-once premise** (`docs/study/radio-and-location.md`
  § Channels, the US bullet and the shared-channel bullet; § Range;
  `requirements.md` [20]). MEASURED: the at-once choice rests on "the 30 m
  at which bays rarely hear each other in the United States", a sentence
  with no figure, while [20] asks a Reader to read a tag at 30 m among
  racking and § Range's EU uplink closes at 30 m with 22.2 dB to spare.
  INFERRED: at 60 m, 16.2 dB is left in free space, before the racking
  beyond 30 m. The study states the at-once choice as the build's to
  confirm, with 8.0–8.9 minutes as the planning figure until then, or
  prices the racking. The US sentence gains its basis or goes.
- **Minor G, a carried tag's time to be found** (`radio-and-location.md`
  § Channels; § Zones, How a tag hears a Field Generator it has not heard
  before, and A Reader's edge; `requirements.md` [22], [25]). MEASURED: a
  lost tag keeps its own channel until its window reaches a frame, about
  35 minutes, then cycles; [22] asks a move shown within 5 minutes, and [25]
  alarms after four missed 15-minute check-ins. INFERRED: a tag carried
  more than a bay past a Reader's edge is found up to about 49 minutes
  after it is lost in an EU warehouse, and a loss that begins more than
  10.6 minutes after a check-in can alarm first. The study gives that time
  from the loss and weighs it against [22] and [25]. It answers the US face
  too, or leaves it to the sitting with its reason.
- **What stays.** No requirement number moves (Sprint 4's D1). A figure
  that a dated pointer line in `requirements.md` quotes, and that this story
  changes, gets a new dated pointer line. Story 2.1.17's Outcome and its
  `**Reviewed:**` record are records and are not edited.
**Done when:** each of Minors F and G is corrected in its document, or its
sentence shown correct with its source; every changed figure is sourced,
MEASURED or INFERRED with its arithmetic; no requirement number moved; and
the PR body quotes each item's grep with each hit's disposition (manifest
prose check `story2117-review-fixes`).

#### Story 2.1.7: The decision sitting and the rebuild spec — ✅ DONE (2026-10-30)

**Completed:** 2026-10-30 — tigerfid #83 (branch `story-2.1.7-rebuild-spec`, pre-tick head `40eaa802`).
**Outcome:** the rebuild's design spec, `docs/superpowers/specs/2026-10-30-rebuild-design.md`, carries Omid's approval, first-hand in the terminal of the session "Windows 5" between its `date -u` reads of 22:00:40Z and 22:01:09Z: "Approved". The sitting ran there from 20:45:34Z to 21:41:39Z on the six studies and the brief, `docs/study/2.1.7-decision-brief.md`, which lands here as its input (#76 closed in its favour).
- **The FCC grant read** (decision 1): both grants, ZAT-1312PSIP-2 and -3, are single modular approvals of the frequency-hopping class, and no public test report shows the 300 kbit/s PHY the design rests on; -3's Wi-SUN report tests 100 kbit/s only (MEASURED, each PDF's SHA-256 matching its page). Omid kept 300 kbit/s, and TI is asked in writing which grant covers it; TI's written answer gates every board (decisions 2 and 12; the spec's gate G1).
- **The other decisions:** the hall's second Reader on the first Reader's network; the EU's 2 × 2 channel pattern, the inventory planned on Readers taking turns; [22] binding a tag carried past a Reader's edge, which Story 2.1.18 designs; one tag core with a card-sized variant only if the range test needs it; the item tag at 12 s on the CR2450 variant; Energizer cells; the worn tags' rule after the bench measures them; no sensor accuracy as a requirement; Omid's first build on the advanced tools with fully assembled boards, the spare boards unpowered; the EU build priced and guided (Story 2.1.19, born here); customers' devices left out of the spec; the starting layout; a host for several Readers and a network-link footprint on the Reader.
- **For Stories 2.1.18 and 2.1.19:** the spec's § 5.3 holds 3b's and 3c's answers, which Story 2.1.18 writes into the study; § 6.3 asks for 2.1.19. Story 2.1.8 waits on both (the spec's gate G5).

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

#### Story 2.1.19: The EU build, priced — ✅ DONE (2026-10-30)

**Completed:** 2026-10-30 — tigerfid #86 (branch `story-2.1.19-eu-build-priced`, pre-tick head `ea612be2`).
**Outcome:** `docs/study/tiered-cost.md` § The EU build prices each part the EU build changes, from pages read 2026-10-30 between this story's clock reads of 22:13:25Z and 22:23:01Z, in euros, taken net of Germany's 19 % VAT and converted at the ECB's reference rate of the day, 1 EUR = 1.1172 USD. No US figure, total or requirement number moved: a script rebuilt all 20 US figures from their unrounded lines, none differing.
- **The parts:** the 868 MHz chip antenna is Johanson's 0868AT43A0020001E, named from its maker's page; its price is not read. Pulse's datasheet gives the W5017 868–928 MHz and 0.89–0.93 dBi at 868 MHz, so the EU build keeps the US build's whip, unread as before. The Field Generator's and the starter iron's supplies are priced with EU plugs (7.42 and 14.55), and the pro tier's Hakko FX-971 at 430.12 (unread in the US build). The two ATTEN stations' 230 V models are not read; the hot plate's supply takes 100–240 V and lacks only its plug; the microscope's light, on 110 V, lacks a 230 V supply.
- **Omid's word:** the four refused prices were put to him first-hand in this story's terminal, in one message, between its clock reads of 22:23:33Z and 22:24:17Z: "Write all as not read (Recommended)".
- **The EU totals,** each lacking parts the US totals count: starter fully assembled 598.18, advanced partly assembled 1,387.85, pro hand-solder 2,144.08, and the spec's first build, advanced fully assembled, 1,413.52.

**Story:** As a beginner in the European Union, I want the EU build's parts
priced beside the US build's, so that the guides' shopping lists serve me
too.
**Context:** born 2026-10-30 by the decision sitting (Story 2.1.7), from
Omid's decision 9a, first-hand in the terminal of the session "Windows 5":
"Price and guide the EU too", between its `date -u` reads of 21:35:18Z and
21:37:50Z, and, asked when, "A study story before this epic closes",
between its reads of 21:37:50Z and 21:38:30Z. The rebuild spec's § 6.3 asks
for it and its gate G5 puts it before Story 2.1.8. MEASURED, what the study
holds: `docs/study/tiered-cost.md` § The EU build names, unpriced, the parts
an EU build changes: the tag's 868 MHz chip antenna, which no study names
(the 0915AT43A0026001E is a 915 MHz part); the whips at 868 MHz, since
whether the W5017 serves 868 MHz is unread (its seller pages refused); the
230 V plugs, the Field Generator's wall supply and the tiers' wall-powered
tools (the ST-2090D is named a 110 V model, the ST-862D's voltage is unread,
and the microscope's light runs on a 110 V supply); and the Reader's 25 mW
e.r.p., a setting that costs nothing. The module, the boards, the cells and
the bench are the same parts (the same section). INFERRED: the hot plate's
supply runs from 100–240 V (§ What each tool needs to run), so it needs only
its plug. Sprint 3's D5–D7 bind every price, as they bind the US build's: a
seller's page and a read date on each, a price no session could read marked
not read and named in its tier's Lacks, never counted as zero, and an
unnamed part priced as a representative part, marked so.
- **What stays.** No US figure, total or requirement number moves. The EU's
  legality is the study's (`docs/study/radio-and-location.md` § Legality of
  the choice), stated as the builder's own compliance; this story prices
  parts, and decides nothing about the law.
**Done when:** `docs/study/tiered-cost.md` § The EU build prices each part the
EU build changes, each from its seller's page with its currency and read
date, the 868 MHz chip antenna named from its maker's page and the whip's
gain at 868 MHz read; gives each tier's EU total, summed before rounding,
with its Lacks; leaves every US figure unchanged; and the PR body quotes the
section's grep (manifest prose check `eu-build-priced`).

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
**Waits on:** Stories 2.1.18 and 2.1.19, which the rebuild spec's gate G5
puts before the close (`docs/superpowers/specs/2026-10-30-rebuild-design.md`
§ 8).
**Done when:** every manifest check passes or is adjudicated, the archive
entry carries the evidence block, Initiative 2 is cut whole, and the build
initiatives are filed in the same PR.
