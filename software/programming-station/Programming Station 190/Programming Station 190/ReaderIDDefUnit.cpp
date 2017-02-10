//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include "ReaderIDDefUnit.h"
//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma resource "*.dfm"
TReaderIDDefForm *ReaderIDDefForm;
extern AnsiString AssignedReaderID;
extern AnsiString AssignedHostID;
//---------------------------------------------------------------------------
__fastcall TReaderIDDefForm::TReaderIDDefForm(TComponent* Owner)
        : TForm(Owner)
{
}
//---------------------------------------------------------------------------

void __fastcall TReaderIDDefForm::OKBitBtnClick(TObject *Sender)
{
   if (ReaderEdit->Text.data() == NULL)
   {
      ::MessageBoxEx(::GetDesktopWindow(), ( LPCSTR )"Error: No Reader ID is assigned.",
       ( LPCSTR )"Programming Station Dialog", MB_OK | MB_ICONEXCLAMATION | MB_TOPMOST  , LANG_ENGLISH );
       return;
   }
   else if (HostEdit->Text.data() == NULL)
   {
      ::MessageBoxEx(::GetDesktopWindow(), ( LPCSTR )"Error: No Host ID is assigned.",
       ( LPCSTR )"Programming Station Dialog", MB_OK | MB_ICONEXCLAMATION | MB_TOPMOST  , LANG_ENGLISH );
       return;
   }
   else if (atoi(HostEdit->Text.c_str()) > 255)
   {
      ::MessageBoxEx(::GetDesktopWindow(), ( LPCSTR )"Error: ID Host Range (1-255).",
       ( LPCSTR )"Programming Station Dialog", MB_OK | MB_ICONEXCLAMATION | MB_TOPMOST  , LANG_ENGLISH );
       return;
   }
   else
   {
      AssignedReaderID = ReaderEdit->Text;
      AssignedHostID = HostEdit->Text;
   }

   Close();
}
//---------------------------------------------------------------------------

