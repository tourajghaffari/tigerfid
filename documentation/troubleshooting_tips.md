# Tags - Troubleshooting

**Note:** These are results that were located in a document. Eventually, we would hope a standardized troubleshooting/testing setup will be created and this document can be archved.

**Note:** This setup is for a Reader or use of a Reader and Smart Field Generator.

## RSSI Changes

Variable | Value
--- | ---
C51 | 10ohms
R47 | 1000ohms
R10 | 10ohm
R2 | DNP

_Note:_ Tune the sensitivity on the oscilloscope until you get the signal.

## Modification Procedures

**Perform these steps before performing any testing.**

### Transceiver Modification Procedures

1. C46 should be changed from 6pf to 8pf. This will provide better results if you have any transmitting signal problems on either 868.92MHz or 916.5MHz.

2. For the old transceiver: Cut the trace in both side of L13 and add an 18nH that actually replaces L13.

3. Threshold has to be set to 1.67K ohms in order to have good communication between the Reader and the Smart Field Generator.

4. Do not populate R6 which is a 10K ohms resister in any transceiver, no exception.

**These changes are optional.**

### Extend the Read Range on the Transceiver

1. R3 should be changed from a 22ohms resistor to a 10ohms resistor

2. L2 should be changed from 18nH to 8.2nH.

3. For TRX 141, Rev 19, changing C33 from 2pf to 2.7pf and C35 from 7pf to 10pf, the read range of the transceiver was extended.

### Extend the Read Range on the Tag

* Change R24 from 100K ohms to 150K ohms.

_Note: This will increase power consumption by a significant amount._

### Tilt Switch Modification

1. Change C16 to 0.01uf, C25 to 27pf, R25 to 39K, and R27 to 10M in order to make the tag work.

2. After making those chagnes, enable the tilt switch and the tamper control before programming the tag.

### Indian Market Modifications

_Note: To tune 867MHz to 335MHz, change C33 to 2pf and C35 to 18pf._

#### Tag 105

1. Make sure the saw resonator is 867MHz.

2. Make sure the transmitter cap C2 has been changed to 2.7pf, C19 to 4.7pf, and C21 to 5.0pf or higher.

3. In order to tune the 867MHz tag to a 335MHz receiver, C2 to 2.2pf, C10 should be changed to 2700, C19 to 5.6pf, C21 to 6.2pf (or around there), and R17 to 1.3K ohms.

#### Field Generator

1. Make sure C10 has been changed to 100pf, C14 to 18pf, C15 to 5pf, C37 to 2.2pf, L4 to 15nH, L10 to 220nH, and L12 to 22nH.

2. C8 should remain 2pf.

#### compactTag

* Make sure C2 has been changed to 4.0pf, C7 to 220pf, C13 to 33pf, C19 to 3.0pf, C21 tp 6.21pf or high, L2 to 6.8nH, L4 to 12nH, L6 to 100nH, and R17 to 1.3K ohms.

#### Compact Flash Card

1. Make the following changes:

Variable | Value
--- | ---
C28 | 100pf
C30 | 5pf
C33 | 2pf
C35 | 18pf
C45 | 2.2pf
L3 | 18mH
L7 | 220nH
L8 | 100nH
L9 | 22nH
L12 | 15nH
R23 | 6.8k ohms
R24 | 2.7K ohms
R27 | 51ohms
R30 | 10 ohms

_Note: match the threshold to 1.702K ohms when measuring._

### Compact Flash Card Reader (All)

1. The threshold should be with the 5k pot equal to 1.5K ohms, or sometimes a little bit more based on how you direct your hand. Also the 2K pot should euqal 2.0k or a little more, depending on how well the pot is calibrated.

2. When R27 equals 51 ohms, it creates a short from Q10 and R27, which casuses the handheld to perform in unexpected ways. The read range is very short because of the value of R27. To solve that problem, you need to go ahead and change the value from 51 ohms to the original value of 560 ohms and threshold to 2k ohms.

### Field Generator Modification

