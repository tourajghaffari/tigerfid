# Project plan — tigerfid

TigeRFID: the ActiveWave active-RFID system — boards, firmware and software
built from 2001 to 2013 — that Touraj Ghaffari's sons open-sourced under
Apache-2.0 in 2017–2018 (the dedication in `readme.md`), and its rebuild
from the ground up. The 2018 state is kept byte for byte at the annotated
tag `activewave-2018` (`f25ff13f3fb672d8ec91f1fadc8894d69f781ed6`) and its
GitHub release; nothing in the history is rewritten. Initiative 1 joined the
repo to the fleet and cleared the current tree for the rebuild (closed
2026-10-27; its record is in `docs/archives/project-plan-completed.md`); Initiative 2
is the design study, which decides the new system and ends in Omid's
decision sitting. Its close files the build: one Reader, one Field Generator
and three tags across two zones, which a complete beginner can follow on
Windows, macOS or Linux with free tools only (milestone 1, Initiative 2).
Everything in this plan is public, written for a stranger (CLAUDE.md
§ Writing in public).

**Provenance / re-review note:** this plan was born at adoption, not audited — authored 2026-10-26 by hub Story 243.1.2 per KB `new-repo-standard`'s authored-plan template, from the approved design spec (hub spec `2026-10-26-tigerfid-onboarding-design`, Omid 2026-10-26, hub PR #4355). No `/audit-project` run yet; the spec's decisions D1–D12 and calls C1–C11 are the re-review record until the first audit.

