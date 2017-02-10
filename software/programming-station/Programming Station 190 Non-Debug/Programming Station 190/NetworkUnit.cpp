//---------------------------------------------------------------------------

#include <vcl.h>
#include "ProgStationUnit.h"
#pragma hdrstop

#include "NetworkUnit.h"

extern char recvBuf[260];
extern bool networkOn;
extern bool RS232On;

//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma resource "*.dfm"
TNetworkForm *NetworkForm;
//---------------------------------------------------------------------------
__fastcall TNetworkForm::TNetworkForm(TComponent* Owner)
        : TForm(Owner)
{
   NetworkForm = this;
}
//---------------------------------------------------------------------------
void __fastcall TNetworkForm::Button1Click(TObject *Sender)
{
   strMsg = "192.168.1.107   connected.";
   Label4->Caption = "";
   Label1->Caption = "";
   Sleep(300);
   ClientSocket1->Active = false;
   ClientSocket1->Address = "192.168.1.107";
   ClientSocket1->Active = true;
   networkOn = true;
   RS232On = false;
}
//---------------------------------------------------------------------------
void __fastcall TNetworkForm::Button2Click(TObject *Sender)
{
   strMsg = "192.168.1.109   connected.";
   Label4->Caption = "";
   Label1->Caption = "";
   Sleep(300);
   ClientSocket1->Active = false;
   ClientSocket1->Address = "192.168.1.109";
   ClientSocket1->Active = true;
   networkOn = true;
   RS232On = false;
}
//---------------------------------------------------------------------------
void __fastcall TNetworkForm::ClientSocket1Connect(TObject *Sender,
      TCustomWinSocket *Socket)
{
    Label1->Caption = strMsg;
}
//---------------------------------------------------------------------------
void __fastcall TNetworkForm::ClientSocket1Disconnect(TObject *Sender,
      TCustomWinSocket *Socket)
{
    Label1->Caption = strMsg;
}
//---------------------------------------------------------------------------
void __fastcall TNetworkForm::ClientSocket1Read(TObject *Sender,
      TCustomWinSocket *Socket)
{
   char buf[255];
   int bytes = 0;;
   int n = Socket->ReceiveLength();
   AnsiString str = "n = ";
   str += n;
   Label2->Caption = str;
   while (n > 32)
   {
      bytes = Socket->ReceiveBuf((char *)buf, 255);
      str = "Bytes Read = ";
      str += bytes;
      Label3->Caption = str;
      n -= bytes;
      str = "n = ";
      str += n;
      Label2->Caption = str;
   }

   if (n > 0)
   {
      bytes = Socket->ReceiveBuf((char *)recvBuf, n);
      ProgStationForm->PacketParser(bytes, 0);
   }
}
//---------------------------------------------------------------------------

void __fastcall TNetworkForm::ClientSocket1Error(TObject *Sender,
      TCustomWinSocket *Socket, TErrorEvent ErrorEvent, int &ErrorCode)
{
   AnsiString str = "Error Code = ";
   str += ErrorCode;
   Label4->Caption = str;
   ErrorCode = 0;

}
//---------------------------------------------------------------------------


void __fastcall TNetworkForm::Timer1Timer(TObject *Sender)
{
   if (once)
   {
      once = false;
   strMsg = "192.168.1.107   connected.";
   Label4->Caption = "";
   Label1->Caption = "";
   //Sleep(300);
   ClientSocket1->Active = false;
   ClientSocket1->Address = "192.168.1.107";
   ClientSocket1->Active = true;
   networkOn = true;
   RS232On = false;
   }
   else
   {
      once = true;
   strMsg = "192.168.1.109   connected.";
   Label4->Caption = "";
   Label1->Caption = "";
   //Sleep(300);
   ClientSocket1->Active = false;
   ClientSocket1->Address = "192.168.1.109";
   ClientSocket1->Active = true;
   networkOn = true;
   RS232On = false;
   }
}
//---------------------------------------------------------------------------

void __fastcall TNetworkForm::Button3Click(TObject *Sender)
{
   Timer1->Enabled = true;        
}
//---------------------------------------------------------------------------

void __fastcall TNetworkForm::FormClose(TObject *Sender,
      TCloseAction &Action)
{
   Timer1->Enabled = false;        
}
//---------------------------------------------------------------------------

void __fastcall TNetworkForm::Button4Click(TObject *Sender)
{
   unsigned char XBuf[10];
   XBuf[0] = 0x7E;
   XBuf[1] = 0x01;
   XBuf[2] = 0x03;
   XBuf[3] = 0x0A;
   XBuf[4] = 0x01;
   XBuf[5] = 0x01;
   XBuf[6] = 0x8E;
   XBuf[7] = 0x5E;
   XBuf[8] = 0x00;
   XBuf[9] = 0x00;


   int k = NetworkForm->ClientSocket1->Socket->SendBuf((unsigned char*)XBuf, 8);
   
}
//---------------------------------------------------------------------------

