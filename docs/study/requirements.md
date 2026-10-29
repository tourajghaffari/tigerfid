# Requirements — the three scenarios, with numbers

The first document of the design study (plan Story 2.1.1). It turns the
three acceptance scenarios into numbers, so every design option in the
rest of the study (Stories 2.1.3–2.1.6) is judged against figures rather
than impressions. Each scenario gets one number, with its reasoning, for
each of five measures:

- indoor read range;
- tags per second through anti-collision;
- zone accuracy;
- alarm latency for tamper, temperature, missed check-in and pairing;
- battery life.

Each number was put to Omid, and his answer is recorded beside it
(§ The record). He accepted 24 of the 27 as recommended and set three
himself: [3], [12] and [13]. Nothing here has been measured yet. The
numbers are targets that the radio, parts and cost studies check. A
figure marked **ASSUMED** is an input to the arithmetic, not a
measurement. A claim marked **INFERRED** was reasoned, not read from a
source.

## Ground rules

- **Set fresh, never copied.** The 2005/2017 datasheets give range, area
  and memory figures that conflict with each other. One reader's floor
  area is given as both 30,000 and 245,000 sq ft. Tag range is given as
  both 100–140 ft and 85 m, and memory as both 256 and 128 Kbit
  (onboarding spec, Appendix B; the datasheets themselves are in the
  reference corpus's capture of the old website). Those figures are the
  old system's claims. Story 2.1.2 reads them as such, and none of them
  is a requirement here.
- **No medical claim** (spec C8). The nursery scenario proves one thing:
  staff can find each tag and read its temperature sensor. The system is
  not a medical device and not a monitor of vital signs. Its temperature
  alarm means "this tag's sensor reading crossed a limit someone set on
  screen". The warehouse uses the same rule for cold-chain goods.
- **A target that cannot be met comes back, it is not quietly lowered.**
  The radio law (Story 2.1.3) or the parts (Story 2.1.4) may rule out a
  number. If one does, that study brings it back to Omid with what
  meeting it would cost, before anything changes here.
- **One design serves all three scenarios.** Where the scenarios differ,
  the strictest number binds the parts they share. § What binds the
  design shows which number that is, measure by measure. Battery life is
  the exception: it depends on the tag and its duty cycle, so each tag
  style is bound by the scenario that uses it.
- **Capacity is not a read rate.** A tags-per-second number is what a
  reader can take in a burst: a hall emptying, a ward checked at once,
  replies retried after errors. It does not mean every tag replies every
  second. That would cost hundreds of mAh a year (§ The arithmetic),
  beyond any coin cell. The battery numbers stand on their own stated
  duty cycles.
- **The scenarios are remembered, not recorded.** They describe
  deployments of the original system as they are remembered, and no
  measurement of them survives. Each scenario's size is therefore an
  assumption, put to Omid with the numbers (items 1, 10 and 19).

## The five measures, defined

Each definition says how the number will be tested once boards exist.
The tests are written for the build, not for this study.

1. **Indoor read range.** The farthest distance at which a reader wakes a
   tag and hears its reply in at least 99 of 100 attempts. The test runs
   indoors, in the kind of building the scenario names, with the tag worn
   or mounted the way that scenario mounts it. Both directions count: the
   wake (reader or Field Generator to tag) and the reply (tag to reader).
   *Test:* carry a tag away from a reader step by step, 100 reads at each
   step.
2. **Tags per second through anti-collision.** How many distinct tags'
   replies a reader receives intact per second when every tag in its
   field answers the same wake. In the warehouse the limit is set per
   radio channel, because readers that share a channel share its airtime.
   *Test:* a known count of tags, one wake, the time until the last tag
   is heard.
3. **Zone accuracy.** Three parts:
   - the size of a zone;
   - the share of reports that place a tag in the right zone;
   - how long a move between zones takes to show on the floorplan.

   *Test:* tags placed in known zones and carried between them, with the
   host's view compared against where they actually are.
4. **Alarm latency.** The time from an event to the alarm on the host's
   screen, for each of four alarms:
   - **tamper:** the tag's tamper input opens (a band cut, a seal broken,
     a case opened);
   - **temperature:** the tag's sensor reading crosses a limit set on
     screen;
   - **missed check-in:** a tag that checks in on a schedule goes silent;
     timed from its last report, so the number includes the check-ins it
     takes to be sure;
   - **pairing:** a tag paired with a partner (a thing with a person, or
     a person with a person) enters a watched zone, usually an exit,
     without that partner.

   *Test:* each event caused on purpose, timed with a stopwatch against
   the screen.
