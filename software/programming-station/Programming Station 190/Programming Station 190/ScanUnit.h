//---------------------------------------------------------------------------

#ifndef ScanUnitH
#define ScanUnitH
//---------------------------------------------------------------------------
#include <Classes.hpp>
#include <Controls.hpp>
#include <StdCtrls.hpp>
#include <Forms.hpp>

#include <winsock2.h>
#include <ws2tcpip.h>

#define COBOX_UDP_PORT 0x77fe	   // CoBox UDP config port 30718
#define CBX_CONF_QUERY	0xe4	   // LTX code get cobox setup record 4
#define UDP_BUF_SIZE	512
#define MAX_INTERFACES	10


#define SETUP_RECORDS 8		   // We only checking one at this point
#define SETUP_LENGTH (128+4)	   // Long enough to hold the entire record, and the header
#define OEM_ID_SIZE  4		   // 4 bytes
#define OEM_ID_OFFSET 0		   // Offset in the record
typedef SOCKET LTX_SOCKET;

//unsigned char Setup[SETUP_RECORDS][SETUP_LENGTH];

struct cbxentry
{
   struct sockaddr_in peer;	   // CoBox we found
   unsigned char record[126];	   // Setup record 4
   struct cbxentry *next;	   // Link to next entry
};

//---------------------------------------------------------------------------
class TScanForm : public TForm
{
__published:	// IDE-managed Components
        TButton *Button1;
        TLabel *Label1;
        TLabel *Label2;
        void __fastcall Button1Click(TObject *Sender);
private:	// User declarations
public:		// User declarations
        __fastcall TScanForm(TComponent* Owner);
        AnsiString s;
        HINSTANCE hDLL;
        void __fastcall CloseSocket(SOCKET sock);
        int __fastcall Scan(SOCKET sock, cbxentry **cbxlist, unsigned long oem_id);
        long __fastcall Interface(SOCKET, struct sockaddr_in *, INTERFACE_INFO *);
        bool __fastcall ScanNetwork();
        int __fastcall UDPEncrypt(char * plaintext, int len, char * ciphertext, int method);
        int __fastcall UDPDecrypt(char * ciphertext, int len, char * plaintext, int method);
        bool __fastcall ChangeIPAddress(AnsiString oldIP, AnsiString newIP);
        LTX_SOCKET open_socket(char *hostname, int port, int type, struct sockaddr_in *peer);
        int write_socket(LTX_SOCKET sock, unsigned char *sbuf, int len, struct sockaddr_in *to);
        int __fastcall read_socket(LTX_SOCKET sock, unsigned char *rbuf, int len, int seconds, struct sockaddr_in *from);
        void __fastcall ltx_closesocket(LTX_SOCKET sock);
};
//---------------------------------------------------------------------------
extern PACKAGE TScanForm *ScanForm;
//---------------------------------------------------------------------------
#endif

