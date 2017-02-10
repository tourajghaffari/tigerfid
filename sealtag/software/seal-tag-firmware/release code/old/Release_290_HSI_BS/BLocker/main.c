/*******************************************************************************
* @file     main.c
* @author   imoware
* @version  V2.9.0
* @date     04/11/12
********************************************************************************
* @version history    Description
* ----------------    -----------
* v2.5.0              LSI Clock Enable for RTC (deprecated with 2.7 release)
* v2.6.0              LSI Clock Calibration (deprecated with 2.7 release)
* v2.7.0              LSE Clock Enable for RTC
* v2.8.0              (1) Tag Version, (2) Last Packet Reserved
* v2.9.0              Tamper Switch Debounce Handler
*******************************************************************************/ 
#include "blocker_services.h"
#include "i2c_ee.h"

#include "stm8l15x.h"
#include "stm8l15x_clk.h"
#include "stm8l15x_gpio.h"
#include "stm8l15x_i2c.h"
#include "stm8l15x_itc.h"
#include "stm8l15x_rtc.h"
#include "stdio.h"
#include "stdlib.h"

extern __IO bool rtcConfigured;
extern __IO bool lowVoltBatteryDetected;
extern __IO bool testTamperSwitch;


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
        wfi();              
        delay((uint16_t)8000);        
    }
}
/*************************** END OF FILE **************************************/