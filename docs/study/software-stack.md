# The software stack

The fifth document of the design study (plan Story 2.1.5). It chooses the
host software for the rebuild, the program on the user's own computer that
shows where every tag is and raises the alarms:

- what the original host programs did, read from their source at the tag;
- the stack, on Windows, macOS and Linux, local first (it works offline);
- the event schema the readers report, following the radio link that
  `docs/study/radio-and-location.md` chose;
- the floorplan view, the four alarms, pairing people with things and an
  open API;
- what a phone reading the seal's HF interface would give the software (plan
  D5);
- the Chorus software items, one by one.

A claim marked **INFERRED** was reasoned, not read: its arithmetic or its
reasoning is shown. A figure marked **ASSUMED** is an input to that
arithmetic, not a measurement. A claim marked **MEASURED** was run by this
study on its own machine, with the date and the version the tool printed.
Everything else cites the text it was read from, by the short names in
§ Sources. No board exists yet, so nothing here has met a real Reader. This
document recommends; the decision sitting (Story 2.1.7) decides.

**Platforms measured: macOS** (macOS 26.6.2, on 2026-10-28: the standard
library parts of the chosen stack, § The stack on three platforms).
**Platforms cited: Windows, macOS and Linux,** from the makers' own pages.
Nothing here was run on Windows or Linux.

## What was read

Every source was read on 2026-10-28, between this leg's clock reads of
00:08:49Z and 00:24:44Z.

- **The original host programs,** at the tag `activewave-2018`, in the
  reference folder recreated from it (it verified at 299 rows):
  `software/smart-tracker/` and `software/programming-station/`
  (§ The original host software). Story 2.1.2 left them for this story
  (`original-system.md` § The records read).
- **Not read:** `software/smart-tracker/SmartTracker  SaniFaucet.msg`, the
  Outlook message beside them. It stays closed under reading rule 7 of
  `docs/reference-repos.md`, and nothing here needed it.
- **The module's datasheet** (PSIP, pinned by Story 2.1.3), for the UART's
  rate and the radio's signal-strength reading.
- **The makers' pages** for the stack's parts, cited by URL and fetch date
  in § Sources. No choice rests on a document that needs pinning, so no
  corpus row is added (plan D8). Story 2.1.4 cited its tool pages the same
  way.
- **Browser support** was read from MDN's own compatibility data, the JSON
  that MDN's tables are drawn from (BCD in § Sources), because the pages'
  tables render by script.
- **Searched, not adopted:** an existing open host to adopt instead of
  writing one. One search, on 2026-10-28, found no maintained open program
  with floorplan zones, alarms and pairing under a licence on this repo's
  allowlist. It named GRAIL RTLS, reelyActive's rtlips-server and Trace Me
  Now, none read beyond the search's own results. The rebuild writes its
  host fresh.

## The original host software

The tag holds two host programs and their source, both for Windows. The
software folder's readme calls them "the applications that were used to
demonstrate the capabilities of the system" (`activewave-2018:software/readme.md`).

### Smart Tracker: monitoring, zones and rules

The readme: "the application that can actually be used to monitor tags, set
zones, assign Field Generators to Readers, and more".

- **Its stack.**
  - A C# program for .NET Framework 4.0 with Windows Forms: `<OutputType>WinExe`
    and `<TargetFrameworkVersion>v4.0`
    (`activewave-2018:software/smart-tracker/Source/Server/Smart Tracker.csproj`).
  - The Reader through the API's .NET wrapper, `AW_API_NET`, built for x86
    (the same file), over the Windows library that `original-system.md`
    § The Reader to the host describes.
  - A MySQL server, reached through the "MySQL ODBC 3.51 Driver"
    (`Source/Server/CommForm.cs`, `DatabaseCheck.cs`).
  - Reports through Microsoft's ReportViewer (the project file's
    references) and, INFERRED from the classes in `Source/Server/Test reports/`,
    Crystal Reports.
- **The event it received.** `rfTagEvent_t`, from the API. Counted over the
  source folder, its fields in use are the tag's ID, status bits and type,
  the Reader, the Field Generator, the signal strength (`RSSI`), the host,
  the event type and a packet ID. **It carries no time.**
- **The time was the host's, and local.** Each event was stamped when it
  arrived, by `DateTime.Now`: 263 uses in the C# source and none of
  `DateTime.UtcNow` (MEASURED: a count over `Source/`). Its tables store
  that local time (`PopulateHistoryTable`, `MainForm.cs`).
- **A zone was a row keyed by a Reader and a Field Generator.**
  `GetParkInfo` looks up "`SELECT ID, Location FROM zones WHERE ReaderID =`"
  the event's Reader "`AND FieldGenID =`" its Field Generator (`MainForm.cs`).
  A reply's zone was therefore the Field Generator that woke it, as
  `original-system.md` § Zones read from the protocol.
- **What it stored.** Each tag's last zone, event and time in `tagactivity`;
  every zone event appended to `history` (`TagID, ZoneID, Event,
  Timestamp`); and a `traffic` log with the tag's name, department and status
  (`MainForm.cs`). The history keeps **no signal strength and no sensor
  value**.
