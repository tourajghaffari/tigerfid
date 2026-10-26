#!/usr/bin/env node
// warn-heroku-spaced-arg.mjs — PreToolUse hook: on win32, WARN (never block) when
// a Bash-tool command runs `heroku` with an argument that contains a space (hub
// Story 128.1.2, Initiative 128; register class
// heroku-run-from-git-bash-never-reaches-a-dyno; KB windows-git-bash-cli-gotchas §7).
//
// Why this exists: from Git Bash on the Windows box, ANY heroku argument holding
// a space, on any subcommand, dies in the `heroku.cmd` shim before the CLI starts
// — `'C:\Program' is not recognized as an internal or external command`, exit 1,
// no `Running … on <app>` line, no dyno. MEASURED at the class's second seat
// (2026-09-29): `heroku version 'plain words'` fails, `heroku version plainword`
// passes. Every `heroku run` smoke carries a spaced argument, so a smoke run that
// way is no result in either direction. Seat 2 (`caught: false`) fired hours
// after the KB bullet reached origin/main; this hook puts the rule at the call.
//
// WHAT FIRES: tool `Bash`, platform win32 (WARN_HEROKU_SPACED_ARG_PLATFORM pins
// it in tests), and a stage whose command word is `heroku` (by any path, `.cmd`
// / `.exe` included; after `NAME=value` assignments, the wrappers command /
// builtin / exec / env / nohup / time / sudo, and the keywords do / then / else /
// elif / if / while / until / !) with a later word holding whitespace — from a
// single- or double-quoted span, or a backslash-escaped space.
//
// WHAT STAYS SILENT: the PowerShell tool (the remedy), any other platform (the
// Mac has no shim), a quoted mention (`echo "heroku …"` — its stage word is echo),
// a WSL relay (`wsl … bash -lc '…'` — the other remedy), heredoc bodies, and
// another command's spaced argument.
//
// Known limits (accepted, warn-level): `heroku` inside `bash -c "…"` or a
// command substitution is invisible; a variable that expands to spaced text
// (`heroku run "$CMD"` holds no literal space) stands down — the quoted span
// `"$CMD"` holds no whitespace as written.
//
// MATCHER: one linear pass over the command (quote scans advance the cursor; no
// nested quantifier). Stages split on && || ; & | newline, parens and backticks.
//
// WARN-level BY DESIGN: `additionalContext` (reaches the model) plus a
// `systemMessage` (reaches the user), never a permissionDecision. Malformed
// stdin: stand down loudly (stderr note, exit 0).
//
// Wired from the hub's .claude/settings.json (PreToolUse, matcher
// "Bash|PowerShell", exec form; it stands down on PowerShell). The fleet roster
// entry and every repo's wiring are hub Epic 128.2's.
//
// Test manually (from the hub; the first warns on any box, the second is silent):
//   echo '{"tool_name":"Bash","tool_input":{"command":"heroku version '"'"'plain words'"'"'"}}' \
//     | WARN_HEROKU_SPACED_ARG_PLATFORM=win32 node scripts/warn-heroku-spaced-arg.mjs
//   echo '{"tool_name":"Bash","tool_input":{"command":"heroku releases -a expeal-app"}}' \
//     | WARN_HEROKU_SPACED_ARG_PLATFORM=win32 node scripts/warn-heroku-spaced-arg.mjs
// Suite: test/warn-heroku-spaced-arg.test.mjs (node --test).


const WRAPPERS = new Set(['command', 'builtin', 'exec', 'env', 'nohup', 'time', 'sudo']);
const KEYWORDS = new Set(['do', 'then', 'else', 'elif', 'if', 'while', 'until', '!']);
const ASSIGNMENT = /^[A-Za-z_][A-Za-z0-9_]*=/;
const SHOWN_MAX = 3;

if (process.stdin.isTTY) {
  console.error('warn-heroku-spaced-arg.mjs expects a PreToolUse JSON payload on stdin.');
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
  console.error(`warn-heroku-spaced-arg: unreadable PreToolUse payload${readWhy ? ` (${readWhy})` : ''} — guard stands down (warn-level, fail-open)`);
  process.exit(0);
}
if (payload.tool_name !== 'Bash') process.exit(0);
const platform = process.env.WARN_HEROKU_SPACED_ARG_PLATFORM || process.platform;
if (platform !== 'win32') process.exit(0);
const command = String(payload.tool_input?.command ?? '');

