//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include "TitleUnit.h"
//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma resource "*.dfm"
TTitleForm *TitleForm;
//extern AnsiString demoTitle;
//---------------------------------------------------------------------------
__fastcall TTitleForm::TTitleForm(TComponent* Owner)
        : TForm(Owner)
{
   //TitleEdit->Text = demoTitle;
}
//---------------------------------------------------------------------------
void __fastcall TTitleForm::BitBtn1Click(TObject *Sender)
{
  // demoTitle = TitleEdit->Text;
}
//---------------------------------------------------------------------------
