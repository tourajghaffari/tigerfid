/*******************************************************************************
* @file     blocker_services.c
* @author   imoware
* @version  V2.9.0
* @date     04/10/12
********************************************************************************
* @version history    Description
* ----------------    -----------
* v2.5.0              LSI Clock Enable for RTC (deprecated with 2.7 release)
* v2.6.0              LSI Clock Calibration (deprecated with 2.7 release)
* v2.7.0              LSE Clock Enable for RTC
* v2.8.0              (1) Tag Version, (2) Last Packet Reserved
* v2.9.0              Tamper Switch Debounce Handler
*******************************************************************************/ 
#include "blocker_services.h"
#include "i2c_ee.h"

uint16_t commandAddress 	    = 0x0000;
uint16_t commandStatusAddress 	    = 0x0001;
uint16_t tagVersionAddress 	    = 0x0013;
uint16_t interruptStatusAddress	    = 0x0016;
uint16_t batteryVoltageAddress 	    = 0x0017;
uint16_t histPacketAddress	    = 0x0004;
uint16_t histPacketNumberAddress    = 0x0006;
uint16_t rtcDateAddress		    = 0x0008;
uint16_t rtcTimeAddress		    = 0x000c;
uint16_t empIdAddress 	  	    = 0x0010;
uint16_t reservedTestAddress        = 0x0015; 

uint8_t histPacket[2] = {0x00, 0x00};
uint8_t histPacketNumber[2] = {0x00, 0x00};

__IO bool rtcConfigured = FALSE;
__IO bool historyRead = FALSE;
__IO bool testTamperSwitch = FALSE;
__IO bool lowVoltBatteryDetected = FALSE;
__IO uint16_t timerTick = 0;

RTC_InitTypeDef init;
RTC_TimeTypeDef time;
RTC_DateTypeDef date;

void tamperIRQHandler()
{
    bool updateHistory = TRUE;
    bool bandOpen = FALSE;
    uint8_t empId[2] = {0x00, 0x00};
    
    if (rtcConfigured == FALSE)
    {
        if (testTamperSwitch == FALSE)      
            return;
    }
    
    if (testTamperSwitch == TRUE)
        updateHistory = FALSE;
    else
        updateHistory = TRUE;
          
    if ((GPIO_ReadInputDataBit((GPIO_TypeDef*)GPIOB, GPIO_Pin_3)) != RESET) 
    {
        bandOpen = isBandOpen();
        if (bandOpen == TRUE)
        {
            updateInterruptStatus(IS_TAMPER_BAND_OPENED, updateHistory);
        }
    }
    else
    {
        updateInterruptStatus(IS_TAMPER_BAND_CLOSED, updateHistory);
    }
    
    if ((testTamperSwitch == FALSE) && (bandOpen == TRUE))
    {
        readRTC();
        updateCommandHistory(TAMPER_INTERRUPT);
        readEmployeeEE(empId);
        updateEmployeeHistory(empId);                    
        setNextPacketHistory();
        stopTimers();
    }
}

bool isBandOpen()
{
    bool bandOpen = FALSE;
    uint16_t bounceTamperCounter = TAMPER_DETECTION_THRESHOLD;
     
    while (bounceTamperCounter != 0)
    {
        delay(0x00ff);
        if ((GPIO_ReadInputDataBit((GPIO_TypeDef*)GPIOB, GPIO_Pin_3)) != RESET)
            {
                bounceTamperCounter--;
                bandOpen = TRUE;
            }
            else
            {
                bandOpen = FALSE;
                break;
            }       
        }
          
    return bandOpen;
}
    
/***
void tamperIRQHandler()
{
    bool updateHistory = TRUE;
    uint8_t empId[2] = {0x00, 0x00};
    
    if (rtcConfigured == FALSE)
    {
        if (testTamperSwitch == FALSE)      
            return;
    }
    
    if (testTamperSwitch == TRUE)
        updateHistory = FALSE;
    else
        updateHistory = TRUE;
          
    if ((GPIO_ReadInputDataBit((GPIO_TypeDef*)GPIOB, GPIO_Pin_3)) != RESET) 
    {
        updateInterruptStatus(IS_TAMPER_BAND_OPENED, updateHistory);
    }
    else
    {
        updateInterruptStatus(IS_TAMPER_BAND_CLOSED, updateHistory);
    }
    
    if (testTamperSwitch == FALSE)
    {
        readRTC();
        updateCommandHistory(TAMPER_INTERRUPT);
        readEmployeeEE(empId);
        updateEmployeeHistory(empId);                    
        setNextPacketHistory();
        stopTimers();
    }
}
****/

