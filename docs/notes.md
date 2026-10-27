# Session Notes

Cross-machine handoff notes so any Claude Code session can pick up where the
last one stopped. Newest session first; only the 3 most recent sessions are
kept (older ones are trimmed by `scripts/wrap-session.mjs` — full history
lives in git). Written at wrap-up per the "Session handoff" section of
CLAUDE.md. These notes are public, like everything in this repo (CLAUDE.md
§ Writing in public).

## Session: 2026-10-27 — Sprint 2 leg 3 (Story 2.1.4, the parts and toolchains study)

- **Landed:** #33 → `3068168d` (GitHub's merge stamp 19:39:32Z, by
  auto-merge armed on the full head sha `69832608`). `scope` was green at
  19:38:23Z and `test` at 19:38:35Z. Story 2.1.4 ✅ DONE. The tick, the
  study and #33's body (its greps, claims audit and checks) are the record.
- **Next:**
  - Sprint 2's leg 4, Story 2.1.5 (`docs/study/software-stack.md`), in a
    FRESH executor session after this wrap. It is the sprint's last leg, so
    its executor stamps the block executed.
  - **For leg 4, from this study:** the Reader reaches the host as a USB CDC
    serial port, with no driver to install on any of the three platforms.
  - **Before any part is bought:** read the FCC grant pages for
    ZAT-1312PSIP-2 and ZAT-1312PSIP-3 in a browser. Every machine route was
    refused (HTTP 403).
  - **The other machine, on its next session:** `node
    scripts/reference-check.mjs` will report the five new `sources/` files
    missing. Fetch each from its row's URL (it counts only once its sha256
    matches), or copy it from this machine.
- **Surprises / decisions:**
  - **The grant read was put to Omid first-hand,** between this terminal's
    `date -u` reads of 19:17:09Z and 19:25:00Z. He answered "Write it as not
    read". Changing the network route did not lift the refusals (friction
    F13).
  - **TI's documents name two FCC IDs:** -2 in the datasheet, and -3 for
    the 2024 filings (F15). `radio-and-location.md`'s label line is the
    datasheet's.
  - **The eCFR's API now needs a compressed request** (`curl
    --compressed`, F14).
  - **The fresh-session read:** session `326f43fb-a014-4eea-838f-59fff96f5faa`.
    Its background-task path id was `c59c545c-e735-4344-b0b8-697d9f897dca`,
    the process the Sprint 2 proposal and legs 1–2 used, so a `/clear` came
    before it. It ran without elevation.

## Session: 2026-10-27 — Sprint 2 leg 2 (Story 2.1.3, the radio and location study)

- **Landed:** #31 → `ef5c2895` (GitHub's merge stamp 18:55:56Z, by
  auto-merge armed on the full head sha `0b929589`; `scope` and `test` green
  at 18:54:37Z and 18:54:47Z). Story 2.1.3 ✅ DONE. The tick, the study and
  #31's body (its greps, claims audit and checks) are the record.
- **Next:**
  - Sprint 2's leg 3, Story 2.1.4 (`docs/study/parts-and-toolchains.md`), in
    a FRESH executor session after this wrap. Leg 4 follows.
  - **For leg 3, from this study:**
    - read the CC1312PSIP's FCC grant record (FCC ID ZAT-1312PSIP-2). The
      FCC's search and fccid.io answered HTTP 403 to a plain request, so its
      equipment class is unread, and a browser read by Omid may be the route;
    - read Part 15 Subpart B for the Reader's USB or Ethernet parts;
    - replace the study's ASSUMED antenna, slot-guard and receive start-up
      figures.
  - **The other machine, on its next session:** `node scripts/reference-check.mjs`
    reports `MISSING folder sources/` until the three TI files arrive. Fetch
    each from its row's URL (it counts only once its sha256 matches), or copy
    it from this machine.
- **Surprises / decisions:**
  - **The EU texts came through the Publications Office's Cellar service.**
    EUR-Lex's own pages answered curl with an empty HTTP 202. The Cellar
    service (`publications.europa.eu/resource/celex/<CELEX>`, with an
    `Accept: application/xhtml+xml` header) served the consolidated texts.
    The Decision's identifier needs `(01)` URL-encoded. Friction F8 and F12.
  - **A browser-identity retry was refused.** The permission check refused
    one retry against a vendor's 403 that would have presented a browser's
    identity, as bot-check circumvention. Every later request was plain
    (friction F8).
  - **The fresh-session read:** session `f367e3c4-268f-4213-80f5-0de763c5a7e2`.
    Its background-task path id was `c59c545c-e735-4344-b0b8-697d9f897dca`,
    the process the Sprint 2 proposal and leg 1 used, so a `/clear` came
    before it. It ran without elevation.

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

