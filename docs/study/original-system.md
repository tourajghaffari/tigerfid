# How the original worked

The second document of the design study (plan Story 2.1.2). It describes
the ActiveWave system of 2001–2013 from its own records, so every option in
the rest of the study has a reference to beat. It covers:

- the radio link;
- the anti-collision scheme;
- the zone mechanism;
- the alarms and I/O;
- the hardware of each milestone device (the Reader, the Field Generator,
  and the cardTag, compactTag and sealTag).

The records are the repo's own history at the tag `activewave-2018`. Each
claim cites the file it was read from, as `activewave-2018:<path>`, or as a
short name from § The records read, with the record's own section number. A
claim marked **INFERRED** was reasoned, not read: no record states it. A
claim marked **MEASURED** was computed by this study from a record's own
figures. The old website's pages are cited as the reference corpus's capture
(`activewaveinc/<path>`); they are the company's claims, not records at the
tag. Nothing here is copied into the rebuild: this is a description, and the
rebuild's own design comes from Stories 2.1.3–2.1.7.

## The records read

Read on 2026-10-27 in the reference folder recreated from the tag (it
verified at 291 rows). The Word documents were read through `antiword`, the
PDFs through `pdftotext`. The spreadsheets and Protel design files are
binary, so their part names were read as text strings. A string proves that
a part is named in the file. It proves which board position the part takes
only where the row names one, as a netlist or a pick-and-place file does.

**The protocol and API documents** (all under
`activewave-2018:api/documents/`):

| Short name | File | What it is |
|---|---|---|
| P06 | `Protocol20061015.doc` | "Protocol Definitions for the ActiveWave RFID System", version 4.2, 15 October 2006: the host-to-reader protocol |
| P08 | `Protocol-tg-07092008.doc` | the protocol's version 20, 8 July 2008 (its footer says Rev 2.0, 8/8/2008): the device-to-device packets, tag to reader and field generator to tag |
| API-2007 | `AW_API_V39.doc` | the host API, version 0.339, 27 June 2007 |
| API-2013 | `AW_API.doc`, `AW_API.pdf` | the host API, version 40, 2 September 2013 |
| — | `VerRelease.doc` | the API's release notes, versions 20–30 |
| — | `tempLog.doc` | temperature logging, new in the .NET API 8.40 |

The API file names mislead: `AW_API_V39.doc` is the **older** document, not
the newer.

**The hardware records** (under `activewave-2018:`):

