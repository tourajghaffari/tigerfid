/*******************************************************************************
* @file     i2c_ee.c
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
* v3.1.0              Tamper Switch Debounce Handler WITHOUT Timer2
* v3.2.0              Battery Low Voltage History is Updated when Timer is on only
*******************************************************************************/ 
#include "stm8l15x.h"
#include "stm8l15x_i2c.h"
#include "i2c_ee.h"
#include "blocker_services.h"

FlagStatus f = 0;
unsigned int cpt = 0;
unsigned int timeout = 60000;
extern void delay(__IO uint16_t counter);

void i2cInit(uint8_t eepromAddress)
{
    I2C_DeInit(I2C1);
    I2C_Cmd(I2C1, ENABLE);
    I2C_Init(I2C1, I2C_SPEED, eepromAddress, I2C_Mode_I2C, I2C_DutyCycle_2, I2C_Ack_Enable, I2C_AcknowledgedAddress_7bit);
}

ErrorStatus i2cReadBuffer(uint8_t *pBuffer, uint16_t ReadAddr, uint8_t NumByteToRead, uint8_t eepromAddress)
{
    ErrorStatus status = SUCCESS;
    uint16_t i2cRetry = I2C_RETRY;
    while(i2cRetry > 0)
    {
        status = i2cReadBufferWorker(pBuffer, ReadAddr, NumByteToRead, eepromAddress);
        if (status == SUCCESS)
            break;
        i2cRetry--;
    }
    return status;
}

ErrorStatus i2cWriteBuffer(uint8_t *pBuffer, uint16_t WriteAddr, uint8_t NumByteToWrite, uint8_t eepromAddress)
{
    ErrorStatus status = SUCCESS;  
    uint16_t i2cRetry = I2C_RETRY;
    while(i2cRetry > 0)
    {
        status = i2cWriteBufferWorker(pBuffer, WriteAddr, NumByteToWrite, eepromAddress); 
        if (status == SUCCESS)
            break;
        i2cRetry--;        
    }
    return status;    
}

ErrorStatus i2cWriteBufferWorker(uint8_t *pBuffer, uint16_t WriteAddr, uint8_t NumByteToWrite, uint8_t eepromAddress)
{
    ErrorStatus v = SUCCESS;
    i2cInit(eepromAddress);
    f = SET;    
    while(TRUE)  
    {
        cpt = 0;
        while (cpt != timeout && f == SET)
        {
            f = I2C_GetFlagStatus(I2C1, I2C_FLAG_BUSY);
            cpt++;
        }
        if (f != SET)
            break;        
    }
    if (f == SET)
    {
        v = ERROR_I2C_BUS_BUSY;
        return v;  
    }    
    
    //--------------------------------------------------------------------------
    I2C_GenerateSTART(I2C1, ENABLE);
    v = ERROR;
    cpt = 0;
    while(cpt < timeout && v == ERROR)
    {
        v = I2C_CheckEvent(I2C1, I2C_EVENT_MASTER_MODE_SELECT);
        cpt++;
    }	
    if (v == ERROR)
    {
        I2C_GenerateSTOP(I2C1, ENABLE);
        v = ERROR_I2C_EVENT_MASTER_MODE_SELECT;
        return v;    
    }

    //-------------------------------------------------------------------------- 
    I2C_Send7bitAddress(I2C1, eepromAddress, I2C_Direction_Transmitter);
    v = ERROR;
    cpt = 0;
    while (cpt < timeout && v == ERROR)
    {
        v = I2C_CheckEvent(I2C1, I2C_EVENT_MASTER_TRANSMITTER_MODE_SELECTED);
        cpt++;
    }
    if (v == ERROR)
    {
        I2C_GenerateSTOP(I2C1, ENABLE);
        v = ERROR_I2C_EVENT_MASTER_TRANSMITTER_MODE_SELECTED;        
        return v;
    }

    //-------------------------------------------------------------------------- 
    I2C_GetFlagStatus(I2C1, I2C_FLAG_ADDR);
    (void)(I2C1->SR3);
    //----(((((   MSB   ))))))---//
    I2C_SendData(I2C1, (uint8_t)(WriteAddr >> 8));
    v = ERROR;
    cpt = 0;
    while (cpt < timeout && v == ERROR)
    {
          v = I2C_CheckEvent(I2C1, I2C_EVENT_MASTER_BYTE_TRANSMITTING);
          cpt++;
    }
    if (v == ERROR)
    {
        I2C_GenerateSTOP(I2C1, ENABLE);
        v = ERROR_I2C_EVENT_MASTER_BYTE_TRANSMITTING;         
        return v;
    }

    //----(((((   LSB   ))))))---//
    I2C_SendData(I2C1, (uint8_t)(WriteAddr));
    v = ERROR;
    cpt = 0;
    while(cpt < timeout && v == ERROR)
    {
        v = I2C_CheckEvent(I2C1, I2C_EVENT_MASTER_BYTE_TRANSMITTING);
        cpt++;
    }
    if (v == ERROR)
    {
        I2C_GenerateSTOP(I2C1, ENABLE);
        v = ERROR_I2C_EVENT_MASTER_BYTE_TRANSMITTING;
        return v;
    }
           
    while(NumByteToWrite--)  
    {
        I2C_SendData(I2C1, *pBuffer);
        pBuffer++; 
        while (!I2C_CheckEvent(I2C1, I2C_EVENT_MASTER_BYTE_TRANSMITTED));
    }
    I2C_GenerateSTOP(I2C1, ENABLE);
    delay(0xffff);    
    return SUCCESS;
}

