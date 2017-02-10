/*******************************************************************************
* @file     rtc.h
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

#ifndef  __RTC_H__
#define  __RTC_H__

typedef enum weekday {
    Weekday_Monday    =  ((uint8_t)0x01), /*!< WeekDay is Monday */
    Weekday_Tuesday   =  ((uint8_t)0x02), /*!< WeekDay is Tuesday */
    Weekday_Wednesday =  ((uint8_t)0x03), /*!< WeekDay is Wednesday */
    Weekday_Thursday  =  ((uint8_t)0x04), /*!< WeekDay is Thursday */
    Weekday_Friday    =  ((uint8_t)0x05), /*!< WeekDay is Friday */
    Weekday_Saturday  =  ((uint8_t)0x06), /*!< WeekDay is Saturday */
    Weekday_Sunday    =  ((uint8_t)0x07)  /*!< WeekDay is Sunday */
} WeekDay;


typedef enum month {
    Month_January   =  ((uint8_t)0x01), /*!< Month is January */
    Month_February  =  ((uint8_t)0x02), /*!< Month is February */
    Month_March     =  ((uint8_t)0x03), /*!< Month is March */
    Month_April     =  ((uint8_t)0x04), /*!< Month is April */
    Month_May       =  ((uint8_t)0x05), /*!< Month is May */
    Month_June      =  ((uint8_t)0x06), /*!< Month is June */
    Month_July      =  ((uint8_t)0x07), /*!< Month is July */
    Month_August    =  ((uint8_t)0x08), /*!< Month is August */
    Month_September =  ((uint8_t)0x09), /*!< Month is September */
    Month_October   =  ((uint8_t)0x10), /*!< Month is October */
    Month_November  =  ((uint8_t)0x11), /*!< Month is November */
    Month_December  =  ((uint8_t)0x12)  /*!< Month is December */
} Month;


typedef struct date {
  WeekDay  WeekDay; /*!< The RTC Calender Weekday. */
  Month    Month;   /*!< The RTC Calender Month. */
  uint8_t  Date;    /*!< The RTC Calender Date. This parameter can be any value from 1 to 31. */
  uint8_t  Year;    /*!< The RTC Calender Date. This parameter can be any value from 0 to 99. */
} Date;


typedef struct time {
  uint8_t  Hours;       /*!< RTC Hours.   This parameter can be any value from 0 to 23. */
  uint8_t  Minutes;     /*!< RTC Minutes. This parameter can be any value from 0 to 59. */
  uint8_t  Seconds;     /*!< RTC Seconds. This parameter can be any value from 0 to 59. */
} Time;


extern const uint16_t rtcDateAddress;
extern const uint16_t rtcTimeAddress;

ErrorCode rtcTest();

#endif
