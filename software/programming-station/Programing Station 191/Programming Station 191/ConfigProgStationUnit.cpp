//---------------------------------------------------------------------------

#include <vcl.h>
#include <mmSystem.h>
#pragma hdrstop

#include "ConfigProgStationUnit.h"
#include "ProgStationUnit.h"
#include <Registry.hpp>
//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma resource "*.dfm"
TConfigProgStationForm *ConfigProgStationForm;
extern bool multiDisplayTagDetect;
extern bool duplicateTagFGenDetect;
extern bool duplicateTagGIDDetect;
//---------------------------------------------------------------------------
__fastcall TConfigProgStationForm::TConfigProgStationForm(TComponent* Owner)
        : TForm(Owner)
{
}
//-------------------------------------------------------------------------
void __fastcall TConfigProgStationForm::FormActivate(TObject *Sender)
{
   if (multiDisplayTagDetect)
      MultiDisplayTagCheckBox->Checked = true;
   else
      MultiDisplayTagCheckBox->Checked = false;

   dFgenFlag = duplicateTagFGenDetect;
   if (duplicateTagFGenDetect)
      DuplicateTagFGenCheckBox->Checked = true;
   else
      DuplicateTagFGenCheckBox->Checked = false;

   dGIdFlag = duplicateTagGIDDetect;
   if (duplicateTagGIDDetect)
      DuplicateTagGIDCheckBox->Checked = true;
   else
      DuplicateTagGIDCheckBox->Checked = false;

   OldHostIDEdit->Text = ProgStationForm->sysHostID;

   if (ProgStationForm->allHostID)
      AllHostCheckBox->Checked = true;
   else
      AllHostCheckBox->Checked = false;

   if (ProgStationForm->displayLowBattery)
      DisplayLowBatteryCheckBox->Checked = true;
   else
      DisplayLowBatteryCheckBox->Checked = false;

   AnsiString s;
   if (TempCalibCRadioButton->Checked)
   {
      s = s.FormatFloat("##0.###", ProgStationForm->tagTempCalibC);
      TempCalibEdit->Text = s;
   }
   else
   {
      float f = ((ProgStationForm->tagTempCalibC/5.0)*9.0); // + 32.0;
      s = s.FormatFloat("##0.###", f);
      TempCalibEdit->Text = s;
   }

   curTagTempCalibC = ProgStationForm->tagTempCalibC;

   if (ProgStationForm->tagTypesAbr[0].data() != NULL)
   {
      Type01CheckBox->Checked = true;
      Type01AbrEdit->Text = ProgStationForm->tagTypesAbr[0];
      Type01Edit->Text = ProgStationForm->tagTypes[0];
   }

   if (ProgStationForm->tagTypesAbr[1].data() != NULL)
   {
      Type02CheckBox->Checked = true;
      Type02AbrEdit->Text = ProgStationForm->tagTypesAbr[1];
      Type02Edit->Text = ProgStationForm->tagTypes[1];
   }

   if (ProgStationForm->tagTypesAbr[2].data() != NULL)
   {
      Type03CheckBox->Checked = true;
      Type03AbrEdit->Text = ProgStationForm->tagTypesAbr[2];
      Type03Edit->Text = ProgStationForm->tagTypes[2];
   }

   if (ProgStationForm->tagTypesAbr[3].data() != NULL)
   {
      Type04CheckBox->Checked = true;
      Type04AbrEdit->Text = ProgStationForm->tagTypesAbr[3];
      Type04Edit->Text = ProgStationForm->tagTypes[3];
   }

   if (ProgStationForm->tagTypesAbr[4].data() != NULL)
   {
      Type05CheckBox->Checked = true;
      Type05AbrEdit->Text = ProgStationForm->tagTypesAbr[4];
      Type05Edit->Text = ProgStationForm->tagTypes[4];
   }

   if (ProgStationForm->tagTypesAbr[5].data() != NULL)
   {
      Type06CheckBox->Checked = true;
      Type06AbrEdit->Text = ProgStationForm->tagTypesAbr[5];
      Type06Edit->Text = ProgStationForm->tagTypes[5];
   }
}
//---------------------------------------------------------------------------
void __fastcall TConfigProgStationForm::SaveBitBtnClick(TObject *Sender)
{
   if (MultiDisplayTagCheckBox->State == cbChecked)
   {
     multiDisplayTagDetect = true;
     ProgStationForm->MTLabel->Font->Color = clLime;
     ProgStationForm->MTLabel->Caption = "ON";
   }
   else
   {
     multiDisplayTagDetect = false;
     ProgStationForm->MTLabel->Font->Color = clRed;
     ProgStationForm->MTLabel->Caption = "OFF";
   }

   if (DuplicateTagFGenCheckBox->State == cbChecked)
   {
     duplicateTagFGenDetect = true;
     ProgStationForm->FGLabel->Font->Color = clLime;
     ProgStationForm->FGLabel->Caption = "ON";
   }
   else
   {
     duplicateTagFGenDetect = false;
     ProgStationForm->FGLabel->Font->Color = clRed;
     ProgStationForm->FGLabel->Caption = "OFF";
   }

   if (DuplicateTagGIDCheckBox->State == cbChecked)
   {
     duplicateTagGIDDetect = true;
     ProgStationForm->GroupLabel->Font->Color = clLime;
     ProgStationForm->GroupLabel->Caption = "ON";
   }
   else
   {
     duplicateTagGIDDetect = false;
     ProgStationForm->GroupLabel->Font->Color = clRed;
     ProgStationForm->GroupLabel->Caption = "OFF";
   }

   if ((dGIdFlag != duplicateTagGIDDetect) ||
       (dFgenFlag != duplicateTagFGenDetect))
       ProgStationForm->NewListItemCheckBox->Checked = false;


   //Defining the Tag Type
   //=======================================================
   unsigned short nTypes = 0;

   if (Type01CheckBox->Checked)
   {
      if ((Type01AbrEdit->Text.data() == NULL) ||
          (Type01Edit->Text.data() == NULL))
      {
         Application->MessageBox("Error: Abbreviation or name for Type 1 is not set",
                                 "Programming Station Information Dialog",
                                   MB_OK | MB_ICONSTOP | MB_TOPMOST);
           return;
      }

      localTagTypesAbr[nTypes] = Type01AbrEdit->Text;
      localTagTypes[nTypes++] = Type01Edit->Text;
      ProgStationForm->tagTypesUpdated = true;
   }
   else
   {
      localTagTypesAbr[nTypes] = "";
      localTagTypes[nTypes++] = "";
   }

   if (Type02CheckBox->Checked)
   {
      if ((Type02AbrEdit->Text.data() == NULL) ||
          (Type02Edit->Text.data() == NULL))
      {
         Application->MessageBox("Error: Abbreviation or name for Type 2 is not set",
                                 "Programming Station Information Dialog",
                                   MB_OK | MB_ICONSTOP | MB_TOPMOST);
           return;
      }

      localTagTypesAbr[nTypes] = Type02AbrEdit->Text;
      localTagTypes[nTypes++] = Type02Edit->Text;
      ProgStationForm->tagTypesUpdated = true;
   }
   else
   {
      localTagTypesAbr[nTypes] = "";
      localTagTypes[nTypes++] = "";
   }

   if (Type03CheckBox->Checked)
   {
      if ((Type03AbrEdit->Text.data() == NULL) ||
          (Type03Edit->Text.data() == NULL))
      {
         Application->MessageBox("Error: Abbreviation or name for Type 3 is not set",
                                 "Programming Station Information Dialog",
                                   MB_OK | MB_ICONSTOP | MB_TOPMOST);
           return;
      }

      localTagTypesAbr[nTypes] = Type03AbrEdit->Text;
      localTagTypes[nTypes++] = Type03Edit->Text;
      ProgStationForm->tagTypesUpdated = true;
   }
   else
   {
      localTagTypesAbr[nTypes] = "";
      localTagTypes[nTypes++] = "";
   }

   if (Type04CheckBox->Checked)
   {
      if ((Type04AbrEdit->Text.data() == NULL) ||
          (Type04Edit->Text.data() == NULL))
      {
         Application->MessageBox("Error: Abbreviation or name for Type 4 is not set",
                                 "Programming Station Information Dialog",
                                   MB_OK | MB_ICONSTOP | MB_TOPMOST);
           return;
      }

      localTagTypesAbr[nTypes] = Type04AbrEdit->Text;
      localTagTypes[nTypes++] = Type04Edit->Text;
      ProgStationForm->tagTypesUpdated = true;
   }
   else
   {
      localTagTypesAbr[nTypes] = "";
      localTagTypes[nTypes++] = "";
   }

   if (Type05CheckBox->Checked)
   {
      if ((Type05AbrEdit->Text.data() == NULL) ||
          (Type05Edit->Text.data() == NULL))
      {
         Application->MessageBox("Error: Abbreviation or name for Type 5 is not set",
                                 "Programming Station Information Dialog",
                                   MB_OK | MB_ICONSTOP | MB_TOPMOST);
           return;
      }

      localTagTypesAbr[nTypes] = Type05AbrEdit->Text;
      localTagTypes[nTypes++] = Type05Edit->Text;
      ProgStationForm->tagTypesUpdated = true;
   }
   else
   {
      localTagTypesAbr[nTypes] = "";
      localTagTypes[nTypes++] = "";
   }

   if (Type06CheckBox->Checked)
   {
      if ((Type06AbrEdit->Text.data() == NULL) ||
          (Type06Edit->Text.data() == NULL))
      {
         Application->MessageBox("Error: Abbreviation or name for Type 6 is not set",
                                 "Programming Station Information Dialog",
                                   MB_OK | MB_ICONSTOP | MB_TOPMOST);
           return;
      }

      localTagTypesAbr[nTypes] = Type06AbrEdit->Text;
      localTagTypes[nTypes++] = Type06Edit->Text;
      ProgStationForm->tagTypesUpdated = true;
   }
   else
   {
      localTagTypesAbr[nTypes] = "";
      localTagTypes[nTypes++] = "";
   }

   if (ProgStationForm->tagTypesUpdated)
   {
        ProgStationForm->numTagTypes = nTypes;

        if (CheckDuplicatTagTypeAbr())
        {
             Application->MessageBox("Error: Found duplicated tag type abbreviation",
                                     "Programming Station Information Dialog",
                                     MB_OK | MB_ICONSTOP | MB_TOPMOST);
             return;
        }

        if (CheckDuplicatTagType())
        {
             Application->MessageBox("Error: Found duplicated tag type name",
                                     "Programming Station Information Dialog",
                                     MB_OK | MB_ICONSTOP | MB_TOPMOST);
             return;
         }

         for (int i=0; i<6; i++)
         {
            ProgStationForm->tagTypesAbr[i] = localTagTypesAbr[i];
            ProgStationForm->tagTypes[i] = localTagTypes[i];
         }

      //Registry-----------------------------
      TRegistry* Reg = new TRegistry;
      Reg->RootKey = HKEY_CURRENT_USER;
      if (Reg->KeyExists("Software"))
      {
         Reg->OpenKey("Software", false);
         if (!Reg->KeyExists("Active Wave"))
            Reg->CreateKey("Active Wave");

         Reg->OpenKey("Active Wave", false);

         if (!Reg->KeyExists("Programming Station"))
            Reg->CreateKey("Programming Station");

         Reg->OpenKey("Programming Station", false);
         Reg->WriteInteger("Host", ProgStationForm->sysHostID);

         if (!Reg->KeyExists("Type01"))
            Reg->CreateKey("Type01");
         Reg->WriteString("Type01", ProgStationForm->tagTypes[0]);

         if (!Reg->KeyExists("Type01Abr"))
            Reg->CreateKey("Type01Abr");
         Reg->WriteString("Type01Abr", ProgStationForm->tagTypesAbr[0]);

         if (!Reg->KeyExists("Type02"))
            Reg->CreateKey("Type02");
         Reg->WriteString("Type02", ProgStationForm->tagTypes[1]);

         if (!Reg->KeyExists("Type02Abr"))
            Reg->CreateKey("Type02Abr");
         Reg->WriteString("Type02Abr", ProgStationForm->tagTypesAbr[1]);

         if (!Reg->KeyExists("Type03"))
            Reg->CreateKey("Type03");
         Reg->WriteString("Type03", ProgStationForm->tagTypes[2]);

         if (!Reg->KeyExists("Type03Abr"))
            Reg->CreateKey("Type03Abr");
         Reg->WriteString("Type03Abr", ProgStationForm->tagTypesAbr[2]);

         if (!Reg->KeyExists("Type04"))
            Reg->CreateKey("Type04");
         Reg->WriteString("Type04", ProgStationForm->tagTypes[3]);

         if (!Reg->KeyExists("Type04Abr"))
            Reg->CreateKey("Type04Abr");
         Reg->WriteString("Type04Abr", ProgStationForm->tagTypesAbr[3]);

         if (!Reg->KeyExists("Type05"))
            Reg->CreateKey("Type05");
         Reg->WriteString("Type05", ProgStationForm->tagTypes[4]);

         if (!Reg->KeyExists("Type05Abr"))
            Reg->CreateKey("Type05Abr");
         Reg->WriteString("Type05Abr", ProgStationForm->tagTypesAbr[4]);

         if (!Reg->KeyExists("Type06"))
            Reg->CreateKey("Type06");
         Reg->WriteString("Type06", ProgStationForm->tagTypes[5]);

         if (!Reg->KeyExists("Type06Abr"))
            Reg->CreateKey("Type06Abr");
         Reg->WriteString("Type06Abr", ProgStationForm->tagTypesAbr[5]);

         delete Reg;
      }

   }

   if (NewHostIDEdit->Text.data() != NULL)
   {
      int id = atoi(NewHostIDEdit->Text.c_str());
      if ((id <= 0) && (id > 255))
      {
         Application->MessageBox("Error: Wrong Host ID", "Error", MB_OK);
         return;
      }

      char c[4]= {'\0', '\0', '\0'};
      strcpy(c, NewHostIDEdit->Text.c_str());
      for (int i=0; i<NewHostIDEdit->Text.Length(); i++)
      {
         if (!isdigit(c[i]))
         {
            Application->MessageBox("Error: Wrong Host ID", "Error", MB_OK);
            return;
         }
      }

      ProgStationForm->sysHostID = ProgStationForm->lastHostID = id;
      ProgStationForm->hostIDStr = "Host ID: ";
      ProgStationForm->hostIDStr += id;
      ProgStationForm->MainStatusBar->Panels->Items[5]->Text =  ProgStationForm->hostIDStr;

      ProgStationForm->UpdateNewHostIDFields(id);


      //Registry-----------------------------
      TRegistry* Reg = new TRegistry;
      Reg->RootKey = HKEY_CURRENT_USER;
      if (Reg->KeyExists("Software"))
      {
         Reg->OpenKey("Software", false);
         if (!Reg->KeyExists("Active Wave"))
            Reg->CreateKey("Active Wave");

         Reg->OpenKey("Active Wave", false);

         if (!Reg->KeyExists("Programming Station"))
            Reg->CreateKey("Programming Station");

         Reg->OpenKey("Programming Station", false);
         Reg->WriteInteger("Host", ProgStationForm->sysHostID);
         delete Reg;
      }
   }

   TRegistry* Reg = new TRegistry;
   Reg->RootKey = HKEY_CURRENT_USER;
   if (Reg->KeyExists("Software"))
   {
      Reg->OpenKey("Software", false);
      if (!Reg->KeyExists("Active Wave"))
         Reg->CreateKey("Active Wave");

      Reg->OpenKey("Active Wave", false);

      if (!Reg->KeyExists("Programming Station"))
         Reg->CreateKey("Programming Station");

      Reg->OpenKey("Programming Station", false);
      if (AllHostCheckBox->Checked)
      {
         Reg->WriteBool("AllHostID", true);
         ProgStationForm->allHostID = true;
      }
      else
      {
         Reg->WriteBool("AllHostID", false);
         ProgStationForm->allHostID = false;
      }
      if (DisplayLowBatteryCheckBox->Checked)
      {
         Reg->WriteBool("DisplayLowBattery", true);
         ProgStationForm->displayLowBattery = true;
      }
      else
      {
         Reg->WriteBool("DisplayLowBattery", false);
         ProgStationForm->displayLowBattery = false;
      }
   }
   delete Reg;

   if (TempCalibEdit->Text.data() != NULL)
   {
      float f;
      if (TempCalibFRadioButton->Checked)  //F
      {
         AnsiString s = TempCalibEdit->Text;
         f = atof(s.c_str());
         ProgStationForm->tagTempCalibF = f;  //F
         //f -= 32.0;
         ProgStationForm->tagTempCalibC = (f*5.0)/9.0;  //C
      }
      else  //C
      {
         f =  atof(TempCalibEdit->Text.c_str());
         ProgStationForm->tagTempCalibC = f;   //C
         ProgStationForm->tagTempCalibF = ((ProgStationForm->tagTempCalibC/5.0)*9.0); // + 32.0; //F
      }

      //ProgStationForm->tagTempCalib = f;

      //ProgStationForm->SaveTagTempCalibToTag();
      //ProgStationForm->UpdateTagTempScreen();
   }
   else
   {
      Application->MessageBox("No Value for Tag Temperature Calibration.",
                               "Programming Station Information Dialog",
                               MB_OK | MB_ICONSTOP | MB_TOPMOST);
          return;
   }

   AnsiString s;
   if (ProgStationForm->TagTempLimitCdegRadioButton->Checked)
   {
      if (ProgStationForm->tagTempCalibC == 0.0)
          s = "(0.00)";
       else if (ProgStationForm->tagTempCalibC > 0)
          s = s.FormatFloat("(+#0.00 C)", ProgStationForm->tagTempCalibC);
       else
          s = s.FormatFloat("(-#0.00 C)", -ProgStationForm->tagTempCalibC);
    }
    else
    {
       //s = TempCalibEdit->Text;
       //float f = atof(s.c_str());
       //f = ((f/5.0)*9.0)+32.0;

       //if (f == 0.0)
          //s = "(0.00)";
       //else if (f > 0)
          //s = s.FormatFloat("(+#0.00 F) ", f);
       //else
          //s = s.FormatFloat("(-#0.00 F) ", -f);

       if (ProgStationForm->tagTempCalibF == 0.0)
          s = "(0.00)";
       else if (ProgStationForm->tagTempCalibF > 0)
          s = s.FormatFloat("(+#0.00 F)", ProgStationForm->tagTempCalibF);
       else
          s = s.FormatFloat("(-#0.00 F)", -ProgStationForm->tagTempCalibF);
    }
    ProgStationForm->TagTempCalibValueLabel->Caption = s;

    if (curTagTempCalibC != ProgStationForm->tagTempCalibC)
      Application->MessageBox("Need to do Tag Config Temperature in order for the Tag Temp Calibration to take effect",
                              "Programming Station Information Dialog",
                              MB_OK | MB_TOPMOST);

   PlaySound("Ding.wav", NULL, SND_ASYNC );

   Close();
}
//---------------------------------------------------------------------------
void __fastcall TConfigProgStationForm::TempCalibCRadioButtonClick(TObject *Sender)
{
   AnsiString s = TempCalibEdit->Text;
   float f = atof(s.c_str());
   //f -= 32.0;
   f = (f*5.0)/9.0;
   s = s.FormatFloat("##0.###", f);
   TempCalibEdit->Text = s;
}
//---------------------------------------------------------------------------
void __fastcall TConfigProgStationForm::TempCalibFRadioButtonClick(
      TObject *Sender)
{
   AnsiString s = TempCalibEdit->Text;
   float f = atof(s.c_str());
   f = ((f/5.0)*9.0); // + 32.0;
   s = s.FormatFloat("##0.###", f);
   TempCalibEdit->Text = s;
}
//---------------------------------------------------------------------------