/*****
void tamperIRQHandler()
{
    uint8_t empId[2] = {0x00, 0x00}; 
    if (rtcConfigured == FALSE)
    {
        if (testTamperSwitch == FALSE)      
            return;
    }
            
    if ((GPIO_ReadInputDataBit((GPIO_TypeDef*)GPIOB, GPIO_Pin_3)) != RESET) 
    {
        t4Enable();
    }
    else
    {
        t4Disable();
        updateInterruptStatus(IS_TAMPER_BAND_CLOSED, TRUE);
        
        readRTC();
        updateCommandHistory(TAMPER_INTERRUPT);
        readEmployeeEE(empId);
        updateEmployeeHistory(empId);                    
        setNextPacketHistory();
    }
}
******/

void timer4IRQHandler()
{
    if (TIM4_GetITStatus(TIM4_IT_Update) != RESET)
    {
        timerTick++;
        if (timerTick >= TAMPER_DETECTION_THRESHOLD)
        {
            t4Disable();
            if ((GPIO_ReadInputDataBit((GPIO_TypeDef*)GPIOB, GPIO_Pin_3)) != RESET)
            {
                // tamper detected!
                reportTamper();
            }          
        }
    }
}
            	
void batteryVoltageIRQHandler()
{
    if (PWR_PVDGetITStatus() == RESET)
    {
        lowVoltBatteryDetected = FALSE;
        updateBatteryVoltage(BATTERY_VOLT_NORMAL);        
        return;
    }
    
    if (PWR_GetFlagStatus(PWR_FLAG_PVDOF) != RESET)
    {
        readRTC();
        lowVoltBatteryDetected = TRUE;
        updateCommandHistory(LOW_BATTERY_INTERRUPT);
        updateBatteryVoltage(BATTERY_VOLT_LOW);
        setNextPacketHistory();        
    }
}
      
void rfCommandIRQHandler()
{
    uint8_t status = SUCCESS;
    uint8_t commandStatus = COMMAND_STATUS_UNKNOWN;
    bool updateHistory = TRUE;
    
    status = readCommandStatus(&commandStatus);
    if (status != SUCCESS)
        return;
        
    if ((commandStatus == COMMAND_STATUS_BUSY))
    {
        setCommandStatus(COMMAND_STATUS_BUSY_ACK, FALSE);
        return;
    }
    
    if ((commandStatus == COMMAND_STATUS_BUSY_ACK))
        return;

    if (commandStatus == COMMAND_STATUS_RECEIVED)
    {
        status = setCommandStatus(COMMAND_STATUS_RUNNING, FALSE);
        if (status != SUCCESS)
            return;        

        if ((GPIO_ReadInputDataBit((GPIO_TypeDef*)GPIOB, GPIO_Pin_3)) != RESET)
        {
            updateInterruptStatus(IS_TAMPER_BAND_OPENED, FALSE);
        }
        else
        {
            updateInterruptStatus(IS_TAMPER_BAND_CLOSED, FALSE);
        }        
        commandHandler();
    }
    else
    {
        setCommandStatus(ERROR_INVALID_COMMAND_STATUS, TRUE);
    }
}

ErrorStatus commandHandler()
{
    ErrorStatus status = SUCCESS;
    uint8_t command = NOOP;
    uint8_t interruptStatus = IS_TAMPER_BAND_CLOSED;
    uint8_t empId[2] = {0x00, 0x00};
    bool updateHistory = TRUE;
      
    status = readCommand(&command);
    if (status != SUCCESS)
        return status;
                                
    if (command == READ_RTC)
        status = commandReadRTC();
    else if (command == START_TIMERS)
        status = commandStartTimers();
    else if (command == STOP_TIMERS)
        status = commandStopTimers();
    else if (command == READ_HISTORY)
        status = commandReadHistory();
    else if (command == FLUSH_HISTORY)
        status = commandFlushHistory();
    else if (command == READ_BATTERY_VOLTAGE)
        status = commandReadBatteryVoltage();
    else if (command == TAMPER_SWITCH_TEST)
        status = tamperSwitchTest();
    else if (command == FACTORY_DATA_RESET)
        status = commandFactoryDataReset();
    else if (command != NOOP)
    {
        status = ERROR_INVALID_COMMAND;
        setCommandStatus(status, TRUE); 
    }

    if ((command != FACTORY_DATA_RESET) || (command != TAMPER_SWITCH_TEST))
    {      
        updateCommandHistory(command);
        readEmployeeEE(empId);
        updateEmployeeHistory(empId);    
    }
    
    if ((command == FACTORY_DATA_RESET) || (command == TAMPER_SWITCH_TEST))
        updateHistory = FALSE;
    
    if (command == STOP_TIMERS)
        rtcConfigured = TRUE;

    if (status == SUCCESS)
    {        
        setCommandStatus(COMMAND_STATUS_COMPLETE, updateHistory);
    }
    
    if ((GPIO_ReadInputDataBit((GPIO_TypeDef*)GPIOB, GPIO_Pin_3)) != RESET)
    {
        updateInterruptStatus(IS_TAMPER_BAND_OPENED, updateHistory);
    }
    else
    {
        updateInterruptStatus(IS_TAMPER_BAND_CLOSED, updateHistory);
    }        

    if ((command != FACTORY_DATA_RESET) || (command != TAMPER_SWITCH_TEST))
    {
        setNextPacketHistory();
    }
    if (command == STOP_TIMERS)
        rtcConfigured = FALSE;    
    
    return status;
}

