//---------------------------------------------------------------------------

#include <vcl.h>
#include <mmSystem.h>
#pragma hdrstop

#include "ComConfigPage.h"
#include "ProgStationUnit.h"
//#include "TCPIPUnit.h"
#include "commands.h"
#include "AWSocketUnit.h"
#include <Registry.hpp>

//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma resource "*.dfm"
extern TCommConfigDlg *comConfigDialog;
extern struct sockaddr_in peer;
extern String comStatusStr;
extern int listViewItemCount;
extern int encryption;
extern SOCKET activeSock[MAX_DESCRIPTOR][2];
//extern ListViewInfoStruct listViewInfo[MAX_DESCRIPTOR];
extern int numIpAvail;
//activeIPsStruct activeIPs[MAX_DESCRIPTOR];
//unsigned int activeIPsIndex = 0;
extern char ipAddr[MAX_DESCRIPTOR][20];
extern bool tcipWindow = false;
extern int numIpSelected;
extern struct networkInfoStruct *networkInfo;
extern bool RS232On;
extern bool networkOn;
extern unsigned short int numOpenedSocket;
extern DWORD FAR PASCAL TCPIPCommThread (LPSTR );
extern HANDLE hTCPIPCommThread;
extern TTAWSocket* AWSockets[MAX_DESCRIPTOR];
extern bool networkOffline;

//---------------------------------------------------------------------------
__fastcall TCommConfigDlg::TCommConfigDlg(TComponent* Owner)
        : TForm(Owner)
{
    comConfigDialog = this;
    sacnForm = NULL;
    comPort = ProgStationForm->comPort;
    baudRate = ProgStationForm->baudRate;
    dataBits = ProgStationForm->dataBits;
    stopBits = ProgStationForm->stopBits;
    parity = ProgStationForm->parity;
    FlowCtrl = ProgStationForm->FlowCtrl;

    connectQueEntry = 0;
    scanDisconnect = false;

    /*if (comPort == 1)
       com1RadioButton->Checked = true;
    else if (comPort == 2)
       com2RadioButton->Checked = true;
    else if (comPort == 3)
       com3RadioButton->Checked = true;*/

    CommPortComboBox->ItemIndex = comPort - 1;

    BaudRateComboBox->Text = baudRate;
    DataBitsComboBox->Text = dataBits;

    if (stopBits == 0)
       StopBitsComboBox->Text = "1";
    else if (stopBits == 1)
       StopBitsComboBox->Text = "1.5";
    else if (stopBits == 2)
       StopBitsComboBox->Text = "2";


    if (parity == NOPARITY)
       ParityComboBox->Text = "None";
    else if (parity == MARKPARITY)
       ParityComboBox->Text = "Mark";
    else if (parity == SPACEPARITY)
       ParityComboBox->Text = "Space";
    else
       ParityComboBox->Text = "  ";

    CommPageControl->ActivePageIndex = 0;

    

    /*if (encryption == 0)
    {
       NoEncryptRadioButton->Checked = true;
       TwoFishEncryptRadioButton->Checked = false;
       RijndaelEncryptRadioButton->Checked = false;
    }
    else if (encryption == 1)
    {
       NoEncryptRadioButton->Checked = false;
       TwoFishEncryptRadioButton->Checked = true;
       RijndaelEncryptRadioButton->Checked = false;
    }
    else
    {
       NoEncryptRadioButton->Checked = false;
       TwoFishEncryptRadioButton->Checked = false;
       RijndaelEncryptRadioButton->Checked = true;
    } */

    sacnForm = new TScanForm(this);
    Application->CreateForm(__classid(TScanForm), &sacnForm);
    sacnForm->Visible = false;
}
//---------------------------------------------------------------------------

void __fastcall TCommConfigDlg::cancelBtnClick(TObject *Sender)
{
   if (sacnForm)
   {
      delete sacnForm;
      sacnForm = NULL;
   }

   Close();
}
//---------------------------------------------------------------------------
void __fastcall TCommConfigDlg::BaudRateComboBoxChange(TObject *Sender)
{
   baudRate = atoi(BaudRateComboBox->Text.c_str());
}
//---------------------------------------------------------------------------

void __fastcall TCommConfigDlg::DataBitsComboBoxChange(TObject *Sender)
{
   dataBits = atoi(DataBitsComboBox->Text.c_str());
}
//---------------------------------------------------------------------------

void __fastcall TCommConfigDlg::ParityComboBoxChange(TObject *Sender)
{
   if (ParityComboBox->Text == "None")
      parity = NOPARITY;
   else if (ParityComboBox->Text == "Space")
      parity = SPACEPARITY;
   else if (ParityComboBox->Text == "Mark")
      parity = MARKPARITY;
   else if (ParityComboBox->Text == "Odd")
      parity = ODDPARITY;
   else if (ParityComboBox->Text == "Even")
      parity = EVENPARITY;
}
//---------------------------------------------------------------------------

void __fastcall TCommConfigDlg::StopBitsComboBoxChange(TObject *Sender)
{
   if (StopBitsComboBox->Text == "0")
       stopBits = 0;
   if (StopBitsComboBox->Text == "1.5")
       stopBits = 1;
   if (StopBitsComboBox->Text == "2")
       stopBits = 2;
}
//------------------------------------------------------------------------------

