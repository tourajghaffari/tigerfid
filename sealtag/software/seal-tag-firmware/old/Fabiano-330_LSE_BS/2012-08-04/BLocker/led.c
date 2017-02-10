/*******************************************************************************
* @file     led.c
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

#include "stm8l15x_gpio.h"


/*
*******************************************************************************
*                                LED_Init()
*
* Description : Initialize LED hardware.
*
* Return(s)   : none.
*******************************************************************************
*/

void  LED_Init (void)
{
    GPIO_Init(GPIOB, GPIO_Pin_4, GPIO_Mode_Out_PP_Low_Slow);
    GPIO_SetBits(GPIOB, GPIO_Pin_4);
}


/*
*******************************************************************************
*                                 LED_Off()
*
* Description : Turn LED off.
*
* Return(s)   : none.
*******************************************************************************
*/

void  LED_Off (void)
{
    GPIO_ResetBits(GPIOB, GPIO_Pin_4);
}


/*
*******************************************************************************
*                                 LED_On()
*
* Description : Turn LED on.
*
* Return(s)   : none.
*******************************************************************************
*/

void  LED_On (void)
{
    GPIO_SetBits(GPIOB, GPIO_Pin_4);
}
