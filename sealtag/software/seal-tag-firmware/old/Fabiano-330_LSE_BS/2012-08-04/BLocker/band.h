/*******************************************************************************
* @file     band.h
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

#ifndef __BAND_H__
#define __BAND_H__


#define TAMPER_DETECTION_THRESHOLD (uint16_t)0x777 // 0x555 is 1 second, 0xaaa is 2 seconds, 0xfff is 3 seconds  TGTG 777


typedef enum openclose {
    CLOSE = 0,
    OPEN  = 1
} OpenClose;


void       Band_Init    (void);
OpenClose  Band_GetState(void);
bool       Band_IsOpen  (void);

#endif