* To change from low to high power and still be able to read the tags, chagne R5 from 470 ohms to 6.8K ohms. This will make a significant difference on power consumption.

### 868.92Mhz Transceiver Modification

1. Changing C56 to 4.7pf and L19 to 5.6nH, a filter is added to reduce teh noise level.

2. After performing a complete test, change C33 to 3pf, C35 to 10pf, R24 to 2.7K ohms, and R27 to 51 ohms. This will increase the read range from a shorter distance to a longer distance. The threshold should be equal to 1.67K ohms on 5K and 2.0K on R7.

_Note: Changing C33 and C35 is for 433.92MHz - it allows the signal to move closer to the center when measuring or testing the trtansceiver._

### 905.8 MHz Transceiver Modification

1. Change L5 to 5.6nH, C19 which is the one connected directly to L5 to 22pf, C9 to 0.5pf, and C23 to 8pf

2. Leave C19 empty.

### 916.5Mhz Transceiver Modification

1. Changing C56 to 4.7pg nd L19 to 5.6nH adds a filter and reduces the noise levels.

2. After performing a complete test, change C33 to 3pf, C35 to 10pf, R24 to 2.7K ohms, and R27 to 51 ohms. This will increase the read range from a shorter distance to a longer distance. The threshold should be equal to 1.67K ohms on 5K and 2.0K on R7.

_Note: Changing C33 and C35 is for 433.92MHz - it allows the signal to move closer to the center when measuring or testing the trtansceiver. If after having performed these modifications and the transceiver still doesn't work, change the 433.92 saw resonator._

### Field Generator Modifications

* Change R5 from 470 ohms to 6.8K ohms and R19 to 2.7K ohms. This will increase the field generator's performance.

### Conversion of a compactTag to a Field Geneartor

By adding a Field Generator microchip to a compactTag, it will allow the compactTag to function like a regular Field Generator.

1. All the pins except for 1, 4, 7, 16, 17, 18, 19, and 20 must be cut from the IC must be cut before soldering.

2. Replace Y1 from either a 868.92MHz saw resonator or a 916.5MHz saw resonator to a 433.92MHz saw resonator.

3. Make the following changes:

Variable | Value
--- | ---
C2 | 12pf
C19 | 10pf
R7 | 36.5K

4. Pin 16 from the processor must be connected to the top of R2.

5. Run a wire from the bottom of R26 to pin 7 of the microchip.

6. Run a wire from R25 to the top of R5.

7. The tilt switch component requires C16 to be 0.1uf, C25 to be 2200pf or 2.2nf, R25 to be 100K, and R26 to be 10M. Also, the tilt switch should be on the bottom.

8. Based on the value of the cap used for C2, it should be 12pf or somewhere around there.

9. By replacing C2 from 12pf to either a higher or lower value, you will change the position of the signal. If you put a lower value, the signal will shift to the right of the spectrum analyzer and a higher value shift to the left of the spectrum analyzer.

10. The reason for changing C2 is because we want to tune the 433.92MHz frequency to the center. By doing sow, we end up with the following values:

_Leave the antenna open on all of these._

Tags 1, 3, and 4

Variable | Value
--- | ---
C2 | 12pf
C19 | 10pf
L4 | 10nH
R6 | 120 ohms
R7 | 43K ohms

Tag 2

Variable | Value
--- | ---
C2 | 12pf
C19 | 10pf
L4 | 10nH
R6 | 120 ohms
R7 | 47K ohms


## Troubleshooting Procedures

### 868.92Mhz transceiver

1. Threshold always set to 3.6volts with this setup - the sensitivity should not be significantly affected.

2. Sensitivity will have to be around -97.5 or higher in order to have a good read range.

3. When checking the 433.92MHz, pay very close attention to see where the signal is located. Based on my reading, I was able to see the 433.92MHz go as high as 49.2DBM.

4. The transmit signal has reached as high as -9.7DBM.

5. The threshold should always be set at 4.3volts with this set up - the transceiver should always provide good results.

