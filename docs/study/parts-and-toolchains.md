# Parts and free toolchains

The fourth document of the design study (plan Story 2.1.4). It names, for
the radio architecture `docs/study/radio-and-location.md` chose:

- the parts of each device;
- each part's free toolchain, and its install on Windows, macOS and Linux;
- a debugger a beginner can afford;
- the licence of each reference design read;
- the warehouse item tag's choice between a slower wake and a larger cell
  (`docs/study/requirements.md` item [27]);
- the chosen parts' datasheet figures, in place of the requirements'
  ASSUMED ones;
- one electronics core in three enclosures against three boards (spec
  §3.7), with milestone 1's sealTag as a new design inside that question.

A claim marked **INFERRED** was reasoned, not read: its arithmetic is shown.
A figure marked **ASSUMED** is an input to that arithmetic, not a
measurement. Everything else cites the text it was read from, by the short
names in § Sources. Nothing here has been measured on a board, and **no
install was run on any platform by this study**: every install and
runs-on claim below is cited from the vendor's own page, with its URL and
fetch date. This document recommends; the decision sitting (Story 2.1.7)
decides. It is not legal advice.

## What was read, and what could not be

Every source was read on 2026-10-27, between this leg's clock reads of
19:05:03Z and 19:30:38Z. The law texts were fetched between 19:09:55Z and
19:10:16Z.

**The law for the parts around the module** is read at the eCFR's point in
time of 2026-10-23, the date the eCFR reported Title 47 as up to date to. A
law text's version date is its pin (plan D8).

| Short name | Text | Last amendment it prints | Read from |
|---|---|---|---|
| §15.3 | 47 CFR 15.3, definitions (h), (i), (k) and (z) | 88 FR 47394 (July 24, 2023) | `https://www.ecfr.gov/api/versioner/v1/full/2026-10-23/title-47.xml?part=15&section=15.3` |
| §15.101 | 47 CFR 15.101, equipment authorisation of unintentional radiators | 82 FR 50832 (Nov. 2, 2017) | the same URL with `section=15.101` |
| §15.103 | 47 CFR 15.103, exempted devices | 90 FR 38071 (Aug. 7, 2025) | `section=15.103` |
| §15.107, §15.109 | 47 CFR 15.107 and 15.109, the conducted and radiated limits | 67 FR 45670 (July 10, 2002); 80 FR 33447 (June 12, 2015) | `section=15.107`, `section=15.109` |
| §2.803 | 47 CFR 2.803(d)(2)(iv), the sale of evaluation kits | 91 FR 57800 (Sept. 11, 2026) | `part=2&section=2.803` |
| §2.906 | 47 CFR 2.906, the Supplier's Declaration of Conformity | 90 FR 38066 (Aug. 7, 2025) | `part=2&section=2.906` |

The same texts are viewed at
`https://www.ecfr.gov/on/2026-10-23/title-47/section-<n>`. The eCFR's API
now answers only a request that accepts a compressed reply (friction F14).

**Not read: the module's FCC grant record.**
- **The requests refused.** The FCC's own equipment-authorisation search
  and two public mirrors of it (fccid.io and fcc.report) answered HTTP 403
  to this leg's plain requests, and so did the harness's web-reading tool.
  The FCC's refusal is a content network's "Access Denied" page. Omid
  switched the session's network route, and a retry at 19:17:08Z was
  refused the same way.
- **The ruling.** The grant opens in a browser only, so it was put to Omid
  first-hand in this terminal, between this leg's clock reads of 19:17:09Z
  and 19:25:00Z. He answered "Write it as not read".
- **What TI's documents say instead:**
  - the datasheet names FCC ID ZAT-1312PSIP-2 for the module part
    CC1312PSIPMOT2, "15.247 Operation within the 902-928 MHz band" (PSIP
    p54, p56);
  - TI's 2024 certification note lists its FCC filings, the modular approval
    letter and the five PHYs' Part 15C test reports (Wi-SUN among them),
    under ZAT-1312PSIP-3 (SWRA797 p14, p17).
- **What stays open.** The two IDs, the PHY each grant covers, and the
  grant's equipment class: frequency hopping, or digital transmission
  500 kHz wide. `radio-and-location.md` labels the device "Contains FCC ID:
  ZAT-1312PSIP-2", from the datasheet. The design hops channels, which holds
  under either class (`radio-and-location.md` § Channels).
- **The read before any part is bought:** both grant pages, in a browser.
  It decides the label's ID and confirms that the chosen PHY is on the
  grant.

