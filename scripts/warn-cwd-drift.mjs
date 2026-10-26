#!/usr/bin/env node
// warn-cwd-drift.mjs — PreToolUse hook: WARN (never block) when a shell-tool
// command (Bash, or the PowerShell tool — Story 73.1.1) changes directory
// into a sub-repo under projects/ (Story 24.3.20).
//
// Why this exists — the two-occurrence citation (the ≥2× rule, Story
// 24.3.1's pattern): the persisted-cwd class has bitten twice. (1) The first
// incident was a FAILED compound containing `cd` — the shell's cwd persisted
// past the failure and later repo commands ran in the wrong repo; it produced
// the machine-local operating rule, whose predicate covered failed compounds
// only. (2) Sprint 14 F2 (2026-08-01) was a SUCCESSFUL
// `cd projects/lawcuments-app` (2026-08-01, under the repo's pre-rename name) that persisted into the next leg
// command and created the leg branch in the WRONG repository (near-miss:
// zero commits landed; detected one command later by two impossible-looking
// read results). The narrow predicate was the defect: a successful `cd`
// persists just as durably as a failed one. This guard warns on ANY cd into
// projects/, successful or not — the match is static, so success never
// enters into it.
//
// Third and fourth seats (the CONCURRENT face — folded 2026-09-04): the
// shell is ONE mutable cwd shared by every caller. (3) 2026-08-23, the
// vituno v0.14.1 consumer wave: four concurrent fork agents plus the parent
// shared it, and three agents saw early commands run in the WRONG repo (KB
// sdd-execution-frictions item 5). (4) 2026-09-04, the hub Epic 71.1 close,
// no agents at all: five Bash tool calls issued in ONE response ran through
// the shell one after another, and a `cd projects/expeal-app && …` in one of
// them drifted a SIBLING call's archive read to expeal-app's file — this
// guard fired on the cd and could not help, because the sibling was already
// queued. The cd is not the hazard; the NEXT command trusting the cwd is.
//
// SECOND ARM (Story 72.1.1, landed 2026-09-04) — the next call: the payload's
// `cwd` is the shell's PERSISTED working directory at the moment this hook
// fires (KB hook-mechanics), so when it resolves to a different repository
// (nearest `.git` — directory or worktree file) than the session root
// (CLAUDE_PROJECT_DIR, else two levels above this file), the arm warns,
// naming both paths and the pin-it remedies. That is the moment a sibling
// call's or an agent's move would misfire — the moment the first arm cannot
// see. Suppressed when the call's first stage is itself `cd`/`pushd`; a cwd
// inside the root's own repository is not drift; a missing/relative cwd, a
// vanished cwd, or a cwd with no `.git` above it stands down loudly. Both
// arms can fire on one call — one JSON, contexts joined.
//
// THIRD ARM (Story 132.1.1, hub Initiative 132 — Omid's "Let's do that hook
// for MSYS", 2026-09-30): the path a command passes, not the cwd it moves.
// Git Bash converts a POSIX path argument (`/c/Users/…`) to its Windows form
// before a native program sees it — unless MSYS2_ARG_CONV_EXCL excludes the
// argument, which sessions set to keep a `rev:path` colon argument intact (KB
// windows-git-bash-cli-gotchas §6). Under `*` the exclusion is total, so the
// same line's `git -C /c/…` reaches git.exe literally and fails "cannot change
// to" — silently behind `2>/dev/null`, where a following count reads the
// empty stream as 0. Register class msys-excl-blocks-posix-c-path: its KB
// entry missed at every post-decision seat but the last (seats 2–4
// caught:false — tigerian, expeal-app, the hub; seat 5, expeal-app again,
// caught by its visible fatal line), so the class is re-decided to this arm.
// It WARNS, Bash only (the PowerShell tool has no MSYS layer), when a stage
// runs under the exclusion — its own prefix assignment, or an earlier
// assignment-only stage or `export` on the line — and one of its arguments is
// a /c/-form path the exclusion's value covers: literal, a flag's `=/c/…`, or
// through the line's own `NAME=/c/…` assignment or `for NAME in /c/…` list.
// Accepted limits: a variable set by an earlier call, a `$(…)`-built value, or
// an expansion beyond naming the variable is not followed (seat 4's shape).
//
// FOURTH ARM (Story 144.1.1, hub Initiative 144 — Omid's "Deny backticks, warn
// paths (Recommended)", 2026-10-01): a Bash-tool `node -e`/`--eval`/`-p`/
// `--print` (bare, or after `mise exec --`) whose program is DOUBLE-quoted is
// read by the shell before node starts. (1) The backtick face DENIES: an
// unescaped backtick pair in the program runs as a command substitution —
// markdown backticks around a repo path ran three files as shell scripts
// (register class double-quoted-node-eval-backtick-runs-files, pantheon Sprint
// 15 F10). A warn would arrive after the command ran, so this face denies.
// (2) The path face WARNS on win32
// (WARN_CWD_DRIFT_PLATFORM pins it in tests): a /c/-form path written into the
// program, or a `$NAME/` naming a `NAME=/c/…` assignment earlier on the line,
// reaches node as text and resolves to C:\c\… (KB windows-git-bash-cli-gotchas
// §23; class posix-path-inside-node-eval-string). (3) The apostrophe face
// DENIES (Story 179.1.1, hub Initiative 179): an apostrophe inside a
// SINGLE-quoted program closes it early, and Bash reads the rest of the line as
// shell (class single-quoted-node-eval-closed-by-apostrophe). It fires on a
// single-quoted part followed directly by a bare part opening with a word
// character; a `'…'"$X"'…'` or `'…'$X'…'` splice and the `'\''` idiom stay
// silent. Accepted limits: an apostrophe before whitespace or punctuation,
// `-e'…'` glued to its flag, `xargs node -e`, `bash -c "node -e '…'"`. When
// both deny faces fire, their reasons join. Silent: a whole single-quoted
// program, an escaped backtick (\`), a `$(…)`, a path passed as an argument, a
// heredoc body, any command that is not node, and the PowerShell tool (its
// backtick is an escape). The arm reads words with its own quote-aware scanner
// (one linear pass), since the shared neutralizer unwraps a double-quoted span
// that holds a backtick. Accepted limit: a `"` nested in a `$(…)` inside the
// program ends the span early.
//
// FIFTH ARM (Story 172.1.1, hub Initiative 172 — Omid's "Let's follow your
// recommendation", 2026-10-07): a git `rev:path` argument Git Bash rewrites.
// MEASURED on the Windows box (the 172.1.1 PR's shape table): a ref holding a
// `/` (origin/main, refs/heads/main, origin/HEAD), a colon, then a path that
// starts with `.` but not `./` reads to MSYS as a POSIX path list and reaches
// git.exe as `origin\main;.github\x.yml` — git fails "ambiguous argument" or
// "Not a valid object name", silently behind `2>/dev/null`, where an existence
// probe reads "absent" (register class msys-colon-rev-path-arg-mangled, seat 2:
// fourteen repos read NO). A ref with no `/` (HEAD, main, a sha, @{u}), a
// `./`-led path and an undotted path are left alone, so they stay silent. It
// WARNS, Bash only and on win32 only (WARN_CWD_DRIFT_PLATFORM pins it in
// tests; the Mac has no MSYS layer), when a `git` stage carries such an
// argument and no MSYS2_ARG_CONV_EXCL on the line covers it — read by arm 3's
// stage walker, so one parser decides what the exclusion reaches. A path that
// starts with a variable the line does not resolve (seat 1: `"origin/main:$f"`
// over an ls-tree loop) warns too, as a "may": the hook cannot see the value.
// THE DENY (Story 215.1.1, hub Initiative 215 — Hub Sprint 56 D4, Omid's
// "Approved" of 2026-10-19): the MEASURED face is DENIED since seats 6–7 of
// the class — one session, two dotted-path reads on 2026-10-19,
// `git show origin/main:.github/workflows/ci.yml` → `fatal: ambiguous argument
// 'origin\main;.github\workflows\ci.yml'` and `origin/main:.rubocop.yml` the
// same; the warn fired both times and governed neither (the vituno Sprint 13 /
// lawcuments-app Sprint 14 reviewer pass's recommendation, routed by Air
// Traffic Control). The deny costs one re-issued line with the cure the reason
// names (MSYS2_ARG_CONV_EXCL='*' on the line with C:/ paths, or the PowerShell
// tool; `git ls-tree --name-only <ref> <dir>/` for an existence probe) and
// joins the other denies at the one emitter. The variable "may" face stays a
// WARN: a deny there would deny every loop over a variable the hook cannot read.
//
// SIXTH ARM (Story 190.1.1, hub Initiative 190 — the Hub Sprint 50 reviewer
// pass's routing of register class msys-leading-slash-url-arg-rewritten's seat
// 12, REVISITED 2026-10-10): a leading-slash argument to `wsl` / `wsl.exe` Git
// Bash rewrites. MSYS converts ANY argument that begins `/` to a Windows path
// under the Git install prefix before a native program sees it; every such
// argument to wsl is a Linux-side path, so there the rewrite is always wrong —
// MEASURED on the Windows box: seat 11 (`wsl.exe -d Ubuntu -- bash
// /mnt/c/…/run-close.sh` reached WSL as `C:/Program Files/Git/mnt/c/…`, "bash:
// No such file or directory", the close runner "ran" in zero seconds behind a
// `| tee`) and seat 12 (`wsl -e /home/…/heroku releases …` reached WSL as
// `C:/Program Files/Git/home/…`, execvpe "No such file or directory"). The
// class's decision DECLINED a generic leading-slash warn (it would fire on every
// legitimate absolute-path argument); this NARROW arm cannot false-fire that
// way. It WARNS, Bash only and on win32 only, when a stage whose verb's basename
// is `wsl` or `wsl.exe` carries an argument that (after the line's own variables
// resolve) begins `/`, or a `--flag=/…` value, and no MSYS2_ARG_CONV_EXCL on the
// line covers it — the third visitor of arm 3's stage walker. Silent by design:
// a lone `/`; a `//`-led spelling (MSYS strips one slash — `//home/x` reaches
// wsl as `/home/x`, the escape form; register seat 10); a `/<letter>/…` drive
// path (converted CORRECTLY — `wsl --import/--export/--mount/--cd` take Windows
// paths); a path inside a quoted script argument (`bash -lc 'cd /home/x …'`
// begins with a word, so MSYS leaves it); a `$VAR` the line does not resolve;
// the PowerShell tool and the Mac (no MSYS layer). Warn-level: a wsl line is
// legitimate; the hazard is the rewrite, and the PowerShell tool is one tool
// away (KB rails-on-windows-wsl-not-native § The harness relay; KB
// windows-git-bash-cli-gotchas §18 names both cures).
//
// SEVENTH ARM (Story 202.1.1, hub Initiative 202 — the register's decision at
// seats 3–4, 2026-10-12): a grep pattern whose bounded repetition the Mac's
// harness grep refuses. The Mac's harness `grep` is ugrep: a pattern carrying
// a bounded span (`{m,n}` or `{m,}`) over a dot, a class or a group fails
// `ugrep: error: … exceeds complexity limits` and prints NOTHING else, so an
// extraction reads as EMPTY, never as failed — four reads in eight days
// (`[^.]{0,60}…[^.]{0,40}` 2026-10-04; `.{0,60}#3550.{0,60}` and
// `.{0,50}(…).{0,50}` 2026-10-11; `.{0,160}R1.s retry.{0,200}` and
// `.{0,200}deccentral-app #615.{0,250}` 2026-10-12, the second lost inside a
// pipeline), each caught by eye and by no guard; `LC_ALL=C` does not clear it
// (MEASURED 2026-10-11). It WARNS, Bash tool and darwin only (the platform read
// as the node-eval arm reads it; WARN_CWD_DRIFT_PLATFORM pins it in tests),
// when a stage whose verb's basename is `grep` or `egrep` (`fgrep` and `ggrep`
// excluded — fixed strings, and GNU grep by name) carries a pattern — the `-e`
// / `--regexp` value when present, else the first word after the verb that is
// neither a flag nor the VALUE of a value-taking flag (`-A`, `-B`, `-C`, `-m`,
// `-d`, `-D`, `-f`, `--include`, `--exclude`, `--max-count` and their kin — a
// naive first-non-flag read of `grep -m 1 -e …` would take `1`) — with `{m,n}`
// or `{m,}` immediately after `.`, a `]` or a `)`: the fourth visitor of arm 3's
// stage walker. Silent by design: a fixed count `{n}`; `-F` / `--fixed-strings`;
// a `{m,n}` inside any other word (an `echo`, a file name); a heredoc; win32 and
// linux; the PowerShell tool. Warn-level, no permissionDecision: the message
// carries `ugrep guard`, the offending span, the needle-alone rewrite
// (`grep -n 'needle' <file>`, then read the line) and the sentence that an
// empty extraction under the error is UNMEASURED, not absent. The reading form
// is CLAUDE.md § scripts/ (the ugrep paragraph); the register class
// ugrep-bounded-repetition-exceeds-complexity-limit holds the seats.
//
// EIGHTH ARM (Story 214.1.1, hub Initiative 214 — the register's decision of
// 2026-10-19, hub #3791, re-decided hook by this story; Hub Sprint 56 leg 1): a
// stdin-fed interpreter. A `python3 - <<'EOF'` run as a background Bash by
// tigerian's Task 7 implementer (23:18Z 2026-10-18) never received EOF — the
// tool mangled the heredoc delimiter — and python3 busy-looped at 100% of one
// core for 11.4 h (40,680 s of CPU) beside every timed battery of a
// twelve-session night, found by a process read at 10:41Z the next morning,
// never by the session that started it; the reproduction at 10:43Z the same
// (register class subagent-background-shell-left-a-runaway-process-beside-
// timed-batteries holds the three seats). It DENIES, both tools and both
// platforms: a stage whose command word — bare, by path, `.exe`, or after
// `mise exec --`, past assignments, keywords and wrappers — is an interpreter
// (python, python2/3, python3.x, py, node, ruby, perl, php) and whose
// arguments carry a BARE `-` program word (`python3 -`, `node -`,
// `python3 -u -`, `python3 - "$@" <<EOF`, `cat x | python3 -`, a here-string
// piped into `python3 -` in the PowerShell tool). A warn would reach the model
// after the interpreter is already waiting — the damage is the wait itself —
// so the face is denied like arm 4's two. Silent by design: a program by path;
// `-c`/`-e` inline programs (arm 4's and the CLAUDE.md inline rule's); a `-`
// to a non-interpreter (`cat -`, `tar -xf -`); a `-` inside quoted data, a
// heredoc body or a here-string body; `--`, `-x` and `-m` flags. Accepted
// limit: an interpreter inside a quoted `bash -lc '…'` or `wsl … bash -lc '…'`
// program is DATA to the stage reader — the CLAUDE.md rule is the reading form
// there. The reason names the remedy (write the script to a file with the
// Write tool and run it by path), the CLAUDE.md paragraph ("No stdin-fed
// interpreter through the Bash tool, either box"), the register class and
// this story; it JOINS arm 4's reason when both fire (the Story 179.1.1 join
// at the one emitter).
//
// NINTH ARM (Story 219.1.1, hub Initiative 219 — the register class's own
// reopen clause fired at seat 7, hub #3865, 2026-10-20; Omid's go relayed by
// Air Traffic Control): a fleet repo's GitHub org GUESSED instead of derived.
// Seven seats in fourteen days (2026-10-06 → 2026-10-20), every one
// `tourajghaffari/<a Tesibus repo>` — `gh run view --repo`, `gh pr create
// --repo` after the push (the commit landed, the PR did not), a three-PR CI
// watcher's loop, a close's pre-read `gh pr list -R`, five close-time PR
// reads under a guessed `Tesibus/…`, and two peer messages naming the wrong
// org — each refused by gh ("GraphQL: Could not resolve to a Repository …")
// and re-derived by hand; the KB entry (github-org-per-repo-derive-from-remote)
// governed none of the first reads (register class
// gh-repo-org-guessed-not-derived, `caught: false` at every post-decision
// seat but the last). The arm DERIVES: for a slug `<org>/<name>` it finds
// `<name>` — the session root's own clone when its origin names that repo,
// else `projects/<name>` under the root or any ancestor of it (a site-rooted
// session reaches up to the hub's projects/) — and reads the owner from
// `git -C <clone> remote get-url origin` (https, scp and ssh forms), only
// over a directory with its OWN .git: MEASURED 2026-10-20, a plain folder
// (`projects/tigerdash-reference`) walks git UP to the hub and prints the
// hub's URL, so it is judged nothing. It DENIES, both tools and both
// platforms, when the written org differs from the derived one (compared
// case-insensitively — GitHub owners are): a gh stage's `-R`/`--repo` value
// (spaced, `=`-joined, glued or quoted; `OWNER/REPO`, `HOST/OWNER/REPO` or a
// URL), a `GH_REPO=` assignment on the line, a `gh api` `repos/<org>/<name>`
// path, a `gh repo <sub>` positional, a github.com URL word to gh, or a BARE
// github.com URL word in any other stage (a clone or a remote by URL). A
// guessed owner either 404s — the error reads like a permissions problem — or,
// for a same-named repo under the other org, RESOLVES to the wrong repository:
// a warn would be wasted in the first case and late in the second, so the
// face denies; the cost is one re-issued line with the right slug in the
// reason. Silent by design: a name with no clone to derive from (not under
// projects/, not the root); a clone whose origin is not GitHub; a quoted
// mention (a PR body, an echo); a heredoc body; a bare `org/name` word
// outside a gh stage (only a github.com URL is read there); an owner spelled
// through a variable the line does not resolve; a gh inside a quoted
// `bash -lc '…'` program (data to the stage reader). The reason names the
// argument as written, the derived slug, the clone's origin, the derivation
// command, the KB entry, the register class and this story; it joins the
// other denies at the one emitter.
//
// THE POWERSHELL TOOL (Story 73.1.1): the harness's PowerShell tool — the
// Windows box's primary shell — is a different tool name, so until this
// story neither arm judged it (register: cwd-drift seat 5). Its payload has
// the same shape (tool_name "PowerShell", tool_input.command, cwd = the
// PowerShell process's persisted cwd — measured 2026-09-04), so the second
// arm works unchanged; the first arm learns the shell's cd verbs
// (Set-Location / sl / chdir / Push-Location beside cd/pushd, compared
// case-insensitively, `-Path`/`-LiteralPath` skipped as flags), here-strings
// (`@'…'@` / `@"…"@`) are dropped as data, and a double-quoted span's
// backtick is an escape rather than substitution (`$(` stays live). Accepted
// limits: the stage splitter still cuts on backticks (a backtick line
// continuation splits a stage — errs toward warning, never silence);
// `-Path:<value>` colon-joined arguments and the call operator `& "…"` are
// not parsed; a here-string body line that merely looks like the terminator
// ends it early. The wiring widens to `Bash|PowerShell` in Story 73.1.2.
//
// Warn-level BY DESIGN (per the verification-loop taxonomy: standalone,
// session layer, warn — never a gate): cd-ing into a sub-repo is legitimate
// (site-rooted work sessions live there); the hazard is the NEXT command
// trusting the implicit cwd. The hook therefore emits NO permissionDecision —
// the command proceeds — with FIVE exceptions, the fourth arm's backtick face
// (Story 144.1.1), its apostrophe face (Story 179.1.1), the fifth arm's
// measured dotted-path colon face (Story 215.1.1), the eighth arm's
// stdin-fed interpreter (Story 214.1.1) and the ninth arm's guessed org
// (Story 219.1.1), denied because the damage is the execution itself (the
// colon face: a read that fails silently; the org face: a wrong-repo read); every
// arm surfaces `additionalContext` (reaches the model: use `git -C <abs>` /
// absolute paths for subsequent repo commands) plus a `systemMessage`
// (reaches the user).
//
// MATCHER CONTRACT (shares the neutralize approach with
// block-commit-on-main.mjs / warn-piped-checker.mjs): heredoc bodies are
// dropped and quoted spans neutralized to placeholders (a double-quoted span
// containing `$(` or a backtick stays live), so `echo "cd projects/x"` and
// `grep 'cd projects/'` never fire while a genuine `cd "projects/x"` still
// does (placeholder contents resolve for the target check). The line splits
// into stages on && || ; & newline, parens, braces, backticks; a stage fires
// iff its command-position word (after VAR=x assignments and wrapper words)
// is `cd`/`pushd` and its first non-flag argument resolves to a path with a
// `projects` SEGMENT (`projects/…`, `./projects/…`, `/abs/…/projects/…` —
// never `projects-archive/`). The arms run at module top level and ONE
// verdict JSON is printed last, so a throw inside a later arm used to forfeit
// every earlier arm's verdict — Node's exit 1 with no stdout reads as
// NON-blocking to the hook law and the denied command ran (MEASURED by the
// Hub Sprint 59 reviewer pass, 2026-10-22, on a scratch copy with a throw at
// the tenth arm's 16:14:13Z crash point; register class
// later-arm-throw-forfeits-the-guards-accumulated-deny). Since Story 233.1.1
// (hub Initiative 233) an uncaughtException handler registered before the
// first arm prints the messages, contexts and denyReason gathered so far plus
// one line naming the arm that threw (`drift guard: arm N (…) threw`, the
// error's message, `— the verdict above is the arms that ran`), and exits 0: a deny gathered
// before the throw rides as a deny; a throw before any arm prints the one line
// and exits 0 (fail-open for the warn arms, never silent). Accepted limit: a
// second throw inside the handler itself exits 1 — Node's default.
//
// Known limits (accepted, warn-level): a cd inside a quoted script argument
// (`bash -c "cd projects/x && …"`) is data to the matcher; variable
// indirection (`d=projects/x && cd $d`) never presents the segment; and a
// double-quoted span containing an ESCAPED backtick (\`) reads as live
// substitution, so a quoted MENTION with escaped backticks can fire
// (observed live on this guard's own rollout PR body — err-toward-warning,
// harmless at warn level; the donor guards share the behavior). The
// backstop is the operating rule this guard exists to reinforce, not
// replace.
//
// TENTH ARM (Story 220.1.3, hub Initiative 220 — the register class
// gh-rest-core-quota-exhausted-by-pr-watch-polling's decided machine form): a
// seconds-cadence `gh` poll loop. GitHub's REST primary limit is 5,000 calls an
// hour per USER — one quota for every session on both boxes — and on 2026-10-21
// it ran out at ~15:5xZ (core 5000/0, reset 16:14:27Z; two seconds-cadence PR
// watchers among the day's shape): every gh read on both boxes failed until the
// reset and a hand heartbeat printed 33 FAILED-TO-DERIVE lines that looked like
// a drift stack. A `while`/`until`/`for` loop (the PowerShell tool's `while`/
// `for`/`foreach`/`do` faces too) whose span carries a `gh` at command position
// and a sleep under 60 s — or no sleep — WARNS, naming the quota and the three
// alternatives (poll ≥ 60 s; the run-anchored watch `node scripts/fleet/watch-pr.mjs`
// with the PR number — Story 236.1.2, in place of `gh pr checks --watch`, which exits
// 1 on "no checks reported" before the head's run registers, KB
// gh-pr-checks-watch-force-push-race; the branch over SSH with
// `git ls-remote --heads origin`). Never a deny: a deliberate short poll with a
// cap is legal when the budget is read first (KB gh-rest-quota-shared-across-
// sessions rule 1). Silent: a loop sleeping 60 s or more (`sleep 1m` is sixty), a
// loop over `git ls-remote` with no gh word, a lone gh call outside any loop, a
// gh word that is quoted data. The span runs from the keyword to its `done`
// (Bash) or matching close brace (PowerShell) — or to the end when cut off — and
// includes the condition, so `until gh pr checks …; do sleep 15; done` counts.
// THE LOOP-HEAD RULE (Story 233.1.2, hub Initiative 233 — register class
// poll-cadence-arm-warns-on-a-bounded-enumeration's decided form; MEASURED by
// the Hub Sprint 59 reviewer pass, 2026-10-22: the fleet's commonest legal gh
// loop, `for n in 3955 3964; do gh pr view $n; done`, drew the warning): KB
// rule 1's class is a POLL — the same read repeated faster than once a minute —
// so the arm reads the loop's HEAD. A `for VAR in` over literal words (no `$(`,
// no backtick, no `seq`, no `{a..b}`; a `$NAME` list counts as literal) or a
// `while read …` is BOUNDED; a bounded loop whose every gh stage carries `$VAR`
// and sleeps 60 s or not at all is an ENUMERATION — silent; a bounded loop
// reading gh with no `$VAR` is a BURST — warned with its count ("reads gh 3
// times with no sleep"); a gh only in a `for`'s word list is one call (the body
// judged alone); `while true|:`, `until`, `for ((…))` and a generated list keep
// warning. A nested span closes at the `done` that matches its `do` by depth; a
// sleep the arm cannot read (`sleep "$N"`, `${N}`, `$((…))`) is named "with a
// sleep the guard cannot read", never "with no sleep", and a quoted literal
// (`sleep "90"`) is READ; `timeout` and `xargs` are wrappers before gh (their
// option shapes at WRAPPERS). THE DERIVED VARIABLE (Story 242.1.4, hub Initiative
// 242 — the same class's derived-variable seats, 2026-10-23 to 2026-10-26: a gh
// stage reading `$n` or `$2` set from the loop variable in the body warned as a
// burst): the loop's variable set grows to a fixed point over the body — an
// assignment whose value expands a member (`r=${p%%:*}`, `n="${p##*:}"`,
// `s=$(git -C projects/$r …)`, backticks), `set -- $p` (the positionals) and `read
// A B <<< "$p"` (its targets) — so a gh stage carrying any member is the
// enumeration. Named limits: the PowerShell faces keep the shape
// predicate; a bounded enumeration sleeping under 60 s keeps the cadence warning
// (the register's decided form); a bare `done` word as an argument (`echo done`)
// closes the span early — the sleep after it falls outside, err-toward-warning;
// `watch -n 5 gh …` has no loop keyword and is outside the predicate; a derived
// variable that does not vary per iteration (`n=${i:+42}`) reads as varying — the
// arm errs toward silence there (warn-only), and a single-quoted `'$p'` value is
// read as an expansion too.
//
// Malformed stdin: stand down loudly (stderr note + exit 0) — a warn-level
// guard with no readable payload has nothing to warn about.
//
// Wired from each repo's own .claude/settings.json (PreToolUse, matcher
// "Bash" — "Bash|PowerShell" once Story 73.1.2 lands, exec form). Rollout
// (the 24.3.17 pattern, decided in the Story
// 24.3.20 tick): the script is fleet-synced via the CANONICALS roster;
// each repo's settings wiring lands once by the rollout PRs, Expeal first
// (KB expeal-default-and-canary-inversion: protections reach Expeal FIRST).
//
// Test manually (arm 1, then arm 2 — the second needs an absolute cwd):
//   echo '{"tool_name":"Bash","tool_input":{"command":"cd projects/vituno && git status"}}' \
//     | node scripts/warn-cwd-drift.mjs
//   echo '{"tool_name":"Bash","cwd":"'"$PWD"'/projects/vituno","tool_input":{"command":"git status"}}' \
//     | CLAUDE_PROJECT_DIR="$PWD" node scripts/warn-cwd-drift.mjs
//   The MSYS exclusion face (arm 3; Story 132.1.1):
//   echo '{"tool_name":"Bash","tool_input":{"command":"MSYS2_ARG_CONV_EXCL=* git -C /c/Users/x show origin/main:a.md"}}' \
//     | node scripts/warn-cwd-drift.mjs
//   The rev:path face (arm 5; Story 172.1.1 — win32 only):
//   echo '{"tool_name":"Bash","tool_input":{"command":"git show origin/main:.gitattributes"}}' \
//     | WARN_CWD_DRIFT_PLATFORM=win32 node scripts/warn-cwd-drift.mjs
//   The wsl face (arm 6; Story 190.1.1 — win32 only):
//   echo '{"tool_name":"Bash","tool_input":{"command":"wsl -e /home/x/bin/tool --flag"}}' \
//     | WARN_CWD_DRIFT_PLATFORM=win32 node scripts/warn-cwd-drift.mjs
//   The ugrep face (arm 7; Story 202.1.1 — darwin only):
//   echo '{"tool_name":"Bash","tool_input":{"command":"grep -n -o -E \u0027.{0,160}needle.{0,200}\u0027 docs/notes.md"}}' \
//     | WARN_CWD_DRIFT_PLATFORM=darwin node scripts/warn-cwd-drift.mjs
//   The node-eval faces (arm 4; Story 144.1.1 — the backtick deny, then the win32 path warn):
//   echo '{"tool_name":"Bash","tool_input":{"command":"node -e \"x(`a` + `b`)\""}}' \
//     | node scripts/warn-cwd-drift.mjs
//   echo '{"tool_name":"Bash","tool_input":{"command":"H=/c/u && node -e \"f('$H/a')\""}}' \
//     | WARN_CWD_DRIFT_PLATFORM=win32 node scripts/warn-cwd-drift.mjs
//   The apostrophe face (arm 4; Story 179.1.1 — denied):
//   printf '%s' '{"tool_name":"Bash","tool_input":{"command":"node -e '"'"'x(\"it'"'"'s\")'"'"'"}}' \
//     | node scripts/warn-cwd-drift.mjs
//   The fault path (Story 233.1.1 — a planted throw on a scratch COPY; the wired guard cannot be made to throw):
//   sed 's/for (const span of spans) {/& throw new Error("planted");/' scripts/warn-cwd-drift.mjs > "$S/warn-cwd-drift.mjs" \
//     && echo '{"tool_name":"Bash","tool_input":{"command":"node - && while true; do gh pr view 1; sleep 5; done"}}' \
//     | node "$S/warn-cwd-drift.mjs"; echo "exit $?"   # $S a scratch dir: exit 0, the deny, the `threw` line
//   The poll-cadence face (arm 10; Story 220.1.3 — warn only):
//   echo '{"tool_name":"Bash","tool_input":{"command":"while true; do gh pr view 1; sleep 30; done"}}' \
//     | node scripts/warn-cwd-drift.mjs
//   The loop-head rule (arm 10; Story 233.1.2 — an enumeration is silent, a burst warns with its count):
//   echo '{"tool_name":"Bash","tool_input":{"command":"for n in 3955 3964; do gh pr view $n; done"}}' \
//     | node scripts/warn-cwd-drift.mjs; echo "exit $?"   # prints nothing, exit 0
//   echo '{"tool_name":"Bash","tool_input":{"command":"for i in 1 2 3; do gh pr view 1234; done"}}' \
//     | node scripts/warn-cwd-drift.mjs                   # reads gh 3 times with no sleep
//   The PowerShell tool (arm 1 on a cmdlet cd; Story 73.1.1):
//   echo '{"tool_name":"PowerShell","tool_input":{"command":"Set-Location projects/vituno; git status"}}' \
//     | node scripts/warn-cwd-drift.mjs
//   The org-derivation face (arm 9; Story 219.1.1 — denied; needs a projects/<name> clone under the root):
//   echo '{"tool_name":"Bash","tool_input":{"command":"gh pr list -R tourajghaffari/expeal.com"}}' \
//     | CLAUDE_PROJECT_DIR="$PWD" node scripts/warn-cwd-drift.mjs
// Suite: test/warn-cwd-drift.test.mjs (node --test).
//
// Canonical: synced to every repo by scripts/sync-session-wrap.mjs. Edit HERE.