### 916.5Mhz transceiver

1. Threshold always set to 4.3volts with this setup - the sensitivity should not be significantly affected.

2. Sensitivity will have to be around -97.5 or higher in order to have a good read range.

3. When checking the 433.92MHz, pay very close attention to see where the signal is located. Based on my reading, I was able to see the 433.92MHz go as high as 49.2DBM.

4. The transmit signal has reached as high as -9.7DBM.

5. The threshold should always be set at 4.3volts with this set up - the transceiver should always provide good results.

## Troubleshooting Tips

### Reader

1. When there’s no communication from the Reader whenever you try to call a tag or L3 is very hot, check C14 to make sure it’s not backwards. If it is, change it to match the polarity.

2. When the Reader is plugged in and the system does not recognize it, meaning that you don’t hear any noise from the system, make sure the two transistors on the bottom of the C memory chip are properly soldered.

3. If there is no response from the Reader when calling the tag, first check to see if there’s unsoldered components. If so, solder them and recheck the Reader.

### Transceiver

1.  Sensitivity cannot tune anything lower than 40 or 50 DBM.

2. Check cap by J3 to make sure it is 100pf. If you are unsure, change it to 100pf to be sure.

3. If after changing it, it still does not work, change U1 which is BGA 2001

4. If you are still having problems, check Y1 and Y2 for missing solder or just change them both.

### Weak signal

1. Make sure the cap by J3 is 100pf, then retune the transceiver once again.

2. If the transceiver is still weak, change U4.

3. If the transceiver is still weak, change U3.

### 433.92MHz does not transmit

1. Check for unsoldered components in the receiver area.

2. If that does not work, change Q5 above C32.

### Transceiver remains at 0 volts

1. Check the polarity on Y1 and Y2 and make sure they are both mounted with the correct side of polarity on the board.

2. If that does not work, change both of them at the same time.

### Threshold does not adjust

1. On Rev 16, change U4 first before changing U3.

2. If threshold does not adjust, double-check C24 to make sure it is 1pf. 

### Short-range Transceiver modification

1. C8 and C19 needs to be changed to .5pf.

2. C2 needs to be changed to 2pf, but it may sometimes vary.

### Reader does not reset

1. Inspect the entire Reader and look for unsoldered parts

2. Inspect the entire Reader and look for shorts

_Note:_ Focus mainly on the two resistors above the E chip.

### Transceiver does not transmit

1. Check for unsoldered part in the transmitter area.

### Transceiver has a very short range

1. Change all the caps - C38, C39, and C2 - to .5pf

2. Change C34 to 27pf and remove the antenna connector

3. Add a 50ohms resistor across or reduce the threshold.

### compactTag, 102 does not respond

1. Change Q3 (which is the transmitter) and then the C3 cap, which plays a big role on the tag (it allows you to tune it to the exact frequency you want from 433MHz to 440Mhz).

2. If there is no receiver signal, check for areas lacking solder or parts in the receiver area.

3. If that doesn't fix it or if the tag doesn't respond, change the Q2 transistor, which controls the receiver and transmitter signal.

### A 102 tag doesn't have data

* U6 could be bad, especially if the tag has been used before.

### Tag 103 Rev 18 issues

1. When placing a tag on a fixture, you should have perfect signal - there shouldn't be any shorts.

2. If there is a short, remove the battery holder next to the positive.

3. If you see a test-point, remove it to prevent a short between the ground and the positive.

### Tag 105 issues

1. Make sure there is enough solder paste.

2. Make sure R21 and U2 are installed and that U2 and U5 are not off pad- these cause issues with the receiver area.

3. If there is no transmitter signal, make sure the 1000pf from C8 and C10 do not have a manufacturing defect. One perfect way you can manage that is by touching the end of the cap with a soldering tap - if it cracks in half, it means the cap is bad and you need to replace it with a new one and recheck the tag. Consider a new manufacturer if the problem is persistent.