- **People, things and permissions.**
  - Tag type 1 is a person's access tag and type 3 an asset's
    (`MainForm.cs`, `TagDetected`); inventory tags are a third type.
  - A person's access to a zone is checked against their group's zones and
    schedules: `EmployeesQuery.CheckAccess(tag, zone, time)`, and the tables
    `groups`, `groupzones` and `groupschedules`.
  - An asset moves with or without permission by a per-asset flag:
    "Asset moving without permission" sets the alarm (`MainForm.cs`;
    `asset` holds `ID, Mobile, Name, Model, SKU, Description, Timestamp`,
    `AssetForm.cs`).
  - **The one pairing in the code** is the parking case. A car's tag is
    stored with its owner (`parkingtags`: `CarID, OwnerName, …, ParkingNum`),
    and a car in another space than its owner's reads "Wrong Parking Space"
    (`Source/Server/CarTrackingClass.cs`). No code pairs a thing with a
    person who must be beside it at an exit.
- **Alarm rules set on screen.** The rule editor (`Source/Server/ActionForm.cs`)
  joins an event to actions:
  - events: `BREACH ALARM`, `TAMPER ALARM`, `ALERT`, `TAG DETECTED`,
    `INVALID TAG DETECTED`, `INPUT DETECTED`;
  - actions: an alarm light, a siren or a door unlock on either of the
    Reader's two relays, "Display Tag Moving Direction", an input timeout,
    calling an access, asset or inventory tag, and "E-mail Event Activity".
- **Sounds:** eleven alarm and status sounds in `software/awi-sounds/`
  (`tamper.wav`, `breach.wav`, `alarm.wav`, `ack.wav` and seven more).
- **No missed check-in alarm in this source.** The protocol makes it the
  host's job (P08 §3.1.16, quoted in `original-system.md` § Alarms and I/O).
  A search of `Source/Server/` for check-in, no-response and lost-tag terms
  found none. The no-response handling that exists, `RF_E_NO_RESPONSE`, is
  on reader events (`AWIComponentLib/CommunicationClass.cs`,
  `NetRdrConnClass.cs`). INFERRED: Smart Tracker watched Readers going
  offline, not tags going silent.
- **No temperature in this source.** None of the 112 C# files under
  `software/` at the tag mentions a temperature (MEASURED: `git grep -i -E
  'temperat|celsius'` over them, no match). The temperature screens are the
  Programming Station's (below).
- **The floorplan was a separate program with no source.** The toolbar's
  "Mapper" button starts `Mapper.exe` as its own process (`MainForm.cs`).
  Its configuration names a `MapViewImage`, a `RfidRefreshInterval` of 1 and
  a plugin, `MyRfidDb.dll` (`software/smart-tracker/Mapper.exe.config`). The
  tree holds the binary, its two libraries and two source files that
  declare `namespace ActiveWave.Mapper` (`TagHistoryView.cs`, a tag's history
  between two dates, and `ReaderListView.cs`). No project for it exists.

### The Programming Station: commissioning

The readme: "the software that needs to be used to actually program and
re-program tags".

- **Its stack.** Borland C++Builder with its VCL forms: a `.bpr` project,
  `.dfm` forms, `#include <vcl.h>` and `Registry.hpp`, with its settings in
  the Windows registry under `HKEY_CURRENT_USER`
  (`activewave-2018:software/programming-station/Programing Station 191/Programming Station 191/ProgStationUnit.cpp`,
  45,055 lines, in the folder of version 1.91).
- **It speaks the protocol itself.** It defines the command codes in
  `Commands.h` and makes no call to the API's `rf…()` functions (MEASURED:
  a count over `ProgStationUnit.cpp`, 0). Its links are a serial port
  (`ComConfigPage.cpp`) and TCP/IP (`TCPIPUnit.cpp`, `WinSock2.h`).
- **What it configures,** from the change history in its source's header
  (versions 1.61 to 1.87): tags (type, tamper, LED and speaker, random
  delay, and temperature: limits, period, number of reads and logging);
  Readers (ID, field strength, signal strength, transmit time); both kinds
  of Field Generator (ID, assigned Reader, hold time, motion detector); the
  relays and inputs; large reads and writes of tag memory; and encrypted
  firmware downloads to the Reader and the Smart Field Generator.
- **Several codes share a value.** In `Commands.h`, `READ_TAG_TEMP_CONFIG`
  and `DEFINE_TAG_READER_ACK` are both `0x90`, `READ_TAG_TEMPERATURE` and
  `INPUT_STATUS_ACK` both `0x91`, and `CONFIG_FIELD_GEN` and
  `CONFIG_TX_TIME` both `0x20`. A code alone does not name a command.
  `original-system.md` § Where the records and the website disagree found
  the same kind of conflict between the protocol's versions.
- **Its encryption keys are fixed in the source.** It encrypts its network
  traffic through `VC_blockEncrypt`, a function it loads from a library at
  run time, and firmware data with Rijndael in ECB mode
  (`oRijndael.Encrypt(…, CRijndael::ECB)`). The keys are a constant array in
  `ProgStationUnit.cpp` (near its line 265) and a key string passed to
  `MakeKey` (near line 41,758), public with the tree since 2018. This
  document does not repeat them.

### What the rebuild takes from them

