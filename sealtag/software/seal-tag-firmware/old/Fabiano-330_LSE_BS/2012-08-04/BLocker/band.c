/*******************************************************************************
* @file     band.c
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

#include "types.h"
#include "stm8l15x.h"
#include "band.h"
#include "blocker_services.h"


/*
*******************************************************************************
*                                Band_Init()
*
* Description : Initialize band hardware.
*
* Return(s)   : none.
*******************************************************************************
*/

void  Band_Init (void)
{
    GPIO_Init(GPIOB, GPIO_Pin_3, GPIO_Mode_In_PU_IT);
    EXTI_SetPinSensitivity(EXTI_Pin_3, EXTI_Trigger_Rising_Falling);
}

/*
*******************************************************************************
*                              Band_GetState()
*
* Description : Get current state of band.
*
* Return(s)   : OPEN,  if band open.
*
*               CLOSE, otherwise.
*******************************************************************************
*/

OpenClose  Band_GetState (void)
{
    if ((GPIO_ReadInputDataBit((GPIO_TypeDef*)GPIOB, GPIO_Pin_3)) != RESET) {
        return (OPEN);
    } else {
        return (CLOSE);
    }
}


/*
*******************************************************************************
*                               Band_IsOpen()
*
* Description : Debounce band tamper detection over defined threshold.
*
* Return(s)   : TRUE,  if band open.
*
*               FALSE, otherwise.
*******************************************************************************
*/

bool  Band_IsOpen (void)
{
    bool bandOpen = FALSE;
    uint16_t bounceTamperCounter = TAMPER_DETECTION_THRESHOLD;

    while (bounceTamperCounter != 0)
    {
        delay(255);        // delay 330us

        if (Band_GetState() == OPEN)
        {
            bounceTamperCounter--;
            bandOpen = TRUE;
        }
        else
        {
            bandOpen = FALSE;
            break;
        }
    }

    return bandOpen;
}