void __fastcall TCommConfigDlg::ConnectBitBtnClick(TObject *Sender)
{
   AnsiString ip;
   
   if (RS232RadioButton->Checked)
   {
       if (CommPortComboBox->ItemIndex >= 0)
          ProgStationForm->comPort = CommPortComboBox->ItemIndex + 1;
       ProgStationForm->baudRate = baudRate;
       ProgStationForm->lastBaudrate = baudRate;
       ProgStationForm->dataBits = dataBits;
       ProgStationForm->stopBits = stopBits;
       ProgStationForm->parity = parity;
       if(networkOn)
           ProgStationForm->CloseNetworkConnection();
       ProgStationForm->ClosePort();
       ConnectLabel->Caption = "";
       //ConnectedLabel->Visible = false;
       if (ProgStationForm->OpenSerial(ProgStationForm->comPort,
                                ProgStationForm->baudRate))
       {
          ProgStationForm->PortOpen = true;
          PlaySound("Ding.wav", NULL, SND_ASYNC );
          //ConnectLabel->Font->Color = clBlue;
          ConnectLabel->Caption = "Connected to RS232";
          ConnectLabel->Update();
       }
    }
    else   //network TCPIP
    {
        /*for (int i=0; i<MAX_DESCRIPTOR; i++)
        {
           ProgStationForm->sockConnectQue[i].ip.SetLength(0);
           ProgStationForm->sockConnectQue[i].connect = false;
           ProgStationForm->sockConnectQue[i].numRetry = 0;
        }

        connectQueEntry = 0;
        if(RS232On)
          ProgStationForm->ClosePort();
        networkOn = true;
        RS232On = false;

        ProgStationForm->ConnectingSockets = true;
        ProgStationForm->PollTimer->Enabled = false;

        ProgStationForm->numSelectedSockConnect = 0;
        ProgStationForm->numSockConnected = 0; */

        listViewItemCount = IPListView->Items->Count;

        //if (UseSpecificIPRadioButton->Checked)
        //{
           //ip = IPSpecEdit1->Text;
           //ip += ".";
           //ip += IPSpecEdit2->Text;
           //ip += ".";
           //ip += IPSpecEdit3->Text;
           //ip += ".";
           //ip += IPSpecEdit4->Text;


           if(RS232On)
              ProgStationForm->ClosePort();
           RS232On = false;
           networkOn = true;

           int index;
           for (int i=0; i<IPListView->Items->Count; i++)
           {
              ip = GetItem(IPListView->Items->Item[i]->SubItems, 0);
              index = GetIpAddressIndex(ip);
              if (IPListView->Items->Item[i]->Checked)
              {
                 ProgStationForm->listViewInfo[i].selected = true;
                 ProgStationForm->ConnectSocket(ip);
              }
              else
              {
                 ProgStationForm->listViewInfo[i].selected = false;
              }
           }

           //for (int i=0; i<IPListView->Items->Count; i++)
              //IPListView->Items->Item[i]->Checked = false;

           //ProgStationForm->sockConnectQue[0].ip = ip;
           //ProgStationForm->numSelectedSockConnect = 1;
        //}
        //else
           //GetAllSelectedItems();

        //ConnectTimer->Enabled = true;
        
        //ProgStationForm->updateIpList = true;

        //Sleep(1000+ProgStationForm->numSelectedSockConnect*200);
        
        //ProgStationForm->PollTimer->Enabled = true;


        /*AnsiString ip;
        AnsiString str;
        int encrypt;
        if (RijndaelEncryptRadioButton->Checked)
          encrypt = 2;
        else
          encrypt = 0;

       int portID;
       if (ComPortIDEdit->Text.data() == NULL)
       {
          Application->MessageBox("Error: Invalid Port ID", "Error", MB_OK);
          return;
       }
       else
       {
          portID = atoi(ComPortIDEdit->Text.c_str());
          ProgStationForm->lastPortID = portID;
       }

       if(RS232On)
          ProgStationForm->ClosePort();

       if (!tcipWindow)
       {
          tcpipForm = new TTCPIPForm(this);
          Application->CreateForm(__classid(TTCPIPForm), &tcpipForm);
          tcpipForm->Visible = false;
       }

       ConnectLabel->Caption = "";

       if (UseSpecificIPRadioButton->Checked)   //specific ip
       {
           ip = IPSpecEdit1->Text;
           ip += ".";
           ip += IPSpecEdit2->Text;
           ip += ".";
           ip += IPSpecEdit3->Text;
           ip += ".";
           ip += IPSpecEdit4->Text;

           ProgStationForm->numItems = IPListView->Items->Count;
           for (int i=0; i<IPListView->Items->Count; i++)
           {
              if (IPListView->Items->Item[i]->Checked)
              {
                 if ((GetItem(IPListView->Items->Item[i]->SubItems, 3) == "Active") &&
                     (GetItem(IPListView->Items->Item[i]->SubItems, 1) == ip))
                 {
                    str = "The connection already exits for ip = ";
                    str += GetItem(IPListView->Items->Item[i]->SubItems, 0);
                    Application->MessageBox(str.c_str(), "Error", MB_OK);
                    return;
                 }
              }
           }

           if (tcpipForm->ConnectSingleIP(ip, encrypt, SOCK_STREAM, 10001))
           {
              str = "IP = ";
              str += ip;
              str += "  was connected.";
              ConnectLabel->Caption = str;
              ConnectLabel->Update();
           }
           else
           {
              str = "IP = ";
              str += ip;
              str += "  Failed to connect.";
              ConnectLabel->Caption = str;
              ConnectLabel->Update();
           }

           Sleep(400);
       }
       else    //search ip
       {
          TStrings *s;
          numIpSelected = 0;
          numOpenedSocket = 0;

          ProgStationForm->numItems = IPListView->Items->Count;
          for (int i=0; i<IPListView->Items->Count; i++)
          {
              if (IPListView->Items->Item[i]->Checked)
              {
                 if (GetItem(IPListView->Items->Item[i]->SubItems, 3) == "Active")
                 {
                   str = "The connection already exits for ip = ";
                   str += GetItem(IPListView->Items->Item[i]->SubItems, 0);
                   Application->MessageBox(str.c_str(), "Error", MB_OK);
                 }
                 else
                 {
                   //ip = IPListView->Items->Item[i]->SubItems->Text;
                   ip = GetItem(IPListView->Items->Item[i]->SubItems, 0);
                   strcpy(&ipAddr[numIpSelected][0], ip.c_str());
                   numIpSelected += 1;
                   networkInfo[i].selected = true;
                 }
              }
              else
                 networkInfo[i].selected = false;
          }

          if (numIpSelected == 0)
          {
             Application->MessageBox("No Item is selected from the list", "Error", MB_OK);
             return;
          }

          //if(networkOn)
             //ProgStationForm->CloseNetworkConnection();

          //ConnectLabel->Font->Color = clBlue;
          ConnectLabel->Caption = "Connecting ...";
          if (tcpipForm->Startup(encrypt, SOCK_STREAM, 10001))
          {
             //ConnectLabel->Caption = "Connected to Network";
             comStatusStr = "Connected to Network";
             ProgStationForm->MainStatusBar->Panels->Items[2]->Text =  "Connected to Network";
          }
          //else
          //{
             //ConnectLabel->Caption = "Network Connection Failed";
             //comStatusStr = "Network Connection Failed";
             //ProgStationForm->MainStatusBar->Panels->Items[2]->Text =  "Failed Network Connection";
          //}

          //PlaySound("Ding.wav", NULL, SND_ASYNC );
          //PlaySound("Ding.wav", NULL, SND_ASYNC );
          
       }//use serach for active ip */
    } //network TCPIP
}
//------------------------------------------------------------------------------
void __fastcall TCommConfigDlg::RS232RadioButtonClick(TObject *Sender)
{
   CommPageControl->ActivePageIndex = 0;
   RS232RadioButton->Font->Color = clRed;
   TCPIPRadioButton->Font->Color = clBlack;
   ConnectBitBtn->Caption = "Connect";
}
//---------------------------------------------------------------------------

void __fastcall TCommConfigDlg::TCPIPRadioButtonClick(TObject *Sender)
{
   CommPageControl->ActivePageIndex = 1;
   RS232RadioButton->Font->Color = clBlack;
   TCPIPRadioButton->Font->Color = clRed;
   //if (UseSearchIPRadioButton->Checked)
      //ConnectBitBtn->Caption = "Connect Selected";
   //else
      //ConnectBitBtn->Caption = "Connect Specific";
}
//---------------------------------------------------------------------------

