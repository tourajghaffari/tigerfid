/*******************************************************************************
* @file     stm8l15x_it.c
* @author   imoware
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
*                     Corrected interrupt handling for Band tamper
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

#include "stm8l15x_it.h"
#include "blocker_services.h"
#include "i2c_ee.h"


INTERRUPT_HANDLER(EXTIE_F_PVD_IRQHandler, 5)
{
    i2cPowerOn();
    batteryVoltageIRQHandler();
    i2cPowerOff();

    /* Clear PVD interrupt flag. */
    PWR->CSR1 |= PWR_CSR1_PVDIF;
}


INTERRUPT_HANDLER(EXTID_H_IRQHandler, 7)
{
    i2cPowerOn();
    rfCommandIRQHandler();
    i2cPowerOff();

    EXTI->SR2 = EXTI_IT_PortD & 0xFF;
}


INTERRUPT_HANDLER(EXTI3_IRQHandler, 11)
{
    EXTI->SR1 = EXTI_IT_Pin3;
    tamperIRQHandler();
}


INTERRUPT_HANDLER_TRAP(TRAP_IRQHandler)
{
#ifdef DEBUG
    nop();  // used for debugging (place debugger breakpoint at this line)
#endif
}


INTERRUPT_HANDLER(FLASH_IRQHandler, 1){}
INTERRUPT_HANDLER(DMA1_CHANNEL0_1_IRQHandler, 2){}
INTERRUPT_HANDLER(DMA1_CHANNEL2_3_IRQHandler, 3){}
INTERRUPT_HANDLER(RTC_CSSLSE_IRQHandler, 4){}
INTERRUPT_HANDLER(EXTIB_G_IRQHandler, 6){}
INTERRUPT_HANDLER(EXTI0_IRQHandler, 8){}
INTERRUPT_HANDLER(EXTI1_IRQHandler, 9){}
INTERRUPT_HANDLER(EXTI2_IRQHandler, 10){}
INTERRUPT_HANDLER(EXTI4_IRQHandler, 12){}
INTERRUPT_HANDLER(EXTI5_IRQHandler, 13){}
INTERRUPT_HANDLER(EXTI6_IRQHandler, 14){}
INTERRUPT_HANDLER(EXTI7_IRQHandler, 15){}
INTERRUPT_HANDLER(LCD_AES_IRQHandler, 16){}
INTERRUPT_HANDLER(SWITCH_CSS_BREAK_DAC_IRQHandler, 17){}
INTERRUPT_HANDLER(ADC1_COMP_IRQHandler, 18){}
INTERRUPT_HANDLER(TIM2_UPD_OVF_TRG_BRK_USART2_TX_IRQHandler, 19){}
INTERRUPT_HANDLER(TIM2_CC_USART2_RX_IRQHandler, 20){}
INTERRUPT_HANDLER(TIM3_UPD_OVF_TRG_BRK_USART3_TX_IRQHandler, 21){}
INTERRUPT_HANDLER(TIM3_CC_USART3_RX_IRQHandler, 22){}
INTERRUPT_HANDLER(TIM1_UPD_OVF_TRG_COM_IRQHandler, 23){}
INTERRUPT_HANDLER(TIM1_CC_IRQHandler, 24){}
INTERRUPT_HANDLER(TIM4_UPD_OVF_TRG_IRQHandler, 25){}
INTERRUPT_HANDLER(SPI1_IRQHandler, 26){}
INTERRUPT_HANDLER(USART1_TX_TIM5_UPD_OVF_TRG_BRK_IRQHandler, 27){}
INTERRUPT_HANDLER(USART1_RX_TIM5_CC_IRQHandler, 28){}
INTERRUPT_HANDLER(I2C1_SPI2_IRQHandler, 29){}
/*************************** END OF FILE **************************************/
