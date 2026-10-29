# Friction log — Sprint 4, "Figures that hold for the sitting"

This log records what got in the way while Sprint 4 was proposed and run:
the friction itself, what it cost, and what would prevent it. It is public,
like everything in this repo, and written for a stranger. The sprint block
is in `docs/project-plan.md` (§ Sprint 4) while the sprint is live, and in
`docs/archives/project-plan-completed.md` once it has run and been reviewed.

Each leg appends one section with:
- its context checkpoint: the leg plan's task count, and how many
  compactions, forced re-reads and lost-context defects it met;
- each friction, with its cost and its suggested fix;
- each guard event, with the guard's own banner line quoted whole and its
  register class id beside the word "seat" (the block's § Friction log).

The reviewer pass's seat check names this sprint by the register's own
wording: `check-register-seats.mjs --sprint 'tigerfid — Sprint 4'` (Sprint
3's friction R4).

## The proposal (2026-10-29)

Proposed by the hub-rooted Windows session "tigerfid Sprint 4 proposal"
(session `cb189c7f-0ffb-4dea-bea2-8485d49ee215`), on the Mac coordinator's
orders 19, in one sitting. **The fresh-session read:** fresh by `/clear`.
The background-task path id is `13efb2b1-ddbf-4cd6-824f-74b58f5302ce`, the
session id of "Windows 4", Sprint 3's leg 3 executor; that process last held
the Sprint 3 reviewer pass (`f050a5d6-…`, wrapped at #54), which this
`/clear` ended. Not elevated: a Windows administrator-role read, `False`, at
00:42:54Z.

**Context checkpoint:** no compaction, no forced re-read, no lost-context
defect.

**The approval:** Omid, first-hand in this terminal between its `date -u`
reads of 00:50:57Z and 06:25:22Z: "Approve as proposed (Recommended)". The
question laid out the one call a reader might change, the split of Story
2.1.12, with its cost, and D2's clause for a baby's band.

- **F1 — The hub checkout's sprint parser is not origin/main's.** The close
  runner printed "hub tooling differs from origin/main:
  scripts/fleet-view/check-sprint-block.mjs,
  scripts/fleet-view/sprint-blocks.mjs,
  scripts/fleet/check-item8-dispositions.mjs, scripts/fleet/watch-pr.mjs".
  The hub checkout is shared by other live seats, so it is read at
  origin/main and never pulled. The proposal extracted origin/main's
  `scripts/fleet-view/` and `scripts/lib/` (`702c116c`) into its scratchpad
  with `git archive` and ran the parser from there, leaving the checkout
  untouched. The close runner's teeth are this repo's own run lines, so its
  PASS and FAIL readings do not depend on that difference. Cost: one
  extraction. Suggested fix, hub-owned: a parser command that runs
  origin/main's copy by itself.
- **F2 — A test-arming read still needs a script** (Sprint 3's F2, still
  open). `scripts/ci-docs-only.mjs` exports `classify()` and
  `repoOptions()`, and no command classifies a change set that does not
  exist yet. The proposal wrote a scratch script again. Cost: one short
  script. Suggested fix, hub-owned (the script is a synced canonical): a
  `--paths <p,…>` flag.
- **F3 — A needle split by a line break, caught.** The anchor grep for
  finding 6, `grep -n 'except the Arm'`, read 0 on
  `docs/study/parts-and-toolchains.md`, because the sentence wraps after
  "for every tool except two." The anchor was read whole at l.242–246
  before the block cited it. The block's merge-loop line carries Sprint 3's
  F7 rule (the shortest needle a line break cannot split).
  `wrapped-sentence-needle-undercount` seat, caught.
- **F4 — `main` moved while the approval waited.** The question went out
  after the read of 00:50:57Z, and the answer came before the read of
  06:25:22Z. In between, `main` took a fleet sync round (#56: the
  `block-secrets`, `close-lint` and `check-tick-merge-claim` canonicals). The
  branch took `main` by a merge, not a rebase, so its push stays a fast
  forward, and the sign-off commit re-ran the synced `close-lint`. Cost: one
  merge. No fix needed: the squash merge flattens it.

**Guard events:**
- `cwd-drift` seat: the calls that opened with a `cd` into this repo drew
  "cwd-drift guard (warn, not a block)", and the calls after them drew its
  second arm. Every git command carried `git -C` with the literal path; none
  ran in the wrong repository.
- `truncated-read-taken-as-source-extent` seat, caught: the close runner's
  verdict lines on this PR's tree were first printed through `cut -c1-90`,
  with their count (5) beside them. The output file was read again whole
  before the block recorded the verdicts, and they matched.
- The marker hook, class id not known: the sign-off's first edit, after the
  read of 06:25:22Z, drew "Widen the marker so concurrent sessions can see
  this work. Run from <hub>: node scripts/fleet-view.mjs --declare tigerfid"
  (the hub's path written here as `<hub>`), on a marker opened with
  `--repos tigerfid`, the face Sprint 3's reviewer pass recorded on its legs
  2 and 3. The command ran and printed "marker declared: … repos: tigerfid"
  before the read of 06:26:05Z. The proposal's earlier edits, between the
  reads of 00:42:54Z and 00:48:58Z, drew no such line.
- `semicolon-chained-checker-does-not-gate-commit` seat, the guard's
  refusal: the sign-off's first commit command ran `close-lint` and `npm
  test` in an `&&` chain to `git commit`, then an `echo` after a `;`, then
  `git push`. The guard refused it before it ran: "piped-checker guard
  (BLOCKED — this command did not run)". It was re-issued as one `&&` chain
  to the push, with the exit read after it.

## Leg 1 — Story 2.1.12 (2026-10-29)

Run by the hub-rooted Windows session "tigerfid Sprint 4 leg 1" (session
`6fb033a2-2fc2-4f7a-8d61-8918850b976e`), on the Mac coordinator's orders 24.
**The fresh-session read:** fresh by `/clear`. The background-task path id
is `13efb2b1-ddbf-4cd6-824f-74b58f5302ce`, the session id of "Windows 4",
Sprint 3's leg 3 executor. That process last held the Sprint 4 proposal
(`cb189c7f-…`, wrapped at #57, its tombstone on the hub's origin), which
this `/clear` ended. Not elevated: a Windows administrator-role read,
`False`, at 07:43:02Z.

**Context checkpoint:** seven tasks, as the block estimated. No compaction,
no forced re-read, no lost-context defect.

**Re-verify, before the first edit** (between the `date -u` reads of
07:31:22Z and 07:31:38Z): `main` at #57; `user.email` the noreply address;
`close-lint` ok and `npm test` 20 of 20; `reference-check` "299 rows
verified, README the stub"; the close runner over the eleven teeth, nine
PASS, with `stub-names-sources` and `rebuild-spec-approved` FAIL, as the
block says; no ASK-OMID row; each anchor in § Inputs swept read as quoted.

- **L1 — The block's order was a second assumption.** The review's finding
  named the beacon. The study also ASSUMED the host could order the block
  "so that a tag hears its set in one window". In a corridor, a stretch's
  set of seven (its four rooms and the stretch on each side) cannot be one
  run beside its neighbours' sets, and two of every four rooms sit one slot
  away from their stretch. The leg therefore states each scenario's order
  and prices runs: one wake for each run of a set, a single slot between
  listened across. Every figure now rests on an order the reviewer's script
  can rebuild. Cost: the order and one rule in the study. No fix needed.
- **L2 — A rounded slot length moves a frame's count.** The study writes a
  reply slot as 0.733 ms. It is 0.7333 ms (20 bytes at 300 kbit/s, 0.5333
  ms, and 0.2 ms of guard), and its "1,363 a second" floors 1,000 ÷ 0.7333.
  The conference's 327 reply slots a frame fit with nothing to spare at
  0.7333 ms. A reproduction at 0.733 ms gives the same counts, but only by
  luck. Cost: one re-run of the leg's script. Suggested fix, for the
  reviewer: reproduce slot counts at 11/15 ms.
- **L3 — The old [27] table sat 0.1 above this leg's script in four
  cells.** At the last digit, the script reads 257.24 for the 4 s row's
  257.3, 800.15 for 800.2, 148.75 for 148.8, and 1.055 for 1.05. Every
  other old cell reproduced, and every row is superseded by this leg. Cost:
  none. It is named so the reviewer does not chase it.
- **L4 — GNU's make page reset the connection again** (between the reads of
  07:34:07Z and 07:34:27Z, curl exit 35). D4's default holds: GNU make is
  named among the tools whose platforms are not cited.
- **L5 — `software-stack.md` quotes the raw slot capacity.** Its burst row
  reads "up to 1,363 a second, a hall of 1,000 in 0.73 s". It is outside
  this leg's ledger, and it stays true as the host's worst case: the
  beacon and the block only lower the rate a host must take. Dispositioned
  current in the PR body, not edited.
- **For leg 2:** `tiered-cost.md` § Both cells reads true as it stands. The
  item tag's cell is still the sitting's, now for margin rather than need,
  and the nursery tag's is still its cell "if the measurement falls short".
  No sentence of it moves.

**Guard events:**
- `cwd-drift` seat: the leg's first `cd` into this repo, in a call of its
  own after the read of 07:22:42Z, drew "cwd-drift guard (warn, not a
  block): this command cds into `<F>` — the shell cwd PERSISTS across later
  tool calls whether this compound succeeds or fails, so a later repo
  command can silently run in the wrong repository." (the repo's path
  written here as `<F>`), and every call after it drew the guard's second
  arm. Every git command carried `git -C` with the literal path, and none
  ran in the wrong repository.
- `swept-surface-guard-warns-on-the-legs-own-planned-edits` seat: every edit
  to the leg's four study and research files drew "This file was verified
  by Sprint 4's staleness sweep (swept 2026-10-29). Your edit may decay its
  pre-verified facts — re-verify or annotate the block (KB
  sprint-proposal-standard step 2)." Each was the leg's own planned edit,
  and its anchors were read again before the work (above).
- `truncated-read-taken-as-source-extent` seat, caught: the PR body's greps
  were first printed through `cut -c1-140`, each count beside its matches,
  after the read of 07:44:55Z. No guard drew it. They were run again with
  every match whole after the read of 07:45:31Z, and the PR body quotes that
  run.
- `piped-checker-exit` seat, flagged: the privacy read of the diff's added
  lines piped `git diff` into three `grep`s and read `$?`. The guard drew
  "piped-checker guard (warn, not a block): this command reads `$?` right
  after the pipeline". The last stage was the read meant (exit 1, no match),
  and the diff's own exit was not the proof.
- The marker hook's "Widen the marker" line, which met the proposal's seat
  on a marker opened with `--repos tigerfid`, did not fire on any of this
  leg's edits.

## Leg 2 — Story 2.1.14 (2026-10-29)

Run by the hub-rooted Windows session "tigerfid Sprint 4 leg 2" (session
`7a8f4e54-9463-4a25-b0d2-718feceb0236`), on the Mac coordinator's orders 28.
**The fresh-session read:** fresh by `/clear`. The background-task path id
is `13efb2b1-ddbf-4cd6-824f-74b58f5302ce`, not the session id: the process
of "Windows 4", which hosted the Sprint 4 proposal and leg 1 before this
leg. The orders name the cleared session as leg 1's (`6fb033a2-…`, wrapped
at #59, its tombstone on the hub's origin). Not elevated: a Windows
administrator-role read, `False`, at 10:17:13Z.

**Context checkpoint:** seven tasks, as the block estimated. No compaction,
no forced re-read, no lost-context defect.

**Re-verify, before the first edit** (between the `date -u` reads of
10:17:13Z and 10:17:35Z): `main` at #59 after #58; `user.email` the
noreply address; `close-lint` ok and `npm test` 20 of 20; `reference-check`
"299 rows verified, README the stub"; the close runner over the eleven
teeth, nine PASS, with `stub-names-sources` and `rebuild-spec-approved`
FAIL, as the block says; no ASK-OMID row. Each `tiered-cost.md` anchor in
§ Inputs swept read as quoted (the file is unchanged since `cf580b05`), with
`grep -c -i 'USB.PD'` and `grep -c -i 'pogo'` each 0. The parts study's
anchors were read by heading: § The tags' Debug row still at l.170; § The
debugger's pointer line now at l.307–308 and its "10-pin XDS110 out
connector" at l.301 and l.315–316, moved by leg 1's edits. § Both cells
reads true, as leg 1 found: no sentence of it moves.

**The merge word:** Omid, first-hand in this terminal between its `date -u`
reads of 10:33:39Z and 10:34:03Z: "Yes, push/edit/ready/merge
(Recommended)". The option read: "Please push, edit, mark ready and merge
any PRs you open during this session when ready and in the right order."

- **T1 — JLCPCB's minimum moves more than the review inferred.** The review
  INFERRED that a minimum above one board "adds a module" to the Reader's
  and the Field Generator's orders. JLCPCB's page gives two boards a design,
  and an assembled order places the same parts on every board of a design.
  So each second board carries its other parts too: 27.08 partly assembled
  and 33.62 fully assembled, against two modules' 21.46. The minimum also
  moves the three-board estimate on the assembled routes, six tag boards
  where one core makes three: 109.67 and 117.12, where the study printed
  56.50 and 62.65. Cost: one table and the three-board bullet. No fix
  needed.
- **T2 — The fully assembled joints were never printed.** The study printed
  the partly assembled joints alone, and its fully assembled fees reproduce
  only from pin counts it never wrote down. Nit 1 turns on them: at a cell
  holder of 3 pins the fees differ by 19.12, and at 4 pins by 19.13. The leg
  states the counts as ASSUMED (the SMA connector 5, the header 10, the
  holder 3), and every printed fee reproduces from them. Cost: one sentence.
  Suggested fix, for the reviewer: reproduce the fees from the stated
  counts.
- **T3 — The pogo-pin clip does not plug into the debugger's cable.**
  Adafruit's clip brings its pins out at 0.1", and the debugger's cable ends
  at 1.27 mm. A breakout board and jumper wires close the gap. The
  breakout's page does not say its header is 0.1", so that is INFERRED.
  Tag-Connect's cable, which plugs in directly, refused a plain request.
  Cost: two more parts priced. No fix needed: the bench story chooses.
- **T4 — The Done-when reaches past the review's three parts.** "Every tier
  that lists a tool which needs another part to run" sent the leg to read
  each powered tool's box. It found two more gaps (the Fluke 117's battery,
  the VNA's cable to a board) and one unknown (the plain tinySA's antenna).
  Each is named in the Lacks rather than priced, since a price would count
  a part that may already be in the box. Cost: twelve page reads, none
  refused.
- **T5 — Two pages refused, and neither was needed.** Tag-Connect answered
  with a bot check and USBGear with HTTP 403. Each had a stand-in whose page
  answered, so no price is unread and D7's message never went. Sprint 3's
  78-minute wait (its F12) did not recur.
- **T6 — An interrupt mid-leg.** After the study's commit, the
  coordinator's orders 32, on Omid's word, sent a box chore ahead of the
  leg's PR: bring the Windows box's WSL tool manager (mise) level with the
  Mac's. Another seat's test battery held the box's lock, so the leg queued
  for it in the background and drafted this section meanwhile. It held the
  lock from 10:35:33Z to 10:37:55Z. The auto-mode classifier refused the
  updater, and Omid ran it by hand. The version did not move: the updater
  printed "minimum release age: 24h", and the Mac's newer release is younger
  than that. The chore touched no file in this repo. Cost: about four
  minutes and one command for Omid.
- **T7 — A CLAIM outside the ledger.** The ledger gave the leg § The
  debugger's pointer line alone, but Story 2.1.4's sentence "Whether a cable
  for that connector comes in the box was not read." would then contradict
  it a paragraph away. The leg sent the coordinator a CLAIM. The GRANT,
  which arrived between this terminal's reads of 10:29:52Z and 10:32:22Z,
  kept the sentence verbatim and appended its dated clause. The item-8 grep
  dispositions it "amended".
- **T8 — A count borrowed, caught.** The PR body's first draft said the
  script reproduces "52" of the study's figures, the Sprint 3 review
  reader's count. A count of the script's own labelled checks read 47, all
  matching, and the body says 47. Cost: one line. No fix needed: a count
  written into a record is the leg's own read.

**Guard events:**
- `cwd-drift` seat: the leg's first `cd` into this repo, in a call of its
  own after the read of 10:17:13Z, drew "cwd-drift guard (warn, not a
  block): this command cds into `<F>` — the shell cwd PERSISTS across later
  tool calls whether this compound succeeds or fails, so a later repo
  command can silently run in the wrong repository." (the repo's path
  written here as `<F>`), and most calls after it drew the guard's second
  arm. Every git command carried `git -C` with the literal path, and none
  ran in the wrong repository.
- `swept-surface-guard-warns-on-the-legs-own-planned-edits` seat: every edit
  to the study, the parts study's two lines and the research log drew "This
  file was verified by Sprint 4's staleness sweep (swept 2026-10-29). Your
  edit may decay its pre-verified facts — re-verify or annotate the block
  (KB sprint-proposal-standard step 2)." Each was the leg's own planned
  edit, and its anchors were read again before the work (above).
- The marker hook, class id not known (the face hub Story 247.1.11 files):
  the leg's first edit, after the read of 10:24:44Z, drew "Widen the marker
  so concurrent sessions can see this work. Run from <hub>: node
  scripts/fleet-view.mjs --declare tigerfid" (the hub's path written here as
  `<hub>`), on a marker opened with `--repos tigerfid`. The command ran and
  printed "marker declared: … repos: tigerfid" before the read of
  10:29:52Z.
- `truncated-read-taken-as-source-extent` seat, caught: the first scan of
  the tool pages for what comes in each box printed each page's matches
  through `head -12`, with each page's count beside it. Every box list the
  study quotes was then read whole, by line range, before it was written.
  The item-8 census was first printed through `cut -c1-400` and `cut
  -c1-300`, its counts beside it, after the read of 10:39:11Z. No guard drew
  it. It was run again with every match whole before the read of
  10:39:36Z, and the PR body quotes that run.
- A missing-file read, class id not known: the leg's check of its quotes
  against the saved pages read the clip page's text file before it was
  written, and printed "0" beside "No such file or directory". The file was
  written and the check run again: each quote matched (1, 2, 2). An error
  line beside a zero is an unread file, not an absent quote.
- `piped-checker-exit` seat, flagged: T6's first read of the tool manager
  was piped into a file-writing stage and read `$LASTEXITCODE` after it. The
  guard drew "piped-checker guard (warn, not a block): this command reads
  `$LASTEXITCODE` right after the pipeline". The read was run again
  unpiped, exit 0, before any line used it.
- The auto-mode classifier, class id not known: T6's updater command was
  refused, "Permission for this action was denied by the Claude Code auto
  mode classifier. Reason: [Code from External]." The leg took no other
  route, as its orders said, and handed Omid the command, which he ran
  himself.

## Leg 3 — Story 2.1.13 (2026-10-29)

Run by the hub-rooted Windows session "Windows 3" (session
`70fda9ed-a4c9-427f-ab17-caed0a9261a1`), on the Mac coordinator's orders 7.
**The fresh-session read:** fresh by launch. The background-task path id
equals the session id, and no `/clear` record sits atop the context, so no
earlier process was cleared. Not elevated: a Windows administrator-role
read, `False`, at 22:57:17Z.

**Context checkpoint:** six tasks, the block's five and the EXECUTED stamp.
No compaction, no forced re-read, no lost-context defect.

**Re-verify, before the first edit** (between the `date -u` reads of
22:51:57Z and 22:52:13Z, the close runner after the second): `main` at #61
after #60; `user.email` the noreply address; `close-lint` ok and `npm test`
20 of 20; `reference-check` "299 rows verified, README the stub"; the close
runner over the eleven teeth, nine PASS, with `stub-names-sources` and
`rebuild-spec-approved` FAIL, as the block says; no ASK-OMID row. The
story's anchors read as quoted: the stub's "the other two" wrapping to
"folders" at its end, the script's `DRIFT   file` line, and the reading
rules with no `pdftotext`.

**The merge word:** Omid, first-hand in this terminal between its `date -u`
reads of 22:50:12Z and 22:51:23Z: "Please merge PRs you open during this
session when ready and in the right order."

- **R1 — The page flags spare the form-feed count.** Sprint 3's F6 found a
  page by counting form feeds in the whole document's text. `pdftotext`'s
  own `-f` and `-l` print only the pages asked for: on pages 23 and 24 of
  the module's datasheet, the two footers read "Submit Document Feedback
  23" and "24 Submit Document Feedback", each page ending in one form feed.
  On the terminal the same read showed ± and µ as replacement characters.
  The file itself held Latin-1 bytes (0xB1 and 0xB5 on 19 lines, no
  replacement character), `pdftotext`'s default, and `-enc UTF-8` gave ±, µ,
  © and ° whole. Rule 8's first wording, in `325aec17`, said the tool
  printed replacement characters; the byte count corrected it before the
  merge. Rule 8 names both flags. Cost: three reads. No fix needed.
- **R2 — The folder README changes before `main` does.** `--write-stub`
  ran on the leg's branch at 22:55:39Z. From then until #62 merges, a check
  run from `main` on this machine reads `README stale-stub`, since the
  folder now holds the branch's stub. At the leg's open, fleet-view's
  markers named no other seat in this repo. Cost: none. Suggested fix: none, since D13 puts the
  command in the leg; a later story that changes the stub can run it after
  its merge instead.
- **R3 — The grant and the ledger name different sections.** The
  coordinator's grant named `docs/reference-repos.md` with § `sources/`,
  "what the leg line and the story name". The story names the reading
  rules, and the block's ledger names them with § Verifying a transfer and
  § `sources/`. The leg edited those three and nothing else in the file, so
  it sent no CLAIM. Cost: one read of the ledger. Suggested fix: a grant
  quotes the ledger's line for the file.
- **R4 — The EXECUTED stamp rides its own PR.** The stamp's record names
  each leg's merge commit, and this leg's PR cannot name its own merge (the
  `scope` job's `check-tick-merge-claim.mjs`). The stamp therefore goes in
  a docs-only PR after #62 merges, as Sprint 3's did (#51). Cost: one more
  PR. No fix needed.
- **R5 — The sprint parser's relative plan path reads the hub's plan.** The
  stamp's check, run from inside this clone as Sprint 3's #51 recorded it
  (`check-sprint-block.mjs <hub> docs/project-plan.md 4`), printed
  `{"found":false,"problems":[]}`, exit 1, after the read of 23:03:53Z. The
  parser's header says a relative plan path is "resolved against the hub
  root argument, never the shell's cwd" (since hub #4543), so it read the
  hub's own plan. The plan's absolute path read the block, after the read
  of 23:04:01Z: `found:true`, `executed:true`, `reviewed:false`, three
  legs, no problems, exit 0. Cost: one re-run. Suggested fix: a proposal's
  and a stamp's command lines give the plan as `<F>/docs/project-plan.md`.

**Guard events:**
- `cwd-drift` seat: the leg's first `cd` into this repo, in the fetch's own
  call after the read of 22:50:12Z, drew "cwd-drift guard (warn, not a
  block): this command cds into `<F>` — the shell cwd PERSISTS across later
  tool calls whether this compound succeeds or fails, so a later repo
  command can silently run in the wrong repository." (the repo's path
  written here as `<F>`), and most calls after it drew the guard's second
  arm. Every git command carried `git -C` with the literal path, or ran
  after a `cd` into this repo in the same call; none ran in the wrong
  repository.
- `swept-surface-guard-warns-on-the-legs-own-planned-edits` seat: every edit
  to the test file, the script, the stub, `docs/reference-repos.md`, the
  plan, the manifest and this log drew "This file was verified by Sprint 4's
  staleness sweep (swept 2026-10-29). Your edit may decay its pre-verified
  facts — re-verify or annotate the block (KB sprint-proposal-standard step
  2)." Each was the leg's own planned edit, and its anchors were read again
  before the work (above).
- `piped-checker-exit` seat, flagged: the branch's first push was piped into
  `tail -3`. The guard drew "piped-checker guard (warn, not a block): this
  command pipes `git push` — a state-changing git command — into another
  command, so a refusal it prints (`! [rejected]`, `non-fast-forward`, a
  remote hook's refusal) is hidden behind the filter, and the compound's
  exit is the LAST stage's whether or not `$?` is read after it." The call
  read the push's own exit, `${PIPESTATUS[0]}` (0), and `git ls-remote
  --heads origin` then read the branch at `325aec17` before the PR opened.
- The marker hook's "Widen the marker" line did not fire: the leg ran
  `fleet-view.mjs --declare tigerfid` at its open, before any edit, as its
  orders said.
- `inferred-cause-recorded-as-measured` seat, caught (R1): the terminal's
  replacement characters were first written into rule 8 as the tool's
  output. No guard drew it. The leg's byte count, after the read of
  22:59:08Z, found Latin-1 bytes in the file, and the rule was corrected in
  this PR.
