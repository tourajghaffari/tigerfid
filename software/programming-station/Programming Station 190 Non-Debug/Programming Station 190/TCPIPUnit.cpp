//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include <conio.h>
#include <sys/types.h>
#include <errno.h>
#include <time.h>
//#include <winsock2.h>
//#include <ws2tcpip.h>
#include <fcntl.h>

#include <math.h>

#include "TCPIPUnit.h"
#include "ProgStationUnit.h"
#include "ComConfigPage.h"
#include "commands.h"

#define SOCKBUFSIZE 256

//char pktBuff[20][64];
//unsigned int readIndex;
//unsigned int parseIndex;
extern char recvBuf[260];
extern SOCKET activeSock[MAX_DESCRIPTOR][2];
sockaddr_in* peerSock[MAX_DESCRIPTOR];
sockaddr_in* peerSockSingle;
//LTX_SOCKET open_socket(char *hostname, int port, int type, struct sockaddr_in *peer);
//void ltx_closesocket(LTX_SOCKET sock);
int main_loop(LTX_SOCKET sock, struct sockaddr_in *from, int encryption);
//int read_socket(LTX_SOCKET sock, char *rbuf, int len, struct sockaddr_in *from);
//int write_socket(LTX_SOCKET sock, char *sbuf, int len, struct sockaddr_in *to);

int tcp_encrypt(char *plaintext, int len, char *ciphertext, int method, int indx);
int udp_encrypt(char *plaintext, int len, char *ciphertext, int method);
int tcp_decrypt(char *ciphertext, int len, char *plaintext, int method, int indx);
int udp_decrypt(char *ciphertext, int len, char *plaintext, int method);

//int  serial_read (char *plaintext, int len);
//void serial_write(char *plaintext, int len);
//void trace(char *txt);

unsigned char key[16] = { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 
						  0x08, 0x09, 0x0a, 0x0b, 0x0c, 0x0d, 0x0e, 0x0f }; /* Encryption key */
int (*blockEncrypt)(char *iv, char *key, int bytes, char *text, char method);				/* Pointer to VC_blockEncrypt() function */
int (*ltx_encrypt)(char *plaintext, int len, char *ciphertext, int method, int indx);				/* Pointer to encryption method routine () */
int (*ltx_decrypt)(char *ciphertext, int len, char *plaintext, int method, int indx);				/* Pointer to decryption method routine () */
char ive[16], ivd[16];				//Pointer to initial
char pstate=0;
char pstatd=0;				//Encryption and decryption state variable
char shared_key[33];				// 2 byte version of key
static HANDLE hCommThread;
WSADATA  info;   // WinSock info handle
HINSTANCE hDLL;  // cbx_enc.dll handle id
int encryption;
LTX_SOCKET sock, lsock;	    // Socket ids
struct sockaddr_in peer;    // Network peer socket information
void __fastcall Generate_CRC(char c);
bool CheckCRC(unsigned int PktLen, char* buf);
int crc = 0xFFFF;
extern bool tcipWindow;
extern char ipAddr[MAX_DESCRIPTOR][20];
extern int numIpSelected;
extern AnsiString comConnectStr;
extern bool comPortOK;
extern struct networkInfoStruct *networkInfo;
extern unsigned short int numOpenedSocket;
extern bool RS232On;
extern bool networkOn;

extern TCommConfigDlg *comConfigDialog;

