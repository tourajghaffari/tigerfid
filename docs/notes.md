# Session Notes

Cross-machine handoff notes so any Claude Code session can pick up where the
last one stopped. Newest session first; only the 3 most recent sessions are
kept (older ones are trimmed by `scripts/wrap-session.mjs` — full history
lives in git). Written at wrap-up per the "Session handoff" section of
CLAUDE.md. These notes are public, like everything in this repo (CLAUDE.md
§ Writing in public).

## Session: 2026-10-30 — Story 2.1.17 reviewed (PASS WITH FINDINGS); Story 2.1.18 filed

- **Landed:**
  - #81 → `8ddc1519` (GitHub's merge stamp 20:48:11Z), squash with
    `--match-head-commit 9100bb44…` once `watch-pr.mjs` read it GREEN.
    - Story 2.1.17 carries its `**Reviewed:**` record: PASS WITH FINDINGS.
      #79's 23 figures reproduce from the studies' inputs, no requirement
      number or printed figure moved, and its two greps reproduce.
    - Story 2.1.18 is filed with the review's two Minors, and the manifest
      gains its `reviewedFor` line, `2.1.18` in `stories` and the prose
      check `story2117-review-fixes`.
- **Next:**
  1. Story 2.1.7, Omid's sitting, from the open draft #76 (its brief). It
     need not wait for Story 2.1.18; if the sitting decides the EU channel
     plan, a lost tag's search or [25]'s alarm rule in the rebuild spec,
     Story 2.1.18 writes the spec's answer into the study.
  2. Story 2.1.18, anywhere before Story 2.1.8.
  3. Story 2.1.8, a FRESH closer, once the spec is approved.
- **Surprises / decisions:**
  - **Minor F's premise came from the review chain itself.** The Story
    2.1.16 review suggested "the US bullet's reason at twice its distance";
    #79 followed it, and that US sentence ("Bays 30 m apart rarely hear each
    other") reads against [20]'s 30 m read range and § Range's 22.2 dB
    (INFERRED: 16.2 dB left at 60 m in free space).
  - **Minor G follows from #79's correct own-channel start.** A tag carried
    past a Reader's edge bay is found up to about 49 minutes after it is
    lost in an EU warehouse (INFERRED), unweighed against [22]'s 5 minutes
    and [25]'s hour.
  - **The register seats** (two `piped-checker-exit`, one
    `zsh-equals-face`, one `zsh-word-split`, all caught; #79's one
    `item-8-anchor-grep-misses-falsified-claims`) go to the coordinator as
    text in #81's body. No hub guard fired in this session: it was launched
    in the home folder, not the hub.
  - **The merge word:** Omid, first-hand in this terminal between its reads
    of 20:17:04Z and 20:20:02Z: "Please merge any PRs you open during this
    session when ready and in the right order."
- **Provenance:** Session id `86ac7523-9bad-4199-8c93-a5a8b64ce65b`, "Mac
  Reviewer", a reviewer seat on the fleet coordinator's orders, a fresh
  launch: its background-task path id equals its session id. Not elevated.
  It did none of Story 2.1.17's work.

## Session: 2026-10-30 — Story 2.1.17: a lost EU tag's channel search, and the inventory on shared channels

- **Landed:**
  - #79 → `185dc2db` (GitHub's merge stamp 20:28:35Z), squash with
    `--match-head-commit dbb3e7fc…` once `watch-pr.mjs` read it GREEN.
    - Story 2.1.17 is ticked, and the manifest gains its `reviewedFor` line.
      `radio-and-location.md` answers both Minors of the Story 2.1.16
      review. Minor D: a lost EU tag searches its site's channels, staying
      on its own Reader's channel until its window reaches a frame. A tag
      lost in its own Reader's area is then found within about 4 minutes in
      its first 35 minutes lost, and after that within 4 minutes at the ward
      and the conference and within 14.7 minutes in the warehouse. Minor E:
      EU Readers that share a channel run their roll calls at once, and take
      turns (8.0–8.9 minutes) if the build finds that they hear each other.
    - No requirement number or printed figure moved; `requirements.md` is
      unchanged. `npm test` 22 of 22; the close runner ten PASS and
      `rebuild-spec-approved` FAIL, as expected until Story 2.1.7.