void __fastcall TConfigProgStationForm::Type01CheckBoxClick(
      TObject *Sender)
{
   if (Type01CheckBox->Checked)
   {
      Type01Edit->ReadOnly = false;
      Type01Edit->Color = clWhite;

      Type01AbrEdit->ReadOnly = false;
      Type01AbrEdit->Color = clWhite;
   }
   else
   {
      Type01Edit->ReadOnly = true;
      Type01Edit->Color = clInactiveBorder;

      Type01AbrEdit->ReadOnly = true;
      Type01AbrEdit->Color = clInactiveBorder;
   }
}
//---------------------------------------------------------------------------

void __fastcall TConfigProgStationForm::Type02CheckBoxClick(
      TObject *Sender)
{
   if (Type02CheckBox->Checked)
   {
      Type02Edit->ReadOnly = false;
      Type02Edit->Color = clWhite;

      Type02AbrEdit->ReadOnly = false;
      Type02AbrEdit->Color = clWhite;
   }
   else
   {
      Type02Edit->ReadOnly = true;
      Type02Edit->Color = clInactiveBorder;

      Type02AbrEdit->ReadOnly = true;
      Type02AbrEdit->Color = clInactiveBorder;
   }
}
//---------------------------------------------------------------------------

void __fastcall TConfigProgStationForm::Type03CheckBoxClick(
      TObject *Sender)
{
   if (Type03CheckBox->Checked)
   {
      Type03Edit->ReadOnly = false;
      Type03Edit->Color = clWhite;

      Type03AbrEdit->ReadOnly = false;
      Type03AbrEdit->Color = clWhite;
   }
   else
   {
      Type03Edit->ReadOnly = true;
      Type03Edit->Color = clInactiveBorder;

      Type03AbrEdit->ReadOnly = true;
      Type03AbrEdit->Color = clInactiveBorder;
   }
}
//---------------------------------------------------------------------------

