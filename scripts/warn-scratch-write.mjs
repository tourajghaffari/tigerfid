#!/usr/bin/env node
// warn-scratch-write.mjs — PreToolUse hook: WARN (never block) when a shell-tool
// command (Bash, or the PowerShell tool) writes a file OUTSIDE both the hub tree
// and the session scratchpad area (hub Story 128.1.1, Initiative 128; register
// class scratch-write-lands-outside-the-scratchpad).
//
// Why this exists: the hub CLAUDE.md § Standing feedback boundary covers WRITES —
// scratch copies, backups and worktrees go to the session scratchpad, never beside
// the hub — and the rule kept being met by mechanism, not intent. Seat 2 (a
// relative `git -C <clone> worktree add` path resolved against the clone, landing
// in Documents/AppData/…), seat 3 (`curl … -o "$A/../../../scratch-dummy"` with $A
// the expeal-app root — above the hub), seat 4 (`> ../../../pp-scratch.md` from
// projects/pantheon — above the hub) and seat 5 (`> "$H/../round-dry.txt"` with H
// the hub root, the hub round's dry run, 2026-10-01): each session held the rule
// and miscounted a `..`. The class was decided `hook` (2026-09-30); this is it.
//
// THE ROOTS a write may land in:
//   - the hub tree: the ancestor of CLAUDE_PROJECT_DIR (else the payload cwd)
//     that holds `projects/`, or that directory itself when none does;
//   - the session scratchpad area: anything under `<os temp>/claude/` or
//     `<os temp>/claude-<digits>/` (WARN_SCRATCH_WRITE_TMPDIR pins the temp dir
//     in tests; on a posix host `/tmp` and `/private/tmp` are temp dirs too).
//     The digits form is the Mac harness's `/private/tmp/claude-<uid>/…`
//     scratchpad (Story 128.1.9: before it, every correct scratch write on the
//     Mac warned);
//   - `/dev/null`, `/dev/stdout`, `/dev/stderr`, `/dev/fd/*` and `NUL`, always.
//
// THE OUTPUT TARGETS read, per stage: a redirection `>`, `>>`, `2>`, `*>`
// (`2>&1`-style duplications are not files); `tee [-a] <path…>`; curl's `-o` /
// `--output` / `--output=` (a short cluster ending in o); wget's `-O` /
// `--output-document[=]`; any command's generic `--out <path>` / `--out=<path>`;
// `git [-C <dir>…] worktree add [-b|-B <branch>] <path>`, the path resolved
// against -C's directory (seat 2); and the PowerShell cmdlets Out-File,
// Set-Content, Add-Content and Tee-Object (`-FilePath` / `-Path` /
// `-LiteralPath`, or the first positional argument).
//
// RESOLUTION: `$NAME` / `${NAME}` expand from `NAME=value` assignments EARLIER
// ON THE SAME LINE (seat 3's mechanism; `export` too); a variable the line never
// assigns stands the target down silently — except `$TMPDIR` on the Windows box
// with TMPDIR unset, which expands empty (hub CLAUDE.md: `"$TMPDIR/x"` is `/x`).
// `~` is the home directory. A `cd` / `pushd` (Set-Location, sl, chdir,
// Push-Location for the PowerShell tool) earlier on the line moves the base a
// relative target resolves against; the base starts at the payload cwd. On
// win32 (WARN_SCRATCH_WRITE_PLATFORM pins it in tests): Git Bash's `/c/…` is
// `C:/…`, any other root-absolute Bash path lands in Git's install dir
// (`/tmp/x` is C:/Program Files/Git/tmp/x — flagged), and paths compare
// case-insensitively.
//
// MATCHER CONTRACT (the warn-guard family's, warn-cwd-drift.mjs's copy):
// heredoc bodies and PowerShell here-strings are dropped; quoted spans become
// placeholders (their contents resolve for the target) unless a double-quoted
// span holds live substitution — `$(` in both shells, a backtick in Bash only;
// the line splits into stages on && || ; & | newline, parens, braces, backticks
// after `2>&1`-style duplications are removed. Linear: no nested quantifier.
//
// Known limits (accepted, warn-level): a write inside `bash -c "…"`, a here-doc
// a program writes through its own API, and a path a program builds itself are
// invisible; a PowerShell variable (`$env:TEMP`, `$x = …`) is never followed;
// `cd -` and an unresolvable cd target stand relative targets down.
//
// WARN-level BY DESIGN: a write outside the roots can be deliberate (an
// install, a real output), so the hook emits NO permissionDecision —
// `additionalContext` (reaches the model) plus a `systemMessage` (reaches the
// user). Malformed stdin: stand down loudly (stderr note, exit 0).
//
// Wired from the hub's .claude/settings.json (PreToolUse, matcher
// "Bash|PowerShell", exec form). The fleet roster entry and every repo's wiring
// are hub Epic 128.2's (one round, one sweep for all of Initiative 128's hooks).
//
// Test manually (from the hub; the first warns, the second is silent):
//   echo '{"tool_name":"Bash","cwd":"'"$PWD"'","tool_input":{"command":"curl --version -o ../x.txt"}}' \
//     | CLAUDE_PROJECT_DIR="$PWD" node scripts/warn-scratch-write.mjs
//   echo '{"tool_name":"Bash","cwd":"'"$PWD"'","tool_input":{"command":"npm test > docs/out.txt"}}' \
//     | CLAUDE_PROJECT_DIR="$PWD" node scripts/warn-scratch-write.mjs
// Suite: test/warn-scratch-write.test.mjs (node --test).

