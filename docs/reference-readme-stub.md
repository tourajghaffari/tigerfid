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

**Nothing in this folder is copied into any repo** except through
tigerfid's licence lane, with its trail, and by Omid's word.

The rules, the licence audit, the pins and the recreate recipe live in the
tigerfid repo beside this folder: `../tigerfid/docs/reference-repos.md`.
The research log is `../tigerfid/docs/reference-research.md`. Verify this
folder from that repo with `node scripts/reference-check.mjs`. Its
`--recreate` flag rebuilds `activewave-2018/` from the tag; the other two
folders travel by copy only.
