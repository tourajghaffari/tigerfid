/*-------------------------------------------------------
|                                                       |
|                      feisc.h                          |
|                                                       |
---------------------------------------------------------

Copyright  1998-2011	FEIG ELECTRONIC GmbH, All Rights Reserved.
						Lange Strasse 4
						D-35781 Weilburg
						Federal Republic of Germany
						phone    : +49 6471 31090
						fax      : +49 6471 310999
						e-mail   : obid-support@feig.de
						Internet : http://www.feig.de

Author         		:	Markus Hultsch
Version       		:	06.03.00 / 18.02.2011 / M. Hultsch

Operation Systems	:	Windows XP/Vista/7/Linux
						WindowsCE/uClinux/VxWorks


This file contains the constants, datatypes and function declartions of FEISC library
*/

#ifndef _FEISC_INCLUDE_H
#define _FEISC_INCLUDE_H


#if defined(_MSC_VER) || defined(__BORLANDC__)
	#ifdef FEISCDLL
		#define DLL_EXT_FUNC __declspec(dllexport) __stdcall
	#else
		#define DLL_EXT_FUNC __declspec(dllimport) __stdcall
	#endif
#else
	#define	DLL_EXT_FUNC
#endif


// type defines
#if defined(__GNUC__) || defined(_FEISC_VXWORKS)
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
// FEISC constants
// #####################################################

// command bytes
#define FEISC_DESTROY					0x18
#define FEISC_HALT						0x1A
#define FEISC_RESET_QUIET_BIT			0x1B
#define FEISC_EAS_REQUEST				0x1C
#define FEISC_MAX_DATA_EXCHANGE			0x1F
#define FEISC_READ_BUFFER				0x21
#define FEISC_ADV_READ_BUFFER			0x22
#define FEISC_READ_BUFFER_INFO			0x31
#define FEISC_CLEAR_BUFFER				0x32
#define FEISC_INIT_BUFFER				0x33
#define FEISC_FORCE_NOTIFY_TRIGGER		0x34
#define FEISC_SOFTWARE_TRIGGER			0x35
#define FEISC_GET_BAUDRATE				0x52
#define FEISC_START_FLASH_LOADER		0x55
#define FEISC_START_FLASH_LOADER_EX		0x55
#define FEISC_CPU_RESET					0x63
#define FEISC_SYSTEM_RESET				0x64
#define FEISC_SOFT_VERSION				0x65
#define FEISC_READER_INFO				0x66
#define FEISC_CHANNEL_ANALYZE			0x68
#define FEISC_RF_RESET					0x69
#define FEISC_RF_ON_OFF					0x6A
#define FEISC_CENTRALIZED_RF_SYNC		0x6B
#define FEISC_SET_NOISE_LEVEL			0x6C
#define FEISC_GET_NOISE_LEVEL			0x6D
#define FEISC_READER_DIAGNOSE			0x6E
#define FEISC_ANTENNA_TUNING			0x6F
#define FEISC_SET_OUTPUT				0x71
#define FEISC_SET_OUTPUT_EX				0x72
#define FEISC_GET_INPUT					0x74
#define FEISC_ADJ_ANTENNA				0x75
#define FEISC_CHECK_ANTENNAS			0x76
#define FEISC_GET_COUNTER				0x77
#define FEISC_SET_COUNTER				0x78
#define FEISC_WRITE_DISPLAY				0x7A
#define FEISC_READ_CONF_BLOCK			0x80
#define FEISC_WRITE_CONF_BLOCK			0x81
#define FEISC_SAVE_CONF_BLOCK			0x82
#define FEISC_RESET_CONF_BLOCK			0x83
#define FEISC_SET_CONF_MEM_LOC			0x84
#define FEISC_SET_SYSTEM_TIMER			0x85
#define FEISC_GET_SYSTEM_TIMER			0x86
#define FEISC_SET_SYSTEM_DATE			0x87
#define FEISC_GET_SYSTEM_DATE			0x88
#define FEISC_READ_CONFIGURATION		0x8A
#define FEISC_WRITE_CONFIGURATION		0x8B
#define FEISC_RESET_CONFIGURATION		0x8C
#define FEISC_PIGGYBACK_COMMAND			0x9F
#define FEISC_READER_LOGIN				0xA0
#define FEISC_WRITE_MIFARE_KEYS			0xA2
#define FEISC_WRITE_DES_AES_KEYS		0xA3
#define FEISC_WRITE_READER_AUTHENT_KEY	0xAD
#define FEISC_READER_AUTHENT			0xAE
#define FEISC_CRYPTO_COMMAND			0xAF
#define FEISC_ISO_COMMAND				0xB0
#define FEISC_ISO_CUST_PROP_CMD			0xB1
#define FEISC_ISO_SP14443_COMMAND		0xB2
#define FEISC_EPC_COMMAND				0xB3
#define FEISC_EPC_UHF_COMMAND			0xB4
#define FEISC_C1G2_TRANSPARENT_CMD		0xBB
#define FEISC_COMMAND_QUEUE				0xBC
#define FEISC_ISO14443A_TRANSPARENT_CMD	0xBD
#define FEISC_ISO14443B_TRANSPARENT_CMD	0xBE
#define FEISC_ISO15693_TRANSPARENT_CMD	0xBF
#define FEISC_SAM_COMMAND				0xC0
#define FEISC_DESFIRE_COMMAND			0xC1
#define FEISC_MIFARE_PLUS_COMMAND		0xC2
#define FEISC_DESFIRE_COMMAND_EX		0xC3


