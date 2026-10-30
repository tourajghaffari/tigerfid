#!/usr/bin/env node
// warn-zsh-word-split.mjs — PreToolUse hook: DENY (Story 99.1.1; the warn form
// of Story 90.1.1 until 2026-09-14) when a Bash-tool command on a zsh box loops
// over an UNQUOTED variable — `for <name> in … $var …` or `set -- $var` — so the
// loop would run ONCE over the whole string, and (Story 151.1.2) when it holds
// an unquoted word beginning `=`, which zsh looks up as a command and stops the
// line on. The file keeps its `warn-` name:
// every repo's `.claude/settings.json` wires it by that name (Story 90.1.2's
// sweep), and a deny needs no new wiring — only this file's return shape.
//
// Why this exists — register class zsh-word-split, SEVEN seats (2026-09-01,
// 09-02, 09-03, 09-08, 09-10, 09-12, 09-14): the harness's Bash tool on the Mac
// is zsh, whose SH_WORD_SPLIT is off, so an unquoted `$var` expands to exactly
// ONE word. Every seat but one was a list-style loop in a hand-written watcher
// or verification pass — `for pair in $repos` (one iteration;
// `bridgetandomid.com:?` printed to a 30-minute cap while all 13 round PRs had
// merged — Hub Sprint 31 F1), `set -- $spec` (`$1` the whole record, `$2`/`$3`
// EMPTY; usage banners and 404s — the Epic 69.1 close), `set -- $total; shift
// 3` (`shift count must be <= $#`; a CI watcher polling past two green checks
// — 2026-09-08). Three reading forms — KB zsh-harness-shell-no-word-split, the
// Mac memory rule, the register — and two seats landed after all three; the
// seat-5 decision (the Sprint 31 reviewer pass, 2026-09-10) superseded them to
// `hook`, WARN-level: this file, wired in the hub and every repo by Story
// 90.1.2, with the clause "a seat AFTER the hook lands escalates it to the
// family's deny shape". Seat 7 (the tigerdash Epic 7.3 close, 2026-09-14, hub
// #2402) landed INSIDE the predicate after the warn form was live: `f=$(grep
// -rl … | head -5); for x in $f; do …; done` — the hook FIRED, but a warn's
// text reaches the model beside the tool result, AFTER the loop has run once
// over the joined names. The clause is executed by Story 99.1.1: the same
// predicate, the same remedies, returned as a deny — the command never runs.
//
// Predicate: `tool_name === 'Bash'` only (the PowerShell tool has no such
// trap — Sprint 32 decision (i)); the harness shell is zsh — process.platform
// darwin, or $SHELL ending in zsh — behind the env seam
// WARN_ZSH_WORD_SPLIT_SHELL=zsh|other so the tests pin both faces on any box
// (the warn-piped-checker.mjs WARN_PIPED_CHECKER_PLATFORM shape). The command
// is NEUTRALIZED first: heredoc bodies, single-quoted spans, double-quoted
// spans, and comments are data — a double-quoted `"$var"` list item is exactly
// the quoted-and-silent form, and a loop inside `bash -c "…"` runs under a
// shell that splits. Then every `for <name> in <list>` head and every
// `set -- <list>` is read: it fires when a list ITEM carries an unquoted
// `$name` / `${name}` and is none of the shapes zsh does split or expand
// (the `for` arm, since Stories 244.1.33 and 247.1.6, only a WHOLE `$name`
// item, a whole-variable part of a brace list, or one across a line
// continuation — an affixed word is one word under zsh, though bash splits it) —
// `${=name}` (the explicit split), a flag or subscript form (`${(f)name}`,
// `${name[@]}`, `$name[@]`), a glob (zsh globs the literal pattern around
// the value), a command substitution (`$(…)` at any nesting, backticks —
// dropped as a SPAN like a quoted one, so a variable INSIDE it is never an
// item: zsh splits the substitution's output — Story 92.1.3), the positional
// arrays (`$@`, `$*`) — and the name was not assigned
// as an ARRAY in the same command (`name=(…)`, the remedy's own shape).
// Silent when the command turns splitting on (`setopt shwordsplit` /
// `sh_word_split`, `set -y`) or leaves zsh (`emulate sh` / `bash` / `ksh`).
// ONE deny per command naming every offending variable, the three remedies
// (`${=var}`; an array; explicit per-item commands or `while read -r`), KB
// zsh-harness-shell-no-word-split, and the register class.
// Accepted limits: the zsh `for name (list)` form and `cmd $flags` (an
// unquoted scalar as ARGUMENTS — except the LIST-SHAPED `$(…)`-fed face,
// read at WARN tier since hub Chore 3, 2026-09-16, and the quoted-LITERAL
// face, read at WARN tier since hub Story 161.1.1, 2026-10-05, both below)
// are not DENIED — the deny's predicate is the two loop forms every
// in-predicate seat shares; a `do` on
// the head's own line without a `;` is not valid shell and is not parsed. The widening question seat 6
// left open (`pids=$(lsof -ti tcp:1402); kill $pids` — the Epic 7.2 close,
// 2026-09-12: `kill` refused the one word, LOUDLY) was DECIDED at Story
// 99.1.1 (Omid, 2026-09-14): NOT widened. An argument list is a different
// reading — one word to the command is sometimes what was meant (`git commit
// -m $msg`, `git show $sha`); the predicate is content-blind, which is exact
// for a loop (nobody loops over one word) and wrong for an argument (most
// scalars ARE one word), so a deny there would block legitimate commands
// fleet-wide; the only content-safe widening (a same-command LITERAL with
// whitespace) would not have caught seat 6 (a `$(…)`-fed name), while the
// widening that would (any `$(…)`-fed scalar) blocks `sha=$(git rev-parse
// HEAD); git show $sha`; an argument list fails loudly in the same tool
// result where a loop fails silently; and the register holds ONE seat of
// that face. Trigger: a second argument-face seat files its own story, at
// WARN tier first (the family's posture for a first mechanism).
//
// Chore 3 (hub, 2026-09-16 — the trigger MET by seat 8, the tigerdash Epic
// 12.1 close, 2026-09-14: `DATA=$(find … | tr '\n' ' '); cat $DATA` read
// `No such file` for the joined string and every count after it was 0): the
// ARGUMENT face is read at WARN tier for a LIST-SHAPED `$(…)`-fed scalar —
// predicate (b) of the chore, chosen on a measured false-positive count —
// as the family's warn shape (systemMessage + additionalContext, no
// permissionDecision; the command proceeds), only when no deny stands. A
// one-word producer (`git show $sha`) stays unread, by the decision above.
// findArgumentFaces / listShaped / renderArgumentWarn below.
//
// Story 161.1.1 (hub, Hub Sprint 43 leg 4, 2026-10-05 — the decision's
// trigger met by the vituno 50.1.1 seat, hub #3231: `ARGS="--src src
// --override … --narrative …"` then `node …/gates/check.mjs $ARGS --site
// <dir>`, three runs each exiting 2 on ONE joined argument while this guard
// said nothing): the ARGUMENT face also reads a LITERAL-fed scalar at WARN
// tier — a scalar assigned a quoted literal that carries whitespace
// (`name="…"` with no `$`, backtick or backslash inside, or `name='…'`), as
// a standalone assignment, then used unquoted as a bare argument by the same
// use rules. This is the content-safe widening the decision above named: the
// literal's own whitespace says a list was likely meant. A literal without
// whitespace and an env prefix (`X="a b" cmd $X`, whose use expands the old
// value) stay silent. The same warn shape, merged with the other warn faces
// into ONE warn object (mergeWarns). findLiteralFaces / renderLiteralWarn
// below.
//
// Chore 8 (hub, 2026-09-17 — register class zsh-modifier-face, seats 1 and
// 2): the MODIFIER face at WARN tier. Under zsh a colon after a BARE `$name`
// (a positional, `$@`, or a subscripted `$x[1]` too) followed by one of the
// THIRTEEN measured letters `a A c e h l P q Q r s t u` is a history MODIFIER
// — `$R:refs/z` hands back `$R` with its extension stripped and the `r`
// consumed, `$H:hub` the dirname of `$H` + `ub`, `$R:scripts` a `bad
// substitution` that aborts the WHOLE command before anything runs — and it
// applies inside double quotes, an unquoted-delimiter heredoc body, `$(…)`,
// backticks and `[[ ]]` alike (every letter, every context, and every
// non-letter measured 2026-09-17 on the harness shell, zsh 5.9: `x` in the
// KB's first list is NOT a modifier; `$a:$b`, `$host:8080`, `$x:docs/y` are
// intact). `${name}:x`, `"$name":x` (the quote closed before the colon),
// `\$name:x`, a single-quoted span, a quoted-delimiter heredoc body, a
// comment, and `emulate sh|bash|ksh` (modifiers off — measured) are silent;
// `setopt shwordsplit` / `set -y` do NOT turn the face off (measured). Seat 1
// (2026-09-16, the hub's Mac seat-9 sitting): a marker-manifest refspec
// `+refs/heads/$R:refs/fleet-view/$R` fetched the wrong ref silently under
// `2>/dev/null`, and a `$R:scripts/…` probe aborted the call. Seat 2
// (2026-09-17, the Mac orientation sweep): `for r in "$H:hub" "$T:tigerdash"
// "$E:expeal.com"` ran three `git -C … ls-remote` calls on non-paths, silent
// under `2>/dev/null` — the seat the KB section named as the trigger ("a
// second seat files the warn-tier hook"). Its own neutralizer (double-quoted
// spans and unquoted heredoc bodies are LIVE here, unlike the loop face's);
// zero legitimate uses in the fleet's commands (nobody writes a history
// modifier in a Bash-tool command), so the predicate is content-safe; WARN
// as the family's first mechanism, one warn object with the argument face
// when both fire, the deny still alone on a mixed command. Accepted limits
// (the family's): a `<<word` inside a double-quoted string is read as a
// heredoc head; an escaped `\"` outside a span is read as text. Built at WARN
// tier; seats after it landed escalated the face to the deny shape (the
// family's clause — Story 241.1.1, below). findModifierFaces /
// renderModifierDeny below.
//
// Story 128.1.4 (hub, Initiative 128 — register class zsh-equals-face, five
// seats 2026-09-29 → 2026-10-01): the EQUALS face at WARN tier, in Chore 8's
// shape. Under zsh's EQUALS option (on in zsh mode) a word that BEGINS with an
// unquoted `=` is a command-path lookup — `=ls` is `/bin/ls` — so an output
// separator `echo ======` fails `(eval):1: ===== not found` (MEASURED, seats 4
// and 5), exit 1, and nothing queued after it runs. The KB hook named the face
// after seat 1; seats 2 to 5 read the hook only. The predicate is word-start
// anchored: start of the command, or after whitespace, `;`, `|`, `&`, `(`, `)`
// or a backtick, an `=` followed by at least one word character (a lone `=`, a
// test's equality, passes). A `>` ends the word — it is a redirection (`echo
// ==> done` looks up `=`). Its own neutralizer: heredoc bodies (either
// delimiter — no filename expansion there), single- and double-quoted spans,
// comments, `${…}` (the explicit split `${=name}` and `${x:=default}`),
// `[[ … ]]` and `(( … ))` / `$(( … ))` are data; `$(…)` and backticks stay
// live. Silent when the command turns EQUALS off (`setopt noequals` /
// `no_equals`, `unsetopt equals`) or leaves zsh mode (`emulate sh|bash|ksh` —
// EQUALS is a zsh-mode option, documented, not measured). Single brackets
// fire: `[ "$a" == b ]` is a word beginning `=` (the same predicate). Each
// word named once, with the command zsh looks up. Built at WARN tier; a seat
// after it landed escalated the face to the deny shape (the family's clause —
// Story 151.1.2, below).
//
// Story 151.1.2 (hub, Initiative 151 — register class zsh-equals-face, seat 7,
// the first after Story 128.1.4 landed): the EQUALS face is a DENY. The arm
// warned on `echo ======` and zsh still stopped the line (`(eval):1: =====
// not found`, exit 1; the read after it never ran) — a PreToolUse warn reaches
// the model only with the result, so it cannot stop the line it names. The
// same predicate, the same remedies (quote the word, or drop the separator),
// returned in the family's deny shape. With the loop deny on the same command,
// ONE deny names both (mergeDenies); the argument face stays WARN tier (the
// modifier face did too, until Story 241.1.1) and is not added beside a deny.
// findEqualsFaces / renderEqualsDeny below.
//
// Story 241.1.1 (hub Sprint 66 leg 2, Initiative 241 — register class
// zsh-modifier-face, seats 3 to 7, every one after Chore 8 landed: pantheon
// 2026-10-03 `"$rev:ruby/…"`, the hub and expeal.com 2026-10-10 refspec
// fetches `$b:r`/`$B:r`, vituno 2026-10-12 `$sha:s` (a `bad substitution`
// that emptied a field), the Hub Sprint 64 reviewer 2026-10-24 `"$r:config/…"`;
// Omid's item 3 (a) of 2026-10-24): the MODIFIER face is a DENY. The warn
// reached each seat only with the result, as the equals face's did. Chore 8's
// predicate and neutralizer unchanged, returned in the family's deny shape with
// three remedies (brace the name, close the quote before the colon, `emulate
// sh`); with the loop or equals deny on one command, ONE deny names each
// (mergeDenies); the argument face is not added beside it. Content-safe as
// Chore 8 said: the proposal's census found zero bare-variable colon-letter
// uses in the fleet's `*.sh`/`*.ps1` scripts and only prose describing the face
// in the hub's KB, skills and CLAUDE.md. renderModifierDeny below.
//
// The entry guard compares REAL paths on both sides (Story 92.1.3): Node
// realpaths the main module's URL, and a hook invoked through a symlinked
// project directory or file arrives with the symlink in argv[1] — a
// path.resolve there left the guard vacuous (silent, exit 0, no signal).
//
// Deny-level since Story 99.1.1 (2026-09-14; warn-level from Story 90.1.1
// before it — the register record's escalation clause, met by seat 7): the
// hook returns the family's shape (block-commit-on-main.mjs) —
// `hookSpecificOutput: { hookEventName: 'PreToolUse', permissionDecision:
// 'deny', permissionDecisionReason }` — and the harness refuses the command
// BEFORE it runs, showing the reason (the variable, the head, what the form
// does with the one word, the three remedies) to the model, which re-issues
// the command in a splitting form. No `systemMessage`, no
// `additionalContext`: one shape, read once. Exit 0 always — the decision is
// stdout's; fail-open stands (an unreadable payload stands down with nothing
// on stdout, so a guard that cannot read never blocks).
//
// Manual test (from any repo with the synced copy):
//   echo '{"tool_name":"Bash","tool_input":{"command":"repos=\"a b\"; for r in $repos; do echo $r; done"}}' \
//     | WARN_ZSH_WORD_SPLIT_SHELL=zsh node scripts/warn-zsh-word-split.mjs
//   The modifier face (Chore 8; a deny since Story 241.1.1):
//   echo '{"tool_name":"Bash","tool_input":{"command":"H=/a/b.c; git -C \"$H:hub\" ls-remote"}}' \
//     | WARN_ZSH_WORD_SPLIT_SHELL=zsh node scripts/warn-zsh-word-split.mjs
// Suite: test/warn-zsh-word-split.test.mjs (node --test). The P25 growth
// probe (graduated sizes over pathological strings — an exponential shows as
// growth, never a hang): node test/warn-zsh-word-split.probe.mjs
//
// Canonical: synced to every repo by scripts/sync-session-wrap.mjs. Edit HERE.

