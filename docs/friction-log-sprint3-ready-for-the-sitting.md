# Friction log — Sprint 3, "Ready for the sitting"

This log records what got in the way while Sprint 3 was proposed and run:
the friction itself, what it cost, and what would prevent it. It is public,
like everything in this repo, and written for a stranger. The sprint block
is in `docs/project-plan.md` (§ Sprint 3) while the sprint is live, and in
`docs/archives/project-plan-completed.md` once it has run and been reviewed.

Each leg appends one section with:
- its context checkpoint: the leg plan's task count, and how many
  compactions, forced re-reads and lost-context defects it met;
- each friction, with its cost and its suggested fix;
- each guard event, with the guard's own banner line quoted whole and its
  register class id beside the word "seat" (the block's § Friction log).

## The proposal (2026-10-28)

Proposed by the hub-rooted Windows session "tigerfid Sprint 3 proposal"
(session `4ef9427a-246a-42f9-98aa-be892d938900`), fresh by `/clear` of the
session "tigerfid Sprint 2 review" (background-task path id
`2a0782f7-f632-4bab-b967-a9cfbfe1ba93`), in one sitting. Not elevated.

**Context checkpoint:** no compaction, no forced re-read, no lost-context
defect.

**The approval:** Omid, first-hand in this terminal between its `date -u`
reads of 10:59:30Z and 11:00:38Z: "Approve as proposed (Recommended)". The
question laid out the one decision a reader might change (D8, the EU build
named and not priced) with its cost.

- **F1 — The orders arrived after the work began.** Omid opened the session
  with the proposal's intent at the terminal's `date -u` read of 10:43:54Z.
  Air Traffic Control's orders 18 were sent at 10:49:58Z, after the inputs
  were read and the findings re-measured. They matched the work and added
  four pre-flight rows (KB `plan-authoring-for-cold-execution` item 53, a
  test-arming read per leg, § Roles' "flagged" wording, the touched-file
  ledger), which the block carries. Cost: none; the rows were added before
  the block was written.
- **F2 — A test-arming read needs a script.** The docs-only classifier
  exports `classify()` and `repoOptions()`, but no command prints the verdict
  for a change set that does not exist yet. The proposal wrote a scratch
  script that feeds each leg's ledger to them (the block's § Verified-live).
  Cost: one short script. Suggested fix: a `--paths <p,…>` flag on
  `scripts/ci-docs-only.mjs` that classifies a given list (hub-owned: the
  script is a synced canonical).
- **F3 — The close runner's review-seating line is not this proposal's.**
  The runner printed "REVIEW SEATING MISSING (Story 147.1.3 — KB
  epic-close-protocol): Sprint 2's review appended no register seat and
  records no none-qualified line". The review sent its seats to Air Traffic
  Control as text. The block hands the census to the closer (Story 2.1.8).

- **F4 — `close-lint` by absolute path from the hub linted the hub.** The
  proposal's commit gate ran `node <F>/scripts/close-lint.mjs` from the hub
  root, and it printed "close-lint: ok (adopted 2026-07-06)", the hub's
  adoption date, not this repo's 2026-10-26: the script reads the repo it is
  run from. The same chain's `npm --prefix <F> test` ran this repo's own
  close-lint ("adopted 2026-10-26", 17 of 17), so the gate held. Cost: one
  re-read. Fixed in the block's § Paths and habits, so a hub-rooted executor
  is told.

**Guard events:**
- The commit guard, class id not known: the first commit, with `git -C
  "$F"` (a shell variable), was refused with "Hub guardrail: never commit on
  main", because the guard "could not be resolved to a branch (shell-variable
  or not-yet-created path)" and judged the hub's own `main`. Re-run with the
  literal path, it committed on `sprint-3/proposal`. The block's § Paths and
  habits now says so.
- `piped-checker-exit` seat: this proposal's first `npm test` read was piped
  into `tail` and drew the guard, "piped-checker guard (warn, not a block)".
  Its `PIPESTATUS[0]` read 0, and the run was repeated unpiped into a scratch
  file: exit 0, 17 of 17.
- `piped-checker-exit` seat: the session's first `git fetch origin` in this
  repo was piped into `tail`, with no banner. The fetch was read after it by
  `git rev-list --count HEAD..origin/main`: 0.
- `cwd-drift` seat: nine calls that opened with a `cd` into this repo or its
  `docs/study/` each drew "cwd-drift guard (warn, not a block)", and the calls
  after them drew its second arm. Every git command carried `git -C` with the
  literal path or ran after a `cd` in the same call; none ran in the wrong
  repository.

