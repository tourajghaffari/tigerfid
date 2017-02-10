//---------------------------------------------------------------------------

#ifndef ChinaDemoUnitH
#define ChinaDemoUnitH
//---------------------------------------------------------------------------
#include <Classes.hpp>
#include <Controls.hpp>
#include <StdCtrls.hpp>
#include <Forms.hpp>
#include <Buttons.hpp>
#include <ExtCtrls.hpp>
//---------------------------------------------------------------------------
struct chinaTagStruct
{
   unsigned int tagID;
   bool responded;
   unsigned short nTries;
};

//
class TChinaDemoForm : public TForm
{
__published:	// IDE-managed Components
        TEdit *Tag1Edit;
        TEdit *Tag2Edit;
        TEdit *Tag3Edit;
        TEdit *Tag4Edit;
        TEdit *Tag5Edit;
        TLabel *Label1;
        TLabel *Label2;
        TLabel *Label3;
        TLabel *Label4;
        TLabel *Label5;
        TBitBtn *StartDemoBitBtn;
        TBitBtn *StopDemoBitBtn;
        TBitBtn *BitBtn3;
        TLabel *Label6;
        TLabel *SysMsgLabel_1;
        TBitBtn *ClearBitBtn;
        TLabel *Label7;
        TEdit *NormalRelayTimeEdit;
        TLabel *Label8;
        TEdit *AbnormalRelayTimeEdit;
        TLabel *DemoStatusLabel;
        TEdit *CloseRelayEdit;
        TLabel *Label9;
        TTimer *NormalRelayCloseTimer;
        TTimer *NormalRelayTimer;
        TTimer *CallTagTimer;
        TTimer *NoRespTimer;
        TTimer *AbnormalRelayTimer;
        TTimer *AbnormalRelayCloseTimer;
        TTimer *StartingTimer;
        TLabel *SysMsgLabel_2;
        TLabel *NormalRelay1Label;
        TLabel *AbnormalRelay2Label;
        TEdit *TagStatus01Edit;
        TEdit *TagStatus02Edit;
        TEdit *TagStatus03Edit;
        TEdit *TagStatus04Edit;
        TEdit *TagStatus05Edit;
        TTimer *StartSearchForMissingTagTimer;
        TBitBtn *TitleBitBtn;
        void __fastcall FormClose(TObject *Sender, TCloseAction &Action);
        void __fastcall StartDemoBitBtnClick(TObject *Sender);
        void __fastcall StopDemoBitBtnClick(TObject *Sender);
        void __fastcall BitBtn3Click(TObject *Sender);
        void __fastcall ClearBitBtnClick(TObject *Sender);
        void __fastcall NormalRelayTimerTimer(TObject *Sender);
        void __fastcall NormalRelayCloseTimerTimer(TObject *Sender);
        void __fastcall CallTagTimerTimer(TObject *Sender);
        void __fastcall NoRespTimerTimer(TObject *Sender);
        void __fastcall AbnormalRelayTimerTimer(TObject *Sender);
        void __fastcall AbnormalRelayCloseTimerTimer(TObject *Sender);
        void __fastcall StartingTimerTimer(TObject *Sender);
        void __fastcall StartSearchForMissingTagTimerTimer(TObject *Sender);
        void __fastcall TitleBitBtnClick(TObject *Sender);
private:	// User declarations
public:		// User declarations
        __fastcall TChinaDemoForm(TComponent* Owner);
        void __fastcall UpdateChinaDemoScreen(int tagID);
        void __fastcall ClearTagStatus();
        void __fastcall UpdateForNoResponse();
        void __fastcall UpdateChinaMsg(int relayNum, int type);
        bool sendOK;
        chinaTagStruct chinaTags[5];
        int __fastcall GetNextTagID();
        unsigned int lastTagID;
        unsigned int tagID;
        short int counter;
        bool tagCallDone;
        bool allOk;
        void __fastcall SendCallTag();
        short int MaxChinaTags;
        DWORD startTime;
        DWORD startSerachTime;
        bool startDemo;
        bool normal;
        bool allowToStart;
        void __fastcall CloseDemo();
        void __fastcall EnableRelay01Label(bool b);
        void __fastcall EnableRelay02Label(bool b);
        void __fastcall UpdateTagStatus01Edit(bool b);
        void __fastcall DisableTagStatus01Edit();
        void __fastcall UpdateTagStatus02Edit(bool b);
        void __fastcall DisableTagStatus02Edit();
        void __fastcall UpdateTagStatus03Edit(bool b);
        void __fastcall DisableTagStatus03Edit();
        void __fastcall UpdateTagStatus04Edit(bool b);
        void __fastcall DisableTagStatus04Edit();
        void __fastcall UpdateTagStatus05Edit(bool b);
        void __fastcall DisableTagStatus05Edit();
        void __fastcall SendGlobal();
        bool __fastcall CheckForAllDone();
};
//---------------------------------------------------------------------------
extern PACKAGE TChinaDemoForm *ChinaDemoForm;
//---------------------------------------------------------------------------
#endif
