/*******************************************************************************
* @file     rtc.c
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

#include "stm8l15x.h"
#include "stm8l15x_rtc.h"
#include "blocker_services.h"
#include "error_code.h"
#include "i2c_ee.h"
#include "rtc.h"


extern uint8_t histPacket[2];
extern volatile bool rtcConfigured;

const uint16_t rtcDateAddress = 0x0008;
const uint16_t rtcTimeAddress = 0x000C;


/*
*******************************************************************************
*                               startTimers()
*
* Description : Start RTC.
*
* Return(s)   : none.
*******************************************************************************
*/

void startTimers()
{
    CLK_RTCClockConfig(CLK_RTCCLKSource_LSE, CLK_RTCCLKDiv_1);
    CLK_PeripheralClockConfig(CLK_Peripheral_RTC, ENABLE);

    delay(2000);      // delay 2.5ms
}


/*
*******************************************************************************
*                               stopTimers()
*
* Description : Stop RTC.
*
* Return(s)   : none.
*******************************************************************************
*/

void stopTimers()
{
    CLK_PeripheralClockConfig(CLK_Peripheral_RTC, DISABLE);
    CLK_RTCClockConfig(CLK_RTCCLKSource_Off, CLK_RTCCLKDiv_1);

    delay(2000);      // delay 2.5ms
}



/*
*******************************************************************************
*                              configureRTC()
*
* Description : Configure RTC date and time.
*
* Return(s)   : ERROR_NONE, if NO error(s).
*
*               ErrorCode,  otherwise.
*******************************************************************************
*/

ErrorCode configureRTC()
{
    RTC_InitTypeDef  init;
    RTC_TimeTypeDef  rtc_time;
    RTC_DateTypeDef  rtc_date;
    Date             date;
    Time             time;
    ErrorCode        status;
    ErrorStatus      ret;


    startTimers();

    init.RTC_HourFormat   = RTC_HourFormat_24;
    init.RTC_AsynchPrediv = 0x7F;
    init.RTC_SynchPrediv  = 0xFF;
    ret = RTC_Init(&init);
    if (ret == ERROR)
    {
        setCommandStatus(ERROR_RTC_SET_DATE, TRUE);
        BREAKPOINT();
        return (ERROR_RTC_SET_DATE);
    }

    date.WeekDay = Weekday_Monday;
    date.Date    = 1;
    date.Month   = Month_January;
    date.Year    = 0;

    status = readDateEE(&date);
    if (status != ERROR_NONE)
    {
        setCommandStatus(status, TRUE);
        BREAKPOINT();
        return status;
    }

    rtc_date.RTC_WeekDay = (RTC_Weekday_TypeDef)date.WeekDay;
    rtc_date.RTC_Month   = (RTC_Month_TypeDef  )date.Month;
    rtc_date.RTC_Date    =                      date.Date;
    rtc_date.RTC_Year    =                      date.Year;

    ret = RTC_SetDate(RTC_Format_BIN, &rtc_date);
    if (ret == ERROR)
    {
        setCommandStatus(ERROR_RTC_SET_DATE, TRUE);
        BREAKPOINT();
        return (ERROR_RTC_SET_DATE);
    }

    status = readTimeEE(&time);
    if (status != ERROR_NONE)
    {
        setCommandStatus(status, TRUE);
        BREAKPOINT();
        return status;
    }

    rtc_time.RTC_Hours   = time.Hours;
    rtc_time.RTC_Minutes = time.Minutes;
    rtc_time.RTC_Seconds = time.Seconds;
    rtc_time.RTC_H12     = RTC_H12_AM;

    ret = RTC_SetTime(RTC_Format_BIN, &rtc_time);
    if (ret == ERROR)
    {
        setCommandStatus(ERROR_RTC_SET_TIME, TRUE);
        BREAKPOINT();
        return (ERROR_RTC_SET_TIME);
    }

    return status;
}

static ErrorCode getTimeTest(RTC_TimeTypeDef *pTime)
{
    while (RTC_WaitForSynchro() != SUCCESS);
    RTC_GetTime(RTC_Format_BIN, pTime);

#ifdef UPDATE_RF_EEPROM
    rtcTime[2] = pTime->RTC_Hours;
    rtcTime[1] = pTime->RTC_Minutes;
    rtcTime[0] = pTime->RTC_Seconds;
    return(i2cWriteBuffer(rtcTime, rtcTimeAddress, 3, EEPROM_USER_ADDRESS));
#else
    return (ERROR_NONE);
#endif
}