**Refused, so no part is named from them:**
- FTDI's driver page and Microchip's product page, both HTTP 403 to a plain
  request (Microchip's was the same "Access Denied" page). So the Reader's
  USB bridge is named by the class it must belong to, not by part
  (§ The Reader).
- TI's certification page for the module (HTTP 404).
- Arm's GNU toolchain download page: it renders by script, so it was not
  read (§ The toolchains).

## The parts, device by device

Every device carries the same radio: **TI's CC1312PSIP**, at the certified
300 kbit/s PHY that `radio-and-location.md` chose. That PHY is Wi-SUN's
"Mode #5" (SWRA797 Table 2-1, p5). The module brings its own processor, its
crystals and its power parts (PSIP p1), so the parts around it are few.

**The module's figures used below** (PSIP, the page each sits on):

| Figure | Value | Page |
|---|---|---|
| Supply | 1.8 V to 3.8 V; boost mode, which the +14 dBm setting uses, needs 2.1 V or more | p1, p12 |
| Receive current, 868 MHz | 5.8 mA | p14 |
| Transmit current | 9.4 mA at 0 dBm and 17.3 mA at +10 dBm (868 MHz); 28.7 mA at +14 dBm in boost mode (868 MHz); 62 mA at +19 dBm and 86 mA at +20 dBm (915 MHz) | p14 |
| Standby, RTC on the 32 kHz crystal | 1.0 µA (0.9 µA on the internal RC oscillator) | p13 |
| Active, the processor at 48 MHz | 2.89 mA | p13 |
| Standby to active | 165 µs | p23 |
| 48 MHz crystal start-up | 200 µs | p23 |
| RF frequency accuracy | ±10 ppm | p23 |
| RTC accuracy, −40 to 65 °C, compensated | −50 to +50 ppm | p24 |
| Internal temperature sensor | 2 °C resolution; ±3.5 °C from 0 to 105 °C; ±5.0 °C from −40 to 0 °C | p32 |
| Wake from standby | on the RTC, or on a pin edge | p51, Table 9-2 |
| Debug | cJTAG (IEEE 1149.7) by default, or 4-pin JTAG | p50 |
| Operating temperature | −40 to +105 °C with the +14 dBm amplifier | p1 |

The receive current is given at 868 MHz only. INFERRED: the study uses it
at 915 MHz as well.

### The Reader

| Part | Choice | Why, and its source |
|---|---|---|
| Radio | CC1312PSIP, sending beacons on its 20 dBm port at 17 dBm | The certified Wi-SUN PHY allows 17 dBm there without duty cycling, under a 2.69 dBi antenna (SWRA797 §2.2.5) |
| Antenna | Pulse W5017 whip, on an SMA connector | A certified antenna, +0.90 dBi (SWRA797 Table 3-2). "The external whip antenna is best for usage with a SMA connector" (SWRA797 p13) |
| Host link | a USB-to-serial bridge that enumerates as a USB CDC device | No driver to install on any of the three platforms. "The CDC class has been implemented in Windows (since Windows 98), macOS and most Linux distributions. Since Windows 10 ... the operating system now has a generic driver that will be used for CDC" (MCHP-CDC § 2.3). The part is the build initiative's choice: the vendors' pages this leg tried refused (§ What was read) |
| Power | USB's 5 V, through a 3.3 V regulator | INFERRED: the regulator is rated above the 86 mA the module draws at +20 dBm (PSIP p14), and USB powers the Reader and carries its data on one cable |
| I/O | two relays and two supervised inputs, with their drivers and protection | The original's I/O, kept (`radio-and-location.md` § Does ActiveWave's approach still win). The parts are the build initiative's |
| Debug | a 10-pin connector for the XDS110's "XDS110 out" cable | § The debugger |

**In the EU,** the Reader stays at 25 mW e.r.p. (SRD band 47, read by
`radio-and-location.md`). INFERRED: with the W5017's +0.90 dBi, that is a
conducted power of 15.2 dBm at most (14.0 dBm e.r.p. + 2.15 − 0.90). At
17 dBm it would be 37.6 mW e.r.p.

**Not chosen for milestone 1: Ethernet.** One Reader talks to one laptop,
which USB serves. A warehouse's 9–12 Readers ([20]) are a later tier's
question. INFERRED.

### The Field Generator

The same module, fixed in a room or at an exit (`radio-and-location.md`
§ Zones):

- **Radio and antenna:** as the Reader's, at a power set for its room. The
  power is the zone's size.
- **Power:** USB's 5 V from a plug-in adapter, and a 3.3 V regulator.
  INFERRED: this keeps mains wiring off a beginner's board.
- **Options:** a relay at an exit, so a door's output need not wait on the
  host (`radio-and-location.md`), and the original's motion input
  (`original-system.md` § The Standard Field Generator).
- **No host link:** a Field Generator talks to the Reader by radio.

### The tags

Each tag style is bound by the scenarios that use it. INFERRED from the
requirements' words:
- the **cardTag**, a badge, by the conference ([5], [9]);
- the **compactTag**, a fob, by the conference and by the nursery's adults
  ([8], [9], [18]);
- the **sealTag**, a band or a seal with a tamper loop, by the nursery's
  babies ([14], [18]) and the warehouse's sealed cases ([23]);
- a **warehouse item tag**, by [27]. It is the item tier of
  `radio-and-location.md`'s three hardware tiers, and not a milestone 1
  device.

