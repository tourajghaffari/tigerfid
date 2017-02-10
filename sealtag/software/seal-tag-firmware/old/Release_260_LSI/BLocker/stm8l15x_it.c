/*******************************************************************************
* @file     stm8l15x_it.c
* @author   imoware
* @version  v2.6.0
* @date     09/08/11
*******************************************************************************/ 
#include "stm8l15x_it.h"
#include "blocker_services.h"

INTERRUPT_HANDLER(EXTIE_F_PVD_IRQHandler, 5)
{
    //i2cPowerOn();    
    batteryVoltageIRQHandler();
    //i2cPowerOff();            
    PWR_PVDClearITPendingBit();
}

INTERRUPT_HANDLER(EXTID_H_IRQHandler, 7)
{
    //i2cPowerOn();  
    rfCommandIRQHandler();
    //i2cPowerOff();    
    EXTI_ClearITPendingBit(EXTI_IT_PortD);
}

INTERRUPT_HANDLER(EXTI3_IRQHandler, 11)
{  
    //i2cPowerOn();  
    tamperIRQHandler();
    //i2cPowerOff();    
    EXTI_ClearITPendingBit(EXTI_IT_Pin3);
}

INTERRUPT_HANDLER(TIM2_CC_USART2_RX_IRQHandler, 20)
{
    //i2cPowerOn();    
    timer2IRQHandler();
    //i2cPowerOff();        
}

INTERRUPT_HANDLER_TRAP(TRAP_IRQHandler){}
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