int tcp_decrypt_me(char *ciphertext, int len, char *plaintext, int method);
int tcp_decrypt_me(char *ciphertext, int len, char *plaintext, int method);
//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma resource "*.dfm"
TTCPIPForm *TCPIPForm;
DWORD FAR PASCAL CommThread (LPSTR );
//---------------------------------------------------------------------------
__fastcall TTCPIPForm::TTCPIPForm(TComponent* Owner)
        : TForm(Owner)
{
   TCPIPForm = this;
   tcipWindow = true;
   commThreadRunning = false;
}
//---------------------------------------------------------------------------
bool __fastcall TTCPIPForm::Startup(int encrypt, int type, int port)
{
   int ret, i, p;
   AnsiString s, s1;
   AnsiString str;

   if ((ret = WSAStartup(MAKEWORD(1,1), &info)) < 0 )
   {
      //fprintf(stderr, "Error on WSAStartup(), error (%d)\n", i);
      Application->MessageBox("Error", "Error on WSAStartup()", MB_OK);
      return (false);
   }

   /*if ((sock = open_socket("192.168.1.200", 10001, type, &peer)) <= 0) {
      WSACleanup();
      return(false);
   }*/

   ///////////////////////////////////////////////////////////////
   //char a[7];
   //char b[7];
   //memset (b, 0, 7);
   //a[0] = 0x01;
   //a[1] = 0x02;
   //a[2] = 0x03;
   //a[3] = 0x04;
   //a[4] = 0x05;
   //a[5] = 0x06;
   //a[6] = 0x07;
   //////////////////////////////////////////////////////////////////
   //*peerSock = (sockaddr_in*) malloc(sizeof(sockaddr_in)*numIpSelected);
   for (int i=0; i<numIpSelected; i++)
   {
       peerSock[i] = new (sockaddr_in);
       sock = open_socket(&ipAddr[i][0], port, SOCK_STREAM, peerSock[i]);
       if (sock <= 0)
       {
           s = "Error opening socket IP=";
           //strcpy(s1.c_str(), &ipAddr[i][0]);
           s += &ipAddr[i][0];
           Application->MessageBox(s.c_str(), "Error", MB_OK);
           //Application->MessageBox(&ipAddr[i][0], "Error", MB_OK);
           //comConnectStr = "Network Connection Failed";
           //MainStatusBar->Panels->Items[2]->Text =  "Failed Network Connection";
           //MainStatusBar->Panels->Items[1]->Text =  " ";
           //sysStr = " ";
           //MainStatusBar->Panels->Items[0]->Text =  " ";
           //comPortOK = false;
           //return (false);
       }
       else
       {
         p = GetNextValidIndex();
         networkInfo[p].validRec = false;  //true when powered up
         networkInfo[p].activeSock = sock;
         networkInfo[p].peerSock = peerSock[i];
         networkInfo[p].pstate = 0;
         networkInfo[p].reader = 0;
         networkInfo[p].host = 0;
         strcpy(networkInfo[p].status, "Offline");
         networkInfo[p].active = true;
         strcpy(networkInfo[p].ipAddr, &ipAddr[i][0]);
         numOpenedSocket += 1;
         comConfigDialog->UpdateSocketStatus(p);

         str = "IP = ";
         str += networkInfo[i].ipAddr;
         str += "  was connected.";
         comConfigDialog->ConnectLabel->Caption = str;
         comConfigDialog->ConnectLabel->Update();
         Sleep(400);
       }
   }

   if (numOpenedSocket == 0)
   {
      Application->MessageBox("Network Connection Failed.", "Error", MB_OK);
      comConnectStr = "Network Connection Failed.";
      comPortOK = false;
      WSACleanup();
      ltx_closesocket(NULL, true);
   }
   //else
      //comConfigDialog->ResetReadersBitBtn->Enabled = true;

   hDLL = 0;
   encryption = encrypt;

   // Initialize all the encryption requirements
   if (encryption)
   {
      // Load the DLL
      hDLL = LoadLibrary("cbx_enc");
      if (hDLL != NULL)
      {
         // Find the VC_blockEncrypt() function
         blockEncrypt = (int (*)(char *iv, char *key, int bytes, char *text, char method)) GetProcAddress(hDLL, "VC_blockEncrypt");
         if (!blockEncrypt)
         {
            FreeLibrary(hDLL);
            return(false);
         }
      }

      // Initialize the encode and decode states
      pstate = pstatd = 0;

      int i, j;
      // Convert HEX key to 2 byte hex sequence
      for (i = 0, j = 0; i < sizeof(key); i++, j+= 2)
      {
         sprintf(&shared_key[j], "%.2X", key[i]);
      }

      if (type == SOCK_STREAM)
      {
         // Initialize the encode and decode states
         //pstate = pstatd = 0;
         //if (peer.sin_addr.S_un.S_addr != 0) // active connect
         for (int g=0; g<numOpenedSocket; g++)
         {
            if (networkInfo[g].peerSock->sin_addr.S_un.S_addr != 0) // active connect
            {
               // Initialize the encode and decode Initialization Vectors
               srand( (unsigned)time(NULL));
               for (i = 0; i < 16; i++)
                  ive[i] = ivd[i] = rand();

               strncpy(networkInfo[g].initVectorEncrypt, ive, 16);
               strncpy(networkInfo[g].initVectorDecrypt, ive, 16);

               encryption = 0;
               // Send IV
               if ((write_socket(networkInfo[g].activeSock, networkInfo[g].initVectorEncrypt, sizeof(ive), (struct sockaddr_in *) networkInfo[g].peerSock, -1)) != sizeof(ive))
               {
                  //trace("Writing IV failed\n");
                  Application->MessageBox("Error", "Writing IV failed", MB_OK);
                  return (false);
               }
            }
         }//for

         encryption = 2;

         // Set the encrypt and decrypt TCP routines
         ltx_encrypt = tcp_encrypt;
         ltx_decrypt = tcp_decrypt;
      }
      /*else
      {
         // Set the encrypt and decrypt UDP routines
         ltx_encrypt = udp_encrypt;
         ltx_decrypt = udp_decrypt;
      }*/
   }

   networkOn = true;

   ////////////////////////////////////
   //ltx_encrypt(a, 6, b, 2, 0);
   //ltx_decrypt(b, 6, a, 2, 0);

   ///////////////////////////////////

   DWORD dwThreadID;
   if (!commThreadRunning)
   {
      if (NULL == (hCommThread = CreateThread( (LPSECURITY_ATTRIBUTES) NULL,
                0,
                (LPTHREAD_START_ROUTINE) CommThread,
                (LPVOID) NULL,
                0, &dwThreadID )))
      {
          ltx_closesocket(NULL, true);
          commThreadRunning = false;
      }
      else
         commThreadRunning = true;
   }

   /*if ( (peer.sin_addr.S_un.S_addr == 0) && (type == SOCK_STREAM) )  // passive listen
   {
     do   // wait for connect
     {
        i = sizeof(peer);
        if ((lsock = accept(sock, (struct sockaddr *) &peer, &i)) < 0)
        {
           //trace("accept failed\n");
           Application->MessageBox("Error", "accept failed", MB_OK);
           peer.sin_addr.S_un.S_addr = 0;
        }
        else
        {
           if (encryption)
           {
              // Read the IV from the peer
              if ((read_socket(lsock, ivd, 16, &peer)) != 16)
              {
                 ;//trace("Reading IV failed\n");
              }
              memcpy(ive, ivd, 16);
           }

           // Main loop dance
           while ((main_loop(lsock, &peer, encryption)) > 0);
              ltx_closesocket(lsock);
        }
     } while (peer.sin_addr.S_un.S_addr != 0);
   }
   else
   {
     // Main loop dance
      while ((main_loop(sock, &peer, encryption)) > 0);
   }

   // Shutdown and close the socket
   ltx_closesocket(sock);
   // Clean up the WinSock resources
   WSACleanup();
   // Free the DLL
   if (encryption && hDLL)
      FreeLibrary(hDLL);
   // Done
   */
   return(true);
}
//------------------------------------------------------------------------------
int main_loop(LTX_SOCKET sock, struct sockaddr_in *from, int encryption)
{
   /*int timeout, in, out, toget, pktLen;
   fd_set rfds, efds;
   struct timeval timev;
   char *buf;
   //char plaintext[SOCKBUFSIZE];	     // Plain text
   //char *plaintext;
   char ciphertext[SOCKBUFSIZE+34];  // Encrypted text. Round up to hold blocks(16), IV(16) and length(2)

   if (encryption)
   {
      buf = ciphertext;
      toget = sizeof(ciphertext);
   }
   else
   {
      //plaintext = recvBuf;
      buf = recvBuf;
      toget = sizeof(SOCKBUFSIZE);
   }

   // main loop
   while (1)
   {
      FD_ZERO(&rfds);
      FD_ZERO(&efds);
      FD_SET(sock, &rfds);
      FD_SET(sock, &efds);
      // Set timeout to 100mS.  This timeout will affect serial polling frequency
      timev.tv_sec = 0;
      timev.tv_usec = 100000;
      timeout = select(FD_SETSIZE, &rfds, (fd_set *) NULL, &efds, &timev);
      if (timeout == -1)
      {
         // OS interrupt during select - should be ignored
         // return(timeout);
      }
      else if (timeout == 0)
      {
         // Real timeout - do nothing
         // return(0);
      }
      else if (FD_ISSET(sock, &efds))
      {
         // Bad news, exception on socket - needs to be closed and reopened
         return(-2);
      }
      else if (FD_ISSET(sock, &rfds))
      {
         // Received socket data input, decrypt it if required, and write it to serial output
         if ((in = TCPIPForm->read_socket(sock, buf, toget, from)) > 0)
         {
            if (encryption)
               in = ltx_decrypt(ciphertext, in, recvBuf, encryption, 0);
            if (in)
            {
               if (recvBuf[0] == 0x7E)
               {
                  pktLen = recvBuf[2];
                  if (CheckCRC(pktLen+5, recvBuf))
                  {
                     TCPIPForm->serial_write(recvBuf, in);
                     ProgStationForm->DisplayRecPackets(recvBuf, pktLen+5, false, false);
                     ProgStationForm->PacketParser(pktLen+5, 0);
                  }
               }
            }
         }
         else if (in < 0)
         {
            // Error reading from socket
            return(-3);
         }
         else
            return(0);
      }

      // Check for serial input, encrypt it if required, and send it
      /*if ((in = serial_read(plaintext, sizeof(plaintext))) > 0)
      {
         if (encryption)
            in = ltx_encrypt(plaintext, in, ciphertext, encryption);
         if ((out =  write_socket(sock, buf, in, (struct sockaddr_in *) from)) < in)
         {
            // Socket write error
            return(-4);
         }
      }
   }*/
   return(0);
}