// error codes

// common errors
#define	FEISC_ERR_NEWREADER_FAILURE			-4000
#define	FEISC_ERR_EMPTY_LIST				-4001
#define FEISC_ERR_POINTER_IS_NULL			-4002
#define	FEISC_ERR_NO_MORE_MEM				-4003
#define FEISC_ERR_UNKNOWN_COMM_PORT			-4004
#define FEISC_ERR_UNSUPPORTED_FUNCTION		-4005
#define FEISC_ERR_NO_USB_SUPPORT			-4006
#define FEISC_ERR_OLD_FECOM					-4007

// query errors
#define	FEISC_ERR_NO_VALUE		   			-4010

// handle errors
#define	FEISC_ERR_UNKNOWN_HND				-4020
#define	FEISC_ERR_HND_IS_NULL				-4021
#define	FEISC_ERR_HND_IS_NEGATIVE			-4022
#define	FEISC_ERR_NO_HND_FOUND				-4023
#define	FEISC_ERR_PORTHND_IS_NEGATIVE		-4024
#define	FEISC_ERR_HND_UNVALID				-4025

// communication errors
#define	FEISC_ERR_PROTLEN        			-4030
#define	FEISC_ERR_CHECKSUM					-4031
#define	FEISC_ERR_BUSY_TIMEOUT				-4032
#define	FEISC_ERR_UNKNOWN_STATUS			-4033
#define	FEISC_ERR_NO_RECPROTOCOL   			-4034
#define	FEISC_ERR_CMD_BYTE        			-4035
#define	FEISC_ERR_TRANSCEIVE     			-4036
#define FEISC_ERR_REC_BUS_ADR				-4037
#define FEISC_ERR_READER_POWER_DOWN			-4038

// parameter errors
#define	FEISC_ERR_UNKNOWN_PARAMETER			-4050
#define	FEISC_ERR_PARAMETER_OUT_OF_RANGE	-4051
#define	FEISC_ERR_ODD_PARAMETERSTRING		-4052
#define	FEISC_ERR_UNKNOWN_ERRORCODE			-4053
#define FEISC_ERR_UNSUPPORTED_OPTION		-4054
#define FEISC_ERR_UNKNOWN_EPC_TYPE			-4055
#define FEISC_ERR_PARAMETER_LOCKED			-4056

// plug-in errors
#define	FEISC_ERR_NO_PLUGIN					-4060
#define	FEISC_ERR_PLUGIN_PRESENT			-4061
#define	FEISC_ERR_UNKNOWN_PLUGIN_ID			-4062
#define	FEISC_ERR_PI_BUILD_DATA				-4063
#define	FEISC_ERR_PI_BUILD_FRAME			-4064
#define	FEISC_ERR_PI_SPLIT_FRAME			-4065
#define	FEISC_ERR_PI_SPLIT_DATA				-4066
#define	FEISC_ERR_PI_GET					-4067
#define	FEISC_ERR_PI_LOAD					-4068
#define	FEISC_ERR_PI_RELEASE				-4069

// communication data flow errors
#define	FEISC_ERR_BUFFER_OVERFLOW			-4070

// task errors
#define	FEISC_ERR_TASK_STILL_RUNNING		-4080
#define	FEISC_ERR_TASK_NOT_STARTED			-4081
#define	FEISC_ERR_TASK_TIMEOUT				-4082
#define	FEISC_ERR_TASK_SOCKET_INIT			-4083
#define	FEISC_ERR_TASK_BUSY					-4084
#define	FEISC_ERR_THREAD_CANCEL_ERROR		-4085

// errors from cryptography module
#define	FEISC_ERR_CRYPT_LOAD_LIBRARY		-4090
#define	FEISC_ERR_CRYPT_INIT				-4091
#define	FEISC_ERR_CRYPT_AUTHENT_PROCESS		-4092
#define	FEISC_ERR_CRYPT_ENCYPHER			-4093
#define	FEISC_ERR_CRYPT_DECYPHER			-4094



// defines for uiFlag in FEISC_EVENT_INIT
#define FEISC_THREAD_ID						1
#define FEISC_WND_HWND						2
#define FEISC_CALLBACK						3
#define FEISC_EVENT							4
#define FEISC_CALLBACK_2					5
#define FEISC_CALLBACK_4					6

// defines for uiUse in FEISC_EVENT_INIT
#define FEISC_PRT_EVENT						1
#define FEISC_SNDPRT_EVENT					2
#define FEISC_RECPRT_EVENT					3
#define FEISC_SCANNER_EVENT					4


