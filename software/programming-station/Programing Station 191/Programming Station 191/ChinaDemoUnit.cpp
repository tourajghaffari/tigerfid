//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include "ChinaDemoUnit.h"
#include "ProgStationUnit.h"
#include "Commands.h"
#include "TitleUnit.h"
//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma resource "*.dfm"
TChinaDemoForm *ChinaDemoForm;
//extern AnsiString demoTitle;
//extern unsigned short demoTags[5][2];
//---------------------------------------------------------------------------
__fastcall TChinaDemoForm::TChinaDemoForm(TComponent* Owner)
        : TForm(Owner)
{
   ChinaDemoForm = this;
   //ProgStationForm->closeChinaDemo = false;

   for (int i=0; i<=5; i++)
   {
      chinaTags[i].tagID = 0;
      chinaTags[i].responded = false;
      chinaTags[i].nTries = 0;
   }
}
//---------------------------------------------------------------------------
void __fastcall TChinaDemoForm::FormClose(TObject *Sender,
      TCloseAction &Action)
{
   //ProgStationForm->closeChinaDemo = true;
   CloseDemo();
}
//-----------------------------------------------------------------------------
void __fastcall TChinaDemoForm::SendCallTag()
{

   if (normal)
   {
      ProgStationForm->WriteComm(RESET_DEVICE, 0, NULL, 0);
      //Sleep(2000);
   }
   else
      normal = true;

   counter = 0;

   /*for (int i=0; i<=4; i++)
   {
      chinaTags[i].tagID = 0;
      chinaTags[i].responded = false;
      chinaTags[i].nTries = 0;
   }*/

   MaxChinaTags = 0;

   if (Tag1Edit->Text.data() != NULL)
      MaxChinaTags++;
   if (Tag2Edit->Text.data() != NULL)
      MaxChinaTags++;
   if (Tag3Edit->Text.data() != NULL)
      MaxChinaTags++;
   if (Tag4Edit->Text.data() != NULL)
      MaxChinaTags++;
   if (Tag5Edit->Text.data() != NULL)
      MaxChinaTags++;

   for (int i=0; i<MaxChinaTags; i++)
   {
      if (!chinaTags[i].responded)
         chinaTags[i].nTries = 0;
   }
   //NormalRelayTimer->Interval =  atoi(NormalRelayTimeEdit->Text.c_str()) * 1000;
   //AbnormalRelayTimer->Interval = atoi(AbnormalRelayTimeEdit->Text.c_str()) * 1000;
   //NormalRelayCloseTimer->Interval = atoi(CloseRelayEdit->Text.c_str()) * 1000;
   //AbnormalRelayCloseTimer->Interval = atoi(CloseRelayEdit->Text.c_str()) * 1000;

   /*chinaTags[0].tagID = atoi(Tag1Edit->Text.c_str());
      chinaTags[1].tagID = atoi(Tag2Edit->Text.c_str());
      chinaTags[2].tagID = atoi(Tag3Edit->Text.c_str());
      chinaTags[3].tagID = atoi(Tag4Edit->Text.c_str());
      chinaTags[4].tagID = atoi(Tag5Edit->Text.c_str());*/
   bool scan = false;
   for (int i=0; i<MaxChinaTags; i++)
   {
      if (!chinaTags[i].responded)
      {
        scan = true;
        break;
      }
   }

   //ProgStationForm->chinaTagIndex = 0;

   if (scan)
   {
      tagID = chinaTags[0].tagID;
      lastTagID = 0;
      CallTagTimer->Enabled = true;
      AbnormalRelayTimer->Enabled = true;
      allowToStart = true;
      DemoStatusLabel->Caption = "Demo Running ...";
      //ProgStationForm->chinaDemoON = true;
      StartDemoBitBtn->Enabled = false;
   }
   else
      tagCallDone = true;
      //SysMsgLabel->Caption = "Message: No Tag ID!";
}
//---------------------------------------------------------------------------
void __fastcall TChinaDemoForm::StartDemoBitBtnClick(TObject *Sender)
{
   /*demoTags[0][0] = atoi(Tag1Edit->Text.c_str());
   demoTags[1][0] = atoi(Tag2Edit->Text.c_str());
   demoTags[2][0] = atoi(Tag3Edit->Text.c_str());
   demoTags[3][0] = atoi(Tag4Edit->Text.c_str());
   demoTags[4][0] = atoi(Tag5Edit->Text.c_str());*/

   //ProgStationForm->chinaDemoON = true;
   allowToStart = false;

   NormalRelayTimer->Interval =  atoi(NormalRelayTimeEdit->Text.c_str()) * 1000;
   AbnormalRelayTimer->Interval = atoi(AbnormalRelayTimeEdit->Text.c_str()) * 1000;
   NormalRelayCloseTimer->Interval = atoi(CloseRelayEdit->Text.c_str()) * 1000;
   AbnormalRelayCloseTimer->Interval = atoi(CloseRelayEdit->Text.c_str()) * 1000;

   if (atoi(NormalRelayTimeEdit->Text.c_str()) < 80)
   {
      ::MessageBoxEx(::GetDesktopWindow(), ( LPCSTR )"Minimum value for Time Interval for Enabling Relay #1 is  60 seconds.",
      ( LPCSTR )"Demo Information",
      MB_OK | MB_ICONSTOP | MB_TOPMOST  , LANG_ENGLISH );
      return;
   }

   if (atoi(AbnormalRelayTimeEdit->Text.c_str()) < 40)
   {
      ::MessageBoxEx(::GetDesktopWindow(), ( LPCSTR )"Minimum value for Time Interval to check for Abnormal (Relay #2) is  40 seconds.",
      ( LPCSTR )"Demo Information",
      MB_OK | MB_ICONSTOP | MB_TOPMOST  , LANG_ENGLISH );
      return;
   } 

   if ((Tag1Edit->Text.data() == NULL) &&
       (Tag2Edit->Text.data() == NULL) &&
       (Tag3Edit->Text.data() == NULL) &&
       (Tag4Edit->Text.data() == NULL) &&
       (Tag5Edit->Text.data() == NULL))
   {
       ::MessageBoxEx(::GetDesktopWindow(), ( LPCSTR )"No Tag ID is Selected",
       ( LPCSTR )"Demo Information",
       MB_OK | MB_ICONSTOP | MB_TOPMOST  , LANG_ENGLISH );
       return;
   }
   TDateTime time = Now();
   Word Hour=0, Min=0, Sec=0, MSec=0;

   DecodeTime(time, Hour, Min, Sec, MSec);
   startTime = Hour*60*60*1000 + Min*60*1000 + Sec*1000;
   //startTime -= 5000;
   startDemo = false;

   ProgStationForm->WriteComm(RESET_DEVICE, 0, NULL, 0);
   //Sleep(5000);

   StartingTimer->Enabled = true;
   ClearTagStatus();
   allOk = false;
   tagCallDone = false;
   //EnableRel1CheckBox->Checked = false;
   EnableRelay01Label(false);
   //EnableRel2CheckBox->Checked = false;
   EnableRelay02Label(false);
   NormalRelayTimer->Enabled = true;    //normal relay enabled
   NoRespTimer->Enabled = true;         //no response enabled
   AbnormalRelayTimer->Enabled = true;  //
   //AbnormalRelayCloseTimer->Enabled = true;
   //DemoStatusLabel->Caption = "Message: ";
   StartDemoBitBtn->Enabled = false;
   DemoStatusLabel->Caption = "Demo Running ...";
   startDemo = true;
   //SendCallTag();



   //ProgStationForm->timeCheckTags = atoi(TimeCheckTagEdit->Text.c_str());
   //ProgStationForm->timeCheckTags *= 1000;
   //ProgStationForm->timeSendStatus = atoi(TimeSendStatusEdit->Text.c_str());
   //ProgStationForm->timeSendStatus *= 1000;
   //ProgStationForm->ChinaTimer1->Interval = 500;
   //ProgStationForm->ChinaTimer1->Enabled = true;

   //SendOKTimer->Interval = ProgStationForm->timeSendStatus;
   //SendOKTimer->Enabled = true;

   //sendOK = true;


   //AbnormalRelayTimer->Interval =  atoi(AbnormalRelayTimeEdit->Text.c_str());



   /*if (ProgStationForm->MaxChinaTags > 0)
   {
      tagID = chinaTags[0].tagID;
      //CallTagTimer->Enabled = true;
      DemoStatusLabel->Caption = "Demo Running ...";
   }
   else
      SysMsgLabel->Caption = "Message: No Tag!"; */

}
//---------------------------------------------------------------------------
void __fastcall TChinaDemoForm::StopDemoBitBtnClick(TObject *Sender)
{
   //ProgStationForm->chinaDemoON = false;
   //ProgStationForm->ChinaTimer2->Enabled = false;
   //ProgStationForm->ChinaTimer1->Enabled = false;
   //SendOKTimer->Enabled = false;
   allOk = false;
   startDemo = false;
   StartingTimer->Enabled = false;
   StartDemoBitBtn->Enabled = true;
   NoRespTimer->Enabled = false;
   AbnormalRelayTimer->Enabled = false;
   NormalRelayTimer->Enabled = false;
   NormalRelayCloseTimer->Enabled = false;
   AbnormalRelayCloseTimer->Enabled = false;
   CallTagTimer->Enabled = false;

   NormalRelayTimer->Enabled = false;
   NoRespTimer->Enabled = false;
   AbnormalRelayTimer->Enabled = false;
   AbnormalRelayCloseTimer->Enabled = false;

   //ProgStationForm->chinaDemoON = false;

   DemoStatusLabel->Caption = "Demo Stopped ...";
}
//---------------------------------------------------------------------------
void __fastcall TChinaDemoForm::BitBtn3Click(TObject *Sender)
{
   CloseDemo();
}
//------------------------------------------------------------------------------
void __fastcall TChinaDemoForm::CloseDemo()
{
   //ProgStationForm->chinaDemoON = false;
   ProgStationForm->ChinaTimer2->Enabled = false;
   ProgStationForm->ChinaTimer1->Enabled = false;

   allOk = false;
   StartDemoBitBtn->Enabled = true;
   NoRespTimer->Enabled = false;
   AbnormalRelayTimer->Enabled = false;
   NormalRelayTimer->Enabled = false;
   NormalRelayCloseTimer->Enabled = false;
   AbnormalRelayCloseTimer->Enabled = false;
   CallTagTimer->Enabled = false;
   StartingTimer->Enabled = false;

   NormalRelayTimer->Enabled = false;
   NoRespTimer->Enabled = false;
   AbnormalRelayTimer->Enabled = false;
   AbnormalRelayCloseTimer->Enabled = false;

   ProgStationForm->EnableReaderBitBtn->Enabled = true;
   ProgStationForm->DisableReaderBitBtn->Enabled = true;
   ProgStationForm->QueryReaderBitBtn->Enabled = true;
   ProgStationForm->AssignReaderBitBtn->Enabled = true;
   ProgStationForm->EnableFGenBitBtn->Enabled = true;
   ProgStationForm->ConfigTagBitBtn->Enabled = true;
   ProgStationForm->EnableTagBitBtn->Enabled = true;
   ProgStationForm->DisableTagBitBtn->Enabled = true;
   ProgStationForm->QueryTagBitBtn->Enabled = true;
   ProgStationForm->CallTagBitBtn->Enabled = true;
   //ProgStationForm->ChinaDemoToolButton->Enabled = true;
}
//------------------------------------------------------------------------------
void __fastcall TChinaDemoForm::UpdateChinaMsg(int relayNum, int type)
{
   AnsiString str;

   if (type == 0)  //enable
   {
      if (relayNum == 1)
      {
         str = "Message Relay #1: ";
         str += " Normal Relay Was Enabled.";
         //EnableRel1CheckBox->Checked = true;
         EnableRelay01Label(true);
         NormalRelayCloseTimer->Enabled = true;
         SysMsgLabel_1->Caption = str;
      }
      else
      {
         str = "Message Relay #2: ";
         str += "Abnormal Relay Was Enabled.";
         //EnableRel2CheckBox->Checked = true;
         EnableRelay02Label(true);
         AbnormalRelayCloseTimer->Enabled = true;
         SysMsgLabel_2->Caption = str;
      }
   }
   else  //disable
   {
      //SysMsgLabel->Caption = str;
      if (relayNum == 1)
      {
         str = "Message Relay #1: ";
         SysMsgLabel_1->Caption = str;
         //EnableRel1CheckBox->Checked = false;
         EnableRelay01Label(false);
         NormalRelayCloseTimer->Enabled = false;
         //NormalRelayCloseTimer->Interval = atoi(NormalRelayTimeEdit->Text.c_str()) * 1000;
      }
      else
      {
         str = "Message Relay #1: ";
         SysMsgLabel_2->Caption = str;
         //EnableRel2CheckBox->Checked = false;
         EnableRelay02Label(false);
         AbnormalRelayCloseTimer->Enabled = false;
         //AbnormalRelayCloseTimer->Interval = atoi(AbnormalRelayTimeEdit->Text.c_str()) * 1000;
      }
   }

}
//---------------------------------------------------------------------------
/*void __fastcall TChinaDemoForm::UpdateChinaDemoScreenGlobal(int tagID)
{
   for (int i=0; i<4; i++)
   {
      if (chinaTags[i].tagID == tagID)
      {
          if (i == 0)
          {
              //Tag1OKCheckBox->Checked = true;
              //Tag1NoResCheckBox->Checked = false;
              UpdateTagStatus01Edit(true);

              chinaTags[i].responded = true;
              chinaTags[i].nTries = 3;
          }
          else if (i == 1)
          {
              //Tag2OKCheckBox->Checked = true;
              //Tag2NoResCheckBox->Checked = false;
              UpdateTagStatus02Edit(true);

              chinaTags[i].responded = true;
              chinaTags[i].nTries = 3;
          }
          else if (i == 2)
          {
              //Tag3OKCheckBox->Checked = true;
              //Tag3NoResCheckBox->Checked = false;
              UpdateTagStatus03Edit(true);

              chinaTags[i].responded = true;
              chinaTags[i].nTries = 3;
          }
          else if (i == 3)
          {
              //Tag4OKCheckBox->Checked = true;
              //Tag4NoResCheckBox->Checked = false;
              UpdateTagStatus04Edit(true);

              chinaTags[i].responded = true;
              chinaTags[i].nTries = 3;
          }
          else if (i == 4)
          {
              //Tag5OKCheckBox->Checked = true;
              //Tag5NoResCheckBox->Checked = false;
              UpdateTagStatus05Edit(true);

              chinaTags[i].responded = true;
              chinaTags[i].nTries = 3;
          }
      }
   }
}*/
//-----------------------------------------------------------------------------
bool __fastcall TChinaDemoForm::CheckForAllDone()
{
   int num=0;
   if (Tag1Edit->Text.data() != NULL)
      num++;
   if (Tag2Edit->Text.data() != NULL)
      num++;
   if (Tag3Edit->Text.data() != NULL)
      num++;
   if (Tag4Edit->Text.data() != NULL)
      num++;
   if (Tag5Edit->Text.data() != NULL)
      num++;

   bool done = true;
   for (int i=0; i<num; i++)
   {
      if (!chinaTags[i].responded)
      {
        done = false;
        break;
      }
   }

   return (done);
}
//---------------------------------------------------------------------------
void __fastcall TChinaDemoForm::UpdateChinaDemoScreen(int tagID)
{
   for (int i=0; i<MaxChinaTags; i++)
   {
      if (chinaTags[i].tagID == tagID)
      {
          if (i == 0)
          {
              //Tag1OKCheckBox->Checked = true;
              //Tag1NoResCheckBox->Checked = false;
              UpdateTagStatus01Edit(true);

              chinaTags[i].responded = true;
              chinaTags[i].nTries = 3;
          }
          else if (i == 1)
          {
              //Tag2OKCheckBox->Checked = true;
              //Tag2NoResCheckBox->Checked = false;
              UpdateTagStatus02Edit(true);

              chinaTags[i].responded = true;
              chinaTags[i].nTries = 3;
          }
          else if (i == 2)
          {
              //Tag3OKCheckBox->Checked = true;
              //Tag3NoResCheckBox->Checked = false;
              UpdateTagStatus03Edit(true);

              chinaTags[i].responded = true;
              chinaTags[i].nTries = 3;
          }
          else if (i == 3)
          {
              //Tag4OKCheckBox->Checked = true;
              //Tag4NoResCheckBox->Checked = false;
              UpdateTagStatus04Edit(true);

              chinaTags[i].responded = true;
              chinaTags[i].nTries = 3;
          }
          else if (i == 4)
          {
              //Tag5OKCheckBox->Checked = true;
              //Tag5NoResCheckBox->Checked = false;
              UpdateTagStatus05Edit(true);

              chinaTags[i].responded = true;
              chinaTags[i].nTries = 3;
          }
      }
   }

   if (CheckForAllDone())
      tagCallDone = true;

}
//----------------------------------------------------------------
void __fastcall TChinaDemoForm::ClearTagStatus()
{

      DisableTagStatus01Edit();
      DisableTagStatus02Edit();
      DisableTagStatus03Edit();
      DisableTagStatus04Edit();
      DisableTagStatus05Edit();

      /*Tag1OKCheckBox->Checked = false;
      Tag1NoResCheckBox->Checked = false;
      Tag2OKCheckBox->Checked = false;
      Tag2NoResCheckBox->Checked = false;
      Tag3OKCheckBox->Checked = false;
      Tag3NoResCheckBox->Checked = false;
      Tag4OKCheckBox->Checked = false;
      Tag4NoResCheckBox->Checked = false;
      Tag5OKCheckBox->Checked = false;
      Tag5NoResCheckBox->Checked = false;*/

      for (int i=0; i<=5; i++)
      {
         chinaTags[i].tagID = 0;
         chinaTags[i].responded = false;
         chinaTags[i].nTries = 0;
      }

      //EnableRel1CheckBox->Checked = false;
      EnableRelay01Label(false);
      //EnableRel2CheckBox->Checked = false;
      EnableRelay02Label(false);
      SysMsgLabel_1->Caption = "Message Relay #1: ";
      SysMsgLabel_2->Caption = "Message Relay #2: ";

}
//--------------------------------------------------------------------------
void __fastcall TChinaDemoForm::ClearBitBtnClick(TObject *Sender)
{
   ClearTagStatus();
   //EnableRel1CheckBox->Checked = false;
   EnableRelay01Label(false);
   //EnableRel2CheckBox->Checked = false;
   EnableRelay02Label(false);
   SysMsgLabel_1->Caption = "Message Relay #1: ";
   SysMsgLabel_2->Caption = "Message Relay #2: ";
}
//---------------------------------------------------------------------------
void __fastcall TChinaDemoForm::UpdateForNoResponse()
{
   bool relay2On = false;

   for (int i=0; i<MaxChinaTags; i++)
   {
      if (chinaTags[i].responded == false)
      {
          if (i == 0)
          {
              //Tag1OKCheckBox->Checked = false;
              //Tag1NoResCheckBox->Checked = true;
              UpdateTagStatus01Edit(false);

              //EnableRel2CheckBox->Checked = true;
              //EnableRelay02Label(true);
              //if (!relayOn)
                 //ProgStationForm->WriteComm(ENABLE_RELAY, 0, "1", 91);
              relay2On = true;
          }
          else if (i == 1)
          {
              //Tag2OKCheckBox->Checked = false;
              //Tag2NoResCheckBox->Checked = true;
              UpdateTagStatus02Edit(false);

              //EnableRel2CheckBox->Checked = true;
              //if (!relayOn)
                 //ProgStationForm->WriteComm(ENABLE_RELAY, 0, "1", 91);
              relay2On = true;
          }
          else if (i == 2)
          {
              //Tag3OKCheckBox->Checked = false;
              //Tag3NoResCheckBox->Checked = true;
              UpdateTagStatus03Edit(false);
              //EnableRel2CheckBox->Checked = true;
              //if (!relayOn)
                 //ProgStationForm->WriteComm(ENABLE_RELAY, 0, "1", 91);
              relay2On = true;
          }
          else if (i == 3)
          {
              //Tag4OKCheckBox->Checked = false;
              //Tag4NoResCheckBox->Checked = true;
              UpdateTagStatus04Edit(false);
              //EnableRel2CheckBox->Checked = true;
              //if (!relayOn)
                 //ProgStationForm->WriteComm(ENABLE_RELAY, 0, "1", 91);
              relay2On = true;
          }
          else if (i == 4)
          {
              //Tag5OKCheckBox->Checked = false;
              //Tag5NoResCheckBox->Checked = true;
              UpdateTagStatus05Edit(false);
              //EnableRel2CheckBox->Checked = true;
              //if (!relayOn)
                 //ProgStationForm->WriteComm(ENABLE_RELAY, 0, "1", 91);
              relay2On = true;
          }
      }
   }

   //ProgStationForm->relay2On = relay2On;
   if (relay2On)
   {
      EnableRelay02Label(true);
      ProgStationForm->WriteComm(ENABLE_RELAY, 0, "2", 82);
      //AbnormalRelayCloseTimer->Enabled = true;
      allOk = false;
      NormalRelayTimer->Enabled = false;
      NormalRelayTimer->Enabled = true;
      //ProgStationForm->relay2On = false;
   }
   else
     allOk = true;
}
//---------------------------------------------------------------------------
void __fastcall TChinaDemoForm::NormalRelayTimerTimer(TObject *Sender)
{
   if (allOk)
   {
      ProgStationForm->WriteComm(ENABLE_RELAY, 0, "1", 81);
      //NormalRelayCloseTimer->Enabled = true;
      allOk = false;
   }
}
//---------------------------------------------------------------------------

