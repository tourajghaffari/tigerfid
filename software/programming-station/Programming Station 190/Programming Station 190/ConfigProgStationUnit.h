//---------------------------------------------------------------------------

#ifndef ConfigProgStationUnitH
#define ConfigProgStationUnitH
//---------------------------------------------------------------------------
#include <Classes.hpp>
#include <Controls.hpp>
#include <StdCtrls.hpp>
#include <Forms.hpp>
#include <Buttons.hpp>
//---------------------------------------------------------------------------
class TConfigProgStationForm : public TForm
{
__published:	// IDE-managed Components
        TGroupBox *GroupBox1;
        TCheckBox *MultiDisplayTagCheckBox;
        TCheckBox *DuplicateTagFGenCheckBox;
        TCheckBox *DuplicateTagGIDCheckBox;
        TGroupBox *GroupBox2;
        TEdit *OldHostIDEdit;
        TLabel *Label1;
        TLabel *Label2;
        TEdit *NewHostIDEdit;
        TCheckBox *AllHostCheckBox;
        TBitBtn *BitBtn1;
        TBitBtn *SaveBitBtn;
        TGroupBox *GroupBox3;
        TEdit *TempCalibEdit;
        TLabel *Label3;
        TRadioButton *TempCalibCRadioButton;
        TRadioButton *TempCalibFRadioButton;
        TGroupBox *GroupBox4;
        TCheckBox *Type01CheckBox;
        TCheckBox *Type02CheckBox;
        TCheckBox *Type03CheckBox;
        TCheckBox *Type04CheckBox;
        TCheckBox *Type05CheckBox;
        TCheckBox *Type06CheckBox;
        TEdit *Type01Edit;
        TEdit *Type02Edit;
        TEdit *Type03Edit;
        TEdit *Type04Edit;
        TEdit *Type05Edit;
        TEdit *Type06Edit;
        TEdit *Type01AbrEdit;
        TEdit *Type02AbrEdit;
        TEdit *Type03AbrEdit;
        TEdit *Type04AbrEdit;
        TEdit *Type05AbrEdit;
        TEdit *Type06AbrEdit;
        TLabel *Label4;
        TLabel *Label5;
        TBitBtn *Reset;
        TLabel *Label6;
        TLabel *Label7;
        void __fastcall FormActivate(TObject *Sender);
        void __fastcall SaveBitBtnClick(TObject *Sender);
        void __fastcall TempCalibCRadioButtonClick(TObject *Sender);
        void __fastcall TempCalibFRadioButtonClick(TObject *Sender);
        void __fastcall Type01CheckBoxClick(TObject *Sender);
        void __fastcall Type02CheckBoxClick(TObject *Sender);
        void __fastcall Type03CheckBoxClick(TObject *Sender);
        void __fastcall Type04CheckBoxClick(TObject *Sender);
        void __fastcall Type05CheckBoxClick(TObject *Sender);
        void __fastcall Type06CheckBoxClick(TObject *Sender);
        void __fastcall ResetClick(TObject *Sender);
private:	// User declarations
public:		// User declarations
        __fastcall TConfigProgStationForm(TComponent* Owner);
        bool dGIdFlag;
        bool dFgenFlag;
        float curTagTempCalibC;
        AnsiString localTagTypes[7];
        AnsiString localTagTypesAbr[7];
        bool __fastcall CheckDuplicatTagType();
        bool __fastcall CheckDuplicatTagTypeAbr();
};
//---------------------------------------------------------------------------
extern PACKAGE TConfigProgStationForm *ConfigProgStationForm;
//---------------------------------------------------------------------------
#endif
