/*******************************************************************************
* @file     battery.c
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
*                     Corrected interrupt handling for Band tamper
*******************************************************************************/

#include "battery.h"
#include "stm8l15x.h"


/*
*******************************************************************************
*                              Battery_Init()
*
* Description : Initialize battery monitoring hardware.
*
* Return(s)   : none.
*******************************************************************************
*/

void  Battery_Init (void)
{
    PWR_PVDCmd(ENABLE);
    PWR_PVDITConfig(ENABLE);
}


/*
*******************************************************************************
*                           Battery_HasLowLevel()
*
* Description : Get current battery low level.
*
* Return(s)   : TRUE,  if battery in low level.
*
*               FALSE, otherwise.
*******************************************************************************
*/

bool Battery_HasLowLevel (void)
{
    if (PWR_GetFlagStatus(PWR_FLAG_PVDOF) != RESET) {
        return (TRUE);
    } else {
        return (FALSE);
    }
}