void __fastcall TCommConfigDlg::CommPageControlChange(TObject *Sender)
{
   if (CommPageControl->ActivePageIndex == 0)
   {
      RS232RadioButton->Checked = true;
      RS232RadioButton->Font->Color = clRed;
      TCPIPRadioButton->Font->Color = clBlack;
      //ConnectBitBtn->Caption = "Connect  To  RS232";
   }
   else
   {
      TCPIPRadioButton->Checked = true;
      RS232RadioButton->Font->Color = clBlack;
      TCPIPRadioButton->Font->Color = clRed;
      UpdateIPListView();
      //ConnectBitBtn->Caption = "Connect  To  Network";

   }
}
//---------------------------------------------------------------------------
void __fastcall TCommConfigDlg::FormActivate(TObject *Sender)
{
  /* int index;
   AnsiString str1;
   AnsiString str;
   TListItem* listItem;


   if (RS232RadioButton->Checked)
   {
      //ConnectBitBtn->Caption = "Connect  To  RS232";
      RS232RadioButton->Font->Color = clRed;
      TCPIPRadioButton->Font->Color = clBlack;
   }
   else
   {
      //ConnectBitBtn->Caption = "Connect  To  Network";
      RS232RadioButton->Font->Color = clBlack;
      TCPIPRadioButton->Font->Color = clRed;
   }

   if (ProgStationForm->lastHostName.data() != NULL)
   {
      ;//HostNameEdit->Text = ProgStationForm->lastHostName;
   }

   if (ProgStationForm->lastPortID > 0)
   {
      ComPortIDEdit->Text = ProgStationForm->lastPortID;
   }

   if (RS232On)
      RS232RadioButton->Checked = true;
   else
      TCPIPRadioButton->Checked = true;

   for (int i=0; i<numIpAvail; i++)
   {
      listItem = IPListView->Items->Add();

      if (listViewInfo[i].selected)
         listItem->Checked = true;
      else
         listItem->Checked = false;

      listItem->SubItems->Add(listViewInfo[i].ip);

      if (listViewInfo[i].reader > 0)
         listItem->SubItems->Add(listViewInfo[i].reader);
      else
         listItem->SubItems->Add("");

      if (listViewInfo[i].host > 0)
         listItem->SubItems->Add(listViewInfo[i].host);
      else
         listItem->SubItems->Add("");

      listItem->SubItems->Add(listViewInfo[i].netStatus);
      listItem->SubItems->Add(listViewInfo[i].rdrStatus);
   } */
}
//---------------------------------------------------------------------------
AnsiString __fastcall TCommConfigDlg::GetIpAddress(short n1, short n2, short n3, short n4, short index)
{
     AnsiString ip;
     switch (index)
     {
        /*case 1:
           if ((IP1Edit1->Text.data() == NULL) || (IP1Edit2->Text.data() == NULL) ||
              (IP1Edit3->Text.data() == NULL) || (IP1Edit4->Text.data() == NULL))
           {
              Application->MessageBox("Error: Wrong IP Address 1", "Error", MB_OK);
              ip = "ERROR";
           }
           else
           {
              ip = IP1Edit1->Text;
              ip += ".";
              ip += IP1Edit2->Text;
              ip += ".";
              ip += IP1Edit3->Text;
              ip += ".";
              ip += IP1Edit4->Text;
           }
        break;*/

        default:
          ip = "ERROR";
        break;

     }

     return ip;
}
//------------------------------------------------------------------------------
void __fastcall TCommConfigDlg::FormClose(TObject *Sender,
      TCloseAction &Action)
{
   if (sacnForm)
   {
      delete sacnForm;
      sacnForm = NULL;
   }

  /* AnsiString s;
   numIpAvail = 0;
   for (int i=0; i<IPListView->Items->Count; i++)
   {
      if (IPListView->Items->Item[i]->Checked)
         listViewInfo[i].selected = true;
      else
         listViewInfo[i].selected = false;

      s = GetItem(IPListView->Items->Item[i]->SubItems, 0); //ip
      strcpy(&listViewInfo[i].ip[0], s.c_str());

      s = GetItem(IPListView->Items->Item[i]->SubItems, 1); //reader
      listViewInfo[i].reader = atoi(s.c_str());

      s = GetItem(IPListView->Items->Item[i]->SubItems, 2); //host
      listViewInfo[i].host = atoi(s.c_str());

      s = GetItem(IPListView->Items->Item[i]->SubItems, 3); //netStatus
      strcpy(&listViewInfo[i].netStatus[0], s.c_str());

      s = GetItem(IPListView->Items->Item[i]->SubItems, 4); //netStatus
      strcpy(&listViewInfo[i].rdrStatus[0], s.c_str());

      numIpAvail += 1;
   } */
   
   comConfigDialog = NULL;
}
//------------------------------------------------------------------------------
void __fastcall TCommConfigDlg::AddIpToList(AnsiString item, bool active)
{
   TListItem* ListItem;
   int ipIndex;

   if ((ipIndex=GetIpAddressIndex(item)) >= 0)   //ip already avail
   {
      ListItem = IPListView->Items->Add();
      ListItem->SubItems->Add(item);  //ip
      ListItem->SubItems->Add(ProgStationForm->listViewInfo[ipIndex].reader);  //reader
      ListItem->SubItems->Add(ProgStationForm->listViewInfo[ipIndex].host);    //host
      ListItem->SubItems->Add(ProgStationForm->listViewInfo[ipIndex].netStatus);  //netStatus
      ListItem->SubItems->Add(ProgStationForm->listViewInfo[ipIndex].rdrStatus);  //rdrStatus

   }
   else   //new ip
   {
      ListItem = IPListView->Items->Add();
      ListItem->SubItems->Add(item);  //ip
      ListItem->SubItems->Add("");    //rdr
      ListItem->SubItems->Add("");    //host
      if (active)
         ListItem->SubItems->Add("Active");
      else
         ListItem->SubItems->Add("Inactive");
      ListItem->SubItems->Add("Offline");    //status
      PlaySound("Ding.wav", NULL, SND_ASYNC );

      ProgStationForm->listViewInfo[myListViewItemCount].ip = item;
      ProgStationForm->listViewInfo[myListViewItemCount].reader = 0;
      ProgStationForm->listViewInfo[myListViewItemCount].host = 0;
      ProgStationForm->listViewInfo[myListViewItemCount].full = true;
      if (active)
         strcpy(ProgStationForm->listViewInfo[myListViewItemCount].netStatus, "Active");
      else
         strcpy(ProgStationForm->listViewInfo[myListViewItemCount].netStatus, "Inactive");
      strcpy(ProgStationForm->listViewInfo[myListViewItemCount].rdrStatus, "Offline");
      myListViewItemCount += 1;
      listViewItemCount += 1;  //jul 27, 06
   }

   IPListView->Refresh();
}
//------------------------------------------------------------------------------
void __fastcall TCommConfigDlg::SearchIPBitBtnClick(TObject *Sender)
{
   ClearIPListView();
   if (NoEncryptRadioButton->Checked)
      encryption = 0;
   else
      encryption = 2;

   if (!KeepListItemCheckBox->Checked)
      IPListView->Items->Clear();

   ConnectLabel->Caption = "Searching the network ........     ";
   ConnectLabel->Update();

   scanDisconnect = true;
   ProgStationForm->DisconnectAllSockets();
   ProgStationForm->InitializeListViewInfo();
   ProgStationForm->InitializeAWSockets();
   myListViewItemCount = 0;
   listViewItemCount = 0;  

   //activeIPsIndex = 0;
   ScanForm->ScanNetwork();
   listViewItemCount = IPListView->Items->Count;

   ConnectLabel->Caption = "Searching the Network Completed.";
   ConnectLabel->Update();

   //for (int i=0; i<IPListView->Items->Count; i++)
       //IPListView->Items->Item[i]->Checked = false;

}
//---------------------------------------------------------------------------
void __fastcall TCommConfigDlg::ClearListBitBtnClick(TObject *Sender)
{
   //ClearIPListView();
   //activeIPsIndex = 0;
   numIpSelected = 0;
   //ResetReadersBitBtn->Enabled = false;
   //ConnectBitBtnOKBitBtn->Enabled = false;
}
//---------------------------------------------------------------------------
void __fastcall TCommConfigDlg::ClearIPListView()
{
   IPListView->Items->Clear();
   IPListView->Refresh();
}
//------------------------------------------------------------------------------
void __fastcall TCommConfigDlg::IPListViewDblClick(TObject *Sender)
{
   TListItem* item = IPListView->Selected;
   if (item)
   {
      if (item->Checked)
      {
         item->Checked = false;
         item->Selected = false;
      }
      else
      {
         item->Checked = true;
         item->Selected = true;

         TStrings* str;
         AnsiString ip;
         AnsiString s;
         int indx;

         str = item->SubItems;
         s = str->Text;
         ip = GetItem(str, 0);  //ip address
         indx = ip.Pos(".");
         s = ip.SubString(1, indx-1);
         IPEdit1->Text = s;
         ip = ip.SubString(indx+1, ip.Length()-indx);
         indx = ip.Pos(".");
         s = ip.SubString(1, indx-1);
         IPEdit2->Text = s;
         ip = ip.SubString(indx+1, ip.Length()-indx);
         indx = ip.Pos(".");
         s = ip.SubString(1, indx-1);
         IPEdit3->Text = s;
         ip = ip.SubString(indx+1, ip.Length()-indx);
         IPEdit4->Text = ip;
      }
   }
}
//------------------------------------------------------------------------------
AnsiString __fastcall TCommConfigDlg::GetItem(TStrings* str, int itemNum)
{
   if (itemNum < str->Count)
     return (str->Strings[itemNum]);
   else
     return (" ");
}
//------------------------------------------------------------------------------
void __fastcall TCommConfigDlg::ResetReadersBitBtnClick(TObject *Sender)
{
   /*AnsiString ip;
   AnsiString s;
   AnsiString str;
   AnsiString netStatus;
   numIpSelected = 0;
   int p;

   ProgStationForm->numItems = IPListView->Items->Count;
   for (int i=0; i<IPListView->Items->Count; i++)
      networkInfo[i].selected = false;

   for (int i=0; i<IPListView->Items->Count; i++)
   {
      if (IPListView->Items->Item[i]->Checked)
      {
         //ip = GetItem(IPListView->Items->Item[numIpSelected]->SubItems, 0);
         //netStatus = GetItem(IPListView->Items->Item[numIpSelected]->SubItems, 3);
         ip = GetItem(IPListView->Items->Item[i]->SubItems, 0);
         netStatus = GetItem(IPListView->Items->Item[i]->SubItems, 3);
         if ((netStatus.data() == NULL) || (netStatus == "Inactive"))
         {
            s = "IP = ";
            s += ip;
            s += "  is not connected to the network.";
            Application->MessageBox(s.c_str(), "Programming Station Error", MB_OK);
            return;
         }
         else
         {
            p = GetActiveIpAddressIndex(ip);
            //networkInfo[numIpSelected].selected = true;
            if (p >= 0)
            {
               strcpy(&ipAddr[numIpSelected][0], ip.c_str());
               networkInfo[p].selected = true;
               numIpSelected += 1;
            }
         }
      }
      //else
         //networkInfo[i].selected = false;
   }

   if (numIpSelected == 0)
   {
      Application->MessageBox("No Item is selected from the list.", "Error", MB_OK);
      return;
   }

   str = "Reseting Reader with IP = ";
   str += ip;
   ConnectLabel->Caption = str;
   ConnectLabel->Update(); */

   //ProgStationForm->WriteTCPIPComm(RESET_DEVICE, 0, "G", 0, 0, NULL, -1);

   AnsiString ip;
   AnsiString s;
   AnsiString netStatus;
   numIpSelected = 0;
   int sockIndex = 0;

   for (int i=0; i<IPListView->Items->Count; i++)
   {
      if (IPListView->Items->Item[i]->Checked)
      {
         ip = GetItem(IPListView->Items->Item[i]->SubItems, 0);
         netStatus = GetItem(IPListView->Items->Item[i]->SubItems, 3);
         if ((netStatus.data() == NULL) || (netStatus == "Inactive"))
         {
            s = "IP = ";
            s += ip;
            s += "  is not connected to the network.";
            Application->MessageBox(s.c_str(), "Programming Station Error", MB_OK);
            return;
         }

         for (int j=0; j<MAX_DESCRIPTOR; j++)
         {
            if (AWSockets[j])
            {
               if (AWSockets[j]->AWClientSocket->Address == ip)
               {
                  sockIndex = j;
                  break;
               }
            }
         }
         numIpSelected += 1;
      }
   }

   if (numIpSelected == 0)
   {
      Application->MessageBox("No Item is selected from the list.", "Error", MB_OK);
      return;
   }

   if (numIpSelected == 1)
   {
      ProgStationForm->WriteAWSocket(RESET_DEVICE, 0, "G", 0, 'S', NULL, 0, sockIndex);
   }
   else
      ProgStationForm->WriteAWSocket(RESET_DEVICE, 0, "G", 0, 'B', NULL, 0, 0);

    for (int i=0; i<IPListView->Items->Count; i++)
       IPListView->Items->Item[i]->Checked = false;
}
//------------------------------------------------------------------------------
void __fastcall TCommConfigDlg::UpdateIPListViewPowerup(int indx)
{
   TListItem* listItem;
   AnsiString ip;
   AnsiString str;
   bool check;


   if (indx >= MAX_DESCRIPTOR)
   {
      PlaySound("Ding.wav", NULL, SND_ASYNC );
      return;
   }

   for (int i=0; i<IPListView->Items->Count; i++)
   {
      ip = GetItem(IPListView->Items->Item[i]->SubItems, 0);
      if (ip == networkInfo[indx].ipAddr)
      {
         listItem = IPListView->Items->Item[i];
         if (listItem == NULL)
            return;
         //if (IPListView->Items->Item[i]->Checked)
            //check = true;
         //else
            //check = false;

         ////////////////////////////////////
         if (indx >= 0)
         {
             //networkInfo[index].activeSock = 0x00;
             //networkInfo[index].peerSock = NULL;
             //networkInfo[index].reader = 0;
             //networkInfo[index].host = 0;
             //strcpy(networkInfo[index].status, "Offline");
             //networkInfo[index].active = false;
             //networkInfo[index].selected = false;

             IPListView->Items->Item[i]->SubItems->Insert(0, networkInfo[indx].ipAddr); //Text = networkInfo[indx].ipAddr;
             IPListView->Items->Item[i]->SubItems->Insert(1, networkInfo[indx].reader); //reader
             IPListView->Items->Item[i]->SubItems->Insert(2, networkInfo[indx].host); //host
             IPListView->Items->Item[i]->SubItems->Insert(3, "Active"); //network status
             IPListView->Items->Item[i]->SubItems->Insert(4, "Online"); //rdr status
             IPListView->Update();
         } //if selected
         /////////////////////////////////////
         /*listItem->Delete();

         //listItem = IPListView->Items->Insert(0);

         //if (listItem == NULL)
            //return;
         listItem->SubItems->Add(networkInfo[indx].ipAddr);
         listItem->SubItems->Add(networkInfo[indx].reader);
         listItem->SubItems->Add(networkInfo[indx].host);
         if (networkInfo[indx].active)
            listItem->SubItems->Add("Active");
         else
            listItem->SubItems->Add("");
         AnsiString stat = networkInfo[indx].status;
         if (stat.data() != NULL)
            listItem->SubItems->Add(stat);
         listItem->Checked = check;*/

         str = "Reader with IP = ";
         str += networkInfo[indx].ipAddr;
         str += "  was powered up.";
         ConnectLabel->Caption = str;
         ConnectLabel->Update();

         return;
      }//if ip = ...
   }//for loop

   //if ip does not exits in the list
   /*listItem = IPListView->Items->Add();
   listItem->SubItems->Add(networkInfo[indx].ipAddr);
   listItem->SubItems->Add(networkInfo[indx].reader);
   listItem->SubItems->Add(networkInfo[indx].host);
   listItem->SubItems->Add("Active");
   listItem->SubItems->Add("Online");

   str = "Reader with IP = ";
   str += networkInfo[indx].ipAddr;
   str += "  was powered up.";
   ConnectLabel->Caption = str;
   ConnectLabel->Update();*/
}
//------------------------------------------------------------------------------
void __fastcall TCommConfigDlg::IPListViewCustomDrawItem(
      TCustomListView *Sender, TListItem *Item, TCustomDrawState State,
      bool &DefaultDraw)
{
   TStrings* str = Item->SubItems;
   AnsiString selectedItem = GetItem(str, 3);
   AnsiString selectedItem1 = GetItem(str, 4);

   if ((selectedItem1 == "Offline") && (selectedItem != "Active"))
      IPListView->Canvas->Font->Color = clRed;
   else if (selectedItem1 == "Offline")
      IPListView->Canvas->Font->Color = clBlue;   //network active
   else if (selectedItem1 == "Online")
     IPListView->Canvas->Font->Color = clGreen;

}
//------------------------------------------------------------------------------
void __fastcall TCommConfigDlg::SelectAllCheckBoxClick(TObject *Sender)
{
   for (int i=0; i<IPListView->Items->Count; i++)
   {
      if (SelectAllCheckBox->State == cbChecked)
      {
         IPListView->Items->Item[i]->Checked = true;
         IPListView->Items->Item[i]->Selected = true;
      }
      else
      {
         IPListView->Items->Item[i]->Checked = false;
         IPListView->Items->Item[i]->Selected = false;
      }
   }
}
//------------------------------------------------------------------------------
int __fastcall TCommConfigDlg::GetIpAddressIndex(AnsiString ip)
{
   int i;

   for (i=0; i<IPListView->Items->Count; i++)
   {
      if (ProgStationForm->listViewInfo[i].ip == ip)
         return (i);
   }

   return (-1);
}
//------------------------------------------------------------------------------
void __fastcall TCommConfigDlg::AssignIPBitBtnClick(TObject *Sender)
{
   if ((IPEdit1->Text.data() == NULL) || (IPEdit2->Text.data() == NULL) ||
       (IPEdit3->Text.data() == NULL) || (IPEdit4->Text.data() == NULL))
   {
      Application->MessageBox("Invalid New IP", "Programming Station Error", MB_OK);
      return;
   }

   AnsiString newIP;
   newIP = IPEdit1->Text;
   newIP += ".";
   newIP += IPEdit2->Text;
   newIP += ".";
   newIP += IPEdit3->Text;
   newIP += ".";
   newIP += IPEdit4->Text;

   TStrings* str;
   AnsiString str1;
   AnsiString oldIP;
   AnsiString s;
   TListItem* item = IPListView->Selected;
   if (item)
   {
      str = item->SubItems;
      s = str->Text;
      oldIP = GetItem(str, 0);  //ip address
   }
   else
   {
       Application->MessageBox("No Item is selected from the list.", "Programming Station Error", MB_OK);
       return;
   }

   int ret;
   s = "Do you want to change IP address : ";
   s += oldIP;
   ret = Application->MessageBox(s.c_str(), "Programming Station", MB_YESNO);
   if (ret == IDNO)
   {
     return;
   }

   //ConnectLabel->Font->Color = clPurple;
   //ConnectLabel->Caption = "Changing the IP Address ...";

   str1 = "Changing IP address ";
   str1 += oldIP;
   ConnectLabel->Caption = str1;
   ConnectLabel->Update();
   Sleep(1000);

   if (ScanForm->ChangeIPAddress(oldIP, newIP))
   {
      str1 = "IP = ";
      str1 += oldIP;
      str1 += "  was changed successfully.";
      ConnectLabel->Caption = str1;
      ConnectLabel->Update();
   }
   else
   {
      str1 = "Changing IP = ";
      str1 += oldIP;
      str1 += "  Failed.";
      ConnectLabel->Caption = str1;
      ConnectLabel->Update();
      //ConnectLabel->Caption = "Changing IP Failed.";
   }
      
   PlaySound("Ding.wav", NULL, SND_ASYNC );

   ClearIPListView();
   if (NoEncryptRadioButton->Checked)
      encryption = 0;
   else
      encryption = 2;

   IPListView->Items->Clear();
   //activeIPsIndex = 0;
   ScanForm->ScanNetwork();
}
//------------------------------------------------------------------------------

