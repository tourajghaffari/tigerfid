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
  ZAT-1312PSIP-2", from the datasheet, or -3 if the grants' read finds that
  one covering the PHY (Story 2.1.10). The design hops channels, which holds
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
| Power | USB's 5 V, through a 3.3 V regulator | INFERRED: the regulator is rated above the 86 mA the module draws at +20 dBm (PSIP p14), and USB powers the Reader and carries its data on one cable. This fills a gap no original record states, the Reader's supply (`original-system.md` § What no record states). Its current is the module's (62 mA at +19 dBm, PSIP p14; the Reader sends 17 dBm) plus the bridge's and the relays', which their parts' datasheets will state |
| I/O | two relays and two supervised inputs, with their drivers and protection | The original's I/O, kept (`radio-and-location.md` § Does ActiveWave's approach still win). The parts are the build initiative's |
| Debug | a 10-pin connector, to take the debugger's "10-pin XDS110 out connector" | § The debugger |

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
| Temperature sensor | TI TMP117, on the nursery's tags and the cold-chain item tags; a fitting option on the others | ±0.1 °C from −20 to 50 °C, specified at "8 averages, 1-Hz conversion cycle"; I²C; 1.7–5.5 V; 135 µA during a conversion; 0.15 µA in shutdown; a 2 mm × 2 mm package (TMP117 p1, p6). One conversion takes 15.5 ms, and eight averages take 124 ms, which is also the factory setting (TMP117 p12). The battery is charged at eight averages (§ Each worn tag's year). The module's own sensor reads to ±3.5 °C at 2 °C resolution (PSIP p32). INFERRED: that is too coarse for a cold-chain limit a few degrees wide, and it measures the chip, not the item |
| Tamper input | the band's or seal's loop, wired to a pin that wakes the module on either edge | Wake on a pin edge from standby (PSIP p51). The 2012 seal woke on its band's input the same way (`original-system.md` § The sealTag) |
| Debug | pads for the debugger's 10-pin connector, through an adapter or a pogo-pin fixture | INFERRED: a 10-pin connector does not fit a fob. The 2012 seal carried pogo pins (`original-system.md` § The sealTag) |

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

**Platforms measured: none. Platforms cited: Windows, macOS and Linux,**
for every tool except three: the Arm GNU Toolchain, the SDK and GNU make.
The Arm GNU Toolchain's platforms were not read: its page renders by script
(§ What was read), and it answered Story 2.1.10's request with HTTP 403.
The SDK is a public Git repository of source (SDK-GH), which the tools below
build on each platform. GNU make's are not cited either: GNU releases its
source only, so its row's platforms are INFERRED from CCS, which ships it.
GNU's own make page reset the connection when Story 2.1.10 asked
(2026-10-28) and when Story 2.1.12 asked again (2026-10-29; § 4 of
`docs/reference-research.md`). CMake, GNU
make, SysConfig and TI Arm Clang
were read again on 2026-10-28, between Story 2.1.10's clock reads of
11:33:59Z and 11:36:39Z: CMake's and GNU make's rows were empty before, and
SysConfig's and TI Arm Clang's licences were not stated.

