// SCLT.cpp : Implementation of WinMain

#include "stdafx.h"
#include "resource.h"
#include "SCLT_i.h"
#include <afx.h>
#include "Commands.h"
#include <iostream>
#include "guicon.h"

class CSCLTModule : public CAtlExeModuleT< CSCLTModule >
{
public :
	DECLARE_LIBID(LIBID_SCLTLib)
	DECLARE_REGISTRY_APPID_RESOURCEID(IDR_SCLT, "{00926068-A94F-457D-917E-BB2075DD56CA}")
};

CSCLTModule _AtlModule;



//
extern "C" int WINAPI _tWinMain(HINSTANCE /*hInstance*/, HINSTANCE /*hPrevInstance*/, 
                                LPTSTR lpCmdLine, int nShowCmd)
{
	//freopen( "CON", "w", stdout );
	//freopen( "CON", "w", stderr );
	//RedirectIOToConsole();
	
	CString out;
	Commands cmd;

	if(wcsstr(lpCmdLine,_T("StartTimer")) != NULL) {
		out = cmd.StartTimer();
		//CT2CA pszConvertedAnsiString (out);
		//std::string str(pszConvertedAnsiString);
		//std::cout << str.c_str() << std::endl;
		OutputDebugString(out);
		wprintf(_T("%s"), out);
	} else if(wcsstr(lpCmdLine,_T("StopTimer")) != NULL) {
		out = cmd.StopTimer();
		OutputDebugString(out);
		wprintf(_T("%s"), out);
	} else if(wcsstr(lpCmdLine,_T("ReadHistory")) != NULL) {
		out = cmd.ReadHistory();
		OutputDebugString(out);
		wprintf(_T("%s"), out);
	} else if(wcsstr(lpCmdLine,_T("ReadHFID")) != NULL) {
		out = cmd.ReadHFID();
		OutputDebugString(out);
		wprintf(_T("%s"), out);
	} else if(wcsstr(lpCmdLine,_T("ReadStatus")) != NULL) {
		out = cmd.ReadStatus();
		OutputDebugString(out);
		wprintf(_T("%s"), out);
	} else if(wcsstr(lpCmdLine,_T("ResetToFactory")) != NULL) {
		out = cmd.ResetToFactory();
		OutputDebugString(out);
		wprintf(_T("%s"), out);
	} else if(wcsstr(lpCmdLine,_T("Flush")) != NULL) {
		out = cmd.Flush();
		OutputDebugString(out);
		wprintf(_T("%s"), out);
	} else if(wcsstr(lpCmdLine,_T("StoreUHFID")) != NULL) {
		if(wcsstr(lpCmdLine,_T(" ")) != NULL){
			wchar_t param1[24];
			for(int i = 0 ; i < 24 ; i++)
			{
				param1[i] = lpCmdLine[11 + i];
			}
			out = cmd.StoreUHFID((CString)param1);
			OutputDebugString(out);
			//out.Format(_T("Parameters:  %s\n"),lpCmdLine);
			wprintf(_T("%s"), out);
		}
	} else if(wcsstr(lpCmdLine,_T("ReadUHFID")) != NULL) {
		out = cmd.ReadUHFID();
		OutputDebugString(out);
		wprintf(_T("%s"), out);
	} else if(wcsstr(lpCmdLine,_T("ReadMemoryAt")) != NULL) {
		if(wcsstr(lpCmdLine,_T(" ")) != NULL){
			wchar_t param1[4];
			for(int i = 0 ; i < 4 ; i++)
			{
				param1[i] = lpCmdLine[13 + i];
			}
			out = cmd.ReadMemoryBank((CString)param1);
			OutputDebugString(out);
			//out.Format(_T("Parameters:  %s\n"),lpCmdLine);
			wprintf(_T("%s"), out);
		}
	} else if(wcsstr(lpCmdLine,_T("WriteMemoryAt")) != NULL) {
		if(wcsstr(lpCmdLine,_T(" ")) != NULL){
			wchar_t param1[12];
			for(int i = 0 ; i < 12 ; i++)
			{
				param1[i] = lpCmdLine[14 + i];
			}
			out = cmd.WriteMemoryBank((CString)param1);
			OutputDebugString(out);
			//out.Format(_T("Parameters:  %s\n"),lpCmdLine);
			wprintf(_T("%s"), out);
		}
	} else if(wcsstr(lpCmdLine,_T("GetTagVersion")) != NULL) {
		out = cmd.GetTagVersion();
		OutputDebugString(out);
		wprintf(_T("%s"), out);
	} else {
		out.Format(_T("Parameters:  %s\n"),lpCmdLine);
		OutputDebugString(out);
		wprintf(out);
	}


	return 0;
    //return _AtlModule.WinMain(nShowCmd);
}