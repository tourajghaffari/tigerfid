# Project plan — tigerfid

TigeRFID: the ActiveWave active-RFID system — boards, firmware and software
built from 2001 to 2013 — that Touraj Ghaffari's sons open-sourced under
Apache-2.0 in 2017–2018 (the dedication in `readme.md`), and its rebuild
from the ground up. The 2018 state is kept byte for byte at the annotated
tag `activewave-2018` (`f25ff13f3fb672d8ec91f1fadc8894d69f781ed6`) and its
GitHub release; nothing in the history is rewritten. Initiative 1 joins the
repo to the fleet and clears the current tree for the rebuild; Initiative 2
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

One open row.

**A1 — The Outlook message in the 2018 tree: read it before it leaves the
tree, or keep the default?** `software/smart-tracker/SmartTracker  SaniFaucet.msg`
(12 MB, an Outlook `.msg` file) was not read at adoption; it may hold
correspondence. **Default:** it leaves the current tree with the rest of the
2018 files in Story 1.1.2 and stays, unchanged, at the tag `activewave-2018`
— the history is never rewritten (spec D6), so the restructure changes the
current tree only. **Asked:** read it first and decide, or keep the default.
**Gates:** nothing — Story 1.1.2 proceeds on the default unless Omid rules
otherwise first. Filed 2026-10-26 by the onboarding PR (spec §2, §7).

## Sprint 1 — "Adopted: the original kept, the study begun" — ✅ APPROVED (2026-10-26, at adoption — the spec is the approval record)

**Born:** 2026-10-26, at adoption (the onboarding PR's merge `<merge sha>`;
hub spec `2026-10-26-tigerfid-onboarding-design`, hub PR #4355).
**Approval:** ✅ APPROVED — Omid, 2026-10-26 (his approval of the spec,
which records this sprint in §7; KB `new-repo-standard` item 5).

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

## Initiative 1: Join the fleet; keep the original

**Objective:** tigerfid works inside the fleet's checks — its plan, close
protocol, CI and session handoff — with the 2018 state kept byte for byte at
the tag `activewave-2018` and its release, the current tree cleared for the
rebuild with the dedication verbatim, the reference corpus adopted on both
machines, and the licence lane in place before any third-party copy.
**Done when:** Epic 1.1 is archived with close evidence (initiative-final —
Epic 1.1 is its only epic).

### Epic 1.1: Onboarding, the restructure, the reference corpus and the licence lane

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

#### Story 1.1.2: The archive restructure — the 2018 tree leaves the current tree, the dedication stays

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
**Done when:** the 1.1 teeth `archive-left-the-tree`, `dedication-verbatim`,
`readme-points-at-the-original` and `contributing-coc-replaced` PASS, and the
PR records Omid's read of the new wording and his code-of-conduct choice.

#### Story 1.1.3: The reference corpus adopted, with the early capture of the old website

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
lane lands before any copy. Spec C11: this leg runs first, so the
activewaveinc.com pages, photos and datasheets are captured before the
sibling site's seat changes the live site; the Wayback copies are the
fallback. The Chorus notes, images and sources gathered for spec D12 are
adopted into `docs/reference-research.md`. The recreate-and-verify tool
follows tigerian's and tigerdash's shape (`scripts/reference-check.mjs`
behind `npm run setup`) — the builder's call.
**Done when:** `docs/reference-repos.md` (reading rules, licence audit,
pins with full commit ids or sha256s, the recreate recipe),
`docs/reference-corpus.tsv` (the pins, once) and
`docs/reference-research.md` (with its Chorus entry) are committed; the
folder is recreated and verified on the first machine, the verify run
quoted in the tick; the capture's file list with sha256s is in the tick,
dated before the site's first change.

#### Story 1.1.4: The licence lane — before any copy

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
**Done when:** the 1.1 teeth `notices-file`, `licence-gate-passes`,
`licence-gate-in-test` and `allowlist-widened` PASS.

#### Story 1.1.5: The second machine — its clone and its reference folder

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

#### Story 1.1.6: Close out Epic 1.1

**Story:** As the fresh-session closer, I want Epic 1.1 closed per KB
`epic-close-protocol`, so that the archive carries the evidence.
**Context:** `node ../../scripts/close/close-epic.mjs --epic 1.1` from the
repo root, in a session that did none of the epic's work. Initiative-final:
the archival PR cuts Initiative 1 whole.
**Done when:** every manifest check passes or is adjudicated, the evidence
block is written into the archive entry, and Initiative 1 is cut to the
archive in the same PR.

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
**Ordering:** 2.1.1 → 2.1.2 (Sprint 1 legs 4–5) → 2.1.3 and 2.1.4 → 2.1.5 →
2.1.6 → 2.1.7 → 2.1.8. Manifest: `docs/close/2.1.json` (born FAILING — every
deliverable is still to be written).
**Done when:** every tooth in `docs/close/2.1.json` passes from the repo root
and its prose checks are adjudicated.

#### Story 2.1.1: The requirements, from the three scenarios, with numbers

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

#### Story 2.1.2: How the original worked

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
its wake-on-command still win.
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
decision.
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
subscription and pairing people with things still win.
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
and milestone 1 (Initiative 2) as its acceptance target.
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