import { execFileSync } from 'node:child_process';
import { existsSync, realpathSync } from 'node:fs';
import path from 'node:path';
import { fileURLToPath } from 'node:url';

// Command-position words the matchers skip before the verb, and the stage
// separators a neutralized command line splits on (shared by both arms).
// `timeout` and `xargs` since Story 233.1.2: wrappers that take arguments of their
// own before the command — `timeout [-s SIG] [-k N] [--signal=SIG] [--kill-after=N]
// [--preserve-status] [--foreground] [-v] DURATION cmd` and `xargs [opts] cmd` (-n -I
// -P -L -d -a -E -s -J take a separate value; `-n5`, `--max-args=5` are one word) —
// which commandWordIndex skips through wrapperArgsEnd (arms 8–10). The cd walkers
// skip only the word (a cd behind them is nonsense either way), and the node-eval
// arm keeps them opaque: `xargs node -e` stays its accepted limit (the 186.1 pin).
const WRAPPERS = new Set(['command', 'builtin', 'exec', 'env', 'nohup', 'time', 'sudo', 'timeout', 'xargs']);
// Story 97.1.3: shell keywords that may lead a stage before the cd —
// block-commit-on-main.mjs's KEYWORDS, copied (a single-file canonical).
const KEYWORDS = new Set(['do', 'then', 'else', 'elif', 'if', 'while', 'until', '!']);
const STAGE_SPLIT = /(?:&&|\|\||[;&\n(){}`|])+/;
// Command-position verbs that move the shell. Bash: cd/pushd. The PowerShell
// tool (Story 73.1.1): the cmdlets and their aliases too, compared
// case-insensitively (PowerShell is) — the cmdlet names belong to the shell
// that has them, so a Bash payload keeps cd/pushd only. `-Path` /
// `-LiteralPath` fall to the leading-dash flag skip like `-P` does.
const CD_VERBS = new Set(['cd', 'pushd']);
const CD_VERBS_POWERSHELL = new Set(['cd', 'pushd', 'chdir', 'sl', 'set-location', 'push-location']);
// The node-eval arm (Story 144.1.1): the flags whose next word is the program,
// and the program's /c/ faces. A drive path written into the program is `/c/`
// plus a segment that a `/`, a quote or the end follows — never a regex
// literal like `/b/g,` (its `g` is followed by a comma). Fixed-width
// lookbehind, no nested quantifier (P25: linear on the bounded-time pin).
const NODE_EVAL_FLAGS = new Set(['-e', '--eval', '-p', '--print', '-pe', '-ep']);
// Interpreters whose BARE `-` program word reads the program from stdin (the
// eighth arm, Story 214.1.1): the command word's basename, `.exe` optional.
const STDIN_INTERPRETERS = /^(?:python(?:[23](?:\.\d+)?)?|py|node|ruby|perl|php)(?:\.exe)?$/;
const PROGRAM_DRIVE_PATH = /(?<![\w.\/\\-])\/[A-Za-z]\/[\w.-]+(?=[\/'"`]|$)/;
const PROGRAM_VAR_SLASH = /(?<!\\)\$(?:\{([A-Za-z_]\w*)\}|([A-Za-z_]\w*))\//g;
const DRIVE_VALUE = /^\/[A-Za-z](?:\/|$)/;
// The ninth arm (Story 219.1.1): a GitHub host prefix a slug word may carry —
// `https://github.com/`, `github.com/`, `git@github.com:` (scp) or
// `ssh://git@github.com/` — and its two memos (a derived org per repo name;
// the session root's own origin, read once). Module-level bindings sit HERE,
// above the top-level arm calls, never below them (the TDZ the exclusion
// walker's comment records).
const GITHUB_HOST = /^(?:https?:\/\/)?(?:www\.)?github\.com\/|^(?:ssh:\/\/)?git@github\.com[:/]/;
const DERIVED_ORGS = new Map();
let rootOrigin;