| Short name | File |
|---|---|
| R6NET | `protel-files/reader/reader-usb/rev6/project-outputs-for-reader-103-usb-rev6/Reader 103 USB Rev6.NET` (the Reader's netlist) |
| R6S2–R6S4 | `protel-files/reader/reader-usb/rev6/Sheet2.schdoc`, `Sheet3.schdoc`, `Sheet4.schdoc` |
| RDR-2012 | `bom-files/reader/medium-reader/MedRDR-11-25-2012-Rev6.xls` |
| TRX-PNP | `protel-files/transceiver/trx-rev19/project-outputs-for-101-rev19/Pick Place for TRX-101 Rev19.csv` |
| TRX-SCH | `protel-files/transceiver/trx-rev19/TRX-101R17.pdf` (schematic prints, Rev 17, dated 2/10/2009) |
| TRX-2012 | `bom-files/transceiver/TRX-14X-Rev19.1 10-22-2012.xls` |
| FGN-BOM | `protel-files/field-generators/standard-fgen/rev2.2/Backup of BOM for Field Generator 22.csv` (dated 6 January 2005) |
| FGN-SCH | the Rev 2.3 sheets in `protel-files/field-generators/standard-fgen/rev2.3/` |
| TAG-PNP | the `Pick Place for Tag10x Rev…csv` files under `protel-files/tags/` |
| T201-PNP | `sealtag/protel-files/seal-tag/2012-04-09-rev4/project-outputs-for-201-rev4/Pick Place for Tag201_Rev4.csv` |
| T201-BOM | `bom-files/tag/Tag-201-Cost-05-31-2013.xlsx` |
| FW | `sealtag/software/seal-tag-firmware/release code/Release_400_LSE-BS/BLocker/` (the sealTag firmware, version 4.0.0, files dated 10/25/12) |
| TUNE | `documentation/optimal_tag_values.md` |
| TIPS | `documentation/troubleshooting_tips.md` |

**Not read:** `software/smart-tracker/SmartTracker  SaniFaucet.msg`, which
stays closed until Omid rules on it (the plan's ASK-OMID A1). The host
applications' source (Smart Tracker and the Programming Station) was not
read: Story 2.1.5 reads it for the software stack.

## The names: products and boards

The website sold products by name, and the records number the boards. The
records map them as follows.

| Product | Board | Read from |
|---|---|---|
| cardTag | Tag 101 | "Card Tag REV17.2Board" (`bom-files/tag/Tag-101 5-10-2011 REV 17.4.xls`); "cardTag, 101" (TUNE) |
| compactTag | **Tag 102** | "Compact Tag REV20.0 Board", "Compact Tag REV21.0 Board" (`bom-files/tag/Tag-102 10-16-2012 REV 21.0.xls`); "compactTag, 102 Rev 17" (TUNE) |
| miniTag | Tag 103 | "Mini Tag REV18.0 Board" (`bom-files/tag/Tag-103 05-10-2011 REV 20-1.xls`); "miniTag, 103 Rev 20" (TUNE) |
| jumboTag | Tag 105 | "Jumbo Tag REV18.3 Board", "Jumbo Tag REV20.0 Board" (`bom-files/tag/Tag-105 5-30-2012 REV 20.xls`); "jumboTag, 105 Rev 17" (TUNE) |
| sealTag | Tag 201 | "TAG-201 Rev4" (T201-BOM) |
| Standard Reader | Reader 103, the "Medium Reader" | "RDR-ASM-103" with "Medium Reader Rev. 6" (RDR-2012). INFERRED that the website's Standard Reader is this board: its I/O matches the datasheet's (four LEDs, two relays, two contact inputs, a motion input, Ethernet). |
| Standard Field Generator | FGN 101 | "FGN ASM-101", "Field Generator Rev 2.2NC" (`bom-files/field-generator/standard-fgen/FGN 101 089-02-10 REV2.3NC.xls`) |

The Protel folders agree. `protel-files/tags/credit-card-tag/` holds
Tag 101, `medium-tag/` holds Tag 102, and `small-tag/` holds Tag 103.

**Two corrections to the onboarding spec** (hub spec
`2026-10-26-tigerfid-onboarding-design`, Context and Appendix B):

- The compactTag is Tag **102**. The "Tag 103 pick-and-place" the spec
  quotes is the miniTag's. The two boards share nearly every part, so the
  spec's parts list holds for both.
- The sealTag of 2012 is **not an active radio tag** (§ The sealTag). The
  spec's Appendix B gave it the same radio as the tiewrapTag. No record at
  the tag names a tiewrapTag; the capture holds only a PDF of that name
  (`activewaveinc/docroot/pdf/TiewrapTagv10.pdf`).

## The system at a glance

```
            RS-232, Ethernet or USB
   Host PC ─────────────────────────── Reader ── Transceiver board
  (Windows DLL)                          │   433.92 MHz out, 916.5 MHz in
                                         │
                            Smart Field Generator (configured through the Reader)
                                         │
   Standard Field Generator  ── 433.92 MHz out only, configured over its own RS-232
                                         │
                       Tags: wake at 433.92 MHz, reply at 916.5 MHz
```

- **The Reader** talks to the host and carries a Transceiver daughterboard
  for its radio (§ The Reader; § The Transceiver).
- **A Field Generator** wakes the tags near it. The Standard model only
  transmits, at 433.92 MHz. The Smart model is configured through a
  Reader: "Configuration of the Reader, tag, and Smart FGen are all done
  via either an RS-232 or Ethernet connection to the Reader" (P06 §2.3).
  The Standard model is configured over "a separate RS-232 connection from
  the Host directly to the FGen" (P06 §2.2).
- **Tags** listen for 433.92 MHz commands and reply at 916.5 MHz (§ The
  radio link).
- **Scale:** "Each Host can have up to 255 Readers" and "Each Reader can
  have up to 255 FGen or Smart FGen" (P06 §2.4.5).
- **A Reader can act as its own Field Generator.** P06 §3.1.4 lists Reader
  Type "6 Field Generator Mode". P08 §3.3.1: "If the Field generator
  address is equal to zero, then it means the reader has field generator
  option hardware."

## The radio link

### Two bands, one each way

- **Down, to the tags:** 433.92 MHz. P08 §3.5.2: "the tag woke up by 433.92
  MHz RF field awakens the tag". P06's system figure labels the link "433
  MHz RF".
- **Up, to the Reader:** 916.5 MHz. P06's figure labels it "916 MHz RF".
  P08 §2.1: "the simplest of which is the 916 MHz tag detector, serially
  connected via RS-232 to a host PC".
- **Regional variants exist in the hardware, not the protocol.** The
  Transceiver's 2012 BOM names "868.95MHz", "927.2MHz" and "867 MHz"
  besides "916.5MHz", and a build "for 868 MHz" (TRX-2012). The Tag 101 BOM
  lists SAW resonators for the same frequencies. No protocol record names
  868 or 927 MHz.
- **The Reader also transmits at 916 MHz** to reach a Smart Field
  Generator: "by enabling the 916 MHz transmitters" (P08 §3.1.19). The
  Transceiver carries a 916.5 MHz transmitter for it (§ The Transceiver).

### Data rate, modulation and line coding: no record states them

No protocol or API record gives the radio's bit rate, its modulation or
its line coding. The only rates in them are serial ones:

- P06 figure 2: "RS-232 (9600 Baud) cable used for configuration only"
  (the Standard Field Generator's cable);
- API-2013 §2.27: "The communication baud rate should be set to 9600 when
  calling this function";
- API-2013 `rfOpen` takes the reader's serial rate as a parameter
  ("Baud rate in bits per second").

The hardware gives two clues:

- **The tag's transmitter** is one RF transistor (NE85619) whose frequency
  is held by a 916.5 MHz SAW resonator, keyed by a "TX DATA" line (TAG-PNP;
  the Tag 105 Rev 21 schematic prints,
  `protel-files/tags/credit-card-tag-tag-with-battery-holder/rev21/schematic/Schematic Prints.pdf`).
  INFERRED: on-off keying, the usual scheme for a SAW-held oscillator keyed
  by a data line.
- **The Reader's receiver** has a 10.7 MHz IF through a "FILTER 10.7MHZ
  330KHZ BANDWIDTH" (TRX-2012). INFERRED: the bit rate sits well inside that
  bandwidth, under about 100 kbit/s.

The requirements' channel arithmetic assumed 250 kbit/s
(`docs/study/requirements.md` § The arithmetic). No record confirms that
figure. INFERRED: the original's rate was lower, so its channel carried
fewer replies a second than that arithmetic allows.

### The packet

One packet format runs over every link: "This protocol will be used over
the RS-232-connected elements of the system as well as over the RF links"
(P08 §1). Its fields, in order (P06 §2.4; P08 §2.5):

| Field | Size | Notes |
|---|---|---|
| Preamble | 1 byte | `0x7E` |
| Command | 1 byte | "All corresponding responses will use the same command ID with the exception of a 1 bit in the msb location" (P06 §2.4.2) |
| Length | 1 byte | "the number of bytes in the packet, following the length byte, and not including the 2-byte Cyclic Redundancy Check (CRC) field" (P06 §2.4.3) |
| Packet Type | 1 byte | a 2-bit send counter, a 3-bit source type and a 3-bit destination type |
| Source address | 1–4 bytes | "Source Device Address (tags 4; other devices 1-2)" (P08 §2.5) |
| Destination address | 1–4 bytes | the same sizes |
| Data | varies | |
| Packet ID | 1 byte | "every response must have the same packet ID as used by the corresponding command" (P06 §2.4.6) |
| CRC | 2 bytes | CRC-16, below |

**A tag's reply on the air is shorter: 9–11 bytes.** P08 §3.5 drops the
Length and Packet Type bytes from the tag's packets:

- 0x70 carries the command, the 4-byte tag ID, Status Byte-1 and the CRC:
  9 bytes with the preamble (P08 §3.5.1);
- 0x71 adds the reader address: 10 bytes (P08 §3.5.2);
- 0x73 adds the reader address and the Field Generator's address: 11 bytes
  (P08 §3.5.4).

INFERRED airtime, since the bit rate is unrecorded: an 11-byte reply is
88 bits, so about 9 ms at 10 kbit/s and about 0.9 ms at 100 kbit/s, before
any lead-in.

**The CRC is CRC-16 in its X.25 form.**

- P08 §2.5.8: "CRC-16 is the chosen method". It adds that "If the residue
  is a specific 16-bit hex value (0xF0B8) at the end of this, the packet can
  be relied upon for accuracy."
- P06 §2.4.7: "This CRC is generated using the standard CRC-CCITT standard,
  with initial seed 0xFFFF."
- **MEASURED:** this study computed the reflected polynomial 0x8408 with
  seed 0xFFFF, a final inversion and the low byte sent first, over the
  records' three test vectors. All three match, and so does the residue:

  | Record | Bytes | CRC computed | CRC printed |
  |---|---|---|---|
  | P06 §2.4.7 | `7E 01 04 0A 01 02 01` | `6D 96` | `6D 96` |
  | P08 §2.5.8 | `7E 04 80 11 81 01` | `86 50` | `86 50` |
  | P08 §2.5.8 | `7E 74 80 11 81 01` | `75 45` | `75 45` |

  Over a whole packet, CRC included, the residue is `0xF0B8`, as P08
  states.
- **Conflict.** P06 covers every byte "from the start byte of 0x7E to the
  Packet ID byte". P08 says the code is "for RF send, not RS232" and that
  "the preamble zero byte is not included in the CRC generation, and that
  the flag byte is". The test vectors only match with `0x7E` included.
  INFERRED: P08's "preamble zero byte" is a separate RF lead-in sent ahead
  of `0x7E`, which no record defines.

## Wake and addressing

### A tag listens on a duty cycle

- **Every 660 ms when enabled, every 4 s when disabled.** P06 §3.2.2: "An
  enabled tag responds to all tag commands and listens for these commands
  every 660ms." and "A disabled tag ignores certain commands (including
  Call Tag) and only listens every 4 seconds in order to conserve battery
  life."
- P08 §2.5.9 makes the period a configuration bit: "Change the 433 MHz
  Receiver Wake up time to normal period" or "to extended period". It gives
  no figures for either.
- **The hardware fits.** The tag's 433 MHz receiver is powered through a
  TC1070 regulator, which the PIC switches by an "ENABLE_RCVR" line (TAG-PNP;
  the Tag 105 Rev 21 schematic prints). INFERRED: the PIC powers the
  receiver for each listen and cuts it between listens.
- **The wake signal itself is not described.** INFERRED: the Reader or
  Field Generator keeps its 433.92 MHz transmitter on for a set "transmit
  time" (P06 §2.4.10) that spans at least one listen window.

### Three ways to address

- P06 §3.2.1: "a specific tag (specific Tag ID and specific Tag Type)", "a
  specific tag type (any Tag ID), or" "all tags (any Tag ID and any Tag
  Type)".
- Tag types include "001 = Access Tag" and "010 = Inventory Tag" (P06
  §2.4.8).
- API-2013 §4.4 adds a range: "Select tags that their ID's are within the
  range."
- Tag IDs are 4 bytes: "The minimum ID field is 32 bits, or 4 bytes" (P08
  §2.2). The factory ID lives in ROM, and a RAM overlay of all zeros falls
  back to it (P08 §3.1.6).

### Check-in: the tag's own timer

- API-2013 §4.5: "An enabled tag will periodically transmit its tag id and
  status without needing an explicit command from the reader."
- The interval counts 330 ms ticks: "these bytes contents will be used as a
  count of the number of 330 millisecond time periods between sends" and
  "The maximum time interval is approximately six hours" (P08 §3.1.6).
- A check-in can be tied to one Reader: "If a specific Reader ID is
  assigned to a tag, then only that Reader will see the tag's auto-send
  packets" (P06 §3.2.8).

### Each wake brings several replies

- "Group Count (GC): The group count is number of times that we have sent
  the Tag ID. Each increment is equal to 2" (P08 §3.1.6). P06 §3.2.4 shows
  a run of eight identical replies, "#1 from tag" to "#8 from tag".
- A status bit marks each new group: "Bit zero of Status Byte-1 will toggle
  when the tag start sending a new group of packets" (P08 §3.5.1).
- A hold-off stops a tag repeating itself: "The time in the field defines
  the time period that the tag must be in the field before sending the
  data to the host again. This time period can be from 1 to 32 seconds"
  (P08 §3.1.6).

## Anti-collision

**A random delay, drawn from one of two windows:**

- P06 §3.2.9: "For the short values, the upper and lower limits are the
  actual milliseconds. For the long values, the limits are the entered
  values multiplied by 20 milliseconds."
- Defaults (P06 §3.2.9): "Short Random Number Period: 200 - 10 (200ms -
  10ms)" and "Long Random Number Period: 89 - 8 (1780ms - 160ms)".
- Each command picks its window: "0 = Short Random Delay; 1 = Long Random
  Delay" (P06 §2.4.8). A Standard Field Generator sets it for the tags it
  wakes: "Random Tag Response Delay 1 bit 0 (short)" (P06 §3.3.2).
- P08 §3.1.18 says when to use each: "When we know there will be a fewer
  tags in the field, then the system should use short period random number
  generator. For example when the field generator turn on in front of
  entrance door. When there are a lot of tags in the field, then we should
  use long period random number generator."
- P08 widens the limits to 16-bit fields ("Short Period Upper Byte" and
  "Short Period Lower Byte") and gives no unit. P06 uses one byte.

**No slots, and no acknowledgement on a broadcast:**

- The Reader waits out the window and reports a timeout: "For all tag
  commands, the Reader will send a timeout packet to the Host when it is
  finished transmitting the command" (P06 §4.1.2). Its example is marked
  "(sent regardless if the tag responded)".
- P08 §3.6.2: "The reader may receive several responds from different tags
  in the field for the broadcast command; therefore it is required to wait
  for certain time before it completes the command."
- P08 §3.5.4 and §3.5.6 show a "Reader acknowledgement for Bi-directional
  tag", and Status Byte-1 carries "1 = Tag is bi-directional". INFERRED:
  only tags that announce themselves as bi-directional are acknowledged,
  never a broadcast's crowd.
- Redundancy comes from the repeated replies of each group (§ Wake and
  addressing).

**No record states a tags-per-second figure.** INFERRED: the scheme is pure
random access, the ALOHA family. A reply survives only if no other reply
overlaps it, and such a channel peaks at about 18% use. The original
therefore traded time for certainty: windows up to 1.78 s wide, and each
reply sent several times. The requirements' [3] (1,000 tags/s per reader)
and [21] (200 tags/s per channel) cannot be met by this family. The
requirements document reaches the same answer by its own arithmetic
(`docs/study/requirements.md`, items [3] and [21]).

## Zones

**No protocol record uses the word "zone"**: it appears in none of P06,
P08, API-2007 or API-2013. The records describe a mechanism, and the zone is
its result:

- **A Field Generator wakes the tags inside its 433 MHz field, and each
  reply names that Field Generator.** P08 §3.3.1 (0x2A): "This packet tells
  particular tag or all tags that are inside the 433 MHz field to wakeup,
  send their tag ID, status Reader, reader address, and the field generator
  address that woke up the tag." The reply 0x73 carries "Field Generator
  Address" (P08 §3.5.4), and P06 §4.2.1's tag report carries "Field
  Generator ID".
- INFERRED: the host maps each Field Generator's ID to a place. That place
  is what the website calls a zone, "a Field Generator plus a motion
  detector".

**What switches a Field Generator on:**

- a motion detector, a timer or the host. P06 §2.4.10 gives "Transmit time
  (1 - 31 seconds)", "0 = Motion detector disabled, 1 = enabled", and "0 =
  Motion detector active low, 1 = active high". P06 §2.4.11's wait time runs
  "1 - 60 seconds, or 1 - 60 minutes, or 1 - 24 hours".
- "If the wait time is zero, then the transmitter is going to get enable
  with motion sensor or command from the host" (P08 §3.1.19). P08 also
  allows "Transmit all the time".
- **Conflict:** P06 allows 1–31 s of transmit time, and P08 "up to maximum
  of 60 second or all the time" (P08 §3.1.19).
- The Standard Field Generator's motion input is a PIR line, labelled
  "~PIR~" on its power-and-PIC sheet (FGN-SCH). The Reader has its own
  "Motion" input (R6S3).

**The zone's size is the field's strength:**

- "The Field Strength is the transmit power from the Reader to the tag",
  and "The lowest setting (shortest range) is 0-short. The highest setting
  (longest range) is 20-long" (P06 §3.1.12): 21 steps in each of two
  ranges.
- In hardware, a GaAs attenuator (AT-113) sits after the 433.92 MHz
  oscillator. On the Field Generator a 10 kΩ potentiometer sets it (FGN-BOM;
  FGN-SCH). On the Transceiver a DS1804 digital potentiometer, driven from
  the Reader, sets it (TRX-SCH). INFERRED: the AT-113 is the strength
  control on both boards, adjusted by the potentiometer.

**Signal strength is reported, not used.** The Reader may add one byte,
"RSSI value" (P06 §4.2.2), switched by "0 = Do not send RSSI byte 1 = Send
RSSI Byte" (P06 §3.1.4). The Transceiver's receiver produces it ("RSSI",
TRX-SCH). No record gives its units or uses it for location.

## Alarms and I/O

- **Tamper, tilt and panic.**
  - P06 §2.4.9: "1 = Tamper switch enabled; 0 = disabled".
  - The 2008 revision added the rest to Status Byte-2. P08's change line
    reads "add tamper, panic, and tilt switch to status byte 2".
  - API-2013 §4.5: "Indicates the tag's tamper switch status. A true value
    means the tamper switch has been activated."
  - The protocol's bit says "enabled" (a setting) while the API's says
    "activated" (an event). The records do not settle which the bit means.
  - The tags' boards carry the inputs: "TAMPER SIG", "TAMPER PWR", "TAMPER
    OPTION" and "PANIC SWITCH" (the Tag 105 Rev 21 schematic prints), and a
    "TILT SWITCH" on Tag 102.
- **Temperature.**
  - The tag reports when asked, when a reading crosses the upper or lower
    limit, or on a period (P06 §4.2.3, the four numbered triggers).
  - The reading is an average: "00 = 4 reads, 01 = 8 reads, 10 = 16
    reads." Its value is "Value / 16 = Celsius", and the period is "(1 -
    127)" minutes or hours (P06 §3.2.13).
  - The 2013 API adds logging into the tag's memory, which "Wraps around if
    tag memory is full if set to True" (API-2013 §4.6; `tempLog.doc`).
  - The sensor is a DS1631 on the I2C bus, under the schematic label "TEMP
    & MEMORY" (TAG-PNP; the Tag 105 Rev 21 schematic prints).
  - **Conflict:** command 0x75 is "Tag Temperature" in P06 and "Emit ID Upon
    Low Tag Battery Detected" in P08.
- **Missed check-in is the host's alarm**, not the tag's or the Reader's:
  "If the host did not receive the tag information upon time out, then it
  will try to search for that tag, because may be the tag has been moved
  from that area … If the host could not found the tag, then the host will
  send an alarm on screen" (P08 §3.1.16).
- **Low battery** is reported even by a disabled tag: "The tag will send
  this packet even if it is disabled" (P08 §3.5.6).
- **LED and buzzer** are switched per command: "0 = LED off; 1 = LED flash"
  and "0 = Buzzer off; 1 = Buzzer beep" (P06 §2.4.8). Release 26.0 made
  both counts "within 0 - 63 range" (`VerRelease.doc`). The tags' boards
  carry a 4 kHz speaker and two LEDs as options (TAG-PNP).
- **Two relay outputs on the Reader.** "This packet activates one of the
  two output relays of the Reader" (P06 §3.1.9); "You can have up to
  two-output relay" (P08 §3.1.2). They are Omron G5V-2 relays, "RELAY DPDT
  2A 5V 70 OHM COIL" (RDR-2012; K1 and K2 in R6NET), driven by NUD3112
  inductive-load drivers (R6S3).
- **Two supervised contact inputs on the Reader.**
  - "Pins-1 and 2 represent digital-input number 1 (DI1), and pins-3 and 4
    represent digital-input number 2 (DI2)" (P08 §3.4.4).
  - They are checked "about ten times a second", and a short or an open
    across a supervised input reports a fault (P08 §3.4.4). "Supervised mode
    uses four states - open, closed, faulty open, or faulty closed" (P06
    §3.1.11).
  - A change sends the host an unsolicited packet (P08 §3.4.4).
  - **Conflict:** P08 puts the inputs on connector J12. On the Rev 6
    Reader, J12 is the 6-pin terminal (Phoenix 1953415) that carries the
    relays, and the inputs come in on J11, a 4-pin terminal (Phoenix
    1937512), through the nets "DI1" and "DI2" (R6NET; R6S3). INFERRED: P08
    describes an earlier board's numbering.

