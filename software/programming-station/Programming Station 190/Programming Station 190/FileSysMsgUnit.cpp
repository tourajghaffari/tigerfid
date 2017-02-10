//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include "FileSysMsgUnit.h"
//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma resource "*.dfm"
TFileSysMsgForm *FileSysMsgForm;
extern unsigned short dlgRet;
//---------------------------------------------------------------------------
__fastcall TFileSysMsgForm::TFileSysMsgForm(TComponent* Owner)
        : TForm(Owner)
{
   dlgRet = 0x00;
}
//---------------------------------------------------------------------------
void __fastcall TFileSysMsgForm::OverWriteBitBtnClick(TObject *Sender)
{
   dlgRet = 0x01;
   Close();
}
//---------------------------------------------------------------------------

void __fastcall TFileSysMsgForm::AppendBitBtnClick(TObject *Sender)
{
   dlgRet = 0x02;
   Close();
}
//---------------------------------------------------------------------------