ErrorStatus commandReadRTC()
{
    ErrorStatus status = SUCCESS;
    if (rtcConfigured == TRUE)
    {
        status = readRTC();
    }
    else
    {
        status = ERROR_RTC_CONFIG_REQUIRED;
        setCommandStatus(status, FALSE);
    }
    return status;
}

ErrorStatus commandStartTimers()
{
    ErrorStatus status = SUCCESS;
    uint8_t interruptStatus = IS_TAMPER_BAND_CLOSED;
    rtcConfigured = FALSE;
    testTamperSwitch = FALSE;
    
    status = readInterruptStatus(&interruptStatus);    
    if (((interruptStatus & IS_TAMPER_BAND_OPENED) == IS_TAMPER_BAND_OPENED) && (status == SUCCESS))
    {
        status = ERROR_BAND_OPEN_MUST_CLOSE;
        setCommandStatus(ERROR_BAND_OPEN_MUST_CLOSE, TRUE);
    }
    else
    {
        if ((interruptStatus & IS_TAMPER_BAND_CLOSED) == IS_TAMPER_BAND_CLOSED)
        {
            status = configureRTC();
            if (status == SUCCESS)
            {
                status = readRTC();
                if (status == SUCCESS)
                {                
                    if ((status = setHarvestUserConfiguration()) == SUCCESS)
                    {
                        rtcConfigured = TRUE;
                    }
                }
            }
        }
    }
    
    if (status != SUCCESS)
    {
        setCommandStatus(status, TRUE);
    }  
    return status;  
}

ErrorStatus commandStopTimers()
{
    ErrorStatus status = SUCCESS;
    
    if (rtcConfigured == TRUE)
    {
        status = readRTC();
        if (status == SUCCESS)
        {
            stopTimers();           
        }
    }
    else
    {
        status = ERROR_RTC_CONFIG_REQUIRED;
        setCommandStatus(status, TRUE);
    }
    return status;
}

ErrorStatus commandReadHistory()
{
    ErrorStatus status = SUCCESS;
    
    status = readRTC();
    if (status == SUCCESS)
    {
        historyRead = TRUE;
    }
    return status;
}

ErrorStatus commandFlushHistory()
{
    ErrorStatus status = SUCCESS;
    
    status = readRTC();
    if (status != SUCCESS)
      return status;
    
    if (rtcConfigured == FALSE)
    {
        status = ERROR_FLUSH_FIRST_READ_HISTORY;
        setCommandStatus(status, FALSE);
    }
    else if (historyRead == FALSE)
    {
        status = ERROR_FLUSH_FIRST_READ_HISTORY;
        setCommandStatus(status, TRUE);				
    }
    else
    {
        status = flushHistory();
        if (status == SUCCESS)
            historyRead = FALSE;
    }  
    return status;
}

ErrorStatus commandReadBatteryVoltage()
{
    ErrorStatus status = SUCCESS;
    status = readRTC();
    if (status != SUCCESS)
        return status;      
  
    if (lowVoltBatteryDetected == TRUE)
        status = updateBatteryVoltage(BATTERY_VOLT_LOW);
    else
        status = updateBatteryVoltage(BATTERY_VOLT_NORMAL);
    
    return status;
}

ErrorStatus commandFactoryDataReset()
{
    ErrorStatus status = SUCCESS;
    status = initBlockerEE(FALSE);
    if (status != SUCCESS)
    {
        setCommandStatus(status, FALSE);				        
    }
    return status;
 }

ErrorStatus configureRTC()
{
    /***
    RTC_InitTypeDef init;
    RTC_TimeTypeDef time;
    RTC_DateTypeDef date;
    ***/
    ErrorStatus status = SUCCESS;
    startTimers();    

    init.RTC_HourFormat = RTC_HourFormat_24;
    init.RTC_AsynchPrediv = 0x7c;
    init.RTC_SynchPrediv = 0x7cf;
    RTC_Init(&init);

    RTC_DateStructInit(&date);
    status = readDateEE(&date);
    if (status != SUCCESS)
    {
        setCommandStatus(status, TRUE);
        return status;
    }
    RTC_SetDate(RTC_Format_BIN, &date);
    
    RTC_TimeStructInit(&time);
    status = readTimeEE(&time);
    if (status != SUCCESS)
    {
        setCommandStatus(status, TRUE);
        return status;
    }
    RTC_SetTime(RTC_Format_BIN, &time);
    return status;
}