void __fastcall TConfigProgStationForm::Type04CheckBoxClick(
      TObject *Sender)
{
   if (Type04CheckBox->Checked)
   {
      Type04Edit->ReadOnly = false;
      Type04Edit->Color = clWhite;

      Type04AbrEdit->ReadOnly = false;
      Type04AbrEdit->Color = clWhite;
   }
   else
   {
      Type04Edit->ReadOnly = true;
      Type04Edit->Color = clInactiveBorder;

      Type04AbrEdit->ReadOnly = true;
      Type04AbrEdit->Color = clInactiveBorder;
   }
}
//---------------------------------------------------------------------------

void __fastcall TConfigProgStationForm::Type05CheckBoxClick(
      TObject *Sender)
{
   if (Type05CheckBox->Checked)
   {
      Type05Edit->ReadOnly = false;
      Type05Edit->Color = clWhite;

      Type05AbrEdit->ReadOnly = false;
      Type05AbrEdit->Color = clWhite;
   }
   else
   {
      Type05Edit->ReadOnly = true;
      Type05Edit->Color = clInactiveBorder;

      Type05AbrEdit->ReadOnly = true;
      Type05AbrEdit->Color = clInactiveBorder;
   }
}
//---------------------------------------------------------------------------

void __fastcall TConfigProgStationForm::Type06CheckBoxClick(
      TObject *Sender)
{
   if (Type06CheckBox->Checked)
   {
      Type06Edit->ReadOnly = false;
      Type06Edit->Color = clWhite;

      Type06AbrEdit->ReadOnly = false;
      Type06AbrEdit->Color = clWhite;
   }
   else
   {
      Type06Edit->ReadOnly = true;
      Type06Edit->Color = clInactiveBorder;

      Type06AbrEdit->ReadOnly = true;
      Type06AbrEdit->Color = clInactiveBorder;
   }
}
//---------------------------------------------------------------------------

