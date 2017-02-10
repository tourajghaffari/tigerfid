/*******************************************************************************
* @file     main.c
* @author   imoware
* @version  v2.6.0
* @date     09/08/11
*******************************************************************************/ 
#include "blocker_services.h"
#include "i2c_ee.h"

#include "stm8l15x.h"
#include "stm8l15x_clk.h"
#include "stm8l15x_gpio.h"
#include "stm8l15x_i2c.h"
#include "stm8l15x_itc.h"
#include "stm8l15x_rtc.h"
#include "stm8l15x_adc.h"
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

    if (i2cTest() != SUCCESS)
        halt();
    if (rtcTest() != SUCCESS)
        halt();    
    
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