// Heredoc bodies are text, not commands: drop every line from the one after a
// `<<WORD` / `<<-'WORD'` operator to its terminator (`<<<` here-strings excepted).
function dropHeredocs(text) {
  const out = [];
  const pending = [];
  for (const line of text.split('\n')) {
    if (pending.length) {
      const want = pending[0];
      if ((want.dash ? line.replace(/^\t+/, '') : line) === want.word) pending.shift();
      continue;
    }
    out.push(line);
    for (const m of line.matchAll(/(?<!<)<<(?!<)(-?)[ \t]*(['"]?)([A-Za-z_][\w.-]*)\2/g)) {
      pending.push({ dash: m[1] === '-', word: m[3] });
    }
  }
  return out.join('\n');
}

// One pass: stages of words, each word { text, raw, spaced } — `spaced` when a
// quoted span or an escaped space put whitespace inside it.
function stagesOf(text) {
  const stages = [];
  let words = [];
  let cur = '';
  let start = -1;
  let spaced = false;
  const endWord = (i) => {
    if (start !== -1) words.push({ text: cur, raw: text.slice(start, i), spaced });
    cur = '';
    start = -1;
    spaced = false;
  };
  const endStage = (i) => {
    endWord(i);
    if (words.length) stages.push(words);
    words = [];
  };
  for (let i = 0; i < text.length; i++) {
    const c = text[i];
    if (c === "'") {
      if (start === -1) start = i;
      const j = text.indexOf("'", i + 1);
      const seg = j === -1 ? text.slice(i + 1) : text.slice(i + 1, j);
      cur += seg;
      if (/\s/.test(seg)) spaced = true;
      i = j === -1 ? text.length : j;
    } else if (c === '"') {
      if (start === -1) start = i;
      let j = i + 1;
      let seg = '';
      while (j < text.length && text[j] !== '"') {
        if (text[j] === '\\' && j + 1 < text.length) {
          seg += text[j + 1];
          j += 2;
        } else seg += text[j++];
      }
      cur += seg;
      if (/\s/.test(seg)) spaced = true;
      i = j;
    } else if (c === '\\') {
      const n = text[i + 1];
      if (n === '\n') {
        i++;
        continue;
      }
      if (start === -1) start = i;
      if (n === ' ' || n === '\t') spaced = true;
      cur += n ?? '';
      i++;
    } else if (c === ' ' || c === '\t' || c === '\r') {
      endWord(i);
    } else if ('\n;&|()`'.includes(c)) {
      endStage(i);
    } else {
      if (start === -1) start = i;
      cur += c;
    }
  }
  endStage(text.length);
  return stages;
}

const findings = [];
for (const words of stagesOf(dropHeredocs(command))) {
  let k = 0;
  while (k < words.length && (KEYWORDS.has(words[k].text) || WRAPPERS.has(words[k].text) || ASSIGNMENT.test(words[k].text))) k++;
  if (k >= words.length) continue;
  const base = words[k].text.replace(/\\/g, '/').split('/').pop().toLowerCase().replace(/\.(?:cmd|exe|bat)$/, '');
  if (base !== 'heroku') continue;
  for (const w of words.slice(k + 1)) if (w.spaced) findings.push(w.raw);
}

if (!findings.length) process.exit(0);

const shown = findings.slice(0, SHOWN_MAX).map((a) => `\`${a}\``).join(', ') +
  (findings.length > SHOWN_MAX ? ` and ${findings.length - SHOWN_MAX} more` : '');
console.log(
  JSON.stringify({
    systemMessage:
      `heroku-spaced guard: a heroku argument with a space (${shown}) dies in the heroku.cmd shim from Git Bash ` +
      'before the CLI starts — run it from PowerShell or WSL (warn only; Story 128.1.2)',
    hookSpecificOutput: {
      hookEventName: 'PreToolUse',
      additionalContext:
        `heroku-spaced guard (warn, not a block): on the Windows box this Bash-tool command runs \`heroku\` with the ` +
        `spaced argument ${shown}. From Git Bash, ANY heroku argument containing a space, on any subcommand, dies ` +
        "in the heroku.cmd shim before the CLI starts: `'C:\\Program' is not recognized as an internal or external " +
        'command`, exit 1, no `Running … on <app>` line and no dyno — so a smoke run this way is no result in ' +
        "either direction (KB windows-git-bash-cli-gotchas §7, the measured trigger: `heroku version 'plain words'` " +
        'fails, `heroku version plainword` passes). Run it from the PowerShell tool (a `heroku run` payload with no ' +
        "double quotes — nest single quotes via ''), or from WSL with the WSL-home CLI by its absolute path. " +
        "Register class heroku-run-from-git-bash-never-reaches-a-dyno, its seats in the hub's " +
        'docs/friction/recurrences.json; this hook is its decided form (Story 128.1.2).',
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