ErrorStatus i2cReadBufferWorker(uint8_t *pBuffer, uint16_t ReadAddr, uint8_t NumByteToRead, uint8_t eepromAddress)
{ 
    ErrorStatus v = SUCCESS;  
    i2cInit(eepromAddress);
    f = SET;
    while(TRUE)  
    {
        cpt = 0;
        while (cpt != timeout && f == SET)
        {
            f = I2C_GetFlagStatus(I2C1, I2C_FLAG_BUSY);
            cpt++;
        }
        if (f != SET)
           break;
    }
    if (f == SET)
    {
        v = ERROR_I2C_BUS_BUSY;
        return v;    
    }    
    
    //--------------------------------------------------------------------------
    I2C_GenerateSTART(I2C1, ENABLE);
    v = ERROR;
    cpt = 0;
    while(cpt < timeout && v == ERROR)
    {
        v = I2C_CheckEvent(I2C1, I2C_EVENT_MASTER_MODE_SELECT);
        cpt++;
    }	
    if (v == ERROR)
    {
        I2C_GenerateSTOP(I2C1, ENABLE);
        v = ERROR_I2C_EVENT_MASTER_MODE_SELECT;        
        return v;
    }

    //--------------------------------------------------------------------------
    I2C_Send7bitAddress(I2C1, eepromAddress, I2C_Direction_Transmitter);
    v = ERROR;
    cpt = 0;
    while(cpt < timeout && v == ERROR)
    {
        v = I2C_CheckEvent(I2C1, I2C_EVENT_MASTER_TRANSMITTER_MODE_SELECTED);
        cpt++;
    }		
    if (v == ERROR)
    {
        I2C_GenerateSTOP(I2C1, ENABLE);
        v = ERROR_I2C_EVENT_MASTER_TRANSMITTER_MODE_SELECTED;                        
        return v;
    }
    
    //--------------------------------------------------------------------------
    I2C_GetFlagStatus(I2C1, I2C_FLAG_ADDR);
    (void)(I2C1->SR3);
    //----(((((   MSB   ))))))---//
    I2C_SendData(I2C1, (uint8_t)(ReadAddr >> 8));
    v = ERROR;
    cpt = 0;
    while(cpt < timeout && v == ERROR)
    {
        v = I2C_CheckEvent(I2C1, I2C_EVENT_MASTER_BYTE_TRANSMITTED);
        cpt++;
    }      
    if (v == ERROR)
    {
        I2C_GenerateSTOP(I2C1, ENABLE);
        v = ERROR_I2C_EVENT_MASTER_BYTE_TRANSMITTED;                
        return v;
    }

    //----(((((   LSB   ))))))---//
    I2C_SendData(I2C1, (uint8_t)(ReadAddr));
    v = ERROR;
    cpt = 0;
    while(cpt < timeout && v == ERROR)
    {
        v = I2C_CheckEvent(I2C1, I2C_EVENT_MASTER_BYTE_TRANSMITTED);
        cpt++;
    }              
    if (v == ERROR)
    {
        I2C_GenerateSTOP(I2C1, ENABLE);
        v = ERROR_I2C_EVENT_MASTER_BYTE_TRANSMITTED;        
        return v;
    }
    
    //--------------------------------------------------------------------------
    I2C_GenerateSTART(I2C1, ENABLE);
    v = ERROR;
    cpt = 0;
    while(cpt < timeout && v == ERROR)
    {
        v = I2C_CheckEvent(I2C1, I2C_EVENT_MASTER_MODE_SELECT);
        cpt++;
    }	
    if (v == ERROR)
    {
        I2C_GenerateSTOP(I2C1, ENABLE);
        v = ERROR_I2C_EVENT_MASTER_MODE_SELECT;                
        return v;
    }

    //--------------------------------------------------------------------------
    I2C_Send7bitAddress(I2C1, eepromAddress, I2C_Direction_Receiver);
    v = ERROR;
    cpt = 0;
    while(cpt < timeout && v == ERROR)
    {
        v = I2C_CheckEvent(I2C1, I2C_EVENT_MASTER_RECEIVER_MODE_SELECTED);
        cpt++;
    }
    if (v == ERROR)
    {
        I2C_GenerateSTOP(I2C1, ENABLE);
        v = ERROR_I2C_EVENT_MASTER_RECEIVER_MODE_SELECTED;        
        return v;
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
    delay(0xffff);    
    return SUCCESS;
}
/*************************** END OF FILE **************************************/