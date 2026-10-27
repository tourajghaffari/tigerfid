# Radio, location and legality

The third document of the design study (plan Story 2.1.3). It chooses the
radio and the location scheme for the rebuild:

- against the requirements (`docs/study/requirements.md`), the hardest
  pairing first: [3]'s 1,000 tags a second per reader against [2]'s 30 m
  with the tag worn;
- against the law in the United States and the European Union, read
  before any radio is compared;
- against a beginner's hands.

The original system is the reference to beat (`docs/study/original-system.md`).
A claim marked **INFERRED** was reasoned, not read: its arithmetic is shown.
A figure marked **ASSUMED** is an input to that arithmetic, not a
measurement. Everything else cites the text it was read from, by the short
names in § Sources. Nothing here has been measured on a board; the build's
tests (`requirements.md` § The five measures, defined) decide.

This is the study's reading of the radio rules, made so that the decision
sitting (Story 2.1.7) can decide. It is not legal advice. A builder outside
the United States and the European Union reads their own country's rules.

## The law, read first

Every text below was read on 2026-10-27, in this order: the US sections and
the EU short-range-device Decision between this leg's clock reads of
18:27:42Z and 18:32:42Z, before any radio datasheet was opened; the Radio
Equipment Directive's articles, ERC/REC 70-03, the ETSI version listings
and §15.3 later in the leg, between 18:36:31Z and 18:41:09Z, after the first
datasheets were downloaded and before this document named any radio. The
US texts are read at the eCFR's point in time of 2026-10-23, the date the
eCFR reported Title 47 as up to date to; a law text's version date is its
pin (plan D8).

| Short name | Text | Version read | Read from |
|---|---|---|---|
| §15.3, §15.5, §15.23, §15.212, §15.231, §15.247, §15.249, §15.503, §15.517, §15.519 | 47 CFR Part 15, each section | eCFR, Title 47 up to date as of 2026-10-23. The last amendment each prints: §15.3, 88 FR 47394 (July 24, 2023); §15.5, 75 FR 63031 (Oct. 13, 2010); §15.23 and §15.503, none of their own; §15.212, 85 FR 18149 (Apr. 1, 2020); §15.231, 69 FR 71383 (Dec. 9, 2004); §15.247, 85 FR 18149 (Apr. 1, 2020); §15.249, 77 FR 4914 (Feb. 1, 2012); §15.517 and §15.519, 67 FR 39632 (June 10, 2002) | `https://www.ecfr.gov/api/versioner/v1/full/2026-10-23/title-47.xml?part=15&section=<n>`. The same text is viewed at `https://www.ecfr.gov/on/2026-10-23/title-47/section-<n>` |
| RED | Directive 2014/53/EU (the Radio Equipment Directive) | consolidated text of 30.05.2026 (CELEX 02014L0053-20260530). Read: Articles 1, 2, 3(1)–(2), 6, 7, 10(1)–(3), 17(1)–(4) and Annex I | `http://publications.europa.eu/resource/celex/02014L0053-20260530` |
| SRD | Commission Decision 2006/771/EC on short-range devices | consolidated text of 23.01.2025 (CELEX 02006D0771(01)-20250123), last amended by Implementing Decision (EU) 2025/105. Read: Articles 1–3, the Annex's general conditions and duty-cycle definition, Table 1, Table 2's bands 44a–45c, 46a, 47, 47a, 47b, 48, 54 and 57a–57c, and notes [6], [7] and [g] | `http://publications.europa.eu/resource/celex/02006D0771%2801%29-20250123` |
| REC | CEPT ERC Recommendation 70-03 | edition of October 2026, "latest amended on 9 October 2026 (FM#114)". Read: Annex 1, pages 7–9, whose footers print "Edition of October 2025" | `https://docdb.cept.org/download/5013` (document page `https://docdb.cept.org/document/845`) |
| EN 300 220-2 | ETSI harmonised standard, short-range devices below 1 GHz | V3.3.1, the published version dated 26 March 2025 in ETSI's listing | `https://www.etsi.org/deliver/etsi_en/300200_300299/30022002/` |
| EN 300 328 | ETSI harmonised standard, 2.4 GHz wideband transmission | V2.2.2, dated 24 July 2019 in ETSI's listing | `https://www.etsi.org/deliver/etsi_en/300300_300399/300328/` |
| EN 302 065-1 | ETSI harmonised standard, ultra-wideband, generic | V2.1.1, dated 15 November 2016 in ETSI's listing | `https://www.etsi.org/deliver/etsi_en/302000_302099/30206501/` |

**Limits on these reads:**

- **The three ETSI standards were read as their version listings only.**
  ETSI's server refused this leg's plain requests (HTTP 403). The listings
  were read through a web-reading tool, and the standards' files were not
  opened. This study relies
  on no clause of their text. Where a band's conditions matter, it cites the
  Decision's Table 2, which the standards serve (SRD note [7]). Each
  standard is still cited by its number and edition here, and marked so.
- **ERC/REC 70-03's tables lost their column alignment** when the PDF was
  read as text. It is cited only for what reads without doubt: EN 300 220
  is the standard named for the 863–870 MHz rows, and the generic UWB rows
  point to ECC/DEC/(06)04 and EN 302 065. The binding EU
  conditions used below are the Decision's.
