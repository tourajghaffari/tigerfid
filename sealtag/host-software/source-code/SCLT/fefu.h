/*-------------------------------------------------------
|                                                       |
|                       fefu.h                          |
|                                                       |
---------------------------------------------------------

Copyright © 2003-2008	FEIG ELECTRONIC GmbH, All Rights Reserved.
						Lange Strasse 4
						D-35781 Weilburg
						Federal Republic of Germany
						phone    : +49 6471 31090
						fax      : +49 6471 310999
						e-mail   : info@feig.de
						Internet : http://www.feig.de
					
Author         		:	Markus Hultsch

Version       		:	01.03.00 / 01.04.2008 / M. Hultsch

Operation Systems	:	Windows 9x/ME/NT/2000/XP/Vista
						Linux


This file contains the constants, datatypes and function declartions of FEFU library
*/

#ifndef _FEFU_INCLUDE_H
#define _FEFU_INCLUDE_H


#if defined(_MSC_VER) || defined(__BORLANDC__)
	#ifdef FEFUDLL
		#define DLL_EXT_FUNC __declspec(dllexport) __stdcall
	#else
		#define DLL_EXT_FUNC __declspec(dllimport) __stdcall
	#endif
#else
	#define	DLL_EXT_FUNC
	#define	CALLBACK
#endif


// type defines
#ifndef UCHAR
	#define UCHAR unsigned char
#endif