**Kept, as ideas:**
- zones defined by Field Generators, set up on a screen;
- alarm rules joining an event to actions, the Reader's relays among them;
- a history per tag between two dates;
- access by group, zone and schedule;
- the parking case's pairing, made general (§ Pairing people with things);
- the alarm sounds. They are the family's Apache-2.0 work, which the
  reference dial lets the rebuild port freely (`docs/reference-repos.md`
  § Strictness dial).

**Left behind:**
- **Windows only,** at every layer: the .NET Framework 4.0 forms, the x86
  library, the ODBC driver, the C++Builder forms and the registry.
- **A database server to install and run,** for one site's data.
- **The host's local arrival time** standing in for when a reply was heard.
- **A command named only by a number** that another command shares.
- **A secret compiled into the program.**
- **A floorplan view whose source is not in the tree.** It is written fresh.

Nothing here is ported as code. The rebuild's protocol is new
(`radio-and-location.md` § The choice), and so is every program on it.

## The stack, against the requirements

### What the host must carry

| Need | The number | Source |
|---|---|---|
| Replies from one Reader in a burst | up to 1,363 a second, a hall of 1,000 in 0.73 s | [3]; `radio-and-location.md` § Anti-collision |
| The host's share of a move shown | 0.25 s from the Reader, 0.5 s to draw (both ASSUMED) | `radio-and-location.md` § Zones |
| Alarm latency, strictest | tamper 5 s, temperature 60 s, missed check-in 60 s, pairing 5 s | [14]–[17], `requirements.md` § What binds the design |
| Check-ins in a day, warehouse | 9.6 million (INFERRED: 100,000 tags × 96 a day at one every 15 min) | [19], [25] |
| Works offline | always | spec §3.5 and D5; the plan's Story 2.1.5 |
| Runs on | Windows, macOS and Linux | spec D5 |

### The options

| | Option | Verdict |
|---|---|---|
| A | **Python 3**, with its standard library's `sqlite3` database and HTTP server, and pyserial for the Reader's port. The user's own browser is the screen. | **Chosen** (§ The choice) |
| B | **Node.js**, with its built-in `node:sqlite` and HTTP server, and the `serialport` package. The browser is the screen. | Runner-up |
| C | **Go,** one compiled program per platform | Declined for milestone 1 |
| D | **A desktop shell** around a web page (Electron, Tauri, Qt and the like) | Declined |
| E | **A page in the browser alone,** reading the port through Web Serial | Declined |
| F | **The original's stack** | Declined: Windows only at every layer (above) |
| G | **An existing open host** | None found (§ What was read) |

- **What every option shares.** The screen is a web page, because every
  platform already has a browser that draws it. Every option except E runs
  a program of its own on the user's machine, which keeps the alarms going
  while no page is open.
  - The floorplan is drawn in SVG, which every current browser has drawn
    for years: Chrome 1, Edge 12, Firefox 1.5, Safari 3 (MEASURED from BCD,
    `svg.elements.svg`).
  - The live view is a stream of Server-Sent Events, through the browser's
    `EventSource`: Chrome 6, Firefox 6, Safari 5 and Opera 11 (MEASURED from
    BCD, `api.EventSource`).
- **B, Node.js: the runner-up.**
  - **For it:** one language from the server to the page. This repo's own
    checks already run on Node, so a contributor needs one runtime fewer.
    Its database is built in: MEASURED on this box, Node v24.15.0's
    `node:sqlite` opened a database at SQLite 3.51.3.
  - **Against it:** its docs mark that module "Stability: 1.2 - Release
    candidate", and its history says "still experimental" (NODE-SQLITE).
    The serial port needs a native add-on: "We ship these builds with the
    package and the appropriate one will be used". Where none fits, "it will
    be compiled during the npm install via node-gyp", which "assumes you
    have everything on your system necessary to compile ANY native module
    for Node.js" (SERIALPORT). INFERRED: that is a part of the host a
    beginner could not repair.
- **C, Go: declined for milestone 1.** It gives one file to download per
  platform, and its install page covers Linux, Mac and Windows (GO-INSTALL).
  It was not measured: no Go is installed on this study's box.
  - **A downloaded program meets macOS's warning:** "If the app developer
    can't be verified and — in macOS Catalina and later — the app hasn't
    been notarized by Apple, macOS can't verify that the app is free of
    malware". The user then clicks "Open Anyway" in Privacy & Security
    (APPLE-GK).
  - **Notarization runs through Apple's developer program,** a "$99 annual
    membership" (APPLE-DEV). That is a paid route, which the anti-goals
    avoid.
  - **Built from source, it needs Go installed,** one installer, as Python
    does. INFERRED: the single file's gain is the one install it saves, and
    it trades a beginner-readable language for it.
- **D, a desktop shell: declined.** INFERRED, none read: a shell adds a
  build and a package per platform, and the browser already draws the
  floorplan and the live stream.
- **E, the browser alone: declined.**
  - **Web Serial is not in every browser:** Chrome 89, Chrome on Android
    148 and Firefox 151; Safari has none (MEASURED from BCD, `api.Serial`).
  - **It needs a secure context:** "This feature is available only in
    secure contexts (HTTPS)" (MDN-SERIAL).
  - **INFERRED:** the alarms would stop when the tab closed, and the missed
    check-in alarm runs on a timer that only an open program can keep.

