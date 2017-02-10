/*******************************************************************************
* @file     rf.c
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

#include "stm8l15x_gpio.h"
#include "stm8l15x_exti.h"
#include "error_code.h"
#include "rf.h"
#include "blocker_services.h"
#include "i2c_ee.h"


extern uint8_t histPacket[2];


/*
*******************************************************************************
*                                 RF_Init()
*
* Description : Initialize RF hardware.
*
* Return(s)   : none.
*******************************************************************************
*/

void  RF_Init (void)
{
    GPIO_Init(GPIOD, GPIO_Pin_0, GPIO_Mode_In_FL_IT);
    EXTI_SetPortSensitivity(EXTI_Port_D, EXTI_Trigger_Rising);
    EXTI_SelectPort(EXTI_Port_D);
    EXTI_SetHalfPortSelection(EXTI_HalfPort_D_LSB, ENABLE);
}


/*
*******************************************************************************
*                              RF_UID_Store()
*
* Description : Store RF 64-bit unique ID into the next history packet.
*
* Return(s)   : ERROR_NONE, if NO error(s).
*
*               ErrorCode,  otherwise.
*******************************************************************************
*/

ErrorCode  RF_UID_Store (void)
{
    const  uint16_t   uidAddress = (uint16_t)0x914;
           uint16_t   histAddr;
           uint8_t    uid[8] = {0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00};
           uint8_t    ix;
           uint8_t    reverse;
           ErrorCode  status;


    status = i2cReadBuffer(uid, uidAddress, 8, EEPROM_SYSTEM_ADDRESS);
    if (status != ERROR_NONE)
        return status;

    for (ix = 0; ix < 4; ix++) {
        reverse     = uid[ix];
        uid[ix]     = uid[7 - ix];
        uid[7 - ix] = reverse;
    }

    histAddr = getInt16(histPacket);
    status   = i2cWriteBuffer(&uid[0], histAddr + 0, 4, EEPROM_USER_ADDRESS);
    if (status == ERROR_NONE) {
        status  = i2cWriteBuffer(&uid[4], histAddr + 4, 4, EEPROM_USER_ADDRESS);
    }

    return status;
}
