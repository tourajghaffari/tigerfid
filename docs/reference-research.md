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

## 4. The study's third-party sources — none pinned yet

The study's own sources (vendor datasheets, reference designs, the radio
rules) are pinned as each study story reads them, under the copy-with-trail
dial. Nothing is copied before Story 1.1.4's licence lane lands.

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