| Part | Choice | Why, and its source |
|---|---|---|
| Radio | CC1312PSIP, replying on its 14 dBm port at 12.0 dBm | The certified Wi-SUN PHY allows 12.0 dBm without duty cycling under a 2.69 dBi antenna (SWRA797 §2.1.5) |
| Antenna | Johanson Technology 0915AT43A0026 chip antenna | Certified, +1.0 dBi (SWRA797 Table 3-2). TI's own compact 2-layer design uses it (SWRA797 p13) |
| Board | four layers | "A 4-layer design is the preferred choice for a robust and low emission design" (SWRA797 p8). A 2-layer board is allowed with care, and "If there is uncertainty or if the previous guidelines cannot be followed, then a 4-layer design can be used" (SWRA797 p10). INFERRED: a beginner's first board takes the forgiving route. Its price is Story 2.1.6's |
| Cell | a CR2032 (Energizer: 235 mAh, 20.0 mm × 3.2 mm); a CR2450 for the item tag (620 mAh, 24.5 mm × 5.0 mm) | § The battery; § [27] |
| Buffer capacitor | across the cell | A capacitor beside a CR2032 "is the most effective choice a designer can make to maximize battery capacity utilization in low power RF applications" (SWRA349 p1) |
| Cell compartment | opened with a tool | The cell maker's safety note: "battery compartments should be designed with one of the following methods: a) a tool such as screwdriver or coin is required to open battery compartment" or two independent movements (Energizer CR2032) |
| Temperature sensor | TI TMP117, on the nursery's tags and the cold-chain item tags; a fitting option on the others | ±0.1 °C from −20 to 50 °C; I²C; 1.7–5.5 V; a one-shot conversion of 15.5 ms at 135 µA; 0.15 µA in shutdown; a 2 mm × 2 mm package (TMP117 p1, p6). The module's own sensor reads to ±3.5 °C at 2 °C resolution (PSIP p32). INFERRED: that is too coarse for a cold-chain limit a few degrees wide, and it measures the chip, not the item |
| Tamper input | the band's or seal's loop, wired to a pin that wakes the module on either edge | Wake on a pin edge from standby (PSIP p51). The 2012 seal woke on its band's input the same way (`original-system.md` § The sealTag) |
| Debug | pads for the XDS110's cable, or a pogo-pin fixture | INFERRED: a 10-pin connector does not fit a fob. The 2012 seal carried pogo pins (`original-system.md` § The sealTag) |

**The nursery tag reads its temperature sensor against a limit set on
screen.** It is not a medical device and not a monitor of vital signs (spec
C8). The requirements set no accuracy for the sensor. INFERRED: the sitting
names one if it wants one, since any figure the study picked would be a new
requirement (plan D1).

**The antenna match may need tuning.** "Tuning of the antenna impedance
matching network is recommended after manufacturing of the PCB to account
for PCB parasitics" (PSIP p57). INFERRED: tuning needs a vector network
analyser. That instrument sits in Story 2.1.6's advanced tool tier. The
build's range test ([2]) decides whether an untuned match is enough.

### The law for the parts around the module (Subpart B)

`radio-and-location.md` left Part 15's Subpart B to this leg.

- **What the Reader's electronics are.** A digital device is "An
  unintentional radiator ... that generates and uses timing signals or
  pulses at a rate in excess of 9,000 pulses (cycles) per second and uses
  digital techniques" (§15.3(k)). A Class B digital device is "marketed for
  use in a residential environment" (§15.3(i)), and a Class A one "for use in
  a commercial, industrial or business environment" (§15.3(h)).
- **The radio's own digital part is carved out.** "an intentional radiator
  subject to subpart C of this part that contains a digital device is not
  subject to the standards for digital devices, provided the digital device
  is used only to enable operation of the radio frequency device and the
  digital device does not control additional functions or capabilities"
  (§15.3(k)).
  - INFERRED: a Field Generator without a relay, and a tag without a
    sensor, fall inside that carve-out.
  - INFERRED: the Reader's USB link and relays are "additional functions",
    so its electronics are a digital device under Subpart B, as are a
    relay-carrying Field Generator and a sensor tag.
- **Milestone 1 needs no authorisation for them.** "Except as otherwise
  exempted in §§ 15.23, 15.103, and 15.113, unintentional radiators shall
  be authorized prior to the initiation of marketing" (§15.101(a)). §15.23
  is the home-built exemption `radio-and-location.md` relies on: five
  devices, built for personal use, not marketed.
- **Beyond five, or once marketed:**
  - "Other Class B digital devices & peripherals" and "All other receivers
    subject to Part 15" take "SDoC or Certification" (§15.101(a), Table 1).
    A receiver tuning "within the frequency range of 30-960 MHz" is subject
    to it (§15.101(b)).
  - The Supplier's Declaration of Conformity is a procedure in which "the
    responsible party ... makes measurements or completes other procedures
    found acceptable to the Commission to ensure that the equipment
    complies" (§2.906(a)).
  - The radiated limit for a Class B device is 200 µV/m at 3 m from 216 to
    960 MHz (§15.109(a)).
  - TI says the same of its module: "the end-customer must also verify that
    there are no other unintentional emission radiators (FCC Part 15B) on
    the design" (SWRA797 p14).
- **No exemption fits.** None of §15.103's exemptions describes a tag, a
  Reader or a Field Generator (§15.103(a)–(h)). INFERRED.

So the module's grant covers the transmitter. The Reader's emissions are
the builder's care in milestone 1 (§15.23(b)), and a measured question for
anyone who goes beyond five or markets a device. The decision sitting
should know that the "beyond five" route of `radio-and-location.md` §
Legality of the choice adds Subpart B's limits for the Reader. Meeting them
takes a measurement this study cannot make. Its layout rules (SWRA797 §3.1)
are the first defence.

## The toolchains, part by part, on three platforms

Only the module runs firmware. The sensor, the antennas, the cells, the
regulators and the relays have no toolchain. The USB bridge needs no driver
on any platform (§ The Reader). Every board is planned in KiCad 10 (spec
D4). The table cites each install. None was run.

