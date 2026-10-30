# The TigeRFID rebuild: design

**Status:** written for Omid's approval (plan Story 2.1.7). The approval line
replaces this one when he has read it.

The rebuild's design, decided in one sitting from the six study documents under
`docs/study/` (plan Story 2.1.7). A builder plans the build initiatives from it,
and its acceptance target is milestone 1 (plan Initiative 2). Where it states a
figure, it cites the study section the figure is read from. Where the sitting
decided, it quotes Omid's answer with the clock reads around it. Where this
document made a call of its own, § 11 lists it.

The marks mean what the studies say they mean: **MEASURED** was run or read
first-hand, with its date; **INFERRED** was reasoned, with its arithmetic or
its reason; **ASSUMED** is an input to arithmetic, not a measurement. Nothing
here has been measured on a board. This is the project's reading of radio
rules, made so that it can decide. It is not legal advice.

## 1. What this is for

TigeRFID is the ActiveWave active-RFID system, built from 2001 and
open-sourced in 2017–2018 (`readme.md`). The rebuild keeps its ideas (zones
from Field Generators, wake on command, relay outputs, owned infrastructure
with no subscription, pairing people with things; `radio-and-location.md`
§ Does ActiveWave's approach still win, `software-stack.md` § Does
ActiveWave's approach still win) and replaces every board, firmware image and
program, so that:

- a complete beginner builds one Reader, one Field Generator and three tags,
  and runs the host software on their own Windows, macOS or Linux machine,
  with free tools only (milestone 1, § 6);
- the same design meets the three acceptance scenarios' 27 requirements
  (`requirements.md`): a conference where each attendee is shown on the
  floorplan, a hospital nursery where staff find each baby's tag and read its
  temperature sensor, and a warehouse that inventories 100,000 items in
  minutes.

**Why it matters soon** (Omid, in the sitting, before decision 2, first-hand in
the terminal of the session "Windows 5" between its `date -u` reads of
21:02:18Z and 21:12:03Z): "I do expect real deploys relatively quickly - some
of the old customers in particular, many of whom had dozens of readers, tens
of dozens of field generators, and thousands if not tens of thousands of tags.
They would want to upgrade their hardware and software right away if we come
up with something good." That is why decision 2 asks TI its question now, and
why its answer gates the first tag board (§ 5.2).

## 2. Goals, non-goals and anti-goals

**Goals:**
- milestone 1, as plan Initiative 2 states it (§ 6), accepted when Omid has
  built it from the guides (spec D9 of the onboarding spec);
- the 27 requirements met on paper by this design, then by the build's tests
  (`requirements.md` § The five measures, defined);
- guides and tiered shopping lists for the US build and the EU build
  (decision 9a, § 6.3).

**Non-goals:**
- **supplying devices to customers.** Devices made for others are "marketed",
  and their compliance (the label, the Reader's emissions under Part 15
  Subpart B, RF exposure for worn tags, the EU's conformity assessment) is
  left out of this spec by Omid's word (decision 9b). § 7 records what the
  studies and the sitting found about it, for whoever plans it;
- the LAN tier: HTTPS, user accounts, share links and phone scanning
  (`software-stack.md` § What the choice leaves to later stories);
- Ethernet on the Reader; an HF (NFC) interface fitted on the seal; location
  finer than a zone (ultra-wideband stays a later comparison,
  `radio-and-location.md` § The other options, one by one).

**Anti-goals** (plan Initiative 2), which bind every answer here:
- nothing a beginner cannot build or debug;
- no dependence on a paid tool or a single vendor's cloud;
- no medical claim: no page calls the system a medical device or a monitor of
  vital signs (spec C8). The nursery's temperature alarm means "this tag's
  sensor reading crossed a limit someone set on screen" (`requirements.md`
  § Ground rules);
- no transmitter a beginner cannot legally run.

## 3. How the sitting ran

- **Its input:** the six study documents at `a52cbf3` (their blobs are those of
  `185dc2db`, Story 2.1.17), and the decision brief,
  `docs/study/2.1.7-decision-brief.md`, written at `354861d5`. Story 2.1.17
  (#79) and its review (#81, which filed Story 2.1.18) landed after the brief.
  The sitting read them, and decision 3 is put from them.
- **Already decided before it:**
  - the onboarding spec's D1–D12 (milestone 1's five devices, KiCad 10, three
    platforms, three tiers on every list, public by design, Omid as the first
    builder, Chorus as the named comparison);
  - the 27 numbers of `requirements.md`, which change only by Omid's word;
  - three software items, by Omid ahead of the sitting
    (`software-stack.md` § Decided ahead of the sitting): the host is Python;
    milestone 1's seal fits no HF interface, and its core keeps an unfitted
    footprint for one, with the coil off the core; raw check-ins are kept
    7 days by default, and the history until the owner deletes it.
- **The sitting:** 2026-10-30, in the terminal of the session "Windows 5",
  between its `date -u` reads of 20:45:34Z and 21:41:39Z. Ten decisions,
  each put to Omid with a recommendation, the figures that drive it and the
  strongest case against. His answers:

| # | Decision | Omid's answer, first-hand | Between the reads of | Here |
|---|---|---|---|---|
| 1 | The FCC grant read | "I can read them now." Then, for the test report: "Happy to pull that Wi-SUN test rport if you help me find it." | 20:45:34Z–20:47:54Z; 20:51:09Z–20:53:39Z | § 5.2 |
| 2 | The radio | "300 kbit/s; ask TI now (Recommended)" | 21:12:03Z–21:13:08Z | § 5.1, § 5.2 |
| 3a | The conference hall's second Reader | "First Reader's network (Recommended)" | 21:13:08Z–21:16:40Z | § 5.3 |
| 3b | The EU's channels | "2x2; plan on turns (Recommended)" | the same | § 5.3 |
| 3c | A tag carried past a Reader's edge | "[22] binds; 2.1.18 designs it (Recommended)" | the same | § 5.3 |
| 4 | One core or three | "One core; card variant if needed (Recommended)" | 21:16:40Z–21:17:09Z | § 5.4 |
| 5a | The item tag's cell | "12 s on the CR2450 variant (Recommended)" | 21:17:09Z–21:18:58Z | § 5.5 |
| 5b | The cells' maker | "Energizer (Recommended)" | the same | § 5.5 |
| 6 | The worn tags' cells | "Measure first, then the rule (Recommended)" | 21:18:58Z–21:19:40Z | § 5.5 |
| 7 | The sensor | "No accuracy; 8 averages default (Recommended)" | 21:19:40Z–21:30:38Z | § 5.5 |
| 8a | The first build | "Advanced tools, assembled (Recommended)" | 21:33:38Z–21:35:18Z | § 6.2 |
| 8b | The spare boards | "Unpowered spares; read first (Recommended)" | the same | § 6.2 |
| 9a | The EU build | "Price and guide the EU too"; then, on when: "A study story before this epic closes" | 21:35:18Z–21:37:50Z; 21:37:50Z–21:38:30Z | § 6.3 |
| 9b | Customers' devices | "Leave it out of this spec" | 21:35:18Z–21:37:50Z | § 2, § 7 |
| 10 | The layout | "The starting shape (Recommended)" | 21:38:30Z–21:41:39Z | § 5.7 |
| — | The study's unflagged choices, as a block | "Confirm all (Recommended)" | the same | § 5 |

Before decision 8 he asked, between the reads of 21:30:38Z and 21:33:38Z:
"Wouldn't it be easier for us to start at the Advanced tier and then create
the lower tiers once we've validated the highest tier or am I mistaken?" The
answer given: the tiers build the same boards from the same parts and differ
in tools and in who places the parts, so the first build validates the one
design with the advanced tools (§ 6.2). Before decision 2 he asked, between
the reads of 21:02:18Z and 21:12:03Z: "I have no idea what would be the best
choice here. What makes you recommend what you've recommended?"

## 4. The block the sitting confirmed

Choices the studies made, each with its rejected options in its own section,
confirmed in one word (§ 3's last row). They are part of this design as the
studies state them:

1. **A tag listens to its zone's and its neighbours' Field Generators,** named
   by the Reader when the host places it: 13.9 µA as weighed, against 71.9 µA
   for the whole block (0.23 years for [18]) and 42.9 µA for a wider listen
   every second time (`radio-and-location.md` § Zones, How a tag hears a
   Field Generator it has not heard before).
2. **Most listens skip the Reader's beacon.** A tag keeps the frame's time on
   its 32 kHz crystal, each slot it hears sets it again, and the host orders
   the block along the floorplan: [18] at 1.24 years one room away from its
   stretch, against 1.15 with a short sync and 0.87 with a second wake
   (§ Zones, The layout of a listen).
3. **At a Reader's edge, a Field Generator of each network** in the bays either
   side, so every item's set is one run of three in its own Reader's block:
   1.21 and 1.10 of five years on a CR2032 (§ Zones, A Reader's edge).
4. **The listen intervals:** 2 s for the conference, 1 s for the nursery,
   12 s for the item tag (`parts-and-toolchains.md` § Each worn tag's year,
   § [27]).
5. **Joining, roll calls and the US hop:** tags join in a short window of each
   frame and answer roll calls in reader-assigned slots; the US network hops
   pseudo-randomly over 42 channels, 0.25 s a frame, a 10.5 s cycle
   (`radio-and-location.md` § Anti-collision, § Channels).
6. **Each device's parts** (`parts-and-toolchains.md` § The parts, device by
   device), with no Ethernet in milestone 1.
7. **The free toolchains:** KiCad 10, TI's SimpleLink Low Power F2 SDK,
   SysConfig, TI Arm Clang or the Arm GNU route, CMake, GNU make, Code
   Composer Studio (optional) and UniFlash; TI's cloud tools, SmartRF Studio
   and IAR declined (`parts-and-toolchains.md` § The toolchains).
8. **Nothing copied from TI's reference designs:** the boards are drawn fresh in
   KiCad from written layout rules (`parts-and-toolchains.md` § The licences
   of the reference designs read). The -3 grant's condition on a "trace
   reference design" is gate G2 (§ 8).
9. **The host's design:** the event schema, the API sketch, the floorplan by
   zone, the four alarms with Chorus's two, scan-and-scan pairing and the
   security rules (`software-stack.md`).
10. **The Chorus answers and "Does ActiveWave's approach still win",** both
    sides (`radio-and-location.md`, `software-stack.md`).

## 5. The design

### 5.1 Radio and location (decision 2)

**Option B: one sub-GHz band, a certified module, reader-assigned slots, and
zones from Field Generators** (`radio-and-location.md` § The choice).

- **Every device carries the same radio:** TI's CC1312PSIP, a 7 mm × 7 mm
  module with its own Arm Cortex-M4F (PSIP p1), at the Wi-SUN **Mode #5**
  PHY: 300 kbit/s, 2-GFSK, ±75 kHz deviation, 600 kHz channels
  (`radio-and-location.md` § The radio chosen; SWRA797 Table 2-1). The PHY
  stays at the SDK's default settings, which the certification requires
  (SWRA797 p6).
- **Powers** (SWRA797 §2.1.5, §2.2.5): tags reply at 12.0 dBm on the 14 dBm
  port; the Reader and the Field Generators send at 17 dBm on the 20 dBm port,
  without duty cycling. In the EU the Reader stays at 25 mW e.r.p., 15.2 dBm
  conducted at most with its whip (`parts-and-toolchains.md` § The Reader).
- **Antennas:** the Pulse W5017 whip (+0.90 dBi) on the Reader and the Field
  Generator; the Johanson 0915AT43A0026 chip antenna (+1.0 dBi) on the tags
  (`parts-and-toolchains.md` § The Reader, § The tags).
- **Throughput:** a slot is 0.7333 ms (a 20-byte reply, 0.533 ms, and 0.2 ms
  of guard, ASSUMED), 1,363 slots a second on one channel. With the beacon, the
  Field Generators' block, the joining window and the downlink slot counted,
  the conference keeps 1,296 reply slots a second, so a hall of 1,000 tags is
  read in about 0.77 s ([3]); the ward keeps 1,284, so its 100 tags take
  0.08 s ([12]); one Reader's block holds up to 158 Field Generators at [3]'s
  rate (`radio-and-location.md` § Anti-collision).
- **Range ([2]):** 20.7 dB of margin at 30 m at the worst body loss
  (`parts-and-toolchains.md` § The battery, the link at 30 m). A crowd and
  indoor fading, ASSUMED 10–20 dB, leave 0.7–10.7 dB (INFERRED, the brief's
  arithmetic): met on paper, not with confidence. The mitigations are a Reader
  antenna mounted high and [2]'s second Reader (§ 5.3), and the build's range
  test decides.
- **Channels, United States:** the network hops as § 4 item 5 says, and each
  Reader hops on its own (§15.247(h)). Both FCC grants are of the frequency
  hopping class (§ 5.2), so the hop is required, and the design has it.
- **Zones:** a Field Generator is the same module fixed in a room or at an
  exit, sending its ID in its own slot of every frame at a power set for its
  room. A tag reports the Field Generators it hears when the strongest
  changes and with every check-in, and the host decides the zone, with
  hysteresis at doorways. An exit's Field Generator also listens. A move is
  shown in 3.0 s at a 1 s listen, inside [13]'s 5 s; 100 of 100 exit passes
  succeed with 98.8 % probability, which the build measures
  (`radio-and-location.md` § Zones).
- **Commands** ride in the Reader's beacon; each Field Generator's slot repeats
  a count the Reader steps when its beacon carries one. A warehouse Reader
  steps it at most once in 5 minutes, so a command reaches an item within
  about 5 minutes and 12 s (§ Zones, The layout of a listen).

### 5.2 The FCC grant read (decision 1), and the question to TI (decision 2)

**What was read, 2026-10-30** (MEASURED; § Sources):

| | ZAT-1312PSIP-3 | ZAT-1312PSIP-2 |
|---|---|---|
| Granted | 2024-05-20 | 2024-01-22 |
| Class | DSS, Part 15 spread spectrum transmitter | the same |
| Modular type | Single Modular | the same |
| Range, rule part | 902.2–927.8 MHz, 15C | the same |
| Most conducted power across the band | 73.5 mW and 65.3 mW | 24.5 mW |
| Its public test reports | WB-DSSS, TI 15.4, **Wi-SUN**, MIOTY, PowerG | three, no PHY in their titles |

- **The test reports' PHYs** (each PDF's own text, its SHA-256 matching the
  page's): -3's Wi-SUN report, FR341305-02C, tests "Wi-SUN mode #2a, 100
  kbps, 25 kHz Deviation, 2-GFSK, 137 kHz RX Bandwidth", hopping 129 channels
  of 200 kHz, and no other rate. -2's three reports test "WB-DSSS, 30 kbps"
  at 915 MHz (FR341305-01A), "TI 15.4, 50 kbps" (01B) and "PowerG PHY, 50
  kbps" (01C). -3's other four were not opened; their titles name other PHY
  families, one report to each row of the grant's five-row table (INFERRED).
- **So no public grant record shows Mode #5 at 300 kbit/s.** TI's
  certification note lists it among the "Certified PHYs" (SWRA797 Table 2-1,
  p5). The grants' operational descriptions are confidential and were not
  readable.
- **The grants' conditions,** quoted from both unless marked:
  - "Single modular approval for mobile RF exposure condition. The module
    antenna(s) must be installed to meet the RF exposure compliance
    separation distance of 20 cm." INFERRED: a badge, a fob or a band is worn
    closer than 20 cm, so beyond five devices a worn tag needs an RF-exposure
    assessment of its own. Its transmitter is on about 0.2 % of the time at
    the busiest (an exit's reply every frame: 0.533 ms in 250 ms), but the
    FCC's exemption text is unread;
  - "Co-location of this module with other transmitters that operate
    simultaneously are required to be evaluated using the FCC multi-transmitter
    procedures." The design puts one transmitter in each device;
  - -3 only: "This module can only be used when installed in a host device
    that follows the required instructions for use of a trace reference
    design." Gate G2 (§ 8);
  - the host integrator's assessment of the composite product "to the FCC
    rules" and KDB 996369.
- **Which label** (INFERRED): "Contains FCC ID: ZAT-1312PSIP-3". The
  Reader's 17 dBm, 50.1 mW, sits under -3's band-wide rows and above -2's
  24.5 mW, and TI's later note points to -3 (SWRA797 p14, p17). It is
  confirmed by TI's answer and by the module bought; -2's paperwork names
  part CC1312PSIPMOT2, and the part priced is CC1312PSIPMOTR.

**Decision 2: keep 300 kbit/s, and ask TI now.** Milestone 1's five devices need
no grant (§ 7). Omid sends TI this question in writing (TI's public E2E forum
fits a public project):

> **CC1312PSIP: which FCC grant covers the Wi-SUN Mode #5 (300 kbps) PHY that
> SWRA797 lists as certified?** SWRA797 (September 2024), Table 2-1, lists
> Wi-SUN Mode #5 (300 kbps, 75 kHz deviation, 2-GFSK, 600 kHz channels) among
> the CC1312PSIP's certified PHYs. In the public exhibits, ZAT-1312PSIP-3's
> Wi-SUN test report (FR341305-02C) tests mode #2a only (100 kbps, 129 ×
> 200 kHz channels), and ZAT-1312PSIP-2's reports test WB-DSSS 30 kbps,
> TI 15.4 50 kbps and PowerG 50 kbps.
> 1. Which FCC ID's grant covers Mode #5 at 300 kbps on its 42 × 600 kHz
>    channel plan, and in which exhibit is that shown?
> 2. May a host using Mode #5 with the SDK's default settings (12 dBm on the
>    14 dBm port, 17 dBm on the 20 dBm port, a listed antenna) rely on that
>    single modular approval, and which ID goes on its label?
> 3. ZAT-1312PSIP-3's grant requires a host to follow "the required
>    instructions for use of a trace reference design". Which document holds
>    those instructions?
> 4. The grants carry a mobile RF-exposure condition (20 cm). What does TI
>    advise for a body-worn host, such as a badge?
>
> Context: an open-source (Apache-2.0) active-RFID rebuild on the CC1312PSIP.

**TI's answer gates the first tag-core drawing** (gate G1, § 8). If TI cannot
show Mode #5 on a grant, the choice returns to Omid before any board is drawn:
- **mode #2a, 100 kbit/s,** the PHY on the record: about 555 slots a second on
  one channel, so [3]'s 1,000 is unmet (INFERRED: 20 bytes in 1.6 ms, plus the
  0.2 ms guard), 7 dB more range (−101 dBm against −94, SWRA797 Table 2-1),
  and every battery figure re-priced. Roughly, a nursery room tag would need
  about 230 mAh a year, about 0.65 years on a CR2032, so the worn tags would
  likely take a CR2450 (INFERRED: each slot heard lasts three times as long,
  8.13 µA of listening becoming about 17.4);
