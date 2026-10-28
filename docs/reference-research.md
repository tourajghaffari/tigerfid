# Research log — the tigerfid reference corpus

What the design study has searched and read, the verdict on each source, and
the searches still to run. A later session reads the verdicts here instead
of running a search again (hub KB `reference-corpus-and-research-log`). The
files themselves are in `../tigerfid-reference/`, pinned in
`docs/reference-corpus.tsv`, under the rules in `docs/reference-repos.md`.
A claim marked **INFERRED** was not read from a source; everything else was.

## 1. The original — `activewave-2018` — KEPT (2026-10-26)

The 2001–2013 system as open-sourced in 2017–2018: boards, firmware,
software, documents, patents and presentations. It is the reference every
design option must beat (plan Story 2.1.2), and it is the family's own
Apache-2.0 work, so the study reads it and ports from it freely. The folder
copy is recreated from the tag; nothing about it needs searching.

**Read by Story 2.1.2 (2026-10-27):** the protocol and API documents, the
Reader, Transceiver, Field Generator and tag boards, and the sealTag's
board and firmware. What each record says, where the records conflict and
what no record states are in `docs/study/original-system.md`, which a later
session reads before re-reading the tree. Not read: the host applications'
source, left for Story 2.1.5, and the Outlook message. Omid answered
ASK-OMID A1 "Keep the default" at #7, so that file left the current tree
unread and stays at the tag, and reading rule 7 of `docs/reference-repos.md`
keeps it closed.

## 2. The old website — activewaveinc.com — KEPT, captured 2026-10-26

**Why first:** the sibling site's seat is about to change the live site, so
the capture had to come before any change (spec C11). It holds:

- the 49 pages and files the site served, with their headers;
- the product photos;
- the 16 datasheet and brochure PDFs;
- the page sources behind the 13 datasheet pages.

`docs/reference-repos.md` § Licence audit records what was taken, what was
left out on purpose and why, and the integrity check.

**What the study takes from it:** the products' own descriptions, figures
and photos, for Story 2.1.2 (the original described) and as context for
Story 2.1.1 (the requirements). The datasheets' figures conflict with each
other in places (spec Appendix B). They are read as the old system's claims,
never copied as requirements.

**rfidguardian.com — DROPPED (2026-10-27).** The activewaveinc.com seat
pulled it alongside the site. Its one measured tie to ActiveWave is that its
domain redirects (HTTP 301) to activewaveinc.com. That seat read it as a
credit-card RFID-shielding product, not ActiveWave's tag system (INFERRED).
None of the study's three scenarios (a conference floorplan, a nursery's
temperature tags, a warehouse count) touches card shielding. The verdict is
reversible: if a later story needs it, the Wayback Machine holds its public
pages.

## 3. Chorus (chorusview.com) — the named modern comparison — KEPT (2026-10-26, spec D12)

**Why it is here:** Omid named it "a modern version of what we're doing
today", to be used as inspiration for the rebuild. The study records it as a
named comparison. Each item in its borrow and not-to-copy lists below is
answered by the study, ADOPTED or DECLINED with a reason (plan Stories
2.1.3 and 2.1.5).

**What it is:** Chorus began as an Alphabet X project in 2019 and was spun
out in April 2025 as ChorusView, Inc. It tracks where goods are in supply
chains and what condition they are in: temperature, shock, light and
tamper. It is sold directly and resold by Kuehne+Nagel ("Max Visibility"),
FourKites and Armada. In its own words, "Chorus doesn't sell hardware": the
sensors come free, and a recurring service fee pays for it (INFERRED).

**How it works:** the tags are Bluetooth, in three tiers:

- **Seeker,** a disposable printed label with up to about six months of
  battery;
- **Explorer,** a reusable key-ring puck with a battery that lasts years;
- **Scout,** a rugged unit that is both sensor and reader, with a cellular
  uplink (INFERRED).

The readers are other parties' infrastructure: Wi-Fi access points from
Cisco and Extreme Networks, Android scanners, PCs and phones, and, since
November 2025, third-party apps (about 100 million devices). Scout fills the
gaps.

Location is coarse: "about a kilometer" typically. Metre-level indoor
location was described in April 2025 as in development.