if (process.stdin.isTTY) {
  console.error('warn-cwd-drift.mjs expects a PreToolUse JSON payload on stdin.');
  process.exit(1);
}

let payload;
let readWhy = null;
try {
  payload = JSON.parse(await readStdin());
} catch (e) {
  payload = null;
  readWhy = e?.code ?? e?.name ?? null;
}
if (typeof payload !== 'object' || payload === null || Array.isArray(payload)) {
  console.error(`warn-cwd-drift: unreadable PreToolUse payload${readWhy ? ` (${readWhy})` : ''} — guard stands down (warn-level, fail-open)`);
  process.exit(0);
}

// Story 73.1.1: the harness's shell tools — Bash on both machines, PowerShell
// on the Windows box (its primary shell). Measured 2026-09-04: the PowerShell
// tool's payload carries tool_name "PowerShell", tool_input.command, and a
// cwd that is the PowerShell process's PERSISTED working directory.
const SHELL_TOOLS = new Set(['Bash', 'PowerShell']);
if (!SHELL_TOOLS.has(payload.tool_name)) process.exit(0);
const isPowerShell = payload.tool_name === 'PowerShell';
const command = payload.tool_input?.command ?? '';
// The platform the node-eval arm's path face reads (Story 144.1.1):
// WARN_CWD_DRIFT_PLATFORM pins it in tests, else the host's.
const platform = process.env.WARN_CWD_DRIFT_PLATFORM || process.platform;

const messages = [];
const contexts = [];
// The denies accumulate here (arms 4, 5, 8 and 9 join their reasons) — hoisted
// above the arms so the handler below reads it (Story 233.1.1).
let denyReason = null;
// The arm now running, for the handler's one line; each arm sets it as it starts.
let currentArm = 'before the first arm';

// Story 233.1.1 (hub Initiative 233): a throw inside any arm must not forfeit the
// verdict the earlier arms gathered — Node's default is exit 1 with no stdout,
// which the hook law reads as NON-blocking, so the denied command would run
// (register class later-arm-throw-forfeits-the-guards-accumulated-deny). The
// handler prints the accumulated verdict — the deny rides when one was gathered
// — plus one line naming the arm that threw, and exits 0 once stdout drains
// (process.exit right after a write can cut a pipe short on macOS). The stack
// goes to stderr for the reader. Accepted limit: a second throw inside this
// handler exits 1, Node's default.
process.on('uncaughtException', (err) => {
  const line =
    `drift guard: arm ${currentArm} threw ${err?.message ?? String(err)} — the verdict above is the arms that ran ` +
    '(the arms after it did not judge this command; Story 233.1.1)';
  messages.push(line);
  contexts.push(line);
  console.error(err?.stack ?? String(err));
  process.stdout.write(`${verdictJson()}\n`, () => process.exit(0));
});

// Arm 1 (Story 24.3.20): this command cds into a sub-repo under projects/.
currentArm = '1 (cwd-drift cd, Story 24.3.20)';
const target = projectsCdTarget(command);
if (target) {
  messages.push(
    `cwd-drift guard: cd into ${target} — use git -C for later repo commands (warn only; Story 24.3.20)`
  );
  contexts.push(
    `cwd-drift guard (warn, not a block): this command cds into \`${target}\` — the shell cwd ` +
      'PERSISTS across later tool calls whether this compound succeeds or fails, so a later ' +
      'repo command can silently run in the wrong repository. For subsequent repo commands, ' +
      'prefer `git -C <absolute path>` / absolute paths over trusting the implicit cwd — and in ' +
      'EVERY call of a parallel batch: sibling tool calls issued in one response, and concurrent ' +
      'agents, share this one shell, so a cd here drifts the calls already queued behind it. ' +
      'Class seen 4x: a failed-compound cd (2026-07-21, origin of the rule); Sprint 14 F2 ' +
      '(2026-08-01, a SUCCESSFUL cd persisted; the next leg branch was created in the wrong repo — ' +
      'Story 24.3.20); four concurrent fork agents sharing the shell (2026-08-23, KB ' +
      "sdd-execution-frictions item 5); a cd in one of five parallel calls drifting a sibling's " +
      'archive read (2026-09-04, the hub Epic 71.1 close). The next call from the moved shell ' +
      'draws this guard\'s second arm (Story 72.1.1).'
  );
}

// Arm 2 (Story 72.1.1): the shell's cwd ALREADY resolves to a different
// repository than the session root — someone moved it (an earlier call, a
// sibling call in this same batch, a concurrent agent), and this is the call
// that would misfire.
currentArm = '2 (drifted shell, Story 72.1.1)';
const drift = driftedShell(command, payload.cwd);
if (drift) {
  messages.push(
    `cwd-drift guard: shell cwd resolves to a different repository (${drift.shellRepo}) than the ` +
      `session root (${drift.root}) — pin this call (warn only; Story 72.1.1)`
  );
  contexts.push(
    'cwd-drift guard (warn, not a block): the shell cwd resolves to a different repository than ' +
      `the session root — the shell sits in \`${drift.cwd}\` (repository \`${drift.shellRepo}\`); ` +
      `the session root is \`${drift.root}\`. This command runs THERE unless it pins its own ` +
      'location. Someone else moved the shell: an earlier call, a sibling call in this same ' +
      'batch, or a concurrent agent — the shell is ONE cwd shared by every caller. Pin it: open ' +
      `the call with \`cd ${drift.root} &&\`, use \`git -C <absolute path>\`, or absolute paths ` +
      'throughout. A deliberately pinned command still draws this warning — the arm sees the ' +
      'cwd, not the intent. Class seen 4x (2026-07-21; 2026-08-01 Sprint 14 F2; 2026-08-23 KB ' +
      'sdd-execution-frictions item 5; 2026-09-04 the hub Epic 71.1 close); this arm is the ' +
      'recorded ≥2× decision (hub Story 72.1.1).'
  );
}

// Arm 3 (Story 132.1.1): under MSYS2_ARG_CONV_EXCL, a /c/-form path reaches a
// native program unconverted. Bash only — the PowerShell tool has no MSYS layer.
currentArm = '3 (msys-path, Story 132.1.1)';
const msys = isPowerShell ? null : msysExcludedDrivePath(command);
if (msys) {
  messages.push(
    `msys-path guard: MSYS2_ARG_CONV_EXCL is set and ${msys.arg} is a /c/-form path — pass ${msys.windows} ` +
      '(warn only; Story 132.1.1)'
  );
  contexts.push(
    `msys-path guard (warn, not a block): this line sets \`MSYS2_ARG_CONV_EXCL=${msys.value}\`, which stops ` +
      `Git Bash converting ${msys.value === '*' ? 'EVERY argument' : 'the arguments it names'} of the command it ` +
      `covers — so \`${msys.arg}\` reaches native git, node or gh as that literal POSIX path and fails ("cannot ` +
      `change to '${msys.arg}'"). Behind a \`2>/dev/null\` the failure is silent, and a following count or cmp ` +
      `reads an empty stream as a verdict. Pass the Windows form, \`${msys.windows}\`, for every path on an ` +
      'excluded command, or run it from the PowerShell tool, which needs neither (KB ' +
      'windows-git-bash-cli-gotchas §6). Register class msys-excl-blocks-posix-c-path, its seats in ' +
      'docs/friction/recurrences.json; this arm is its re-decided form (Story 132.1.1).'
  );
}