import { existsSync, realpathSync } from 'node:fs';
import os from 'node:os';
import path from 'node:path';

const WRAPPERS = new Set(['command', 'builtin', 'exec', 'env', 'nohup', 'time', 'sudo']);
const KEYWORDS = new Set(['do', 'then', 'else', 'elif', 'if', 'while', 'until', '!']);
const STAGE_SPLIT = /(?:&&|\|\||[;&\n(){}`|])+/;
const CD_VERBS = new Set(['cd', 'pushd']);
const CD_VERBS_POWERSHELL = new Set(['cd', 'pushd', 'chdir', 'sl', 'set-location', 'push-location']);
const PS_WRITERS = new Set(['out-file', 'set-content', 'add-content', 'tee-object']);
const PS_PATH_PARAMS = new Set(['-filepath', '-path', '-literalpath']);
const DEVICE = /^(?:\/dev\/(?:null|stdout|stderr|fd\/\d+)|nul|\$null)$/i;
const GIT_INSTALL = 'C:/Program Files/Git';

if (process.stdin.isTTY) {
  console.error('warn-scratch-write.mjs expects a PreToolUse JSON payload on stdin.');
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
  console.error(`warn-scratch-write: unreadable PreToolUse payload${readWhy ? ` (${readWhy})` : ''} — guard stands down (warn-level, fail-open)`);
  process.exit(0);
}
if (payload.tool_name !== 'Bash' && payload.tool_name !== 'PowerShell') process.exit(0);
const isPowerShell = payload.tool_name === 'PowerShell';
const command = payload.tool_input?.command ?? '';
const platform = process.env.WARN_SCRATCH_WRITE_PLATFORM || process.platform;
const isWin = platform === 'win32';

// --- paths -------------------------------------------------------------------

// A normalized absolute path: forward slashes, `.`/`..` folded, an upper-case
// drive on win32. Null for a relative path with no base.
function toAbs(p, base) {
  let s = isWin || isPowerShell ? p.replace(/\\/g, '/') : p;
  if (s === '~' || s.startsWith('~/')) s = fwd(os.homedir()) + s.slice(1);
  if (isWin) {
    if (!isPowerShell) {
      const drive = /^\/([A-Za-z])(?=\/|$)/.exec(s);
      if (drive) s = `${drive[1].toUpperCase()}:${s.slice(2) || '/'}`;
      else if (s.startsWith('/')) s = GIT_INSTALL + s;
    } else if (s.startsWith('/') && base) s = base.slice(0, 2) + s;
    if (/^[A-Za-z]:/.test(s)) return winNorm(s);
    return base ? winNorm(`${base}/${s}`) : null;
  }
  if (s.startsWith('/')) return path.posix.normalize(s);
  return base ? path.posix.normalize(`${base}/${s}`) : null;
}

function winNorm(s) {
  const drive = s.slice(0, 2).toUpperCase();
  const rest = path.posix.normalize(s.slice(2) || '/');
  return drive + (rest.startsWith('/') ? rest : `/${rest}`);
}

function fwd(p) {
  return p.replace(/\\/g, '/');
}

// A native path (the payload cwd, an env dir, a realpath) in toAbs's form.
const native = (p) => (p ? toAbs(fwd(p), null) : null);

function inside(abs, root) {
  if (!root) return false;
  const [a, r] = isWin ? [abs.toLowerCase(), root.toLowerCase()] : [abs, root];
  return a === r || a.startsWith(r.endsWith('/') ? r : `${r}/`);
}

function variants(p) {
  const out = [native(p)];
  try {
    if (existsSync(p)) out.push(native(realpathSync.native(p)));
  } catch {
    /* a root that cannot be realpath'd keeps its given form */
  }
  return out.filter(Boolean);
}

// The hub tree: the ancestor of CLAUDE_PROJECT_DIR (else the cwd) holding
// projects/, or that directory itself.
function hubRoot() {
  const start = process.env.CLAUDE_PROJECT_DIR || payload.cwd;
  if (typeof start !== 'string' || !start) return null;
  for (let d = start; ; ) {
    if (existsSync(path.join(d, 'projects'))) return d;
    const parent = path.dirname(d);
    if (parent === d) return start;
    d = parent;
  }
}

const hub = hubRoot();
const tmpOverride = process.env.WARN_SCRATCH_WRITE_TMPDIR;
const tmpBase = tmpOverride || os.tmpdir();
const roots = hub ? variants(hub) : [];
// The session scratchpad area: a temp base's child named `claude` or
// `claude-<digits>` (Story 128.1.9 — the Mac harness's scratchpad is
// /private/tmp/claude-<uid>/…, which no fixed `claude` root is a prefix of).
const tempBases = [...variants(tmpBase), ...(!tmpOverride && !isWin ? ['/tmp', '/private/tmp'] : [])];
const AREA_DIR = isWin ? /^claude(?:-\d+)?$/i : /^claude(?:-\d+)?$/;
function inScratchArea(abs) {
  return tempBases.some((b) => inside(abs, b) && AREA_DIR.test(abs.slice(b.length).replace(/^\/+/, '').split('/')[0]));
}
const scratchArea = native(path.join(tmpBase, 'claude'));

// --- matcher -----------------------------------------------------------------

// Heredoc bodies and here-strings dropped; quoted spans to placeholders unless
// a double-quoted span holds live substitution (warn-cwd-drift.mjs's copy).
function neutralize(text0) {
  let text = text0;
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
  const live = isPowerShell ? /\$\(/ : /\$\(|`/;
  text = text.replace(/'[^']*'|"(?:[^"\\]|\\.)*"/g, (q) => {
    const inner = q.slice(1, -1);
    if (q.startsWith('"') && live.test(inner)) return inner;
    values.push(inner);
    return `\x00${values.length - 1}\x00`;
  });
  return { text, values };
}

