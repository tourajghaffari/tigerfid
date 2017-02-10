/*	BASIC INTERRUPT VECTOR TABLE FOR STM8 devices
 *	Copyright (c) 2007 STMicroelectronics
 */

#include "stm8l15x_it.h"
typedef void @far (*interrupt_handler_t)(void);

struct interrupt_vector
{
		unsigned char interrupt_instruction;
		interrupt_handler_t interrupt_handler;
};

@far @interrupt void NonHandledInterrupt (void)
{
		// in order to detect unexpected events during development, 
		// it is recommended to set a breakpoint on the following instruction
		return;
}

// startup routine
extern void _stext();

struct interrupt_vector const _vectab[] =
{
	{0x82, (interrupt_handler_t)_stext},// reset
	{0x82, TRAP_IRQHandler}, 						// trap
	{0x82, NonHandledInterrupt},				// irq0: reserved
	{0x82, FLASH_IRQHandler}, 					// irq1: FLASH
	{0x82, DMA1_CHANNEL0_1_IRQHandler},	// irq2: DMA1 chan0/1
	{0x82, DMA1_CHANNEL2_3_IRQHandler}, // irq3: DMA1 chan2/3
	{0x82, RTC_CSSLSE_IRQHandler},			// irq4: RTC
	{0x82, EXTIE_F_PVD_IRQHandler},			// irq5: External PORTE/F interrupt /PVD
	{0x82, EXTIB_G_IRQHandler}, 				// irq6: External PORTB / PORTG
	{0x82, EXTID_H_IRQHandler}, 				// irq7: External PORTD / PORTH
	{0x82, EXTI0_IRQHandler}, 					// irq8: External PIN0
	{0x82, EXTI1_IRQHandler}, 					// irq9: External PIN1
	{0x82, EXTI2_IRQHandler}, 					// irq10: External PIN2
	{0x82, EXTI3_IRQHandler}, 					// irq11: External PIN3
	{0x82, EXTI4_IRQHandler}, 					// irq12: External PIN4
	{0x82, EXTI5_IRQHandler}, 					// irq13: External PIN5
	{0x82, EXTI6_IRQHandler}, 					// irq14: External PIN6
	{0x82, EXTI7_IRQHandler}, 					// irq15: External PIN7
	{0x82, LCD_AES_IRQHandler}, 				// irq16: LCD / AES
	{0x82, SWITCH_CSS_BREAK_DAC_IRQHandler},// irq17: CLK switch/CSS interrupt/ TIM1 Break interrupt / DAC
	{0x82, ADC1_COMP_IRQHandler}, 			// irq18: ADC1 and Comparator interrupt
	{0x82, TIM2_UPD_OVF_TRG_BRK_USART2_TX_IRQHandler}, // irq19: TIM2 Update/Overflow/Trigger/Break / USART2 TX
	{0x82, TIM2_CC_USART2_RX_IRQHandler}, 						// irq20: TIM2 Capture/Compare / USART2 RX
	{0x82, TIM3_UPD_OVF_TRG_BRK_USART3_TX_IRQHandler},// irq21: TIM3 Update/Overflow/Trigger/Break / USART3 TX
	{0x82, TIM3_CC_USART3_RX_IRQHandler}, 						// irq22: TIM3 Capture/Compare /USART3 RX
	{0x82, TIM1_UPD_OVF_TRG_COM_IRQHandler}, 					// irq23: TIM1 Update/Overflow/Trigger/Commutation
	{0x82, TIM1_CC_IRQHandler}, 											// irq24: TIM1 Capture/Compare
	{0x82, TIM4_UPD_OVF_TRG_IRQHandler}, 							// irq25: TIM4 Update/Overflow/Trigger
	{0x82, SPI1_IRQHandler}, 													// irq26: SPI1 interrupt
	{0x82, USART1_TX_TIM5_UPD_OVF_TRG_BRK_IRQHandler},// irq27: USART1 TX / TIM5 Update/Overflow/Trigger/Break
	{0x82, USART1_RX_TIM5_CC_IRQHandler}, 						// irq28: USART1 RX / TIM1 Capture/Compare
	{0x82, I2C1_SPI2_IRQHandler}, 										// irq29: I2C1 / SPI2
};