## The Reader to the host

- **The same packets run on every host link.** "The same packets can be
  used over Ethernet with basic serial-to-Ethernet conversions" (P06 §1).
- **The Rev 6 Reader's links** (R6NET; RDR-2012):
  - USB through an FT232RQ;
  - Ethernet through a Lantronix XPort module ("CONN XPORT").
- **RS-232 left the board by Rev 6.** The 2004 and 2006 boards carried a
  MAX202 and a DB9 connector (`protel-files/reader/reader-103/Backup of
  Copy of BOM for Reader 103.csv`; `bom-files/reader/medium-reader/old/MedRDR103081006Rev4.xls`).
  Neither is in R6NET or RDR-2012.
- **No WLAN part is in any Reader record.** The datasheet's "optional WLAN"
  is the website's claim.
- **The host side is a Windows library.** Its functions include `rfOpen`
  (serial), `rfOpenSocket`, `rfScanNetwork` and `rfScanIP` (API-2013;
  `rfOpenSocket` is in API-2007 too). Release 27 renamed the DLL
  "AWI_API_C_Vxx.dll" (`VerRelease.doc`).
- **Repeaters:** "The more remote Reader must transmit data four times
  without receiving any response from the destination device before the
  closer Reader enters Dynamic Repeater mode" (P08 §2.4).
