# Session Notes

Cross-machine handoff notes so any Claude Code session can pick up where the
last one stopped. Newest session first; only the 3 most recent sessions are
kept (older ones are trimmed by `scripts/wrap-session.mjs` — full history
lives in git). Written at wrap-up per the "Session handoff" section of
CLAUDE.md. These notes are public, like everything in this repo (CLAUDE.md
§ Writing in public).

## Session: 2026-10-28 — Sprint 2 leg 4 (Story 2.1.5, the software stack study) and the Sprint 2 stamp

- **Landed:**
  - #36 → `bbc791d7` (GitHub's merge stamp 00:35:02Z): squash by hand
    under Omid's in-session delegation, `--match-head-commit b8f7608b…`.
    `scope` and `test` were green on CI's first run. Story 2.1.5 ✅ DONE.
    The tick, the study and #36's body (its greps, claims audit and checks)
    are the record.
  - #37 → `d59e1ae0` (GitHub's merge stamp 00:38:06Z), by hand on green:
    the Sprint 2 block, stamped ✅ EXECUTED (2026-10-28), with its
    **Executed:** record and `**Review:** PENDING`.
  - #39 → `afb408c0` (GitHub's merge stamp 00:45:44Z), by hand on green,
    after the first wrap: Omid's three decisions from the software study,
    first-hand, ahead of the sitting.
    - **The decisions:** Python for the host; no HF interface on milestone
      1's seal, and no space reserved for one on its core; raw check-ins
      kept 7 days by default.
    - **Where they are recorded:** Story 2.1.7's Context and
      `docs/study/software-stack.md` § Decided ahead of the sitting.
- **Next:**
  1. The Sprint 2 reviewer pass, in a FRESH seat (Omid's `/clear` after this
     wrap lands). Its input is the block's "Open for the review" list, and
     #39 landed after the stamp.
  2. The Sprint 3 proposal: Story 2.1.6, with no D9 remainder.
  3. Story 2.1.7, Omid's decision sitting. The software study leaves it
     nothing open: it carries #39's three decisions into the spec.
  4. Epic 2.1's close, Story 2.1.8.
- **Surprises / decisions:**
  - **This machine's corpus had no `sources/`.** All eight files, fetched
    from their rows' URLs, matched their pins, and the check reads 299 rows
    here (F18). The other machine has nothing new to fetch from leg 4.
  - **The harness `grep` printed nothing,** not even a count, over a
    non-UTF-8 source file of the original. `/usr/bin/grep -a` read it
    (F19).
  - **The research-log entry** rode #36 on Air Traffic Control's GRANT of
    00:28:48Z (F21).
  - **The fresh-session read:** session `a60005ee-b36d-474f-9f59-b2624330eb37`,
    fresh by launch (its background-task path id is its own session id), and
    not elevated.

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

