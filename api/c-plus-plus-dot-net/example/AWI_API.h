//#############################################################################
//   VERSION 43
//#############################################################################

// The following ifdef block is the standard way of creating macros which make exporting 
// from a DLL simpler. All files within this DLL are compiled with the ACTIVEWAVELIB_EXPORTS
// symbol defined on the command line. this symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see 
// ACTIVEWAVELIB_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.

//#ifdef _DEBUG
//#ifndef _DBUG_ACTIVEWAVE
//#define _DEBUG_ACTIVEWAVE
//#endif
//#endif
//---------------------------------------------------------------------------
//   Common Data Type declaration
//---------------------------------------------------------------------------
//Comply with .NET Framework data type

typedef unsigned int UInt32;            // 4 bytes - 32 bits unsigned integer
typedef unsigned short UInt16;          // 2 bytes - 16 bits unsigned integer
typedef short Int16;                    // 2 bytes - 16 bits signed integer
typedef long Int32;                     // 4 bytes - 32 bits signed integer
typedef unsigned char Byte;             // 1 byte  -  8 bits unsigned integer
typedef char SByte;                     // 1 byte  -  8 bits signed integer
typedef float Single;                   // 4 bytes - 32 bits Singleing point number (precision 7 bits)
typedef bool Boolean;                   //
//---------------------------------------------------------------------------

//---------------------------------------------------------------------------

#ifndef ACTIVEWAVE_API_H
#define ACTIVEWAVE_API_H

#define MAX_TAG_SELECT    50
#define MAX_DATA         255
#define IP_MAX_DATA       20
#define MAX_TAG_WRITE     15

//  Structures
//---------------------------------------------------------------------------
typedef struct rfVersionInfoStruct{
   Byte dataCodeVer;
   Byte progCodeVer;
   Byte hostCodeVer;
} rfVersionInfo_t;

typedef struct rfSmartFGenStruct{
  UInt16  ID;         //SFGen ID
  UInt16  readerID;   //Assigned Reader ID
  Byte    txTime;     //Transmit Time sec
  Byte    waitTime;   //Wait Time
  Byte    wTimeType;  //Wait Time 0=sec, 1=min, 2=hour
  Byte    tagType;    //ALL_TAGS, ACCESS_TAG, INVENTORY_TAG, ASSET_TAG  
  UInt32  tagID;      
  Byte    fsValue;    //Field Strength
  Boolean longDistance;
  Boolean mDetectStatus;   //Motion Detector true=enable, false=disable
  Boolean mDetectActive;   //Motion Detector true=active-high  false=active-low
  Boolean longInterval;    //Long or short interval between each tag transmit
  Boolean assignRdr;       //Assign reader ID to the tag
}rfSmartFGen_t;

typedef struct rfTagSelectStruct{
  UInt32 selectType;
  Byte tagType;
  UInt32 tagList[MAX_TAG_SELECT];    
  UInt32 numTags;
  bool ledOn;
  bool speakerOn;
  Byte rangeIndex;
} rfTagSelect_t;

typedef struct rfTagStatusStruct{
  Boolean batteryLow;
  Boolean tamperSwitch;
  Boolean continuousField;
  Boolean bidirectional;
  Boolean batteryRechargeable;
  Boolean enabled;
  Byte type;
} rfTagStatus_t;

typedef struct rfTagTempStruct{
  Boolean rptUnderLowerLimit;       
  Boolean rptOverUpperLimit;         
  Boolean rptPeriodicRead;
  Boolean enableTempLogging;
  Boolean logging;
  UInt16 numReadAve; 
  Boolean wrapAround;
  UInt16 periodicRptTime; 
  UInt16 periodicTimeType;
  Byte status;
  Single lowerLimitTemp;
  Single upperLimitTemp;
  Single temperature;  
}rfTagTemp_t;

typedef struct rfTagLogTimeStamp_t {
   Byte year;
   Byte month;
   Byte day;
   Byte hour;
   Byte min;
   Byte sec;
} rfTagLogTimeStamp_t;

typedef enum rfSaniUnitType_e {
   NoUnit = 0,
   Door = 7,
   Faucet,
   Sanitization,
   Contamination,
   Bed
} rfSaniUnitType_e;

typedef enum rfSaniStatus_e {
   Violation = 0,
   ContaminatedOther,
   ContaminatedPatient,
   EngagingPatient,
   ContaminatedBathroom,
   Clean,
   AlcoholClean,
   SoapClean
} rfSaniStatus_e;

typedef struct rfTagSani_t {
   rfSaniUnitType_e UnitType;
   rfSaniStatus_e Status;
   Byte EventCnt;
} rfTagSani_t;

