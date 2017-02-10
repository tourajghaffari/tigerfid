/*******************************************************************************
* @file     blocker_services.c
* @author   imoware
* @version  V3.4.0
* @date     04/13/12
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
*******************************************************************************/
#include "blocker_services.h"
#include "clock.h"
#include "i2c_ee.h"
#include "band.h"
#include "battery.h"
#include "led.h"
#include "rf.h"
#include "platform.h"
#include <stdio.h>

const uint16_t commandAddress 	        = 0x0000;
const uint16_t commandStatusAddress 	= 0x0001;
const uint16_t tagVersionAddress 	    = 0x0013;
const uint16_t interruptStatusAddress	= 0x0016;
const uint16_t batteryVoltageAddress 	= 0x0017;
const uint16_t histPacketAddress	    = 0x0004;
const uint16_t histPacketNumberAddress  = 0x0006;
const uint16_t empIdAddress 	  	    = 0x0010;
const uint16_t reservedTestAddress      = 0x0015;

uint8_t histPacket[2] = {0x00, 0x00};
uint8_t histPacketNumber[2] = {0x00, 0x00};

volatile bool rtcConfigured = FALSE;
volatile bool historyRead = FALSE;
volatile bool testTamperSwitch = FALSE;
volatile bool lowVoltBatteryDetected = FALSE;
volatile uint16_t timerTick = 0;

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

    if (Band_GetState() == OPEN)
    {
        bandOpen = Band_IsOpen();
        if (bandOpen == TRUE)
        {
            i2cPowerOn();
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
        rtcConfigured = FALSE;
        i2cPowerOff();
    }
}

void batteryVoltageIRQHandler()
{
    if (Battery_HasLowLevel() == TRUE)
    {
        lowVoltBatteryDetected = TRUE;
        updateBatteryVoltage(BATTERY_VOLT_LOW);
        if (rtcConfigured == TRUE)
        {
            readRTC();
            updateCommandHistory(LOW_BATTERY_INTERRUPT);
            setNextPacketHistory();
        }
    }
    else
    {
        lowVoltBatteryDetected = FALSE;
        updateBatteryVoltage(BATTERY_VOLT_NORMAL);
    }
}