LTX_SOCKET __fastcall TTCPIPForm::open_socket(char *hostname, int port, int type, struct sockaddr_in *peer)
{
   LTX_SOCKET sock;
   struct hostent *hdata;
   unsigned long ipaddr;

   //  create the socket
   if ((sock = socket(AF_INET, type, 0)) < 0)
   {
      //trace("Error creating socket\n");
      Application->MessageBox("Error", "Error creating socket", MB_OK);
      return(0);
   }

   //setting to non block - did not work - connect function fails
   //-------------------------------------------------------------
   //works best - first option
   //int iMode = -1;
   //int r1 = ioctlsocket(sock, FIONBIO, (u_long FAR*) &iMode);

   //second option
   //unsigned int wMsg;
   //int rc = WSAAsyncSelect(sock, this->Handle, wMsg, FD_CONNECT);
   //---------------------------------------------------------------


   // Find the IP address and convert to 4 byte int
   if (isdigit(hostname[0]))  // dot decimal notation?
   {
      if ((ipaddr = inet_addr(hostname)) == INADDR_NONE )
      {
         // inet_addr failed
         //trace("inet_addr failed\n");
         ltx_closesocket(sock, false);
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
        //trace("Can't resolve hostname\n");
        ltx_closesocket(sock, false);
        return(0);
      }
      memcpy (&peer->sin_addr, *(hdata->h_addr_list), hdata->h_length);
   }

   // Complete initialization of peer data structure
   peer->sin_family = AF_INET;
   peer->sin_port = htons((unsigned short) port);

   if (peer->sin_addr.S_un.S_addr == 0)  // Wait for incoming
   {
      // bind struct the port
      if ((bind(sock, (struct sockaddr *) peer, sizeof(struct sockaddr_in))) < 0)
      {
         //trace("Bind error\n");
         closesocket(sock);
         return(-1);
      }
   }
		
   if (type == SOCK_DGRAM) // (UDP) we're done
		return(sock);

   // Must be TCP
   if (peer->sin_addr.S_un.S_addr != 0)  // active connect
   {
      // Let's connect to the peer
      if ((connect(sock, (struct sockaddr *) peer, sizeof(struct sockaddr_in))) != 0)
      {
         // Connection failed
         //trace("Connection failed\n");
         int k = WSAGetLastError();
         shutdown(sock, 2);
         ltx_closesocket(sock, false);
         return(0);
      }
      // Return socket id
      return(sock);
   }

   // Must be passive listen
   // Setup the listen queue
   if ((listen(sock, 5)) != 0)
   {
      //trace("listen failure");
      shutdown(sock, 2);
      closesocket(sock);
      return(-1);
   }

   return(sock);
}

