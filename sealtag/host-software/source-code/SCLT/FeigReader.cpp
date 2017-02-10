#include "stdafx.h"
#include "FeigReader.h"
#include "feusb.h"
#include "feisc.h"

unsigned char sRspData[32];
int iRspLen, iDevHnd, iFeiscHnd;

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

FeigReader::FeigReader(void)
{
	iRspLen = 0;
	iDevHnd = 0;
	iFeiscHnd = 0;
}

FeigReader::~FeigReader(void)
{
}

int FeigReader::connect(){
	char sDeviceHandle[32];
	char sDeviceName[32];
	char sDeviceSerialNumber[32];
	char sDeviceFamilyName[32];
	char sDevicePresence[2];
	DWORD dwDeviceSerialNumber = 0;
	FEUSB_ClearScanList();/* check USB device connected */
	int iErr;
	int timeout = 10;
	while (FEUSB_Scan(FEUSB_SCAN_ALL,NULL) < 0)
	{
		timeout--;
		if(timeout == 0){
			return -2;
		}
	}
	int nDev = FEUSB_GetScanListSize();

	if(nDev > 0){
		/* GetScanListPara : Get Detected FEIG USB device parameters */
		FEUSB_GetScanListPara( 0, "DeviceName", sDeviceName ) ;
		FEUSB_GetScanListPara( 0, "Device-ID", sDeviceSerialNumber ) ;
		FEUSB_GetScanListPara( 0, "DeviceHnd", sDeviceHandle ) ;
		FEUSB_GetScanListPara( 0, "FamilyName", sDeviceFamilyName ) ;
		FEUSB_GetScanListPara( 0, "Present", sDevicePresence ) ;
		/* convert receive data in hexa format */
		sscanf_s((const char*)sDeviceSerialNumber, "%lx",&dwDeviceSerialNumber);
		/* Open Communciation with one FEIG USB device */
		/* and get USB hanle for FEUSB functions */
		iDevHnd = FEUSB_OpenDevice(dwDeviceSerialNumber);
		if (iDevHnd < 0)
		{
			/* Error : USB connection problem */
			/* Close USB connection */
			iErr = FEUSB_CloseDevice(iDevHnd);
			return iErr;
		}
		/* Link FEIG USB device with FEISC functions */
		/* and get USB hanle */
		iFeiscHnd = FEISC_NewReader( iDevHnd );
		if (iFeiscHnd < 0)
		{
			/* Error : USB connection problem */
			/* Close USB connection */
			iErr = FEISC_DeleteReader( iFeiscHnd );
			return iErr;
		}
		else
		{
			/* USB connection OK */
			int ret = FEISC_0x69_RFReset(iFeiscHnd, 0xFF);
			//wprintf(_T("RFReset Return: %d"),ret);
			return 0;
		}
	} else {
		return -1;
	}
}

int FeigReader::disconnect(){
	if (iDevHnd > 0)
		{
			/* Close USB connection */
			FEUSB_CloseDevice(iDevHnd);
			/* Unlink reader */
			FEISC_DeleteReader(iFeiscHnd);
			return 0;
		}
	return -1;
}

int FeigReader::write(unsigned char memoryBank[2], unsigned char commandStatus, unsigned char command, unsigned char data[2]){
	int iRspLength=56;
	int iReqLen,iResult;
	unsigned char sReqData[32]={0};
	
	sReqData[0] = (UCHAR)0x02; //Reader parameter
	sReqData[1] = (UCHAR)0x1F; //Reader parameter
	sReqData[2] = (UCHAR)0x0A; //Flag
	sReqData[3] = (UCHAR)0x21; //RF ISO 15693 Write single block command
	sReqData[4] = memoryBank[1];
	sReqData[5] = memoryBank[0];
	sReqData[6] = command;
	sReqData[7] = commandStatus;
	sReqData[8] = data[0];
	sReqData[9] = data[1];
	iReqLen = 10; // (number of characters :param=2 in request)
	//Last byte is first

	/* FEIG USB request in transparent mode */
	iResult = FEISC_0xBF_ISOTranspCmd (iFeiscHnd, 0xFF,// USB parameters
									   2,// MODE 2 : write like answer
									   iRspLength,
									   sReqData,// request
									   iReqLen,// USB request length
									   &sRspData[0],// answer
									   &iRspLen,
									   2 );// length format 2 : Number of Bytes
	//RF REQUEST RESULT
	return iResult;
}