The software is the Summit cloud dashboard, with ERP and partner
integrations; no public API and no floorplan view are shown. A tag is bound
to a shipment, a SKU or a purchase-order line, by scanning its QR code
(INFERRED). It never pairs a person with a thing.

**Against the three scenarios:**

- **Warehouse:** the closest fit. It offers "automated stock counts" with
  about 100% accuracy claimed, but its count speed and tags per site are
  unpublished.
- **Hospital nursery:** partly, by analogy. It tracks equipment and
  temperature-sensitive goods, not people, and drives no door outputs.
- **Conference floorplan:** not covered. It tracks goods, not people, and
  only at coarse location.

**To borrow** (each answered by the study):

1. An item history: a timeline per tag of zones, times and sensor values.
2. Alarm rules set on screen: a temperature range, too long in one place
   (dwell), left a zone, opened or tampered, missed check-in.
3. "Find the nearest available X", ranked by zone.
4. A printed QR code and ID on every tag, so pairing becomes "scan the tag,
   scan the person or the item".
5. Share links: view-only and time-limited, for example for a conference
   organiser.
6. Four layers: tags, readers, server, dashboard. A "reader" is an
   abstraction that any device emitting the same event schema can fill.
7. Simple readers and a smart server: readers send the tag id, reader id,
   signal strength, time and payload, and the server decides zones and
   alarms.
8. Three hardware tiers: a cheap item tag, a reusable badge or fob, and a
   combined sensor-and-reader.
9. Prototype tags on off-the-shelf modules, as X apparently did. That is
   INFERRED from the prototypes' Bluetooth hardware prefix, which a public
   MAC-vendor lookup assigns to Minew.

**Not to copy:**

1. A cloud-only service. It cannot work offline, and it is a privacy problem
   for hospitals and for tracking people. The rebuild's server is local
   first.
2. A crowd-sourced reader network. Its coverage and latency are outside our
   control, and it is a privacy problem for tags worn by people.
3. Free hardware with a subscription, and printed battery labels. The model
   needs capital, and a beginner cannot manufacture printed labels.

**Where ActiveWave's design still differs or wins** (for the study to
confirm or overturn): room-level zones from a field generator at a doorway;
relay outputs for doors and cameras; waking tags on command, for battery
life and on-demand polling; owned infrastructure with no subscription, which
also works offline; and pairing people with things.

**Sources** (read 2026-10-26, public pages only, no login or form):

- https://chorusview.com/ and its `/technology/`, `/products/`, `/about/`,
  `/healthcare/` and `/news/` pages
- https://chorusview.com/2024/11/introducing-new-chorus-solutions/
- https://chorusview.com/2025/04/chorus-independent-company/
- https://chorusview.com/2025/10/precisely-orchestrated-how-chorus-is-revolutionizing-healthcare-asset-management/
- https://x.company/blog/posts/chorus-graduation/
- https://x.company/blog/posts/introducing-chorus/
- https://www.freightwaves.com/news/how-a-moonshot-is-giving-voice-to-truck-parcels
- https://www.kuehne-nagel.com/services/road-transport/real-time-transportation-visibility
- https://www.fourkites.ai/press/fourkites-and-chorus-partner-to-tackle-multi-billion-dollar-inventory-challenges
- https://www.armada.net/armada-announces-strategic-partnership-with-chorus-to-deliver-real-time-item-level-supply-chain-intelligence/
- https://maclookup.app/macaddress/ac233f (the hardware prefix in item 9)

The images and the Kuehne+Nagel PDF from these pages are in the folder's
`chorus/`, third-party and study only.

## 4. The study's third-party sources

The study's own sources (vendor datasheets, reference designs, the radio
rules) are pinned as each study story reads them, under the copy-with-trail
dial. Nothing is copied before Story 1.1.4's licence lane lands. A source a
choice rests on is pinned under the folder's `sources/`; a law text is cited
by its dated URL, which is its pin; other pages are cited by URL and fetch
date (plan D8).

### Story 2.1.3 — radio, location and legality (2026-10-27)

What was read is cited in `docs/study/radio-and-location.md`, which a later
session reads before searching again.

