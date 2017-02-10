//---------------------------------------------------------------------------

#ifndef ReadLargeDataUnitH
#define ReadLargeDataUnitH
//---------------------------------------------------------------------------
#include <Classes.hpp>
#include <Controls.hpp>
#include <StdCtrls.hpp>
#include <Forms.hpp>
#include <Buttons.hpp>
#include <CheckLst.hpp>
//---------------------------------------------------------------------------
class TTagReadLargDataForm : public TForm
{
__published:	// IDE-managed Components
        TLabel *NumPktsLabel;
        TLabel *Label1;
        TLabel *BytesToSendLabel;
        TLabel *PktsToSendLabel;
        TLabel *Label2;
        TLabel *ByesSentLabel;
        TLabel *PktsSentLabel;
        TLabel *Label7;
        TLabel *SecLabel;
        TListBox *ListBox;
        TBitBtn *StopBitBtn;
        void __fastcall StopBitBtnClick(TObject *Sender);
private:	// User declarations
public:		// User declarations
        __fastcall TTagReadLargDataForm(TComponent* Owner);
        void __fastcall DisplayReadBytes (AnsiString s, int bytes, int pkts);
        void __fastcall UpdateElapsedTime (int sec);
};
//---------------------------------------------------------------------------
extern PACKAGE TTagReadLargDataForm *TagReadLargDataForm;
//---------------------------------------------------------------------------
#endif
