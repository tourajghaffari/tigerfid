# The tiered cost of milestone 1

The sixth document of the design study (plan Story 2.1.6). It prices the
first build, milestone 1 (spec §4): one Reader, one Field Generator and
three tags (a sealTag, a compactTag and a cardTag). It prices:

- each device, from the parts `docs/study/parts-and-toolchains.md` names;
- each build route: hand-solder, partly assembled and fully assembled;
- each tool tier: starter, advanced and pro, with several ways to the same
  end (spec D7);
- the bench that spec D9 asks for: a tool tier plus a cheap radio receiver
  to see what the boards transmit;
- a total for each tier, saying what it is made of.

This is the rebuild's first budget. It is a reading of public price pages on
one day, not a quote and not a shopping list. The build initiatives choose
the parts this document marks representative.

**How to read it:**
- Every price is in US dollars, from a seller's public page, with the date
  it was read, the quantity it is for, and the page in § Sources. Prices are
  cited, never pinned: no choice rests on a price page (plan Sprint 2's D8).
- **Shipping and tax are left out** of every figure.
- A part marked **representative, for pricing** stands in for a part the
  studies leave unnamed (plan Sprint 3's D6). It is not a choice.
- A price marked **not read** is one no session could read. It is never
  counted as zero: each total that needs it names it as lacking.
- A claim marked **INFERRED** was reasoned, not read: its arithmetic is
  shown. A figure marked **ASSUMED** is an input to that arithmetic.
  Everything else cites the page it was read from.
- Each total is summed before rounding.

This document recommends; the decision sitting (Story 2.1.7) decides.

## What was read, and what could not be

Every price was read on **2026-10-28**, between this leg's clock reads of
14:30:15Z and 14:42:01Z. Each page was fetched once, by a plain request
with a generic user agent. Each price below is the one the page printed for
the quantity this build buys.

**Five prices are not read, by Omid's word.** Five pages refused a plain
request, or price only through a script that runs in a browser. They were
put to Omid first-hand in this leg's terminal, in one message, between its
clock reads of 14:42:21Z and 15:59:59Z (plan Sprint 3's D7). He answered
"Write all as not read (Recommended)". The five:

| Price | What it is for | The page | Why it is unread |
|---|---|---|---|
| Pulse W5017 whip antenna | the Reader's and the Field Generator's antenna, one each | Digi-Key's W5017 page | HTTP 403, a bot check. Mouser, Arrow and Newark refused the same way |
| 4-layer boards at JLCPCB | the partly and fully assembled routes' boards | JLCPCB's instant quote | it prices by script only |
| The module at JLCPCB | whether JLCPCB stocks the CC1312PSIP or it must be sent to them | JLCPCB's parts search | it lists by script only |
| Hakko FX-971 | the pro tier's soldering station | Hakko USA's FX-971 page | HTTP 403, a bot check |
| Hammond 1551HRGY | the tags' enclosure, representative | Jameco's 1551HRGY page | HTTP 403, a bot check |

**Refused, but not needed:**
- Chip Quik's own store (HTTP 403): Adafruit sells the same maker's flux;
- JBC's US pages (HTTP 403): one pro station is enough;
- Hakko USA's other pages (HTTP 403): the advanced tier's station is read
  at Adafruit.
- LCSC answered a quick run of requests with a challenge page. Each of its
  pages was read again with 15 s between requests, and each answered.

**Not found:** a seller's page that prices a USB barcode scanner. Adafruit's
search lists none. SparkFun's search page carried no results in the text
read. The scanner is the software study's optional bench item, so it sits
outside every total (§ The bench).

## How the build is counted

- **The quantities are milestone 1's:** one Reader, one Field Generator and
  three tags. Each part is priced at the break that the build's count falls
  in. A seller that sells only in packs is bought by the pack, and the pack
  is shared across the devices (§ Shared buys).
- **One core in three enclosures,** as `parts-and-toolchains.md` recommends:
  the three tags share one board design. The sitting decides between one
  core and three boards. INFERRED for three boards: the hand-solder route's
  boards cost about the same, since OSH Park prices by area. An assembled
  route pays two more designs' fees: about 56.50 partly assembled and 62.65
  fully assembled (2 × 28.25 and 2 × 31.32, a one-board tag design's fees
  by § The build routes' arithmetic).
- **ASSUMED board sizes**, since no board is drawn (plan D3): the tag core
  30 × 25 mm, the Reader 60 × 40 mm and the Field Generator 50 × 40 mm. All
  three have four layers, the route `parts-and-toolchains.md` § The tags
  takes for a beginner's first board. The Reader and the Field Generator
  take the same layout rules (SWRA797 §3.1, cited there).
- **The US build** (915 MHz parts), since Omid builds milestone 1 there
  (spec D9). § The EU build names what differs.

## The devices

### The Reader

| Part | Priced as | Quantity | Unit price (break) | Line | Seller |
|---|---|---|---|---|---|
| Radio module | TI CC1312PSIPMOTR, the one orderable part TI lists for the CC1312PSIP | 1 | 10.732 (1–99) | 10.73 | TI-PSIP |
| Antenna | Pulse W5017 whip | 1 | **not read** | — | Digi-Key |
| Antenna connector | RF Solutions CON-SMA-EDGE-S, an SMA edge connector: **representative, for pricing.** The W5017 has an SMA plug, and `parts-and-toolchains.md` names no connector part | 1 | 1.9861 (1+) | 1.99 | LCSC-SMA |
| USB bridge | Microchip MCP2200-I/SS: **representative, for pricing.** LCSC lists it as "USB to UART 921.6Kbps", above the 436,160 baud that `software-stack.md`'s binary frame needs. Whether it enumerates as a CDC device is the build initiatives' read of its datasheet | 1 | 3.7563 (1+) | 3.76 | LCSC-MCP2200 |
| USB connector | Hroparts TYPE-C-31-M-12, a USB-C receptacle: **representative, for pricing** | 1 | from the 5-pack (§ Shared buys) | — | LCSC-USBC |
| 3.3 V regulator | TI TLV75533PDBVR, 500 mA: **representative, for pricing.** It is rated above the module's 86 mA at +20 dBm (PSIP p14, cited in `parts-and-toolchains.md`) | 1 | 0.374 (1–99) | 0.37 | TI-TLV755P |
| Relays | HONGFA HF46F-G/005-HS1, a 5 V relay: **representative, for pricing.** The study keeps the original's two relays and names no part | 2 | 1.1947 (1+) | 2.39 | LCSC-RELAY |
| Relay driver | TI TPL7407LDR: **representative, for pricing** | 1 | 0.731 (1–99) | 0.73 | TI-TPL7407L |
| Debug connector | hanxia HX PZ1.27-2x5P TP, a 10-pin 1.27 mm header: **representative, for pricing** | 1 | from the 5-pack | — | LCSC-HEADER |
| Passives | 100 nF capacitors and 10 kΩ resistors (0402): **representative, for pricing**, about twenty a board (ASSUMED) | ~20 | from the 100-packs | — | LCSC-C1525; LCSC-C25744 |
| Enclosure | Adafruit's flanged weatherproof enclosure with cable glands: **representative, for pricing** | 1 | 9.95 | 9.95 | ADA-3931 |
| USB cable | USB-C to USB-C, 1 m: **representative, for pricing** | 1 | 9.95 | 9.95 | ADA-4199 |
| **The Reader** | | | | **39.87, without its antenna** | |

The supervised inputs' protection parts are left to the build initiatives
with the I/O (`parts-and-toolchains.md` § The Reader). INFERRED: they are a
few passives, inside the shared packs.

### The Field Generator

| Part | Priced as | Quantity | Unit price (break) | Line | Seller |
|---|---|---|---|---|---|
| Radio module | TI CC1312PSIPMOTR | 1 | 10.732 (1–99) | 10.73 | TI-PSIP |
| Antenna | Pulse W5017 whip | 1 | **not read** | — | Digi-Key |
| Antenna connector | CON-SMA-EDGE-S: **representative, for pricing** | 1 | 1.9861 (1+) | 1.99 | LCSC-SMA |
| USB connector, for power | TYPE-C-31-M-12: **representative, for pricing** | 1 | from the 5-pack | — | LCSC-USBC |
| 3.3 V regulator | TLV75533PDBVR: **representative, for pricing** | 1 | 0.374 (1–99) | 0.37 | TI-TLV755P |
| Debug connector | the 10-pin header: **representative, for pricing** | 1 | from the 5-pack | — | LCSC-HEADER |
| Passives | as the Reader's, about twelve (ASSUMED) | ~12 | from the 100-packs | — | LCSC-C1525; LCSC-C25744 |
| USB power adapter | Adafruit's vertical wall supply with USB-C, 5 V 3 A: **representative, for pricing** | 1 | 5.95 | 5.95 | ADA-5801 |
| Enclosure | Adafruit's flanged weatherproof enclosure: **representative, for pricing** | 1 | 9.95 | 9.95 | ADA-3931 |
| **The Field Generator** | | | | **28.99, without its antenna** | |

**Not in milestone 1's count:** the exit's relay and its driver, 1.93
(1.1947 + 0.731, the Reader's parts), and the motion input
(`parts-and-toolchains.md` § The Field Generator). Milestone 1's Field
Generator makes a zone, and needs neither.

### The tags

The three tags share one core (§ How the build is counted). Each core:

| Part | Priced as | Quantity per core | Unit price (break) | Line | Seller |
|---|---|---|---|---|---|
| Radio module | TI CC1312PSIPMOTR | 1 | 10.732 (1–99) | 10.73 | TI-PSIP |
| Chip antenna | Johanson 0915AT43A0026001E, the current part number of the study's 0915AT43A0026 (LCSC names the old one "deprecated") | 1 | 1.1947 (1+) | 1.19 | LCSC-ANT |
| Cell holder | MYOUNG BS-08-B2AA016, a CR2032 holder: **representative, for pricing** | 1 | 0.4331 (1+) | 0.43 | LCSC-H2032 |
| Cell | a CR2032: **representative, for pricing.** Adafruit's page cites Maxell's datasheet and 220 mAh. The study's planning figure rests on Energizer's sheet, and the sitting names the cell | 1 | 0.95 | 0.95 | ADA-654 |
| Buffer capacitor | Samsung CL31A107MQHNNNE, 100 µF: **representative, for pricing.** SWRA349 gives no single value in the study's citation | 1 | from the 10-pack | — | LCSC-C15008 |
| Passives | about eight (ASSUMED) | ~8 | from the 100-packs | — | LCSC-C1525; LCSC-C25744 |
| Enclosure | Hammond 1551HRGY, a key-ring box: **representative, for pricing** | 1 | **not read** | — | Jameco |
| **Each core** | | | | **13.31, without its enclosure** | |

- **The sensor** is the TMP117AIDRVR, 5.757 at 1–99 (TI-TMP117). The study
  fits it to the nursery's tags, and a fitting option on the others. So the
  hand-solder route fits it to the sealTag alone, 5.76. An assembled order
  places the same parts on every board of one design, so INFERRED: the
  assembled routes fit it to all three cores, 17.27 (3 × 5.757).
- **The seal's band and its tamper loop** are its enclosure's, and are
  priced with it: not read.
- **The three tags: 39.93**, plus the sensor, without their enclosures.

### Shared buys

| Part | Pack | Unit price (break) | Pack price | Seller |
|---|---|---|---|---|
| USB-C receptacle | 5 (the minimum) | 0.1886 (5+) | 0.94 | LCSC-USBC |
| 10-pin 1.27 mm header | 5 (the minimum) | 0.0983 (5+) | 0.49 | LCSC-HEADER |
| 100 µF capacitor | 10 (the minimum) | 0.1100 (10+) | 1.10 | LCSC-C15008 |
| 100 nF capacitor | 100 (the minimum) | 0.0032 (100+) | 0.32 | LCSC-C1525 |
| 10 kΩ resistor | 100 (the minimum) | 0.0024 (100+) | 0.24 | LCSC-C25744 |
| **Shared buys** | | | **3.09** | |

LCSC printed "0" in stock for the SMA connector and for the 10 kΩ resistor
on the day. The prices stand as printed. The build buys whatever is in stock
then.

### Both cells, where the sitting chooses

`parts-and-toolchains.md` leaves two cells to the sitting: the item tag's,
and the nursery tag's if the measurement falls short. Each is priced both
ways (plan Sprint 3's D5):

| Cell | Cell | Holder | Per tag |
|---|---|---|---|
| CR2032 | 0.95 (ADA-654; its page cites Maxell's datasheet, 220 mAh) | 0.4331 (LCSC-H2032, 1+) | 1.38 |
| CR2450 | 0.75 (ADA-2850; 610 mAh by its page) | 0.1982 (LCSC-H2450, 5+; sold 5 at least, 0.99) | 0.95 |

INFERRED: the larger cell costs no more at these sellers. Its cost is the
size `parts-and-toolchains.md` § [27] names, 4.5 mm wider and 1.8 mm
thicker. Price does not decide between them. The CR2450's page names no
maker for its own cell. The study's battery figures rest on Energizer's
sheets, so the cells the build buys are the sitting's to name.

### The parts, all devices

| | Hand-solder route | Assembled routes |
|---|---|---|
| The Reader | 39.87 | 39.87 |
| The Field Generator | 28.99 | 28.99 |
| The three tag cores | 39.93 | 39.93 |
| The sensor | 5.76 (one) | 17.27 (three) |
| Shared buys | 3.09 | 3.09 |
| **The parts** | **117.64** | **129.16** |
| Lacks | two W5017 whips; three tag enclosures | the same |

## The build routes

The module is a 0.5 mm-pitch package with a reflow profile (PSIP p9, p65,
cited in `radio-and-location.md`). INFERRED there: it is not soldered with
an iron. So each route says who places it.

- **Hand-solder.** The builder buys bare boards and places every part.
  The module and the sensor go down with solder paste on the hot plate of
  the advanced or the pro tier, and the rest goes down with the iron.
  - The boards are OSH Park's 4-layer service: "$10 per square inch, which
    includes three copies of your design", made in the United States
    (OSH-4L). Three copies of each design cover milestone 1.
  - A stencil for the paste is not priced. INFERRED: paste from a syringe on
    a 0.5 mm land grid is the route's hardest step. The build's bench story
    weighs a stencil.
  - The advanced tier's paste is a low-temperature one (ADA-SEARCH-PASTE).
    Whether it suits the module's reflow profile (PSIP p65) is not read.
- **Partly assembled.** JLCPCB's Economic assembly places the surface-mount
  parts the builder cannot place with an iron: the module, the sensor, the
  regulator, the USB bridge and connector, the relay driver, the antenna
  chip, the capacitors and the resistors. The builder solders the relays,
  the SMA connector, the cell holder and the debug header.
- **Fully assembled.** The same service places every part, the relays by
  its hand-soldering service. The builder fits the cell and the enclosure.

**JLCPCB's fees** (JLC-PCBA, "Last updated on Oct 22, 2026"; Economic
assembly, one side):

| Fee | Price |
|---|---|
| Setup | 8.18 per design |
| Stencil | 1.53 per design |
| Surface-mount joints | 0.0016 a joint, up to 100,000 |
| Feeder loading | 3.07 per extended part type |
| Hand-soldering labour (through-hole) | 3.58 per order, and 0.0164 a joint |
| Packing | free |

- **ASSUMED: every part type is an extended part.** The page charges feeder
  loading on extended parts only, and no page read says which of these are
  basic. The fees below are the upper bound.
- **ASSUMED: the joints,** from the parts' pin counts: the module's 48
  (TI-PSIP), and 2 per passive. They come to 145 on the Reader, 93 on the
  Field Generator and 74 on each tag core, partly assembled. At 0.0016 a
  joint they cost cents.
- Economic assembly takes up to 30 boards an order, unless the boards are
  panelled (JLC-FAQ, item 10). The build assembles what milestone 1 uses:
  one Reader, one Field Generator, three tag cores. JLCPCB's minimum
  assembled quantity is not read.

| Fees per design | Partly assembled | Fully assembled |
|---|---|---|
| The Reader | 31.43 (7 part types) | 41.31 (9 types, and its two relays by hand: 3.58 + 8 joints) |
| The Field Generator | 25.21 (5 types) | 31.37 (7 types) |
| The tag core, three boards | 28.49 (6 types) | 31.57 (7 types) |
| **The fees** | **85.13** | **104.25** |

**The routes:**

| Route | The parts | The boards | Assembly | The route | Lacks |
|---|---|---|---|---|---|
| Hand-solder | 117.64 | 79.83 (OSH Park: the tag core 11.63, the Reader 37.20, the Field Generator 31.00; 1.16, 3.72 and 3.10 square inches) | — | **197.47** | two W5017 whips; three tag enclosures |
| Partly assembled | 129.16 | **not read** (JLCPCB) | 85.13 | **214.28** | the same, the boards, and the module's sourcing at JLCPCB |
| Fully assembled | 129.16 | **not read** (JLCPCB) | 104.25 | **233.41** | the same as partly assembled |

The module's sourcing: if JLCPCB does not stock the CC1312PSIP, the module
is bought from TI (priced above) and sent to the service. That service's
fee, and the shipping to it, are not read.

## The tool tiers

Three ways to the same jobs. Each tier holds what its route needs (§ The
totals), and every tier carries the LP-XDS110ET. `parts-and-toolchains.md`
recommends one per builder, since it measures the tag's current. The
wall-powered tools are a US seller's: the ST-2090D is named a 110 V model,
and the ST-862D's voltage is not read.

| Job | Starter | Advanced | Pro |
|---|---|---|---|
| Soldering iron | Pinecil V2, 25.99, and a USB-C supply for it, 16.50 | ATTEN ST-2090D 80 W station, 59.95 | Hakko FX-971 station, **not read** |
| Placing the module | — (the boards come assembled) | Miniware MHP50-B5 hot plate, 99.95; low-temperature paste, 9.95; tack flux, 15.95 | the same hot plate, paste and flux; ATTEN ST-862D hot air station, 279.95 |
| Solder and its helpers | lead-free solder wire 0.5 mm, 12.50; flux pen, 7.95; solder wick, 2.95; flush cutters, 7.25; ESD tweezers, 3.95 | the same, with ceramic tweezers, 8.95 | the same, with SAC305 lead-free wire, 27.50, and the ceramic tweezers |
| Seeing the joints | a magnifier on helping hands, 6.95 | AmScope SE400-Z stereo microscope, 291.99 | the same microscope |
| Measuring | a pocket autoranging meter, 24.95 | Fluke 117, 304.99 | Fluke 87V, 579.99 |
| The antenna match | — (the range test decides, `parts-and-toolchains.md` § The tags) | NanoVNA-H4, 10 kHz to 1.5 GHz, 109.95 | the same VNA |
| Programming and current | LP-XDS110ET, 59.29 | the same | the same |
| **The tier** | **168.28** | **991.62** | **1,501.62, without its iron** |

Each price and its page (§ Sources):
- **Starter:** Pinecil V2 at its "Community price" (PINE64; its retail price
  is 35.99); the Raspberry Pi 45 W USB-C supply (ADA-6320): **representative,
  for pricing**, and whether it gives the iron its full power is not read;
  wire ADA-2473, flux pen ADA-3468 (Chip Quik CQ4LF), wick ADA-4456,
  cutters ADA-152, tweezers ADA-421; the meter ADA-850 and the magnifier
  ADA-291; the debugger TI-XDS110ET.
- **Advanced:** the station ADA-4921; the hot plate ADA-5903; the paste
  ADA-3217 and the tack flux ADA-2667 (Chip Quik SMD291); the ceramic
  tweezers ADA-5950; the microscope AMSCOPE-SE400Z; the meter FLUKE-117;
  the VNA RL-NANOVNAH4.
- **Pro:** the hot air station ADA-5238; the SAC305 wire ADA-1930; the
  meter FLUKE-87V. No costlier VNA was read, so the pro tier keeps the H4.
- **Out of stock on the day** (the price stands as the page printed it): the
  pocket meter ADA-850 and the ATTEN ST-2090D ADA-4921.

## The bench (spec D9)

Spec D9 asks for "a tool tier plus a cheap radio receiver to see what the
boards transmit", for C9. The bench is the tier above plus:

| Item | Starter | Advanced | Pro |
|---|---|---|---|
| The receiver | tinySA, a spectrum analyser for 100 kHz to 960 MHz, 89.95 | the same | tinySA Ultra Plus (ZS406), 100 kHz to 5.4 GHz, 169.95; out of stock on the day |
| The development board | LP-EM-CC1312PSIP, 47.19 | the same | the same |
| **The bench, beyond the tier** | **137.14** | **137.14** | **217.14** |

- **The receivers** (RL-TINYSA, RL-TINYSAULTRAPLUS). The tinySA covers the
  902–928 MHz band the US build sends in, by the range its page prints.
- **A cheaper receiver, not in the totals:** the RTL-SDR Blog V4L dongle,
  37.95, and the maker's dipole antenna kit, 17.95, together 55.90 (RTLSDR).
  The page read gives no upper end of its tuning range, only that "the
  lowest frequency an RTL-SDR can comfortably tune to is about 24 MHz". The
  bench story reads its datasheet before it chooses it.
- **The development board** (TI-LPEM) is the bench tool for a builder's
  first firmware, before their own boards exist. TI's terms for it, and why
  it is never part of a device, are in `parts-and-toolchains.md` § The
  debugger.
- **The USB barcode scanner,** for scan-and-scan pairing, is optional
  (`software-stack.md` § What the choice leaves to later stories). No
  seller's page this leg reached prices one (§ What was read), so it is
  **not read**, and outside every total.
- **The host software costs nothing:** Python, pyserial, SQLite and the
  browsers (`software-stack.md`).

## The totals

Each tier is paired with a route its tools can build. Starter has no way
to place the module, so its boards come assembled, and fully assembled asks
the least of a beginner. Advanced takes the partly assembled boards, and
pro builds everything by hand.

| Tier | Its route | The devices | The tools | The bench | **The total** | Lacks |
|---|---|---|---|---|---|---|
| Starter | fully assembled | 233.41 | 168.28 | 137.14 | **538.83** | two W5017 whips; three tag enclosures; the JLCPCB boards; the module's sourcing at JLCPCB |
| Advanced | partly assembled | 214.28 | 991.62 | 137.14 | **1,343.04** | the same as starter |
| Pro | hand-solder | 197.47 | 1,501.62 | 217.14 | **1,916.23** | two W5017 whips; three tag enclosures; the pro iron |

**Any route with any tier** (the devices, the tier's tools and its bench;
each lacks what its route and its tier lack above):

| Route | Starter | Advanced | Pro |
|---|---|---|---|
| Hand-solder | — (no tool to place the module) | 1,326.23 | 1,916.23 |
| Partly assembled | 519.70 | 1,343.04 | 1,933.04 |
| Fully assembled | 538.83 | 1,362.17 | 1,952.17 |

**What the totals say** (INFERRED, from the tables above):
- **The tools and the bench set the tiers apart, not the devices.** The
  devices run from 197.47 to 233.41 on any route, before the unread prices.
  The tools run from 168.28 to 1,501.62.
- **The routes differ little in money.** Assembly costs 16.81 to 35.94 more
  than the hand-solder route's parts and boards, before JLCPCB's boards and
  the module's sourcing. What a route changes is the skill and the tools it
  needs.
- **The module is the largest part:** five of them, 53.66, 46 % of the
  hand-solder route's parts.
- **Recommended for a first build: the starter tier with fully assembled
  boards,** 538.83 before its unread prices. It asks the least skill of a
  beginner (spec anti-goals), with the least spent on tools. Its costs:
  - partly assembled boards would save 19.13 (519.70), for four parts by
    iron per board: the relays, the SMA connector, the cell holder and the
    debug header;
  - the builder cannot rework the module without hot air;
  - the builder cannot tune the antenna match without a VNA. Whether the
    match needs tuning is the range test's to show ([2],
    `parts-and-toolchains.md` § The tags).

## The EU build: named, not priced (plan Sprint 3's D8)

Spec D9's first builder builds in the US. An EU build differs in:
- **the tag's chip antenna:** the 0915AT43A0026001E is a 915 MHz part. The
  EU's 868 MHz part is the build initiatives' to name;
- **the whips:** whether the W5017 serves 868 MHz is not read, since its
  seller pages refused;
- **the plugs:** the Field Generator's wall supply and the wall-powered
  tools are the 230 V models;
- **the settings, which cost nothing:** the Reader's 25 mW e.r.p.
  (`parts-and-toolchains.md` § The Reader).

The module, the boards, the cells and the bench are the same parts. The
module's datasheet gives its figures at 868 MHz and 915 MHz (PSIP p14).

## What the choice leaves to later stories

- **Story 2.1.7, the sitting:** it reads the totals. It decides one core or
  three, and the item and nursery tags' cells, neither of which price
  decides. If it wants the five unread prices, it reads them in a browser
  (§ What was read).
- **The build initiatives:**
  - the bench story, first (spec D9): the receiver (the tinySA, or the
    RTL-SDR once its range is read), a stencil for the hand-solder route,
    and the paste against the module's reflow profile;
  - every part marked representative, chosen from its datasheet, and the
    boards' real sizes, which re-price the boards;
  - the JLCPCB quote for the drawn boards and the module's sourcing, before
    an assembled route is ordered;
  - prices read again before anything is bought. These are 2026-10-28's.

## Sources

Every page was read 2026-10-28, between 14:30:15Z and 14:42:01Z, as
§ What was read says. None is pinned in the corpus. The cited datasheets
(PSIP, SWRA797, SWRA349) are `parts-and-toolchains.md`'s, pinned there.

**Parts:**

| Short name | Seller and item | Page |
|---|---|---|
| TI-PSIP | TI, CC1312PSIP: CC1312PSIPMOTR, 1–99 at 10.732, 100–249 at 9.374, 250–999 at 7.228, 1,000 and up at 6.465 | `https://www.ti.com/product/CC1312PSIP` |
| TI-TMP117 | TI, TMP117: TMP117AIDRVR, 1–99 at 5.757 | `https://www.ti.com/product/TMP117` |
| TI-TLV755P | TI, TLV755P: TLV75533PDBVR, 1–99 at 0.374 | `https://www.ti.com/product/TLV755P` |
| TI-TPL7407L | TI, TPL7407L: TPL7407LDR, 1–99 at 0.731 | `https://www.ti.com/product/TPL7407L` |
| LCSC-ANT | LCSC C17443637, Johanson 0915AT43A0026001E: 1+ at 1.1947, 10+ at 0.9784 | `https://www.lcsc.com/product-detail/C17443637.html` |
| LCSC-SMA | LCSC C5356059, RF Solutions CON-SMA-EDGE-S: 1+ at 1.9861 | `https://www.lcsc.com/product-detail/C5356059.html` |
| LCSC-MCP2200 | LCSC C220000, Microchip MCP2200-I/SS: 1+ at 3.7563 | `https://www.lcsc.com/product-detail/C220000.html` |
| LCSC-USBC | LCSC C165948, Hroparts TYPE-C-31-M-12: 5+ at 0.1886, sold 5 at least | `https://www.lcsc.com/product-detail/C165948.html` |
| LCSC-RELAY | LCSC C2976854, HONGFA HF46F-G/005-HS1: 1+ at 1.1947 | `https://www.lcsc.com/product-detail/C2976854.html` |
| LCSC-HEADER | LCSC C41376037, hanxia HX PZ1.27-2x5P TP: 5+ at 0.0983, sold 5 at least | `https://www.lcsc.com/product-detail/C41376037.html` |
| LCSC-H2032 | LCSC C964792, MYOUNG BS-08-B2AA016: 1+ at 0.4331 | `https://www.lcsc.com/product-detail/C964792.html` |
| LCSC-H2450 | LCSC C964845, MYOUNG MY-2450-01: 5+ at 0.1982, sold 5 at least | `https://www.lcsc.com/product-detail/C964845.html` |
| LCSC-C15008 | LCSC C15008, Samsung CL31A107MQHNNNE, 100 µF 1206: 10+ at 0.1100, sold 10 at least | `https://www.lcsc.com/product-detail/C15008.html` |
| LCSC-C1525 | LCSC C1525, Samsung CL05B104KO5NNNC, 100 nF 0402: 100+ at 0.0032, sold 100 at least | `https://www.lcsc.com/product-detail/C1525.html` |
| LCSC-C25744 | LCSC C25744, UNI-ROYAL 0402WGF1002TCE, 10 kΩ 0402: 100+ at 0.0024, sold 100 at least | `https://www.lcsc.com/product-detail/C25744.html` |
| ADA-654 | Adafruit 654, CR2032 lithium coin cell: 0.95 | `https://www.adafruit.com/product/654` |
| ADA-2850 | Adafruit 2850, CR2450 lithium coin cell: 0.75 | `https://www.adafruit.com/product/2850` |
| ADA-3931 | Adafruit 3931, flanged weatherproof enclosure with PG-7 cable glands: 9.95 | `https://www.adafruit.com/product/3931` |
| ADA-4199 | Adafruit 4199, USB-C to USB-C cable, 1 m: 9.95 | `https://www.adafruit.com/search?q=usb+c+cable` |
| ADA-5801 | Adafruit 5801, vertical wall supply with USB-C, 5 V 3 A: 5.95 | `https://www.adafruit.com/search?q=usb+power+supply` |

**Boards and assembly:**

| Short name | Seller and item | Page |
|---|---|---|
| OSH-4L | OSH Park, 4 Layer Prototype Service: $10 a square inch, three copies | `https://docs.oshpark.com/services/four-layer/` |
| JLC-PCBA | JLCPCB, "PCB Assembly Cost: What Does the Price Include?", last updated Oct 22, 2026 | `https://jlcpcb.com/help/article/pcb-assembly-price` |
| JLC-FAQ | JLCPCB, "PCB Assembly FAQs", item 10 | `https://jlcpcb.com/help/article/pcb-assembly-faqs` |

**Tools and the bench:**

| Short name | Seller and item | Page |
|---|---|---|
| PINE64 | PINE64, Pinecil V2: community price 25.99 | `https://pine64.com/product/pinecil-smart-mini-portable-soldering-iron/` |
| ADA-6320 | Adafruit 6320, Raspberry Pi 45 W USB-C supply: 16.50 | `https://www.adafruit.com/search?q=usb+power+supply` |
| ADA-2473, ADA-1930 | Adafruit, lead-free solder wire 0.5 mm 50 g: 12.50; SAC305 0.5 mm 50 g: 27.50 | `https://www.adafruit.com/search?q=solder` |
| ADA-4456 | Adafruit 4456, solder wick 0.8 mm: 2.95 | `https://www.adafruit.com/search?q=solder` |
| ADA-3468, ADA-2667 | Adafruit, Chip Quik CQ4LF flux pen: 7.95; Chip Quik SMD291 tack flux: 15.95 | `https://www.adafruit.com/search?q=flux` |
| ADA-SEARCH-PASTE, ADA-3217 | Adafruit 3217, "Maker Paste - Low Temperature Lead-Free Prototyping Solder Paste": 9.95 | `https://www.adafruit.com/search?q=solder+paste` |
| ADA-152 | Adafruit 152, flush diagonal cutters CHP170: 7.25 | `https://www.adafruit.com/search?q=flush+cutters` |
| ADA-421, ADA-5950 | Adafruit, fine-tip ESD tweezers: 3.95; ceramic sharp-point tweezers: 8.95 | `https://www.adafruit.com/search?q=tweezers` |
| ADA-850 | Adafruit 850, pocket autoranging multimeter: 24.95, out of stock | `https://www.adafruit.com/search?q=multimeter` |
| ADA-291 | Adafruit 291, helping-hands magnifier MZ101: 6.95 | `https://www.adafruit.com/search?q=magnifier` |
| ADA-4921 | Adafruit 4921, ATTEN ST-2090D 80 W station: 59.95, out of stock | `https://www.adafruit.com/search?q=soldering+iron` |
| ADA-5903, ADA-5238 | Adafruit, Miniware MHP50-B5 hot plate: 99.95; ATTEN ST-862D hot air station: 279.95 | `https://www.adafruit.com/search?q=hot+air` |
| AMSCOPE-SE400Z | AmScope, SE400-Z stereo microscope: 291.99 | `https://amscope.com/products/se400-z` |
| FLUKE-117 | Fluke, 117: 304.99 | `https://www.fluke.com/en-us/product/electrical-testing/digital-multimeters/fluke-117` |
| FLUKE-87V | Fluke, 87V: 579.99 | `https://www.fluke.com/en-us/product/electrical-testing/digital-multimeters/fluke-87v` |
| RL-NANOVNAH4 | R&L Electronics, NanoVNA-H4: 109.95 | `https://www.randl.com/index.php?main_page=product_info&products_id=75145` |
| RL-TINYSA | R&L Electronics, tinySA: 89.95 | `https://www.randl.com/index.php?main_page=product_info&products_id=75243` |
| RL-TINYSAULTRAPLUS | R&L Electronics, tinySA Ultra Plus: 169.95, out of stock | `https://www.randl.com/index.php?main_page=product_info&products_id=76222` |
| RTLSDR | RTL-SDR Blog, V4L dongle: 37.95; dipole antenna kit: 17.95 | `https://www.rtl-sdr.com/buy-rtl-sdr-dvb-t-dongles/` |
| TI-XDS110ET | TI, LP-XDS110ET: 59.29 | `https://www.ti.com/tool/LP-XDS110ET` |
| TI-LPEM | TI, LP-EM-CC1312PSIP: 47.19 | `https://www.ti.com/tool/LP-EM-CC1312PSIP` |

**Not read** (§ What was read):
- Digi-Key, W5017: `https://www.digikey.com/en/products/detail/pulse-electronics/W5017/3025810`;
- JLCPCB's instant quote: `https://cart.jlcpcb.com/quote`;
- JLCPCB's parts search for the module:
  `https://jlcpcb.com/parts/componentSearch?searchTxt=CC1312PSIPMOTR`;
- Hakko USA, FX-971: `https://hakkousa.com/fx-971-soldering-station.html`;
- Jameco, Hammond 1551HRGY:
  `https://www.jameco.com/z/1551HRGY-Hammond-Manufacturing-Gray-Plastic-Enclosure-with-Key-Ring-2-36-x-1-38-x-0-79-Inch-Light-Gray_2550951.html`.

TI's prices are the pages' own price lists, in US dollars by their
`priceCurrency`. LCSC's are its pages' US-dollar price ladders. A
search-page source is the Adafruit search the item was read from, its
product number in the short name.
