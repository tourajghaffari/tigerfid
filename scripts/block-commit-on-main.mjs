#!/usr/bin/env node
// block-commit-on-main.mjs — PreToolUse hook: deny `git commit` while on main.
// CANONICAL COPY lives in the hub (scripts/block-commit-on-main.mjs) and is
// synced verbatim into each repo under projects/ by scripts/sync-session-wrap.mjs
// — do NOT hand-edit a synced copy; edit the hub canonical and re-run the sync.
//
// Wired directly from the hub's .claude/settings.json (PreToolUse, matcher
// "Bash" — "Bash|PowerShell" once Story 73.1.2 lands, exec form:
// "command":"node" + args — so no shell is involved on any platform).
// Receives the PreToolUse JSON payload on stdin; if the shell-tool command
// (Bash, or the PowerShell tool — Story 73.1.1) contains a git commit
// invocation and the repo the commit TARGETS (`git -C <path>`, else the last
// `cd <path>` before the commit, else the session cwd) has main checked out,
// emits a permissionDecision "deny" (see docs/kb/hook-mechanics.md in the
// hub). The GitHub protect-main ruleset only rejects the *push* — this stops
// the commit.
//
// THE POWERSHELL TOOL (Story 73.1.1): the Windows box's primary shell is the
// harness's PowerShell tool, a different tool name — a commit on main issued
// from it was never intercepted (KB hook-mechanics had said so since
// 2026-06-14). Same payload shape (measured 2026-09-04), so the target
// resolution reads the shell's cd verbs too (Set-Location / sl / chdir /
// pushd / Push-Location beside cd, compared case-insensitively;
// `-Path`/`-LiteralPath` skipped as flags), here-strings (`@'…'@` /
// `@"…"@` — the tool's own recommended commit-message form) are dropped as
// data, and a double-quoted span's backtick is an escape rather than
// substitution (`$(` stays live). Accepted limits: the segment splitter
// still cuts on backticks (a backtick line continuation splits a segment —
// errs toward blocking, never silence); `-Path:<value>` colon-joined
// arguments and the call operator `& "…"` are not parsed; a here-string body
// line that merely looks like the terminator ends it early.
//
// MATCHER CONTRACT (Story 2.1.7, 2026-07-18). The guard matches command
// STRUCTURE, not raw text: heredoc bodies are dropped and quoted spans are
// neutralized, the line is split into segments (on && || ; | & newline,
// parentheses, braces, backticks), and a segment counts as a commit only when
// its command-position word — after VAR=x assignments and wrapper words
// (command/exec/env/nohup/time/nice/ionice/xargs/sudo/builtin) — is `git` and
// the first token after git's global flags (-C/-c and friends consume their
// argument) is the subcommand `commit`.
//   Blocked while the target repo is on main: the bare form; any &&/||/;/|/
//   newline-chained or non-leading position; `git -C <path> commit`;
//   prefix forms (VAR=x git commit, sudo git commit); command substitution
//   ($(git commit …) or `git commit`), which the shell does execute.
//   Ignored (no commit invocation — these false-blocked before 2.1.7): prose
//   inside quotes or heredoc bodies; "commit" appearing inside a filename,
//   branch name, or path argument (git show x:docs/block-commit-gap.md,
//   git checkout -b fix/commit-lint-rule); git commands whose subcommand is
//   not commit.
//   Target-branch resolution (Story 24.3.22): the target's LIVE branch wins,
//   but only when the target path EXISTS — a nonexistent path is never
//   walked (the .git walk-up would climb past it into the cwd's repo and
//   read the wrong branch). A not-yet-created worktree the SAME command
//   creates via `git worktree add <path> -b <branch>` (either argument
//   order) is judged by that declared branch. Only when neither yields a
//   branch does the session cwd's branch govern — and a deny decided by
//   that cwd FALLBACK says so in its message, naming the literal-absolute-
//   path and split-into-separate-tool-calls workarounds.
//
// Site-repo adoption: the sync roster (CANONICALS in scripts/sync-session-wrap.mjs)
// writes this file into each repo's scripts/; the repo's own .claude/settings.json
// carries the matching PreToolUse entry (repo-owned wiring). Hooks do NOT inherit
// from ancestor directories (see docs/kb/claude-md-scope.md), so each repo needs
// its own copy + wiring — this deliberate duplication was chosen over a
// plugin/marketplace, which was over-engineered for a handful of repos (see
// project-plan story 1.1.7).
//
// Escape hatch: a deliberate sandbox/pilot session that must commit on a
// throwaway clone's main can launch with FGL_ALLOW_COMMIT_ON_MAIN=1 to opt out
// for its duration (session-scoped, so it can't silently disable the guard for
// later work). The deny message names it, so a future pilot that hits the block
// learns the opt-out instead of parking the hub on a side branch. The target-repo
// resolution below (git -C / cd) shipped in hub PR #174, so the guard already
// judges where the commit ACTS, not where the session sits.
//
// Known limits (accepted): a script that commits internally bypasses Bash
// filtering (the hub's wrap-session.mjs carries its own guards for that
// reason); since 2.1.7 that class also covers a commit inside a quoted script
// argument (`bash -c "git commit …"`) — quoted text is data to this matcher;
// a compound command that switches BRANCHES before committing (git
// checkout/switch mid-command) is judged by the branch checked out *now* —
// still accepted (honoring the compound's declared branch was adjudicated
// UNSAFE: the segment splitter treats && and ; alike, so `git checkout -b x;
// git commit` commits on main when the checkout fails), but since Story
// 24.3.27 a deny on the branch-CREATING shape (`checkout -b/-B` /
// `switch -c/-C` earlier in the compound; four live hits 2026-08-06→07)
// names the split-calls remedy in its message — message-only, verdicts
// byte-unchanged;
// backslash-newline line-continuation and variable indirection/reassembly
// (`c=commit && git $c`) still slip through (Story 8.5.1 — a matcher cannot
// parse shell); double-quoted text containing `$(` or a backtick is treated as
// live substitution (the shell interpolates it), so prose quoting those exact
// characters can still false-block — errs toward blocking; target parsing
// covers `git -C` and plain/quoted `cd` paths only — subshells or SHELL
// VARIABLES ($WT and friends) cannot be expanded by a matcher (the 8.5.1
// class), so a variable-valued target falls back to the session cwd's
// branch and can false-block a legitimate cross-repo commit (Story
// 24.3.22, three live occurrences): ACCEPTED — the deny message names the
// cwd fallback and both workarounds, so a false-blocked session
// self-serves. The sibling pre-compound shape (a literal `-C` path CREATED
// by an earlier `worktree add -b <branch>` stage of the same compound) is
// CLOSED: the declared branch is judged instead. Backstops for every
// under-block class: the GitHub protect-main ruleset rejects the push, and
// wrap-session.mjs carries its own on-main guards.
// TOCTOU (2026-07-17, expeal.com F9): the guard judges HEAD when the hook
// RUNS; a concurrent session sharing the checkout can move HEAD to main
// before the approved command executes. No PreToolUse hook can close that
// race — parallel sessions take worktrees (KB parallel-sessions-one-checkout).
// Malformed stdin (Story 8.5.2, decided 2026-07-12): an unreadable payload —
// unparseable JSON, or a parsed value that is not an object — makes the guard
// STAND DOWN with one stderr note + exit 0 (fail-open by design: with no
// payload it cannot know a commit is being attempted, and the protect-main
// ruleset still rejects a push to main server-side).
//
// Test manually:
//   echo '{"tool_name":"Bash","cwd":".","tool_input":{"command":"git commit"}}' \
//     | node scripts/block-commit-on-main.mjs
//   The PowerShell tool (a cmdlet cd names the target; Story 73.1.1):
//   echo '{"tool_name":"PowerShell","cwd":".","tool_input":{"command":"Set-Location projects/vituno; git commit -m x"}}' \
//     | node scripts/block-commit-on-main.mjs
// Suite: test/block-commit-on-main.test.mjs — HUB-resident (run node --test
//   there); synced site copies carry no suite of their own, the hub CI covers
//   the canonical.