- **Not read:** the EU's ultra-wideband Decision and ECC/DEC/(06)04,
  because ultra-wideband is declined on the US texts and the requirements
  (§ The other options, one by one); EN 302 065-2, the location-tracking part; the FCC's
  Subpart B for digital devices (§ Legality of the choice); and the module
  grant's own record (§ The radio chosen).
- **Every list item of plan D6 exists and applies.** None was found
  superseded. This leg added §15.3 and §15.5, which §15.23(b) and the
  seal's question (D5) need.

### What the US rules say, for this study

- **Home-built devices** (§15.23(a)): "Equipment authorization is not
  required for devices that are not marketed, are not constructed from a
  kit, and are built in quantities of five or less for personal use." Under
  §15.23(b), "the builder is expected to employ good engineering practices
  to meet the specified technical standards to the greatest extent
  practicable", and "The provisions of § 15.5 apply to this equipment".
- **Every device** (§15.5(b)): operation "is subject to the conditions that
  no harmful interference is caused and that interference must be accepted".
  Under §15.5(c), the operator must cease "upon notification by a Commission
  representative that the device is causing harmful interference".
- **Modules** (§15.212):
  - a single modular transmitter is "a completely self-contained
    radiofrequency transmitter device that is typically incorporated into
    another product, host or device", and is "approved with an antenna"
    (§15.212(a));
  - a host whose module label is hidden carries an outside label such as
    "Contains FCC ID: XYZMODEL1" (§15.212(a)(1)(vi)(A));
  - the module "must comply with any specific rules or operating
    requirements that ordinarily apply to a complete transmitter", and its
    maker "must provide adequate instructions" (§15.212(a)(1)(vii));
  - a limited modular approval needs its applicant to "state how control of
    the end product into which the module will be installed will be
    maintained" (§15.212(b)). INFERRED: a beginner cannot rely on a
    limited approval, which binds the end product to its applicant. The
    rebuild wants a full single-modular grant.
- **433 MHz, periodic operation** (§15.231):
  - restricted to "a control signal such as those used with alarm systems,
    door openers, remote switches, etc."; "Continuous transmissions ... are
    not permitted" (§15.231(a));
  - "A transmitter activated automatically shall cease transmission within
    5 seconds after activation" (§15.231(a)(2));
  - "Periodic transmissions at regular predetermined intervals are not
    permitted", except polling of security or safety transmitters within
    "two seconds per hour for each transmitter" (§15.231(a)(3));
  - the alternative of §15.231(e) allows "any type of operation" at lower
    field strength, provided "the duration of each transmission shall not
    be greater than one second and the silent period between transmissions
    shall be at least 30 times the duration of the transmission but in no
    case less than 10 seconds".
