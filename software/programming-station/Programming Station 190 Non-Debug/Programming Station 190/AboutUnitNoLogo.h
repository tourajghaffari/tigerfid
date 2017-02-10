//---------------------------------------------------------------------------

#ifndef AboutUnitNoLogoH
#define AboutUnitNoLogoH
//---------------------------------------------------------------------------
#include <Classes.hpp>
#include <Controls.hpp>
#include <StdCtrls.hpp>
#include <Forms.hpp>
//---------------------------------------------------------------------------
class TAboutFormNoLogo : public TForm
{
__published:	// IDE-managed Components
        TLabel *Label2;
        TLabel *Label3;
        TLabel *Label4;
private:	// User declarations
public:		// User declarations
        __fastcall TAboutFormNoLogo(TComponent* Owner);
};
//---------------------------------------------------------------------------
extern PACKAGE TAboutFormNoLogo *AboutFormNoLogo;
//---------------------------------------------------------------------------
#endif