5. **Battery life.** How long one battery lasts in continuous service at
   the scenario's duty cycle. The duty cycle is stated with each number,
   because it drives the answer. *Test:* the tag's measured average
   current against its cell's rated capacity, plus a long soak test on a
   bench.

## The arithmetic behind the numbers

These assumptions feed the battery and throughput reasoning below.
Story 2.1.4 replaces each with the chosen parts' datasheet figures.

- **ASSUMED: the cell.** A coin cell of the CR2032 size, rated about
  220–240 mAh by its makers. Radio pulses cost a coin cell part of its
  capacity, so the study counts **150 mAh** as usable. A CR2450-size cell
  is rated about 600 mAh and is thicker and wider.
  *Replaced 2026-10-27 by Story 2.1.4:* rated 235 mAh (CR2032) and 620 mAh
  (CR2450), each to 2.0 V; 150 mAh stays the planning figure, bounded below
  by 117.5 mAh (`docs/study/parts-and-toolchains.md` § The battery).
- **ASSUMED: one reply** costs about 25 mA for about 3 ms (wake-up, a
  short packet and back to sleep), which is roughly **0.02 µAh**. A tag
  replying every second would spend about 630 mAh a year on replies
  alone (86,400 a day). A faster link shortens the packet, but the
  wake-up stays, so the figure stays in the hundreds of mAh.
  *Replaced 2026-10-27 by Story 2.1.4:* 18.09 µC (0.0050 µAh) a reply, its
  start-up included (`docs/study/parts-and-toolchains.md` § The battery).
- **ASSUMED: listening.** A tag listening for a wake, duty-cycled so that
  it answers within about a second, averages about **8 µA**, sleep
  included. Over a year that is about **70 mAh**, and it dominates every
  scenario's battery budget. Listening less often (answering within a few
  seconds) cuts it to about 2–3 µA.
  *Replaced 2026-10-27 by Story 2.1.4:* 13.9 µA at a listen every 1 s,
  7.3 µA every 2 s and 1.8 µA every 12 s
  (`docs/study/parts-and-toolchains.md` § The battery).
  *Re-priced 2026-10-28 by Story 2.1.10:* those figures are for the
  Reader's beacon and two Field Generator slots. A tag now listens to its
  zone's slot and its neighbours', and each more slot adds 2.3 µA at a 1 s
  listen (`docs/study/radio-and-location.md` § Zones;
  `docs/study/parts-and-toolchains.md` § The battery).
  *Re-priced 2026-10-29 by Story 2.1.12:* most listens leave out the
  Reader's beacon, which a tag hears after each check-in. A tag that hears
  two slots in one run listens for 8.13 µA at a listen every 1 s, and each
  more slot in the run adds 2.3 µA (`docs/study/radio-and-location.md`
  § Zones; `docs/study/parts-and-toolchains.md` § The battery).
- **INFERRED: a channel's capacity at 250 kbit/s.** A short reply of
  about 30 bytes, preamble included, lasts about 1 ms on air. A scheme
  where the reader assigns reply slots can carry a few hundred replies a
  second on one channel. A purely random scheme (each tag waits a random
  delay, then sends) peaks at about 18% use of the channel, which is the
  textbook ALOHA limit: under 200 replies a second. The original's
  "random delay" was a scheme of that family (onboarding spec, Context;
  Story 2.1.2 reads how it really worked, and at what data rate). At
  slower data rates both figures fall in proportion.
- **INFERRED: a fast link, for [3]'s 1,000 tags/s.** At about 1 Mbit/s,
  a reply cut to its essentials (preamble, sync, ID and a check: about
  12 bytes) lasts about 0.1 ms, or about 0.3 ms with the radio's
  turnaround. That allows about 3,000 replies a second with
  reader-assigned slots, or about 1,200 with random slots. A reader that
  listens on several channels at once is the other route. The price is
  range: a receiver's sensitivity falls with its bandwidth, roughly 10 dB
  from 100 kbit/s to 1 Mbit/s, which works against [2]'s 30 m with the tag
  worn. Story 2.1.3 weighs that trade against the radio law.

## Scenario 1 — the conference

Every attendee wears a tag, as a bracelet or a badge. The host software
shows each person on the venue's floorplan.

**[1] Size — ASSUMED: 1,000 attendees in one venue of about ten rooms,
with a main hall of about 40 m × 30 m.** The numbers below scale from
this size.
**Omid's answer:** as recommended.