ErrorStatus readDateEE(RTC_DateTypeDef *date)
{
    ErrorStatus status = SUCCESS;
    uint8_t rtcDate[4] = {0x00, 0x00, 0x00, 0x00};
    
    status = i2cReadBuffer(rtcDate, rtcDateAddress, 0x04, (uint8_t)EEPROM_USER_ADDRESS);
    if (status == SUCCESS)
    {
        date->RTC_WeekDay = rtcDate[3];
        date->RTC_Month = rtcDate[2];
        date->RTC_Date = rtcDate[1];
        date->RTC_Year = rtcDate[0];                
    }
    return status;
}

ErrorStatus readTimeEE(RTC_TimeTypeDef *time)
{
    ErrorStatus status = SUCCESS;
    uint8_t rtcTime[3] = {0x00, 0x00, 0x00};
    
    status = i2cReadBuffer(rtcTime, rtcTimeAddress, 0x03, (uint8_t)EEPROM_USER_ADDRESS);
    if (status == SUCCESS)
    {
        time->RTC_Hours = rtcTime[2];
        time->RTC_Minutes = rtcTime[1];
        time->RTC_Seconds = rtcTime[0];
    }
    return status;
}

ErrorStatus getTimeTest(RTC_TimeTypeDef *pTime)
{
  
    ErrorStatus status = SUCCESS;
    uint8_t rtcTime[3] = {0x00, 0x00, 0x00};    
    while (RTC_WaitForSynchro() != SUCCESS);     
    RTC_GetTime(RTC_Format_BIN, pTime);
    
#ifdef UPDATE_RF_EEPROM    
    rtcTime[2] = pTime->RTC_Hours;
    rtcTime[1] = pTime->RTC_Minutes;
    rtcTime[0] = pTime->RTC_Seconds;    
    return(i2cWriteBuffer(rtcTime, rtcTimeAddress, 0x03, (uint8_t)EEPROM_USER_ADDRESS));
#else
    return status;
#endif    
}

ErrorStatus readRTC()
{
    ErrorStatus status = SUCCESS;

    /**    
    RTC_TimeTypeDef time;
    RTC_DateTypeDef date;
    **/
    
    uint8_t rtcDate[4] = {0x00, 0x00, 0x00, 0x00};
    uint8_t rtcTime[3] = {0x00, 0x00, 0x00};
    uint16_t rtcHistoryAddress = getInt16(histPacket);

    /**    
    RTC_DateStructInit(&date);
    RTC_TimeStructInit(&time);
    **/    
    while (RTC_WaitForSynchro() != SUCCESS);   
    RTC_GetDate(RTC_Format_BIN, &date);
    RTC_GetTime(RTC_Format_BIN, &time);
    
    rtcDate[3] = date.RTC_WeekDay;
    rtcDate[2] = date.RTC_Month;
    rtcDate[1] = date.RTC_Date;
    rtcDate[0] = date.RTC_Year;    

    rtcTime[2] = time.RTC_Hours;
    rtcTime[1] = time.RTC_Minutes;
    rtcTime[0] = time.RTC_Seconds;    
              
    status = i2cWriteBuffer(rtcDate, rtcDateAddress, 0x04, (uint8_t)EEPROM_USER_ADDRESS);
    if (status == SUCCESS)
    {
        status = i2cWriteBuffer(rtcTime, rtcTimeAddress, 0x03, (uint8_t)EEPROM_USER_ADDRESS);
        if (status == SUCCESS)
        {
            status = i2cWriteBuffer(rtcDate, rtcHistoryAddress, 0x04, (uint8_t)EEPROM_USER_ADDRESS);
            if (status == SUCCESS)
            {
                status = i2cWriteBuffer(rtcTime, rtcHistoryAddress + 4, 0x03, (uint8_t)EEPROM_USER_ADDRESS);
            }
        }
    }
    if (status != SUCCESS)
    {
        setCommandStatus(status, TRUE);
    }
    return status;
}
    
void startTimers()
{
    CLK_RTCClockConfig(CLK_RTCCLKSource_HSI, CLK_RTCCLKDiv_64);     
    CLK_PeripheralClockConfig(CLK_Peripheral_RTC, ENABLE);
    delay((uint16_t)2000);
}

void stopTimers()
{
    CLK_PeripheralClockConfig(CLK_Peripheral_RTC, DISABLE);
    CLK_RTCClockConfig(CLK_RTCCLKSource_Off, CLK_RTCCLKDiv_1);
    rtcConfigured = FALSE;
    delay((uint16_t)2000);
}

ErrorStatus readCommand(uint8_t *pCommand)
{
    ErrorStatus status = i2cReadBuffer(pCommand, commandAddress, 0x01, (uint8_t)EEPROM_USER_ADDRESS);
    if (status != SUCCESS)
    {
        setCommandStatus(status, TRUE);
    }
    return status;
}

uint8_t readCommandStatus(uint8_t *pCommandStatus)
{
    return(i2cReadBuffer(pCommandStatus, commandStatusAddress, 0x01, (uint8_t)EEPROM_USER_ADDRESS));
}

