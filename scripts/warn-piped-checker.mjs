#!/usr/bin/env node
// warn-piped-checker.mjs — PreToolUse hook: WARN when a shell-tool compound
// (Bash, or the PowerShell tool — Story 73.1.1) pipes a fleet checker (Story
// 24.3.15; any check-*.mjs by pattern since Story 104.1.1; the verdict scripts
// not named that way since Story 115.1.1), a `node --test` battery (Story
// 24.3.26), or an rspec suite invocation (Story 30.1.2) into another command —
// or (Chore 10, the semicolon arm) sequences a gate with `;` ahead of a
// state-changing verb; or (Story 139.1.1, the shape arm) reads `$?` /
// `$LASTEXITCODE` right after ANY pipeline; or (Story 167.1.1, the background
// arm) runs in the background with its last pipeline piping a gate into tail,
// head or grep, whose exit the harness's task notification reports; or (Story
// 244.1.8, the git-verb arm) pipes a state-changing git command into anything;
// and BLOCK three shapes with no legitimate reading: a recognized verdict run ended by `;` ahead of `git
// commit` / `git push` (Story 115.1.1), a `gh pr merge` / `gh pr checks` gate
// ended by `;` ahead of a local branch change (Story 126.1.1), and a `git
// push` ended by `;` ahead of `gh pr merge` (Story 140.1.2).
// CANONICAL COPY lives in the hub (scripts/warn-piped-checker.mjs) and is
// synced verbatim into each repo under projects/ by scripts/sync-session-wrap.mjs
// — do NOT hand-edit a synced copy; edit the hub canonical and re-run the sync.
//
// Why this exists — the three-recurrence citation (the ≥2× rule, Story
// 24.3.1's pattern): the pipe-masks-`$?` class recurred across three sprints —
// deccentral.com Sprint 1 legs 1+5, hub Sprint 10 F3, hub Sprint 11 F1 —
// every instance self-caught, zero damage, and the WRITTEN UNPIPED rule (the
// sprint merge-loop line + KB `sprint-proposal-standard` step 6) did not
// retire the habit. When `node scripts/check-fleet.mjs | tail -30` runs, the
// compound's exit code is tail's, not the checker's — so an "exit 0 cited"
// proof gated on `$?` proves nothing. This guard makes the session SEE the
// masking at the moment it happens. FOURTH strike, and the first OUTSIDE the
// checker set: hub Sprint 15 E1 (`node --test … | tail -9; echo $?` — the
// echoed exit was tail's), which widened the guard to piped `node --test`
// batteries — the leg-gate proof surface every sprint merge loop gates on
// (Story 24.3.26; adjudication in the Sprint 15 friction log, triage item 1).
// FIFTH strike, the first in the rspec lane: the hub 29.1.1 Rails-on-Windows
// bring-up's suite wrapper (`bundle exec rspec … | tail`) masked rspec's exit
// at first read — caught by reading the printed tally — which widened the
// guard to rspec/bundle-exec suite invocations, `mise exec --`-wrapped forms
// included (Story 30.1.2, the Epic 29.1 close's recurrence-prevention
// decision, Omid's in-session go).
//
// Warn-level BY DESIGN for a PIPE (per the verification-loop taxonomy:
// standalone, session layer, warn — never a gate): piping a checker is
// legitimate when the exit is not the thing being proven (trimming a long
// listing during exploration). So a pipe, and every semicolon shape but the
// three blocks below, emits NO permissionDecision — the command proceeds — and surfaces
// `additionalContext` (reaches the model: re-run UNPIPED if this run is the
// exit-code proof) plus a `systemMessage` (reaches the user). See
// docs/kb/hook-mechanics.md: exit-0 stdout without hookSpecificOutput lands in
// the debug log only. THE FIRST BLOCK (Story 115.1.1 — register class
// semicolon-chained-checker-does-not-gate-commit, 18 seats at its filing; the
// Chore 10 warning fired as lawcuments-app #606's red tick commit landed and
// pushed anyway): a gate the pipe arm recognizes — a checker, a verdict
// script, a battery, a suite — ended by `;` (or a newline) with `git commit`
// or `git push` in a later clause has no legitimate reading, so it emits
// `permissionDecision: "deny"` (block-commit-on-main.mjs's mechanism) with
// the advice as the reason; the command never runs, and re-issuing it with
// `&&` is the whole remedy. The same stand-downs as the warning apply (`||
// exit`, `set -e`, an `if` / `[` between). THE SECOND BLOCK, the merge face
// (Story 126.1.1, hub Initiative 126 — Omid's "Deny the merge face",
// 2026-09-30): `gh pr merge` / `gh pr checks` as the gate, ended by `;` (or a
// newline), with a LOCAL branch change — `git switch`, `checkout`, `branch
// -D`, `commit`, `push` — in a later clause is denied the same way, with the
// same stand-downs. Story 115.1.1 had kept it at warn ("a merge is gated
// server-side by the ruleset's required checks"), and it kept recurring:
// nine seats, two on a real red (tigerdash #220 ran `branch -D` after a
// refused merge; lawcuments-app #615 switched after a failed one). What
// still only warns: a merge after a lane watch or a checker with no local
// verb after it (the merge IS gated server-side), and every other verb after
// a checker; `gh pr create` changes no branch, so it is no gate. THE
// RE-ANCHOR (Story 140.1.1 — the Hub Sprint 42 reviewer pass's I-1, register
// class guard-deny-on-an-already-gated-shape): the merge face anchored on the
// EARLIEST gh gate, so `gh pr checks 5 --watch; gh pr merge 5 --squash && git
// switch main` was refused though its switch runs only on a merged PR. A
// later-clause gh gate now re-anchors the deny on its own clause, keeping the
// earliest gate's name for the message; under `set -e` a later gh gate ends
// the script when it fails, so it clears the anchor. `timeout [flags] <n>`
// is read past to the command it wraps, and a force delete spelled `--delete
// --force` / `-d -f` / `-df` counts as `branch -D`. THE THIRD BLOCK, the
// stale-head face (Story 140.1.2 — fragment
// pantheon-auto-merge-arm-after-ungated-update-push, register class
// semicolon-chained-checker-does-not-gate-commit seat 31): pantheon #423's
// approval record ran `git push … | tail -1; … && gh pr merge 423 --auto
// --squash`. The push landed — but had it failed, auto-merge would have landed
// the PR's previous green head, the proposal without its sign-off: the ruleset
// judges the head it has. So a `git push` ended by `;` (or a newline) with `gh
// pr merge` (with or without `--auto`) in a later clause is denied, with the
// commit face's stand-downs; the `&&` chain is one clause and stays silent, and
// a push piped into `tail` is still the push stage.
//
// THE SHAPE ARM (Story 139.1.1, hub Initiative 139 — Omid's "File both as
// recommended", 2026-10-01): the recognizers below know a piped gate BY NAME,
// and register class piped-checker-exit's misses since the hook decision came
// by SHAPE — six of its eight caught:false seats read `$?` or `$LASTEXITCODE`
// after a pipeline, two of them a bare `grep … | head; echo "grep exit=$?"`
// (seats 28 and 30) that no name list can reach, one a `gh pr checks …|
// tail` (seat 23). So the guard also WARNS, never denies, when the FIRST
// command after a pipeline of two or more stages reads `$?` (Bash or zsh) or
// `$LASTEXITCODE` (the PowerShell tool, case-insensitive), whatever the
// pipeline ran: that read is the last stage's exit. Silent under an earlier
// `set -o pipefail` (`-euo pipefail` too) or zsh `setopt pipefail`, when that
// command reads `${PIPESTATUS…}` / `$pipestatus` in its place, when any other
// command runs between (it resets the exit), for a single-quoted `$?` (it
// never expands), and when the pipe arm already warned (its advice covers the
// read). The shared neutralizer decides what is a pipe — a `|` inside a quoted
// pattern is not one — and now records each quoted span's quote character.
//
// THE GIT-VERB ARM (Story 244.1.8, hub Initiative 244 — register class
// piped-checker-exit, the pen's two seats of 2026-10-26, hub #4358): `git merge
// --ff-only origin/main 2>&1 | tail -n 1` hid a refusal twice (15:55:07Z,
// 19:39:26Z) — the tail showed one line, not whether the checkout moved, and no
// `$?` was read, so the shape arm above never saw it. So the guard also WARNS,
// never denies, when a NON-FINAL pipe stage runs `git` whose verb is exactly
// `merge`, `pull`, `checkout`, `rebase` or `push` (push joined at Story 244.1.34;
// GIT_STATE_VERBS holds the five), whether or not a `$?` follows — the
// remedy is to run it unpiped and read its exit. Global options before the verb
// are read past: `-C PATH`, `-c KEY=VALUE`, `--git-dir[=]PATH`,
// `--work-tree[=]PATH`, `--no-pager`; any other option there ends the read
// (silent). The verb is the WHOLE word, so `merge-base`, `checkout-index` and
// every read verb stay silent, as do `gh pr checkout` (not git) and an unpiped
// merge. Judged (recorded in the Hub Sprint 68 log): `git rebase
// --show-current-patch` is a read (silent); `git checkout -- PATH` is a write (it
// warns). A `$?` read after such a pipe is named by this arm alone (the shape arm
// folds in). Census (P74, the hub's docs/ at the leg's base): every recorded
// command that pipes one of these verbs (four at that census; `push` joined at
// Story 244.1.34) is state-changing — no recorded read pipes them.
//
// THE POWERSHELL TOOL (Story 73.1.1): the Windows box's primary shell is the
// harness's PowerShell tool, a different tool name — register seat
// piped-checker-exit 6 is a checker piped into Select-String from it, unseen
// by this guard. The pipe detection is syntax-shared (`|`, `2>&1`, `;`,
// `&&`); the re-run advice speaks PowerShell for a PowerShell payload
// (`node … *> out.txt; $LASTEXITCODE` — `*>` redirects every stream and
// keeps the native exit). Its here-strings (`@'…'@` / `@"…"@`) are dropped
// as data and a double-quoted span's backtick is an escape, not
// substitution (`$(` is read, Story 151.1.1); same accepted limits as
// warn-cwd-drift.mjs (backtick line continuations split a stage — errs
// toward warning; `-Path:<value>` and `& "…"` unparsed; a here-string body
// line that looks like the terminator ends it early). The wiring widens to
// `Bash|PowerShell` in Story 73.1.2.
//
// THE NPM WRAPPER AND THE ZSH PIPESTATUS READ (Story 84.1.1 — register seat
// piped-checker-exit 7, the tigerdash Epic 3.1 close): `npm test 2>&1 | tail
// -45; echo "EXIT=${PIPESTATUS[0]}"` printed an EMPTY proof line unflagged,
// two faces at once. The battery recognizer matched command-position `node`
// with `--test` only, so `npm test` — whose script IS `node --test …` in
// tigerdash and every vituno consumer — was invisible; and bash's uppercase
// PIPESTATUS is unset on the Mac's zsh, whose array is the lowercase
// `$pipestatus` (KB zsh-pipestatus-lowercase). Two widenings: the `npm test`
// family in command position (`npm test`, `npm t`, `npm run test`, with
// `-w <pkg>` / `--workspace=<pkg>` / `--workspaces` tolerated between) is a
// battery when it is a NON-FINAL pipe stage — unconditionally, whatever the
// script, since a piped `npm test` is never an honest exit proof; and a Bash
// command reading `${PIPESTATUS` while the host is darwin draws a warn naming
// `$pipestatus` and the unpiped shape, independently of pipe structure. The
// host reaches the guard through WARN_PIPED_CHECKER_PLATFORM (else
// process.platform) so the darwin pin runs on both boxes — never a
// hard-coded platform skip.
//
// THE SEMICOLON ARM (Chore 10, 2026-09-18 — register class
// semicolon-chained-checker-does-not-gate-commit, the seat-2 `hook` decision
// routed by inbox fragment tigerdash-semicolon-chain-guard-arm): the pipe
// class's sibling. The pipe HIDES a gate's exit; the semicolon READS it and
// discards it, so whatever follows runs on a red — and the harness prints the
// red and the new state in the SAME result, where it reads as fine. Seat 1
// (hub #2411, 2026-09-14): `node scripts/close-lint.mjs; echo "exit=$?"` then
// `git diff --check && git add … && git commit … && git push` — the tick
// commit landed and pushed on a close-lint FAIL. Seat 2 (tigerdash #220, F159,
// 2026-09-17): `gh pr checks --watch` returned 1 and `gh pr merge` was
// refused, but the chain ran on `;` — it switched to main and deleted the
// story branch with `-D`. The arm warns when a GATE — anything the pipe arm
// recognizes (a fleet checker, a `node --test` battery, the `npm test`
// family, an rspec suite), plus `gh pr checks` and `gh pr merge` — sits in an
// EARLIER clause than a STATE-CHANGING VERB: `git commit` / `push` / `switch`
// / `checkout` / `branch -D`, or `gh pr merge`. Clauses end at `;`, at a
// newline that does not continue a chain (after `&&` / `||` / `|`, a Bash
// backslash or a PowerShell backtick it does), or at a lone `&`; `(){}` and
// backticks flatten to spaces, so a loop or group body reads as the same
// chain and `do` / `then` / `else` / `!` are read past. SILENT when the verb
// is `&&`-chained in the gate's own clause (the endorsed shape: `gate && git
// add … && git commit …; echo "exit=$?"` — the record line AFTER the chain);
// when no state-changing verb follows (`checker; echo "exit=$?"` alone is the
// other endorsed shape); when the gate is an `if` / `elif` / `while` / `until`
// condition (consumed, never discarded); and when the command visibly handles
// the exit between the two — an `exit` / `return` / `throw` word (`|| exit 1`,
// `if ($LASTEXITCODE -ne 0) { exit 1 }`), a clause opening on `if` / `[` /
// `[[` / `test` / `case`, or Bash `set -e` / `set -o errexit` before the gate
// (Bash only — the PowerShell tool has no such switch). A piped gate followed
// by `;` and a verb draws ONE warn carrying both faces. The new recognizers
// strip a Windows executable suffix (`git.exe` is git — KB
// windows-executable-suffix-name-checks); the pipe arm's recognizer is left
// as that entry records it.
//
// THE PATTERN RECOGNIZER (Story 104.1.1 — register seat piped-checker-exit
// 11, the hub Epic 102.1 close, 2026-09-19): the seat's command piped
// scripts/fleet/check-claude-md-paths.mjs into `tail -2` and echoed tail's
// `exit 0`; the guard stayed SILENT, because it knew a checker BY NAME — the
// three its first story named — while the hub carries twenty-two and the site
// repos more. Every miss since the hook decision was a predicate miss, and a
// by-name list misses the next checker the same way. A checker is now any
// BASENAME that starts `check-` and ends `.mjs`, plus the named close-lint.mjs
// (the one fleet checker not named that way) — two string tests, no regex,
// so nothing to backtrack; do not fold it into a pattern without the sprint
// standard's two-axis probe. One recognizer feeds BOTH arms, so the pipe arm
// and the semicolon arm widen together. A name that merely contains the word
// (a `recheck-…` prefix, a `checker-…` stem) fails the prefix test by
// construction. A check-*.test.mjs run directly and piped warns too — that is
// an exit proof; under `node --test` the same file stays a BATTERY (its story,
// its advice), and only the three NAMED checkers outrank `--test`, as before.
// The re-run advice prints the script AS THE COMMAND GAVE IT — the old advice
// guessed `scripts/<basename>`, a wrong path for anything under
// scripts/fleet/, scripts/audit/ or a site's own tree.
//
// THE VERDICT SCRIPTS (Story 115.1.1 — register seat piped-checker-exit 18,
// lawcuments-app, 2026-09-26, caught: false): `close-epic.mjs … 2>&1 | head
// -12; echo "RUNNER EXIT=$?"` printed `RUNNER EXIT=0` under the runner's own
// `[ERROR]` (its true exit was 2) and the guard stayed silent — the class's
// next seat the deferral above once waited for. VERDICT_SCRIPTS now joins the
// recognizer: close-epic.mjs on every run, and `sync-session-wrap.mjs
// --check`, `round.mjs --watch`, `sweep.mjs --verify` (the flag anywhere after
// the script) and `kb.mjs check-hooks` (the SUBCOMMAND — `kb.mjs search
// check-hooks` is no verdict) only with their verdict word. A Map lookup and
// string compares, no regex; the two-axis probe (size × match) was run before
// the fold and after it, and is quoted in the Story 115.1.1 PR. Consult sites
// (register list-to-pattern-widening-reaches-every-consult-site): readChecker's
// command-position test (a script run by its shebang) and its `node`-arguments
// loop — where a verdict script counts in the SCRIPT position only (node's
// first non-flag argument), never as data to another script, while the
// check-* test keeps its any-argument reach as it shipped; through
// readChecker both the pipe arm and the semicolon arm's gateOf; pipeStory
// routes a verdict script to 115.1.1. Still NOT read: a site checker in
// another extension (no seat names one).
//
// Wired from the hub's .claude/settings.json (PreToolUse, matcher
// "Bash|PowerShell" since Story 73.1.2, exec form — no shell on any
// platform). Guards every fleet checker — a basename that starts `check-` and
// ends `.mjs`, plus close-lint.mjs (Story 104.1.1; the first story named
// three: check-fleet.mjs, check-portfolio.mjs, close-lint.mjs) — matched
// by basename, so hub-rooted `scripts/…`, `scripts/fleet/…` and site-session
// `../../scripts/…` invocations all count, as does a site's own
// `scripts/check-….mjs` — plus `node --test` batteries (command-position
// `node`/`nodejs` with `--test` among its arguments; Story 24.3.26), the
// `npm test` family (command-position `npm` whose first non-flag words are
// `test`, `t`, or `run test`, workspace flags tolerated; Story 84.1.1), and
// rspec suite invocations (command-position `rspec`/`bin/rspec`, with
// `bundle`/`mise` treated as wrapper words so `bundle exec rspec` and
// `mise exec -- bundle exec rspec` both resolve; Story 30.1.2).
//
// MATCHER CONTRACT (shares the neutralize approach with
// block-commit-on-main.mjs): heredoc bodies are dropped and quoted spans
// neutralized (a double-quoted span containing `$(` or a backtick is data too,
// but the shell runs its substitutions: each body is read as a command of its
// own, and its last clause — the one whose exit the substitution returns —
// stays beside the span; Story 151.1.1); the line splits into pipelines on && || ; &
// newline, parens, braces, backticks (`|&` first normalizes to `|`); a
// pipeline with 2+ stages warns iff a NON-FINAL stage actually EXECUTES a
// checker — its command-position word (after VAR=x assignments and wrapper
// words) is `node`/`nodejs` with a checker basename among its arguments, or
// is itself a checker basename — or a `node --test` battery (same
// command-position word with `--test` among its arguments; a NAMED checker
// basename takes precedence when both match, a pattern-only match does not —
// Story 104.1.1) — or an `npm test` family stage
// (command-position `npm`, first non-flag words `test` / `t` / `run test`;
// Story 84.1.1). A checker in the LAST stage keeps the
// compound's exit, so it stays silent; a checker path appearing as a data
// argument (`git log -- scripts/check-fleet.mjs | head`) never fires.
// Independently of pipe structure, a Bash command reading `${PIPESTATUS`
// while the host is darwin warns (Story 84.1.1) — read with only the
// single-quoted spans dropped, since the read usually sits inside a
// double-quoted proof line (which expands, and which the neutralizer below
// would treat as data) while a single-quoted mention never expands.
//
// Known limits (accepted, warn-level): a checker piped inside a quoted script
// argument (`bash -c "… | tail"`) is data to the matcher; group-command
// pipelines (`{ …; } | tail`) split at the braces and slip through;
// variable indirection (`c=scripts/check-fleet.mjs && node $c | tail`) never
// presents the basename. The backstop is the review pass reading the cited
// runs — this guard exists to catch the habit, not to be a boundary.
// The semicolon arm's own limits (Chore 10, same posture): ANY conditional
// clause between the gate and the verb stands the arm down, whether or not
// it reads the gate's exit (`G; [ -f x ] && echo hi; git push` is missed —
// the arm errs toward silence on visible control flow, since a false warn
// trains the eye to skip it); a gate or verb inside `bash -c "…"` is data;
// a verb outside the list (`git merge`, `git reset`, `git tag`) is unread —
// the class's next seat widens it; `gh pr merge --auto` counts as a gate
// like any merge (the `&&` rewrite is valid for it too). The commit and merge
// blocks (Stories 115.1.1 and 126.1.1) inherit every one of these limits —
// each fires only where the warning would — and, past a warn-only verb, still
// read a later commit, push or branch change; the commit block also reads a
// verdict gate that sits behind an earlier gh gate. The push block (Story
// 140.1.2) needs no gate before it — the push is its own — and keeps the same
// stand-downs and the same blind spots (`bash -c "…"` is data).
//
// A QUOTED SUBSTITUTION'S SEMICOLONS ARE DATA (Story 151.1.1 — register class
// piped-checker-splits-a-quoted-substitution-span, seat 1: pantheon Sprint
// 16's executor, hub #3091). An all-`&&` `git push && gh pr create --body
// "the $(x) form; y" && gh pr merge …` was DENIED: the live span stayed in
// place, so the `;` in its text ended the outer clause and read as the push
// face. The span is now a placeholder like any quoted span; each `$(…)` /
// backtick body in it is read by every arm as a command of its own (a `;`
// inside separates only the inner commands, so `"$(git push; gh pr merge 5)"`
// is the push face, and a checker piped inside still warns); and the body's
// last clause stays beside the placeholder, since a substitution returns that
// clause's exit and an assignment carries it (`x="$(checker)"; git push` is
// still refused). Accepted limit: a double quote nested inside the
// substitution (`"$(echo "a; b")"`) ends the span early, as before.
//
// Malformed stdin: stand down loudly (stderr note + exit 0) — a warn-level
// guard with no readable payload has nothing to warn about.
//
// Site-repo adoption (same path as block-commit-on-main.mjs): hooks do NOT
// inherit from ancestor directories, so each consumer repo carries its own
// copy and its own PreToolUse entry. This guard RIDES THE FLEET SYNC ROSTER
// (scripts/sync-session-wrap.mjs), sha256-pinned in each consumer's
// scripts/sync-provenance.json and wired as a PreToolUse hook there — every
// sync round overwrites the synced copies, which is why hand-edits to a copy
// never survive (Story 26.1.3).
//
// Test manually:
//   echo '{"tool_name":"Bash","tool_input":{"command":"node scripts/check-fleet.mjs | tail -5"}}' \
//     | node scripts/warn-piped-checker.mjs
//   The PowerShell tool (the $LASTEXITCODE re-run shape; Story 73.1.1):
//   echo '{"tool_name":"PowerShell","tool_input":{"command":"node scripts/check-fleet.mjs 2>&1 | Select-String ok"}}' \
//     | node scripts/warn-piped-checker.mjs
//   The npm wrapper + the zsh PIPESTATUS read, the host forced (Story 84.1.1):
//   echo '{"tool_name":"Bash","tool_input":{"command":"npm test 2>&1 | tail -45; echo EXIT=${PIPESTATUS[0]}"}}' \
//     | WARN_PIPED_CHECKER_PLATFORM=darwin node scripts/warn-piped-checker.mjs
//   The semicolon arm, seat 2's chain (Chore 10; DENIED since Story 126.1.1):
//   echo '{"tool_name":"Bash","tool_input":{"command":"gh pr checks 5 --watch; gh pr merge 5 --squash; git switch main"}}' \
//     | node scripts/warn-piped-checker.mjs
//   The pattern recognizer, seat 11's command (Story 104.1.1):
//   echo '{"tool_name":"Bash","tool_input":{"command":"node scripts/fleet/check-claude-md-paths.mjs | tail -2"}}' \
//     | node scripts/warn-piped-checker.mjs
//   The verdict scripts and the block, seat 18's pipe and #606's chain (Story 115.1.1):
//   echo '{"tool_name":"Bash","tool_input":{"command":"node scripts/close/close-epic.mjs --epic 1.1 2>&1 | head -12"}}' \
//     | node scripts/warn-piped-checker.mjs
//   echo '{"tool_name":"Bash","tool_input":{"command":"node scripts/close-lint.mjs; git commit -m tick"}}' \
//     | node scripts/warn-piped-checker.mjs
//   The shape arm, seat 30's bare grep (Story 139.1.1):
//   echo '{"tool_name":"Bash","tool_input":{"command":"grep -n x CLAUDE.md | head -20; echo \"grep exit=$?\""}}' \
//     | node scripts/warn-piped-checker.mjs
// Suite: test/warn-piped-checker.test.mjs (node --test).