void rfCommandIRQHandler()
{
    ErrorCode status;
    uint8_t commandStatus = COMMAND_STATUS_UNKNOWN;

    status = readCommandStatus(&commandStatus);
    if (status != ERROR_NONE)
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
        if (status != ERROR_NONE)
            return;

        if (Band_GetState() == OPEN)
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

ErrorCode commandHandler()
{
    ErrorCode status;
    uint8_t command = NOOP;
    uint8_t empId[2] = {0x00, 0x00};
    bool updateHistory = TRUE;

    status = readCommand(&command);
    if (status != ERROR_NONE)
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

    if ((command != FACTORY_DATA_RESET) && (command != TAMPER_SWITCH_TEST))
    {
        updateCommandHistory(command);
        readEmployeeEE(empId);
        updateEmployeeHistory(empId);
    }

    if ((command == FACTORY_DATA_RESET) || (command == TAMPER_SWITCH_TEST))
        updateHistory = FALSE;

    if (command == STOP_TIMERS)
        rtcConfigured = TRUE;

    if (status == ERROR_NONE)
    {
        setCommandStatus(COMMAND_STATUS_COMPLETE, updateHistory);
    }

    if (Band_GetState() == OPEN)
    {
        updateInterruptStatus(IS_TAMPER_BAND_OPENED, updateHistory);
    }
    else
    {
        updateInterruptStatus(IS_TAMPER_BAND_CLOSED, updateHistory);
    }

    if ((command != FACTORY_DATA_RESET) && (command != TAMPER_SWITCH_TEST))
    {
        setNextPacketHistory();
    }

    if (command == STOP_TIMERS)
        rtcConfigured = FALSE;

    return status;
}

ErrorCode commandReadRTC()
{
    ErrorCode status;

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

ErrorCode commandStartTimers()
{
    ErrorCode status;
    uint8_t interruptStatus = IS_TAMPER_BAND_CLOSED;
    rtcConfigured = FALSE;
    testTamperSwitch = FALSE;

    status = readInterruptStatus(&interruptStatus);
    if (((interruptStatus & IS_TAMPER_BAND_OPENED) == IS_TAMPER_BAND_OPENED) && (status == ERROR_NONE))
    {
        status = ERROR_BAND_OPEN_MUST_CLOSE;
        setCommandStatus(ERROR_BAND_OPEN_MUST_CLOSE, TRUE);
    }
    else
    {
        if ((interruptStatus & IS_TAMPER_BAND_CLOSED) == IS_TAMPER_BAND_CLOSED)
        {
            status = configureRTC();
            if (status == ERROR_NONE)
            {
                status = readRTC();
                if (status == ERROR_NONE)
                {
                    status = setHarvestUserConfiguration();
                    if (status == ERROR_NONE)
                    {
                        rtcConfigured = TRUE;
                    }
                }
            }
        }
    }

    if (status != ERROR_NONE)
    {
        setCommandStatus(status, TRUE);
    }
    return status;
}

ErrorCode commandStopTimers()
{
    ErrorCode status;

    if (rtcConfigured == TRUE)
    {
        status = readRTC();
        if (status == ERROR_NONE)
        {
            stopTimers();
            rtcConfigured = FALSE;
        }
    }
    else
    {
        status = ERROR_RTC_CONFIG_REQUIRED;
        setCommandStatus(status, TRUE);
    }
    return status;
}

ErrorCode commandReadHistory()
{
    ErrorCode status = readRTC();
    if (status == ERROR_NONE)
    {
        historyRead = TRUE;
    }
    return status;
}

ErrorCode commandFlushHistory()
{
    ErrorCode status = readRTC();
    if (status != ERROR_NONE)
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
        if (status == ERROR_NONE)
            historyRead = FALSE;
    }
    return status;
}

ErrorCode commandReadBatteryVoltage()
{
    ErrorCode status = readRTC();
    if (status != ERROR_NONE)
        return status;

    if (lowVoltBatteryDetected == TRUE)
        status = updateBatteryVoltage(BATTERY_VOLT_LOW);
    else
        status = updateBatteryVoltage(BATTERY_VOLT_NORMAL);

    return status;
}

ErrorCode commandFactoryDataReset()
{
    ErrorCode status = initBlockerEE(FALSE);
    if (status != ERROR_NONE)
    {
        setCommandStatus(status, FALSE);				
    }
    return status;
 }

ErrorCode readCommand(uint8_t *pCommand)
{
    ErrorCode status = i2cReadBuffer(pCommand, commandAddress, 1, EEPROM_USER_ADDRESS);
    if (status != ERROR_NONE)
    {
        setCommandStatus(status, TRUE);
    }
    return status;
}

ErrorCode readCommandStatus(uint8_t *pCommandStatus)
{
    return(i2cReadBuffer(pCommandStatus, commandStatusAddress, 1, EEPROM_USER_ADDRESS));
}

ErrorCode readInterruptStatus(uint8_t *pInterruptStatus)
{
    return(i2cReadBuffer(pInterruptStatus, interruptStatusAddress, 1, EEPROM_USER_ADDRESS));
}

ErrorCode flushHistory()
{
      ErrorCode status = ERROR_NONE;
      uint8_t flushData = 0xff;
      uint16_t historySpaceAddress = EE_HISTORY_START_ADDRESS;
      uint16_t historySpace        = EE_BLOCKER_SIZE - historySpaceAddress;

      DisableInterrupts();
      while (historySpace > 0)
      {
          status = i2cWriteBuffer(&flushData, historySpaceAddress, 1, EEPROM_USER_ADDRESS);
          if (status != ERROR_NONE)
          {
              break;
          }
          historySpace--;
          historySpaceAddress++;
      }
      if (status != ERROR_NONE)
      {
          setCommandStatus(status, TRUE);
          EnableInterrupts();
          return status;
      }

      histPacket[0] = EE_HISTORY_START_ADDRESS;
      histPacket[1] = 0;

      histPacketNumber[0] = 0x00;
      histPacketNumber[1] = 0x00;

      status = i2cWriteBuffer(histPacket, histPacketAddress, 2, EEPROM_USER_ADDRESS);
      if (status == ERROR_NONE)
      {
          status = i2cWriteBuffer(histPacketNumber, histPacketNumberAddress, 2, EEPROM_USER_ADDRESS);
      }
      EnableInterrupts();
      return status;
}

ErrorCode setNextPacketHistory()
{
    ErrorCode status;
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

    status = i2cWriteBuffer(histPacket, histPacketAddress, 2, EEPROM_USER_ADDRESS);
    if (status == ERROR_NONE)
    {
        status = i2cWriteBuffer(histPacketNumber, histPacketNumberAddress, 2, EEPROM_USER_ADDRESS);
    }
    return status;
}

ErrorCode updateInterruptStatus(uint8_t interruptStatus, bool updateHistory)
{
    ErrorCode status;
    status = i2cWriteBuffer(&interruptStatus, interruptStatusAddress, 1, EEPROM_USER_ADDRESS);
    if (status == ERROR_NONE)
    {
        if ((updateHistory == TRUE) && (rtcConfigured == TRUE))
        {
            status  = updateInterruptStatusHistory(interruptStatus);
        }
    }
    return status;
}

ErrorCode updateInterruptStatusHistory(uint8_t interruptStatus)
{
    uint16_t interruptStatusHistoryAddress = getInt16(histPacket) + 10;
    return(i2cWriteBuffer(&interruptStatus, interruptStatusHistoryAddress, 1, EEPROM_USER_ADDRESS));
}

ErrorCode updateBatteryVoltage(uint8_t batteryVoltage)
{
    return(i2cWriteBuffer(&batteryVoltage, batteryVoltageAddress, 1, EEPROM_USER_ADDRESS));
}

ErrorCode setCommandStatus(ErrorCode commandStatus, bool updateHistory)
{
    ErrorCode status;
    uint8_t cmd_status = (uint8_t)commandStatus;
    status = i2cWriteBuffer(&cmd_status, commandStatusAddress, 1, EEPROM_USER_ADDRESS);
    if (status != ERROR_NONE)
    {
        setCommandStatus(status, TRUE);
        return status;
    }

    if (status == ERROR_NONE)
    {
        if ((updateHistory == TRUE) && (rtcConfigured == TRUE))
        {
            status = updateCommandStatusHistory(commandStatus);
        }
    }
    return status;
}

ErrorCode updateCommandStatusHistory(uint8_t status)
{
    uint16_t commandStatusHistoryAddress = getInt16(histPacket) + 11;
    return(i2cWriteBuffer(&status, commandStatusHistoryAddress, 1, EEPROM_USER_ADDRESS));
}

ErrorCode updateCommandHistory(uint8_t command)
{
    uint16_t commandHistory = getInt16(histPacket) + 7;
    return(i2cWriteBuffer(&command, commandHistory, 1, EEPROM_USER_ADDRESS));
}

void readEmployeeEE(uint8_t *empId)
{
    ErrorCode status;
    status = i2cReadBuffer(empId, empIdAddress, 2, EEPROM_USER_ADDRESS);
    if (status != ERROR_NONE)
        setCommandStatus(status, TRUE);
}

void updateEmployeeHistory(uint8_t *empId)
{
    ErrorCode status;
    uint16_t empIdHistoryAddress = getInt16(histPacket) + 8;
    status = i2cWriteBuffer(empId, empIdHistoryAddress, 2, EEPROM_USER_ADDRESS);
    if (status != ERROR_NONE)
        setCommandStatus(status, TRUE);
}

ErrorCode initBlockerEE(bool bootup)
{
    ErrorCode status;
    uint8_t commandStatus = COMMAND_STATUS_UNKNOWN;
    uint8_t empId[2] = {0x00, 0x00};
    uint8_t command = NOOP;
    uint8_t tagVersion = TAG_VERSION;

    status = setRuntime(bootup);
    if (status != ERROR_NONE)
        return status;

    status = setHarvestUserConfiguration();
    if (status != ERROR_NONE)
        return status;
                 		
    status = i2cReadBuffer(histPacket, histPacketAddress, 2, EEPROM_USER_ADDRESS);
    if (status != ERROR_NONE)
        return status;

    if ((histPacket[0] == 0xff) && (histPacket[1] == 0xff))
    {
        histPacket[0] = EE_HISTORY_START_ADDRESS;
        histPacket[1] = 0;
        status = i2cWriteBuffer(histPacket, histPacketAddress, 2, EEPROM_USER_ADDRESS);
        if (status != ERROR_NONE)
            return status;
    }

    status = i2cReadBuffer(histPacketNumber, histPacketNumberAddress, 2, EEPROM_USER_ADDRESS);
    if (status != ERROR_NONE)
        return status;

    if ((histPacketNumber[0] == 0xff) && (histPacketNumber[1] == 0xff))
    {
        histPacketNumber[0] = 0x00;
        histPacketNumber[1] = 0x00;
        status = i2cWriteBuffer(histPacketNumber, histPacketNumberAddress, 2, EEPROM_USER_ADDRESS);
        if (status != ERROR_NONE)
            return status;
    }

    status = i2cWriteBuffer(&command, commandAddress, 1, EEPROM_USER_ADDRESS);
    if (status != ERROR_NONE)
        return status;
    status = i2cWriteBuffer(&commandStatus, commandStatusAddress, 1, EEPROM_USER_ADDRESS);
    if (status != ERROR_NONE)
        return status;

    if (Band_GetState() == OPEN)
    {
        status = updateInterruptStatus(IS_TAMPER_BAND_OPENED, FALSE);
        if (status != ERROR_NONE)
            return status;
    }
    else
    {
        status = updateInterruptStatus(IS_TAMPER_BAND_CLOSED, FALSE);
        if (status != ERROR_NONE)
            return status;
    }

    if (Battery_HasLowLevel() == TRUE)
    {
        lowVoltBatteryDetected = TRUE;
        status = updateBatteryVoltage(BATTERY_VOLT_LOW);
        if (status != ERROR_NONE)
            return status;
    }
    else
    {
        lowVoltBatteryDetected = FALSE;
        status = updateBatteryVoltage(BATTERY_VOLT_NORMAL);
        if (status != ERROR_NONE)
            return status;
    }
    status = i2cWriteBuffer(empId, empIdAddress, 2, EEPROM_USER_ADDRESS);
    if (status != ERROR_NONE)
        return status;

    status = i2cWriteBuffer(&tagVersion, tagVersionAddress, 1, EEPROM_USER_ADDRESS);
    return status;
}

ErrorCode setRuntime(bool bootup)
{
    ErrorCode status = ERROR_NONE;
    uint8_t prepData = 0xff;
    uint8_t bKey = 0xff;
    const uint16_t runtimeStartAddress  = 0x0000;
    const uint16_t runtimeBootupAddress = 0x0014;
    const uint16_t sealAddress = 0x07F8;
    const uint8_t seal[8] = "Acti-GTP";     //TGTG

    if (bootup == TRUE)
    {
        status = i2cReadBuffer(&bKey, runtimeBootupAddress, 1, EEPROM_USER_ADDRESS);
    }

    if (status == ERROR_NONE)
    {
        if (bKey != BLOCKER_KEY)
        {
            for (int i = 0; i < 24; i++)
            {
                status = i2cWriteBuffer(&prepData, runtimeStartAddress + i, 1, EEPROM_USER_ADDRESS);
                if (status != ERROR_NONE)
                    break;
            }

            if (status == ERROR_NONE)
            {
                bKey = BLOCKER_KEY;
                status = i2cWriteBuffer(&bKey, runtimeBootupAddress, 1, EEPROM_USER_ADDRESS);
            }
        }
    }

    if (status == ERROR_NONE)
    {
        status = i2cWriteBuffer(&seal[0], sealAddress, 4, EEPROM_USER_ADDRESS);
        if (status == ERROR_NONE) {
            status = i2cWriteBuffer(&seal[4], sealAddress + 4, 4, EEPROM_USER_ADDRESS);
        }
    }
    return status;
}

ErrorCode setHarvestUserConfiguration()
{
    return(disableEHMode(0xff));
}

ErrorCode disableEHMode(uint8_t configByte)
{
    ErrorCode status;
    const uint16_t configByteAddress      = 0x0910;
    const uint16_t controlRegisterAddress = 0x0920;
    uint8_t controlRegister = 0x00;


    status = i2cWriteBuffer(&controlRegister, controlRegisterAddress, 1, EEPROM_SYSTEM_ADDRESS);
    if (status != ERROR_NONE)
        return status;

    return(i2cWriteBuffer(&configByte, configByteAddress, 1, EEPROM_SYSTEM_ADDRESS));
}

uint16_t getInt16(uint8_t *pBytes)
{
    uint16_t value = 0;
    value  =  *pBytes;
    value |= *(pBytes + 1) << 8;
    return value;
}

void getArrayFromInt16(uint16_t val, uint8_t *pBytes)
{
    pBytes[1] = (uint8_t)(val >> 8);
    pBytes[0] = (uint8_t) val;
}

void delay(uint16_t counter)
{
    volatile uint16_t  dly = counter;

    while (dly != 0)
    {
        dly--;
    }
}

void initializeTagPins()
{
    clockGpioConfig();
    DisableInterrupts();
    i2cPowerOn();
}

void initializeTagEE()
{
    if (initBlockerEE(TRUE) != ERROR_NONE) {
        Halt();
    }

    // turn off test LED...
    LED_Off();
    i2cPowerOff();
    EnableInterrupts();
}

void clockGpioConfig()
{
    Clock_Init();

    //--- I2C
    i2cInit();

    //--- Power, Test
    LED_Init();

    //--- Tamper
    Band_Init();

    //--- RF Command
    RF_Init();

    //--- Battery Low Voltage Detection
    Battery_Init();
}

ErrorCode tamperSwitchTest()
{
    testTamperSwitch = TRUE;
    return (ERROR_NONE);
}

void tagDiagnostics()
{
    // memory test...
    if (i2cTest() != ERROR_NONE) {
        Halt();
    }

    // RTC test...
    if (rtcTest() != ERROR_NONE) {
        Halt();
    }
}

ErrorCode i2cTest()
{
    ErrorCode status;
    uint8_t testWriteByte = 0xAA;
    uint8_t testReadByte  = 0x00;

    for (int i = 0; i < 3; i++)
    {
        status = i2cWriteBuffer(&testWriteByte, reservedTestAddress + i, 1, EEPROM_USER_ADDRESS);
        if (status != ERROR_NONE)
            break;

        status = i2cReadBuffer(&testReadByte, reservedTestAddress + i, 1, EEPROM_USER_ADDRESS);
        if (status != ERROR_NONE)
            break;

        if (testWriteByte != testReadByte)
        {
            status = ERROR_UNKNOWN;
            break;
        }

        testWriteByte = ~(int)testWriteByte;
    }

    if (status == ERROR_NONE)
    {
        uint8_t uid[8] = {0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00};
        uint16_t uidAddress = (uint16_t)0x914;
        status = i2cReadBuffer(uid, uidAddress, 8, EEPROM_SYSTEM_ADDRESS);
        if (status == ERROR_NONE)
        {
            if ((uid[6] != 0x02) ||
                (uid[7] != 0xE0)) {
                 status = ERROR_NONE;
            }
        }
    }

    return status;
}

ErrorCode  readDateEE (Date  *date)
{
    ErrorCode status;
    uint8_t rtcDate[4] = { 0, 0, 0, 0 };

    status = i2cReadBuffer(&rtcDate[0], rtcDateAddress, 4, EEPROM_USER_ADDRESS);
    if (status == ERROR_NONE)
    {
        date->WeekDay = (WeekDay)rtcDate[3];
        date->Month   = (Month  )rtcDate[2];
        date->Day     =          rtcDate[1];
        date->Year    =          rtcDate[0];
    }
    return status;
}

ErrorCode  readTimeEE (Time  *time)
{
    ErrorCode status;
    uint8_t rtcTime[3] = { 0, 0, 0 };

    status = i2cReadBuffer(&rtcTime[0], rtcTimeAddress, 3, EEPROM_USER_ADDRESS);
    if (status == ERROR_NONE)
    {
        time->Hours   = rtcTime[2];
        time->Minutes = rtcTime[1];
        time->Seconds = rtcTime[0];
    }
    return status;
}

/*************************** END OF FILE **************************************/
