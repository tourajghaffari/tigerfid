//---------------------------------------------------------------------------

#ifndef NetworkUnitH
#define NetworkUnitH
//---------------------------------------------------------------------------
#include <Classes.hpp>
#include <Controls.hpp>
#include <StdCtrls.hpp>
#include <Forms.hpp>
#include <ScktComp.hpp>
#include <ExtCtrls.hpp>
//---------------------------------------------------------------------------
class TNetworkForm : public TForm
{
__published:	// IDE-managed Components
        TClientSocket *ClientSocket1;
        TButton *Button1;
        TButton *Button2;
        TLabel *Label1;
        TLabel *Label2;
        TLabel *Label3;
        TLabel *Label4;
        TTimer *Timer1;
        TButton *Button3;
        TButton *Button4;
        void __fastcall Button1Click(TObject *Sender);
        void __fastcall Button2Click(TObject *Sender);
        void __fastcall ClientSocket1Connect(TObject *Sender,
          TCustomWinSocket *Socket);
        void __fastcall ClientSocket1Disconnect(TObject *Sender,
          TCustomWinSocket *Socket);
        void __fastcall ClientSocket1Read(TObject *Sender,
          TCustomWinSocket *Socket);
        void __fastcall ClientSocket1Error(TObject *Sender,
          TCustomWinSocket *Socket, TErrorEvent ErrorEvent,
          int &ErrorCode);
        void __fastcall Timer1Timer(TObject *Sender);
        void __fastcall Button3Click(TObject *Sender);
        void __fastcall FormClose(TObject *Sender, TCloseAction &Action);
        void __fastcall Button4Click(TObject *Sender);
private:	// User declarations
public:		// User declarations
        __fastcall TNetworkForm(TComponent* Owner);
        AnsiString strMsg;
        bool once;
};
//---------------------------------------------------------------------------
extern PACKAGE TNetworkForm *NetworkForm;
//---------------------------------------------------------------------------
#endif
