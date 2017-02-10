//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include "AboutUnit.h"
//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma resource "*.dfm"
TAboutForm *AboutForm;
//---------------------------------------------------------------------------
__fastcall TAboutForm::TAboutForm(TComponent* Owner)
        : TForm(Owner)
{
}
//---------------------------------------------------------------------------
void __fastcall TAboutForm::FormCreate(TObject *Sender)
{
    char *build = new char[7 + 11 + 1 + 1]; // sizeof("Build :") + sizeof(__DATE__) + colon + null char;

    memcpy(&build[0],   "Build: ",    7 * sizeof(char));
    memcpy(&build[7],    __DATE__,    6 * sizeof(char));
    memmove(&build[15], &__DATE__[7], 4 * sizeof(char));

    build[13] = ',';
    build[14] = ' ';
    build[19] = '\0';

    lblBuild->Caption = build;

    delete[] build;
}
//---------------------------------------------------------------------------