import { readFileSync, statSync } from 'node:fs';
import { join, dirname, isAbsolute } from 'node:path';

if (process.stdin.isTTY) {
  console.error('block-commit-on-main.mjs expects a PreToolUse JSON payload on stdin.');
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
  // Documented disposition (Story 8.5.2, Omid 2026-07-12): fail OPEN, loudly.
  // With no readable payload the guard cannot tell whether a commit is even
  // being attempted; denying every Bash command would over-block (see the
  // 2026-07-05 false-deny precedent in the header). The GitHub protect-main
  // ruleset still rejects a push to main server-side.
  console.error(
    `block-commit-on-main: unreadable PreToolUse payload${readWhy ? ` (${readWhy})` : ''} — guard stands down (fail-open; the protect-main ruleset still guards the push)`
  );
  process.exit(0);
}

// Story 73.1.1: the harness's shell tools — Bash on both machines, PowerShell
// on the Windows box (its primary shell). Until this story a commit issued
// from the PowerShell tool was never intercepted (KB hook-mechanics).
const SHELL_TOOLS = new Set(['Bash', 'PowerShell']);
if (!SHELL_TOOLS.has(payload.tool_name)) process.exit(0);
const isPowerShell = payload.tool_name === 'PowerShell';
const command = payload.tool_input?.command ?? '';