## Leg 1 — Story 2.1.10 (2026-10-28)

Executed by the hub-rooted Windows session "tigerfid Sprint 3 leg 1"
(session `0082985c-c964-4b27-a5c7-4592056265f0`), on Air Traffic Control's
orders 24, sent 11:20:51Z. **The fresh-session read:** fresh by `/clear`.
The background-task path id is `2a0782f7-f632-4bab-b967-a9cfbfe1ba93`, not
the session id. That process hosted the session "tigerfid Sprint 2 review"
and then the session "tigerfid Sprint 3 proposal" (`4ef9427a-…`), the
proposer's process the orders name as the one this `/clear` cleared. Not
elevated: a Windows administrator-role read, `False`, at 11:49:05Z.

**Context checkpoint:** the plan was eight tasks, the block's tripwire but
not above it (the block estimated seven: the PR's opening came before the
tick, as its own task). No compaction, no forced re-read, no lost-context
defect.

- **F5 — The orders described the leg by D9's seam.** Orders 24 said "leg 1
  at line 246 — findings 1, 2 and 5 and the five nits". Line 246 is D9's
  remainder, the part a split would move out. The block's leg line and
  Story 2.1.10 carry all five findings, and the orders said the block
  governs, so the leg ran whole. Cost: one re-read. Suggested fix: orders
  quote a leg from the block's numbered leg list, never from a decision
  that describes a split.
- **F6 — No PDF page reader on this box.** The harness's file reader
  needs `pdftoppm`, which this box lacks, so it could not open the pinned
  datasheets' pages. Git for Windows' `pdftotext -layout` read them, and
  each page number was recovered by counting form feeds. Cost: a few calls.
  Suggested fix: the reading rules name `pdftotext` as the Windows route.
- **F7 — A predicate grep split by a line break.** The story's first grep,
  `space reserved|reserves no space|one browser`, cannot find the Decided
  line of `software-stack.md`, which wrapped "no space" and "reserved"
  onto two lines. The proposal's anchor list had named that line by hand.
  The leg ran a second grep for `no space`. Cost: one grep. Suggested fix:
  a predicate grep for a phrase a line break can split uses its shortest
  unsplittable needle.
- **F8 — Three makers' pages would not answer.** GNU's make page reset the
  connection twice, and a GNU mirror's release list and the release's own
  `README` and `COPYING` stood in. Arm's toolchain page answered HTTP 403.
  TI's SysConfig and Arm Clang pages answered, but neither states a price
  or a licence, so both licences are written as not read.
- **F9 — `tar` read a Windows path as a host.** Under Git Bash, `tar -xzf
  C:/…` printed "Cannot connect to C: resolve failed". `--force-local` with
  a `/c/…` path worked. Cost: one re-run.
- **F10 — A carriage-return count counted every line.** `grep -c $'\r'`
  over the edited files returned each file's line count, though `git
  ls-files --eol` reads them `w/lf`: the pattern did not reach `grep` as a
  carriage return. Cost: one check. Suggested fix: read line endings with
  `git ls-files --eol`, never by a grep for `\r`.
- **F11 — The old-value grep's frozen hits.** Story 2.1.4's Outcome holds
  four figures this leg supersedes (5–11 %, 2.9 years, 1.99 years and 6 %).
  The story's § What stays keeps Outcomes as records, so the tick names
  the new figures and the old ones stay.

**Guard events:**
- `piped-checker-exit` seat: the leg's first read of the close runner's
  usage, `close-epic.mjs --help` piped into `head`, flagged "piped-checker
  guard (warn, not a block): this command pipes `close-epic.mjs` into
  another command, so the compound's exit code is the LAST pipe stage's —
  `$?` no longer proves the checker's verdict." It was a usage read, not a
  verdict. Every verdict run after it went to a file, unpiped, its exit
  read.
- `cwd-drift` seat: most calls after the leg's first `cd` into this repo
  drew "cwd-drift guard (warn, not a block)", its first arm on a `cd` and
  its second arm after it. Every git command carried `git -C` with the
  literal path or ran after a `cd` in the same call; none ran in the wrong
  repository.
- The staleness-sweep hook, class id not known: every edit of a file the
  proposal swept drew "This file was verified by Sprint 3's staleness sweep
  (swept 2026-10-28). Your edit may decay its pre-verified facts —
  re-verify or annotate the block (KB sprint-proposal-standard step 2)."
  The edits were the leg's own, each finding's anchor re-read before it.
