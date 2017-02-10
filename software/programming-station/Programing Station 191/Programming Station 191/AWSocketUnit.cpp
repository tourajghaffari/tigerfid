//---------------------------------------------------------------------------

#include <vcl.h>
#include <math.h>
#include "ProgStationUnit.h"
#include "ComConfigPage.h"
#pragma hdrstop

#include "AWSocketUnit.h"
#include "Commands.h"

//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma resource "*.dfm"
TTAWSocket *TAWSocket;
extern char recvBuf[MAX_DESCRIPTOR];
extern unsigned int pktCounter;
extern AnsiString comStatusStr;
extern TCommConfigDlg *comConfigDialog;
extern bool networkOffline;
//---------------------------------------------------------------------------
__fastcall TTAWSocket::TTAWSocket(TComponent* Owner)
        : TForm(Owner)
{
   TAWSocket = this;
}
//---------------------------------------------------------------------------
void __fastcall TTAWSocket::AWClientSocketRead(TObject *Sender,
                                               TCustomWinSocket *Socket)
{
   char buf[255];
   int bytes = 0;
   AnsiString ip;
   AnsiString s;
   int indx;
   int reader;
   int host;

   if (index < 0)
   {
      if (comConfigDialog)
      {
         s = "Index Error for IP = ";
         s += AWClientSocket->Address;
         comConfigDialog->Msg->Caption = s;
         return;
      }
   }

   int n = Socket->ReceiveLength();
   while (n > 32)
   {
      bytes = Socket->ReceiveBuf((char *)buf, 255);
      n -= bytes;
   }

   if (n > 0)
   {
      bytes = Socket->ReceiveBuf((char *)recvBuf, n);
      if (ProgStationForm->displayRx)
      {
         pktCounter++;
         ProgStationForm->DisplayRecPackets(recvBuf, n, false, false, AWClientSocket->Address);
      }

      if (recvBuf[1] == POWER_UP)
      {
         if ((recvBuf[3] & 0x38) == 0x30)  //exteded reader
         {
            reader = (unsigned int)(unsigned char)recvBuf[4]*pow(2, 8)+(unsigned int)(unsigned char)recvBuf[5];
            host = (unsigned char)recvBuf[6];
         }
         else
         {
            reader = (unsigned char)recvBuf[4];
            host = (unsigned char)recvBuf[5];
         }

         indx = ProgStationForm->GetIpAddressIndex(AWClientSocket->Address);
         if (indx >= 0)
         {
            strcpy(ProgStationForm->listViewInfo[indx].rdrStatus, "Online");
            ProgStationForm->listViewInfo[indx].reader = reader;
            ProgStationForm->listViewInfo[indx].host = host;
            ProgStationForm->listViewInfo[indx].selected = true;
            if (comConfigDialog)
               comConfigDialog->UpdateIPListView();
         }

      }//if power-up
      ProgStationForm->PacketParser(bytes, index);
   }
}
//---------------------------------------------------------------------------
void __fastcall TTAWSocket::AWClientSocketError(TObject *Sender,
                                                TCustomWinSocket *Socket,
                                                TErrorEvent ErrorEvent,
                                                int &ErrorCode)
{
   AnsiString s;
   AnsiString ip;

   if (ErrorEvent == eeConnect)
   {
      //index = ((TComponent *)Sender)->Tag;

      if (comConfigDialog)
      {
         s = "IP = ";
         s += ip;
         s += " failed to connect.";
         comConfigDialog->Msg->Caption = s;
      }
   }
   else if (ErrorEvent == eeDisconnect)
   {
      //index = ((TComponent *)Sender)->Tag;

      s = "IP = ";
      s += ip;
      s += " got disconnected. ";
      TDateTime time = Now();
      s += time.DateTimeString();

      if (comConfigDialog)
      {
         comConfigDialog->Msg->Caption = s;
         //ProgStationForm->TempStaticText->Caption = s;   //this is for test and sould be removed
      }
      //else
         //ProgStationForm->TempStaticText->Caption = s;   //this is for test and sould be removed

   }

   ErrorCode = 0;
}
//---------------------------------------------------------------------------
bool __fastcall TTAWSocket::WriteSocket(unsigned char* xbuf, int len)
{
    int k = AWClientSocket->Socket->SendBuf(xbuf, len);

    if (ProgStationForm->displayTx)
    {
       pktCounter++;
       ProgStationForm->DisplayTransmitPackets(xbuf, len, AWClientSocket->Address);
    }

    if ((ProgStationForm->fileHandle != NULL) && ProgStationForm->recording)
    {
       ProgStationForm->txDebugStr = ProgStationForm->BuildTxRecordPktStr(xbuf, len);
       ProgStationForm->txDebugStr += "\n";
       fwrite(ProgStationForm->txDebugStr.c_str(), ProgStationForm->txDebugStr.Length(), 1, ProgStationForm->fileHandle);
    }

   if (k == len)
      return (true);
   else
      return (false);
}
//------------------------------------------------------------------------------
void __fastcall TTAWSocket::AWClientSocketConnect(TObject *Sender,
      TCustomWinSocket *Socket)
{
    AnsiString s;
    int indx, indx2;

    connected = true;
    //((TComponent *)Sender)->Tag;

    //get listview index for this ip
    indx = ProgStationForm->GetIpAddressIndex(AWClientSocket->Address);
    if (indx >= 0)
    {
       //get sock index for this ip
       if ((indx2=ProgStationForm->GetSocketIndex(AWClientSocket->Address)) >= 0)
          index = indx2;
       else
          return;
       strcpy(ProgStationForm->listViewInfo[indx].netStatus, "Active");
       ProgStationForm->listViewInfo[indx].selected = true;
    }
    else
       return;


    if (comConfigDialog)
    {
       comConfigDialog->UpdateIPListView();
       s = "Socket IP = ";
       s += AWClientSocket->Address;
       s += " connected.";
       comConfigDialog->Msg->Caption = s;
    }

    networkOffline = false;
    comStatusStr = "Connected To Network";
}
//------------------------------------------------------------------------------
void __fastcall TTAWSocket::AWClientSocketDisconnect(TObject *Sender,
      TCustomWinSocket *Socket)
{
    AnsiString s;
    int indx;

    connected = false;
    //((TComponent *)Sender)->Tag;

    indx = ProgStationForm->GetIpAddressIndex(AWClientSocket->Address);
    if (indx >= 0)
    {
       index = indx;
       strcpy(ProgStationForm->listViewInfo[indx].netStatus, "Inactive");
       strcpy(ProgStationForm->listViewInfo[indx].rdrStatus, "Offline");
    }
    else
       return;

    if (comConfigDialog)
    {
       if (!comConfigDialog->scanDisconnect)
       {
           comConfigDialog->UpdateIPListView();
           s = "Socket IP = ";
           s += AWClientSocket->Address;
           s += " disconnected.";
           comConfigDialog->Msg->Caption = s;
       }
    }
}
//---------------------------------------------------------------------------