const inv = commitInvocation(command);
if (!inv) process.exit(0);

// Judge the COMMIT'S TARGET repo, not the session cwd: hub-rooted agent
// threads legitimately commit into projects/* feature branches while the
// session cwd (the hub) sits on main (2026-07-05 fleet sweep: all 7 agents
// false-denied). Target = `git -C <path>` if present, else the last `cd
// <path>` before the commit segment, else the session cwd. If the parsed
// target yields no branch (nonexistent path, detached HEAD), fall back to
// the session cwd's branch — the guard keeps erring toward blocking.
// A readable payload can still carry a garbage cwd — a non-string would crash
// path.join (Story 8.5.2 review fix). Fall back to the hook's own cwd so the
// guard still judges instead of standing down.
const cwd = typeof payload.cwd === 'string' && payload.cwd ? payload.cwd : process.cwd();
// MSYS/Git-Bash drive path (`/c/Users/…`, Story 26.1.7): absolute by its
// leading slash on win32 but stat-ing as `<current-drive>:\c\…` — a path that
// names nothing — so the guard used to fall through to the session-cwd
// fallback: a false BLOCK from a main cwd, a false ALLOW from a branch cwd.
// Normalize to `C:/Users/…` before resolving. win32-only: on POSIX `/c/…` is
// a legitimate filesystem path and must stay untouched.
const msysDrive = (p) => {
  if (process.platform !== 'win32' || !p) return p;
  const m = /^\/([A-Za-z])(\/.*)?$/.exec(p);
  return m ? `${m[1].toUpperCase()}:${m[2] ?? '/'}` : p;
};
const targetRaw = inv.target ? msysDrive(inv.target) : null;
const target = targetRaw ? (isAbsolute(targetRaw) ? targetRaw : join(cwd, targetRaw)) : null;
// Resolution order (Story 24.3.22): the target's LIVE branch wins; a branch
// the same compound DECLARES for a not-yet-created worktree (`worktree add
// <path> -b <branch>`) fills the void where live state yields nothing; only
// then the session cwd's branch — the fallback that can mis-attribute a
// cross-repo commit, so when it decides a deny, the message says so.
// A NONEXISTENT target is never walked: gitBranch's .git walk-up would climb
// past the bogus path into whatever repo contains the cwd (the $VAR shape:
// join(cwd, '$WT') under an on-main checkout read as 'main'), attributing a
// branch to a path that names nothing. Existing paths keep the walk (a
// subdir of a repo must still resolve to that repo's branch).
const targetExists = target ? Boolean(statSync(target, { throwIfNoEntry: false })) : false;
const targetBranch = targetExists ? gitBranch(target) : '';
const branch = targetBranch || inv.declared || gitBranch(cwd);
const usedCwdFallback = Boolean(inv.target) && !targetBranch && !inv.declared;
if (branch !== 'main') process.exit(0);

