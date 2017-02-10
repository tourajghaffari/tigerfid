/*******************************************************************************
  * @file    stm8l15x_iwdg.h
  * @author  MCD Application Team
  * @version V1.5.0
  * @date    13-May-2011
  * @brief   This file contains all the functions prototypes for the IWDG 
  *          firmware library.
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
#ifndef __STM8L15x_IWDG_H
#define __STM8L15x_IWDG_H

/* Includes ------------------------------------------------------------------*/
#include "stm8l15x.h"

/** @addtogroup STM8L15x_StdPeriph_Driver
  * @{
  */

/** @addtogroup IWDG
  * @{
  */
  
/* Exported variables ------------------------------------------------------- */
/* Exported constants --------------------------------------------------------*/

/** @defgroup IWDG_Exported_Constants
  * @{
  */

/** @defgroup IWDG_KeyRefresh
  * @{
  */
#define IWDG_KEY_REFRESH    ((uint8_t)0xAA)  /*!< This value written in the Key 
                                                  register prevent the watchdog reset */
/**
 * @}
 */
 
/** @defgroup IWDG_KeyEnable
  * @{
  */
#define IWDG_KEY_ENABLE     ((uint8_t)0xCC)  /*!< This value written in the Key
                                                  register start the watchdog counting down*/
/**
 * @}
 */
/**
 * @}
 */

/* Exported macros -----------------------------------------------------------*/
/* Exported types ------------------------------------------------------------*/

/** @defgroup IWDG_Exported_Types
  * @{
  */

/** @defgroup IWDG_WriteAccess
  * @{
  */
typedef enum
{
  IWDG_WriteAccess_Enable  = (uint8_t)0x55, 
  IWDG_WriteAccess_Disable = (uint8_t)0x00
} IWDG_WriteAccess_TypeDef;
#define IS_IWDG_WRITE_ACCESS_MODE(MODE) (((MODE) == IWDG_WriteAccess_Enable) || \
                                         ((MODE) == IWDG_WriteAccess_Disable))
/**
 * @}
 */
 
/** @defgroup IWDG_prescaler 
  * @{
  */
typedef enum
{
  IWDG_Prescaler_4   = (uint8_t)0x00, /*!< Used to set prescaler register to 4 */
  IWDG_Prescaler_8   = (uint8_t)0x01, /*!< Used to set prescaler register to 8 */
  IWDG_Prescaler_16  = (uint8_t)0x02, /*!< Used to set prescaler register to 16 */
  IWDG_Prescaler_32  = (uint8_t)0x03, /*!< Used to set prescaler register to 32 */
  IWDG_Prescaler_64  = (uint8_t)0x04, /*!< Used to set prescaler register to 64 */
  IWDG_Prescaler_128 = (uint8_t)0x05, /*!< Used to set prescaler register to 128 */
  IWDG_Prescaler_256 = (uint8_t)0x06  /*!< Used to set prescaler register to 256 */
} IWDG_Prescaler_TypeDef;
#define IS_IWDG_PRESCALER_VALUE(VALUE) (((VALUE) == IWDG_Prescaler_4)   || \
                                        ((VALUE) == IWDG_Prescaler_8)   || \
                                        ((VALUE) == IWDG_Prescaler_16)  || \
                                        ((VALUE) == IWDG_Prescaler_32)  || \
                                        ((VALUE) == IWDG_Prescaler_64)  || \
                                        ((VALUE) == IWDG_Prescaler_128) || \
                                        ((VALUE) == IWDG_Prescaler_256))
/**
 * @}
 */        
                                 
/**
  * @}
  */

/* Exported functions ------------------------------------------------------- */
/* Prescaler and Counter configuration functions ******************************/
void IWDG_WriteAccessCmd(IWDG_WriteAccess_TypeDef IWDG_WriteAccess);
void IWDG_SetPrescaler(IWDG_Prescaler_TypeDef IWDG_Prescaler);
void IWDG_SetReload(uint8_t IWDG_Reload);
void IWDG_ReloadCounter(void);

/* IWDG activation function ***************************************************/
void IWDG_Enable(void);

#endif /* __STM8L15x_IWDG_H */

/**
  * @}
  */
  
/**
  * @}
  */

/******************* (C) COPYRIGHT 2011 STMicroelectronics *****END OF FILE****/
