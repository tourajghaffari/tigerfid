# Third-party notices — tigerfid

New work in this repo is Apache-2.0 (`license`). This file is the audit trail
for every piece of third-party work copied into it, under the copy-with-trail
dial (`docs/reference-repos.md`; hub spec
`2026-10-26-tigerfid-onboarding-design`, C5). A copy is allowed only from a
source whose licence is on the allowlist below, and only with both halves of
the trail:

1. **A marker at the copy site:**
   `Derived from <project> (<licence> © <author>) — see THIRD_PARTY_NOTICES.md`.
   It goes in a comment, in the copied file's own syntax. A file that cannot
   carry a comment (a KiCad file, a PDF, an image) gets a plain-text file
   beside it, named `<file>.license`, holding the marker line.
2. **An entry under `## Entries`.** Its heading is `### <project>`, with the
   same name exactly, and it carries these five lines in this order, before
   the next heading:
   - `Source:` <the repository or page it came from, as a URL>
   - `Pin:` <a commit URL carrying the full 40-hex revision, or
     `sha256:<64 hex>` of the file as fetched>
   - `License:` <SPDX id> (© <author>)
   - `Taken:` <what was taken, in words>
   - `Landed:` <the repo path or paths where it lives>

**The allowlist** (SPDX ids):

- **for code and documents:** MIT, Apache-2.0, BSD-2-Clause, BSD-3-Clause,
  ISC, 0BSD, Unlicense and CC0-1.0, which are tigerian's;
- **widened for hardware sources:** CERN-OHL-P-2.0 and CC-BY-4.0.

Anything else is never copied, however small the piece: GPL, LGPL, AGPL, MPL,
the reciprocal CERN-OHL-S and CERN-OHL-W, CC-BY-SA, and a source with no
licence at all. A vendor reference design whose terms are not on the list
goes to the plan's ASK-OMID register, one design at a time, and is never
copied silently. Translation into another language or another tool is still
a copy. A source the reference corpus marks `study-only` is never a copy
source.

**The gate** is `node scripts/check-licenses.mjs`, in CI's required `test`
job. It enforces both directions:

- a marker without an entry fails;
- an entry without a marker fails;
- a mention of this file's name that is not a well-formed marker fails;
- a marker whose licence or author differs from its entry fails.

It also checks each entry's shape and its licence against the allowlist, and
it refuses a source the corpus marks `study-only`.

## Standing notes

### The KiCad standard libraries — CC-BY-SA-4.0, with the libraries' design exception

Recorded here once (spec C5). The rebuild's boards are drawn in KiCad 10. Its
standard libraries are licensed under CC-BY-SA-4.0, with this exception
(https://www.kicad.org/libraries/license/, read 2026-10-27):

> To the extent that the creation of electronic designs that use 'Licensed
> Material' can be considered to be 'Adapted Material', then the copyright
> holder waives article 3 of the license with respect to these designs and
> any generated files which use data provided as part of the 'Licensed
> Material'.

**What that means here:**

- A design that uses the library parts stays this repo's Apache-2.0 work and
  needs no entry. That covers the schematics, the boards, the parts KiCad
  embeds in them, and their generated outputs, such as Gerbers, drill files,
  BOMs and plots.
- A library file itself, or a library of our own derived from one, is
  CC-BY-SA-4.0 material. CC-BY-SA-4.0 is not on the allowlist, so such a
  copy goes to ASK-OMID.

## Entries

(None yet: the lane was born on 2026-10-27 with zero entries, before the
first copy.)