uint8_t readInterruptStatus(uint8_t *pInterruptStatus)
{
    return(i2cReadBuffer(pInterruptStatus, interruptStatusAddress, 0x01, (uint8_t)EEPROM_USER_ADDRESS));
}

ErrorStatus flushHistory()
{
      ErrorStatus status = SUCCESS;
      uint8_t flushData = 0xff;
      uint16_t historySpaceAddress = EE_HISTORY_START_ADDRESS;
      uint16_t historySpace = EE_BLOCKER_SIZE - historySpaceAddress;
      
      disableInterrupts();
      while (historySpace > 0)
      {
          status = i2cWriteBuffer(&flushData, historySpaceAddress, 0x01, (uint8_t)EEPROM_USER_ADDRESS);
          if (status != SUCCESS)
          {           
              break;
          }
          historySpace--;
          historySpaceAddress++;
      }
      if (status != SUCCESS)
      {
          setCommandStatus(status, TRUE);
          enableInterrupts();          
          return status;
      }
      
      histPacket[0] = 0x18;
      histPacket[1] = 0x00;      

      histPacketNumber[0] = 0x00;
      histPacketNumber[1] = 0x00;      
      
      status = i2cWriteBuffer(histPacket, histPacketAddress, 0x02, (uint8_t)EEPROM_USER_ADDRESS);
      if (status == SUCCESS)
      {
          status = i2cWriteBuffer(histPacketNumber, histPacketNumberAddress, 0x02, (uint8_t)EEPROM_USER_ADDRESS);
      }
      enableInterrupts();
      return status;
}

ErrorStatus setNextPacketHistory()
{
    ErrorStatus status = SUCCESS;
    uint16_t histPacketInt;
    uint16_t histPacketNumberInt;    
      
    if (rtcConfigured == FALSE)
        return ERROR_RTC_CONFIG_REQUIRED;

    histPacketInt = getInt16(histPacket);    
    histPacketInt += 12;
    if (histPacketInt >= EE_LAST_PACKET) // 2028
    {
        histPacketInt = EE_HISTORY_START_ADDRESS;
        histPacketNumber[1] = 0x00;
        histPacketNumber[0] = 0x00;                                   
    }    
    histPacketNumberInt = getInt16(histPacketNumber);
    histPacketNumberInt++;    
    
    getArrayFromInt16(histPacketNumberInt, histPacketNumber);
    getArrayFromInt16(histPacketInt, histPacket);    

    status = i2cWriteBuffer(histPacket, histPacketAddress, 0x02, (uint8_t)EEPROM_USER_ADDRESS);
    if (status == SUCCESS)
    {
        status = i2cWriteBuffer(histPacketNumber, histPacketNumberAddress, 0x02, (uint8_t)EEPROM_USER_ADDRESS);
    }
    return status;
}

ErrorStatus updateInterruptStatus(uint8_t interruptStatus, bool updateHistory)
{
    ErrorStatus status = SUCCESS;  
    status = i2cWriteBuffer(&interruptStatus, interruptStatusAddress, 0x01, (uint8_t)EEPROM_USER_ADDRESS);     
    if (status == SUCCESS)
    {
        if ((updateHistory == TRUE) && (rtcConfigured == TRUE))
        {
            status  = updateInterruptStatusHistory(interruptStatus);
        }
    }
    return status;
}

ErrorStatus updateInterruptStatusHistory(uint8_t interruptStatus)
{
    uint16_t interruptStatusHistoryAddress = getInt16(histPacket) + 10;
    return(i2cWriteBuffer(&interruptStatus, interruptStatusHistoryAddress, 0x01, (uint8_t)EEPROM_USER_ADDRESS));
}

ErrorStatus updateBatteryVoltage(uint8_t batteryVoltage)
{
    return(i2cWriteBuffer(&batteryVoltage, batteryVoltageAddress, 0x01, (uint8_t)EEPROM_USER_ADDRESS));
}

ErrorStatus setCommandStatus(uint8_t commandStatus, bool updateHistory)
{
    ErrorStatus status = SUCCESS;
    status = i2cWriteBuffer(&commandStatus, commandStatusAddress, 0x01, (uint8_t)EEPROM_USER_ADDRESS);
    if (status != SUCCESS)
    {
        setCommandStatus(status, TRUE);      
        return status;
    }
    
    if (status == SUCCESS)    
    {
        if ((updateHistory == TRUE) && (rtcConfigured == TRUE))
        {
            status = updateCommandStatusHistory(commandStatus);
        }
    }
    return status;
}

ErrorStatus updateCommandStatusHistory(uint8_t status)
{
    uint16_t commandStatusHistoryAddress = getInt16(histPacket) + 11;
    return(i2cWriteBuffer(&status, commandStatusHistoryAddress, 0x01, (uint8_t)EEPROM_USER_ADDRESS));
}