// Story 104.1.1: a checker is recognized by PATTERN (isCheckerName); the three
// the first story named survive only as a routing fact — they keep Story
// 24.3.15's reference and their precedence over `--test`.
const NAMED_CHECKERS = new Set(['check-fleet.mjs', 'check-portfolio.mjs', 'close-lint.mjs']);
const isCheckerName = (b) => (b.startsWith('check-') && b.endsWith('.mjs')) || b === 'close-lint.mjs';
// Story 115.1.1: the verdict scripts not named `check-*`. close-epic.mjs's
// every run is a close verdict (0 pass / 1 fail / 2 error); the others are
// verdicts only with their verdict word — a flag anywhere after the script,
// or kb.mjs's SUBCOMMAND (its first non-flag word). A Map lookup and string
// compares, no regex, so nothing to backtrack (the header's two-axis probe).
const VERDICT_SCRIPTS = new Map([
  ['close-epic.mjs', null],
  ['sync-session-wrap.mjs', { flag: '--check' }],
  ['round.mjs', { flag: '--watch' }],
  ['sweep.mjs', { flag: '--verify' }],
  ['kb.mjs', { sub: 'check-hooks' }],
]);
// The verdict word a run of basename `b` carries ('' for close-epic.mjs),
// given the RESOLVED words after the script; null when it is no verdict.
const verdictWord = (b, rest) => {
  if (!VERDICT_SCRIPTS.has(b)) return null;
  const want = VERDICT_SCRIPTS.get(b);
  if (!want) return '';
  if (want.flag) return rest.includes(want.flag) ? want.flag : null;
  return rest.find((t) => !t.startsWith('-')) === want.sub ? want.sub : null;
};
// Story 115.1.1, arm (b): the verbs a recognized verdict run ended by `;` may
// not reach — the command is DENIED before it runs. Every other state-changing
// verb keeps the Chore 10 warning.
const BLOCKED_VERBS = new Set(['git commit', 'git push']);
// Story 126.1.1 (the merge face): the LOCAL branch changes a `gh pr merge` /
// `gh pr checks` gate ended by `;` may not reach. `gh pr merge` itself is not
// here — a merge is gated server-side by the ruleset's required checks — so a
// lane watch followed by a merge alone keeps the warning.
const MERGE_FACE_VERBS = new Set(['git switch', 'git checkout', 'git branch -D', 'git commit', 'git push']);