// defines for uiFlag in FEISC_TASK_INIT
#define FEISC_TASKCB_1						1
#define FEISC_TASKCB_2						2
#define FEISC_TASKCB_3						3
#define FEISC_TASKCB_NET_1				   10

// defines for task identifier
#define FEISC_TASKID_FIRST_NEW_TAG			1
#define FEISC_TASKID_EVERY_NEW_TAG			2
#define FEISC_TASKID_NOTIFICATION			3
#define FEISC_TASKID_SAM_COMMAND			4
#define FEISC_TASKID_COMMAND_QUEUE			5
#define FEISC_TASKID_MAX_EVENT				6
#define FEISC_TASKID_PEOPLE_COUNTER_EVENT	7

// defines for channel types in FEISC_TASK_INIT
#define FEISC_TASK_CHANNEL_TYPE_AS_OPEN		1	// for open serial, usb, tcp channels
#define FEISC_TASK_CHANNEL_TYPE_NEW_TCP		5	// for new tcp connection, established by host or reader



// #####################################################
// FEISC structures
// #####################################################

// structure for transfering thread-IDs, message-handles or callbacks
typedef struct _FEISC_EVENT_INIT
{
	void* pAny;		// pointer to anything, which is reflected as the first parameter 
					// in the callback function (e.g. can be used to pass the object pointer)

	unsigned int uiUse;		// defines the event (e.g. FEISC_PRT_EVENT)
	unsigned int uiMsg;		// message code used with dwThreadID and hwndWnd (e.g. WM_USER_xyz)
	unsigned int uiFlag;	// specifies the use of the union (e.g. FEISC_WND_HWND)
	union
	{
#if defined(_MSC_VER) || defined(__BORLANDC__)
		DWORD	dwThreadID;					// for thread-ID
		HWND	hwndWnd;					// for window-handle
		HANDLE	hEvent;						// for event-handle
		void	(*cbFct2)(BSTR, int, int);	// for callback2-function
#endif
		void	(*cbFct)(int, int);			// for callback-function
		void	(*cbFct4)(	void* pAny,			// [in] pointer to anything (from struct _FEISC_EVENT_INIT)
							const char* cMsg,	// [in] pointer to message
							int iStatus );		// [in] status byte (>=0) or error code (<0)
#ifdef __cplusplus
	};
#else
	}Method;
#endif

} FEISC_EVENT_INIT;



// structure for transfering anything for an asynchronous task
typedef struct _FEISC_TASK_INIT
{
	void*			pAny;			// pointer to anything, which is reflected as the first parameter 
									// in the callback function (e.g. can be used to pass the object pointer)
	unsigned char	ucBusAdr;		// busaddress for serial communication
	unsigned int	uiChannelType;	// defines the channel type to be used
	int				iConnectByHost;	// if 0: TCP/IP connection is initiated by reader. otherwise by host
	char			cIPAdr[16];		// server ip address
									// note: only for channel type FEISC_TASK_CHANNEL_TYPE_NEW_TCP
	int				iPortNr;		// server or host port address
									// note: only for channel type FEISC_TASK_CHANNEL_TYPE_NEW_TCP
	unsigned int	uiTimeout;		// timeout for asynchronous task in steps of 100ms
	unsigned int	uiFlag;			// specifies the use of the union (e.g. FEISC_TASKCB_1)

	//  only for authentication in notification mode
	bool			bCryptoMode;		// security mode on/off
	unsigned int	uiAuthentKeyLength;	// authent key length
	unsigned char	ucAuthentKey[32];	// authent key

	union
	{
		int iNotifyWithAck;	// 0: notification without acknowledge
							// 1: notification with acknowledge (only for non-secured communication)
#ifdef __cplusplus
	};
#else
	}InData;