| Tool | What it is for | Windows | macOS | Linux | Licence and price | Source |
|---|---|---|---|---|---|---|
| KiCad 10.0.7 | every board: the Reader, the Field Generator, the tag core | Windows 10 and 11 | macOS 12 and newer | an AppImage for x86-64 | GPL-3.0-or-later; no price. Its libraries' licence is in `THIRD_PARTY_NOTICES.md` | KICAD-DL; KICAD-LIC |
| SimpleLink Low Power F2 SDK 8.33.00.16 | the module's drivers, its RF driver and the certified PHY settings | — | — | — | TI's limited licence, "royalty-free" and for "use only with TI Devices"; no price (§ The licences) | SDK-GH; SDK-LIC |
| SysConfig 1.28.2 | the module's configuration, and the SDK's default PHY settings that the certification requires | installer | installer | installer | no price stated on the page | TI-SYSCONFIG |
| TI Arm Clang 5.1.1.LTS | the compiler | x64 installer | x64 and arm64 installers; the x86 build is "known to work with MacOS version 13.6" | x64 and arm64 installers | no price stated on the page | TI-CGT |
| Arm GNU Toolchain | the open compiler the SDK also builds with ("ARM Gnu Toolchain (GCC_ARMCOMPILER)") | not read | not read | not read | not read | SDK-GH. Arm's page renders by script and was not read |
| CMake, GNU make | the SDK's build ("run GNU `make`") | — | — | — | — | SDK-GH |
| Code Composer Studio 21.0.1 | an optional IDE and debugger | Windows 11 and 10, 64-bit | macOS 26, 15 and 14 | Ubuntu 26.04, 24.04 and 22.04 (it "will run on other distributions", untested there); after installing, `install_drivers.sh` with `sudo` for the debug probes ("DO NOT SKIP THIS STEP") | "There is no license fee associated with CCStudio IDE" | CCS-GUIDE § 2.1, § 2.3, § 3.1.4, § 3.2.3.7; TI-CCS |
| UniFlash 9.7.0 | writes a firmware image to a board, with no IDE | installer | installer | installer | "available free of charge" | TI-UNIFLASH; PSIP p68 |

**The SDK's settings are the certification.** "The default settings in the
SDK must be used when selecting the CC1312PSIP. ... If the settings are
changed then the FCC Certification is not valid" (SWRA797 p6). INFERRED:
the build guides keep SysConfig's PHY settings for Mode #5 untouched, and a
firmware change review checks them.

**Every tool runs offline.** CCS ships "single file (offline)" installers
(TI-CCS), and UniFlash and SysConfig ship desktop installers. The SDK is a
public Git repository (SDK-GH).

**Declined:**

- **TI's cloud versions** of CCS, UniFlash and SysConfig (TI-CCS,
  TI-UNIFLASH, TI-SYSCONFIG): the anti-goals rule out a single vendor's
  cloud.
- **SmartRF Studio:** "a Windows application" (PSIP p67). Nothing in the
  beginner's path needs it. TI uses it for test-house work (SWRA797 p16).
- **IAR Embedded Workbench:** "A 30-day evaluation or a 32 KB size-limited
  version" (PSIP p67), so a paid tool beyond it.

**Not read:** the TI download pages' login terms, and the GCC route's
platforms. INFERRED: the GCC route matters only if TI's free compiler is
ever withdrawn.

## The debugger

| Item | Price on TI.com, read 2026-10-27 | What it is | Source |
|---|---|---|---|
| LP-XDS110 | 35.09 | "a tool to program and debug" TI devices; a "10-pin XDS110 out connector" for boards other than a LaunchPad; USB-C to the host; "UART serial port" | TI-XDS110 |
| LP-XDS110ET | 59.29 | the same, with "added circuitry for power measurements to support EnergyTrace™ software" | TI-XDS110ET |
| LP-EM-CC1312PSIP | 47.19 | the module's development board: "a split LaunchPad development kit which means that it does not contain an XDS debugger" | TI-LPEM |

The prices are the pages' `data-price` values. INFERRED: they are US
dollars, since the pages' exchange-rate table is quoted against the dollar.
Story 2.1.6 prices the rest and dates every price again.

**Recommended: the LP-XDS110ET**, one per builder. INFERRED: the
requirements test battery life by "the tag's measured average current
against its cell's rated capacity" (`requirements.md` § The five measures,
defined), and the ET variant measures current. That makes the battery
figures below measurable by the first builder, for 24.20 more than the
plain debugger. It connects to the rebuild's boards through its 10-pin
cable. The module debugs over cJTAG (PSIP p50).

**The development board** is a bench tool for a builder's first firmware,
before their own boards exist. It is never part of a device:

- **What it needs.** "To evaluate the CC1312PSIP, there is a LaunchPad
  available LP-EM-CC1312PSIP and this must be plugged into a LP-XDS110 or
  LP-XDS110ET" (SWRA797 p4). The "LP-CC1312PSIP Development Kit" that the
  datasheet's first page names (PSIP p1), and that `radio-and-location.md`
  quotes, is sold as this board plus a separate debugger.
- **TI's terms bind both, as evaluation items** (TI-LPEM; TI-XDS110):
  - "EVMs are intended solely for product or software developers for use in
    a research and development setting ... EVMs shall not be directly or
    indirectly assembled as a part or subassembly in any finished product"
    (SSZZ027 §1.1);
  - "EVMs are intended solely for use by technically qualified,
    professional electronics experts" (SSZZ027 §4.3.2).
- **Its FCC status is not read.** The terms carry two US notices, one "for
  EVMs not FCC-Approved" and one for EVMs "annotated as FCC ... Part 15
  Compliant" (SSZZ027 §3.1.1, §3.1.2). Which one this board carries is
  printed in its quick start guide, which no public page this leg reached
  holds. Its EU declaration names the RoHS directives only (TI-LPEM-DOC).