typedef struct rfTagStruct{
  UInt32 id;
  Byte tagType;
  Byte version;
  rfTagStatus_t status;
  rfTagTemp_t temp;
  Byte timeInField;
  Byte groupCount;
  Byte resendTime;
  UInt16 resendTimeType;
  Byte data[MAX_DATA];  
  UInt16 dataLen;
  UInt16 assignedReader;
  UInt16 selectType;
  rfTagSani_t sani;
} rfTag_t;

typedef struct rfNewTagConfigStruct{
   UInt32  newTagID;
   Byte  newTagType;
   Byte  configByte;
   Byte  timeInField;
   Byte  groupCount;
   Byte  resendTime;
   Int16 resendTimeType;
   Boolean reportTamper;
   Boolean reportTamperHistory;
   Boolean noTamperReport;
   Boolean noChangeTamper;
   Boolean factorySetting;
   UInt16 assignedReader;
   Int16 RSSI;
}rfNewTagConfig_t;

typedef struct rfTagEventStruct{
  UInt16 host;
  UInt16 reader;         // address of reader
  UInt16 repeater;
  UInt16 fGenerator;
  UInt16 eventType;      // type of event
  Int16  eventStatus;    // status of event
  Int16  errorStatus;    // status of error
  Int16  RSSI;           // field strength
  UInt16 pktID;
  UInt16 cmdType;        // type of command
  rfTag_t tag;          // tag that caused event
  Byte data[MAX_DATA];
  rfTagLogTimeStamp_t logTimestamp;
} rfTagEvent_t;

typedef struct rfReaderEventStruct{
  UInt16 host;
  UInt16 reader;
  UInt16 repeater;
  UInt16 relay;
  UInt16 fGenerator;
  UInt16 eventType;
  UInt16 cmdType;
  Int16  eventStatus;
  Int16  errorStatus;
  UInt16 pktID;
  Byte data[MAX_DATA];
  Byte ip[IP_MAX_DATA];
  UInt32 port;
  UInt16 cmdRef;
  rfVersionInfo_t versionInfo;
  //rfReaderInfo_t readerInfo;
  rfSmartFGen_t smartFgen;
} rfReaderEvent_t;

