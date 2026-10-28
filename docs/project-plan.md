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

**Last staleness sweep:** 2026-10-28 (tigerfid #43, the Sprint 3 proposal's step-2 sweep,
KB `plan-staleness-sweep`: scoped to the sprint's inputs, which were Epic
2.1, the archive's Sprint 2 record, `docs/close/2.1.json`, the study anchors
the review's findings cite and `scripts/reference-check.mjs`).

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

## Sprint 3 — "Ready for the sitting: the three studies corrected, milestone 1 priced in three tiers, and a new machine told it may fetch the sources" (proposed 2026-10-28; 3 legs, serial) — ✅ APPROVED (2026-10-28)

**Approval:** approved 2026-10-28 (Omid, first-hand in the proposer's
terminal "tigerfid Sprint 3 proposal", between its `date -u` reads of
10:59:30Z and 11:00:38Z: "Approve as proposed (Recommended)"): the block as
proposed, three legs, D1–D12 at their defaults. The sign-off edit is the
proposer's, on the proposal PR's branch (tigerfid #43).

**Proposed:** 2026-10-28 by the hub-rooted Windows session "tigerfid Sprint 3
proposal" (session `4ef9427a-246a-42f9-98aa-be892d938900`), on Air Traffic
Control's orders 18, sent 10:49:58Z. The session was fresh by `/clear`: its
background-task path id, `2a0782f7-f632-4bab-b967-a9cfbfe1ba93`, is the
process of the session "tigerfid Sprint 2 review", which this `/clear`
ended. Role proposer; not elevated (a Windows administrator-role read:
`False`).

**The outcome:** Epic 2.1's study made ready for Omid's decision sitting:
- the Sprint 2 review's corrections land in the three technical studies, so
  the sitting decides on figures that hold and on the HF ruling Omid gave
  (Story 2.1.10);
- milestone 1 is priced per device, per build route and per tool tier, every
  price dated: the rebuild's first budget (Story 2.1.6);
- the reference check tells a new machine that the study's sources may be
  fetched from their URLs (Story 2.1.11).

**Admission (the meta-work test):** legs 1 and 2 ship public documents that
a stranger reads on GitHub this month and that the sitting decides from. Leg
3 changes what `npm run setup` prints to every stranger who sets up the
reference folder.

**Inputs swept 2026-10-28.** The scope is Epic 2.1 (its preamble, its
Ordering line and Stories 2.1.6–2.1.8), the archive's Sprint 2 record (its
`**Reviewed:**` findings, nits and routed list), `docs/close/2.1.json`, each
study anchor a finding cites, and `scripts/reference-check.mjs` for F11.
- **The events digest.** `git log origin/main --oneline --grep '<id>'` for
  each of 2.1.6, 2.1.7 and 2.1.8:
  - 2.1.6: only #41, which wrote its Context sentence;
  - 2.1.7: #23, #39 and #41, each a Context edit;
  - 2.1.8: no hit.

  The same grep for 2.1.10, 2.1.11 and 2.1.12 finds nothing, so the ids are
  free. `docs/notes.md` and the archive name 2.1.6–2.1.8 only as forward
  work. `node <hub>/scripts/fleet/open-prs.mjs --repos tigerfid` read 0 open
  PRs, and `git ls-remote --heads origin` lists `main` alone. Every story is
  CURRENT; none has run.
- **Each finding's anchor, re-read on `main` (`9cb0127`)** before a story
  cites it:
  - finding 1: `software-stack.md` lines 588 ("one browser only"), 608 (the
    Decided line, "no space") and 686 (its table row);
  - finding 2: `radio-and-location.md` names `ZAT-1312PSIP-2` at lines 365,
    530 and 534, and `-3` nowhere (`grep -c` 0);
  - finding 3: `parts-and-toolchains.md` line 167 charges "a one-shot
    conversion of 15.5 ms";
  - finding 4: `radio-and-location.md` line 477, "the Field Generators it
    last heard";
  - finding 5: `parts-and-toolchains.md` lines 242 ("except the Arm GNU
    Toolchain"), 248–249 ("no price stated on the page") and 251 (CMake and
    GNU make, no platform cells); `software-stack.md` lines 473–476
    ("Against 921,600 baud", "222 %");
  - the nits: `software-stack.md` 85 ("It carries no time"), 347 and 470
    ("[3]'s 1,363"); `radio-and-location.md` 292 ("no worse by the
    tables"); `parts-and-toolchains.md` 324 (§2.803's notice) and 384
    ("counted only the sending antenna").
- **Refreshed in this PR:**
  - Epic 2.1's Ordering line, which named the fix-up without its story;
  - Story 2.1.7's Context, whose "until the Sprint 3 fix-up corrects it"
    now names Story 2.1.10.
- **Observed, the closer's (Story 2.1.8):** the close runner prints "REVIEW
  SEATING MISSING" for Sprint 2's review. That review sent its fifteen seats
  to Air Traffic Control as text; the closer censuses the register.
- This PR refreshes the plan's staleness stamp.

The standard (KB `sprint-proposal-standard`) was read by its steps 0–8 at
their rule level and the newest entries of its friction log (P60–P74), not
entry by entry, with the rows Air Traffic Control's orders named: KB
`plan-authoring-for-cold-execution` item 53, a test-arming read per leg, §
Roles' "flagged" wording and the touched-file ledger.

**Why this order (step 2):**
- **Leg 1** goes first, as the review routed it: its findings 3 and 4 can
  move a cell and the tags' listening budget, which leg 2 prices. The
  corrections land before prices sit beside them.
- **Leg 2** is the epic's path to the sitting. Its prices are dated as near
  the sitting as the sprint allows.
- **Leg 3** gates nothing in the study and edits no study file. It touches
  `scripts/`, so it merges by Omid's word rather than by auto-merge, and
  placing it last keeps the sitting's path from waiting on that merge. It
  shares only the plan, the manifest and the friction log with legs 1 and 2,
  so serial order spares those files a conflict.
- **Stories 2.1.7 and 2.1.8 stay outside the sprint.** The sitting is Omid's,
  and no executor seat can hold it. The close needs the sitting's spec for
  `rebuild-spec-approved` and `build-initiatives-filed`, and a closer who did
  none of the epic's work.

**The sprint's shape.** Three legs, under the cap. Weighed and declined:
- **Two fix-up stories** (the battery and the mechanism; the rest): one
  leg's plan fits the eight-task tripwire on the estimate in § Pre-flight,
  and D9 names the seam if it does not.
- **F11 outside the sprint, as a chore:** the review routed it as a story,
  and it is small.
- **Story 2.1.6 in a later sprint, after a review of the fix-up:** leg 2
  prices both cells wherever the studies leave the choice to the sitting
  (D5), so no fix-up outcome re-prices it. A review between them would cost
  a proposal and a review for no change a price depends on.

1. **Leg 1 — Story 2.1.10** — the Sprint 2 review's fixes in
   `docs/study/software-stack.md`, `docs/study/radio-and-location.md` and
   `docs/study/parts-and-toolchains.md`: findings 1–5 and the five nits,
   with finding 4's mechanism chosen (D3) and its listening cost carried into
   the battery figures with finding 3's (D2). Gate: the story's greps quoted
   in the PR body, each hit's disposition beside it, and a claims audit over
   every changed figure. Flip map: `fcc-ids-both-named` FAIL → PASS; the four
   study teeth stay PASS. Prose for the closer: `studies-review-fixes`, with
   `three-platform-claims-sourced`, `free-tools-three-platforms` and
   `no-medical-claim` read again where the leg edits. Item-8 scope: the
   three studies, `docs/study/requirements.md`'s dated pointer lines (D1),
   and Story 2.1.7's Context.
2. **Leg 2 — Story 2.1.6** — `docs/study/tiered-cost.md`: milestone 1 per
   device, per build route (hand-solder, partly assembled, fully assembled)
   and per tool tier (starter, advanced, pro), with D9's bench and a total
   per tier, every price dated (D5–D8). Flip map: `tiered-cost-doc` FAIL →
   PASS. Prose for the closer: `tiers-dated` and `no-medical-claim`. Item-8
   scope: the three studies' lines that hand work to 2.1.6 (each § What the
   choice leaves to later stories, and `parts-and-toolchains.md` § The
   debugger's "Story 2.1.6 prices the rest").
3. **Leg 3 — Story 2.1.11** — the reference check names the fetch route for
   a missing `sources/` file or folder (D11). Gate: the new tests seen red
   against `main`'s script, then green, with `npm test` green. Flip map: no
   tooth flips. Prose for the closer: `reference-check-sources-route`.
   Item-8 scope: `docs/reference-repos.md` and `docs/reference-corpus.tsv`'s
   header comment, where they describe the tool's messages.

After the sprint, `rebuild-spec-approved` (Story 2.1.7) stays RED.

**Decisions (step 5) — no decision currently blocks execution on Omid.** Each
is the executor's at its default unless Omid says otherwise at the approval.

- **D1 — A study chooses; the sitting decides** (Sprint 2's D1, unchanged).
  A number Omid set or accepted in `docs/study/requirements.md` changes only
  by his word. A figure that a dated pointer line there quotes, and that this
  sprint changes, gets a new dated pointer line beside it; the text stays
  otherwise unchanged.
- **D2 — A margin that fails on paper after the re-pricing.** If findings 3
  and 4 together leave [9], [18], [26] or [27] unmet on paper at the planning
  capacity:
  - the leg records it, names the routes with their costs (measure first, a
    larger cell, a slower listen where a latency allows it, the fallback in
    which the Field Generators listen) and recommends one, and the sitting
    decides. No requirement is lowered.
  - Only if no route meets the number does the leg put it to Omid first-hand
    in its terminal, framed by the goal the number serves with the
    recommendation for that goal (the review's friction R3), and record his
    words. If he cannot be reached in the leg, it files ASK-OMID A2 with that
    question. Default: the number stands, and the study's recommendation is
    the closest route. The leg proceeds on the default.
- **D3 — Finding 4's mechanism is the leg's choice.** The review's example is
  the Reader's beacon naming the slots of the zone's neighbours. Others the
  leg may weigh: the Field Generators' beacons grouped in one block after the
  Reader's beacon, or a periodic wider listen. The leg weighs each on its
  listening cost, on [13]'s 5 s move and on [22]'s 5 min, states the chosen
  one in `radio-and-location.md` § Zones, and carries its cost into
  `parts-and-toolchains.md` § Each worn tag's year and § [27].
- **D4 — Finding 5: cite it or mark it.**
  - **The tools:** each tool's platforms, install and licence are cited from
    its maker's page by URL and fetch date, or marked not read. A licence
    read from the tool's own licence text names it.
  - **The wire frame:** the line rate the table is measured against is
    sourced or marked ASSUMED. The module's 3 MBaud UART (PSIP p26) is
    stated beside it, the USB bridge's own limit stays with the build
    initiatives, and one sentence says whether the binary frame still wins
    and why.
- **D5 — Prices.**
  - In US dollars, each from a seller's public page, with the date it was
    read, the quantity it is for and the page's URL.
  - Cited, never pinned: no choice rests on a price page (Sprint 2's D8).
  - Shipping and tax are left out, and the document says so.
  - Where the studies leave a choice to the sitting (the item tag's cell; the
    nursery tag's cell if the measurement falls short), both options are
    priced.
  - Each tier's total states what it is made of: which devices, at which
    build route, with which tools and bench.
- **D6 — A part the studies leave unnamed** (the USB bridge, the regulator,
  the relays, the passives, the enclosures) is priced as a representative
  part the leg names, marked "representative, for pricing" with the reason.
  It is never presented as a choice: the build initiatives choose it.
- **D7 — A page that refuses a plain request** (as the FCC's, FTDI's and
  Microchip's did for Sprint 2: F8, F13) is put to Omid early in the leg, in
  ONE message listing every such URL with what each price is for and the
  leg's default if it stays unread. His answer is recorded first-hand, with
  the terminal's two bracketing `date -u` reads. A price no one reads is
  written "not read", and the tier's total names what it lacks. No total
  counts an unread price as zero.
- **D8 — Milestone 1 is priced for the US build** (915 MHz parts), since
  Omid, its first builder (spec D9), builds it there. The parts that differ
  for the EU build are named, not priced.
- **D9 — A leg is a session.** The tripwire: an executor's plan above eight
  tasks stops before its first task, narrows the leg in story order, splits
  the remainder off as a new story under the same step, and logs the miss
  against this proposal. The seams:
  - **Leg 1:** findings 1, 2 and 5 and the five nits become the remainder.
    Findings 3 and 4 stay, because they can move what leg 2 prices.
  - **Leg 2:** the tool tiers and D9's bench become the remainder. The
    devices and the build routes stay.
  - **Leg 3:** none; it is small.

  A session limit mid-leg pushes the branch and leaves a notes DRAFT, and the
  resumed sitting re-runs the re-verify set first.
- **D10 — Every claim is sourced or marked** (Sprint 2's D10, unchanged):
  every number and every "the rule allows" or "the part supports" sentence
  names its source, is MEASURED with the read, or is INFERRED with its
  arithmetic. Also binding:
  - no figure from the 2005/2017 datasheets becomes a requirement (spec
    Appendix B);
  - no medical claim (spec C8);
  - no third-party text, figure or file is copied into the repo (the licence
    lane);
  - nothing from the reference folder is committed;
  - the Outlook message stays unread (reading rule 7).
- **D11 — F11's fix is the message, not a fetcher.** A `--fetch` arm that
  downloads `sources/` rows is declined:
  - **its cost:** network code in the tool, and tests that need a stand-in
    for the network;
  - **its benefit:** eight rows, fetched once per machine;
  - **its risk:** sellers refuse plain requests (F13), so the tool would fail
    where a browser succeeds.

  The tool keeps its rule, "The tool never fetches a file row"
  (`docs/reference-repos.md`).
- **D12 — Merges.**
  - **Legs 1 and 2 are docs-only** (`docs/**`). Each merges by `gh pr merge
    --squash --match-head-commit <full sha>` on green, under the session's
    merge delegation as it stands, or by Omid's word.
  - **Leg 3 touches `scripts/`.** It merges by Omid's word, which his
    in-session delegation sentence is (this repo's `CLAUDE.md` § Git
    workflow).
  - **A relayed delegation can be refused** by the auto-mode classifier. The
    leg then holds and tells the coordinator the PR and its head sha, and
    Omid's own `!` in the leg's terminal is the pass.

**The touched-file ledger** (Sprint 2's F21: leg 4's grants left out the
research log). Every file each leg may edit, so the coordinator's grants name
them all. Every leg edits `docs/project-plan.md` (its tick),
`docs/close/2.1.json` (its `reviewedFor` line) and this sprint's friction log.
- **Leg 1:**
  - the three studies;
  - `docs/study/requirements.md`, only for a dated pointer line (D1);
  - `docs/reference-research.md` § 4, the research log's entry for the leg's
    reads (MDN's Web NFC data, the tools' pages, the tag's `AWI_API.h`), as
    every Sprint 2 study leg wrote one;
  - `docs/reference-corpus.tsv` and `docs/reference-repos.md` § `sources/`,
    only if the leg pins a source a choice rests on (Sprint 2's D8);
  - Story 2.1.7's Context sentence on the HF footprint, refreshed once the
    study carries the ruling.
- **Leg 2:**
  - `docs/study/tiered-cost.md` (new);
  - `docs/reference-research.md` § 4, the price reads' entry;
  - the corpus files only if it pins a source (none is expected, D5);
  - a study's § What the choice leaves to later stories only where the item-8
    grep finds a line the leg answers.
- **Leg 3:**
  - `scripts/reference-check.mjs` and `scripts/reference-check.test.mjs`;
  - `docs/reference-repos.md` only if a line there quotes the old message.

  `docs/ci-contract.md` does not change: `test` already runs the test file.

**Pre-flight (step 4), per leg** (KB `plan-authoring-for-cold-execution`
item 53's two halves, and a test-arming read):
- **Leg 1:**
  - **narrowing half:** each item of the review's routed list maps to one
    Context bullet of Story 2.1.10 (findings 1–5, the five nits), and the
    story's Done-when names each. Nothing is narrowed; finding 1's two
    "fitting option" lines stay because the review measured them right.
  - **forward-seam half:** leg 2 reads leg 1's cell choices and battery
    figures, and prices both cells (D5), so a leg 1 outcome moves no price.
    Leg 3 shares no file with leg 1 but the plan, the manifest and the log.
  - **test arming, MEASURED** through `scripts/ci-docs-only.mjs`'s
    `classify()` with `repoOptions('tigerfid')`: `test` runs in full, because
    the plan and the manifest are CI-validated docs and this repo records no
    validator job.
  - **estimate:** seven tasks (re-verify; finding 4's mechanism; findings 3
    and 4 in the battery; finding 1 and two nits; finding 2 and a nit;
    finding 5 and two nits; the greps, the audit, the tick and the PR).
- **Leg 2:**
  - **narrowing half:** the Done-when's "tables with a date on every price
    and a total per tier" against spec §3.6 and D7: per device, per route,
    per tool tier, and D9's bench, each a table. `tiers-dated` reads all
    four.
  - **forward-seam half:** Story 2.1.7's sitting reads the totals, and the
    build initiatives' bench story reads the bench table. Leg 3 touches
    neither.
  - **test arming, MEASURED** the same way: `test` runs in full.
  - **estimate:** eight tasks (re-verify; the price list and the D7 message
    to Omid; the device tables; the boards and assembly routes; the tool
    tiers; the bench; the totals; the greps, the audit, the tick and the
    PR). It sits at the tripwire, and D9's seam is named.
- **Leg 3:**
  - **narrowing half:** F11's sentence, the "copy-only" message where D8 lets
    a `sources/` file be fetched from its row's URL, maps to the Done-when's
    three cases. F18, the same message met again in leg 4, is the same
    fix.
  - **forward-seam half:** no later leg in this sprint. The build
    initiatives' new `sources/` rows read the same message.
  - **test arming, MEASURED** the same way: `test` runs in full, since
    `scripts/` is not documentation. The new tests run in `npm test` and in
    CI's `node --test scripts/reference-check.test.mjs`.
  - **estimate:** four tasks (the red tests; the messages and the header
    comment; the item-8 grep over the corpus documents; the tick and the
    PR).

**Checks per leg:**
- the flip map, through `node <hub>/scripts/close/close-epic.mjs --epic 2.1
  --repo <F> --only <ids>`;
- `node scripts/close-lint.mjs` and `npm test` (the `test` job's commands),
  unpiped, on every leg;
- `node scripts/reference-check.mjs` on every leg that adds corpus rows, and
  on leg 3.

**Gate line (every leg):** the required set is `scope` + `test` (the
`protect-main` ruleset).
- **`test` runs in full on every leg**, by the test-arming reads above.
- **`scope` also gates the tick and secrets.** It runs
  `check-tick-merge-claim.mjs` (a tick never claims its own PR's merge) and
  the secret scan.

**Merge-loop line:** each leg's PR:
1. ticks its story in place: the heading gains `— ✅ DONE (date)`, with
   `**Completed:**` (the date, `tigerfid #N`, the branch and the pre-tick
   head, never the merge) and `**Outcome:**` directly under it;
2. appends its `reviewedFor` line to `docs/close/2.1.json` in the same PR
   (lockstep);
3. runs the item-8 anchor grep, `grep -n "Story 2.1.<K>"
   docs/project-plan.md docs/close/2.1.json`, and the document-name grep,
   `grep -rn "<document>.md" docs/`, under a PR-body heading titled exactly
   "Item-8 anchor grep" (the hub's `check-item8-dispositions.mjs` reads no
   other title: the review's friction R1). Each hit's disposition stands
   beside it (refreshed, current, or frozen in a DONE story), and each
   `current` one quotes its line;
4. runs the C8 grep, `grep -n -i -E 'medical|vital' docs/study/<document>.md`
   (each hit a negation), and reads the diff for a hostname, an IP address, a
   machine name, a private path, or any person beyond those `readme.md`
   names;
5. carries a CLAIMS AUDIT in its body: every number and every rule or part
   claim beside its source, its measurement or its INFERRED arithmetic (D10);
6. runs `node scripts/close-lint.mjs` and `npm test` unpiped, gated by `&&`
   all the way to `git commit`;
7. rebases on the fetched `main` and merges per D12, after `node
   <hub>/scripts/fleet/watch-pr.mjs <N> --repo tigerfid`.

No CHANGELOG.

**Kickoff (step 6, baked):** a FRESH executor session per leg, on either box:
1. **Open:** from the hub root, `node scripts/fleet-view.mjs --start
   "tigerfid Sprint 3 leg <N> — Story 2.1.<K>" --role executor --repos
   tigerfid` (the marker goes to the hub's origin by itself). It fetches and
   fast-forwards what it may. When it prints a read-at-origin line for the
   hub, the hub's tools are read at `origin/main` and the hub is never
   pulled.
2. **Fetch this repo:** on `main`, `git -C <F> fetch origin` and `git -C <F>
   merge --ff-only origin/main`, each unpiped, its exit read.
3. **Clocks:** `date -u` before every clock written.
4. **Read, before any search:**
   - this block and the leg's story, with its checks in
     `docs/close/2.1.json`;
   - `CLAUDE.md` (this repo's), § Writing in public above all; a hub-rooted
     seat reads it by hand, since it does not load there;
   - `docs/reference-repos.md`, for the reading rules, and
     `docs/reference-research.md`, for § 4's search ideas;
   - the study documents the story cites, and the archive's Sprint 2
     `**Reviewed:**` record (leg 1);
   - the previous leg's PR body and this sprint's friction log.
5. **Re-verify first:** the set below.
6. **Branch:** `story-2.1.<K>-<slug>`, from the fetched, fast-forwarded
   `main`.

Paths and habits:
- `<hub>` is the hub root's absolute path on the executor's box, `<F>` is
  `<hub>/projects/tigerfid`, and `<SCRATCH>` is the session scratchpad
  (`mkdir -p` first; on Windows `$TMPDIR` is unset).
- Checkers run unpiped, and a census never reads through `head`, `tail` or
  `cut`. Long files are written with the Write tool. `git -C` takes the
  literal path, never a shell variable (the commit guard cannot resolve one
  and judges the session's own checkout). `npm --prefix <F> test` runs the
  repo's checks without a `cd`. `close-lint` lints the repo it is run FROM:
  `node <F>/scripts/close-lint.mjs` from the hub root lints the hub and
  prints the hub's adoption date, 2026-07-06, not this repo's 2026-10-26.
  `npm test` here is light and takes no battery lock.
- On Windows, a colon path through git takes `MSYS2_ARG_CONV_EXCL='*'`, and
  no interpreter is fed by stdin.
- On the Mac, the lid stays open while any fleet job runs (KB
  `mac-standby-sleep-abandons-jobs`).
- CI runs on GitHub-hosted runners only. No leg edits `.github/`, and only
  leg 3 edits `scripts/`. A stranger's PR is never checked out.

**Friction log:** `docs/friction-log-sprint3-ready-for-the-sitting.md`. Each
leg appends its section, written for a stranger:
- its context checkpoint: the plan's task count, compactions, forced
  re-reads and lost-context defects;
- each piece of friction it met;
- each guard event with the guard's own banner line quoted whole and its
  register class id beside the word "seat", so the hub's
  `check-register-seats.mjs` can pair it (it read none of Sprint 2's, which
  were written as prose). A seat note says "flagged" or "drew the arm",
  never a verdict word, when a census line precedes the banner in its row (§
  Roles).

**Re-verify at execution** (each leg, before its first edit):
- `git -C <F> log --oneline -3 origin/main` shows the previous leg's merge;
- `git -C <F> config user.email` is Omid's GitHub noreply address;
- `node scripts/close-lint.mjs` exits 0 and `npm test` is green on `main`;
- `node scripts/reference-check.mjs` exits 0 on this box. A `MISSING file`
  under `sources/` is fetched from its row's URL or copied (Sprint 2's D8);
- the flip map's teeth read as the block says through the close runner
  before the work;
- the ASK-OMID register: any row an earlier leg filed, with its answer if
  any;
- leg 1 only: each finding's anchor above still reads as quoted.

**Independent checks for the reviewer pass** (as Sprint 2's):
- **leg 1's quotes against their sources:** the tag's `AWI_API.h` for
  `logTimestamp`, and Smart Tracker's source read as live code, not comment
  blocks (Sprint 2's F22); the eCFR's §2.803; PSIP p16 and p26; TMP117 p6
  and p12; SWRA797 p14 and p17; MDN's Web NFC data;
- **leg 1's arithmetic:** every re-priced margin reproduces from the study's
  own inputs, by a script;
- **the Chorus adopt-and-decline list** of `radio-and-location.md` and
  `software-stack.md`, unchanged by leg 1 unless the leg says why;
- **leg 2's prices:** a sample re-read at their URLs, any change since the
  read date named as the date's, not as an error;
- **leg 3's tests,** run against `main`'s script before the leg, red.

**Verified-live at proposal** (2026-10-28, the session above, after its
`date -u` read of 10:48:56Z, on Node v24.22.1):
- `node scripts/close-lint.mjs`: `close-lint: ok (adopted 2026-10-26)`;
  `npm test`: 17 of 17, exit 0, unpiped; `node scripts/reference-check.mjs`:
  299 rows verified, exit 0.
- **The close runner** at `9cb0127`: `lint` PASS, `legality-rules-cited`
  PASS, `tiered-cost-doc` FAIL (the file is absent) and
  `rebuild-spec-approved` FAIL (exit 1), each for its right reason.
- **The test-arming reads**, through `classify()` over each leg's ledger:
  the proposal and legs 1–3 read `docsOnly=false lintOnly=false`; a
  notes-only wrap reads `docsOnly=true`.
- **`fcc-ids-both-named`, executed against real files** through the close
  runner (shell: Git Bash), after this session's `date -u` read of
  10:57:11Z:
  - on this PR's tree: FAIL (exit 1), the study naming `-2` alone;
  - in a scratch worktree of `3770051` with `ZAT-1312PSIP-3` appended to
    the study: PASS (exit 0);
  - the same, with every `ZAT-1312PSIP-2` replaced: FAIL (exit 1).
- **The parser,** `node <hub>/scripts/fleet-view/check-sprint-block.mjs
  <hub> <plan> 3`, read this block as proposed, exit 0:
  `{"found":true,"approvedDate":null,"executed":false,"reviewed":false,"declaredLegs":3,"legs":["2.1.10","2.1.6","2.1.11"],"legsSource":"structured","ambiguous":false,"sweptDate":"2026-10-28","problems":[]}`.
- **The parser, as approved:** the same command over a scratch copy with an
  approval pasted into the heading and the label line, exit 0:
  `{"found":true,"approvedDate":"2026-10-28","executed":false,"reviewed":false,"declaredLegs":3,"legs":["2.1.10","2.1.6","2.1.11"],"legsSource":"structured","ambiguous":false,"sweptDate":"2026-10-28","problems":[]}`.

**After the sprint:** the last leg's executor stamps the block as executed:
the heading's stamp, the Executed record, and a line saying the review is
pending. It then wraps. Then, in order:
1. the Sprint 3 reviewer pass (FRESH);
2. the decision sitting (Story 2.1.7), Omid's;
3. Epic 2.1's close (Story 2.1.8, a FRESH closer).

## ASK-OMID register

No open rows. A1, answered, moved to the archive with Initiative 1 (KB
`register-relocation-at-close`, 2026-10-27).

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
2.1.5 (Sprint 2) → 2.1.10 → 2.1.6 → 2.1.11 (Sprint 3) → 2.1.7 → 2.1.8.
Serial since the Sprint 2 proposal (2026-10-27): 2.1.4 names parts for the
radio 2.1.3 chooses, and 2.1.5's reader abstraction follows 2.1.3's link.
The Sprint 2 review (2026-10-28) routes a fix-up of the three studies,
Story 2.1.10, ahead of 2.1.6: its findings can move a cell and the tags'
listening budget (the archive's § Executed sprints › Sprint 2). It routes
F11's message fix, Story 2.1.11, after them.
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

#### Story 2.1.9: The Sprint 1 review's two fixes to the original-system study — ✅ DONE (2026-10-27)

**Completed:** 2026-10-27 — tigerfid #29 (branch `story-2.1.9-review-fixes`).
**Outcome:** both findings fixed in the three files, and nothing else moved.
- **Finding 3.** `docs/study/original-system.md` now lists three corrections
  to the onboarding spec, and its disagreement table has a row for each. The
  new one is the Reader's radio, which is on the Transceiver daughterboard.
  § The Reader now points to the list.
  - MEASURED in the reference folder: the Transceiver's pick-and-place names
    the 916.5 MHz SAW filter, the BGA2001 and the SA636 once each. The Reader's
    Rev 6 netlist names no SAW, BGA2001, SA636, NE85619 or BFR193 part.
- **Finding 4.** Three files said the Outlook message waits on ASK-OMID A1:
  the study's § The records read, `docs/reference-research.md` § 1 and
  `docs/reference-repos.md` reading rule 7. Each now says that Omid answered
  A1 "Keep the default" at #7, and that reading rule 7 keeps the file closed.
- **The story's two greps** are quoted in the PR body with each hit's
  disposition. The headings Sprint 2's later legs cite are unchanged, and
  `original-system-doc` stays PASS.
- **Before this leg, its own PR** (#28): a root `.gitattributes` pins LF line
  endings, and this clone's CRLF working tree was repaired (hub #4452;
  friction F4).

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

#### Story 2.1.3: Radio, location and legality — ✅ DONE (2026-10-27)

**Completed:** 2026-10-27 — tigerfid #31 (branch `story-2.1.3-radio-and-location`).
**Outcome:** `docs/study/radio-and-location.md` reads the law first, compares six options against the requirements, and chooses one sub-GHz band on a pre-certified module.
- **The law, read first, each text dated with its URL.** 47 CFR Part 15 at the eCFR's point in time of 2026-10-23: §15.3, §15.5, §15.23, §15.212, §15.231, §15.247, §15.249 and Subpart F. Also the Radio Equipment Directive (consolidated 30.05.2026), Decision 2006/771/EC (consolidated 23.01.2025) and ERC/REC 70-03 (October 2026). The three ETSI standards were read as their version listings only, and are marked so.
- **What the law decided first.** §15.231 rules out the original's 433 MHz wake field in the US for [13]'s 5 s. §15.23 covers milestone 1's five devices and no more, so the system's radio needs its own modular grant. The EU has no home-built route.
- **[3] against [2], answered first (D2):** met, with no ASK-OMID row. TI's CC1312PSIP module (FCC ID ZAT-1312PSIP-2, a single-modular grant under §15.247) runs its certified 300 kbit/s 2-GFSK PHY with reader-assigned slots. That gives about 1,360 slots a second on one channel (INFERRED). [2]'s 30 m worn is met on paper, with 21.8 dB of margin before fading at the worst body loss; the crowded hall is the unmeasured risk.
- **The rest of the choice.** Channel hopping in 902–928 MHz holds under either grant class, since the grant's own record was unread (HTTP 403). In the EU the band is 865–868 MHz at 25 mW e.r.p. under a 1 % duty cycle. Zones come from Field Generators' beacons, and a move is shown within 3 s against [13]'s 5 s (INFERRED). The chosen radio states the bit rate, modulation, output power, certification and wake signal's form that no original record does.
- **Chorus, radio side:** five items, four ADOPTED and the crowd-sourced network DECLINED. ActiveWave's room-level zones, relay outputs and wake-on-command each still win. D5's radio side: a passive HF interface needs no transmitter of its own (INFERRED from §15.3(o)).
- **For Story 2.1.4:** the module grant's record, the parts around the module, and the ASSUMED antenna, slot and start-up figures. Three TI sources are pinned under the corpus's new `sources/` (294 rows).

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

#### Story 2.1.4: Chips, modules and the free toolchains — ✅ DONE (2026-10-27)

**Completed:** 2026-10-27 — tigerfid #33 (branch `story-2.1.4-parts-and-toolchains`).
**Outcome:** `docs/study/parts-and-toolchains.md` names each device's parts around TI's CC1312PSIP, the free toolchain for each with its install on Windows, macOS and Linux, the debugger, and one core in three enclosures as its recommendation.
- **The parts.**
  - **The Reader:** the module at 17 dBm on a certified whip (+0.90 dBi), a USB bridge of the CDC class so that no platform needs a driver, a 3.3 V regulator, and the original's relays and inputs.
  - **The Field Generator:** the same module and antenna on a USB adapter's 5 V.
  - **The tags:** the module at 12.0 dBm on a certified chip antenna (+1.0 dBi), four layers, a CR2032 with a buffer capacitor, a tool-opened compartment, TI's TMP117 on the nursery and cold-chain tags, and the tamper loop on a wake pin.
- **The toolchains, cited from the vendors' pages, none run by this leg:**
  - KiCad 10.0.7, the SDK 8.33.00.16 from TI's public Git repository, SysConfig, TI Arm Clang, UniFlash, and CCS as an optional IDE, each on all three platforms;
  - TI's cloud tools, SmartRF Studio (Windows only) and IAR (paid) declined.
- **The debugger:** the LP-XDS110ET (59.29 on TI.com, read 2026-10-27), because it measures the current the battery tests need. TI's evaluation terms address professional users, and the sitting should know it.
- **The licences.** TI's reference design and SDK carry TI's own terms, which are not on the allowlist, so nothing is copied. The boards follow SWRA797's written layout rules.
- **The figures replaced (D1).** The cell is rated at 235 mAh, with the 150 mAh planning figure now bounded below at 117.5 mAh. A reply costs 18.09 µC. Listening costs 13.9 µA at 1 s and 1.8 µA at 12 s. Standby is 1.0 µA, and the Reader's antenna is +0.90 dBi. `requirements.md` carries the four dated pointer lines.
- **[27] (D4): the slower wake, a listen every 12 s.** It keeps five years on a CR2032 on paper, with a margin of 5–11 %, and [26] is met at 25 s of 30. A CR2450 alone lasts about 2.9 years at a 1 s wake. The CR2450 is recommended as well, for about 2.5 times the need. No ASK-OMID row.
- **[9] and [18].** The conference badge listens every 2 s and lasts 1.99 years. The nursery's tag holds [18] on paper by 6 %; measure first, then a CR2450 if short.
- **One core or three (D3):** one core, recommended, with the sealTag a new design inside it: the core in a band whose loop wakes it. The sitting decides.
- **Not read:** the module's FCC grant record. The FCC's search and two mirrors refused this leg's requests, and Omid answered "Write it as not read". TI's documents name two IDs, ZAT-1312PSIP-2 and -3, for a browser read before any part is bought. Subpart B is read: the Reader's USB and relays make it a digital device, which §15.23 covers in milestone 1.
- Five sources are pinned under `sources/` (299 rows).

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

#### Story 2.1.5: The software stack — ✅ DONE (2026-10-28)

**Completed:** 2026-10-28 — tigerfid #36 (branch `story-2.1.5-software-stack`, pre-tick head `bcf062f5`), with the research log's Story 2.1.5 entry (`docs/reference-research.md` § 4).
**Outcome:** `docs/study/software-stack.md` reads the original host programs at the tag, chooses a small local Python program whose screen is the user's own browser, sketches the event schema and the API, and answers its Chorus items one by one.
- **The original host programs, read at the tag.** Both are Windows only:
  - **Smart Tracker:** C# on .NET Framework 4.0, the API's x86 wrapper, and MySQL through ODBC;
  - **the Programming Station:** C++Builder, building the protocol's packets itself.
  - **What they did:** zones keyed by a Reader and a Field Generator, alarm rules on screen, a history per tag, and access by group and schedule.
  - **What they lacked:** no event carried a time, so the host stamped its local arrival time (263 `DateTime.Now`, no `UtcNow`, MEASURED). There is no missed check-in alarm and no temperature in Smart Tracker's source. The floorplan program has no source in the tree, and the station's encryption keys are compiled in. The Outlook message stays unread.
- **The stack (option A).** Python 3 with its standard library's `sqlite3` and HTTP server, and pyserial (pure Python, BSD-3-Clause) for the Reader's port. The page is plain HTML, CSS and JavaScript, with SVG and Server-Sent Events, and nothing is fetched from the internet. It listens on the user's own machine only in milestone 1.
  - **Measured here, on macOS 26.6.2:** 400,000 rows at one transaction per frame in 0.36 s, and 1,000 streamed events in 16 ms.
  - **Cited:** Windows and Linux.
  - **The runner-up:** Node.js (one language; its `node:sqlite` is a release candidate, and its serial port needs a native add-on).
  - **Declined:** Go, a desktop shell, Web Serial in the page alone, and the original's stack.
- **The event schema:** `tag`, `reader` (a Reader or a listening Field Generator), `via`, `rssi_dbm`, `time` (UTC, from the Reader's frame clock), `seq` and a `payload` with the Field Generators heard. On the wire it is a binary frame, INFERRED from the UART's rate at 1,363 replies a second. The API is HTTP and JSON with a live stream.
- **Chorus, software side:** the five items ADOPTED (share links for the LAN tier, with a vendor's relay declined), both not-to-copy items DECLINED, and the four layers' schema, adopted by Story 2.1.3, written. Owned infrastructure with no subscription, and pairing people with things, still win.
- **D5's software side:** no HF interface on milestone 1's seal (recommended). Web NFC is in Chrome on Android only, and the printed QR code and the radio's tamper alarm already cover the need. The sitting decides it with the seal's design.

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

#### Story 2.1.10: The Sprint 2 review's fixes to the three studies

**Story:** As the decision sitting, I want the three technical studies to
carry the Sprint 2 review's corrections, so that I decide on figures that
hold and on the HF ruling Omid gave.
**Context:** born 2026-10-28 by the Sprint 3 proposal, from the Sprint 2
review's findings 1–5 and its five nits (tigerfid #41; the archive's
§ Executed sprints › Sprint 2, its `**Reviewed:**` record). The review
routed the studies' half of each here, ahead of Story 2.1.6. Each item says
what the review MEASURED and what changes.
- **Finding 1, `docs/study/software-stack.md`.**
  - § Decided ahead of the sitting and its table row say "no space reserved"
    for an HF interface on the seal's core. Omid's ruling in the review
    replaces it: the core keeps an unfitted footprint for an NFC memory, its
    I²C link and two antenna pads at the board's edge, and the coil lives in
    the enclosure (Story 2.1.7's Context).
  - § The seal's HF interface says "A web page reads NFC in one browser
    only". MDN's data lists Web NFC in Chrome, Opera, Samsung Internet and
    WebView on Android: still Android only, so milestone 1's no-HF decision
    stands.
  - The two "fitting option" lines are right as they stand.
- **Finding 2, `docs/study/radio-and-location.md`.** It names FCC ID
  ZAT-1312PSIP-2 alone. Its own pinned source points to ZAT-1312PSIP-3
  (SWRA797 p14) and lists the -3 filings (p17). The study gains a pointer to
  both IDs and to the browser read that Story 2.1.7 gives the sitting.
- **Finding 3, `docs/study/parts-and-toolchains.md`.** The TMP117's ±0.1 °C
  is specified at "8 averages, 1-Hz conversion cycle" (TMP117 p6), and its
  factory setting averages eight conversions, 124 ms (p12). The study charges
  one 15.5 ms conversion. At eight averages the review found [18]'s year
  holding by 1.8 %, not 6 %, and [27]'s cold-chain item tag at a 12 s listen
  by 1.4 %, not 5 %. The study either charges the setting that its cited
  accuracy needs, or names the setting it charges and the accuracy that
  setting has.
- **Finding 4, `radio-and-location.md` § Zones and
  `parts-and-toolchains.md` § The battery.** A tag listens only in "the slots
  of the Field Generators it last heard", and no sentence says how it hears
  one it has not heard before. INFERRED by the review: a tag carried into
  such a room never listens in its slot, and both the 3.0 s move and [18]'s
  battery assume it does. The study chooses the mechanism (Sprint 3's D3),
  re-checks the move against [13], and re-prices the listening budget in
  § Each worn tag's year and § [27] (Sprint 3's D2).
- **Finding 5, two figures with no source or mark** (Sprint 2's D10):
  - `parts-and-toolchains.md` § The toolchains: CMake and GNU make carry no
    platform, install or licence entry; TI Arm Clang ("TI's free compiler")
    and SysConfig carry "no price stated on the page". The sentence that
    every tool's platforms are cited except the Arm GNU Toolchain's is then
    untrue. The closer's `free-tools-three-platforms` needs this.
  - `software-stack.md`'s wire-frame table measures the JSON form "against
    921,600 baud", a figure with no source and no ASSUMED mark. The module's
    UART runs at 3 MBaud (PSIP p26), and the USB bridge's limit is unread.
    The study sources or marks the figure and says whether the binary frame
    still wins (Sprint 3's D4).
- **The five nits,** each MEASURED by the review:
  - `software-stack.md` says the original's event "carries no time". Its
    `rfTagEvent_t` has a `logTimestamp` member (`AWI_API.h` at the tag),
    which Smart Tracker's source never reads. What the host lacked was a
    time of hearing.
  - `parts-and-toolchains.md` quotes §2.803(d)(2)(iv)'s notice as ending at
    "FCC license holder". It goes on: "or must secure an experimental
    authorization under part 5 of this chapter." The conclusion stands.
  - `radio-and-location.md` calls a 500 kbit/s link "no worse by the
    tables" at −95 dBm, from the CC1312R7's sheet. The chosen module's own
    sheet prints −90 dBm under the same conditions (PSIP p16). The verdict
    stands: it is declined for its certification.
  - `parts-and-toolchains.md` says the radio study "counted only the sending
    antenna". That study's table counted the Reader's +2 dBi too; the 21.8 dB
    → 20.7 dB is that antenna's change to +0.90 dBi.
  - `software-stack.md` twice calls 1,363 a second "[3]'s". [3] is 1,000
    tags a second; 1,363 is the radio study's slot capacity.
- **Predicates, not examples** (KB `plan-authoring-for-cold-execution` item
  10, its review face). Each grep's hits are quoted in the PR body, each
  with its disposition:

  ```
  grep -n -i -E 'space reserved|reserves no space|one browser' docs/study/*.md docs/project-plan.md
  grep -n 'ZAT-1312PSIP' docs/study/*.md
  grep -n -i -E '15\.5 ms|averag' docs/study/*.md
  grep -n -E '921|222 %' docs/study/*.md
  grep -n -i -E 'CMake|GNU make|no price stated|except the Arm' docs/study/*.md
  grep -n -E "carries no time|\[3\]'s 1,363|no worse by the tables|counted only the sending|FCC license holder" docs/study/*.md
  ```

  And an old-value grep: each figure the re-pricing changes, grepped by its
  old value across `docs/study/*.md` and `docs/project-plan.md`. Each hit is
  refreshed, named current, or named frozen in a DONE story's Outcome.
- **What stays.** No requirement number moves (Sprint 3's D1). A figure that
  a dated pointer line in `requirements.md` quotes, and that this story
  changes, gets a new dated pointer line. The three studies' choices and
  Chorus verdicts stand unless a fix moves one, and the document then says
  so. The Outcomes of Stories 2.1.3–2.1.5 are records and are not edited;
  this story's tick names the figures it supersedes.
**Done when:** each of findings 1–5 and the five nits is corrected in its
document, or its sentence is shown correct with its source; finding 4's
mechanism is stated, with its listening cost carried into every battery
figure it moves; and the PR body quotes the story's greps with each hit's
disposition (manifest tooth `fcc-ids-both-named` and prose check
`studies-review-fixes`).

#### Story 2.1.6: The tiered cost of milestone 1

**Story:** As a beginner, I want the first build priced in starter, advanced
and pro tiers, so that I can choose what to spend before I start.
**Context:** spec §3.6, D7 and D9. Priced per device (the Reader, the Field
Generator, the three tags); per build route (hand-solder, partly assembled,
fully assembled); per tool tier (starter, advanced, pro — several ways to
the same end, for example several soldering irons); and the bench D9 needs
(a tool tier plus a cheap radio receiver to see what the boards transmit).
Every price is dated. This is the rebuild's first real budget. From the
software study (Story 2.1.5, 2026-10-28): the host software costs nothing
(Python, pyserial, SQLite and the browsers), and a USB barcode scanner for
scan-and-scan pairing is an optional bench item to price
(`docs/study/software-stack.md` § What the choice leaves to later stories).
From the radio and parts studies (the Sprint 2 review, 2026-10-28): the
build routes price the module's 0.5 mm-pitch land grid, which favours an
assembled board, and one board in larger quantity for the assembly service;
the advanced tool tier carries the vector network analyser
(`docs/study/radio-and-location.md` and `docs/study/parts-and-toolchains.md`,
each § What the choice leaves to later stories). A price page that refuses a
plain request, as the FCC's, FTDI's and Microchip's pages did for the study
(friction F8 and F13 of Sprint 2), is put to Omid early in the leg for a
browser read, or written as not read. From the Sprint 3 proposal
(2026-10-28), the block's D5–D8 bind the prices: US dollars from sellers'
public pages, each cited with its URL and read date; both cells wherever the
sitting chooses between them; a part the studies leave unnamed priced as a
representative part, marked so; the refused pages put to Omid in one
message; and the US build priced, with the parts that differ for the EU
named.
**Done when:** `docs/study/tiered-cost.md` carries the tables with a date on
every price and a total per tier.

#### Story 2.1.11: The reference check names the fetch route for the study's sources

**Story:** As a builder setting up the reference folder on a new machine, I
want the reference check to say that a missing `sources/` file may be
fetched from its row's URL, so that I follow the corpus's own rule instead of
looking for a machine to copy it from.
**Context:** born 2026-10-28 by the Sprint 3 proposal, from Sprint 2's
friction F11 (leg 2) and F18 (leg 4), which the review routed here as a
story of its own (the archive's Sprint 2 record). MEASURED on `main`
(`9cb0127`):
- **What the check prints.** `scripts/reference-check.mjs` prints "(copy-only
  — copy it, never move it, from a machine whose folder verifies)" for every
  missing file row, and "copy-only: copy the folder whole, never move it, …"
  for an absent folder. Its header comment says "File rows are copy-only:
  the tool never fetches one".
- **What the corpus's rule allows** for `sources/`:
  - the header comment of `docs/reference-corpus.tsv`: "copy-only (never
    re-fetched), except sources/, which may be fetched again from its url and
    counts only once its bytes match the pin";
  - `docs/reference-repos.md` § `sources/`: "On another machine: fetch each
    file from its row's URL, or copy it from a machine whose folder
    verifies. A fetched file counts only once its sha256 matches".
- **The fix is the messages, not a fetcher** (Sprint 3's D11). The tool
  still never fetches a file row (`docs/reference-repos.md`: "The tool never
  fetches a file row").
- **The tests,** in `scripts/reference-check.test.mjs`, written first and
  seen red against `main`'s script:
  - a missing `sources/` file names its row's URL, the sha256 rule and the
    copy route;
  - an absent `sources/` folder names the fetch route for its files;
  - a missing file in another folder keeps the copy-only line.
- `docs/ci-contract.md` needs no change: `test` already runs `node --test
  scripts/reference-check.test.mjs`.
**Done when:** the check's output for a missing `sources/` file and for an
absent `sources/` folder names the fetch route, with the sha256 rule and the
copy route as the alternative; every other file row's line is unchanged; and
the tests above pass in `npm test` after failing before the fix, the PR body
quoting the red run (manifest prose check `reference-check-sources-route`).

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
archive's Story 1.1.2 Context). From the software study (Story 2.1.5,
2026-10-28): its three items for the sitting were **decided by Omid ahead
of it**, first-hand in the terminal of the session "tigerfid Sprint 2 leg 4",
between its `date -u` reads of 00:39:20Z and 00:42:52Z: "I like all those
recommendations, let's go with those."
- **The host is Python.** Node.js, the runner-up, is declined.
- **Milestone 1's seal has no HF interface** for a phone (D5). Its core
  reserves an unfitted footprint for one, with the coil off the core, by
  Omid's later word below; #39 had recorded "the core reserves no space for
  one".
- **Raw check-ins are kept 7 days by default.** The history (zone moves,
  alarms and their acknowledgements, sensor readings) is kept until the owner
  deletes it, and the setting sits on screen beside the disk use.

The sitting carries the three into the rebuild spec. The study records them
with their basis (`docs/study/software-stack.md` § Decided ahead of the
sitting).

From the Sprint 2 review (2026-10-28):
- **The seal's HF footprint, decided by Omid** first-hand in the terminal of
  the session "tigerfid Sprint 2 review", between its `date -u` reads of
  10:01:36Z and 10:27:02Z: "Reserve footprint, coil off-core
  (Recommended)". The core keeps an unfitted footprint for an NFC memory, its
  I²C link and two antenna pads at the board's edge. The coil lives in the
  enclosure, away from the module's antenna and its ground plane (SWRA797
  §3.1.2). Milestone 1 fits none of it. The study's Decided line still reads
  "no space reserved" until Story 2.1.10 corrects it.
- **The FCC grant read is the sitting's,** by Omid's hand in a browser: the
  grant pages of both FCC IDs in TI's documents, ZAT-1312PSIP-2 and -3,
  before the spec names an FCC ID or any part is bought
  (`docs/study/parts-and-toolchains.md` § What was read, and what could not
  be).
- **The parts study's open items are the sitting's too:** the item tag's
  cell, the nursery tag's cell if the measurement falls short, and the
  sensor's accuracy (`docs/study/parts-and-toolchains.md` § What the choice
  leaves to later stories).
- **The spec carries each study's build-initiative items** from its § What
  the choice leaves to later stories, so that the close (Story 2.1.8) files
  them.
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