const { text, values } = neutralize(command);
const vars = new Map();
const unresolved = Symbol('unresolved');

// A word's text with placeholders filled and the line's own variables expanded;
// null when it names a variable the line never assigned.
function expand(word) {
  let missing = false;
  const filled = word.replace(/\x00(\d+)\x00/g, (_, n) => values[Number(n)] ?? '');
  const out = filled.replace(/\$\{([A-Za-z_]\w*)\}|\$([A-Za-z_]\w*)/g, (_, a, b) => {
    const name = a ?? b;
    if (vars.has(name)) return vars.get(name);
    if (name === 'TMPDIR' && isWin && !process.env.TMPDIR) return '';
    missing = true;
    return '';
  });
  return missing ? unresolved : out;
}

const findings = [];
function judge(word, base) {
  const e = expand(word);
  if (e === unresolved || e === '' || e === '-' || DEVICE.test(e)) return;
  const abs = toAbs(e, base);
  if (!abs || roots.some((r) => inside(abs, r)) || inScratchArea(abs)) return;
  const shown = word.replace(/\x00(\d+)\x00/g, (_, n) => values[Number(n)] ?? '');
  if (!findings.some((f) => f.abs === abs)) findings.push({ shown, abs });
}

const commandWord = (t) => {
  const b = expand(t);
  return b === unresolved ? '' : b.split(/[\\/]/).pop().toLowerCase().replace(/\.exe$/, '');
};

let base = typeof payload.cwd === 'string' && path.isAbsolute(payload.cwd) ? native(payload.cwd) : null;
const stages = text
  .replace(/\|&/g, '|')
  .replace(/\d*>&\d*/g, ' ')
  .replace(/&>>?/g, '>')
  .split(STAGE_SPLIT);
