//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include "DiagnosticUnit.h"
#include "ProgStationUnit.h"
#include "Commands.h"
//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma resource "*.dfm"
TDiagnosticForm *DiagnosticForm;
extern char DiagnosticByte;
//---------------------------------------------------------------------------
__fastcall TDiagnosticForm::TDiagnosticForm(TComponent* Owner)
        : TForm(Owner)
{

}
//---------------------------------------------------------------------------
void __fastcall TDiagnosticForm::FormActivate(TObject *Sender)
{
  switch (DiagnosticByte)
  {
     case 0x00:
        CheckBoxBit00->Checked = false;
        CheckBoxBit01->Checked = false;
        CheckBoxBit02->Checked = false;
        CheckBoxBit03->Checked = false;
        CheckBoxBit04->Checked = false;
        CheckBoxBit05->Checked = false;
        CheckBoxBit06->Checked = false;
        CheckBoxBit07->Checked = false;
        CheckBoxBit06->Font->Color = clBlue;
        Label06->Font->Color = clGray;
     break;

     case 0x40:
        CheckBoxBit00->Checked = false;
        CheckBoxBit01->Checked = false;
        CheckBoxBit02->Checked = false;
        CheckBoxBit03->Checked = false;
        CheckBoxBit04->Checked = false;
        CheckBoxBit05->Checked = false;
        CheckBoxBit06->Checked = true;
        CheckBoxBit07->Checked = false;
        Label06->Font->Color = clBlue;
        CheckBoxBit06->Font->Color = clGray;
     break;
  }
}
//---------------------------------------------------------------------------

void __fastcall TDiagnosticForm::CheckBoxBit06Click(TObject *Sender)
{
   if (CheckBoxBit06->State == cbChecked)
   {
      DiagnosticByte = 0x40;
      Label06->Font->Color = clBlue;
      CheckBoxBit06->Font->Color = clGray;
   }
   else
   {
      DiagnosticByte = 0x00;
      Label06->Font->Color = clGray;
      CheckBoxBit06->Font->Color = clBlue;
   }
}
//---------------------------------------------------------------------------

void __fastcall TDiagnosticForm::SetCommandBitBtnClick(TObject *Sender)
{
    ProgStationForm->WriteRS232Comm(DIAGNOSTIC, 5, NULL, 0);
}
//---------------------------------------------------------------------------

