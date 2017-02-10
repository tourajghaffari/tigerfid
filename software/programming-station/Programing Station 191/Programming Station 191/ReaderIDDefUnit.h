//---------------------------------------------------------------------------

#ifndef ReaderIDDefUnitH
#define ReaderIDDefUnitH
//---------------------------------------------------------------------------
#include <Classes.hpp>
#include <Controls.hpp>
#include <StdCtrls.hpp>
#include <Forms.hpp>
#include <Buttons.hpp>
//---------------------------------------------------------------------------
class TReaderIDDefForm : public TForm
{
__published:	// IDE-managed Components
        TEdit *ReaderEdit;
        TLabel *Label1;
        TBitBtn *OKBitBtn;
        TLabel *Label2;
        TEdit *HostEdit;
        void __fastcall OKBitBtnClick(TObject *Sender);
private:	// User declarations
public:		// User declarations
        __fastcall TReaderIDDefForm(TComponent* Owner);
};
//---------------------------------------------------------------------------
extern PACKAGE TReaderIDDefForm *ReaderIDDefForm;
//---------------------------------------------------------------------------
#endif
