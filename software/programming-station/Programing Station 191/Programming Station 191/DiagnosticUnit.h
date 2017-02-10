//---------------------------------------------------------------------------

#ifndef DiagnosticUnitH
#define DiagnosticUnitH
//---------------------------------------------------------------------------
#include <Classes.hpp>
#include <Controls.hpp>
#include <StdCtrls.hpp>
#include <Forms.hpp>
#include <Buttons.hpp>
//---------------------------------------------------------------------------
class TDiagnosticForm : public TForm
{
__published:	// IDE-managed Components
        TBitBtn *SetCommandBitBtn;
        TBitBtn *BitBtn1;
        TGroupBox *GroupBox1;
        TLabel *Label1;
        TCheckBox *CheckBoxBit00;
        TLabel *Label2;
        TCheckBox *CheckBoxBit01;
        TCheckBox *CheckBoxBit02;
        TLabel *Label3;
        TLabel *Label4;
        TCheckBox *CheckBoxBit03;
        TLabel *Label5;
        TCheckBox *CheckBoxBit04;
        TLabel *Label6;
        TCheckBox *CheckBoxBit05;
        TLabel *Label7;
        TCheckBox *CheckBoxBit06;
        TLabel *Label8;
        TCheckBox *CheckBoxBit07;
        TLabel *Label06;
        void __fastcall FormActivate(TObject *Sender);
        void __fastcall CheckBoxBit06Click(TObject *Sender);
        void __fastcall SetCommandBitBtnClick(TObject *Sender);

private:	// User declarations
public:		// User declarations
        __fastcall TDiagnosticForm(TComponent* Owner);
};
//---------------------------------------------------------------------------
extern PACKAGE TDiagnosticForm *DiagnosticForm;
//---------------------------------------------------------------------------
#endif