#ifdef __cplusplus
	extern "C" {
#endif




// #####################################################
// FEFU constants
// #####################################################

// common errors
#define FEFU_ERR_POINTER_IS_NULL			-4102
#define	FEFU_ERR_NO_MORE_MEM				-4103
#define FEFU_ERR_UNSUPPORTED_FUNCTION		-4105

// communication errors
#define	FEFU_ERR_PROTLEN        			-4130
#define	FEFU_ERR_CHECKSUM					-4031
#define	FEFU_ERR_TIMEOUT					-4132
#define	FEFU_ERR_UNKNOWN_STATUS				-4133
#define	FEFU_ERR_NO_RECDATA		   			-4134

// parameter errors
#define	FEFU_ERR_UNKNOWN_PARAMETER			-4150
#define	FEFU_ERR_PARAMETER_OUT_OF_RANGE		-4151
#define	FEFU_ERR_UNKNOWN_ERRORCODE			-4153



// constants for dynamic load of library

// miscellaneous functions
#define FEFU_GET_DLL_VERSION				4100
#define FEFU_GET_ERROR_TEXT					4101
#define FEFU_GET_STATUS_TEXT				4102

// query functions
#define FEFU_GET_LAST_STATE					4103
#define FEFU_GET_LAST_ERROR					4104

// communication functions for multiplexer
#define FEFU_MUX_CPU_RESET					4110
#define FEFU_MUX_SOFT_VERSION				4111
#define FEFU_MUX_SELECT_CHANNEL				4112
#define FEFU_MUX_DETECT						4113
#define FEFU_UMUX_CPU_RESET					4114
#define FEFU_UMUX_SOFT_VERSION				4115
#define FEFU_UMUX_SELECT_CHANNEL			4116
#define FEFU_UMUX_DETECT					4117

// communication functions for antenna tuner
#define FEFU_DAT_SOFT_VERSION				4120
#define FEFU_DAT_CPU_RESET					4121
#define FEFU_DAT_SET_CAPACITIES				4122
#define FEFU_DAT_GET_VALUES					4123
#define FEFU_DAT_SET_OUTPUT					4124
#define FEFU_DAT_RE_TUNING					4125
#define FEFU_DAT_START_TUNING				4126
#define FEFU_DAT_SWITCH_ANTENNA				4127
#define FEFU_DAT_STORE_SETTINGS				4128
#define FEFU_DAT_SET_ADDRESS				4129
#define FEFU_DAT_DETECT						4130
#define FEFU_DAT_SET_MODE					4131



// #####################################################
// FEFU functions
// #####################################################

// miscellaneous functions
void DLL_EXT_FUNC FEFU_GetDLLVersion( char* cVersion );
int  DLL_EXT_FUNC FEFU_GetErrorText( int iErrorCode, char* cErrorText );
int  DLL_EXT_FUNC FEFU_GetStatusText( UCHAR ucStatus, char* cStatusText );

// query functions
int  DLL_EXT_FUNC FEFU_GetLastState( char* cStatusText );
int  DLL_EXT_FUNC FEFU_GetLastError( int* iErrorCode, char* cErrorText );

// communication functions for multiplexer
int  DLL_EXT_FUNC FEFU_MUX_CPUReset( int iReaderHnd, UCHAR ucReaderBusAdr, UCHAR ucMuxAdr );
int  DLL_EXT_FUNC FEFU_MUX_SoftVersion( int iReaderHnd, UCHAR ucReaderBusAdr, UCHAR ucMuxAdr, UCHAR* ucVersion );
int  DLL_EXT_FUNC FEFU_MUX_SelectChannel( int iReaderHnd, UCHAR ucReaderBusAdr, UCHAR ucMuxAdr, UCHAR ucIn1, UCHAR ucIn2 );
int  DLL_EXT_FUNC FEFU_MUX_Detect( int iReaderHnd, UCHAR ucReaderBusAdr, UCHAR ucMuxAdr );

// communication functions for antenna tuner
int  DLL_EXT_FUNC FEFU_DAT_SoftVersion( int iReaderHnd, UCHAR ucReaderBusAdr, UCHAR ucDatAdr, UCHAR* ucVersion );
int  DLL_EXT_FUNC FEFU_DAT_CPUReset( int iReaderHnd, UCHAR ucReaderBusAdr, UCHAR ucDatAdr );
int  DLL_EXT_FUNC FEFU_DAT_SetCapacities( int iReaderHnd, UCHAR ucReaderBusAdr, UCHAR ucDatAdr, UCHAR ucCap1, UCHAR ucCap2 );
int  DLL_EXT_FUNC FEFU_DAT_GetValues( int iReaderHnd, UCHAR ucReaderBusAdr, UCHAR ucDatAdr, UCHAR* ucValues );
int  DLL_EXT_FUNC FEFU_DAT_SetOutput( int iReaderHnd, UCHAR ucReaderBusAdr, UCHAR ucDatAdr, UCHAR ucOut );
int  DLL_EXT_FUNC FEFU_DAT_ReTuning( int iReaderHnd, UCHAR ucReaderBusAdr, UCHAR ucDatAdr );
int  DLL_EXT_FUNC FEFU_DAT_StartTuning( int iReaderHnd, UCHAR ucReaderBusAdr, UCHAR ucDatAdr );
int  DLL_EXT_FUNC FEFU_DAT_SwitchAntenna( int iReaderHnd, UCHAR ucReaderBusAdr, UCHAR ucDatAdr );
int  DLL_EXT_FUNC FEFU_DAT_StoreSettings( int iReaderHnd, UCHAR ucReaderBusAdr, UCHAR ucDatAdr );
int  DLL_EXT_FUNC FEFU_DAT_SetAddress( int iReaderHnd, UCHAR ucReaderBusAdr, UCHAR ucDatAdr, UCHAR ucNewDatAdr );
int  DLL_EXT_FUNC FEFU_DAT_Detect( int iReaderHnd, UCHAR ucReaderBusAdr, UCHAR ucDatAdr );
int  DLL_EXT_FUNC FEFU_DAT_SetMode( int iReaderHnd, UCHAR ucReaderBusAdr, UCHAR ucDatAdr, UCHAR ucMode );


// communication functions for UHF multiplexer
int  DLL_EXT_FUNC FEFU_UMUX_CPUReset( int iReaderHnd, UCHAR ucReaderBusAdr, UCHAR ucMuxAdr, UCHAR ucFlags, UCHAR* ucMuxState );
int  DLL_EXT_FUNC FEFU_UMUX_SoftVersion( int iReaderHnd, UCHAR ucReaderBusAdr, UCHAR ucMuxAdr, UCHAR ucFlags,UCHAR* ucMuxState, UCHAR* ucVersion );
int  DLL_EXT_FUNC FEFU_UMUX_SelectChannel( int iReaderHnd, UCHAR ucReaderBusAdr, UCHAR ucMuxAdr, UCHAR ucFlags, UCHAR ucChannelNo, UCHAR* ucMuxState);
int  DLL_EXT_FUNC FEFU_UMUX_Detect_GetPower( int iReaderHnd, UCHAR ucReaderBusAdr, UCHAR ucMuxAdr, UCHAR ucFlags, UCHAR* ucMuxState, UCHAR* ucData );

#undef DLL_EXT_FUNC

#ifdef __cplusplus
	}