- **or a second vendor's certified module,** which the study never read.

The bench story comes before the first tag board and is the same under either
PHY, so the wait costs no time.

### 5.3 The conference hall, the EU's channels, and a carried tag (decision 3)

- **3a. The hall's second Reader listens on the first Reader's network.** It
  times its frame from the first Reader's beacon, sends no beacon, has no
  block, and passes every reply it hears to the host, which keeps one copy by
  the tag's `seq`. The hall stays one block on one channel, and [9] holds by
  22 % (122.5 mAh a year, 1.22 years). Its own network is declined: [9] by
  9 % beside the edge, and a badge rejoining at each crossing leaves [9] unmet
  (0.81 years). The acknowledgement path of a reply only the second Reader
  hears is measured by the build; were every check-in retried once, the badge
  would last 1.19 years (`radio-and-location.md` § Zones, The conference
  hall's second Reader).
- **3b. In the EU, each Reader keeps one channel, and Readers beside each other
  take different ones:** four of band 47's five 600 kHz channels in a repeating
  2 × 2 pattern, one spare. One channel for every Reader is declined: the edge
  bay's two Field Generators would meet 0.32 % of the time, at worst for 40 s
  once in about 3.5 hours, without bound (§ Channels). **The warehouse
  inventory is planned on Readers that share a channel taking turns:** 534.5 s
  with 9 Readers and 478.9 s with 12, about 8.9 and 8.0 minutes, inside
  [21]'s 10 (§ Channels). They roll call at once (5.9–6.1 minutes) only if the
  build's [21] test on two Readers sharing a channel shows they do not hear
  each other: the study's own budget leaves 16.2 dB at 60 m in free space
  (Story 2.1.17's review, Minor F). A lost EU tag searches as Story 2.1.17
  states (§ Channels): its own Reader's channel until its window reaches a
  frame, about 35 minutes, then its site's channels in turn, the spare left
  out.
- **3c. [22] binds a tag carried past a Reader's edge.** A tag carried beyond
  the edge bay between two listens hears none of its network. As the study
  stands, it is found up to about 49 minutes after it is lost in an EU
  warehouse and, in the US, only by chance until its window reaches the hop
  cycle, about 24 hours (Story 2.1.17's review, Minor G). No requirement
  moves: [22]'s move shown within 5 minutes binds it, and with it no false
  [25] alarm. Story 2.1.18 designs and prices the search that meets it before
  Epic 2.1 closes (for example, the downlink that places a tag at an edge bay
  hands it the next Reader's timing and channel or hop order), and anything it
  cannot meet within the battery comes back to Omid.
- **Story 2.1.18 writes 3b and 3c into the study** (plan Story 2.1.18's
  Context: "If the sitting runs first and decides the EU channel plan, a lost
  tag's search or [25]'s alarm rule in the rebuild spec, this story writes the
  spec's answer into the study").

### 5.4 The devices (decision 4)

- **The Reader** (`parts-and-toolchains.md` § The Reader): the module at 17 dBm,
  the W5017 on an SMA connector, a USB-to-serial bridge of the CDC class (no
  driver on any of the three platforms), USB's 5 V through a 3.3 V regulator,
  two relays and two supervised inputs (the original's I/O), and a 10-pin
  debug header. No Ethernet in milestone 1.
- **The Field Generator** (§ The Field Generator): the same module and whip at a
  power set for its room, on a USB wall supply and a 3.3 V regulator, with no
  host link. An exit relay and the original's motion input are options, not in
  milestone 1.
- **One tag core** (decision 4; `parts-and-toolchains.md` § One core or three):
  one schematic and one four-layer layout, sized for the fob (the smallest
  enclosure that takes a CR2032), carrying the module, the chip antenna, the
  cell and its buffer capacitor in a tool-opened compartment, and debug pads
  within 25 mm of an edge for the pogo-pin clip (`tiered-cost.md` § What each
  tool needs to run). Fitting options: the TMP117 sensor, the tamper loop on a
  pin that wakes the module, the badge's buttons, and the unfitted NFC
  footprint (an NFC memory, its I²C link and two antenna pads at the board's
  edge; the coil in the enclosure, off the core). One firmware image serves
  every style, the style a setting. Its variants, from the same schematic:
  - **the warehouse item tag,** with a CR2450 holder (decision 5a);
  - **a card-sized badge,** only if [2]'s range test shows the badge short on
    the small core. A board shorter than a quarter wave, 8.2 cm at 915 MHz,
    loses efficiency (SWRA797 p9), and a card-sized board recovers some of it.
- **The sealTag** is a new design (`original-system.md` § What this means for
  the study): the 2012 seal was an ISO 15693 logging seal with no active
  radio. The new one keeps its form, a band or a seal whose loop is wired to
  the core's wake pin. The loop's opening wakes the module, which reports in
  the next frame: about 1.5 s to the host against [14]'s 5 s (INFERRED,
  `parts-and-toolchains.md` § One core or three). Milestone 1's seal fits the
  TMP117, for its temperature reading. The 2012 seal's two-chip board is not
  ported.
- **The compactTag (a fob) and the cardTag (a badge)** are the core in their
  enclosures, with the options their scenarios use.

### 5.5 Cells and the sensor (decisions 5, 6 and 7)

- **5a. The warehouse item tag listens every 12 s, on the core's CR2450
  variant:** 2.76 times five years' need, 2.54 on cold chain. An item that
  cannot take the larger tag gets the plain core on a CR2032 at 12 s: 1.21 and
  1.10 of five years. The CR2450 costs no more (0.95 a tag with its holder,
  against 1.38), and is 4.5 mm wider and 1.8 mm thicker. [26]'s 30 s pairing
  alarm bounds the listen at 14.5 s, and 12 s gives 25 s
  (`parts-and-toolchains.md` § [27]; `tiered-cost.md` § Both cells).
- **5b. Milestone 1 buys Energizer cells.** Every battery figure rests on
  Energizer's sheets (CR2032 235 mAh, CR2450 620 mAh, to 2.0 V), and the
  usable 150 mAh is still ASSUMED, with a floor of 117.5 mAh
  (`parts-and-toolchains.md` § The battery). The bench's first measurements
  then test the cell the figures assume. Their price is read at purchase
  (not read).
- **6. The worn tags: measure first, then the rule.** On paper, a nursery tag in
  a room holds [18] by 24 % (1.24 years; 0.97 at the floor), a badge in the
  hall [9] by 22 % (1.22; 0.96), and a nursery tag that lives in the corridor
  fails (0.60) (`parts-and-toolchains.md` § Each worn tag's year). The bench
  measures the three ASSUMED inputs (the 0.3 ms set-up, the 1.0 ms beacon, the
  0.4 ms slot) on the LP-XDS110ET. Then:
  1. an adult's fob left short takes a CR2450 (1.58 years even for a tag
     that never leaves the corridor);
  2. a baby's band left short takes one conversion per sensor reading (1.31
     years in a room), then shorter corridor stretches (2.3 µA saved for each
     neighbour fewer, at the cost of more Field Generators);
  3. a conference badge left short takes a 3 s listen in the hall (1.71
     years; [8]'s pairing alarm in about 7 s, inside its 15);
  4. anything still short comes back to Omid. No number is lowered
     (`requirements.md` § Ground rules).
- **7. The sensor: no accuracy is a requirement.** The TMP117's averaging is a
  per-tag setting, eight averages by default, the setting at which TI
  specifies ±0.1 °C from −20 to 50 °C and at which every battery figure is
  priced (TMP117 p6; `parts-and-toolchains.md` § The tags). One conversion is
  rule 2's route above. The guides may quote the datasheet's ±0.1 °C at eight
  averages as the part's property, never as a promise, and never as a medical
  claim.

### 5.6 The host software (decided ahead of the sitting, and the block)

As `software-stack.md` states it:
- **A small Python 3 program on the owner's machine,** with the standard
  library's `sqlite3` and `ThreadingHTTPServer` and pyserial for the Reader's
  port. Its screen is a page in the owner's browser (SVG for the floorplan,
  Server-Sent Events for the live view), with no build step and nothing
  fetched from the internet. It works offline (§ The choice).
- **The event schema** (`v`, `tag`, `reader`, `via`, `rssi_dbm`, `time` in UTC
  from the Reader's frame clock, `seq`, `payload`), published as a JSON Schema
  file. The Reader sends compact binary frames closed by a CRC, which the
  host's adapter turns into events (§ The event schema the readers report).
- **The API sketch,** the floorplan by zone with the neighbour links the radio
  needs, the four alarms (tamper, temperature, missed check-in, pairing) with
  Chorus's dwell and left-a-zone, inputs, low battery and a Reader offline,
  each acknowledged by a named user (§ The floorplan, the alarms and
  pairing).
- **Scan-and-scan pairing,** every tag carrying its ID as a number and a QR
  code; the scan at the host machine in milestone 1.
- **Security:** the host listens on the owner's machine only, answers only
  requests addressed to it by that name, takes a token on every change, makes
  its own secrets on its first run, and keeps the data in one file, backed up
  through SQLite's backup interface (§ Security on the user's machine).
- **Retention:** raw check-ins 7 days by default; the history until the owner
  deletes it; the setting on screen beside the disk use.

### 5.7 The repository's layout (decision 10)

The starting shape Story 1.1.2 left:
- `hardware/`: the Reader, the Field Generator and the tag core in KiCad 10,
  with the core's variants;
- `firmware/`: one tree for the one module, the Reader's, the Field
  Generator's and the tag's images sharing a protocol library, the tag style a
  setting. TI's SDK is never copied in: each builder fetches it
  (`parts-and-toolchains.md` § The licences of the reference designs read);
- `software/`: the Python host, its page and the published JSON Schema;
- `guides/`: the beginner's build, the US and the EU (§ 6.3);
- `docs/`: the plan, the study and the specs, as now.

`scripts/` (the repo's own checks) and `_assets/` (the 2018 README's images)
stay. The subfolders are the build's to refine (§ 10).

## 6. Milestone 1: the acceptance target

### 6.1 What it is

As plan Initiative 2 states it: a builder with no experience follows
`guides/` and ends with one Reader, one Field Generator, and a sealTag, a
compactTag and a cardTag built; the host software running on their own
Windows, macOS or Linux machine; all three tags read together
(anti-collision); a tag carried between two zones, the move showing on the
floorplan; a tamper alarm and a temperature reading arriving; and a tag paired
with a person raising an alarm when it leaves without them. Every shopping
list is tiered (D7). **It is accepted when Omid has built it from the guides**
(D9), and the guides are revised from what his build taught before anyone
else is pointed at them. Its acceptance starts the product website's work
(plan Initiative 2, "What its acceptance starts").

### 6.2 The first build (decision 8)

- **Omid's build uses the advanced tool tier with fully assembled boards:**
  1,475.59 US dollars before its unread prices (`tiered-cost.md` § The totals,
  "Any route with any tier"). The tiers build the same boards from the same
  parts; they differ in the tools and in who places the parts. So the first
  build validates the one design with the instruments it needs: the
  NanoVNA-H4 for the antenna match ("Tuning of the antenna impedance matching
  network is recommended after manufacturing of the PCB", PSIP p57), the
  microscope to see a joint, and the hot plate to rework a board. The fully
  assembled boards walk the assembly steps most beginners will take, and the
  guides record each step that needed an advanced tool.
- **Every tier carries the LP-XDS110ET** (59.29), which measures the tags'
  current for decision 6, with its cable to the boards and the tags' pogo-pin
  clip.
- **The spare boards stay unpowered** (decision 8b). JLCPCB assembles at least
  two boards of a design, so an assembled order brings a second Reader and a
  second Field Generator: seven radio boards against §15.23's "quantities of
  five or less". The two are replacement parts, never a sixth device in
  service, and the bench story reads §15.23's count before the assembled
  order (`tiered-cost.md` § The build routes; `radio-and-location.md` § What
  the law decides before any radio is compared).
- **TI's terms for its evaluation boards** say they are "intended solely for use
  by technically qualified, professional electronics experts" (SSZZ027
  §4.3.2). The guides say so plainly, and the debugger and the development
  board stay bench tools (`parts-and-toolchains.md` § The debugger).

### 6.3 The guides, in the US and the EU (decision 9a)

- **The guides and their shopping lists cover the US build and the EU build.**
  The EU parts are priced by **Story 2.1.19** before Epic 2.1 closes: the tag's
  868 MHz chip antenna (unnamed), the whips at 868 MHz (whether the W5017
  serves it is unread), and the 230 V plugs for the Field Generator's supply
  and the wall-powered tools (`tiered-cost.md` § The EU build). The module,
  the boards, the cells and the bench are the same parts.
- **The guides say plainly that in the EU the builder carries compliance:**
  there is no home-built allowance and no module grant (§ 7).
- **Omid's acceptance build is the US build** (D9), so the guides' EU-only
  steps have no first builder's test until someone builds there.

## 7. Legality

- **United States, milestone 1.** Five devices built for personal use, not
  marketed and not from a kit need no equipment authorisation (§15.23(a)).
  The builder meets the technical standards "to the greatest extent
  practicable" (§15.23(b)), and §15.5 applies (`radio-and-location.md`
  § Legality of the choice).
- **United States, beyond five.** The route is the module's own single modular
  grant (§15.212), at a certified PHY with the SDK's default settings, the
  listed powers and a listed antenna type at 2.69 dBi at most, one transmitter
  to a device. Whether Mode #5 is on a grant is gate G1 (§ 5.2, § 8), and the
  grants' conditions are recorded in § 5.2. Beyond five, or once marketed, the
  Reader's electronics are a digital device under Part 15 Subpart B, which
  takes a measurement this study could not make (`parts-and-toolchains.md`
  § The law for the parts around the module). Devices made for customers are
  outside this spec (decision 9b).
- **European Union.** No home-built exemption and no module grant. The route is
  a radio whose maker names EN 300 220 (PSIP p54), operation inside band 47's
  conditions (25 mW e.r.p., a duty cycle under 1 %), and the builder's own care
  for the Radio Equipment Directive's Article 3 (`radio-and-location.md`
  § Legality of the choice). The study "cannot make that route as firm as the
  US grant".
- **Neither region needs the 433 MHz band** whose US rule the original's wake
  field could not meet.

## 8. Gates

| # | Gate | Before | Source |
|---|---|---|---|
| G1 | TI's written answer on which grant covers Mode #5 at 300 kbit/s; if none, the choice back to Omid (§ 5.2) | the first tag-core drawing | decision 2 |
| G2 | TI's integration instructions for the -3 grant's "trace reference design" read; if they bind a board to TI's own geometry, publishing it here goes to the ASK-OMID register first, since TI's terms are not on the licence lane's allowlist | any board is drawn | § 11 |
| G3 | §15.23's count for never-powered spare boards read | the assembled order | decision 8b |
| G4 | The JLCPCB quote for the drawn boards, the module's sourcing there, the five unread prices, and every price read again | anything is bought | `tiered-cost.md` § What the choice leaves to later stories |
| G5 | Stories 2.1.18 (3b's planning figure, 3c's search) and 2.1.19 (the EU build priced) | Epic 2.1's close | decisions 3 and 9a |
| G6 | TI's SDK licence ("for use only with TI Devices") taken to the ASK-OMID register | any firmware image is published | `parts-and-toolchains.md` § The licences of the reference designs read |

## 9. What the close files

Story 2.1.8, Epic 2.1's close, files the build initiatives from this spec:
hardware in KiCad 10, firmware, software, guides, and milestone 1's two-zone
demonstration, with D9's "set up the bench" story first (a tool tier and a
cheap radio receiver to see what the boards transmit). Every hardware story
is proven by Omid's hand: `requires: omid` teeth and committed, public-safe
evidence (photos, measurements and the receiver's captures). The items each
carries:

- **from the radio study:** the beacon's and a Field Generator slot's lengths,
  measured; the floorplan's neighbour links and the guides' setup walk that
  checks them; the block's order along each floorplan; the count each Field
  Generator's slot repeats; and the hop order (or, in the EU, the site's
  channels) a tag is given when it joins;
- **from the parts study:** the USB bridge part and the chip antenna's land
  pattern, each from its maker's datasheet; the boards in KiCad 10; and the
  first battery measurements on the LP-XDS110ET, in place of the ASSUMED
  0.3 ms set-up, 1.0 ms beacon and 0.4 ms slot;
- **from the software study:** the JSON Schema file and the binary frame; the
  USB bridge's baud limit in place of the ASSUMED 921,600; the seal core's
  unfitted NFC footprint and the coil's place in the enclosure; the host's
  first measurements on Windows and Linux, and with a Reader on all three
  platforms; the guides' install steps, checked on each; pyserial copied in
  with its trail or installed by `pip`; the QR decoder; and the LAN tier
  later;
- **from the cost study:** the bench story first (the receiver, a stencil for
  the hand-solder route, the paste against the module's reflow profile, how a
  tag's board meets the VNA); every part marked representative chosen from its
  datasheet; the boards' real sizes, which re-price them; and gate G4;
- **from the sitting:** gates G1–G3 and G6; the antenna-match check on the
  NanoVNA-H4 (§ 6.2); Energizer cells (5b); the CR2450 item variant (5a) and
  the card-sized badge variant if [2]'s test needs it (decision 4); decision
  6's rule after the battery measurements; the averaging setting (decision 7);
  the build's [21] test on two Readers sharing an EU channel (3b); the second
  Reader's acknowledgement path (3a); and the guides for both regions (§ 6.3).

**Open for the build, as the studies mark them:** whether TI's 12 dBm setting
uses the boost mode that needs 2.1 V (`parts-and-toolchains.md` § Each worn
tag's year); the development board's FCC status (§ The debugger); whether a
passive NFC coil counts as "any other antenna" under the module's co-location
condition (`software-stack.md` § Decided ahead of the sitting); and the
Reader's emissions under Subpart B (§ 7).

## 10. Left to the builder

- every part the studies mark representative: the USB bridge, the regulator,
  the relays and their driver, the connectors, the passives, the enclosures,
  the seal's band and loop;
- the boards' sizes and layouts, under SWRA797 §3's written rules and gate G2;
- the subfolders under § 5.7's five;
- the hop sequence, the frame's exact layout, and each Field Generator's power
  for its room, inside § 5.1's figures;
- the API's paths and the schema's details, inside `software-stack.md`'s
  sketch;
- the QR decoder, and pyserial's copy or install.

## 11. Calls made in writing this spec

These are this document's, not the sitting's. Omid's approval covers them as
written:
1. **Gate G2,** from the -3 grant's "trace reference design" condition, which
   no study read (§ 5.2, § 8).
2. **The label ID, -3,** INFERRED from the grants' listed powers and TI's note,
   pending gate G1 (§ 5.2).
3. **The fallback's battery cost** (worn tags likely on a CR2450 at 100 kbit/s)
   is a rough INFERRED figure for the record, not a priced one (§ 5.2).
4. **3c's example** of a search (the edge downlink handing a tag the next
   Reader's timing) is an example for Story 2.1.18, not a design (§ 5.3).
5. **The card-sized badge variant** follows the item tag's precedent: the same
   schematic, a larger board, made only if a measurement asks (§ 5.4).
6. **The first build's network analyser** is the advanced tier's NanoVNA-H4,
   and the guides record which steps needed an advanced tool; the guides'
   first recommendation to other beginners is written from that record (§ 6.2).
7. **The FCC records are cited by URL and SHA-256, not pinned** in
   `docs/reference-corpus.tsv`. Whoever settles gate G1 may pin them (§ Sources).
8. **The subfolders in § 5.7** are an example layout.

## Sources

- **The six study documents** at `a52cbf3`: `requirements.md`,
  `original-system.md`, `radio-and-location.md`, `parts-and-toolchains.md`,
  `software-stack.md` and `tiered-cost.md`. Their own § Sources hold the law
  texts (cited at the versions read), the pinned datasheets (PSIP, SWRA797, R7,
  the Energizer cells, SWRA349, TMP117, SSZZ027) and the price pages.
- **The decision brief,** `docs/study/2.1.7-decision-brief.md`.
- **SWRA797, Table 2-1** (p5), read again in the sitting from the reference
  corpus's pinned copy (`2fbe59c4ae3f…`).
- **The FCC records,** read 2026-10-30. The grant pages were read by Omid in a
  browser and pasted into the session; the test reports were fetched by the
  session from the PDF store the pages load them from, each file's SHA-256
  matching the one its page prints:

| Record | Address | SHA-256 |
|---|---|---|
| ZAT-1312PSIP-3, the grant and its exhibit list | `https://fccid.io/ZAT-1312PSIP-3` | — |
| ZAT-1312PSIP-2, the grant and its exhibit list | `https://fccid.io/ZAT-1312PSIP-2` | — |
| -3, FR341305-02C (Wi-SUN), 112 pages | `https://fccid.io/ZAT-1312PSIP-3/Test-Report/FR341305-02C-R02-Part15C-Texas-Instrument-CC1312PSIP-Phase2c-Wi-Sun-7313984` | `40b96cb37714861e4ebba980cddacc48d2c08a69151e3009407a68bcf302ef18` |
| -2, FR341305-01A (WB-DSSS 30 kbps) | its page under `https://fccid.io/ZAT-1312PSIP-2` | `92aa37b0b0c08799fe4bb5ed8e75ac1f028377d12ca3971b7f0b9cab13f631de` |
| -2, FR341305-01B (TI 15.4 50 kbps) | the same | `7128a84d2b8e4138882916709aa98836e326536161f28f5b3952137104e6ed1c` |
| -2, FR341305-01C (PowerG 50 kbps) | the same | `81631f52ddd72de4b15f80b0d80286f91f574a956b3536011e5b11f44013d382` |

Each PDF is served at `https://fccid.io/m/<SHA-256>.pdf`. fccid.io's pages
answered the session's own plain requests with HTTP 403, as they had answered
Story 2.1.4's. The PDF store answered with the files.