**[2] Indoor read range — 30 m, the tag worn on the body, in an open
hall.** A reader on one wall of a 40 m × 30 m hall then reaches most of
the room, and a second reader covers the rest. A body between tag and
reader absorbs a large part of the signal, roughly 10–20 dB at 900 MHz
(**ASSUMED**). The number is therefore set with the tag worn, not
hanging in free air, where every radio looks better than it will in use.
**Omid's answer:** as recommended.

**[3] Tags per second — 1,000 tags/s per reader: a full hall's 1,000 tags
read within about a second, with time left over for retries.** The study
recommended 100 tags/s, which reads the hall in 10 s. Omid set 1,000,
because 10 s for 1,000 people is a long time once errors and their
retries are counted. **Can it be done?** By the arithmetic, plausibly,
but not the way the original did it (**INFERRED**). One channel at
250 kbit/s cannot do it: a 30-byte reply alone fills the 1 ms each tag
gets. A fast link with short replies and reader-assigned slots can, and
so can a reader listening on several channels (§ The arithmetic). Its
price is some range and a more complex reader, and Story 2.1.3 verifies
it. The number is capacity, not a read rate: [9]'s battery stands on its
own duty cycle (§ Ground rules).
**Omid's answer:** "I want 1000 tags/s per reader, 10s for 1000 people —
especially if we have errors is a lot. Can we do that?" Recorded as
1,000 tags/s per reader. The answer to his question is the paragraph
above, given to him in the same terminal. If the radio cannot meet the
number, Story 2.1.3 brings it back to him with its cost.

**[4] Zone accuracy — a zone is a room, or a marked area of the main hall
at least 10 m across. The right zone in at least 95% of reports, and a
move shown within 30 s.** Finding a person needs the right room, not the
right metre. 95% leaves room for the occasional doorway confusion any
radio zone has, and 30 s is about the time it takes to walk from one room
to the next.
**Omid's answer:** as recommended.

**Alarm latency:**