// Arm 4 (Story 144.1.1): a double-quoted inline node program. Bash only — in
// the PowerShell tool a backtick inside double quotes is an escape.
currentArm = '4 (node-eval, Story 144.1.1)';
const nodeEval = isPowerShell ? null : nodeEvalFaces(command);
if (nodeEval?.backtick) {
  const advice =
    `node-eval guard (BLOCKED — this command did not run): the program of \`node ${nodeEval.backtick.flag} "…"\` ` +
    'is DOUBLE-quoted and carries an unescaped backtick pair, so Bash runs each backticked span as a command ' +
    'BEFORE node starts: markdown backticks around a repo path run that file as a shell script. Remedies: ' +
    "single-quote the program (`node -e '…'`, its own strings in double quotes); send text carrying backticks " +
    'through a file written with the Write tool and read the file in the program; or, if a substitution is ' +
    'meant, write $(…). KB inline-node-eval-double-quotes-run-backticks. Register class ' +
    "double-quoted-node-eval-backtick-runs-files, its seats in the hub's docs/friction/recurrences.json; this " +
    'deny is its decided form (Story 144.1.1).';
  denyReason = advice;
  messages.push(
    `node-eval guard: BLOCKED — a double-quoted node ${nodeEval.backtick.flag} program carries backticks Bash runs ` +
      'as commands before node starts (Story 144.1.1)'
  );
  contexts.push(advice);
}
// Story 179.1.1: a single-quoted program an apostrophe closed early. When the
// backtick face fired too, the two reasons JOIN rather than one overwriting.
if (nodeEval?.apostrophe) {
  const advice =
    `node-eval guard (BLOCKED — this command did not run): an apostrophe inside the single-quoted program of ` +
    `\`node ${nodeEval.apostrophe.flag} '…'\` closes it early. Bash ends the program at that apostrophe and reads ` +
    'the rest of the line as shell: node gets a truncated program, the tail becomes arguments, and anything after ' +
    'a ; | or & runs as a command. A single-quoted span cannot carry an apostrophe, escaped or as \\u0027. ' +
    'Remedies: write the program to a file with the Write tool and run it with node; or pass the text as an ' +
    'argument and read it with process.argv in the program. KB inline-node-eval-double-quotes-run-backticks ' +
    '§ The single-quote face. Register class single-quoted-node-eval-closed-by-apostrophe, its seats in ' +
    "the hub's docs/friction/recurrences.json; this deny is its decided form (Story 179.1.1).";
  denyReason = denyReason ? `${denyReason}\n\n${advice}` : advice;
  messages.push(
    `node-eval guard: BLOCKED — an apostrophe closes this single-quoted node ${nodeEval.apostrophe.flag} program ` +
      'early, and Bash reads the rest of the line as shell (Story 179.1.1)'
  );
  contexts.push(advice);
}
if (nodeEval?.drivePath && platform === 'win32') {
  const { flag, shown } = nodeEval.drivePath;
  messages.push(
    `node-eval guard: ${shown} is a /c/-form path inside a double-quoted node ${flag} program — pass it as an ` +
      'argument (warn only; Story 144.1.1)'
  );
  contexts.push(
    `node-eval guard (warn, not a block): Git Bash converts a /c/-form path only where it stands as its own ` +
      `ARGUMENT. Spliced into a double-quoted \`node ${flag}\` program (${shown}), it reaches node as text and ` +
      'resolves to C:\\c\\… (ENOENT). Pass the path as an argument and read it with process.argv — ' +
      "`node -e '…process.argv[1]…' \"$H/docs/…\"` — or write the Windows form C:/… in the program (KB " +
      'windows-git-bash-cli-gotchas §23). Register class posix-path-inside-node-eval-string, its seats in ' +
      "the hub's docs/friction/recurrences.json; this arm is its decided form (Story 144.1.1)."
  );
}
// Story 128.1.7: a string replacement holding a splice pattern, in any quoting.
if (nodeEval?.replace) {
  const { flag, pattern, shown } = nodeEval.replace;
  messages.push(
    `node-eval guard: the string replacement ${shown} in this node ${flag} program holds ${pattern} — String.replace ` +
      'splices it; use the function form replace(old, () => text) (warn only; Story 128.1.7)'
  );
  contexts.push(
    `node-eval guard (warn, not a block): this inline node ${flag} program calls .replace with the string ` +
      `replacement ${shown}, which holds ${pattern}. String.replace reads a STRING replacement's patterns — ` +
      "$& is the match, $` the text before it, $' the text after it, $$ one dollar sign — so the text is " +
      'spliced, not inserted (expeal.com Sprint 18 leg 2: a backticked dollar sign spliced 1,541 lines of the ' +
      "plan's prefix into it, and every content gate passed). Use the function form, .replace(old, () => text), " +
      'whose return value is inserted as-is, or make the edit with the Edit tool; then read git diff before ' +
      'committing. KB scripted-replacement-metacharacters. Register class sed-replacement-backslash-escapes, ' +
      "its seats in the hub's docs/friction/recurrences.json; this arm is its inline-node face's decided form " +
      '(Story 128.1.7).'
  );
}

// Arm 5 (Story 172.1.1): a git rev:path argument Git Bash rewrites. Bash and
// win32 only — the PowerShell tool and the Mac have no MSYS layer.
currentArm = '5 (msys-colon, Story 172.1.1)';
const colon = isPowerShell || platform !== 'win32' ? null : msysColonRevPath(command);
if (colon && !colon.variable) {
  // Story 215.1.1 (Hub Sprint 56 D4): the MEASURED face is DENIED — seats 6–7
  // showed the warn fired and governed neither read. Joins the other denies.
  const advice =
    `msys-colon guard (BLOCKED — this command did not run): Git Bash rewrites \`${colon.arg}\` to \`${colon.mangled}\` ` +
    'before git.exe sees it — a ref holding `/`, a colon, then a path that starts with `.` (not `./`) reads to MSYS ' +
    'as a POSIX path list — so git fails "ambiguous argument" or "Not a valid object name", silently behind ' +
    '`2>/dev/null` (MEASURED on the Windows box; seats 6–7 of the class, 2026-10-19: the warn fired twice and ' +
    "governed neither read). Cures: prefix MSYS2_ARG_CONV_EXCL='*' on this command line — then pass C:/ paths for " +
    'every path argument on it (the msys-path guard warns on /c/ ones) — or run the read from the PowerShell tool, ' +
    'which has no MSYS layer. For an existence probe, `git ls-tree --name-only <ref> <dir>/` takes no colon ' +
    'argument. Hub CLAUDE.md § scripts/ (the Windows-box paragraph); KB windows-git-bash-cli-gotchas §6. Register ' +
    "class msys-colon-rev-path-arg-mangled, its seats in the hub's docs/friction/recurrences.json; this deny is its " +
    'decided form (Story 215.1.1 — the deny face of Story 172.1.1\'s arm; the warn stays for the variable "may" face).';
  denyReason = denyReason ? `${denyReason}\n\n${advice}` : advice;
  messages.push(
    `msys-colon guard: BLOCKED — Git Bash rewrites ${colon.arg} before git sees it; prefix MSYS2_ARG_CONV_EXCL='*' on ` +
      'this line (with C:/ paths) or use the PowerShell tool (Story 215.1.1 — the deny face of Story 172.1.1\'s arm)'
  );
  contexts.push(advice);
} else if (colon) {
  const face = colon.variable
    ? `\`${colon.arg}\` names its path through ${colon.variable}: if that expands to a path that starts with \`.\` ` +
      `(not \`./\`), Git Bash rewrites the argument before git.exe sees it, as it does \`origin/main:.github/x.yml\` → ` +
      '`origin\\main;.github\\x.yml`'
    : `Git Bash rewrites \`${colon.arg}\` to \`${colon.mangled}\` before git.exe sees it — a ref holding \`/\`, a colon, ` +
      'then a path that starts with `.` reads to MSYS as a POSIX path list';
  messages.push(
    `msys-colon guard: ${colon.variable ? `${colon.arg} may be` : `${colon.arg} is`} rewritten by Git Bash before git sees ` +
      "it — set MSYS2_ARG_CONV_EXCL='*' on this line (with C:/ paths), or use the PowerShell tool (warn only; Story 172.1.1)"
  );
  contexts.push(
    `msys-colon guard (warn, not a block): ${face}, so git fails "ambiguous argument" or "Not a valid object name" ` +
      '(MEASURED on the Windows box; a ref with no `/` such as HEAD, main or a sha, a `./`-led path and an undotted ' +
      'path are left alone). Behind `2>/dev/null` the failure is silent, and an existence probe reads "absent". ' +
      "Cures: prefix MSYS2_ARG_CONV_EXCL='*' on this command line — then pass C:/ paths for every path argument on " +
      'it (the msys-path guard warns on /c/ ones) — or run it from the PowerShell tool, which has no MSYS layer. For ' +
      'an existence probe, `git ls-tree --name-only <ref> <dir>/` takes no colon argument. KB ' +
      'windows-git-bash-cli-gotchas §6. Register class msys-colon-rev-path-arg-mangled, its seats in ' +
      "the hub's docs/friction/recurrences.json; this arm is its decided form (Story 172.1.1)."
  );
}

// Arm 6 (Story 190.1.1): a wsl stage's leading-slash argument Git Bash
// rewrites. Bash and win32 only — the PowerShell tool and the Mac have no MSYS
// layer.
currentArm = '6 (msys-wsl, Story 190.1.1)';
const wsl = isPowerShell || platform !== 'win32' ? null : msysWslPath(command);
if (wsl) {
  messages.push(
    `msys-wsl guard: ${wsl.arg} is rewritten by Git Bash to ${wsl.mangled} before wsl sees it — run WSL from the ` +
      "PowerShell tool, or set MSYS2_ARG_CONV_EXCL='*' on this line (warn only; Story 190.1.1)"
  );
  contexts.push(
    'msys-wsl guard (warn, not a block): Git Bash converts an argument that begins `/` to a Windows path under the ' +
      `Git install prefix before a native program sees it, so \`${wsl.arg}\` reaches wsl as \`${wsl.mangled}\` — ` +
      'every leading-slash argument to wsl is a Linux-side path, and there the rewrite is always wrong (MEASURED on ' +
      'the Windows box: `/mnt/c/…` → `C:/Program Files/Git/mnt/c/…`, a close runner that "ran" in zero seconds ' +
      'against a script that did not exist; `/home/…` → `C:/Program Files/Git/home/…`, execvpe "No such file or ' +
      'directory"). Cures: run this line from the PowerShell tool, which has no MSYS layer (KB ' +
      'rails-on-windows-wsl-not-native § The harness relay — the multi-step logic in a script FILE, the exit read ' +
      "via $LASTEXITCODE), or prefix MSYS2_ARG_CONV_EXCL='*' on this command line (KB windows-git-bash-cli-gotchas " +
      '§18). A `//`-led spelling (MSYS strips one slash), a `/<letter>/…` drive path (converted correctly) and a ' +
      'path inside a quoted script argument stay silent. Register class msys-leading-slash-url-arg-rewritten, its ' +
      "seats in the hub's docs/friction/recurrences.json; this arm is the wsl face's decided form (Story 190.1.1)."
  );
}

// Arm 7 (Story 202.1.1): a grep pattern's bounded repetition the Mac's ugrep
// refuses — the ERE `{m,n}` and the BRE `\{m,n\}` spellings alike (the BRE face
// passed the first predicate silently on 2026-10-17; register seat 7). Bash and
// darwin only — the harness grep is ugrep on the Mac alone.
currentArm = '7 (ugrep, Story 202.1.1)';
const ugrep = isPowerShell || platform !== 'darwin' ? null : ugrepBoundedRepetition(command);
if (ugrep) {
  messages.push(
    `ugrep guard: the pattern ${ugrep.pattern} carries a bounded repetition (${ugrep.span}) — the Mac's harness grep is ugrep and prints ` +
      "'exceeds complexity limits' with NO match line; write the needle alone (grep -n 'needle' <file>) and read the line — an empty " +
      "extraction under that error is UNMEASURED, not absent (warn only; Story 202.1.1)"
  );
  contexts.push(
    'ugrep guard (warn, not a block): the Mac\'s harness `grep` is ugrep, and a pattern carrying a bounded repetition (`{m,n}` or ' +
      `\`{m,}\`; the BRE spelling \`\\{m,n\\}\` without -E included — ugrep reads it as the interval too, MEASURED 2026-10-17) over a dot, ` +
      `a class or a group — here \`${ugrep.span}\` in \`${ugrep.pattern}\` — fails \`ugrep: error: … exceeds ` +
      'complexity limits` and prints NOTHING else, so an extraction reads as EMPTY, never as failed (four reads in eight days — ' +
      '`[^.]{0,60}…[^.]{0,40}`, `.{0,60}#3550.{0,60}`, `.{0,160}R1.s retry.{0,200}`, one of them lost inside a pipeline — each ' +
      'caught by eye and by no guard; `LC_ALL=C` does NOT clear it, MEASURED 2026-10-11). Write the needle alone — ' +
      "`grep -n 'needle' <file>` — and read the line; an empty extraction under that error is UNMEASURED, not absent. " +
      'A fixed count (`{n}`), `-F`, and win32/linux (GNU grep) are not this shape. The reading form is CLAUDE.md § scripts/ ' +
      '(the ugrep paragraph); register class ugrep-bounded-repetition-exceeds-complexity-limit, its seats in the hub\'s ' +
      'docs/friction/recurrences.json; this arm is the class\'s decided form (Story 202.1.1).'
  );
}

