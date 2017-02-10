/*******************************************************************************
* @file     clock.c
* @author   fgk
* @version  V3.4.0
* @date     08/04/12
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
*******************************************************************************/

#include "stm8l15x_clk.h"
#include "clock.h"


/*
*******************************************************************************
*                               Clock_Init()
*
* Description : Initialize clock hardware and configuration.
*
* Return(s)   : none.
*******************************************************************************
*/

void  Clock_Init (void)
{
    CLK_DeInit();

    //--- enable LSE (32.768 KHz) for RTC...
    CLK_LSEConfig(CLK_LSE_ON);
    while (CLK_GetFlagStatus(CLK_FLAG_LSERDY) == RESET);

#ifdef I2C_FAST_MODE
    CLK_SYSCLKDivConfig(CLK_SYSCLKDiv_1);
#else
    CLK_SYSCLKDivConfig(CLK_SYSCLKDiv_2);
#endif
}
