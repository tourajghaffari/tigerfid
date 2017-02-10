/*******************************************************************************
* @file     blocker_services.h
* @author   imoware
* @version  V3.0.0
* @date     04/09/12
********************************************************************************
* @version history    Description
* ----------------    -----------
* v2.5.0              LSI Clock Enable for RTC (deprecated with 2.7 release)
* v2.6.0              LSI Clock Calibration (deprecated with 2.7 release)
* v2.7.0              LSE Clock Enable for RTC
* v2.8.0              (1) Tag Version, (2) Last Packet Reserved
* v3.0.0              Tamper Switch Debounce Handler
*******************************************************************************/ 
#ifndef __I2C_EE_H
#define __I2C_EE_H

#ifdef I2C_FAST_MODE
#define I2C_SPEED 400000  // 400kHz
#else
#define I2C_SPEED 100000  // 100kHz
#endif

#define I2C_RETRY 256

#define EEPROM_USER_ADDRESS 0xa6
#define EEPROM_SYSTEM_ADDRESS 0xae

void i2cInit(uint8_t eepromAddress);
ErrorStatus i2cReadBuffer(uint8_t *pBuffer, uint16_t ReadAddr, uint8_t NumByteToRead, uint8_t eepromAddress);
ErrorStatus i2cWriteBuffer(uint8_t *pBuffer, uint16_t WriteAddr, uint8_t NumByteToWrite, uint8_t eepromAddress);
ErrorStatus i2cWriteBufferWorker(uint8_t* pBuffer, uint16_t WriteAddr, uint8_t NumByteToWrite, uint8_t eepromAddress);
ErrorStatus i2cReadBufferWorker(uint8_t* pBuffer, uint16_t ReadAddr, uint8_t NumByteToRead, uint8_t eepromAddress);
#endif
/*************************** END OF FILE **************************************/