| Tool | What it is for | Windows | macOS | Linux | Licence and price | Source |
|---|---|---|---|---|---|---|
| KiCad 10.0.7 | every board: the Reader, the Field Generator, the tag core | Windows 10 and 11 | macOS 12 and newer | an AppImage for x86-64 | GPL-3.0-or-later; no price. Its libraries' licence is in `THIRD_PARTY_NOTICES.md` | KICAD-DL; KICAD-LIC |
| SimpleLink Low Power F2 SDK 8.33.00.16 | the module's drivers, its RF driver and the certified PHY settings | — | — | — | TI's limited licence, "royalty-free" and for "use only with TI Devices"; no price (§ The licences) | SDK-GH; SDK-LIC |
| SysConfig 1.28.2 | the module's configuration, and the SDK's default PHY settings that the certification requires | installer | installer | installer | **not read:** the page offers its installers with no price and no licence text (read 2026-10-28). The licence is in the installer, which no session ran. CCS includes SysConfig (SDK-GH) | TI-SYSCONFIG |
| TI Arm Clang 5.1.1.LTS | the compiler | x64 installer | x64 and arm64 installers; the x86 build is "known to work with MacOS version 13.6" | x64 and arm64 installers | **not read:** the page offers its installers with no price and no licence text (read 2026-10-28). The licence is in the installer, which no session ran | TI-CGT |
| Arm GNU Toolchain | the open compiler the SDK also builds with ("ARM Gnu Toolchain (GCC_ARMCOMPILER)") | not read | not read | not read | not read | SDK-GH. Arm's page renders by script and was not read |
| CMake 4.4.5 | a build tool the SDK names among its dependencies | x64 and ARM64 installers (`.msi`) | a universal disk image, for macOS 12 or later | x86-64 and aarch64 installers (`.sh`) | "distributed under the OSI-approved BSD 3-clause License"; no price | CMAKE-DL; CMAKE-LIC; SDK-GH |
| GNU make 4.4.1 | the SDK's build ("run GNU `make`") | GNU releases source only. CCS ships it as `gmake` in `ccs/utils/bin/` (SDK-GH); INFERRED, on each platform CCS installs on | the same | the same | GPL-3.0-or-later, read from its own licence text: "either version 3 of the License, or (at your option) any later version"; no price | GNU-MAKE; SDK-GH |
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

**Not read:** the TI download pages' login terms, SysConfig's and TI Arm
Clang's licences, the GCC route's platforms, and a page that states GNU
make's (its row's are INFERRED). INFERRED: TI offers SysConfig and TI Arm
Clang for download with no price, and the SDK names the GCC route beside TI's
compiler (SDK-GH), so the build does not rest on TI's compiler alone. The
epic close's `free-tools-three-platforms` check (plan Story 2.1.8) reads
these two licences as unread.

## The debugger

| Item | Price on TI.com, read 2026-10-27 | What it is | Source |
|---|---|---|---|
| LP-XDS110 | 35.09 | "a tool to program and debug" TI devices; a "10-pin XDS110 out connector" for boards other than a LaunchPad; USB-C to the host; "UART serial port" | TI-XDS110 |
| LP-XDS110ET | 59.29 | the same, with "added circuitry for power measurements to support EnergyTrace™ software" | TI-XDS110ET |
| LP-EM-CC1312PSIP | 47.19 | the module's development board: "a split LaunchPad development kit which means that it does not contain an XDS debugger" | TI-LPEM |

The prices are the pages' `data-price` values. INFERRED: they are US
dollars, since the pages' exchange-rate table is quoted against the dollar.
Story 2.1.6 prices the rest and dates every price again. It read these
three again on 2026-10-28 at the same prices (`tiered-cost.md`).

**Recommended: the LP-XDS110ET**, one per builder. INFERRED: the
requirements test battery life by "the tag's measured average current
against its cell's rated capacity" (`requirements.md` § The five measures,
defined), and the ET variant measures current. That makes the battery
figures below measurable by the first builder, for 24.20 more than the
plain debugger. It reaches the rebuild's boards through its "10-pin XDS110
out connector" (TI-XDS110ET). The module debugs over cJTAG (PSIP p50).
Whether a cable for that connector comes in the box was not read.

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
  operate under the authority of an FCC license holder or must secure an
  experimental authorization under part 5 of this chapter"
  (§2.803(d)(2)(iv); its last clause re-read by Story 2.1.10 at the same
  point in time, 2026-10-23).
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
| Listening | about 8 µA "so that it answers within about a second" | **8.13 µA** at a listen every 1 s; **4.41 µA** every 2 s; **1.32 µA** every 12 s, each for two slots in one run. **Each more slot in the run adds 2.3 µA at a 1 s listen, and each more run 2.8 µA.** A check-in adds one wake for the Reader's beacon, 8.77 µC (Story 2.1.12). Stories 2.1.4 and 2.1.10 charged the beacon at every listen: 13.9, 7.3 and 1.8 µA | Each listen is a start-up for each run of the tag's set in the Field Generators' block, then a window at 5.8 mA (PSIP p14) of 0.4 ms for each slot heard or listened across (ASSUMED in `radio-and-location.md` § Zones; the runs in its § The layout of a listen). Most listens leave out the Reader's beacon, 1.0 ms (ASSUMED there), which the tag hears after each check-in. The window is widened by 2 × 60 ppm of the time since the tag last heard a slot, for clock drift: 0.12 ms at 1 s, 1.44 ms at 12 s, and 0.03 ms for the beacon after a check-in (INFERRED: the tag's RTC ±50 ppm, PSIP p24, plus the Reader's ±10 ppm, PSIP p23) |
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

