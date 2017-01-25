# Optimal Tag Values

**Note:** These are the values from testing each tag that resulted in an optimal result. We included the "Deprecated" portion in the event there was something from those old results that may be useful in the latest results. Eventually, we would hope to remove that sectionand leave only the latest optimal values.

## 868.92Mhz Tags

_05/12/2011_

### compactTag, 102 Rev 17

Variable | Value
--- | ---
C2 | 4.0pf
C7 | 220pf
C8 | 1000pf
C13 | 33pf
C19 | 3.0pf
C21 | 1.8pf or 2pf
L2 | 6.8nH
L4 | 12nH
L6 | 100nH
R17 | 1.3k ohms

### jumboTag, 105 Rev 17

Variable | Value
--- | ---
C2 | 2.2pf
C7 | 220pf
C8 | 1000pf
C13 | 33pf
C19 | 5.6pf
C21 | 1.8 or 2pf
L2 | 6.8nH or 8.2nH
L6 | 100nH
R17 | 1.3k ohms

### miniTag, Rev 20

Variable | Value
--- | ---
C2 | 2.4pf
C7 | 220pf
C13 | 33pf
C19 | 4.7pf
C21 | 1.8pf or 2pf
L1 | 15nH
L2 | 5.6nH
L6 | 100nH
R17 | 1.3k ohms
R24 | 150k ohms

## 867Mhz Tags (India)

_05/10/2011_

### compactTag, 102 Rev 17

Variable | Value
--- | ---
C2 | 4.0pf
C7 | 220pf
C8 | 10pf
C10 | 2700pf
C13 | 47pf
C19 | 3.0pf
C21 | 1.8pf or 2pf
L2 | 6.8nH
L4 | 12nH
L6 | 100nH
R17 | 1.3k ohms

### jumboTag, 105 Rev 17

Variable | Value
--- | ---
C2 | 2.2pf
C7 | 220pf
C8 | 1000pf
C10 | 1000pf
C13 | 33pf
C19 | 5.6pf
C21 | 6.0pf+
L2 | 6.8nH or 8.2nH
L6 | 100nH
R17 | 1.3k ohms

### miniTag, 103 Rev 20

Variable | Value
--- | ---
C2 | 2.4pf
C7 | 220pf
C8 | 10pf
C10 | 2700pf
C13 | 47pf
C19 | 4.7pf
C21 | 6pf or 7pf
L1 | 15nH
L2 | 5.6nH
L6 | 100nH
R17 | 1.3k ohms

## 916.5Mhz Tags

_05/12/2011_

### compactTag, 102 Rev 17

Variable | Value
--- | ---
C2 | 3.6pf
C7 | 220pf
C8 | 1000pf
C13 | 33pf
C19 | 2.7pf
C21 | 1.8pf or 2pf
L2 | 6.8nH
L4 | 12nH
L6 | 100nH
R17 | 1.3k ohms

### jumboTag, 105 Rev 17

Variable | Value
--- | ---
C2 | 2.7pf
C7 | 220pf
C8 | 1000pf
C13 | 33pf
C19 | 5.6pf
C21 | 1.8 or 2pf
L2 | 6.8nH or 8.2nH
L6 | 100nH
R17 | 1.3k ohms

### miniTag, Rev 20

Variable | Value
--- | ---
C2 | 1.5pf
C7 | 220pf
C8 | 1000pf
C13 | 33pf
C19 | 4.7pf
C21 | 1.8pf or 2pf
L2 | 5.6nH
L4 | 5.6nH
L6 | 100nH
R17 | 1.3k ohms
R24 | 150k ohms

## 927.2Mhz Tags

_05/12/2011_

### compactTag, 102 Rev 17

Variable | Value
--- | ---
C2 | 3.0pf
C7 | 220pf
C8 | 1000pf
C13 | 33pf
C19 | 2.4pf
C21 | 1.8pf or 2pf
L2 | 6.8nH
L4 | 12nH
L6 | 100nH
R17 | 1.3k ohms

### jumboTag, 105 Rev 17

Variable | Value
--- | ---
C2 | 2.7pf
C7 | 220pf
C8 | 1000pf
C13 | 33pf
C19 | 4.6pf
C21 | 1.8 or 2pf
L2 | 6.8nH or 8.2nH
L6 | 100nH
R17 | 1.3k ohms

### miniTag, 103 Rev 20

Variable | Value
--- | ---
C2 | 1.8pf
C7 | 220pf
C8 | 1000pf
C13 | 33pf
C19 | 3.0pf
C21 | 1.8pf or 2pf
L2 | 5.6nH
L4 | 5.6nH
L6 | 100nH
R17 | 1.3k ohms

## Special Notes

* **jumboTag and compactTag:** Add a 1M ohms resistor to the base of Q4 and the other side of the resistor goes to ground. This should increase the response time of the tag. Also, adding a 22uf cap - one side to the positive of the battery holder and the other side to ground - will increase the performance of the buzzer.

* **Tag 103:** To have a strong 433.92MHz signal, you need to change C7 from 100pf to 220pf (the change will get rid of the narrow signal) and L5 from 0.33uH to 100nH. For 868.92MHz tags, change C8 from 1000pf to 10pf and L1 to 15nH. For 916.5MHz and 868.92Mhz tags, change C19 to 4.7pf.

### Deprecated

#### 868.92MHz - cardTag, 101 Rev 13

Variable | Value
--- | ---
C2 | 2.7pf
C19 | 5.6pf
L2 | 6.8nH

#### 868.92MHz - jumboTag, 105 Rev 8

Variable | Value
--- | ---
C2 | 2.2pf
C7 | 220pf
C19 | 5.6pf
L2 | 6.8nH or 8.2nH
L6 | 100nH

#### 868.92MHz - miniTag, 103 Rev 8

Variable | Value
--- | ---
C2 | 1.8pf
C19 | 4.7pf
L1 | 2.7nH
L2 | 5.6nH

#### 916.5MHz - cardTag. 101

Variable | Value
--- | ---
C2 | 2.7pf
C19 | 5.6pf
L2 | 6.8nH

#### 916.5MHz - compactTag, 102

Variable | Value
--- | ---
C2 | 4.0pf
C19 | 3.0pf
L2 | 6.8nH
L4 | 12nH

#### 916.5MHz - jumboTag, 105

Variable | Value
--- | ---
C2 | 2.7pf
C7 | 220pf
C19 | 5.6pf
C21 | 1.8pf
L2 | 6.8nH or 8.2nH
L6 | 100nH

#### 916.5MHz - miniTag, Rev 8

Variable | Value
--- | ---
C2 | 2.7pf
C19 | 3.6pf
L1 | 2.7nH
L2 | 4.7nH

#### Special Notes

* **For tag 103 and metal surfaces:** Changing C2 from 2.7pf to 2.0pf, C19 from 3.6pf to 5.6pf, and L2 from 4.7nH to 5.6nH, the tag can work on metal with a wristband and it also increases the read range of the tag.

* **For tag 103 and a narrow signal:** Changing C7 from 100pf to 220pf, L6 from .33uH to 100nH, L1 to 15nH, while on 868.92Mhz, changing C8 to 10pfz, will allow you to have a strong 433.92 signal. It will also get rid of the narrow signal from receiver and give you a better and nicer shaped signal.

* **Reducing noise levels:** If you ever have a noise issue, adjust the threshold on R11 to reduce the noise level so we do not have any distraction from the Reader.
