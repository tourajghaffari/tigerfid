//---------------------------------------------------------------------------

#ifndef TitleUnitH
#define TitleUnitH
//---------------------------------------------------------------------------
#include <Classes.hpp>
#include <Controls.hpp>
#include <StdCtrls.hpp>
#include <Forms.hpp>
#include <Buttons.hpp>
//---------------------------------------------------------------------------
class TTitleForm : public TForm
{
__published:	// IDE-managed Components
        TEdit *TitleEdit;
        TLabel *Label1;
        TBitBtn *BitBtn1;
        void __fastcall BitBtn1Click(TObject *Sender);
private:	// User declarations
public:		// User declarations
        __fastcall TTitleForm(TComponent* Owner);
};
//---------------------------------------------------------------------------
extern PACKAGE TTitleForm *TitleForm;
//---------------------------------------------------------------------------
#endif
