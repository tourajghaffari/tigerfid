# Reference corpus — `../tigerfid-reference/`

The design study reads three kinds of material: the 2018 original, the old
ActiveWave website, and the modern systems it compares against. They live in
a plain folder beside this repo, `../tigerfid-reference/` (in the fleet,
`projects/tigerfid-reference/`). That folder is never a git repo and is never
committed anywhere. Each machine holds one copy, and nothing syncs it. Keeping
the corpus outside the repo keeps its provenance clean by construction:
nothing in it can ride into a commit by accident.

Three versioned files hold the corpus:

- this document: the strictness dial, the reading rules, the licence audit
  and the recreate recipe;
- `docs/reference-corpus.tsv`: the pins, kept there and nowhere else. Each
  tree row is pinned by its full commit id and each file row by its sha256;
- `docs/reference-research.md`: the research log. It records what was
  searched and read, each verdict, and the searches still to run.

`node scripts/reference-check.mjs` verifies the folder against the pins, and
`npm run setup` runs it last. Adopted by plan Story 1.1.3 (hub spec
`2026-10-26-tigerfid-onboarding-design`, C4 and C11; hub KB
`new-repo-standard` row 14 and `reference-corpus-and-research-log`).

## Strictness dial — copy-with-trail, since 2026-10-26 (spec C4)

A third-party file may be copied into this repo only when three things hold:

- its licence is on the licence gate's allowlist;
- a marker sits at the copy site;
- `THIRD_PARTY_NOTICES.md` carries its entry.

The gate enforces both halves (`node scripts/check-licenses.mjs`, in CI's
`test` job; tigerian's form). The lane landed with plan Story 1.1.4
(2026-10-27) before the first copy, and its format and allowlist live in
`THIRD_PARTY_NOTICES.md`. A source whose terms are not on the allowlist, such as a vendor
reference design, goes to the plan's ASK-OMID register one by one and is
never copied silently. New work stays Apache-2.0.

Each source class has its own rule:

- **The original** (`activewave-2018/`, licence `Apache-2.0`): the family's
  own work, in this repo's own history. Read it and port from it freely; it
  needs no notice.
- **ActiveWave's website** (`activewaveinc/`, licence `ActiveWave`): the
  company's own pages, photos and datasheets. They were never released under
  this repo's licence. Read them for study. A copy into this repo is Omid's
  call, item by item, and goes through the lane with its trail.
- **Third-party** (`chorus/`'s images and PDF, and `sources/`'s vendor
  documents, licence `third-party`): all rights reserved. Study only. No
  image, text or file is copied; cite by URL and fetch date.
- **Ours** (`chorus/NOTES.md`, licence `ours`): a fleet session's own notes,
  adopted as prose into the research log.

## Reading rules — before deep-reading anything in the folder

1. **Licence first.** Read the row's licence column. For a pinned commit,
   read its licence file at the pin.
2. **Cite what informed a decision** in a spec's Sources section. Cite the
   original by its path at the tag (`activewave-2018:<path>`), a file row by
   its name and sha256 prefix, and a web page by its URL and fetch date.
   "Inspired by" is free, but it is still cited.
3. **Never interact with the upstreams.** No form, login, comment, issue or
   email: public pages only. The Chorus read used no login and no form.
4. **Nothing from the folder is committed.** A fixture or an example that
   must resemble a source is written from our own output.
5. **No medical claim is borrowed.** The old site describes hospital uses;
   the rebuild never calls itself a medical device or a monitor of vital
   signs (spec C8).
6. **Long paths.** The original's longest path is 192 characters. With the
   folder's own path in front, some exceed Windows' 260-character limit. Read
   them with a tool that supports long paths (git, Node, PowerShell 7), not
   File Explorer.
7. **`software/smart-tracker/SmartTracker  SaniFaucet.msg`** (an Outlook
   message, 12 MB) is in the folder like every other file of the original.
   It may hold correspondence, and no session opens it. Omid answered the
   plan's ASK-OMID A1 "Keep the default" at #7 (the row is in the plan's
   archive): the file left the current tree unread and stays at the tag. A
   session that needs it files a new ASK-OMID row first.

## Licence audit