- **The law: READ, cited by dated URL, not pinned.** 47 CFR Part 15 §15.3,
  §15.5, §15.23, §15.212, §15.231, §15.247, §15.249, §15.503, §15.517 and
  §15.519, through the eCFR's API at its point in time of 2026-10-23. The
  Radio Equipment Directive (consolidated 30.05.2026) and Decision
  2006/771/EC (consolidated 23.01.2025), through the EU Publications Office's
  Cellar service at `publications.europa.eu`. ERC/REC 70-03 (edition of
  October 2026), from CEPT's document database. The study's § The law, read
  first, lists each with its version.
- **KEPT and pinned** (`sources/`, `third-party`, study only), because the
  choice rests on them:
  - `ti-cc1312psip-swrs293.pdf`, the CC1312PSIP module's datasheet: its
    certifications (FCC ID ZAT-1312PSIP-2), radio figures and package;
  - `ti-swra797.pdf`, TI's note on reusing the module's FCC certification:
    the certified PHYs, their powers and the antenna conditions;
  - `ti-cc1312r7-swrs254a.pdf`, the CC1312R7 datasheet: the radio core's
    sniff-mode and whitening features, and the faster PHYs' sensitivities.
- **KEPT, cited by URL and fetch date, not pinned:** TI's CC1352P7
  datasheet (SWRS251A), for Bluetooth LE Coded's sensitivity in the
  comparison; The Things Network's US902-928 page, for LoRa's bit rates.