void __fastcall TConfigProgStationForm::ResetClick(TObject *Sender)
{
    Type01CheckBox->Checked = true;
    Type02CheckBox->Checked = true;
    Type03CheckBox->Checked = true;
    Type04CheckBox->Checked = false;
    Type05CheckBox->Checked = false;
    Type06CheckBox->Checked = false;

    Type01AbrEdit->Text = "ACC";
    Type02AbrEdit->Text = "INV";
    Type03AbrEdit->Text = "AST";
    Type04AbrEdit->Text = "";
    Type05AbrEdit->Text = "";
    Type06AbrEdit->Text = "";

    Type01Edit->Text = "Access";
    Type02Edit->Text = "Inventory";
    Type03Edit->Text = "Asset";
    Type04Edit->Text = "";
    Type05Edit->Text = "";
    Type06Edit->Text = "";
}
//---------------------------------------------------------------------------
bool __fastcall TConfigProgStationForm::CheckDuplicatTagTypeAbr()
{
    //need to check for dup entry
    for (int i=0; i<6; i++)
    {
       for (int j=i+1; j<6; j++)
       {
          if ((localTagTypesAbr[i] == localTagTypesAbr[j]) && (localTagTypesAbr[i].data() != NULL))
             return (true);
       }
    }

    return false;
}
//---------------------------------------------------------------------------
 bool __fastcall TConfigProgStationForm::CheckDuplicatTagType()
{
    for (int i=0; i<6; i++)
    {
       for (int j=i+1; j<6; j++)
       {
          if ((localTagTypes[i] == localTagTypes[j]) && (localTagTypes[i].data() != NULL))
          {
             return (true);
          }
       }
    }

    return false;
}