- **Retries:** the host "will resend the same command two more times and
  increment the two bits CNTR from Type Bytes" (P08 §2.5.18).
- **Errors:** "1 Invalid Command", then time-out, checksum, parameter,
  read-memory and write-memory as codes 2–6 (P06 §4.1.3).

## Tag memory and commands

- **User memory** is 32 bytes of RAM at "0x00E0 - 0x00FF", plus optional
  external memory up to "0x40FF (16384 bytes = 16KB = 128Kb)" (P06
  §3.2.10).
- A read or write moves at most 12 bytes: "Up to 12 bytes can be read at a
  time from any 16 byte segment located in the tag's user memory area"
  (P06 §3.2.10).
- **The memory part** is a 24AA256 on every Tag 101–105 pick-and-place
  (TAG-PNP). That part holds 256 Kbit, twice what P06 can address. The tag
  BOMs also offer a 24AA04 (4 Kbit) build. INFERRED: this explains the
  datasheets' two memory figures. The 256 Kbit is the part, and the 128 Kbit
  is the protocol's limit.
- **P08's tag commands:** 0x06 Configure, 0x07 Enable, 0x08 Disable,
  0x09 Query, 0x0A Call, 0x0B Write, 0x0C Read, 0x10 Define Reader and
  0x12 Random Number Generator. **Conflict:** P06 configures a tag by a
  0x0B memory write.