- **The law it is sold under.** The FCC lets an evaluation kit be sold to
  "product developers, software developers, and system integrators", with
  a notice that ends: "Unless the assembled kit is designed to operate under
  part 15, part 18 or part 95 of this chapter, the operator of the kit must
  operate under the authority of an FCC license holder" (§2.803(d)(2)(iv)).
  INFERRED: the board is designed to operate under Part 15, so a builder
  needs no licence to run it on the bench.

**For the sitting:** TI's terms address professionals, and the rebuild's
builders are beginners. INFERRED: the guides say so plainly, and the
debugger's use stays on the bench. A debugger outside TI's terms was not
searched.

## The licences of the reference designs read

| Design | Read | Its terms | What the rebuild does |
|---|---|---|---|
| LP-EM-CC1312PSIP design files, `SWRR186.ZIP` (sha256 `a84bfbf54548…`) | The archive's file list and its schematic's text, to look for a licence. It holds Allegro and OrCAD sources, Gerbers, a BOM and PDFs, and no licence file | The board's page: "TI's Standard Terms and Conditions for Evaluation Items apply" (TI-LPEM). TI's documents carry: "TI grants you permission to use these resources only for development of an application that uses the TI products described in the resource. Other reproduction and display of these resources is prohibited" (SWRA797 p18) | Nothing is copied. TI's terms are not on the licence lane's allowlist, and "Translation into another language or another tool is still a copy" (`THIRD_PARTY_NOTICES.md`). The boards are drawn fresh in KiCad, following SWRA797 §3's written layout rules |
| SWRA797's compact 2-layer design (Figures 3-5 to 3-8) | The figures and their text | The same TI notice (SWRA797 p18) | The text's rules are followed. The figures' geometry is not copied |
| SimpleLink Low Power F2 SDK | Its licence file and README | TI's limited licence: modification and redistribution of source are permitted, but "Any redistribution and use of the source code, including any resulting derivative works, are licensed by TI for use only with TI Devices", and binaries keep TI's notices (SDK-LIC) | Never copied into this repo; each builder fetches it. INFERRED: a published firmware image would carry TI's object code under those terms. The firmware initiative brings that to the ASK-OMID register before any image is published |

**What following written rules means for the tag's antenna.** INFERRED:
following a layout rule, such as "1.5mm_max distance must be maintained
from each GND pad to a GND via" (SWRA797 p11), is not a copy. Redrawing
TI's board or its printed antenna would be one.
- So the LP-EM-CC1312PSIP's printed antenna (+2.69 dBi, SWRA797 Table 3-2)
  is declined, since it exists only as TI's geometry.
- The chip antenna takes its land pattern from its maker's datasheet, which
  the build initiative reads. This leg did not.

**KiCad's libraries** are recorded once, in `THIRD_PARTY_NOTICES.md` §
Standing notes.

## The battery: the requirements' ASSUMED figures replaced (D1)

`requirements.md` § The arithmetic marked its cell, reply and listening
figures ASSUMED, for this story to replace. `radio-and-location.md` left the
tag antenna, the slot guard and the receive start-up to it as well.

| Input | Was | Now | Source |
|---|---|---|---|
| The cell, rated | CR2032 "about 220–240 mAh"; CR2450 "about 600 mAh" | CR2032 235 mAh and CR2450 620 mAh, each to 2.0 V (rated at 15 kΩ and 7.5 kΩ); self-discharge "~1% / year" | Energizer CR2032 (Form No. 2032NA0618); Energizer CR2450 (Form No. 2450NA0521) |
| The cell, usable | 150 mAh, ASSUMED | **Still ASSUMED: 150 mAh** (64 % of 235), on a branded cell with a buffer capacitor. The floor beside it is 117.5 mAh (50 %) | "Some vendors, including some branded name vendors, only achieve app. 50% of rated capacity for both 15mA and 30mA peak current" (SWRA349 p4); a capacitor gives "a solid >40% improvement" on such cells (SWRA349 p7). No datasheet read states a usable figure, so the planning figure stays an assumption, now bounded |
| One reply | 25 mA for 3 ms, about 0.02 µAh | **18.09 µC (0.0050 µAh):** a start-up of 2.795 µC, then 0.533 ms at 28.7 mA | Start-up below; 28.7 mA at +14 dBm (PSIP p14), an upper bound for the 12.0 dBm the tag sends; the 0.533 ms airtime from `radio-and-location.md` (INFERRED there) |
| A wake's start-up | not stated (`radio-and-location.md`: the datasheet's start-up energies) | **2.795 µC:** 165 µs + 200 µs at 2.89 mA, then 0.3 ms at 5.8 mA | 165 µs standby to active and 200 µs crystal start-up (PSIP p23); 2.89 mA (PSIP p13). **ASSUMED:** the 0.3 ms for the radio's set-up and synthesizer calibration, which no datasheet read states. INFERRED: the two times are added, not overlapped |
| Listening | about 8 µA "so that it answers within about a second" | **13.9 µA** at a listen every 1 s; **7.3 µA** every 2 s; **1.8 µA** every 12 s | Each listen is the start-up plus a window at 5.8 mA (PSIP p14). The window is the 1.8 ms of `radio-and-location.md` (ASSUMED there), widened by 2 × 60 ppm of the interval for clock drift: 0.12 ms at 1 s, 1.44 ms at 12 s (INFERRED: the tag's RTC ±50 ppm, PSIP p24, plus the Reader's ±10 ppm, PSIP p23) |
| Standby | 0.9 µA (`radio-and-location.md`) | **1.0 µA**, since a tag keeps its slots on the 32 kHz crystal | PSIP p13 |
| Tag antenna | −5 dBi, ASSUMED | **Still −5 dBi:** the certified chip antenna's +1.0 dBi, less 6 dB ASSUMED for a small ground plane and the body | +1.0 dBi (SWRA797 Table 3-2). "GND planes that are less than this distance [8.2 cm] have a reduced efficiency and bandwidth" (SWRA797 p9) |
| Reader antenna | +2 dBi, ASSUMED | **+0.90 dBi**, the W5017 | SWRA797 Table 3-2 |
| Slot guard | 0.2 ms of guard and turnaround, ASSUMED | **The clock part is 15 µs per 0.25 s frame**: 60 ppm × 0.25 s (INFERRED, from the two accuracies above). The radio's receive-to-transmit turnaround is in no datasheet read, so the 0.2 ms stays ASSUMED as the whole guard | PSIP p23, p24 |