#endif

	union
	{
		// for notification and inventory task, SAM and Queue Command response, People Counter event
		void	(*cbFct1)(	void* pAny,					// [in] pointer to anything (from struct _FEISC_TASK_INIT)
							int iReaderHnd,				// [in] reader handle of FEISC
							int iTaskID,				// [in] task identifier from FEISC_StartAsyncTask(..)
							int iError,					// [in] OK (=0), error code (<0) or status byte from reader (>0)
							unsigned char ucCmd,		// [in]	reader command
							unsigned char* ucRspData,	// [in] response data
							int iRspLen );				// [in] length of response data

		// only for notification task and People Counter event
		void	(*cbFct2)(	void* pAny,					// [in] pointer to anything (from struct _FEISC_TASK_INIT)
							int iReaderHnd,				// [in] reader handle of FEISC
							int iTaskID,				// [in] task identifier from FEISC_StartAsyncTask(..)
							int iError,					// [in] OK (=0), error code (<0) or status byte from reader (>0)
							unsigned char ucCmd,		// [in]	reader command
							unsigned char* ucRspData,	// [in] response data
							int iRspLen,				// [in] length of response data
							char* cRemoteIP,			// [in] ip address of the reader
							int iLocalPort );			// [in] local port number which received the notification

		// only for MAX notification task
		int		(*cbFct3)(	void* pAny,					// [in] pointer to anything (from struct _FEISC_TASK_INIT)
							int iReaderHnd,				// [in] reader handle of FEISC
							int iTaskID,				// [in] task identifier from FEISC_StartAsyncTask(..)
							int iError,					// [in] OK (=0), error code (<0) or status byte from reader (>0)
							unsigned char ucCmd,		// [in]	reader command
							unsigned char* ucRspData,	// [in] response data
							int iRspLen,				// [in] length of response data
							char* cRemoteIP,			// [in] ip address of the reader
							int iLocalPort,				// [in] local port number which received the notification
							unsigned char& ucAction );	// [out] action set by host application

#if defined(_MSC_VER)
		// for .NET delegates; for SAM, Queue and Container commands
		void	(CALLBACK* cbFctNET1)(	int iTaskID,				// [in] task identifier
										int iError,					// [in] OK (=0), error code (<0) or status byte from reader (>0)
										unsigned char ucCmd,		// [in]	reader command
										unsigned char* ucRspData,	// [in] response data
										int iRspLen );				// [in] length of response data

#endif

#ifdef __cplusplus
	};
#else
	}Method;
#endif

} FEISC_TASK_INIT;




// #####################################################
// FEISC functions
// #####################################################
	

// miscellaneous functions
void DLL_EXT_FUNC FEISC_GetDLLVersion( char* cVersion );
int  DLL_EXT_FUNC FEISC_GetErrorText( int iErrorCode, char* cErrorText );
int  DLL_EXT_FUNC FEISC_GetStatusText( unsigned char ucStatus, char* cStatusText );

// functions for event notification
int  DLL_EXT_FUNC FEISC_AddEventHandler(int iReaderHnd, FEISC_EVENT_INIT* pInit);
int  DLL_EXT_FUNC FEISC_DelEventHandler(int iReaderHnd, FEISC_EVENT_INIT* pInit);

// administration functions
void DLL_EXT_FUNC FEISC_SetCbPtr_ProtStr( void (*cbPtr) );
int  DLL_EXT_FUNC FEISC_NewReader( int iPortHnd );
int  DLL_EXT_FUNC FEISC_DeleteReader( int iReaderHnd );
int  DLL_EXT_FUNC FEISC_GetReaderList( int iNext );
int  DLL_EXT_FUNC FEISC_GetReaderPara( int iReaderHnd, char* cPara, char* cValue );
int  DLL_EXT_FUNC FEISC_SetReaderPara( int iReaderHnd, char* cPara, char* cValue );

// functions for managing/using custom communication interfaces with Plug-ins
int  DLL_EXT_FUNC FEISC_PI_Get( const char* cLibName, void** pPlugIn );
int  DLL_EXT_FUNC FEISC_PI_Install( int iReaderHnd, void* pPlugIn );
int  DLL_EXT_FUNC FEISC_PI_Remove( int iReaderHnd );
int  DLL_EXT_FUNC FEISC_PI_OpenPort( int iReaderHnd, char* cPortDefinition );
int  DLL_EXT_FUNC FEISC_PI_ClosePort( int iReaderHnd );
int  DLL_EXT_FUNC FEISC_PI_GetPortPara( int iReaderHnd, char* cPara, char* cValue );
int  DLL_EXT_FUNC FEISC_PI_SetPortPara( int iReaderHnd, char* cPara, char* cValue );
int  DLL_EXT_FUNC FEISC_PI_GetDLLVersion( int iReaderHnd, char* cVersion );
int  DLL_EXT_FUNC FEISC_PI_GetErrorText( int iReaderHnd, int iErrorCode, char* cErrorText );

// functions for asynchrone tasks
int  DLL_EXT_FUNC FEISC_StartAsyncTask(	int iReaderHnd,				// reader handle
										int iTaskID,				// task ID
										FEISC_TASK_INIT* pInit,		// initialization structure
										void* pInput );				// for task specific input data

int  DLL_EXT_FUNC FEISC_CancelAsyncTask( int iReaderHnd );
int  DLL_EXT_FUNC FEISC_TriggerAsyncTask( int iReaderHnd );

// functions for protocol frame
int  DLL_EXT_FUNC FEISC_BuildSendProtocol(	int iReaderHnd, 
											unsigned char cBusAdr, 
											unsigned char cCmdByte, 
											unsigned char* cSendData, 
											int iDataLen, 
											unsigned char* cSendProt, 
											int iDataFormat );

int  DLL_EXT_FUNC FEISC_BuildRecProtocol(	int iReaderHnd, 
											unsigned char cBusAdr, 
											unsigned char cCmdByte, 
											unsigned char cStatus, 
											unsigned char* cRecData, 
											int iDataLen, 
											unsigned char* cRecProt, 
											int iDataFormat );