ErrorStatus updateCommandHistory(uint8_t command)
{
    uint16_t commandHistory = getInt16(histPacket) + 7;
    return(i2cWriteBuffer(&command, commandHistory, 0x01, (uint8_t)EEPROM_USER_ADDRESS));
}

void readEmployeeEE(uint8_t *empId)
{
    ErrorStatus status = SUCCESS;    
    status = i2cReadBuffer(empId, empIdAddress, 0x02, (uint8_t)EEPROM_USER_ADDRESS);
    if (status != SUCCESS)
        setCommandStatus(status, TRUE);
}

void updateEmployeeHistory(uint8_t *empId)
{  
    ErrorStatus status = SUCCESS;      
    uint16_t empIdHistoryAddress = getInt16(histPacket) + 8;
    status = i2cWriteBuffer(empId, empIdHistoryAddress, 0x02, (uint8_t)EEPROM_USER_ADDRESS);
    if (status != SUCCESS)
        setCommandStatus(status, TRUE);    
}

ErrorStatus initBlockerEE(bool bootup)
{
    ErrorStatus status = SUCCESS;
    uint8_t commandStatus = COMMAND_STATUS_UNKNOWN;
    uint8_t empId[2] = {0x00, 0x00};    
    uint8_t command = NOOP;
    uint8_t batteryVoltage = BATTERY_VOLT_NORMAL;
    uint8_t tagVersion = TAG_VERSION;

    status = setRuntime(bootup);
    if (status != SUCCESS)
        return status;

    status = setHarvestUserConfiguration();
    if (status != SUCCESS)
        return status;
                 		
    status = i2cReadBuffer(histPacket, histPacketAddress, 0x02, (uint8_t)EEPROM_USER_ADDRESS);
    if (status != SUCCESS)
        return status;
    
    if ((histPacket[0] == 0xff) && (histPacket[1] == 0xff))
    {
        histPacket[0] = 0x18;
        histPacket[1] = 0x00;       
        status = i2cWriteBuffer(histPacket, histPacketAddress, 0x02, (uint8_t)EEPROM_USER_ADDRESS);
        if (status != SUCCESS)
            return status;        
    }
                    
    status = i2cReadBuffer(histPacketNumber, histPacketNumberAddress, 0x02, (uint8_t)EEPROM_USER_ADDRESS);
    if (status != SUCCESS)
        return status;

    if ((histPacketNumber[0] == 0xff) && (histPacketNumber[1] == 0xff))
    {
        histPacketNumber[0] = 0x00;
        histPacketNumber[1] = 0x00;        
        status = i2cWriteBuffer(histPacketNumber, histPacketNumberAddress, 0x02, (uint8_t)EEPROM_USER_ADDRESS);
        if (status != SUCCESS)
            return status;        
    }

    status = i2cWriteBuffer(&command, commandAddress, 0x01, (uint8_t)EEPROM_USER_ADDRESS);
    if (status != SUCCESS)
        return status;    
    status = i2cWriteBuffer(&commandStatus, commandStatusAddress, 0x01, (uint8_t)EEPROM_USER_ADDRESS);
    if (status != SUCCESS)
        return status;        
   
    if ((GPIO_ReadInputDataBit((GPIO_TypeDef*)GPIOB, GPIO_Pin_3)) != RESET)
    {
        status = updateInterruptStatus(IS_TAMPER_BAND_OPENED, FALSE);
        if (status != SUCCESS)
            return status;                
    }
    else
    {
        status = updateInterruptStatus(IS_TAMPER_BAND_CLOSED, FALSE);    
        if (status != SUCCESS)
            return status;                
    }

    if (PWR_GetFlagStatus(PWR_FLAG_PVDOF) != RESET)
    {
        lowVoltBatteryDetected = TRUE;
        status = updateBatteryVoltage(BATTERY_VOLT_LOW);
        if (status != SUCCESS)
            return status;                        
    }
    else
    {
        lowVoltBatteryDetected = FALSE;
        status = updateBatteryVoltage(BATTERY_VOLT_NORMAL);      
        if (status != SUCCESS)
            return status;                        
    }
    status = i2cWriteBuffer(empId, empIdAddress, 0x02, (uint8_t)EEPROM_USER_ADDRESS);
    if (status != SUCCESS)
        return status;
    
    status = i2cWriteBuffer(&tagVersion, tagVersionAddress, 0x01, (uint8_t)EEPROM_USER_ADDRESS);    
    return status;
}