for (const stage of stages) {
  const tokens = stage.trim().split(/\s+/).filter(Boolean);
  if (!tokens.length) continue;
  // Redirections, anywhere in the stage: `>f`, `> f`, `2>>f`, `*> f`.
  for (let k = 0; k < tokens.length; k++) {
    const r = /^(?:\d*|\*)>>?(?!=)(.*)$/.exec(tokens[k]);
    if (!r) continue;
    if (r[1]) judge(r[1], base);
    else if (tokens[k + 1]) judge(tokens[++k], base);
  }
  let i = 0;
  const exported = tokens[0] === 'export';
  if (exported) i = 1;
  while (i < tokens.length && (KEYWORDS.has(tokens[i]) || WRAPPERS.has(tokens[i]) || /^[A-Za-z_]\w*=/.test(tokens[i]))) {
    const a = /^([A-Za-z_]\w*)=(.*)$/.exec(tokens[i]);
    if (a) {
      const v = expand(a[2]);
      if (v === unresolved) vars.delete(a[1]);
      else vars.set(a[1], v);
    }
    i++;
  }
  if (i >= tokens.length) continue;
  const word = commandWord(tokens[i]);
  const args = tokens.slice(i + 1).filter((t) => !/^(?:\d*|\*)>/.test(t));
  // A cd moves the base for the stages after it.
  if (isPowerShell ? CD_VERBS_POWERSHELL.has(word) : CD_VERBS.has(word)) {
    const dir = args.find((t) => !t.startsWith('-'));
    const e = dir === undefined ? fwd(os.homedir()) : expand(dir);
    base = e === unresolved || e === '-' ? null : toAbs(e, base);
    continue;
  }
  // A generic --out <path> / --out=<path>, any command.
  for (let k = 0; k < args.length; k++) {
    if (args[k] === '--out' && args[k + 1]) judge(args[k + 1], base);
    else if (args[k].startsWith('--out=')) judge(args[k].slice(6), base);
  }
  if (word === 'tee') {
    for (const t of args) if (!t.startsWith('-')) judge(t, base);
  } else if (word === 'curl' || word === 'wget') {
    const short = word === 'curl' ? /^-[A-Za-z]*o$/ : /^-[A-Za-z]*O$/;
    const long = word === 'curl' ? '--output' : '--output-document';
    for (let k = 0; k < args.length; k++) {
      if ((short.test(args[k]) || args[k] === long) && args[k + 1]) judge(args[++k], base);
      else if (args[k].startsWith(`${long}=`)) judge(args[k].slice(long.length + 1), base);
    }
  } else if (word === 'git') {
    let dir = base;
    let k = 0;
    while (k < args.length && args[k].startsWith('-')) {
      if (args[k] === '-C' && args[k + 1]) {
        const e = expand(args[k + 1]);
        dir = e === unresolved ? null : toAbs(e, dir);
        k += 2;
      } else k += args[k] === '-c' ? 2 : 1;
    }
    if (args[k] === 'worktree' && args[k + 1] === 'add' && dir) {
      for (let j = k + 2; j < args.length; j++) {
        if (['-b', '-B', '--reason'].includes(args[j])) j++;
        else if (!args[j].startsWith('-')) {
          judge(args[j], dir);
          break;
        }
      }
    }
  } else if (isPowerShell && PS_WRITERS.has(word)) {
    const named = args.findIndex((t) => PS_PATH_PARAMS.has(t.toLowerCase()));
    if (named !== -1 && args[named + 1]) judge(args[named + 1], base);
    else if (args[0] && !args[0].startsWith('-')) judge(args[0], base);
  }
}

if (!findings.length) process.exit(0);

const list = findings.map((f) => `\`${f.shown}\` → \`${f.abs}\``).join('; ');
console.log(
  JSON.stringify({
    systemMessage:
      `scratch-write guard: ${findings.length === 1 ? findings[0].abs : `${findings.length} writes`} lands outside ` +
      'the hub tree and the session scratchpad — write scratch files into the scratchpad (warn only; Story 128.1.1)',
    hookSpecificOutput: {
      hookEventName: 'PreToolUse',
      additionalContext:
        `scratch-write guard (warn, not a block): this command writes ${list} — outside both the hub tree ` +
        `(\`${hub ? native(hub) : '?'}\`) and the session scratchpad area (\`${scratchArea}/\`, or a \`claude-<digits>/\` ` +
        `beside it${!tmpOverride && !isWin ? '; /tmp and /private/tmp count as temp dirs' : ''}). Scratch files, ` +
        'copies, logs and worktrees go to the session scratchpad: the hub CLAUDE.md § Standing feedback boundary ' +
        'covers WRITES (2026-09-15, a copy landed a level above the hub). Check each `..` against where it lands; ' +
        'if the write is deliberate (an install, a real output), ignore this. Register class ' +
        "scratch-write-lands-outside-the-scratchpad, its seats in the hub's docs/friction/recurrences.json; this " +
        'hook is its decided form (Story 128.1.1).',
    },
  })
);

// Story 128.1.8 (register class stdin-readfilesync-eagain-on-nonblocking-pipe):
// stdin through the async iterator, never readFileSync(0) — a 1.1 MB payload on a
// non-blocking pipe read as unreadable on Linux CI (Node 24, hub #3034's first run).
async function readStdin() {
  const chunks = [];
  for await (const chunk of process.stdin) chunks.push(chunk);
  return Buffer.concat(chunks).toString('utf8');
}
