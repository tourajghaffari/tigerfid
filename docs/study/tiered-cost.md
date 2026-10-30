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

Every price was read on **2026-10-28**, between Story 2.1.6's clock reads of
14:30:15Z and 14:42:01Z, except those Story 2.1.14 added on **2026-10-29**
(§ Read on 2026-10-29). Each page was fetched by a plain request with a
generic user agent: once, except LCSC's, which were read a second time 15 s
apart (below). Each price below is the one the page printed for the
quantity this build buys.

**Five prices are not read, by Omid's word.** Five pages refused a plain
request, or price only through a script that runs in a browser. They were
put to Omid first-hand in Story 2.1.6's terminal, in one message, between its
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

### Read on 2026-10-29

Story 2.1.14 read what three tools need to run and JLCPCB's minimum, the
Sprint 3 review's findings 2–4. It read these pages on **2026-10-29**,
between its clock reads of 10:18:16Z and 10:24:44Z, each by a plain
request:
- the hot plate's page (ADA-5903), for its kit list and its supply note;
- a 100 W USB-C supply (ACRONAME-R470), and the parts that connect the
  debugger to the boards (ADA-1675, ADA-5434, ADA-2743, ADA-266);
- TI's pages for both debuggers (TI-XDS110ET, TI-XDS110), for their box
  lists and prices;
- JLCPCB's assembly capabilities (JLC-CAP), for its minimum, and its FAQ
  again (JLC-FAQ), which names none;
- the pages of the other tools that run on power or connect to a board,
  for what comes in each box (§ What each tool needs to run): the Pinecil
  and its supply, both stations, the magnifier, the microscope, the three
  meters, the VNA and both receivers. The hand tools and the solder need
  nothing to run, and were not read again. The development board plugs
  into the LP-XDS110ET (`parts-and-toolchains.md` § The debugger), which
  every tier carries.

Every price that story read again matched the one printed here, so no
row's price changed. The prices it added are dated 2026-10-29 in § Sources.

**Refused, but not needed:**
- Tag-Connect's page for its TC2050-IDC-NL cable, a pogo-pin cable for a
  board's debug pads: a bot check ("Please wait while your request is being
  verified..."). Adafruit's pogo-pin clip stands in;
- USBGear's 100 W supply (HTTP 403). Acroname's stands in.

No price this story needs is unread.

### Read on 2026-10-30

