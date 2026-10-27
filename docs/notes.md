# Session Notes

Cross-machine handoff notes so any Claude Code session can pick up where the
last one stopped. Newest session first; only the 3 most recent sessions are
kept (older ones are trimmed by `scripts/wrap-session.mjs` — full history
lives in git). Written at wrap-up per the "Session handoff" section of
CLAUDE.md. These notes are public, like everything in this repo (CLAUDE.md
§ Writing in public).

## Session: 2026-10-27 — Sprint 2 leg 1 (Story 2.1.9) and the line-ending pin

- **Landed:** both squash-merged, each pinned to its green head.
  - #28 → `fc86cd14` (GitHub's merge stamp 18:09:01Z, by auto-merge): a root
    `.gitattributes`, LF stored and checked out. `license` and `_assets/`,
    the 2018 files unchanged since the tag, are `-text`. This closes friction
    F4 and gap (1) of hub #4452; gaps (2) and (3) are the hub's.
  - #29 → `bdd23c4f` (merge stamp 18:16:19Z): Story 2.1.9 ✅ DONE. The
    tick and the PR body are the record: both greps with each hit's
    disposition, and a claims audit. The arm had not fired on a green, CLEAN
    head, so it was merged by hand, pinned to the full head sha.
- **Next:**
  - Sprint 2's leg 2, Story 2.1.3 (`docs/study/radio-and-location.md`), in
    a FRESH executor session per the block's kickoff, after this wrap. Legs 3
    and 4 follow serially.
  - **The Mac clone, on its next session:** run `git ls-files --eol`. Any
    `w/crlf` row takes #28's repair (delete the files, then
    `git checkout-index -f -u`, never a renormalize). INFERRED: none, since
    git on macOS leaves `autocrlf` unset.
- **Surprises / decisions:**
  - **"The originals" read as the 2018 files still on `main`.** The orders
    asked that the originals' tree be marked `-text`. Naming the tag's
    folders would also catch the rebuild's own future `software/`. MEASURED:
    a `.gitattributes` on `main` cannot reach the tag, because `git archive`
    of the tag ignored it. `readme.md` and `contributing.md` were rewritten
    by #7, so they follow `*.md`. #28's body and friction F6 record this.
  - **This Windows clone is repaired.** All 54 tracked files read `w/lf`.
    The sprint parser now reads the working-tree plan (`found: true`, exit 0),
    so the workaround of reading `git show :docs/project-plan.md` is retired
    here.
  - **The fresh-session read:** session `014c145a-45db-4c1d-b7ff-a2e9bcbcb0c4`.
    Its background-task path id was `c59c545c-e735-4344-b0b8-697d9f897dca`,
    the Sprint 2 proposal's process, so a `/clear` came before it. It ran
    without elevation.

## Session: 2026-10-27 — the Sprint 2 proposal (approved, four legs)

- **Landed:** #26 → `e06bf9b9` (17:31:48Z), merged under Omid's in-session
  merge instruction, pinned to its green head.
  - The plan's § Sprint 2 block is the record.
  - Omid approved it first-hand in this terminal, between its `date -u`
    reads of 17:29:28Z and 17:29:48Z: "Approve as proposed (Recommended)".
  - Hub inbox #4452 merged at 17:28:39Z (the line-ending gap, below).
- **Next:**
  - Sprint 2's leg 1, Story 2.1.9, in a FRESH executor session per the
    block's kickoff (one session per leg, either machine). Legs 2–4 (2.1.3,
    2.1.4, 2.1.5) follow serially, each after the previous leg merges.
  - After leg 4: the Sprint 2 reviewer pass, then the Sprint 3 proposal
    (Story 2.1.6 and any remainder the block's D9 splits off), then the
    decision sitting (2.1.7).
- **Surprises / decisions:**
  - **No line-ending pin here.** This repo has no `.gitattributes`, and Git
    for Windows checks text out with CRLF line endings. Over the working-tree
    plan the fleet's sprint parser printed `found: false`; over the staged
    blob (LF) it read all four legs. MEASURED.
    - The fleet's other repos pin LF in a `.gitattributes`.
    - Filed to the hub as #4452. Until the hub acts, a parser read on Windows
      runs over `git show :docs/project-plan.md`, not the file.
  - **Three stale "waits on A1" lines, not one.** The Sprint 1 review's
    finding 4 named one. A grep for its predicate found three:
    `original-system.md`, `reference-research.md` and `reference-repos.md`
    reading rule 7. Story 2.1.9 carries all three.
  - **One manifest check could never pass, fixed in #26.**
    `free-tools-three-platforms` asked for boards "drawn in KiCad 10", but
    the study draws none. It now asks that every planned board is planned in
    KiCad 10.
  - **The fresh-session read:** session `49c256bc-78bf-445e-b2a6-f970dfb1f707`.
    Its background-task path id was `c59c545c-e735-4344-b0b8-697d9f897dca`,
    the Sprint 1 leg 4 session's process, last used by the Sprint 1 reviewer
    pass, so a `/clear` came before it. It ran without elevation.

## Session: 2026-10-27 — the Sprint 1 reviewer pass (every leg PASS)

- **Landed:** both merged on Omid's in-session merge instruction, each pinned
  to its green head.
  - #23 → `a647e11b` (15:28:02Z). The archive's Sprint 1 `**Reviewed:**`
    record is the adjudication: every leg PASS, five findings. #23 also
    refreshes the two restated PENDING copies, adds one dated Context
    sentence to each of Stories 2.1.3, 2.1.4, 2.1.5 and 2.1.7, and corrects
    `docs/close/2.1.json`'s `rebuild-spec-approved` run line, with one
    `reviewedFor` entry.
  - #24 → `3c2cdbbf` (15:31:22Z) corrects the record's register-seats
    bullet. The Epic 1.1 close's two seats had landed in hub #4446 before
    #23 opened.
- **Next:**
  - The Sprint 2 proposal, on Omid's word, from Story 2.1.3. The Context
    sentences on 2.1.3–2.1.7 are its inputs.
  - Its first study PR also carries the review's fix-up to
    `docs/study/original-system.md` (findings 3–4 in the Sprint 1 record):
    the spec corrections are three, not two, and the Outlook-message
    sentence still waits on A1, which is answered.
- **Surprises / decisions:**
  - **A born tooth that could never pass.** `rebuild-spec-approved`'s regex
    failed to parse on every input (`SyntaxError`). MEASURED on fixtures
    under Git Bash. INFERRED: the same lost-backslash mechanism as 1.1's
    `build-inputs-name-notices`. A born-RED tooth hides this kind of defect,
    because its red reads as "not yet".
  - **A shared record moved during a wait.** Omid's answer on the #6–#8
    merges came between this terminal's `date -u` reads of 13:21:53Z and
    15:22:47Z. The hub register gained the close's two seats in between, so
    the record written from the first read was stale, and #24 corrects it.
  - **A CR check that misread.** On this machine, `grep -c $'\r'` over
    `git show` output counted every line, on `origin/main`'s files too. Only
    `git cat-file -p … | od -c` showed the blobs are LF. MEASURED.
  - **The fresh-session read:** session `f08270bf-1a7d-43fc-8c97-df28f66d8ebc`.
    Its background-task path id was `c59c545c-e735-4344-b0b8-697d9f897dca`,
    leg 4's process, which leg 5's session `c40e0637-…` also ran in. So a
    `/clear` came before it. It ran without elevation.