#ifdef __cplusplus
extern "C" {
#endif

//#ifndef ACTIVEWAVE_API_H
//#define ACTIVEWAVE_API_H

#ifndef ACTIVEWAVELIB_EXPORTS
#define ACTIVEWAVELIB_API __declspec (dllimport)
#else
#define ACTIVEWAVELIB_API __declspec (dllexport)
#endif

//---------------------------------------------------------------------------
//  General functions
//---------------------------------------------------------------------------


//---------------------------------------------------------------------------
//  Communication functions
//---------------------------------------------------------------------------
ACTIVEWAVELIB_API
Int32 __stdcall rfOpen(UInt32 baudRate, 
			           UInt32 port, 
			           HANDLE* hConn);

ACTIVEWAVELIB_API
Int32 __stdcall rfScanNetwork(UInt16 pktID);

ACTIVEWAVELIB_API
Int32 __stdcall rfScanIP(Byte ip[], UInt16 pktID);

ACTIVEWAVELIB_API
Int32 __stdcall rfClose(HANDLE* hConn);

ACTIVEWAVELIB_API
Int32 __stdcall rfOpenSocket(Byte IP[],  //Byte *IP,
							 UInt16 host,
							 Boolean  encrypt,
							 UInt16 cmdType,
                             UInt16 pktID); 
ACTIVEWAVELIB_API
Int32 __stdcall rfOpenSocketRdr(Byte IP[],  //Byte *IP,
							 UInt16 host,
							 Boolean  encrypt,
							 UInt16 cmdType,
							 UInt16 rdr,
                             UInt16 pktID); 

ACTIVEWAVELIB_API			                
Int32 __stdcall rfCloseSocket(Byte IP[],  //Byte *ip,
							  UInt16 cmdType);

ACTIVEWAVELIB_API
Int32 __stdcall rfChangeIPAddress(Byte oldIP[],
								  Byte newIP[]);


//---------------------------------------------------------------------------
//  Reader functions
//---------------------------------------------------------------------------

ACTIVEWAVELIB_API 
Int32 __stdcall rfResetReaderSocket(UInt16 host,
								    Byte* ip,
                                    UInt16 pktID);

ACTIVEWAVELIB_API
Int32 __stdcall rfResetReader(UInt16 host,
                              UInt16 reader,
                              UInt16 repeater,
                              UInt16 cmdType,
                              UInt16 pktID);
ACTIVEWAVELIB_API
Int32 __stdcall rfResetRS232Reader(UInt16 host,
                                   UInt16 reader,
                                   UInt16 repeater,
                                   UInt16 cmdType,
                                   UInt16 pktID);

ACTIVEWAVELIB_API
Int32 __stdcall rfPowerupReader(UInt16 host,
                                UInt16 reader,
                                UInt16 repeater,
                                UInt16 NewHost,
                                UInt16 NewReader,
                                UInt16 NewRepeater,
                                UInt16 config,
                                UInt16 type,
                                UInt16 dynamicRdr,
                                UInt16 cmdRef,
                                UInt16 pktID);

ACTIVEWAVELIB_API
Int32 __stdcall rfQueryReader(UInt16 host,
                              UInt16 reader,
                              UInt16 repeater,
                              UInt16 cmdType,
                              UInt16 pktID);

ACTIVEWAVELIB_API
Int32 __stdcall rfEnableReader(UInt16 host,
                               UInt16 reader,
                               UInt16 repeater,
                               Boolean enable,
                               UInt16 cmdType,
                               UInt16 pktID);

ACTIVEWAVELIB_API
Int32 __stdcall rfSetReaderFS (UInt16  host,
                               UInt16  reader,           
                               UInt16  repeater,  
                               UInt16  actionType,
                               Byte    fsValue,
							   Boolean range,
                               UInt16  cmdType,
                               UInt16  pktID);       

ACTIVEWAVELIB_API
Int32 __stdcall rfGetReaderFS (UInt16  host,
                               UInt16  reader,           
                               UInt16  repeater,  
                               UInt16  cmdType,
                               UInt16  pktID); 


ACTIVEWAVELIB_API
Int32 __stdcall rfEnableRelay(UInt16 host,
                              UInt16 reader,
                              UInt16 repeater,
                              UInt16  relay,
                              Boolean enable,
                              UInt16 cmdType,
                              UInt16 pktID);

ACTIVEWAVELIB_API
Int32 __stdcall rfConfigInputPort(UInt16 host,
                                  UInt16 reader,
                                  UInt16 repeater,
								  UInt16 input1config,
								  UInt16 input2config,
								  Boolean supervised,
                                  UInt16 cmdType,
                                  UInt16 pktID);

ACTIVEWAVELIB_API
Int32 __stdcall rfGetInputPortStatus(UInt16 host,
                                     UInt16 reader,
                                     UInt16 repeater,
                                     UInt16 cmdType,
                                     UInt16 pktID);

ACTIVEWAVELIB_API 
Int32 __stdcall rfConfigureReader(UInt16 host,
                                  UInt16 reader,
                                  UInt16 repeater,
                                  UInt16 cmdType,
								  UInt32 configType,
                                  UInt16 param1,
                                  UInt16 param2,
                                  UInt16 param3,
								  UInt16 param4,
                                  UInt16 pktID);


ACTIVEWAVELIB_API 
Int32 __stdcall rfGetReaderConfig(UInt16 host,                     
							      UInt16 reader,
                                  UInt16 repeater,
                                  UInt16 cmdType,
                                  UInt16  pktID);

ACTIVEWAVELIB_API
Int32 __stdcall rfGetReaderVersion(UInt16 host,
                                   UInt16 reader,
                                   UInt16 repeater,
                                   UInt16 cmdType,
                                   UInt16 pktID);

//---------------------------------------------------------------------------
//  Smart Field Generator functions
//---------------------------------------------------------------------------

ACTIVEWAVELIB_API
Int32 __stdcall rfResetSmartFGen(UInt16 host,
								 UInt16 reader,
								 UInt16 repeater,
								 UInt16 sFGen,
								 UInt16 cmdType,
								 Boolean broadcast,
								 UInt16 pktID);

ACTIVEWAVELIB_API
Int32 __stdcall rfQuerySmartFGen(UInt16 host,
					             UInt16 reader,
					             UInt16 repeater,
					             UInt16 sFGen,
					             UInt16 cmdType,
					             Boolean broadcast,
					             UInt16 bcastType,
					             UInt16 pktID);

ACTIVEWAVELIB_API
Int32 __stdcall rfSetConfigSmartFGen(UInt16 host,
					                 UInt16 reader,
					                 UInt16 repeater,
					                 UInt16 sFGen,
					                 UInt16 cmdType,
									 rfSmartFGen_t* smartFgenData,
					                 UInt16 pktID);

ACTIVEWAVELIB_API
Int32 __stdcall rfCallTagSmartFGen(UInt16 host,
					               UInt16 reader,
					               UInt16 repeater,
					               UInt16 sFGen,
					               UInt16 cmdType,
					               Boolean broadcast,
					               rfTagSelect_t* tagSelect,
								   Boolean setTxTime,
								   Boolean longInterval,
					               UInt16 pktID);

ACTIVEWAVELIB_API
Int32 __stdcall rfSetSmartFGenFS(UInt16 host,
								 UInt16 reader,
								 UInt16 repeater,
								 UInt16 sFGen,
								 UInt16 cmdType,
								 Boolean broadcast,
								 UInt16 actionType,
								 UInt16 absValue,
								 UInt16 pktID);

ACTIVEWAVELIB_API
Int32 __stdcall rfGetSmartFGenFS(UInt16 host,
								 UInt16 reader,
								 UInt16 repeater,
								 UInt16 sFGen,
								 UInt16 cmdType,
								 Boolean broadcast,
								 UInt16 pktID);
         
//---------------------------------------------------------------------------
//  Standard Field Generator functions
//---------------------------------------------------------------------------

ACTIVEWAVELIB_API
Int32 __stdcall rfQuerySTDFGen(UInt16 host,
							   UInt16 fgen,
							   UInt16 pktID);

ACTIVEWAVELIB_API
Int32 __stdcall rfConfigSTDFGen(UInt16 host,
								UInt16 fgen,
							    UInt16 configType,
								rfSmartFGen_t* fGenData,
								UInt16 pktID);

//---------------------------------------------------------------------------
//  Tag functions
//---------------------------------------------------------------------------

ACTIVEWAVELIB_API
Int32 __stdcall rfEnableTags(UInt16 host,
                             UInt16 reader,
                             UInt16 repeater,
                             rfTagSelect_t* tagSelect,
                             Boolean enable,
						  	 Boolean setTxTimeInterval,
							 Boolean timeInterval,
							 UInt16 cmdType,
                             UInt16 pktID);

ACTIVEWAVELIB_API
Int32 __stdcall rfQueryTags(UInt16 host,
                            UInt16 reader,
                            UInt16 repeater,
                            rfTagSelect_t* tagSelect,
						    Boolean setTxTimeInterval,
						    Boolean timeInterval,
						    UInt16 cmdType,
                            UInt16 pktID);

ACTIVEWAVELIB_API
Int32 __stdcall rfReadTags(UInt16 host,
                           UInt16 reader,
                           UInt16 repeater,
                           rfTagSelect_t* tagSelect,
                           UInt32 address,
                           UInt16 length,
						   Boolean setTxTimeInterval,
						   Boolean timeInterval,
						   UInt16 cmdType,
                           UInt16 pktID);

ACTIVEWAVELIB_API
Int32 __stdcall rfWriteTags(UInt16 host,
                            UInt16 reader,
                            UInt16 repeater,
                            rfTagSelect_t* tagSelect,
                            UInt16 address,
                            UInt16 length,
						    Byte data[MAX_TAG_WRITE],
						    Boolean setTxTimeInterval, 
						    Boolean timeInterval,
						    UInt16 cmdType,
                            UInt16 pktID);

ACTIVEWAVELIB_API 
Int32 __stdcall rfCallTags(UInt16 host,
                           UInt16 reader,
                           UInt16 repeater,
                           UInt16 fieldGen,
                           rfTagSelect_t* tagSelect,
						   Boolean setTxTimeInterval,
						   Boolean timeInterval,
				           UInt16 cmdType,
                           UInt16 pktID);


ACTIVEWAVELIB_API 
Int32 __stdcall rfConfigureTags(UInt16 host,
                                UInt16 reader,
                                UInt16 repeater,
                                rfTagSelect_t* tagSelect,
                                rfNewTagConfig_t* newTagCfg,
							    UInt16 configType,
							    Boolean setTxTimeInterval,
							    Boolean timeInterval,
                                UInt16 cmdType,
                                UInt16 pktID);

ACTIVEWAVELIB_API
Int32 __stdcall rfGetTagTempConfig(UInt16 host,
                                   UInt16 reader,
                                   UInt16 repeater,
                                   rfTagSelect_t* tagSelect,
								   Boolean setTxTimeInterval, 
								   Boolean timeInterval,
						           UInt16 cmdType,
                                   UInt16 pktID);

ACTIVEWAVELIB_API
Int32 __stdcall rfSetTagTempConfig(UInt16 host,
                                   UInt16 reader,
                                   UInt16 repeater,
                                   rfTagSelect_t* tagSelect,
								   rfTagTemp_t* tagTemp,
								   Boolean setTxTimeInterval,
								   Boolean timeInterval,
						           UInt16 cmdType,
                                   UInt16 pktID);

ACTIVEWAVELIB_API
Int32 __stdcall rfGetTagTemp(UInt16 host,
                             UInt16 reader,
                             UInt16 repeater,
                             rfTagSelect_t* tagSelect,
							 Boolean setTxTimeInterval,
							 Boolean timeInterval,
						     UInt16 cmdType,
                             UInt16 pktID);

ACTIVEWAVELIB_API
Int32 __stdcall rfSetTagTempLogTimestamp(UInt16 host,
                                         UInt16 reader,
                                         UInt16 repeater,
                                         rfTagSelect_t* tagSelect,
							             Boolean setTxTimeInterval,
							             Boolean timeInterval,
						                 UInt16 cmdType,
                                         UInt16 pktID);

ACTIVEWAVELIB_API
Int32 __stdcall rfGetTagTempLogTimestamp(UInt16 host,
                                         UInt16 reader,
                                         UInt16 repeater,
                                         rfTagSelect_t* tagSelect,
							             Boolean setTxTimeInterval,
							             Boolean timeInterval,
						                 UInt16 cmdType,
                                         UInt16 pktID);

ACTIVEWAVELIB_API
Single __stdcall rfGetTagTempCalib();

ACTIVEWAVELIB_API
Int32 __stdcall rfSetTagTempCalib(Single calib);

ACTIVEWAVELIB_API 
Int32 __stdcall rfGetTagLEDConfig(UInt16 host,
                                  UInt16 reader,
                                  UInt16 repeater,
                                  rfTagSelect_t* tagSelect,
						          Boolean timeInterval,
				                  UInt16 cmdType,
                                  UInt16 pktID);

ACTIVEWAVELIB_API 
Int32 __stdcall rfSetTagLEDConfig(UInt16 host,
                                  UInt16 reader,
                                  UInt16 repeater,
                                  rfTagSelect_t* tagSelect,
								  UInt16 led,
						          Boolean timeInterval,
				                  UInt16 cmdType,
                                  UInt16 pktID);
ACTIVEWAVELIB_API 
Int32 __stdcall rfSetTagSpeakerConfig(UInt16 host,
                                      UInt16 reader,
                                      UInt16 repeater,
                                      rfTagSelect_t* tagSelect,
									  UInt16 speaker,
						              Boolean timeInterval,
				                      UInt16 cmdType,
                                      UInt16 pktID);

ACTIVEWAVELIB_API
Int32 __stdcall rfAssignTagReader(UInt16 host,
                                  UInt16 reader,
                                  UInt16 repeater,
					              rfTagSelect_t* tagSelect,
                                  rfNewTagConfig_t* tagInfo,
                                  Boolean setTxTimeInterval,
					              Boolean timeInterval,
					              UInt16 cmdType,
                                  UInt16 pktID);

//---------------------------------------------------------------------------
//  Callback functions
//---------------------------------------------------------------------------

typedef Int32 (*rfReaderEvent)(Int32 status,
                               HANDLE funcId,
                               rfReaderEvent_t* readerEvent,  
                               void* userArg);

typedef Int32 (*rfTagEvent)(Int32 status,
                            HANDLE funcId,
                            rfTagEvent_t* tagEvent,  
                            void* userArg);


ACTIVEWAVELIB_API
void __stdcall rfRegisterReaderEvent(rfReaderEvent fnCallback, void* userArg);

ACTIVEWAVELIB_API
void __stdcall rfRegisterTagEvent(rfTagEvent fnCallback, void* userArg);

//---------------------------------------------------------------------------
//  Debug functions
//---------------------------------------------------------------------------
#ifdef _DEBUG

   typedef struct rfDebugEventStruct {
      UInt16 eventType;
      SByte recv[300];
      SByte send[300];
      UInt16 recvLen;
      UInt16 sendLen;
	  SByte ip[20];
   }rfDebugEvent_t;

   typedef Int32 (*rfDebugEventFn)(rfDebugEvent_t* debugEvent);

   ACTIVEWAVELIB_API
   bool __stdcall rfRegisterDebugEvent(rfDebugEventFn fnCallback);
    
#endif

//---------------------------------------------------------------------------

#ifdef __cplusplus
}
#endif