| # | Alarm | Number | Reasoning | Omid's answer |
|---|---|---|---|---|
| [5] | Tamper | 10 s | A bracelet cut off, or a badge opened and passed to someone else, quietly breaks the floorplan, so staff should know before the person walks far. | as recommended |
| [6] | Temperature | not required | Conference tags carry no temperature sensor, so the alarm does not arise here. The nursery's [15] binds the design. | as recommended |
| [7] | Missed check-in | 5 min | A silent badge was usually left behind or its battery is dead, and nobody needs to know within seconds. One check-in a minute, alarming after five missed, costs the battery little. | as recommended |
| [8] | Pairing | 15 s | A loaned item (an interpreter's headset receiver, a laptop) paired with its borrower alarms when it reaches an exit zone without them. 15 s is about 20 m of walking, still within sight of the door. | as recommended |

**[9] Battery life — at least 1 year, on a coin cell, checking in once a
minute plus one report per zone change.** By the arithmetic, listening
takes about 70 mAh a year and check-ins about 11 mAh (1,440 a day). Zone
changes add little, so the total is about 80–90 mAh a year: around 1.7
years from 150 mAh. One year leaves margin for cold, ageing and the
smaller cell a bracelet may need, and a yearly battery change fits an
events calendar.
**Omid's answer:** as recommended.

## Scenario 2 — the nursery

Each baby's tag carries a temperature sensor. Staff can find each baby,
with no mix-ups, and read each tag's sensor from the host. This scenario
proves finding tags and reading a sensor. It is not a medical device or a
monitor of vital signs (spec C8).

**[10] Size — ASSUMED: about 100 tags: 30 babies' tags plus about 60
worn by the adults they are paired with (parents and staff). One ward of
about 20 rooms off a corridor, with two exits.**
**Omid's answer:** as recommended.

**[11] Indoor read range — 15 m through one interior wall.** Rooms of
4–6 m open off a corridor, and a reader in the corridor has to reach the
far side of each room through its wall. The distance is shorter than the
conference's, but a wall now stands in the way.
**Omid's answer:** as recommended.

**[12] Tags per second — 100 tags/s: the ward's 100 tags all read within
1 s.** The study recommended 20 tags/s, which reads the ward in 5 s. Omid
set 100, so the whole ward is read in a second. The conference's [3] is
stricter and binds the design, so this asks nothing more of the radio.
Like [3], it is capacity, not a read rate: [18]'s battery stands on its
own duty cycle.
**Omid's answer:** "I want 100 tags/s so that assumed room is read in 1
second" (the ward assumed in [10]). Recorded as 100 tags/s.

**[13] Zone accuracy — a zone is a room. The right room in at least 99%
of reports, every pass through an exit zone detected (100 of 100 test
walk-throughs at walking pace), and a move shown within 5 s.** "No
mix-ups" means a baby's tag must not appear in the wrong room. The exit
zone is where the pairing alarm fires, so a missed pass there is a missed
alarm. That is why the exit carries the strictest test in the study. The
study recommended a move shown within 10 s, and Omid set 5 s. A tag that
listens for a wake about once a second and reports when its zone changes
can meet 5 s without checking in more often, so [18]'s battery holds
(**INFERRED**). Story 2.1.3 designs how a move is noticed.
**Omid's answer:** "I want a move shown in 5s." Recorded as a move shown
within 5 s; the room and exit figures as recommended.

**Alarm latency:**

| # | Alarm | Number | Reasoning | Omid's answer |
|---|---|---|---|---|
| [14] | Tamper | 5 s | A cut or removed band breaks the only link between a baby and their tag. This is the strictest tamper number of the three. | as recommended |
| [15] | Temperature | 60 s | The tag reports its sensor with every check-in, every 20 s ([18]), so the alarm still arrives within a minute even when two reports are lost. It is the same sensor-against-a-limit rule the warehouse uses ([24]). | as recommended |
| [16] | Missed check-in | 60 s | A baby's tag that goes silent may be shielded or broken, and staff should know within a minute. One check-in every 20 s, alarming after three missed. | as recommended |
| [17] | Pairing | 5 s | A baby's tag entering an exit zone without its paired adult's tag. 5 s at walking pace is about 7 m, so an exit zone set back that far from the door alarms before the door is reached. | as recommended |

**[18] Battery life — at least 1 year, on a coin cell, checking in every
20 s with the temperature reading carried in each check-in.** By the
arithmetic, listening takes about 70 mAh a year and 4,320 check-ins a day
about 32 mAh. That is about 100 mAh a year, or around 1.5 years from
150 mAh. The tags are reused across many stays and cleaned between them,
so the battery is changed at a planned yearly service, never mid-stay.
*Priced 2026-10-28 by Story 2.1.10:* a tag that stays in the rooms holds
the year on paper by 1.8 %. One that walks the corridor more than about
37 minutes a day does not, and the parts study names the routes, measuring
first and then a CR2450 (`docs/study/parts-and-toolchains.md` § Each worn
tag's year). The number is unchanged.
*Re-priced 2026-10-29 by Story 2.1.12:* a tag that stays in the rooms holds
the year on paper by 24 %. One in the corridor more than about 5.4 hours a
day does not, and the same routes stand (`docs/study/parts-and-toolchains.md`
§ Each worn tag's year). The number is unchanged.
**Omid's answer:** as recommended.

## Scenario 3 — the warehouse

Every item carries a tag, and the whole stock of 100,000 or more items is
inventoried in minutes, not over a weekend of hand counting.

**[19] Size — ASSUMED: 100,000 tagged items in one building of about
100 m × 100 m with metal racking and several dock doors. Some items are
cold-chain goods whose tags carry a temperature sensor.**
**Omid's answer:** as recommended.

**[20] Indoor read range — 30 m among metal racking, with the tag on an
item on a shelf.** Racks and stock reflect and block radio. A reader for
every bay of about 30 m (roughly 9–12 readers for the building) is cheaper
and easier to debug than one radio strong enough to cross the whole
building. The old datasheets' 85 m is not copied (§ Ground rules).
**Omid's answer:** as recommended.

**[21] Tags per second — 200 tags/s on one radio channel: 100,000 items
inventoried in at most 10 minutes, with at least 99.9% read in the pass
(at most 100 missed, which a second pass finds).** 100,000 items in 600 s
is about 167 tags/s, so 200 leaves margin. The limit is per channel
because readers that share a channel share its airtime. By the
arithmetic, a purely random-delay scheme peaks just under 200 replies a
second even at 250 kbit/s, and lower at slower rates (**INFERRED**). This
number therefore pushes the design toward reader-assigned reply slots, a
faster radio link or several channels; Story 2.1.3 decides which. A
reader that meets the conference's [3] on one channel meets this too.
**Omid's answer:** as recommended.

**[22] Zone accuracy — a zone is an aisle or a bay of about 10 m. The
right zone in at least 95% of reports, and a move shown within 5 min.**
Finding an item means walking to its aisle. Items move by forklift, and
the next inventory pass finds them again.
**Omid's answer:** as recommended.

**Alarm latency:**

| # | Alarm | Number | Reasoning | Omid's answer |
|---|---|---|---|---|
| [23] | Tamper | 30 s | A seal tag on a case or container opened. A broken seal is a loss to investigate, rarely something to chase. | as recommended |
| [24] | Temperature | 5 min | Cold-chain goods warm over minutes, not seconds. The tag reads its sensor every 2 min, so one lost reading still alarms within 5 min. | as recommended |
| [25] | Missed check-in | 1 h | An item that stops answering has left coverage or its tag has died. One check-in every 15 min, alarming after four missed, costs the battery little. | as recommended |
| [26] | Pairing | 30 s | An item paired with its handler or vehicle reaches a dock-door zone without that partner. Loading at a dock takes longer than 30 s. | as recommended |

**[27] Battery life — at least 5 years, with one inventory a day, a
check-in every 15 min, and a temperature reading every 2 min on cold-chain
tags only.** At 100,000 tags, battery life decides the workload. A 1-year
battery means about 274 battery changes every day, and a 5-year battery
about 55. By the arithmetic, listening alone would take about 350 mAh over
5 years at 8 µA, more than a CR2032 gives. The warehouse tag therefore
needs either a slower wake (a few seconds is fine for an inventory, about
2–3 µA) or a larger cell (CR2450-size, about 600 mAh rated). Story 2.1.4
chooses between them.
*Chosen 2026-10-27 by Story 2.1.4:* the slower wake, a listen every 12 s,
which keeps five years on a CR2032 on paper; a CR2450 alone does not. The
CR2450 is recommended as well, for margin
(`docs/study/parts-and-toolchains.md` § [27]). The number is unchanged.
*Re-priced 2026-10-28 by Story 2.1.10:* the 12 s wake keeps five years on
a CR2032 for the plain item tag, by 4 %, and not for the cold-chain one
(0.96 of five years), on which the CR2450 is recommended
(`docs/study/parts-and-toolchains.md` § [27]). The number is unchanged.
*Re-priced 2026-10-29 by Story 2.1.12:* the 12 s wake keeps five years on a
CR2032 for both item tags, by 22 % and, on cold chain, by 11 %. The CR2450
stays recommended for margin wherever the item takes it
(`docs/study/parts-and-toolchains.md` § [27]). The number is unchanged.
**Omid's answer:** as recommended.

## What binds the design

| Measure | Conference | Nursery | Warehouse | Binds the shared design |
|---|---|---|---|---|
| Indoor read range | 30 m, worn, open hall | 15 m through a wall | 30 m among racks | all three: one radio, three conditions |
| Tags per second | 1,000 per reader | 100 | 200 per channel | the conference |
| Zone accuracy | room or area ≥ 10 m; 95%; 30 s | room; 99%; exits 100 of 100; 5 s | aisle ≈ 10 m; 95%; 5 min | the nursery |
| Tamper latency | 10 s | 5 s | 30 s | the nursery |
| Temperature latency | not required | 60 s | 5 min | the nursery |
| Missed check-in latency | 5 min | 60 s | 1 h | the nursery |
| Pairing latency | 15 s | 5 s | 30 s | the nursery |
| Battery life | ≥ 1 year | ≥ 1 year | ≥ 5 years | per tag style: the warehouse binds the item tag; the conference and nursery bind the worn tags |

Read across:

- the conference sets how fast a reader takes replies;
- the warehouse sets the volume and the item tag's battery;
- the nursery sets the certainty of zones and the speed of alarms.

The hardest pairing is [3] against [2]: a reader that takes 1,000
replies a second and still hears a worn tag 30 m away. Story 2.1.3
answers it first.

## The record

The numbers [1]–[27] were put to Omid as one numbered list in the
Story 2.1.1 session's terminal on 2026-10-27, each with its reasoning in a
line and a recommendation. The list went out after that terminal's
`date -u` read of 11:57:23Z. His answer came as one message, before the
read of 12:05:05Z, and is quoted here whole:

> All as recommended except for:
>
> 3. I want 1000 tags/s per reader, 10s for 1000 people — especially if we have errors is a lot. Can we do that?
> 12. I want 100 tags/s so that assumed room is read in 1 second
> 13. I want a move shown in 5s.

Each answer is recorded beside its number above, first-hand.
