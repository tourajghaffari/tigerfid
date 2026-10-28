# Completed work — tigerfid

Archive of closed epics and executed sprints, cut from `docs/project-plan.md`
at epic close (the close PR is the archival PR). Newest first within each
section.

## Executed sprints

### Sprint 2 — "The study makes its choices: a radio a beginner may legally run, the parts and free tools to build it, and host software for any laptop" (proposed 2026-10-27; 4 legs, serial) — ✅ APPROVED (2026-10-27) — ✅ EXECUTED (2026-10-28)

**Executed:** 2026-10-28, 4/4 legs strictly serial (D7), each by a fresh
repo-rooted executor seat on Air Traffic Control's orders. None was elevated.
- **The seats** (each leg's friction-log section has its fresh-session read):
  - **leg 1:** "tigerfid Sprint 2 Executor" `014c145a-…`;
  - **leg 2:** "tigerfid Sprint 2 leg 2" `f367e3c4-…`;
  - **leg 3:** "tigerfid Sprint 2 leg 3" `326f43fb-…`.

    Each of these three was a `/clear` of the process `c59c545c-…`.
  - **leg 4:** "tigerfid Sprint 2 leg 4" `a60005ee-…`, fresh by launch.
- **The legs,** each clock GitHub's merge stamp:
  - **Before leg 1, its own PR — #28 → `fc86cd14` (2026-10-27,
    18:09:01Z):** the root `.gitattributes` pins LF line endings (F4, F5).
  - **Leg 1, Story 2.1.9 — #29 → `bdd23c4f` (18:16:19Z):** the
    original-system study lists its three corrections to the onboarding
    spec. The Outlook message's three mentions say why it stays closed.
  - **Leg 2, Story 2.1.3 — #31 → `ef5c2895` (18:55:56Z):** the law read and
    dated first, then one sub-GHz band on TI's certified CC1312PSIP module,
    reader-assigned slots and zones from Field Generators.
    - [3] against [2] was met with no ASK-OMID row (D2).
    - Three sources were pinned (294 rows).
  - **Leg 3, Story 2.1.4 — #33 → `3068168d` (19:39:32Z):** each device's
    parts and the free tools on three platforms (cited, none run), the
    LP-XDS110ET, a 12 s wake for [27] (D4, no row), and one core recommended
    (D3).
    - The FCC grant record is written as not read, on Omid's first-hand
      "Write it as not read".
    - Five sources were pinned (299 rows).
  - **Leg 4, Story 2.1.5 — #36 → `bbc791d7` (2026-10-28, 00:35:02Z):** the
    original host programs read at the tag, a local Python host with Node.js
    the runner-up, the event schema, the Chorus software items, and no HF
    interface recommended for milestone 1's seal (D5). macOS was measured,
    and Windows and Linux cited.
- **The manifest,** `docs/close/2.1.json`, flipped as mapped:
  - leg 1 flipped no tooth;
  - leg 2 flipped `radio-and-location-doc` and `legality-rules-cited`;
  - leg 3 flipped `parts-and-toolchains-doc`;
  - leg 4 flipped `software-stack-doc`.

  `tiered-cost-doc` and `rebuild-spec-approved` stay RED, as the block said.
- **No ASK-OMID row** was filed by any leg. **No D9 split:** the legs
  planned 6, 8, 8 and 8 tasks.
- **The record:** `docs/friction-log-sprint2-the-study-makes-its-choices.md`
  (F1–F22).