### The choice

**Option A: the host is a small Python program on the user's own machine,
and its screen is a page in the user's own browser.**

- **The parts:**
  - **Python 3,** from python.org's installer on Windows and macOS (version
    3.15.2 offered on 2026-10-28, released Oct. 25, 2026). For Linux,
    python.org offers the source release (PYTHON-DL). INFERRED: Linux
    distributions package Python themselves.
  - **Its standard library's `sqlite3`** for the database: "a lightweight
    disk-based database that doesn't require a separate server process"
    (PY-SQLITE3). One file holds a site's data.
  - **Its standard library's `ThreadingHTTPServer`** for the screen, the API
    and the live stream, listening on the user's own machine only (PY-HTTP).
  - **pyserial** for the Reader's port: "It provides backends for Python
    running on Windows, OSX, Linux, and BSD", and "The files in this package
    are 100% pure Python" (PYSERIAL). Its licence is BSD-3-Clause
    (PYSERIAL-LIC), which is on this repo's allowlist (`THIRD_PARTY_NOTICES.md`).
  - **The page:** plain HTML, CSS and JavaScript files that the host serves.
    There is no build step, and nothing is fetched from the internet: no
    content network and no web fonts.
- **Why it wins:**
  - **No compiler anywhere.** Every part is Python's standard library or
    pure Python. Against B, no native add-on has to fit the user's machine.
  - **Nothing of ours is a downloaded program.** The host is text that the
    user's own Python runs. INFERRED: it meets none of the warnings and
    paid signing that option C's binary meets.
  - **It can run with no package install at all.** The licence lane lets
    pyserial be copied in with its trail, a marker and an entry in
    `THIRD_PARTY_NOTICES.md` (INFERRED: the build initiative chooses between
    that copy and an install by `pip`).
  - **It ran here.** MEASURED on this box (§ The stack on three platforms):
    the database took 1.1 million rows a second, and the stream carried
    1,000 events in 16 ms.
  - INFERRED, and the weakest reason: Python is a common first language, and
    the beginner may have to debug the host.
- **What it costs:**
  - **The HTTP server is basic.** "`http.server` is not recommended for
    production. It only implements basic security checks" (PY-HTTP). In
    milestone 1 it listens on the user's own machine only, behind the checks
    in § Security on the user's machine. The LAN tier (share links and
    phones) chooses a hardened server when it comes.
  - **Two languages,** Python on the host and JavaScript in the page.
  - **A runtime to install:** one installer per platform.
- **For the sitting:** B stays a sound choice if the sitting weighs one
  language over the compiler-free path. The schema and the API below do not
  change between A and B.

### The stack on three platforms

| Part | Windows | macOS | Linux | Source |
|---|---|---|---|---|
| Python 3 | python.org's installer | python.org's installer; **MEASURED** here: Python 3.12.15 | python.org's source release; INFERRED: the distributions' own packages | PYTHON-DL |
| `sqlite3` | in the standard library | **MEASURED** here: SQLite 3.54.1 | in the standard library | PY-SQLITE3 |
| `ThreadingHTTPServer` | in the standard library | **MEASURED** here (the probe below) | in the standard library | PY-HTTP |
| pyserial | a backend | a backend ("OSX") | a backend | PYSERIAL |
| The Reader's port | USB CDC, with Windows' generic driver since Windows 10 | CDC built in | CDC in most distributions | MCHP-CDC, via `parts-and-toolchains.md` § The Reader |
| The page | SVG and `EventSource` in Chrome, Edge and Firefox | the same in Safari | the same in Chrome and Firefox | BCD |

- **The probe, MEASURED on 2026-10-28 between this leg's clock reads of
  00:22:33Z and 00:22:34Z** (macOS 26.6.2, Python 3.12.15, standard library
  only):
  - **The database:** 400,000 reply rows, written as one transaction per
    0.25 s frame of 1,000 replies in a file database in write-ahead mode,
    took 0.36 s. That is 1,111,189 rows a second, at 1,111 transactions a
    second.
  - **The stream:** `ThreadingHTTPServer` sent 1,000 Server-Sent Events to a
    client on the same machine, and the client read all 1,000 in 16 ms.
  - **What it shows:** on this box, the database and the stream are not
    where the host's time goes. INFERRED: against [3]'s 1,363 replies a
    second, a laptop a hundred times slower would still keep a margin of
    eight. SQLite's own FAQ gives "50,000 or more INSERT statements per
    second on an average desktop computer" but "only ... a few dozen
    transactions per second" on a spinning disk (SQLITE-FAQ, item 19). The
    host therefore writes one transaction per frame, never one per reply.
  - **Not measured:** Windows, Linux, and the Reader's port on any platform,
    because no Reader exists yet. The build's first host story measures
    them.
- **INFERRED, for the guides on Linux:** a user may need permission to open
  the Reader's port. The guide checks it on a Linux machine before it says
  so.

### Security on the user's machine

The original's lesson is a key compiled into the program (§ The
Programming Station). INFERRED design rules for the build:
- **The host listens on the user's own machine only** in milestone 1.
- **It answers only requests addressed to it by that name,** and every
  request that changes anything carries a token that the host gave the page
  at sign-in. A web page in the same browser therefore cannot drive the API.
