/*-------------------------------------------------------
|                                                       |
|                       fecom.h                         |
|                                                       |
---------------------------------------------------------

Copyright  2000-2010	FEIG ELECTRONIC GmbH, All Rights Reserved.
                        Lange Strasse 4
                        D-35781 Weilburg
                        Federal Republic of Germany
                        phone    : +49 6471 31090
                        fax      : +49 6471 310999
                        e-mail   : obid-support@feig.de
                        Internet : http://www.feig.de
					
Author         		:	Markus Hultsch

Version       		:	Windows:		02.08.12 / 25.03.2010 / M. Hultsch
Version       		:	Linux:			02.08.10 / 18.08.2009 / M. Sahm
Version       		:	Windows CE:		02.08.11 / 22.03.2010 / M. Hultsch

Operation Systems	:	Windows 2000/XP/Vista/7
						Linux
						Windows CE


This file contains the constants, datatypes and function declartions of FECOM library
*/

#ifndef _FECOM_INCLUDE_H
#define _FECOM_INCLUDE_H


#if defined(_MSC_VER) || defined(__BORLANDC__)
	#ifdef FECOMDLL
		#define DLL_EXT_FUNC __declspec(dllexport) __stdcall
	#else
		#define DLL_EXT_FUNC __declspec(dllimport) __stdcall
	#endif

	#ifndef CALLBACK
		#define CALLBACK __stdcall
	#endif
#else
	#define	DLL_EXT_FUNC
	#define	CALLBACK
#endif


// type defines
#ifdef __GNUC__
	#ifndef __int64
		#define __int64 long long
	#endif
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




// #####################################################
// FECOM constants
// #####################################################

// FECOM error codes

// common errors
#define	FECOM_ERR_NEWPORT_FAILURE			-1000
#define	FECOM_ERR_EMPTY_LIST				-1001
#define FECOM_ERR_POINTER_IS_NULL			-1002
#define FECOM_ERR_NO_MEMORY					-1003
#define FECOM_ERR_UNSUPPORTED_HARDWARE		-1004
#define FECOM_ERR_PORT_NOT_FOUND			-1005
#define FECOM_ERR_UNSUPPORTED				-1006

// error while open the port
#define	FECOM_ERR_NO_PORT					-1010
#define	FECOM_ERR_NO_CONNECT				-1011
#define	FECOM_ERR_LINK_ID					-1012
#define	FECOM_ERR_PORT_IS_OPEN				-1013

// handle errors
#define	FECOM_ERR_UNKNOWN_HND				-1020
#define	FECOM_ERR_HND_IS_NULL				-1021
#define	FECOM_ERR_HND_IS_NEGATIVE			-1022
#define	FECOM_ERR_NO_HND_FOUND				-1023

// communication errors
#define	FECOM_ERR_TIMEOUT					-1030
#define	FECOM_ERR_NO_SENDPROTOCOL			-1031
#define	FECOM_ERR_RECEIVE_PROCESS			-1032
#define	FECOM_ERR_INIT_COMM_PROCESS			-1033
#define	FECOM_ERR_FLUSH_INPUT_BUFFER		-1034
#define	FECOM_ERR_FLUSH_OUTPUT_BUFFER		-1035
#define	FECOM_ERR_CHANGE_PORT_PARA			-1036
#define	FECOM_ERR_TRANSMIT_PROCESS			-1037
#define	FECOM_ERR_RECEIVE_NOISE_DATA		-1038
#define	FECOM_ERR_PORT_IS_BUSY				-1039
#define FECOM_ERR_READ_PORT_PARA            -1040

// parameter errors
#define	FECOM_ERR_UNKNOWN_PARAMETER			-1050
#define	FECOM_ERR_PARAMETER_OUT_OF_RANGE	-1051
#define	FECOM_ERR_ODD_PARAMETERSTRING		-1052
#define	FECOM_ERR_PORTNR_OUT_OF_RANGE		-1053
#define	FECOM_ERR_UNKNOWN_ERRORCODE			-1054

// receive buffer overflow
#define	FECOM_ERR_OVL_RECBUF				-1070



// constants for dynamic load of library

// miscellaneous functions
#define FECOM_GET_DLL_VERSION			1000
#define FECOM_GET_ERROR_TEXT			1001
#define FECOM_GET_LAST_ERROR			1002
#define FECOM_DETECT_PORT				1003

// functions for event notification
#define FECOM_ADD_EVENT_HANDLER			1010
#define FECOM_DEL_EVENT_HANDLER			1011

// port functions
#define FECOM_OPEN_PORT					1020
#define FECOM_CLOSE_PORT				1021
#define FECOM_GET_PORT_LIST				1022
#define FECOM_GET_PORT_PARA				1023
#define FECOM_SET_PORT_PARA				1024
#define FECOM_DO_PORT_CMD				1025
#define FECOM_GET_PORT_HND				1026

// communication function
#define FECOM_TRANSCEIVE				1030
#define FECOM_TRANSMIT					1031
#define FECOM_RECEIVE					1032



// constants for structure _FECOM_EVENT_INIT

// defines for uiFlag in FECOM_EVENT_INIT
#define FECOM_THREAD_ID		1
#define FECOM_WND_HWND		2
#define FECOM_CALLBACK		3
#define FECOM_EVENT			4