#endif


// #####################################################
// typedefs of DLL-functions for explicite loading
// #####################################################

// miscellaneous functions
typedef void (CALLBACK* LPFN_FEFU_GET_DLL_VERSION)(char*);
typedef int  (CALLBACK* LPFN_FEFU_GET_ERROR_TEXT)(int, char*);
typedef int  (CALLBACK* LPFN_FEFU_GET_STATUS_TEXT)(UCHAR, char*);

// query functions
typedef int  (CALLBACK* LPFN_FEFU_GET_LAST_ERROR)(int*, char*);
typedef int  (CALLBACK* LPFN_FEFU_GET_LAST_STATE)(char*);

// communication functions for multiplexer
typedef int  (CALLBACK* LPFN_FEFU_MUX_CPU_RESET)(int, UCHAR, UCHAR);
typedef int  (CALLBACK* LPFN_FEFU_MUX_SOFT_VERSION)(int, UCHAR, UCHAR, UCHAR*);
typedef int  (CALLBACK* LPFN_FEFU_MUX_SELECT_CHANNEL)(int, UCHAR, UCHAR, UCHAR, UCHAR);
typedef int  (CALLBACK* LPFN_FEFU_MUX_DETECT)(int, UCHAR, UCHAR);

// communication functions for UHF multiplexer
typedef int  (CALLBACK* LPFN_FEFU_UMUX_CPU_RESET)(int, UCHAR, UCHAR, UCHAR, UCHAR*);
typedef int  (CALLBACK* LPFN_FEFU_UMUX_SOFT_VERSION)(int, UCHAR, UCHAR, UCHAR, UCHAR*, UCHAR*);
typedef int  (CALLBACK* LPFN_FEFU_UMUX_SELECT_CHANNEL)(int, UCHAR, UCHAR, UCHAR, UCHAR, UCHAR*);
typedef int  (CALLBACK* LPFN_FEFU_UMUX_DETECT)(int, UCHAR, UCHAR, UCHAR, UCHAR*, UCHAR*);

// communication functions for dynamic antenna tuner
typedef int  (CALLBACK* LPFN_FEFU_DAT_CPU_RESET)(int, UCHAR, UCHAR);
typedef int  (CALLBACK* LPFN_FEFU_DAT_SOFT_VERSION)(int, UCHAR, UCHAR, UCHAR*);
typedef int  (CALLBACK* LPFN_FEFU_DAT_SET_CAPACITIES)(int, UCHAR, UCHAR, UCHAR, UCHAR);
typedef int  (CALLBACK* LPFN_FEFU_DAT_GET_VALUES)(int, UCHAR, UCHAR, UCHAR*);
typedef int  (CALLBACK* LPFN_FEFU_DAT_SET_OUTPUT)(int, UCHAR, UCHAR, UCHAR);
typedef int  (CALLBACK* LPFN_FEFU_DAT_RE_TUNING)(int, UCHAR, UCHAR);
typedef int  (CALLBACK* LPFN_FEFU_DAT_START_TUNING)(int, UCHAR, UCHAR);
typedef int  (CALLBACK* LPFN_FEFU_DAT_SWITCH_ANTENNA)(int, UCHAR, UCHAR);
typedef int  (CALLBACK* LPFN_FEFU_DAT_STORE_SETTINGS)(int, UCHAR, UCHAR);
typedef int  (CALLBACK* LPFN_FEFU_DAT_SET_ADDRESS)(int, UCHAR, UCHAR, UCHAR);
typedef int  (CALLBACK* LPFN_FEFU_DAT_DETECT)(int, UCHAR, UCHAR);
typedef int  (CALLBACK* LPFN_FEFU_DAT_SET_MODE)(int, UCHAR, UCHAR, UCHAR);

#endif // _FEFU_INCLUDE_H