void __fastcall TChinaDemoForm::NormalRelayCloseTimerTimer(TObject *Sender)
{
   //if (ProgStationForm->relay1On)
   {
      ProgStationForm->WriteComm(DISABLE_RELAY, 0, "1", 91);
   }

   //NormalRelayCloseTimer->Enabled = false;
   //StartDisableRelay->Enabled = true;

}
//---------------------------------------------------------------------------
void __fastcall TChinaDemoForm::CallTagTimerTimer(TObject *Sender)
{
   int id;

   /*if (lastTagID != tagID)
   {
      ProgStationForm->AccessCtrlRadioButton->Checked = true;
      ProgStationForm->TagIDRadioButton->Checked = true;
      ProgStationForm->TagIDEdit->Text = tagID;
      chinaTags[ProgStationForm->chinaTagIndex].nTries += 1;
      ProgStationForm->ChinaDemoCallTag();
      lastTagID = tagID;
   }
   else
   { */
      /*if (chinaTags[ProgStationForm->chinaTagIndex].nTries >= 3)
      {
          if ((id=GetNextTagID()) > 0)
          {
              tagID = id;
              ProgStationForm->AccessCtrlRadioButton->Checked = true;
              ProgStationForm->TagIDRadioButton->Checked = true;
              ProgStationForm->TagIDEdit->Text = tagID;
              chinaTags[ProgStationForm->chinaTagIndex].nTries += 1;
              //ProgStationForm->WriteComm(RESET_DEVICE, 0, NULL, 0);
              //Sleep(2000);
              ProgStationForm->ChinaDemoCallTag();
              lastTagID = tagID;
          }
          else
             return;
      }
      else
      {
         //if (counter == 0)
         {
              ProgStationForm->AccessCtrlRadioButton->Checked = true;
              ProgStationForm->TagIDRadioButton->Checked = true;

              chinaTags[ProgStationForm->chinaTagIndex].tagID;
              ProgStationForm->TagIDEdit->Text = tagID;
              chinaTags[ProgStationForm->chinaTagIndex].nTries += 1;
              //ProgStationForm->WriteComm(RESET_DEVICE, 0, NULL, 0);
              //Sleep(2000);
              if (chinaTags[ProgStationForm->chinaTagIndex].nTries == 1)
                ProgStationForm->ChinaDemoCallTag();
              else
                tagCallDone = true;
              lastTagID = tagID;
              counter += 1;
         }
         //else
         //{
              //counter = 0;
         //}
      }//else
   //}//else */
}
//---------------------------------------------------------------------------
int __fastcall TChinaDemoForm::GetNextTagID()
{
    for (int i=0; i<MaxChinaTags; i++)
    {
       if (chinaTags[i].responded == false)
       {
          if (chinaTags[i].nTries < 1)
          {
             //ProgStationForm->chinaTagIndex = i;
             tagCallDone = false;
             return (chinaTags[i].tagID);
          }
       }
    }

    tagCallDone = true;
    return (-1);
}
//--------------------------------------------------------------------------
void __fastcall TChinaDemoForm::NoRespTimerTimer(TObject *Sender)
{
    if (tagCallDone)
    {
      tagCallDone = false;
      CallTagTimer->Enabled = false;
      UpdateForNoResponse();
    }
}
//---------------------------------------------------------------------------
void __fastcall TChinaDemoForm::AbnormalRelayTimerTimer(TObject *Sender)
{
   if (allowToStart)
   {
   DisableTagStatus01Edit();
   DisableTagStatus02Edit();
   DisableTagStatus03Edit();
   DisableTagStatus04Edit();
   DisableTagStatus05Edit();

   /*Tag1OKCheckBox->Checked = false;
   Tag1NoResCheckBox->Checked = false;
   Tag2OKCheckBox->Checked = false;
   Tag2NoResCheckBox->Checked = false;
   Tag3OKCheckBox->Checked = false;
   Tag3NoResCheckBox->Checked = false;
   Tag4OKCheckBox->Checked = false;
   Tag4NoResCheckBox->Checked = false;
   Tag5OKCheckBox->Checked = false;
   Tag5NoResCheckBox->Checked = false;*/

   for (int i=0; i<=4; i++)
   {
      chinaTags[i].tagID = 0;
      chinaTags[i].responded = false;
      chinaTags[i].nTries = 0;
   }
   

   //SendCallTag();
   //ProgStationForm->chinaDemoON = true;
   //ProgStationForm->ClearForGlobalChina();

   TDateTime time = Now();
   DWORD now;
   Word Hour=0, Min=0, Sec=0, MSec=0;

   DecodeTime(time, Hour, Min, Sec, MSec);
   startSerachTime = Hour*60*60*1000 + Min*60*1000 + Sec*1000;
   StartSearchForMissingTagTimer->Enabled = true;

   SendGlobal();
   }
}
//---------------------------------------------------------------------------


