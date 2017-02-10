/**
  ******************************************************************************
  * @file    stm8l15x_rst.c
  * @author  MCD Application Team
  * @version V1.5.0
  * @date    13-May-2011
  * @brief   This file provides firmware functions to manage the following 
  *          functionalities of the RST peripheral:
  *           - Flag management
  *           - NRST Pin configuration
  *
  *  @verbatim
  *               
  *          ===================================================================
  *                               RST specific features
  *          ===================================================================
  *
  *           When a reset occurs, there is a reset phase from the external pin 
  *           pull-down to the internal reset signal release. During this phase,
  *           the microcontroller sets some hardware configurations before going
  *           to the reset vector.
  *           At the end of this phase, most of the registers are configured with
  *           their “reset state” values. 
  *           During the reset phase, some pin configurations may be different from
  *           their “reset state” configuration.
  *           
  *           The NRST pin is an input and can be configured as open-drain output
  *           using the RST_GPOutputEnable() function 
  *
  *  @endverbatim
  *    
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

/* Includes ------------------------------------------------------------------*/

#include "stm8l15x_rst.h"

/** @addtogroup STM8L15x_StdPeriph_Driver
  * @{
  */

/** @defgroup RST 
  * @brief RST driver modules
  * @{
  */ 
/* Private define ------------------------------------------------------------*/
#define RST_CR_MASK  0xD0 /*!< Enable the GPIO */
/* Private macro -------------------------------------------------------------*/
/* Private variables ---------------------------------------------------------*/
/* Private function prototypes -----------------------------------------------*/
/* Private functions ---------------------------------------------------------*/

/** @defgroup RST_Private_Functions
  * @{
  */

/** @defgroup RST_Group1 Flag management functions
 *  @brief   Flag management functions 
 *
@verbatim   
 ===============================================================================
                       Flag management functions
 ===============================================================================  

@endverbatim
  * @{
  */

/**
  * @brief   Checks whether the specified RST flag is set or not.
  * @param   RST_Flag : specify the reset flag to check.
  *          This parameter can be one of the following values:
  *            @arg RST_FLAG_PORF: POR reset flag
  *            @arg RST_FLAG_SWIMF: SWIM reset flag
  *            @arg RST_FLAG_ILLOPF: Illegal opcode reset flag
  *            @arg RST_FLAG_IWDGF: Independent watchdog reset flag 
  *            @arg RST_FLAG_WWDGF: Window watchdog reset flag
  *            @arg RST_FLAG_BORF: BOR reset flag
  * @retval The new state of RST_Flag (SET or RESET).
  */
FlagStatus RST_GetFlagStatus(RST_FLAG_TypeDef RST_Flag)
{
  /* Check the parameters */
  assert_param(IS_RST_FLAG(RST_Flag));

  /* Get flag status */

  return ((FlagStatus)((uint8_t)RST->SR & (uint8_t)RST_Flag));
}

/**
  * @brief  Clears the specified RST flag.
  * @param   RST_Flag : specify the reset flag to check.
  *          This parameter can be one of the following values:
  *            @arg RST_FLAG_PORF: POR reset flag
  *            @arg RST_FLAG_SWIMF: SWIM reset flag
  *            @arg RST_FLAG_ILLOPF: Illegal opcode reset flag
  *            @arg RST_FLAG_IWDGF: Independent watchdog reset flag 
  *            @arg RST_FLAG_WWDGF: Window watchdog reset flag
  *            @arg RST_FLAG_BORF: BOR reset flag
  * @retval None
  */
void RST_ClearFlag(RST_FLAG_TypeDef RST_Flag)
{
  /* Check the parameters */
  assert_param(IS_RST_FLAG(RST_Flag));

  RST->SR = (uint8_t)RST_Flag;
}

/**
  * @}
  */
  
/** @defgroup RST_Group2 NRST Pin configuration function
 *  @brief   NRST Pin configuration function 
 *
@verbatim   
 ===============================================================================
                      NRST Pin configuration function
 ===============================================================================  

@endverbatim
  * @{
  */
  
/**
  * @brief  Configures the reset pad as GP output.
  * @param  None
  * @retval None
  */
void RST_GPOutputEnable(void)
{

  RST->CR = RST_CR_MASK;
}

/**
  * @}
  */

/**
  * @}
  */
  
/**
  * @}
  */

/**
  * @}
  */

/******************* (C) COPYRIGHT 2011 STMicroelectronics *****END OF FILE****/
