//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include "ScanUnit.h"
#include "ComConfigPage.h"

//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma resource "*.dfm"

TScanForm *ScanForm;
extern TCommConfigDlg *comConfigDialog;
extern int encryption;

unsigned char Setup[SETUP_RECORDS][SETUP_LENGTH];

//char ive[16], ivd[16];	// Pointer to initialization vectors

//Encryption key
//unsigned char key[16] = { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07,
                          //0x08, 0x09, 0x0a, 0x0b, 0x0c, 0x0d, 0x0e, 0x0f };

//Pointer to VC_blockEncrypt() function
//int (*blockEncrypt)(char *iv, char *key, int bytes, char *text, char method);

//char shared_key[33];   //2 byte version of key

#define ACTIVE_WAVE_OEM_ID    102
#define SOCKBUFSIZE           256
#define QUERY_CONFIG          0xF8
//---------------------------------------------------------------------------
__fastcall TScanForm::TScanForm(TComponent* Owner)
        : TForm(Owner)
{
   ScanForm = this;
}
//---------------------------------------------------------------------------
bool __fastcall TScanForm::ScanNetwork()
{
   SOCKET sock;
   int i;
   unsigned long oem_id;
   cbxentry *cbxlp, *cbxlp1;
   WSADATA	info;

   oem_id = htonl(ACTIVE_WAVE_OEM_ID);
	
   // Clear the records
   for (i = 0; i < SETUP_RECORDS; i++)
      memset(&Setup[i], 0, SETUP_LENGTH);

   cbxlp = (cbxentry *) NULL;

   //start up winsock
   if ((i = WSAStartup(MAKEWORD(1,1), &info)) < 0 )
   {
      //fprintf(stderr, "Error on WSAStartup(), error (%d)\n", i);
      Application->MessageBox("Error: WSAStartup()", "Error", MB_OK);
      return(i);
   }

   // Create the SOCK_DGRAM socket
   if ((sock = socket(AF_INET, SOCK_DGRAM, 0)) < 0)
   {
      //fprintf(stderr, "Error opening socket (%d)\n", sock);
      Application->MessageBox("Error: Opening socket", "Error", MB_OK);
      WSACleanup();
      return(-1);
   }

   /*if (encryption)
   {
      //Load the DLL
      hDLL = LoadLibrary("cbx_enc");
      if (hDLL != NULL)
      {
         //Find the VC_blockEncrypt() function
         blockEncrypt = (int (*)(char *, char *, int , char *, char)) GetProcAddress(hDLL, "VC_blockEncrypt");
         if (!blockEncrypt)
         {
            FreeLibrary(hDLL);
            return(-1);
         }
      }

      int j;
      //Convert HEX key to 2 byte hex sequence
      for (i = 0, j = 0; i < sizeof(key); i++, j+= 2)
      {
         sprintf(&shared_key[j], "%.2X", key[i]);
      }
   } //encryption*/

   // Scan the network for devices
   if (Scan(sock, &cbxlp, oem_id) < 0)
   {
      CloseSocket(sock);
      WSACleanup();
      return(-1);
   }

   cbxlp1 = cbxlp;
   while ((cbxlp != (cbxentry *) NULL) && (cbxlp->peer.sin_addr.S_un.S_addr != 0) )
   {
      //printf("cobox at %s ", inet_ntoa(cbxlp->peer.sin_addr));
      //ScanForm->Label2->Caption = ScanForm->s.sprintf("cobox at %s ", inet_ntoa(cbxlp->peer.sin_addr));
      s = s.sprintf("%s", inet_ntoa(cbxlp->peer.sin_addr));
      comConfigDialog->AddIpToList(s, false);

      cbxlp = cbxlp->next;
   }

   // Free the records
   cbxlp = cbxlp1;
   while (cbxlp != (cbxentry *) NULL)
   {
      cbxlp = cbxlp->next;
      free(cbxlp1);
      cbxlp1 = cbxlp;
   }

   // Close up shop
   CloseSocket(sock);
   WSACleanup();

   return 0;
}
//------------------------------------------------------------------------------
void __fastcall TScanForm::CloseSocket(SOCKET sock)
{
   shutdown(sock, 2);
   closesocket(sock);
}
//------------------------------------------------------------------------------
int __fastcall TScanForm::Scan(SOCKET sock, cbxentry **cbxlist, unsigned long oem_id)
{
    //unsigned char  reqt[18];
    unsigned char  reqt[4];
    unsigned char  buf[UDP_BUF_SIZE];
    int i, len, n, nbytes, timeout;
    unsigned long br = 0;
    INTERFACE_INFO ifo[MAX_INTERFACES];
    struct sockaddr_in peer;
    fd_set  rfds, efds;
    struct timeval timev;
    cbxentry *cbxlp1, *cbxlp2;

    memset(reqt, 0, sizeof(reqt));
    memset(buf, 0, sizeof(buf));

    reqt[3] = CBX_CONF_QUERY;

    // It's a good idea to bind this socket to a local port number
    peer.sin_family = AF_INET;
    peer.sin_addr.s_addr = INADDR_ANY;
    peer.sin_port = 0;
    if (bind(sock, (struct sockaddr *) &peer, sizeof(peer)))
    {
       //fprintf(stderr, "Error binding socket\n");
       Application->MessageBox("Error: Binding socket", "Error", MB_OK);
       return(-1);
    }

    // Grab all the network interfaces
    memset(&ifo[0], 0, sizeof(ifo));
    if ((WSAIoctl(sock, SIO_GET_INTERFACE_LIST, NULL, 0, &ifo[0], sizeof(ifo), &br, NULL, NULL)) != 0)
    {
       //fprintf(stderr, "Get SIO_GET_INTERFACE_LIST error (%d)\n", (WSAGetLastError()));
       Application->MessageBox("Error: Get SIO_GET_INTERFACE_LIST", "Error", MB_OK);
       return(-3);
    }

    // Query each network
    for (i = 0; i < (int)(br / sizeof(INTERFACE_INFO)); i++)
    {
       if (Interface(sock, &peer, &ifo[i]))
       {
          if ((n = sendto(sock, reqt, sizeof(reqt), 0, (struct sockaddr *) &peer, sizeof(struct sockaddr_in))) <= 0)
          {
             //fprintf(stderr, "sendto (%s) failed!\n", inet_ntoa(peer.sin_addr));
             Application->MessageBox("Error: sendto failed!", "Error", MB_OK);
          }
       }

    }

    // We've sent the queries, now see if anyone responded
    cbxlp1 = *cbxlist;
    n = 0;
    while(1)
    {
       FD_ZERO(&rfds);
       FD_ZERO(&efds);
       FD_SET(sock, &rfds);
       FD_SET(sock, &efds);
       timev.tv_sec = 3;	//three second timeout
       timev.tv_usec = 0;
       timeout = select(FD_SETSIZE, &rfds, (fd_set *) NULL, &efds, &timev);
       if (timeout == -1)
       {
          continue;
       }
       else if (timeout == 0)
       {
          break;
       }
       else if (FD_ISSET(sock, &efds))
       {
          //fprintf(stderr, "Bad news, exception on socket\n");
          Application->MessageBox("Error: Exception on socket", "Error", MB_OK);
          break;
       }
       else if (FD_ISSET(sock, &rfds))
       {
          memset(buf, 0, sizeof(buf));
          len = sizeof(struct sockaddr_in);
          if ((nbytes = recvfrom(sock, buf, sizeof(buf), 0, (struct sockaddr *) &peer, &len)) > 0)
          {
             if ( (oem_id == 0) || (!(memcmp(buf+4+OEM_ID_OFFSET, &oem_id, OEM_ID_SIZE))) )
             {
                if (cbxlp1 == (cbxentry *) NULL)
                {
                   if ((cbxlp1 = (cbxentry *) malloc(sizeof(cbxentry))) == (cbxentry *) NULL)
                      return(n);
                   memset(cbxlp1, 0, sizeof(cbxentry));
                   memcpy(&cbxlp1->peer, &peer, sizeof(struct sockaddr_in));
                   memcpy(&cbxlp1->record, buf+4, sizeof(cbxlp1->record));
                   if (n == 0)
                      *cbxlist = cbxlp2 = cbxlp1;
                   else
                   {
                      cbxlp2->next = cbxlp1;
                      cbxlp2 = cbxlp1;
                   }
                   cbxlp1 = cbxlp1->next;
                }
                n++;
             }
          }
       }
    }
    
    return(n);
}
//------------------------------------------------------------------------------
void __fastcall TScanForm::Button1Click(TObject *Sender)
{
   Label1->Caption = "AA";
   Label2->Caption = "BB";
   ScanNetwork();
}
//------------------------------------------------------------------------------
int __fastcall TScanForm::UDPEncrypt(char* plaintext, int len, char* eText, int method)
{
   int i, newlen;

   // Seed random number generator
   /*srand( (unsigned)time(NULL));

   // Fill Initialization Vector with random data and copy it into the UDP payload
   for (i = 0; i < 16; i++)
      ive[i] = eText[i] = rand();

   //Add the data length to the payload
   eText[16] = len >> 8;
   eText[17] = len & 0xff;

   //Copy the plaintext into the payload because the encoder will perform the transformation in place
   memcpy(&eText[18], plaintext, len);

   //Round up the length to a multiple of 16 (bytes per block)
   newlen = ((len + 15) / 16) * 16;

   //Pad blocks with random data
   for (i = len; i < newlen; i++)
      eText[i+18] = rand();

   //CBC encrypt
   blockEncrypt(ive, shared_key, newlen, &eText[18], (method - 1));

   //Return the payload length
   return(newlen + 18);   */

   return (0);
}
//------------------------------------------------------------------------------
int __fastcall TScanForm::UDPDecrypt(char* dText, int len, char* plaintext, int method)
{
   int dlen;

   /*
   //Do we have the Initialization Vector and length?
   if (len < 18)
       return (0);

   //Extract the payload Initialization Vector
   memcpy(ivd, dText, 16);

   //Extract the payload data length
   dlen = (((dText[16] << 8) + dText[17] + 15) / 16) * 16;

   //Do we have a complete payload?
   if (len < (dlen + 18))
      return (0);

   //Extract the payload real data length
   len = (dText[16] << 8) + dText[17];

   //Extract the ciphertext into the destination array because the decoder will perform the transformation in place
   memcpy(plaintext, &dText[18], dlen);

   //Decrypt it by negating the encrypted data length
   blockEncrypt(ivd, shared_key, -dlen, plaintext, (method - 1));

   //Return the length of the real decrypted data
   */
   return(len);

}
//-------------------------------------------------------------------------------
bool __fastcall TScanForm::ChangeIPAddress(AnsiString oldIP, AnsiString newIP)
{
    LTX_SOCKET sock;
    int in, ret;
    struct sockaddr_in peer;
    unsigned char reply[20];
    WSADATA info;
    int record = 0;
    unsigned long ipAddr;
    bool b;

    if ((ret = WSAStartup(MAKEWORD(1,1), &info)) < 0 )
    {
       Application->MessageBox("Error on WSAStartup()", "Programming Station Error", MB_OK);
       return(false);
    }

    if ((sock = open_socket(oldIP.c_str(), COBOX_UDP_PORT, SOCK_DGRAM, &peer)) < 0)
    {
       AnsiString s = "Error opening socket IP = ";
       s += oldIP;
       Application->MessageBox(s.c_str(), "Programming Station Error", MB_OK);
       WSACleanup();
       return(false);
    }

    Setup[record][3] = QUERY_CONFIG;
    if ((write_socket(sock, Setup[record], 4, &peer)) <= 0)
    {
       Application->MessageBox("sendto for ip config failed", "Programming Station Error", MB_OK);
       ltx_closesocket(sock);
       WSACleanup();
       return(false);
    }

    if ((in = read_socket(sock, Setup[record], ((record == 0) ? 124 : 130), 5, &peer)) <= 0)
    {
       Application->MessageBox("readSocket for ip config failed", "Programming Station Error", MB_OK);
       ltx_closesocket(sock);
       WSACleanup();
       return(false);
    }

    ipAddr = inet_addr(newIP.c_str());
    memcpy(&Setup[record][4], &ipAddr, sizeof(unsigned long));

    Setup[record][2] = 0x00;
    Setup[record][3] = 0xFD + record;   //SET CONFIG

    if ((write_socket(sock, Setup[record], ((record == 0) ? 124 : 130), &peer)) <= 0)
     {
        Application->MessageBox("Write of new IP failed", "Programming Station Error", MB_OK);
        ltx_closesocket(sock);
        WSACleanup();
        return(false);
    }

    in = read_socket(sock, reply, 4, 5, &peer);   //read the relay and ignor it

    // WARNING	Device will 'reboot' after write
    // sleep long enough to avoid writing during the reboot
    Sleep(8000);

    memset(&Setup[record], 0, SETUP_LENGTH);
    Setup[record][3] = QUERY_CONFIG;
    if ((write_socket(sock, Setup[record], 4, &peer)) <= 0)
    {
       Application->MessageBox("sendto to get new IP failed", "Programming Station Error", MB_OK);
       ltx_closesocket(sock);
       WSACleanup();
       return(false);
    }

    if ((in = read_socket(sock, Setup[record], ((record == 0) ? 124 : 130), 5, &peer)) <= 0)
    {
       Application->MessageBox("readSocket to get new IP failed", "Programming Station Error", MB_OK);
       ltx_closesocket(sock);
       WSACleanup();
       return(false);
    }

    if (!(memcmp(&Setup[record][4], &ipAddr, sizeof(unsigned long))))
       //comConfigDialog->ConnectLabel->Caption = "Changing IP succeeded.";
       b = true;
    else
       //comConfigDialog->ConnectLabel->Caption = "Changing IP Failed.";
       b = false;

    ltx_closesocket(sock);
    WSACleanup();

    return(b);
}
//------------------------------------------------------------------------------
void __fastcall TScanForm::ltx_closesocket(LTX_SOCKET sock)
{
   shutdown(sock, 2);
   closesocket(sock);
}
//------------------------------------------------------------------------------
int __fastcall TScanForm::read_socket(LTX_SOCKET sock, unsigned char *rbuf, int len, int seconds, struct sockaddr_in *from)
{
   int fromlen;
   int	timeout, in, n, toget;
   fd_set rfds, efds;
   struct timeval timev;

   n = 0;
   toget = len;
   while (n < len)
   {
      FD_ZERO(&rfds);
      FD_ZERO(&efds);
      FD_SET(sock, &rfds);
      FD_SET(sock, &efds);
      timev.tv_sec = seconds;	// timeout
      timev.tv_usec = 0;
      timeout = select(FD_SETSIZE, &rfds, (fd_set *) NULL, &efds, &timev);
      if (timeout == -1)
      {
         // OS interrupt during select - should be ignored
         return(timeout);
      }
      else if (timeout == 0)
      {
         // Real timeout
         return(n);
      }
      else if (FD_ISSET(sock, &efds))
      {
         // Bad news, exception on socket - needs to be closed and reopened
         return(-2);
      }
      else if (FD_ISSET(sock, &rfds))
      {
         // Received real data
         fromlen = sizeof(struct sockaddr_in);
         if ( (in = recvfrom(sock, (char *)(rbuf+n), toget, 0, (struct sockaddr *) from, &fromlen)) <= 0)
         {
            // Error reading from socket
            return(-3);
         }
         n += in;
         toget -= in;
         if (n == len)
         {
            return(n);
         }
      }
   }
   return(n);
}
//------------------------------------------------------------------------------
int TScanForm::write_socket(LTX_SOCKET sock, unsigned char *sbuf, int len, struct sockaddr_in *to)
{
   int n, sent, tosend;

   sent = 0;
   tosend = len;
   while(tosend)
   {
      n = sendto(sock, (char *) &sbuf[sent], tosend, 0, (struct sockaddr *) to, sizeof (struct sockaddr));
      if (n <= 0)
      {
         // sendto failed
         return(-1);
      }
      else
      {
         sent += n;
         tosend -= n;
      }
   }
   return(sent);
}
//------------------------------------------------------------------------------
LTX_SOCKET TScanForm::open_socket(char *hostname, int port, int type, struct sockaddr_in *peer)
{
   LTX_SOCKET  sock;
   struct hostent *hdata;
   unsigned long  ipaddr;

   // create the socket
   if ((sock = socket(AF_INET, type, 0)) < 0)
   {
      return(sock);
   }

   if (isdigit(hostname[0]))
   {
      // dot decimal notation?
      if ((ipaddr = inet_addr(hostname)) == INADDR_NONE )
      {
         // inet_addr failed
         Application->MessageBox("Bad IP address", "Programming Station Error", MB_OK);
         ltx_closesocket(sock);
         return(0);
      }

      // We have a valid address.
      memcpy (&peer->sin_addr, &ipaddr, 4);
   }
   else
   {
      if ((hdata = gethostbyname (hostname)) == (struct hostent *) NULL)
      {
         // Can't resolve hostname
         Application->MessageBox("Can not resolve host name", "Programming Station Error", MB_OK);
         ltx_closesocket(sock);
         return(0);
      }
      memcpy (&peer->sin_addr, *(hdata->h_addr_list), hdata->h_length);
   }

   peer->sin_family = AF_INET;
   peer->sin_port = htons((unsigned short) port);

   if (type == SOCK_DGRAM) // (UDP) we're done
      return(sock);

   if ((connect(sock, (struct sockaddr *) peer, sizeof(struct sockaddr_in))) != 0)
   {
      // Connection failed
      Application->MessageBox("Connection failed", "Programming Station Error", MB_OK);
      ltx_closesocket(sock);
      return(0);
   }
   return(sock);
}
//------------------------------------------------------------------------------
long __fastcall TScanForm::Interface(SOCKET sock, struct sockaddr_in* peer, INTERFACE_INFO* ifo)
{
   peer->sin_addr.s_addr = ifo->iiAddress.AddressIn.sin_addr.s_addr;
   if ((ifo->iiAddress.AddressIn.sin_addr.S_un.S_addr != 0) &&
      ((ifo->iiFlags & IFF_LOOPBACK) == 0) &&
      ((ifo->iiFlags & (IFF_UP | IFF_BROADCAST)) == (IFF_UP | IFF_BROADCAST)) )
   {
      // The iiBroadcastAddress field is bogus, so do it manually
      peer->sin_addr.s_addr = ifo->iiAddress.AddressIn.sin_addr.s_addr & ifo->iiNetmask.AddressIn.sin_addr.s_addr;
      peer->sin_addr.s_addr |= ~(ifo->iiNetmask.AddressIn.sin_addr.s_addr);
      peer->sin_port = htons(COBOX_UDP_PORT);
      peer->sin_family = AF_INET;
      return(1);
   }

   return(0);
}
