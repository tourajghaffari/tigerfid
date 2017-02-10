//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include "MsgThreadUnit.h"
#include "netConnectMsgUnit.h"
#pragma package(smart_init)
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

__fastcall TMsgThread::TMsgThread(bool CreateSuspended)
        : TThread(CreateSuspended)
{
   netCoonectMsgForm = new TNetConnectStatusForm (NULL);
   Application->CreateForm(__classid(TNetConnectStatusForm), &netCoonectMsgForm);
   netCoonectMsgForm->Update();
   Suspend();
}
//---------------------------------------------------------------------------
void __fastcall TMsgThread::Execute()
{
    netCoonectMsgForm->Show();
}
//---------------------------------------------------------------------------