int  DLL_EXT_FUNC FEISC_SplitSendProtocol(	int iReaderHnd, 
											unsigned char* cSendProt, 
											int iSendLen, 
											unsigned char* cBusAdr, 
											unsigned char* cCmdByte, 
											unsigned char* cSendData, 
											int* iDataLen, 
											int iDataFormat );

int  DLL_EXT_FUNC FEISC_SplitRecProtocol(	int iReaderHnd, 
											unsigned char* cRecProt, 
											int iRecLen, 
											unsigned char* cBusAdr, 
											unsigned char* cCmdByte, 
											unsigned char* cRecData, 
											int* iDataLen, 
											int iDataFormat );

int  DLL_EXT_FUNC FEISC_Conv2StdProtocol( unsigned char* cProt, int iLen );
int  DLL_EXT_FUNC FEISC_Conv2AdvProtocol( unsigned char* cProt, int iLen );

// query functions
int  DLL_EXT_FUNC FEISC_GetLastSendProt( int iReaderHnd, unsigned char* cSendProt, int iDataFormat );
int  DLL_EXT_FUNC FEISC_GetLastRecProt( int iReaderHnd, unsigned char* cRecProt, int iDataFormat );
int  DLL_EXT_FUNC FEISC_GetLastState( int iReaderHnd, char* cStateText );
int  DLL_EXT_FUNC FEISC_GetLastRecProtLen(int iReaderHnd);
int  DLL_EXT_FUNC FEISC_GetLastError( int iReaderHnd, int* iErrorCode, char* cErrorText );

// common communication functions
int  DLL_EXT_FUNC FEISC_SendTransparent(	int iReaderHnd, 
											unsigned char* cSendProt, 
											int iSendLen, 
											unsigned char* cRecProt, 
											int iMaxRecLen, 
											int iCheckSum, 
											int iDataFormat );

int  DLL_EXT_FUNC FEISC_Transmit(	int iReaderHnd, 
									unsigned char* cSendProt, 
									int iSendLen, 
									int iCheckSum, 
									int iDataFormat );

int  DLL_EXT_FUNC FEISC_Receive(	int iReaderHnd, 
									unsigned char* cRecProt, 
									int iMaxRecLen, 
									int iDataFormat );

// communication functions
int  DLL_EXT_FUNC FEISC_0x18_Destroy(	int iReaderHnd, 
										unsigned char cBusAdr, 
										unsigned char ucMode, 
										unsigned char* ucEPC, 
										unsigned char* ucPW );

int  DLL_EXT_FUNC FEISC_0x1A_Halt( int iReaderHnd, unsigned char cBusAdr );
int  DLL_EXT_FUNC FEISC_0x1B_ResetQuietBit(	int iReaderHnd, unsigned char cBusAdr );
int  DLL_EXT_FUNC FEISC_0x1C_EASRequest( int iReaderHnd, unsigned char cBusAdr );

int  DLL_EXT_FUNC FEISC_0x1F_MAXDataExchange(	int iReaderHnd, 
												unsigned char cBusAdr, 
												unsigned char cCmd, 
												unsigned char cMode, 
												unsigned char cTableID, 
												unsigned char* cReqData,
												int iReqDataLen,
												unsigned char* cRspData,
												int* iRspDataLen,
												int iDataFormat );

int  DLL_EXT_FUNC FEISC_0x21_ReadBuffer(	int iReaderHnd, 
											unsigned char cBusAdr, 
											unsigned char cSets, 
											unsigned char* cTrData, 
											unsigned char* cRecSets, 
											unsigned char* cRecDataSets, 
											int iDataFormat );

int  DLL_EXT_FUNC FEISC_0x22_ReadBuffer(	int iReaderHnd, 
											unsigned char cBusAdr, 
											int iSets, 
											unsigned char* cTrData, 
											int* iRecSets, 
											unsigned char* cRecDataSets, 
											int iDataFormat );

int  DLL_EXT_FUNC FEISC_0x31_ReadDataBufferInfo(	int iReaderHnd, 
													unsigned char cBusAdr, 
													unsigned char* cTabSize, 
													unsigned char* cTabStart, 
													unsigned char* cTabLen, 
													int iDataFormat );

int  DLL_EXT_FUNC FEISC_0x32_ClearDataBuffer( int iReaderHnd, unsigned char cBusAdr );
int  DLL_EXT_FUNC FEISC_0x33_InitBuffer( int iReaderHnd, unsigned char cBusAdr );
int  DLL_EXT_FUNC FEISC_0x34_ForceNotifyTrigger( int iReaderHnd, unsigned char cBusAdr, unsigned char cMode );
int	 DLL_EXT_FUNC FEISC_0x35_SoftwareTrigger( int iReaderHnd, UCHAR cBusAdr, UCHAR cMode, UCHAR cOption, UCHAR cCycle, UINT uiCycleTimeout);
int  DLL_EXT_FUNC FEISC_0x52_GetBaud( int iReaderHnd, unsigned char cBusAdr );
int  DLL_EXT_FUNC FEISC_0x55_StartFlashLoader( int iReaderHnd );
int  DLL_EXT_FUNC FEISC_0x55_StartFlashLoaderEx( int iReaderHnd, unsigned char cBusAdr );
int  DLL_EXT_FUNC FEISC_0x63_CPUReset( int iReaderHnd, unsigned char cBusAdr );
int  DLL_EXT_FUNC FEISC_0x64_SystemReset( int iReaderHnd, unsigned char cBusAdr, unsigned char cMode );

