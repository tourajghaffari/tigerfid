/*******************************************************************************
* @file     blocker_services.h
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

#ifndef __blocker_services_H
#define __blocker_services_H


#include "types.h"
#include "error_code.h"
#include "rtc.h"


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
#define TAMPER_INTERRUPT	0xAA
#define LOW_BATTERY_INTERRUPT	0xBB

#define BATTERY_VOLT_NORMAL	0x00
#define BATTERY_VOLT_LOW	0x01
																					// 7  6  5  4  3  2  1  0
// Interrupt status...
#define IS_TAMPER_BAND_CLOSED 	    0x00
#define IS_TAMPER_BAND_OPENED	    0x02

#define IS_NO_INTERRUPTS_RUNNING    0x00
#define IS_RF_RUNNING	            0x10
#define IS_COMMAND_RUNNING	    0x20
#define IS_TAMPER_SWITCH_FALTERED   0x40
#define IS_LOW_BATTERY_RUNNING      0x80

#define TAG_VERSION 0x36                //TGTG
#define EE_BLOCKER_SIZE 0x0800
#define EE_HISTORY_START_ADDRESS 0x0018
#define EE_LAST_PACKET 0x07EC
#define BLOCKER_KEY (uint8_t)37

void tamperIRQHandler(void);
void batteryVoltageIRQHandler(void);
void rfCommandIRQHandler(void);

ErrorCode commandHandler(void);
ErrorCode commandReadRTC(void);
ErrorCode commandStartTimers(void);
ErrorCode commandStopTimers(void);
ErrorCode commandReadHistory(void);
ErrorCode commandFlushHistory(void);
ErrorCode commandReadBatteryVoltage(void);
ErrorCode commandFactoryDataReset(void);

ErrorCode configureRTC(void);
ErrorCode readRTC(void);
void startTimers(void);
void stopTimers(void);
ErrorCode flushHistory(void);
ErrorCode updateCommand(uint8_t command);

ErrorCode setCommandStatus(ErrorCode commandStatus, bool updateHistory);
ErrorCode setNextPacketHistory(void);
ErrorCode updateCommandStatusHistory(uint8_t status);
ErrorCode updateCommandHistory(uint8_t command);
ErrorCode updateInterruptStatus(uint8_t interruptStatus, bool updateHistory);
ErrorCode updateInterruptStatusHistory(uint8_t interruptStatus);
ErrorCode updateBatteryVoltage(uint8_t batteryVoltage);

ErrorCode readCommand(uint8_t *pCommand);
void readEmployeeEE(uint8_t *empId);
void updateEmployeeHistory(uint8_t *empId);
ErrorCode readCommandStatus(uint8_t *pCommandStatus);
ErrorCode readInterruptStatus(uint8_t *pInterruptStatus);
uint16_t getInt16(uint8_t *pBytes);
void getArrayFromInt16(uint16_t val, uint8_t *pBytes);
ErrorCode initBlockerEE(bool bootup);
ErrorCode setRuntime(bool bootup);
ErrorCode setHarvestUserConfiguration(void);
ErrorCode disableEHMode(uint8_t configByte);
void delay(uint16_t nCount);
void clockGpioConfig(void);

ErrorCode tamperSwitchTest(void);
ErrorCode i2cTest(void);
ErrorCode rtcTest(void);
void initializeTagPins(void);
void initializeTagEE(void);
void tagDiagnostics(void);

ErrorCode  readDateEE (Date  *date);
ErrorCode  readTimeEE (Time  *time);

#ifdef DEBUG
#define  BREAKPOINT()       trap()
#else
#define  BREAKPOINT()
#endif

#endif
/*************************** END OF FILE **************************************/