### `activewave-2018/` — the original

- **Pin:** the annotated tag `activewave-2018` → commit
  `f25ff13f3fb672d8ec91f1fadc8894d69f781ed6`. It is never moved; the check
  reports `MOVED` if it ever is.
- **Licence at the pin:** Apache License 2.0 (`git show activewave-2018:license`,
  read 2026-10-27).
- **Contents:** 9,477 files, every one a regular file (no links, no
  submodules, no `.gitattributes`). No path holds a character Windows or
  macOS refuses, and no two paths differ only by case (measured 2026-10-27).
- **Recreated** by `git archive` of the pin (`--recreate`, below), with line
  endings untouched. The check reads every file back against its git blob id.

### `activewaveinc/` — the old website, captured 2026-10-26

- **The capture:** the activewaveinc.com seat pulled the live site before
  changing it, and this folder copied the parts below from that pull at
  2026-10-26T19:50:53Z. Every file's modified time is that minute. Spec C11
  set the order (the capture first, before the site's first change).
  - `rendered/` — 99 files: the 49 responses the site served, fetched
    19:43:31Z–19:43:36Z, each with its headers, plus the capture's own
    `INDEX.tsv` (URL, status, bytes, sha256, fetch time).
  - `docroot/images/` (138 files), `docroot/pdf/` (16 datasheets and the
    brochure) and `docroot/css/` (2): from the site's files, pulled
    19:48:01Z–19:49:11Z.
  - The 13 `docroot/products_datasht_*.php` page sources, whose figures the
    datasheet pages print.
- **Left out on purpose:**
  - the rest of the server-side code: it carries the site's configuration
    and credentials, which belong in no public repo and not in this folder;
  - the visitor-log folders: other people's data;
  - the form-mail and control-panel scripts, a commercial vendor's menu
    runtime, and one more script folder.
- **Integrity:** the 49 response bodies match the sha256 that `INDEX.tsv`
  recorded at fetch time (measured 2026-10-27 by Story 1.1.3). The pins of
  all 268 files are the tsv's `activewaveinc/` rows.
- **Licence:** `ActiveWave` — the company's own content, not under this
  repo's licence (the dial above).
- **Fallback:** the Wayback Machine's copies of activewaveinc.com, for a page
  this capture lacks.
- **Not taken:** rfidguardian.com (the research log has the verdict).

### `chorus/` — the Chorus comparison (spec D12)

- **The read:** 22 files from chorusview.com and its partners' public pages,
  read 2026-10-26 with no login or form.
- **Licences:** `NOTES.md` is ours, adopted into the research log. The other
  21 (images and one PDF) are `third-party`, all rights reserved, study only.

### `sources/` — the study's third-party sources (plan D8)

- **What it holds:** each datasheet, application note or reference design a
  study choice rests on, as fetched from its public URL, one row each.
  Pages read without a choice resting on them are cited by URL and fetch
  date in the research log and are not pinned. A law text is cited by its
  dated URL and is not pinned either: its version date is its pin.
- **The first rows,** added by Story 2.1.3 (2026-10-27): three Texas
  Instruments documents for the CC1312PSIP module, its certification note
  and the CC1312R7 datasheet. The research log's § 4 has their verdicts.
- **Licence:** `third-party`. TI's documents carry TI's own notice and no
  open licence, so they are read for study and never copied into the repo.
- **On another machine:** fetch each file from its row's URL, or copy it
  from a machine whose folder verifies. A fetched file counts only once its
  bytes match the pin. Vendors revise a document behind the same URL, so a
  mismatch means a newer revision, and the pinned bytes are copied instead.

## Recreating the folder on a new machine

Run these steps before any corpus-dependent review runs on that machine (KB
`new-repo-standard` row 14).

1. **Clone** as a partial clone, so only the current tree's blobs come down:
   `git clone --filter=blob:none https://github.com/tourajghaffari/tigerfid.git`
   (in the fleet, into `projects/tigerfid`).
2. **`npm run setup`** arms the pre-commit hook and runs the check, which
   reports what is missing.
3. **`node scripts/reference-check.mjs --recreate`:**
   - creates `../tigerfid-reference/` if it is absent;
   - fetches the tag's missing blobs in one batch;
   - streams `git archive` of the pin into `activewave-2018.partial/` and
     renames it into place once complete;
   - then verifies every file against its blob id.

   On the first machine (2026-10-27) this took under two minutes: 6,062
   blobs fetched (the clone's pack grew to 758 MiB) and 2.8 GB written.
4. **Copy `activewaveinc/` and `chorus/` whole from a machine whose folder
   verifies** (the first machine at adoption). Use a direct copy you
   control, such as a drive or a local network copy, and never a repo, a
   cloud share or a public link.
   - **Copy, never move.** A move that stops partway leaves the source
     damaged as well as the destination. The original's paths past Windows'
     260-character limit are one way a Windows tool stops.
   - Run the check on the source before and after the copy. On 2026-10-27
     the first machine lost its `chorus/` and 528 files of
     `activewave-2018/` within two minutes. The cause was not measured; the
     shape fits a move that stopped partway. The tree came back by
     `--recreate`, and the images by a pin-checked re-fetch where the public
     bytes still match, or from the other machine's verified copy. These folders are copy-only:
   the site they came from changes, so they are never re-fetched. `sources/`
   is fetched from its rows' URLs or copied (§ `sources/`, above). On macOS,
   strip the sidecars the copy leaves:
   `find ../tigerfid-reference \( -name '._*' -o -name '.DS_Store' \) -type f -print -delete`.
5. **`node scripts/reference-check.mjs --write-stub`** writes the folder's
   README from `docs/reference-readme-stub.md`.
6. **`node scripts/reference-check.mjs`** must exit 0 and print
   `reference-check: ok — 294 rows verified, README the stub` (291 before
   Story 2.1.3's three `sources/` rows).

Outside the fleet, step 3 gives you the original tree. The website capture
and the Chorus images exist only on the fleet's machines; the Wayback Machine
holds the site's public pages.

## Verifying a transfer

`node scripts/reference-check.mjs` prints one line per check. It exits 0 when
every row is ok, with no sidecar and the README the stub; 1 on any finding;
and 2 when the folder is absent or the usage is wrong.

- `ok tree` — every file of the pin is present at its git blob id, and none
  is extra.
- `ok files` — every file row in that folder matches its sha256.
- `MISSING tree` — run `--recreate`.
- `MISSING folder` / `MISSING file` — copy it, never move it, from a machine
  whose folder verifies. A file still served at its source may also be
  re-fetched, but it only counts once its bytes match the pin.
- `DRIFT tree` lists each path `missing`, `changed` or `extra`. `DRIFT file`
  means the bytes are not the pin's. Never edit a corpus file; copy it again.
- `UNPINNED file` — a file is in a pinned folder without a row. Add the row
  (and its research-log entry) or remove the file.
- `MOVED tag` — the local tag no longer peels to the pin. Stop and ask: the
  tag is never moved.
- `NOPIN tree` — the pin's commit is not in this clone. Run
  `git fetch origin tag activewave-2018`.
- `SIDECARS` — macOS metadata files. Strip them (step 4).
- `README` — the folder README is not the stub. Run `--write-stub`; it
  refuses to overwrite a README it does not recognize.

The tool never fetches a file row, never modifies or deletes a file that is
present, and writes nothing without `--recreate` or `--write-stub`.

## Adding to the corpus

- **A file:** put it under a folder, then add its row: the sha256, the URL
  it came from (or `-`) and its licence, read at the source. Add the
  research-log entry with its verdict in the same PR. Copying it into the
  repo itself is the lane's business (the dial above).
- **A tree:** a commit of this repo, pinned by its full id; `--recreate`
  builds it from this clone.
- **A third-party repo to clone:** the tool has no `clone` kind yet. The PR
  that adds the first clone adds the kind too, in tigerian's shape (a
  shallow clone checked out at its pin, verified by `HEAD`).

## The folder README

`docs/reference-readme-stub.md` is the folder README's text, byte for byte.
The check fails until the two match, and `--write-stub` writes it. The
adoption-era README the capture left in the folder (its record of what was
taken and what was left out) is carried into the licence audit above. The
stub replaced it at Story 1.1.3.
