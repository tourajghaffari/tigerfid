#!/usr/bin/env node
// warn-invisible-char.mjs — PreToolUse hook: WARN (never block) when an Edit's
// new_string or a Write's content holds an invisible format character, naming
// each with its line:column (hub Story 128.1.3, Initiative 128; register class
// sed-replacement-backslash-escapes, the Edit/Write face; class
// stray-control-byte-hides-file-from-text-tooling seat 11, the bidi controls).
//
// Why this exists: the harness Write and Edit tools decode a typed Unicode
// escape into the character itself (KB scripted-replacement-metacharacters), so
// a byte-order mark or a zero-width character lands in a file with nothing on
// screen to show it — deccentral-app #531's tick merged one, caught later by a
// hand scan, and tigerdash's Sprint 10 proposal had a right-to-left override
// materialize beside a byte-order mark. scripts/check-control-bytes.mjs covers
// C0 controls only, so neither tripped it. No KB recall prevents a face the
// author cannot see; this hook names it at the call.
//
// THE CHARACTERS (code points, compared as NUMBERS — no character and no escape
// sequence is typed in this source, which would be the class's own face):
// U+FEFF (byte-order mark / zero width no-break space), U+200B–U+200F (zero
// width space, non-joiner, joiner, left-to-right and right-to-left marks),
// U+2060 (word joiner), and the bidi controls U+202A–U+202E (embeddings and
// overrides) and U+2066–U+2069 (isolates). Visible non-ASCII text (accents,
// dashes, arrows, emoji) is silent, and so is the six-character escape sequence
// written as source text — that is the class's own rule for spelling one.
//
// POSITION: line:column, both 1-based; the column counts UTF-16 code units from
// the line start (every listed character is in the BMP).
//
// WARN-level BY DESIGN: a file may hold one on purpose (a BOM fixture), so the
// hook emits NO permissionDecision — `additionalContext` (reaches the model)
// plus a `systemMessage` (reaches the user). Malformed stdin: stand down loudly
// (stderr note, exit 0). One linear pass; no regex over the text.
//
// Wired from the hub's .claude/settings.json (PreToolUse, matcher "Edit|Write",
// exec form). The fleet roster entry and every repo's wiring are hub Epic 128.2's.
//
// Test manually (from the hub; the first warns, the second is silent):
//   node -e 'process.stdout.write(JSON.stringify({tool_name:"Write",tool_input:{file_path:"x.md",content:String.fromCodePoint(0xfeff)+"x"}}))' \
//     | node scripts/warn-invisible-char.mjs
//   node -e 'process.stdout.write(JSON.stringify({tool_name:"Write",tool_input:{file_path:"x.md",content:"plain"}}))' \
//     | node scripts/warn-invisible-char.mjs
// Suite: test/warn-invisible-char.test.mjs (node --test).

const NAMES = new Map([
  [0xfeff, 'byte-order mark (zero width no-break space)'],
  [0x200b, 'zero width space'],
  [0x200c, 'zero width non-joiner'],
  [0x200d, 'zero width joiner'],
  [0x200e, 'left-to-right mark'],
  [0x200f, 'right-to-left mark'],
  [0x2060, 'word joiner'],
  [0x202a, 'left-to-right embedding'],
  [0x202b, 'right-to-left embedding'],
  [0x202c, 'pop directional formatting'],
  [0x202d, 'left-to-right override'],
  [0x202e, 'right-to-left override'],
  [0x2066, 'left-to-right isolate'],
  [0x2067, 'right-to-left isolate'],
  [0x2068, 'first strong isolate'],
  [0x2069, 'pop directional isolate'],
]);
const SHOWN_MAX = 5;
const NEWLINE = 10;

if (process.stdin.isTTY) {
  console.error('warn-invisible-char.mjs expects a PreToolUse JSON payload on stdin.');
  process.exit(1);
}

// stdin through the async iterator, never readFileSync(0): a Write's payload
// carries the whole file, and on Linux (CI, Node 24) a 1.1 MB payload read
// synchronously came back unreadable — register class
// stdin-readfilesync-eagain-on-nonblocking-pipe, seat 2. The iterator waits on
// the event loop instead of failing on an empty non-blocking pipe.
let payload = null;
let why = 'not a JSON object';
try {
  const chunks = [];
  for await (const chunk of process.stdin) chunks.push(chunk);
  payload = JSON.parse(Buffer.concat(chunks).toString('utf8'));
} catch (e) {
  why = e?.code ?? e?.name ?? 'read failed';
}
if (typeof payload !== 'object' || payload === null || Array.isArray(payload)) {
  console.error(`warn-invisible-char: unreadable PreToolUse payload (${why}) — guard stands down (warn-level, fail-open)`);
  process.exit(0);
}
const tool = payload.tool_name;
if (tool !== 'Edit' && tool !== 'Write') process.exit(0);
const text = String((tool === 'Write' ? payload.tool_input?.content : payload.tool_input?.new_string) ?? '');
const file = String(payload.tool_input?.file_path ?? '?');

const findings = [];
let line = 1;
let lineStart = 0;
for (let i = 0; i < text.length; i++) {
  const c = text.charCodeAt(i);
  if (c === NEWLINE) {
    line++;
    lineStart = i + 1;
  } else if (NAMES.has(c)) {
    findings.push({ code: `U+${c.toString(16).toUpperCase().padStart(4, '0')}`, name: NAMES.get(c), at: `${line}:${i - lineStart + 1}` });
  }
}

if (!findings.length) process.exit(0);

const shown = findings.slice(0, SHOWN_MAX).map((f) => `${f.code} ${f.name} at ${f.at}`).join('; ') +
  (findings.length > SHOWN_MAX ? `; and ${findings.length - SHOWN_MAX} more` : '');
const field = tool === 'Write' ? 'content' : 'new_string';
console.log(
  JSON.stringify({
    systemMessage:
      `invisible-char guard: ${findings.length === 1 ? 'an invisible format character' : `${findings.length} invisible format characters`} ` +
      `in this ${tool} to ${file} — ${findings[0].code} at ${findings[0].at} (warn only; Story 128.1.3)`,
    hookSpecificOutput: {
      hookEventName: 'PreToolUse',
      additionalContext:
        `invisible-char guard (warn, not a block): this ${tool}'s ${field} for \`${file}\` holds ${shown}. Nothing ` +
        'on screen shows these characters. The harness Write and Edit tools decode a typed Unicode escape into the ' +
        'character itself (KB scripted-replacement-metacharacters), so a record meant to SHOW an escape spells it in ' +
        'words, and a test fixture builds it by code point (String.fromCodePoint). If the character is deliberate ' +
        "(a BOM fixture), ignore this. Register classes sed-replacement-backslash-escapes (this face's decided form, " +
        "Story 128.1.3) and stray-control-byte-hides-file-from-text-tooling, their seats in the hub's " +
        'docs/friction/recurrences.json.',
    },
  })
);
