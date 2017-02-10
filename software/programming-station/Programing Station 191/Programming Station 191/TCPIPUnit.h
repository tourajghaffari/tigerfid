//---------------------------------------------------------------------------

#ifndef TCPIPUnitH
#define TCPIPUnitH
//---------------------------------------------------------------------------
#include <Classes.hpp>
#include <Controls.hpp>
#include <StdCtrls.hpp>
#include <Forms.hpp>
#include <winsock2.h>
#include <ws2tcpip.h>
typedef SOCKET LTX_SOCKET;
//---------------------------------------------------------------------------
class TTCPIPForm : public TForm
{
__published:	// IDE-managed Components
        TListBox *ListBox1;
        TLabel *Label1;
        void __fastcall FormActivate(TObject *Sender);
        void __fastcall FormClose(TObject *Sender, TCloseAction &Action);
private:	// User declarations
public:		// User declarations
        __fastcall TTCPIPForm(TComponent* Owner);
        bool __fastcall Startup(int incrypt, int type, int port);
        bool commThreadRunning;
        /*WSADATA  info;   // WinSock info handle
        HINSTANCE hDLL;  // cbx_enc.dll handle id
        int encryption;
        LTX_SOCKET sock, lsock;	    // Socket ids
	struct sockaddr_in peer;    // Network peer socket information
        */
        //sockaddr_in* peerSock;
        int __fastcall write_socket(LTX_SOCKET sock, char *sbuf, int len, struct sockaddr_in *to, int indx);
        int __fastcall read_socket(LTX_SOCKET sock, char *rbuf, int len, struct sockaddr_in *from);
        void __fastcall ltx_closesocket(LTX_SOCKET sock, bool all);
        LTX_SOCKET __fastcall open_socket(char *hostname, int port, int type, struct sockaddr_in *peer);
        //int __fastcall main_loop(LTX_SOCKET sock, struct sockaddr_in *from, int encryption);
        void __fastcall serial_write(char *plaintext, int len);
        bool __fastcall ConnectSingleIP(AnsiString ip, int encrypt, int type, int port);
        int __fastcall GetNextValidIndex();
};
//---------------------------------------------------------------------------
extern PACKAGE TTCPIPForm *TCPIPForm;
//---------------------------------------------------------------------------
#endif
