//---------------------------------------------------------------------------

#ifndef SocketUnitH
#define SocketUnitH
//---------------------------------------------------------------------------
#include <Classes.hpp>
#include <Controls.hpp>
#include <StdCtrls.hpp>
#include <Forms.hpp>
#include <ScktComp.hpp>
//---------------------------------------------------------------------------
class TSocketForm : public TForm
{
__published:	// IDE-managed Components
        TClientSocket *ClientSocket1;
private:	// User declarations
public:		// User declarations
        __fastcall TSocketForm(TComponent* Owner);
};
//---------------------------------------------------------------------------
extern PACKAGE TSocketForm *SocketForm;
//---------------------------------------------------------------------------
#endif
