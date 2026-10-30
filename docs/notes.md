# Session Notes

Cross-machine handoff notes so any Claude Code session can pick up where the
last one stopped. Newest session first; only the 3 most recent sessions are
kept (older ones are trimmed by `scripts/wrap-session.mjs` — full history
lives in git). Written at wrap-up per the "Session handoff" section of
CLAUDE.md. These notes are public, like everything in this repo (CLAUDE.md
§ Writing in public).

## Session: 2026-10-29 — Sprint 4 reviewed (PASS WITH FINDINGS); Story 2.1.15 filed

- **Landed:**
  - #65 → `9940597e` (GitHub's merge stamp 00:00:55Z, 2026-10-30), squash
    with `--match-head-commit 82e2ca46…` once `watch-pr.mjs` read it GREEN.
    - The Sprint 4 block is cut whole to the archive's § Executed sprints,
      with its `**Reviewed:**` record: PASS WITH FINDINGS, each leg MET.
    - Story 2.1.15 is filed ahead of Story 2.1.7. It carries finding 1,
      Important: the warehouse's "one run of three" does not hold at a
      Reader's edge. INFERRED: a CR2032 lasts 0.92 of five years there, and
      the cold-chain tag 0.85, while the recommended CR2450 still meets
      [27]. It also carries four Minors and five nits.
    - The manifest gains `2.1.15` and the prose check
      `sprint4-review-fixes`.
- **Next:**
  1. **A Sprint 5 proposal for Story 2.1.15** (a FRESH proposer), then its
     leg and its review. Omid may sit first, at the cost the record names:
     choosing the item tag's cell from a CR2032 margin that does not hold at
     a Reader's edge.
  2. Story 2.1.7, Omid's sitting; then Story 2.1.8, a FRESH closer, who
     also adjudicates GNU make's INFERRED chain under
     `three-platform-claims-sourced` (the record's note for the closer).
  3. **The Mac's reference folder** still reads `README stale-stub`. MEASURED
     by this pass: it is byte for byte the stub before leg 3. The next
     tigerfid session on the Mac runs `node scripts/reference-check.mjs
     --write-stub` (D13; the friction log's V1).
- **Surprises / decisions:**
  - **The register seats** of legs 2 and 3, and this pass's own, went as
    text in #65's body for the coordinator's fold. Until that fold lands,
    the close runner prints "REVIEW SEATING MISSING" for Sprint 4.
  - **The merge word:** Omid, first-hand in this terminal between its reads
    of 23:36:35Z and 23:40:19Z: "Yes, push/edit/ready/merge
    (Recommended)". The merge did not hold.
- **Provenance:** Session id `6c5fdfae-3e60-414d-bac8-80781e57d20e`, the
  hub-rooted reviewer seat "Mac 3", fresh by `/clear` of the process
  `5c057329-…` (the friction log's fresh-session read).

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