if (process.stdin.isTTY) {
  console.error('warn-piped-checker.mjs expects a PreToolUse JSON payload on stdin.');
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
  console.error(`warn-piped-checker: unreadable PreToolUse payload${readWhy ? ` (${readWhy})` : ''} — guard stands down (warn-level, fail-open)`);
  process.exit(0);
}

// Story 73.1.1: the harness's shell tools — Bash on both machines, PowerShell
// on the Windows box (its primary shell; register seat piped-checker-exit 6).
// Story 244.1.8, the git-verb arm (pipedGitVerb, below): the state-changing verbs,
// and the global options that take a value before the verb. Story 244.1.34 adds
// `push` (piped-checker-exit's push face: `git push … | grep -v` read silent).
const GIT_STATE_VERBS = new Set(['merge', 'pull', 'checkout', 'rebase', 'push']);
const GIT_VALUE_OPTIONS = new Set(['-C', '-c', '--git-dir', '--work-tree']);

const SHELL_TOOLS = new Set(['Bash', 'PowerShell']);
if (!SHELL_TOOLS.has(payload.tool_name)) process.exit(0);
const isPowerShell = payload.tool_name === 'PowerShell';
const command = payload.tool_input?.command ?? '';

// `masked` is the basename (the summary's and every comparison's name);
// `maskedScript` the invocation as the command gave it, for the re-run advice
// (Story 104.1.1) — null for a battery, a suite and the npm family.
const maskedHit = maskedCheckerInvocation(command);
const masked = maskedHit?.name ?? null;
const maskedScript = maskedHit?.script ?? null;
// Story 84.1.1, the second face of register seat piped-checker-exit 7: a
// `${PIPESTATUS…}` read on a darwin host expands EMPTY — the fleet tool shell
// there is zsh, whose array is the lowercase `$pipestatus` (KB
// zsh-pipestatus-lowercase) — so an exit proof written that way prints
// nothing. Read on the command with only its SINGLE-quoted spans dropped: a
// double-quoted `"EXIT=${PIPESTATUS[0]}"` expands (the proof-line shape) and
// must fire, while a single-quoted mention never expands — data, like quoted
// prose to the matcher below (measured at Story 84.1.1's own delivery: the
// guard fired on its manual-test recipe, a JSON payload in single quotes).
// The read is a proof-line habit, not pipe structure, so it sits outside the
// pipeline split. The host reaches the guard through a seam the tests set —
// WARN_PIPED_CHECKER_PLATFORM, else process.platform — so the darwin pin runs
// on both boxes; never a hard-coded platform skip. Bash payloads only: the
// PowerShell tool has no PIPESTATUS.
const platform = process.env.WARN_PIPED_CHECKER_PLATFORM || process.platform;
const pipestatusOnDarwin =
  !isPowerShell && platform === 'darwin' && /\$\{?PIPESTATUS/.test(command.replace(/'[^']*'/g, "''"));
// Chore 10, the semicolon arm (register class
// semicolon-chained-checker-does-not-gate-commit): a gate whose clause ends at
// `;` has its exit READ and discarded, so a state-changing verb in a later
// clause runs on a red — the pipe class's sibling (the pipe HIDES the exit).
const semi = ungatedVerbAfterGate(command);
// Story 139.1.1, the shape arm: a `$?` / `$LASTEXITCODE` read right after ANY
// pipeline. Read only when no recognized gate is piped — the pipe arm's advice
// already covers that read.
// Story 244.1.8, the git-verb arm: a state-changing git verb in a non-final pipe
// stage, read whether or not a `$?` follows; its advice covers that read.
const gitPipe = pipedGitVerb(command);
const dollarQ = masked || gitPipe ? null : dollarQAfterPipe(command);
// Story 167.1.1, the background arm (register class piped-checker-exit): a
// `run_in_background` call's exit is read by the HARNESS, whose task
// notification reports the compound's exit, the LAST pipe stage's. So when
// the last pipeline pipes a gate into tail, head or grep, "exited with code 0"
// is the filter's. No `$?` appears for the shape arm, and a lane watch is in
// no checker list, so neither arm saw kitchenbridget.com #347's
// `gh pr checks … --watch | tail -12`. The flag is a documented Bash
// tool_input field (Claude Code's hooks reference).
const bgPipe = payload.tool_input?.run_in_background === true ? backgroundMaskedGate(command) : null;
if (!masked && !gitPipe && !pipestatusOnDarwin && !semi && !dollarQ && !bgPipe) process.exit(0);

// `npm test` (Story 84.1.1) is a battery like `node --test` (Story 24.3.26):
// its script IS `node --test …` in tigerdash and every vituno consumer, and a
// piped `npm test` is never an honest exit proof whatever the script runs.
const isNpm = masked === 'npm test';
const isBattery = masked === 'node --test' || isNpm;
const isSuite = masked === 'rspec';
const kind = isBattery ? 'battery' : isSuite ? 'suite' : 'checker';
// A pattern match that is none of the three named routes to Story 104.1.1.
const pipeStory = isNpm
  ? '84.1.1'
  : isBattery
    ? '24.3.26'
    : isSuite
      ? '30.1.2'
      : NAMED_CHECKERS.has(masked)
        ? '24.3.15'
        : VERDICT_SCRIPTS.has(masked)
          ? '115.1.1'
          : '104.1.1';
// The re-run advice speaks the payload's shell (Story 73.1.1): PowerShell
// keeps a native exit in $LASTEXITCODE and redirects every stream with `*>`;
// the pipe detection above is syntax-shared and needs no shell branch.
const exitVar = isPowerShell ? '$LASTEXITCODE' : '$?';
const unpiped = (invocation) =>
  isPowerShell ? `\`${invocation} *> out.txt; $LASTEXITCODE\`` : `\`${invocation} > /tmp/out 2>&1; echo $?\``;
const rerun = isNpm
  ? unpiped('npm test')
  : isBattery
    ? unpiped('node --test <files>')
    : isSuite
      ? unpiped('bundle exec rspec <args>') + ' — or gate on the printed tally'
      : // The script as the command gave it, never a guessed directory (Story
        // 104.1.1); the bare basename is the fallback.
        unpiped(maskedScript || masked);
const pipeAdvice = masked
  ? `piped-checker guard (warn, not a block): this command pipes \`${masked}\`` +
    (isBattery ? ' (a test battery)' : isSuite ? ' (a test suite)' : '') +
    ` into another command, so the compound's exit code is the LAST pipe stage's — \`${exitVar}\` no ` +
    `longer proves the ${kind}'s verdict. If this run is the ` +
    `exit-code proof, re-run UNPIPED (e.g. ${rerun}). `
  : '';
// Story 244.1.34: a piped push hides a rejection the same way; its proof is the exit, or the branch read on origin.
const gitIsPush = gitPipe?.verb === 'push';
const gitAdvice = gitPipe
  ? `piped-checker guard (warn, not a block): this command pipes \`git ${gitPipe.verb}\` — a state-changing git ` +
    'command — into another command, so a refusal it prints (' +
    (gitIsPush
      ? '`! [rejected]`, `non-fast-forward`, a remote hook\'s refusal'
      : '`Not possible to fast-forward`, a conflict, `would be overwritten`') +
    ") is hidden behind the filter, and the compound's exit is the LAST stage's " +
    `whether or not \`${exitVar}\` is read after it. Before you believe ${gitIsPush ? 'the push landed' : 'the checkout moved'}, run it UNPIPED and read ` +
    `its exit (e.g. ${unpiped(gitPipe.invocation)})${gitIsPush ? ', or read the branch on origin (`git ls-remote --heads origin BRANCH`)' : ''}. `
  : '';
const pipestatusAdvice = pipestatusOnDarwin
  ? 'piped-checker guard (warn, not a block): this command reads `${PIPESTATUS…}` on a darwin host, where the ' +
    "fleet tool shell is zsh and bash's uppercase PIPESTATUS is unset — the read expands EMPTY, so an exit " +
    "proof written that way prints nothing (KB zsh-pipestatus-lowercase). zsh's array is the lowercase " +
    '`$pipestatus`; better, run the proof UNPIPED and read `$?` (e.g. ' + unpiped('<command>') + '). '
  : '';
// Story 139.1.1: the shape arm's advice names the shape, not a tool — the
// pipeline it read, and the unpiped re-run of its first stage. Story 128.1.5:
// it also says when it does not apply — the last stage IS the command meant
// (a payload piped into a hook), the stand-down Initiative 139's decision
// promised (register class dollar-q-shape-arm-warns-on-the-intended-tail-exit).
const dollarQAdvice = dollarQ
  ? `piped-checker guard (warn, not a block): this command reads \`${dollarQ.read}\` right after the pipeline ` +
    `\`${dollarQ.pipeline}\` — \`${dollarQ.read}\` after a pipe is the last stage's exit, whatever was piped, so ` +
    `it proves nothing about \`${dollarQ.first}\`. If the LAST stage is the command whose exit you meant (the ` +
    "earlier stages only feed it, as in `printf … | node hook; echo $?`), this warning does not apply — ignore it. " +
    `If this run is the exit-code proof, re-run UNPIPED (e.g. ` +
    `${unpiped(dollarQ.first)})` +
    (isPowerShell
      ? '. '
      : ", or read `${PIPESTATUS[0]}` (bash) / `$pipestatus[1]` (zsh), or `set -o pipefail` ahead of the pipeline. ") +
    "Register class piped-checker-exit, its seats in the hub's docs/friction/recurrences.json: its misses since " +
    'the hook decision came by shape — a bare grep, a lane watch — where no name list reaches (Story 139.1.1). '
  : '';
// Story 167.1.1: the background arm's advice names the gate, the filter and
// the reader of the exit (the task notification), then the two honest shapes.
const bgAdvice = bgPipe
  ? `piped-checker guard (warn, not a block): this command runs in the background (run_in_background), and its ` +
    `last pipeline pipes \`${bgPipe.gate}\` into \`${bgPipe.filter}\`. The task notification reports the compound's ` +
    `exit — the LAST stage's — so its "exited with code 0" is \`${bgPipe.filter}\`'s, never \`${bgPipe.gate}\`'s ` +
    `verdict. Write the gate's output to a file and record its own exit (e.g. ${unpiped(bgPipe.first)}), or put ` +
    '`set -o pipefail` ahead of the pipeline. Register class piped-checker-exit: kitchenbridget.com #347 read a ' +
    "background lane watch's piped code 0 as the watch's (Story 167.1.1). "
  : '';
// The semicolon arm's advice (Chore 10): the rewrite is the same in both
// shells (pwsh 7 chains with `&&` too); only the record line differs.
const recordLine = isPowerShell ? '`<gate> && <next> && …; $LASTEXITCODE`' : '`<gate> && <next> && …; echo "exit=$?"`';
// Story 115.1.1: a recognized verdict run ended by `;` ahead of `git commit` /
// `git push` has no legitimate reading, so that shape is DENIED — the command
// never runs, and re-issuing it with `&&` is the whole remedy.
const semiAdvice = semi
  ? (semi.block
      ? 'piped-checker guard (BLOCKED — this command did not run): '
      : 'piped-checker guard (warn, not a block): ') +
    `\`${semi.gate}\` ends its clause at a \`;\` (or a newline), so its ` +
    `exit code is read and discarded — it does not gate the \`${semi.verb}\` after it, which runs even on a red, ` +
    'and the harness prints the red and the new state in the same result. Chain every step after a checker, a ' +
    'lane watch or a merge with `&&`; a line that records the exit goes AFTER the chain, never between the gate ' +
    `and the verb (${recordLine}). ` +
    // Story 140.1.2: why a push ahead of a merge is refused, not warned.
    (semi.face === 'push'
      ? 'A push that fails leaves the PR on its previous head, and the merge — auto-merge above all — lands THAT ' +
        'head: the ruleset judges the head it has and cannot know a push meant to replace it failed. Chain it: ' +
        '`git push && gh pr merge <n> --auto --squash` — and to pin the head the merge may land, add ' +
        '`--match-head-commit <full 40-char sha>` (measured at Story 140.1.2: a sha that is not the head is ' +
        'refused at arming, "expected head oid does not match the current head oid"; a short sha cannot be ' +
        'coerced to a GitObjectID). ' +
        // Story 128.1.5, widened by the second 2026-10-02 inbox triage (register class
        // post-push-merge-state-lags, seat 5 — tigerdash #171): the pushed head's own sha
        // was refused for seconds, until the PR's head caught up to the push.
        'For the pin, in a separate call, after the push\'s own call returns, read `gh pr view <n> --json headRefOid` ' +
        'until it prints the pushed sha, then arm with that sha: for a few seconds the PR\'s head can lag the push, ' +
        'and an arm inside that window is refused with the same error — the race, not a wrong sha, so re-read and ' +
        're-arm (register class post-push-merge-state-lags; KB github-clean-merge-refusal shape 3). ' +
        // Story 171.1.1: the Mac hub wrap of 2026-10-07 wrote this read into the push's
        // own command as a loop, and the loop's `;` drew this deny on its own advice.
        'Never write that read into the push\'s command: an `until …; do …; done` loop there puts a `;` between ' +
        'the push and the merge, which this guard denies. '
      : '') +
    (semi.block ? 'Re-issue the command in that shape. ' : '')
  : '';
const pipeCitations =
  'Class recurred 3x: deccentral.com S1 legs 1+5, hub S10 F3, hub S11 F1 (Story 24.3.15); ' +
  '4th, first outside the checker set: hub S15 E1, a piped test battery (Story 24.3.26); ' +
  "5th, the rspec lane: hub 29.1.1's WSL suite wrapper (Story 30.1.2); " +
  '6th, the PowerShell tool (Story 73.1.1); ' +
  '7th, the npm wrapper spelling + the zsh PIPESTATUS read: the tigerdash Epic 3.1 close (Story 84.1.1); ' +
  '8th, a checker the by-name list did not name: register seat 11, check-claude-md-paths.mjs piped into tail ' +
  'at the hub Epic 102.1 close (Story 104.1.1); ' +
  '9th, a verdict script not named check-*: register seat 18, close-epic.mjs piped into head at a ' +
  'lawcuments-app close, `RUNNER EXIT=0` over a true exit 2 (Story 115.1.1).';
// Story 126.1.1: the register holds the seats, so the citation names it and
// carries no count — "2 seats" read stale against 14 by 2026-09-26 (seat 15).
const semiCitations =
  "Class semicolon-chained-checker-does-not-gate-commit, its seats in the hub's docs/friction/recurrences.json: hub #2411 " +
  '(2026-09-14 — a tick commit landed and pushed on a close-lint FAIL); tigerdash #220, F159 (2026-09-17 — a ' +
  'switch and a `branch -D` after a refused merge) (Chore 10); the warning alone did not stop lawcuments-app ' +
  '#606 (a red tick commit landed and pushed under it), so the commit/push shape is refused (Story 115.1.1); ' +
  'the merge face kept recurring at warn — lawcuments-app #615 switched after a FAILED merge — so a merge gate ' +
  'ahead of a local branch change is refused too (Story 126.1.1); pantheon #423 armed auto-merge after a push ' +
  'ended by `;` — had the push failed, the PR\'s previous head would have landed — so a push ahead of a merge is ' +
  'refused (Story 140.1.2).';
// Story 244.1.8: the git-verb arm's own faces.
const gitCitations =
  "Class piped-checker-exit, its seats in the hub's docs/friction/recurrences.json: the pen's two of 2026-10-26 " +
  '(hub #4358) — `git merge --ff-only origin/main 2>&1 | tail -n 1` hid a refusal twice, and no `$?` was read ' +
  'for the shape arm to see (Story 244.1.8)' +
  (gitIsPush
    ? '; and its push face, every seat uncaught by any arm until Story 244.1.34 — `git push … | grep -v` and `| tail` ' +
      "(activewaveinc.com's Epic 2.1 close and its Sprint 2 leg 3, 2026-10-27; the expeal.com Sprint 30 review, 2026-10-28)."
    : '.');
const citations = [masked || pipestatusOnDarwin ? pipeCitations : null, gitPipe ? gitCitations : null, semi ? semiCitations : null]
  .filter(Boolean)
  .join(' ');
const advice = pipeAdvice + gitAdvice + pipestatusAdvice + dollarQAdvice + bgAdvice + semiAdvice + citations;
const summary = [
  masked ? `${masked} piped — exit code masked` : null,
  gitPipe ? `git ${gitPipe.verb} piped — a refusal hidden` : null,
  pipestatusOnDarwin ? '${PIPESTATUS read on zsh — expands empty' : null,
  dollarQ ? `${dollarQ.read} read after a pipe — the last stage's exit` : null,
  bgPipe ? `${bgPipe.gate} piped in a background call — the notification's exit is ${bgPipe.filter}'s` : null,
  semi ? `${semi.gate} then \`;\` — its exit does not gate ${semi.verb}` : null,
]
  .filter(Boolean)
  .join('; ');
const stories = [
  ...new Set(
    [masked ? pipeStory : null, gitPipe ? '244.1.8' : null, gitIsPush ? '244.1.34' : null, pipestatusOnDarwin ? '84.1.1' : null, dollarQ ? '139.1.1' : null, bgPipe ? '167.1.1' : null].filter(
      Boolean
    )
  ),
].join(' + ');
const blockStory =
  semi?.face === 'merge' ? 'Story 126.1.1' : semi?.face === 'push' ? 'Story 140.1.2' : semi?.block ? 'Story 115.1.1' : null;
const refs = [stories ? `Story ${stories}` : null, semi ? 'Chore 10' : null, blockStory]
  .filter(Boolean)
  .join(' + ');

// A pipe and every other semicolon shape stay warn-level: no permissionDecision,
// the command proceeds. The commit/push shape (Story 115.1.1) and the merge
// face (Story 126.1.1) are denied, with the same advice as the reason;
// additionalContext and systemMessage ride
// beside it unchanged, so one output carries every face.
console.log(
  JSON.stringify({
    systemMessage: semi?.block
      ? `piped-checker guard: BLOCKED — ${summary} (${refs})`
      : `piped-checker guard: ${summary} (warn only; ${refs})`,
    hookSpecificOutput: {
      hookEventName: 'PreToolUse',
      ...(semi?.block ? { permissionDecision: 'deny', permissionDecisionReason: advice } : {}),
      additionalContext: advice,
    },
  })
);

// --- matcher ----------------------------------------------------------------

// Neutralize text that is data, not command structure — same approach as
// block-commit-on-main.mjs: heredoc bodies dropped (early-terminator errs
// toward warning), quoted spans become indexed placeholders whose contents are
// kept (so a quoted checker PATH argument still resolves). A double-quoted
// span holding live substitution (`$(` or backtick) is a placeholder too
// (Story 151.1.1 — before it the span stayed in place, so a `;` in its text
// ended the outer clause), followed by each substitution's LAST clause as
// `$(…)`: a substitution's exit is that clause's, and an assignment from it
// carries it (`x="$(checker)"; git push` stays a gate). Every body is also
// returned whole in `nested`, which each arm reads as a command of its own.
function neutralize(command) {
  let text = command;
  // PowerShell here-strings (Story 73.1.1): `@'` … `'@` and `@"` … `"@`, the
  // terminator at line start, are argument DATA — dropped whole before the
  // quote pass, which would otherwise split a body at its first apostrophe.
  // A body line that merely LOOKS like the terminator ends the here-string
  // early (errs toward warning, never silence).
  if (isPowerShell) text = text.replace(/@(['"])\r?\n[\s\S]*?\r?\n\1@/g, '');
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
  const values = [];
  // Each placeholder's quote character (Story 139.1.1): a double-quoted span
  // expands `$?` and a single-quoted one never does.
  const kinds = [];
  const nested = [];
  // Live substitution inside double quotes: `$(` in both shells; the backtick
  // only in Bash — in PowerShell it is the escape character (Story 73.1.1).
  const live = isPowerShell ? /\$\(/ : /\$\(|`/;
  const hold = (q) => {
    values.push(q.slice(1, -1));
    kinds.push(q[0]);
    return `\x00${values.length - 1}\x00`;
  };
  text = text.replace(/'[^']*'|"(?:[^"\\]|\\.)*"/g, (q) => {
    const inner = q.slice(1, -1);
    if (!(q.startsWith('"') && live.test(inner))) return hold(q);
    const bodies = substitutionBodies(inner);
    nested.push(...bodies);
    // The spliced clause's own single-quoted spans are data too.
    return hold(q) + bodies.map((b) => ` $(${lastClause(b).replace(/'[^']*'/g, hold)})`).join('');
  });
  return { text, values, kinds, nested };
}

// Story 151.1.1 — the substitution bodies in a double-quoted span's text:
// `$(…)` to its matching paren (nested parens counted, single-quoted spans
// skipped), and in Bash a backtick pair. An unclosed body runs to the end of
// the span (errs toward reading it). The escape character is the shell's.
function substitutionBodies(inner) {
  const escape = isPowerShell ? '`' : '\\';
  const bodies = [];
  for (let i = 0; i < inner.length; i++) {
    if (inner[i] === escape) {
      i++;
    } else if (inner[i] === '$' && inner[i + 1] === '(') {
      let depth = 1;
      let j = i + 2;
      for (; j < inner.length && depth; j++) {
        if (inner[j] === escape) j++;
        else if (inner[j] === "'") j = inner.includes("'", j + 1) ? inner.indexOf("'", j + 1) : inner.length;
        else if (inner[j] === '(') depth++;
        else if (inner[j] === ')') depth--;
      }
      bodies.push(inner.slice(i + 2, depth ? inner.length : j - 1));
      i = j - 1;
    } else if (inner[i] === '`') {
      let j = i + 1;
      while (j < inner.length && inner[j] !== '`') j += inner[j] === '\\' ? 2 : 1;
      bodies.push(inner.slice(i + 1, j));
      i = j;
    }
  }
  return bodies;
}

// Story 151.1.1 — a body's last clause: its text after the last `;`, newline
// or lone `&` outside nested parens and quoted or backtick spans. A
// substitution's exit status is that clause's.
function lastClause(body) {
  let depth = 0;
  let start = 0;
  for (let i = 0; i < body.length; i++) {
    const c = body[i];
    if (c === '\\') i++;
    else if (c === "'" || c === '"' || c === '`') i = body.includes(c, i + 1) ? body.indexOf(c, i + 1) : body.length;
    else if (c === '(') depth++;
    else if (c === ')') depth = Math.max(0, depth - 1);
    else if (!depth && (c === ';' || c === '\n' || (c === '&' && !/[&<>|]/.test(body[i - 1] ?? '') && !/[&<>]/.test(body[i + 1] ?? '')))) start = i + 1;
  }
  return body.slice(start);
}

// The stage reader both arms share (hoisted out of the pipe matcher by Chore
// 10, behaviour unchanged): the neutralized text plus the command-position
// finder and the checker recognizer. `skipWords` lets the semicolon arm read
// past `do` / `then` / `else` / `!`; the pipe arm passes none, as before.
function stageReader(command) {
  const WRAPPERS = new Set([
    'command', 'exec', 'env', 'nohup', 'time', 'nice', 'ionice', 'xargs', 'sudo', 'builtin',
    // Suite-runner wrappers (Story 30.1.2): `bundle exec rspec` and
    // `mise exec -- bundle exec rspec` resolve to their rspec command word
    // (`--` is skipped by the leading-dash rule below).
    'bundle', 'mise',
  ]);
  const { text, values, nested } = neutralize(command);
  const resolve = (t) => t.replace(/\x00(\d+)\x00/g, (_, n) => values[Number(n)] ?? '');
  const basename = (t) => resolve(t).split(/[\\/]/).pop();
  const tokensOf = (stage) => stage.trim().split(/\s+/).filter(Boolean);
  const commandIndex = (tokens, skipWords) => {
    let i = 0;
    while (i < tokens.length) {
      // Story 140.1.1: `timeout [-k <d>] [-s <sig>] [flags] <duration> <command>`
      // — the wrapper, its flags (two that take a value) and its duration are
      // read past, so `timeout 600 gh pr checks 5 --watch;` is the lane watch.
      if (tokens[i] === 'timeout') {
        i++;
        while (i < tokens.length && tokens[i].startsWith('-')) i += /^-[ks]$/.test(tokens[i]) ? 2 : 1;
        if (i < tokens.length && /^\d+(?:\.\d+)?[smhd]?$/.test(tokens[i])) i++;
        continue;
      }
      if (
        /^[A-Za-z_][A-Za-z0-9_]*=/.test(tokens[i]) || WRAPPERS.has(tokens[i]) ||
        (skipWords?.has(tokens[i]) ?? false) || tokens[i].startsWith('-') || tokens[i] === '$'
      ) {
        i++;
        continue;
      }
      break;
    }
    return i;
  };

  // A stage executes a checker when its command-position word is node with a
  // checker basename argument, or is itself a checker basename. readChecker
  // returns { name, script }: `name` the basename (or the battery / suite /
  // npm label), `script` the invocation as the command gave it — the re-run
  // advice prints that, never a guessed directory (Story 104.1.1).
  const readChecker = (stage, skipWords) => {
    const tokens = tokensOf(stage);
    const i = commandIndex(tokens, skipWords);
    if (i >= tokens.length) return null;
    const word = basename(tokens[i]);
    if (isCheckerName(word)) return { name: word, script: resolve(tokens[i]) };
    // Story 115.1.1: a verdict script run directly (its shebang), with its word.
    const direct = verdictWord(word, tokens.slice(i + 1).map(resolve));
    if (direct !== null) return { name: word, script: resolve(tokens[i]) + (direct ? ` ${direct}` : '') };
    if (word === 'rspec') return { name: 'rspec', script: null };
    if (word === 'node' || word === 'nodejs') {
      const args = tokens.slice(i + 1);
      // Under `--test` the stage is a BATTERY: only a NAMED checker outranks
      // it (as before Story 104.1.1) — a pattern-only match there is a test
      // file (`node --test test/check-fleet.test.mjs`), and relabelling it a
      // checker would print advice that drops `--test`.
      const battery = args.includes('--test');
      let scriptPosition = true;
      for (let k = 0; k < args.length; k++) {
        const t = args[k];
        if (t.startsWith('-')) continue;
        const b = basename(t);
        if (NAMED_CHECKERS.has(b) || (!battery && isCheckerName(b))) return { name: b, script: `node ${resolve(t)}` };
        // Story 115.1.1: a verdict script counts in the SCRIPT position only —
        // node's first non-flag argument — never as data to another script
        // (register list-to-pattern-widening-reaches-every-consult-site, face
        // 2: this loop scans every argument, and the check-* test above keeps
        // that reach as it shipped).
        const v = !battery && scriptPosition ? verdictWord(b, args.slice(k + 1).map(resolve)) : null;
        if (v !== null) return { name: b, script: `node ${resolve(t)}` + (v ? ` ${v}` : '') };
        scriptPosition = false;
      }
      if (battery) return { name: 'node --test', script: null };
    }
    // Story 84.1.1: the `npm test` family in command position — `npm test`,
    // `npm t`, `npm run test` — with workspace flags tolerated between
    // (`-w <pkg>` / `--workspace <pkg>` take a value; `--workspace=<pkg>`,
    // `--workspaces`, and any other flag are skipped). Unconditional: a
    // piped `npm test` is never an honest exit proof, whatever the script.
    // Any other npm verb (`install`, `run build`) is not a battery.
    if (word === 'npm') {
      const args = tokens.slice(i + 1);
      const words = [];
      for (let k = 0; k < args.length; k++) {
        const t = resolve(args[k]);
        if (t === '-w' || t === '--workspace') {
          k++;
          continue;
        }
        if (t.startsWith('-')) continue;
        words.push(t);
      }
      const [verb, script] = words;
      if (verb === 'test' || verb === 't' || (verb === 'run' && script === 'test')) return { name: 'npm test', script: null };
    }
    return null;
  };
  // The name alone — what the semicolon arm's gateOf and both summaries read.
  const executesChecker = (stage, skipWords) => readChecker(stage, skipWords)?.name ?? null;
  return { text, nested, resolve, basename, tokensOf, commandIndex, readChecker, executesChecker };
}

// Returns { name, script } for the first checker whose exit a pipe masks —
// its basename and its invocation as given — else null.
function maskedCheckerInvocation(command) {
  const { text, nested, readChecker } = stageReader(command);

  // `|&` is `2>&1 |` shorthand — normalize so the pipe split sees it; fd
  // redirects (`2>&1`, `>&2`, `&>file`) are neutralized so their `&` doesn't
  // read as a background-job boundary and cut the pipeline. Then split into
  // pipelines on chain/grouping boundaries (never on a single `|`) and check
  // every NON-FINAL stage of each multi-stage pipeline.
  const pipelines = text
    .replace(/\|&/g, '|')
    .replace(/\d*>&\d*/g, ' ')
    .replace(/&>>?/g, '>')
    .split(/(?:&&|\|\||[;&\n(){}`])+/);
  for (const pipeline of pipelines) {
    const stages = pipeline.split('|');
    if (stages.length < 2) continue;
    for (const stage of stages.slice(0, -1)) {
      const hit = readChecker(stage);
      if (hit) return hit;
    }
  }
  // Story 151.1.1: each quoted substitution body is a command of its own.
  for (const body of nested) {
    const hit = maskedCheckerInvocation(body);
    if (hit) return hit;
  }
  return null;
}

// Story 244.1.8 — the git-verb arm. Returns { verb, invocation } for the first
// non-final pipe stage that runs `git` with a state-changing verb, else null.
// The pipeline split and the substitution bodies are maskedCheckerInvocation's.
// Its two sets sit with the other top-level sets (GIT_STATE_VERBS, above).
function pipedGitVerb(command) {
  const reader = stageReader(command);
  const pipelines = reader.text
    .replace(/\|&/g, '|')
    .replace(/\d*>&\d*/g, ' ')
    .replace(/&>>?/g, '>')
    .split(/(?:&&|\|\||[;&\n(){}`])+/);
  for (const pipeline of pipelines) {
    const stages = pipeline.split('|');
    if (stages.length < 2) continue;
    for (const stage of stages.slice(0, -1)) {
      const hit = gitStateVerb(stage, reader);
      if (hit) return hit;
    }
  }
  for (const body of reader.nested) {
    const hit = pipedGitVerb(body);
    if (hit) return hit;
  }
  return null;
}
function gitStateVerb(stage, { resolve, basename, tokensOf, commandIndex }) {
  const tokens = tokensOf(stage);
  const i = commandIndex(tokens);
  if (i >= tokens.length || !/^git(?:\.exe)?$/i.test(basename(tokens[i]))) return null;
  let k = i + 1;
  while (k < tokens.length) {
    const t = resolve(tokens[k]);
    if (GIT_VALUE_OPTIONS.has(t)) k += 2;
    else if (/^--(?:git-dir|work-tree)=/.test(t) || t === '--no-pager') k += 1;
    else break;
  }
  const verb = k < tokens.length ? resolve(tokens[k]) : null;
  if (!GIT_STATE_VERBS.has(verb)) return null;
  // Judged a read (Hub Sprint 68 leg 5): it prints the stopped patch and moves nothing.
  if (verb === 'rebase' && tokens.slice(k + 1).map(resolve).includes('--show-current-patch')) return null;
  return { verb, invocation: tokens.slice(i).map(resolve).join(' ') };
}

// Story 139.1.1 — the shape arm. Returns { read, pipeline, first } when the
// first command after a multi-stage pipeline reads `$?` (Bash or zsh) or
// `$LASTEXITCODE` (the PowerShell tool), whatever the pipeline ran: that read
// is the LAST stage's exit. Null under an earlier `set -o pipefail` /
// `setopt pipefail`, when that command reads `${PIPESTATUS…}` / `$pipestatus`
// instead, and when any other command runs between (it resets the exit). The
// shared neutralizer decides what is a pipe, so a `|` inside a quoted pattern
// is not one; a single-quoted `$?` never expands, a double-quoted one does.
// Accepted limit: an unquoted `${?}` splits at its braces and is not read.
function dollarQAfterPipe(command) {
  const { text, values, kinds, nested } = neutralize(command);
  const shown = (s) => s.replace(/\x00(\d+)\x00/g, (_, n) => `${kinds[Number(n)]}${values[Number(n)]}${kinds[Number(n)]}`).trim();
  const expands = (s) => s.replace(/\x00(\d+)\x00/g, (_, n) => (kinds[Number(n)] === '"' ? values[Number(n)] : ''));
  const READ = isPowerShell ? /\$LASTEXITCODE\b/i : /\$\?/;
  const HONEST = /\$\{?(?:PIPESTATUS|pipestatus)\b/;
  const parts = text
    .replace(/\|&/g, '|')
    .replace(/\d*>&\d*/g, ' ')
    .replace(/&>>?/g, '>')
    .replace(isPowerShell ? /`\r?\n/g : /\\\r?\n/g, ' ')
    .replace(/(&&|\|\||\|)\s*\n\s*/g, '$1 ')
    .split(/(&&|\|\||[;&\n|(){}`])/);
  let pipefail = false;
  let pipeline = []; // the current pipeline's stages
  let after = null; // a pipeline of 2+ stages that just ended, awaiting the next command
  // Story 244.1.34 — the producer rule (register class
  // piped-checker-guard-misattributes-a-substitution-pipeline): each `(` opens a frame by the
  // character before it — `$(` a command substitution, `<(` / `>(` a process substitution,
  // anything else a `( … )` group — saving the outer pipeline. At its `)` the frame decides
  // what the substitution's last pipeline is to the outer command: a group's exit IS it; a
  // bare assignment's substitution (`x=$(… | …)`, every word before it an assignment, no
  // command word) carries it into the assignment's exit; a substitution in a command's
  // arguments (`echo $(… | …)`) and a process substitution never produce the outer
  // command's exit. The outer pipeline resumes either way, so `echo $(…) | tail; echo $?`
  // names `echo … | tail`. A group keeps its old reading exactly. Accepted limit: a
  // backtick substitution is still split as before (the story names `$( )`, `<( )`, `>( )`).
  // Story 247.1.5 — the WHOLE simple command (the Hub Sprint 70 review's finding 2): a
  // reserved word that opens a body (`do`, `then`, `else`, `{`, `!`) is not a command word,
  // so a loop or branch body's `x=$(… | …)` is a bare assignment; and a command word joined
  // into the same simple command after the `)` (`x=$(… | …) node run.mjs`) makes `$?` that
  // command's, so the substitution's pipeline is never the producer. Redirections (`2>…`,
  // `> FILE`) and further assignments are not command words.
  // Story 248.1.19 — only a command word (the Hub Sprint 72 review's finding 2): a word glued to
  // the `)` with no whitespace before it (`x=$(…).log`) is the assignment's own suffix; a word
  // opening `#` begins a comment; the text is read NEUTRALIZED, so a quoted value (`y="a b"`)
  // stays one word; and an append (`y+=1`) or a subscript (`arr[0]=1`) is an assignment.
  const frames = [];
  const ASSIGNMENT = /^[A-Za-z_][A-Za-z0-9_]*(?:\[[^\]]*\])?\+?=/;
  const BODY_OPENERS = new Set(['do', 'then', 'else', '{', '!']);
  const hasCommandWord = (s) => {
    const ws = s.split(/\s+/).filter(Boolean);
    for (let i = /^\s/.test(s) ? 0 : 1; i < ws.length; i++) {
      if (ws[i].startsWith('#')) return false; // a comment to the line's end
      if (/^\d*[<>]+$/.test(ws[i])) { i++; continue; } // a redirection and its target
      if (/^\d*[<>]/.test(ws[i]) || ASSIGNMENT.test(ws[i])) continue;
      return true;
    }
    return false;
  };
  let afterFromSub = false; // `after` is a bare assignment's substitution pipeline
  // After a substitution's `)`, the text up to the next separator is the SAME outer command
  // (`diff <(…) <(…)`'s second `<`), joined to its stage — never a stage of its own.
  let joinNext = false;
  for (let k = 0; k < parts.length; k++) {
    if (k % 2 === 1) {
      // A separator: `|` continues the pipeline; anything else ends it.
      if (parts[k] === '|') {
        joinNext = false;
        continue;
      }
      if (parts[k] === '(') {
        const before = (parts[k - 1] ?? '').replace(/\s+$/, '');
        const kind = before.endsWith('$') ? 'command' : /[<>]$/.test(before) ? 'process' : 'group';
        const words = kind === 'command' ? before.slice(0, -1).trim().split(/\s+/).filter(Boolean) : [];
        while (words.length && BODY_OPENERS.has(words[0])) words.shift();
        const bareAssignment = words.length > 0 && words.every((w) => ASSIGNMENT.test(w));
        frames.push({ kind, bareAssignment, outer: pipeline });
        if (kind === 'group') {
          if (pipeline.length >= 2) after = pipeline;
        } else {
          after = null;
        }
        pipeline = [];
        continue;
      }
      if (parts[k] === ')' && frames.length) {
        const f = frames.pop();
        const inner = pipeline.length >= 2 ? pipeline : null;
        if (f.kind === 'group') {
          if (inner) after = inner;
          pipeline = [];
          joinNext = false;
        } else {
          // A substitution: the outer command resumes; only a bare assignment's carries the exit.
          pipeline = f.outer;
          after = f.bareAssignment && inner ? inner : null;
          afterFromSub = Boolean(after);
          joinNext = true;
        }
        continue;
      }
      joinNext = false;
      if (pipeline.length >= 2) after = pipeline;
      pipeline = [];
      continue;
    }
    const stage = parts[k].trim();
    if (!stage) continue;
    if (joinNext && pipeline.length) {
      pipeline[pipeline.length - 1] += ` ${stage}`;
      joinNext = false;
      // Story 247.1.5: a command word after a bare assignment's substitution owns `$?`.
      // Story 248.1.19: the raw part (its leading whitespace kept, its quoted values still placeholders).
      if (after && afterFromSub && hasCommandWord(parts[k])) after = null;
      continue;
    }
    joinNext = false;
    if (after) {
      const reads = expands(stage);
      if (!pipefail && READ.test(reads) && !HONEST.test(reads)) {
        // Story 128.1.5: a loop or branch keyword opening the stage is not
        // part of the pipeline (`do printf … | node …` shows `printf …`).
        const head = after[0].replace(/^(?:do|then|else)\s+/, '');
        return {
          read: isPowerShell ? '$LASTEXITCODE' : '$?',
          pipeline: shown([head, ...after.slice(1)].join(' | ')).slice(0, 160),
          first: shown(head).slice(0, 120),
        };
      }
      after = null; // any other command resets the exit
    }
    const words = stage.split(/\s+/);
    if (!isPowerShell && words[0] === 'set') {
      for (let w = 1; w < words.length; w++) {
        if (words[w] === 'pipefail' && /^[-+][A-Za-z]*o$/.test(words[w - 1])) pipefail = words[w - 1][0] === '-';
      }
    }
    if (!isPowerShell && (words[0] === 'setopt' || words[0] === 'unsetopt') && words.some((w) => /^pipe_?fail$/i.test(w))) {
      pipefail = words[0] === 'setopt';
    }
    pipeline.push(stage);
  }
  // Story 151.1.1: each quoted substitution body is a command of its own.
  for (const body of nested) {
    const hit = dollarQAfterPipe(body);
    if (hit) return hit;
  }
  return null;
}

// Story 167.1.1 — the background arm. Returns { gate, filter, first } when the
// command's LAST multi-stage pipeline pipes a gate into tail, head or grep and
// nothing carries the gate's exit to the harness, else null. The gates: every
// checker, battery and suite readChecker recognizes, plus the two lane watches
// whose exit is a verdict, `gh pr checks` and `gh run watch --exit-status`.
// Null under an earlier `set -o pipefail` / `setopt pipefail`, and when a
// command after that pipeline reads `${PIPESTATUS…}` / `$pipestatus` (the
// record line carries the gate's exit). Quoted substitution bodies are not
// read: the notification reports the top-level compound's exit. Accepted
// limit: a single-quoted PIPESTATUS mention after the pipeline also stands it
// down (it never expands), erring toward silence on a shape nobody writes.
function backgroundMaskedGate(command) {
  const { text, resolve, basename, tokensOf, commandIndex, readChecker } = stageReader(command);
  const FILTERS = new Set(['tail', 'head', 'grep']);
  const HONEST = /\$\{?(?:PIPESTATUS|pipestatus)\b/;
  const program = (t) => basename(t).toLowerCase().replace(/\.(exe|cmd|bat|com)$/, '');
  // Compared as the joined phrase, as the semicolon arm's ghPrVerb does
  // (test/forge-boundary.test.mjs pins a standalone quoted forge-CLI literal
  // as an exec target; this reader never runs the forge).
  const laneWatch = (stage) => {
    const tokens = tokensOf(stage);
    const i = commandIndex(tokens);
    if (i >= tokens.length) return null;
    const words = tokens.slice(i + 1).map(resolve);
    const [group, verb] = words.filter((t) => !t.startsWith('-'));
    const phrase = `${program(tokens[i])} ${group}`;
    if (phrase === 'gh pr' && verb === 'checks') return `${phrase} ${verb}`;
    if (phrase === 'gh run' && verb === 'watch' && words.includes('--exit-status')) return `${phrase} ${verb}`;
    return null;
  };
  const parts = text
    .replace(/\|&/g, '|')
    .replace(/\d*>&\d*/g, ' ')
    .replace(/&>>?/g, '>')
    .replace(/\\\r?\n/g, ' ')
    .replace(/(&&|\|\||\|)\s*\n\s*/g, '$1 ')
    .split(/(&&|\|\||[;&\n|(){}`])/);
  let pipefail = false;
  let pipeline = []; // the current pipeline's stages
  let last = null; // { stages, pipefail } of the latest pipeline of 2+ stages
  let honestAfter = false; // a PIPESTATUS read after `last`
  const end = () => {
    if (pipeline.length >= 2) {
      last = { stages: pipeline, pipefail };
      honestAfter = false;
    }
    pipeline = [];
  };
  for (let k = 0; k < parts.length; k++) {
    if (k % 2 === 1) {
      // A separator: `|` continues the pipeline; anything else ends it.
      if (parts[k] !== '|') end();
      continue;
    }
    const stage = parts[k].trim();
    if (!stage) continue;
    if (last && HONEST.test(resolve(stage))) honestAfter = true;
    const words = stage.split(/\s+/);
    if (words[0] === 'set') {
      for (let w = 1; w < words.length; w++) {
        if (words[w] === 'pipefail' && /^[-+][A-Za-z]*o$/.test(words[w - 1])) pipefail = words[w - 1][0] === '-';
      }
    }
    if ((words[0] === 'setopt' || words[0] === 'unsetopt') && words.some((w) => /^pipe_?fail$/i.test(w))) {
      pipefail = words[0] === 'setopt';
    }
    pipeline.push(stage);
  }
  end();
  if (!last || last.pipefail || honestAfter) return null;
  const final = tokensOf(last.stages[last.stages.length - 1]);
  const fi = commandIndex(final);
  const filter = fi < final.length ? program(final[fi]) : null;
  if (!FILTERS.has(filter)) return null;
  for (const stage of last.stages.slice(0, -1)) {
    const gate = readChecker(stage)?.name ?? laneWatch(stage);
    if (gate) return { gate, filter, first: resolve(stage).replace(/\s+/g, ' ').trim().slice(0, 120) };
  }
  return null;
}

// Chore 10 — the semicolon arm. Returns { gate, verb, block, face } for the
// first state-changing verb that runs in a LATER clause than a gate (so the
// gate's exit was read and discarded), else null — `block` true with `face`
// 'commit' (Story 115.1.1), 'merge' (Story 126.1.1) or 'push' (Story 140.1.2)
// when that shape is denied. Clauses end at `;`, a newline that
// does not continue a chain, or a lone `&`; `&&` / `||` / `|` stay inside one.
function ungatedVerbAfterGate(command) {
  const { text, nested, resolve, basename, tokensOf, commandIndex, executesChecker } = stageReader(command);
  // Words read PAST to reach the command position: a loop or branch body is
  // the same chain (`do G; V; done`). `if` / `elif` / `while` / `until` are
  // NOT skipped — a gate in a condition is consumed by it, never discarded.
  const BODY_WORDS = new Set(['do', 'then', 'else', '!']);
  // A clause opening on one of these between the gate and the verb is the
  // command visibly handling the exit — stand down.
  const CONDITIONALS = new Set(['if', 'elif', '[', '[[', 'test', 'case', 'while', 'until']);
  const LEAVES = new Set(['exit', 'return', 'throw']);
  const GIT_ARG_FLAGS = new Set([
    '-C', '-c', '--config-env', '--git-dir', '--work-tree', '--namespace', '--super-prefix', '--exec-path',
  ]);
  const GIT_VERBS = new Set(['commit', 'push', 'switch', 'checkout']);
  // KB windows-executable-suffix-name-checks: git.exe is git, gh.exe is gh.
  const program = (t) => basename(t).toLowerCase().replace(/\.(exe|cmd|bat|com)$/, '');
  // Compared as the joined phrase, the way warn-unmarked-session.mjs reads
  // `gh pr create`: test/forge-boundary.test.mjs pins every standalone quoted
  // forge-CLI literal under scripts/ as an exec target, and this recognizer
  // only READS a command line — it never runs the forge.
  const ghPrVerb = (tokens, i) => {
    const [group, verb] = tokens.slice(i + 1).map(resolve).filter((t) => !t.startsWith('-'));
    return `${program(tokens[i])} ${group}` === 'gh pr' ? verb : null;
  };
  const stateChangingVerb = (tokens, i) => {
    if (ghPrVerb(tokens, i) === 'merge') return 'gh pr merge';
    if (program(tokens[i]) !== 'git') return null;
    let j = i + 1;
    while (j < tokens.length && tokens[j].startsWith('-')) j += GIT_ARG_FLAGS.has(tokens[j]) ? 2 : 1;
    const verb = resolve(tokens[j] ?? '');
    if (GIT_VERBS.has(verb)) return `git ${verb}`;
    // A force delete is `-D`, or (Story 140.1.1) a delete and a force spelled
    // apart: `--delete --force`, `-d -f`, `-df`. A plain `-d` refuses an
    // unmerged branch by itself, so it is no branch change on a red.
    const rest = tokens.slice(j + 1).map(resolve);
    const forceDelete =
      rest.some((t) => /^-[A-Za-z]*D[A-Za-z]*$/.test(t)) ||
      (rest.some((t) => t === '--delete' || /^-[A-Za-z]*d[A-Za-z]*$/.test(t)) &&
        rest.some((t) => t === '--force' || /^-[A-Za-z]*f[A-Za-z]*$/.test(t)));
    return verb === 'branch' && forceDelete ? 'git branch -D' : null;
  };
  const gateOf = (stage, tokens, i) => {
    const pr = ghPrVerb(tokens, i);
    if (pr === 'checks' || pr === 'merge') return `gh pr ${pr}`;
    return executesChecker(stage, BODY_WORDS);
  };

  const clauses = text
    .replace(/\|&/g, '|')
    .replace(/\d*>&\d*/g, ' ')
    .replace(/&>>?/g, '>')
    // Line continuations: a backslash-newline (Bash), a backtick-newline
    // (PowerShell), and a newline straight after `&&` / `||` / `|`.
    .replace(isPowerShell ? /`\r?\n/g : /\\\r?\n/g, ' ')
    .replace(/(&&|\|\||\|)\s*\n\s*/g, '$1 ')
    .replace(/[(){}`]/g, ' ')
    .split(/;|\n|(?<!&)&(?!&)/);

  let gate = null; // { name, clause } — the earliest gate not yet stood down
  // Story 115.1.1: the earliest RECOGNIZED VERDICT gate (a checker, battery or
  // suite — never `gh pr checks` / `gh pr merge`), read even behind an earlier
  // gh gate; the block keys on it.
  let checkerGate = null;
  // Story 126.1.1: the `gh pr merge` / `gh pr checks` gate — set as a gate, or
  // by a `gh pr merge` met as a VERB (its own exit gates nothing after its `;`
  // either: seat 2's `checks; merge; switch`). Since Story 140.1.1 its clause
  // is the LAST gh gate's (the earliest name kept). The merge-face deny keys on it.
  let ghGate = null;
  let warn = null; // the first ungated verb — returned when nothing is blocked
  let pushGate = null; // Story 140.1.2: { clause } of a `git push` not yet stood down
  let errexit = false; // Bash `set -e` / `set -o errexit`: a red gate ends the script
  for (let c = 0; c < clauses.length; c++) {
    for (const stage of clauses[c].split(/&&|\|\||\|/)) {
      const tokens = tokensOf(stage);
      const i = commandIndex(tokens, BODY_WORDS);
      if (i >= tokens.length) continue;
      const word = tokens[i].toLowerCase();
      if (!isPowerShell && word === 'set') {
        const flags = tokens.slice(i + 1).map(resolve);
        if (flags.some((t, k) => /^-[A-Za-z]*e/.test(t) || (t === '-o' && flags[k + 1] === 'errexit'))) errexit = true;
        if (flags.some((t, k) => /^\+[A-Za-z]*e/.test(t) || (t === '+o' && flags[k + 1] === 'errexit'))) errexit = false;
        continue;
      }
      // Story 140.1.2, the stale-head face: a `git push` ended by `;` (or a
      // newline) with `gh pr merge` in a later clause. A failed push leaves the
      // PR on its previous head, and the merge — auto-merge above all — lands
      // THAT head. The commit face's stand-downs apply (`|| exit`, `set -e`
      // ahead of the push, an `if` / `[` between); the `&&` chain is one clause.
      if (pushGate) {
        if ((c > pushGate.clause && CONDITIONALS.has(word)) || tokens.some((t) => LEAVES.has(t.toLowerCase()))) {
          pushGate = null;
        } else if (c > pushGate.clause && ghPrVerb(tokens, i) === 'merge') {
          return { gate: 'git push', verb: 'gh pr merge', block: true, face: 'push' };
        }
      }
      if (!errexit && stateChangingVerb(tokens, i) === 'git push') pushGate = { clause: c };
      if (gate) {
        const later = c > gate.clause;
        // Raw tokens, never resolved: a quoted "exit" is data, not a command.
        if ((later && CONDITIONALS.has(word)) || tokens.some((t) => LEAVES.has(t.toLowerCase()))) {
          gate = null;
          checkerGate = null;
          ghGate = null;
          continue;
        }
        // Story 126.1.1: a gh gate past the first gate is read too — a merge or
        // a lane watch in the same clause (`npm test && gh pr merge 5; git
        // switch main`) or a later one discards its exit at the next `;` alike.
        // Story 140.1.1: a LATER gh gate re-anchors the deny on its own clause
        // (keeping the earliest gate's name for the message), so `checks; merge
        // && switch` — the switch gated by the merge — is no longer refused;
        // under `set -e` a later gh gate ends the script when it fails, so it
        // gates what follows and the anchor clears.
        const pr = ghPrVerb(tokens, i);
        if (pr === 'merge' || pr === 'checks') {
          if (errexit) ghGate = null;
          else ghGate = { name: ghGate?.name ?? `gh pr ${pr}`, clause: c };
        }
        const verb = later ? stateChangingVerb(tokens, i) : null;
        if (verb) {
          warn ??= { gate: gate.name, verb, block: false };
          // Story 115.1.1: past a warn-only verb, a commit or push the
          // checker gate cannot reach is still read — and refused.
          if (checkerGate && c > checkerGate.clause && BLOCKED_VERBS.has(verb)) {
            return { gate: checkerGate.name, verb, block: true, face: 'commit' };
          }
          // Story 126.1.1: a local branch change a gh gate cannot reach.
          if (ghGate && c > ghGate.clause && MERGE_FACE_VERBS.has(verb)) {
            return { gate: ghGate.name, verb, block: true, face: 'merge' };
          }
          continue;
        }
        if (!checkerGate) {
          const name = executesChecker(stage, BODY_WORDS);
          if (name) checkerGate = { name, clause: c };
        }
        continue;
      }
      if (errexit) continue;
      const name = gateOf(stage, tokens, i);
      if (name) {
        gate = { name, clause: c };
        if (name.startsWith('gh pr ')) ghGate = gate;
        else checkerGate = gate;
      }
    }
  }
  // Story 151.1.1: each quoted substitution body is a command of its own — a
  // refusal inside one stands; its warn only when the outer command has none.
  for (const body of nested) {
    const inner = ungatedVerbAfterGate(body);
    if (inner?.block) return inner;
    warn ??= inner;
  }
  return warn;
}

// Story 128.1.8 (register class stdin-readfilesync-eagain-on-nonblocking-pipe):
// stdin through the async iterator, never readFileSync(0) — a 1.1 MB payload on a
// non-blocking pipe read as unreadable on Linux CI (Node 24, hub #3034's first run).
async function readStdin() {
  const chunks = [];
  for await (const chunk of process.stdin) chunks.push(chunk);
  return Buffer.concat(chunks).toString('utf8');
}
