//---------------------------------------------------------------------------

#ifndef AWSocketUnitH
#define AWSocketUnitH
//---------------------------------------------------------------------------
#include <Classes.hpp>
#include <Controls.hpp>
#include <StdCtrls.hpp>
#include <Forms.hpp>
#include <ScktComp.hpp>
//---------------------------------------------------------------------------
class TTAWSocket : public TForm
{
__published:	// IDE-managed Components
        TClientSocket *AWClientSocket;
        void __fastcall AWClientSocketRead(TObject *Sender,
          TCustomWinSocket *Socket);
        void __fastcall AWClientSocketError(TObject *Sender,
          TCustomWinSocket *Socket, TErrorEvent ErrorEvent,
          int &ErrorCode);
        void __fastcall AWClientSocketConnect(TObject *Sender,
          TCustomWinSocket *Socket);
        void __fastcall AWClientSocketDisconnect(TObject *Sender,
          TCustomWinSocket *Socket);
private:	// User declarations
public:
        int reader;
        int host;
        bool connected;
        int newReader;
        AnsiString rdrStatus;
        int index;

        __fastcall TTAWSocket(TComponent* Owner);
        bool __fastcall WriteSocket(unsigned char* xbuf, int len);
};
//---------------------------------------------------------------------------
extern PACKAGE TTAWSocket *TAWSocket;
//---------------------------------------------------------------------------
#endif