//---------------------------------------------------------------------------
//  Return codes
//---------------------------------------------------------------------------
//Event Status
//---------------------------------------------
#define RF_S_DONE		                   0
#define RF_S_OK			                   1
#define RF_S_PEND	      	               2
#define RF_S_OK_PEND	                   3
#define RF_S_ERROR                         4
#define RF_S_TIMEOUT                       5


//Error Status
//---------------------------------------------
#define RF_E_NO_ERROR 	                    0
#define RF_E_UNSPECIFIED  	               -1
#define RF_E_ERROR                         -2
#define RF_E_PACKET                        -100
#define RF_E_NOT_IMPLEMENTED	           -101
#define RF_E_NOT_SUPPORTED	               -102
#define RF_E_COMMAND		               -103
#define RF_E_ARGUMENT	   	               -104
#define RF_E_CRC	       	               -105
#define RF_E_TIMEOUT	                   -106
#define RF_E_CREATE_FILE                   -107
#define RF_E_CREATE_EVENT                  -108
#define RF_E_SET_UP_COMM                   -109
#define RF_E_SET_COM_MASK                  -110
#define RF_E_CREATE_THREAD                 -111
#define RF_E_TERMINATE_THREAD              -112
#define RF_E_SET_PIORITY                   -113
#define RF_E_SET_COM_STATE                 -114
#define RF_E_SET_COM_TIMEOUT               -115
#define RF_E_TX                            -116
#define RF_E_ALLOCATE_MEM                  -117
#define RF_E_QUE_FULL                      -118
#define RF_E_RX_QUE_FULL                   -119
#define RF_E_PENDING_PKTID                 -120
#define RF_E_NO_RESPONSE                   -121
#define RF_E_CHECKSUM                      -122
#define RF_E_READ_MEMORY                   -123
#define RF_E_WRITE_MEMORY                  -124
#define RF_E_RESPOND_ERROR_STATUS          -125
#define RF_E_BUILD_PACKET                  -126
#define RF_E_MAX_READ_MEM_LEN              -127
#define RF_E_MAX_WRITE_MEM_LEN             -128
#define RF_E_USER_RAM_MEM_ADDR             -129
#define RF_E_USER_MEM_BOUNDRY              -130
#define RF_E_READER_BUSY                   -131
#define RF_E_PARAM_OUT_OF_RANGE            -132
#define RF_E_PKT_ID                        -133
#define RF_E_CLOSE_PORT                    -135
#define RF_E_GET_COM_STATE                 -139
#define RF_E_PORT_ALREADY_OPEN             -140
#define RF_E_GET_COM_TIMEOUT               -149

