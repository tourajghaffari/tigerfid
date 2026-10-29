# tigerfid-reference — STOP before reading

Reference-only folder for the tigerfid design study. It is never a git repo
and is never committed anywhere. Each machine holds one copy, and nothing
syncs it.

**What is here:**

- `activewave-2018/` — the 2018 tree, recreated from the tag
  `activewave-2018`. It is the family's own Apache-2.0 work, so read it and
  port from it freely.
- `activewaveinc/` — the old website's pages, photos and datasheets, as
  captured on 2026-10-26. They are ActiveWave's own content, but not under
  the repo's licence: read them for study only.
- `chorus/` — the Chorus comparison's notes and images. The images are
  third-party and all rights reserved: read them for study only.
- `sources/` — the vendor documents the study's choices rest on
  (datasheets, notes and terms), each as fetched from its public URL. They
  are third-party and all rights reserved: read them for study only.

**Nothing in this folder is copied into any repo** except through
tigerfid's licence lane, with its trail, and by Omid's word.

The rules, the licence audit, the pins and the recreate recipe live in the
tigerfid repo beside this folder: `../tigerfid/docs/reference-repos.md`.
The research log is `../tigerfid/docs/reference-research.md`. Verify this
folder from that repo with `node scripts/reference-check.mjs`. How each
folder travels to another machine:

- `activewave-2018/` is rebuilt from the tag by the check's `--recreate`
  flag.
- `activewaveinc/` and `chorus/` travel by copy only.
- Each `sources/` file is fetched from its row's URL in
  `../tigerfid/docs/reference-corpus.tsv`, or copied. A fetched file counts
  only once its sha256 matches the pin. One that does not is a newer
  revision behind that URL, so copy the pinned bytes instead.

Copy a folder or a file, never move it.