Story 2.1.15 read one page again, R&L's NanoVNA-H4 (RL-NANOVNAH4), for its
box list (the Sprint 4 review's Minor 4), by one plain request between two
clock reads that both read 08:31:29Z. The page printed 109.95, the price
below, so no price changed (§ What each tool needs to run).

## How the build is counted

- **The quantities are milestone 1's:** one Reader, one Field Generator and
  three tags. Each part is priced at the break that the build's count falls
  in. A seller that sells only in packs is bought by the pack, and the pack
  is shared across the devices (§ Shared buys).
- **One core in three enclosures,** as `parts-and-toolchains.md` recommends:
  the three tags share one board design. The sitting decides between one
  core and three boards. INFERRED for three boards, each at the core's
  ASSUMED size below:
  - **hand-solder:** OSH Park's price "includes three copies of your
    design" (OSH-4L), so each design is paid for. Three designs cost 34.88
    (3 × 11.625, the board's price before rounding), 23.25 more than one
    core;
  - **an assembled route** pays two more designs' fees. JLCPCB's minimum of
    two boards a design (§ The build routes) then makes six tag boards where
    one core makes three. Partly assembled, that is 109.67 more: 56.62 in
    fees (three designs at two boards, against one at three) and 53.05 for
    the three more boards' modules, chip antennas and sensors. Fully
    assembled, it is 117.12 more: 62.77 in fees and 54.35 in parts, the
    cell holders included. The shared packs still cover the capacitors.
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
  Field Generator and 74 on each tag core, partly assembled. Fully
  assembled adds what the builder would solder, ASSUMED at the SMA
  connector's 5 pins, the debug header's 10 and the cell holder's 3: 160,
  108 and 77. At 0.0016 a joint they cost cents.
- **JLCPCB assembles two boards of a design at the least.** Its
  capabilities page gives the order volume as "2 - 50 pcs" for Economic
  assembly and "2 - 80000 pcs" for Standard (JLC-CAP). Its FAQ caps an
  Economic order at 30 boards unless they are panelled (JLC-FAQ, item 10).
  The tag core, three boards, meets the minimum. INFERRED: the Reader and
  the Field Generator, one board each, are assembled two at a time, with
  the same parts on both boards of a design (§ The tags). So each assembled
  route buys a second Reader and a second Field Generator (§ The minimum's
  second boards). They are spares, and need no antenna, enclosure or cable.

| Fees per design | Partly assembled | Fully assembled |
|---|---|---|
| The Reader | 31.43 (7 part types) | 41.31 (9 types, and its two relays by hand: 3.58 + 8 joints) |
| The Field Generator | 25.21 (5 types) | 31.37 (7 types) |
| The tag core, three boards | 28.49 (6 types) | 31.57 (7 types) |
| **The fees** | **85.13** | **104.25** |

**The minimum's second boards** (INFERRED, from the rows above):

| The second boards | Partly assembled | Fully assembled |
|---|---|---|
| The second Reader | 15.59 (the module, the regulator, the USB bridge and the relay driver) | 19.97 (the same, the SMA connector and two relays) |
| The second Field Generator | 11.11 (the module and the regulator) | 13.09 (the same and the SMA connector) |
| Their joints | 0.38 (145 + 93 at 0.0016) | 0.56 (160 + 108 at 0.0016, and the relays' 8 by hand at 0.0164) |
| **The minimum's second boards** | **27.08** | **33.62** |

The shared packs cover the rest: four of the five USB-C receptacles, four
of the five headers, and about 88 of the 200 passives.

**The routes:**

| Route | The parts | The boards | Assembly | The minimum's second boards | The route | Lacks |
|---|---|---|---|---|---|---|
| Hand-solder | 117.64 | 79.83 (OSH Park: the tag core 11.63, the Reader 37.20, the Field Generator 31.00; 1.16, 3.72 and 3.10 square inches) | — | — | **197.47** | two W5017 whips; three tag enclosures |
| Partly assembled | 129.16 | **not read** (JLCPCB) | 85.13 | 27.08 | **241.36** | the same, the boards, and the module's sourcing at JLCPCB |
| Fully assembled | 129.16 | **not read** (JLCPCB) | 104.25 | 33.62 | **267.03** | the same as partly assembled |

The module's sourcing: if JLCPCB does not stock the CC1312PSIP, the module
is bought from TI (priced above) and sent to the service. That service's
fee, and the shipping to it, are not read.

## The tool tiers

Three ways to the same jobs. Each tier holds what its route needs (§ The
totals), with the parts its tools need to run (§ What each tool needs to
run). Every tier carries the LP-XDS110ET, its cable to the boards and the
tags' clip, and the advanced and pro tiers carry the hot plate's supply.
`parts-and-toolchains.md` recommends one LP-XDS110ET per builder, since it
measures the tag's current. The wall-powered tools are a US seller's: the
ST-2090D is named a 110 V model, and the ST-862D's voltage is not read.

| Job | Starter | Advanced | Pro |
|---|---|---|---|
| Soldering iron | Pinecil V2, 25.99, and a USB-C supply for it, 16.50 | ATTEN ST-2090D 80 W station, 59.95 | Hakko FX-971 station, **not read** |
| Placing the module | — (the boards come assembled) | Miniware MHP50-B5 hot plate, 99.95; low-temperature paste, 9.95; tack flux, 15.95 | the same hot plate, paste and flux; ATTEN ST-862D hot air station, 279.95 |
| Powering the hot plate | — | a 100 W USB-C PD supply, 50.00 | the same supply |
| Solder and its helpers | lead-free solder wire 0.5 mm, 12.50; flux pen, 7.95; solder wick, 2.95; flush cutters, 7.25; ESD tweezers, 3.95 | the same, with ceramic tweezers, 8.95, in place of the ESD pair | the same, with SAC305 lead-free wire, 27.50, in place of the lead-free wire, and the ceramic tweezers in place of the ESD pair |
| Seeing the joints | a magnifier on helping hands, 6.95 | AmScope SE400-Z stereo microscope, 291.99 | the same microscope |
| Measuring | a pocket autoranging meter, 24.95 | Fluke 117, 304.99 | Fluke 87V, 579.99 |
| The antenna match | — (the range test decides, `parts-and-toolchains.md` § The tags) | NanoVNA-H4, 10 kHz to 1.5 GHz, 109.95 | the same VNA |
| Programming and current | LP-XDS110ET, 59.29 | the same | the same |
| Connecting the debugger to the boards | a 10-pin 1.27 mm cable, 2.95, to the Reader's and the Field Generator's headers; for the tags, a pogo-pin clip, 19.95, an SWD breakout board, 2.95, and jumper wires, 3.95 | the same | the same |
| **The tier** | **198.08** | **1,071.42** | **1,581.42, without its iron** |

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
- **The parts the tools need, in every tier that needs them:** the
  supply ACRONAME-R470; the cable ADA-1675; the clip ADA-5434, the
  breakout board ADA-2743 and the jumper wires ADA-266. Each is
  **representative, for pricing**: the studies name none of them, and the
  build initiatives' bench story chooses them.
- **Out of stock on the day** (the price stands as the page printed it): the
  pocket meter ADA-850 and the ATTEN ST-2090D ADA-4921, on 2026-10-28 and
  again on 2026-10-29.
- **A cheaper debugger, not in the totals:** the LP-XDS110, 35.09
  (TI-XDS110, read 2026-10-29). It is the same debugger without the current
  measurement for which `parts-and-toolchains.md` § The debugger recommends
  the LP-XDS110ET.

### What each tool needs to run

Each tool's page was read on 2026-10-29 for what comes in its box (§ Read
on 2026-10-29). Three tools need a part they do not come with, and each
part is priced above:
- **The hot plate.** Its kit holds the "MHP50 Controller and Hot Plate" and
  a "Silicone USB C Cable (20V 5A)", and its page says: "Note that a 20V 5A
  USB PD power supply is not included! You'll need to pick up a 100W
  'laptop' power supply with USB type C PD support" (ADA-5903). The supply
  priced is Acroname's R470-TYPEC-PS-1. It gives "up to 20V at 5A" through
  "a captive USB-C (69") cable", from 100–240 V (ACRONAME-R470).
- **The debugger.** TI's LP-XDS110ET page lists "LaunchPad development
  kit", "USB-A to USB-C cable" and "Quick start guide", and no cable for
  its "10-pin XDS110 out connector" (TI-XDS110ET). ASSUMED: that connector
  takes the 1.27 mm 2 × 5 layout of the boards' representative header
  (§ The Reader), since TI's page gives no pitch. So:
  - the Reader and the Field Generator take a socket-to-socket cable whose
    "connector is a 1.27mm (0.05") pitch 2x5 box" (ADA-1675);
  - the tags carry pads, not a connector (`parts-and-toolchains.md`
    § The tags). They take a pogo-pin clip with "2x5 0.05" pitch SWD
    spacing" (ADA-5434). Its pins come out on "a 2x5 0.1" header", so the
    cable reaches it through an SWD breakout board, made for "tiny boards
    that use the 10-pin SWD programming connector" (ADA-2743), and
    female-to-female jumper wires with "0.1" sockets on either end"
    (ADA-266). INFERRED: the breakout's header is 0.1", the clip's pitch;
    its page says only "the breakout board and some header".
  - The clip reaches "25mm" from a board's edge (ADA-5434). INFERRED: the
    tag core's debug pads sit within 25 mm of an edge, for the build
    initiatives to keep.
