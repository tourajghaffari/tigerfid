//---------------------------------------------------------------------------

#ifndef netConnectMsgUnitH
#define netConnectMsgUnitH
//---------------------------------------------------------------------------
#include <Classes.hpp>
#include <Controls.hpp>
#include <StdCtrls.hpp>
#include <Forms.hpp>
#include <ExtCtrls.hpp>
//---------------------------------------------------------------------------
class TNetConnectStatusForm : public TForm
{
__published:	// IDE-managed Components
        TLabel *MsgLabel;
        TTimer *Timer1;
        void __fastcall Timer1Timer(TObject *Sender);
private:	// User declarations
public:		// User declarations
        __fastcall TNetConnectStatusForm(TComponent* Owner);
        int msgCount;
};
//---------------------------------------------------------------------------
extern PACKAGE TNetConnectStatusForm *NetConnectStatusForm;
//---------------------------------------------------------------------------
#endif
