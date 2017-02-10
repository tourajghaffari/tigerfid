//---------------------------------------------------------------------------

#include <vcl.h>
#include <math.h>
#pragma hdrstop

#include "ReadLargeDataUnit.h"
#include "ProgStationUnit.h"
//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma resource "*.dfm"
TTagReadLargDataForm *TagReadLargDataForm;
TProgStationForm* prgStation;
//---------------------------------------------------------------------------
__fastcall TTagReadLargDataForm::TTagReadLargDataForm(TComponent* Owner)
        : TForm(Owner)
{
   int n = ((TProgStationForm *)Owner->Components[1])->MaxReadLargeData;
   BytesToSendLabel->Caption = n;
   if ((n%12) == 0)
      PktsToSendLabel->Caption = n/12;
   else
      PktsToSendLabel->Caption = abs(n/12) + 1;

   prgStation = (TProgStationForm *)Owner;
}
//---------------------------------------------------------------------------
void __fastcall TTagReadLargDataForm::DisplayReadBytes(AnsiString s, int bytes, int pkts)
{
    ListBox->AddItem(s, this);
    ByesSentLabel->Caption = bytes;
    PktsSentLabel->Caption = pkts;
}

void __fastcall TTagReadLargDataForm::UpdateElapsedTime (int sec)
{
   AnsiString s = sec;
   s += " sec";
   SecLabel->Caption = s;
}
void __fastcall TTagReadLargDataForm::StopBitBtnClick(TObject *Sender)
{
   prgStation->ReadLargDataTimer->Enabled = false;
   prgStation->displayElapsedTime = false;
}
//---------------------------------------------------------------------------