- **Open for the review** (each a deferred Minor or a routed item):
  - **Leg 1:** none recorded.
  - **Leg 2:**
    - F11: `scripts/reference-check.mjs` still prints "copy-only" for a
      missing `sources/` file, where D8 lets it be fetched from its row's
      URL. The script was outside every leg's grant, and leg 4 met the
      message again (F18).
    - F9: the block's "the law before the radios" did not say whether it
      binds the reading order or the document's order.
  - **Leg 3:**
    - the FCC grant pages for ZAT-1312PSIP-2 and -3, a browser read before
      any part is bought (F13, F15);
    - the hub's `CLAUDE.md` still says "291 pins" for this corpus. It is
      hub-owned (#33's body).
  - **Leg 4:**
    - legs 1–3's Completed lines name no pre-tick head, which KB
      `sprint-proposal-standard` § Roles asks for; leg 4's does;
    - F19: a search that prints no count at all is re-run in text mode
      before a finding rests on it;
    - F21: the orders' path grants for leg 4 omitted the research log.
  - **The register seats,** sent to Air Traffic Control as text with leg 4's
    DONE line, each caught:
    - `zsh-equals-face`;
    - a new face, class id not known: the harness `grep` silent over a
      non-UTF-8 file;
    - two `piped-checker-exit` reads;
    - a `ugrep-bounded-repetition-exceeds-complexity-limit` warning;
    - a cwd-drift warning;
    - close-lint's reviewed-date face;
    - the grant-list omission;
    - a comment read as live code.

**Reviewed:** 2026-10-28, by the Sprint 2 reviewer pass (tigerfid #41). The
pass was the hub-rooted Windows session "tigerfid Sprint 2 review" (session
`2a0782f7-f632-4bab-b967-a9cfbfe1ba93`), on Air Traffic Control's orders of
10:00:08Z. It was fresh by launch: its background-task path id is its own
session id, so no earlier process was cleared. It ran without elevation and
held no other role in this sprint. **Verdict: every leg meets its Done-when
and its gate, with findings.** Six findings are Important. Two are fixed in
this PR, and four go to the Sprint 3 proposal as a fix-up to the studies.
Omid ruled one of them first-hand in this pass.
- **The method** (hub KB `parallel-per-leg-reviewers-then-re-measure`):
  - four fresh, read-only readers, one per leg; leg 1's reader also took
    #28, #37 and #39;
  - then the pass re-measured every Important candidate itself, against the
    pinned sources, the tag `activewave-2018` and the law's own text.
  - Each finding below says what was MEASURED and what is INFERRED.
- **Leg 1 (#29; Story 2.1.9): PASS.**
  - The three corrections and their table rows are in place. Both of the
    story's greps, re-run on `main`, match #29's dispositions.
  - The tick's MEASURED claim was re-counted in the reference folder. The
    Transceiver's pick-and-place names the 916.5 MHz SAW filter, the BGA2001
    and the SA636 once each. The Reader's Rev 6 netlist names none of the five
    radio parts.
  - **#28, before the leg:** `.gitattributes` marks only `license` and
    `_assets/` as never rewritten. Every stored file is LF (57, none CRLF),
    and the rebuild's future folders check out as LF text.
- **Leg 2 (#31; Story 2.1.3): PASS.** Re-measured, each reading as the study
  says:
  - 1,363 slots a second: 20 bytes at 300 kbit/s plus a 0.2 ms guard;
  - the link's 21.8 dB at the worst body loss, and the zone move's 3.0 s;
  - the module's figures and certification (PSIP p1, p19, p54);
  - the three pins' sha256;
  - §15.231, §15.23, §15.212 and §15.247 at the eCFR's API for 2026-10-23,
    and Decision 2006/771/EC's band 47 row.

  The Chorus radio items are answered one by one: four ADOPTED, the
  crowd-sourced network DECLINED, and ActiveWave's zones, relays and
  wake-on-command each still win.
- **Leg 3 (#33; Story 2.1.4): PASS.**
  - Every figure of the battery arithmetic reproduces from the study's own
    inputs.
  - Each datasheet figure checked reads as cited: PSIP, SWRA797, the two
    Energizer sheets, TMP117, SWRA349 and SSZZ027.
  - The five pins match their sha256.
  - KiCad 10.0.7, the SDK 8.33.00.16 and TI Arm Clang 5.1.1.LTS read at their
    vendors' pages as cited.
- **Leg 4 (#36; Story 2.1.5): PASS.** The block's three independent checks:
  - **The quotes against the tag:** 55 of the study's phrases from the
    original's source were checked, and 51 match byte for byte. The other four:
    - a paraphrase set in code type, `EmployeesQuery.CheckAccess(tag, zone,
      time)`, where the source's signature is `CheckAccess(uint id, int zone,
      DateTime date)`;
    - two table names in changed case;
    - a file name, `WinSock2.h`, which is a file in the folder.
  - **The Chorus list:** the five software items ADOPTED, the two
    not-to-copy items DECLINED, and the four layers written.
  - **The source read against its comments** (friction F22): the claims
    about what the code does sit on live code. Re-classified by this pass,
    the cipher calls, the key set-up, ECB and the no-response code each have
    live uses. The 263 `DateTime.Now` uses include 39 inside comments (224
    live); the point they make, a local time stamped by the host, stands.
  - **Privacy:** of 32 secret-shaped values in the original's source, the
    ten the study shares are common words, identifiers, "localhost" inside an
    MDN quote and a file's name. No key byte, connection-string value or
    developer's name is quoted.

  pyserial's licence, `node:sqlite`'s release-candidate status and the UART's
  3 MBaud read as cited.
- **#37, the stamp:** every sha and merge clock matches GitHub's record. The
  flip map holds through the close runner on `main`: the four study teeth
  PASS, and `tiered-cost-doc` and `rebuild-spec-approved` FAIL, each for its
  right reason.
- **#39, after the stamp:** read as a record of Omid's decisions, not as a
  leg. Its retention sizing reproduces. Finding 1 changes one of its three
  decisions.
- **Findings, Important:**
  1. **The seal's HF decision said more than the recommendation it cited.**
     - **MEASURED:** #39 recorded "no space reserved for one on its core"
       under Omid's "I like all those recommendations". The written
       recommendation, unchanged since #36, says "The HF memory stays a
       fitting option on the core, for a later tier" (`software-stack.md`),
       and `parts-and-toolchains.md` says the same.
     - **The basis it records is wrong too.** "A web page reads NFC in one
       browser only": MDN's data lists Web NFC in Chrome, Opera, Samsung
       Internet and WebView on Android. That is still Android only, so
       milestone 1's no-HF decision stands.
     - **Omid's ruling,** first-hand in this pass's terminal between its
       `date -u` reads of 10:01:36Z and 10:27:02Z, after a recommendation:
       "Reserve footprint, coil off-core (Recommended)". The core keeps an
       unfitted footprint for an NFC memory, its I²C link and two antenna pads
       at the board's edge. The coil lives in the enclosure, away from the
       module's antenna and its ground plane, which TI asks to be "as large as
       possible" with "as little as possible routing" (SWRA797 §3.1.2).
     - **Fixed in this PR:** Story 2.1.7's Context.
     - **Routed to the Sprint 3 fix-up:** `software-stack.md`'s Decided line,
       its table row and the "one browser only" sentence. The two "fitting
       option" lines are right as they stand.
     - **Open for the build, INFERRED:** whether a passive NFC coil counts as
       "any other antenna" under the module's no-co-location condition (PSIP
       p54) is unread. A passive tag sends nothing of its own, so it should
       not.
  2. **The FCC grant read had no owner, and the radio study names one of
     the two IDs.** MEASURED:
     - the read "before any part is bought" is written only in Story 2.1.4's
       Outcome and its study, in no live story;
     - `radio-and-location.md` names ZAT-1312PSIP-2 alone, with no mention
       of -3. Its own pinned source points to "fccid.io/ZAT-1312PSIP-3"
       (SWRA797 p14) and lists the -3 filings (p17).

     **Fixed in this PR:** Story 2.1.7's Context gives the read to the
     sitting, by Omid's hand in a browser, before the spec names an FCC ID.
     **Routed to the Sprint 3 fix-up:** a pointer to both IDs in the radio
     study.
  3. **Two battery margins assume the sensor's cheapest setting, where its
     cited accuracy is not specified.** MEASURED:
     - the TMP117's ±0.1 °C is specified at "8 averages, 1-Hz conversion
       cycle" (TMP117 p6), and its factory setting averages eight conversions,
       124 ms (p12);
     - `parts-and-toolchains.md` charges one 15.5 ms conversion.

     At eight averages, [18]'s year holds by 1.8 %, not 6 %. [27]'s
     cold-chain item tag at a 12 s listen holds by 1.4 %, not 5 %. The study's
     advice, to measure first and then take the larger cell, stands on a
     thinner margin. **Routed to the Sprint 3 fix-up.**
  4. **No sentence says how a tag hears a Field Generator it has not heard
     before.**
     - **MEASURED, by search across the three studies:** a tag listens only
       in "the slots of the Field Generators it last heard", and its
       listening budget is the beacon and two slots.
     - **INFERRED, the failure:** a tag carried into a room whose Field
       Generator is not among them never listens in that room's slot. Both
       the 3.0 s move and [18]'s battery assume the next listen hears it.

     **Routed to the Sprint 3 fix-up:** the mechanism, for example the
     Reader's beacon naming the slots of the zone's neighbours, with the
     listening budget re-priced.
  5. **Two choices rest on figures with no source or mark (plan D10).**
     MEASURED:
     - **The no-IDE build path:** CMake and GNU make have no platform,
       install or licence entry. "TI's free compiler" and SysConfig carry "no
       price stated on the page". The study still says every tool's platforms
       are cited except the Arm GNU Toolchain's.
     - **The binary wire frame:** the "222 %, so over the line" that decides
       it is measured against 921,600 baud, a figure with no source and no
       ASSUMED mark. The module's UART runs at 3 MBaud (PSIP p26), and the USB
       bridge's limit is unread.

     **Routed to the Sprint 3 fix-up.** The closer's
     `free-tools-three-platforms` needs the first.
  6. **Legs 2 and 3 assigned work to later stories that none of those
     stories carried** (register class
     `premark-promise-not-written-into-owning-story`; its second seat here,
     after the Sprint 1 review's finding 1). MEASURED: their studies' § What
     the choice leaves to later stories gives Stories 2.1.6 and 2.1.7 items
     those stories' text did not hold. Leg 4 wrote its own into both (#36,
     #39). **Fixed in this PR,** on Air Traffic Control's grant: one dated
     Context sentence each on Stories 2.1.6 and 2.1.7. No Done-when moved.
- **Nits** (five, the cap). Each is MEASURED, and each goes to the Sprint 3
  fix-up with findings 1–5:
  - `software-stack.md` says the original's event "carries no time". Its
    `rfTagEvent_t` has a `logTimestamp` member (`AWI_API.h`), which Smart
    Tracker's source never reads. What the host lacked was a time of hearing.
  - `parts-and-toolchains.md` quotes §2.803(d)(2)(iv)'s notice as ending at
    "FCC license holder". It goes on: "or must secure an experimental
    authorization under part 5 of this chapter." The conclusion stands.
  - `radio-and-location.md` calls a 500 kbit/s link "no worse by the tables"
    at −95 dBm, from the CC1312R7's sheet. The chosen module's own sheet prints
    −90 dBm under the same conditions (PSIP p16). The verdict stands: it is
    declined for its certification.
  - `parts-and-toolchains.md` says the radio study "counted only the sending
    antenna". The radio study's table counted the Reader's +2 dBi too. The
    21.8 dB → 20.7 dB is that antenna's change to +0.90 dBi.
  - `software-stack.md` twice calls 1,363 a second "[3]'s". [3] is 1,000
    tags a second; 1,363 is the radio study's slot capacity.
- **The block's "Open for the review" list, each item's disposition:**
  - **F11,** the reference check's "copy-only" message for `sources/`: routed
    to the Sprint 3 proposal as a story of its own, outside the study.
    `scripts/` is outside this pass's paths. It gates nothing, but it misleads
    each new machine's first verify.
  - **F9:** no change. The study states its reading order with clock reads,
    and the closer judges `legal-texts-read-dated` on it.
  - **The FCC grant pages** (F13, F15): finding 2.
  - **The hub's `CLAUDE.md` "291 pins"** (299 now): sent to Air Traffic
    Control with this pass's DONE line, because the file is the hub's.
  - **Legs 1–3's pre-tick heads:** no change. The block's merge-loop line
    never asked for one, and this record's squash shas locate each leg.
  - **F19, F21 and the register seats leg 4 sent as text:** each is in the
    register. F19's "23 includes" counts the word "include": the file has 21
    `#include` lines.
  - **The friction log's suggested fixes that the list left out:**
    - F8 and F13 (name the browser-only records up front): Story 2.1.6's new
      Context sentence carries them for its prices;
    - F16's lesson is D6's own rule;
    - the proposal's F2 is advice to the next study manifest's author.
- **Register seats:** the friction log's § Reviewer pass lists fifteen, each
  sent to Air Traffic Control as text by class id.
- **Routed to the Sprint 3 proposal, in order:**
  1. a fix-up story for the three studies, from findings 1–5 and the nits.
     It goes before Story 2.1.6 prices the parts, since finding 3 can move a
     cell and finding 4 the listening budget;
  2. Story 2.1.6, with no D9 remainder from this sprint;
  3. F11's message fix, as its own story.

  Then Story 2.1.7's sitting and Story 2.1.8's close.
- **Checks:** `node scripts/close-lint.mjs` ok; `npm test` 17 of 17; `node
  scripts/reference-check.mjs` 299 rows verified on this machine.

**Approval:** approved 2026-10-27 (Omid, first-hand in the proposer's
terminal "tigerfid Sprint 2 Proposal", between its `date -u` reads of
17:29:28Z and 17:29:48Z: "Approve as proposed (Recommended)"): the block as
proposed, four legs, D1–D11 at their defaults. The sign-off edit is the
proposer's, on the proposal PR's branch (tigerfid #26).

**Proposed:** 2026-10-27 by the repo-rooted session "tigerfid Sprint 2
Proposal" (session `49c256bc-78bf-445e-b2a6-f970dfb1f707`), on Air Traffic
Control's orders of 17:13:26Z. The session was fresh by `/clear`: its
background-task path id, `c59c545c-e735-4344-b0b8-697d9f897dca`, is the
Sprint 1 leg 4 session's process, which the Sprint 1 reviewer pass used
last. Role proposer; not elevated.

**The outcome:** Epic 2.1's three technical studies written, each making its
choice, so that the tiered cost has parts to price and Omid's decision sitting
has a design to decide:
- the Sprint 1 review's two fixes land first (Story 2.1.9);
- a radio and location architecture that meets the requirements and that a
  beginner may legally run (Story 2.1.3);
- each device's parts, with a free toolchain on Windows, macOS and Linux
  (Story 2.1.4);
- the host software's stack and the event schema its readers report
  (Story 2.1.5).

**Admission (the meta-work test):** every leg ships a public document that a
stranger reads on GitHub this month, and together they set the direction that
opens this repo to pull requests (spec D10).

**Inputs swept 2026-10-27.** The scope is the whole live plan (it is small:
the preamble, the empty ASK-OMID register and Epic 2.1), the teeth and prose
of `docs/close/2.1.json`, and every record the stories cite.
- **The events digest.** `git log origin/main --oneline --grep '<id>'` for
  each of 2.1.3–2.1.8 finds only the Sprint 1 review (#23), which wrote their
  Context sentences. `docs/notes.md` and the archive name them only as forward
  work. No PR is open, and `git ls-remote --heads origin` lists `main` alone.
  Every story is CURRENT; none has run.
- **The cited anchors exist:**
  - in `docs/study/requirements.md`: § Ground rules, § The arithmetic behind
    the numbers, and items [2], [3], [13], [21] and [27];
  - in `docs/study/original-system.md`: § The records read, § What no record
    states and § What this means for the study.
- **FALSIFIED, fixed in this PR:**
  - Epic 2.1's manifest clause, "every deliverable is still to be written"
    (two documents exist);
  - its Ordering line's "2.1.3 and 2.1.4", now serial (D7);
  - `free-tools-three-platforms`' "the chosen boards are drawn in KiCad 10".
    No board is drawn before the build initiatives, so the check now asks that
    every board the study plans is planned in KiCad 10.
- **FALSIFIED, routed to leg 1:** three documents still say the Outlook
  message waits on ASK-OMID A1 (Story 2.1.9).
- **The teeth, run:** the close runner reads `lint`, `requirements-doc` and
  `original-system-doc` PASS. The four unwritten documents' teeth,
  `legality-rules-cited` and `rebuild-spec-approved` read FAIL, each for its
  right reason (the file is absent).
- This PR refreshes the plan's staleness stamp.

The standard (KB `sprint-proposal-standard`) was read by its steps 0–8 and
the newest entries of its friction log, not entry by entry.

**Why this order (step 2):**
- **Leg 1** is a short docs fix, and it goes first: three public documents
  tell strangers something no longer true, and leg 2 reads one of them.
- **Leg 2** decides what the others build on: the radio architecture and its
  legality. It answers [3] against [2] first.
- **Leg 3** names the parts for that architecture: the radio, the
  microcontrollers and their toolchains.
- **Leg 4** chooses the host software for the event schema the readers
  report, which follows leg 2's link.

**The sprint's shape.** Air Traffic Control's lean was four legs, with Story
2.1.6, the tiered cost, proposed after this sprint's review. Weighed, it
stands:
- **For:**
  - 2.1.6 prices exactly the parts leg 3 names and the radio leg 2 chooses.
    This sprint's review re-checks those choices before dated prices sit
    beside them, so a review finding that moves a part re-prices nothing.
  - A requirement put back to Omid in legs 2–3 (D2) can move a part too.
  - Prices dated nearer the sitting are fresher for it.
  - A remainder the session-fit tripwire splits off leg 2 or leg 4 (D9) has a
    sprint to land in.
- **Against:** one more proposal and review, two fresh seats, for a short
  sprint.
- **Declined:** a fifth leg for 2.1.6, gated on "no ASK-OMID row open from
  legs 2–3". That gate cannot see a review finding.

1. **Leg 1 — Story 2.1.9** — the Sprint 1 review's two fixes in
   `docs/study/original-system.md`, `docs/reference-research.md` and
   `docs/reference-repos.md` reading rule 7. Gate: the story's two greps
   quoted in the PR body, each hit's disposition beside it. Flip map: no tooth
   flips; `original-system-doc` stays PASS; `original-system-review-fixes` is
   the closer's. Item-8 scope: the three files. The archive's Sprint 1 record
   is history and is not edited.
2. **Leg 2 — Story 2.1.3** — `docs/study/radio-and-location.md`, covering:
   - the options against the requirements, [3] against [2] first (D2);
   - the legality reads of D6, each cited before any radio is named;
   - the chosen architecture, with how a zone move is noticed within [13]'s
     5 s;
   - what the chosen radio states that no original record does: the bit rate,
     modulation, output power, certification and the wake signal's form;
   - the Chorus radio items, one by one.

   Flip map: `radio-and-location-doc` and `legality-rules-cited` FAIL → PASS.
   Prose for the closer: `legality-and-modules`, `legal-texts-read-dated`,
   `chorus-items-answered` (its radio half) and `no-medical-claim`. Item-8
   scope: `docs/study/requirements.md` items [2], [3], [13] and [21] (D1), and
   `docs/study/original-system.md` § What no record states.
3. **Leg 3 — Story 2.1.4** — `docs/study/parts-and-toolchains.md`, covering:
   - the parts per device for leg 2's architecture;
   - each part's free toolchain and its install on Windows, macOS and Linux
     (D10);
   - a debugger a beginner can afford;
   - the licence of each reference design read;
   - [27]'s choice (D4);
   - the requirements' ASSUMED figures replaced (D1);
   - §3.7's one-core-or-three weighed, the sealTag as a new design inside it
     (D3).

   Flip map: `parts-and-toolchains-doc` FAIL → PASS. Prose for the closer:
   `three-platform-claims-sourced`, `free-tools-three-platforms` and
   `no-medical-claim`. Item-8 scope: `docs/study/requirements.md` § The
   arithmetic behind the numbers and item [27] (D1), and
   `docs/study/original-system.md` § What this means for the study.
4. **Leg 4 — Story 2.1.5** — `docs/study/software-stack.md`, covering:
   - the host stack, local first, on all three platforms;
   - the event schema the readers report;
   - the original host applications' source, read at the tag;
   - whether the seal keeps an HF interface for a phone (D5);
   - the Chorus software items, one by one.

   Flip map: `software-stack-doc` FAIL → PASS. Prose for the closer:
   `chorus-items-answered` (whole once this leg lands),
   `three-platform-claims-sourced`, `free-tools-three-platforms` and
   `no-medical-claim`. Item-8 scope: `docs/study/original-system.md` § The
   records read, and `docs/study/radio-and-location.md` where it names the
   link.

After the sprint, `tiered-cost-doc` (Story 2.1.6) and `rebuild-spec-approved`
(Story 2.1.7) stay RED.

**Decisions (step 5) — no decision currently blocks execution on Omid.** Each
is the executor's at its default unless Omid says otherwise at the approval.

- **D1 — A study chooses; the sitting decides.** Each document names its
  choice with its reasons and its rejected options, and that choice is the
  sitting's default (Story 2.1.7).
  - A number Omid set or accepted in `docs/study/requirements.md` changes
    only by his word.
  - A study that replaces an ASSUMED or INFERRED input there carries the new
    figure and its source. `requirements.md` gains a dated pointer line
    beside the old figure, and its text stays otherwise unchanged.
- **D2 — [3] against [2].** Leg 2 answers first whether one reader takes
  1,000 replies a second and still hears a worn tag 30 m away.
  - If no legal option meets [3], the leg puts the closest option, its
    figure and its cost to Omid first-hand in its terminal, and records his
    words.
  - If he cannot be reached in the leg, it files ASK-OMID A2 with that
    question. Its default: [3] stands unchanged and the study's choice is the
    closest option. The leg proceeds on the default.
- **D3 — One core or three, and the sealTag.** Leg 3 weighs §3.7's question,
  with milestone 1's sealTag as a new design inside it (the 2012 seal has no
  active radio), and names its recommendation. The sitting decides. No board
  is drawn this sprint.
- **D4 — [27], the warehouse item tag.** Leg 3 chooses between a slower wake
  and a CR2450-size cell, and states what each costs [27]'s five years. It
  goes to Omid only if neither choice keeps five years.
- **D5 — The seal's HF interface.** Leg 2 states the radio side (whether it
  needs a transmitter of its own, and the rule if so). Leg 4 states what a
  phone reading it would give the software, and records the recommendation.
  The sitting decides it with the seal's design.
- **D6 — The legality reads come before any radio is named** (spec C9). Leg 2
  reads and cites each text below, by URL and the version date the text
  prints, and names it as read before it compares radios. Where the list came
  from: spec C9 names the four US sections the tooth reads. The proposer added
  the rest from its own knowledge, not read at proposal, so the leg confirms
  each one exists and applies before citing it, and says so where one does
  not.
  - **US, 47 CFR Part 15:**
    - §15.231, periodic operation at 433 MHz;
    - §15.249 and §15.247, 902–928 MHz and 2.4 GHz;
    - §15.23, home-built devices;
    - §15.212, modular transmitters: what a pre-certified module's grant
      lets a beginner build without authorisation of their own;
    - Subpart F, if UWB is compared.

    A dated URL (the eCFR's point-in-time view, where the leg finds it)
    pins each version. The tooth `legality-rules-cited` reads the first four
    section numbers.
  - **EU:**
    - the Radio Equipment Directive, 2014/53/EU;
    - the short-range-device harmonisation, Decision 2006/771/EC in its
      current consolidated version;
    - CEPT ERC Recommendation 70-03;
    - the harmonised standards for the bands compared: ETSI EN 300 220
      (sub-GHz), EN 300 328 (2.4 GHz) and EN 302 065 (UWB, if compared).

    Each is cited at the version the leg reads, and a text found superseded
    is cited at its successor.
  - **Limits on the reads:** public pages only (reading rule 3). A standard
    behind a paywall is cited by its number and edition, with the
    regulation's own summary in its place, and marked so.
- **D7 — Serial, not parallel.** Leg 3's radio parts follow leg 2's
  architecture, and leg 4's reader abstraction follows leg 2's link. A
  parallel 2.1.4 would weigh chips for an architecture 2.1.3 might decline.
- **D8 — Sources: pinned when a choice rests on them.** A datasheet,
  application note or reference design a choice rests on is pinned in the
  reference corpus under one folder, `sources/`, by the rules of
  `docs/reference-repos.md` § Adding to the corpus: its row, its sha256, its
  URL and licence, and its research-log entry with a verdict, in the leg's PR.
  - A law text is cited by its dated URL and its own version date, not
    pinned: the version date is its pin.
  - Pages read without a choice resting on them are cited by URL and fetch
    date.
  - The other machine fetches a new row from its URL, and it counts once its
    bytes match; otherwise it is copied from the machine that pinned it.
- **D9 — A leg is a session.** The tripwire: an executor's plan above eight
  tasks stops before its first task, narrows the leg in story order, splits
  the remainder off as a new story under the same step, and logs the miss
  against this proposal. Two seams are named now, so a split is not
  improvised:
  - **Leg 2:** the Chorus radio items and the "does ActiveWave's approach
    still win" answers become the remainder. They gate nothing in leg 3.
  - **Leg 4:** the Chorus software items become the remainder. The original
    host source stays in the leg, because the stack is chosen against it.

  A session limit mid-leg pushes the branch and leaves a notes DRAFT, and the
  resumed sitting re-runs the re-verify set first.
- **D10 — Every claim is sourced or marked.** The house style of Sprint 1's
  two documents holds:
  - every number and every "the rule allows" or "the part supports" sentence
    names its source (the law's section and version date, the datasheet's
    title, revision and page), is MEASURED with the read, or is INFERRED with
    its arithmetic;
  - an install on a platform is MEASURED (the box's OS, the date and the
    version the tool prints) or cited from the vendor's documentation (URL
    and fetch date). It is never stated as run where no session ran it
    (`three-platform-claims-sourced`).
  - **Also binding:**
    - no figure from the 2005/2017 datasheets becomes a requirement (spec
      Appendix B);
    - no medical claim (spec C8);
    - no third-party text, figure or file is copied into the repo (the
      licence lane);
    - nothing from the reference folder is committed;
    - the Outlook message stays unread (reading rule 7).
- **D11 — Merges.** Every leg is docs-only (`docs/**` alone). Each merges by
  `gh pr merge --auto --squash --match-head-commit <full sha>` under the
  session's merge delegation as it stands, or by Omid's word. None is assumed.

**Checks per leg:**
- the flip map, through `node <hub>/scripts/close/close-epic.mjs --epic 2.1
  --repo <F> --only <ids>`;
- `node scripts/close-lint.mjs` and `npm test` (the `test` job's commands),
  unpiped, on every leg;
- `node scripts/reference-check.mjs` on every leg that adds corpus rows.

**Gate line (every leg):** the required set is `scope` + `test` (the
`protect-main` ruleset, read between this session's `date -u` reads of
17:22:25Z and 17:22:56Z).
- **`test` runs in full on every leg.** Each leg ticks its story in
  `docs/project-plan.md` and edits `docs/close/2.1.json`, which the docs-only
  classifier never counts as documentation, so no leg is predicted to skip
  it.
- **`scope` also gates the tick and secrets.** It runs
  `check-tick-merge-claim.mjs` (a tick never claims its own PR's merge) and
  the secret scan.

**Merge-loop line:** each leg's PR:
1. ticks its story in place: the heading gains `— ✅ DONE (date)`, with
   `**Completed:**` (the date, `tigerfid #N` and the branch, never the merge)
   and `**Outcome:**` directly under it;
2. appends its `reviewedFor` line to `docs/close/2.1.json` in the same PR
   (lockstep);
3. runs the item-8 anchor grep, `grep -n "Story 2.1.<K>"
   docs/project-plan.md docs/close/2.1.json`, and the document-name grep,
   `grep -rn "<document>.md" docs/`, naming each hit's disposition in the PR
   body (refreshed, current, or frozen in a DONE story);
4. runs the C8 grep, `grep -n -i -E 'medical|vital' docs/study/<document>.md`
   (each hit a negation), and reads the diff for a hostname, an IP address, a
   machine name, a private path, or any person beyond those `readme.md`
   names;
5. carries a CLAIMS AUDIT in its body: every number and every rule or part
   claim beside its source, its measurement or its INFERRED arithmetic (D10);
6. runs `node scripts/close-lint.mjs` and `npm test` unpiped, gated by `&&`
   all the way to `git commit`;
7. rebases on the fetched `main` and merges per D11.

No CHANGELOG.

**Kickoff (step 6, baked):** a FRESH executor session per leg, on either box:
1. **Fetch:** `cd <hub> && git pull --ff-only && cd projects/tigerfid && git
   pull --ff-only`.
2. **Open:** `node <hub>/scripts/fleet-view.mjs --start "tigerfid Sprint 2
   leg <N> — Story 2.1.<K>" --role executor --repos tigerfid` (the marker
   goes to the hub's origin by itself).
3. **Clocks:** `date -u` before every clock written.
4. **Read, before any search:**
   - this block and the leg's story, with its checks in
     `docs/close/2.1.json`;
   - `docs/reference-repos.md`, for the reading rules, and
     `docs/reference-research.md`, for § 4's search ideas;
   - the study documents the story cites;
   - `CLAUDE.md` § Writing in public;
   - the previous leg's PR body and this sprint's friction log.
5. **Re-verify first:** the set below.
6. **Branch:** `story-2.1.<K>-<slug>`, from the fetched, fast-forwarded
   `main`.

Paths and habits:
- `<hub>` is the hub root's absolute path on the executor's box, `<F>` is
  `<hub>/projects/tigerfid`, and `<SCRATCH>` is the session scratchpad
  (`mkdir -p` first).
- Checkers run unpiped; long files are written with the Write tool; `git -C`
  takes the literal path. `npm test` here is light and takes no battery lock.
- On the Mac, the lid stays open while any fleet job runs (KB
  `mac-standby-sleep-abandons-jobs`); before a long wait, `caffeinate -i -t
  <seconds>` covers the sitting, and the lid rule still holds.
- CI runs on GitHub-hosted runners only. No leg edits `.github/` or
  `scripts/`. A stranger's PR is never checked out.

**Friction log:** `docs/friction-log-sprint2-the-study-makes-its-choices.md`.
Each leg appends its section, written for a stranger:
- its context checkpoint: the plan's task count, compactions, forced
  re-reads and lost-context defects;
- each piece of friction it met.

**Re-verify at execution** (each leg, before its first edit):
- `git -C <F> log --oneline -3 origin/main` shows the previous leg's merge;
- `git -C <F> config user.email` is Omid's GitHub noreply address;
- `node scripts/close-lint.mjs` exits 0 and `npm test` is green on `main`;
- `node scripts/reference-check.mjs` exits 0 on this box. A `MISSING file`
  from a row an earlier leg pinned is fetched from its URL or copied (D8);
- the flip map's teeth read FAIL through the close runner before the work;
- the ASK-OMID register: any row an earlier leg filed, with its answer if any.

**Verified-live at proposal** (2026-10-27, the session above):
- **The close runner.** The baseline, at `831db91`, is the verdicts above.
- **`legality-rules-cited`, executed against real files.** It ran through
  the close runner (shell: Git Bash) in a scratch worktree of `831db91`, with
  three fixture documents written for it:
  - all four sections cited: PASS (exit 0);
  - §15.23 absent, the other three present: FAIL (exit 1);
  - §15.23 as the file's last token: PASS (exit 0).

  This PR adds no run line and changes none.
- **The parser.** `node <hub>/scripts/fleet-view/check-sprint-block.mjs
  <hub> <plan> 2` read this block as proposed, exit 0:
  `{"found":true,"approvedDate":null,"executed":false,"reviewed":false,"declaredLegs":4,"legs":["2.1.9","2.1.3","2.1.4","2.1.5"],"legsSource":"structured","ambiguous":false,"sweptDate":"2026-10-27","problems":[]}`.
  - It read the plan as git stores it, with LF line endings.
  - This proposal's checkout converts to CRLF (`core.autocrlf=true`). Over
    that working-tree file the parser printed `{"found":false,"problems":[]}`
    and exit 1, because its heading pattern does not match a line that ends in
    a carriage return. That is friction F4 in the log.
- **The parser, as approved:** the same command over a scratch copy with the
  approval pasted into the heading and the label line. It read the same four
  legs, `problems: []`, exit 0; the PR body quotes its line.
- **`close-lint`:** `close-lint: ok (adopted 2026-10-26)`, exit 0.

**After the sprint:** the last leg's executor stamps the block as executed:
the heading's stamp, the Executed record, and a line saying the review is
pending. It then wraps. Then, in order:
1. the Sprint 2 reviewer pass (FRESH);
2. the Sprint 3 proposal: Story 2.1.6 and any remainder D9 split off, from
   the plan as it stands after the review;
3. the decision sitting (Story 2.1.7);
4. Epic 2.1's close (Story 2.1.8, a FRESH closer).

### Sprint 1 — "Adopted: the original kept, the study begun" — ✅ APPROVED (2026-10-26, at adoption — the spec is the approval record) — ✅ EXECUTED (2026-10-27)

**Born:** 2026-10-26, at adoption (the onboarding PR, tigerfid #4 — its merge sha is recorded by hub Story 243.1.2's tick;
hub spec `2026-10-26-tigerfid-onboarding-design`, hub PR #4355).
**Approval:** ✅ APPROVED — Omid, 2026-10-26 (his approval of the spec,
which records this sprint in §7; KB `new-repo-standard` item 5).
**Executed:** 2026-10-27. The five legs, each its own PR, squash-merged in
order:
1. Story 1.1.3 — PR #6 → `2221bd1b` (2026-10-27T00:46:05Z);
2. Story 1.1.2 — PR #7 → `e8597e58` (01:55:07Z);
3. Story 1.1.4 — PR #8 → `435d1ce5` (02:01:38Z);
4. Story 2.1.1 — PR #14 → `566947f0` (12:08:37Z);
5. Story 2.1.2 — PR #16 → `d476e556` (12:53:26Z).

Between legs 3 and 4, `main` also took #9 (a reference-check fix) and #11
(Story 1.1.5, the second machine, not a leg). Leg 5 was run by the
repo-rooted Windows session "tigerfid Sprint 1 Executor", fresh by `/clear`
(session `c40e0637-2dc0-4ae7-bea5-428bee160ea7`; the cleared process,
`c59c545c-e735-4344-b0b8-697d9f897dca`, was leg 4's).
**Reviewed:** 2026-10-27, by the Sprint 1 reviewer pass (tigerfid #23).
The pass was the repo-rooted Windows session "tigerfid Sprint 1 Reviewer",
fresh by `/clear` (session `f08270bf-1a7d-43fc-8c97-df28f66d8ebc`). The
cleared process, `c59c545c-e735-4344-b0b8-697d9f897dca`, was leg 4's, and
leg 5's session ran in it too. The pass ran without elevation and held no
other role in this sprint. **Verdict: all five legs meet their gates.** It
made five findings, each fixed in #23 or routed below. This repo keeps no
sprint friction log, so the record read is the five PR bodies, their ticks,
the wrap notes and the Epic 1.1 close evidence.

- **Legs 1–3 (#6, #7, #8; Stories 1.1.3, 1.1.2, 1.1.4): PASS.** The Epic 1.1
  close adjudicated their teeth before this pass (19 machine checks PASS,
  below). Read against its gate line above, each leg meets it:
  - leg 1: the three documents, the first machine's verify, and the
    capture's list bound by digest;
  - leg 2: its five teeth PASS on its branch, with Omid's read recorded in
    #7 before its merge;
  - leg 3: its four teeth PASS, with the gate in `test`.
- **Leg 4 (#14; Story 2.1.1): PASS.** [1]–[27] are nine numbers per
  scenario: its size, read range, tags per second, zone accuracy, four alarm
  latencies and battery life. Each carries its reasoning and Omid's answer,
  and his reply is quoted whole. This pass re-ran the document's arithmetic,
  and every figure reproduces: the reply cost, the three battery budgets, the
  ALOHA limits, the channel capacities and the walking distances.
- **Leg 5 (#16; Story 2.1.2): PASS.** This pass re-measured three of its
  claims independently, in this machine's reference folder:
  - the CRC table: CRC-16/X.25 over the three test vectors gives the printed
    bytes and the `0xF0B8` residue, and none matches without `0x7E`, as the
    document's coverage note says;
  - 28 quoted phrases from 16 records: 27 are found byte for byte, and the
    28th is found once the source's en dash is read as the hyphen the
    document prints;
  - the firmware search: 414 project `.c`/`.h` files, none matching the
    radio, random-delay or temperature terms, and 6 matching "ActiWAVE".
- **Merges:** every leg merged directly; no leg's timeline shows an
  auto-merge. Legs 1–3 were Omid's to merge, since each changed CI,
  `scripts/` or the README (leg 1 too: it carried `scripts/` and CI, so it
  was not docs-only). Omid confirmed "I merged or said the word" for #6, #7
  and #8, first-hand in this pass's terminal, between its `date -u` reads of
  13:21:53Z and 15:22:47Z.
- **Findings:**
  1. **Forward work lived only in the ticks and the documents.** The 2.1.1
     and 2.1.2 ticks, the two study documents and #7 assign work to Stories
     2.1.3, 2.1.4, 2.1.5 and 2.1.7, and none of those stories carried it
     (register class `premark-promise-not-written-into-owning-story`).
     Fixed in #23, on Air Traffic Control's grant, with one dated Context
     sentence on each of the four stories; no Done-when moved. Among them:
     the original host software's source, which 2.1.2 left unread, is now
     2.1.5's, and the rebuild's top-level layout, which #7 left to "the
     study" and no story named, is now 2.1.7's.
  2. **The `rebuild-spec-approved` tooth could never pass.** Its born run
     line exited 1 with a `SyntaxError` on every input. It is fixed in
     #23 on the same grant, and the measurement is in
     `docs/close/2.1.json`'s `reviewedFor` entry. 1.1's born
     `build-inputs-name-notices` pattern, which #8 corrected, has the same
     lost-backslash shape (INFERRED, one mechanism).
  3. **`docs/study/original-system.md` lists two corrections to the
     onboarding spec, not three.** The 2.1.2 tick, #16's body, the notes and
     hub inbox #4436 name three. The document states the third in § The
     Reader: the Reader's radio parts are on the Transceiver, against the
     spec's Context lines 60–61. It is missing from the document's
     corrections list and its disagreement table. Routed to the Sprint 2
     proposal as a fix-up for its first study PR. #4436 already lists all
     three, so it is cited here, not re-filed.
  4. **The same document's Outlook-message sentence is stale.** It says the
     message "stays closed until Omid rules on it (the plan's ASK-OMID A1)".
     A1 was answered at #7 ("Keep the default") and now sits in this archive.
     What keeps the message closed is `docs/reference-repos.md` reading
     rule 7. Routed with finding 3.
  5. **This line, while PENDING, named only one other copy of the pending
     review.** Story 1.1.6's Outcome restated it too. #23 refreshes both
     copies (KB `epic-close-before-sprint-review` rule 4).
- **Owner deferrals in the leg bodies,** each traced to its destination:
  - #6's form for the capture's list: the close, PASS by digest;
  - #7's conduct alias: Omid at the close, "Yes, it works";
  - #7's `ENOBUFS` fix: hub Story 244.1.20;
  - #7's top-level layout: finding 1;
  - #8's hub companion: landed by round #13;
  - #14's and #16's prose checks: Epic 2.1's closer, in
    `docs/close/2.1.json`.
- **The Epic 1.1 close's triage stands, extended:** finding 2 is a second
  face of its item 3.
- **Register seats.** Sent to Air Traffic Control as text, by class id:
  - finding 2's face;
  - this pass's own guard events, each caught before it changed anything:
    - a refusal, a seat of
      `stdin-guard-reads-a-bare-dash-argument-as-the-stdin-program`;
    - a warning, a seat of `piped-checker-exit`;
    - a refusal of close-lint chained to a commit by `;`, a seat of
      `semicolon-chained-checker-does-not-gate-commit`.

  Not re-sent: the two seats the close's triage names (items 3 and 4). The
  hub register did not hold them at this pass's first read, between its
  `date -u` reads of 13:13:47Z and 13:21:53Z. They landed by hub #4446 at
  13:41:54Z (`manifest-check-keyed-on-a-plan-spelling` and
  `reference-folder-moved-not-copied`), before #23 opened. Corrected by #24.
**Archived:** 2026-10-27, by the Epic 1.1 close (Story 1.1.6), on the
runner's RELOCATE banner. The legs span Epics 1.1 and 2.1, and every leg read
ticked once #16 landed (hub Story 89.1.1). Legs 1–3 (Stories 1.1.3, 1.1.2,
1.1.4) archive with Epic 1.1, below. Legs 4–5 (Stories 2.1.1, 2.1.2) stay
ticked in the plan until Epic 2.1's close archives them.

Story 1.1.1 is the onboarding PR itself — it carries this plan and is
ticked in it, so it is not a leg. Five legs, in this order, each its own PR:

1. **Leg 1 — Story 1.1.3** — the reference corpus adopted, FIRST, so the
   early capture of the activewaveinc.com pages, photos and datasheets
   comes before the sibling site's seat changes the live site (spec C11).
   Gate: the three reference docs committed; the folder recreated and
   verified on the first machine; the capture's file list with sha256s in
   the tick.
2. **Leg 2 — Story 1.1.2** — the archive restructure: every 2018 path but
   four leaves the current tree, the dedication stays verbatim. Gate: the
   1.1 teeth `archive-left-the-tree`, `dedication-verbatim`,
   `readme-points-at-the-original` and `contributing-coc-replaced` PASS on
   the branch; Omid has read the README wording in the PR (D11).
3. **Leg 3 — Story 1.1.4** — the licence lane, before any third-party copy.
   Gate: the 1.1 teeth `notices-file`, `licence-gate-passes`,
   `licence-gate-in-test` and `allowlist-widened` PASS; `test` runs the gate.
4. **Leg 4 — Story 2.1.1** — the requirements with numbers, each number put
   to Omid. Gate: `docs/study/requirements.md` committed with his recorded
   answers.
5. **Leg 5 — Story 2.1.2** — the original system described from its own
   records. Gate: `docs/study/original-system.md` committed, every claim
   cited to its file at the tag or marked INFERRED.

**Merge loop (every leg):** rebase on `main`, run `node scripts/close-lint.mjs`
unpiped, run the item-8 anchor grep (`grep -n "Story <id>" docs/project-plan.md
docs/close/*.json` — the tick and its manifest line both present), then
`gh pr merge --auto --squash --match-head-commit <full sha>` for a docs-only
leg (legs 1, 4 and 5 when they carry documents only); a leg that changes CI,
`scripts/` or the README (legs 2 and 3) is Omid-merged. A stranger's PR
branch is never checked out (CLAUDE.md).

*(closed initiatives follow, newest first)*

## Initiative 1: Join the fleet; keep the original — ✅ DONE (2026-10-27)

**Completed:** 2026-10-27 — an initiative-final close: Epic 1.1, its only
epic, closed by Story 1.1.6's PR, which cut this section whole. Its Done-when,
"Epic 1.1 is archived with close evidence", is adjudicated PASS in the close
evidence below (`initiative-1-final`). The design study, Initiative 2, is the
plan's live work.

**ASK-OMID entries relocated at this close** (KB `register-relocation-at-close`,
2026-10-27). Each moved dated and intact; its heading is now a bullet:

- **A1 — The Outlook message in the 2018 tree: read it before it leaves the
  tree, or keep the default? — ✅ ANSWERED (2026-10-27): keep the default.**
  Omid answered "Keep the default (Recommended)" first-hand in the Story 1.1.2
  session's terminal (tigerfid #7). The file left the current tree unread with
  the rest of the 2018 files and stays, unchanged, at the tag. `software/smart-tracker/SmartTracker  SaniFaucet.msg`
  (12 MB, an Outlook `.msg` file) was not read at adoption; it may hold
  correspondence. **Default:** it leaves the current tree with the rest of the
  2018 files in Story 1.1.2 and stays, unchanged, at the tag `activewave-2018`
  — the history is never rewritten (spec D6), so the restructure changes the
  current tree only. **Asked:** read it first and decide, or keep the default.
  **Gates:** nothing — Story 1.1.2 proceeds on the default unless Omid rules
  otherwise first. Filed 2026-10-26 by the onboarding PR (spec §2, §7).

**Objective:** tigerfid works inside the fleet's checks — its plan, close
protocol, CI and session handoff — with the 2018 state kept byte for byte at
the tag `activewave-2018` and its release, the current tree cleared for the
rebuild with the dedication verbatim, the reference corpus adopted on both
machines, and the licence lane in place before any third-party copy.
**Done when:** Epic 1.1 is archived with close evidence (initiative-final —
Epic 1.1 is its only epic).

### Epic 1.1: Onboarding, the restructure, the reference corpus and the licence lane — ✅ DONE (2026-10-27)

**Completed:** 2026-10-27 — closed per KB `epic-close-protocol` by Story
1.1.6's PR, a fresh session that did none of the epic's work (the close PR
is the archival PR); initiative-final, so Initiative 1 is cut whole. The
Sprint 1 block moved with it, to § Executed sprints above.

**Goal:** the repo is adopted and gated (1.1.1), its reference corpus is
adopted with the old website captured early (1.1.3), its current tree is
cleared for the rebuild (1.1.2), its licence lane guards every copy (1.1.4),
and the second machine holds a verified clone and corpus (1.1.5).
**Ordering:** 1.1.1 (the onboarding PR, done at adoption) → 1.1.3 → 1.1.2 →
1.1.4 (Sprint 1 legs 1–3) → 1.1.5 (before any corpus-dependent review runs
on the second machine) → 1.1.6. Manifest: `docs/close/1.1.json` (born
FAILING — its census, README, corpus and licence teeth read files the later
stories write).
**Done when:** every tooth in `docs/close/1.1.json` passes from the repo root
and its prose checks are adjudicated.

#### Story 1.1.1: The onboarding PR — the repo joins the fleet conformant — ✅ DONE (2026-10-26)

**Completed:** 2026-10-26 — the onboarding PR, tigerfid #4 (branch `onboarding/fleet-adoption`), the adoption record in its body.
**Outcome:** the born-with files in adoption form (CLAUDE.md, notes, this plan with Sprint 1, the archive, `docs/close/protocol.json` with the born-failing 1.1 and 2.1 manifests, CI with its contract, `package.json`, `.npmrc`, `.gitignore`, Dependabot, the pre-commit hook, the session settings); `scope` + `test` required on `protect-main` once CI first reported; the synced canonicals by the fleet's canonical round.

**Story:** As the fleet, I want tigerfid adopted inside the checker family,
so that no heartbeat reads a half-adopted repo and the first merged PR
proves the gate end to end.
**Context:** spec §1 rows 2–13, in adoption form (hub Story 243.1.2). The
adoption acts come first, each readable back from GitHub: the settings
family (`vulnerability-alerts` first), the annotated tag `activewave-2018`
on `f25ff13f3fb672d8ec91f1fadc8894d69f781ed6` with its release, the default
branch renamed `master` → `main` through GitHub's rename (old links
redirect; forks untouched), and the `protect-main` ruleset without required
checks. This PR then carries the born-with files; the synced canonicals
(`scripts/wrap-session.mjs`, the guard scripts, `scripts/close-lint.mjs`
and the CI helpers) arrive by the fleet's canonical round, never
hand-copied. CI runs on GitHub-hosted `ubuntu-latest` only (spec C2) with
blobless checkouts (C3). `REPO_BUILD_INPUTS`: no markdown build inputs,
assessed 2026-10-26 — no build exists; the study re-assesses it if a docs
site or a generated guide ever reads markdown.
**Done when:** the PR body records the adoption (the org `tourajghaffari`,
PUBLIC, "adoption, not birth", the tag and its sha, the rename, the settings
read-back, the ruleset id and the commit identity); `protect-main` requires
`test` + `scope`; this is the first PR through the ruleset with both green;
the 1.1 teeth `runner-hosted-only` and `tag-preserves-2018` PASS.

#### Story 1.1.2: The archive restructure — the 2018 tree leaves the current tree, the dedication stays — ✅ DONE (2026-10-27)

**Completed:** 2026-10-27 — tigerfid #7 (branch `story/1.1.2-restructure`).
**Outcome:** the 2018 tree left the current tree. `git rm` removed 9,472 paths (`api/`, `bom-files/`, `documentation/`, `patents/`, `presentations/`, `protel-files/`, `sealtag/`, `software/`) and kept 5 (`license`, `contributing.md`, `readme.md`, `_assets/readme.md`, `_assets/tigerfid.jpg`); the tag and the history keep every byte.
- **The README:** the Introduction, Fun Touraj Fact and Thank You sections are verbatim. "What is this?" stays, its presentations link re-pointed at the tag. `## Where this is going` is new: the rebuild, the original at the tag folder by folder, the one-line partial clone, and issues now, pull requests after the study. The 2018 sections that described files no longer on `main` ("current state", "get involved", "Contents") are read at the tag, through the 2018 README link.
- **`contributing.md`:** it names the Contributor Covenant 3.0, linked, not copied. Copying its CC BY-SA 4.0 text would be a third-party copy, which waits on Story 1.1.4. Reports go to conduct@activewaveinc.com. The stance is issues now and pull requests once the study sets the direction, and the 18F credit line stays.
- **The sparse view retired:** both `sparse-checkout` blocks are gone from `ci.yml`, and CLAUDE.md § CI is rewritten. This machine's clone runs `git sparse-checkout disable` once its `main` holds this change; the second machine's clone is Story 1.1.5's.
- **Omid's read (D11),** first-hand in the Story 1.1.2 session's terminal, between that terminal's `date -u` reads of 00:46:07Z and 01:42:38Z, 2026-10-27:
  - README: "As drafted (Recommended)", the wording in this PR;
  - code of conduct: "Contributor Covenant 3.0 (Recommended)";
  - reporting: "A role email address (Recommended)", then "conduct@activewaveinc.com (Recommended)";
  - ASK-OMID A1: "Keep the default (Recommended)".
- **A new tooth, `front-door-links-resolve`:** every relative link in `readme.md` and `contributing.md` resolves on `main`. Removing folders is how a front door gets dead links.

**Story:** As a beginner arriving at the repo, I want a front door that
shows the rebuild and links the original, so that I can start without
wading through 2.97 GB of 2001–2013 files.
**Context:** spec §2, D6, D10, D11. One PR, after the onboarding PR: every
2018 path leaves the current tree by `git rm` except `license`,
`contributing.md`, `readme.md` and `_assets/`; the history and the tag keep
every byte. In `readme.md`, the Introduction, Fun Touraj Fact and Thank You
sections stay VERBATIM. A new `## Where this is going` section names the
rebuild, links the original at the tag folder by folder, carries the
one-line partial clone (`git clone --filter=blob:none …`, so a beginner's
clone fetches only the current tree's blobs), and says issues are welcome
now and pull requests once the study sets the direction. `contributing.md`
is refreshed in the same PR: its 18F Code of Conduct link is replaced by a
current code of conduct — the builder proposes one, Omid chooses in the PR.
No new README wording merges without Omid's read (D11: his read is the
family's). The `.msg` file follows ASK-OMID A1. The rebuild's top-level
layout is settled by the study (a starting shape: `hardware/`, `firmware/`,
`software/`, `guides/`, `docs/`). After this lands, a CI checkout fetches
only the small current tree.

The same PR retires the interim sparse view:
- both `sparse-checkout` blocks in `.github/workflows/ci.yml`;
- CLAUDE.md § CI's sparse sentence;
- `git sparse-checkout disable` in each machine's clone.

Why it must go: its excluded names (`software/`, `api/`) would otherwise hide
the rebuild's own folders from CI and from `block-secrets --repo-scan`, which
skips files that are not on disk.

**Done when:**
- the 1.1 teeth `archive-left-the-tree`, `dedication-verbatim`,
  `readme-points-at-the-original`, `contributing-coc-replaced` and
  `sparse-view-retired` PASS;
- the PR records Omid's read of the new wording and his code-of-conduct choice.

#### Story 1.1.3: The reference corpus adopted, with the early capture of the old website — ✅ DONE (2026-10-27)

**Completed:** 2026-10-27 — tigerfid #6 (branch `story/1.1.3-reference-corpus`).
**Outcome:** `docs/reference-repos.md`, `docs/reference-corpus.tsv` (291 pins: the tree row and 290 file rows), `docs/reference-research.md` (its Chorus entry as prose and links), the folder stub `docs/reference-readme-stub.md`, and `scripts/reference-check.mjs` behind `npm run setup`, its tests in CI's `test`. No third-party file is committed.
- **The folder recreated and verified on the first machine.** `--recreate` ran from 00:36:57Z to 00:38:44Z: 6,062 missing blobs fetched in one batch, then `git archive` of `f25ff13f3fb6` wrote 9,477 files. The verify ran at 2026-10-27T00:39:34Z, exit 0:

  ```
  ok      tree   activewave-2018  f25ff13f3fb6 — 9477 files at their blob ids, none extra
  ok      files  activewaveinc/  268 of 268 at their sha256
  ok      files  chorus/  22 of 22 at their sha256
  ok      sidecars none
  ok      readme stub
  reference-check: ok — 291 rows verified, README the stub
  ```
- **The capture's file list with sha256s** is the 268 `activewaveinc/` rows of `docs/reference-corpus.tsv`, kept there once, because the pins live in that file only.
  - The rows: `rendered/` 99, `docroot/images/` 138, `docroot/pdf/` 16, `docroot/css/` 2, and the 13 datasheet page sources, 4,909,103 bytes in all.
  - The digest binding the list: `dfacd09c277c07be25d2dd7ba7e7285bae64b1d23207ca0b5b6f0344e09f5525`. That is the sha256 of the rows as `<sha256>  <name>` lines, in the tsv's order, LF-joined with a trailing LF.
- **Dated before the site's first change.** The capture was taken on 2026-10-26:
  - `rendered/` fetched 19:43:31Z–19:43:36Z;
  - `docroot/` pulled 19:48:01Z–19:49:11Z;
  - both copied into the folder at 19:50:53Z. Every file's modified time is that minute, and none is newer.

  The 49 response bodies match the sha256s the capture's `INDEX.tsv` recorded at fetch time. When #6 opened, the activewaveinc.com repo was not yet cloned into the fleet: no fleet change had reached the site. The Wayback copies were not needed.

  **Correction (2026-10-27, the Epic 1.1 close):** the live site had changed before #6 opened at 00:43:13Z. It was frozen to static HTML at 20:04:41Z (host clock) on 2026-10-26, during that day's incident (hub spec `2026-10-26-activewaveinc-onboarding-design`). The sentence above reads the site's repo, not the host. The capture's last act, at 19:50:53Z, came 13 min 48 s before the freeze, so the capture's dating holds.
- **rfidguardian.com was dropped,** with its reason and reversible: the research log, § 2.

**Story:** As a session doing the design study, I want the original tree,
the old website's pages and datasheets and the study's third-party sources
pinned in a recreatable reference folder, so that every comparison reads the
same bytes on either machine.
**Context:** KB `new-repo-standard` row 14 and KB
`reference-corpus-and-research-log`; spec C4, C11, D12. The folder
`projects/tigerfid-reference/` sits beside the clone — a plain folder, never
a repo, never committed — and is recreated from the tag by `git archive`
(the family's own Apache-2.0 work, so reading or porting from it needs no
notice). The study's third-party sources (vendor datasheets, reference
designs) are pinned with the dial set to **copy-with-trail**; Story 1.1.4's
lane lands before any copy.

Spec C11's capture is already TAKEN. On 2026-10-26 at 19:50:53Z the old
website's rendered pages, photos, datasheet PDFs and datasheet sources were
copied into the folder's `activewaveinc/` from the activewaveinc.com seat's
pre-change pull. The folder's README records what was left out: server code, a
database login, visitor logs and a vendor's menu runtime. This story adopts the
capture; the Wayback copies are the fallback.

The Chorus notes and source URLs gathered for spec D12 go into
`docs/reference-research.md` as prose and links only. Images, captures and
datasheets stay in the uncommitted folder. **This story commits no third-party
file:** the licence lane (Story 1.1.4) comes after it, and this repo is public
with a history that is never rewritten. The recreate-and-verify tool
follows tigerian's and tigerdash's shape (`scripts/reference-check.mjs`
behind `npm run setup`) — the builder's call.
**Done when:** `docs/reference-repos.md` (reading rules, licence audit,
pins with full commit ids or sha256s, the recreate recipe),
`docs/reference-corpus.tsv` (the pins, once) and
`docs/reference-research.md` (with its Chorus entry) are committed; the
folder is recreated and verified on the first machine, the verify run
quoted in the tick; the capture's file list with sha256s is in the tick,
dated before the site's first change.

#### Story 1.1.4: The licence lane — before any copy — ✅ DONE (2026-10-27)

**Completed:** 2026-10-27 — tigerfid #8 (branch `story/1.1.4-licence-lane`); the hub companion rides hub inbox #4410.
**Outcome:** the lane landed before any copy, with zero entries.
- **`THIRD_PARTY_NOTICES.md`** holds:
  - the two halves of the trail: a marker at the copy site, or in a `<file>.license` text file beside a file that takes no comments, and an entry under `## Entries`;
  - the five-line entry shape: `Source`, `Pin` (a commit URL with a full 40-hex revision, or `sha256:` of the file as fetched, for hardware sources with no commit), `License`, `Taken`, `Landed`;
  - the allowlist: tigerian's eight, plus `CERN-OHL-P-2.0` and `CC-BY-4.0`;
  - the KiCad standard libraries' `CC-BY-SA-4.0` and their design exception, recorded once, quoted from kicad.org, read 2026-10-27.
- **`scripts/check-licenses.mjs`** checks:
  - both directions, and the marker-to-entry licence match;
  - the allowlist, the entry shape and the Pin;
  - that no entry's Source is a corpus row marked `study-only`, or the original, which needs no entry;
  - with entries read only under `## Entries`, so a standing note is never one.

  It runs in CI's `test` job, as do its 8 tests; `npm test` runs it too, and `docs/ci-contract.md` names both commands. On the branch: `check-licenses: ok — 0 entries, 0 marker(s), 10 files scanned`.
- **The manifest:** `build-inputs-name-notices`'s born regex, `[[^]]*`, could never match the entry's real shape, so it is corrected (both measured). `licence-gate-tested` is added.
- **The hub companion** (`tigerfid: ['*_NOTICES.md']` in `REPO_BUILD_INPUTS`) is hub-owned: it is filed as hub inbox #4410 and reaches this repo by the sync round.
  - Until then, `build-inputs-name-notices` reads RED, and a notices-only PR here still skips `test`.
  - `docs/ci-contract.md`'s "no markdown build inputs" line is corrected in this PR.

**Story:** As the repo's owner, I want every third-party file copied in to
carry its trail and every licence checked in CI, so that the rebuild stays
Apache-2.0-clean and nothing is copied silently.
**Context:** spec C5, in tigerian's two-halves form: a marker at the copy
site and an entry in `THIRD_PARTY_NOTICES.md`, enforced both ways by a gate,
`scripts/check-licenses.mjs`, that runs in CI's `test` job. The allowlist is
tigerian's, widened for hardware sources with `CERN-OHL-P-2.0` and
`CC-BY-4.0`. The KiCad standard libraries' licence (CC-BY-SA-4.0 with its
design exception) is recorded once in the notices file. A vendor reference
design whose terms are not on the list goes to the ASK-OMID register one by
one and is never copied silently. New work stays Apache-2.0.
`docs/ci-contract.md` gains the gate's command in the same PR.

Once the gate reads `THIRD_PARTY_NOTICES.md`, that file is a `test` input, so a
notices-only PR must not skip `test`. The story's hub companion adds
`tigerfid: ['*_NOTICES.md']` to `REPO_BUILD_INPUTS` in the hub's canonical
`scripts/ci-docs-only.mjs`, and it reaches this repo by the fleet's sync round,
never by a hand edit here. The same PR corrects `docs/ci-contract.md`'s "no
markdown build inputs" line. This is the class tigerian's entry records. At
adoption the table carries no tigerfid entry: no markdown build inputs, assessed
2026-10-26, recorded in #4's body.

**Done when:**
- the 1.1 teeth `notices-file`, `licence-gate-passes`, `licence-gate-in-test`
  and `allowlist-widened` PASS;
- the hub's `REPO_BUILD_INPUTS` names `tigerfid` with the notices glob.

#### Story 1.1.5: The second machine — its clone and its reference folder — ✅ DONE (2026-10-27)

**Completed:** 2026-10-27 — tigerfid #11 (branch `story/1.1.5-second-machine`).
**Outcome:** the second machine (macOS) holds a verified clone and a verified reference folder. Every line below was read by this story's session from the clone's root, unpiped, unless it names the set-up sitting.
- **The verify run,** between the clock reads 10:32:33Z and 10:32:35Z on 2026-10-27, exit 0, every pin matched:

  ```
  ok      tree   activewave-2018  f25ff13f3fb6 — 9477 files at their blob ids, none extra
  ok      files  activewaveinc/  268 of 268 at their sha256
  ok      files  chorus/  22 of 22 at their sha256
  ok      sidecars none
  ok      readme stub
  reference-check: ok — 291 rows verified, README the stub
  ```
- **The identity (spec D8),** read back at 10:32:51Z: `git config --local user.email` printed `oghaffari@users.noreply.github.com`. It was set before the clone's first commit, which is this PR's.
- **The pre-commit hook is armed:** `core.hooksPath` is `.githooks`, and `.githooks/pre-commit` is `100755` in the index and executable on disk (#5's fix, read on this machine).
- **The clone is partial and not sparse.**
  - The set-up sitting made it with `gh repo clone … --filter=blob:none --no-checkout`; the reflog's first entry is 2026-10-27T00:26:28Z. Spec §6's check passed before the checkout: `fetch` exited 0, and `git show origin/main:` read the plan and CLAUDE.md.
  - Read here: `remote.origin.promisor` is `true` and `remote.origin.partialclonefilter` is `blob:none`. The history is full, not shallow. `core.sparseCheckout` is `false`, so the sparse view is retired on this machine too (1.1.2's handoff).
  - `git show` at the tag and at `origin/main` both exit 0, and the tag peels to `f25ff13f3fb672d8ec91f1fadc8894d69f781ed6`. The pack is 758.07 MiB: it holds the 2018 blobs that `--recreate` fetched.
- **The folder, part by part** (the set-up sitting's record):
  - `activewave-2018/` came from `--recreate`, 00:46:53Z–00:47:59Z, with 6,062 blobs fetched.
  - `activewaveinc/` came from the first machine as one tar (sha256 `6425dbcf…`, 268 entries), carried by hand. Two earlier folder copies landed partial (140, then 175 of 268); the tar landed whole. The verify's 268 of 268 is its check.
  - `chorus/` was already on this machine at its pins.

  The set-up sitting's own verify read 291 of 291 at 09:51:19Z. The run above is the one this tick quotes.
- **No corpus-dependent review ran here before it.** No study story has begun: Stories 2.1.1 and 2.1.2 (Sprint 1 legs 4–5) are unticked, and `docs/study/` is absent. Origin holds only `main`, and every PR before this one (#1–#10) is merged; none is a study story.

**Story:** As a session on the fleet's second machine, I want tigerfid
cloned and its reference folder recreated and verified there, so that a
corpus-dependent review never runs against a missing or different corpus.
**Context:** KB `new-repo-standard` row 14 (the folder is recreated and
verified on the other machine BEFORE any corpus-dependent review runs
there); spec §6 (a partial clone, `--filter=blob:none`, after a check that
the fleet's fetch and `show` reads work on it) and D8 (the per-clone noreply
identity set before the clone's first commit). Waits on Story 1.1.3's
recreate tool.
**Done when:** the tick quotes the second machine's verify run (every pin
matched) and its `git config user.email` read-back ending
`@users.noreply.github.com`.

#### Story 1.1.6: Close out Epic 1.1 — ✅ DONE (2026-10-27)

**Completed:** 2026-10-27 — tigerfid #19 (branch `close/epic-1.1`, pre-tick head `050b5d6`; the close PR is the archival PR), by session `ce689038-d1e9-483f-9350-492b8e1ff75f`: a fresh launch (its background-task path id equals the session id; no `/clear` before it), not elevated, role closer, with none of the epic's work.
**Outcome:** the close evidence below — 19 machine checks PASS, 4 prose checks adjudicated, 2 of them by Omid — and Initiative 1 archived whole, with ASK-OMID A1 relocated beside it and the Sprint 1 block moved to § Executed sprints (its review then pending; reviewed by #23, 2026-10-27). A dated correction is added to Story 1.1.3's tick, and one CLAUDE.md line the evidence falsified is corrected in this PR.

**Story:** As the fresh-session closer, I want Epic 1.1 closed per KB
`epic-close-protocol`, so that the archive carries the evidence.
**Context:** `node ../../scripts/close/close-epic.mjs --epic 1.1` from the
repo root, in a session that did none of the epic's work. Initiative-final:
the archival PR cuts Initiative 1 whole.
**Done when:** every manifest check passes or is adjudicated, the evidence
block is written into the archive entry, and Initiative 1 is cut to the
archive in the same PR.

**Close evidence (2026-10-27, Opus 5.5, darwin):** HEAD 9478118 (clean); hub tooling: DIFFERS (1 file) from origin/main fc0f3d71. The differing file is outside the close tooling: `scripts/close/`, `scripts/close-lint.mjs` and `scripts/lib/` show no diff from origin/main. The runner ran from the repo root through the box's battery lock (exit 0), three times:
  - at 90ce1a1, 12:41:37Z–12:41:38Z;
  - after the rebase onto #16, 12:55:16Z–12:55:18Z, which first printed the RELOCATE banner;
  - the record below, 12:58:22Z–12:58:23Z, after the lock waited from 12:57:37Z on the Hub Sprint 68 Executor's battery.

  The machine verdicts were the same 19 PASS at all three.
- lint: PASS
- runner-hosted-only: PASS
- tag-preserves-2018: PASS
- archive-left-the-tree: PASS
- dedication-verbatim: PASS
- readme-points-at-the-original: PASS
- contributing-coc-replaced: PASS
- front-door-links-resolve: PASS
- sparse-view-retired: PASS
- readme-read-by-omid: PASS — Omid's verdict, "PASS (Recommended)", given first-hand in this session's terminal between its `date -u` reads of 12:49:43Z and 12:54:24Z, on the closer's evidence:
  - **His read predates the merge.** #7's body, § "Omid's read (D11)", records four answers given first-hand in the Story 1.1.2 session's terminal, between its `date -u` reads of 00:46:07Z and 01:42:38Z. #7 merged at 01:55:07Z (`e8597e58`).
  - **The answers:**
    - the README, "As drafted (Recommended)": the wording in #7, which carries `## Where this is going` and the stance (issues now, pull requests after the study, D10);
    - the code of conduct, "Contributor Covenant 3.0 (Recommended)";
    - the reporting address, "conduct@activewaveinc.com (Recommended)";
    - ASK-OMID A1, "Keep the default (Recommended)".
  - **A1's default held.** The `.msg` file is in the tag's tree (`software/smart-tracker/SmartTracker  SaniFaucet.msg`, blob `66346fa5`, 12,032,512 bytes) and absent from origin/main's 51 paths.
  - **Outside the rubric:** #7's body said the alias `conduct@activewaveinc.com` "must exist and reach Omid before this merges", and no record showed it. Asked at this close, Omid answered "Yes, it works", in the same sitting.
- reference-repos-doc: PASS
- reference-corpus-pins: PASS
- reference-research-log: PASS
- reference-check-tested: PASS
- corpus-recreated-and-captured: PASS
  - **The first machine's verify.** Story 1.1.3's tick (above) quotes it at 2026-10-27T00:39:34Z, exit 0: 291 rows verified, every pin matched. The 2018 tree was recreated by `git archive` of `f25ff13f3fb6` (9,477 files), after `--recreate` fetched 6,062 blobs.
  - **The capture's file list with sha256s** is the tsv's 268 `activewaveinc/` rows. The pins live there once (CLAUDE.md), so the tick binds the rows with a digest rather than repeating them. This close recomputed the digest from origin/main's tsv by the tick's own recipe: 268 rows, `dfacd09c277c07be25d2dd7ba7e7285bae64b1d23207ca0b5b6f0344e09f5525`, identical to the tick's. The list in the tick is therefore the list pinned.
  - **Dated before the site's first change.** The capture's acts ran 19:43:31Z–19:50:53Z on 2026-10-26. The site first changed at 20:04:41Z (host clock) the same day, frozen to static HTML during the incident (hub spec `2026-10-26-activewaveinc-onboarding-design`). No Wayback copy was used.
  - **Corrected:** the tick's "no fleet change had reached the site" reads the repo, not the host; a dated correction is added under it.
- notices-file: PASS
- licence-gate-passes: PASS
- licence-gate-in-test: PASS
- allowlist-widened: PASS
- licence-gate-tested: PASS
- build-inputs-name-notices: PASS — green since the fleet's round `20261027` (#13) brought the hub companion here.
- second-machine-clone: PASS — Omid's verdict, "PASS (Recommended)", in the same sitting, on the closer's evidence:
  - **The tick.** Story 1.1.5's tick (above) quotes the second machine's verify, 10:32:33Z–10:32:35Z on 2026-10-27, exit 0, 291 rows verified with every pin matched. It also quotes the read-back `oghaffari@users.noreply.github.com` at 10:32:51Z.
  - **Re-taken by this closer** on the same machine and clone:
    - the verify, 12:49:00Z–12:49:03Z, exit 0, the same six lines (`reference-check: ok — 291 rows verified, README the stub`);
    - `git config --local user.email`: `oghaffari@users.noreply.github.com`;
    - `.githooks/pre-commit`: `100755`.
  - **No corpus-dependent review ran first.** #11 merged at 10:38:17Z. The only study PR so far, #14 (Story 2.1.1), opened at 12:06:54Z. None of #1–#10 is a study story.
  - **The one clause the record cannot prove:** whether a review ran on that machine and left no PR. It is read from the PR record, not from the machine.
- initiative-1-final: PASS
  - **tigerfid works inside the fleet's checks.** `protect-main` (ruleset `25909952`, active) requires `test` and `scope`, and every PR from #3 to #15 merged through it. The latest fleet heartbeat (the hub's wrap of 2026-10-27, stamped at 12:09Z) reads tigerfid only as the close-due waiver for 1.1.6, which this close discharges.
  - **The 2018 state is kept at the tag and its release.** `tag-preserves-2018` PASS: the tag peels to `f25ff13f…`. The release "ActiveWave, as open-sourced (2017–2018)" on `activewave-2018` was published 2026-10-26T19:57:19Z and is neither a draft nor a prerelease (`gh release view`, read at this close).
  - **The current tree is cleared, the dedication verbatim:** `archive-left-the-tree` and `dedication-verbatim` PASS.
  - **The corpus is adopted on both machines:** Stories 1.1.3 and 1.1.5, and the second machine's re-verify above. The first machine's folder was damaged at 01:56Z and restored to 291 of 291 at 09:50:05Z (its wrap note of 2026-10-27). This close did not re-read the first machine.
  - **The licence lane came before any copy.** The gate landed with #8 (`435d1ce`, merged 02:01:38Z). The 42 files added between the tag and #8 (`git log --diff-filter=A activewave-2018..435d1ce`) are all the fleet's own work: 22 synced canonicals (#3), 14 born-with files (#4), and the corpus docs and tool (#6, "No third-party file is committed"). #5 and #7 added no file. At this close the gate reads `check-licenses: ok — 0 entries, 0 marker(s)`.
  - **Initiative Done-when** ("Epic 1.1 is archived with close evidence"): PASS. This PR archives it. At `90ce1a1` the plan held one epic of Initiative 1, Epic 1.1.
- **Sprint 1's review had not run at this close.** The runner printed REVIEW SEATING MISSING for Sprint 1. The sprint finished during this close: leg 5 (#16) merged at 12:53:26Z and its EXECUTED stamp (#17) at 12:55:50Z, and the review comes next. The runner's re-run after #16 also printed RELOCATE SPRINT BLOCK(S) ("its legs span several epics, every leg ticked; this close owns the relocation (Story 89.1.1)"). So this PR moves the block, stamped by #17, whole to § Executed sprints, with its review PENDING (on Air Traffic Control's grant, the block being the executor's region). The triage below is a floor the Sprint 1 reviewer extends, never narrows. **Refreshed by the Sprint 1 review (#23, 2026-10-27):** all five legs PASS, with five findings fixed in that PR or routed. The triage below stands, extended: its item 3 gains a second face, the 2.1 manifest's `rebuild-spec-approved` tooth. The record is § Executed sprints › Sprint 1's `**Reviewed:**` line.
- **Recurrence-prevention triage.** The hub register, censused by sitting at `d73d483d`, holds 12 seats naming tigerfid. Two are Story 1.1.5's own (`ugrep-bounded-repetition-exceeds-complexity-limit`, `zsh-pipestatus-lowercase`, both caught by their guards). None comes from legs 1–3 (#6, #7, #8). Those legs' faces and their decisions:
  1. **The pre-commit hook was stored `100644` by the round** (a confirmed bug-class), so macOS's git skipped it. Decision: a machine check, hub Story 244.1.16 ✅ DONE (2026-10-27): a round stages the declared mode, and the sync check reads a lost one. tigerfid's own fix is #5. The hub's filing judged it one face, with no register class.
  2. **The pre-commit secret scan failed closed (`ENOBUFS`) on #7's deletion** (a confirmed bug-class: it buffered lines it never reads). Decision: a machine fix, hub Story 244.1.20, filed from hub inbox #4409. The hook was never skipped.
  3. **The born tooth `build-inputs-name-notices` could never match** its target's real shape; #8 corrected it after measuring both cases. A seat goes to the register under `manifest-check-keyed-on-a-plan-spelling` (decided, kb-entry), caught before the close relied on it.
  4. **The first machine's reference folder was moved, not copied,** by a hand slip. One face; the tool's wording became "copy, never move" (#9). A seat goes to the register.
  5. **The same-day re-affirm** (#11, #14) was governed both times by KB `epic-close-protocol` § Same-day multi-leg re-affirmation, form (b). No new face.

  No other item is seen twice.
- **CLAUDE.md truth step.** § CI said `npm test` runs the lint and the reference-check tests. Since #8, `npm test` and CI's `test` job run four commands, as `docs/ci-contract.md` records, and this PR corrects the line. The other claims, read against the epic's landed diff, are current:
  - § The originals: the four kept 2018 paths, per `archive-left-the-tree`;
  - § CI: hosted runners and blobless, not sparse, per `runner-hosted-only` and `sparse-view-retired`;
  - § The licence lane, per `licence-gate-in-test`;
  - § The reference corpus: `npm run setup`.
- **Merge policy:** the manifest is prose-bearing and owner-gated, so this is a MANUAL merge on Omid's word, pinned to the head after its run reads green. The manifest is untouched since #11.
