/**
  ********************************************************************************
  * @file    stm8l15x_aes.h
  * @author  MCD Application Team
  * @version V1.5.0
  * @date    13-May-2011
  * @brief   This file contains all the functions prototypes for the AES firmware
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
#ifndef __STM8L15x_AES_H
#define __STM8L15x_AES_H

/* Includes ------------------------------------------------------------------*/
#include "stm8l15x.h"

/** @addtogroup STM8L15x_StdPeriph_Driver
  * @{
  */
  
/** @addtogroup AES
  * @{
  */ 

/* Exported types ------------------------------------------------------------*/

/** @defgroup AES_Exported_Types
  * @{
  */

/** @defgroup AES_Operation_Mode
  * @{
  */
typedef enum
{
  AES_Operation_Encryp            = (uint8_t)0x00, /*!< AES in Encryption mode */
  AES_Operation_KeyDeriv          = (uint8_t)0x02, /*!< AES in Key Derivation mode */
  AES_Operation_Decryp            = (uint8_t)0x04, /*!< AES in Decryption mode */
  AES_Operation_KeyDerivAndDecryp = (uint8_t)0x06  /*!< AES in Key Derivation and Decryption mode */
} AES_Operation_TypeDef;

#define IS_AES_MODE(Operation) (((Operation) == AES_Operation_Encryp) || \
                                ((Operation) == AES_Operation_KeyDeriv) || \
                                ((Operation) == AES_Operation_Decryp)   || \
                                ((Operation) == AES_Operation_KeyDerivAndDecryp))
/**
  * @}
  */
  
/** @defgroup AES_Flags
  * @{
  */
typedef enum
{
  AES_FLAG_CCF      = (uint8_t)0x01,  /*!< Computation Complete Flag */
  AES_FLAG_RDERR    = (uint8_t)0x02,  /*!< Read Error Flag           */
  AES_FLAG_WRERR    = (uint8_t)0x04   /*!< Write Error Flag          */
}AES_FLAG_TypeDef;

#define IS_AES_FLAG(Flag)  (((Flag) == AES_FLAG_CCF) || \
                            ((Flag) == AES_FLAG_RDERR)  || \
                            ((Flag) == AES_FLAG_WRERR))
   
/**
  * @}
  */
  
/** @defgroup AES_Interrupts
  * @{
  */
typedef enum
{
  AES_IT_CCIE   = (uint16_t)0x20, /*!< Computation Complete interrupt enable */
  AES_IT_ERRIE  = (uint16_t)0x40  /*!< Error interrupt enable                */
}AES_IT_TypeDef;

#define IS_AES_IT(IT) (((IT) == AES_IT_CCIE) || \
                       ((IT) == AES_IT_ERRIE))
   
/**
  * @}
  */
  
/** @defgroup AES_DMA_Transfer_Direction
  * @{
  */
typedef enum
{
  AES_DMATransfer_InOut = (uint8_t) 0x80 /*!< DMA requests enabled for input transfer phase
                                              as well as for the output transfer phase */
}
AES_DMATransfer_TypeDef;

#define IS_AES_DMATRANSFER(Transfer)   ((Transfer) == AES_DMATransfer_InOut)
/**
  * @}
  */
  
/**
  * @}
  */
  
/* Exported constants --------------------------------------------------------*/
/* Exported macros -----------------------------------------------------------*/
/* Exported functions ------------------------------------------------------- */
/*  Function used to set the AES configuration to the default reset state *****/  
void AES_DeInit(void);

/* AES Configuration **********************************************************/
void AES_OperationModeConfig(AES_Operation_TypeDef AES_Operation);
void AES_Cmd(FunctionalState NewState);

/* AES Read and Write operations **********************************************/
void AES_WriteSubData(uint8_t Data);
void AES_WriteSubKey(uint8_t Key);
uint8_t AES_ReadSubData(void);
uint8_t AES_ReadSubKey(void);

/* DMA transfers management function ******************************************/
void AES_DMAConfig(AES_DMATransfer_TypeDef AES_DMATransfer, FunctionalState NewState);

/* Interrupts and flags management functions **********************************/
void AES_ITConfig(AES_IT_TypeDef AES_IT, FunctionalState NewState);
FlagStatus AES_GetFlagStatus(AES_FLAG_TypeDef AES_FLAG);
void AES_ClearFlag(AES_FLAG_TypeDef AES_FLAG);
ITStatus AES_GetITStatus(AES_IT_TypeDef AES_IT);
void AES_ClearITPendingBit(AES_IT_TypeDef AES_IT);

#endif /* __STM8L15x_AES_H */

/**
  * @}
  */

/**
  * @}
  */

/******************* (C) COPYRIGHT 2011 STMicroelectronics *****END OF FILE****/