ErrorStatus setRuntime(bool bootup)
{
    ErrorStatus status = SUCCESS;
    uint8_t prepData = 0xff;
    uint8_t bKey = 0xff;
    uint16_t runtimeStartAddress = 0x0000;
    uint16_t runtimeBootupAddress = 0x0014;
    uint16_t sealAddress = 0x07f8;    
    uint8_t seal[8] = {0x69, 0x6d, 0x6f, 0x77, 0x61, 0x72, 0x65, 0x69};      
    
    if (bootup == TRUE)
    {
        status = i2cReadBuffer(&bKey, runtimeBootupAddress, 1, (uint8_t)EEPROM_USER_ADDRESS);
    }
    
    if (status == SUCCESS)
    {
        if (bKey != BLOCKER_KEY)
        {
            for (int i = 0; i < 24; i++)
            {        
                status = i2cWriteBuffer(&prepData, runtimeStartAddress++, 1, (uint8_t)EEPROM_USER_ADDRESS);       
                if (status != SUCCESS)
                    break;
            }

            if (status == SUCCESS)
            {
                bKey = BLOCKER_KEY;
                status = i2cWriteBuffer(&bKey, runtimeBootupAddress, 1, (uint8_t)EEPROM_USER_ADDRESS);      
            }
        }
    }
    
    if (status == SUCCESS)
    {
        for (int i = 0; i < 8; i++)
        {
            status = i2cWriteBuffer(&seal[i], sealAddress++, 0x01, (uint8_t)EEPROM_USER_ADDRESS);
            if (status != SUCCESS)
                break;            
        }
    }
    return status;  
}

ErrorStatus setHarvestUserConfiguration()
{
    return(disableEHMode(0xff));
}

ErrorStatus disableEHMode(uint8_t configByte)
{
    ErrorStatus status = SUCCESS;
    uint16_t configByteAddress = 0x0910;
    uint16_t controlRegisterAddress = 0x0920;
    uint8_t controlRegister = 0x00;
    status = i2cWriteBuffer(&controlRegister, controlRegisterAddress, 0x01, (uint8_t)EEPROM_SYSTEM_ADDRESS);
    if (status != SUCCESS)
        return status;
    return(i2cWriteBuffer(&configByte, configByteAddress, 0x01, (uint8_t)EEPROM_SYSTEM_ADDRESS));  
}

void i2cPowerOn()
{
    GPIO_SetBits(GPIOB, GPIO_Pin_6);  
    delay((uint16_t)0xffff);
}

void i2cPowerOff()
{
    delay((uint16_t)0xffff);
    GPIO_ResetBits(GPIOB, GPIO_Pin_6);  
}

uint16_t getInt16(uint8_t *pBytes)
{
    uint16_t value = 0;
    value = *pBytes;
    value |= *(pBytes + 1) << 8;
    return value;
}

void getArrayFromInt16(uint16_t val, uint8_t *pBytes)
{
    pBytes[1] = (uint8_t)(val >> 8);
    pBytes[0] = (uint8_t)val;    
}

void delay(__IO uint16_t counter)
{
    while (counter != 0)
    {
        counter--;
    }
}

void initializeTagPins()
{
    clockGpioConfig();
    disableInterrupts();
    i2cPowerOn();
}

void initializeTagEE()
{
    if (initBlockerEE(TRUE) != SUCCESS)
        halt();
    
    // turn off test LED...    
    GPIO_ResetBits(GPIOB, GPIO_Pin_4);  
    enableInterrupts();
}        

void clockGpioConfig()
{
    CLK_DeInit();
    CLK_SYSCLKSourceConfig(CLK_SYSCLKSource_HSI);
    
#ifdef I2C_FAST_MODE
    CLK_SYSCLKDivConfig(CLK_SYSCLKDiv_1);
#else
    CLK_SYSCLKDivConfig(CLK_SYSCLKDiv_2);
#endif
    CLK_SYSCLKSourceSwitchCmd(ENABLE);
           
    while (CLK_GetSYSCLKSource() != CLK_SYSCLKSource_HSI);
    CLK_PeripheralClockConfig(CLK_Peripheral_I2C1, ENABLE);   
   
    //---Power, Test    
    GPIO_Init(GPIOB, GPIO_Pin_6, GPIO_Mode_Out_PP_Low_Fast);
    GPIO_Init(GPIOB, GPIO_Pin_4, GPIO_Mode_Out_PP_Low_Slow);
    GPIO_SetBits(GPIOB, GPIO_Pin_4);
    //--- Tamper    
    GPIO_Init(GPIOB, GPIO_Pin_3, GPIO_Mode_In_PU_IT);
    EXTI_SetPinSensitivity(EXTI_Pin_3, EXTI_Trigger_Rising_Falling);
    //--- RF Command    
    GPIO_Init(GPIOD, GPIO_Pin_0, GPIO_Mode_In_FL_IT);
    EXTI_SetPortSensitivity(EXTI_Port_D, EXTI_Trigger_Rising);
    EXTI_SelectPort(EXTI_Port_D);
    EXTI_SetHalfPortSelection(EXTI_HalfPort_D_LSB, ENABLE);
    //--- Battery Low Voltage Detection
    PWR_PVDCmd(ENABLE);
    PWR_PVDITConfig(ENABLE);
    //--- Tamper Bounce Handler Time base configuration (HSE/HSI)
    CLK_PeripheralClockConfig(CLK_Peripheral_TIM4, ENABLE);    
    TIM4_TimeBaseInit(TIM4_Prescaler_32768, 244);     // 500 ms timebase    
}