int  DLL_EXT_FUNC FEISC_0x65_SoftVersion(	int iReaderHnd, 
											unsigned char cBusAdr, 
											unsigned char* cVersion, 
											int iDataFormat );

int  DLL_EXT_FUNC FEISC_0x66_ReaderInfo(	int iReaderHnd, 
											unsigned char cBusAdr, 
											unsigned char cMode, 
											unsigned char* cVersion, 
											int iDataFormat );

int  DLL_EXT_FUNC FEISC_0x68_ChannelAnalyze( int iReaderHnd, 
											  unsigned char cBusAdr, 
											  unsigned char cMode, 
											  unsigned char cRes1, 
											  unsigned char cRes2, 
											  unsigned char cRes3, 
											  int iRes4, 
											  long* nData );

int  DLL_EXT_FUNC FEISC_0x69_RFReset( int iReaderHnd, unsigned char cBusAdr );
int  DLL_EXT_FUNC FEISC_0x6A_RFOnOff( int iReaderHnd, unsigned char cBusAdr, unsigned char cRF );
int  DLL_EXT_FUNC FEISC_0x6B_InitNoiseThreshold( int iReaderHnd, unsigned char cBusAdr );

int  DLL_EXT_FUNC FEISC_0x6B_CentralizedRFSync( int iReaderHnd,
												unsigned char cBusAdr,
												unsigned char cMode,
												unsigned char cTxChannel,
												int   iTxPeriod,
												unsigned char cRes1,
												unsigned char cRes2 );

int  DLL_EXT_FUNC FEISC_0x6C_SetNoiseLevel( int iReaderHnd, 
											unsigned char cBusAdr, 
											unsigned char* cLevel, 
											int iDataFormat );

int  DLL_EXT_FUNC FEISC_0x6D_GetNoiseLevel( int iReaderHnd, 
											unsigned char cBusAdr, 
											unsigned char* cLevel, 
											int iDataFormat );

int  DLL_EXT_FUNC FEISC_0x6E_RdDiag( int iReaderHnd, unsigned char cBusAdr, unsigned char cMode, unsigned char* cData );
int  DLL_EXT_FUNC FEISC_0x6F_AntennaTuning( int iReaderHnd, unsigned char cBusAdr );

int  DLL_EXT_FUNC FEISC_0x71_SetOutput( int iReaderHnd, 
										unsigned char cBusAdr,  
										int iOS, 
										int iOSF, 
										int iOSTime, 
										int iOutTime );

int  DLL_EXT_FUNC FEISC_0x72_SetOutput( int iReaderHnd, 
										unsigned char cBusAdr,
										unsigned char cMode,
										unsigned char cOutN,
										unsigned char* pRecords );

int  DLL_EXT_FUNC FEISC_0x74_ReadInput( int iReaderHnd, unsigned char cBusAdr, unsigned char* cInput );

int  DLL_EXT_FUNC FEISC_0x75_AdjAntenna(	int iReaderHnd, 
											unsigned char cBusAdr, 
											unsigned char* cValue, 
											int iDataFormat );

int  DLL_EXT_FUNC FEISC_0x76_CheckAntennas(	int iReaderHnd, 
											unsigned char cBusAdr, 
											unsigned char cMode,
											unsigned char* cAntOut,
											int* iAntOutLen );

int  DLL_EXT_FUNC FEISC_0x7A_WriteDisplay(	int iReaderHnd, 
											unsigned char cBusAdr, 
											unsigned char cMode,
											unsigned char cRfu1,
											unsigned char cRfu2,
											unsigned char cLine,
											unsigned char cOffset,
											unsigned char cTextLen,
											char* cText );

int  DLL_EXT_FUNC FEISC_0x80_ReadConfBlock( int iReaderHnd, 
											unsigned char cBusAdr, 
											unsigned char cConfAdr, 
											unsigned char* cConfBlock, 
											int iDataFormat );

int  DLL_EXT_FUNC FEISC_0x81_WriteConfBlock(	int iReaderHnd, 
												unsigned char cBusAdr, 
												unsigned char cConfAdr, 
												unsigned char* cConfBlock, 
												int iDataFormat );

int  DLL_EXT_FUNC FEISC_0x82_SaveConfBlock( int iReaderHnd, unsigned char cBusAdr, unsigned char cConfAdr );
int  DLL_EXT_FUNC FEISC_0x83_ResetConfBlock( int iReaderHnd, unsigned char cBusAdr, unsigned char cConfAdr );
int  DLL_EXT_FUNC FEISC_0x84_SetCFGMemLoc( int iReaderHnd, unsigned char cBusAdr, unsigned char cMemAdr );