#define RF_E_WINSOCKET_VER_NOT_SUPPORTED   -150
#define RF_E_CREATE_SOCKET                 -151
#define RF_E_INVALID_IP_ADDRESS            -152
#define RF_E_INVALID_HOST_NAME             -153
#define RF_E_BINDING_SOCKET                -154
#define RF_E_CLOSING_SOCKET                -155
#define RF_E_CONNECT                       -156
#define RF_E_GET_INTERFACE_LIST            -157
#define RF_E_WSASTARTUP                    -158
#define RF_E_OPEN_SOCKET                   -159
#define RF_E_IP_NOT_FOUND                  -160
#define RF_E_LISTEN                        -161
#define RF_E_LOADING_LIB                   -162
#define RF_E_WRITE_IV                      -163
#define RF_E_SOCKET_EXCEPTION              -164
#define RF_E_READING_SOCKET                -165
#define RF_E_BLOCKENCRYPT_NOT_FOUND        -166
#define RF_E_SOCKET_NOT_ACTIVE             -167
#define RF_E_READER_NOT_POWERED_UP         -168
#define RF_E_SOCKET_WRITE                  -169
#define RF_E_SOCKET_READ                   -170
#define RF_E_CHANGE_IP                     -171
#define RF_E_INVALID_OEMID                 -172
#define RF_E_IP_NOT_SCANNED                -173
#define RF_E_TEMP_LIMIT                    -174
#define RF_E_SOCKET_ALREADY_CONNECTED      -175
#define RF_E_NO_OPEN_SOCKET                -176
#define RF_E_SOCKET_CONNECTION_PENDING     -177
#define RF_E_MAX_SOCKET_OPEN_SLOT          -178
#define RF_E_MAX_FS                        -182
#define RF_E_COMM_TYPE                     -183
#define RF_E_DUPLICATED_READER_ID          -184
#define RF_E_READER_ID_NOT_FOUND           -185

