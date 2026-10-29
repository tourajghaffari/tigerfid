# Session Notes

Cross-machine handoff notes so any Claude Code session can pick up where the
last one stopped. Newest session first; only the 3 most recent sessions are
kept (older ones are trimmed by `scripts/wrap-session.mjs` — full history
lives in git). Written at wrap-up per the "Session handoff" section of
CLAUDE.md. These notes are public, like everything in this repo (CLAUDE.md
§ Writing in public).

## Session: 2026-10-29 — Sprint 4 leg 3 (Story 2.1.13, the sources/ route) and the sprint executed

- **Landed:**
  - #62 → `d149ade6` (GitHub's merge stamp 23:02:54Z), squash with
    `--match-head-commit 53a5f270…` once `watch-pr.mjs` read it GREEN
    (by 23:02:41Z).
    - The folder's README stub names `sources/` and how each folder
      travels. A drifted `sources/` file's line names the newer revision
      behind its URL and the copy route. Reading rule 8 names `pdftotext
      -layout -enc UTF-8 -f <first> -l <last> <file> -`.
    - `stub-names-sources` flipped FAIL → PASS; `npm test` reads 22 of 22.
      `--write-stub` ran on this leg's machine.
  - #63 → `ad5808eb` (23:06:10Z): the Sprint 4 block is stamped EXECUTED,
    with its Executed record and `**Review:** PENDING`; the friction log
    gains R5.
- **Next:**
  1. **The other machine's reference folder** reads `README stale-stub`
     until `node scripts/reference-check.mjs --write-stub` runs there
     (D13). The next session there runs it when the check prints it.
  2. **The Sprint 4 reviewer pass,** a FRESH seat. Then Story 2.1.7's
     sitting (Omid's), then Story 2.1.8's close (a FRESH closer).
- **Surprises / decisions:**
  - **The merge word:** Omid, first-hand in this terminal between its
    reads of 22:50:12Z and 22:51:23Z: "Please merge PRs you open during
    this session when ready and in the right order." Neither merge held.
  - **For the reviewer:** R5, `check-sprint-block.mjs` resolves a relative
    plan path against the hub argument (since hub #4543), so give it the
    plan's absolute path. R1, `pdftotext` writes Latin-1 without `-enc
    UTF-8`: a byte count settled it, not the terminal. R3, the grant named
    one of the three `reference-repos.md` sections the ledger names.
- **Provenance:** Session id `70fda9ed-a4c9-427f-ab17-caed0a9261a1`, the
  hub-rooted executor seat "Windows 3", fresh by launch (the friction log's
  fresh-session read).

## Session: 2026-10-29 — Sprint 4 leg 2 (Story 2.1.14, the tiers)

- **Landed:** #60 → `b707bf1a` (GitHub's merge stamp 10:44:46Z), squash
  with `--match-head-commit 6c6e8ded…` once `watch-pr.mjs` read it GREEN
  (10:44:30Z).
  - Every tier prices what its tools need to run: the hot plate's 100 W
    supply (advanced, pro), and the debugger's cable and the tags'
    pogo-pin clip (every tier). Three box contents no page settles are
    named in the Lacks.
  - JLCPCB's minimum is read, two boards a design, so each assembled route
    buys a second Reader and Field Generator. Three boards by hand cost
    34.88. The four nits are fixed, and the totals are 602.25, 1,449.92
    and 1,996.03.
  - The tick, the manifest's `reviewedFor` line and the friction log's
    leg 2 section ride the same PR.
- **Next:**
  1. **Sprint 4 leg 3, Story 2.1.13,** in a FRESH executor seat, by the
     block's kickoff. It touches `scripts/` and merges by Omid's word.
  2. Then the Sprint 4 review, Story 2.1.7's sitting and Story 2.1.8's
     close.
- **Surprises / decisions:**
  - **A CLAIM, granted.** The parts study's "not read" cable sentence
    stays verbatim, with a dated clause appended, on the coordinator's
    GRANT (friction T7).
  - **The merge word:** Omid, first-hand in this terminal between its
    reads of 10:33:39Z and 10:34:03Z: "Yes, push/edit/ready/merge
    (Recommended)".
  - **For the reviewer:** reproduce the fees from the stated fully
    assembled joints (T2). The leg's script is in #60's body.
  - **A box chore mid-leg** (friction T6): the tool manager's updater held
    its version, under a 24 h minimum release age. No repo file was
    touched.
- **Provenance:** Session id `7a8f4e54-9463-4a25-b0d2-718feceb0236`, the
  hub-rooted executor seat "tigerfid Sprint 4 leg 2", fresh by `/clear` of
  "Windows 4"'s process (the friction log's fresh-session read).

## Session: 2026-10-29 — Sprint 4 leg 1 (Story 2.1.12, the listening window)

- **Landed:** #58 → `cf580b05` (GitHub's merge stamp 10:05:48Z), squash
  with `--match-head-commit 64c09fb0…` once `watch-pr.mjs` read it GREEN
  (07:50:38Z).
  - The radio study states the layout of a listen: the beacon skipped on
    most listens. Every battery figure is re-priced, and Sprint 3's D2 is
    re-run: [18] by 24 %, [9] by 22 %, [27] by 22 % and 11 % on cold
    chain. All are met on paper, and no ASK-OMID row is filed.
  - The conference's block is computed (1,308 reply slots a second), and
    GNU make is named among the tools not cited (D4's default).
  - The tick, the manifest's `reviewedFor` line and the friction log's
    leg 1 section ride the same PR.
- **Next:**
  1. **Sprint 4 leg 2, Story 2.1.14,** in a FRESH executor seat, by the
     block's kickoff. It reads `tiered-cost.md` anchors at `cf580b05`.
     `tiered-cost.md` § Both cells needs no sentence moved (#58's body,
     § For leg 2).
  2. Then leg 3 (Story 2.1.13, merged by Omid's word), the Sprint 4
     review, Story 2.1.7's sitting and Story 2.1.8's close.
- **Surprises / decisions:**
  - **The classifier refused the relayed merge delegation** ("Merge
    Without Review"), before the read of 07:51:08Z. Omid answered
    first-hand in this terminal, "Merge #58 (Recommended)", between that
    read and 10:05:45Z. He then gave this session its own delegation:
    "Please merge any PRs you open during this session when ready and in
    the right order."
  - **For the reviewer:** the block's order was a second hidden
    assumption (friction L1); reproduce slot counts at 11/15 ms (L2). The
    leg's script is in #58's body.
- **Provenance:** Session id `6fb033a2-2fc2-4f7a-8d61-8918850b976e`, the
  hub-rooted executor seat "tigerfid Sprint 4 leg 1", fresh by `/clear` of
  "Windows 4"'s process, which last held the Sprint 4 proposal (the friction
  log's fresh-session read).