## The milestone devices' hardware

Firmware source exists for the sealTag only. The Reader, the Field
Generators and the PIC tags have neither source nor image in the tree
(onboarding spec, Context). The Reader's three microcontrollers carry
ActiveWave's own markings instead of part numbers in R6NET: "AWI501C",
"AWI501D" and "AWI501E".

### The Reader (Reader 103, USB Rev 6, 2007)

- **Three microcontrollers:** two PIC16F870 and one PIC16F876A (R6S2;
  RDR-2012). They carry the markings "AWI501C", "AWI501D" and "AWI501E"
  (R6NET). P06 §3.1.8 asks for "the firmware versions of the Reader's three
  microcontrollers - C, D, and E". INFERRED: the markings are those three.
- **A 16 MHz oscillator** ("OSCILLATOR 16MHZ HCMOS SMD", RDR-2012).
- **A four-position switch** read through a 74165 shift register (R6NET).
  INFERRED: the Reader's address or configuration switch.
- **An FPGA in the drawings, absent from the 2012 build.** R6NET and R6S4
  carry an Altera EP1C3T100 with its configuration memory and a 1.5 V
  regulator; R6S4 notes "IF USED, DNP". The 2012 BOM names no Altera part.
  INFERRED: it became an unpopulated option.
- **No radio parts.** None of R6NET's parts is a SAW part, an RF transistor
  or a radio IC. R6S3 carries the radio's signals, "433_TX_DATA",
  "916_RX_DATA", "916_TX_DATA", "RSSI" and the "DIGITAL POT" lines, to a
  "1X10 HEADER", J10. The Transceiver has a 10-pin connector, J2 "CON10"
  (TRX-PNP). INFERRED: the Transceiver plugs into J10. The older Reader
  assembly lists "PWA, Transceiver Board" beside "PWA, Reader Board"
  (`bom-files/reader/standard-old-reader/RDRASM102110804.xls`).