void __fastcall TChinaDemoForm::AbnormalRelayCloseTimerTimer(
      TObject *Sender)
{
   //if (ProgStationForm->relay2On)
   {
      //ProgStationForm->relay2On = false;
      ProgStationForm->WriteComm(DISABLE_RELAY, 0, "2", 92);
      //StartDisableRelay->Enabled = true;
   }

   //AbnormalRelayCloseTimer->Enabled = false;
}
//---------------------------------------------------------------------------

void __fastcall TChinaDemoForm::StartingTimerTimer(TObject *Sender)
{
    TDateTime time = Now();
    DWORD now;
    Word Hour=0, Min=0, Sec=0, MSec=0;

    DecodeTime(time, Hour, Min, Sec, MSec);
    now = Hour*60*60*1000 + Min*60*1000 + Sec*1000;

   if (startDemo)
   {
      if (now - startTime >  AbnormalRelayTimer->Interval)
      {
         StartingTimer->Enabled = false;
         startDemo = false;
         normal = false;
         MaxChinaTags = 4;
         //ProgStationForm->chinaDemoON = true;
         //ProgStationForm->ClearForGlobalChina();
         SendGlobal();
         startSerachTime = now;
         StartSearchForMissingTagTimer->Enabled = true;
         //SendCallTag();
      }
   }
}
//---------------------------------------------------------------------------
void __fastcall TChinaDemoForm::SendGlobal()
{
      //ProgStationForm->AccessCtrlRadioButton->Checked = true;
      //ProgStationForm->TagIDRadioButton->Checked = false;
      //ProgStationForm->TagIDEdit->Text = "";

      chinaTags[0].tagID = atoi(Tag1Edit->Text.c_str());
      chinaTags[1].tagID = atoi(Tag2Edit->Text.c_str());
      chinaTags[2].tagID = atoi(Tag3Edit->Text.c_str());
      chinaTags[3].tagID = atoi(Tag4Edit->Text.c_str());
      chinaTags[4].tagID = atoi(Tag5Edit->Text.c_str());

      //ProgStationForm->WriteComm(CALL_TAG, 0, NULL, 0);
}
//---------------------------------------------------------------------------
void __fastcall TChinaDemoForm::EnableRelay01Label(bool b)
{
    if (b)
    {
       NormalRelay1Label->Caption = "NORMAL Relay  (Relay #1)  ENABLED.";
       NormalRelay1Label->Color = clGreen;
       NormalRelay1Label->Font->Color = clWhite;
       NormalRelay1Label->Invalidate();
    }
    else
    {
       NormalRelay1Label->Caption = "NORMAL Relay  (Relay #1).";
       NormalRelay1Label->Color = clInactiveBorder;
       NormalRelay1Label->Font->Color = clInactiveCaption;
       NormalRelay1Label->Invalidate();
    }
}
//------------------------------------------------------------------------------
void __fastcall TChinaDemoForm::EnableRelay02Label(bool b)
{
    if (b)
    {
       AbnormalRelay2Label->Caption = "ABNORMAL Relay  (Relay #2)  ENABLED.";
       AbnormalRelay2Label->Color = clRed;
       AbnormalRelay2Label->Font->Color = clWhite;
       AbnormalRelay2Label->Invalidate();
    }
    else
    {
       AbnormalRelay2Label->Caption = "ABNORMAL Relay  (Relay #2).";
       AbnormalRelay2Label->Color = clInactiveBorder;
       AbnormalRelay2Label->Font->Color = clInactiveCaption;
       AbnormalRelay2Label->Invalidate();
    }
}
//------------------------------------------------------------------------------
void __fastcall TChinaDemoForm::UpdateTagStatus01Edit(bool b)
{
    if (b)
    {
       TagStatus01Edit->Text = "                OK";
       TagStatus01Edit->Color = clGreen;
       TagStatus01Edit->Font->Color = clWhite;
       TagStatus01Edit->Invalidate();
    }
    else
    {
       TagStatus01Edit->Text = "      NO RESPONSE";
       TagStatus01Edit->Color = clRed;
       TagStatus01Edit->Font->Color = clWhite;
       TagStatus01Edit->Invalidate();
    }
}
//------------------------------------------------------------------------------
void __fastcall TChinaDemoForm::DisableTagStatus01Edit()
{
   TagStatus01Edit->Text = "";
   TagStatus01Edit->Color = clInactiveCaptionText;
   TagStatus01Edit->Invalidate();
}
//------------------------------------------------------------------------------
void __fastcall TChinaDemoForm::UpdateTagStatus02Edit(bool b)
{
    if (b)
    {
       TagStatus02Edit->Text = "                OK";
       TagStatus02Edit->Color = clGreen;
       TagStatus02Edit->Font->Color = clWhite;
       TagStatus02Edit->Invalidate();
    }
    else
    {
       TagStatus02Edit->Text = "      NO RESPONSE";
       TagStatus02Edit->Color = clRed;
       TagStatus02Edit->Font->Color = clWhite;
       TagStatus02Edit->Invalidate();
    }
}
//------------------------------------------------------------------------------
void __fastcall TChinaDemoForm::DisableTagStatus02Edit()
{
   TagStatus02Edit->Text = "";
   TagStatus02Edit->Color = clInactiveCaptionText;
   TagStatus02Edit->Invalidate();
}
//------------------------------------------------------------------------------
void __fastcall TChinaDemoForm::UpdateTagStatus03Edit(bool b)
{
    if (b)
    {
       TagStatus03Edit->Text = "                OK";
       TagStatus03Edit->Color = clGreen;
       TagStatus03Edit->Font->Color = clWhite;
       TagStatus03Edit->Invalidate();
    }
    else
    {
       TagStatus03Edit->Text = "      NO RESPONSE";
       TagStatus03Edit->Color = clRed;
       TagStatus03Edit->Font->Color = clWhite;
       TagStatus03Edit->Invalidate();
    }
}
//------------------------------------------------------------------------------
void __fastcall TChinaDemoForm::DisableTagStatus03Edit()
{
   TagStatus03Edit->Text = "";
   TagStatus03Edit->Color = clInactiveCaptionText;
   TagStatus03Edit->Invalidate();
}
//------------------------------------------------------------------------------
void __fastcall TChinaDemoForm::UpdateTagStatus04Edit(bool b)
{
    if (b)
    {
       TagStatus04Edit->Text = "                OK";
       TagStatus04Edit->Color = clGreen;
       TagStatus04Edit->Font->Color = clWhite;
       TagStatus04Edit->Invalidate();
    }
    else
    {
       TagStatus04Edit->Text = "      NO RESPONSE";
       TagStatus04Edit->Color = clRed;
       TagStatus04Edit->Font->Color = clWhite;
       TagStatus04Edit->Invalidate();
    }
}
//------------------------------------------------------------------------------
void __fastcall TChinaDemoForm::DisableTagStatus04Edit()
{
   TagStatus04Edit->Text = "";
   TagStatus04Edit->Color = clInactiveCaptionText;
   TagStatus04Edit->Invalidate();
}
//------------------------------------------------------------------------------
void __fastcall TChinaDemoForm::UpdateTagStatus05Edit(bool b)
{
    if (b)
    {
       TagStatus05Edit->Text = "                OK";
       TagStatus05Edit->Color = clGreen;
       TagStatus05Edit->Font->Color = clWhite;
       TagStatus05Edit->Invalidate();
    }
    else
    {
       TagStatus05Edit->Text = "      NO RESPONSE";
       TagStatus05Edit->Color = clRed;
       TagStatus05Edit->Font->Color = clWhite;
       TagStatus05Edit->Invalidate();
    }
}
//------------------------------------------------------------------------------
void __fastcall TChinaDemoForm::DisableTagStatus05Edit()
{
   TagStatus05Edit->Text = "";
   TagStatus05Edit->Color = clInactiveCaptionText;
   TagStatus05Edit->Invalidate();
}
//------------------------------------------------------------------------------
void __fastcall TChinaDemoForm::StartSearchForMissingTagTimerTimer(TObject *Sender)
{

   TDateTime time = Now();
   DWORD now;
   Word Hour=0, Min=0, Sec=0, MSec=0;

   DecodeTime(time, Hour, Min, Sec, MSec);
   now = Hour*60*60*1000 + Min*60*1000 + Sec*1000;

   if(startSerachTime+10000 >= now)
   {
       StartSearchForMissingTagTimer->Enabled = false;
       SendCallTag();
   }
}
//---------------------------------------------------------------------------
void __fastcall TChinaDemoForm::TitleBitBtnClick(TObject *Sender)
{
   TTitleForm* titleDlg = new TTitleForm (this);
   titleDlg->ShowModal();
   delete titleDlg;
   //Caption = demoTitle;
}
//---------------------------------------------------------------------------

