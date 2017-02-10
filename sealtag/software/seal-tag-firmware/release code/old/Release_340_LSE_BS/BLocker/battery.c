/*******************************************************************************
* @file     battery.c
* @author   fgk
* @version  V4.0.0
* @date     10/25/12
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
* v3.6.0              Fixed I2C initialization to delay communication for 84ms
*                     to allow power to stabilize.
* v3.7.0              Prevent factory reset when RTC is running.
* v3.8.0              Added serial number to STM8 EEPROM.
* v3.9.0              Modified serial number to use STM8 Unique ID.
* v4.0.0              Check battery level on start.
*                     Modified read battery voltage command to include battery
*                     voltage level.
*                     Added read RF UID command.
*                     Optimized code to reduce ROM footprint.
*******************************************************************************/

#include "battery.h"
#include "stm8l15x.h"
#include "stm8l15x_adc.h"


extern void delay(uint16_t counter);


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
    /* Configure the PVD level */
    PWR->CSR1 &= ~(uint16_t)PWR_CSR1_PLS;
    PWR->CSR1 |=            PWR_PVDLevel_2V26;

    /* Enable the PWR PVD */
    PWR->CSR1 |= PWR_CSR1_PVDE;

    /* Enable the PVD interrupt. */
    PWR->CSR1 |= PWR_CSR1_PVDIEN;

                                                                /* Set ADC1 registers to reset values :                 */
    ADC1->CR1      =  ADC_CR1_RESET_VALUE;                      /*     Configuration                                    */
    ADC1->CR2      =  ADC_CR2_RESET_VALUE;
    ADC1->CR3      =  ADC_CR3_RESET_VALUE;
    ADC1->SR       =  (uint8_t)~(uint16_t)ADC_SR_RESET_VALUE;   /*     Status                                           */
    ADC1->HTRH     =  ADC_HTRH_RESET_VALUE;                     /*     High Threshold                                   */
    ADC1->HTRL     =  ADC_HTRL_RESET_VALUE;
    ADC1->LTRH     =  ADC_LTRH_RESET_VALUE;                     /*     Low Threshold                                    */
    ADC1->LTRL     =  ADC_LTRL_RESET_VALUE;
    ADC1->SQR[0]   =  ADC_SQR1_RESET_VALUE;                     /*     Channels Sequence                                */
    ADC1->SQR[1]   =  ADC_SQR2_RESET_VALUE;
    ADC1->SQR[2]   =  ADC_SQR3_RESET_VALUE;
    ADC1->SQR[3]   =  ADC_SQR4_RESET_VALUE;
    ADC1->TRIGR[0] =  ADC_TRIGR1_RESET_VALUE;                   /*     Channels Trigger                                 */
    ADC1->TRIGR[1] =  ADC_TRIGR2_RESET_VALUE;
    ADC1->TRIGR[2] =  ADC_TRIGR3_RESET_VALUE;
    ADC1->TRIGR[3] =  ADC_TRIGR4_RESET_VALUE;
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

bool  Battery_HasLowLevel (void)
{
    if ((PWR->CSR1 & PWR_FLAG_PVDOF) != 0) {
        return (TRUE);
    } else {
        return (FALSE);
    }
}


/*
*******************************************************************************
*                           Battery_GetVoltage()
*
* Description : Get battery voltage.
*
* Return(s)   : Battery voltage in centi-volts.
*******************************************************************************
*/

uint16_t  Battery_GetVoltage (void)
{
    const uint8_t   *VREFIN_Factory = (uint8_t *)0x4910;
          uint8_t    cnt;
          uint16_t   vref;
          uint16_t   vbat;
          uint16_t   value;


    CLK_PeripheralClockConfig(CLK_Peripheral_ADC1, ENABLE);

    /* Enable ADC1. */
    ADC1->CR1 |= ADC_CR1_ADON;

    /* Enable the Internal Voltage reference. */
    ADC1->TRIGR[0] |= ADC_TRIGR1_VREFINTON;

    /* Set the resolution and the conversion mode */
    ADC1->CR1 &= (uint8_t)~(ADC_CR1_CONT |
                            ADC_CR1_RES);
    ADC1->CR1 |= (uint8_t)  ADC_ConversionMode_Single |
                 (uint8_t)  ADC_Resolution_12Bit;

    /* Set the Prescaler */
    ADC1->CR2 &= ~(uint16_t)ADC_CR2_PRESC;
    ADC1->CR2 |=  (uint8_t) ADC_Prescaler_2;

    /* Configures the sampling time for the Fast ADC channel group. */
    ADC1->CR3 &= ~(uint16_t)ADC_CR3_SMPT2;
    ADC1->CR3 |=  (uint8_t)(ADC_SamplingTime_384Cycles << 5);

    /* Enable ADC Vref channel. */
    ADC1->SQR[0] |= ADC_Channel_Vrefint;

    /* Delay 3ms */
    delay(2400);

    value = 0;
    for (cnt = 0; cnt < 8; cnt++) {
        ADC1->CR1 |=  ADC_CR1_START;
        while ((ADC1->SR & ADC_FLAG_EOC) == 0) {
            ;
        }

        value += ADC1->DRH << 8;
        value += ADC1->DRL;

        /* Delay 1ms */
        delay(800);
    }

    vref = 0x600 | *VREFIN_Factory;
    vbat = 3 * 8 * 100uL * vref / value;

    /* Disable ADC Vref channel. */
    ADC1->SQR[0] &= ~(uint16_t)ADC_Channel_Vrefint;

    /* Disable ADC1. */
    ADC1->CR1 &= ~(uint16_t)ADC_CR1_ADON;

    CLK_PeripheralClockConfig(CLK_Peripheral_ADC1, DISABLE);

    return (vbat);
}
