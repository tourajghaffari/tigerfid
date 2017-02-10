/*******************************************************************************
* @file     serial_number.c
* @author   fgk
* @version  V3.9.0
* @date     10/19/12
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
*******************************************************************************/

#include "serial_number.h"
#include "blocker_services.h"
#include "i2c_ee.h"


extern uint8_t histPacket[2];


/*
*******************************************************************************
*                           SerialNumber_Store()
*
* Description : Store 96-bit unique serial number into the next history packet.
*
* Return(s)   : ERROR_NONE, if NO error(s).
*
*               ErrorCode,  otherwise.
*******************************************************************************
*/

ErrorCode  SerialNumber_Store (void)
{
    const uint8_t   *U_ID = (uint8_t *)0x4926;
          uint16_t   histAddr;
          ErrorCode  status;


    histAddr = getInt16(histPacket);
    status   = i2cWriteBuffer(&U_ID[0], histAddr + 0, 4, EEPROM_USER_ADDRESS);
    if (status == ERROR_NONE) {
        status   = i2cWriteBuffer(&U_ID[4], histAddr + 4, 4, EEPROM_USER_ADDRESS);
        if (status == ERROR_NONE) {
            status   = i2cWriteBuffer(&U_ID[8], histAddr + 8, 4, EEPROM_USER_ADDRESS);
        }
    }

    return status;
}