#define RF_E_SYNC_CONNECTION_TIMEOUT	   -189
#define RF_E_INVALID_TAG_ID                -201


//Reader cmd type
//-------------------------------------------- 
#define SPECIFIC_READER                    550
#define SPECIFIC_REPEATER                  551
#define ALL_READERS                        552
#define ALL_REPEATERS                      553
#define SPECIFIC_IP                        554
#define ALL_IPS                            555
#define SPECIFIC_SFGEN                     556
#define ALL_SFGEN                          557
#define ALL_RS232_READERS                  558   //rs232 and network both on
#define CFG_READER_INIT                    560
#define CFG_REPEATER_INIT                  561
#define CFG_READER_NEW                     562
#define CFG_REPEATER_NEW                   563
#define CFG_REPEATER_BACKWARD_FORWARD      564
#define CFG_REPEATER_BACKWARD              565
#define CFG_REPEATER_FORWARD               566

//event type
//--------------------------------------------
#define RF_READER_POWERUP                  100
#define RF_STD_FGEN_POWERUP                101
#define RF_SMART_FGEN_POWERUP              102
#define RF_READER_ENABLE                   104
#define RF_READER_DISABLE                  105
#define RF_READER_RESET_ALL                106
#define RF_REPEATER_RESET_ALL              107
#define RF_READER_RESET                    108
#define RF_REPEATER_RESET                  109
#define RF_READER_CONFIG                   110
#define RF_GET_READER_VERSION              111
#define RF_READER_PING                     112
#define RF_POWER_UP                        113
#define RF_RELAY_ENABLE                    114
#define RF_RELAY_DISABLE                   115
#define RF_REPEATER_ENABLE                 116
#define RF_REPEATER_ENABLE_ALL             117
#define RF_READER_ENABLE_ALL               118
#define RF_REPEATER_DISABLE_ALL            119
#define RF_REPEATER_DISABLE                120
#define RF_READER_DISABLE_ALL              121
#define RF_RELAY_ENABLE_ALL                122
#define RF_RELAY_DISABLE_ALL               123
#define RF_READER_PING_ALL                 124
#define RF_REPEATER_PING_ALL               125
#define RF_REPEATER_PING                   126
#define RF_READER_GET_VERSION_ALL          127
#define RF_REPEATER_GET_VERSION_ALL        128
#define RF_READER_GET_VERSION              129
#define RF_REPEATER_GET_VERSION            130
#define RF_TAG_DETECTED                    131
#define RF_TAG_DETECTED_RSSI               132
#define RF_SELECT_FIELD                    133
#define RF_SELECT_TAG_TYPE                 134
#define RF_SELECT_TAG_ID                   135
#define RF_SELECT_TAG_LIST                 136
#define RF_SELECT_TAG_RANGE                137
//#define RF_SELECT_FACTORY                  138
//#define RF_SELECT_FIELDFACTORY             139
#define RF_TAG_CONFIGURE                   140
#define RF_TAG_ENABLE                      141
#define RF_TAG_DISABLE                     142
#define RF_TAG_QUERY                       143
#define RF_TAG_CALL                        144
#define RF_TAG_READ                        145
#define RF_TAG_WRITE                       146
//#define RF_FGENERATOR_CONFIG               147
#define RF_CONFIG_STD_FGEN                 147
//#define RF_FGENERATOR_QUERY                148
#define RF_QUERY_STD_FGEN                  148
#define RF_TAG_ASSIGN_READER               149
#define RF_INVALID_PACKET                  150
#define RF_GET_INPUT_STATUS                151
#define RF_GET_INPUT_STATUS_ALL            152
#define RF_GET_TAG_TEMP_CONFIG             153
#define RF_SET_TAG_TEMP_CONFIG             154
#define RF_GET_TAG_TEMP                    155
#define RF_TAG_TEMP                        156
#define RF_SET_TAG_TEMP_LOG_TIMESTAMP      157
#define RF_GET_TAG_TEMP_LOG_TIMESTAMP      158
#define RF_TAG_LOW_BATTERY                 159
//#define RF_STANDARD_READER                 160
//#define RF_ENABLE_AT_POWERUP               161
//#define RF_RESPOND_TO_BROADCAST            162
#define RF_SCAN_NETWORK                    163
#define RF_OPEN_SOCKET                     164
#define RF_SCANN_BUSY                      165
#define RF_SCAN_IP                         166
#define RF_CLOSE_SOCKET                    167
#define RF_READER_QUERY_ALL				   168
//#define RF_SEND_RSSI					   169
#define RF_READER_CONFIG_TX_WT			   170
#define RF_READER_CONFIG_TX_WT_ALL		   171
#define RF_SELECT_TAG_ID_ANY_TYPE		   172 
#define RF_END_OF_BROADCAST                190 //0xBE
#define RF_SET_RDR_FS                      195
#define RF_SET_RDR_FS_ALL                  196
#define RF_GET_RDR_FS                      197
#define RF_GET_RDR_FS_ALL                  198
#define RF_RESET_SMART_FGEN_ALL            199
#define RF_RESET_SMART_FGEN                200
#define RF_QUERY_SMART_FGEN                201
#define RF_QUERY_SMART_FGEN_ALL            202
#define RF_CALL_TAG_SMART_FGEN             203
#define RF_SET_FS_SMART_FGEN               204
#define RF_SET_FS_SMART_FGEN_ALL           205
#define RF_GET_FS_SMART_FGEN               206
#define RF_GET_FS_SMART_FGEN_ALL           207
#define RF_SET_CONFIG_SMART_FGEN           208
#define RF_SET_CONFIG_SMART_FGEN_ALL       209
#define RF_READER_CONFIG_ALL               215
#define RF_GET_READER_CONFIG               216
#define RF_GET_READER_CONFIG_ALL           217
#define RF_GET_TAG_LED_CONFIG              218 
#define RF_GET_TAG_LED_CONFIG_ALL          219
#define RF_GET_TAG_SPEAKER_CONFIG          220
#define RF_SET_TAG_LED_CONFIG              221
#define RF_SET_TAG_SPEAKER_CONFIG          222
#define RF_CONFIG_INPUT_PORT               223
#define RF_CONFIG_INPUT_PORT_ALL           224
#define RF_TAG_DETECTED_TAMPERED           225
#define RF_TAG_DETECTED_RSSI_TAMPERED      226
#define RF_TAG_DETECTED_SANI               227
    