void __fastcall TCommConfigDlg::IPListViewClick(TObject *Sender)
{
   /*TStrings* str;
   AnsiString ip;
   AnsiString s;
   int indx;

   TListItem* item = IPListView->ItemFocused;

   if (item)
   {
      str = item->SubItems;
      s = str->Text;
      ip = GetItem(str, 0);  //ip address
      //indx = GetActiveIpIndex(ip);
      indx = GetIpAddressIndex(ip);
      if (indx >= 0)
      {
         if (item->Selected)
            //activeIPs[indx].selected = true;
            networkInfo[indx].selected = true;
         else
            //activeIPs[indx].selected = false;
            networkInfo[indx].selected = false;
      }
   }*/
}
//------------------------------------------------------------------------------
/*int TCommConfigDlg::GetActiveIpIndex(AnsiString ip)
{
   int i;

   for (i=0; i<activeIPsIndex; i++)
   {
      if (activeIPs[i].ip == ip)
         return (i);
   }
   return (-1);
}*/
//------------------------------------------------------------------------------
void TCommConfigDlg::UpdateSocketStatus(int indx)
{
   TListItem* item;
   AnsiString ip;
   bool check;

   for (int i=0; i<IPListView->Items->Count; i++)
   {
      ip = GetItem(IPListView->Items->Item[i]->SubItems, 0);
      if (ip == networkInfo[indx].ipAddr)
      {
         item = IPListView->Items->Item[i];

         if (indx >= 0)
         {
             IPListView->Items->Item[i]->SubItems->Insert(0, networkInfo[indx].ipAddr); //Text = networkInfo[indx].ipAddr;
             IPListView->Items->Item[i]->SubItems->Insert(1, networkInfo[indx].reader); //reader
             IPListView->Items->Item[i]->SubItems->Insert(2, networkInfo[indx].host); //host
             IPListView->Items->Item[i]->SubItems->Insert(3, "Active"); //network status
             IPListView->Items->Item[i]->SubItems->Insert(4, networkInfo[indx].status); //rdr status
             IPListView->Update();
             PlaySound("Ding.wav", NULL, SND_ASYNC);
             return;
         } //if selected
         /*if (IPListView->Items->Item[i]->Checked)
            check = true;
         else
            check = false;
         item->Delete();
         if ((i-1) >= 0)
            item = IPListView->Items->Insert(i-1);
         else
            item = IPListView->Items->Insert(0);
         item->SubItems->Add(networkInfo[indx].ipAddr);
         if (networkInfo[indx].reader > 0)
            item->SubItems->Add(networkInfo[indx].reader);
         else
            item->SubItems->Add("");
         if (networkInfo[indx].host > 0)
            item->SubItems->Add(networkInfo[indx].host);
         else
            item->SubItems->Add("");
         if (networkInfo[indx].active)
            item->SubItems->Add("Active");
         else
            item->SubItems->Add("");
         if (networkInfo[indx].status == "Online")
            item->SubItems->Add(networkInfo[indx].status);
         else
            item->SubItems->Add("Offline");
         item->Checked = check;

         return;*/
      }
   }
}
//------------------------------------------------------------------------------
/*void TCommConfigDlg::GetActiveIpSeleceted()
{
   AnsiString ip;
   int indx;

   for (int i=0; i<activeIPsIndex; i++)
   {
      ip = GetItem(IPListView->Items->Item[i]->SubItems, 0);
      if (ip.data() != NULL)
      {
         indx = GetActiveIpIndex(ip);
         if (IPListView->Items->Item[i]->Checked && (indx >= 0))
            activeIPs[indx].selected = true;
         else
            activeIPs[indx].selected = false;
      }
   }
}*/
//------------------------------------------------------------------------------
void __fastcall TCommConfigDlg::RijndaelEncryptRadioButtonClick(
      TObject *Sender)
{
   encryption = 2;

   numIpSelected = 0;
   //ResetReadersBitBtn->Enabled = false;
   //ConnectBitBtnOKBitBtn->Enabled = false;
   //for (int i=0; i<activeIPsIndex; i++)
   for (int i=0; i<IPListView->Items->Count; i++)
   {
      //activeIPs[i].ip = 0;
      //activeIPs[i].reader = 0;
      //activeIPs[i].host = 0;
      //activeIPs[i].active = false;

      networkInfo[i].reader = 0;
      networkInfo[i].host = 0;
      strcpy(networkInfo[i].status, "Offline");
      networkInfo[i].active = false;
      networkInfo[i].selected = false;
      networkInfo[i].activeSock = 0;
      //networkInfo[i].activeSock[1] = 0;
      //networkInfo[i].activeSock[2] = 0;
      networkInfo[i].validRec = false;
      //strcpy(networkInfo[i].ipAddr, "          ");
   }

   ClearIPListView();
   //activeIPsIndex = 0;
}
//------------------------------------------------------------------------------
void __fastcall TCommConfigDlg::NoEncryptRadioButtonClick(TObject *Sender)
{
   encryption = 0;

   numIpSelected = 0;
   //ResetReadersBitBtn->Enabled = false;
   //ConnectBitBtnOKBitBtn->Enabled = false;
   //for (int i=0; i<activeIPsIndex; i++)
   //
   for (int i=0; i<IPListView->Items->Count; i++)
   {
      //activeIPs[i].ip = 0;
      //activeIPs[i].reader = 0;
      //activeIPs[i].host = 0;
      //activeIPs[i].active = false;

      networkInfo[i].reader = 0;
      networkInfo[i].host = 0;
      strcpy(networkInfo[i].status, "Offline");
      networkInfo[i].active = false;
      networkInfo[i].selected = false;
      networkInfo[i].activeSock = 0;
      //networkInfo[i].activeSock[2] = 0;
      networkInfo[i].validRec = false;
      //strcpy(networkInfo[i].ipAddr, "          ");
   }

   ClearIPListView();

   //activeIPsIndex = 0;
}
//------------------------------------------------------------------------------
void __fastcall TCommConfigDlg::IPListViewColumnClick(TObject *Sender,
      TListColumn *Column)
{
   //columnToSort = Column->Index;
   //((TCustomListView *)Sender)->AlphaSort();
   //if ( Column->Index == 0)

}
//------------------------------------------------------------------------------
void __fastcall TCommConfigDlg::IPListViewCompare(TObject *Sender,
      TListItem *Item1, TListItem *Item2, int Data, int &Compare)
{
   /*int index;
   AnsiString selectedItem1;
   AnsiString selectedItem2;
   AnsiString s;
   TStrings* str;
   int n1, n2;

    if (columnToSort == 0)   //selected
    {
       n1 = Item1->Checked;
       n2 = Item2->Checked;
       if (n1 == n2)
          Compare = 0;
       else if (n1 > n2)
          Compare = 1;
       else
          Compare = -1;
    }
    else if (columnToSort == 1)  //ipaddress
    {
       str = Item1->SubItems;
       selectedItem1 = GetItem(str, 0);

       str = Item2->SubItems;
       selectedItem2 = GetItem(str, 0);

       Compare = CompareText(selectedItem1, selectedItem2);
    }
    else if (columnToSort == 2) //reader
    {
       str = Item1->SubItems;
       selectedItem1 = GetItem(str, 1);

       str = Item2->SubItems;
       selectedItem2 = GetItem(str, 1);

       n1 = atoi(selectedItem1.c_str());
       n2 = atoi(selectedItem2.c_str());
       if (n1 == n2)
            Compare = 0;
       else if (n1 > n2)
            Compare = 1;
       else
           Compare = -1;
    }
    else if (columnToSort == 3) //host
    {
       str = Item1->SubItems;
       selectedItem1 = GetItem(str, 2);

       str = Item2->SubItems;
       selectedItem2 = GetItem(str, 2);

       n1 = atoi(selectedItem1.c_str());
       n2 = atoi(selectedItem2.c_str());
       if (n1 == n2)
            Compare = 0;
       else if (n1 > n2)
            Compare = 1;
       else
           Compare = -1;
    }
    else if (columnToSort == 4)  //network status
    {
         str = Item1->SubItems;
         selectedItem1 = GetItem(str, 3);
         str = Item2->SubItems;
         selectedItem2 = GetItem(str, 3);
         Compare = CompareText(selectedItem1, selectedItem2);
    }
    else if (columnToSort == 5)  //reader status
    {
         str = Item1->SubItems;
         selectedItem1 = GetItem(str, 4);
         str = Item2->SubItems;
         selectedItem2 = GetItem(str, 4);
         Compare = CompareText(selectedItem1, selectedItem2);
    }*/
}
//---------------------------------------------------------------------------
void __fastcall TCommConfigDlg::UseSearchIPRadioButtonClick(
      TObject *Sender)
{
    SearchIPBitBtn->Enabled = true;
    ConnectBitBtn->Caption = "Connect Selected";
    
    IPSpecEdit1->Color = clMenu;
    IPSpecEdit2->Color = clMenu;
    IPSpecEdit3->Color = clMenu;
    IPSpecEdit4->Color = clMenu;

    IPSpecEdit1->ReadOnly = true;
    IPSpecEdit2->ReadOnly = true;
    IPSpecEdit3->ReadOnly = true;
    IPSpecEdit4->ReadOnly = true;

    IPSpecEdit1->Font->Color = clGray;
    IPSpecEdit2->Font->Color = clGray;
    IPSpecEdit3->Font->Color = clGray;
    IPSpecEdit4->Font->Color = clGray;

    AddIPBitBtn->Enabled = false;
}
//---------------------------------------------------------------------------
void __fastcall TCommConfigDlg::UseSpecificIPRadioButtonClick(
      TObject *Sender)
{
    SearchIPBitBtn->Enabled = false;
    AddIPBitBtn->Enabled = true;
    ConnectBitBtn->Caption = "Connect Specific";

    IPSpecEdit1->SetFocus();
    IPSpecEdit1->Color = clWhite;
    IPSpecEdit2->Color = clWhite;
    IPSpecEdit3->Color = clWhite;
    IPSpecEdit4->Color = clWhite;

    IPSpecEdit1->ReadOnly = false;
    IPSpecEdit2->ReadOnly = false;
    IPSpecEdit3->ReadOnly = false;
    IPSpecEdit4->ReadOnly = false;

    IPSpecEdit1->Font->Color = clBlack;
    IPSpecEdit2->Font->Color = clBlack;
    IPSpecEdit3->Font->Color = clBlack;
    IPSpecEdit4->Font->Color = clBlack;

    for (int i=0; i<IPListView->Items->Count; i++)
       IPListView->Items->Item[i]->Checked = false;
}
//---------------------------------------------------------------------------
void __fastcall TCommConfigDlg::AddConnectedIpToActiveList(AnsiString connectedIp, int indx)
{
   TListItem* item;
   AnsiString ip;
   bool check;

   for (int i=0; i<IPListView->Items->Count; i++)
   {
      ip = GetItem(IPListView->Items->Item[i]->SubItems, 0);
      if (ip == networkInfo[indx].ipAddr)
      {
         item = IPListView->Items->Item[i];
         if (IPListView->Items->Item[i]->Checked)
            check = true;
         else
            check = false;
         item->Delete();
         if ((i-1) >= 0)
            item = IPListView->Items->Insert(i-1);
         else
            item = IPListView->Items->Insert(0);
         item->SubItems->Add(networkInfo[indx].ipAddr);
         if (networkInfo[indx].reader > 0)
            item->SubItems->Add(networkInfo[indx].reader);
         else
            item->SubItems->Add("");
         if (networkInfo[indx].host > 0)
            item->SubItems->Add(networkInfo[indx].host);
         else
            item->SubItems->Add("");
         if (networkInfo[indx].active)
            item->SubItems->Add("Active");
         else
            item->SubItems->Add("");
         if (networkInfo[indx].status == "Online")
            item->SubItems->Add(networkInfo[indx].status);
         else
            item->SubItems->Add("Offline");
         item->Checked = check;
         return;
      }
   }

   item = IPListView->Items->Add();
   item->Checked = false;
   item->SubItems->Add(connectedIp);
   item->SubItems->Add("");
   item->SubItems->Add("");
   item->SubItems->Add("Active");
   item->SubItems->Add("Offline");
}
//------------------------------------------------------------------------------
void __fastcall TCommConfigDlg::DisconnectBitBtnClick(TObject *Sender)
{

   if (RS232RadioButton->Checked)
   {
       ProgStationForm->ClosePort();
       ConnectLabel->Caption = "Closed RS232 Comm Port.";
       PlaySound("Ding.wav", NULL, SND_ASYNC );
   }
   else   //network TCPIP
   {
        numIpSelected = 0;
        int index;
        AnsiString ip;
        AnsiString str;
        scanDisconnect = false;

        for (int i=0; i<IPListView->Items->Count; i++)
        {
           ip = GetItem(IPListView->Items->Item[i]->SubItems, 0);
           index = GetIpAddressIndex(ip);
           if (IPListView->Items->Item[i]->Checked)
              ProgStationForm->listViewInfo[i].selected = true;
           else
              ProgStationForm->listViewInfo[i].selected = false;
        }

        int numClosedSockets = 0;
        for (int i=0; i<IPListView->Items->Count; i++)
        {
           ip = GetItem(IPListView->Items->Item[i]->SubItems, 0);
           if (IPListView->Items->Item[i]->Checked)
           {
              ProgStationForm->DisconnectSocket(ip);
              numClosedSockets += 1;
           }
        }

        if  (numClosedSockets == IPListView->Items->Count)
           networkOffline = true;
        else
        {
           numClosedSockets = 0;
           for (int i=0; i<IPListView->Items->Count; i++)
           {
              if ((GetItem(IPListView->Items->Item[i]->SubItems, 3) == "Inactive") ||
                  (GetItem(IPListView->Items->Item[i]->SubItems, 3) == ""))
                  numClosedSockets += 1;
           }

           if  (numClosedSockets == IPListView->Items->Count)
               networkOffline = true;
           else
               networkOffline = false;
        }

        //for (int i=0; i<IPListView->Items->Count; i++)
           //IPListView->Items->Item[i]->Checked = false;

        return;

        for (int i=0; i<IPListView->Items->Count; i++)
           if (IPListView->Items->Item[i]->Checked)
              numIpSelected += 1;
               
        if (numIpSelected == 0)
        {
            Application->MessageBox("No Item is selected from the list.", "Error", MB_OK);
            return;
        }

        //ProgStationForm->PollTimer->Enabled = false;
        int r;
        for (int i=0; i<IPListView->Items->Count; i++)
        {
           if (IPListView->Items->Item[i]->Checked)
           {
              ip = GetItem(IPListView->Items->Item[i]->SubItems, 0);
              //index = ProgStationForm->GetSockPollQueIndex(ip);
              if (index >= 0)
              {
                 for (unsigned int i=index; i<ProgStationForm->numSockPoll-1; i++)
                   ProgStationForm->sockPollQue[i] = ProgStationForm->sockPollQue[i+1];

                 ProgStationForm->sockPollQue[ProgStationForm->numSockPoll-1].ip = "";
                 ProgStationForm->sockPollQue[ProgStationForm->numSockPoll-1].txFlag = false;
                 ProgStationForm->sockPollQue[ProgStationForm->numSockPoll-1].reader = 0;
                 ProgStationForm->sockPollQue[ProgStationForm->numSockPoll-1].host = 0;
                 ProgStationForm->sockPollQue[ProgStationForm->numSockPoll-1].txLen = 0;
                 for (int i=0; i<255; i++)
                    ProgStationForm->sockPollQue[ProgStationForm->numSockPoll-1].XBuf[i] = '\0';
                 ProgStationForm->numSockPoll -= 1;
              }

              index = GetIpAddressIndex(ip);
              if (index >= 0)
              {
                 strcpy(ProgStationForm->listViewInfo[index].netStatus, "Inactive");
                 strcpy(ProgStationForm->listViewInfo[index].rdrStatus,"Offline");
                 ProgStationForm->listViewInfo[index].host = 0;
                 ProgStationForm->listViewInfo[index].reader = 0;
                 ProgStationForm->listViewInfo[index].selected = false;

                 IPListView->Items->Item[i]->SubItems->Insert(0, ip); //ip //Text = ip;
                 IPListView->Items->Item[i]->SubItems->Insert(1, ""); //reader
                 IPListView->Items->Item[i]->SubItems->Insert(2, ""); //host
                 IPListView->Items->Item[i]->SubItems->Insert(3, "Inactive"); //network status
                 IPListView->Items->Item[i]->SubItems->Insert(4, "Offline"); //rdr status
                 IPListView->Items->Item[i]->SubItems->Insert(5, ""); //connect time

                 str = "IP = ";
                 str += ip;
                 str += "  was disconnected.";
                 ConnectLabel->Caption = str;
                 ConnectLabel->Update();
              }
           } //if selected
        } //for loop ipListView

        ProgStationForm->PollTimer->Enabled = true;
        //for (int i=0; i<IPListView->Items->Count; i++)
           //IPListView->Items->Item[i]->Checked = false;

        PlaySound("Ding.wav", NULL, SND_ASYNC );

    } //network TCPIP
}
//---------------------------------------------------------------------------
void __fastcall TCommConfigDlg::InitNetworkBitBtnClick(TObject *Sender)
{
    ClearIPListView();
    for (int i=0; i<MAX_DESCRIPTOR; i++)
    {
          networkInfo[i].activeSock = 0x00;
          networkInfo[i].peerSock = NULL;
          networkInfo[i].reader = 0;
          networkInfo[i].host = 0;
          strcpy(networkInfo[i].status, "Offline");
          networkInfo[i].active = false;
          networkInfo[i].selected = false;
          networkInfo[i].validRec = false;
          strcpy(networkInfo[i].ipAddr, "          ");
    }

    if (!KeepListItemCheckBox->Checked)
         IPListView->Items->Clear();

    if (networkOn)
    {
       if (tcipWindow)
       {
          TCPIPForm->ltx_closesocket(NULL, true);
          TCPIPForm->commThreadRunning = false;
       }

       TerminateThread(hTCPIPCommThread, 0);
       WSACleanup();

       /*ClearIPListView();
       for (int i=0; i<MAX_DESCRIPTOR; i++)
       {
          networkInfo[i].activeSock = 0x00;
          networkInfo[i].peerSock = NULL;
          networkInfo[i].reader = 0;
          networkInfo[i].host = 0;
          strcpy(networkInfo[i].status, "Offline");
          networkInfo[i].active = false;
          networkInfo[i].selected = false;
          networkInfo[i].validRec = false;
          strcpy(networkInfo[i].ipAddr, "          ");
       }*/


       if (NoEncryptRadioButton->Checked)
         encryption = 0;
       else
         encryption = 2;

       //if (!KeepListItemCheckBox->Checked)
         //IPListView->Items->Clear();

       if (UseSearchIPRadioButton->Checked)
       {
          ConnectLabel->Caption = "Searching the network ........     ";
          ConnectLabel->Update();
          //activeIPsIndex = 0;
          ScanForm->ScanNetwork();

          ConnectLabel->Caption = "Searching the Network Completed.";
          ConnectLabel->Update();
       }
    }
}
//------------------------------------------------------------------------------
int __fastcall TCommConfigDlg::GetActiveIpAddressIndex(AnsiString ip)
{
   int i;

   for (i=0; i<MAX_DESCRIPTOR; i++)
   {
      //if (networkInfo[i].activeSock[2] == 1)
      if (networkInfo[i].active)
      {
         if (ip == networkInfo[i].ipAddr)
            return (i);
      }
   }

   return (-1);
}
//------------------------------------------------------------------------------
void __fastcall TCommConfigDlg::GetAllSelectedItems()
{
   int indx;
   AnsiString ip;
   bool found = false;

   for (int i=0; i<IPListView->Items->Count; i++)
   {
      ip = GetItem(IPListView->Items->Item[i]->SubItems, 0);
      indx = GetIpAddressIndex(ip);

      if (IPListView->Items->Item[i]->Checked)
      {
         found = false;
         //ip = GetItem(IPListView->Items->Item[i]->SubItems, 0);

         if (indx >= 0)
            ProgStationForm->listViewInfo[indx].selected = true;

         for (unsigned int j=0; j<ProgStationForm->numSockConnected; j++)
         {
            if (ProgStationForm->sockConnectQue[j].ip == ip)
              found = true;
         }

         if (!found)
         {
            ProgStationForm->sockConnectQue[connectQueEntry].ip = ip;
            connectQueEntry += 1;
         }

         ProgStationForm->numSelectedSockConnect += 1;
      }//checked
      else
      {
         if (indx >= 0)
            ProgStationForm->listViewInfo[indx].selected = false;
      }
   }//for loop
}
//------------------------------------------------------------------------------
/*void __fastcall TCommConfigDlg::GetAllSelectedItems()
{
   AnsiString ip;
   bool found = false;

   for (int i=0; i<IPListView->Items->Count; i++)
   {
      if (IPListView->Items->Item[i]->Selected)
      {
         found = false;
         ip = GetItem(IPListView->Items->Item[i]->SubItems, 0);

         for (int j=0; j<ProgStationForm->numSockPoll; j++)
         {
            if (ProgStationForm->sockPollQue[j] == ip)
              found = true;
         }

         if (!found)
         {
            ProgStationForm->sockPollQue[ProgStationForm->numSockPoll] = ip;
            ProgStationForm->numSockPoll += 1;
         }
      }//selected
   }//for loop
}*/
//------------------------------------------------------------------------------
void __fastcall TCommConfigDlg::UpdateIPListView()
{
   IPListView->Items->Clear();
   TListItem* listItem;

   for (int i=0; i<listViewItemCount; i++)
   {
      listItem = IPListView->Items->Add();
      if (ProgStationForm->listViewInfo[i].selected)
         listItem->Checked = true;
      listItem->SubItems->Add(ProgStationForm->listViewInfo[i].ip);
      listItem->SubItems->Add(ProgStationForm->listViewInfo[i].reader);
      listItem->SubItems->Add(ProgStationForm->listViewInfo[i].host);
      listItem->SubItems->Add(ProgStationForm->listViewInfo[i].netStatus);
      listItem->SubItems->Add(ProgStationForm->listViewInfo[i].rdrStatus);
      listItem->SubItems->Add(""); //connect time
   }
}
//------------------------------------------------------------------------------
void __fastcall TCommConfigDlg::ConnectTimerTimer(TObject *Sender)
{
  /*for (int i=0; i<ProgStationForm->numSockPoll; i++)
  {
      ProgStationForm->ClientSocket->Active = false;
      ProgStationForm->ClientSocket->Address = ProgStationForm->sockPollQue[i];
      ProgStationForm->ClientSocket->Active = true;
  }

  //ProgStationForm->updateIpList = false; */
  /////////////////////////////////////////////
  //ProgStationForm->sockConnectQue[0].ip = "192.168.1.105";
   //ProgStationForm->sockPollQue[1] = "192.168.1.109";
   //ProgStationForm->numSockConnected = 1;

   /*AnsiString s;
   if (ProgStationForm->numSelectedSockConnect <= 0)
   {
      ConnectTimer->Enabled = false;
      return;
   }

   if (ProgStationForm->sockConnectQue[ProgStationForm->sockConnectQueIndex].numRetry > ProgStationForm->maxConnectRetry)
      ProgStationForm->numSelectedSockConnect -= 1;
   */

   if (ProgStationForm->numSockConnected < ProgStationForm->numSelectedSockConnect)
   {
      ProgStationForm->ClientSocket->Active = false;
      ProgStationForm->ClientSocket->Address = ProgStationForm->sockConnectQue[ProgStationForm->sockConnectQueIndex].ip;
      ProgStationForm->ClientSocket->Tag = ProgStationForm->sockConnectQueIndex; // + MAX_DESCRIPTOR;
      ProgStationForm->ClientSocket->Active = true;

   //ProgStationForm->temp = "Connect ";
   //ProgStationForm->temp += ProgStationForm->sockConnectQue[ProgStationForm->sockConnectQueIndex].ip;
      //if ( ProgStationForm->ClientSocket->Active)
      {
         ProgStationForm->sockConnectQueIndex += 1;
         if (ProgStationForm->sockConnectQueIndex >= ProgStationForm->numSelectedSockConnect)
            ProgStationForm->sockConnectQueIndex = 0;

         ProgStationForm->updateIpList = true;
         Msg->Caption = "";

      }
      /*else
      {
          s = "ip = ";
          s += ProgStationForm->sockConnectQue[ProgStationForm->sockConnectQueIndex].ip;
          s += " failed to connect.";
          Msg->Caption = s;
      }*/
   }
   else
   {
      ConnectTimer->Enabled = false;
      ProgStationForm->PollTimer->Enabled = true;
      ProgStationForm->updateIpList = false;
      ProgStationForm->ConnectingSockets = false;
   }
}
//------------------------------------------------------------------------------