- **Comparators:** three LM339 parts (R6NET), with no labelled role.
- **I/O:** the two relays, the two contact inputs and the motion input
  (§ Alarms and I/O); four LEDs, the parts "GLED", "BLED" and two "RLED"
  (R6NET).
- **Power:** a bridge rectifier ("BRIDGE 100V 0.5A"), a CS51414 buck
  regulator, and NCP1117 (5 V) and NCP1086 (3.3 V) linear regulators
  (R6NET; RDR-2012). No record states the input voltage or current. The
  website's "12 V DC 1.5 A" is its claim.

### The Transceiver (TRX-101, Rev 17–19)

The board the BOM folder's readme was unsure about ("we're not 100% sure
what this is"). It is the Reader's radio: three radio chains, two SMA
antenna jacks and a 10-pin connector. The parts are read from TRX-PNP. How
they chain is INFERRED from the parts themselves, and from TRX-SCH where its
text names them.

- **433.92 MHz transmitter:** two BFR193 transistors with a
  "433.92_Resonator", then the AT-113 attenuator and the DS1804 digital
  potentiometer that sets it.
- **916.5 MHz receiver**, a superheterodyne:
  - an RF switch (UPG52TA; UPG152TA in the 2012 BOM);
  - a "916.5_MHz Saw Filter";
  - a BGA2001 (INFERRED: the low-noise amplifier);
  - an SA636DK mixer and IF strip;
  - a local oscillator, an NE85619 with a "905.8_Resonator": 916.5 − 905.8
    = 10.7 MHz, the IF;
  - two "10.7 Mhz Filter" parts, of 330 kHz bandwidth (TRX-2012), then
    LMV821 op-amps and a TLV1391 comparator. INFERRED: these produce the
    Reader's "916_RX_DATA" and "RSSI" signals.
  - The 2012 BOM lists a local-oscillator frequency 10.7 MHz below each
    receive frequency (858.25 MHz for 868.95 MHz, 856.3 MHz for 867 MHz;
    TRX-2012).
- **916.5 MHz transmitter:** the second NE85619 with a "916.5_Resonator".
  INFERRED: keyed by the Reader's "916_TX_DATA", as its link to a Smart Field
  Generator.
- **Bench figures** (TIPS): "Sensitivity will have to be around -97.5 or
  higher in order to have a good read range"; "Threshold has to be set to
  1.67K ohms in order to have good communication between the Reader and the
  Smart Field Generator". INFERRED: the 5 kΩ potentiometer R52 is that
  threshold (TRX-SCH).
- INFERRED: the Smart Field Generator carries a Transceiver too. Its BOM
  names microcontrollers and no RF part
  (`bom-files/field-generator/smart-fgen/SMFGen 9-07-2007 REV 08.xls`), and
  TIPS tunes "the Reader and the Smart Field Generator" together.

### The Standard Field Generator (FGN 101, Rev 2.2–2.3, 2005)

- **Microcontroller:** a PIC16LF628A with a 4 MHz crystal (FGN-BOM). The
  Rev 2.3 BOM orders a PIC16F628A. **Conflict:** the low-voltage part against
  the standard one.
- **Radio, transmit only:** a "433.92Mhz" resonator, two BFR193 transistors,
  the AT-113 attenuator and a "50 Ohm Ant" connector (FGN-BOM). Neither BOM
  names a receiving part, and its sheets are named "Power and PIC" and
  "433Mhz Xmit" (FGN-SCH).
- **Range:** a 10 kΩ potentiometer on the 433 MHz sheet (FGN-SCH). On
  record (TIPS): "To change from low to high power and still be able to
  read the tags, chagne R5 from 470 ohms to 6.8K ohms. This will make a
  significant difference on power consumption."
- **Interfaces:** a MAX202 RS-232 transceiver, an RJ11 jack, the PIR input
  and status LEDs (FGN-BOM; FGN-SCH).
- **Power:** labels "9 Volt", "BATT", "+ 12 Volts" and "DC Power In", a
  4.2 V voltage detector (TC54) and a "~LBATT_A" line (FGN-SCH; FGN-BOM).
  INFERRED: a battery or a DC supply, with a low-battery warning.

### The cardTag (Tag 101) and the compactTag (Tag 102)

