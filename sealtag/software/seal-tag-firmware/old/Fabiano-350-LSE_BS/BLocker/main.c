/*******************************************************************************
* @file     main.c
* @author   imoware
* @version  V3.5.0
* @date     04/13/12
********************************************************************************
* @version history    Description
* ----------------    -----------
* v2.5.0              LSI Clock Enable for RTC (deprecated with 2.7 release)
* v2.6.0              LSI Clock Calibration (deprecated with 2.7 release)
* v2.7.0              LSE Clock Enable for RTC
* v2.8.0              (1) Tag Version, (2) Last Packet Reserved
* v3.0.0              Tamper Switch Debounce Handler
* v3.1.0              Tamper Switch Debounce Handler WITHOUT Timer2
* v3.2.0              Battery Low Voltage History is Updated when Timer is on only
* v3.3.0              on/off
* v3.4.0              Corrected command handling for Factory Reset,
*                     Corrected interrupt handling for Band tamper,
*                     Added check for I2C writes,
*                     Added check for RTC configuration/reads
* v3.5.0              Set low battery level detection to 2.26V,
*                     Prevent multiple Start/Stop Timer commands to be invoked.
*******************************************************************************/

#include "blocker_services.h"
#include "stm8l15x.h"


extern volatile bool rtcConfigured;
extern volatile bool lowVoltBatteryDetected;
extern volatile bool testTamperSwitch;


void main(void)
{			
    rtcConfigured = FALSE;
    testTamperSwitch = FALSE;
    lowVoltBatteryDetected = FALSE;

    // initialize Tag i/o's
    initializeTagPins();

    // run Tag Diagnostics
    tagDiagnostics();

    // initialize Tag EEPROM
    initializeTagEE();

    // tag is ready for host operations...
    while (TRUE)
    {
#ifndef DEBUG
        halt();
#else
        delay(8000);
#endif
    }
}
/*************************** END OF FILE **************************************/
