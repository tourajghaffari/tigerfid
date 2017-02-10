/*******************************************************************************
* @file     blocker_services.h
* @author   imoware
* @version  v2.6.0
* @date     09/08/11
*******************************************************************************/ 
#ifndef __blocker_services_H
#define __blocker_services_H
#include "stm8l15x.h"
#include "stm8l15x_tim2.h"
#include "stm8l15x_beep.h"


//------------------------------------------------------------------------------
// Error Codes
//------------------------------------------------------------------------------
#define ERROR_FLUSH_FIRST_READ_HISTORY 	(uint8_t)-1
#define ERROR_BAND_OPEN_MUST_CLOSE	(uint8_t)-2
#define ERROR_RTC_CONFIG_REQUIRED	(uint8_t)-3
#define ERROR_INVALID_COMMAND           (uint8_t)-4
#define ERROR_INVALID_COMMAND_STATUS    (uint8_t)-5
#define ERROR_I2C_BUS_BUSY		                  (uint8_t)-6
#define ERROR_I2C_EVENT_MASTER_MODE_SELECT                (uint8_t)-7
#define ERROR_I2C_EVENT_MASTER_TRANSMITTER_MODE_SELECTED  (uint8_t)-8
#define ERROR_I2C_EVENT_MASTER_BYTE_TRANSMITTING	  (uint8_t)-9
#define ERROR_I2C_EVENT_MASTER_BYTE_TRANSMITTED	          (uint8_t)-10
#define ERROR_I2C_EVENT_MASTER_RECEIVER_MODE_SELECTED     (uint8_t)-11


//------------------------------------------------------------------------------
// RF Commands...
//------------------------------------------------------------------------------
#define NOOP			0x00	// NOP -- do nothing
#define READ_RTC   		0x01
#define START_TIMERS 		0x02
#define STOP_TIMERS 		0x03
#define READ_HISTORY		0x04
#define FLUSH_HISTORY		0x05
#define READ_BATTERY_VOLTAGE	0x06
#define TAMPER_SWITCH_TEST	0x07
#define FACTORY_DATA_RESET	0x08
//---- background stuff...
#define TAMPER_INTERRUPT	0xaa
#define LOW_BATTERY_INTERRUPT	0xbb

// Command Status...
#define	COMMAND_STATUS_COMPLETE	0X00	// ERROR CODE otherwise...
#define COMMAND_STATUS_BUSY	0X01
#define COMMAND_STATUS_BUSY_ACK	0x02
#define COMMAND_STATUS_RECEIVED	0x03
#define COMMAND_STATUS_RUNNING	0x04
#define COMMAND_STATUS_UNKNOWN	0xff

#define BATTERY_VOLT_NORMAL	0X00
#define BATTERY_VOLT_LOW	0X01
																						// 7  6  5  4  3  2  1  0
// Interrupt status...
#define IS_TAMPER_BAND_CLOSED 	    0x00
#define IS_TAMPER_BAND_OPENED	    0x02

#define IS_NO_INTERRUPTS_RUNNING    0x00
#define IS_RF_RUNNING	            0x10
#define IS_COMMAND_RUNNING	    0x20
#define IS_TAMPER_SWITCH_FALTERED   0x40
#define IS_LOW_BATTERY_RUNNING      0x80

#define EE_BLOCKER_SIZE 0x0800
#define EE_HISTORY_START_ADDRESS 0x0018
#define BLOCKER_KEY (uint8_t)37

#define LSI_PERIOD_NUMBERS 10

void tamperIRQHandler(void);
void batteryVoltageIRQHandler(void);
void rfCommandIRQHandler(void);

ErrorStatus commandHandler(void);
ErrorStatus commandReadRTC(void);
ErrorStatus commandStartTimers(void);
ErrorStatus commandStopTimers(void);
ErrorStatus commandReadHistory(void);
ErrorStatus commandFlushHistory(void);
ErrorStatus commandReadBatteryVoltage(void);
ErrorStatus commandFactoryDataReset(void);

ErrorStatus configureRTC(void);
ErrorStatus readRTC(void);
void startTimers(void);
void stopTimers(void);
ErrorStatus flushHistory(void);
ErrorStatus updateCommand(uint8_t command);

ErrorStatus setCommandStatus(uint8_t commandStatus, bool updateHistory);
ErrorStatus setNextPacketHistory(void);
ErrorStatus updateCommandStatusHistory(uint8_t status);
ErrorStatus updateCommandHistory(uint8_t command);
ErrorStatus updateInterruptStatus(uint8_t interruptStatus, bool updateHistory);
ErrorStatus updateInterruptStatusHistory(uint8_t interruptStatus);
ErrorStatus updateBatteryVoltage(uint8_t batteryVoltage);

ErrorStatus readCommand(uint8_t *pCommand);
ErrorStatus readDateEE(RTC_DateTypeDef *date);
ErrorStatus readTimeEE(RTC_TimeTypeDef *date);
void readEmployeeEE(uint8_t *empId);
void updateEmployeeHistory(uint8_t *empId);
uint8_t readCommandStatus(uint8_t *pCommandStatus);
uint8_t readInterruptStatus(uint8_t *pInterruptStatus);
uint16_t getInt16(uint8_t *pBytes);
void getArrayFromInt16(uint16_t val, uint8_t *pBytes);
ErrorStatus initBlockerEE(bool bootup);
ErrorStatus setRuntime(bool bootup);
ErrorStatus setHarvestUserConfiguration(void);
ErrorStatus disableEHMode(uint8_t configByte);
void delay(uint16_t nCount);
void i2cPowerOff(void);
void i2cPowerOn(void);
void clockGpioConfig(void);

ErrorStatus tamperSwitchTest(void);
ErrorStatus i2cTest(void);
ErrorStatus rtcTest(void);
ErrorStatus getTimeTest(RTC_TimeTypeDef *pTime);

void timer2IRQHandler(void);
uint32_t calibrateLSI(void);
#endif
/*************************** END OF FILE **************************************/