int  DLL_EXT_FUNC FEISC_0x85_SetSysTimer(	int iReaderHnd, 
											unsigned char cBusAdr, 
											unsigned char* cTime, 
											int iDataFormat );

int  DLL_EXT_FUNC FEISC_0x86_GetSysTimer(	int iReaderHnd, 
											unsigned char cBusAdr, 
											unsigned char* cTime, 
											int iDataFormat );

int  DLL_EXT_FUNC FEISC_0x87_SetSystemDate( int iReaderHnd, 
											unsigned char cBusAdr, 
											unsigned char cCentury, 
											unsigned char cYear, 
											unsigned char cMonth, 
											unsigned char cDay,
											unsigned char cTimezone, 
											unsigned char cHour, 
											unsigned char cMinute, 
											int iMilliSecond );

int  DLL_EXT_FUNC FEISC_0x88_GetSystemDate( int iReaderHnd, 
											unsigned char cBusAdr, 
											unsigned char* cCentury, 
											unsigned char* cYear, 
											unsigned char* cMonth, 
											unsigned char* cDay,
											unsigned char* cTimezone, 
											unsigned char* cHour, 
											unsigned char* cMinute, 
											int* iMilliSecond );

int  DLL_EXT_FUNC FEISC_0x8A_ReadConfiguration(	int iReaderHnd, 
												unsigned char  cBusAdr, 
												unsigned char  cDevice,			// in: device
												unsigned char  cBank,			// in: bank
												unsigned char  cMode,			// in: mode
												unsigned int   iReqBlockAdr,	// in: first block address
												unsigned char  cReqBlockCount,	// in: number of blocks
												unsigned char* cRspBlockCount,	// out: number of blocks
												unsigned char* cRspBlockSize,	// out: blocksize
												unsigned char* cRspData );		// out: cReqBlockCount * [cfg-adr + configuration data]

int  DLL_EXT_FUNC FEISC_0x8B_WriteConfiguration(int iReaderHnd, 
												unsigned char  cBusAdr, 
												unsigned char  cDevice,			// in: device
												unsigned char  cBank,			// in: bank
												unsigned char  cMode,			// in: mode
												unsigned char  cReqBlockCount,	// in: number of blocks
												unsigned char  cReqBlockSize,	// in: blocksize
												unsigned char* cReqData );		// in: cReqBlockCount * [cfg-adr + configuration data]

int  DLL_EXT_FUNC FEISC_0x8C_ResetConfiguration(int iReaderHnd, 
												unsigned char  cBusAdr, 
												unsigned char  cDevice,			// in: device
												unsigned char  cBank,			// in: bank
												unsigned char  cMode,			// in: mode
												unsigned int   iReqBlockAdr,	// in: first block address
												unsigned char  cReqBlockCount );// in: number of blocks

int  DLL_EXT_FUNC FEISC_0x9F_Piggyback_Command(	int iReaderHnd, 
												unsigned char  cBusAdr, 
												unsigned char  cMode,			// in: mode
												unsigned char  cDevice,			// in: device
												unsigned char  cPort,			// in: port
												unsigned char* cReqData,		// in:
												int			   iReqLen,			// in: 
												unsigned char* cRspData,		// out:
												int*		   iRspLen );		// out:

int  DLL_EXT_FUNC FEISC_0xA0_RdLogin(	int iReaderHnd, 
										unsigned char cBusAdr, 
										unsigned char* cRd_PW, 
										int iDataFormat );

int  DLL_EXT_FUNC FEISC_0xA2_WriteMifareKeys(	int iReaderHnd, 
												unsigned char cBusAdr, 
												unsigned char cType, 
												unsigned char cAdr, 
												unsigned char* cKey, 
												int iDataFormat );

int  DLL_EXT_FUNC FEISC_0xA3_Write_DES_AES_Keys(	int iReaderHnd, 
													unsigned char cBusAdr, 
													unsigned char cMode, 
													unsigned char cReaderKeyIndex,
													unsigned char cAuthentMode,
													unsigned char cKeyLen,
													unsigned char* cKey, 
													int iDataFormat );

int  DLL_EXT_FUNC FEISC_0xAD_WriteReaderAuthentKey(	int iReaderHnd, 
													unsigned char cBusAdr, 
													unsigned char cMode, 
													unsigned char cKeyType,
													unsigned char cKeyLen,
													unsigned char* cKey, 
													int iDataFormat );

int  DLL_EXT_FUNC FEISC_0xAE_ReaderAuthent(			int iReaderHnd, 
													unsigned char cBusAdr, 
													unsigned char cMode, 
													unsigned char cKeyType,
													unsigned char cKeyLen,
													unsigned char* cKey, 
													int iDataFormat );

int  DLL_EXT_FUNC FEISC_0xB0_ISOCmd(	int iReaderHnd, 
										unsigned char cBusAdr, 
										unsigned char* cReqData, 
										int iReqLen, 
										unsigned char* cRspData, 
										int* iRspLen, 
										int iDataFormat );

