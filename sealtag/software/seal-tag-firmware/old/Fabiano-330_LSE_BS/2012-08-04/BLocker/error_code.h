/*******************************************************************************
* @file     error_code.h
* @author   fgk
* @version  V3.4.0
* @date     08/04/12
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
*                     Corrected interrupt handling for Band tamper
*******************************************************************************/

#ifndef __ERROR_CODE_H__
#define __ERROR_CODE_H__

//------------------------------------------------------------------------------
// Error Codes
//------------------------------------------------------------------------------
typedef  enum  errorcode {
// Command Status...
    COMMAND_STATUS_COMPLETE                          =  0,	// ERROR CODE otherwise...
    COMMAND_STATUS_BUSY	                             =  1,
    COMMAND_STATUS_BUSY_ACK                          =  2,
    COMMAND_STATUS_RECEIVED                          =  3,
    COMMAND_STATUS_RUNNING                           =  4,
    COMMAND_STATUS_UNKNOWN                           = 0xFF,
    ERROR_NONE                                       =  0,
    ERROR_FLUSH_FIRST_READ_HISTORY                   = -1,
    ERROR_BAND_OPEN_MUST_CLOSE	                     = -2,
    ERROR_RTC_CONFIG_REQUIRED	                     = -3,
    ERROR_INVALID_COMMAND                            = -4,
    ERROR_INVALID_COMMAND_STATUS                     = -5,
    ERROR_I2C_BUS_BUSY		                         = -6,
    ERROR_I2C_EVENT_MASTER_MODE_SELECT               = -7,
    ERROR_I2C_EVENT_MASTER_TRANSMITTER_MODE_SELECTED = -8,
    ERROR_I2C_EVENT_MASTER_BYTE_TRANSMITTING	     = -9,
    ERROR_I2C_EVENT_MASTER_BYTE_TRANSMITTED	         = -10,
    ERROR_I2C_EVENT_MASTER_RECEIVER_MODE_SELECTED    = -11,
    ERROR_RTC_INIT                                   = -12,
    ERROR_RTC_SET_DATE                               = -13,
    ERROR_RTC_SET_TIME                               = -14,
    ERROR_UNKNOWN                                    = -15
} ErrorCode;

#endif