void __fastcall TTCPIPForm::ltx_closesocket(LTX_SOCKET sock, bool all)
{
   if (all)
   {
      //for (int i=0; i<numOpenedSocket; i++)
      for (int i=0; i<2; i++)
      {
         shutdown(networkInfo[i].activeSock, 2);
         closesocket(networkInfo[i].activeSock);
         networkInfo[i].validRec = false;
      }

      for (int i=0; i<numIpSelected; i++)
         delete peerSock[i];
   }
   else
   {
      // Disable further reads and writes
      //shutdown(sock, 2);
      // Close it
      //closesocket(sock);
   }
}

int __fastcall TTCPIPForm::read_socket(LTX_SOCKET sock, char *rbuf, int len, struct sockaddr_in *from)
{
   int in, fromlen;

	fromlen = sizeof(struct sockaddr_in);
	if ((in = recvfrom(sock, rbuf, len, 0, (struct sockaddr *) from, &fromlen)) < 0) {
          //trace("recvfrom failed\n");
	}
	return(in);
}

int __fastcall TTCPIPForm::write_socket(LTX_SOCKET sock, char *sbuf, int len, struct sockaddr_in *to, int indx)
{
	int n, sent, tosend;
        char encryptedtext[260];

        char decryptedtext[260];
        int len1 = 5;

	if (to->sin_addr.S_un.S_addr == 0)
		return(0);

        if (encryption)
        {
           len = ltx_encrypt(sbuf, len, encryptedtext, 2, indx);
        }

	sent = 0;
	tosend = len;
			/* Loop until all data is sent, or error condition */
	while(tosend){
                if (encryption)
                   n = sendto(sock, (char *) &encryptedtext[sent], tosend, 0, (struct sockaddr *) to, sizeof (struct sockaddr));
                else
		   n = sendto(sock, (char *) &sbuf[sent], tosend, 0, (struct sockaddr *) to, sizeof (struct sockaddr));
		if (n <= 0) {
			//trace("sendto failed\n");
			return(sent);
		}
		else {
			sent += n;
			tosend -= n;
		}
	}

        if (encryption)
        {
           ProgStationForm->DisplayTransmitPackets(encryptedtext, sent, NULL);

           len1 = tcp_decrypt_me(encryptedtext, len, decryptedtext, encryption);
           ProgStationForm->DisplayTransmitPackets(decryptedtext, len1, NULL);
        }
	return(sent);
}