int  DLL_EXT_FUNC FEISC_0xB1_ISOCustAndPropCmd( int iReaderHnd, 
												unsigned char cBusAdr, 
												unsigned char cMfr, 
												unsigned char* cReqData, 
												int iReqLen, 
												unsigned char* cRspData, 
												int* iRspLen, 
												int iDataFormat );

int  DLL_EXT_FUNC FEISC_0xB2_ISOCmd(	int iReaderHnd, 
										unsigned char cBusAdr, 
										unsigned char* cReqData, 
										int iReqLen, 
										unsigned char* cRspData, 
										int* iRspLen, 
										int iDataFormat );

int  DLL_EXT_FUNC FEISC_0xB3_EPCCmd(	int iReaderHnd, 
										unsigned char cBusAdr, 
										unsigned char* cReqData, 
										int iReqLen, 
										unsigned char* cRspData, 
										int* iRspLen, 
										int iDataFormat );

int  DLL_EXT_FUNC FEISC_0xB4_EPC_UHF_Cmd(	int iReaderHnd, 
											unsigned char cBusAdr, 
											unsigned char cMfr, 
											unsigned char* cReqData, 
											int iReqLen, 
											unsigned char* cRspData, 
											int* iRspLen, 
											int iDataFormat );


int  DLL_EXT_FUNC FEISC_0xBB_C1G2_TranspCmd(int iReaderHnd, 
											unsigned char ucBusAdr, 
											unsigned char ucMode, 
											unsigned char ucTxPara, 
											unsigned char ucRxPara, 
											unsigned int uiTs, 
											unsigned int uiRspLength, 
											unsigned char* ucReqData, 
											unsigned int uiReqLen,
											unsigned char* ucRspData,
											unsigned int* uiRspLen);

int  DLL_EXT_FUNC FEISC_0xBC_CmdQueue(		int iReaderHnd, 
											int iMode,
											int iCmdCount,
											unsigned char* cCmdQueue,
											int iCmdQueueLen,
											FEISC_TASK_INIT* pInit );

int  DLL_EXT_FUNC FEISC_0xBD_ISOTranspCmd(	int iReaderHnd, 
											unsigned char cBusAdr, 
											int iMode, 
											int iRspLength, 
											unsigned char* cReqData, 
											int iReqLen, 
											unsigned char* cRspData, 
											int* iRspLen, 
											int iDataFormat );

int  DLL_EXT_FUNC FEISC_0xBE_ISOTranspCmd(	int iReaderHnd, 
											unsigned char cBusAdr, 
											int iMode, 
											int iRspLength, 
											unsigned char* cReqData, 
											int iReqLen, 
											unsigned char* cRspData, 
											int* iRspLen, 
											int iDataFormat );

int  DLL_EXT_FUNC FEISC_0xBF_ISOTranspCmd(	int iReaderHnd, 
											unsigned char cBusAdr, 
											int iMode, 
											int iRspLength, 
											unsigned char* cReqData, 
											int iReqLen, 
											unsigned char* cRspData, 
											int* iRspLen, 
											int iDataFormat );

int  DLL_EXT_FUNC FEISC_0xC0_SAMCmd(		int iReaderHnd, 
											int iSlot,
											unsigned char* cReqData, 
											int iReqLen, 
											FEISC_TASK_INIT* pInit );

int  DLL_EXT_FUNC FEISC_0xC0_SAMCmd_Sync(	int iReaderHnd,
											unsigned char ucBusAdr,
											int iSlot,
											int iTimeout,
											unsigned char* cReqData, 
											int iReqLen, 
											unsigned char* cRspData, 
											int* iRspLen );

int  DLL_EXT_FUNC FEISC_0xC1_DESFireCmd(	int iReaderHnd, 
											unsigned char cBusAdr, 
											unsigned char cSubCmd,
											unsigned char cMode,
											unsigned char* cAppID,
											unsigned char cReaderKeyIndex,
											unsigned char* cReqData,
											int iReqLen,
											unsigned char* cRspData,
											int* iRspLen, 
											int iDataFormat );

int  DLL_EXT_FUNC FEISC_0xC2_MifarePlusCmd(	int iReaderHnd, 
											unsigned char cBusAdr, 
											unsigned char cSubCmd,
											unsigned char cMode,
											unsigned char* cReqData,
											int iReqLen,
											unsigned char* cRspData,
											int* iRspLen, 
											int iDataFormat );


int  DLL_EXT_FUNC FEISC_0xC3_DESFireCmd(	int iReaderHnd, 
											unsigned char cBusAdr, 
											unsigned char cSubCmd,
											unsigned char cMode,
											unsigned char* cReqData,
											int iReqLen,
											unsigned char* cRspData,
											int* iRspLen, 
											int iDataFormat );

#undef DLL_EXT_FUNC

#ifdef __cplusplus
	}
#endif

#endif // _FEISC_INCLUDE_H