`requirements.md` carries a dated pointer line beside each replaced figure
(plan D1). Its numbers, which Omid set or accepted, are unchanged.

**The link at 30 m, with both antennas counted** (INFERRED, the same
free-space loss of 61.2 dB at 915 MHz and [2]'s 10–20 dB body loss as
`radio-and-location.md`):
- **up:** 12.0 dBm − 5 dBi + 0.90 dBi gives −63.3 to −73.3 dBm received.
  That is 30.7 to 20.7 dB above the −94 dBm sensitivity.
- **down:** 17 dBm + 0.90 dBi − 5 dBi gives −58.3 to −68.3 dBm, which is
  35.7 to 25.7 dB above it.
- **the EU's worst case,** at 868 MHz with 20 dB of body loss: 21.1 dB up
  and 24.3 dB down.

`radio-and-location.md` counted only the sending antenna, so its 21.8 dB at
the worst body loss becomes 20.7 dB here. [2] stays met on paper, with the
crowded hall still the unmeasured risk.

### Each worn tag's year

INFERRED, from the figures above (mAh a year; the listen interval in
brackets):

| Tag | Listening | Check-ins | Sensor | Standby | Total | Years from 150 mAh | From the 117.5 mAh floor |
|---|---|---|---|---|---|---|---|
| Conference badge (every 2 s), a check-in a minute ([9]) | 64.1 | 2.6 | — | 8.8 | 75.5 | 1.99 | 1.56 |
| Nursery tag (every 1 s), a check-in and a reading every 20 s ([18]) | 122.0 | 7.9 | 2.2 | 8.8 | 140.9 | 1.06 | 0.83 |

- **The conference listens every 2 s.** Its tightest latency is [8]'s
  15 s pairing alarm. Two hearings 2 s apart, a slot, the Reader and the
  host take about 5 s (INFERRED: 2 × 2 + 0.25 + 0.25 + 0.5). [9]'s year
  then holds with margin.
- **The nursery must listen every 1 s.** [13]'s 5 s takes 3.0 s at a 1 s
  listen (`radio-and-location.md` § Zones). At 2 s it would take 5.0 s, with
  no margin. So [18]'s year holds on paper by 6 %, at the planning
  capacity, and fails at the floor. The study does not lower the number
  (`requirements.md` § Ground rules). It names three routes for the
  sitting:
  - measure first: the LP-XDS110ET reads the tag's current, and the
    ASSUMED 0.3 ms set-up and 1.8 ms window are the inputs to measure;
  - a CR2450 in the nursery's band housing, if the housing allows: about
    2.8 years (INFERRED: 396 ÷ 140.9);
  - `radio-and-location.md`'s fallback, in which the Field Generators
    listen and the tags only transmit.

  The study recommends measuring first, then the larger cell if the
  measurement falls short.
- **The +14 dBm boost mode needs 2.1 V** (PSIP p12), and the cell is rated
  to 2.0 V. The tag sends 12.0 dBm. Whether TI's 12 dBm setting uses boost
  mode is not read. If it does, the cell's last tenth of a volt is lost to
  replies. The build measures it.

### [27]: the warehouse item tag (D4)

[27] asks for five years, with one inventory a day, a check-in every
15 min and a temperature reading every 2 min on cold-chain tags. INFERRED,
over five years, including the cell's ~1 % a year of self-discharge (mAh;
the usable capacity in brackets: 150 for a CR2032, and 396 for a CR2450 at
the same 64 %):

| Listen every | CR2032: needs (150) | CR2032, cold chain | CR2450: needs (396) | CR2450, cold chain |
|---|---|---|---|---|
| 1 s | 666.6: 0.23 of five years | 673.9 | 685.9: 0.58 | 693.2 |
| 4 s | 231.8: 0.65 | 239.2 | 251.1: 1.58 | 258.4 |
| 8 s | 159.4: 0.94 | 166.7 | 178.6: 2.22 | 186.0 |
| 12 s | **135.2: 1.11** | **142.6: 1.05** | 154.5: 2.56 | 161.8: 2.45 |
| 16 s | 123.2: 1.22 | 130.5: 1.15 | 142.4: 2.78 | 149.7: 2.64 |