- **The host makes its own secrets on its first run,** and no secret is ever
  in the source.
- **The data is one file on the owner's disk.** A backup is a copy made with
  SQLite's own backup interface (SQLITE-WHEN: "The File/Save_As menu option
  can be implemented using the backup API").

## The event schema the readers report

### Where an event comes from

It follows `radio-and-location.md`'s link:
- **A tag replies in its own slot** of the Reader's frame (§ Anti-collision
  there). Its report names the Field Generators it heard, with their signal
  strength, when the strongest one changes and with every check-in
  (§ Zones there).
- **The device that hears the reply** measures its signal strength. The
  module's radio reports a digital RSSI (PSIP p46), with an accuracy of
  ±3 dB from its sensitivity limit (PSIP §8.10, pp16–17).
- **That device is the event's reader:** the Reader, or an exit's Field
  Generator, which also listens (`radio-and-location.md` § The Chorus
  comparison, its first row). INFERRED: a listening Field Generator has no
  host link (`parts-and-toolchains.md` § The Field Generator), so it passes
  what it hears to the Reader by radio, and the event names both.
- **The host decides zones and alarms** from the events (simple readers, a
  smart server).

### The fields

| Field | What it is | Set by |
|---|---|---|
| `v` | the schema's version | the host |
| `tag` | the tag's ID, the number printed beside its QR code | the tag |
| `reader` | the device that heard the reply: a Reader (`R…`) or a Field Generator (`FG…`) | that device |
| `via` | the Reader that carried the event to the host, when it is not the hearer | the Reader |
| `rssi_dbm` | the reply's signal strength at the hearer, in dBm | the hearer's radio |
| `time` | when the reply was heard, in UTC, to the millisecond | the Reader's frame clock (below) |
| `seq` | the tag's own counter, one up per report | the tag |
| `payload.kind` | `checkin`, `zone`, `tamper`, `sensor`, `battery`, `answer` (to a command) or `join` | the tag |
| `payload.heard` | the Field Generators the tag heard, each with its signal strength | the tag |
| `payload.tamper` | the tamper loop: open or closed | the tag |
| `payload.temp_c` | the sensor's reading in °C, on a tag that has one | the tag (TMP117, `parts-and-toolchains.md` § The tags) |
| `payload.battery` | `ok` or `low` | the tag |

- **The tag's ID** prefixes nothing, unlike the original's `ACC`, `AST` and
  `INV` table keys (`MainForm.cs`). What a tag is used for is the host's
  record, not part of its ID.
- **`seq`** lets the host drop a reply heard twice, by the Reader and by a
  Field Generator, and count the replies a tag lost. The original removed
  duplicates in code too (`IsTagDuplicated`, `MainForm.cs`).
- **Events with no tag** come from a Reader:
  - `input`: one of its supervised inputs changed, to open, closed, faulty
    open or faulty closed (the original's four states,
    `original-system.md` § Alarms and I/O);
  - `relay`: a relay's change, confirmed;
  - `reader`: the Reader online, offline, or reporting its firmware version.
- **Every kind is named by a word,** never by a number that a second kind
  could share (§ The Programming Station).

An example, written for this document: a cold-chain item's check-in, heard
by Reader 1.

```json
{
  "v": 1,
  "tag": 40217,
  "reader": "R1",
  "rssi_dbm": -71,
  "time": "2026-10-28T00:21:07.250Z",
  "seq": 3187,
  "payload": {
    "kind": "checkin",
    "heard": [{ "fg": "FG3", "rssi_dbm": -58 }, { "fg": "FG4", "rssi_dbm": -77 }],
    "tamper": false,
    "temp_c": 4.2,
    "battery": "ok"
  }
}
```

**The schema is published as a JSON Schema file in this repo,** and the
build initiative writes it. Any device that sends events in that shape is a
reader to the host (§ The Chorus comparison, software side).

### Time, from the Reader's clock

- **The Reader's frames fix the time.** INFERRED: a reply's time is its
  frame's start plus its slot. The host sets the Reader's clock when it
  connects and once a minute after.
- **Its drift:** the module's real-time clock is good to ±50 ppm
  (PSIP p24), so a minute drifts 3 ms at most (INFERRED: 50 × 10⁻⁶ × 60 s).
- **Why not the host's arrival time, as the original used:** INFERRED. A
  burst of 1,000 replies spends most of a second on the link. Their order
  and spacing decide doorway hysteresis and exit passes. A deadline counted
  in UTC also survives a change of daylight-saving time.

### On the wire: the Reader's link

The Reader's host link is a UART behind a USB bridge of the CDC class
(`parts-and-toolchains.md` § The Reader). The UART's rate caps the replies a
second it can carry. The module's UART runs at up to 3 MBaud (PSIP §8.14.5.1,
p26).

INFERRED, at 10 bits a byte on the line and [3]'s worst case of 1,363
replies a second:

| Form on the wire | Bytes a reply | Line rate needed | Against 921,600 baud |
|---|---|---|---|
| A binary frame with the packet's CRC (ASSUMED 32 bytes) | 32 | 436,160 bit/s | 47 % |
| The JSON event above (ASSUMED 150 bytes) | 150 | 2,044,500 bit/s | 222 %, so over the line |