- **The others:**
  - the Pinecil V2's package holds the iron and one tip (PINE64). The
    starter supply's "1.5m 17AWG captive cable" ends in a "USB-C output
    connector" (ADA-6320);
  - the pocket meter "Runs on two AA batteries that are included!"
    (ADA-850). The Fluke 87V's box holds a "9V Battery (Installed)"
    (FLUKE-87V);
  - **the Fluke 117's box list names no battery,** though the meter takes
    a "9 volt Alkaline" (FLUKE-117). So the advanced tier's Lacks name it;
  - the ATTEN station includes its "Station/receptacle" and "Soldering
    iron" (ADA-4921). The hot air station "Comes with a nice stand and four
    different nozzles" (ADA-5238). The microscope carries its LED light on
    a 110 V supply (AMSCOPE-SE400Z);
  - **the NanoVNA-H4's box holds its cables, and a tag's board has no
    connector for them.** Its page's "Packing list" names the VNA "with
    1950mAh battery", two "15cm SMA male to male RG316RF cable", "SMA
    female to female connector x1", an SMA male calibration kit (open,
    short and load) and two USB-C cables (RL-NANOVNAH4, read 2026-10-30).
    "LOAD KIT (SUPPLIED)", at 12.95, and a "TEST JIG FOR TESTING YOUR OWN
    CIRCUIT", at 9.95, are among the page's paid accessories. INFERRED: the
    cables meet the Reader's and the Field Generator's SMA connector, and a
    tag's board, with its chip antenna, has none. How a tag's board meets
    the VNA, a coaxial pigtail or a test connector, is the bench story's
    choice. The advanced and pro tiers' Lacks name it. The Sprint 4 review
    found the page's box list first (2026-10-29), where this line had said
    the page lists no cable to a board;
  - the tinySA Ultra Plus includes an "Antenna with SMA connector" and two
    SMA cables (RL-TINYSAULTRAPLUS). **The plain tinySA's page lists no box
    contents.** It sells a "REPLACEMENT ANTENNA" at 9.95 (RL-TINYSA), so
    the starter and advanced tiers' Lacks name its antenna, if one is not
    in the box.

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
  Whether its antenna comes with it is not read (§ What each tool needs to
  run).
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
| Starter | fully assembled | 267.03 | 198.08 | 137.14 | **602.25** | two W5017 whips; three tag enclosures; the JLCPCB boards; the module's sourcing at JLCPCB; the tinySA's antenna, if not in its box |
| Advanced | partly assembled | 241.36 | 1,071.42 | 137.14 | **1,449.92** | the same as starter; the Fluke 117's battery, if not in its box; a tag board's connection to the VNA |
| Pro | hand-solder | 197.47 | 1,581.42 | 217.14 | **1,996.03** | two W5017 whips; three tag enclosures; the pro iron; a tag board's connection to the VNA |

