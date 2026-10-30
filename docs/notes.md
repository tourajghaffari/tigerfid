# Session Notes

Cross-machine handoff notes so any Claude Code session can pick up where the
last one stopped. Newest session first; only the 3 most recent sessions are
kept (older ones are trimmed by `scripts/wrap-session.mjs` — full history
lives in git). Written at wrap-up per the "Session handoff" section of
CLAUDE.md. These notes are public, like everything in this repo (CLAUDE.md
§ Writing in public).

## Session: 2026-10-30 — Story 2.1.18 reviewed (PASS WITH FINDINGS); Story 2.1.20 filed

- **Landed:**
  - #89 → `e0fae75d` (GitHub's merge stamp 22:40:34Z), squash with
    `--match-head-commit ff0a9d61…` once `watch-pr.mjs` read it GREEN.
    - Story 2.1.18 carries its `**Reviewed:**` record: PASS WITH FINDINGS.
      #85's 48 figures reproduce from the studies' inputs, no requirement
      number or printed figure moved, and its greps reproduce. It writes
      the rebuild spec's 3b and 3c into the study as § 5.3 states them.
    - Story 2.1.20 is filed with the review's Minor H, and the manifest
      gains its `reviewedFor` line, `2.1.20` in `stories` and the prose
      check `story2118-review-fixes`.