// Deliberate, session-scoped escape hatch: a sandbox/pilot that intentionally
// commits on main (e.g. the Initiative-10 concurrency pilot writing to a
// throwaway clone's main) opts out for the session by launching with
// FGL_ALLOW_COMMIT_ON_MAIN=1. It evaporates when the session ends, so it can't
// silently disable the guard for later work — unlike parking the hub on a side
// branch, which also confuses follow-up `git status` reads (see the KB).
if (process.env.FGL_ALLOW_COMMIT_ON_MAIN === '1') process.exit(0);

console.log(
  JSON.stringify({
    hookSpecificOutput: {
      hookEventName: 'PreToolUse',
      permissionDecision: 'deny',
      permissionDecisionReason:
        'Hub guardrail: never commit on main (CLAUDE.md "Git workflow"). ' +
        'Create a branch and open a PR instead. For session wrap-up, ' +
        'wrap-session.mjs handles the branching itself. For a deliberate ' +
        'sandbox/pilot session that must commit on main, relaunch with ' +
        'FGL_ALLOW_COMMIT_ON_MAIN=1.' +
        (usedCwdFallback
          ? ' NOTE (Story 24.3.22): the commit\'s -C/cd target could not be ' +
            'resolved to a branch (shell-variable or not-yet-created path), ' +
            'so the judged branch is the SESSION CWD\'s — a cwd FALLBACK, ' +
            'not the target\'s own state. If the target really is on a ' +
            'feature branch, use a literal absolute path in -C/cd, or split ' +
            'setup and commit into separate tool calls.'
          : '') +
        (inv.branchCreate
          ? ' NOTE (Story 24.3.27): this compound creates its branch ' +
            'mid-command (checkout -b / switch -c), but the guard judges the ' +
            'branch checked out NOW — a mid-compound switch cannot be ' +
            'honored (the segment splitter treats && and ; alike, and a ' +
            ';-chained commit still runs on main when the checkout fails). ' +
            'Run the branch creation as its own tool call first, then ' +
            'commit — the split form passes.'
          : ''),
    },
  })
);

// --- matcher ---------------------------------------------------------------
// (The Sets live inside commitInvocation: this module CALLS the hoisted
// functions above these definitions, so module-level consts here would still
// be in their temporal dead zone at call time.)