**Any route with any tier** (the devices, the tier's tools and its bench;
each lacks what its route and its tier lack above):

| Route | Starter | Advanced | Pro |
|---|---|---|---|
| Hand-solder | — (no tool to place the module) | 1,406.03 | 1,996.03 |
| Partly assembled | 576.58 | 1,449.92 | 2,039.92 |
| Fully assembled | 602.25 | 1,475.59 | 2,065.59 |

**What the totals say** (INFERRED, from the tables above):
- **The tools and the bench set the tiers apart, not the devices.** The
  devices run from 197.47 to 267.03 on any route, before the unread prices.
  The tools run from 198.08 to 1,581.42.
- **The routes differ little in money.** Assembly costs 43.89 to 69.56 more
  than the hand-solder route's parts and boards, before JLCPCB's boards and
  the module's sourcing. JLCPCB's minimum, the second Reader and Field
  Generator, is 27.08 and 33.62 of that. What a route changes is the skill
  and the tools it needs.
- **The module is the largest part:** five of them, 53.66, 46 % of the
  hand-solder route's parts. An assembled route buys seven, 75.12, the
  minimum's two included.
- **Recommended for a first build: the starter tier with fully assembled
  boards,** 602.25 before its unread prices. It asks the least skill of a
  beginner (spec anti-goals), with the least spent on tools. Its costs:
  - partly assembled boards would save 25.66 (576.58): 19.12 in fees and
    6.54 in the minimum's second boards. The builder then solders four
    parts by iron on each board: the relays, the SMA connector, the cell
    holder and the debug header;
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
  three, where three boards cost 23.25 to 117.12 more by route (§ How the
  build is counted), and the item and nursery tags' cells, which price does
  not decide. If it wants the five unread prices, it reads them in a browser
  (§ What was read).
- **The build initiatives:**
  - the bench story, first (spec D9): the receiver (the tinySA, or the
    RTL-SDR once its range is read), a stencil for the hand-solder route,
    the paste against the module's reflow profile, and how a board meets
    the VNA;
  - every part marked representative, chosen from its datasheet, and the
    boards' real sizes, which re-price the boards;
  - the JLCPCB quote for the drawn boards and the module's sourcing, before
    an assembled route is ordered;
  - prices read again before anything is bought. These are 2026-10-28's
    and 2026-10-29's.

## Sources

Every page was read 2026-10-28, between 14:30:15Z and 14:42:01Z, as
§ What was read says. A page dated 2026-10-29 was read by Story 2.1.14,
between 10:18:16Z and 10:24:44Z (§ Read on 2026-10-29), and a page dated
both was read again then at the same price. None is pinned in the corpus.
The cited datasheets (PSIP, SWRA797, SWRA349) are `parts-and-toolchains.md`'s,
pinned there.

