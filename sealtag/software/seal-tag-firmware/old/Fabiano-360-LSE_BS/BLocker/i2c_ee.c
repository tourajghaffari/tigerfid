/*******************************************************************************
* @file     i2c_ee.c
* @author   imoware
* @version  V3.6.0
* @date     08/18/12
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
*******************************************************************************/

#include "stm8l15x.h"
#include "stm8l15x_i2c.h"
#include "i2c_ee.h"
#include "blocker_services.h"
#include <string.h>


#define  TIMEOUT         4750       // delay ~30ms

#define  I2C_ALWAYS_ON              // I2C stays enabled all the time


extern void delay(uint16_t counter);

static  ErrorCode  i2cWriteBufferWorker(const uint8_t   *pBuffer,
                                              uint16_t   WriteAddr,
                                              uint8_t    NumByteToWrite,
                                              uint8_t    eepromAddress);
static  ErrorCode  i2cReadBufferWorker (      uint8_t   *pBuffer,
                                              uint16_t   ReadAddr,
                                              uint8_t    NumByteToRead,
                                              uint8_t    eepromAddress);

void  i2cInit (void)
{
    GPIO_Init(GPIOB, GPIO_Pin_6, GPIO_Mode_Out_PP_Low_Fast);
#ifdef I2C_ALWAYS_ON
    GPIO_SetBits(GPIOB, GPIO_Pin_6);
#endif
    
    delay(0xFFFF); // delay 84ms
    
    CLK_PeripheralClockConfig(CLK_Peripheral_I2C1, ENABLE);
}


static  void  i2cHwInit(uint8_t eepromAddress)
{
    I2C_DeInit(I2C1);
    I2C_Cmd(I2C1, ENABLE);
    I2C_Init(I2C1, I2C_SPEED, eepromAddress, I2C_Mode_I2C, I2C_DutyCycle_2, I2C_Ack_Enable, I2C_AcknowledgedAddress_7bit);
}

void i2cPowerOn (void)
{
#ifndef I2C_ALWAYS_ON
    GPIO_SetBits(GPIOB, GPIO_Pin_6);
    delay(0xffff);        // delay 84ms
#endif
}

void i2cPowerOff (void)
{
#ifndef I2C_ALWAYS_ON
    delay(0xffff);        // delay 84ms
//    GPIO_ResetBits(GPIOB, GPIO_Pin_6);
#endif
}

ErrorCode i2cReadBuffer(uint8_t *pBuffer, uint16_t ReadAddr, uint8_t NumByteToRead, uint8_t eepromAddress)
{
    ErrorCode status = ERROR_NONE;
    uint16_t i2cRetry = I2C_RETRY;
    while(i2cRetry > 0)
    {
        status = i2cReadBufferWorker(pBuffer, ReadAddr, NumByteToRead, eepromAddress);
        if (status == ERROR_NONE)
            break;
        delay(16000);        // delay 20ms
        i2cRetry--;
    }
#ifdef DEBUG
    if (status != ERROR_NONE) {
        BREAKPOINT();
    }
#endif
    return status;
}

ErrorCode i2cWriteBuffer(const uint8_t *pBuffer, uint16_t WriteAddr, uint8_t NumByteToWrite, uint8_t eepromAddress)
{
    ErrorCode status = ERROR_NONE;
    uint16_t i2cRetry = I2C_RETRY;
    uint8_t  rd_buf[4];

    while(i2cRetry > 0)
    {
        status = i2cWriteBufferWorker(pBuffer, WriteAddr, NumByteToWrite, eepromAddress);
        if (status == ERROR_NONE) {
            if (eepromAddress == EEPROM_SYSTEM_ADDRESS) {
                break;
            } else {
                status = i2cReadBufferWorker(&rd_buf[0], WriteAddr, NumByteToWrite, eepromAddress);
                if (status == ERROR_NONE) {
                    if (memcmp(pBuffer, rd_buf, NumByteToWrite) == 0) {
                        break;
                    } else {
                        status = ERROR_I2C_WRITE_CHECK;
                    }
                }
            }
        }
        delay(16000);        // delay 20ms
        i2cRetry--;
    }
#ifdef DEBUG
    if (status != ERROR_NONE) {
        BREAKPOINT();
    }
#endif
    return status;
}

