// scripts/warn-unmarked-session.mjs — PreToolUse WARN guard (never blocks).
// One throttled warn at the first substantive write when this session never
// pushed a presence marker for the repo (pairs with fleet-view's UNDECLARED
// classification). Canonical hub copy — fleet-synced.
import { existsSync, mkdirSync, readFileSync, writeFileSync, realpathSync } from 'node:fs';
import { execFileSync } from 'node:child_process';
import os from 'node:os';
import path from 'node:path';
// Inlined from scripts/fleet-view/constants.mjs — hooks are fleet-synced
// single-file canonicals; keep in agreement when retuning.
const WARN_THROTTLE_HOURS = 4;
// Story 97.1.2: the lifecycle bounds a registry row is judged by (mirrors of
// constants.mjs). A `free` row is the repo's marker for the TTL; a work-role
// or roleless row until the sweep bound — WITHOUT corroboration (fleet-view
// stretches a work-role marker to STALE on repo activity this hook never
// reads), so a work-role row past the sweep is dead here even where the view
// still prints it STALE. The registry FILE keeps dead rows: pruneRegistry
// drops them only for the checkout a --start roots in (Story 53.1.1, kept),
// so a repo no session had started in since held a dead `free` row that
// nudged every landing there for ever, and a dead `executor` row that
// silenced this guard for ever (Sprint 34 F8; the reviewer pass's census:
// ten of twelve rows on the Mac past a bound).
const MARKER_TTL_HOURS = 8;
const MARKER_SWEEP_HOURS = 48;

if (process.stdin.isTTY) { console.error('warn-unmarked-session.mjs expects a PreToolUse JSON payload on stdin.'); process.exit(1); }
let payload; try { payload = JSON.parse(await readStdin()); } catch { payload = null; }
if (typeof payload !== 'object' || payload === null) process.exit(0);
// Story 94.1.1 (the Bash face): a shell payload — Bash on both machines, the
// PowerShell tool on the Windows box (Story 73.1.1's payload shape:
// tool_input.command + a cwd) — is read for the three verbs that LAND work:
// `git commit`, `git push`, `gh pr create`, found outside quoted spans,
// heredoc bodies, comments and substitutions; a Bash-first session on a bare
// `free` marker otherwise never met this nudge before its first commit and
// landed on a marker that expires at 8 h and cannot tombstone (expeal.com
// #523, 2026-09-12). The verb's target repo is its segment's `git -C <dir>`,
// else the last `cd` before it, else the payload's cwd. The Edit/Write face
// below is unchanged; the registry read, the throttle key and the message are
// shared, so one nudge per repo across both faces.
const SHELL_TOOLS = new Set(['Bash', 'PowerShell']);
const isShell = SHELL_TOOLS.has(payload.tool_name);
if (!isShell && payload.tool_name !== 'Edit' && payload.tool_name !== 'Write') process.exit(0);
const isPowerShell = payload.tool_name === 'PowerShell';

