//---------------------------------------------------------------------------
#include "netConnectMsgUnit.h"
#include <vcl.h>
#pragma hdrstop

//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma resource "*.dfm"
TNetConnectStatusForm *NetConnectStatusForm;
//---------------------------------------------------------------------------
__fastcall TNetConnectStatusForm::TNetConnectStatusForm(TComponent* Owner)
        : TForm(Owner)
{
   msgCount = 0;
}
//---------------------------------------------------------------------------
void __fastcall TNetConnectStatusForm::Timer1Timer(TObject *Sender)
{
   if (msgCount == 0)
      MsgLabel->Caption = " ";
   else if (msgCount == 1)
      MsgLabel->Caption += "Please ";
   else if (msgCount == 2)
      MsgLabel->Caption += "Wait! ";
   else if (msgCount == 3)
      MsgLabel->Caption += "Trying ";
   else if (msgCount == 4)
      MsgLabel->Caption += "to ";
   else if (msgCount == 5)
      MsgLabel->Caption += "Connect ";
   else if (msgCount == 6)
      MsgLabel->Caption += "to ";
   else if (msgCount == 7)
      MsgLabel->Caption += "network ";

   msgCount++;
   if (msgCount >= 8)
      msgCount = 0;
}
//---------------------------------------------------------------------------