ErrorStatus tamperSwitchTest()
{
    testTamperSwitch = TRUE;
    return SUCCESS;
}

void tagDiagnostics()
{
    // memory test...
    if (i2cTest() != SUCCESS)
        halt();
    
    // RTC test...
    if (rtcTest() != SUCCESS)
        halt();
}

ErrorStatus i2cTest()
{
    ErrorStatus status = SUCCESS;
    uint8_t testWriteByte = 0xaa;
    uint8_t testReadByte = 0x00;
       
    for (int i = 0; i < 3; i++)
    {
        status = i2cWriteBuffer(&testWriteByte, reservedTestAddress, 0x01, (uint8_t)EEPROM_USER_ADDRESS);
        if (status != SUCCESS)
            break;
        
        status = i2cReadBuffer(&testReadByte, reservedTestAddress, 0x01, (uint8_t)EEPROM_USER_ADDRESS);
        if (status != SUCCESS)
            break;
    
        if (testWriteByte != testReadByte)
        {
            status = ERROR;
            break;
        }
        
        i++;
        testWriteByte = ~testWriteByte;
        reservedTestAddress++;
    }

    if (status == SUCCESS)
    {
        uint8_t uid[8] = {0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00};
        uint16_t uidAddress = (uint16_t)2324;
        status = i2cReadBuffer(uid, uidAddress, 0x08, (uint8_t)EEPROM_SYSTEM_ADDRESS);
        if (status == SUCCESS)
        {
            if ((uid[6] == 0x02) && (uid[7] == 0xe0))
            {
                status = SUCCESS;               
            }
        }
    }
    return status;
}

ErrorStatus rtcTest()
{
    ErrorStatus status = SUCCESS;
    uint8_t tSeconds = 0;
    uint16_t rtcTestTimeout = 500;
    
    RTC_InitTypeDef init;
    RTC_TimeTypeDef time;
    RTC_DateTypeDef date;

    //--------------------------------------------------------------------------    
    startTimers();
    //--------------------------------------------------------------------------        

    init.RTC_HourFormat = RTC_HourFormat_24;
    init.RTC_AsynchPrediv = 0x7c;
    init.RTC_SynchPrediv = 0x7cf;
    RTC_Init(&init);
	
    RTC_DateStructInit(&date);
    date.RTC_WeekDay = ((uint8_t)0x05);
    date.RTC_Month = ((uint8_t)0x06);
    date.RTC_Date = 24;
    date.RTC_Year = 11;
    RTC_SetDate(RTC_Format_BIN, &date);

    RTC_TimeStructInit(&time);
    time.RTC_Hours   = 0x01;
    time.RTC_Minutes = 0x00;
    time.RTC_Seconds = 0x00;
    RTC_SetTime(RTC_Format_BIN, &time);
    
    while(tSeconds < 3)
    {
        delay(0xffff);
        while (RTC_WaitForSynchro() != SUCCESS);
        //.........RTC_GetTime(RTC_Format_BIN, &time);
        //-----------------------------
        status = getTimeTest(&time);
        if (status != SUCCESS)
            break;
        //-----------------------------
        tSeconds = (uint8_t)time.RTC_Seconds;
        
        rtcTestTimeout--;
        if (rtcTestTimeout < 0)
        {
            status = ERROR;
            break;
        }
    }
    
    //--------------------------------------------------------------------------    
    stopTimers();
    //--------------------------------------------------------------------------        
      
    if (status == SUCCESS)
    {
        delay(0xffff);
        while (RTC_WaitForSynchro() != SUCCESS);
        RTC_GetTime(RTC_Format_BIN, &time);
        tSeconds = (uint8_t)time.RTC_Seconds;
       
        for (int i = 0; i < tSeconds / 2; i++)
            delay(0xffff);       
        while (RTC_WaitForSynchro() != SUCCESS);
        RTC_GetTime(RTC_Format_BIN, &time);
        status = ERROR;
        if ((uint8_t)time.RTC_Seconds == tSeconds)
        {
            status = SUCCESS;
        }          
    }
    return status;
}

void reportTamper()
{
    uint8_t empId[2] = {0x00, 0x00}; 

    updateInterruptStatus(IS_TAMPER_BAND_OPENED, TRUE);
    readRTC();
    updateCommandHistory(TAMPER_INTERRUPT);
    readEmployeeEE(empId);
    updateEmployeeHistory(empId);                    
    setNextPacketHistory();
    stopTimers();
}

void t4Enable()
{
    timerTick = 0;
    TIM4_ITConfig(TIM4_IT_Update, ENABLE);
    TIM4_Cmd(ENABLE);    
}

void t4Disable()
{
    TIM4_Cmd(DISABLE);
    TIM4_ITConfig(TIM4_IT_Update, DISABLE);            
}
/*************************** END OF FILE **************************************/