// Data, not command structure — copied from block-commit-on-main.mjs's
// neutralize (a hook is a single-file canonical, never an import): PowerShell
// here-strings and heredoc bodies dropped whole; quoted spans become one
// space (a double-quoted span carrying a live `$(` / backtick keeps its inner
// text — the shell interpolates it). No placeholders: this hook never reads
// the quoted values back.
function neutralize(command) {
  let text = String(command);
  if (isPowerShell) text = text.replace(/@(['"])\r?\n[\s\S]*?\r?\n\1@/g, ' ');
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
  const live = isPowerShell ? /\$\(/ : /\$\(|`/;
  text = text.replace(/'[^']*'|"(?:[^"\\]|\\.)*"/g, (q) => (q.startsWith('"') && live.test(q.slice(1, -1)) ? q.slice(1, -1) : ' '));
  return text;
}
// Substitution spans are data too (warn-zsh-word-split.mjs's linear scan,
// Story 92.1.3): `$(…)` at any nesting and backtick spans become a space; an
// unclosed head leaves the rest as written — never a backtracking pattern.
function dropSubstitutions(text) {
  let out = '';
  let i = 0;
  while (i < text.length) {
    const ch = text[i];
    if (ch === '`' && !isPowerShell) {
      const j = text.indexOf('`', i + 1);
      if (j === -1) return out + text.slice(i);
      out += ' ';
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
      out += ' ';
      i = j + 1;
      continue;
    }
    out += ch;
    i++;
  }
  return out;
}
// The first landing verb in the command, with its target directory — or null.
// Wrapper words and VAR=value prefixes are skipped; `git -C <dir>` and the
// other argument-taking git globals are skipped the way commitInvocation
// skips them (block-commit-on-main.mjs); a `#` comment (after the quotes are
// gone, BEFORE the substitution scan) runs to its line end.
function landingVerb(command, cwd) {
  const WRAPPERS = new Set(['command', 'exec', 'env', 'nohup', 'time', 'nice', 'ionice', 'xargs', 'sudo', 'builtin']);
  // Story 97.1.3: shell keywords that may lead a segment before the verb —
  // block-commit-on-main.mjs's KEYWORDS, copied (a single-file canonical).
  const KEYWORDS = new Set(['do', 'then', 'else', 'elif', 'if', 'while', 'until', '!']);
  const GIT_ARG_FLAGS = new Set(['-C', '-c', '--config-env', '--git-dir', '--work-tree', '--namespace', '--super-prefix', '--exec-path']);
  const CD_VERBS = new Set(isPowerShell ? ['cd', 'chdir', 'sl', 'set-location', 'pushd', 'push-location'] : ['cd', 'pushd']);
  // Comments drop after the quotes are gone and BEFORE the substitution scan
  // (the word-split hook's Sprint 33 N-2, in this copy of the shape —
  // 2026-09-14): a `# note (` on the first line of a multi-line `$(…)` would
  // otherwise leave the substitution unclosed, the text standing as written,
  // and a verb inside it read as a landing verb.
  const text = dropSubstitutions(neutralize(command).replace(/(^|\s)#[^\n]*/g, '$1'));
  const abs = (p, base) => path.resolve(msysDrive(base), msysDrive(p));
  let lastCd = null;
  for (const seg of text.split(/(?:&&|\|\||[;&|\n(){}])+/)) {
    const toks = seg.trim().split(/\s+/).filter(Boolean);
    let i = 0;
    while (i < toks.length && (KEYWORDS.has(toks[i]) || WRAPPERS.has(toks[i]) || /^[A-Za-z_][A-Za-z0-9_]*=/.test(toks[i]))) i++;
    if (i >= toks.length) continue;
    const head = isPowerShell ? toks[i].toLowerCase() : toks[i];
    if (CD_VERBS.has(head)) {
      const arg = toks.slice(i + 1).find((t) => !t.startsWith('-'));
      if (arg) lastCd = abs(arg, lastCd ?? cwd);
      continue;
    }
    if (toks[i] === 'git') {
      let j = i + 1;
      let cDir = null;
      while (j < toks.length && toks[j].startsWith('-')) {
        if (GIT_ARG_FLAGS.has(toks[j])) {
          if (toks[j] === '-C' && toks[j + 1]) cDir = toks[j + 1];
          j += 2;
        } else j++;
      }
      const sub = toks[j];
      if (sub === 'commit' || sub === 'push') return { verb: `git ${sub}`, target: cDir ? abs(cDir, lastCd ?? cwd) : (lastCd ?? abs('.', cwd)) };
      continue;
    }
    // The PR-open verb is the three-word phrase (compared whole: this hook
    // reads a command STRING for it and never invokes the CLI — the
    // forge-boundary census flags a bare quoted CLI name as a call).
    if (toks.slice(i, i + 3).join(' ') === 'gh pr create') return { verb: 'gh pr create', target: lastCd ?? abs('.', cwd) };
  }
  return null;
}
const msysDrive = (p) => { if (process.platform !== 'win32' || !p) return p; const m = /^\/([A-Za-z])(\/.*)?$/.exec(p); return m ? `${m[1].toUpperCase()}:${m[2] ?? '/'}` : p; };
let repo;
if (isShell) {
  const hit = landingVerb(payload.tool_input?.command ?? '', payload.cwd || process.cwd());
  if (!hit) process.exit(0);
  repo = hit.target;
} else {
  const file = msysDrive(payload.tool_input?.file_path ?? '');
  if (!file) process.exit(0);
  repo = path.dirname(path.resolve(file));
}
while (repo && !existsSync(path.join(repo, '.git'))) { const up = path.dirname(repo); if (up === repo) process.exit(0); repo = up; }
// Story 106.1.1: the NATIVE realpath. The JavaScript realpathSync keeps the
// caller's case on Windows (it resolves links component by component and
// never asks the volume for the name), so a registry row written from git's
// toplevel in the on-disk spelling (`Documents\GitHub`) never equalled a
// write addressed as the session typed it (`documents\github`) — the
// unmarked nudge fired on marked repos three times in two days (register
// class win32-js-realpath-keeps-typed-case). The native call answers with
// the on-disk spelling on both sides of every compare below; a path that
// does not exist still falls back to the string as given.
const norm = (p) => { try { return realpathSync.native(p); } catch { return p; } };
// Story 37.1.3 (the worktree face): a `.claude/worktrees/<name>` worktree is a
// different PATH but the SAME repo — the session's marker is per-repo, so the
// registry lookup keys on the git COMMON dir, never the checkout path. A
// checkout whose common dir can't be read (no git; a bare `.git` dir fixture)
// falls back to its path — the pre-37.1.3 behavior, so nothing fails closed.
const commonDir = (dir) => {
  try {
    const out = execFileSync('git', ['rev-parse', '--git-common-dir'], { cwd: dir, encoding: 'utf8', timeout: 5_000, stdio: ['ignore', 'pipe', 'ignore'] }).trim();
    return norm(path.isAbsolute(out) ? out : path.join(dir, out));
  } catch { return null; }
};
const repoKey = commonDir(repo) ?? norm(repo);

const home = process.env.FGL_HOME || path.join(os.homedir(), '.fgl');
const readJson = (p, d) => { try { return JSON.parse(readFileSync(p, 'utf8')); } catch { return d; } };
const reg = readJson(path.join(home, 'session-markers.json'), { markers: [] });
// Story 97.1.2: a row past its lifecycle bound is not the repo's marker. An
// undatable row is past every bound (the guard fails toward one throttled
// line, never toward silence). The age check runs first — it spares the git
// read behind commonDir for every dead row.
const hoursOld = (m) => { const t = Date.parse(m.createdAt ?? ''); return Number.isFinite(t) ? (Date.now() - t) / 3_600_000 : Infinity; };
const alive = (m) => hoursOld(m) <= (m.role === 'free' ? MARKER_TTL_HOURS : MARKER_SWEEP_HOURS);
// Story 108.1.2: a row COVERS this repo when it is rooted here, or when its
// declared `repos` (written into the registry by fleet-view --declare; the
// manifest's spelling — the repo's directory name, read from the common dir
// so a worktree names its repo) names it.
const repoName = path.basename(repoKey) === '.git' ? path.basename(path.dirname(repoKey)) : path.basename(repo);
const covers = (m) => norm(m.repoRoot) === norm(repo) || (commonDir(m.repoRoot) ?? norm(m.repoRoot)) === repoKey
  || (Array.isArray(m.repos) && m.repos.includes(repoName));
const live = reg.markers.filter(alive);
// The WRITER's own rows first (the payload's session_id against the row's
// sessionId, which addMarker records from CLAUDE_CODE_SESSION_ID). Rows are
// keyed by repo, so without this a sibling's orientation row for the target
// repo was read as the writer's marker, and a hub-rooted session writing in a
// sibling repo was told to --declare-role — which, from the hub, amends the
// hub marker and covers nothing. No session_id, or no row of the writer's
// (legacy rows carry none): today's by-repo reading, declarations included.
const sid = typeof payload.session_id === 'string' && payload.session_id ? payload.session_id : null;
const own = sid ? live.filter((m) => m.sessionId === sid) : [];
const mine = (own.length ? own : live).filter(covers);

const stampsPath = path.join(home, 'warn-stamps.json');
const stamps = readJson(stampsPath, {});
const throttled = (key) => stamps[key] && (Date.now() - new Date(stamps[key])) < WARN_THROTTLE_HOURS * 3_600_000;
const emit = (key, systemMessage, additionalContext) => {
  if (throttled(key)) process.exit(0);
  stamps[key] = new Date().toISOString();
  mkdirSync(home, { recursive: true }); writeFileSync(stampsPath, JSON.stringify(stamps, null, 2));
  console.log(JSON.stringify({ systemMessage, hookSpecificOutput: { hookEventName: 'PreToolUse', additionalContext } }));
  process.exit(0);
};

if (own.length && !mine.length) {
  // Story 108.1.2: the writer HAS a live marker, rooted elsewhere, that does
  // not name this repo — the remedy is to widen it, from its own root.
  const marker = own.slice().sort((a, b) => hoursOld(a) - hoursOld(b))[0];
  // Story 108.1.4: name the command the marker's ROOT can run — the hub
  // carries scripts/fleet-view.mjs, a site under projects/ reaches it as
  // ../../scripts/fleet-view.mjs. Walk up from the root to the first
  // directory that has it; none found → both forms, the free-role shape.
  let script = null;
  for (let dir = marker.repoRoot; dir; ) {
    if (existsSync(path.join(dir, 'scripts', 'fleet-view.mjs'))) {
      script = path.relative(marker.repoRoot, path.join(dir, 'scripts', 'fleet-view.mjs')).replace(/\\/g, '/');
      break;
    }
    const up = path.dirname(dir);
    dir = up === dir ? null : up;
  }
  const declare = script
    ? `node ${script} --declare ${repoName}`
    : `node scripts/fleet-view.mjs --declare ${repoName} (site sessions: node ../../scripts/fleet-view.mjs --declare ${repoName})`;
  emit(`undeclared:${repoKey}`,
    `unmarked-session guard: this session's marker (${marker.ref}) is rooted in ${path.basename(marker.repoRoot)} and does not declare ${repoName} — this ${isShell ? 'command' : 'write'} lands work there (warn only)`,
    `Widen the marker so concurrent sessions can see this work. Run from ${marker.repoRoot}: ${declare}`);
}

if (mine.length > 0) {
  // Story 42.1.2 (spec Amendment 2026-08-17, decision 4): role: free MEANS
  // orientation-only — a substantive write under an all-free registry gets
  // one throttled nudge toward the cheap upgrade. Entries without a role
  // field (pre-42.1.2 format) stay silent: we can't know, so we don't nag.
  if (!(mine.length && mine.every((m) => m.role === 'free'))) process.exit(0);
  // Story 97.1.2: name the marker read. Rows are keyed by REPO, not by
  // session, so the row is often another session's (F8: the leg-6 fire came
  // from a sibling's twelve-day-old orientation row for the target repo) —
  // "this session's marker" named a marker this session never pushed. The
  // newest live free row is named; any others are counted.
  const newest = mine.slice().sort((a, b) => hoursOld(a) - hoursOld(b))[0];
  const others = mine.length > 1 ? `, +${mine.length - 1} more` : '';
  emit(`freerole:${repoKey}`,
    `free-role guard: the marker registered for ${path.basename(repo)} (${newest.ref}${others}, role: free) is orientation-only, but this ${isShell ? 'command' : 'write'} lands work there (warn only)`,
    // Story 247.1.15: the six known roles (fleet-view's KNOWN_ROLES, inlined — this
    // canonical imports builtins only), in fleet-view's own USAGE spelling.
    `Declare the work role so classification matches what this session is doing. Run: node scripts/fleet-view.mjs --declare-role ROLE (site sessions: node ../../scripts/fleet-view.mjs --declare-role ROLE) — ROLE is one of executor, reviewer, closer, proposer, coordinator, free.`);
}

// Story 247.1.15: the start line names --role and --task-id (the role a work session
// opens with; the background-task path id its fresh-session read takes), with
// `fleet-view.mjs --start` kept contiguous and fleet-view's own USAGE spelling.
emit(`unmarked:${repoKey}`,
  `unmarked-session guard: no presence marker for ${path.basename(repo)} (warn only)`,
  `This session never ran the start ritual here. Run: node scripts/fleet-view.mjs --start "INTENT" --role ROLE --task-id ID (site sessions: node ../../scripts/fleet-view.mjs --start "INTENT" --role ROLE --task-id ID) so concurrent sessions can see this work — ROLE is one of executor, reviewer, closer, proposer, coordinator, free; ID is the background-task path id (start any background command and read the id in its task-output path).`);

// Story 128.1.8 (register class stdin-readfilesync-eagain-on-nonblocking-pipe):
// stdin through the async iterator, never readFileSync(0) — a 1.1 MB payload on a
// non-blocking pipe read as unreadable on Linux CI (Node 24, hub #3034's first run).
async function readStdin() {
  const chunks = [];
  for await (const chunk of process.stdin) chunks.push(chunk);
  return Buffer.concat(chunks).toString('utf8');
}
