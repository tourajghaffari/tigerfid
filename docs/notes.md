# Session Notes

Cross-machine handoff notes so any Claude Code session can pick up where the
last one stopped. Newest session first; only the 3 most recent sessions are
kept (older ones are trimmed by `scripts/wrap-session.mjs` — full history
lives in git). Written at wrap-up per the "Session handoff" section of
CLAUDE.md. These notes are public, like everything in this repo (CLAUDE.md
§ Writing in public).

## Session: 2026-10-28 — the Sprint 3 proposal (approved, three legs)

- **Landed:** #43 → `88d54034` (GitHub's merge stamp 11:02:31Z), squash by
  hand on green under the session's merge delegation, `--match-head-commit
  10906b17…`. The Sprint 3 block is the state: `docs/project-plan.md`
  § Sprint 3, approved by Omid first-hand between this terminal's `date -u`
  reads of 10:59:30Z and 11:00:38Z ("Approve as proposed (Recommended)").
- **Next:**
  1. **Leg 1, Story 2.1.10**, the Sprint 2 review's fixes to the three
     studies, in a FRESH executor seat. The block's kickoff is baked; read
     the touched-file ledger for the grants.
  2. **Leg 2, Story 2.1.6**, the tiered cost, after leg 1 merges. Its D7
     message to Omid (the refused price pages) comes early in the leg.
  3. **Leg 3, Story 2.1.11**, the reference check's `sources/` message. It
     touches `scripts/`, so it merges by Omid's word.
  4. Then the Sprint 3 review, Story 2.1.7's sitting and Epic 2.1's close.
- **Surprises / decisions:**
  - **`close-lint` lints the repo it is run from.** Run by absolute path
    from the hub root, it linted the hub ("adopted 2026-07-06"). `npm
    --prefix <F> test` linted this repo. The block's § Paths and habits says
    so (friction F4).
  - **The commit guard cannot resolve `git -C "$VAR"`** and judged the hub's
    `main`; a literal path committed.
  - **The close runner prints "REVIEW SEATING MISSING" for Sprint 2's
    review.** It is the closer's census (Story 2.1.8), noted in the block.
  - **The fresh-session read:** session
    `4ef9427a-246a-42f9-98aa-be892d938900`. Its background-task path id,
    `2a0782f7-…`, was the Sprint 2 reviewer pass's process, so a `/clear`
    came before it. It ran without elevation.

## Session: 2026-10-28 — the Sprint 2 reviewer pass (every leg PASS; six findings)

- **Landed:** #41 → `53abe343` (GitHub's merge stamp 10:39:16Z), squash by
  hand on green under Omid's in-session sentence, `--match-head-commit
  335ffd16…`. The Sprint 2 block is now in the archive's § Executed sprints,
  with its `**Reviewed:**` record. That record is the state; this note is
  the delta.
- **Next:**
  1. **The Sprint 3 proposal,** in a FRESH seat. Its input is the record's
     routed list:
     - a fix-up story for the three studies (findings 1–5 and five nits),
       ahead of Story 2.1.6;
     - then 2.1.6;
     - then F11's message fix, as its own story.
  2. **Story 2.1.7's sitting.** Its Context now carries Omid's HF footprint
     ruling, the FCC grant read (both IDs, in his browser), the parts study's
     open items and the studies' build-initiative items.
  3. **Epic 2.1's close,** Story 2.1.8.
- **Surprises / decisions:**
  - **Omid changed one of #39's three decisions** in this pass, asked as a
    question of commercial viability: "Reserve footprint, coil off-core
    (Recommended)". The core keeps an unfitted NFC footprint and two antenna
    pads, with the coil in the enclosure. `docs/study/software-stack.md`
    still reads "no space reserved" until the fix-up corrects it.
  - **The hub's Item-8 check reads only a section titled "Item-8 anchor
    grep".** The legs' "Item-8 greps" sections read as nothing to it
    (friction R1).
  - **The fresh-session read:** session
    `2a0782f7-f632-4bab-b967-a9cfbfe1ba93`, fresh by launch (its
    background-task path id is its own session id), and not elevated.

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

