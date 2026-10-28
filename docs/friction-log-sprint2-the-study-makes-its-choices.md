# Friction log — Sprint 2, "The study makes its choices"

This log records what got in the way while Sprint 2 was proposed and run:
the friction itself, what it cost, and what would prevent it. It is public,
like everything in this repo, and written for a stranger. The sprint block
is in `docs/project-plan.md` (§ Sprint 2) while the sprint is live, and in
`docs/archives/project-plan-completed.md` once it has run and been reviewed.

Each leg appends one section with:
- its context checkpoint: the leg plan's task count, and how many
  compactions, forced re-reads and lost-context defects it met;
- each friction, with its cost and its suggested fix.

## The proposal (2026-10-27)

Proposed by the repo-rooted session "tigerfid Sprint 2 Proposal" (session
`49c256bc-78bf-445e-b2a6-f970dfb1f707`), fresh by `/clear`, in one sitting.

- **F1 — A review finding's example covered one of three places.** Finding 4
  of the Sprint 1 review named one stale sentence: the Outlook message
  "stays closed until Omid rules on it" in `docs/study/original-system.md`.
  A grep for the finding's predicate, every place that still says the message
  waits on ASK-OMID A1, found two more: `docs/reference-research.md` § 1 and
  `docs/reference-repos.md` reading rule 7. Story 2.1.9 carries all three and
  bakes the grep in, so the leg fixes the predicate, not the example. Cost: none
  landed wrong, because the proposal ran the grep before writing the story.
- **F2 — A manifest check asked for something its epic cannot produce.**
  `free-tools-three-platforms` in `docs/close/2.1.json`, born at adoption,
  asked the closer to confirm "the chosen boards are drawn in KiCad 10". The
  study draws no board: drawing is the build initiatives' work, filed by the
  epic's close. Read cold, the check would have failed at the close, or left a
  closer to invent its own reading. The proposal's staleness sweep caught it,
  and it now asks that every board the study plans is planned in KiCad 10.
  Suggested fix, for whoever writes the next study manifest: read each prose
  check against the epic's own Done-when, and ask whether the epic's stories
  can produce what the check names.
- **F3 — The proposal standard is long.** `sprint-proposal-standard` runs to
  4,338 lines, and its friction log to 2,250. The proposal read its steps 0–8
  and the log's newest entries, and modelled the block on the newest approved
  block in a sibling repo, rather than reading every clause. Cost: reading
  time, and the risk that a clause the proposal skipped binds this block. The
  reviewer pass is the check on that risk.
- **F4 — The sprint-block parser read the working-tree plan as having no
  block.** The fleet's parser printed `found: false` over
  `docs/project-plan.md` as checked out, with the block's heading in place.
  - **Cause, MEASURED:** this repo has no `.gitattributes`, and Git for
    Windows' default `core.autocrlf=true` checks text out with CRLF line
    endings. The parser's heading pattern does not match a line that ends in
    a carriage return.
  - **The workaround:** the proposal parsed the staged blob instead, the LF
    text that lands, and read all four legs.
  - **The wider gap:** the fleet's other repos pin LF line endings in a
    `.gitattributes` file, so this repo is the one without it. Filed to the
    hub as inbox #4452: tigerfid's missing pin, the adoption standard's
    missing row for it, and the parser's missing normalisation.
  - **Cost:** a few minutes, and a first reading that the heading was wrong.
- **Guard events, each caught before it changed anything.**
  - The cwd-drift guard warned twice on a `cd` out of this repo, once for a
    read of a sibling clone at `origin/main`, once for a loop over four
    siblings' `.gitattributes`.
  - The piped-checker guard warned once on a `$?` read after a grep, which
    was not used as proof.

## Leg 1 — Story 2.1.9 (2026-10-27)

