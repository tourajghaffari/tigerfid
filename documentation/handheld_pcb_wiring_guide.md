# Handheld PCB Wiring Guide

**Wiring instructions for programming the microcontrollers on the Handheld Reader PCB**

Notes:

* To program microcontroller C, D and E on the Handheld Reader, you must go to the respective J connector for that controller.

* J101 is for microcontroller C, J102 is for microcontroller D, and J103 is for microcontroller E

* Solder the wires from the 5-pin adaptor connector to the correct J connector of the microcontroller you wish to program.

* Use a female 5-pin connector for programming the Handheld Reader

Pin | Wire
--- | ---
Pin 1 | White wire to J101pin1 or J102pin1 or J103pin1
Pin 2 | Green wire to J101pin2 or J102pin2 or J103pin2
Pin 3 | Purple wire	to J101pin3 or J102pin3 or J103pin3
Pin 4 | Black wire to GND TP20
Pin 5 | Red wire to VCC TP19#RS232 Adaptor Guide

RS232 | USB Cable
--- | ---
Pin 1 | GND (Black)
Pin 2 | NC
Pin 3 | PWR (Red)
Pin 4 | TXD (White)
Pin 5 | RXD (Green)

PCB | Male Pins to PCB
--- | ---
Pin 1 | PWR (Red)
Pin 2 | RXD (White)
Pin 3 | TXD (Green)
Pin 4 | NC
Pin 5 | GND (Black)