static  ErrorCode  i2cWriteBufferWorker(const uint8_t *pBuffer, uint16_t WriteAddr, uint8_t NumByteToWrite, uint8_t eepromAddress)
{
    uint8_t  retry;


    i2cHwInit(eepromAddress);
    FlagStatus f = SET;

    for (retry = 0; retry < 10; retry++)
    {
        unsigned int cpt = 0;
        while((cpt < TIMEOUT) && (f == SET))
        {
            f = I2C_GetFlagStatus(I2C1, I2C_FLAG_BUSY);
            cpt++;
        }
        if (f != SET)
            break;
    }
    if (f == SET)
    {
        BREAKPOINT();
        return (ERROR_I2C_BUS_BUSY);
    }

    //--------------------------------------------------------------------------
    I2C_GenerateSTART(I2C1, ENABLE);
    ErrorStatus v = ERROR;
    unsigned int cpt = 0;
    while((cpt < TIMEOUT) && (v == ERROR))
    {
        v = I2C_CheckEvent(I2C1, I2C_EVENT_MASTER_MODE_SELECT);
        cpt++;
    }	
    if (v == ERROR)
    {
        I2C_GenerateSTOP(I2C1, ENABLE);
        BREAKPOINT();
        return (ERROR_I2C_EVENT_MASTER_MODE_SELECT);
    }

    //--------------------------------------------------------------------------
    I2C_Send7bitAddress(I2C1, eepromAddress, I2C_Direction_Transmitter);
    v = ERROR;
    cpt = 0;
    while((cpt < TIMEOUT) && (v == ERROR))
    {
        v = I2C_CheckEvent(I2C1, I2C_EVENT_MASTER_TRANSMITTER_MODE_SELECTED);
        cpt++;
    }
    if (v == ERROR)
    {
        I2C_GenerateSTOP(I2C1, ENABLE);
        //BREAKPOINT();
        return (ERROR_I2C_EVENT_MASTER_TRANSMITTER_MODE_SELECTED);
    }

    //--------------------------------------------------------------------------
    I2C_GetFlagStatus(I2C1, I2C_FLAG_ADDR);
    (void)(I2C1->SR3);
    //----(((((   MSB   ))))))---//
    I2C_SendData(I2C1, (uint8_t)(WriteAddr >> 8));
    v = ERROR;
    cpt = 0;
    while((cpt < TIMEOUT) && (v == ERROR))
    {
          v = I2C_CheckEvent(I2C1, I2C_EVENT_MASTER_BYTE_TRANSMITTING);
          cpt++;
    }
    if (v == ERROR)
    {
        I2C_GenerateSTOP(I2C1, ENABLE);
        BREAKPOINT();
        return (ERROR_I2C_EVENT_MASTER_BYTE_TRANSMITTING);
    }

    //----(((((   LSB   ))))))---//
    I2C_SendData(I2C1, (uint8_t)(WriteAddr));
    v = ERROR;
    cpt = 0;
    while((cpt < TIMEOUT) && (v == ERROR))
    {
        v = I2C_CheckEvent(I2C1, I2C_EVENT_MASTER_BYTE_TRANSMITTING);
        cpt++;
    }
    if (v == ERROR)
    {
        I2C_GenerateSTOP(I2C1, ENABLE);
        BREAKPOINT();
        return (ERROR_I2C_EVENT_MASTER_BYTE_TRANSMITTING);
    }

    while(NumByteToWrite--)
    {
        I2C_SendData(I2C1, *pBuffer);
        pBuffer++;
        while (!I2C_CheckEvent(I2C1, I2C_EVENT_MASTER_BYTE_TRANSMITTED));
    }
    I2C_GenerateSTOP(I2C1, ENABLE);
    delay(8000);        // delay 10ms
    return (ERROR_NONE);
}