import { realpathSync } from 'node:fs';
import path from 'node:path';
import { fileURLToPath } from 'node:url';

const NAME = '[A-Za-z_][A-Za-z0-9_]*';
// A `$name` / `${name}` reference — not an escaped `\$`, not `$name[…]` (a
// subscript). `${=name}`, `${(flags)name}`, `${name[…]}`, and `$(…)` never
// match this shape (the char after `${` or `$` is not a name start), so the
// item test below only has to recognize the shapes zsh DOES split.
const REF_RE = new RegExp(`(?<!\\\\)\\$(?:\\{(${NAME})\\}|(${NAME}))(?!\\[)`, 'g');
// An item zsh splits or expands on its own: the explicit split, a flag form,
// an array subscript, the positional arrays, a command substitution, a glob.
const SPLIT_ITEM_RE = /\$\{=|\$\{\(|\[[@*]\]|\$[@*]|\$\(|`|[*?[]/;
// Command text that turns splitting on, or leaves zsh, for the whole command.
const SPLIT_ON_RE = /\b(?:setopt\s+sh_?word_?split|set\s+-y\b|emulate\s+(?:-[LR]+\s+)?(?:sh|bash|ksh)\b)/i;
// Story 247.1.6: the list continues across a backslash-newline (`for f in a \` then `$B`).
const FOR_RE = new RegExp(`\\bfor\\s+(${NAME})\\s+in\\s+((?:[^;\\n\\\\]|\\\\\\r?\\n|\\\\[^\\n])*)`, 'g');
const SET_RE = /\bset\s+--\s+([^;\n|&]*)/g;
const ARRAY_RE = new RegExp(`(?:^|[\\s;&|(])(${NAME})=\\(`, 'g');

// Command substitutions are data too (Story 92.1.3): `$(…)` at any nesting
// and backtick spans become an empty double-quoted item, like quoted spans —
// a linear scan, never a backtracking pattern (the P25 bound). An UNCLOSED
// `$(` or backtick is left as written; the item test still skips the item
// carrying it, as before.
function dropSubstitutions(text) {
  let out = '';
  let i = 0;
  while (i < text.length) {
    const ch = text[i];
    if (ch === '`') {
      const j = text.indexOf('`', i + 1);
      if (j === -1) return out + text.slice(i);
      out += ' "" ';
      i = j + 1;
      continue;
    }
    if (ch === '$' && text[i + 1] === '(') {
      let depth = 0;
      let j = i + 1;
      for (; j < text.length; j++) {
        if (text[j] === '(') depth++;
        else if (text[j] === ')' && --depth === 0) break;
      }
      if (j >= text.length) return out + text.slice(i);
      out += ' "" ';
      i = j + 1;
      continue;
    }
    out += ch;
    i++;
  }
  return out;
}

// Heredoc bodies, quoted spans, command substitutions, and comments are data.
// The heredoc walk is warn-cwd-drift.mjs's; quoted spans and substitutions
// become an empty double-quoted item — a quoted span carrying a newline
// (literal, or the `\\n` of `tr '\\n' ' '`) becomes `"\\n"` instead, the one
// tell-tale the Chore 3 join-tail read needs; still one quoted item to the
// loop face. neutralizeSpans stops before the substitution drop: the Chore 3
// assignment scan reads substitution BODIES there.
// A quoted span (single, or double with its escapes) and a comment, as the
// shell reads them: neutralizeSpans blanks them, the literal read (Story
// 161.1.1) tokenizes the spans and drops the comments the same way.
const QUOTED_SPAN_RE = /'[^']*'|"(?:[^"\\]|\\.)*"/g;
const COMMENT_RE = /(^|\s)#[^\n]*/g;

// Heredoc bodies dropped (each body and its terminator line). Shared by
// neutralizeSpans and the literal read (Story 161.1.1), so both see the same
// command.
function stripHeredocs(command) {
  let text = String(command);
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

export function neutralizeSpans(command) {
  let text = stripHeredocs(command);
  text = text.replace(QUOTED_SPAN_RE, (m) => (/\\n|\n/.test(m) ? ' "\\n" ' : ' "" '));
  // Comments go BEFORE the substitution scan (Sprint 33 N-2): a `# note (`
  // on the first line of a multi-line `$(…)` is data to the shell too, and
  // its unbalanced paren would otherwise leave the substitution unclosed —
  // the text left as written, the inner variable a false warning. Quoted
  // spans are already gone, so a quoted `#` cannot be mistaken for one.
  text = text.replace(COMMENT_RE, (_, ws) => ws);
  return text;
}

export function neutralize(command) {
  return dropSubstitutions(neutralizeSpans(command));
}

// Every `for … in <list>` head and `set -- <list>` whose list carries an
// unquoted `$name` zsh will not split — in command order, one record per
// head, its variables in first-seen order. Pure; the tests and the probe
// import it.
export function findWordSplits(command) {
  const text = neutralize(command);
  if (SPLIT_ON_RE.test(text)) return [];
  const arrays = new Set();
  for (const m of text.matchAll(ARRAY_RE)) arrays.add(m[1]);
  const found = [];
  // Story 244.1.33 (register class zsh-word-split-guard-denies-separate-variable-prefixed-items):
  // the `for` arm reads a variable only when it is a WHOLE whitespace item — a bare `$VAR` or
  // `${VAR}`. A word built around one (`$D/a.log`, `$a-$b`) is one word under zsh, the harness
  // shell, which never splits a parameter (bash WOULD split it: with D="a b", bash's
  // `for f in $D/x.log` loops over `a` and `b/x.log`), so a list of them is a list there, never
  // the trap; `for d in $P $P/x` still denies by its whole `$P`. Story 247.1.6: a brace list
  // (`{$A,$B}`) denies by each whole-variable part, and a backslash-newline is removed before
  // the items are read (the shell joins the lines there), so a whole item glued to it
  // (`$A\` then `b`) or on the continued line (`a \` then `$B`) is read. The `set --` face keeps
  // reading every reference in an item (D10: unchanged).
  const WHOLE_REF = new RegExp(`^${REF_RE.source}$`);
  const BRACE_LIST = /^\{([^{}]*,[^{}]*)\}$/;
  const judge = (form, head, list) => {
    const vars = [];
    const items = form === 'for' ? list.replace(/\\\r?\n/g, '').split(/\s+/) : list.split(/\s+/);
    for (const item of items) {
      if (!item || SPLIT_ITEM_RE.test(item)) continue;
      if (form === 'for' && BRACE_LIST.test(item)) {
        for (const part of BRACE_LIST.exec(item)[1].split(',')) {
          const r = new RegExp(`^${REF_RE.source}$`).exec(part);
          if (!r) continue;
          const name = r[1] ?? r[2];
          if (!arrays.has(name) && !vars.includes(name)) vars.push(name);
        }
        continue;
      }
      if (form === 'for' && !WHOLE_REF.test(item)) continue;
      for (const r of item.matchAll(REF_RE)) {
        const name = r[1] ?? r[2];
        if (!arrays.has(name) && !vars.includes(name)) vars.push(name);
      }
    }
    if (vars.length) found.push({ form, head: head.trim(), vars });
  };
  for (const m of text.matchAll(FOR_RE)) judge('for', m[0], m[2]);
  for (const m of text.matchAll(SET_RE)) judge('set', m[0], m[1]);
  return found;
}

// --- Chore 3 (hub, 2026-09-16): the ARGUMENT face at WARN tier ---
// Register class zsh-word-split seats 6 and 8 — both `$(…)`-fed scalars used
// unquoted as a bare argument list (`kill $pids`, `cat $DATA`); Omid's
// WIDENING-DECIDED trigger (2026-09-14) met by seat 8. Predicate (b) of the
// chore, chosen on the false-positive measurement quoted in its tick: a scalar
// assigned from a command substitution whose producer is LIST-SHAPED, later
// used unquoted as a bare argument in the same command string. The shape is
// read from the substitution's LAST pipeline stage backwards: a JOIN tail
// (`tr '\n' ' '`, bare `xargs`, `paste -s`) or a PRODUCER (find, ls, lsof,
// pgrep, seq, grep -l, git ls-files / diff --name-only / ls-remote / branch /
// tag, gh … list) is a list; a REDUCER (wc, head -1 / -n 1, tail -1, tr -d,
// grep -c / -q, basename …) is one word; a PASS-THROUGH filter (head -5,
// tail, sort, uniq, grep, sed, awk, cut, xargs <cmd>, jq …) defers to the
// stage before it; an UNKNOWN producer (`git rev-parse HEAD`, `cat file`,
// `node -e`, `gh pr view`, `echo`) is one word — conservative for a first
// mechanism, so `sha=$(git rev-parse HEAD); git show $sha` is silent by
// construction, not by a special case. A use is a bare argument when the
// `$name` / `${name}` sits in a command segment whose head is a real command:
// not an assignment (`x=$name`), not a redirect target, not the command name
// itself, not a loop head (`for` / `select` / `set` — the deny's domain), and
// not under `echo` / `printf` / `case` / `[[` / `[` / `test` / `export` /
// `local` / `eval` … (one word is right, or harmless, there).
// The deny (the loop face) wins when both faces share a command — one shape,
// read once. Option (a) of the chore — ANY `$(…)`-fed scalar — is reachable
// as `{ producer: 'any' }` for the measurement only; the hook reads (b).

const ASSIGN_SUB_RE = new RegExp(`(?:^|[\\s;&|({])(${NAME})=\\$\\(`, 'g');
const REDUCERS = new Set(['wc', 'md5', 'md5sum', 'shasum', 'sha1sum', 'sha256sum', 'cksum', 'basename', 'dirname', 'date', 'realpath', 'readlink', 'mktemp', 'pwd', 'whoami', 'hostname', 'id', 'uuidgen']);
const PRODUCERS = new Set(['find', 'ls', 'lsof', 'pgrep', 'seq', 'fd', 'locate', 'comm']);
const PASS_THROUGH = new Set(['head', 'tail', 'sort', 'uniq', 'sed', 'awk', 'cut', 'tee', 'cat', 'rev', 'nl', 'column', 'grep', 'egrep', 'fgrep', 'rg', 'xargs', 'tr', 'jq', 'paste']);
const WRAPPERS = new Set(['command', 'sudo', 'env', 'time', 'nice', 'exec', 'nohup']);
const SEGMENT_KEYWORDS = new Set(['then', 'do', 'else', 'elif', 'if', 'while', 'until', '!', ...WRAPPERS]);
// Heads under which an unquoted scalar is one word by design, or harmless.
// Loop heads are the deny's domain (findWordSplits), never a bare argument.
const LOOP_HEADS = new Set(['for', 'select', 'set']);
const ONE_WORD_HEADS = new Set(['echo', 'printf', 'print', 'case', '[[', '[', 'test', 'export', 'local', 'declare', 'typeset', 'readonly', 'let', 'exit', 'return', 'unset', ':', 'true', 'false', 'eval', 'read']);
const ENV_ASSIGN_RE = new RegExp(`^${NAME}=`);

// The shape of ONE pipeline stage: 'join' | 'producer' | 'reducer' | 'pass' |
// 'unknown'. `first` marks the stage that has nothing before it — a filter
// there reads a file, which is unknown, not a list.
function stageShape(stage, first) {
  const tokens = stage.trim().split(/\s+/).filter(Boolean);
  while (tokens.length && (ENV_ASSIGN_RE.test(tokens[0]) || WRAPPERS.has(tokens[0]))) tokens.shift();
  if (!tokens.length) return 'unknown';
  const [t0, ...rest] = tokens;
  const shortFlags = rest.filter((t) => /^-[A-Za-z0-9]+$/.test(t)).map((t) => t.slice(1));
  const has = (f) => rest.includes(f);
  const passOrUnknown = first ? 'unknown' : 'pass';
  if (t0 === 'tr') {
    if (shortFlags.some((f) => f.includes('d'))) return 'reducer';
    if (rest.some((t) => t === '"\\n"')) return 'join';
    return passOrUnknown;
  }
  if (t0 === 'xargs') return rest.every((t) => t.startsWith('-')) ? 'join' : passOrUnknown;
  if (t0 === 'paste') return shortFlags.some((f) => f.includes('s')) ? 'join' : passOrUnknown;
  if (t0 === 'head' || t0 === 'tail') {
    const one = rest.some((t, i) => t === '-1' || t === '-n1' || /^-[a-z]*c/.test(t) || (t === '-n' && rest[i + 1] === '1'));
    return one ? 'reducer' : passOrUnknown;
  }
  if (t0 === 'grep' || t0 === 'egrep' || t0 === 'fgrep' || t0 === 'rg') {
    if (shortFlags.some((f) => /[cq]/.test(f)) || has('--count') || has('--quiet') || has('-m1') || (has('-m') && rest[rest.indexOf('-m') + 1] === '1')) return 'reducer';
    if (shortFlags.some((f) => /[lL]/.test(f)) || has('--files-with-matches') || has('--files-without-match')) return 'producer';
    return passOrUnknown;
  }
  if (REDUCERS.has(t0)) return 'reducer';
  if (PRODUCERS.has(t0)) return 'producer';
  if (t0 === 'git') {
    const t1 = rest[0];
    if (['ls-files', 'ls-remote', 'ls-tree', 'branch', 'tag', 'for-each-ref'].includes(t1)) return 'producer';
    if ((t1 === 'stash' || t1 === 'worktree') && rest[1] === 'list') return 'producer';
    if (['diff', 'diff-tree', 'show', 'log'].includes(t1) && rest.some((t) => t === '--name-only' || t === '--name-status')) return 'producer';
    return 'unknown';
  }
  // A regex test, not a quoted literal: the forge-boundary gate (test/forge-
  // boundary.test.mjs) reads any standalone quoted gh literal under scripts/
  // as an exec target — register class regex-code-gate-scans-string-literal-
  // interiors, seat 2 (2026-09-16).
  if (/^gh$/.test(t0)) return rest.slice(0, 3).includes('list') ? 'producer' : 'unknown';
  if (PASS_THROUGH.has(t0)) return passOrUnknown;
  return 'unknown';
}

// Pipeline stages of a substitution body: split on a top-level `|` (`||`
// is a command separator, treated the same — the last command decides).
function splitStages(body) {
  const stages = [];
  let depth = 0;
  let start = 0;
  for (let i = 0; i < body.length; i++) {
    const ch = body[i];
    if (ch === '(' || ch === '{') depth++;
    else if (ch === ')' || ch === '}') depth--;
    else if ((ch === '|' || ch === ';' || ch === '&') && depth === 0) {
      stages.push(body.slice(start, i));
      if (body[i + 1] === ch) i++;
      start = i + 1;
    }
  }
  stages.push(body.slice(start));
  return stages.filter((s) => s.trim().length);
}

// Walk from the LAST stage back through pass-through filters.
export function listShaped(body) {
  const stages = splitStages(body);
  for (let i = stages.length - 1; i >= 0; i--) {
    const shape = stageShape(stages[i], i === 0);
    if (shape === 'join' || shape === 'producer') return true;
    if (shape === 'reducer' || shape === 'unknown') return false;
  }
  return false;
}

const displayText = (s, cap = 80) => {
  const t = s.replace(/\s*""\s*/g, ' "…" ').replace(/\s+/g, ' ').trim();
  return t.length > cap ? `${t.slice(0, cap - 1)}…` : t;
};

// Every substitution-fed scalar (list-shaped by default) used unquoted as a
// bare argument — in assignment order, each once, with up to three uses.
// Pure; the tests and the measurement import it.
export function findArgumentFaces(command, { producer = 'list' } = {}) {
  const text0 = neutralizeSpans(command);
  if (SPLIT_ON_RE.test(text0)) return [];
  const arrays = new Set();
  for (const m of text0.matchAll(ARRAY_RE)) arrays.add(m[1]);
  const fed = new Map();
  for (const m of text0.matchAll(ASSIGN_SUB_RE)) {
    const name = m[1];
    if (arrays.has(name) || fed.has(name)) continue;
    const open = m.index + m[0].length - 2;
    let depth = 0;
    let j = open + 1;
    for (; j < text0.length; j++) {
      if (text0[j] === '(') depth++;
      else if (text0[j] === ')' && --depth === 0) break;
    }
    if (j >= text0.length) continue;
    const body = text0.slice(open + 2, j);
    if (producer === 'list' && !listShaped(body)) continue;
    fed.set(name, { name, producer: displayText(body), uses: [], count: 0 });
  }
  if (!fed.size) return [];
  fillBareUses(text0, fed);
  return [...fed.values()].filter((f) => f.count).map(({ name, producer: prod, uses }) => ({ name, producer: prod, uses }));
}

// Chore 3's use rules, shared by both argument reads (the substitution-fed
// face above, the literal-fed face below — Story 161.1.1): each `$name` /
// `${name}` of a fed scalar that sits in a command segment as a BARE argument
// — never after `=`, never a redirect target, never the head word, never
// under an ONE_WORD_HEADS or loop head — counts on its record, with up to
// three distinct uses. `text0` is neutralizeSpans' output; `fed` maps a name
// to its record ({ count, uses, … }), filled in place.
function fillBareUses(text0, fed) {
  const text1 = dropSubstitutions(text0);
  // Segment starts in one pass; the head of each segment computed once.
  const seps = [];
  for (const m of text1.matchAll(/[;\n|&(){}]/g)) seps.push(m.index);
  let k = 0;
  const heads = new Map();
  const headOf = (segStart, segEnd) => {
    if (heads.has(segStart)) return heads.get(segStart);
    const seg = text1.slice(segStart, segEnd);
    let head = null;
    let offset = -1;
    for (const w of seg.matchAll(/\S+/g)) {
      if (SEGMENT_KEYWORDS.has(w[0]) || ENV_ASSIGN_RE.test(w[0])) continue;
      head = w[0];
      offset = w.index;
      break;
    }
    const rec = { head, offset };
    heads.set(segStart, rec);
    return rec;
  };
  for (const m of text1.matchAll(REF_RE)) {
    const name = m[1] ?? m[2];
    const face = fed.get(name);
    if (!face) continue;
    const p = m.index;
    while (k < seps.length && seps[k] < p) k++;
    const segStart = k ? seps[k - 1] + 1 : 0;
    const segEnd = k < seps.length ? seps[k] : text1.length;
    if (text1[p - 1] === '=') continue;
    if (/[<>]\s*$/.test(text1.slice(Math.max(segStart, p - 8), p))) continue;
    const { head, offset } = headOf(segStart, segEnd);
    if (head === null || segStart + offset === p || ONE_WORD_HEADS.has(head) || LOOP_HEADS.has(head)) continue;
    face.count++;
    if (face.uses.length < 3) {
      const use = displayText(text1.slice(segStart + offset, p + m[0].length));
      if (!face.uses.includes(use)) face.uses.push(use);
    }
  }
}

// --- Story 161.1.1 (Hub Sprint 43 leg 4): the LITERAL-fed argument face ---
// A scalar assigned a QUOTED literal that carries whitespace — `name="…"`
// with no `$`, backtick or backslash inside (a plain string), or `name='…'`
// — as a standalone assignment (the rest of its segment is only further
// assignments: `X="a b" cmd $X` is an env prefix, whose use expands the OLD
// value), then used unquoted as a bare argument by Chore 3's use rules
// (fillBareUses). The literal is read from the RAW span: after the heredoc
// strip, each quoted span becomes an indexed placeholder (neutralizeSpans
// blanks them), and comments drop as neutralizeSpans drops them. A name also
// assigned an array or a `$(…)` in the same command keeps that reading.
// Every scan is linear (P25). Pure; the tests and the measurement import it.
const LITERAL_ASSIGN_RE = new RegExp(`(?:^|[\\s;&|({])(${NAME})=@@([0-9]+)@@(?=[\\s;&|)}]|$)`, 'g');
const SEGMENT_END = new Set([';', '\n', '&', '|', '(', ')', '{', '}']);
const WHITESPACE_RE = /\s/;
const NOT_PLAIN = new Set(['$', '`', String.fromCharCode(92)]);

export function findLiteralFaces(command) {
  const spans = [];
  let text = stripHeredocs(command).replace(QUOTED_SPAN_RE, (m) => {
    spans.push(m);
    return `@@${spans.length - 1}@@`;
  });
  text = text.replace(COMMENT_RE, (_, ws) => ws);
  if (SPLIT_ON_RE.test(text)) return [];
  const other = new Set();
  for (const m of text.matchAll(ARRAY_RE)) other.add(m[1]);
  for (const m of text.matchAll(ASSIGN_SUB_RE)) other.add(m[1]);
  const fed = new Map();
  for (const m of text.matchAll(LITERAL_ASSIGN_RE)) {
    const name = m[1];
    const span = spans[Number(m[2])];
    if (span === undefined || other.has(name) || fed.has(name)) continue;
    const inner = span.slice(1, -1);
    if (!WHITESPACE_RE.test(inner)) continue;
    if (span[0] === '"' && [...inner].some((ch) => NOT_PLAIN.has(ch))) continue;
    const from = m.index + m[0].length;
    let stop = from;
    while (stop < text.length && !SEGMENT_END.has(text[stop])) stop++;
    const rest = text.slice(from, stop).trim();
    if (rest && !rest.split(/\s+/).every((w) => ENV_ASSIGN_RE.test(w))) continue;
    fed.set(name, { name, literal: displayText(span), uses: [], count: 0 });
  }
  if (!fed.size) return [];
  fillBareUses(neutralizeSpans(command), fed);
  return [...fed.values()].filter((f) => f.count).map(({ name, literal, uses }) => ({ name, literal, uses }));
}

const tick = (s) => '`' + s + '`';

// ONE warn line for the literal-fed face (the family's warn shape, as
// renderArgumentWarn's); mergeWarns joins it with the other warn faces.
export function renderLiteralWarn(faces) {
  const parts = faces.map((f) => tick('$' + f.name) + ' ← ' + tick(f.literal) + ' → ' + f.uses.map(tick).join(', '));
  const v0 = faces[0].name;
  const quoted = tick('"$' + v0 + '"');
  const split = tick('${=' + v0 + '}');
  const array = tick(v0 + '=(…)') + ' then ' + tick('"${' + v0 + '[@]}"');
  const systemMessage =
    'zsh word-split guard — WARN (hub Story 161.1.1): ' + parts.join('; ') +
    ' — a quoted literal with whitespace, used unquoted as a bare argument, is ONE word under zsh, never an argument list; ' +
    'say which you meant: ' + quoted + ' (one word), ' + split + ' (split), or an array, ' + array + ' (warn only — the command proceeds)';
  const additionalContext =
    'zsh word-split guard — the ARGUMENT face, LITERAL-fed (WARN tier, hub Story 161.1.1, Hub Sprint 43 leg 4, 2026-10-05; register class ' +
    tick('zsh-word-split') + ', the vituno 50.1.1 seat, hub #3231: ' + tick('ARGS="--src src --override … --narrative …"') + ' then ' +
    tick('node …/gates/check.mjs $ARGS --site <dir>') + ' ran three times, each exiting 2 on ' + tick('gates: unknown argument --src src …') +
    ' while the guard said nothing). ' + parts.join('; ') + '. The Mac harness Bash is zsh: an unquoted scalar expands to exactly ONE word, ' +
    'so a flag string kept in a scalar reaches the command as one argument carrying its spaces. ' +
    'Remedies: (1) one word was meant — quote it, ' + quoted + ', and this warning stops; (2) a list was meant — force the split, ' + split +
    ', or keep an array, ' + array + '. ' +
    'A literal without whitespace (' + tick('b=main; git push origin $b') + ') and a one-word producer (' + tick('sha=$(git rev-parse HEAD); git show $sha') +
    ') are not read — one word is what they hold. KB ' + tick('zsh-harness-shell-no-word-split') + '; register class ' + tick('zsh-word-split') + '.';
  return { systemMessage, hookSpecificOutput: { hookEventName: 'PreToolUse', additionalContext } };
}

// ONE warn line (the family's warn shape — warn-piped-checker.mjs:
// systemMessage for the user, additionalContext for the model; never a
// permissionDecision — the command proceeds).
export function renderArgumentWarn(faces) {
  const parts = faces.map((f) => `\`$${f.name}\` ← \`${f.producer}\` → ${f.uses.map((u) => `\`${u}\``).join(', ')}`);
  const v0 = faces[0].name;
  const systemMessage =
    `zsh word-split guard — WARN (hub Chore 3): ${parts.join('; ')} — a list-shaped \`$(…)\` result used unquoted as a bare argument is ONE word under zsh (newlines and all), never an argument list; ` +
    `say which you meant: \`"$${v0}"\` (one word), \`\${=${v0}}\` (split), or pipe the producer straight in (\`… | xargs\`) (warn only — the command proceeds)`;
  const additionalContext =
    `zsh word-split guard — the ARGUMENT face (WARN tier, hub Chore 3, 2026-09-16; register class \`zsh-word-split\` seats 6 and 8: ` +
    '`pids=$(lsof …); kill $pids` refused `illegal pid: 7904\\n90015` and the server stayed up; `DATA=$(find … | tr \'\\n\' \' \'); cat $DATA` read `No such file or directory` for the whole joined string and every count after it was 0). ' +
    `${parts.join('; ')}. The Mac harness Bash is zsh: an unquoted scalar expands to exactly ONE word, so a substitution that produced LINES ` +
    "(find, ls, lsof, grep -l, git ls-files, gh … list, a `tr '\\n' ' '` join …) reaches the command as one argument carrying the whitespace. " +
    `Remedies: (1) one word was meant — quote it, \`"$${v0}"\`, and this warning stops; (2) a list was meant — force the split, \`\${=${v0}}\`, or capture an array, \`${v0}=($(…))\` then \`"\${${v0}[@]}"\`; ` +
    '(3) skip the scalar — pipe the producer into the consumer, `<producer> | xargs <cmd>` (`-print0 | xargs -0` for paths). ' +
    'A one-word producer (`sha=$(git rev-parse HEAD); git show $sha`) is not read, by decision (Omid, 2026-09-14, Story 99.1.1; this WARN is the first mechanism that decision named); a quoted literal with whitespace (`flags="-a -b"; ls $flags`) draws its own warn since hub Story 161.1.1. ' +
    'KB `zsh-harness-shell-no-word-split`; register class `zsh-word-split`.';
  return { systemMessage, hookSpecificOutput: { hookEventName: 'PreToolUse', additionalContext } };
}

// --- Chore 8 (hub, 2026-09-17): the MODIFIER face (WARN tier; a DENY since Story 241.1.1) ---
// Register class zsh-modifier-face, seats 1 (2026-09-16) and 2 (2026-09-17).
// The thirteen letters are the MEASUREMENT (zsh 5.9, every letter a–z A–Z
// after `$R:` — see the test's `MODIFIER_LETTERS`), never zsh's documentation
// (`x` is documented and does nothing after a parameter). A bare reference is
// `$name`, a positional `$1`, `$@` / `$*`, or a subscripted `$x[…]`; the
// optional subscript is read so `$x[1]:hub` (measured mangled) is caught and
// `${x[1]}:hub` (braced, measured intact) is not — braces never match here.

const MODIFIER_LETTERS = 'aAcehlPqQrstu';
const MODIFIER_RE = new RegExp(`(?<!\\\\)\\$(${NAME}|[0-9]+|[@*])(?:\\[[^\\]\\n]*\\])?:([${MODIFIER_LETTERS}])`, 'g');
const EMULATE_RE = /\bemulate\s+(?:-[LR]+\s+)?(?:sh|bash|ksh)\b/i;
const USE_EDGE_RE = /[\s;|&(){}]/;
const MODIFIER_GLOSS = {
  a: 'the absolute path',
  A: 'the absolute path with symlinks resolved',
  c: "the command's path",
  e: 'the extension',
  h: 'the head (dirname)',
  l: 'lowercase',
  P: 'the realpath',
  q: 'the value quoted',
  Q: 'the value unquoted',
  r: 'the root (the extension stripped)',
  s: 'a substitution `:s/old/new/` — alone it is `bad substitution` and the WHOLE command aborts',
  t: 'the tail (basename)',
  u: 'uppercase',
};

// This face's own neutralizer. Data: a heredoc body whose delimiter is
// QUOTED (`<<'EOF'`, `<<"EOF"`, `<<\EOF` — no expansion), a single-quoted
// span, a comment. LIVE (kept verbatim, because zsh expands there — measured):
// a double-quoted span, an unquoted-delimiter heredoc body, `$(…)` and
// backtick bodies. Spans and comments are read in ONE left-to-right pass so a
// `#` inside a kept double-quoted span is never a comment and a `'` inside a
// comment never opens a span. A heredoc scan resumes AFTER a kept body (its
// text is data to the scan for further `<<`), and stops at the end when a
// body has no terminator — each character read once (the P25 bound). Returns
// null when the command leaves zsh (`emulate sh|bash|ksh`: modifiers off).
function neutralizeForModifiers(command) {
  let text = String(command);
  const heredoc = /<<-?\s*(?:(["'])(\w+)\1|(\\)?(\w+))/g;
  const terms = new Map();
  let m;
  while ((m = heredoc.exec(text))) {
    const quoted = Boolean(m[1] || m[3]);
    const word = m[2] ?? m[4];
    const bodyStart = text.indexOf('\n', m.index + m[0].length);
    if (bodyStart === -1) break;
    if (!terms.has(word)) terms.set(word, new RegExp(`^\\s*${word}\\s*$`, 'gm'));
    const termRe = terms.get(word);
    termRe.lastIndex = bodyStart + 1;
    const term = termRe.exec(text);
    const bodyEnd = term ? term.index + term[0].length : text.length;
    if (quoted) {
      text = text.slice(0, bodyStart) + text.slice(bodyEnd);
      heredoc.lastIndex = bodyStart;
    } else {
      heredoc.lastIndex = bodyEnd;
    }
  }
  text = text.replace(/'[^']*'|"(?:[^"\\]|\\.)*"|(?:^|(?<=\s))#[^\n]*/g, (s) => (s[0] === '"' ? s : ' '));
  if (EMULATE_RE.test(text)) return null;
  return text;
}

const capUse = (s, cap = 80) => (s.length > cap ? `${s.slice(0, cap - 1)}…` : s);

// Every bare-variable modifier use — name, letter, the use as written (the
// run of non-separator characters around the match) — each (name, letter)
// once, in command order. Pure; the tests and the probe import it.
export function findModifierFaces(command) {
  const text = neutralizeForModifiers(command);
  if (text === null) return [];
  const seen = new Set();
  const faces = [];
  for (const m of text.matchAll(MODIFIER_RE)) {
    const name = m[1];
    const letter = m[2];
    const key = `${name}:${letter}`;
    if (seen.has(key)) continue;
    seen.add(key);
    let s = m.index;
    while (s > 0 && !USE_EDGE_RE.test(text[s - 1])) s--;
    let e = m.index + m[0].length;
    while (e < text.length && !USE_EDGE_RE.test(text[e])) e++;
    faces.push({ name, letter, use: capUse(text.slice(s, e)) });
  }
  return faces;
}

// ONE deny (the family's deny shape — as renderEqualsDeny; Story 241.1.1): the
// lead sentence names each use once with its letter and what zsh does with it,
// the rest the measured letters, the contexts, the three remedies, the KB entry
// and the register class.
export function renderModifierDeny(faces) {
  const uses = faces.map((f) => `\`${f.use}\` (\`$${f.name}\` then \`:${f.letter}\` = ${MODIFIER_GLOSS[f.letter]})`);
  const n0 = faces[0].name;
  const permissionDecisionReason =
    `zsh word-split guard — DENIED (Story 241.1.1): ${uses.join('; ')} — under zsh a colon after a BARE variable is a history MODIFIER, inside double quotes too, ` +
    'so the value would reach the command mangled, or the whole call abort on `:s`. ' +
    `The Mac harness Bash is zsh: after a BARE \`$name\` (a positional, \`$@\`, or a subscripted \`$x[1]\` too), a colon followed by one of the thirteen measured letters ` +
    'a A c e h l P q Q r s t u is a history modifier — :a the absolute path, :A the same with symlinks resolved, :c the command path, :e the extension, :h the head (dirname), :l lowercase, ' +
    ':P the realpath, :q quoted, :Q unquoted, :r the root (extension stripped), :s substitute (alone, `bad substitution` — the WHOLE command aborts before anything runs), :t the tail (basename), :u uppercase — ' +
    'and it applies inside double quotes, an unquoted-delimiter heredoc body, `$(…)`, backticks and `[[ ]]` alike; any other character after the colon passes through (`$a:$b`, `$host:8080`, `$x:docs/y`, `$x:main`). ' +
    `Re-issue the command. Remedies: (1) brace the name — \`\${${n0}}:…\`; (2) close the quote before the colon — \`"$${n0}":…\`; ` +
    '(3) `emulate sh` first, which turns modifiers off (measured; `setopt shwordsplit` does not); single quotes when the text is literal. ' +
    '`${name:h}` (braced) is the intentional form and stays silent. ' +
    'Bash on the Windows box has no modifiers — the same line is right there and wrong here. ' +
    'KB `zsh-harness-shell-no-word-split` § The modifier face; register class `zsh-modifier-face` (decision: hook — WARN tier from hub Chore 8, 2026-09-17; ' +
    'five seats met the warn after it landed, and each read it only with the result, so the family\'s clause escalated it to this deny, hub Story 241.1.1).';
  return { hookSpecificOutput: { hookEventName: 'PreToolUse', permissionDecision: 'deny', permissionDecisionReason } };
}

// Story 128.1.4 — the EQUALS face. EQUALS is off under `setopt noequals` /
// `no_equals`, `unsetopt equals`, and outside zsh mode (`emulate sh|bash|ksh`).
const EQUALS_OFF_RE = /\b(?:setopt\s+no_?equals|unsetopt\s+equals|emulate\s+(?:-[LR]+\s+)?(?:sh|bash|ksh))\b/i;
// A word-start `=` followed by at least one word character; `>` and `<` end
// the word (redirections), as the stage separators and quotes do.
const EQUALS_WORD_RE = /(?:^|(?<=[\s;|&()`]))=[^\s;|&()<>`'"]+/g;

// Blank every `open`…`close` span (an unclosed one runs to the end): each
// character is scanned at most twice — the P25 bound.
function blankSpans(text, open, close) {
  let out = '';
  let i = 0;
  for (;;) {
    const s = text.indexOf(open, i);
    if (s === -1) return out + text.slice(i);
    const e = text.indexOf(close, s + open.length);
    out += `${text.slice(i, s)} `;
    if (e === -1) return out;
    i = e + close.length;
  }
}

// This face's own neutralizer. Data: every heredoc body (either delimiter —
// no filename expansion there), single- and double-quoted spans, comments,
// `${…}`, `[[ … ]]`, and `(( … ))` / `$(( … ))`. LIVE: `$(…)` and backticks
// (zsh runs them with EQUALS on). Kept segments are joined once, so many
// terminated heredocs stay linear. Returns null when EQUALS is off.
function neutralizeForEquals(command) {
  const text0 = String(command);
  const heredoc = /<<-?\s*(?:(["'])(\w+)\1|\\?(\w+))/g;
  const terms = new Map();
  const parts = [];
  let keepFrom = 0;
  let m;
  while ((m = heredoc.exec(text0))) {
    const word = m[2] ?? m[3];
    const bodyStart = text0.indexOf('\n', m.index + m[0].length);
    if (bodyStart === -1) break;
    if (!terms.has(word)) terms.set(word, new RegExp(`^\\s*${word}\\s*$`, 'gm'));
    const termRe = terms.get(word);
    termRe.lastIndex = bodyStart + 1;
    const term = termRe.exec(text0);
    const bodyEnd = term ? term.index + term[0].length : text0.length;
    parts.push(text0.slice(keepFrom, bodyStart));
    keepFrom = bodyEnd;
    heredoc.lastIndex = bodyEnd;
  }
  parts.push(text0.slice(keepFrom));
  let text = parts.join('').replace(/'[^']*'|"(?:[^"\\]|\\.)*"|(?:^|(?<=\s))#[^\n]*/g, ' ');
  if (EQUALS_OFF_RE.test(text)) return null;
  text = blankSpans(text, '${', '}');
  text = blankSpans(text, '[[', ']]');
  return blankSpans(text, '((', '))');
}

// Every word-start `=` word — the word as written and the command zsh looks
// up — each word once, in command order. Pure; the tests import it.
export function findEqualsFaces(command) {
  const text = neutralizeForEquals(command);
  if (text === null) return [];
  const seen = new Set();
  const faces = [];
  for (const m of text.matchAll(EQUALS_WORD_RE)) {
    if (seen.has(m[0])) continue;
    seen.add(m[0]);
    faces.push({ word: capUse(m[0]), lookup: capUse(m[0].slice(1)) });
  }
  return faces;
}

// ONE deny (the family's deny shape — as render; Story 151.1.2): the lead
// sentence names each word once, the rest what zsh does with it, the
// remedies, the KB entry and the register class.
export function renderEqualsDeny(faces) {
  const words = faces.map((f) => `\`${f.word}\``).join(', ');
  const uses = faces.map((f) => `\`${f.word}\` (zsh looks up a command named \`${f.lookup}\`)`);
  const permissionDecisionReason =
    `zsh word-split guard — DENIED (Story 151.1.2): ${words} — under zsh a word beginning an unquoted \`=\` is a command-path lookup (the EQUALS option), never text. ` +
    `${uses.join('; ')}. The Mac harness Bash is zsh, and its EQUALS option (on in zsh mode) reads such a word as \`=command\`: ` +
    "it expands to that command's path (`=ls` is `/bin/ls`), or fails `<name> not found` and stops the command line, so nothing after it runs. " +
    `Re-issue the command. Remedies: (1) quote the word — \`'${faces[0].word}'\`; (2) drop the separator, or start it with another character — \`---\`, \`###\`; ` +
    '(3) `setopt noequals` first when the text must stay bare. ' +
    "An `=` inside a word (`a=b`, `--flag=value`), a lone `=` (a test's equality), a quoted or escaped `=`, a heredoc body, `[[ … == … ]]`, `(( … ))` and `${…}` are untouched; " +
    'single brackets are not — `[ "$a" == b ]` looks up a command named `=`, so use `=` or `[[ ]]`. ' +
    'Bash on the Windows box has no EQUALS — the same line is right there and wrong here. ' +
    'KB `zsh-harness-shell-no-word-split` (its third default, a word beginning `=`); register class `zsh-equals-face` (decision: hook — ' +
    'WARN tier from hub Story 128.1.4; seat 7 met the warn and zsh still stopped the line, since a warn reaches the model only with the result, so the ' +
    "family's clause escalated it to this deny, hub Story 151.1.2).";
  return { hookSpecificOutput: { hookEventName: 'PreToolUse', permissionDecision: 'deny', permissionDecisionReason } };
}

// Two denies on one command render as ONE deny: the reasons joined, each
// face's own lead first — one shape, read once (Story 151.1.2).
export function mergeDenies(denies) {
  if (denies.length === 1) return denies[0];
  return {
    hookSpecificOutput: {
      hookEventName: 'PreToolUse',
      permissionDecision: 'deny',
      permissionDecisionReason: denies.map((d) => d.hookSpecificOutput.permissionDecisionReason).join('\n\n'),
    },
  };
}

// Two warn faces on one command render as ONE warn object: the lines joined
// on ` | `, the contexts stacked — one shape, read once, never two JSON
// objects on stdout.
export function mergeWarns(warns) {
  if (warns.length === 1) return warns[0];
  return {
    systemMessage: warns.map((w) => w.systemMessage).join(' | '),
    hookSpecificOutput: { hookEventName: 'PreToolUse', additionalContext: warns.map((w) => w.hookSpecificOutput.additionalContext).join('\n\n') },
  };
}

// ONE deny: the reason's LEAD sentence names each offending variable ONCE
// through the first head that carries it (the line a reader sees first);
// the rest names every head, what each form does with the one word, the
// three remedies, the KB entry, and the register class. The family's shape
// (block-commit-on-main.mjs): hookSpecificOutput alone, its three keys.
export function render(found) {
  const covered = new Set();
  const firstHeads = [];
  for (const f of found) {
    if (f.vars.some((v) => !covered.has(v))) firstHeads.push(`\`${f.head}\``);
    for (const v of f.vars) covered.add(v);
  }
  const vars = [...covered];
  const heads = [...new Set(found.map((f) => `\`${f.head}\``))];
  const hasFor = found.some((f) => f.form === 'for');
  const hasSet = found.some((f) => f.form === 'set');
  const one = vars.length === 1;
  const v0 = vars[0];
  const permissionDecisionReason =
    `zsh word-split guard — DENIED (Story 99.1.1): ${firstHeads.join(', ')} — an unquoted variable is ONE word under zsh, never a list. ` +
    `This command would run ${heads.join(' and ')} on a zsh harness shell, and zsh ` +
    `does NOT word-split an unquoted variable — ${vars.map((v) => `\`$${v}\``).join(', ')} expand${one ? 's' : ''} to exactly ONE ` +
    `word, whatever whitespace ${one ? 'it holds' : 'they hold'}. ` +
    (hasFor ? 'The `for` loop runs ONCE with the whole string as its item. ' : '') +
    (hasSet ? '`set --` hands `$1` the whole string and leaves `$2`, `$3`… EMPTY. ' : '') +
    (hasFor ? 'Only a variable that is a WHOLE item of the list is read here (`$VAR`, `${VAR}`, or a whole-variable part of a brace list `{$A,$B}`; a continued line is read with its list): a word built around one (`$D/a.log`) is one word under zsh, the harness shell, and passes — bash would split it, so the same line under bash is a different loop (hub Stories 244.1.33, 247.1.6). ' : '') +
    "This class's seats read nothing for their whole budget — usually silently, under `2>/dev/null` or a " +
    'watcher that kept polling; one ran with the warning already beside it, which is why this is a deny. ' +
    `Re-issue the command in a splitting form. Remedies: (1) force the split — \`\${=${v0}}\`; (2) an array — \`${v0}=(a b c)\` then ` +
    `\`for x in $${v0}\` or \`"\${${v0}[@]}"\`; (3) explicit per-item commands, or \`while read -r a b c; do …; done <<'REC'\` ` +
    'for records. A glob, a command substitution `$(…)`, and `"$@"` split or expand as you expect; a double-quoted `"$var"` ' +
    "is one item by design. Read the FIRST iteration's output before trusting the loop. KB `zsh-harness-shell-no-word-split`; " +
    'register class `zsh-word-split` (its seats from 2026-09-01; the ≥2× decision `hook` — hub Story 90.1.1, warn-level; ' +
    'escalated to this deny by hub Story 99.1.1 after seat 7 landed inside the predicate).';
  return { hookSpecificOutput: { hookEventName: 'PreToolUse', permissionDecision: 'deny', permissionDecisionReason } };
}

// The zsh predicate behind the env seam: WARN_ZSH_WORD_SPLIT_SHELL=zsh|other
// wins; else darwin (the Mac's harness Bash tool is zsh — CLAUDE.md) or a
// $SHELL ending in zsh; the Windows box's Git Bash is `other` and splits.
export function harnessShell(env = process.env, platform = process.platform) {
  const seam = env.WARN_ZSH_WORD_SPLIT_SHELL;
  if (seam) return seam === 'zsh' ? 'zsh' : 'other';
  return platform === 'darwin' || /zsh$/.test(env.SHELL ?? '') ? 'zsh' : 'other';
}

async function main() {
  if (process.stdin.isTTY) {
    console.error('warn-zsh-word-split.mjs expects a PreToolUse JSON payload on stdin.');
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
    console.error(`warn-zsh-word-split: unreadable PreToolUse payload${readWhy ? ` (${readWhy})` : ''} — guard stands down (fail-open: a deny that cannot read never blocks)`);
    process.exit(0);
  }
  if (payload.tool_name !== 'Bash') process.exit(0);
  if (harnessShell() !== 'zsh') process.exit(0);
  const command = payload.tool_input?.command ?? '';
  // The DENY faces: the loop face (Story 99.1.1), the EQUALS face (Story
  // 128.1.4, a deny since Story 151.1.2) and the MODIFIER face (Chore 8, a
  // deny since Story 241.1.1) — any of them on one command → one deny.
  const denies = [];
  const found = findWordSplits(command);
  if (found.length) denies.push(render(found));
  const eqs = findEqualsFaces(command);
  if (eqs.length) denies.push(renderEqualsDeny(eqs));
  const mods = findModifierFaces(command);
  if (mods.length) denies.push(renderModifierDeny(mods));
  if (denies.length) {
    console.log(JSON.stringify(mergeDenies(denies)));
    return;
  }
  // Chore 3: the ARGUMENT face (substitution-fed; literal-fed since Story
  // 161.1.1), WARN tier — only when no deny stands; both on one command → one
  // warn object.
  const warns = [];
  const faces = findArgumentFaces(command);
  if (faces.length) warns.push(renderArgumentWarn(faces));
  const literals = findLiteralFaces(command);
  if (literals.length) warns.push(renderLiteralWarn(literals));
  if (warns.length) console.log(JSON.stringify(mergeWarns(warns)));
}

// Story 92.1.3: real paths on both sides — a symlinked directory or file in
// argv[1] resolves to the module Node already realpath'd; a path that does
// not exist falls back to path.resolve (never a throw at load).
const realArgv1 = (p) => {
  try {
    return realpathSync(p);
  } catch {
    return path.resolve(p);
  }
};
const isMain = Boolean(process.argv[1]) && realArgv1(process.argv[1]) === fileURLToPath(import.meta.url);
if (isMain) main();

// Story 128.1.8 (register class stdin-readfilesync-eagain-on-nonblocking-pipe):
// stdin through the async iterator, never readFileSync(0) — a 1.1 MB payload on a
// non-blocking pipe read as unreadable on Linux CI (Node 24, hub #3034's first run).
async function readStdin() {
  const chunks = [];
  for await (const chunk of process.stdin) chunks.push(chunk);
  return Buffer.concat(chunks).toString('utf8');
}