- **902–928 MHz and 2.4 GHz, any modulation, low power** (§15.249): the
  fundamental's field strength is 50 millivolts/meter at 3 meters
  (§15.249(a), (c)). INFERRED, by the conversion §15.503(k) prints ("adding
  95.2"): 50 mV/m is 94.0 dBµV/m, which is −1.2 dBm EIRP.
- **902–928 MHz and 2.4 GHz, hopping or digital modulation, up to 1 W**
  (§15.247):
  - frequency hopping in 902–928 MHz with a 20 dB bandwidth of 250 kHz or
    more: "at least 25 hopping frequencies and the average time of
    occupancy on any frequency shall not be greater than 0.4 seconds within
    a 10 second period" (§15.247(a)(1)(i));
  - digital modulation: "The minimum 6 dB bandwidth shall be at least
    500 kHz" (§15.247(a)(2));
  - power: "1 watt for systems employing at least 50 hopping channels; and,
    0.25 watts for systems employing less than 50 hopping channels, but at
    least 25" (§15.247(b)(2)), and 1 W for digital modulation
    (§15.247(b)(3)), with antennas up to 6 dBi (§15.247(b)(4));
  - a hopping system "employing short transmission bursts must comply with
    the definition of a frequency hopping system and must distribute its
    transmissions over the minimum number of hopping channels"
    (§15.247(g));
  - a system may adapt its own hopset "to avoid hopping on occupied
    channels", but "The coordination of frequency hopping systems in any
    other manner for the express purpose of avoiding the simultaneous
    occupancy of individual hopping frequencies by multiple transmitters is
    not permitted" (§15.247(h)).
- **Ultra-wideband** (Subpart F):
  - a UWB transmitter has "a fractional bandwidth equal to or greater than
    0.20 or has a UWB bandwidth equal to or greater than 500 MHz"
    (§15.503(d));
  - indoor systems: "employed solely for indoor operation", contained
    between 3100 MHz and 10,600 MHz, at −41.3 dBm EIRP average per MHz in
    3100–10600 MHz, and "A communications system shall transmit only when
    the intentional radiator is sending information to an associated
    receiver" (§15.517(a), (a)(5), (b), (c));
  - hand-held systems "do not employ a fixed infrastructure", and must
    cease transmission "within 10 seconds unless it receives an
    acknowledgement" (§15.519(a), (a)(1)).
- **What a transmitter is** (§15.3(o)): an intentional radiator is "A
  device that intentionally generates and emits radio frequency energy by
  radiation or induction".

### What the EU rules say, for this study

- **Scope** (RED Article 1(1)): a framework "for the making available on the
  market and putting into service in the Union of radio equipment".
  "Putting into service" is "the first use of radio equipment in the Union
  by its end-user" (Article 2(1)(11)). A "manufacturer" is a person who
  "manufactures radio equipment ... and markets that equipment under his
  name or trade mark" (Article 2(1)(12)).
- **Use** (Article 7): "Member States shall allow the putting into service
  and use of radio equipment if it complies with this Directive when it is
  properly installed, maintained and used for its intended purpose."
- **The essential requirements** (Article 3): health and safety, and
  electromagnetic compatibility (3(1)); and "Radio equipment shall be so
  constructed that it both effectively uses and supports the efficient use
  of radio spectrum in order to avoid harmful interference" (3(2)).
- **The manufacturer's duties** start "When placing their radio equipment on
  the market" (Article 10(1)). They include the conformity assessment of
  Article 17, the EU declaration of conformity and the CE marking (Article
  10(3)).
- **No home-built exemption.** Annex I excludes radio amateurs' equipment
  ("equipment constructed by individual radio amateurs for experimental and
  scientific purposes related to amateur radio") and "Custom-built
  evaluation kits destined for professionals to be used solely at research
  and development facilities". Neither describes a beginner's tags.
- **The bands** (SRD Table 2). Every row is "on a non-exclusive,
  non-interference and non-protected basis" (Article 3(1)), and a duty cycle
  is measured over "a continuous one hour period" (the Annex's definition):
  - 433.05–434.79 MHz, non-specific: 1 mW e.r.p. (band 44a), or 10 mW e.r.p.
    with "Duty cycle ≤ 10 %" (44b); and 434.04–434.79 MHz at 10 mW e.r.p.
    with "Duty cycle ≤ 100 % subject to bandwidth ≤ 25 kHz" (45c);
  - 865–868 MHz, non-specific: 25 mW e.r.p., with "Requirements on
    techniques to access spectrum and mitigate interference apply" or
    "Alternatively, a duty cycle ≤ 1 % applies" (47);
  - 868–868.6 MHz: the same, 25 mW e.r.p. and ≤ 1 % (48);
  - 869.4–869.65 MHz: 500 mW e.r.p., or "a duty cycle ≤ 10 %" (54);
  - RFID in 865–868 MHz: interrogators at 2 W e.r.p. in four channels (47a),
    and "RFID tags respond at a very low power level (-20 dBm e.r.p.)"
    (note [6]);
  - 2400–2483.5 MHz: 10 mW e.i.r.p. non-specific (57a), and 100 mW e.i.r.p.
    for wideband data transmission with access techniques (57c).
- **The access techniques** (SRD note [7]) are those "described in
  harmonised standards ... published in the Official Journal of the European
  Union under Directive 2014/53/EU". For the sub-GHz rows that standard is
  EN 300 220 (REC Annex 1).

### What the law decides before any radio is compared

- **A 433 MHz wake channel cannot meet [13] in the United States.**
  INFERRED from §15.231. A zone noticed within 5 s needs a wake field at
  least every few seconds, which is "Periodic transmissions at regular
  predetermined intervals", not permitted under §15.231(a)(3). The
  alternative of §15.231(e) allows periodic operation, but never more often
  than every 10 seconds. The original's 433.92 MHz wake, kept on for 1–31 s
  or "Transmit all the time" (`original-system.md` § Zones), fits neither.
  The EU's 433 MHz rows would allow it (bands 44b, 45c). A design meant to
  run in both regions is bound by the stricter rule, here the US one.
- **A low-power §15.249 radio starts 17 dB behind the EU's 25 mW.** INFERRED:
  −1.2 dBm EIRP against 25 mW e.r.p., which is 14.0 dBm e.r.p. or 16.1 dBm
  e.i.r.p. A link built for [2]'s 30 m wants §15.247's power in the United
  States. That means hopping, or digital modulation 500 kHz wide.
- **Five devices, then modules.** §15.23 covers milestone 1's five devices
  (one Reader, one Field Generator, three tags), built for personal use, not
  marketed, not from a kit. Read as a total, five is the limit itself, so
  milestone 1 has no room for a sixth device. A conference's 1,000 tags is
  far past it. INFERRED: the rebuild's radio must carry its own grant as a
  module (§15.212), and a design is not a kit when the builder buys every
  part themselves, which §15.23 does not define.
- **The EU has no home-built route.** INFERRED from RED Articles 7 and 10(1)
  and Annex I. A self-built device put into service must still comply with
  the Directive. The formal steps of Articles 10 and 17 fall on a
  manufacturer placing equipment on the market. The builder's route is a
  radio already conforming to EN 300 220, used inside the Decision's band
  conditions.
- **The EU's duty cycles rule out a wake field kept on.** INFERRED: in
  865–868 MHz a transmitter may be on 1 % of an hour (36 s), or use the
  harmonised standard's access techniques. A Field Generator that keeps a
  wake preamble on air, as the original's did, exceeds that. Short beacons
  at known times fit.
- **Readers may not coordinate their hopping.** INFERRED from §15.247(h): in
  the warehouse, where 9–12 readers share 902–928 MHz ([20]), each reader's
  network picks its own hopset. Readers may not divide the channels among
  themselves to avoid each other.

## The options against the requirements

Six options, from spec §3.3 and the story:

- **A,** the original's split: a 433 MHz wake channel plus a 902–928 MHz
  (US) or 868 MHz (EU) reply channel;
- **B,** a single sub-GHz band with wake-on-radio;
- **C,** dual-band chips: sub-GHz for tracking, Bluetooth for phone setup;
- **D,** Bluetooth Long Range;
- **E,** LoRa;
- **F,** ultra-wideband.

### [3] against [2] first

[3] needs a reader to take 1,000 replies in about a second, with time left
for retries. [2] needs that reader to hear a worn tag 30 m away in an open
hall. Throughput wants a fast link and short replies. Range wants a slow
link, because sensitivity falls with bandwidth (`requirements.md` § The
arithmetic). The question is whether any legal radio meets both, and with
what anti-collision.

**The radios' own figures:**

| Link | Sensitivity | Source |
|---|---|---|
| 300 kbit/s 2-GFSK, ±75 kHz deviation | −94 dBm (917.6 MHz, 576 kHz RX bandwidth) | PSIP p19; SWRA797 Table 2-1 ("Mode #5") |
| 200 kbit/s 2-GFSK, ±100 kHz | −103 dBm | SWRA797 Table 2-1 ("TI 15.4 200kbit/s") |
| 500 kbit/s 2-GFSK, ±190 kHz | −95 dBm (916 MHz, 1 % PER, 127-byte payload) | R7 p13 |
| 1 Mbit/s 2-GFSK, ±350 kHz | −97 dBm (915 MHz, BER 10⁻²) | R7 p14 |
| Bluetooth LE Coded, 125 kbit/s | −104 dBm | P7 p18 |
| LoRa SF7, 125 kHz: 5,470 bit/s; SF8, 500 kHz: 12,500 bit/s | — | TTN |

**The airtime of a reply.** INFERRED arithmetic with ASSUMED framing:
- **The reply:** a 4-byte tag ID and a status byte, as the original's replies
  carried (`original-system.md` § The packet), plus a 2-byte check, a
  length byte and a lead-in (preamble and sync word) of 8–12 bytes.
  ASSUMED: 16–20 bytes in all.
- **At 300 kbit/s,** 20 bytes last 0.533 ms on air. ASSUMED: 0.2 ms of
  guard and turnaround per slot, so a slot is 0.733 ms. That is 1,363 slots
  a second on one channel, and 1,000 tags take 0.73 s with 0.27 s left for
  retries. With 16 bytes: 1,595 slots a second.
- **At 200 kbit/s,** the same 20 bytes make a 1.00 ms slot: 1,000 a second,
  with no time left for retries.
- **At 125 kbit/s (Bluetooth LE Coded),** 16 bytes of payload alone last
  1.02 ms, before the coded PHY's own lead-in: under 1,000 a second even
  with perfect slots.
- **LoRa at 12,500 bit/s:** a 12-byte payload alone lasts 7.68 ms, so at
  most 130 replies a second per channel. At 5,470 bit/s it lasts 17.6 ms.

**The anti-collision.** INFERRED: at 300 kbit/s, slotted random access peaks
at 1/e of the slots, 501 replies a second, and pure random access (the
original's family) at about half that. Only slots the reader assigns reach
1,000.

| Option | [3] on one channel | [2] (§ Range) | Legal in both regions | Verdict for [3] against [2] |
|---|---|---|---|---|
| A: 433 MHz wake + sub-GHz reply | the reply side as B | as B | **no**: the 433 MHz wake fails §15.231 for [13] | declined |
| B: one sub-GHz band, 300 kbit/s, reader-assigned slots | **yes**: 1,363 slots/s | **yes on paper**, 21.8 dB before fading at the worst body loss | **yes**, on a certified module (§ The radio chosen) | **chosen** |
| B at 500 kbit/s or 1 Mbit/s | yes, with more margin | no worse by the tables: −95 and −97 dBm, though measured under other conditions than the module's −94 dBm | only as five home-built devices: no module this leg read is certified at these rates (SWRA797 Table 2-1) | declined |
| B with several channels per reader | yes | as B | the module's grant: "This transmitter must not be co-located or operating with any other antenna or transmitter" (PSIP p54) | declined for milestone 1 |
| C: dual-band, Bluetooth for setup | as B, on its sub-GHz half | as B | yes on paper; this leg found no certified module of it (research log) | not chosen; the sitting's option (§ The seal's HF interface) |
| D: Bluetooth Long Range | **no**: under 976 a second even with perfect slots | 8.5 dB more path loss at 2,440 MHz than at 915 MHz (INFERRED, the frequency ratio), against 10 dB better sensitivity | yes (§15.247; SRD 57c) | declined |
| E: LoRa | **no**: at most 130 a second per channel | best of all | yes | declined |
| F: ultra-wideband | yes, by its short pulses (INFERRED) | the weakest: −41.3 dBm/MHz EIRP average (§15.517(c)) | indoors only, with a fixed network of receivers (§15.517) | declined (§ Ultra-wideband) |

**The answer to D2:** one option meets [3] and [2] together, legally:
option B at 300 kbit/s with reader-assigned slots, on a certified module.
[3] is met on one channel, not brought back to Omid. [2] is met on paper,
with the crowded hall as the unmeasured risk (§ Range). No ASK-OMID row is
filed.

### The other options, one by one

- **A, the original's split.** Kept in spirit and declined in form. The
  wake channel moves into the reply band (option B's beacons). The split's
  real benefit was a tag whose receiver never shares a frequency with its
  transmitter (`original-system.md` § What this means for the study). A
  time-slotted single band gives the same separation in time.
- **C, dual-band.** TI's CC1352P7 adds Bluetooth Low Energy to the same
  sub-GHz radio, and up to +20 dBm in both bands (P7 p1). The leg found TI's
  certified module only in the sub-GHz-only CC1312PSIP. A dual-band tag
  would need its own Bluetooth certification route, a second antenna path
  and its own power budget. Phone pairing does not need it: Chorus's printed
  QR code is a host-software item (Story 2.1.5). Recommended against for
  milestone 1, and left open for the sitting with the seal's question.
- **D, Bluetooth Long Range.** Declined for tracking. It fails [3] by its
  rate. It also loses 8.5 dB of path to the 2.4 GHz frequency against
  10 dB of better sensitivity: −104 dBm against −94 dBm. And a body absorbs
  2.4 GHz at least as much as 900 MHz (ASSUMED; no source read).
- **E, LoRa.** Declined. It has the best range and fails [3] by an order of
  magnitude. TTN's US915 page also states "a 400 ms maximum dwell time per
  channel".
- **F, ultra-wideband.** Declined for milestone 1. It locates to the metre, which no requirement asks for: a zone is a
  room or an aisle ([4], [13], [22]). Under §15.517 it needs a fixed indoor
  network that "shall transmit only when the intentional radiator is sending
  information to an associated receiver". Its power, −41.3 dBm/MHz average,
  shortens its range against [2]. Its EU rules were not read. It stays a
  comparison for a later, finer location tier.

## The choice

Option B: **one sub-GHz band, a certified module, reader-assigned slots, and
zones from Field Generators.**

### The radio chosen

**Texas Instruments' CC1312PSIP**, a sub-GHz system-in-package: the radio,
an Arm Cortex-M4F, its crystals and its power parts in one 7 mm × 7 mm
module (PSIP p1). It runs one of its FCC-certified PHYs.

What it states that no original record does (`original-system.md` § What no
record states):

- **Bit rate:** 300 kbit/s, the module's fastest certified PHY ("Mode #5,
  300kbps", SWRA797 Table 2-1).
- **Modulation:** 2-GFSK, ±75 kHz deviation, on 600 kHz channels from
  902.6 to 927.2 MHz: 42 channels (SWRA797 Table 2-1; the count INFERRED).
- **Line coding:** data whitening is available in the main 2-(G)FSK mode
  (R7 Table 8-1). INFERRED: the PSIP's radio is the same family, since TI
  writes of "additional PHYs supported by the CC1312" beside the module's
  certified ones (SWRA797 p5). The PHY's default setting decides whether it
  is on.
- **Output power** in the United States, for the module's certification to
  hold (SWRA797 §2):
  - tags, on the 14 dBm port: 12.0 dBm "without duty cycling" with an
    antenna under 2.69 dBi, or 12.5 dBm under 2 dBi (SWRA797 §2.1.5);
  - the Reader and Field Generators, on the 20 dBm port: 17 dBm without
    duty cycling, 19 dBm with "duty cycling of 60ms_max" (SWRA797 §2.2.5);
  - in the EU the band's limit binds instead: 25 mW e.r.p. in 865–868 MHz
    (SRD band 47).
- **Certification** (PSIP p54, Table 9-3):
  - FCC ID ZAT-1312PSIP-2, "15.247 Operation within the 902-928 MHz band",
    certified "as a single-modular transmitter" that "carries a modular
    grant";
  - ISED 451H-1312PSIP2;
  - for Europe, the table names EN 300 220 (863–870 MHz), EN 303 204 and
    EN 303 659, with no certificate ID;
  - for 868 MHz, "this is not covered by the FCC modular certification"
    (SWRA797 §1.1): the US and EU routes differ.
- **The certification's conditions** (SWRA797):
  - "The default settings in the SDK must be used ... If the settings are
    changed then the FCC Certification is not valid and new tests are
    required at an official FCC test house" (p6);
  - an antenna of a listed type, at no more than 2.69 dBi (§3.2.2);
  - and, from PSIP p54, "This transmitter must not be co-located or
    operating with any other antenna or transmitter".
- **The wake signal's form:** a short beacon, sent at a time the tag already
  knows, on the network's current channel (§ Zones). For a tag that has
  lost its timing, the radio's "Carrier Sense and Preamble Detection can be
  used to implement sniff modes where the radio is duty cycled to save
  power" (R7 Table 8-1, note 2). INFERRED that the PSIP's radio has the
  same, as the same family (line coding, above).
- **Not read: the grant's own record.** The FCC's equipment-authorisation
  search, and a public mirror of it, refused a plain request (HTTP 403). So
  the grant's equipment class is unread: frequency hopping, or digital
  transmission 500 kHz wide. The design below hops (§ Channels), which
  holds under either. Leg 3 reads the grant before any part is bought.

**For a beginner's hands:** the module is a 0.5 mm-pitch package (PSIP p9)
with a reflow profile (PSIP p65). INFERRED: it is not hand-soldered. Its
board is assembled by a service, or bought on a carrier, and that is
Stories 2.1.4 and 2.1.6's build-route question. In exchange, INFERRED, the
module removes the original's hardest skill: no radio is tuned by hand on a
spectrum analyzer (`original-system.md` § What this means for the study).

### Anti-collision: slots the reader assigns

- **Joining.** A tag joins a Reader once, in a short contention window of
  each frame, and is given a slot number. New tags arrive a few at a time,
  so random access is enough for joining (INFERRED).
- **Roll calls.** The Reader's beacon opens a roll call, and every joined
  tag answers in its own slot. A tag's missing reply is retried in the next
  frame's spare slots.
- **The figures.** 1,363 slots a second on one channel: 1,000 tags in
  0.73 s with 0.27 s left for retries ([3]). The same scheme gives
  [21]'s 200 a second per channel with large margin. A frame of 0.25 s holds
  340 slots (INFERRED, § [3] against [2] first).
- **The original's ideas kept:** one packet format and a CRC on every
  reply, and group replies by tag type, now addressed in the beacon
  (`original-system.md` § Three ways to address).

### Range

INFERRED link budgets at 30 m, from the radio's figures and these ASSUMED
inputs: a tag antenna of −5 dBi on a worn badge (below the −0.5 to
+1.0 dBi of the chip antennas certified with the module, SWRA797 Table 3-2);
a Reader antenna of +2 dBi (under the grant's 2.69 dBi); and [2]'s body loss
of 10–20 dB.

| Path | Sent | Lost over 30 m (free space) | Body | Received | Margin to −94 dBm |
|---|---|---|---|---|---|
| Tag to Reader, US 915 MHz | 12.0 dBm − 5 dBi | 61.2 dB | 10–20 dB | −62.2 to −72.2 dBm | 31.8 to 21.8 dB |
| Reader to tag, US 915 MHz | 17 dBm + 2 dBi | 61.2 dB | 10–20 dB | −57.2 to −67.2 dBm | 36.8 to 26.8 dB |
| Tag to Reader, EU 868 MHz | 12.0 dBm − 5 dBi | 60.8 dB | 20 dB | −71.8 dBm | 22.2 dB |
| Reader to tag, EU 868 MHz, at 25 mW e.r.p. | 16.1 dBm e.i.r.p. | 60.8 dB | 20 dB | −69.6 dBm | 24.4 dB |

- **The free-space loss** is 20·log₁₀(4π·d·f/c) at d = 30 m: 61.2 dB at
  915 MHz and 60.8 dB at 868 MHz (INFERRED).
- **Down is stronger than up** by 5 dB, so a tag the Reader can hear can
  also be woken (INFERRED).
- **The unmeasured risk is the crowd.** A hall with 1,000 people adds the
  other bodies between tag and Reader, and indoor fading adds more. ASSUMED
  10–20 dB for both together. That leaves 1.8–11.8 dB at the worst body
  loss. [2] is therefore met on paper, not with confidence. Its mitigations
  are known: a Reader antenna mounted high, and [2]'s second Reader per
  hall. The build's range test ([2], 100 reads at each step) decides.
- **The EU Reader** stays at 25 mW e.r.p. INFERRED: 17 dBm with a 2 dBi
  antenna would be 48.4 mW e.r.p., over band 47's limit.

### Channels

- **United States: the network hops.** The Reader's network changes channel
  every 0.25 s frame, in a pseudo-random order over the 42 channels, and the
  tags and Field Generators follow it from the beacon. A full cycle takes
  10.5 s, so no channel is used for more than 0.25 s in any 10 s (INFERRED).
  That meets §15.247(a)(1)(i)'s 25 channels and 0.4 s, and §15.247(g)'s
  spreading of short bursts. It is harmless if the grant turns out to be the
  wide digital class.
- **Each Reader hops independently** (§15.247(h)). INFERRED: with 12 readers
  in the warehouse, another reader shares a given reader's channel in about
  23 % of frames (1 − (41/42)¹¹). Bays 30 m apart rarely hear each other.
  Even a 23 % loss leaves about 1,050 slots a second, far above [21]'s 200.
- **European Union: one band, 865–868 MHz** (SRD band 47), 25 mW e.r.p.,
  within the 1 % duty cycle. INFERRED duty cycles:
  - a Reader beacon of about 1 ms every 0.25 s is 0.40 %;
  - a tag answering every roll call of a 1 s burst is 0.073 %;
  - a Field Generator's beacon is less than the Reader's.

  The band carries five 600 kHz channels (INFERRED), and band 47's row
  sets no bandwidth limit. Hopping is not required there.

### Zones: how a move is noticed within 5 s

The original's zone was a Field Generator's 433 MHz field, set by a field
strength control, and each reply named the Field Generator that woke the
tag (`original-system.md` § Zones). The rebuild keeps that idea in the
single band:

- **A Field Generator is the same module, fixed in a room or at an exit.** It
  sends a short beacon carrying its ID in its own slot of every frame, at a
  power set for its room. The power is the zone's size, as the original's
  field strength was.
- **A tag wakes on its own timer** for the Reader's beacon and the slots of
  the Field Generators it last heard. It reports the Field Generators it
  hears, with their signal strength, when the strongest one changes, and
  with every check-in.
- **The server decides the zone** from those reports, with hysteresis at
  doorways (Chorus's "smart server", § The Chorus comparison).
- **An exit's Field Generator also listens.** A tag that hears an exit's
  beacon answers in every frame while it stays in the exit zone. The
  pairing alarm ([17], 5 s) and the exit test ([13], 100 of 100) are
  therefore decided where the radio is busiest.

**The timing, for the nursery's [13]** (INFERRED):

| Step | Time, at most |
|---|---|
| The tag's next listen after it enters the room | 1 s (a listen every second) |
| A second hearing, to confirm the change | 1 s |
| Its report, in the next frame's slot | 0.25 s |
| The Reader to the host | 0.25 s (ASSUMED) |
| The host draws the move | 0.5 s (ASSUMED) |
| **Total** | **3.0 s, inside [13]'s 5 s** |

**At an exit** (INFERRED): at walking pace a tag spends about 4 s in a
3 m-radius exit field and hears it at least three times. ASSUMED: 95 % per
hearing. A pass is missed once in 8,000, so 100 of 100 passes succeed with
98.8 % probability. That is the strictest test in the study, and the build
measures it.

**The battery this costs** (INFERRED, for Story 2.1.4 to replace with the
datasheet's start-up energies):
- listening 1.8 ms a second (the beacon and two Field Generator slots) at
  the module's 5.8 mA receive current (PSIP p1, at 868 MHz): 91.5 mAh a
  year;
- [18]'s check-in every 20 s, a 0.733 ms reply at 28.7 mA (PSIP p1, at
  +14 dBm): 9.2 mAh;
- standby at 0.9 µA (PSIP p1): 7.9 mAh;
- in all, 108.6 mAh a year: 1.38 years from the requirements' usable
  150 mAh, against [18]'s one year.

The listening dominates, as the requirements predicted (`requirements.md`
§ The arithmetic). If Story 2.1.4's figures break [18], the fallback keeps
the zones: the Field Generators listen and the tags only transmit.

### Legality of the choice

**Why a beginner's build is legal to run without equipment authorisation of
their own:**

- **United States, milestone 1.** Five devices built for personal use, not
  marketed, not from a kit, need no equipment authorisation (§15.23(a)).
  The builder still meets the technical standards "to the greatest extent
  practicable" (§15.23(b)), and §15.5 applies. That covers the five even
  before the module is counted.
- **United States, beyond five.** Every transmitter is the CC1312PSIP,
  whose own modular grant covers it (§15.212; FCC ID ZAT-1312PSIP-2). It is
  used only at a certified PHY with the SDK's default settings, at the
  powers above, with a listed antenna type of at most 2.69 dBi, and never
  beside a second transmitter (SWRA797; PSIP p54). The device outside
  carries "Contains FCC ID: ZAT-1312PSIP-2" (§15.212(a)(1)(vi)(A)).
- **Not read:** the Reader's USB or Ethernet electronics are a digital
  device under Part 15's Subpart B (§15.3(k)), which this leg did not read.
  Story 2.1.4 reads it with the Reader's parts.
- **European Union.** There is no home-built exemption and no module grant.
  INFERRED from RED Articles 7 and 10 and Annex I: a self-built device may be
  put into service if it complies with the Directive. The rebuild's route
  has three parts:
  - a radio whose maker names EN 300 220 for 863–870 MHz (PSIP p54);
  - operation inside band 47's conditions: 25 mW e.r.p. and a duty cycle
    under 1 %;
  - the builder's own care for Article 3's safety and EMC requirements.

  The study cannot make that route as firm as the US grant. The decision
  sitting should know that the EU build rests on the builder's compliance.
- **Neither region needs the 433 MHz band,** whose US rule the original's
  wake field could not meet (§ What the law decides).

### What the choice leaves to later stories

- **Story 2.1.4** names the parts around the module, reads the FCC grant's
  record, and replaces this document's ASSUMED figures with the datasheet's.
  Those figures are the tag antenna, the slot guard and the receive start-up
  energy. It also weighs one core in three enclosures (spec §3.7): one
  module in every tag style suits it (INFERRED).
- **Story 2.1.5** takes the event schema the Reader and the Field
  Generators report: tag, reader or Field Generator, signal strength, time
  and payload.
- **Story 2.1.6** prices the module's assembled-board routes.
- **Story 2.1.7** decides, with this document's choice as its default.

## The Chorus comparison, radio side

Spec D12 and §3.8, and the research log's § 3. Each radio-side item is
ADOPTED or DECLINED with its reason. The software-side items are Story
2.1.5's.

| Chorus item | Answer | Reason |
|---|---|---|
| Four layers (tags, readers, server, dashboard), a "reader" being any device that emits the same event schema | **ADOPTED** | The Reader and the Field Generator both emit it, so an exit's listening Field Generator is a reader too. Story 2.1.5 writes the schema. |
| Simple readers and a smart server: readers report tag, reader, signal strength, time and payload, and the server decides zones and alarms | **ADOPTED,** with two time-critical parts kept local | The server decides zones from the tags' reports (§ Zones). The Reader keeps the beacon and the slot schedule. A tag keeps the decision of when to report, which is what makes 5 s and a year's battery fit together. |
| Three hardware tiers: an item tag, a reusable badge or fob, and a combined sensor-and-reader | **ADOPTED,** with the combined unit's cellular uplink DECLINED | The item tag is the warehouse's. The badge and fob are the compactTag and cardTag. The combined unit is a Field Generator that beacons, listens and carries sensors, on mains power. Chorus's Scout reports over a cellular link (INFERRED there); the rebuild's server is local, so a cellular uplink adds a carrier's subscription and no requirement needs it. |
| Prototype on off-the-shelf modules | **ADOPTED** | The radio is a certified module. The first bench builds use its vendor's development kit, the "LP-CC1312PSIP Development Kit" (PSIP p1). That kit's own FCC status was not read; Story 2.1.4 reads it before it is bought. |
| Not to copy: a crowd-sourced reader network | **DECLINED** | Its coverage and latency are outside the system's control, and it is a privacy problem for tags worn by people (research log § 3). On the radio side, phones and Wi-Fi access points do not receive this sub-GHz band (INFERRED), and [13]'s exits need owned receivers in fixed places. |

## Does ActiveWave's approach still win

The radio side of spec §3.8's question. Owned infrastructure with no
subscription, and pairing people with things, are Story 2.1.5's.

- **Room-level zones from a Field Generator: it still wins, and is kept.**
  Chorus places goods to "about a kilometer" (research log § 3). The
  requirements ask for the right room 99 % of the time and every exit pass
  ([13]), which a Field Generator per room and per exit gives. The 433 MHz
  wake field is replaced by a beacon in the same band (§ Zones).
- **Relay outputs: they still win, and are kept.** The Reader keeps the
  original's two relays and two supervised inputs (`original-system.md`
  § Alarms and I/O), for doors and cameras. The radio choice changes nothing
  there. INFERRED: an exit's Field Generator may carry a relay too, so a
  door's output need not wait on the host.
- **Wake-on-command: it still wins, and is kept in a new form.** A command
  for a tag, a type or all tags (`original-system.md` § Three ways to
  address) rides in the Reader's beacon. A tag acts on it at its next
  listen, at most 1 s later. Tags that send on their own timers, Chorus's
  way, spend battery on every send whether anyone listens or not. Waking on
  command spends it on listening that the slots keep short (§ Zones).

## The seal's HF interface (D5, the radio side)

The 2012 sealTag was read by a 13.56 MHz ISO 15693 reader through its
dual-interface memory (`original-system.md` § The sealTag). Whether the
rebuild's seal keeps such an interface, for a phone to read, is for Story
2.1.5 and the sitting. The radio side:

- **A passive HF interface needs no transmitter of its own.** INFERRED from
  §15.3(o): an intentional radiator "intentionally generates and emits radio
  frequency energy". A passive tag is powered by the phone's field and
  answers by changing its load on that field. The phone is the transmitter,
  under its own authorisation. This leg read no FCC or EU text on passive
  tags, so the inference stands until one is read.
- **The seal's only transmitter stays the sub-GHz module,** under the rules
  above.
- **The alternative is option C's Bluetooth,** which a phone also reads. It
  would be a second transmitter under §15.247 and SRD band 57c, and is not
  chosen for milestone 1 (§ The other options).

## Sources

**Law texts:** the table in § The law, read first. Each is cited at the
version it prints, with its URL; none is pinned (plan D8).

**Pinned in the reference corpus** (`docs/reference-corpus.tsv`, folder
`sources/`, licence `third-party`, study only; the choice rests on them):

| Short name | Document | Pin (sha256, first 12) | URL |
|---|---|---|---|
| PSIP | TI, "CC1312PSIP SimpleLink Sub-1-GHz Wireless System-in-Package", SWRS293, November 2023 | `da5eb47438cf` | `https://www.ti.com/lit/ds/symlink/cc1312psip.pdf` |
| SWRA797 | TI, "Design Considerations for CC1312PSIP FCC and IC Certified RF Module", SWRA797, September 2024 | `2fbe59c4ae3f` | `https://www.ti.com/lit/an/swra797/swra797.pdf` |
| R7 | TI, "CC1312R7 SimpleLink High-Performance Sub-1 GHz Wireless MCU", SWRS254A, May 2021, revised November 2021 | `04b6984dd5c7` | `https://www.ti.com/lit/ds/symlink/cc1312r7.pdf` |

**Cited by URL and fetch date** (fetched 2026-10-27; no choice rests on
them alone):

- **P7:** TI, "CC1352P7 SimpleLink High-Performance Multi-Band Wireless MCU
  with Integrated Power Amplifier", SWRS251A, May 2021, revised November
  2021: `https://www.ti.com/lit/ds/symlink/cc1352p7.pdf`.
- **TTN:** The Things Network, "US902-928 MHz" regional parameters:
  `https://www.thethingsnetwork.org/docs/lorawan/regional-parameters/us915/`.

Page numbers are each PDF's own page count (page 1 is the cover page).