**Parts:**

| Short name | Seller and item | Page | Read |
|---|---|---|---|
| TI-PSIP | TI, CC1312PSIP: CC1312PSIPMOTR, 1–99 at 10.732, 100–249 at 9.374, 250–999 at 7.228, 1,000 and up at 6.465 | `https://www.ti.com/product/CC1312PSIP` | 2026-10-28 |
| TI-TMP117 | TI, TMP117: TMP117AIDRVR, 1–99 at 5.757 | `https://www.ti.com/product/TMP117` | 2026-10-28 |
| TI-TLV755P | TI, TLV755P: TLV75533PDBVR, 1–99 at 0.374 | `https://www.ti.com/product/TLV755P` | 2026-10-28 |
| TI-TPL7407L | TI, TPL7407L: TPL7407LDR, 1–99 at 0.731 | `https://www.ti.com/product/TPL7407L` | 2026-10-28 |
| LCSC-ANT | LCSC C17443637, Johanson 0915AT43A0026001E: 1+ at 1.1947, 10+ at 0.9784 | `https://www.lcsc.com/product-detail/C17443637.html` | 2026-10-28 |
| LCSC-SMA | LCSC C5356059, RF Solutions CON-SMA-EDGE-S: 1+ at 1.9861 | `https://www.lcsc.com/product-detail/C5356059.html` | 2026-10-28 |
| LCSC-MCP2200 | LCSC C220000, Microchip MCP2200-I/SS: 1+ at 3.7563 | `https://www.lcsc.com/product-detail/C220000.html` | 2026-10-28 |
| LCSC-USBC | LCSC C165948, Hroparts TYPE-C-31-M-12: 5+ at 0.1886, sold 5 at least | `https://www.lcsc.com/product-detail/C165948.html` | 2026-10-28 |
| LCSC-RELAY | LCSC C2976854, HONGFA HF46F-G/005-HS1: 1+ at 1.1947 | `https://www.lcsc.com/product-detail/C2976854.html` | 2026-10-28 |
| LCSC-HEADER | LCSC C41376037, hanxia HX PZ1.27-2x5P TP: 5+ at 0.0983, sold 5 at least | `https://www.lcsc.com/product-detail/C41376037.html` | 2026-10-28 |
| LCSC-H2032 | LCSC C964792, MYOUNG BS-08-B2AA016: 1+ at 0.4331 | `https://www.lcsc.com/product-detail/C964792.html` | 2026-10-28 |
| LCSC-H2450 | LCSC C964845, MYOUNG MY-2450-01: 5+ at 0.1982, sold 5 at least | `https://www.lcsc.com/product-detail/C964845.html` | 2026-10-28 |
| LCSC-C15008 | LCSC C15008, Samsung CL31A107MQHNNNE, 100 µF 1206: 10+ at 0.1100, sold 10 at least | `https://www.lcsc.com/product-detail/C15008.html` | 2026-10-28 |
| LCSC-C1525 | LCSC C1525, Samsung CL05B104KO5NNNC, 100 nF 0402: 100+ at 0.0032, sold 100 at least | `https://www.lcsc.com/product-detail/C1525.html` | 2026-10-28 |
| LCSC-C25744 | LCSC C25744, UNI-ROYAL 0402WGF1002TCE, 10 kΩ 0402: 100+ at 0.0024, sold 100 at least | `https://www.lcsc.com/product-detail/C25744.html` | 2026-10-28 |
| ADA-654 | Adafruit 654, CR2032 lithium coin cell: 0.95 | `https://www.adafruit.com/product/654` | 2026-10-28 |
| ADA-2850 | Adafruit 2850, CR2450 lithium coin cell: 0.75 | `https://www.adafruit.com/product/2850` | 2026-10-28 |
| ADA-3931 | Adafruit 3931, flanged weatherproof enclosure with PG-7 cable glands: 9.95 | `https://www.adafruit.com/product/3931` | 2026-10-28 |
| ADA-4199 | Adafruit 4199, USB-C to USB-C cable, 1 m: 9.95 | `https://www.adafruit.com/search?q=usb+c+cable` | 2026-10-28 |
| ADA-5801 | Adafruit 5801, vertical wall supply with USB-C, 5 V 3 A: 5.95 | `https://www.adafruit.com/search?q=usb+power+supply` | 2026-10-28 |

