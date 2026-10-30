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
  guard and turnaround per slot, so a slot is 0.7333 ms (11/15 ms). That is
  1,363 slots a second on one channel, and 1,000 tags take 0.73 s with
  0.27 s left for retries, before the Reader's beacon, the Field Generators'
  slots, the joining window and the downlink slot take their share of each
  frame (§ Anti-collision computes each scenario's).
  With 16 bytes: 1,595 slots a second.
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
| B at 500 kbit/s or 1 Mbit/s | yes, with more margin | worse on the chosen module's own sheet: −90 dBm at 500 kbit/s (916 MHz, 1 % PER, 127-byte payload) and −93 dBm at 1 Mbit/s (915 MHz, BER 10⁻²), against its −94 dBm at 300 kbit/s (PSIP p16, p17, p19). The CC1312R7's sheet prints −95 and −97 dBm (R7 p13, p14) | only as five home-built devices: no module this leg read is certified at these rates (SWRA797 Table 2-1) | declined |
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
  - a second ID in TI's own certification note: its FCC filings, the
    modular approval letter among them, are listed under ZAT-1312PSIP-3,
    and "all the files are available on the https://fccid.io/ZAT-1312PSIP-3
    homepage" (SWRA797 p14, p17). Which of the two grants covers the chosen
    PHY is unread (below);
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
  knows, on the network's current channel; on most listens a tag hears its
  Field Generators' slots in its place (§ Zones). For a tag that has
  lost its timing, the radio's "Carrier Sense and Preamble Detection can be
  used to implement sniff modes where the radio is duty cycled to save
  power" (R7 Table 8-1, note 2). INFERRED that the PSIP's radio has the
  same, as the same family (line coding, above).
- **Not read: the grant's own record.** The FCC's equipment-authorisation
  search, and a public mirror of it, refused a plain request (HTTP 403). So
  the grant's equipment class is unread: frequency hopping, or digital
  transmission 500 kHz wide. The design below hops (§ Channels), which
  holds under either. Story 2.1.4's requests were refused the same way, and
  Omid answered "Write it as not read" (`parts-and-toolchains.md` § What was
  read). The read is now the decision sitting's, by Omid's hand in a
  browser: the grant pages of both IDs, ZAT-1312PSIP-2 and ZAT-1312PSIP-3,
  before the rebuild spec names an FCC ID or any part is bought (plan Story
  2.1.7).

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
- **Roll calls.** The Reader's beacon opens a roll call, which the Field
  Generators' slots flag to the tags that skip the beacon (§ Zones), and
  every joined tag answers in its own slot. A tag's missing reply is retried
  in the next frame's spare slots.
- **The figures.** The channel carries 1,363 slots a second, and a frame of
  0.25 s holds 340 of them, before the Reader's beacon, the Field
  Generators' block, the joining window and the downlink slot take their
  share (INFERRED, § [3] against [2] first). The beacon takes 1.0 ms of each
  frame and each Field Generator 0.4 ms (both ASSUMED, § Zones); the layout
  of a tag's listen adds none (§ Zones, The layout of a listen). The joining
  window and the downlink slot that names a set take three reply slots of
  each frame, 2.2 ms (ASSUMED: two for joining, one for the downlink; the
  build measures both). INFERRED, each scenario's block (added by plan
  Story 2.1.12, from the Sprint 3 review's nit), with the joining window and
  the downlink slot counted (added by plan Story 2.1.15, from the Sprint 4
  review's Minor 3):
  - **the conference:** 23 Field Generators, ASSUMED from [1]: the main
    hall's twelve areas (§ Zones), the other nine rooms and two exits, as
    many exits as the ward's. [2]'s second Reader in the main hall adds no
    block of its own, since it listens on the first Reader's network
    (§ Zones, The conference hall's second Reader). The block takes 10.2 ms
    of each frame, which leaves 324 reply slots a frame, 1,296 a second. A
    hall of 1,000 tags then takes about 0.77 s, with 0.23 s left for
    retries ([3]);
  - **the ward:** 27 Field Generators (§ Zones), 11.8 ms, which leaves 321
    reply slots a frame, 1,284 a second. Its 100 tags take 0.08 s ([12]);
  - **any one Reader:** its block can hold up to 158 Field Generators before
    [3]'s 1,000 a second is lost (250 ms, less 250 reply slots of 11/15 ms,
    the 1.0 ms beacon and the 2.2 ms for joining and the downlink, leaves
    63.5 ms, at 0.4 ms a Field Generator). The warehouse's Readers stay far
    inside it, since [21] asks 200 a second: at that rate a block may hold
    525, its edge Field Generators' slots included (§ Zones, A Reader's
    edge).

  Before Story 2.1.15 these figures left out the joining window and the
  downlink slot, so they were upper bounds: 327 reply slots a frame and
  1,308 a second for the conference (0.76 s), 324 and 1,296 for the ward,
  and up to 164 Field Generators for any one Reader.
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
  hall, which listens on the first Reader's network (§ Zones, The
  conference hall's second Reader). The build's range test ([2], 100 reads
  at each step) decides.
- **The EU Reader** stays at 25 mW e.r.p. INFERRED: 17 dBm with a 2 dBi
  antenna would be 48.4 mW e.r.p., over band 47's limit.

### Channels

- **United States: the network hops.** The Reader's network changes channel
  every 0.25 s frame, in a pseudo-random order over the 42 channels. The
  Field Generators follow it from the beacon. A tag is given the order when
  it joins, counts frames on its crystal between the beacons it hears, and
  reads the frame's number in each one (§ Zones, The layout of a listen).
  A full cycle takes
  10.5 s, so no channel is used for more than 0.25 s in any 10 s (INFERRED).
  That meets §15.247(a)(1)(i)'s 25 channels and 0.4 s, and §15.247(g)'s
  spreading of short bursts. It is harmless if the grant turns out to be the
  wide digital class.
- **Each Reader hops independently** (§15.247(h)). INFERRED: with 12 readers
  in the warehouse, another reader shares a given reader's channel in about
  23 % of frames (1 − (41/42)¹¹). Even a 23 % loss leaves about 1,050 slots
  a second, far above [21]'s 200. That bound counts every shared frame as
  lost, as if each Reader heard all the others, so it rests on no claim
  about how far apart two Readers hear each other (plan Story 2.1.18, from
  the Story 2.1.17 review's Minor F).
  A bay at a Reader's edge has a neighbour in the next Reader's network, on
  that network's time and channel. So each network keeps a Field Generator
  of its own in the bay across each of its edges (§ Zones, A Reader's
  edge).
- **European Union: one band, 865–868 MHz** (SRD band 47), 25 mW e.r.p.,
  within the 1 % duty cycle. INFERRED duty cycles:
  - a Reader beacon of about 1 ms every 0.25 s is 0.40 %;
  - a tag answering every roll call of a 1 s burst is 0.073 %;
  - a Field Generator's beacon is less than the Reader's.

  The band carries five 600 kHz channels (INFERRED), and band 47's row
  sets no bandwidth limit. Hopping is not required there.
- **In the European Union, each Reader keeps one channel, and Readers
  beside each other take different ones** (added by plan Story 2.1.16, from
  the Story 2.1.15 review's Minor A). INFERRED:
  - **the warehouse is where it matters.** Its 9–12 Readers ([20]) sit in a
    grid, each with up to eight around it. Four channels in a repeating
    2 × 2 pattern give each Reader a channel that no Reader around it uses,
    and leave one of the five spare. The ward and the conference each keep
    one block of slots (§ Anti-collision; § Zones, The conference hall's
    second Reader), one network, so one channel serves each;
  - **Readers that share a channel take turns at the day's roll call**
    (added by plan Story 2.1.17, from the Story 2.1.16 review's Minor E;
    planned on turns by the rebuild spec's decision 3b,
    `docs/superpowers/specs/2026-10-30-rebuild-design.md` § 5.3 at
    `168bebb8`, and written in by plan Story 2.1.18, from the Story 2.1.17
    review's Minor F). The pattern repeats a channel two Readers along: a
    3 × 3 grid puts four of its nine Readers on one channel, and a 3 × 4
    grid four of its twelve on each of two. [21] counts its 200 a second per
    channel, "because readers that share a channel share its airtime"
    (`requirements.md` [21]). The beacon that opens a roll call names its
    Reader's turn, so each tag still hears one beacon and sends one reply,
    and `parts-and-toolchains.md` § [27] is unchanged. The busiest
    channel's four shares, 4 × 100,000 ÷ 9 = 44,444 items or 4 × 100,000 ÷
    12 = 33,333, take 222 s or 167 s at 200 a second, after the count's
    wait of 300 + 12 + 0.25 = 312.25 s (§ Zones, The layout of a listen).
    The inventory then takes 534.5 s or 478.9 s, about 8.9 or 8.0 minutes,
    inside [21]'s 10. That is the EU warehouse's planning figure.
    INFERRED, why not at once: Readers on one channel stand two bays apart,
    about 60 m. § Range's EU tag-to-Reader row closes at 30 m with 22.2 dB
    to spare, and 60 m takes 20·log₁₀(60 ÷ 30) = 6.0 dB more in free space,
    which leaves 16.2 dB before the racking between them, which this study
    does not price. A tag in the other Reader's bay can stand nearer, about
    45 m with each Reader mid-bay: 3.5 dB more, 18.7 dB left. So two
    Readers on one channel can hear each other's tags. They roll call at
    once, and the inventory keeps 5.9–6.1 minutes, only if the build's [21]
    test on two Readers that share a channel shows that they do not;
  - **one channel for every Reader is declined.** A bay at a Reader's edge
    holds a Field Generator of each network (§ Zones, A Reader's edge). On
    one channel their 0.4 ms slots would meet whenever their starts lay
    within 0.4 ms of each other: 0.8 ms of each 250 ms frame, 0.32 % of the
    time. The two Readers' crystals, each ±10 ppm (PSIP p23), slide by up
    to 20 µs a second. So at their worst a meeting lasts 0.8 ms ÷ 20 µs =
    40 s, once in 250 ms ÷ 20 µs = 12,500 s, about 3.5 hours. Crystals that
    agree more closely meet less often and for longer, without bound, as in
    the declined row of § A Reader's edge;
  - **a tag that moves to another Reader** learns that Reader's channel
    from the downlink that places it (§ Zones, A Reader's edge), and listens
    there for at most a frame;
  - **a lost tag searches its site's channels** (added by plan Story
    2.1.17, from the Story 2.1.16 review's Minor D): the channels the site's
    Readers use, which a Reader names to a tag when it joins, as a US tag is
    given the hop order. At a site of more than one channel, its first two
    searches are wide: a whole frame on each of the site's channels, its own
    first, which meets any network in reach whatever its time (added by plan
    Story 2.1.18, from the rebuild spec's decision 3c; § Zones, A tag
    carried past a Reader's edge). They start once a listen to its own
    network's whole block hears none of that network's Field Generators,
    whether or not it hears the beacon, and the tag joins the network whose
    Field Generator it hears (added by plan Story 2.1.20, from the Story
    2.1.18 review's Minor H). What follows is the search after two wide
    searches hear no Field Generator. Until its window reaches a frame, after about 35
    minutes lost (0.25 s ÷ (2 × 60 ppm) = 2,083 s; § Zones, How a tag hears a
    Field Generator it has not heard before), each search keeps its own
    Reader's channel. A narrower window is timed on its own network's
    frame, and another network keeps its own time, so it would meet that
    network only by chance. After that, the tag tries the next of its
    site's channels at each search. A tag that hears nothing cannot tell
    whose area it lies in, so it cycles wherever it lies. Each search then
    listens a frame, 2.795 µC + 5.8 mA × 250 ms = 1.45 mC, which takes 220 s
    at 6.6 µA (there). So a tag lost in its own Reader's area is found:
    - **in its first 35 minutes lost,** at its next search, on its own
      channel, within 220 s, about 4 minutes, at any site. A search's
      window is then narrower than a frame, so it costs less and comes
      sooner;
    - **after that, at the ward and the conference,** one channel each
      (above), at its next search, within 220 s, about 4 minutes;
    - **after that, in the warehouse,** within four searches, 4 × 220 s =
      14.7 minutes, as is a tag lost in another Reader's area once its
      window reaches a frame. The spare channel stays out of the cycle:
      over all five, it would take up to 5 × 220 s = 18.3 minutes.

### Zones: how a move is noticed within 5 s

The original's zone was a Field Generator's 433 MHz field, set by a field
strength control, and each reply named the Field Generator that woke the
tag (`original-system.md` § Zones). The rebuild keeps that idea in the
single band:

- **A Field Generator is the same module, fixed in a room or at an exit.** It
  sends a short beacon carrying its ID in its own slot of every frame, at a
  power set for its room. The power is the zone's size, as the original's
  field strength was.
- **A tag wakes on its own timer** for the slots of its listen set, and for
  the Reader's beacon when it needs it (§ How a tag hears a Field Generator
  it has not heard before, and its § The layout of a listen, below). It
  reports the Field Generators it hears, with their
  signal strength, when the strongest one changes, and with every check-in.
- **The server decides the zone** from those reports, with hysteresis at
  doorways (Chorus's "smart server", § The Chorus comparison).
- **An exit's Field Generator also listens.** A tag that hears an exit's
  beacon answers in every frame while it stays in the exit zone. The
  pairing alarm ([17], 5 s) and the exit test ([13], 100 of 100) are
  therefore decided where the radio is busiest.

#### How a tag hears a Field Generator it has not heard before

Added by plan Story 2.1.10, from the Sprint 2 review's finding 4. A tag that
listened only to the Field Generators it last heard would never listen in
the slot of a room it had not entered before, so a move into that room would
never be noticed. The rebuild names the slots for it:

- **The floorplan draws each zone's neighbours:** the zones a tag can be
  carried into from it. The host's zones already hold them
  (`software-stack.md` § The API).
- **A tag's listen set** is the slot of its zone's Field Generator and the
  slots of that zone's neighbours. When the host places a tag in a zone, the
  Reader names the set to it in a downlink slot of its next frame, which a
  tag that has just reported a change listens for. INFERRED: that is one
  more short listen per zone change, beside the 86,400 listens of a day at a
  1 s listen, so this study does not price it.
- **A tag with no set listens to every slot.** A tag that has just joined,
  or that has heard none of its set in two listens, listens to every Field
  Generator's slot until the host places it. A tag still unplaced after a
  minute slows that search to one listen every 12 s, so a tag shut in a
  drawer does not drain its cell. Such a tag hears the beacon and the whole
  block in one window. INFERRED: 6.6 µA while it stays lost among the
  ward's 27 Field Generators, against 10.45 µA at most in a room (§ The
  layout of a listen, below).
- **A lost tag's window has a bound, and its searches space out** (added by
  plan Story 2.1.15, from the Sprint 4 review's Minor 5). INFERRED: the
  window widens by 2 × 60 ppm of the time since the tag last heard a slot
  (§ The layout of a listen), so a tag that hears nothing passes a 0.25 s
  frame's window after about 35 minutes. At a search every 12 s it would
  then listen a whole frame each time, about 121 µA. So:
  - **the window stops growing** where a listen is sure to meet the
    network: at one frame in the EU, where a network keeps its channel (a
    lost tag then tries its site's channels in turn, § Channels), and at
    one hop cycle, 10.5 s, in the US, where a tag on one channel meets the
    network once a cycle (§ Channels). The US window reaches the cycle
    after about 24 hours lost;
  - **each search waits for its own charge over 6.6 µA.** A search that
    listens longer comes later, so a lost tag listens for 6.6 µA however
    long it is lost, the figure it started at.
  - **Its cost** is the time to be found again, which differs by region:
    - **in the EU,** one search in about 4 minutes once the window reaches
      a frame, after about 35 minutes lost, however long the tag then
      stays lost. The ward and the conference search their one channel, so
      that is their time to be found. In the warehouse a lost tag then
      tries its site's four channels in turn, so it is found within four
      searches, about 15 minutes, in its own Reader's area as in another's
      (§ Channels);
    - **in the US,** where the window keeps widening, the same at about 35
      minutes, then one search in about 6.5 minutes after an hour lost, and
      at most about 2.6 hours once the window reaches the cycle.

    These are the times for a tag that the wide search has not found. At a
    site of more than one network, it finds a tag carried into another
    Reader's area within about a minute (§ A tag carried past a Reader's
    edge, below; added by plan Story 2.1.18).

    The build initiatives may restart the search at once on a pin edge, a
    seal's loop or a badge's button (INFERRED).
- **The slots sit in one block after the Reader's beacon.** ASSUMED: the
  beacon lasts 1.0 ms and each Field Generator's slot 0.4 ms, which together
  reproduce this study's 1.8 ms for the beacon and two slots. The build
  measures both lengths. The host orders the block along the floorplan
  (§ The layout of a listen, below).
- **The block takes frame time.** INFERRED, for the ward's 27: the slots
  and the beacon take 11.8 ms of each 0.25 s frame, which leaves, with the
  joining window and the downlink slot, 321 reply slots a frame, 1,284 a
  second. That is still above [3]'s 1,000, the most any scenario asks of
  one Reader. § Anti-collision computes the conference's block beside it.

**The neighbours, ASSUMED from each scenario's size** (`requirements.md`
items [1], [10] and [19]):
- **The nursery:** the corridor is covered in five stretches, each with a
  Field Generator of its own at the doors of about four rooms. A room then
  has one neighbour, its stretch. A stretch has six: its four rooms, and the
  stretch or the exit at each end. The ward has 27 Field Generators: 20
  rooms, two exits and five stretches.
- **The conference:** a room has two neighbours. An area of the main hall
  has at most eight: a 40 m × 30 m hall in areas of about 10 m, the inner
  ones with eight around them.
- **The warehouse:** an item at rest in a bay has two, the bays either side
  of it along its aisle.

**Weighed against the other two ways** (plan Sprint 3's D3). INFERRED, for
the nursery's tag at its 1 s listen and for the warehouse item tag at its
12 s listen, with `parts-and-toolchains.md` § The battery's figures and the
lengths above:

| Mechanism | What a tag listens to | [13]'s move | [22]'s move | Listening, the nursery's tag in a room | Its cost |
|---|---|---|---|---|---|
| **The neighbours named (chosen)** | its zone's slot and its neighbours' | 3.0 s | 25 s | 13.9 µA as weighed: its own slot and its stretch's | a tag in a zone with many neighbours listens to more slots; a link the floorplan misses (below) |
| The whole block at every listen | every Field Generator's slot | 3.0 s | 25 s | 71.9 µA, the ward's 27 slots | 630 mAh a year of listening: 0.23 years for [18] |
| A wider listen every second time | the slots it last heard, and the whole block every second listen | 4.0 s (2 + 1 + 1.0) | 37 s (24 + 12 + 1) | 42.9 µA | 376 mAh a year of listening: 0.37 years for [18] |

The three rows are priced as Story 2.1.10 weighed them, with the Reader's
beacon in every listen. The layout below takes the beacon out of most
listens (added by plan Story 2.1.12). The beacon's 5.8 µA at a 1 s listen
then comes off each row alike, so the ranking stands (INFERRED). With the
block's order counted as well, the chosen row's room tag listens for
8.13 µA beside its stretch in the block and 10.45 µA one room away from it.

**The choice: the neighbours named.** It is the only one of the three that
keeps a room's tag at the figure this study priced, and it meets [13] and
[22] as the design did before. Its cost falls on a tag in a zone with many
neighbours, and `parts-and-toolchains.md` prices it (§ Each worn tag's year,
§ [27]). One mechanism serves the three scenarios, so the tags keep one
firmware (`parts-and-toolchains.md` § One core or three).

**A link the floorplan misses** (INFERRED). A tag carried along it hears
none of its set once its old zone fades. Two missed listens, a listen to the
whole block and a second hearing then put the move on screen in 5.0 s at a
1 s listen (2 + 1 + 1, then 1.0 s for the slot, the Reader and the host), at
[13]'s limit. While the old zone's Field Generator is still heard, the move
is not seen at all. The links are therefore part of setting up the zones.
INFERRED: the guides' setup walk carries a tag through every doorway in a
setup mode that listens to the whole block, and the host records each pair
of zones a step of the walk joins.

**The timing, for the nursery's [13]**, a move into a zone the floorplan
draws as a neighbour (INFERRED):

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

**The layout of a listen: the beacon skipped on most listens** (added by
plan Story 2.1.12, from the Sprint 3 review's finding 1). One block of slots
after one beacon can put only one zone's set next to the beacon. Every other
tag must wake twice, once for the beacon and once for its set, or listen
across the slots between them. With the second wake, the nursery's room tag
listens for 16.73 µA, not 13.9 µA, and [18] fails on paper (the review's
figure, reproduced here). So the rebuild takes the beacon out of most
listens:
- **A tag listens to its set's slots alone.** It keeps the frame's time on
  its 32 kHz crystal between listens, and each Field Generator's slot it
  hears sets that time again. The Field Generators time their slots from the
  Reader's beacon, which they hear in every frame, so their slots carry the
  Reader's time. The window is widened for drift as before, by 2 × 60 ppm of
  the time since the tag last heard a slot: the tag's RTC to ±50 ppm (PSIP
  p24) and the Reader's crystal to ±10 ppm (PSIP p23).
- **The host orders the block along the floorplan, and a tag wakes once for
  each run of its set.** A single slot between two of the set's slots is
  listened across: 0.4 ms at 5.8 mA (2.32 µC) costs less than a second
  start-up (2.795 µC, `parts-and-toolchains.md` § The battery). Across two
  slots or more the tag sleeps and wakes again, which costs less, and the
  start-up's 0.665 ms (165 µs and 200 µs, PSIP p23, then the 0.3 ms set-up)
  fits in two slots' 0.8 ms. INFERRED, each scenario's order:
  - **the ward:** one exit, then each stretch with two of its rooms either
    side of it (room, room, stretch, room, room), stretch by stretch along
    the corridor, then the other exit. A room's set is one run: two slots
    beside its stretch, or three one room away. A stretch's set is two runs
    at an end of the corridor and three in its middle (its own group, and
    the stretch on each side);
  - **the main hall:** its twelve areas, 4 × 3 of about 10 m, column by
    column, three areas to a column. An inner area's set, the three columns
    around it, is one run of nine slots. Every other area's is one run of at
    most eight, the areas between included. [2]'s second Reader keeps the
    hall one block (§ The conference hall's second Reader, below);
  - **the conference's other rooms:** each room's set, its slot and its two
    neighbours', is priced as three wakes, the most three slots can need
    wherever they sit, so its figure needs no order;
  - **the warehouse:** the bays aisle by aisle, in order along each aisle,
    so an item's set (its bay and the bay on either side) is one run of
    three. At a Reader's edge the bay across it belongs to the next
    Reader's network, so a second Field Generator in that bay follows this
    Reader's network, its slot next to the edge bay's, and the run of three
    holds there too (§ A Reader's edge, below).
- **The tag hears the Reader's beacon when it needs it:**
  - **after each check-in.** A check-in follows a listen, in that frame's
    reply slot, and the tag then hears the next frame's beacon for its
    reply's acknowledgement and anything addressed to it. That is one more
    wake for each check-in, priced in `parts-and-toolchains.md` § The
    battery;
  - **when the beacon carries news.** Each Field Generator's slot repeats a
    count, which the Reader steps whenever its beacon carries a command (for
    a tag, a type or all tags: `original-system.md` § Three ways to
    address) or a change to the hop order. A tag that sees the count change
    hears the next beacon, at most 0.25 s later. ASSUMED: the count is one
    byte, 27 µs at 300 kbit/s, inside the slot's ASSUMED 0.4 ms. Each step
    costs every tag under that Reader one wake for the beacon, 8.77 µC, at
    its next listen (`parts-and-toolchains.md` § The battery). INFERRED,
    what that allows (added by plan Story 2.1.15, from the Sprint 4
    review's Minor 2):
    - **at the worn tags' 1 s and 2 s listens,** a step every 2.6 s would
      use the whole of the nursery room tag's margin, and every 2.8 s the
      hall badge's. Commands come far less often, so this study does not
      price them there;
    - **in the warehouse,** a tag listens 7,200 times a day, not 86,400,
      and the cold-chain tag's margin is the thinnest. Its Reader therefore
      steps the count at most once in 5 minutes, and a command waits for the
      next step: it reaches a tag within about 5 minutes and 12 s.
      `parts-and-toolchains.md` § [27] prices that bound, 288 wakes a day.
      A step a minute would cost 6.4 mAh over five years, and leave the
      cold-chain tag 1.06 of them;
    - **the day's inventory waits for the step too,** since a roll call is
      a command (§ Anti-collision), and it still finishes inside [21]'s
      10 minutes (added by plan Story 2.1.16, from the Story 2.1.15 review's
      Minor B). The step comes within 300 s. Each tag hears it at its next
      listen, within 12 s, and the next beacon within 0.25 s. Each Reader's
      share of [19]'s 100,000 items, 8,333 to 11,111 among [20]'s 9–12
      Readers, then answers at [21]'s 200 a second in 42–56 s, every
      Reader's roll call at once in the US. That is 354–368 s, about
      5.9–6.1 minutes. In the EU, Readers that share a channel take turns,
      and the inventory takes 478.9–534.5 s, about 8.0–8.9 minutes, the
      planning figure (§ Channels; the rebuild spec's decision 3b, written in
      by plan Story 2.1.18). A reply missed in the pass is
      retried in a later frame's spare slots of the same roll call
      (§ Anti-collision), so it waits for no second step.

    The downlink that names a listen set needs no count, since a tag that
    has just reported a change listens for it (above);
  - **when it has lost its way.** A tag with no set, or one that has heard
    none of its set in two listens, hears the beacon and the whole block in
    one window (above).
- **In the United States,** a tag follows the hop without the beacon
  (§ Channels).
- **What it costs:**
  - a command reaches a tag at its next listen and the beacon after it: at
    most 1.25 s at a 1 s listen, where the old layout took 1 s (§ Does
    ActiveWave's approach still win). In the warehouse it waits for the
    count's next step as well, so it takes up to about 5 minutes and 12 s,
    and the day's inventory finishes in about 6.1 minutes at most in the
    US, and 8.9 in the EU, where Readers that share a channel take turns,
    inside [21]'s 10 (above; § Channels);
  - one more wake for each check-in;
  - no frame time: the block keeps its length (§ Anti-collision).
- **What it leaves unchanged:** [13]'s 3.0 s, [17]'s pairing and [22]'s
  25 s, since a tag still hears its whole set at every listen. The exit's
  answer in every frame ([13], [17]) is unchanged too.

**Weighed against the review's two ways** (plan Sprint 4's D3). INFERRED,
for the nursery's room tag at its 1 s listen, with its one neighbour, its
stretch:

| Layout | Listening | [18], one room away from its stretch | The ward's frame time | Its cost |
|---|---|---|---|---|
| **The beacon skipped (chosen)** | 8.13 µA beside its stretch; 10.45 µA one room away | 1.24 years (`parts-and-toolchains.md` § Each worn tag's year) | none added | a command waits up to 0.25 s more for the beacon; one more wake for each check-in |
| A short sync before each group of slots | 9.29 µA and 11.61 µA (a sync of 0.2 ms, ASSUMED) | 1.15 years | 1.4 ms more: seven groups, its five stretches and two exits | the sync tells a tag nothing a Field Generator's slot does not, so it costs listening and frame time for no gain |
| The second wake, charged | 16.73 µA (the review's) | 0.87 years: **unmet** | none added | [18] unmet on paper for every room's tag |

**A Reader's edge: a Field Generator of each network in the bays either
side** (added by plan Story 2.1.15, from the Sprint 4 review's finding 1).
- **The edge.** The warehouse has 9–12 Readers ([20]), each at its own
  frame time and, in the United States, on its own hop (§ Channels). Each
  Reader's Field Generators time their slots from its beacon, in a block
  after it (above). An item's set is its bay and the bay on either side
  along its aisle (the neighbours, above). INFERRED: along a 100 m aisle of
  10 m bays ([19], [22]), with a Reader for every 30–33 m, the aisle
  crosses two or three Readers' edges, so four or six of its ten bays sit
  beside a bay of another Reader. An item there has a neighbour whose slot
  follows another beacon, at another time and on another channel: in the
  US by its own hop, and in the EU because Readers beside each other take
  different channels (§ Channels). Its set cannot be one run in its own
  Reader's block.
- **The choice: a second Field Generator in the bay across each edge.** It
  follows this Reader's network, as any of its Field Generators does, at a
  power set for that bay, and its slot sits in this Reader's block next to
  the edge bay's. The bay's own Field Generator follows the next Reader's.
  The host draws the two as one zone (INFERRED). Every item's set is then
  one run of three in its own Reader's block, at a Reader's edge as at any
  other bay. No device's firmware changes: the tag, the Field Generator and
  the Reader each keep one network.
- **What it costs** (INFERRED):
  - **more Field Generators:** two at each edge, one in the bay on each
    side. A 100 m aisle with two or three edges takes four or six beside
    its ten, about half as many again;
  - **a slot in the block for each:** two for each aisle a Reader covers.
    A block may hold 525 at [21]'s 200 a second (§ Anti-collision);
  - **an item carried across the edge** joins the next Reader once the
    host places it in the far bay, as a new tag joins: in the US, at most
    one hop cycle, 10.5 s, of listening at 5.8 mA, 0.017 mAh. In the EU the
    downlink that places it names the next Reader's channel, and it listens
    there for at most a frame, 0.0004 mAh (§ Channels). Items at rest
    rarely move, so this study does not price it on its own: the wide
    search's bound for each carry covers it (below). An item carried beyond
    the far bay between two listens never hears that bay's Field Generator
    of its own network, and finds the next Reader by the wide search
    (§ A tag carried past a Reader's edge, below).
- **Where more Field Generators are not wanted,** the items at the edge
  take the second row below: five years on a CR2032 still holds on paper
  for both item tags.

INFERRED, for the warehouse item tag at its 12 s listen, with
`parts-and-toolchains.md` § [27]'s inputs and the count's beacon wakes at
their bound (above):

| Way | The edge item tag's listening | [27] on a CR2032: plain, cold chain | A move across the edge | Its cost |
|---|---|---|---|---|
| **A Field Generator of each network at the edge (chosen)** | 1.51 µA, one run of three | 1.21 and 1.10 of five years, as at every bay | 25 s | four or six more Field Generators along a 100 m aisle, each a slot in its block |
| A second wake every fifth listen, for the far neighbour | 1.64 µA | 1.16 and 1.05 | about 2 minutes (2 × 60 s + 1 s) | the tag keeps the next network's time and hop order, which the downlink that names its set must carry |
| The edge's own Field Generator in both blocks | 1.51 µA | 1.21 and 1.10 | 25 s, and none while its two slots meet | it follows two networks, and its two slots slide over each other: declined (below) |
| A second wake at every listen | 1.99 µA | 1.03 and 0.95: **cold chain unmet** | 25 s | as the second row, with five times its wakes |
| The review's second wake, widened as the first (2 × 60 ppm × 12 s) | 2.44 µA | 0.91 and 0.84: **both unmet** | 25 s | as the row above |

- **The second wake's window** (the second and fourth rows; INFERRED) is
  timed from the slot the tag has just heard in its own Reader's block. So
  it widens for the two Readers' crystals, each ±10 ppm (PSIP p23): 2 ×
  20 ppm of the time since the tag last heard the far slot. It widens as
  well for the tag's own RTC across the gap between the two slots, at most a
  frame (2 × 50 ppm × 0.25 s = 0.025 ms). That is 0.48 + 0.025 = 0.505 ms
  at 12 s, the fourth row's, and 2.4 + 0.025 = 2.43 ms at 60 s, the
  second's.
  When the far slot falls inside the tag's own run, the tag hears it in the
  next frame. The review's figures (163.8 and 176.4 mAh; 138.3 and 151.0)
  leave out the count's wakes and count years of 365.25 days, where
  § [27] counts 365. Its second pair widened the window for 20 ppm × 12 s
  on one side only.
- **One Field Generator in both blocks is declined** (INFERRED). It would
  send its slot in each block, but the two networks' frames slide against
  each other by up to 5 µs a frame (20 ppm × 0.25 s). Its two slots, with
  the 0.3 ms retune between them (the ASSUMED set-up,
  `parts-and-toolchains.md` § The battery), then meet while their starts
  lie within 0.4 + 0.3 = 0.7 ms either way: 1.4 ms of each 250 ms frame,
  0.56 % of the time. At the crystals' worst, 20 µs a second, that is
  1.4 ms ÷ 20 µs = 70 s, once in 250 ms ÷ 20 µs = 12,500 s, about 3.5
  hours. Two crystals that agree more closely meet less often and for
  longer, and two that agree exactly may meet for good. While they meet,
  the far items lose that neighbour, and a move across the edge goes
  unseen. Nothing bounds that.
- **A CR2450 at the edge** meets [27] by any of the five: 2.01 times the
  need on cold chain even with the review's second wake (INFERRED). That
  wake adds 2.44 − 1.51 = 0.93 µA over 43,800 hours, 40.7 mAh, to the
  cold-chain tag's 156.2 mAh on a CR2450 (`parts-and-toolchains.md`
  § [27]), and 396 ÷ 196.9 = 2.01.

**A tag carried past a Reader's edge: the wide search** (added by plan Story
2.1.18, from the Story 2.1.17 review's Minor G, on the rebuild spec's
decision 3c, `docs/superpowers/specs/2026-10-30-rebuild-design.md` § 5.3 at
`168bebb8`: [22] binds a tag carried past a Reader's edge, and with it no
false [25] alarm, and this story designs and prices the search that meets
it).
- **The case.** ASSUMED: a forklift moves at a few metres a second, so in
  the 12 s between an item tag's listens it can carry the item more than a
  bay past its Reader's edge. The item then never hears the second Field
  Generator in the bay across the edge (§ A Reader's edge, above). It hears
  none of its set, so after two missed listens it is lost (§ How a tag hears
  a Field Generator it has not heard before), and its next listen, to its
  own network's whole block, hears none of that network's Field Generators
  either. As the study stood, it
  then searched as § Channels states. In the EU it was found up to 2,083 s +
  4 × 220.1 s = 2,964 s, about 49 minutes, after the loss. In the US it met
  another network only by chance until its window reached the hop cycle,
  about 24 hours. [22] asks a move shown within 5 minutes. [25] alarms at
  the fourth missed 15-minute check-in, 60 minutes after the last one made,
  so a loss that began more than 60 − 49.4 = 10.6 minutes after a check-in
  could alarm before the EU tag was found (INFERRED, the review's
  arithmetic, reproduced here).

  Such a tag may still hear its old Reader's beacon (INFERRED, added by
  plan Story 2.1.20, from the Story 2.1.18 review's Minor H). A Reader's
  edge lies 15–16.5 m from that Reader, half of the 30–33 m between
  Readers (§ A Reader's edge, above), so a tag one or two 10 m bays past the
  edge stands 25–36.5 m from its old Reader. § Range's EU Reader-to-tag row
  closes at 30 m with 24.4 dB to spare; at 36.5 m that is
  20·log₁₀(36.5 ÷ 30) = 1.7 dB less, 22.7 dB left before the racking,
  which this study does not price. The US row leaves 26.8 dB at 30 m at the
  same 20 dB body loss, more again. Each of that network's Field Generators
  sends at a power set for its own bay, so the tag hears none of them.
- **The choice: at a site of more than one network, a lost tag's first two
  searches are wide.** They start once its listen to its own network's
  whole block hears none of that network's Field Generators, whether or not
  it hears the beacon (plan Story 2.1.20). A beacon heard alone names no
  zone, so a tag that waited for silence would stay an unplaced tag of its
  old network, with no zone for the host to show and [22] unmet for that
  carry. Each listens long enough to meet any network in reach, whatever
  that network's time (INFERRED):
  - **in the EU,** a whole frame on each of the site's four channels, its
    own first: 4 × (2.795 µC + 5.8 mA × 250 ms) = 5.81 mC, 0.0016 mAh, in
    about 1 s. A network keeps its channel and sends its beacon and its
    block in every frame, so a frame's listen on its channel hears it
    (§ Channels);
  - **in the US,** one channel for up to one hop cycle: 2.795 µC + 5.8 mA ×
    10.5 s = 60.9 mC, 0.0169 mAh. Every network visits every channel once a
    cycle (§ Channels), and the listen stops at the first frame whose block
    carries a Field Generator it hears. It passes over a beacon whose block
    carries none, its old Reader's among them; every other network in reach
    still visits within the cycle, so the bound stands (plan Story 2.1.20).
    The Reader tells a tag when it joins whether its site has other
    networks, beside the hop order, and the second wide search takes
    another channel.

  The tag joins the network whose Field Generator it hears, never one whose
  beacon alone it hears (plan Story 2.1.20): in the EU, on the first of the
  site's channels, its own first, whose frame carries one; in the US, at the
  first such frame on its channel. It joins in that frame's joining window
  (ASSUMED: the window follows the block, in the order § Anti-collision
  lists a frame's shares; were it before the block, the tag would join a
  frame later, 0.25 s), as a new tag joins, and the host places it. If
  neither wide search hears a Field Generator, the tag searches as
  § Channels states, at 6.6 µA. The ward and
  the conference keep one network each (§ Channels), so their lost tags
  make no wide search, and the worn tags' years are unchanged.
- **Its time** (INFERRED, at the worst): two missed listens, 24 s; the
  whole-block listen that hears none of its network's Field Generators,
  12 s; the wide search, about 1 s
  in the EU and at most 10.5 s in the US; joining in that frame, 0.25 s; a
  second hearing at the next listen, 12 s; then 1 s for the slot, the
  Reader and the host. That is about 50 s in the EU and 60 s in the US
  (24 + 12 + 1 + 0.25 + 12 + 1 = 50.25; 24 + 12 + 10.5 + 0.25 + 12 + 1 =
  59.75), inside [22]'s 5 minutes, or 63 s and 82 s if only the second wide
  search hears a Field Generator. They hold as well for a tag that still
  hears its old Reader's beacon, whose wide search starts at the same
  listen (plan Story 2.1.20). The tag makes its next check-in through the
  Reader it has joined, so a carry delays at most one check-in, and [25]
  raises no alarm for it. A tag that neither wide search finds heard no
  Field Generator on any channel. If it hears no beacon either, it has left
  the Readers' reach or lies shielded, which is [25]'s case; if it still
  hears its old Reader, it lies beyond every Field Generator's bay and keeps
  checking in through that Reader, so [25] stays silent for it.
- **Its cost** (INFERRED): at most two wide searches for each carry that
  loses a tag, 11.6 mC, 0.0032 mAh, in the EU and 121.8 mC, 0.0338 mAh, in
  the US. That bound covers the join of an item carried into the far bay as
  well (§ A Reader's edge, above). [27] counts no carries, and items at rest
  rarely move, so this study gives the carries each cell allows over five
  years, from `parts-and-toolchains.md` § [27]'s needs at a 12 s listen
  (five years of 365 days, 1,825 days):

  | Cell, at a 12 s listen | Five years' need: margin (mAh) | Carries a tag may lose, EU | Carries, US |
  |---|---|---|---|
  | CR2450 variant, plain | 143.5 of 396: 252.5 | 78,211, about 43 a day | 7,463, about 4.1 a day |
  | CR2450 variant, cold chain | 156.2 of 396: 239.8 | 74,278, about 41 a day | 7,087, about 3.9 a day |
  | CR2032 core, plain | 124.2 of 150: 25.8 | 7,991, about 4.4 a day | 763, one every 2.4 days |
  | CR2032 core, cold chain | 136.9 of 150: 13.1 | 4,058, about 2.2 a day | 387, one every 4.7 days |

  Each count is the margin divided by the carry's bound, unrounded, for
  example 252.5 ÷ 0.033835 = 7,463. At a carry a day for five years, the CR2450
  variant needs 205.2 mAh in the US, 1.93 of five years, or 217.9 mAh, 1.82,
  on cold chain. The CR2032 core needs 185.9 and 198.6 mAh there, 0.81 and
  0.76 of five years, short. In the EU a carry a day leaves the CR2032 core
  1.15 and 1.05 of five years.
- **The CR2032's limit, decided by Omid** first-hand in the terminal of the
  session "Mac Closer", between its `date -u` reads of 22:11:39Z and
  22:24:12Z: "State the limit; CR2450 if carried". The item tag is the
  CR2450 variant (the rebuild spec's decision 5a). The CR2032 core, kept for
  items that cannot take the larger tag, keeps five years on paper in a US
  warehouse while its item is carried and lost at most once every 2.4 days,
  or 4.7 days on cold chain. An item carried more often takes the CR2450
  variant. One that cannot, and is carried that often in a US warehouse,
  keeps fewer than five years on paper: 0.81 of them, or 0.76 on cold chain,
  at a carry a day. No requirement number moves.
- **Weighed against the other ways** (INFERRED):
  - **an aimed search, the rebuild spec's example:** the downlink that
    places a tag at an edge bay hands it the next Reader's channel, or in
    the US its hop order and frame offset, so the search listens where that
    network will be. For that one carry it costs less: a frame on one
    channel, 1.45 mC, in the EU, and in the US a window of about 36 ms where
    the wide search listens for up to 10.5 s. But it serves only a carry
    into the next Reader along the aisle, not one to another aisle or a dock
    door, which still needs the wide search. In the US the offset also ages,
    by up to 20 µs a second between two Readers' crystals (each ±10 ppm,
    PSIP p23): past a frame's 0.25 s within 12,500 s, about 3.5 hours, and
    by ±18 ms over a 15-minute check-in. So it would be renewed after each
    check-in, from a host that reads both Readers' frame clocks to a few
    milliseconds, which no study has measured. Not chosen. The build may add
    it ahead of the wide search at edge bays, to save the US carry's cost;
  - **holding [25]'s alarm while a carried tag may still be searching:** no
    battery cost, but the move stays off the screen for up to 49 minutes in
    the EU and a day in the US, where [22] binds. Declined;
  - **a wide search at every site:** at the ward and the conference, a lost
    tag's whole-block listen already meets its own network, the only one
    there. Not needed.

**The conference hall's second Reader: on the first Reader's network**
(added by plan Story 2.1.16, from the Story 2.1.15 review's Minor C). [2]
places "a second reader" in the main hall to cover the part the first does
not reach (`requirements.md` [2]; § Range).
- **The choice: it listens on the first Reader's network.** It times its
  frame from the first Reader's beacon, as a Field Generator does, sends
  no beacon and has no block, and passes every reply it hears to the host.
  The host keeps one copy of each by the tag's `seq`
  (`software-stack.md` § The fields), as it does for a reply heard by both
  the Reader and a Field Generator. The hall stays one block on one
  channel, so an inner area's set stays one run of nine, and [9] holds by
  22 % as priced (`parts-and-toolchains.md` § Each worn tag's year).
- **It serves [2] where [2] is weak** (INFERRED). A tag's reply is the
  weaker path, by 5 dB in the US and 2.2 dB in the EU (§ Range), and the
  second Reader is a second receiver for it. The first Reader's beacon
  still reaches the whole hall. From the middle of a wall, no point of a
  40 m × 30 m hall is more than √(40² + 15²) = 42.7 m away, 3.1 dB more
  loss than at 30 m (20·log₁₀(42.7 ÷ 30)). In the US the beacon's 5 dB
  covers that. In the EU it leaves the far corner 24.4 − 3.1 = 21.3 dB of
  margin against a reply's 22.2 dB at 30 m, so [2]'s range test decides
  there, as it does for the reply.
- **What it costs** (INFERRED):
  - **one more role for the Reader:** the Field Generator's timing beside
    the Reader's receive path, on the same module (§ Zones);
  - **the acknowledgement's path.** A reply that only the second Reader
    hears is acknowledged in the first Reader's next beacon once the host
    passes it on. If it comes too late, the tag retries in the next frame's
    spare slots (§ Anti-collision). If every check-in of a badge were
    retried once, its year would rise by the check-ins' 3.9 mAh: 126.4 mAh,
    1.19 years in an area of the hall. The build measures the path.
- **Its own network is declined** (INFERRED). It would put a Reader's edge
  across the hall:
  - **with no Field Generator added,** an area beside the edge hears six
    slots in its own block and, at a second wake, three in the other's:
    (2.795 + 5.8 × (2.4 + 0.24)) + (2.795 + 5.8 × (1.2 + 0.105)) =
    18.107 + 10.364 µC every 2 s, 14.24 µA against the run of nine's
    12.53. The second wake's window is 2 × 20 ppm × 2 s + 0.025 ms =
    0.105 ms (§ A Reader's edge). That is 137.4 mAh a year: 1.09 years, so
    [9] holds by 9 %, not 22 %, and 0.86 years at the 117.5 mAh floor, not
    0.96. The warehouse's route keeps the 22 % with six more Field
    Generators, three in each column beside the edge;
  - **a badge that crosses the edge** joins the other Reader each time, as a
    new tag joins: in the US up to one hop cycle, 0.017 mAh. At ten
    crossings a day (ASSUMED), that is up to 61.7 mAh a year, half the hall
    badge's year again: 184.2 mAh even on the warehouse's route, 0.81
    years, and [9] unmet. Items at rest rarely cross an edge; people at a
    conference do;
  - **in the EU, a second channel** (§ Channels).

  It would add reply slots the hall does not need: one network gives 1,296
  a second against [3]'s 1,000 (§ Anti-collision).

**The battery this costs** is priced in `parts-and-toolchains.md` § Each
worn tag's year and § [27]: Story 2.1.4 replaced this study's ASSUMED
inputs with the datasheets' figures, Story 2.1.10 priced the listen set
above, and Story 2.1.12 the layout of a listen. This study's own first
estimate, 108.6 mAh a year for [18]'s tag
from its ASSUMED inputs, is superseded by those figures.

The listening dominates, as the requirements predicted (`requirements.md`
§ The arithmetic). Where the figures break a battery number, the parts
study names the routes. One of them keeps the zones another way: the Field
Generators listen and the tags only transmit.

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
  carries "Contains FCC ID: ZAT-1312PSIP-2" (§15.212(a)(1)(vi)(A)), or
  ZAT-1312PSIP-3 if the sitting's read of both grants finds that one
  covering the chosen PHY (§ The radio chosen).
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
- **Story 2.1.6** prices the module's assembled-board routes
  (`tiered-cost.md` § The build routes, 2026-10-28).
- **Story 2.1.7** decides, with this document's choice as its default. Its
  sitting reads both FCC grant pages, ZAT-1312PSIP-2 and -3, in a browser
  (§ The radio chosen).
- **The build initiatives** (added by Story 2.1.10): the beacon's and a
  Field Generator slot's lengths, measured; the floorplan's neighbour
  links; and the guides' setup walk that checks them (§ Zones). Added by
  Story 2.1.12: the block's order along each floorplan, the count each
  Field Generator's slot repeats, and the hop order a tag is given when it
  joins (§ Zones, The layout of a listen). Added by Story 2.1.18: the wide
  search a lost tag makes at a site of more than one network, and, in the
  US, the word a tag is given when it joins that its site has other
  networks (§ Zones, A tag carried past a Reader's edge). Added by Story
  2.1.20: the search's start, a whole-block listen that hears none of the
  tag's network's Field Generators, beacon or not, and the join to the
  network whose Field Generator it hears (the same section); and the [21] test
  on two EU Readers that share a channel, which decides whether they roll
  call at once (§ Channels).

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
  address) rides in the Reader's beacon, and the Field Generators' slots
  flag it (§ Zones, The layout of a listen). A tag acts on it at its next
  listen and the beacon after it, at most 1.25 s later at a 1 s listen
  (1 s before plan Story 2.1.12). Tags that send on their own timers, Chorus's
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