- **Next:**
  1. Story 2.1.7, Omid's sitting, from the open draft #76 (its brief). The
     study now carries two more answers the sitting may confirm or overrule
     in the rebuild spec: the lost EU tag's channel search, and roll calls
     at once on a shared EU channel (#79's body, § For Story 2.1.7).
  2. Story 2.1.8, a FRESH closer, once the spec is approved.
- **Surprises / decisions:**
  - **The search's start is set by the window, not chosen.** A window
    narrower than a frame is timed on the tag's own network, so a search on
    another channel would meet that network only by chance. Cycling channels
    therefore starts when the window reaches a frame (INFERRED from the
    study's own drift figures; the review had not named it). That is what
    splits the own-area answer into two time regimes.
  - **Turn-taking keeps the battery** only if the beacon that opens a roll
    call names each Reader's turn, so a tag still hears one beacon and sends
    one reply. The study says so, and `parts-and-toolchains.md` § [27] is
    unchanged.
  - **The register seats** (one `piped-checker-exit`: the tick's push was
    piped into `grep`, then read unpiped by `git ls-remote`, head
    `dbb3e7fc`; the `cwd-drift` warnings of a hub-rooted seat working in the
    clone) go to the coordinator as text.
  - **The merge word:** Omid, first-hand in this terminal between its reads
    of 20:15:12Z and 20:20:45Z: "Please merge any PRs you open during this
    session when ready and in the right order."
- **Provenance:** Session id `1a792865-c18c-4244-828d-b27c1fc8206b`, a
  hub-rooted executor seat on the fleet coordinator's orders, a fresh
  launch: its background-task path id equals its session id. Not elevated.

## Session: 2026-10-30 — Story 2.1.16 reviewed (PASS WITH FINDINGS); Story 2.1.17 filed

- **Landed:**
  - #74 → `f373440e` (GitHub's merge stamp 15:52:42Z), squash with
    `--match-head-commit 7fa56213…` once `watch-pr.mjs` read it GREEN.
    - Story 2.1.16 carries its `**Reviewed:**` record: PASS WITH FINDINGS,
      none Important. One script rebuilt 58 figures from the studies'
      inputs, #72's and the sitting's, and none differs. #72's eight greps
      reproduce, and PSIP p23 reads as the new text cites it.
    - Story 2.1.17 is filed, anywhere before 2.1.8, with two Minors: which
      channels a lost EU tag's search covers (a tag lost in its own
      Reader's area may wait four searches, not one), and whether EU
      Readers sharing a channel under the 2 × 2 pattern roll call at once
      (if they take turns, about 8.0–8.9 minutes, still inside [21]'s 10).
      The manifest gains `2.1.17` and the prose check
      `story2116-review-fixes`.
- **Next:**
  1. Story 2.1.7, Omid's sitting. Nothing in this review holds it: every
     figure it reads is unchanged and reproduces.
  2. Story 2.1.17 (a fresh executor), anywhere before 2.1.8. If the sitting
     decides a lost tag's channel search or the EU channel plan in the
     rebuild spec first, the story writes the spec's answer into the study.
  3. Story 2.1.8, a FRESH closer.
- **Surprises / decisions:**
  - **Both Minors come from fixes meeting each other.** Minor A's channel
    rule changes the lost tag's time to be found, which the nit-2 sentence
    prints, and its 2 × 2 reuse meets [21]'s "readers that share a channel
    share its airtime" under Minor B's arithmetic. Each fix read alone is
    correct.
  - **A census across line breaks** found one hit #72's Minor A grep could
    not see (l.895–896, correct as it stands). A grep for a two-word phrase
    misses it where the phrase wraps.
  - **The register seats** (two `piped-checker-exit`, one
    `block-commit-hook-denies-a-variable-c-path-commit`, the `cwd-drift`
    warnings, and #72's one `wrapped-sentence-needle-undercount`) go to the
    coordinator as text. This wrap's own `cd` into the clone draws the
    cwd-drift arm once more.
  - **The merge word:** Omid, first-hand in this terminal between its reads
    of 15:50:00Z and 15:50:09Z: "Yes, merge when green (Recommended)", to
    push, open, merge on green and land the session's two wraps.
- **Provenance:** Session id `8e92bdb3-93a0-4e1c-8cf5-f688876287c4`, a
  hub-rooted reviewer seat on the fleet coordinator's orders, fresh by
  `/clear`: its background-task path id, `ee8b7871-…`, names an earlier
  session, a wrapped lawcuments-app Sprint 16 review. Not elevated.

