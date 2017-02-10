//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include "WriteThreadUnit.h"
#include "ProgStationUnit.h"
#include "Commands.h"
#pragma package(smart_init)

extern unsigned char XBuf[260];
extern HANDLE comPortID;
static OVERLAPPED overlapThreadWrite = {0};
extern UINT pktCounter;
extern UINT txCommand;
extern int txLen;
extern bool RS232On;
extern bool networkOn;
//extern unsigned char txBuf[16];
#define WRITE_TIMEOUT 1000   //millisecond
//---------------------------------------------------------------------------

//   Important: Methods and properties of objects in VCL can only be
//   used in a method called using Synchronize, for example:
//
//      Synchronize(UpdateCaption);
//
//   where UpdateCaption could look like:
//
//      void __fastcall Unit1::UpdateCaption()
//      {
//        Form1->Caption = "Updated in a thread";
//      }
//---------------------------------------------------------------------------

__fastcall WriteThread::WriteThread(bool CreateSuspended)
        : TThread(CreateSuspended)
{
   overlapThreadWrite.hEvent = CreateEvent(NULL, false, false, NULL);
   if (overlapThreadWrite.hEvent == NULL)
   {
      ::MessageBoxEx(::GetDesktopWindow(), ( LPCSTR )"ERROR: Creating write overlapped event.",
                    ( LPCSTR )"Programming Station Information Dialog", MB_OK | MB_ICONSTOP | MB_TOPMOST  , LANG_ENGLISH );
      //sysStr = "ERROR: Creating write overlapped event.";
      //MainStatusBar->Panels->Items[0]->Text = "ERROR: Creating write overlapped event.";

   }
}
//---------------------------------------------------------------------------
void __fastcall WriteThread::Execute()
{
        //---- Place thread code here ----
    int len;
    while (1)
    {
       WriteComm();
       Suspend();
    }
}
//---------------------------------------------------------------------------

bool __fastcall WriteThread::WriteComm()
{
   DWORD BytesSend;
   DWORD dwRes;

   if (RS232On)
   {
      Priority = tpTimeCritical;
      for (int i=0; i<ProgStationForm->pktLenToTransmit; i++)
      {
         //if (!WriteFile(comPortID, &XBuf[0], TotalLen, &BytesSend, &overlapWrite))
         if (!WriteFile(comPortID, &XBuf[i], 1, &BytesSend, &overlapThreadWrite))
         {
             if (GetLastError() != ERROR_IO_PENDING)
             {
                 //SetThreadPriority(GetCurrentThread(), THREAD_PRIORITY_NORMAL);
                 //WriteComm(LAST_COMMAND, XBufLast[0], NULL);
                 return (0);
             }
             else
             {
                 dwRes = WaitForSingleObject(overlapThreadWrite.hEvent, WRITE_TIMEOUT);
                 switch (dwRes)
                 {
                     case WAIT_OBJECT_0:
                        if(!GetOverlappedResult(comPortID, &overlapThreadWrite, &BytesSend, TRUE))
                        {
                           //SetThreadPriority(GetCurrentThread(), THREAD_PRIORITY_NORMAL);
                           //WriteComm(LAST_COMMAND, XBufLast[0], NULL);
                           return (0);
                        }
                     break;

                     default:
                     break;

                 }//switch
             }
         }
      }//for loop

      Priority = tpNormal;
   }

   ProgStationForm->lastLenBytesSent = ProgStationForm->pktLenToTransmit;

   if (ProgStationForm->displayTx)
   {
      ProgStationForm->DisplayTxInfo();
   }

   return (true);
}