void __fastcall TCommConfigDlg::AddIPBitBtnClick(TObject *Sender)
{
   AnsiString ip;

   ip = IPSpecEdit1->Text;
   ip += ".";
   ip += IPSpecEdit2->Text;
   ip += ".";
   ip += IPSpecEdit3->Text;
   ip += ".";
   ip += IPSpecEdit4->Text;

   int index = GetIpAddressIndex(ip);
   if (index >= 0)
   {
       Application->MessageBox( "ERROR: IP address already exits in the list.",
                                "Programming Station", MB_OK | MB_ICONEXCLAMATION );
       return;
   }

   index = listViewItemCount;
   listViewItemCount += 1;

    strcpy(ProgStationForm->listViewInfo[index].rdrStatus, "Offline");
    strcpy(ProgStationForm->listViewInfo[index].netStatus, "Inactive");
    ProgStationForm->listViewInfo[index].ip = ip;
    ProgStationForm->listViewInfo[index].reader = 0;
    ProgStationForm->listViewInfo[index].host = 0;
    //listViewInfo[index].selected = false;

    TListItem* item;
   item = IPListView->Items->Add();
   item->Checked = false;
   item->SubItems->Add(ip);
   item->SubItems->Add("0");
   item->SubItems->Add("0");
   item->SubItems->Add("Inactive");
   item->SubItems->Add("Offline");
}
//------------------------------------------------------------------------------
void __fastcall TCommConfigDlg::RemoveIPBitBtnClick(TObject *Sender)
{
    /*for (int i=0; i<IPListView->Items->Count; i++)
    {
       ip = GetItem(IPListView->Items->Item[i]->SubItems, 0);
       index = GetIpAddressIndex(ip);
       if (IPListView->Items->Item[i]->Checked)
          ProgStationForm->listViewInfo[i].selected = true;
       else
          ProgStationForm->listViewInfo[i].selected = false;
    }

    for (int i=0; i<IPListView->Items->Count; i++)
    {
       ip = GetItem(IPListView->Items->Item[i]->SubItems, 0);
       if (IPListView->Items->Item[i]->Checked)
       {
          s = "Do you want to remove IP = ";
          s += ip;
          int ret = Application->MessageBox(s.c_str(), "Programming Station", MB_YESNO);
          if (ret == IDNO)
             return;
          ProgStationForm->RemoveSocket(ip);
       }
    }*/
}
//------------------------------------------------------------------------------