- **Next:**
  1. Story 2.1.20, before Story 2.1.8: what starts the wide search, and
     which network a carried tag joins. On the study's own budget a tag
     carried one or two bays past its Reader's edge likely still hears its
     old Reader's beacon but none of its Field Generators, so it stays
     unplaced and the wide search never runs (INFERRED).
  2. The Story 2.1.19 review (Mac Reviewer's next order).
  3. Story 2.1.8, a FRESH closer, once Stories 2.1.19 and 2.1.20 are done
     and reviewed.
- **Surprises / decisions:**
  - **This pass filed Story 2.1.18** (as the Story 2.1.17 review) and
    reviewed it, so it held #85 to the written Done-when and § 5.3 and
    re-measured every figure.
  - **The register seats** (#85's four wrapped hits, each correct; this
    pass's one, a reading `node -e` that also wrote the manifest, caught
    before the commit) go to the coordinator as text in #89's body.
  - **The merge word:** Omid, first-hand in this terminal between its reads
    of 20:17:04Z and 20:20:02Z: "Please merge any PRs you open during this
    session when ready and in the right order."
- **Provenance:** Session id `86ac7523-9bad-4199-8c93-a5a8b64ce65b`, "Mac
  Reviewer", a reviewer seat on the fleet coordinator's orders, a fresh
  launch: its background-task path id equals its session id. Not elevated.
  It did none of Story 2.1.18's work.

## Session: 2026-10-30 — Story 2.1.19: the EU build, priced

- **Landed:**
  - #86 → `97d1ae40` (GitHub's merge stamp 22:32:30Z), squash with
    `--match-head-commit cfd5fdd1…` once `gh pr checks` read it GREEN (run
    41138721800). Omid's merge word, first-hand in this terminal: "Please
    merge PRs you open when ready and in the right order."
    - `docs/study/tiered-cost.md` § The EU build prices each part the EU
      build changes, from pages read 2026-10-30 between 22:13:25Z and
      22:23:01Z, in euros net of Germany's 19 % VAT, at the ECB's rate of the
      day (1 EUR = 1.1172 USD). Each tier's EU total, with its Lacks: starter
      598.18, advanced 1,387.85, pro 2,144.08, the spec's first build
      1,413.52. No US figure moved: a script rebuilt all 20 US figures from
      their unrounded lines, none differing.
    - Story 2.1.19 is ticked, with its line in `docs/close/2.1.json`; the
      prose check `eu-build-priced` is the closer's.
  - One `git merge origin/main` before landing, over Story 2.1.18's #85
    (`c46efb83`): the manifest's two new `reviewedFor` lines met, and both
    are kept, 2.1.18's first.
- **Next:**
  1. Mac Reviewer reviews #86, after Story 2.1.18's review, as Story
     2.1.19's `**Reviewed:**` record.
  2. Story 2.1.8, a FRESH closer, after the reviews (the spec's gate G5).
- **Surprises / decisions:**
  - **The W5017 serves 868 MHz.** Pulse's datasheet gives it 868–928 MHz and
    0.89–0.93 dBi at 868 MHz, so the EU build keeps the US build's whip and
    the Reader's 15.2 dBm EU ceiling stands.
  - **Johanson prints the 868 MHz antenna's gain two ways:** "Peak Gain 1.0"
    on its page, "-2.5 Typ." on its datasheet. At the datasheet's figure the
    EU link at 30 m keeps 17.6 dB up and 20.8 dB down (INFERRED); the build's
    range test measures it.
  - **Omid's ruling, this session (plan Sprint 3's D7):** four prices stay
    not read, "Write all as not read (Recommended)", first-hand between this
    terminal's reads of 22:23:33Z and 22:24:17Z: the 868 MHz antenna, both
    ATTEN stations' 230 V models, and the W5017 at EU sellers.
  - **For the reviewer:** the D7 message named TME for the two stations
    before their TME pages were requested; they answered HTTP 403 after his
    answer (22:26:44Z), and the study says so. The FX-971's 230 V is
    INFERRED from a German seller's page that names no voltage.
  - **The register seats:** two `zsh-equals-face` (an `echo "=====…"` label
    and a `==` label in read-only commands; caught, nothing written), and the
    D7 message's claim before its read (above), caught and recorded.
  - **The seat:** "Mac 4", a `/clear` of the process `ab15bb49-…` (the
    background-task path id); this conversation is
    `28d2be54-66c1-4303-b696-2b48c0bc788c`.

## Session: 2026-10-30 — Story 2.1.18: the EU inventory planned on turns, and the wide search for a carried tag

- **Landed:**
  - #85 → `c46efb83` (GitHub's merge stamp 22:31:05Z), squash with
    `--match-head-commit 0d491857…` once `watch-pr.mjs` read it GREEN (run
    41138555413). Omid's merge word, first-hand in this terminal between its
    reads of 20:53:09Z and 20:54:04Z: "Please merge any PRs you open during
    this session when ready and in the right order."
    - `docs/study/radio-and-location.md` carries the rebuild spec's 3b and 3c
      (§ 5.3 at `168bebb8`). EU Readers that share a channel take turns:
      8.0–8.9 minutes, the planning figure. The US sentence on bays 30 m apart
      goes. A lost tag at a site of several networks makes two wide searches
      first, so a carried tag is shown in about 50 s (EU) or 60 s (US).
    - Story 2.1.18 is ticked, with its line in `docs/close/2.1.json`. The
      close runner at the pre-tick head `2911c2af` read eleven PASS (exit 0).
- **Next:**
  1. Mac Reviewer reviews #85, as Story 2.1.18's `**Reviewed:**` record.
  2. Story 2.1.19, the EU build priced (Mac 4), then Story 2.1.8, a FRESH
     closer, after the reviews.
- **Surprises / decisions:**
  - **The spec's 3c widened the story past its filed text.** As filed, it
    asked for a carried tag's time and a weighing. § 5.3 asks this story to
    design and price the search that meets [22]. The study holds both (the
    wide search, and the carries each cell allows), and the filed Done-when
    is met with them.
  - **Omid's ruling, this session:** the CR2032 core's US limit, one carry
    every 2.4 days or 4.7 on cold chain. "State the limit; CR2450 if
    carried", first-hand between this terminal's reads of 22:11:39Z and
    22:24:12Z, recorded in the study and the tick.
  - **For the reviewer:** the wide search's time counts a second hearing at
    the next 12 s listen before the host places a newly joined tag (INFERRED).
    The forklift's few metres a second is ASSUMED.
  - **The seat:** "Mac Closer", a `/clear` of the process `f9546ae8-…` (the
    background-task path id); this conversation is
    `a6bdd28f-d94a-4447-b476-2e883dfb76cf`.