4. If the 433.92MHz signal does not appear, C4 - which is .1uf - may be bad. Touch the end of one side with your hot soldering iron - if it cracks in half, it means the cap is bad and you need to replace it with a new one and recheck the tag. Consider a new manufacturer if the problem is persistent.

5. If the 433.92MHz signal looks strange or narrow, chances are C10 - which is 100pf - may be bad and need to be changed.

6. When placing the battery, if you do not see the tag on the screen, your program is corrupted. Reprogram the tag.

### Buzzer issues

1. If the buzzer on any tag is not beeping, chances are you have a low voltage battery or Q4 may be bad.

2. If the buzzer's volume is very low, replace the battery with a higher voltage one.

3. If either the buzzer or the LED (or both) do not work, first change the components around them. If that doesn’t fix it, replace the buzzer.

### Reader keeps automatically resetting

1. Change the switch - a bad switch will put the Reader in an automatic reset loop.

2. If the issue continues, it may be the RS232, itself.

3. If one of the processors has a bridge, it will cause the automatic reset loop. Re-solder the processor one-by-one, starting with the D processor.

### Reader LED keeps flashing

1. If one of the LEDs keeps flashing when programming the Reader, the switch may be bad and need to be changed. Use the same method when you plug in the tag and no sound comes out of it, meaning that you plug in the tag and you do not hear any sound from the computer.

### Compact Flash Card

_Note: When measuring the threshold on the CF card, when you power down the reader, the reading from the top side of R11 should be 1.9K or 2K. When you power it back up, you should have a reading of 4.6K to 4.8K from the top side of R11._

### Special Notes For new CF Card Reve0: Upgrade

1. After performing a complete test, we realized that some changes need to be applied on the transceiver. R23 should be 3.3K ohms, C33 should be 2pf, C35 should be 7pf, L8 should be 47nH or 60nH, R48 should be 2.7K ohms, C56 shold be 4.7pf, and L19 should be 5.6nH.

2. With the digital area populated on the CD Card, there will not be a long read range. However, removing R113, R114, R115, R116, R117, R122, and R123, it should be fine.

### Transceiver Sensitivity Issues

1. To reduce/remove sensitivity, make sure C9 is equal to 0.5pf, C23 is 8pf, and then populate.

2. When the 905.8MHz frequency transmitter signal is there but not snesitive enough after having changed most parts, change the U8 chip.

3. When the 905.8MHz frequency transmitter signal is not there, sometimes C23 can be a different value, just keep changing from high to low. For example, when C9 is 0.5pf and C23 is 8pf, there is no signal. Changing C23 from 8pf to 3pf will result in signal coming back.

### compactTag

* When Q4 is populated and the buzzer is not populated, calling the tag will not work. Either remove Q4 or populate the buzzer. For the sake of the battery, it is best to remove both.

### compactTag Rev 20

1.	In the receive area, change C7 to 100PF and C3 from 2pf to 7.0pf. This change shifts the signal to the left of the analyzer.

2. C21 should be changed from 1.8pf to 0.5pf or 1pf. This shifts the signal to the right fo the analyzer, allowing you to perform your tuning.

3. Change C13 from 33pf to 27pf. This shifts the signal to the right of the analyzer.

### Latest Reader issues

1. When plugging in the Reader after having populated it, U23 gets very hot. Most likely, the chip is bad. Replace it with DigiKey Part # IS768-1008-ND and MFG P/N: FT232RQ-REEL (IC USB FS SERIAL UART 32-QFN)/

2.	When everything on the reader works but calling a tag does not result in deteing a tag, most likely R69 may be a different value. The value on R69 is supposed to be a 0 ohm resistor.

### Tag becomes detuned

* Sometimes, there is a short in the transmitter that results in the tag being detuned. Please refer to the tag's schematic and check the transmitter area. For example, Tag 105 Rev 20 was detuned, so we looked at the transmitter area. In particular, C2 and C19. By changing C2 from 2.7pf to 1.8pf, it shifted our transmitter signal to the right and the tag performed much better.