`radio-and-location.md` counted the Reader's antenna as +2 dBi, ASSUMED. The
W5017's +0.90 dBi therefore takes its 21.8 dB at the worst body loss to
20.7 dB here. [2] stays met on paper, with the crowded hall still the
unmeasured risk.

### Each worn tag's year

INFERRED, from the figures above (mAh a year; the listen interval and the
zone's neighbours in brackets). Re-priced by Story 2.1.10 for the Sprint 2
review's findings 3 and 4:
- **the sensor at eight averages,** the setting its ±0.1 °C is specified
  at (TMP117 p6). A reading is 124 ms at 135 µA (TMP117 p12), 16.74 µC, so
  one every 20 s takes 7.3 mAh a year. Shutdown adds 0.15 µA, 1.3 mAh. The
  study had charged one 15.5 ms conversion, 2.2 mAh in all;
- **the listen set,** a tag's zone and its neighbours
  (`radio-and-location.md` § Zones), with the neighbours that section
  assumes for each scenario.

Re-priced again by Story 2.1.12, for the Sprint 3 review's finding 1: a tag
hears the Reader's beacon after each check-in, not at every listen, and
wakes once for each run of its set in its scenario's block order
(`radio-and-location.md` § Zones, The layout of a listen). Each row is the
costliest zone of its kind in that order. The check-ins column holds each
reply and the beacon after it.

| Tag | Listening | Check-ins | Sensor | Standby | Total | Years from 150 mAh | From the 117.5 mAh floor |
|---|---|---|---|---|---|---|---|
| Conference badge in a room (every 2 s; 2 neighbours, three wakes at most), a check-in a minute ([9]) | 73.3 | 3.9 | — | 8.8 | 86.0 | 1.74 | 1.37 |
| Conference badge in an area of the hall (every 2 s; 8 neighbours, one run of nine) | 109.8 | 3.9 | — | 8.8 | 122.5 | 1.22 | 0.96 |
| Nursery tag in a room (every 1 s; 1 neighbour, one run of three, one room away from its stretch), a check-in and a reading every 20 s ([18]) | 91.5 | 11.8 | 8.6 | 8.8 | 120.7 | 1.24 | 0.97 |
| Nursery tag in a stretch of the corridor (every 1 s; 6 neighbours, three runs of seven) | 221.8 | 11.8 | 8.6 | 8.8 | 251.0 | 0.60 | 0.47 |

Each total is summed before rounding. A room's tag beside its stretch in the
block listens for 71.2 mAh a year: 100.4 mAh in all, 1.49 years. Before
Story 2.1.12, with the beacon at every listen, the four rows read 85.6,
146.6, 147.4 and 249.0 mAh (1.75, 1.02, 1.02 and 0.60 years). Before Story
2.1.10 the table had one row for each tag: the badge at 75.5 mAh, 1.99
years, and the nursery tag at 140.9 mAh, 1.06 years.

- **The conference listens every 2 s.** Its tightest latency is [8]'s
  15 s pairing alarm. Two hearings 2 s apart, a slot, the Reader and the
  host take about 5 s (INFERRED: 2 × 2 + 0.25 + 0.25 + 0.5). [9]'s year
  holds on paper in every zone (plan Sprint 3's D2, re-run by Story 2.1.12):
  with margin in a room, and by 22 % in an area of the hall, which listens
  to nine slots (2.3 % before Story 2.1.12). At the floor the hall's badge
  falls short, 0.96 years. If the measurement falls short, a 3 s listen in
  the hall is the route: 1.71 years, and [8] in about 7 s (INFERRED:
  2 × 3 + 1). Its cost is fewer hearings of an exit's
  beacon: at least one in the 4 s a walker spends in an exit's field,
  against two at 2 s (`radio-and-location.md` § Zones).
- **The nursery must listen every 1 s.** [13]'s 5 s takes 3.0 s at a 1 s
  listen (`radio-and-location.md` § Zones). At 2 s it would take 5.0 s, with
  no margin. **[18] on paper** (plan Sprint 3's D2, re-run by Story 2.1.12
  under Sprint 4's D2):
  - a tag that stays in the rooms holds its year by 24 % at the planning
    capacity (1.8 % before Story 2.1.12), even one room away from its
    stretch in the block, and falls short at the floor, 0.97 years;
  - a stretch of the corridor costs 14.9 µA more than such a room, while
    the tag is in it. INFERRED: a tag holds [18] if it spends at most 1,969
    hours a year in the corridor's stretches, about 5.4 hours a day (227
    hours, about 37 minutes a day, before Story 2.1.12). A tag that walks
    the corridor 2 hours a day now lasts 1.14 years, and one that never
    leaves it 0.60 years: **[18] is unmet on paper for a tag in the corridor
    more than about 5.4 hours a day.**

  The study does not lower the number (`requirements.md` § Ground rules).
  The routes, each with its cost:
  - **measure first.** The LP-XDS110ET reads the tag's current, and the
    ASSUMED 0.3 ms set-up, 1.0 ms beacon and 0.4 ms slot are the inputs to
    measure. It costs a bench session, and its answer can go either way;
  - **a CR2450,** where the housing allows: 1.58 years for a tag that never
    leaves the corridor, and 3.01 years for one that walks it 2 hours a day
    (INFERRED: 396 ÷ 251.0 and 396 ÷ 131.6). Its cost is the larger cell
    (§ [27]): an adult's fob can take it, and a baby's band may not;
  - **shorter stretches:** each neighbour fewer saves 2.3 µA while a tag is
    in that stretch, at the cost of more Field Generators;
  - **one conversion per reading** in place of eight averages: it saves
    6.4 mAh a year, so a room's tag needs 114.3 mAh, 1.31 years. Its cost:
    the ±0.1 °C is specified at eight averages only (TMP117 p6). One
    conversion repeats to about ±3 LSB, ±0.023 °C, against ±1 LSB at eight
    (TMP117 p6, p12). The requirements set no accuracy, so the sitting
    chooses (§ The tags);
  - **`radio-and-location.md`'s fallback,** in which the Field Generators
    listen and the tags only transmit. No tag keeps a listen set, but every
    Field Generator listens in every frame and relays what it hears, which
    the radio study has not designed.

  **Recommended: measure first, then the CR2450 for any tag the measurement
  leaves short.** INFERRED: the tags that spend hours in the corridor are
  the adults', whose fobs take the larger cell. A route meets [18] without
  lowering it, so nothing goes to Omid ahead of the sitting (plan Sprint 3's
  D2). INFERRED as well: a baby's tag spends its stay in the rooms, and its
  passes along the corridor take far less than 5.4 hours a day. So it holds
  [18] on its CR2032 on paper, and Sprint 4's D2 clause for a baby's band
  does not fire: no tag that needs the larger cell is a baby's. The sitting
  decides the nursery tag's cell.
- **The +14 dBm boost mode needs 2.1 V** (PSIP p12), and the cell is rated
  to 2.0 V. The tag sends 12.0 dBm. Whether TI's 12 dBm setting uses boost
  mode is not read. If it does, the cell's last tenth of a volt is lost to
  replies. The build measures it.

### [27]: the warehouse item tag (D4)

[27] asks for five years, with one inventory a day, a check-in every
15 min and a temperature reading every 2 min on cold-chain tags. INFERRED,
over five years, including the cell's ~1 % a year of self-discharge (mAh;
the usable capacity in brackets: 150 for a CR2032, and 396 for a CR2450 at
the same 64 %). Re-priced by Story 2.1.10: an item at rest listens to its
bay's slot and its two neighbours' (`radio-and-location.md` § Zones), and
the cold-chain reading is taken at eight averages (§ Each worn tag's year).
Re-priced again by Story 2.1.12: the three slots are one run in the
warehouse's block order, and the tag hears the Reader's beacon after each
check-in and for the day's inventory, not at every listen
(`radio-and-location.md` § Zones, The layout of a listen). Those beacons
and the inventory's reply add 0.44 mAh over five years.

| Listen every | CR2032: needs (150) | CR2032, cold chain | CR2450: needs (396) | CR2450, cold chain |
|---|---|---|---|---|
| 1 s | 514.6: 0.29 of five years | 527.3: 0.28 | 533.9: 0.74 | 546.5: 0.72 |
| 4 s | 194.2: 0.77 | 206.9: 0.73 | 213.4: 1.85 | 226.1: 1.75 |
| 8 s | 140.8: 1.07 | 153.4: 0.98 | 160.0: 2.47 | 172.7: 2.29 |
| 12 s | **123.0: 1.22** | **135.6: 1.11** | 142.2: 2.78 | 154.9: 2.55 |
| 14 s | 117.9: 1.27 | 130.6: 1.15 | 137.1: 2.89 | 149.8: 2.64 |
| 16 s | 114.1: 1.32 | 126.7: 1.18 | 133.3: 2.97 | 146.0: 2.71 |

Before Story 2.1.12, the 12 s row read 143.7 (1.04) and 156.4 (0.96) on a
CR2032, and 162.9 (2.43) and 175.6 (2.25) on a CR2450. Before Story 2.1.10
it read 135.2 (1.11) and 142.6 (1.05) on a CR2032, and 154.5 (2.56) and
161.8 (2.45) on a CR2450.

- **A CR2450 alone does not keep five years.** At a 1 s listen it lasts
  about 3.7 years (0.74 × 5): five years at a listen every second take
  534 mAh, less than its rated 620 mAh but more than the 396 mAh this study
  plans on.
- **A slower wake alone does, for the plain and the cold-chain tag alike,
  well inside [26]'s bound.** A CR2032 keeps five years from a listen every
  6.8 s, or every 8.6 s for cold-chain tags (10.8 s and 13.5 s before Story
  2.1.12). The pairing alarm at a dock door
  ([26], 30 s) bounds the interval from above. Two hearings plus 1 s for
  the slot, the Reader and the host take 2T + 1 s, so T may be 14.5 s at
  most. A 12 s listen takes 25 s.
  - Tamper ([23], 30 s) and temperature ([24], 5 min) start at the tag, so
    the listen interval does not bound them. INFERRED: a tag woken by its
    tamper input or a reading over its limit listens until the next beacon,
    which is at most 0.25 s away, and reports in that frame.
  - A missed check-in ([25], 1 h) and a zone move ([22], 5 min) are far
    looser.

**The choice: the slower wake, a listen every 12 s,** unchanged by Stories
2.1.10 and 2.1.12. It keeps five years on a CR2032 on paper for both item
tags:
- **its cost:** a margin of 22 % at the planning capacity for the plain
  item tag, and 11 % for the cold-chain tag (4 % and 0.96 of five years
  before Story 2.1.12; 11 % for the plain tag before Story 2.1.10); a zone
  change shown in up to about 25 s; and [26] met with 5 s to spare;
- **the CR2450's cost, beside it:** 4.5 mm wider and 1.8 mm thicker, for
  2.64 times the rated capacity (Energizer).

**The cold-chain item tag now holds on a CR2032 at 12 s:** 1.11 of five
years, where it was 0.96 (plan Sprint 3's D2, re-run by Story 2.1.12 under
Sprint 4's D2). The routes this section named for it stay open, for margin:
- **measure first:** the same three ASSUMED inputs as the worn tags'. It
  costs a bench session, and its answer can go either way;
- **the CR2450:** 2.55 times the need at 12 s, for the larger cell;
- **one conversion per reading:** 130.3 mAh, 1.15 of five years at 12 s,
  at the cost of the accuracy the sensor is specified at (§ Each worn tag's
  year);
- **the fallback** in which the Field Generators listen (§ Each worn tag's
  year).

**Recommended on top of the choice: the CR2450 wherever the item takes the
larger tag, the cold-chain tags first.** Their 11 % is the thinner margin.
With the slower wake it gives 2.78 times the need, or 2.55 on cold chain.
INFERRED: that margin counts at 100,000 tags. [27]'s own reasoning is the
battery-change workload, and a thin margin turns a poor cell or a cold
warehouse into early changes.

Sprint 2's D4 put [27] to Omid only if neither choice kept five years, and
Sprint 3's D2 only if no route meets the number. The slower wake keeps five
years for both item tags on a CR2032 on paper, so no ASK-OMID row is filed
(Sprint 4's D2). The sitting decides the cells.

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
(`radio-and-location.md` § The seal's HF interface). Omid has since
decided that milestone 1 fits none, and that the core keeps an unfitted
footprint for one with the coil off the core (`software-stack.md`
§ Decided ahead of the sitting).

## What the choice leaves to later stories

- **Before any part is bought:** the FCC grant pages for ZAT-1312PSIP-2
  and -3, read in a browser (§ What was read).
- **Story 2.1.5:** the host software, which reads the Reader's USB CDC port
  on all three platforms.
- **Story 2.1.6:** the prices of every part named here, the assembly routes
  for the module's 0.5 mm-pitch land grid (`radio-and-location.md`), and
  the tool tiers, the vector network analyser among them. Priced in
  `tiered-cost.md` (2026-10-28), where the W5017's price is not read.
- **Story 2.1.7:** the sitting decides:
  - one core or three;
  - the item tag's cell: a CR2032 at 12 s keeps five years on paper, by
    22 %, or 11 % on cold chain, and the CR2450 is recommended for margin
    wherever the item takes it, the cold-chain tags first (§ [27]);
  - the nursery tag's cell, if the measurement falls short (§ Each worn
    tag's year);
  - the sensor's accuracy, if it wants one, and with it the setting: eight
    averages as priced, or one conversion (§ Each worn tag's year).
- **The build initiatives:**
  - the USB bridge part, and the chip antenna's land pattern, each from its
    maker's datasheet;
  - the boards in KiCad 10;
  - the first battery measurements on the LP-XDS110ET, which replace this
    document's remaining ASSUMED inputs: the 0.3 ms set-up, and the window's
    1.0 ms beacon and 0.4 ms slot (`radio-and-location.md` § Zones).

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
  1.28.2.4845, released Oct 20, 2026; read again 2026-10-28).
- **TI-CGT:** `https://www.ti.com/tool/ARM-CGT` (TI Arm Clang 5.1.1.LTS,
  released May 18, 2026; read again 2026-10-28).
- **SDK-GH:** `https://github.com/TexasInstruments/simplelink-lowpower-f2-sdk`,
  its README at tag `lpf2-8.33.00.16` (its § Resources read again
  2026-10-28).
- **CMAKE-DL:** `https://cmake.org/download/` (the latest release 4.4.5,
  read 2026-10-28).
- **CMAKE-LIC:** `https://cmake.org/licensing/` (read 2026-10-28).
- **GNU-MAKE:** GNU make 4.4.1, from the GNU mirror
  `https://mirrors.kernel.org/gnu/make/` (dated 26-Feb-2023 there), its
  `README` and `COPYING` read 2026-10-28. GNU's own page,
  `https://www.gnu.org/software/make/`, reset the connection twice.
- **SDK-LIC:** that repository's
  `license_simplelink_cc13xx_cc26xx_sdk_8_33_00_16.txt`.
- **KICAD-DL:** `https://www.kicad.org/download/windows/`, `.../macos/` and
  `.../linux/` (current version 10.0.7).
- **KICAD-LIC:** `https://www.kicad.org/about/licenses/`.
- **MCHP-CDC:** Microchip, "AVR® DU as a USB CDC Communication Device",
  § 2.3, "Advantages of Using CDC for Serial Communication",
  `https://onlinedocs.microchip.com/oxy/GUID-38BB0804-1305-4C34-99DC-04025E667766-en-US-5/GUID-2B9FD466-ED6A-4DC5-B368-0056FC94FDE8.html`.

Page numbers are each PDF's own page count (page 1 is the cover page).
