# Session Notes

Cross-machine handoff notes so any Claude Code session can pick up where the
last one stopped. Newest session first; only the 3 most recent sessions are
kept (older ones are trimmed by `scripts/wrap-session.mjs` — full history
lives in git). Written at wrap-up per the "Session handoff" section of
CLAUDE.md. These notes are public, like everything in this repo (CLAUDE.md
§ Writing in public).

## Session: 2026-10-27 — Story 1.1.5: the second machine's clone and reference folder

- **Landed:** #11 is Story 1.1.5. The plan's tick and the PR body are the
  record: the second machine's verify run (291 of 291, exit 0) and its
  noreply identity read-back.
- **Next:**
  - Story 1.1.6, the Epic 1.1 close, in a fresh session. It waits on
    `build-inputs-name-notices` going green, which happens when the fleet's
    sync round (hub Sprint 68) brings Story 1.1.4's hub companion (hub inbox
    #4410) here.
  - Sprint 1 legs 4–5, Stories 2.1.1 and 2.1.2. The corpus now verifies on
    both machines, so either one can run them.
- **Surprises / decisions:**
  - **A tick-only PR still touches its manifest.** `close-lint`'s lockstep
    check fails a diff that edits a story's lines but not
    `docs/close/<epic>.json`. Its suggested remedy, a `"reviewed"` date bump,
    does nothing when the field already reads today's date. #11 re-affirmed
    `1.1.json` with a `reviewedFor` entry in the form
    `Story X (PR #n, date): … No run, expect or prose changed.` Legs 4–5
    will meet the same check for `2.1.json`.
  - **The second machine's clone is partial,** as the plan asks:
    `--filter=blob:none`, full history, not sparse. Its pack is 758 MiB,
    because `--recreate` fetched the 2018 blobs.
  - **The fresh-session read:** session `54985f54-8bd1-4ecb-8f31-b4a125beafb4`.
    Its background-task path id was the same, so no `/clear` came before it.
    It ran without elevation.

## Session: 2026-10-27 — Sprint 1 legs 1–3: the corpus, the restructure, the licence lane (Stories 1.1.3, 1.1.2, 1.1.4)

- **Landed:**
  - #5 gave the pre-commit hook its executable bit;
  - #6 is Story 1.1.3;
  - #7 is Story 1.1.2;
  - #8 is Story 1.1.4;
  - #9 makes the check say "copy, never move, from a machine whose folder
    verifies" (the incident below).

  The plan's ticks and the PR bodies are the record. **In flight in the hub,
  hub-owned:** inbox #4409 (below) and #4410, which adds tigerfid to the
  canonical's `REPO_BUILD_INPUTS` (Story 1.1.4's companion).
- **Next:**
  - Story 1.1.5, the second machine's clone and corpus. The recipe is
    `docs/reference-repos.md` § Recreating. A plain partial clone is enough
    now: `main` is small and there is no sparse view to set. That machine's
    `chorus/` already reads 22 of 22 (its set-up sitting's read, relayed by
    Air Traffic Control). Its `activewaveinc/` comes from the tar in transit
    (below). Its `activewave-2018/` comes from `--recreate`.
  - Then Sprint 1 legs 4–5, Stories 2.1.1 and 2.1.2.
  - Story 1.1.6, the close, waits on 1.1.5 and on `build-inputs-name-notices`
    going green, which happens once the sync round carries #4410.
- **Surprises / decisions:**
  - **The first machine's reference folder was damaged.**
    - **Measured:** between 01:56:15Z and 01:57:54Z on 2026-10-27 it lost
      `chorus/` (22 files) and 528 files of `activewave-2018/`. The website
      capture was untouched.
    - **The cause, in Omid's words:** "I accidentally used ctrl+x instead
      of ctrl+c". The move stopped partway through the 2018 tree. #9's
      wording, "copy, never move", is the guard against a repeat.
    - **Restored:** the tree by `--recreate`, 9,477 of 9,477.
    - **`chorus/`, 22 of 22:**
      - 16 images were re-fetched pin-exact (Omid's choice). The 4 X-blog
        images are served by Google's image host at extensionless URLs,
        which a by-extension search misses.
      - `NOTES.md` was rewritten pin-exact.
      - The last 5 (the four `kn-*` Kuehne+Nagel files and
        `intro-chorus-gif-2.jpg`) are not served byte-identical by any cited
        page. They came from the second machine's verified copy by Omid's
        USB: the tar was checked by hash, then each file against its pin.
    - **The folder verifies 291 of 291** (2026-10-27T09:50:05Z).
    - **In transit:** `activewaveinc.tar` (sha256 `6425dbcf…`, 268 entries)
      is on its way to the second machine for Story 1.1.5. It will be that
      machine's copy of the capture, checked on arrival.
    - **The damaged copy** was set aside, then removed on Omid's word.
    - **Backups:** `activewaveinc/`'s only other byte-identical copy is on
      this machine, in `projects/activewaveinc-reference/` (268 of 268,
      measured). The activewaveinc.com repo holds none of it, so copying it
      to the second machine makes the first off-machine copy.
  - **The hook's mode.** `.githooks/pre-commit` was stored `100644`, because
    the fleet round staged a new file from Windows git. git on macOS ignores
    a non-executable hook, so the scan was off there. #5 set the mode to
    `100755`.
  - **The secret scan on a large deletion.** The pre-commit scan fails closed
    (`ENOBUFS`) when a commit's staged diff passes its 64 MB buffer, even when
    the diff is all deletions, which it never scans. #7's 9,472 removals
    (a 233 MB diff) went as seven commits, each one scanned; the hook was
    never skipped. Hub inbox #4409 asks for `--diff-filter=d`.
  - **The capture's file list** is pinned once, in the tsv's 268
    `activewaveinc/` rows. The 1.1.3 tick binds it with a digest rather than
    repeating 268 lines; the closer adjudicates that.
  - **The born manifest tooth** `build-inputs-name-notices` used a regex that
    could never match; #8 corrected it (both cases measured).
  - **The conduct alias.** `conduct@activewaveinc.com` is published by #7.
    Omid creates the alias (his choice, made at the merge).
  - **The local clone** is no longer sparse (`git sparse-checkout disable`
    after #7). Since the recreate, its pack holds the 2018 blobs (758 MiB).
  - **The fresh-session read:** session `1ade5972-e3f4-4596-87f5-971fd6edb515`.
    Its background-task path id was the same, so no `/clear` came before it.
    Elevation is not read on this platform.

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
