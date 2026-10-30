# Session Notes

Cross-machine handoff notes so any Claude Code session can pick up where the
last one stopped. Newest session first; only the 3 most recent sessions are
kept (older ones are trimmed by `scripts/wrap-session.mjs` — full history
lives in git). Written at wrap-up per the "Session handoff" section of
CLAUDE.md. These notes are public, like everything in this repo (CLAUDE.md
§ Writing in public).

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

## Session: 2026-10-30 — Story 2.1.7: Omid's decision sitting, and the rebuild spec approved

- **Landed:**
  - #83 → `168bebb8` (GitHub's merge stamp 22:09:30Z), squash with
    `--match-head-commit 5178d624…` once `watch-pr.mjs` read it GREEN. Omid's
    merge word, first-hand in this terminal between its reads of 22:01:09Z
    and 22:09:27Z: "Please merge".
    - The spec, `docs/superpowers/specs/2026-10-30-rebuild-design.md`, with
      Omid's approval ("Approved", between the reads of 22:00:40Z and
      22:01:09Z); `rebuild-spec-approved` passes, eleven of eleven teeth.
    - The brief lands as `docs/study/2.1.7-decision-brief.md`. Draft #76 is
      closed in its favour by Omid's hand (GitHub's stamp 22:10:12Z), with a
      comment pointing at #83; auto mode refused this session the close.
    - Story 2.1.19 born (the EU build priced), with the prose check
      `eu-build-priced`; Story 2.1.8 waits on 2.1.18 and 2.1.19.
- **Next:**
  1. Omid sends TI the question in the spec's § 5.2. TI's written answer
     is gate G1, before any board is drawn; with no clear answer by the
     bench story's end, it comes back to him.
  2. Story 2.1.18 (the spec's § 5.3: the EU inventory planned on turns, and
     a search that finds a carried tag within [22]'s 5 minutes) and Story
     2.1.19 (§ 6.3), both before the close.
  3. Story 2.1.8, a FRESH closer, after both.
- **Surprises / decisions:**
  - **No public FCC record shows the 300 kbit/s PHY** the design rests on
    (MEASURED: four test reports, each PDF's SHA-256 matching its page).
    -3's Wi-SUN report tests mode #2a at 100 kbps; TI's SWRA797 Table 2-1
    lists Mode #5 as certified. Omid kept 300 kbit/s and asks TI (decision 2).
  - **fccid.io's pages refuse a plain request (HTTP 403), but its PDF store
    answers** at `https://fccid.io/m/<SHA-256>.pdf`; each document page's
    source names the address (`data-pdf-url`).
  - **The grants carry conditions no study read:** a mobile RF-exposure
    condition (20 cm), co-location, and, on -3, a "trace reference design"
    (the spec's gate G2).
  - **One inference withdrawn in the sitting:** "19 dBm is out" compared a
    setting with a measured output; -3's report tested 19.0 dBm. The design
    keeps 17 dBm either way.
  - **This terminal was a `/clear`** of process `1a792865-…`, Story 2.1.17's
    executor (task-output path id against session id
    `98adb3c6-9f4f-4420-a168-a93826056c78`).