// Neutralize text that is data, not command structure. Heredoc bodies are
// removed outright (they are stdin data — never executed by the outer shell;
// a body line that merely LOOKS like the terminator ends the body early, which
// errs toward blocking). Quoted spans become indexed \x00N\x00 placeholders
// whose original contents are kept, so quoted cd/-C path arguments survive
// target resolution while quoted prose can never look like an invocation.
// Exception: a double-quoted span containing `$(` or a backtick is left in
// place with its quotes stripped — the shell interpolates those, so they are
// live structure, not data.
function neutralize(command) {
  let text = command;
  // PowerShell here-strings (Story 73.1.1): `@'` … `'@` and `@"` … `"@`, the
  // terminator at line start, are argument DATA (the PowerShell tool's own
  // recommended commit-message form) — dropped whole before the quote pass,
  // which would otherwise split a body at its first apostrophe. A body line
  // that merely LOOKS like the terminator ends the here-string early (errs
  // toward blocking, never silence).
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

// Scan the neutralized command for a git invocation whose subcommand is
// commit. Returns null when there is none, else { target, declared,
// branchCreate } — target is the -C path of the commit's own segment, falling
// back to the last cd path seen before it, else null (caller judges the
// session cwd); declared is the branch an EARLIER `git worktree add <path> -b
// <branch>` stage of the same command declared for that exact target path
// (Story 24.3.22 — the pre-compound shape: the path does not exist at scan
// time, but the compound itself says which branch it will hold), else null;
// branchCreate is true when an earlier segment creates-and-switches to a
// branch (`checkout -b/-B` / `switch -c/-C` — Story 24.3.27, message-only:
// it labels the deny, never decides it). First commit invocation wins,
// matching the pre-2.1.7 behavior.
function commitInvocation(command) {
  // Wrapper words that may precede the real command in its segment.
  const WRAPPERS = new Set([
    'command', 'exec', 'env', 'nohup', 'time', 'nice', 'ionice', 'xargs', 'sudo', 'builtin',
  ]);
  // Shell keywords that may lead a segment before the real command (Story
  // 97.1.3): the splitter cuts at `;`, so a commit inside `for … do … done`,
  // after `if` / `while` / `until` / `then` / `else` / `elif`, or negated
  // with `!` begins its segment with the keyword — neither a wrapper nor a
  // `VAR=` prefix, so the skip stopped there and the fleet's own loop idiom
  // was a bypass (five shapes measured NOT BLOCKED, 2026-09-13). Skipped
  // beside WRAPPERS here and in warn-unmarked-session.mjs's landingVerb and
  // warn-cwd-drift.mjs's cd finders — single-file canonicals, the set is
  // copied, never imported. `{`, `(` and `)` already split segments;
  // `time` is a wrapper.
  const KEYWORDS = new Set(['do', 'then', 'else', 'elif', 'if', 'while', 'until', '!']);
  // git global options that consume a SEPARATE following argument (the
  // =-joined forms like --git-dir=x parse as plain flags, skipped anyway).
  const GIT_ARG_FLAGS = new Set([
    '-C', '-c', '--config-env', '--git-dir', '--work-tree', '--namespace', '--super-prefix', '--exec-path',
  ]);
  // The shell-moving verb whose argument becomes the commit's target when
  // no -C names one: `cd` for Bash (unchanged); the PowerShell tool (Story
  // 73.1.1) adds the cmdlets and their aliases, compared case-insensitively.
  // `-Path` / `-LiteralPath` fall to the leading-dash skip below.
  const CD_VERBS = new Set(isPowerShell
    ? ['cd', 'chdir', 'sl', 'set-location', 'pushd', 'push-location']
    : ['cd']);
  const isCdVerb = (t) => CD_VERBS.has(isPowerShell ? t.toLowerCase() : t);
  const { text, values } = neutralize(command);
  const resolve = (t) => t.replace(/\x00(\d+)\x00/g, (_, n) => values[Number(n)] ?? '');
  let lastCd = null;
  // Story 24.3.27 (message-only): true when a segment BEFORE the commit
  // creates-and-switches to a branch (`checkout -b/-B`, `switch -c/-C`).
  // Marks the accepted compound-checkout class so the deny can name the
  // split-calls remedy; never consulted for the verdict itself.
  let sawBranchCreate = false;
  // Story 24.3.22: worktree paths this command CREATES, keyed by their
  // resolved path string, valued with the `-b`/`-B` branch the same segment
  // declares (null when the add names no branch — the cwd fallback then
  // governs as before).
  const wtDeclared = new Map();
  for (const seg of text.split(/[;|&\n(){}`]+/)) {
    const tokens = seg.trim().split(/\s+/).filter(Boolean);
    if (!tokens.length) continue;
    let i = 0;
    while (
      i < tokens.length &&
      (KEYWORDS.has(tokens[i]) || /^[A-Za-z_][A-Za-z0-9_]*=/.test(tokens[i]) ||
        WRAPPERS.has(tokens[i]) || tokens[i].startsWith('-') || tokens[i] === '$')
    ) i++;
    if (i >= tokens.length) continue;
    // The cd verb is read AFTER the skip (Story 97.1.3): `do cd <path> &&
    // git commit` names the commit's target the way a bare cd does.
    if (isCdVerb(tokens[i])) {
      const arg = tokens.slice(i + 1).find((t) => !t.startsWith('-'));
      lastCd = arg ? resolve(arg) : null;
      continue;
    }
    if (tokens[i] !== 'git') continue;
    i++;
    let dashC = null;
    while (i < tokens.length) {
      const t = tokens[i];
      if (GIT_ARG_FLAGS.has(t)) {
        if (t === '-C') dashC = resolve(tokens[i + 1] ?? '');
        i += 2;
        continue;
      }
      // Attached form `-C<path>` — git accepts both shapes (Story 26.1.7).
      // Uppercase-C only: attached `-c<name>=<val>` is a config flag.
      if (t.startsWith('-C') && t.length > 2) {
        dashC = resolve(t.slice(2));
        i++;
        continue;
      }
      if (t.startsWith('-')) { i++; continue; }
      if (resolve(t) === 'commit') {
        const target = dashC || lastCd || null;
        return {
          target,
          declared: (target && wtDeclared.get(target)) || null,
          branchCreate: sawBranchCreate,
        };
      }
      if (resolve(t) === 'worktree' && resolve(tokens[i + 1] ?? '') === 'add') {
        // `git worktree add [<flags>] <path> [<commit-ish>]`, with `-b
        // <branch>` legal before OR after the path. Record path → branch.
        let wtPath = null;
        let wtBranch = null;
        for (let j = i + 2; j < tokens.length; j++) {
          const a = tokens[j];
          if (a === '-b' || a === '-B') { wtBranch = resolve(tokens[j + 1] ?? '') || null; j++; continue; }
          if (a.startsWith('-')) continue;
          if (wtPath === null) wtPath = resolve(a);
          // later non-flag tokens are the <commit-ish> — ignored
        }
        if (wtPath) wtDeclared.set(wtPath, wtBranch);
      }
      if (resolve(t) === 'checkout' || resolve(t) === 'switch') {
        const createFlags = resolve(t) === 'checkout' ? ['-b', '-B'] : ['-c', '-C'];
        if (tokens.slice(i + 1).some((a) => createFlags.includes(a))) sawBranchCreate = true;
      }
      break; // a git invocation with some other subcommand — not a commit
    }
  }
  return null;
}

// Same .git/HEAD walk as the hub's scripts/statusline.mjs — a hub-resident
// sibling, not synced to site repos — (including the worktree
// "gitdir:" pointer case), EXCEPT on detached HEAD: statusline shows the short
// SHA there, while this returns '' — "no branch", so the caller falls back to
// the session cwd's branch (see above). The copies deliberately diverge on
// that case; don't "unify" them (relevant to Story 8.2.1's tests).
function gitBranch(dir) {
  try {
    for (let d = dir; ; d = dirname(d)) {
      const dotGit = join(d, '.git');
      const st = statSync(dotGit, { throwIfNoEntry: false });
      if (st) {
        let gitDir = dotGit;
        if (st.isFile()) {
          const m = readFileSync(dotGit, 'utf8').match(/^gitdir:\s*(.+?)\s*$/m);
          if (!m) return '';
          gitDir = isAbsolute(m[1]) ? m[1] : join(d, m[1]);
        }
        const head = readFileSync(join(gitDir, 'HEAD'), 'utf8').trim();
        const ref = head.match(/^ref: refs\/heads\/(.+)$/);
        return ref ? ref[1] : '';
      }
      if (dirname(d) === d) return '';
    }
  } catch {
    return '';
  }
}

// Story 128.1.8 (register class stdin-readfilesync-eagain-on-nonblocking-pipe):
// stdin through the async iterator, never readFileSync(0) — a 1.1 MB payload on a
// non-blocking pipe read as unreadable on Linux CI (Node 24, hub #3034's first run).
async function readStdin() {
  const chunks = [];
  for await (const chunk of process.stdin) chunks.push(chunk);
  return Buffer.concat(chunks).toString('utf8');
}