// Arm 8 (Story 214.1.1): a stdin-fed interpreter — both tools, both platforms.
// Joins arm 4's reason when both fire (the Story 179.1.1 join).
currentArm = '8 (stdin-interpreter, Story 214.1.1)';
const stdinFace = stdinInterpreterFace(command);
if (stdinFace) {
  const advice =
    `stdin-interpreter guard (BLOCKED — this command did not run): \`${stdinFace.verb} -\` reads its program ` +
    'from stdin, and through the Bash or PowerShell tool a mangled heredoc or here-string delimiter leaves the ' +
    'interpreter waiting on stdin at 100% of one core with no error — the register\'s first seat ran 11.4 h beside ' +
    'every timed battery of a twelve-session night (tigerian\'s Task 7 implementer, 2026-10-18/19), found by a ' +
    'process read, never by the session that started it. Remedy: write the script to a file with the Write tool ' +
    `and run it by path (\`${stdinFace.verb} <path>\`); a one-line program takes the inline flag the CLAUDE.md inline ` +
    'rule governs. Reading form: hub CLAUDE.md § scripts/ "No stdin-fed interpreter through the Bash tool, either ' +
    'box". Register class subagent-background-shell-left-a-runaway-process-beside-timed-batteries, its seats in ' +
    "the hub's docs/friction/recurrences.json; this deny is its decided form (Story 214.1.1, Initiative 214).";
  denyReason = denyReason ? `${denyReason}\n\n${advice}` : advice;
  messages.push(
    `stdin-interpreter guard: BLOCKED — \`${stdinFace.verb} -\` reads its program from stdin; a mangled heredoc ` +
      'leaves it waiting at 100% of a core — write the script to a file and run it by path (Story 214.1.1)'
  );
  contexts.push(advice);
}
// Arm 9 (Story 219.1.1): a fleet repo's org guessed, not derived from its
// clone's origin — both tools, both platforms. Joins the other denies.
currentArm = '9 (org-derivation, Story 219.1.1)';
const org = orgMismatch(command);
if (org) {
  const advice =
    `org-derivation guard (BLOCKED — this command did not run): \`${org.shown}\` names \`${org.slug}\`, but the fleet ` +
    `repo \`${org.name}\` lives under \`${org.derivedOrg}\` — derived from its clone's own origin (\`${org.origin}\`, read ` +
    `by \`git -C ${org.dir} remote get-url origin\`), never guessed. A guessed owner either fails "GraphQL: Could not ` +
    'resolve to a Repository" (the error reads like a permissions problem) or, for a same-named repo under the other ' +
    `org, RESOLVES to the wrong repository. Re-issue with \`${org.derived}\`. Reading form: KB ` +
    'github-org-per-repo-derive-from-remote (the one-command derivation and the census). Register class ' +
    "gh-repo-org-guessed-not-derived, its seats in the hub's docs/friction/recurrences.json; this deny is its decided " +
    'form (Story 219.1.1, Initiative 219).';
  denyReason = denyReason ? `${denyReason}\n\n${advice}` : advice;
  messages.push(
    `org-derivation guard: BLOCKED — ${org.slug} names a repo whose clone's origin is under ${org.derivedOrg}; ` +
      `re-issue with ${org.derived} (Story 219.1.1)`
  );
  contexts.push(advice);
}
// Arm 10 (Story 220.1.3): a seconds-cadence gh poll loop — both tools, both
// platforms. Warn only: the budget read first makes a capped short poll legal.
currentArm = '10 (poll-cadence, Story 220.1.3)';
const poll = pollCadenceFace(command);
if (poll) {
  messages.push(
    `poll-cadence guard: this loop ${poll.verb} ${poll.cadence} — the account's REST quota is 5,000/h shared by every session; ` +
      'poll ≥ 60 s, run node scripts/fleet/watch-pr.mjs with the PR number (the runs on its head, one read per 60 s, exit by ' +
      'their conclusion — Story 236.1.2), or read the branch over SSH (git ls-remote --heads origin, the branch named) ' +
      '(warn only; Story 220.1.3)'
  );
  contexts.push(
    `poll-cadence guard (warn, not a block): this ${poll.shape} loop reads \`gh\` ${poll.cadence}. GitHub's REST primary limit ` +
      'is 5,000/h per USER — one quota for every session on both boxes — and a seconds-cadence watcher spends it unnoticed: ' +
      'on 2026-10-21 the core budget read 5000/0 at 15:56Z (reset 16:14:27Z), every gh read on both boxes failed until the ' +
      'reset, and a hand heartbeat printed 33 FAILED-TO-DERIVE lines that looked like a drift stack. Remedies: poll ≥ 60 s ' +
      '(`sleep 60`); run `node scripts/fleet/watch-pr.mjs` with the PR number — the runs on its head sha read once per 60 s ' +
      'until every one completes, exit 0 green / 1 red / 2 no run within the cap / 3 CONFLICTING with its base while no ' +
      'run is registered — rebase, push, re-run (Story 236.1.2, exit 3 Story 242.1.1; it replaces `gh pr checks ' +
      '--watch`, which exits 1 on "no checks reported" before the run registers — KB gh-pr-checks-watch-force-push-race); ' +
      'or read the branch over SSH (`git ls-remote --heads origin` with the branch named — no REST call at all) — and read ' +
      '`gh api rate_limit` before any battery-sized read. ' +
      'Warn, never deny: a deliberate short poll with a cap is legal when the budget is read first. Reading form: KB ' +
      'gh-rest-quota-shared-across-sessions (rules 1–4); register class gh-rest-core-quota-exhausted-by-pr-watch-polling, ' +
      "its seats in the hub's docs/friction/recurrences.json; this arm is the class's decided form (Story 220.1.3, Initiative 220)."
  );
}
if (!messages.length) process.exit(0);

// Warn-level throughout, with the node-eval backtick face (Story 144.1.1), the
// apostrophe face (Story 179.1.1), the msys-colon measured face (Story
// 215.1.1), the stdin-interpreter face (Story 214.1.1) and the org-derivation
// face (Story 219.1.1) the five denies:
// additionalContext and
// systemMessage ride beside the
// permissionDecision unchanged, so one output carries every arm.
console.log(verdictJson());

// The one verdict: systemMessage (the user), the deny when a deny arm fired, and
// additionalContext (the model). The final print and the uncaughtException
// handler (Story 233.1.1) both build it from the arrays above.
function verdictJson() {
  return JSON.stringify({
    systemMessage: messages.join(' | '),
    hookSpecificOutput: {
      hookEventName: 'PreToolUse',
      ...(denyReason ? { permissionDecision: 'deny', permissionDecisionReason: denyReason } : {}),
      additionalContext: contexts.join('\n\n'),
    },
  });
}

// --- matcher ----------------------------------------------------------------

