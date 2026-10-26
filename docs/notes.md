# Session Notes

Cross-machine handoff notes so any Claude Code session can pick up where the
last one stopped. Newest session first; only the 3 most recent sessions are
kept (older ones are trimmed by `scripts/wrap-session.mjs` — full history
lives in git). Written at wrap-up per the "Session handoff" section of
CLAUDE.md. These notes are public, like everything in this repo (CLAUDE.md
§ Writing in public).

## Session: 2026-10-26 — adopted into the fleet (Story 1.1.1; hub Story 243.1.2)

- **The adoption:** before the onboarding PR, on GitHub — the settings
  family, the annotated tag `activewave-2018` on the 2018 head with its
  release, the default branch renamed `master` → `main`, and the
  `protect-main` ruleset. The onboarding PR (Story 1.1.1) carries CLAUDE.md,
  this file, the plan with Sprint 1, the archive, the close protocol and two
  born-failing manifests, CI (`scope` + `test` on `ubuntu-latest`) with its
  contract doc, `package.json` (no dependencies) and `.npmrc`, `.gitignore`,
  Dependabot for the Actions pins and the session settings; the synced
  scripts and the pre-commit hook arrived first, by the fleet round #3. Its
  body is the adoption record.
- **Next:** Sprint 1 leg 1 — Story 1.1.3, the reference corpus (adopting the
  old website capture already taken) (plan § Sprint 1); then Stories 1.1.2, 1.1.4,
  2.1.1 and 2.1.2, in that order.
- **Surprises / decisions:**
  - **The website capture is already done.** The old website's pages, photos and
    datasheets were captured on 2026-10-26, before the site changed, into the local
    reference folder (spec C11). Story 1.1.3 adopts that capture rather than re-taking
    it.
  - **An older release exists.** `1.0.0` (2017-02-10) is the system "as he left it when
    he passed". `activewave-2018` is the newest release, and its notes link to `1.0.0`
    (Omid's ruling).
  - **Sparse checkouts until Story 1.1.2.** This clone and CI skip the four large 2018
    folders until Story 1.1.2 removes them from the tree (CLAUDE.md § CI).