int udp_encrypt(char *plaintext, int len, char *ciphertext, int method)
{
	int i, newlen;

			/* Seed random number generator */
	srand( (unsigned)time(NULL));
			/* Fill Initialization Vector with random data and copy 
					it into the UDP payload */
	for (i = 0; i < 16; i++)
	    ive[i] = ciphertext[i] = rand();
			/* Add the data length to the payload */
	ciphertext[16] = len >> 8;
	ciphertext[17] = len & 0xff;
			/* Copy the plaintext into the payload because the
				encoder will perform the transformation in place */
	memcpy(&ciphertext[18], plaintext, len);
			/* Round up the length to a multiple of 16 (bytes per block) */
	newlen = ((len + 15) / 16) * 16;
			/* Pad blocks with random data */
	for (i = len; i < newlen; i++)
		ciphertext[i+18] = rand();
			/* CBC encrypt */
	blockEncrypt(ive, shared_key, newlen, &ciphertext[18], (method - 1));
			/* Return the payload length */
	return(newlen + 18);
}

int udp_decrypt(char *ciphertext, int len, char *plaintext, int method)
{
	int dlen;

			/* Do we have the Initialization Vector and length? */
	if (len < 18) return (0);
			/* Extract the payload Initialization Vector */
	memcpy(ivd, ciphertext, 16);
			/* Extract the payload data length */
	dlen = (((ciphertext[16] << 8) + ciphertext[17] + 15) / 16) * 16;
			/* Do we have a complete payload? */
	if (len < (dlen + 18)) return (0);
			/* Extract the payload real data length */
	len = (ciphertext[16] << 8) + ciphertext[17];
			/* Extract the ciphertext into the destination array because the
				decoder will perform the transformation in place */
	memcpy(plaintext, &ciphertext[18], dlen);
			/* Decrypt it by negating the encrypted data length */
	blockEncrypt(ivd, shared_key, -dlen, plaintext, (method - 1));
			/* Return the length of the real decrypted data */
	return(len);
}

int tcp_encrypt(char *plaintext, int len, char *ciphertext, int method, int indx)
{
		int i;

		for (i = 0; i < len; i++) {
			if (networkInfo[indx].pstate == 0) {
				blockEncrypt(networkInfo[indx].initVectorEncrypt, shared_key, 0, networkInfo[indx].initVectorEncrypt, (method - 1));
				networkInfo[indx].pstate = 16;
			}
			networkInfo[indx].initVectorEncrypt[16 - networkInfo[indx].pstate] ^= plaintext[i];
        	ciphertext[i] = networkInfo[indx].initVectorEncrypt[16 - networkInfo[indx].pstate];
        	networkInfo[indx].pstate--;
		}
		return(len);
}