// Neutralize text that is data, not command structure — same approach as
// warn-piped-checker.mjs: heredoc bodies dropped, quoted spans become indexed
// placeholders whose contents are kept (so a quoted cd TARGET still
// resolves), except double-quoted spans containing live substitution (`$(` or
// backtick), which stay in place.
function neutralize(command) {
  let text = command;
  // PowerShell here-strings (Story 73.1.1): `@'` … `'@` and `@"` … `"@`, the
  // terminator at line start, are argument DATA — dropped whole before the
  // quote pass, which would otherwise split a body at its first apostrophe.
  // A body line that merely LOOKS like the terminator ends the here-string
  // early (errs toward warning, never silence).
  if (isPowerShell) text = text.replace(/@(['"])\r?\n[\s\S]*?\r?\n\1@/g, '');
  text = dropHeredocs(text);
  const values = [];
  // Live substitution inside double quotes: `$(` in both shells; the backtick
  // only in Bash — in PowerShell it is the escape character (Story 73.1.1).
  const live = isPowerShell ? /\$\(/ : /\$\(|`/;
  text = text.replace(/'[^']*'|"(?:[^"\\]|\\.)*"/g, (q) => {
    const inner = q.slice(1, -1);
    if (q.startsWith('"') && live.test(inner)) return inner;
    values.push(inner);
    return `\x00${values.length - 1}\x00`;
  });
  return { text, values };
}

// Heredoc bodies are argument DATA: each `<<WORD` / `<<-WORD` / `<<'WORD'`
// body is dropped through its terminator line (shared by neutralize() and
// the node-eval arm).
function dropHeredocs(command) {
  let text = command;
  const heredoc = /<<-?\s*(["']?)(\w+)\1/g;
  let m;
  while ((m = heredoc.exec(text))) {
    const bodyStart = text.indexOf('\n', m.index + m[0].length);
    if (bodyStart === -1) break;
    const term = text.slice(bodyStart + 1).match(new RegExp(`^\\s*${m[2]}\\s*$`, 'm'));
    const bodyEnd = term ? bodyStart + 1 + term.index + term[0].length : text.length;
    text = text.slice(0, bodyStart) + text.slice(bodyEnd);
    heredoc.lastIndex = m.index + m[0].length;
  }
  return text;
}

// The verb at command position moves the shell (see CD_VERBS above).
function isCdVerb(token) {
  return isPowerShell ? CD_VERBS_POWERSHELL.has(token.toLowerCase()) : CD_VERBS.has(token);
}

// A path targets projects/ when a segment equals `projects` and another
// segment follows or the path ends there (`projects-archive` never matches).
function isProjectsPath(p) {
  return /(^|\/)projects(\/|$)/.test(p);
}

// Returns the resolved cd target under projects/, else null.
function projectsCdTarget(command) {
  const { text, values } = neutralize(command);
  const resolve = (t) => t.replace(/\x00(\d+)\x00/g, (_, n) => values[Number(n)] ?? '');

  const stages = text.split(STAGE_SPLIT);
  for (const stage of stages) {
    const tokens = stage.trim().split(/\s+/).filter(Boolean);
    let i = 0;
    while (
      i < tokens.length &&
      (KEYWORDS.has(tokens[i]) || /^[A-Za-z_][A-Za-z0-9_]*=/.test(tokens[i]) || WRAPPERS.has(tokens[i]))
    ) i++;
    if (i >= tokens.length) continue;
    if (!isCdVerb(tokens[i])) continue;
    for (const t of tokens.slice(i + 1)) {
      if (t.startsWith('-')) continue; // cd flags (-P, -L, pushd -n)
      const resolved = resolve(t);
      // First real arg decides THIS stage; later stages still get scanned
      // (`cd docs && cd ../projects/x` must fire on the second stage).
      if (isProjectsPath(resolved)) return resolved;
      break;
    }
  }
  return null;
}

// --- third arm: the MSYS exclusion face (Story 132.1.1) ----------------------

// A token that IS a POSIX drive path (`/c`, `/c/Users/…`), or a flag's
// `=/c/…` value: the drive path, else null. `/dev/null`, `/tmp` never match —
// a single letter must stand alone before the next `/`.
function drivePathOf(token) {
  return /(?:^|=)(\/[A-Za-z](?:\/\S*)?)$/.exec(token)?.[1] ?? null;
}

// Returns { arg, value, windows } for the first argument a MSYS2_ARG_CONV_EXCL
// exclusion leaves unconverted while it is a /c/-form path, else null. The
// exclusion reaches a stage by its own prefix assignment, or by an earlier
// assignment-only stage or `export` on the same line; `*` excludes every
// argument, any other value the arguments beginning with one of its
// `;`-separated prefixes. Paths resolve through the line's own simple
// assignments (`R=/c/… && … $R`, `${R}`, `"$R"`) and a `for NAME in` list's
// first word — static reads only (accepted limits: a variable set by an
// earlier CALL, a `$(…)`-built value, or parameter-expansion operators beyond
// naming the variable are not followed — the register's seat 4 shape).
function msysExcludedDrivePath(command) {
  return walkExclusionStages(command, ({ args, value, expand, excludes }) => {
    if (value == null) return null;
    for (const t of args) {
      const arg = drivePathOf(expand(t));
      if (arg && excludes(value, arg)) {
        return { arg, value, windows: `${arg[1].toUpperCase()}:${arg.slice(2) || '/'}` };
      }
    }
    return null;
  });
}

// --- fifth arm: a git rev:path argument Git Bash rewrites (Story 172.1.1) ----

// Returns { arg, mangled, variable } for the first argument of a `git` stage
// that MSYS will rewrite — a ref holding `/`, a colon, then a path that starts
// with `.` but not `./` (MEASURED: the 172.1.1 PR's shape table) — when no
// MSYS2_ARG_CONV_EXCL on the line covers it, else null. `variable` names the
// `$NAME` a path starts with when the line does not resolve it (the hook cannot
// see the value, so the warning is a "may"). Splitting on the FIRST colon keeps
// a URL (`https://…`), an scp remote (`git@host:o/r`) and a `host:port` out:
// none of their left sides holds a `/`.
function msysColonRevPath(command) {
  return walkExclusionStages(command, ({ verb, args, value, expand, excludes, raw }) => {
    if (verb !== 'git' && verb !== 'git.exe') return null;
    for (const t of args) {
      const arg = expand(t);
      const m = /^([^\s:]+):(.+)$/.exec(arg);
      if (!m || !m[1].includes('/') || m[1].startsWith('-')) continue;
      if (value != null && excludes(value, arg)) continue;
      const [, ref, rel] = m;
      if (rel.startsWith('.') && !rel.startsWith('./') && rel.length > 1) {
        return { arg, mangled: `${ref.replace(/\//g, '\\')};${rel.replace(/\//g, '\\')}`, variable: null };
      }
      const shown = raw(t);
      const v = /^[^\s:]+:(\$\{?[A-Za-z_]\w*\}?)/.exec(shown);
      if (v && rel.startsWith('$')) return { arg: shown, mangled: null, variable: v[1] };
    }
    return null;
  });
}

// --- sixth arm: a wsl stage's leading-slash argument Git Bash rewrites (Story 190.1.1) ----

// Returns { arg, mangled } for the first argument of a `wsl` / `wsl.exe` stage
// (the verb's basename, lower-case — a path-suffixed `…/wsl.exe` counts) that
// MSYS will rewrite: an argument that begins `/`, or a `--flag=/…` value, that
// is not a lone `/`, not a `//`-led spelling (MSYS strips one slash — the
// escape form, register seat 10) and not a `/<letter>/…` drive path (converted
// CORRECTLY; `wsl --import/--export/--mount/--cd` take Windows paths) — when no
// MSYS2_ARG_CONV_EXCL on the line covers it, else null. The exclusion is tested
// against the VALUE, as drivePathOf's `(?:^|=)` reads a flag's value. MEASURED
// (register seats 11 and 12): the rewrite is the Git install prefix glued to the
// argument — `/mnt/c/x` → `C:/Program Files/Git/mnt/c/x`.
function msysWslPath(command) {
  // Local, not module-level: the top-level arm calls this before a const below
  // it would be initialized (the TDZ — the walker's REDIRECT_OP note).
  const GIT_INSTALL_PREFIX = 'C:/Program Files/Git';
  return walkExclusionStages(command, ({ verb, args, value, expand, excludes }) => {
    const base = verb.replace(/\\/g, '/').split('/').pop().toLowerCase();
    if (base !== 'wsl' && base !== 'wsl.exe') return null;
    for (const t of args) {
      const arg = expand(t);
      const v = /(?:^|=)(\/\S*)$/.exec(arg)?.[1];
      if (!v || v === '/' || v.startsWith('//') || drivePathOf(arg)) continue;
      if (value != null && excludes(value, v)) continue;
      return { arg: v, mangled: `${GIT_INSTALL_PREFIX}${v}` };
    }
    return null;
  });
}

// --- seventh arm: a grep pattern's bounded repetition the Mac's ugrep refuses (Story 202.1.1) ----

// Returns { pattern, span } for the first `grep` / `egrep` stage (the verb's
// basename, lower-case; `fgrep` and `ggrep` are not ugrep) whose PATTERN carries
// `{m,n}` or `{m,}` immediately after `.`, a `]` or a `)`, else null. The
// pattern is the `-e` / `--regexp` value when present (`-e X`, `-eX`, `--regexp
// X`, `--regexp=X`), else the first word after the verb that is neither a flag
// nor the VALUE of a value-taking flag (the GNU set, which ugrep shares — a
// naive first-non-flag read of `grep -m 1 -e '…' f` would take `1`). Silent
// under `-F` / `--fixed-strings` (the braces are literal there) and on a fixed
// count `{n}`. Words arrive from the walker with their quotes removed; `raw`
// resolves the placeholder without expanding `$VAR` (a single-quoted pattern's
// dollar is ugrep's, not the shell's). The span is the first match, for the
// message; a `{m,n}` elsewhere on the line (an `echo`, a file name) is not read.
function ugrepBoundedRepetition(command) {
  const VALUE_FLAGS = new Set(['-A', '-B', '-C', '-m', '-d', '-D', '-f', '--after-context', '--before-context', '--context',
    '--max-count', '--directories', '--devices', '--file', '--include', '--exclude', '--exclude-dir', '--include-dir',
    '--exclude-from', '--label', '--binary-files', '--color', '--colour', '--group-separator']);
  // `{m,n}` / `{m,}` after a dot, a class or a group — and the BRE spelling `\{m,n\}` (no -E):
  // ugrep reads the escaped braces as the same interval and fails the same way (seat 7, 2026-10-17).
  const SHAPE = /(?:\.|\]|\))\\?\{\d+,\d*\\?\}/;
  return walkExclusionStages(command, ({ verb, args, raw }) => {
    const base = verb.replace(/\\/g, '/').split('/').pop().toLowerCase();
    if (base !== 'grep' && base !== 'egrep') return null;
    const words = args.map((t) => raw(t));
    if (words.some((w) => w === '-F' || w === '--fixed-strings' || /^-[A-Za-z]*F[A-Za-z]*$/.test(w))) return null;
    let pattern = null;
    for (let i = 0; i < words.length && pattern == null; i++) {
      const w = words[i];
      if (w === '-e' || w === '--regexp') pattern = words[i + 1] ?? null;
      else if (w.startsWith('--regexp=')) pattern = w.slice('--regexp='.length);
      else if (/^-e.+/.test(w) && !w.startsWith('--')) pattern = w.slice(2);
    }
    if (pattern == null) {
      for (let i = 0; i < words.length; i++) {
        const w = words[i];
        if (w.startsWith('-') && w.length > 1) { if (VALUE_FLAGS.has(w)) i++; continue; }
        pattern = w;
        break;
      }
    }
    if (pattern == null) return null;
    const m = SHAPE.exec(pattern);
    return m ? { pattern, span: m[0] } : null;
  });
}

// The stage walk arms 3, 5, 6 and 7 share, so one parser decides what an exclusion
// reaches. For each command stage it calls visit({ verb, args, value, expand,
// excludes, raw }) — `value` the MSYS2_ARG_CONV_EXCL covering the stage (its
// own prefix, or an earlier assignment-only stage or `export` on the line;
// null if none), `args` the words after the verb minus redirection targets,
// `expand` a word with placeholders and the line's own variables resolved,
// `raw` a word with placeholders only — and returns the first non-null result.
function walkExclusionStages(command, visit) {
  const { text, values } = neutralize(command);
  const vars = new Map();
  const raw = (t) => t.replace(/\x00(\d+)\x00/g, (_, n) => values[Number(n)] ?? '');
  const expand = (t) =>
    raw(t).replace(/\$\{([A-Za-z_]\w*)[^}]*\}|\$([A-Za-z_]\w*)/g, (m, a, b) => vars.get(a ?? b) ?? m);
  const excludes = (value, arg) => value === '*' || value.split(';').some((p) => p && arg.startsWith(p));
  let lineWide = null; // an exclusion set by an assignment-only stage or `export`, for later stages
  for (const stage of text.split(STAGE_SPLIT)) {
    const tokens = stage.trim().split(/\s+/).filter(Boolean);
    if (!tokens.length) continue;
    if (tokens[0] === 'for' && tokens[2] === 'in' && /^[A-Za-z_]\w*$/.test(tokens[1])) {
      vars.set(tokens[1], tokens[3] ? expand(tokens[3]) : '');
      continue;
    }
    const exported = tokens[0] === 'export';
    let i = exported ? 1 : 0;
    const assigns = [];
    while (i < tokens.length && (KEYWORDS.has(tokens[i]) || /^[A-Za-z_][A-Za-z0-9_]*=/.test(tokens[i]) || WRAPPERS.has(tokens[i]))) {
      const m = /^([A-Za-z_][A-Za-z0-9_]*)=(.*)$/.exec(tokens[i]);
      if (m) assigns.push([m[1], expand(m[2])]);
      i++;
    }
    if (exported || i >= tokens.length) {
      for (const [name, value] of assigns) {
        if (name === 'MSYS2_ARG_CONV_EXCL') lineWide = value;
        else vars.set(name, value);
      }
      continue;
    }
    const value = assigns.find(([name]) => name === 'MSYS2_ARG_CONV_EXCL')?.[1] ?? lineWide;
    // Story 128.1.6: the token after a redirection operator (`>`, `>>`, `2>`,
    // `<`) is Bash's to open, never the program's argument, so the exclusion
    // does not reach it — a /c/ target works. A glued `>/c/x` never matched
    // drivePathOf (it anchors at the token start), so the spaced form is the face.
    // Local, not module-level: the top-level arms call this function before a
    // module-level const below them would be initialized (the TDZ).
    const REDIRECT_OP = /^(?:\d*>{1,2}|&>{1,2}|<{1,3})$/;
    const args = [];
    let target = false;
    for (const t of tokens.slice(i + 1)) {
      if (target) {
        target = false;
        continue;
      }
      if (REDIRECT_OP.test(t)) {
        target = true;
        continue;
      }
      args.push(t);
    }
    const hit = visit({ verb: tokens[i], args, value, expand, excludes, raw });
    if (hit) return hit;
  }
  return null;
}

// --- fourth arm: the double-quoted inline node program (Story 144.1.1) -------

// Words of a Bash line, quote-aware, grouped into stages: each word is a list
// of parts { q, text } — q is '' (bare), "'" or '"' — so a word's double-quoted
// spans keep their raw text (escapes intact) for the node-eval faces. One
// linear pass; an unterminated quote runs to the end of the line. Stages split
// on ; & | newline, parens, braces and backticks outside quotes, as
// STAGE_SPLIT does. Accepted limit: a `"` nested inside a `$(…)` within double
// quotes ends the span early.
function shellWords(text) {
  const stages = [[]];
  let word = null;
  const part = (q, s) => {
    word ??= [];
    const last = word[word.length - 1];
    if (q === '' && last?.q === '') last.text += s;
    else word.push({ q, text: s });
  };
  const endWord = () => {
    if (word) stages[stages.length - 1].push(word);
    word = null;
  };
  for (let i = 0; i < text.length; i++) {
    const c = text[i];
    if (c === "'") {
      const end = text.indexOf("'", i + 1);
      const stop = end === -1 ? text.length : end;
      part("'", text.slice(i + 1, stop));
      i = stop;
    } else if (c === '"') {
      let j = i + 1;
      while (j < text.length && text[j] !== '"') j += text[j] === '\\' ? 2 : 1;
      part('"', text.slice(i + 1, Math.min(j, text.length)));
      i = j;
    } else if (c === '\\') {
      part('', text.slice(i, i + 2));
      i++;
    } else if (c === ' ' || c === '\t' || c === '\r') {
      endWord();
    } else if (';&|\n(){}`'.includes(c)) {
      endWord();
      if (stages[stages.length - 1].length) stages.push([]);
    } else {
      part('', c);
    }
  }
  endWord();
  return stages.filter((s) => s.length);
}

function wordText(w) {
  return w.map((p) => p.text).join('');
}

function isBare(w) {
  return w.length === 1 && w[0].q === '';
}

// Count the backticks a double-quoted span leaves live: a backslash escapes
// the next character, so `\`` is a literal.
function liveBackticks(span) {
  let n = 0;
  for (let i = 0; i < span.length; i++) {
    if (span[i] === '\\') i++;
    else if (span[i] === '`') n++;
  }
  return n;
}

// Returns { backtick: { flag }, drivePath: { flag, shown }, apostrophe: { flag } }
// (any may be absent) for the first node invocation — bare, or after `mise
// exec --` — whose -e/--eval/-p/--print program is a double-quoted span that
// carries an unescaped backtick pair, or a /c/-form path (literal, or `$NAME/`
// naming a `NAME=/c/…` assignment earlier on the line), or a single-quoted
// span an apostrophe closed early (Story 179.1.1). Heredoc bodies are data.
function nodeEvalFaces(command) {
  const vars = new Map();
  const found = {};
  for (const stage of shellWords(dropHeredocs(command))) {
    let i = stage[0] && isBare(stage[0]) && stage[0][0].text === 'export' ? 1 : 0;
    while (i < stage.length) {
      // A word that opens bare is read whole (`H="/c/x"` is an assignment).
      const t = stage[i][0].q === '' ? wordText(stage[i]) : '';
      const assign = /^([A-Za-z_]\w*)=([\s\S]*)$/.exec(t);
      if (assign) {
        vars.set(assign[1], assign[2]);
        i++;
      } else if (KEYWORDS.has(t) || (WRAPPERS.has(t) && t !== 'timeout' && t !== 'xargs')) i++; // Story 233.1.2: opaque here
      else break;
    }
    if (i < stage.length && wordText(stage[i]) === 'mise' && stage[i + 1] && wordText(stage[i + 1]) === 'exec') {
      const dash = stage.findIndex((w, k) => k > i + 1 && wordText(w) === '--');
      if (dash === -1) continue;
      i = dash + 1;
    }
    if (i >= stage.length || !/^(?:.*[\/\\])?node(?:\.exe)?$/.test(wordText(stage[i]))) continue;
    for (let k = i + 1; k < stage.length; k++) {
      const head = wordText(stage[k]);
      let flag = null;
      let program = null;
      if (NODE_EVAL_FLAGS.has(head) && stage[k + 1]) [flag, program] = [head, stage[k + 1]];
      else {
        const joined = /^(--eval|--print)=/.exec(stage[k][0].q === '' ? stage[k][0].text : '');
        if (joined) [flag, program] = [joined[1], [{ q: '', text: stage[k][0].text.slice(joined[0].length) }, ...stage[k].slice(1)]];
      }
      if (!program) continue;
      // Story 128.1.7: the replace face reads the program under EITHER quoting
      // (a single-quoted program is the fleet's recommended form).
      if (!found.replace) {
        const r = replaceDollarPattern(program.map((s) => s.text).join(''));
        if (r) found.replace = { flag, ...r };
      }
      // Story 179.1.1: a single-quoted part followed directly by a bare part
      // opening with a word character — an apostrophe closed the program early.
      if (!found.apostrophe) {
        for (let p = 0; p + 1 < program.length; p++) {
          if (program[p].q === "'" && program[p + 1].q === '' && opensWordChar(program[p + 1].text)) {
            found.apostrophe = { flag };
            break;
          }
        }
      }
      for (const { q, text } of program) {
        if (q !== '"') continue;
        if (!found.backtick && liveBackticks(text) >= 2) found.backtick = { flag };
        if (!found.drivePath) {
          const literal = PROGRAM_DRIVE_PATH.exec(text);
          if (literal) found.drivePath = { flag, shown: literal[0] };
          for (const m of text.matchAll(PROGRAM_VAR_SLASH)) {
            if (found.drivePath) break;
            const name = m[1] ?? m[2];
            if (DRIVE_VALUE.test(vars.get(name) ?? '')) found.drivePath = { flag, shown: `$${name} (= ${vars.get(name)})` };
          }
        }
      }
      break;
    }
    if (found.backtick && found.drivePath && found.replace && found.apostrophe) break;
  }
  return found.backtick || found.drivePath || found.replace || found.apostrophe ? found : null;
}

// --- eighth arm: the stdin-fed interpreter (Story 214.1.1) -------------------

// Returns { verb } for the first stage whose command word — bare, by path,
// `.exe`, or after `mise exec --`, past assignments, keywords and wrappers —
// is an interpreter (STDIN_INTERPRETERS) and whose arguments carry a BARE `-`
// word (never `--`, never `-x`, never a `-` inside quotes); null otherwise.
// Heredoc bodies and PowerShell here-strings are data, dropped before the
// words are read: `python3 - <<'EOF'` keeps its operator word and loses its
// body, and a `python3 -` LINE inside a body never fires. Accepted limit,
// stated in the header: an interpreter inside a quoted `bash -lc '…'` program
// is one quoted word to this reader.
function stdinInterpreterFace(command) {
  let text = command;
  if (isPowerShell) text = text.replace(/@(['"])\r?\n[\s\S]*?\r?\n\1@/g, '');
  for (const stage of shellWords(dropHeredocs(text))) {
    const i = commandWordIndex(stage);
    if (i === -1 || !isBare(stage[i])) continue;
    const verb = wordText(stage[i]);
    const base = verb.slice(Math.max(verb.lastIndexOf('/'), verb.lastIndexOf('\\')) + 1);
    if (!STDIN_INTERPRETERS.test(base)) continue;
    if (stage.slice(i + 1).some((w) => isBare(w) && w[0].text === '-')) return { verb: base };
  }
  return null;
}
// --- ninth arm: a fleet repo's org derived from its clone's origin (Story 219.1.1) ----

// The index of a stage's command word — past `export`, assignments, keywords
// and wrappers, and past `mise exec --` when the stage runs through mise — or
// -1 when the stage has none (an assignment-only stage, a `mise exec` with no
// `--`). Shared by arms 8 and 9.
function commandWordIndex(stage) {
  let i = stage[0] && isBare(stage[0]) && stage[0][0].text === 'export' ? 1 : 0;
  while (i < stage.length) {
    const t = stage[i][0].q === '' ? wordText(stage[i]) : '';
    if (/^[A-Za-z_]\w*=/.test(t) || KEYWORDS.has(t)) i++;
    else if (WRAPPERS.has(t)) i = wrapperArgsEnd(stage, i + 1, t);
    else break;
  }
  if (i < stage.length && wordText(stage[i]) === 'mise' && stage[i + 1] && wordText(stage[i + 1]) === 'exec') {
    const dash = stage.findIndex((w, k) => k > i + 1 && wordText(w) === '--');
    if (dash === -1) return -1;
    i = dash + 1;
  }
  return i < stage.length ? i : -1;
}

// The index past a wrapper's own arguments (Story 233.1.2): `timeout` consumes its
// options (`-s SIG`/`-k N`/`--signal SIG`/`--kill-after N` with a separate value, any
// other `-…` word alone) and then ONE duration word; `xargs` consumes its options
// (-n -I -P -L -d -a -E -s -J with a separate value, any other `-…` word alone).
// Every other wrapper consumes only its own word. An option whose value is missing
// runs the index off the end — no command word, nothing judged.
function wrapperArgsEnd(stage, i, wrapper) {
  const bare = (k) => (stage[k] && isBare(stage[k]) ? wordText(stage[k]) : null);
  if (wrapper === 'timeout') {
    while (i < stage.length) {
      const t = bare(i);
      if (t === '-s' || t === '-k' || t === '--signal' || t === '--kill-after') i += 2;
      else if (t && t.startsWith('-')) i++;
      else break;
    }
    return i + 1;
  }
  if (wrapper === 'xargs') {
    while (i < stage.length) {
      const t = bare(i);
      if (t && /^-[nIPLdaEsJ]$/.test(t)) i += 2;
      else if (t && t.startsWith('-')) i++;
      else break;
    }
  }
  return i;
}

// `OWNER/REPO` read from a word — bare, `HOST/OWNER/REPO`, or a GitHub URL
// (https, scp, ssh); a trailing `.git` or a path after the name is dropped.
// `hosted` says the word carried a github.com host. Null when it is no slug.
function slugOf(word) {
  const hosted = GITHUB_HOST.test(word);
  const m = /^([A-Za-z0-9_.-]+)\/([A-Za-z0-9_.-]+?)(?:\.git)?(?:\/|$)/.exec(word.replace(GITHUB_HOST, ''));
  if (!m || /^\.\.?$/.test(m[1]) || /^\.\.?$/.test(m[2])) return null;
  return { org: m[1], name: m[2], hosted };
}

// The owner, name and URL of a clone's origin — a GitHub origin only, read
// only over a directory with its OWN .git (a plain folder walks git up to the
// enclosing repository: MEASURED 2026-10-20, projects/tigerdash-reference
// printed the hub's URL). Null otherwise.
function originOf(dir) {
  if (!existsSync(path.join(dir, '.git'))) return null;
  let url;
  try {
    url = execFileSync('git', ['-C', dir, 'remote', 'get-url', 'origin'], {
      encoding: 'utf8',
      stdio: ['ignore', 'pipe', 'ignore'],
    }).trim();
  } catch {
    return null;
  }
  const slug = slugOf(url);
  return slug?.hosted ? { org: slug.org, name: slug.name, origin: url } : null;
}

// The fleet clone a repo NAME derives from: the session root's own clone when
// its origin names that repo, else `projects/<name>` under the root or any
// ancestor of it (a site-rooted session reaches up to the hub's projects/).
// Null when nothing derives — the arm then judges nothing. Memoized per name
// (DERIVED_ORGS); the root's origin read once (rootOrigin, above).
function deriveOrg(name) {
  const key = name.toLowerCase();
  if (DERIVED_ORGS.has(key)) return DERIVED_ORGS.get(key);
  let found = null;
  let root = null;
  try {
    root = realpathSync(sessionRoot());
  } catch {
    root = null;
  }
  if (root) {
    rootOrigin ??= originOf(root) ?? false;
    if (rootOrigin && rootOrigin.name.toLowerCase() === key) found = { ...rootOrigin, dir: root };
    for (let d = root; !found; ) {
      const dir = path.join(d, 'projects', name);
      if (existsSync(path.join(dir, '.git'))) {
        const o = originOf(dir);
        if (o) found = { ...o, dir };
        break;
      }
      const parent = path.dirname(d);
      if (parent === d) break;
      d = parent;
    }
  }
  DERIVED_ORGS.set(key, found);
  return found;
}

// Returns { shown, slug, org, name, derived, derivedOrg, origin, dir } for the
// first word naming a fleet repo under an org other than its clone's own, else
// null. Read per stage: a gh stage's -R/--repo value (spaced, =-joined, glued,
// quoted), a GH_REPO= assignment, a `gh api` repos/ path, a `gh repo <sub>`
// positional and any github.com URL word; in every other stage a BARE
// github.com URL word only. Heredoc bodies and here-strings are data.
function orgMismatch(command) {
  let text = command;
  if (isPowerShell) text = text.replace(/@(['"])\r?\n[\s\S]*?\r?\n\1@/g, '');
  for (const stage of shellWords(dropHeredocs(text))) {
    const candidates = [];
    for (const w of stage) {
      const t = isBare(w) ? w[0].text : '';
      const m = /^GH_REPO=(.+)$/.exec(t);
      if (m) candidates.push({ shown: t, text: m[1] });
    }
    const i = commandWordIndex(stage);
    const verb = i !== -1 && isBare(stage[i]) ? wordText(stage[i]) : '';
    const isGh = /^gh(?:\.exe)?$/i.test(verb.slice(Math.max(verb.lastIndexOf('/'), verb.lastIndexOf('\\')) + 1));
    const args = i === -1 ? [] : stage.slice(i + 1);
    const sub = isGh && args[0] && isBare(args[0]) ? wordText(args[0]) : '';
    for (let k = 0; k < args.length; k++) {
      const t = wordText(args[k]);
      if (isGh) {
        if (t === '-R' || t === '--repo') {
          if (args[k + 1]) candidates.push({ shown: `${t} ${wordText(args[k + 1])}`, text: wordText(args[k + 1]) });
          k++;
          continue;
        }
        const joined = /^(?:--repo=|-R)(.+)$/.exec(t);
        if (joined) {
          candidates.push({ shown: t, text: joined[1] });
          continue;
        }
        if (sub === 'api' && k >= 1) {
          const m = /^\/?repos\/(.+)$/.exec(t);
          if (m) {
            candidates.push({ shown: t, text: m[1] });
            continue;
          }
        }
        if (sub === 'repo' && k >= 2 && !t.startsWith('-')) {
          candidates.push({ shown: t, text: t });
          continue;
        }
      }
      if (GITHUB_HOST.test(t) && !/\s/.test(t) && (isGh || isBare(args[k]))) candidates.push({ shown: t, text: t });
    }
    for (const c of candidates) {
      const slug = slugOf(c.text);
      if (!slug) continue;
      const found = deriveOrg(slug.name);
      if (!found || found.org.toLowerCase() === slug.org.toLowerCase()) continue;
      return {
        shown: c.shown,
        slug: `${slug.org}/${slug.name}`,
        org: slug.org,
        name: slug.name,
        derived: `${found.org}/${found.name}`,
        derivedOrg: found.org,
        origin: found.origin,
        dir: found.dir,
      };
    }
  }
  return null;
}

// Story 179.1.1: does a bare part open with [A-Za-z0-9_]? Read by character
// code, so the face adds no regex (P25's two-axis proof is not triggered).
function opensWordChar(text) {
  const c = text.charCodeAt(0);
  return (c >= 48 && c <= 57) || (c >= 65 && c <= 90) || (c >= 97 && c <= 122) || c === 95;
}

// Story 128.1.7 — the replace face. A JS string literal opening at js[i]:
// { value (escapes decoded), end (just past the closing quote) }, else null.
function readJsString(js, i) {
  const q = js[i];
  let value = '';
  let j = i + 1;
  while (j < js.length && js[j] !== q) {
    if (js[j] === '\\' && j + 1 < js.length) {
      value += js[j + 1];
      j += 2;
    } else value += js[j++];
  }
  return j < js.length ? { value, end: j + 1 } : null;
}

// From just after `.replace(`, skip the first argument to its top-level comma
// (strings, regex literals and brackets respected); null when the call closes
// first or the program ends.
function skipFirstArg(js, i) {
  let depth = 0;
  let prev = '(';
  while (i < js.length) {
    const c = js[i];
    if (c === '"' || c === "'" || c === '`') {
      const s = readJsString(js, i);
      if (!s) return null;
      i = s.end;
      prev = c;
      continue;
    }
    if (c === '/' && '(,=:[!&|?{};'.includes(prev)) {
      let j = i + 1;
      let cls = false;
      while (j < js.length && (cls || js[j] !== '/')) {
        if (js[j] === '\\') j++;
        else if (js[j] === '[') cls = true;
        else if (js[j] === ']') cls = false;
        j++;
      }
      i = j + 1;
      while (i < js.length && /[a-z]/i.test(js[i])) i++;
      prev = '/';
      continue;
    }
    if ('([{'.includes(c)) depth++;
    else if (')]}'.includes(c)) {
      if (depth === 0) return null;
      depth--;
    } else if (c === ',' && depth === 0) return i + 1;
    if (!/\s/.test(c)) prev = c;
    i++;
  }
  return null;
}

// The first `.replace(` / `.replaceAll(` whose second argument is a string
// literal holding a splice pattern — $& (the match), $` (the text before it),
// $' (the text after it), $$ (one dollar sign): { pattern, shown }, else null.
// A template literal's ${…} interpolations are dropped first; $1 and $<name>
// are capture references, intended with a regex, and never flagged. Bounded:
// at most 64 calls are read.
function replaceDollarPattern(js) {
  const patterns = ['$&', '$$', "$'", '$`'];
  let calls = 0;
  for (const m of js.matchAll(/\.replace(?:All)?\(/g)) {
    if (++calls > 64) break;
    let i = skipFirstArg(js, m.index + m[0].length);
    if (i === null) continue;
    while (i < js.length && /\s/.test(js[i])) i++;
    if (js[i] !== '"' && js[i] !== "'" && js[i] !== '`') continue;
    const s = readJsString(js, i);
    if (!s) continue;
    const value = js[i] === '`' ? s.value.split(/\$\{[^}]*\}/).join('') : s.value;
    const pattern = patterns.find((p) => value.includes(p));
    if (pattern) return { pattern, shown: js.slice(i, s.end).slice(0, 80) };
  }
  return null;
}

// --- second arm: the drifted shell -----------------------------------------

// The session root: CLAUDE_PROJECT_DIR (the variable the hook wiring itself
// expands to reach this file), else two levels above this file — every synced
// copy lives at <repo>/scripts/warn-cwd-drift.mjs, so the two agree.
// --- tenth arm: a seconds-cadence gh poll loop (Story 220.1.3; the loop-head rule, Story 233.1.2) ----
// Bash: a `while`/`until`/`for` at command position with a `do` after it, the span to the
// `done` that MATCHES its `do` by depth (Story 233.1.2 — a nested loop's `done` no longer
// cuts the outer span short) or the end; the PowerShell tool: `while`/`for`/`foreach`/`do`
// at command position, the span to the matching close brace of the body or the end. Quoted
// data is neutralized first (a `$(gh …)` inside double quotes stays live). The HEAD and the
// BODY come apart (Story 233.1.2): a `for`'s head is its VAR and word list — a gh there is
// one call, so the body is judged alone; a `while`/`until` head is the condition, judged
// WITH the body (`until gh pr checks …; do sleep 15; done` reads gh in the condition).
// Inside the judged text: the `gh` stages (gh at command position — arms 8 and 9's stage
// walk, `timeout`/`xargs` skipped with their arguments) and the SMALLEST sleep — `sleep
// N[smhd]`/`sleep N.N` in Bash, a quoted literal (`sleep "90"`) READ from the neutralizer's
// placeholder; `Start-Sleep [-Seconds|-s|-Milliseconds|-ms] N` or `sleep N` under PowerShell
// — a sleep whose argument is not a literal number (`"$N"`, `${N}`, `$((…))`, `-Sec 90`) is
// UNREAD and named so, never as none. THE LOOP-HEAD RULE: a `for VAR in` over LITERAL words
// (no `$(`, no backtick, no `seq` word, no `{a..b}` range; a `$NAME` word is a list fixed
// before the loop and counts as literal) or a `while read …` is BOUNDED. A bounded loop whose
// every gh stage carries `$VAR` (a different resource per iteration) and sleeps 60 s or not
// at all is an ENUMERATION — silent. `$VAR` is the loop's variable or one DERIVED from it in
// the body, to a fixed point (Story 242.1.4, derivedVars: an assignment whose value expands a
// member, `set -- $VAR`'s positionals, `read … <<< $VAR`'s targets). A bounded loop whose gh stage carries no `$VAR` is the
// same read N times — a BURST, warned with the count ("reads gh 3 times with no sleep"; "once
// per input line" for a `while read`; "once per argument" for a bare `for x; do`). `while
// true|:`, `until`, `for ((…))` and a generated list keep the shape predicate. The face is
// the worst loop: { shape, verb, cadence, seconds } with seconds null for no sleep; null when
// no loop reads gh under the floor. Named limits: the PowerShell faces keep the shape
// predicate (no loop-head rule there); a bounded enumeration sleeping under 60 s keeps the
// cadence warning (the register's decided form); a bare `done` word used as an argument
// (`echo done`) closes the span early — the sleep after it falls outside, and the arm errs
// toward warning; `watch -n 5 gh …` carries no loop keyword and is outside the predicate; a
// derived variable that does not vary per iteration (`n=${i:+42}`) reads as varying (Story
// 242.1.4's accepted limit — the arm errs toward silence there).
// (Constants live inside the functions: the main flow above runs before a top-level `const`
// down here would initialize — the helper section is declarations only.)
function pollCadenceFace(command) {
  const POLL_FLOOR_SECONDS = 60;
  const { text, values } = neutralize(command);
  const spans = isPowerShell ? powershellLoopSpans(text) : bashLoopSpans(text);
  let worst = null;
  for (const span of spans) {
    // `${NAME}` reads as `$NAME` here: the stage walk splits on braces, and the braced form is
    // the same variable to the loop-head rule (the arm's own view of the text, nothing else's).
    const judged = (span.shape === 'for' ? span.body : `${span.head}\n${span.body}`).replace(
      /\$\{([A-Za-z_]\w*)\}/g,
      (_, v) => `$${v}`
    );
    const reads = ghStages(judged);
    if (!reads.length) continue;
    const sleep = sleepReading(judged, values);
    if (sleep.seconds !== null && sleep.seconds >= POLL_FLOOR_SECONDS) continue;
    let verb = 'polls gh';
    let cadence;
    if (sleep.seconds !== null) cadence = `every ${sleep.seconds} s`;
    else if (sleep.unread) cadence = 'with a sleep the guard cannot read';
    else {
      const bounded = boundedHead(span);
      if (bounded) {
        // Story 242.1.4: the loop variable's set grown by the body's derivations, to a fixed point.
        const vars = derivedVars(judged, bounded.vars, values);
        if (vars.length && reads.every((stage) => stageCarriesVar(stage, vars, values))) continue;
        verb = 'reads gh';
        cadence = `${bounded.per} with no sleep`;
      } else cadence = 'with no sleep';
    }
    const worse = !worst || sleep.seconds === null || (worst.seconds !== null && sleep.seconds < worst.seconds);
    if (worse) worst = { shape: span.shape, verb, cadence, seconds: sleep.seconds };
  }
  return worst;
}

// Bash loop spans: { shape, head, body }. The head is the text between the keyword and its
// `do`; the body runs from that `do` to the `done` that matches it by depth, or to the end
// when the loop is cut off. Quoted spans are placeholders by now, so a `-m "done"` is data;
// a BARE `done` word counts as a close (the named limit — err-toward-warning).
function bashLoopSpans(text) {
  const spans = [];
  const re = /(?<=^|[;&|(){}\n])\s*(while|until|for)\b/g;
  let m;
  while ((m = re.exec(text))) {
    const start = m.index + m[0].length - m[1].length;
    const rest = text.slice(start);
    const open = /\bdo\b/.exec(rest);
    if (!open) continue; // a `for`/`while` word with no loop body is not a loop
    const head = rest.slice(m[1].length, open.index);
    const walk = /\b(do|done)\b/g;
    walk.lastIndex = open.index;
    let depth = 0;
    let end = rest.length;
    let w;
    while ((w = walk.exec(rest))) {
      if (w[1] === 'do') depth++;
      else if (--depth === 0) { end = w.index; break; }
    }
    spans.push({ shape: m[1], head, body: rest.slice(open.index + open[0].length, end) });
  }
  return spans;
}

// PowerShell loop spans: { shape, head: '', body } — the whole span is the body (the
// loop-head rule is Bash-only, a named limit).
function powershellLoopSpans(text) {
  const spans = [];
  const re = /(?<=^|[;{}|\n])\s*(while|for|foreach|do)\b/gi;
  let m;
  while ((m = re.exec(text))) {
    const start = m.index + m[0].length - m[1].length;
    const open = text.indexOf('{', start);
    if (open === -1) continue;
    let depth = 0;
    let end = text.length;
    for (let i = open; i < text.length; i++) {
      if (text[i] === '{') depth++;
      else if (text[i] === '}' && --depth === 0) { end = i + 1; break; }
    }
    spans.push({ shape: m[1].toLowerCase(), head: '', body: text.slice(start, end) });
  }
  return spans;
}

// The stages of the judged text that read `gh` at command position (arms 8 and 9's walk —
// wrappers and their arguments skipped, so `timeout 30 gh …` and `xargs -n 1 gh …` read as gh).
function ghStages(text) {
  const out = [];
  for (const stage of shellWords(text)) {
    const i = commandWordIndex(stage);
    if (i !== -1 && isBare(stage[i]) && wordText(stage[i]) === 'gh') out.push(stage);
  }
  return out;
}

// The loop-head rule (Story 233.1.2): { vars, per } for a BOUNDED head — a `for VAR in` over
// literal words (per = "N times"; "once per argument" for a bare `for x; do`) or a `while
// read …` (per = "once per input line"; the variables read, `REPLY` when none) — or null for
// an unbounded one (`while true|:`, `until`, `for ((…))`, a `$(…)`/backtick/`seq`/brace-range
// list, a condition that is not a lone `read`, the PowerShell tool).
function boundedHead(span) {
  if (isPowerShell) return null;
  const head = span.head.replace(/[;\s]+$/, '').trim();
  if (span.shape === 'for') {
    const m = /^([A-Za-z_]\w*)(?:\s+in\b([\s\S]*))?$/.exec(head);
    if (!m) return null;
    const list = (m[2] ?? '').trim();
    if (/\$\(|`|\bseq\b|\{[^{}]*\.\.[^{}]*\}/.test(list)) return null;
    if (m[2] === undefined) return { vars: [m[1]], per: 'once per argument' };
    const count = list ? list.split(/\s+/).length : 0;
    return { vars: [m[1]], per: `${count} ${count === 1 ? 'time' : 'times'}` };
  }
  if (span.shape === 'while') {
    const stages = shellWords(head);
    if (stages.length !== 1) return null;
    const stage = stages[0];
    const i = commandWordIndex(stage);
    if (i === -1 || !isBare(stage[i]) || wordText(stage[i]) !== 'read') return null;
    const vars = [];
    for (let k = i + 1; k < stage.length; k++) {
      const t = isBare(stage[k]) ? wordText(stage[k]) : null;
      if (t === null) continue;
      if (/^-[dnNptuai]$/.test(t)) { k++; continue; }
      if (t.startsWith('-')) continue;
      if (/^[A-Za-z_]\w*$/.test(t)) vars.push(t);
    }
    return { vars: vars.length ? vars : ['REPLY'], per: 'once per input line' };
  }
  return null;
}

// Story 242.1.4 (the loop-head rule's DERIVED variable — register class
// poll-cadence-arm-warns-on-a-bounded-enumeration, its derived-variable seats): the bounded loop's
// variable set grown to a fixed point over the judged text. A `NAME=VALUE` word (an assignment-only
// stage, an env prefix, or after `export`/`local`/`declare`/`typeset`/`readonly`) joins when VALUE
// expands a member — `$V`, `${V…}` with any modifier, inside `$(…)`, backticks or double quotes;
// `set -- … $V …` adds the positionals 1–9, `@` and `*`; `read [opts] A B … <<< …$V…` adds A, B
// and the rest (`REPLY` when none). shellWords splits stages on parens and backticks, so each `$(…)`
// and backtick span is collapsed to one \x01N\x01 placeholder first, its text kept for the test.
function derivedVars(text, seed, values) {
  const subs = [];
  const stages = shellWords(collapseSubstitutions(text, subs));
  const resolve = (t) => {
    let out = t;
    for (let prev = null; out !== prev; ) {
      prev = out;
      out = out
        .replace(/\x01(\d+)\x01/g, (_, k) => subs[Number(k)] ?? '')
        .replace(/\x00(\d+)\x00/g, (_, k) => values[Number(k)] ?? '');
    }
    return out;
  };
  const vars = new Set(seed);
  const expands = (t) => [...vars].some((v) => expandsVar(resolve(t), v));
  const POSITIONALS = ['1', '2', '3', '4', '5', '6', '7', '8', '9', '@', '*'];
  for (let grew = true; grew; ) {
    grew = false;
    const add = (names) => {
      for (const n of names) if (!vars.has(n)) { vars.add(n); grew = true; }
    };
    for (const stage of stages) {
      for (const [name, value] of stageAssignments(stage)) if (expands(value)) add([name]);
      const i = commandWordIndex(stage);
      if (i === -1 || !isBare(stage[i])) continue;
      const cmd = wordText(stage[i]);
      if (cmd === 'set' && stage[i + 1] && wordText(stage[i + 1]) === '--') {
        if (stage.slice(i + 2).some((w) => expands(wordText(w)))) add(POSITIONALS);
      } else if (cmd === 'read') {
        const names = [];
        let source = null;
        for (let k = i + 1; k < stage.length; k++) {
          const t = wordText(stage[k]);
          if (t.startsWith('<<<')) {
            source = t.length > 3 ? t.slice(3) : stage[k + 1] ? wordText(stage[k + 1]) : '';
            break;
          }
          if (!isBare(stage[k])) continue;
          if (/^-[dnNptuai]$/.test(t)) { k++; continue; }
          if (t.startsWith('-')) continue;
          if (/^[A-Za-z_]\w*$/.test(t)) names.push(t);
        }
        if (source !== null && expands(source)) add(names.length ? names : ['REPLY']);
      }
    }
  }
  return [...vars];
}

// A stage's assignment words as [NAME, VALUE] pairs: the leading `NAME=VALUE` words (an
// assignment-only stage or an env prefix), or those after a declaring builtin and its flags.
function stageAssignments(stage) {
  const out = [];
  let k = 0;
  if (stage[0] && isBare(stage[0]) && /^(?:export|local|declare|typeset|readonly)$/.test(wordText(stage[0]))) {
    k = 1;
    while (k < stage.length && isBare(stage[k]) && wordText(stage[k]).startsWith('-')) k++;
  }
  for (; k < stage.length; k++) {
    const w = stage[k];
    const m = w[0].q === '' ? /^([A-Za-z_]\w*)=([\s\S]*)$/.exec(w[0].text) : null;
    if (!m) break;
    out.push([m[1], m[2] + w.slice(1).map((p) => p.text).join('')]);
  }
  return out;
}

// Each balanced `$(…)` (or `$((…))`), each `${…}` (shellWords splits on braces too, so
// `r=${p%%:*}` would read as `r=$`) and each backtick span becomes one \x01N\x01 placeholder,
// its full text pushed to `subs` (quoted spans are already the neutralizer's placeholders, so a
// paren inside quotes never counts). An unbalanced span runs to the end.
function collapseSubstitutions(text, subs) {
  let out = '';
  for (let i = 0; i < text.length; i++) {
    let end = -1;
    const open = text[i] === '$' ? { '(': ')', '{': '}' }[text[i + 1]] : undefined;
    if (open) {
      let depth = 0;
      for (let j = i + 1; j < text.length; j++) {
        if (text[j] === text[i + 1]) depth++;
        else if (text[j] === open && --depth === 0) { end = j; break; }
      }
      if (end === -1) end = text.length - 1;
    } else if (text[i] === '`') {
      end = text.indexOf('`', i + 1);
      if (end === -1) end = text.length - 1;
    }
    if (end === -1) { out += text[i]; continue; }
    subs.push(text.slice(i, end + 1));
    out += `\x01${subs.length - 1}\x01`;
    i = end;
  }
  return out;
}

// Does the text expand variable `v` — `$v` or `${v…}` (any modifier), never `$vx`? `v` may be a
// positional (`1`, `@`, `*`), escaped for the pattern.
function expandsVar(t, v) {
  const name = v.replace(/[.*+?^${}()|[\]\\]/g, '\\$&');
  return new RegExp(`\\$\\{?${name}(?![\\w])`).test(t);
}

// A word of a gh stage that expands one of the loop's variables — `$n`, `${n}`, `"$n"` (the
// quoted span's value resolved from the neutralizer's placeholders), `$n/…`; since Story 242.1.4
// the set carries the derived variables and positionals too (`$2`, `"$@"`).
function stageCarriesVar(stage, vars, values) {
  const resolve = (t) => t.replace(/\x00(\d+)\x00/g, (_, k) => values[Number(k)] ?? '');
  return stage.some((w) => {
    const t = resolve(wordText(w));
    return vars.some((v) => expandsVar(t, v));
  });
}

// The smallest sleep in the judged text, { seconds, unread }: seconds null when there is no
// readable sleep; unread true when a sleep's argument is not a literal number (a quoted
// literal is resolved from the neutralizer's placeholders and READ).
function sleepReading(text, values) {
  const UNIT_SECONDS = { s: 1, m: 60, h: 3600, d: 86400 };
  let min = null;
  let unread = false;
  const take = (seconds) => { if (min === null || seconds < min) min = seconds; };
  const literal = (arg) => arg.replace(/\x00(\d+)\x00/g, (_, k) => values[Number(k)] ?? '');
  if (isPowerShell) {
    const re = /(?<![\w-])(?:Start-Sleep|sleep)\b(?:\s+-(Seconds?|s|Milliseconds|ms)\b)?\s+([^\s;|})]+)/gi;
    let m;
    while ((m = re.exec(text))) {
      const arg = literal(m[2]);
      if (/^\d+(?:\.\d+)?$/.test(arg)) take(/^m/i.test(m[1] ?? '') ? Number(arg) / 1000 : Number(arg));
      else unread = true;
    }
  } else {
    const re = /(?<![\w-])sleep\s+([^\s;&|)]+)/g;
    let m;
    while ((m = re.exec(text))) {
      const n = /^(\d+(?:\.\d+)?)([smhd])?$/.exec(literal(m[1]));
      if (n) take(Number(n[1]) * UNIT_SECONDS[n[2] ?? 's']);
      else unread = true;
    }
  }
  return { seconds: min, unread };
}

function sessionRoot() {
  const env = process.env.CLAUDE_PROJECT_DIR;
  if (env && path.isAbsolute(env)) return env;
  return path.resolve(path.dirname(fileURLToPath(import.meta.url)), '..');
}

// Nearest ancestor (inclusive) holding `.git` — a directory (a clone) or a
// FILE (a worktree) — else null. A repository boundary, not a repo root check.
function nearestGit(dir) {
  let d = dir;
  for (;;) {
    if (existsSync(path.join(d, '.git'))) return d;
    const parent = path.dirname(d);
    if (parent === d) return null;
    d = parent;
  }
}

// Does the command's FIRST stage move the shell itself (`cd`/`pushd`)? Then
// the cwd this arm would read is moot, and the first arm has already judged
// where the move goes.
function firstStageIsCd(command) {
  const { text } = neutralize(command);
  const stages = text
    .split(STAGE_SPLIT)
    .map((s) => s.trim())
    .filter(Boolean);
  if (!stages.length) return false;
  const tokens = stages[0].split(/\s+/).filter(Boolean);
  let i = 0;
  while (i < tokens.length && (KEYWORDS.has(tokens[i]) || /^[A-Za-z_][A-Za-z0-9_]*=/.test(tokens[i]) || WRAPPERS.has(tokens[i]))) i++;
  return i < tokens.length && isCdVerb(tokens[i]);
}

// Stand down loudly: a warn-level arm with nothing judgeable says so on
// stderr and returns null (exit stays 0 — fail-open).
function standDown(reason) {
  console.error(`warn-cwd-drift: ${reason} — the drifted-shell arm stands down (warn-level, fail-open)`);
  return null;
}

// Returns { cwd, shellRepo, root } when the shell's cwd resolves to a
// different repository than the session root; null when it does not, when
// the call moves the shell itself, or when there is nothing to judge.
function driftedShell(command, cwd) {
  if (typeof cwd !== 'string' || !path.isAbsolute(cwd)) {
    return standDown('payload cwd is missing or relative');
  }
  if (firstStageIsCd(command)) return null;
  let realCwd;
  try {
    realCwd = realpathSync(cwd);
  } catch {
    return standDown(`payload cwd ${cwd} does not exist`);
  }
  const rootRaw = sessionRoot();
  let realRoot;
  try {
    realRoot = realpathSync(rootRaw);
  } catch {
    return standDown(`session root ${rootRaw} does not exist`);
  }
  const rootRepo = nearestGit(realRoot) ?? realRoot;
  const shellRepo = nearestGit(realCwd);
  if (!shellRepo) return standDown(`no .git above payload cwd ${cwd}`);
  if (shellRepo === rootRepo) return null;
  return { cwd: realCwd, shellRepo, root: rootRepo };
}

// Story 128.1.8 (register class stdin-readfilesync-eagain-on-nonblocking-pipe):
// stdin through the async iterator, never readFileSync(0) — a 1.1 MB payload on a
// non-blocking pipe read as unreadable on Linux CI (Node 24, hub #3034's first run).
async function readStdin() {
  const chunks = [];
  for await (const chunk of process.stdin) chunks.push(chunk);
  return Buffer.concat(chunks).toString('utf8');
}