- **A CR2450 alone does not keep five years.** At a 1 s listen it lasts
  about 2.9 years (0.58 × 5). Its rated 620 mAh is less than the 686 mAh
  that five years of listening every second take.
- **A slower wake alone does.** A CR2032 keeps five years from a listen
  every 9.1 s, or every 10.3 s for cold-chain tags. The pairing alarm at a
  dock door ([26], 30 s) bounds the interval from above. Two hearings plus
  1 s for the slot, the Reader and the host take 2T + 1 s, so T may be
  14.5 s at most. A 12 s listen takes 25 s.
  - Tamper ([23], 30 s) and temperature ([24], 5 min) start at the tag, so
    the listen interval does not bound them. INFERRED: a tag woken by its
    tamper input or a reading over its limit listens until the next beacon,
    which is at most 0.25 s away, and reports in that frame.
  - A missed check-in ([25], 1 h) and a zone move ([22], 5 min) are far
    looser.

**The choice: the slower wake, a listen every 12 s.** It keeps five years
on a CR2032 on paper:
- **its cost:** a margin of 11 %, or 5 % on cold-chain tags, at the planning
  capacity; a zone change shown in up to about 25 s; and [26] met with 5 s
  to spare;
- **the CR2450's cost, beside it:** 4.5 mm wider and 1.8 mm thicker, for
  2.64 times the rated capacity (Energizer).

**Recommended on top of the choice: the CR2450 as well, wherever the item
takes the larger tag.** With the slower wake it gives about 2.5 times the
need: 2.56, or 2.45 on cold chain. INFERRED: that margin counts at 100,000
tags. [27]'s own reasoning is the battery-change workload, and a thin margin
turns a poor cell or a cold warehouse into early changes.

The slower wake keeps five years, so D4's condition for putting [27] to
Omid ("only if neither choice keeps five years") is not met, and no
ASK-OMID row is filed. The sitting decides between the two cells.

## One core or three (spec §3.7), with the sealTag inside it (D3)

Spec §3.7 asks whether the seal, the fob and the card share one electronics
core in three enclosures, or take three boards. It calls that a suggestion
to weigh, not a decision.

**The sealTag is a new design.** The 2012 sealTag is an ISO 15693 logging
seal with no active radio. It logs a tamper for an HF reader to collect
later (`original-system.md` § The sealTag). Milestone 1's sealTag must send
its tamper alarm to the host within [14]'s 5 s.
- **The new seal keeps the old one's form:** a band, or a seal, whose loop
  is wired to a pin.
- **It replaces the electronics** with the tag core above. The loop's
  opening wakes the module on a pin edge (PSIP p51), and the module reports
  in the next frame.
- **Its timing.** INFERRED: at most 0.25 s to the next beacon, a slot in
  that frame, 0.25 s from the Reader to the host and 0.5 s to draw, so
  about 1.5 s against [14]'s 5 s.
- **The 2012 seal's two-chip board is not ported:** an STM8L
  microcontroller and an HF memory.

**For one core:**

- **One radio layout.** The module's certification rests on the layout as
  well as the settings: ground vias at 1.5 mm at most, tracks of 8 mm at
  most outside the shielded layers, ground under the module (SWRA797
  §3.1.3, §3.1.4), and a certified antenna type at 2.69 dBi at most
  (SWRA797 §3.2.2). INFERRED: one core is one layout to get right, test for
  range and check for emissions, where three boards are three.
- **One firmware image,** with the tag style as a setting. INFERRED.
- **One board in larger quantity** for the assembly service (Story 2.1.6
  prices it). INFERRED.
- **One set of certification conditions:** the same antenna, power and PHY
  in every tag. INFERRED.
- **One board for the beginner to learn,** and to debug with one fixture.
  INFERRED.

**Against one core:**

- **The card's larger ground plane goes unused.** A ground plane shorter
  than a quarter wave, 8.2 cm at 915 MHz, loses efficiency and bandwidth
  (SWRA797 p9). A card is wide enough to approach it. A core sized for the
  fob is not. INFERRED: a card built on the small core gives up range that
  a card-sized board would have.
- **The tag styles differ in their options.** The seal needs the loop, the
  nursery and cold-chain tags the sensor, and the original's badges carried
  panic, tilt, a speaker and LEDs (`original-system.md` § The cardTag and
  the compactTag). INFERRED: one core carries them as fitting options, so
  every board pays for the footprints.
- **The item tag's CR2450** does not fit the CR2032 holder. INFERRED: its
  core is a variant of the same schematic, with a larger holder.

**The recommendation: one core.** One schematic and one layout, sized for
the fob (the smallest enclosure that takes a CR2032), with the sensor, the
tamper loop and the badge's buttons as fitting options. It goes into the
seal, the fob and the card. The card accepts the small core's range cost,
which the build's range test ([2]) measures. The warehouse item tag is a
later variant of the same schematic with a CR2450 holder.

INFERRED: the single radio layout outweighs the card's lost ground plane.
The layout is where a beginner's board most often fails, and a certified
module's conditions bind it (SWRA797 §3.1). The decision sitting (Story
2.1.7) decides. No board is drawn in this study (plan D3).

