//---------------------------------------------------------------------------

#ifndef FileSysMsgUnitH
#define FileSysMsgUnitH
//---------------------------------------------------------------------------
#include <Classes.hpp>
#include <Controls.hpp>
#include <StdCtrls.hpp>
#include <Forms.hpp>
#include <Buttons.hpp>
//---------------------------------------------------------------------------
class TFileSysMsgForm : public TForm
{
__published:	// IDE-managed Components
        TBitBtn *OverWriteBitBtn;
        TBitBtn *AppendBitBtn;
        TBitBtn *BitBtn3;
        TLabel *Label1;
        void __fastcall OverWriteBitBtnClick(TObject *Sender);
        void __fastcall AppendBitBtnClick(TObject *Sender);
private:	// User declarations
public:		// User declarations
        __fastcall TFileSysMsgForm(TComponent* Owner);
};
//---------------------------------------------------------------------------
extern PACKAGE TFileSysMsgForm *FileSysMsgForm;
//---------------------------------------------------------------------------
#endif