**Last staleness sweep:** 2026-10-26 (adopted this day — every pointer in
this plan was written against the state it cites; the first sweep proper is
the first sprint proposal's step 2, KB `plan-staleness-sweep`).

## Plan format

Three levels, fleet-standard:

## Initiative N: <name>
### Epic N.M: <name>
#### Story N.M.K: <name>

Completed stories are ticked ✅ DONE in place by the completing PR; the
physical cut to `docs/archives/project-plan-completed.md` happens at epic
close only, never per story — the close PR is the archival PR (the archival
clause; fleet convention, Omid 2026-07-07). Every epic ends with a
`Close out Epic N.M` story and has a manifest in `docs/close/` (KB
`epic-close-protocol`); the PR that edits an epic's stories updates its
manifest.

Sprint orchestration blocks (kept in this file while live) are cut the same
way once every leg story is ✅ DONE **and** the sprint's review has landed —
into the archive's `## Executed sprints` section, newest first, the heading
stamped `— ✅ EXECUTED (date)` plus an **Executed:**/**Reviewed:** record.
Keep that section above the first `## Initiative` block in the archive.

## ASK-OMID register

No open rows. A1, answered, moved to the archive with Initiative 1 (KB
`register-relocation-at-close`, 2026-10-27).

## Sprint 2 — "The study makes its choices: a radio a beginner may legally run, the parts and free tools to build it, and host software for any laptop" (proposed 2026-10-27; 4 legs, serial)

**Approval:** PENDING — Omid signs first-hand in the proposer's terminal, and
the proposer records his words on this line.

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

## Initiative 2: The design study

**Objective:** turn spec D1–D7 into a design a builder can execute, decided
bottom-up against three acceptance scenarios: a conference where every
attendee wears a tag and the software shows each person on the floorplan; a
hospital nursery where each baby's tag carries a temperature sensor, so
staff can find each baby and read the sensor remotely; and a warehouse
inventorying 100,000+ items in minutes. The study answers spec §3: the
requirements with numbers, how the original worked, the radio and location
architecture with its legality, the parts and free toolchains, the software
stack for Windows, macOS and Linux, and the tiered cost — then Omid's
decision sitting writes the rebuild's design spec in this repo (the tigerian
shape: a research initiative with the design sitting at its end). The
anti-goals bind every answer: nothing a beginner cannot build or debug, no
paid tool or single vendor's cloud, no medical claim, no transmitter a
beginner cannot legally run.
**Milestone 1 (filed by this study's close — spec §4):** a builder with no
experience follows `guides/` and ends with one Reader, one Field Generator
and a sealTag, a compactTag and a cardTag built; the host software running
on their own Windows, macOS or Linux machine; all three tags read together
(anti-collision); a tag carried between two zones, the move showing on the
floorplan; a tamper alarm and a temperature reading arriving; and a tag
paired with a person raising an alarm when it leaves without them. Every
shopping list is tiered (D7). Omid is the first builder (D9): milestone 1 is
accepted when he has built it from the guides, and the guides are revised
from what his build taught before anyone else is pointed at them.
**Done when:** Epic 2.1 is archived with close evidence (initiative-final —
Epic 2.1 is its only epic), and its close has filed the build initiatives.

### Epic 2.1: The study, the decision sitting and the rebuild spec

**Goal:** six study documents under `docs/study/`, each answering one part
of spec §3, then the decision sitting and the approved rebuild spec.
**Ordering:** 2.1.1 → 2.1.2 (Sprint 1 legs 4–5) → 2.1.9 → 2.1.3 → 2.1.4 →
2.1.5 (Sprint 2) → 2.1.6 (proposed after Sprint 2's review) → 2.1.7 → 2.1.8.
Serial since the Sprint 2 proposal (2026-10-27): 2.1.4 names parts for the
radio 2.1.3 chooses, and 2.1.5's reader abstraction follows 2.1.3's link.
Manifest: `docs/close/2.1.json` (born FAILING; each study document's tooth
stays RED until its document lands).
**Done when:** every tooth in `docs/close/2.1.json` passes from the repo root
and its prose checks are adjudicated.

#### Story 2.1.1: The requirements, from the three scenarios, with numbers — ✅ DONE (2026-10-27)

**Completed:** 2026-10-27 — tigerfid #14 (branch `story-2.1.1-requirements`).
**Outcome:** `docs/study/requirements.md` carries 27 numbers. They are the five measures for each of the three scenarios, plus each scenario's size as an assumption, and each has its reasoning and Omid's answer recorded first-hand beside it.
- **The answers.** The list went out after the session terminal's `date -u` read of 11:57:23Z, and his one-message answer came before the read of 12:05:05Z. He accepted 24 numbers as recommended and set three himself:
  - [3]: 1,000 tags/s per reader;
  - [12]: 100 tags/s in the nursery;
  - [13]: a nursery move shown within 5 s.
- **His question on [3]** ("Can we do that?") is answered in the document by the arithmetic, marked INFERRED. It needs a fast link with short replies and reader-assigned slots, or several channels per reader; the original's random delay cannot do it.
- **For Stories 2.1.3–2.1.4:** every input to the arithmetic is marked ASSUMED or INFERRED, for those stories to replace with real parts' figures.
- **The ground rules hold.** No datasheet figure is copied (spec Appendix B). The nursery is framed as finding tags and reading a sensor; every "medical" or "vital" mention is a negation (C8).
- **Story 2.1.3 answers first** the hardest pairing: [3]'s 1,000 replies a second against [2]'s 30 m with the tag worn.

**Story:** As the study, I want each scenario turned into measurable
requirements, so that every design option is judged against numbers, not
impressions.
**Context:** spec §3.1. A number for each of: indoor read range; tags per
second through anti-collision (the warehouse's 100,000+ items in minutes
implies hundreds per second — INFERRED from the arithmetic; the study sets
the number); zone accuracy; alarm latency for tamper, temperature, missed
check-in and pairing; battery life. Each number is put to Omid and his
answer recorded beside it. The old datasheets' figures conflict with each
other (spec Appendix B) and are never copied as requirements. The nursery
scenario proves "find each tag and read its temperature sensor" — never a
medical device or a monitor of vital signs (spec C8).
**Done when:** `docs/study/requirements.md` carries, per scenario, a number,
its reasoning and Omid's recorded answer for each of the five measures.

#### Story 2.1.2: How the original worked — ✅ DONE (2026-10-27)

**Completed:** 2026-10-27 — tigerfid #16 (branch `story-2.1.2-original-system`).
**Outcome:** `docs/study/original-system.md` describes the 2001–2013 system from its records at the tag `activewave-2018`: the radio link, the anti-collision scheme, the zone mechanism, the alarms and I/O, and the hardware of the Reader, the Field Generator and the three tags. Each claim cites the file it was read from or is marked INFERRED, and a script found all 208 of its quoted phrases in their sources.
- **Measured:** the packets' CRC is CRC-16/X.25. It matches the records' three test vectors and their `0xF0B8` residue.
- **No record states the radio's data rate**, its modulation or any tags-per-second figure. The requirements' 250 kbit/s stays ASSUMED, and the receiver's 330 kHz IF filter suggests the original ran slower (INFERRED).
- **Three corrections to the onboarding spec** (Context and Appendix B):
  - the compactTag is Tag 102; Tag 103 is the miniTag;
  - the Reader's radio is the Transceiver daughterboard;
  - the 2012 sealTag is an ISO 15693 logging seal with no active radio and no temperature sensor.
- **For Stories 2.1.3, 2.1.4 and 2.1.7:** milestone 1's sealTag is therefore a new design. The document's last section says why: one core in a seal enclosure, per spec §3.7, is weighed by 2.1.4 and decided by 2.1.7.

**Story:** As the study, I want one written description of the 2001–2013
system, read from its own records, so that every option has a reference to
beat.
**Context:** spec §3.2 and Appendix B, read at the tag (or in the reference
folder): the protocol documents (`api/documents/Protocol20061015.doc`,
`Protocol-tg-07092008.doc`, `AW_API.pdf`, `AW_API_V39.doc`), the Reader 103
and Tag 103 schematics and BOMs, and the sealTag's STM8L firmware source —
the only firmware source in the tree (none exists for the Standard Reader
or the PIC tags). The two-way radio: the Reader or a Field Generator wakes
and commands tags at 433 MHz; tags reply at 916/927 MHz (US) or 868 MHz
(EU) with random-delay anti-collision; a zone is a Field Generator plus a
motion detector.
**Done when:** `docs/study/original-system.md` describes the radio link, the
anti-collision scheme, the zone mechanism, the alarms and I/O, and each
milestone device's hardware, citing for each claim the file at the tag it
was read from; a claim no record supports is marked INFERRED.

#### Story 2.1.9: The Sprint 1 review's two fixes to the original-system study

**Story:** As a stranger reading the study, I want
`docs/study/original-system.md` to list every correction it makes to the
onboarding spec, and the reference documents to say truly why the Outlook
message stays closed, so that I can trust the records over the spec and
never wait on a ruling already made.
**Context:** born 2026-10-27 by the Sprint 2 proposal, from the Sprint 1
review's findings 3 and 4 (tigerfid #23; the archive's Sprint 1 record),
which routed both to Sprint 2's first study PR.
- **Finding 3.** The document makes three corrections to the onboarding spec
  (hub spec `2026-10-26-tigerfid-onboarding-design`). Its corrections list
  (§ The names: products and boards) and its table (§ Where the records and
  the website disagree) carry two. The third is stated in § The Reader: the
  Reader board carries no radio parts, and the radio is the Transceiver
  daughterboard, where the spec's Context lists an LNA and a SAW front end
  among the Reader 103 sheets' parts. The Story 2.1.2 tick names all three.
- **Finding 4.** Three files say the Outlook message waits on ASK-OMID A1:
  `docs/study/original-system.md` § The records read,
  `docs/reference-research.md` § 1 and `docs/reference-repos.md` reading
  rule 7. Omid answered A1 "Keep the default" at #7, and the row is in the
  archive: the file left the current tree unread and stays at the tag.
  Reading rule 7, with A1's answer, is what keeps it closed.
- **Predicates, not examples** (KB `plan-authoring-for-cold-execution` item
  10, its review face). The fix runs two greps:
  - `grep -n -i 'spec' docs/study/original-system.md`: each hit is either a
    correction, which then appears in both the list and the table, or a
    citation, which stays;
  - `grep -n -E 'A1|rules on it' docs/study/*.md docs/reference-repos.md docs/reference-research.md`:
    each hit is refreshed or named current.
- Nothing here edits the hub spec. The Sprint 1 record cites hub inbox #4436
  as already carrying the three corrections to it.
**Done when:** `docs/study/original-system.md` lists three corrections, each
with a row in its disagreement table; no study or reference document says
the message waits on a ruling; and the PR body quotes both greps with each
hit's disposition (manifest prose check `original-system-review-fixes`).

#### Story 2.1.3: Radio, location and legality

**Story:** As the study, I want the radio and location architecture chosen
against the scenarios, the law and a beginner's hands, so that the first
build is legal to run and possible to build.
**Context:** spec §3.3, §3.8 and C9. The options: a sub-GHz system-on-chip
in the original's split (a wake channel plus a reply channel); a
single-band design with wake-on-radio; dual-band chips (sub-GHz for
tracking, Bluetooth for phone setup); Bluetooth Long Range, LoRa and UWB as
comparisons. Pre-certified radio modules are preferred wherever they meet
the scenarios, so a beginner's build is legal without equipment
authorisation. The US rules (47 CFR Part 15 — §15.231 at 433 MHz, §15.249
and §15.247 in 902–928 MHz, §15.23 for home-built devices) and their EU
equivalent are verified before any radio is chosen. The Chorus comparison
(spec D12, §3.8), radio side: four layers (tags, readers, server,
dashboard) with "reader" an abstraction any device emitting the same event
schema can fill; simple readers and a smart server; three hardware tiers
(item tag, reusable badge or fob, combined sensor-and-reader); prototyping
on off-the-shelf modules; and, not to copy, a crowd-sourced reader network.
Each item is ADOPTED or DECLINED with its reason, and the study says whether
ActiveWave's room-level zones from a field generator, its relay outputs and
its wake-on-command still win. From the study's first two documents (the
Sprint 1 review, 2026-10-27): this story answers first [3]'s 1,000 tags/s
per reader against [2]'s 30 m with the tag worn, and meets [3] or brings it
back to Omid with its cost; it chooses reader-assigned slots, a faster link
or several channels for [3] and [21], designs how a zone move is noticed
within [13]'s 5 s, and takes from the chosen radio what no original record
states — the bit rate, modulation, output power, certification and the wake
signal's form (the Story 2.1.1 tick; `docs/study/requirements.md` § Ground
rules and items [3], [13] and [21]; `docs/study/original-system.md` § What
no record states).
**Done when:** `docs/study/radio-and-location.md` compares the options
against the requirements, cites each rule above, names the chosen
architecture, and answers its Chorus items one by one.

#### Story 2.1.4: Chips, modules and the free toolchains

**Story:** As a builder, I want each device's parts and its free firmware
toolchain named, so that I can buy, flash and debug them on my own machine.
**Context:** spec §3.4 and §3.7; D4 (KiCad 10 for the boards), D5
(Windows, macOS and Linux), and the anti-goals (no paid tool, no single
vendor's cloud). For each candidate chip or module: availability, a free
toolchain on all three platforms, a debugger a beginner can afford, and the
licence of any reference design (Story 1.1.4's lane). It weighs §3.7's
question — one electronics core in three enclosures (the seal, the fob, the
card) against three separate boards — a suggestion to weigh, not a
decision. The spec's §7 files §3.7 under 2.1.7: this story weighs it, and
2.1.7's decision sitting decides it. From the study's first two documents
(the Sprint 1 review, 2026-10-27): this story replaces the requirements'
ASSUMED cell, reply-cost and listening figures with the chosen parts'
datasheet figures, chooses for the warehouse item tag ([27]) between a slower
wake and a CR2450-size cell, and weighs milestone 1's sealTag as a new
design inside §3.7's question, because the 2012 sealTag is an ISO 15693
logging seal with no active radio (the Story 2.1.2 tick;
`docs/study/requirements.md` § The arithmetic and item [27];
`docs/study/original-system.md` § What this means for the study).
**Done when:** `docs/study/parts-and-toolchains.md` names the parts per
device, the toolchain per part with its install on each platform, and the
one-core-or-three answer with its reasons.

#### Story 2.1.5: The software stack

**Story:** As a user on any of the three platforms, I want the host
software's stack chosen, so that the floorplan, the alarms and pairing run
on my own machine without a vendor's cloud.
**Context:** spec §3.5, §3.8 and D5. The host runs on Windows, macOS and
Linux, local first (it works offline); the floorplan view; the alarms
(tamper, temperature, missed check-in, pairing); pairing people with
things; an open API. The Chorus comparison (spec D12, §3.8), software side:
an item-history timeline per tag (zones, times, sensor values); alarm rules
set on screen (temperature range, dwell, left a zone, tampered, missed
check-in); "find the nearest available X" by zone; a printed QR code and ID
on every tag, so pairing is scan-and-scan; view-only, time-limited share
links; and, not to copy, a cloud-only service, and free hardware plus a
subscription with printed battery labels. Each item is ADOPTED or DECLINED
with its reason, and the study says whether owned infrastructure with no
subscription and pairing people with things still win. From the study's
second document (the Sprint 1 review, 2026-10-27): this story reads the
original host applications' source, Smart Tracker and the Programming
Station, which Story 2.1.2 left for it (the Outlook message beside them stays
unread under `docs/reference-repos.md` reading rule 7), and, with Story
2.1.3, says whether the rebuild's seal keeps an HF interface for reading by
a phone (`docs/study/original-system.md` § The records read and § What this
means for the study).
**Done when:** `docs/study/software-stack.md` names the stack and why,
sketches the event schema the readers report (tag, reader, signal strength,
time, payload), and answers its Chorus items one by one.

#### Story 2.1.6: The tiered cost of milestone 1

**Story:** As a beginner, I want the first build priced in starter, advanced
and pro tiers, so that I can choose what to spend before I start.
**Context:** spec §3.6, D7 and D9. Priced per device (the Reader, the Field
Generator, the three tags); per build route (hand-solder, partly assembled,
fully assembled); per tool tier (starter, advanced, pro — several ways to
the same end, for example several soldering irons); and the bench D9 needs
(a tool tier plus a cheap radio receiver to see what the boards transmit).
Every price is dated. This is the rebuild's first real budget.
**Done when:** `docs/study/tiered-cost.md` carries the tables with a date on
every price and a total per tier.

#### Story 2.1.7: The decision sitting and the rebuild spec

**Story:** As Omid, I want to decide the design in one sitting from the
study's documents, so that the rebuild has an approved spec to build from.
**Context:** spec §3.9. The six study documents are the sitting's input; its
output is the rebuild's design spec in this repo, at
`docs/superpowers/specs/<date>-rebuild-design.md`, with Omid's approval line
and milestone 1 (Initiative 2) as its acceptance target. From the study so
far (the Sprint 1 review, 2026-10-27): the sitting also decides milestone 1's
sealTag design, which Story 2.1.4 weighs (`docs/study/original-system.md`
§ What this means for the study), and the spec names the rebuild's top-level
layout, which Story 1.1.2 left to the study with a starting shape of
`hardware/`, `firmware/`, `software/`, `guides/` and `docs/` (#7; the
archive's Story 1.1.2 Context).
**Done when:** the spec exists at that path and carries `**Approved by:** Omid`
with the date.

#### Story 2.1.8: Close out Epic 2.1

**Story:** As the fresh-session closer, I want Epic 2.1 closed per KB
`epic-close-protocol` and the build initiatives filed, so that the rebuild
has its next plan.
**Context:** `node ../../scripts/close/close-epic.mjs --epic 2.1` from the
repo root, in a session that did none of the epic's work. Initiative-final.
The close files the build initiatives from the approved spec (spec §4):
hardware in KiCad 10, firmware, software, guides, and milestone 1's two-zone
demonstration — with D9's "set up the bench" story first (a tool tier plus
a cheap radio receiver to see what the boards transmit). Every hardware
story is proven by Omid's hand (D9): `requires: omid` teeth plus committed,
public-safe evidence — photos, measurements and the receiver's captures.
**Done when:** every manifest check passes or is adjudicated, the archive
entry carries the evidence block, Initiative 2 is cut whole, and the build
initiatives are filed in the same PR.
