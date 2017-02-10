//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop
USEFORM("ProgStationUnit.cpp", ProgStationForm);
USEFORM("ComConfigPage.cpp", CommConfigDlg);
USEFORM("DiagnosticUnit.cpp", DiagnosticForm);
USEFORM("AboutUnit.cpp", AboutForm);
USEFORM("ReaderIDDefUnit.cpp", ReaderIDDefForm);
USEFORM("FileSysMsgUnit.cpp", FileSysMsgForm);
USEFORM("ConfigProgStationUnit.cpp", ConfigProgStationForm);
USEFORM("ScanUnit.cpp", ScanForm);
USEFORM("TCPIPUnit.cpp", TCPIPForm);
USEFORM("NetworkUnit.cpp", NetworkForm);
USEFORM("AWSocketUnit.cpp", TAWSocket);
USEFORM("AboutUnitNoLogo.cpp", AboutFormNoLogo);
USEFORM("ReadLargeDataUnit.cpp", TagReadLargDataForm);
//---------------------------------------------------------------------------
WINAPI WinMain(HINSTANCE, HINSTANCE, LPSTR, int)
{
        HANDLE mutex;
        try
        {
#if 0
                 const char *mutexname = "ProgrammingStation";
                 mutex = mutex = CreateMutex(NULL, true, mutexname);
                 int r = GetLastError();
                 if ( r != 0 )
                 {
                     ::MessageBoxEx(::GetDesktopWindow(), ( LPCSTR )"Programming Station Already Running",
                     ( LPCSTR )"Programming Station Information Dialog",
                      MB_OK | MB_ICONSTOP | MB_TOPMOST  , LANG_ENGLISH );

                     ReleaseMutex( mutex );
                     return 0;
                 }
#endif
                 Application->Initialize();
                 Application->Title = "Programming Station";
                 Application->CreateForm(__classid(TProgStationForm), &ProgStationForm);
                 Application->CreateForm(__classid(TDiagnosticForm), &DiagnosticForm);
                 Application->CreateForm(__classid(TAboutForm), &AboutForm);
                 Application->CreateForm(__classid(TReaderIDDefForm), &ReaderIDDefForm);
                 Application->CreateForm(__classid(TFileSysMsgForm), &FileSysMsgForm);
                 Application->CreateForm(__classid(TConfigProgStationForm), &ConfigProgStationForm);
                 Application->CreateForm(__classid(TScanForm), &ScanForm);
                 Application->CreateForm(__classid(TNetworkForm), &NetworkForm);
                 Application->CreateForm(__classid(TTAWSocket), &TAWSocket);
                 Application->CreateForm(__classid(TAboutFormNoLogo), &AboutFormNoLogo);
                 Application->CreateForm(__classid(TTagReadLargDataForm), &TagReadLargDataForm);
                 Application->Run();
        }
        catch (Exception &exception)
        {
                 Application->ShowException(&exception);
        }
        return 0;
}
//------------------------------------------------------------------------------
