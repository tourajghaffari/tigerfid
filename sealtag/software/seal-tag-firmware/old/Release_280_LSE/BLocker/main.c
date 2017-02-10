/*******************************************************************************
* @file     main.c
* @author   imoware
* @version  V2.8.0
* @date     03/15/12
********************************************************************************
* @version history    Description
* ----------------    -----------
* v2.5.0              LSI Clock Enable for RTC (deprecated with 2.7 release)
* v2.6.0              LSI Clock Calibration (deprecated with 2.7 release)
* v2.7.0              LSE Clock Enable for RTC
* v2.8.0              (1) Tag Version, (2) Last Packet Reserved
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
    clockGpioConfig();
           
    disableInterrupts();
    i2cPowerOn();

    // memory test..
    if (i2cTest() != SUCCESS)
        halt();
    // rtc test...
    if (rtcTest() != SUCCESS)
        halt();
    // initalialize EEPROM
    if (initBlockerEE(TRUE) != SUCCESS)
        halt();

    //i2cPowerOff();
    enableInterrupts();

    while (TRUE)
    {
        halt();              
        delay((uint16_t)8000);        
    }
}
/*************************** END OF FILE **************************************/