//tag configuration parameters
#define RF_CFG_TAG_ID                      1//--->
#define RF_CFG_TAG_TYPE                    2
#define RF_CFG_TAG_TIF_GC                  4
#define RF_CFG_TAG_RESEND_TIME            16
#define RF_CFG_TAG_REPORT_TAMPER          32
#define RF_CFG_TAG_REPORT_TAMPER_HISTORY  64
#define RF_CFG_TAG_REPORT_NO_TAMPER      128
#define RF_CFG_TAG_FACTORY_SETTING       256

// Log file errors
#define RF_E_LOG_NULL      	               -900  // No filename has been specified
#define RF_E_LOG_OPEN  	    	           -901  // Error opening file for write access
#define RF_E_LOG_WRITE      	           -902  // Error writing to file

//Tag ID
#define ANY_TAG_ID                        0

//Tag Type
#define ALL_TAGS                           0x00
#define ACCESS_TAG                         0x01
#define INVENTORY_TAG                      0x02
#define ASSET_TAG                          0x03
#define CAR_TAG                            0x04
#define FACTORY_TAG                        0x07

//Input Config Type
#define IGNOR_INPUT_CHANGE                 0x01
#define REPORT_INPUT_CHANGE                0x02
#define NO_CHANGE_INPUT                    0x03