- **The choice:** the Reader sends compact binary frames, each closed by a
  CRC, as the original's one packet format was (`original-system.md` § The
  packet). The host's reader adapter turns them into the JSON events above.
- **JSON at full rate** would need 68 % of the module's 3 MBaud maximum,
  and the bridge's own limit is unread: the part is the build initiative's.
- **The schema is the API's,** not the wire's. A reader on another link (a
  later Ethernet Reader, or another maker's device) sends JSON straight to
  the API.

### The API

INFERRED, a sketch for the build initiative: HTTP with JSON, served by the
host on the user's own machine.

| Path | What it does |
|---|---|
| `GET /api/v1/tags`, `GET /api/v1/tags/{id}` | each tag's zone, state and pairing |
| `GET /api/v1/tags/{id}/history?from=…&to=…` | a tag's timeline (§ The Chorus comparison, software side) |
| `GET`, `PUT /api/v1/zones` | the floorplan's zones, their Field Generators and their neighbours |
| `GET`, `POST /api/v1/rules` | the alarm rules |
| `GET`, `POST /api/v1/pairings` | the pairs |
| `GET /api/v1/alarms`, `POST /api/v1/alarms/{id}/ack` | the alarms, and who acknowledged each, and when |
| `GET /api/v1/stream` | every event and alarm as it happens, as Server-Sent Events |
| `POST /api/v1/events` | events from a reader on another link, in the schema |
| `POST /api/v1/readers/{id}/commands` | call, query, enable or disable a tag; switch a relay; set the time |

The commands keep the original's set by name: call, query, enable, disable,
configure, read and write memory, and the relays (`original-system.md`
§ Tag memory and commands).

## The floorplan, the alarms and pairing

### The floorplan

INFERRED design:
- **The owner loads a picture of the building** and draws each zone over it
  as a shape. Each zone is named, linked to the Field Generators whose
  beacons define it, and linked to the zones beside it. Exits are marked as
  watched zones.
- **A tag is shown in its zone,** never at a point inside it. A zone is a
  room ([4], [13]) or an aisle ([22]). The radio places a tag in a zone, not
  at coordinates (`radio-and-location.md` § Zones). Each zone shows its count
  and its tags' names, and a click opens a tag's history.
- **The original's Mapper read a map picture too** (`MapViewImage`). Its
  source is not in the tree, so the rebuild writes the view fresh.

### The alarms

Each rule is set on screen. Each alarm shows on the page with a sound, can
switch a Reader's or an exit Field Generator's relay, and is acknowledged by
a named user with the time.

| Alarm | Who decides it | Set on screen | The binding number, and how it is met |
|---|---|---|---|
| Tamper | the tag: the loop on a wake pin sends a `tamper` report in the next frame | which tags and zones alarm | [14] 5 s: about 1.5 s for the seal (INFERRED, `parts-and-toolchains.md` § One core or three) |
| Temperature | the host: each reading against the limits | a low and a high limit, per tag or type | [15] 60 s: a reading every 20 s ([18]), checked as it arrives |
| Missed check-in | the host's own timer: a deadline per tag of its period × the misses allowed | the period and the count, per type | [16] 60 s: 3 × 20 s. INFERRED: a sweep every 0.25 s frame keeps the alarm within a frame of its deadline |
| Pairing | the host: a paired tag heard in a watched zone, with no partner heard there within a grace time | the pairs, the watched zones, the grace | [17] 5 s. INFERRED: 2.25 s to notice the zone, 0.25 s to the host, 1.0 s of grace (ASSUMED) and 0.5 s to draw make 4.0 s |

- **The tag reports; the host decides.** The temperature rule means "this
  tag's sensor reading crossed a limit someone set on screen"
  (`requirements.md` § Ground rules). It is not a medical device and not a
  monitor of vital signs (spec C8).
- **Missed check-in is new code.** It is the host's job by the protocol,
  and Smart Tracker's source shows no such alarm (§ Smart Tracker).
- **The rules also take** Chorus's two (dwell too long in one zone, and
  left a zone), the Reader's inputs, a low battery and a Reader gone
  offline.
- **E-mail and other outside messages** need a network and a mail service.
  INFERRED: they are not in milestone 1, and a later tier sends them only
  through the owner's own server.

### Pairing people with things

- **A pair joins two tags,** or a tag with a group of tags: a baby with the
  adults allowed to carry them ([10]), or a loaned headset with its borrower
  ([8]). It names the zones it watches, when it starts and ends (a loan, a
  stay), and who made it. INFERRED design.
- **Its alarm** fires when a paired tag is heard in a watched zone and none
  of its partners is heard there within the grace time (§ The alarms). A
  tag in an exit zone answers in every frame (`radio-and-location.md`
  § Zones), so both tags are heard there quickly.
- **Scan and scan.** Every tag carries its ID as a number and as a QR code.
  Pairing is: scan the item, then scan the person, or type the two numbers.
  In milestone 1 the scan happens at the host machine, in one of two ways:
  - a USB barcode scanner that types what it reads, as a keyboard does
    (INFERRED: it needs no driver);
  - the laptop's camera. A page may open a camera only in a secure context:
    "a page loaded using HTTPS or the file:/// URL scheme, or a page loaded
    from localhost" (MDN-GUM). The host's own page qualifies. A phone
    reaching the host over the LAN by plain HTTP does not, so phone scanning
    waits for the LAN tier's HTTPS.

  The page's QR decoder is the build initiative's choice: the browser's own,
  or a library under an allowlisted licence.

