// scripts/warn-swept-surface.mjs — PreToolUse WARN guard (never blocks).
// Warns when an Edit/Write touches a file an APPROVED-but-unexecuted sprint
// block's staleness sweep verified (fleet-view.json sweptSurfaces) — the
// bump-vs-train class. Inactive-but-loud when the JSON is stale (throttled).
// Canonical hub copy — fleet-synced. Manual test: pipe a payload on stdin.
import { existsSync, mkdirSync, readFileSync, writeFileSync } from 'node:fs';
import os from 'node:os';
import path from 'node:path';
// Inlined from scripts/fleet-view/constants.mjs — hooks are fleet-synced
// single-file canonicals; keep in agreement when retuning.
const JSON_FRESH_HOURS = 24;
const WARN_THROTTLE_HOURS = 4;

if (process.stdin.isTTY) { console.error('warn-swept-surface.mjs expects a PreToolUse JSON payload on stdin.'); process.exit(1); }
let payload; try { payload = JSON.parse(await readStdin()); } catch { payload = null; }
if (typeof payload !== 'object' || payload === null) process.exit(0);
if (payload.tool_name !== 'Edit' && payload.tool_name !== 'Write') process.exit(0);
const msysDrive = (p) => { if (process.platform !== 'win32' || !p) return p; const m = /^\/([A-Za-z])(\/.*)?$/.exec(p); return m ? `${m[1].toUpperCase()}:${m[2] ?? '/'}` : p; };
if (!payload.tool_input?.file_path) process.exit(0);
const file = path.resolve(msysDrive(payload.tool_input.file_path));

const home = process.env.FGL_HOME || path.join(os.homedir(), '.fgl');
const viewPath = path.join(home, 'fleet-view.json');
const stampsPath = path.join(home, 'warn-stamps.json');
const readJson = (p, d) => { try { return JSON.parse(readFileSync(p, 'utf8')); } catch { return d; } };
const throttled = (key) => {
  const stamps = readJson(stampsPath, {});
  const last = stamps[key] ? new Date(stamps[key]) : null;
  if (last && (Date.now() - last) < WARN_THROTTLE_HOURS * 3_600_000) return true;
  stamps[key] = new Date().toISOString();
  mkdirSync(home, { recursive: true }); writeFileSync(stampsPath, JSON.stringify(stamps, null, 2));
  return false;
};
const emit = (systemMessage, additionalContext) => { console.log(JSON.stringify({ systemMessage, hookSpecificOutput: { hookEventName: 'PreToolUse', additionalContext } })); process.exit(0); };

if (!existsSync(viewPath)) process.exit(0);
const view = readJson(viewPath, null);
if (!view) process.exit(0);
const ageH = (Date.now() - new Date(view.derivedAt)) / 3_600_000;
// The MOST SPECIFIC repo wins (hub Story 108.1.1): fleet-view's roster lists
// the hub first, and the hub's directory contains every projects/<site>/
// clone — a first match judged every site file against the HUB, so a site's
// own swept surfaces never matched. Among the repos containing the file, the
// longest directory is the one it belongs to.
const repo = (view.repos ?? [])
  .filter((r) => r.dir && file.startsWith(path.resolve(r.dir) + path.sep))
  .sort((a, b) => path.resolve(b.dir).length - path.resolve(a.dir).length)[0];
if (!repo) process.exit(0);
const rel = path.relative(path.resolve(repo.dir), file).replace(/\\/g, '/');
const hit = (repo.sweptSurfaces ?? []).find((s) => (s.paths ?? []).includes(rel));
if (!hit) process.exit(0);

if (ageH > JSON_FRESH_HOURS) {
  if (throttled(`swept-inactive:${repo.dir}`)) process.exit(0);
  emit('swept-surface guard: INACTIVE — fleet-view.json is stale (warn only)', `fleet-view.json derivedAt is ${Math.round(ageH)}h old; re-run node scripts/fleet-view.mjs for live swept-surface protection.`);
}
emit(`swept-surface guard: ${rel} is cited by ${repo.name} Sprint ${hit.sprint}'s approved block (warn only)`, `This file was verified by Sprint ${hit.sprint}'s staleness sweep (swept ${hit.sweptDate ?? 'date unrecorded'}). Your edit may decay its pre-verified facts — re-verify or annotate the block (KB sprint-proposal-standard step 2).`);

// Story 128.1.8 (register class stdin-readfilesync-eagain-on-nonblocking-pipe):
// stdin through the async iterator, never readFileSync(0) — a 1.1 MB payload on a
// non-blocking pipe read as unreadable on Linux CI (Node 24, hub #3034's first run).
async function readStdin() {
  const chunks = [];
  for await (const chunk of process.stdin) chunks.push(chunk);
  return Buffer.concat(chunks).toString('utf8');
}
