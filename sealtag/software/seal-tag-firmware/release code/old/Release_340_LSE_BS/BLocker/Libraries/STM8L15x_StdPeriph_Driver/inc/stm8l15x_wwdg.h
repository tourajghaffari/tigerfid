/**
  ******************************************************************************
  * @file    stm8l15x_wwdg.h
  * @author  MCD Application Team
  * @version V1.5.0
  * @date    13-May-2011
  * @brief   This file contains all the functions prototypes for the WWDG firmware
  *          library.
  ******************************************************************************
  * @attention
  *
  * THE PRESENT FIRMWARE WHICH IS FOR GUIDANCE ONLY AIMS AT PROVIDING CUSTOMERS
  * WITH CODING INFORMATION REGARDING THEIR PRODUCTS IN ORDER FOR THEM TO SAVE
  * TIME. AS A RESULT, STMICROELECTRONICS SHALL NOT BE HELD LIABLE FOR ANY
  * DIRECT, INDIRECT OR CONSEQUENTIAL DAMAGES WITH RESPECT TO ANY CLAIMS ARISING
  * FROM THE CONTENT OF SUCH FIRMWARE AND/OR THE USE MADE BY CUSTOMERS OF THE
  * CODING INFORMATION CONTAINED HEREIN IN CONNECTION WITH THEIR PRODUCTS.
  *
  * <h2><center>&copy; COPYRIGHT 2011 STMicroelectronics</center></h2>
  ******************************************************************************  
  */

/* Define to prevent recursive inclusion -------------------------------------*/
#ifndef __STM8L15x_WWDG_H
#define __STM8L15x_WWDG_H

/* Includes ------------------------------------------------------------------*/
#include "stm8l15x.h"

/** @addtogroup STM8L15x_StdPeriph_Driver
  * @{
  */
  
/** @addtogroup WWDG
  * @{
  */ 
  
/* Exported types ------------------------------------------------------------*/
/* Exported constants --------------------------------------------------------*/
/* Exported macro ------------------------------------------------------------*/

/** @defgroup  WWDG_Exported_Macros
  * @{
  */

/** @defgroup WWDG_WindowLimitValue 
  * @{
  */ 
#define IS_WWDG_WINDOW_LIMIT_VALUE(WindowLimitValue) ((WindowLimitValue) <= 0x7F)

/**
  * @}
  */

/** @defgroup WWDG_CounterValue 
  * @{
  */
#define IS_WWDG_COUNTER_VALUE(CounterValue) ((CounterValue) <= 0x7F)
/**
  * @}
  */
  
/**
  * @}
  */

/* Exported functions ------------------------------------------------------- */
/* Refresh window and Counter configuration functions *************************/
void WWDG_Init(uint8_t Counter, uint8_t WindowValue);
void WWDG_SetWindowValue(uint8_t WindowValue);
void WWDG_SetCounter(uint8_t Counter);

/* WWDG activation function ***************************************************/
void WWDG_Enable(uint8_t Counter);

/* WWDG counter and software reset management **********************************/
uint8_t WWDG_GetCounter(void);
void WWDG_SWReset(void);


#endif /* __STM8L15x_WWDG_H */
/**
  * @}
  */

/**
  * @}
  */

/******************* (C) COPYRIGHT 2011 STMicroelectronics *****END OF FILE****/