/*******************************************************************************
* @file     i2c_ee.h
* @author   imoware
* @version  V2.7.0
* @date     01/11/12
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