int FeigReader::read(unsigned char memoryBank[2]){
	int iRspLength=56;
	int iReqLen,iResult;
	unsigned char sReqData[32]={0};

	sReqData[0] = (UCHAR)0x02; //Reader parameter
	sReqData[1] = (UCHAR)0x1F; //Reader parameter
	sReqData[2] = (UCHAR)0x0A; //Flag
	sReqData[3] = (UCHAR)0x20; //RF ISO 15693 Read single block command
	sReqData[4] = memoryBank[1];
	sReqData[5] = memoryBank[0];
	//Last byte is first
	iReqLen = 6; //(number of characters :param=2 in request)

	/* FEIG USB request in transparent mode */
	iResult = FEISC_0xBF_ISOTranspCmd (iFeiscHnd, //USB Handle 
									   0xFF, //Communication address
									   1, //MODE 1 : read answer 
									   iRspLength,
									   sReqData,//request 
									   iReqLen,//USB request length
									   &sRspData[0],//answer
									   &iRspLen,
									   2); //length format 2 : Number of Bytes

	return iResult;
}

int FeigReader::readUid(void){
	this->resetToReady();
	UCHAR sReqData[32] = {0};
	int iReqLen = 2;
	int iResult = -1;

	/* Inventory request HOST MODE command : B0 + 0100 */
	sReqData[0] = (UCHAR)0x01;
	sReqData[1] = (UCHAR)0x00;

	for (int i=0; i<iReqLen; i++){
		//printf("%.2x",sReqData[i]);
	}
	/* FEIG USB INVENTORY request in HOST MODE command */
	iResult = FEISC_0xB0_ISOCmd(iFeiscHnd,//USB Handle
								0xFF,//Communication address
								sReqData,/* request */
								iReqLen,//USB Request Length
								&sRspData[0], /* answer */
								&iRspLen,/* Answer Length*/
								2);/* length format 2 : Number of Bytes */

	if (iRspLen == 0) {
		//OutputDebugString(_T("No tag answer received"));
	}
	else{
		for (int i=0; i<iRspLen; i++){
			//printf("%c",sRspData[i]);
		}
	}

	/* RF INVENTORY REQUEST RESULT */
	/* if(iResult == 0) PASS else FAIL */
	/* if (iRspLen == 0) No transponder answer */
	/* else sRspData contains the transponder(s) answer(s) */
	if (iResult != 0)
	{
		/* No Tag detected in the Antenna Field */
	}
	else
	{
		/* 1 or more transponders are in Antenna Field */
	}
	/* after INVENTORY request, all transponders are in QUIET mode */
	//this->resetToReady();
	return iResult;
}

int FeigReader::resetToReady(void){
	UCHAR sReqData[32]={0};
	int iReqLen;
	int iResult;

	/* RESET TO READY request is sent to Wake Up transponders */
	/* RESET TO READY [0xB0] request */
	sReqData[0] = (UCHAR)0x26;
	sReqData[1] = (UCHAR)0x00;
	iReqLen = 2; /* (number of bytes :param=2 in request) */
	iResult = FEISC_0xB0_ISOCmd(iFeiscHnd,
								 0xFF,
								 sReqData,
								 iReqLen, /* request */
								 &sRspData[0],
								 &iRspLen,/* answer */
								 2);/* length format 2 */
	if (iResult != 0)
	{
		/* Reset to ready request problem */
	}
	else
	{
		/* Reset to ready request OK */
	}
	return iResult;
}

unsigned char *FeigReader::getRsp(void){
	return sRspData;
}

int FeigReader::getRspLen(void){
	return iRspLen;
}