- **Refused, not read** (each a public page that refused this leg's
  request; the friction log's leg 2 section records how each was met):
  - Silicon Labs' FGM230S and EFR32FG23 datasheets (HTTP 403), a
    sub-GHz module the leg meant to compare;
  - the FCC's equipment-authorisation search and the fccid.io mirror
    (HTTP 403), so the module grant's own record, with its equipment class,
    is unread. Story 2.1.4 reads it;
  - ETSI's server (HTTP 403 to a plain request): the version listings of
    EN 300 220-2, EN 300 328 and EN 302 065-1 were read through a
    web-reading tool, and the standards' files were not opened;
  - ST's STM32WLE5 datasheet (the connection closed);
  - EUR-Lex's own pages (an empty HTTP 202); the same texts came from the
    Cellar service.
- **Searched, not found:** a pre-certified module of a dual-band
  (sub-GHz plus Bluetooth) TI chip. The search found TI's CC1312PSIP and
  nothing for the CC1352P7.
- **Not read:** the EU's ultra-wideband Decision, ECC/DEC/(06)04,
  EN 302 065-2, and Part 15's Subpart B for digital devices. The study says
  why each was not needed, or which story reads it.

### Story 2.1.4 — parts and toolchains (2026-10-27)

What was read is cited in `docs/study/parts-and-toolchains.md`, which a
later session reads before searching again.

- **The law: READ, cited by dated URL, not pinned.** 47 CFR §15.3(h), (i),
  (k) and (z), §15.101, §15.103, §15.107, §15.109, §2.803(d)(2)(iv) and
  §2.906, through the eCFR's API at its point in time of 2026-10-23. The API
  now answers only a request that accepts a compressed reply.
- **KEPT and pinned** (`sources/`, `third-party`, study only), because a
  choice rests on them:
  - `energizer-cr2032.pdf` and `energizer-cr2450.pdf`: the two cells'
    rated capacities, sizes, self-discharge and the compartment safety note.
    [27]'s choice rests on them;
  - `ti-swra349.pdf`: TI's 2010 white paper on coin cells under peak
    currents. The usable-capacity bounds rest on it;
  - `ti-tmp117-snosd82d.pdf`: the tag's temperature sensor;
  - `ti-sszz027.pdf`: TI's standard terms for evaluation modules, which bind
    the development board and the debugger.
- **KEPT, cited by URL and fetch date, not pinned:**
  - TI's pages for the development board, the two XDS110 debuggers, CCS,
    UniFlash, SysConfig and TI Arm Clang;
  - the CCS 21.0.1 user's guide;
  - the board's EU declaration;
  - the SDK's public Git repository, its README and licence;
  - KiCad's download and licence pages;
  - a Microchip note on USB CDC.
  - The board's design files (`SWRR186.ZIP`) were opened for their licence
    only. They hold no licence file, and their sha256 is in the study.
- **Refused or not read:**
  - the FCC's equipment-authorisation search, fccid.io and fcc.report
    (HTTP 403, a content network's "Access Denied" page). A retry after Omid
    changed the network route was refused the same way, and Omid answered
    "Write it as not read". So the grant record stays unread, and two FCC
    IDs in TI's documents (ZAT-1312PSIP-2 and -3) are left for a browser
    read before any part is bought;
  - FTDI's driver page and Microchip's product pages (HTTP 403), so no USB
    bridge part is named by datasheet;
  - TI's certification page (HTTP 404);
  - Arm's GNU toolchain page, which renders by script;
  - the development board's quick start guide, which holds its US FCC
    notice: no public page this leg reached carries it.

### Story 2.1.5 — software stack (2026-10-28)

What was read is cited in `docs/study/software-stack.md`, which a later
session reads before searching again.

- **The original host programs: READ at the tag.** Smart Tracker's source
  (`software/smart-tracker/Source/`) and the Programming Station's
  (`software/programming-station/`), by targeted searches, not line by line.
  The Outlook message beside them stays unread (reading rule 7 of
  `docs/reference-repos.md`). The binaries and the installers were not
  opened.
- **No new pin.** No choice rests on a document whose bytes must be kept,
  so no `sources/` row is added (plan D8). The UART's rate and the radio's
  signal-strength reading come from the CC1312PSIP datasheet that Story
  2.1.3 pinned.
- **KEPT, cited by URL and fetch date, not pinned:**
  - pySerial's documentation and licence (BSD-3-Clause);
  - python.org's downloads page and Python's `sqlite3` and `http.server`
    documentation;
  - SQLite's "Appropriate Uses" page and its FAQ;
  - Node.js's `node:sqlite` documentation and the `serialport` installation
    guide, for the runner-up;
  - Go's install page;
  - Apple's pages on opening apps from unverified developers and on its
    developer programme;
  - MDN's pages on Web Serial and `getUserMedia()`, and MDN's
    browser-compatibility data for Web Serial, Web NFC, `EventSource` and
    SVG. The data was read as JSON, because the pages' tables render by
    script.
- **Searched, not adopted:** an existing open host with floorplan zones,
  alarms and pairing under an allowlisted licence. One web search found
  none to adopt. It named GRAIL RTLS, reelyActive's rtlips-server and Trace
  Me Now, none read beyond the search's own results. The rebuild writes its
  host fresh.
- **MEASURED on this study's machine (macOS 26.6.2):** Python 3.12.15 with
  SQLite 3.54.1, and Node v24.15.0 with SQLite 3.51.3. A standard-library
  probe of the chosen stack is recorded in the study. Windows and Linux were
  not measured.

### Story 2.1.10 — the Sprint 2 review's fixes to the three studies (2026-10-28)

What was read is cited in the three studies it corrects
(`docs/study/radio-and-location.md`, `docs/study/parts-and-toolchains.md`
and `docs/study/software-stack.md`).

- **The pinned sources, read again as text** (`sources/`, no new pin): the
  TMP117 datasheet's pages 6 and 12, for the accuracy's averaging condition
  and the eight averages' 124 ms; SWRA797's pages 9, 14 and 17, for the
  ground plane and the ZAT-1312PSIP-3 filings; the CC1312PSIP datasheet's
  pages 16, 17, 26 and 54, for the faster PHYs' sensitivities, the UART and
  the co-location condition.
- **The original, at the tag:** `api/c-plus-plus-dot-net/example/AWI_API.h`,
  for `rfTagEvent_t`'s `logTimestamp`, and a count over Smart Tracker's 103
  `.cs` files in the reference folder, none of which names it.
- **The law: READ, cited by dated URL, not pinned.** 47 CFR §2.803 at the
  eCFR's point in time of 2026-10-23, for its notice's last clause.
- **KEPT, cited by URL and fetch date, not pinned:**
  - MDN's browser-compatibility data for Web NFC (`api/NDEFReader.json`)
    and the mirroring browsers' upstreams (`browsers/<name>.json`), read as
    JSON;
  - CMake's download and licensing pages;
  - GNU make 4.4.1's release list on a GNU mirror, and its `README` and
    `COPYING` from the release;
  - TI's SysConfig and TI Arm Clang pages, read again: neither states a
    price or a licence;
  - the SDK's README, § Resources, read again.
- **Refused, not read:** GNU's own make page (the connection reset twice;
  the mirror stood in), and Arm's GNU toolchain page (HTTP 403).

### Story 2.1.6 — the tiered cost (2026-10-28)

What was read is cited in `docs/study/tiered-cost.md` § Sources, which a
later session reads before searching again. No source is pinned: no choice
rests on a price page (plan Sprint 2's D8).

- **KEPT, cited by URL and read date, not pinned:** the sellers' public
  price pages, each read once by a plain request:
  - TI's product pages for the module, the sensor, the regulator and the
    relay driver, and its tool pages for the two debuggers and the
    development board (each page's own price list);
  - LCSC's product pages for the chip antenna, the connectors, the USB
    bridge, the relay, the cell holders and the passives (its US-dollar
    ladders);
  - Adafruit's product and search pages for the cells, the enclosure, the
    supplies, the cable and the hand tools; PINE64's Pinecil page;
    AmScope's, Fluke's and R&L Electronics' product pages; RTL-SDR Blog's
    store page;
  - OSH Park's 4-layer service page, and JLCPCB's assembly price page and
    assembly FAQ.
- **Refused, or priced by script only, and written not read** by Omid's
  word ("Write all as not read (Recommended)", the study's § What was read):
  Digi-Key's W5017 page (and Mouser's, Arrow's and Newark's), JLCPCB's
  instant quote and parts search, Hakko USA's pages and Jameco's.
- **Refused, not needed:** Chip Quik's store and JBC's US pages (HTTP 403).
- **LCSC answered a quick run of requests with a challenge page.** Read
  again with 15 s between requests, each page answered. Nothing about the
  request was changed but its pace.
- **Searched, not found:** a seller's page that prices a USB barcode
  scanner (Adafruit's search lists none).

**Search ideas, by study story.** These come FROM THE PLAN AND FROM MEMORY,
UNVERIFIED: a list to start from, not research. Each is searched, then given
a verdict here.

- **2.1.3 (radio, location, legality):** the US rules at the eCFR (47 CFR
  Part 15: §15.231, §15.247, §15.249, §15.23); the EU's short-range-device
  rules for 868 MHz and 433 MHz; sub-GHz systems-on-chip with wake-on-radio;
  pre-certified sub-GHz and dual-band (sub-GHz plus Bluetooth) modules;
  Bluetooth Long Range, LoRa and UWB as comparisons; how open active-RFID and
  indoor-location projects place zones.
- **2.1.4 (parts and toolchains):** each candidate chip's free toolchain on
  Windows, macOS and Linux, and its debugger's price; the licence of each
  vendor reference design; KiCad 10's library licence (recorded once in the
  notices file by Story 1.1.4).
- **2.1.5 (software stack):** local-first host software on all three
  platforms; open floorplan views; an open event schema for readers.
- **2.1.6 (tiered cost):** dated prices for each part, board-assembly
  service, tool tier and cheap radio receiver.

## Searches already run

- **2026-10-26:** the activewaveinc.com capture (by that site's seat, before
  any change) and the Chorus read for spec D12 (§3's sources).
- **2026-10-27:** Story 2.1.3's searches for the radio rules and the radio
  parts (§ 4, above). The search ideas for 2.1.3 above are answered there.
  How open active-RFID and indoor-location projects place zones was not
  searched: the study's zones follow the original's Field Generators.
- **2026-10-27:** Story 2.1.4's searches for the module's parts, tools,
  debugger, cells, sensor, Subpart B and the grant record (§ 4, above). The
  search ideas for 2.1.4 above are answered there. A debugger outside TI's
  terms, and the GCC route's platforms, were not searched.
- **2026-10-28:** Story 2.1.5's reads for the host stack and its one search
  for an existing open host (§ 4, above). The search ideas for 2.1.5 above
  are answered there: no open host was found to adopt, the floorplan view is
  written fresh, and the event schema is the study's own.
- **2026-10-28:** Story 2.1.10's reads for the Sprint 2 review's fixes
  (§ 4, above). No search was run: each read went to a source a finding
  cited, or to the maker's page of a tool a finding named.
- **2026-10-28:** Story 2.1.6's price reads (§ 4, above). Web searches
  found the sellers' pages, and each price was then read from the page
  itself, never from a search result. The search ideas for 2.1.6 above are
  answered there: a dated price for each part, the board and assembly
  services, each tool tier and the bench's receiver.