int tcp_decrypt(char *ciphertext, int len, char *plaintext, int method, int indx)
{
		int i;

		for (i = 0; i < len; i++) {
			if (networkInfo[indx].pstatd == 0) {
				blockEncrypt(networkInfo[indx].initVectorDecrypt, shared_key, 0, networkInfo[indx].initVectorDecrypt, (method - 1));
				networkInfo[indx].pstatd = 16;
			}
			plaintext[i] = networkInfo[indx].initVectorDecrypt[16 - networkInfo[indx].pstatd] ^ ciphertext[i];
        	networkInfo[indx].initVectorDecrypt[16 - networkInfo[indx].pstatd] = ciphertext[i];
        	networkInfo[indx].pstatd--;
		}
		return(len);
}

void __fastcall TTCPIPForm::serial_write(char *plaintext, int len)
{
		int i;
                AnsiString s, s1;

                if (len > 260)
                   len = 260;
		for (i = 0; i < len; i++) {
			s = s.sprintf("%2x ", plaintext[i]);
                        s1 += s;
		}

                ListBox1->Items->Insert(0, s1);
                ListBox1->Refresh();

}
void __fastcall TTCPIPForm::FormActivate(TObject *Sender)
{
   //Startup(2, SOCK_STREAM);
}
//---------------------------------------------------------------------------
void __fastcall TTCPIPForm::FormClose(TObject *Sender,
      TCloseAction &Action)
{
    ltx_closesocket(NULL, true);
    tcipWindow = false;
}
//---------------------------------------------------------------------------
DWORD FAR PASCAL CommThread(LPSTR lpData)
{
   //int i;

   //if ( (peer.sin_addr.S_un.S_addr == 0) && (type == SOCK_STREAM) )  // passive listen
   /*if (peer.sin_addr.S_un.S_addr == 0) // && (type == SOCK_STREAM) )  // passive listen
   {
     do   // wait for connect
     {
        i = sizeof(peer);
        if ((lsock = accept(sock, (struct sockaddr *) &peer, &i)) < 0)
        {
           //trace("accept failed\n");
           Application->MessageBox("Error", "accept failed", MB_OK);
           peer.sin_addr.S_un.S_addr = 0;
        }
        else
        {
           if (encryption)
           {
              // Read the IV from the peer
              if ((TCPIPForm->read_socket(lsock, ivd, 16, &peer)) != 16)
              {
                 ;//trace("Reading IV failed\n");
              }
              memcpy(ive, ivd, 16);
           }

           // Main loop dance
           while ((main_loop(lsock, &peer, encryption)) > 0);
              TCPIPForm->ltx_closesocket(lsock);
        //}
     } while (peer.sin_addr.S_un.S_addr != 0);
   }
   else
   {
     // Main loop dance
      while ((main_loop(sock, &peer, encryption)) > 0);
   }
   */

   //while ((main_loop(sock, &peer, encryption)) > 0);

   //////////////////////////
   int timeout, in, out, toget;
   UINT pktLen;
   fd_set rfds, wfds, efds;
   struct timeval timev;
   char *buf;
   int loop;
   //char plaintext[SOCKBUFSIZE];	     // Plain text
   //char *plaintext;
   char ciphertext[SOCKBUFSIZE+34];  // Encrypted text. Round up to hold blocks(16), IV(16) and length(2)

   if (encryption)
   {
      buf = ciphertext;
      toget = sizeof(ciphertext);
   }
   else
   {
      //plaintext = recvBuf;
      buf = recvBuf;
      toget = 255;
      //toget = sizeof(SOCKBUFSIZE);
   }

   // main loop
   while (1)
   {
      FD_ZERO(&rfds);
      FD_ZERO(&wfds);
      FD_ZERO(&efds);

      //FD_SET(sock, &rfds);
      //FD_SET(sock, &efds);

      for (loop=0; loop<numOpenedSocket; loop++)
      {
         FD_SET(networkInfo[loop].activeSock, &rfds);
         FD_SET(networkInfo[loop].activeSock, &wfds);
         FD_SET(networkInfo[loop].activeSock, &efds);
      }

      // Set timeout to 100mS.  This timeout will affect serial polling frequency
      timev.tv_sec = 0;
      timev.tv_usec = 100000;
      timeout = select(FD_SETSIZE, &rfds, (fd_set *) NULL, &efds, &timev);
      if (timeout == -1)
      {
         // OS interrupt during select - should be ignored */
         // return(timeout);
      }
      else if (timeout == 0)
      {
         // Real timeout - do nothing */
         // return(0);
      }
      else if (FD_ISSET(sock, &efds))
      {
         // Bad news, exception on socket - needs to be closed and reopened
         return(-2);
      }

      loop = 0;
      for (loop=0; loop<numOpenedSocket; loop++)
      {
          if (FD_ISSET(networkInfo[loop].activeSock, &rfds))
          {
          // Received socket data input, decrypt it if required, and write it to serial output
          if ((in = TCPIPForm->read_socket(networkInfo[loop].activeSock, buf, toget, networkInfo[loop].peerSock)) > 0)
          {

            if (encryption)
            {
               in = ltx_decrypt(ciphertext, in, recvBuf, encryption, loop);
            }
            if (in)
            {
               if (recvBuf[0] == 0x7E)
               {
                  pktLen = recvBuf[2];
                  if (CheckCRC(pktLen+5, recvBuf))
                  {
                      if (recvBuf[1] == POWER_UP)
                      {
                         if ((recvBuf[3] & 0x38) == 0x30)  //exteded reader
                         {
                            //networkInfo[loop].activeSock[1] = (unsigned int)(unsigned char)recvBuf[4]*pow(2, 8)+(unsigned int)(unsigned char)recvBuf[5];  //reader ID
                            networkInfo[loop].reader = (unsigned int)(unsigned char)recvBuf[4]*pow(2, 8)+(unsigned int)(unsigned char)recvBuf[5];
                            networkInfo[loop].host = (int)recvBuf[6];
                         }
                         else
                         {
                             //networkInfo[loop].activeSock[1] = (int)recvBuf[4];
                             networkInfo[loop].reader = (int)recvBuf[4];
                             networkInfo[loop].host = (int)recvBuf[5];
                          }

                          strcpy(networkInfo[loop].status, "Online");
                          //networkInfo[loop].activeSock[2] = 1;  //set flag when populated
                          networkInfo[loop].validRec = true;  //set flag when populated

                          if (comConfigDialog != NULL)
                             comConfigDialog->UpdateIPListViewPowerup(loop);
                     }
                     
                     //TCPIPForm->serial_write(recvBuf, in);
                     ProgStationForm->DisplayRecPackets(recvBuf, pktLen+5, false, false, networkInfo[loop].ipAddr);
                     ProgStationForm->PacketParser(pktLen+5, loop);
                  }
               }
            }
         }
         else if (in < 0)
         {
            // Error reading from socket
            return(-3);
         }
         else
            return(0);
      }

      // Check for serial input, encrypt it if required, and send it
      /*if ((in = serial_read(plaintext, sizeof(plaintext))) > 0)
      {
         if (encryption)
            in = ltx_encrypt(plaintext, in, ciphertext, encryption);
         if ((out =  write_socket(sock, buf, in, (struct sockaddr_in *) from)) < in)
         {
            // Socket write error
            return(-4);
         }
      }*/

      } //for loop
   } //while loop

   TCPIPForm->ltx_closesocket(NULL, true);
   // Clean up the WinSock resources
   WSACleanup();
   // Free the DLL
   if (encryption && hDLL)
      FreeLibrary(hDLL);
   return(0);

   ///////////////////////////

   // Shutdown and close the socket
   //TCPIPForm->ltx_closesocket(NULL, true);
   // Clean up the WinSock resources
   //WSACleanup();
   // Free the DLL
   //if (encryption && hDLL)
      //FreeLibrary(hDLL);
   // Done
   //return(0);
}
//---------------------------------------------------------------------------
 bool CheckCRC(unsigned int PktLen, char* buf)
{
   if (PktLen > 2)
   {
      crc = 0xFFFF;
      for (int i=0; i<(int)PktLen-2; i++)
         Generate_CRC(buf[i]);
      char c0 = (char)(crc & 0x00FF) ^ 0xFF;  //LSB
      char c1 = (char)((crc >> 8) & 0x00FF) ^ 0xFF;  //MSB
      if ((buf[PktLen-1] == c1) && (buf[PktLen-2] == c0))
         return(true);
   }
   return (false);
}
//------------------------------------------------------------------------------
//------------------------------------------------------------------------------
void __fastcall Generate_CRC(char c)
{
   unsigned char d, e, f, g, h;
   d = 0;
   e = (char)(crc & 0x00FF) ^ c;
   for(h=0; h<8; h++)   //for all 8 bits
   {
      g = d & 0x01;
      d >>= 1;
      d &= 0x7F;
      f = e & 0x01;
      e >>= 1;
      e &= 0x7F;
      if (g) e |= 0x80;
      if (f)
      {
         d ^= 0x84;
         e ^= 0x08;
      } //end if carry out of e
   }// end for
   e ^= (char)((crc >> 8) & 0x00FF);  //new crc lo
   crc = (unsigned int)(( d << 8) & 0xFF00) + (unsigned int)e;
}