// defines for uiUse in FECOM_EVENT_INIT
#define FECOM_CTS_EVENT				 1
#define FECOM_DCD_EVENT				 2
#define FECOM_DSR_EVENT				 3
#define FECOM_RTS_EVENT				 4
#define FECOM_DTR_EVENT				 5

#define FECOM_PORT_CLOSED_EVENT		20	// only for Windows CE (customized versions)
#define FECOM_PORT_REOPEN_EVENT		21	// only for Windows CE (customized versions)



// #####################################################
// FECOM structures
// #####################################################

// structure for transfering thread-IDs, message-handles or callbacks
typedef struct _FECOM_EVENT_INIT
{
	UINT uiUse;		// defines the event (e.g. FECOM_CTS_EVENT)
	UINT uiMsg;		// message code used with dwThreadID and hwndWnd (e.g. WM_USER_xyz)
	UINT uiFlag;	// specifies the use of the union (e.g. FECOM_WND_HWND)
	union
	{
#if defined(_MSC_VER) || defined(__BORLANDC__)
		DWORD	dwThreadID;			// for thread-ID
		HWND	hwndWnd;			// for window-handle
		HANDLE	hEvent;				// for event-handle
#endif
		void	(*cbFct)(int, int);	// for callback-function
#ifdef __cplusplus
	};
#else
	}Method;
#endif

} FECOM_EVENT_INIT;



// #####################################################
// FECOM functions
// #####################################################

// miscellaneous functions
void DLL_EXT_FUNC FECOM_GetDLLVersion( char* cVersion );
int  DLL_EXT_FUNC FECOM_GetErrorText( int iErrorCode, char* cErrorText );
int  DLL_EXT_FUNC FECOM_GetLastError( int iPortHnd, int* iErrorCode, char* cErrorText );
int  DLL_EXT_FUNC FECOM_DetectPort( int iPortNr );

// functions for event notification
int  DLL_EXT_FUNC FECOM_AddEventHandler(int iPortHnd, FECOM_EVENT_INIT* pInit);
int  DLL_EXT_FUNC FECOM_DelEventHandler(int iPortHnd, FECOM_EVENT_INIT* pInit);

// port functions
int  DLL_EXT_FUNC FECOM_OpenPort( char* cPortNr );
int  DLL_EXT_FUNC FECOM_ClosePort( int iPortHnd );
int  DLL_EXT_FUNC FECOM_GetPortList( int iNext );
int  DLL_EXT_FUNC FECOM_GetPortPara( int iPortHnd, char* cPara, char* cValue );
int  DLL_EXT_FUNC FECOM_SetPortPara( int iPortHnd, char* cPara, char* cValue );
int  DLL_EXT_FUNC FECOM_DoPortCmd( int iPortHnd, char* cCmd, char* cValue );
int  DLL_EXT_FUNC FECOM_GetPortHnd( char* cPortNr );

// communication function
int  DLL_EXT_FUNC FECOM_Transceive( int iPortHnd, UCHAR* cSendProt, int iSendLen, UCHAR* cRecProt, int iRecLen );
int  DLL_EXT_FUNC FECOM_Transmit( int iPortHnd, UCHAR* cSendProt, int iSendLen );
int  DLL_EXT_FUNC FECOM_Receive( int iPortHnd, UCHAR* cRecProt, int iRecLen );


#undef DLL_EXT_FUNC

#ifdef __cplusplus
}
#endif


// #####################################################
// typedefs of library functions for explicite loading
// #####################################################

// miscellaneous functions
typedef void (CALLBACK* LPFN_FECOM_GET_DLL_VERSION)(char*);
typedef int  (CALLBACK* LPFN_FECOM_GET_ERROR_TEXT)(int, char*);
typedef int  (CALLBACK* LPFN_FECOM_GET_LAST_ERROR)(int, int*, char*);
typedef int  (CALLBACK* LPFN_FECOM_DETECT_PORT)(int);

// functions for event notification
typedef int  (CALLBACK* LPFN_FECOM_ADD_EVENT_HANDLER)(int, FECOM_EVENT_INIT*);
typedef int  (CALLBACK* LPFN_FECOM_DEL_EVENT_HANDLER)(int, FECOM_EVENT_INIT*);

// port functions
typedef int  (CALLBACK* LPFN_FECOM_OPEN_PORT)(char*);
typedef int  (CALLBACK* LPFN_FECOM_CLOSE_PORT)(int);
typedef int  (CALLBACK* LPFN_FECOM_GET_PORT_LIST)(int);
typedef int  (CALLBACK* LPFN_FECOM_GET_PORT_PARA)(int, char*, char*);
typedef int  (CALLBACK* LPFN_FECOM_SET_PORT_PARA)(int, char*, char*);
typedef int  (CALLBACK* LPFN_FECOM_DO_PORT_CMD)(int, char*, char*);
typedef int  (CALLBACK* LPFN_FECOM_GET_PORT_HND)(char*);

// communication function
typedef int  (CALLBACK* LPFN_FECOM_TRANSCEIVE)(int, UCHAR*, int, UCHAR*, int);
typedef int  (CALLBACK* LPFN_FECOM_TRANSMIT)(int, UCHAR*, int);
typedef int  (CALLBACK* LPFN_FECOM_RECEIVE)(int, UCHAR*, int);

#endif // _FECOM_INCLUDE_H