static  ErrorCode  i2cReadBufferWorker(uint8_t *pBuffer, uint16_t ReadAddr, uint8_t NumByteToRead, uint8_t eepromAddress)
{
    uint8_t  retry;


    i2cHwInit(eepromAddress);
    FlagStatus f = SET;

    for (retry = 0; retry < 10; retry++)
    {
        unsigned int cpt = 0;
        while((cpt < TIMEOUT) && (f == SET))
        {
            f = I2C_GetFlagStatus(I2C1, I2C_FLAG_BUSY);
            cpt++;
        }
        if (f != SET)
           break;
    }
    if (f == SET)
    {
        BREAKPOINT();
        return (ERROR_I2C_BUS_BUSY);
    }

    //--------------------------------------------------------------------------
    I2C_GenerateSTART(I2C1, ENABLE);
    ErrorStatus v = ERROR;
    unsigned int cpt = 0;
    while((cpt < TIMEOUT) && (v == ERROR))
    {
        v = I2C_CheckEvent(I2C1, I2C_EVENT_MASTER_MODE_SELECT);
        cpt++;
    }	
    if (v == ERROR)
    {
        I2C_GenerateSTOP(I2C1, ENABLE);
        BREAKPOINT();
        return (ERROR_I2C_EVENT_MASTER_MODE_SELECT);
    }

    //--------------------------------------------------------------------------
    I2C_Send7bitAddress(I2C1, eepromAddress, I2C_Direction_Transmitter);
    v = ERROR;
    cpt = 0;
    while((cpt < TIMEOUT) && (v == ERROR))
    {
        v = I2C_CheckEvent(I2C1, I2C_EVENT_MASTER_TRANSMITTER_MODE_SELECTED);
        cpt++;
    }		
    if (v == ERROR)
    {
        I2C_GenerateSTOP(I2C1, ENABLE);
        BREAKPOINT();
        return (ERROR_I2C_EVENT_MASTER_TRANSMITTER_MODE_SELECTED);
    }

    //--------------------------------------------------------------------------
    I2C_GetFlagStatus(I2C1, I2C_FLAG_ADDR);
    (void)(I2C1->SR3);
    //----(((((   MSB   ))))))---//
    I2C_SendData(I2C1, (uint8_t)(ReadAddr >> 8));
    v = ERROR;
    cpt = 0;
    while((cpt < TIMEOUT) && (v == ERROR))
    {
        v = I2C_CheckEvent(I2C1, I2C_EVENT_MASTER_BYTE_TRANSMITTED);
        cpt++;
    }
    if (v == ERROR)
    {
        I2C_GenerateSTOP(I2C1, ENABLE);
        BREAKPOINT();
        return (ERROR_I2C_EVENT_MASTER_BYTE_TRANSMITTED);
    }

    //----(((((   LSB   ))))))---//
    I2C_SendData(I2C1, (uint8_t)(ReadAddr));
    v = ERROR;
    cpt = 0;
    while((cpt < TIMEOUT) && (v == ERROR))
    {
        v = I2C_CheckEvent(I2C1, I2C_EVENT_MASTER_BYTE_TRANSMITTED);
        cpt++;
    }
    if (v == ERROR)
    {
        I2C_GenerateSTOP(I2C1, ENABLE);
        BREAKPOINT();
        return (ERROR_I2C_EVENT_MASTER_BYTE_TRANSMITTED);
    }

    //--------------------------------------------------------------------------
    I2C_GenerateSTART(I2C1, ENABLE);
    v = ERROR;
    cpt = 0;
    while((cpt < TIMEOUT) && (v == ERROR))
    {
        v = I2C_CheckEvent(I2C1, I2C_EVENT_MASTER_MODE_SELECT);
        cpt++;
    }	
    if (v == ERROR)
    {
        I2C_GenerateSTOP(I2C1, ENABLE);
        BREAKPOINT();
        return (ERROR_I2C_EVENT_MASTER_MODE_SELECT);
    }

    //--------------------------------------------------------------------------
    I2C_Send7bitAddress(I2C1, eepromAddress, I2C_Direction_Receiver);
    v = ERROR;
    cpt = 0;
    while((cpt < TIMEOUT) && (v == ERROR))
    {
        v = I2C_CheckEvent(I2C1, I2C_EVENT_MASTER_RECEIVER_MODE_SELECTED);
        cpt++;
    }
    if (v == ERROR)
    {
        I2C_GenerateSTOP(I2C1, ENABLE);
        BREAKPOINT();
        return (ERROR_I2C_EVENT_MASTER_RECEIVER_MODE_SELECTED);
    }

    I2C_GetFlagStatus(I2C1, I2C_FLAG_ADDR);
    (void)(I2C1->SR3);
    while(NumByteToRead)
    {
        if(NumByteToRead == 1)
        {
            I2C_AcknowledgeConfig(I2C1, DISABLE);
        }

        if (I2C_CheckEvent(I2C1, I2C_EVENT_MASTER_BYTE_RECEIVED))
        {
            *pBuffer = I2C_ReceiveData(I2C1);
            pBuffer++;
            NumByteToRead--;
        }
    }
    I2C_AckPositionConfig(I2C1, I2C_AckPosition_Current);
    I2C_GenerateSTOP(I2C1, ENABLE);
    delay(8000);        // delay 10ms
    return (ERROR_NONE);
}
/*************************** END OF FILE **************************************/