int tcp_decrypt_me(char *ciphertext, int len, char *plaintext, int method)
{
		int i;

		for (i = 0; i < len; i++) {
			if (pstatd == 0) {
				blockEncrypt(ivd, shared_key, 0, ivd, (method - 1));
				pstatd = 16;
			}
			plaintext[i] = ivd[16 - pstatd] ^ ciphertext[i];
        	ivd[16 - pstatd] = ciphertext[i];
        	pstatd--;
		}
		return(len);
}

int tcp_encrypt_me(char *plaintext, int len, char *ciphertext, int method)
{
		int i;

		for (i = 0; i < len; i++) {
			if (pstate == 0) {
				blockEncrypt(ive, shared_key, 0, ive, (method - 1));
				pstate = 16;
			}
			ive[16 - pstate] ^= plaintext[i];
        	ciphertext[i] = ive[16 - pstate];
        	pstate--;
		}
		return(len);
}
//------------------------------------------------------------------------------
bool __fastcall TTCPIPForm::ConnectSingleIP(AnsiString ip, int encrypt, int type, int port)
{
   int ret;
   AnsiString s;

   if ((ret = WSAStartup(MAKEWORD(1,1), &info)) < 0 )
   {
      Application->MessageBox("Error", "Error on WSAStartup()", MB_OK);
      return (false);
   }

   peerSockSingle = new (sockaddr_in);
   sock = open_socket(ip.c_str(), port, SOCK_STREAM, peerSockSingle);
   if (sock <= 0)
   {
      s = "Error opening socket IP=";
      s += ip;
      Application->MessageBox(s.c_str(), "Error", MB_OK);
      return (false);
   }
   else
   {
      //int optLen;
      //char optVal[10];
      //int r = getsockopt(sock, SOL_SOCKET, SO_DONTLINGER, optVal, &optLen);
      //r = setsockopt(sock, SOL_SOCKET, SO_DONTLINGER, );

      networkInfo[numOpenedSocket].validRec = false;   //true when powered up
      networkInfo[numOpenedSocket].activeSock = sock;
      networkInfo[numOpenedSocket].peerSock = peerSockSingle;
      networkInfo[numOpenedSocket].pstate = 0;
      networkInfo[numOpenedSocket].reader = 0;
      networkInfo[numOpenedSocket].host = 0;
      strcpy(networkInfo[numOpenedSocket].status, "Offline");
      networkInfo[numOpenedSocket].active = true;
      strcpy(networkInfo[numOpenedSocket].ipAddr, ip.c_str());
      comConfigDialog->AddConnectedIpToActiveList(ip, numOpenedSocket);
      strcpy(&ipAddr[0][0], ip.c_str());
      numIpSelected = 1;
      numOpenedSocket += 1;
   }

   DWORD dwThreadID;
   if (!commThreadRunning)
   {
      if (NULL == (hCommThread = CreateThread( (LPSECURITY_ATTRIBUTES) NULL,
                0,
                (LPTHREAD_START_ROUTINE) CommThread,
                (LPVOID) NULL,
                0, &dwThreadID )))
      {
          ltx_closesocket(NULL, true);
          commThreadRunning = false;
      }
      else
         commThreadRunning = true;

      networkOn = true;
   }
   return (true);
}
//------------------------------------------------------------------------------
int __fastcall TTCPIPForm::GetNextValidIndex()
{
   for (int i=0; i<MAX_DESCRIPTOR; i++)
   {
      if (networkInfo[i].active == false)
         return i;
   }

   return -1;
}