//Input Status Type
#define NORMAL_CLOSED                      0x00
#define NORMAL_OPEN                        0x01
#define FAULTY_CLOSED                      0x02
#define FAULTY_OPEN                        0x03

//Smart FGen Field Strength
#define RF_INC_FS      0x01
#define RF_DEC_FS      0x02
#define RF_ABS_FS      0x03

//#define RF_E_NO_RESPONSE                 200
#define ENCRYPT_DATA                         2
#define NO_ENCRYPTION                        0

//Tag time params
#define RF_TIME_HOUR                        80
#define RF_TIME_MINUTE                      81
#define RF_TIME_SECOND                      82
#define RF_TIME_NEVER                       83
#define RF_TIME_FOREVER                     84

//Tag Temperature params
#define RF_TAG_TEMP_NORM                    87
#define RF_TAG_TEMP_HIGH                    88
#define RF_TAG_TEMP_LOW                     89


//Config reader options
#define RF_NO_CHANGE                         0
#define RF_RESPOND_TO_BROADCAST              2
#define RF_ENABLE_AT_POWERUP                 4
#define RF_SEND_RSSI                         8
#define RF_DO_NOT_SEND_RSSI                 16

//-------------- [TODO] -------------------------
  //RF_NO_RESPOND_TO_BROADCAST(1)
  //RF_DISABLE_AT_POWERUP(4)
  //RF_ENABLE_AT_POWERUP(8)
  //RF_SEND_RSSI(16)
  //RF_DO_NOT_SEND_RSSI(32)
//-----------------------------------------------

//Reader config params
#define CFG_HOST_ID                          1
#define CFG_READER_ID                        2
#define CFG_REPEATER_ID                      4
#define CFG_READER_TYPE                      8
#define CFG_READER_OPTIONS                  16
#define CFG_TX_TIME                         32
#define CFG_WT_TIME                         64


// Tag status bit mask	
#define TS_BatteryLow					  0x00000001
#define TS_TamperSwitch				      0x00000002
#define TS_ContinousField				  0x00000004
#define uint TS_Bidirectional			  0x00000008
#define TS_BatteryRechargeable			  0x00000010
#define TS_Enabled						  0x00000020
#define TS_Type							  0x000001C0
#define TS_Reserved						  0xFFFFFE00

//Motion Detector
#define RF_MD_ACTIVE_HIGH                    1
#define RF_MD_ACTIVE_LOW                     2
#define RF_MD_ENABLE                         4
#define RF_MD_DISABLE                        8



//Reader Type
#define RF_RDR_PROG_STATION                 1
#define RF_RDR_STANDARD                     2
#define RF_RDR_ACCESS_CTRL                  3
#define RF_RDR_SMALL_RF                     4
#define RF_RDR_PDA                          5
#define RF_FGEN_READER                      6

//ID type
#define RF_VER_DATA                        500
#define RF_VER_PRG                         501
#define RF_VER_HOST                        502

//Action Type
#define RF_ABSOLUTE           1
#define RF_RANGE              2
#define RF_INCREMENT          3
#define RF_DECREMENT          4

//Smart FGen Response
#define RESPOND_SPEC_RDR      1
#define RESPOND_ANY_RDR       2

//Smart FGen & STD FGen Config Params
#define ALL_PARAMS                0
#define ASSIGNED_RDR_ID           2
#define FGEN_ID                   4
#define TAG_TYPE                  8
#define TAG_ID                   16
#define TX_TIME                  32
#define WAIT_TIME                64
#define FIELD_STRENGTH          128
#define FS_RANGE                265
#define MOTION_DETECT_STATUS    512
#define MOTION_DETECT_ACTIVE   1024


//Network Communication Port
#define NET_PORT_NUM                     10001  //Lantronix port number

#endif

#ifdef _DEBUG
   #define RF_DEBUG_RECV             1000
   #define RF_DEBUG_SEND             1001
#endif