ErrorCode readRTC()
{
    ErrorCode        status;
    RTC_TimeTypeDef  rtc_time;
    RTC_DateTypeDef  rtc_date;
    uint8_t          date[4] = { 0, 0, 0, 0 };
    uint8_t          time[3] = { 0, 0, 0 };
    uint16_t         rtcHistoryAddress = getInt16(histPacket);


    while (RTC_WaitForSynchro() != SUCCESS);

    RTC_GetDate(RTC_Format_BIN, &rtc_date);
    RTC_GetTime(RTC_Format_BIN, &rtc_time);

    date[3] = rtc_date.RTC_WeekDay;
    date[2] = rtc_date.RTC_Month;
    date[1] = rtc_date.RTC_Date;
    date[0] = rtc_date.RTC_Year;

    time[2] = rtc_time.RTC_Hours;
    time[1] = rtc_time.RTC_Minutes;
    time[0] = rtc_time.RTC_Seconds;

    status = i2cWriteBuffer(date, rtcDateAddress, 4, EEPROM_USER_ADDRESS);
    if (status == ERROR_NONE)
    {
        status = i2cWriteBuffer(time, rtcTimeAddress, 3, EEPROM_USER_ADDRESS);
        if (status == ERROR_NONE)
        {
            status = i2cWriteBuffer(date, rtcHistoryAddress, 4, EEPROM_USER_ADDRESS);
            if (status == ERROR_NONE)
            {
                status = i2cWriteBuffer(time, rtcHistoryAddress + 4, 3, EEPROM_USER_ADDRESS);
            }
        }
    }
    if (status != ERROR_NONE)
    {
        setCommandStatus(status, TRUE);
        BREAKPOINT();
    }
    return status;
}

ErrorCode rtcTest()
{
    ErrorCode status = ERROR_NONE;
    uint8_t tSeconds = 0;
    int16_t rtcTestTimeout = 500;

    RTC_InitTypeDef init;
    RTC_TimeTypeDef time;
    RTC_DateTypeDef date;

    //--------------------------------------------------------------------------
    startTimers();
    //--------------------------------------------------------------------------

    init.RTC_HourFormat   = RTC_HourFormat_24;
    init.RTC_AsynchPrediv = 0x7F;
    init.RTC_SynchPrediv  = 0xFF;
    RTC_Init(&init);
	
    RTC_DateStructInit(&date);
    date.RTC_WeekDay = RTC_Weekday_Friday;
    date.RTC_Month   = RTC_Month_June;
    date.RTC_Date    = 24;
    date.RTC_Year    = 11;
    if (RTC_SetDate(RTC_Format_BIN, &date) == ERROR) {
        BREAKPOINT();
        return (ERROR_RTC_SET_DATE);
    }

    RTC_TimeStructInit(&time);
    time.RTC_Hours   = 1;
    time.RTC_Minutes = 0;
    time.RTC_Seconds = 0;
    if (RTC_SetTime(RTC_Format_BIN, &time) == ERROR) {
        BREAKPOINT();
        return (ERROR_RTC_SET_TIME);
    }

    while(tSeconds < 3)
    {
        delay(0xffff);
        while (RTC_WaitForSynchro() != SUCCESS);
        //.........RTC_GetTime(RTC_Format_BIN, &time);
        //-----------------------------
        status = getTimeTest(&time);
        if (status != ERROR_NONE) {
            BREAKPOINT();
            break;
        }
        //-----------------------------
        tSeconds = (uint8_t)time.RTC_Seconds;

        rtcTestTimeout--;
        if (rtcTestTimeout < 0)
        {
            status = ERROR_UNKNOWN;
            BREAKPOINT();
            break;
        }
    }

    //--------------------------------------------------------------------------
    stopTimers();
    rtcConfigured = FALSE;
    //--------------------------------------------------------------------------

    if (status == ERROR_NONE)
    {
        delay(0xffff);
        while (RTC_WaitForSynchro() != SUCCESS);
        RTC_GetTime(RTC_Format_BIN, &time);
        tSeconds = (uint8_t)time.RTC_Seconds;

        for (int i = 0; i < tSeconds / 2; i++)
            delay(0xffff);
        while (RTC_WaitForSynchro() != SUCCESS);
        RTC_GetTime(RTC_Format_BIN, &time);
        status = ERROR_UNKNOWN;
        if ((uint8_t)time.RTC_Seconds == tSeconds)
        {
            status = ERROR_NONE;
        }
    }
    return status;
}