**Boards and assembly:**

| Short name | Seller and item | Page | Read |
|---|---|---|---|
| OSH-4L | OSH Park, 4 Layer Prototype Service: $10 a square inch, three copies | `https://docs.oshpark.com/services/four-layer/` | 2026-10-28 |
| JLC-PCBA | JLCPCB, "PCB Assembly Cost: What Does the Price Include?", last updated Oct 22, 2026 | `https://jlcpcb.com/help/article/pcb-assembly-price` | 2026-10-28 |
| JLC-FAQ | JLCPCB, "PCB Assembly FAQs", item 10; "Last updated on Sep 09, 2026" on the second read, which found no minimum | `https://jlcpcb.com/help/article/pcb-assembly-faqs` | 2026-10-28; 2026-10-29 |
| JLC-CAP | JLCPCB, "PCB Assembly Technical Capabilities": order volume "2 - 50 pcs" (Economic), "2 - 80000 pcs" (Standard) | `https://jlcpcb.com/capabilities/pcb-assembly-capabilities` | 2026-10-29 |

**Tools and the bench:**

| Short name | Seller and item | Page | Read |
|---|---|---|---|
| PINE64 | PINE64, Pinecil V2: community price 25.99 | `https://pine64.com/product/pinecil-smart-mini-portable-soldering-iron/` | 2026-10-28; 2026-10-29 |
| ADA-6320 | Adafruit 6320, Raspberry Pi 45 W USB-C supply: 16.50 | `https://www.adafruit.com/search?q=usb+power+supply`; on 2026-10-29, `https://www.adafruit.com/product/6320` | 2026-10-28; 2026-10-29 |
| ADA-2473, ADA-1930 | Adafruit, lead-free solder wire 0.5 mm 50 g: 12.50; SAC305 0.5 mm 50 g: 27.50 | `https://www.adafruit.com/search?q=solder` | 2026-10-28 |
| ADA-4456 | Adafruit 4456, solder wick 0.8 mm: 2.95 | `https://www.adafruit.com/search?q=solder` | 2026-10-28 |
| ADA-3468, ADA-2667 | Adafruit, Chip Quik CQ4LF flux pen: 7.95; Chip Quik SMD291 tack flux: 15.95 | `https://www.adafruit.com/search?q=flux` | 2026-10-28 |
| ADA-SEARCH-PASTE, ADA-3217 | Adafruit 3217, "Maker Paste - Low Temperature Lead-Free Prototyping Solder Paste": 9.95 | `https://www.adafruit.com/search?q=solder+paste` | 2026-10-28 |
| ADA-152 | Adafruit 152, flush diagonal cutters CHP170: 7.25 | `https://www.adafruit.com/search?q=flush+cutters` | 2026-10-28 |
| ADA-421, ADA-5950 | Adafruit, fine-tip ESD tweezers: 3.95; ceramic sharp-point tweezers: 8.95 | `https://www.adafruit.com/search?q=tweezers` | 2026-10-28 |
| ADA-850 | Adafruit 850, pocket autoranging multimeter: 24.95, out of stock | `https://www.adafruit.com/search?q=multimeter`; on 2026-10-29, `https://www.adafruit.com/product/850` | 2026-10-28; 2026-10-29 |
| ADA-291 | Adafruit 291, helping-hands magnifier MZ101: 6.95 | `https://www.adafruit.com/search?q=magnifier`; on 2026-10-29, `https://www.adafruit.com/product/291` | 2026-10-28; 2026-10-29 |
| ADA-4921 | Adafruit 4921, ATTEN ST-2090D 80 W station: 59.95, out of stock | `https://www.adafruit.com/search?q=soldering+iron`; on 2026-10-29, `https://www.adafruit.com/product/4921` | 2026-10-28; 2026-10-29 |
| ADA-5903, ADA-5238 | Adafruit, Miniware MHP50-B5 hot plate: 99.95, its kit list and its supply note; ATTEN ST-862D hot air station: 279.95 | `https://www.adafruit.com/search?q=hot+air`; on 2026-10-29, `https://www.adafruit.com/product/5903` and `https://www.adafruit.com/product/5238` | 2026-10-28; 2026-10-29 |
| ACRONAME-R470 | Acroname, R470-TYPEC-PS-1, 100 W USB-C Power Delivery charger, up to 20 V at 5 A: 50.00 | `https://acroname.com/store/r470-typec-ps-1` | 2026-10-29 |
| ADA-1675 | Adafruit 1675, 10-pin 2 × 5 socket-to-socket 1.27 mm IDC (SWD) cable, 150 mm: 2.95 | `https://www.adafruit.com/product/1675` | 2026-10-29 |
| ADA-5434 | Adafruit 5434, pogo pin probe clip, 2 × 5 1.27 mm pitch SWD: 19.95 | `https://www.adafruit.com/product/5434` | 2026-10-29 |
| ADA-2743 | Adafruit 2743, SWD (2 × 5 1.27 mm) cable breakout board: 2.95 | `https://www.adafruit.com/product/2743` | 2026-10-29 |
| ADA-266 | Adafruit 266, premium female/female jumper wires, 40 × 6": 3.95 | `https://www.adafruit.com/product/266` | 2026-10-29 |
| AMSCOPE-SE400Z | AmScope, SE400-Z stereo microscope: 291.99 | `https://amscope.com/products/se400-z` | 2026-10-28; 2026-10-29 |
| FLUKE-117 | Fluke, 117: 304.99 | `https://www.fluke.com/en-us/product/electrical-testing/digital-multimeters/fluke-117` | 2026-10-28; 2026-10-29 |
| FLUKE-87V | Fluke, 87V: 579.99 | `https://www.fluke.com/en-us/product/electrical-testing/digital-multimeters/fluke-87v` | 2026-10-28; 2026-10-29 |
| RL-NANOVNAH4 | R&L Electronics, NanoVNA-H4: 109.95 | `https://www.randl.com/index.php?main_page=product_info&products_id=75145` | 2026-10-28; 2026-10-29; 2026-10-30 |
| RL-TINYSA | R&L Electronics, tinySA: 89.95; its replacement antenna: 9.95 | `https://www.randl.com/index.php?main_page=product_info&products_id=75243` | 2026-10-28; 2026-10-29 |
| RL-TINYSAULTRAPLUS | R&L Electronics, tinySA Ultra Plus: 169.95, out of stock | `https://www.randl.com/index.php?main_page=product_info&products_id=76222` | 2026-10-28; 2026-10-29 |
| RTLSDR | RTL-SDR Blog, V4L dongle: 37.95; dipole antenna kit: 17.95 | `https://www.rtl-sdr.com/buy-rtl-sdr-dvb-t-dongles/` | 2026-10-28 |
| TI-XDS110ET | TI, LP-XDS110ET: 59.29, and its box list | `https://www.ti.com/tool/LP-XDS110ET` | 2026-10-28; 2026-10-29 |
| TI-XDS110 | TI, LP-XDS110: 35.09, and its box list | `https://www.ti.com/tool/LP-XDS110` | 2026-10-29 |
| TI-LPEM | TI, LP-EM-CC1312PSIP: 47.19 | `https://www.ti.com/tool/LP-EM-CC1312PSIP` | 2026-10-28 |