## The seal's HF interface (D5, the software side)

`radio-and-location.md` § The seal's HF interface gave the radio side: a
passive HF interface needs no transmitter of its own (INFERRED there).
`parts-and-toolchains.md` § One core or three called an HF memory one more
fitting option on the core.

**What a phone reading it would give the software:**
- the seal's ID and its tamper log, read by hand without a Reader, for
  example at a dock;
- tap to pair, in place of the printed QR code;
- a second path to a tamper record, for a seal out of the Reader's range.

**What it would cost:**
- **A web page reads NFC in one browser only.** Web NFC is in Chrome on
  Android from version 89 and in no desktop browser and no Safari, and it is
  still marked experimental (MEASURED from BCD, `api.NDEFReader`).
- **An iPhone would need a native app.** INFERRED, not read: that is one
  program per phone platform, outside the three-platform host and a
  beginner's build.
- **The phone would still have to reach the host's API** on the LAN, which
  is the later tier's (§ Pairing people with things).

**What already covers it in milestone 1:**
- the printed QR code and ID pair a tag with any scanner or camera
  (§ Pairing people with things);
- the radio carries the seal's tamper alarm within [14]'s 5 s and [23]'s
  30 s (`parts-and-toolchains.md` § One core or three).

**Recommended: no HF interface on milestone 1's seal.** INFERRED: it would
add a part and a phone-side program, for a hand read that no requirement
asks for. The HF memory stays a fitting option on the core, for a later tier
whose scenario needs it. The sitting decides it with the seal's design.

## The Chorus comparison, software side

Spec D12 and §3.8, and the research log's § 3. Each software-side item is
ADOPTED or DECLINED with its reason. The radio-side items are in
`radio-and-location.md`.

| Chorus item | Answer | Reason |
|---|---|---|
| An item history: a timeline per tag of zones, times and sensor values | **ADOPTED** | The original had a history between two dates (`TagHistoryView.cs`) but stored no signal strength and no sensor value. The rebuild's timeline holds zone entries and exits, alarms, readings and signal strength (§ The fields). INFERRED: raw check-ins are kept for a period the owner sets, and the changes for as long as the owner keeps the file. At 9.6 million warehouse check-ins a day, keeping every one forever is the owner's choice, not the default. |
| Alarm rules set on screen: a temperature range, dwell, left a zone, tampered, missed check-in | **ADOPTED** | The original already joined events to actions on screen (`ActionForm.cs`). The rebuild adds the requirements' four alarms with their limits, and Chorus's dwell and left-a-zone (§ The alarms). |
| "Find the nearest available X", ranked by zone | **ADOPTED,** by zone | INFERRED: "nearest" counts zones along the neighbour links drawn on the floorplan, since the radio places a tag in a zone and not at a point. "Available" means not paired and not alarmed. |
| A printed QR code and ID on every tag, so pairing is scan and scan | **ADOPTED** | § Pairing people with things. In milestone 1 the scan is at the host machine. Phones wait for the LAN tier's HTTPS (MDN-GUM). |
| View-only, time-limited share links | **ADOPTED for the LAN tier,** with a vendor's relay **DECLINED** | A link with an expiry, scoped to some zones or tags, that its maker can revoke: for example for a conference organiser. INFERRED: it needs the host reachable on the LAN, so it comes with that tier's HTTPS and hardened server (§ The choice). A relay through a vendor's server would be the cloud service declined below. |
| Not to copy: a cloud-only service | **DECLINED** | The host runs on the owner's machine and needs no internet after its install, and the data stays there. The research log names the reasons: it cannot work offline, and it is a privacy problem for hospitals and for tracking people (§ 3). |
| Not to copy: free hardware plus a subscription, with printed battery labels | **DECLINED** | The software is Apache-2.0 with no fee, and the hardware is built from the guides or bought by its owner. The cells are replaceable CR2032s in a tool-opened compartment (`parts-and-toolchains.md` § The tags). The research log's reasons: the model needs capital, and a beginner cannot manufacture printed labels (§ 3). |
| Four layers, a "reader" being any device that emits the schema | **ADOPTED** (by Story 2.1.3), **written here** | § The event schema. `POST /api/v1/events` is the door for a reader on another link. |

With `radio-and-location.md`'s five radio-side items, every Chorus borrow
and not-to-copy item in the research log's § 3 is answered.

## Does ActiveWave's approach still win (software side)

The software side of spec §3.8's question. The radio side, with room-level
zones, relay outputs and wake-on-command, is `radio-and-location.md`'s.