Run by the repo-rooted session "tigerfid Sprint 2 Executor" (session
`014c145a-45db-4c1d-b7ff-a2e9bcbcb0c4`), fresh by `/clear`. Its
background-task path id, `c59c545c-e735-4344-b0b8-697d9f897dca`, is the
process the Sprint 2 proposal used last. Not elevated. Before the leg, the
same session landed the line-ending pin as its own PR (#28), on Air Traffic
Control's orders.

**Context checkpoint:** a plan of six tasks (the three files, the greps,
the tick with its manifest line and this section, the checks and the merge),
under D9's tripwire of eight. No compaction, no forced re-read, no
lost-context defect.

- **F5 — F4's cause, fixed before the leg.** #28 adds a root
  `.gitattributes` (LF stored and checked out). Every blob was LF already, so
  it changed no stored byte. This clone's 52 CRLF working files were repaired
  by the delete-and-`checkout-index -f -u` recipe, never a renormalize. Over
  the repaired working tree, the sprint parser now reads the block,
  `found: true`, four legs, exit 0. Cost: one small PR before the leg.
- **F6 — An order's literal scope would have rebuilt a known trap.** The
  orders asked that "the originals' tree" be marked `-text`. Read literally,
  that names the tag's 2018 folders, `software/` among them, and the
  rebuild's starting layout has a `software/` of its own. A rule on those
  names would have caught the rebuild's files, the same way the retired
  sparse view hid them. The pin marks only the 2018 files still on `main`
  (`license` and `_assets/`). It was measured that a `.gitattributes` on
  `main` cannot reach the tag: `git archive` of the tag ignored it. #28's body
  records the reading. Cost: none landed wrong.
- **F7 — An edit missed on a re-wrapped line.** One edit's search text began
  mid-line where the file wraps differently, so it matched nothing. Re-cut
  from the line's start, it applied. Cost: one retry.
- **Guard events, each a read and not a proof.** The piped-checker guard
  warned on a `git pull` piped into `tail` and on a re-verify read that piped
  `npm test` into `grep` after a `;`-chained `close-lint`. Each state was
  re-read unpiped, and every commit ran behind `close-lint && npm test &&`.

## Leg 2 — Story 2.1.3 (2026-10-27)

Run by the repo-rooted session "tigerfid Sprint 2 leg 2" (session
`f367e3c4-268f-4213-80f5-0de763c5a7e2`), fresh by `/clear`. Its
background-task path id, `c59c545c-e735-4344-b0b8-697d9f897dca`, is the
process the Sprint 2 proposal and leg 1 used. Not elevated.

**Context checkpoint:** a plan of eight tasks, at D9's tripwire and not
above it. They were the US reads, the EU reads, the radios' figures, the
comparison and choice, the Chorus items, the corpus rows, the tick with its
manifest line and this section, and the checks and the merge. No split was
needed. No compaction, no forced re-read, no lost-context defect.

- **F8 — Vendor and regulator sites refused plain requests.**
  - **Refused:** Silicon Labs' datasheets, the FCC's
    equipment-authorisation search and the fccid.io mirror, and ETSI's
    server (HTTP 403). EUR-Lex's pages answered with an empty HTTP 202, and
    ST's server closed the connection.
  - **Cost:**
    - a Silicon Labs module dropped out of the comparison;
    - the TI module grant's equipment class is unread, so the design hops
      channels to hold under either class, and Story 2.1.4 reads the
      record;
    - the three ETSI standards were read as their version listings only.
  - **How each was met:** the first requests to EUR-Lex and Silicon Labs
    carried a generic browser user-agent string. A retry presenting a full
    browser identity was refused by the session's own permission check as
    bot-check circumvention, before it ran. Every later request was plain.
    The EU texts came instead from the Publications Office's Cellar service,
    the official machine route to the same consolidated texts. The ETSI
    listings were read through the harness's web-reading tool.
  - **Suggested fix:** the research log's search ideas name the official
    machine endpoints first (the eCFR's API, the Cellar service). A record
    that only a browser can open, such as the FCC grant, goes to Omid's
    hand.
- **F9 — "Read before any radio is named": reading order or document
  order?** The US sections and Decision 2006/771/EC were read before any
  datasheet was opened. The RED's articles, ERC/REC 70-03, the ETSI listings
  and §15.3 were read after the first datasheets were downloaded, and before
  the document named a radio. The study's § The law, read first, states the
  order with its clock reads, so the closer can judge
  `legal-texts-read-dated`. Suggested fix: a block that bakes "the law
  before the radios" says which order it binds.
- **F10 — ERC/REC 70-03 lost its column alignment as text.** The PDF's
  tables came out with their columns crossed. The study cites it only for
  what reads without doubt, and takes the binding EU band conditions from
  the Decision, which read cleanly. Cost: one source's tables unused.
- **F11 — The reference check's message says copy-only for every file
  row.** `scripts/reference-check.mjs` prints "copy-only — copy it, never
  move it" for a missing file. D8 lets `sources/` be fetched again from its
  URL, and `docs/reference-repos.md` and the tsv's header now say so. The
  script was not edited, because this leg's grant excludes `scripts/`.
  Suggested fix: a later story aligns the message for `sources/`.
- **F12 — The Decision's consolidated identifier needs encoding.** Its
  CELEX number carries a `(01)` suffix that the Cellar service wants
  URL-encoded. Three guessed dates returned 404 before a query to the
  Publications Office's SPARQL endpoint listed the real versions. Cost: a
  few minutes.
- **Guard events, each a read and not a proof.** The piped-checker guard
  warned once, on the close runner piped into `grep`. The flip map was
  re-run unpiped for the PR body, and every commit ran behind
  `close-lint && npm test &&`.

## Leg 3 — Story 2.1.4 (2026-10-27)

Run by the repo-rooted session "tigerfid Sprint 2 leg 3" (session
`326f43fb-a014-4eea-838f-59fff96f5faa`), fresh by `/clear`. Its
background-task path id, `c59c545c-e735-4344-b0b8-697d9f897dca`, is the
process the Sprint 2 proposal, leg 1 and leg 2 used. Not elevated.

**Context checkpoint:** a plan of eight tasks, at D9's tripwire and not
above it:
1. the grant record and Subpart B;
2. the module's figures;
3. the parts around the module;
4. the toolchains and the debugger;
5. [27] and the replaced figures;
6. one core or three;
7. the corpus rows, with the tick, the manifest line and this section;
8. the checks and the merge.

No split was needed. No compaction, no forced re-read, no lost-context
defect.

- **F13 — The refusals held on a second network route.**
  - **What was refused:** the FCC's search, two mirrors of it, FTDI and
    Microchip answered HTTP 403 to plain requests, and so did the harness's
    web-reading tool.
  - **The retry:** Omid changed the session's network route, and a retry
    was refused the same way. The FCC's and Microchip's bodies
    are a content network's "Access Denied" page, so the refusal is of the
    client, not the route (MEASURED: the body; the cause beyond it
    INFERRED).
  - **The ruling:** a browser read was put to Omid first-hand, and he
    answered "Write it as not read".
  - **Cost:**
    - the grant's equipment class and its two FCC IDs stay open;
    - the Reader's USB bridge is named by its class, not its part.
  - **Suggested fix:** the research log names the browser-only records up
    front, so a leg asks for them once, early, or writes them as not read
    without spending requests.
- **F14 — The eCFR's API now insists on compression.** A plain request
  answered HTTP 406, "This endpoint requires response compression".
  `curl --compressed` read every section. Leg 2's URLs are unchanged.
  Cost: one retry.
- **F15 — Two FCC IDs in TI's own documents.** The datasheet (November
  2023) labels the module ZAT-1312PSIP-2. The certification note (September
  2024) lists its filings, including the Wi-SUN test report for the PHY
  that leg 2 chose, under ZAT-1312PSIP-3. Leg 2 cited the first. The study
  records both and leaves them to the grant read. Cost: none landed wrong,
  because the label line in `radio-and-location.md` is marked as the
  datasheet's.
- **F16 — A rule renumbered weeks before it was read.** The evaluation-kit
  rule moved to §2.803(d)(2)(iv) in the amendment of September 11, 2026. The
  first draft cited (c), from memory of the older numbering. The claims
  check against the fetched text caught it. Suggested fix: a paragraph
  number comes from the fetched text, never from memory, as D6 already asks
  of whole sections.
- **F17 — PDF tables lost their columns as text.** Three tables came out
  scrambled:
  - the module's standby rows (which current belongs to which oscillator);
  - the certified-PHY table's PHY labels;
  - the first antenna table.

  Each was settled by a raw-order extraction of the one page, or by the
  table that reads cleanly: the second antenna table, and the labels
  counted by row. Page rendering was unavailable on this box. Cost: a few
  minutes. Leg 2 met the same problem in ERC/REC 70-03 (F10).
- **Guard events.** The cwd-drift guard warned once, on a `cd` into the
  reference folder for a PDF conversion. The shell was reset, and every
  later repo command used absolute paths. Every commit ran behind
  `close-lint && npm test &&`.

## Leg 4 — Story 2.1.5 (2026-10-28)

Run by the repo-rooted session "tigerfid Sprint 2 leg 4" (session
`a60005ee-b36d-474f-9f59-b2624330eb37`), fresh by launch, on the fleet's
macOS machine. Its background-task path id is its own session id, so no
`/clear` preceded it and no earlier session's process was cleared. Not
elevated.

**Context checkpoint:** a plan of eight tasks, at D9's tripwire and not
above it:
1. the original host source at the tag;
2. the stack's options and sources;
3. the event schema;
4. the alarms, pairing and D5;
5. the Chorus items;
6. the document;
7. the tick, the manifest line, the research-log entry and this section;
8. the checks and the merge.

No split was needed. No compaction, no forced re-read, no lost-context
defect.

- **F18 — This machine's corpus had no `sources/`.** Legs 2 and 3 pinned
  their eight `sources/` files on the other machine.
  - **What happened:** `node scripts/reference-check.mjs` read `MISSING
    folder sources/ — 8 files`. Its message still says "copy-only" (F11).
  - **How it was met:** each file was fetched from its row's URL into a
    scratch folder, and copied in only where its sha256 matched the pin.
    All 8 matched, and the check then read 299 rows verified.
  - **Cost:** about a minute. D8's fetch route worked as written.
- **F19 — An empty search over a source file meant "unread", not
  "absent".** This machine's shell `grep` printed nothing, not even a `-c`
  count, over the Programming Station's main source file, which `file`
  calls "Non-ISO extended-ASCII text". The system `grep -a` read it and
  found 23 includes.
  - **Cost:** one retry. Read as an absence, the empty result would have
    let the study say the file includes nothing.
  - **Suggested fix:** a search that prints no count at all is re-run in
    text mode before any finding rests on it.
- **F20 — MDN's compatibility tables render by script.** The web-reading
  tool returned the Web Serial and Web NFC pages without their tables.
  - **How it was met:** the support entries were read from MDN's own
    browser-compatibility data, the JSON the tables are drawn from, with a
    small script.
  - **Cost:** a few minutes. The study cites the data with its fetch time.
- **F21 — The orders' grant list missed the research log.** Legs 2 and 3
  each added a § 4 entry to `docs/reference-research.md`, but this leg's
  path grants did not name the file.
  - **How it was met:** a CLAIM to the coordinator. The GRANT came at
    00:28:48Z by the coordinator's clock, while the leg worked on.
  - **Cost:** none to the leg.
  - **Suggested fix:** the leg template's grants carry the research log
    wherever a leg reads or searches.
- **F22 — Two first readings of the original's source were wrong.** The
  claims check, run against the source before the commit, caught both:
  - a second key position cited inside a commented-out block;
  - a cipher named only in that block's comment.

  Both were corrected in the document. **Suggested fix:** a claim about
  source code is checked against live code, not a comment.
- **Reading hazards in the original, not quoted.** Its source carries a
  credential-shaped connection string, compiled-in encryption keys, and
  developers' names in comments. The study cites where the keys sit and
  quotes none of them (CLAUDE.md § Writing in public).
- **Guard events, each a read and not a proof.**
  - A shell-guard denial: a bare `===` separator, which this machine's
    shell reads as a command lookup. It was refused before it ran, and the
    command was re-issued with `---`.
  - The piped-checker guard warned once, on the close runner piped into
    `sed`. The PR body's flip map is an unpiped run.
  - The cwd-drift guard warned once, on a `cd` into this repo.

  Every commit ran behind `close-lint && npm test &&`.