The two share one design with Tag 103 and Tag 105. Every pick-and-place of
Tag 101, 102, 103 and 105 names the same core (TAG-PNP: the Tag 101 Rev 21,
Tag 102 Rev 17 and Rev 20, Tag 103 Rev 21 and Tag 105 Rev 20 and Rev 21
files):

- **Microcontroller:** a PIC16F690.
- **916.5 MHz transmitter:** NE85619 transistors on a 916.5 MHz resonator,
  keyed by "TX DATA".
- **433 MHz receiver:** a MIC861 op-amp and a MAX9117 or MAX9119
  comparator to "RX DATA", with no SAW part or resonator at 433 MHz. Its
  supply comes through the TC1070 (a regulator, by its part number),
  switched by "ENABLE_RCVR". INFERRED: a detector receiver, tuned by hand,
  that the PIC powers only while it listens. The Tag 105 schematic's title
  reads "Active Tag 433.92 MHz Receiver".
- **Temperature and memory:** a DS1631 and a 24AA256 (§ Alarms and I/O;
  § Tag memory and commands).
- **Sleep clock:** a 32.768 kHz crystal appears on some revisions (the
  Tag 102 Rev 17 and Tag 103 Rev 21 files) and not on others (Tag 101
  Rev 21, Tag 102 Rev 20, Tag 105 Rev 21).
- **Options:** tamper, tilt, panic, a 4 kHz speaker and two LEDs.
- **Power:** a CR2032 holder ("BATT HOLDER COIN CR2032", the tag BOMs). The
  datasheet's "1 - 3 years" is the website's claim
  (`activewaveinc/docroot/products_datasht_cardtag.php`). No record states a
  battery life.

**Every radio was tuned by hand.** TUNE lists the capacitor and inductor
values per tag style and frequency, found "from testing each tag that
resulted in an optimal result". For example, the compactTag 102 Rev 17 at
916.5 MHz takes "C2 | 3.6pf" and "L2 | 6.8nH". TIPS tunes the 433 MHz
receiver by a capacitor "from 433MHz to 440Mhz" on a spectrum analyzer, and
turns a compactTag into a Field Generator by replacing its resonator with a
433.92 MHz one. INFERRED: the tag's transmitter and the Field Generator's
share one oscillator design.

**The cardTag gained its sensor late.** The Tag 101 BOM of May 2011 names no
DS1631. The Tag 101 Rev 21 pick-and-place does, in a folder backed up on
2013-08-06 (`protel-files/tags/credit-card-tag/101-rev21/`). INFERRED: the
temperature sensor was added between those revisions.

### The sealTag (Tag 201, Rev 4, 2012): a logging seal with no active radio

The sealTag is the only device whose firmware is in the tree, and its
records describe a different kind of device from the rest of the family.

