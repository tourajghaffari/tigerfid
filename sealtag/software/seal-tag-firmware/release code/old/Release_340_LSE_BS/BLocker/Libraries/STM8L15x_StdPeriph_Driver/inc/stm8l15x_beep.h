/**
  ******************************************************************************
  * @file    stm8l15x_beep.h
  * @author  MCD Application Team
  * @version V1.5.0
  * @date    13-May-2011
  * @brief   This file contains all the functions prototypes for the BEEP firmware
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
#ifndef __STM8L15x_BEEP_H
#define __STM8L15x_BEEP_H

/* Includes ------------------------------------------------------------------*/
#include "stm8l15x.h"

/** @addtogroup STM8L15x_StdPeriph_Driver
  * @{
  */
  
/** @addtogroup BEEP
  * @{
  */ 
  
/* Exported types ------------------------------------------------------------*/
/** @defgroup BEEP_Exported_Types
  * @{
  */
  
/** @defgroup BEEP_Frequency
  * @{
  */
typedef enum {
  BEEP_Frequency_1KHz = (uint8_t)0x00,  /*!< Beep signal output frequency 1 KHz */
  BEEP_Frequency_2KHz = (uint8_t)0x40,  /*!< Beep signal output frequency 2 KHz */
  BEEP_Frequency_4KHz = (uint8_t)0x80   /*!< Beep signal output frequency 4 KHz */
} BEEP_Frequency_TypeDef;

#define IS_BEEP_FREQUENCY(FREQ) (((FREQ) == BEEP_Frequency_1KHz) || \
                                 ((FREQ) == BEEP_Frequency_2KHz) || \
                                 ((FREQ) == BEEP_Frequency_4KHz))
 
/**
  * @}
  */

/**
  * @}
  */
      
/* Exported constants --------------------------------------------------------*/
/** @defgroup BEEP_Exported_Constants
  * @{
  */

#define BEEP_CALIBRATION_DEFAULT ((uint8_t)0x01)  /*!< Default value when calibration is not done */
#define LSI_FREQUENCY_MIN ((uint32_t)25000)       /*!< LSI minimum value in Hertz */
#define LSI_FREQUENCY_MAX ((uint32_t)75000)       /*!< LSI maximum value in Hertz */

/**
  * @}
  */

/* Exported macros -----------------------------------------------------------*/
/** @defgroup BEEP_Exported_Macros
  * @{
  */
#define IS_LSI_FREQUENCY(FREQ) (((FREQ) >= LSI_FREQUENCY_MIN) && ((FREQ) <= LSI_FREQUENCY_MAX))

/**
  * @}
  */

/* Exported functions ------------------------------------------------------- */

/*  Function used to set the BEEP configuration to the default reset state *****/  
void BEEP_DeInit(void);

/* Initialization and Configuration functions *********************************/
void BEEP_Init(BEEP_Frequency_TypeDef BEEP_Frequency);
void BEEP_Cmd(FunctionalState NewState);

/* Low Speed Internal Clock(LSI) Calibration functions functions **************/
void BEEP_LSClockToTIMConnectCmd(FunctionalState NewState);
void BEEP_LSICalibrationConfig(uint32_t LSIFreqHz);


#endif /* __STM8L15x_BEEP_H */

/**
  * @}
  */

/**
  * @}
  */

/******************* (C) COPYRIGHT 2011 STMicroelectronics *****END OF FILE****/