**Not read** (§ What was read):
- Digi-Key, W5017: `https://www.digikey.com/en/products/detail/pulse-electronics/W5017/3025810`;
- JLCPCB's instant quote: `https://cart.jlcpcb.com/quote`;
- JLCPCB's parts search for the module:
  `https://jlcpcb.com/parts/componentSearch?searchTxt=CC1312PSIPMOTR`;
- Hakko USA, FX-971: `https://hakkousa.com/fx-971-soldering-station.html`;
- Jameco, Hammond 1551HRGY:
  `https://www.jameco.com/z/1551HRGY-Hammond-Manufacturing-Gray-Plastic-Enclosure-with-Key-Ring-2-36-x-1-38-x-0-79-Inch-Light-Gray_2550951.html`.

**Refused on 2026-10-29, not needed** (§ Read on 2026-10-29):
- Tag-Connect, TC2050-IDC-NL:
  `https://www.tag-connect.com/product/tc2050-idc-nl-10-pin-no-legs-cable-with-ribbon-connector`;
- USBGear, CG-PD100C: `https://www.usbgear.com/CG-PD100C.html`.

TI's prices are the pages' `data-price` values. They name no currency, and
INFERRED, they are US dollars, since the pages' exchange-rate table is
quoted against the dollar (`parts-and-toolchains.md` § The debugger).
LCSC's are its pages' US-dollar price ladders. A
search-page source is the Adafruit search the item was read from, its
product number in the short name.
