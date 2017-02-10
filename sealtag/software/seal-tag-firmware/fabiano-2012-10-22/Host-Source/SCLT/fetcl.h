/*-------------------------------------------------------
|                                                       |
|                      fetcl.h                          |
|                                                       |
---------------------------------------------------------

Copyright © 2005-2011	FEIG ELECTRONIC GmbH, All Rights Reserved.
						Lange Strasse 4
						D-35781 Weilburg
						Federal Republic of Germany
						phone    : +49 6471 31090
						fax      : +49 6471 310999
						e-mail   : obid-support@feig.de
						Internet : http://www.feig.de

Autor         	:		M. Hultsch
Version       	:		01.00.07 / 18.02.2011 / M. Hultsch
						
Projekt       	:		API for APDU transfer 
						
Betriebssystem	:		Windows XP/Vista/7
						WindowsCE
						Linux

This file contains the constants, datatypes snd function declartions of FETCL library
*/


#ifndef _FETCL_INCLUDE_H
 #define _FETCL_INCLUDE_H


#if defined (_MSC_VER) || defined(__BORLANDC__)
 #ifdef FETCLDLL
  #define DLL_EXT_FUNC __declspec(dllexport) __stdcall
 #else
  #define DLL_EXT_FUNC __declspec(dllimport) __stdcall
 #endif
#else
 #define	DLL_EXT_FUNC
#endif

#ifndef UCHAR
 #define UCHAR unsigned char
#endif

#ifndef UINT
 #define UINT unsigned int
#endif


#ifdef __cplusplus
 extern "C" {
#endif


#define LANGUAGE		9


// error codes
//********************

// common errors
#define	FETCL_ERR_NEW_TRANSPONDER_FAILURE       -4200
#define	FETCL_ERR_EMPTY_LIST					-4201
#define	FETCL_ERR_POINTER_IS_NULL				-4202
#define FETCL_ERR_NO_MORE_MEM					-4203
#define FETCL_ERR_UNKNOWN_COMM_PORT				-4204
#define FETCL_ERR_UNSUPPORTED_FUNCTION			-4205
#define FETCL_ERR_NO_USB_SUPPORT				-4206
#define FETCL_ERR_OLD_FECOM						-4207
#define FETCL_ERR_FILE_COULD_NOT_BE_OPENED      -4208
#define FETCL_ERR_APDU_CURRENTLY_RUNNING        -4209

// query errors
#define FETCL_ERR_NO_VALUE		   				-4210

// handle errors
#define FETCL_ERR_UNKNOWN_HND					-4220
#define FETCL_ERR_HND_IS_NULL					-4221
#define FETCL_ERR_HND_IS_NEGATIVE				-4222
#define FETCL_ERR_NO_HND_FOUND					-4223
#define FETCL_ERR_TRANSPONDER_HND_IS_NEGATIVE   -4224
#define FETCL_ERR_HND_UNVALID					-4225
#define FETCL_ERR_READER_HND_IS_NEGATIVE        -4226
#define FETCL_ERR_THREAD_NOT_CREATED            -4227

// parameter errors
#define FETCL_ERR_UNKNOWN_PARAMETER				-4250
#define FETCL_ERR_PARAMETER_OUT_OF_RANGE        -4251
#define FETCL_ERR_ODD_PARAMETERSTRING           -4252
#define FETCL_ERR_UNKNOWN_ERRORCODE				-4253
#define	FETCL_ERR_UNDERSIZED_RESPONSE_BUFFER    -4257		// Responsebuffer < 64kB

// communication data flow errors
#define FETCL_ERR_BUFFER_OVERFLOW				-4270
#define	FETCL_ERR_OVERSIZED_RESPONSE            -4271		// Response > 64kB
#define	FETCL_INVALID_ACKNOWLEDGEMENT           -4272
#define	FETCL_INVALID_ACKNOWLEDGEMENT_LENGTH    -4273
#define FETCL_LIST_COMPLETE_FAILURE				-4274		// Liste sollte komplett sein.
#define	FETCL_INCOMPLETE_RESPONSE				-4275		// Response zurueck, aber nicht	komplett.												//	gab aber was anderes 
#define FETCL_INVALID_PROTOCOL					-4276		
#define FETCL_INVALID_TRANSMISSION				-4277		




// defines for uiFlag in FETCL_EVENT_INIT
#define FETCL_THREAD_ID			1
#define FETCL_WND_HWND			2
#define FETCL_CALLBACK			3
#define FETCL_CALLBACK2			4
#define FETCL_TASKCB_NET_1     10


// #################
// FETCL structures
// #################


typedef struct _FETCL_EVENT_INIT	// Structur for eventhandling; thread-ID, 
{									//   message-handle or callback

	void*	pAny;		// pointer to anything, which is reflected as the first parameter 
						// in the callback function cbFct2 (e.g. can be used to pass the object pointer)
	UINT	uiUse;		// unused
	UINT	uiMsg;		// message code used with dwThreadID and hwndWnd (e.g. WM_USER_xyz)
	UINT	uiFlag;		// specifies the use of the union (e.g. FETCL_WND_HWND)
	union
	{
#if defined(_MSC_VER) || defined(__BORLANDC__)
		DWORD	dwThreadID;					// for thread-ID
		HWND	hwndWnd;					// for window-handle
#endif
		void	(*cbFct)(int, int, int);                        // for callback-function
		void	(*cbFct2)(void*, int, int, int);                // for callback-function

#if defined(_MSC_VER)
		// for .NET delegates; for SAM, Queue and Container commands
		void	(CALLBACK* cbFctNET1)(	int iHandle,	// [out] handle of FETCL transponder object
										int iError,		// [out] OK (=0), error code (<0) or status byte from reader (>0)
										int iRspLen );	// [out] length of response data

#endif
#ifdef __cplusplus
	};
#else
	}Method;
#endif

} FETCL_EVENT_INIT;




//##########################
// FETCL functions
//##########################
	

// miscellaneous functions
//********************

void DLL_EXT_FUNC FETCL_GetDLLVersion( char* cVersion );

int DLL_EXT_FUNC FETCL_GetErrorText(	int iErrorCode, char* cErrorText );

int DLL_EXT_FUNC FETCL_GetLastError( int* iErrorCode, char* cErrorText );



// ddministrative functions
//***************************

int  DLL_EXT_FUNC FETCL_NewTransponder(	int iReaderHnd, 
										UCHAR ucBusAdr,
										UCHAR ucCid, 
										UCHAR ucNad, 
										bool bUseCid, 
										bool bUseNad );

int  DLL_EXT_FUNC FETCL_DeleteTransponder( int iTransponderHnd );

int  DLL_EXT_FUNC FETCL_GetTransponderList( int iNext );


// communication functions
//**************************

int DLL_EXT_FUNC FETCL_Apdu(	int iTransponderHnd, 
								UCHAR* ucData, 
								int iDataLen, 
								FETCL_EVENT_INIT* pInit );

int DLL_EXT_FUNC FETCL_Deselect( int iTransponderHnd );

int DLL_EXT_FUNC FETCL_Ping( int iTransponderHnd );

int DLL_EXT_FUNC FETCL_GetResponseData(	int iTransponderHnd, 
										UCHAR* ucData, 
										int iDataBufLen );


#undef DLL_EXT_FUNC

#ifdef __cplusplus
	}
#endif


#endif	// _FETCL_INCLUDE_H