- **The board has two chips.** T201-PNP lists 24 parts. Its only
  integrated circuits are an STM8L151F2 microcontroller and an ST M24LR16E
  memory. The memory is a dual-interface EEPROM, writable over the air at
  13.56 MHz (ISO 15693) as well as from the microcontroller (INFERRED from
  its part number; the host software's ISO 15693 commands, below, agree).
  The rest are a 32.768 kHz crystal, a coin-cell holder, pogo pins,
  resistors and capacitors (T201-PNP). The schematic beside it
  (`Tag201_Rev4.SchDoc`) names the nets "ANTENNA" and "WRIST". INFERRED: the
  antenna is a coil printed on the board, since no coil part is listed, and
  "WRIST" is the band's tamper loop.
- **No 433 MHz or UHF radio exists.** No firmware file outside ST's vendor
  library mentions 433, 916, 927 or 868 MHz, a random delay or a temperature
  sensor. This study ran that search over the project's 414 `.c` and `.h`
  files: 0 matched those terms, and 6 matched the string "ActiWAVE", so the
  search did run.
- **The tag never transmits on its own.** An HF reader writes a command into
  the EEPROM over the air. The microcontroller wakes on an interrupt
  (`rfCommandIRQHandler`, FW `blocker_services.c`), runs the command, and
  writes the answer back for the reader to read. INFERRED: the interrupt is
  the EEPROM's own signal, on the schematic's "BUSY" net.
  - The handshake's states run from `COMMAND_STATUS_COMPLETE = 0` through
    `BUSY` (1), `BUSY_ACK` (2) and `RECEIVED` (3) to `RUNNING` (4) (FW
    `error_code.h`).
  - The host software drives a FEIG HF reader over USB, sending "RF ISO
    15693 Write single block" commands
    (`sealtag/host-software/source-code/SCLT/FeigReader.cpp`).
  - INFERRED: any collision handling is the HF reader's own ISO 15693
    inventory, not ActiveWave's.
- **The firmware sleeps until an interrupt.** `main()` halts in a loop (FW
  `main.c`). Three things wake it:
  - the command interrupt above;
  - the band input, on either edge (FW `band.c`);
  - the supply falling below 2.26 V: `PWR_PVDLevel_2V26` (FW `battery.c`).

  The real-time clock only timestamps; its interrupt handler is empty (FW
  `stm8l15x_it.c`).
- **Tamper is logged, not sent.** Opening the band logs an event (0xAA) with
  the clock's time and an operator number into a ring of 12-byte records in
  the EEPROM. The firmware then stops the clock, so a seal records its first
  opening only (FW `blocker_services.c`). The host sees the event only when
  an HF reader next reads the log.
  - The debounce is set by `TAMPER_DETECTION_THRESHOLD (uint16_t)0x777`,
    whose own comment reads "0x555 is 1 second, 0xaaa is 2 seconds"
    (FW `band.h`).
  - The step delay's comment ("delay 330us", FW `band.c`) gives about
    0.63 s instead. INFERRED: the true time is unmeasured.
- **A low-battery event (0xBB)** is logged the same way, and a command reads
  the cell's voltage (FW `battery.c`; `blocker_services.h`).
- **No temperature sensor, and no buzzer.** One LED, used at power-up as a
  test light (FW `blocker_services.c`).
- **A passive UHF label rides on the seal.** T201-BOM carries a line "UHF
  Tag" beside the band, housing, rivet and washer. The host
  software stores a 96-bit "UHF ID" in the EEPROM (`StoreUHFID`,
  `sealtag/host-software/source-code/SCLT/Commands.cpp`). INFERRED: a
  passive UHF inlay, since 96 bits is the usual EPC length. No record names
  its part, its protocol or the reader that reads it.
- **Conflict:** the microcontroller is an STM8L151F2 on T201-PNP and an
  STM8L151F3U6 in T201-BOM and in the firmware's IAR project
  (`FW/IAR/Blocker.ewp`).

So the 2012 sealTag cannot raise a tamper alarm by radio, and cannot report
a temperature. The datasheet figures the onboarding spec gave it (433 MHz
in, 916/927/868 MHz out, temperature) belong to the PIC tag family.

## Where the records and the website disagree

| Subject | Records | Website or spec |
|---|---|---|
| The sealTag's radio | an ISO 15693 seal; no 433 MHz or UHF active radio (§ The sealTag) | Appendix B: the tiewrapTag's radio and a temperature sensor |
| The compactTag's board | Tag 102 | the spec's Context quotes Tag 103, the miniTag |
| Reply bands | 916 MHz only in the protocol; 868.95, 927.2 and 867 MHz builds in the BOMs | 916/927 MHz (US), 868 MHz (EU) |
| Tag memory | a 256 Kbit part; a protocol that addresses 128 Kbit; a 4 Kbit option | 256 or 128 Kbit |
| Reader host links | USB and Ethernet on Rev 6; RS-232 on the 2004–2006 boards; no WLAN part | RS-232, Ethernet, optional WLAN |
| Field Generator transmit time | 1–31 s (P06), up to 60 s or continuous (P08) | — |
| Command 0x75 | temperature (P06), low battery (P08) | — |
| Configure Tag | 0x0B (P06), 0x06 (P08) | — |
| Contact inputs | on J12 (P08); on J11 on the Rev 6 board | — |
| CRC coverage | from `0x7E` (P06, and the test vectors); excluding a "preamble zero byte" (P08) | — |
| The Field Generator page's description | the page body: "transmits wake up commands to tags at 433 MHz" | its meta description: "a continuous 13.56 MHz field … passive tag" (`activewaveinc/docroot/products_datasht_standardfieldgen.php`) |

The website's range, area and battery figures are its claims. No record at
the tag states a read range in metres, a floor area or a battery life.

## What no record states

These are the gaps Stories 2.1.3 and 2.1.4 must fill from the new parts,
not from the original:

- the radio's bit rate, modulation and line coding;
- any tags-per-second figure;
- any read range, floor area or battery life;
- the output power at either frequency, and any FCC ID or certification
  (the onboarding spec also found no Part 15 statement on the website);
- the Reader's supply voltage and current;
- what a tag's "LF RX DATA" programming line and its TLE4913 part are for;
- the wake signal's form (a carrier, a tone or a preamble);
- the 2012 sealTag's UHF label: its part, protocol and reader.

## What this means for the study

**The reference to beat.** The original's design choices, each read above:

- **Two bands.** Commands go out at 433.92 MHz and replies come back at
  916.5 MHz, so a tag's receiver and transmitter never share a frequency.
- **Wake on command,** with the tag's receiver powered only for its
  listen windows: 660 ms enabled, 4 s disabled.
- **Check-in on the tag's own timer,** and a missed check-in alarmed by the
  host.
- **Zones from Field Generators.** A short-range 433 MHz field, started by
  motion, a timer or the host, wakes the tags near it, and each reply names
  the Field Generator that woke it.
- **I/O and alarms on the Reader and the tags.** The Reader has two relays
  and two supervised inputs. The tags report tamper, tilt, panic,
  temperature and low battery.
- **One packet and one CRC** on every link, the CRC-16/X.25 measured above.

**Its limits, against the requirements:**

- **Anti-collision.** Random delays and repeated replies cannot carry [3]'s
  1,000 tags/s or [21]'s 200 tags/s per channel (§ Anti-collision). The
  rebuild needs reader-assigned slots, a faster link or several channels,
  and Story 2.1.3 decides which.
- **The data rate is unknown.** The requirements' 250 kbit/s stays an
  assumption, and the records suggest the original ran slower (§ The radio
  link).
- **Hand-tuned radios.** Every tag's radio was tuned by capacitors and
  inductors found by testing, on a spectrum analyzer. That is beyond a
  beginner's bench, and it bears on Story 2.1.3's preference for
  pre-certified modules. INFERRED from TUNE and TIPS.
- **No firmware to port** for the Reader, the Field Generators or the PIC
  tags. The rebuild writes all of it fresh, and the protocol records are its
  only description of what that firmware did.

**The sealTag is a new design.** Milestone 1 asks for a sealTag whose
tamper alarm arrives at the host (plan Initiative 2, Milestone 1). The 2012
sealTag cannot send one: it logs a tamper for an HF reader to collect later
(§ The sealTag). The rebuild's sealTag therefore keeps the original's
form, a sealing band with a tamper loop, but not its electronics. INFERRED:
this is the case the onboarding spec's §3.7 suggestion already describes,
one electronics core in three enclosures with the seal as one of them.
Story 2.1.4 weighs it, and the decision sitting (Story 2.1.7) decides it.
Whether the rebuild's seal also keeps an HF interface, for reading it by a
phone, is a question for Stories 2.1.3 and 2.1.5.