**Whether the seal keeps an HF interface** for a phone to read is Story
2.1.5's and the sitting's (plan D5). INFERRED: an HF memory would be one
more fitting option on the same core. It needs no transmitter of its own
(`radio-and-location.md` § The seal's HF interface).

## What the choice leaves to later stories

- **Before any part is bought:** the FCC grant pages for ZAT-1312PSIP-2
  and -3, read in a browser (§ What was read).
- **Story 2.1.5:** the host software, which reads the Reader's USB CDC port
  on all three platforms.
- **Story 2.1.6:** the prices of every part named here, the assembly routes
  for the module's 0.5 mm-pitch land grid (`radio-and-location.md`), and
  the tool tiers, the vector network analyser among them.
- **Story 2.1.7:** the sitting decides:
  - one core or three;
  - the item tag's cell;
  - the nursery tag's cell, if the measurement falls short;
  - the sensor's accuracy, if it wants one.
- **The build initiatives:**
  - the USB bridge part, and the chip antenna's land pattern, each from its
    maker's datasheet;
  - the boards in KiCad 10;
  - the first battery measurements on the LP-XDS110ET, which replace this
    document's two remaining ASSUMED inputs, the 0.3 ms set-up and the
    1.8 ms window.

## Sources

**Law texts:** the table in § What was read. Each is cited at the version
it prints, with its URL; none is pinned (plan D8).

**Pinned in the reference corpus** (`docs/reference-corpus.tsv`, folder
`sources/`, licence `third-party`, study only; the choices rest on them):

| Short name | Document | Pin (sha256, first 12) | URL |
|---|---|---|---|
| PSIP | TI, "CC1312PSIP SimpleLink Sub-1-GHz Wireless System-in-Package", SWRS293, November 2023 (pinned by Story 2.1.3) | `da5eb47438cf` | `https://www.ti.com/lit/ds/symlink/cc1312psip.pdf` |
| SWRA797 | TI, "Design Considerations for CC1312PSIP FCC and IC Certified RF Module", SWRA797, September 2024 (pinned by Story 2.1.3) | `2fbe59c4ae3f` | `https://www.ti.com/lit/an/swra797/swra797.pdf` |
| Energizer CR2032 | Energizer, "CR2032" product datasheet, Form No. 2032NA0618 | `ff05548e1efc` | `https://data.energizer.com/pdfs/cr2032.pdf` |
| Energizer CR2450 | Energizer, "CR2450" product datasheet, Form No. 2450NA0521 | `537429858e7a` | `https://data.energizer.com/pdfs/cr2450.pdf` |
| SWRA349 | TI, "Coin cells and peak current draw", white paper SWRA349, 2010-09-30 | `adbc6c86ed9f` | `https://www.ti.com/lit/wp/swra349/swra349.pdf` |
| TMP117 | TI, "TMP117 High-Accuracy, Low-Power, Digital Temperature Sensor", SNOSD82D, June 2018, revised September 2022 | `78212e91e478` | `https://www.ti.com/lit/ds/symlink/tmp117.pdf` |
| SSZZ027 | TI, "Standard Terms for Evaluation Modules", © 2023 | `26d56134c6f8` | `https://www.ti.com/lit/pdf/sszz027` |

**Cited by URL and fetch date** (fetched 2026-10-27; no choice rests on
them alone):

- **TI-LPEM:** `https://www.ti.com/tool/LP-EM-CC1312PSIP`, and its design
  files `https://www.ti.com/lit/zip/swrr186` (sha256
  `a84bfbf545488eb5e56778fe371f83700140fcbc569efbba8fcd338157aa3bf6`, not
  pinned: no choice rests on their contents).
- **TI-LPEM-DOC:** "LP-EM-CC1312PSIP EU Declaration of Conformity",
  `https://www.ti.com/lit/pdf/SSZQRR7`.
- **TI-XDS110:** `https://www.ti.com/tool/LP-XDS110`.
- **TI-XDS110ET:** `https://www.ti.com/tool/LP-XDS110ET`.
- **TI-CCS:** `https://www.ti.com/tool/CCSTUDIO` (version 21.0.1, released
  Aug 24, 2026).
- **CCS-GUIDE:** the CCStudio IDE 21.0.1 user's guide,
  `https://software-dl.ti.com/ccs/esd/documents/users_guide_ccs_21.0.1/ccs_overview.html`
  and `.../ccs_installation.html`.
- **TI-UNIFLASH:** `https://www.ti.com/tool/UNIFLASH` (version 9.7.0,
  released Oct 5, 2026).
- **TI-SYSCONFIG:** `https://www.ti.com/tool/SYSCONFIG` (version
  1.28.2.4845, released Oct 20, 2026).
- **TI-CGT:** `https://www.ti.com/tool/ARM-CGT` (TI Arm Clang 5.1.1.LTS,
  released May 18, 2026).
- **SDK-GH:** `https://github.com/TexasInstruments/simplelink-lowpower-f2-sdk`,
  its README at tag `lpf2-8.33.00.16`.
- **SDK-LIC:** that repository's
  `license_simplelink_cc13xx_cc26xx_sdk_8_33_00_16.txt`.
- **KICAD-DL:** `https://www.kicad.org/download/windows/`, `.../macos/` and
  `.../linux/` (current version 10.0.7).
- **KICAD-LIC:** `https://www.kicad.org/about/licenses/`.
- **MCHP-CDC:** Microchip, "AVR® DU as a USB CDC Communication Device",
  § 2.3, "Advantages of Using CDC for Serial Communication",
  `https://onlinedocs.microchip.com/oxy/GUID-38BB0804-1305-4C34-99DC-04025E667766-en-US-5/GUID-2B9FD466-ED6A-4DC5-B368-0056FC94FDE8.html`.

Page numbers are each PDF's own page count (page 1 is the cover page).