- **Owned infrastructure with no subscription: it still wins.**
  - A nursery's alarms cannot wait on a provider's network or uptime.
    Tracking people stays on the owner's own machine.
  - There is no recurring fee to fund.
  - The cost is the owner's. They keep the host running and copy one file
    as a backup (§ Security on the user's machine). INFERRED: that is less
    than the original asked, a MySQL server to run.
- **Pairing people with things: it still wins.**
  - Chorus binds a tag to a shipment, a SKU or a purchase-order line
    (INFERRED there), and it "never pairs a person with a thing" (research
    log § 3).
  - The requirements need the pair at an exit in every scenario: [8] 15 s,
    [17] 5 s and [26] 30 s.
  - The original paired a car only with its owner's parking space
    (`CarTrackingClass.cs`). The rebuild makes the pair general: a thing
    with a person, or a person with a person (`requirements.md` § The five
    measures, defined).

## What the choice leaves to later stories

- **Story 2.1.6:** nothing to price for the host itself. Python, pyserial,
  SQLite and the browsers cost nothing. A USB barcode scanner is an
  optional bench item.
- **Story 2.1.7:** the sitting decides:
  - A or B, Python or Node.js (§ The choice);
  - the seal's HF interface (§ The seal's HF interface), with the seal's
    design;
  - how long raw check-ins are kept by default.
- **The build initiatives:**
  - the JSON Schema file, and the binary frame on the Reader's wire;
  - the USB bridge's own baud limit, from its datasheet;
  - the host's first measurements on Windows and Linux, and with a Reader on
    all three platforms;
  - the guides' install steps for each platform, checked on each;
  - pyserial copied in with its trail, or installed by `pip`;
  - the QR decoder;
  - the LAN tier: HTTPS, user accounts, share links and phone scanning.

## Sources

**The original,** at the tag `activewave-2018` (`docs/reference-repos.md`
reading rule 2), all under `software/`:
- `readme.md`;
- `smart-tracker/Source/Server/`: `Smart Tracker.csproj`, `MainForm.cs`,
  `CommForm.cs`, `DatabaseCheck.cs`, `ActionForm.cs`, `AssetForm.cs`,
  `CarTrackingClass.cs`, `TagHistoryView.cs`, `ReaderListView.cs` and
  `Test reports/`;
- `smart-tracker/Source/AWIComponentLib/AWIComponentLib/`:
  `CommunicationClass.cs`, `NetRdrConnClass.cs`, `IAPIEventHandler.cs` and
  `EventActionClass.cs`;
- `smart-tracker/Mapper.exe.config` and `awi-sounds/`;
- `programming-station/Programing Station 191/Programming Station 191/`:
  `ProgStationUnit.cpp`, `Commands.h`, `ComConfigPage.cpp` and `TCPIPUnit.cpp`.

**Pinned in the reference corpus** (`docs/reference-corpus.tsv`, folder
`sources/`, licence `third-party`, study only):

| Short name | Document | Pin (sha256, first 12) | URL |
|---|---|---|---|
| PSIP | TI, "CC1312PSIP SimpleLink Sub-1-GHz Wireless System-in-Package", SWRS293, November 2023 (pinned by Story 2.1.3) | `da5eb47438cf` | `https://www.ti.com/lit/ds/symlink/cc1312psip.pdf` |

**Cited by URL and fetch date** (fetched 2026-10-28; none pinned, plan D8):

- **PYSERIAL:** pySerial's documentation, `https://pyserial.readthedocs.io/en/latest/pyserial.html`.
- **PYSERIAL-LIC:** pySerial's licence, BSD-3-Clause, `https://raw.githubusercontent.com/pyserial/pyserial/master/LICENSE.txt`.
- **PYTHON-DL:** `https://www.python.org/downloads/` (Python 3.15.2, released Oct. 25, 2026).
- **PY-SQLITE3:** `https://docs.python.org/3/library/sqlite3.html` (the 3.15.2 documentation).
- **PY-HTTP:** `https://docs.python.org/3/library/http.server.html` (the 3.15.2 documentation).
- **SQLITE-WHEN:** SQLite, "Appropriate Uses For SQLite", `https://www.sqlite.org/whentouse.html`.
- **SQLITE-FAQ:** SQLite's FAQ, item 19, `https://www.sqlite.org/faq.html`.
- **NODE-SQLITE:** `https://nodejs.org/api/sqlite.html` (the Node.js v26.12.1 documentation).
- **SERIALPORT:** `https://serialport.io/docs/guide-installation`.
- **GO-INSTALL:** `https://go.dev/doc/install`.
- **APPLE-GK:** Apple, "Safely open apps on your Mac", `https://support.apple.com/en-us/102445`.
- **APPLE-DEV:** Apple Developer Program, `https://developer.apple.com/programs/`.
- **MDN-SERIAL:** MDN, "Web Serial API", `https://developer.mozilla.org/en-US/docs/Web/API/Web_Serial_API`.
- **MDN-GUM:** MDN, "MediaDevices: getUserMedia() method", `https://developer.mozilla.org/en-US/docs/Web/API/MediaDevices/getUserMedia`.
- **BCD:** MDN's browser compatibility data, read 2026-10-28 at 00:19:22Z
  and 00:24:28Z: `https://raw.githubusercontent.com/mdn/browser-compat-data/main/`
  `api/Serial.json`, `api/NDEFReader.json`, `api/EventSource.json` and
  `svg/elements/svg.json`.
- **MCHP-CDC:** cited through `parts-and-toolchains.md` § Sources, not
  fetched again.

**Searched, not read beyond the search's results:** GRAIL RTLS, reelyActive's
rtlips-server and Trace Me Now (§ What was read).

Page numbers are each PDF's own page count (page 1 is the cover page).
