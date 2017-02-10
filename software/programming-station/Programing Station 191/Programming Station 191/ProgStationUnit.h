//---------------------------------------------------------------------------

#ifndef ProgStationUnitH
#define ProgStationUnitH
//---------------------------------------------------------------------------
#include <Classes.hpp>
#include <Controls.hpp>
#include <StdCtrls.hpp>
#include <Forms.hpp>
#include <ExtCtrls.hpp>
#include <ComCtrls.hpp>
#include <ToolWin.hpp>
#include <Buttons.hpp>
#include <ImgList.hpp>
#include <Dialogs.hpp>
#include <stdio.h>
#include <winsock2.h>
#include <ws2tcpip.h>
#include <Grids.hpp>

#include "ChinaDemoUnit.h"  //*********china Demo
#include "WriteThreadUnit.h"
#include "ReadLargeDataUnit.h"
#include <ScktComp.hpp>
#include <Graphics.hpp>

//------------------------------------------------------------------------------

//typedef SOCKET LTX_SOCKET;


/*typedef struct cbxentry
{
    struct sockaddr_in peer;	      // CoBox we found
    unsigned char record[126];	      // Setup record 4
    struct cbxentry *next;	      // Link to next entry
} cbxentry;

typedef INTERFACE_INFO LTX_INTERFACE_INFO;

#define COBOX_UDP_PORT 0x77fe	      // CoBox UDP config port 30718
#define CBX_CONF_QUERY	0xe4	      // LTX code get cobox setup record 4
#define UDP_BUF_SIZE	512
#define MAX_INTERFACES	10


#define SETUP_RECORDS 8		      // We only checking one at this point
#define SETUP_LENGTH (128+4)	      // Long enough to hold the entire record, and the header

#define OEM_ID_SIZE  4		      // 4 bytes
#define OEM_ID_OFFSET 0		      // Offset in the record
*/
//------------------------------------------------------------------------------

#define DELAY_TAG_SEND_TIME   2000  //msecond

struct LastProgTagStruct
{
   int tagID;
   AnsiString tagIDInfo;
   AnsiString tagType;
   AnsiString direction;
   AnsiString tamperSwitch;
};

struct tagDetectedArrayStruct
{
  bool gID;
  bool tamperEnabled;
  bool field;
  bool tagEnabled;
  unsigned int cmd;
  unsigned int tagID;
  unsigned short tagType;
  unsigned short tagTIF;
  unsigned short tagGC;
  unsigned short tagResendTime;
  unsigned short tagVersion;
  unsigned short readerID;
  unsigned short fGenID;
  unsigned short rssi;
  TDateTime lastDetectTime;
  DWORD timeValue;
};

struct tagArrayStruct
{
  unsigned int tagID;
  unsigned short tagType;
};

struct networkInfoStruct
{
  SOCKET activeSock;
  sockaddr_in* peerSock;
  char ipAddr[20];
  unsigned int reader;
  unsigned short int host;
  bool validRec;
  char status[10];
  bool active;
  bool selected;
  char initVectorEncrypt[16];
  char initVectorDecrypt[16];
  char pstate;
  char pstatd;
};

struct socketInfoStruct
{
  char ipAddr[20];
  unsigned int reader;
  unsigned short int host;
  bool validRec;      //true=valid record, false=not valid record
  bool action;        //true=connect/false=disconnect
  bool readerStatus;  //true=Online, false=offline
  bool socketStatus;  //true=active, false=inactive
  bool selected;      //true=selected, false=not selected
  char initVectorEncrypt[16];
  char initVectorDecrypt[16];
  char pstate;
  char pstatd;
};

struct ListViewInfoStruct
{
   bool selected;
   AnsiString ip;
   int reader;
   int host;
   char netStatus[10];
   char rdrStatus[10];
   bool full;
   //char time[30];
};

struct sockConnectQueStruct
{
   AnsiString ip;
   int numRetry;
   bool connect;
};

struct sockPollQueStruct
{
   AnsiString ip;
   int reader;
   int host;
   bool txFlag;
   int txLen;
   unsigned char XBuf[255];
};

#define MAX_TAG_DETECTED   300
#define MAX_TAG            200
#define MAX_DESCRIPTOR     255
#define MAX_ENTRY_TEMP_LISTVIEW    200
#define MAX_ENTRY_DETECT_LISTVIEW  200

//---------------------------------------------------------------------------
class TProgStationForm : public TForm
{
__published:	// IDE-managed Components
        TStatusBar *MainStatusBar;
        TTimer *Timer2;
        TImageList *ImageList1;
    TTimer *Timer3;
        TSaveDialog *SaveDialog;
        TToolBar *ToolBar1;
        TToolButton *CommToolButton;
        TToolButton *DebugDisplayToolButton;
        TToolButton *TextDisplayToolButton;
        TToolButton *RecordToolButton;
        TToolButton *StartRecToolButton;
        TToolButton *StopRecToolButton;
        TToolButton *HelpToolButton;
        TToolButton *CloseToolButton;
        TToolButton *ConfigToolButton;
        TTimer *CMDEnableTimer;
        TTimer *BootloadTimer;
        TPanel *Panel1;
        TGroupBox *GroupBox1;
        TListBox *TxListBox;
        TPanel *TXNormalDisplayGroup;
        TLabel *TxTypeLabel;
        TLabel *TxIDLabel;
        TLabel *Label5;
        TLabel *Label6;
        TLabel *TxTagTypeLabel;
        TLabel *TxTagIDLabel;
        TLabel *TxCommandLabel;
        TLabel *TxTagTimeLabel;
        TLabel *Label7;
        TLabel *TxTagDirection;
        TLabel *NewTagIDLabel;
        TLabel *Label2;
        TLabel *TxReaderIDLabel;
        TLabel *Label1;
        TLabel *TxTIFLabel;
        TLabel *Label10;
        TLabel *TxGCLabel;
        TLabel *Label18;
        TLabel *TxTamperLabel;
        TLabel *TxNewTagIDLabel;
        TLabel *FieldGenIDLabel01;
        TLabel *FieldGenIDLabel;
        TBitBtn *TxClearBitBtn;
        TBitBtn *StopGoTXBitBtn;
        TBitBtn *TxClear;
        TGroupBox *GeneralGroupBox;
        TGroupBox *GroupBox4;
        TRadioButton *AccessCtrlRadioButton;
        TRadioButton *InvetRadioButton;
        TRadioButton *AssetCtrlRadioButton;
        TRadioButton *ReservedRadioButton2;
        TRadioButton *CarRadioButton;
        TRadioButton *AnyTagRadioButton;
        TRadioButton *ReservedRadioButton;
        TRadioButton *ReservedRadioButton1;
        TGroupBox *GroupBox5;
        TEdit *NewIDEdit;
        TEdit *TagIDEdit;
        TCheckBox *TagIDRadioButton;
        TCheckBox *NewIDRadioButton;
        TGroupBox *DisplayFormatBox;
        TRadioButton *HexRadioButton;
        TRadioButton *DecRadioButton;
        TGroupBox *GroupBox10;
        TLabel *Label20;
        TLabel *Label21;
        TLabel *Label22;
        TLabel *Label23;
        TLabel *Label34;
        TLabel *Label35;
        TLabel *Label36;
        TLabel *Label38;
        TEdit *HostIDEdit;
        TEdit *FieldGenIDEdit;
        TEdit *RepeaterIDEdit;
        TCheckBox *NewReaderIDCheckBox;
        TEdit *NewReaderIDEdit;
        TCheckBox *NewHostIDCheckBox;
        TEdit *NewHostIDEdit;
        TCheckBox *NewRepeaterIDCheckBox;
        TEdit *NewRepeaterIDEdit;
        TComboBox *ReaderIDComboBox;
        TComboBox *ReaderTypeComboBox;
        TCheckBox *RedaerNoBroadcastCheckBox;
        TCheckBox *ReaderEnablePowerupCheckBox;
        TCheckBox *ReaderNoRSSICheckBox;
        TCheckBox *ReaderDisableCheckBox;
        TGroupBox *GroupBox3;
        TListBox *RecListBox;
        TBitBtn *RecClear;
        TBitBtn *StopGoRXBitBtn;
        TPanel *RXNormalDisplayGroup;
        TLabel *Label11;
        TLabel *Label12;
        TLabel *Label13;
        TLabel *Label14;
        TLabel *RxTagTypeLabel;
        TLabel *RxTagIDLabel;
        TLabel *RxTagDirection;
        TLabel *RxTagTimeLabel;
        TLabel *Label8;
        TLabel *RxCommandLabel;
        TLabel *Label3;
        TLabel *GroupID;
        TLabel *Label4;
        TLabel *RxReaderIDLabel;
        TLabel *Label9;
        TLabel *RxTIFLabel;
        TLabel *Label16;
        TLabel *RxGCLabel;
        TLabel *Label17;
        TLabel *RxTamperLabel;
        TLabel *Label19;
        TLabel *ContinuousLabel;
        TLabel *Label15;
        TLabel *RxTagStatusLabel;
        TLabel *Label24;
        TLabel *RxTagVersionLabel;
        TLabel *Label33;
        TLabel *FGenIDLabel;
        TBitBtn *RxClearBitBtn;
        TGroupBox *GroupBox9;
        TGroupBox *TagDetectedGroupBox;
        TLabel *FGLabel;
        TLabel *GroupLabel;
        TLabel *MTLabel;
        TListView *DetectedTagListView;
        TBitBtn *ClearTagListBitBtn;
        TCheckBox *NewListItemCheckBox;
        TGroupBox *GroupBox17;
        TLabel *ReportType1Label;
        TLabel *ReportType03Label;
        TLabel *ReportType3Label;
        TLabel *ReportType04Label;
        TLabel *ReportType4Label;
        TLabel *ReportType01Label;
        TLabel *ReportType02Label;
        TLabel *ReportType2Label;
        TLabel *TotalLabel;
        TLabel *ReportTotalLabel;
        TLabel *Label37;
        TLabel *ReportNDupLabel;
        TGroupBox *GroupBox19;
        TLabel *DetectedMsg;
        TLabel *TamperSWMsg;
        TBitBtn *ClearMsg;
        TGroupBox *GroupBox6;
        TStaticText *StaticText;
        TBitBtn *ClearMsgBitBtn;
        TGroupBox *ReaderCodeVerGroupBox;
        TLabel *Label39;
        TLabel *Label40;
        TComboBox *RdrCodeVerReaderComboBox;
        TEdit *RdrCodeVerHostEdit;
        TGroupBox *ConfigReaderTxTimeGroupBox;
        TRadioButton *ConfigTxTimeReaderRadioButton;
        TLabel *Label44;
        TLabel *Label52;
        TLabel *Label45;
        TLabel *Label41;
        TGroupBox *GroupBox20;
        TLabel *Label47;
        TComboBox *TxTimeComboBox;
        TRadioButton *TxTimeSecRadioButton;
        TRadioButton *TxTimeAllRadioButton;
        TGroupBox *GroupBox21;
        TLabel *Label46;
        TComboBox *WaitTimeComboBox;
        TRadioButton *WaitTimeSecRadioButton;
        TRadioButton *WaitTimeMinRadioButton;
        TRadioButton *WaitTimeHourRadioButton;
        TRadioButton *WaitTimeAllRadioButton;
        TEdit *ConfigTxTimeFieldGenIDEdit;
        TEdit *ConfigTxTimeRepeaterIDEdit;
        TEdit *ConfigTxTimeHostIDEdit;
        TComboBox *ConfigTxTimeReaderIDComboBox;
        TRadioButton *ConfigTxTimeFieldGenRadioButton;
        TLabel *Label49;
        TLabel *Label50;
        TLabel *Label51;
        TLabel *Label53;
        TGroupBox *AssignTagReaderGroupBox;
        TLabel *Label63;
        TLabel *Label64;
        TComboBox *AssignTagRdrRdrIDComboBox;
        TEdit *AssignTagRdrHostIDEdit;
        TLabel *Label67;
        TComboBox *AssignTagRdrTagRdrIDComboBox;
        TListView *AssignTagRdrListView;
        TBitBtn *AssignTagRdrClearBitBtn;
        TCheckBox *AssignTagRdrBroadcastRdrCheckBox;
        TGroupBox *ConfigTagRandGroupBox;
        TLabel *Label68;
        TLabel *Label69;
        TComboBox *ConfigTagRNDRdrIDComboBox;
        TEdit *ConfigTagRNDHostIDEdit;
        TListView *ConfigTagRNDListView;
        TBitBtn *ConfigTagRNDClearBitBtn;
        TCheckBox *ConfigTagRNDBroadcastCheckBox;
        TGroupBox *ResetReaderGroupBox;
        TLabel *Label77;
        TLabel *Label79;
        TLabel *Label80;
        TEdit *ResetRepeaterIDEdit;
        TComboBox *ResetReaderIDComboBox;
        TEdit *ResetHostIDEdit;
        TListView *ResetListView;
        TBitBtn *ResetClearBitBtn;
        TCheckBox *ResetBroadcastReaderCheckBox;
        TCheckBox *ResetBroadcastRepeaterCheckBox;
        TCheckBox *ResetModifyReaderCheckBox;
        TLabel *ResetNewReaderIDLabel;
        TLabel *ResetNewHostIDLabel;
        TLabel *ResetNewRepeaterIDLabel;
        TEdit *ResetNewReaderIDEdit;
        TEdit *ResetNewHostIDEdit;
        TEdit *ResetNewRepeaterIDEdit;
        TGroupBox *GroupBox24;
        TComboBox *ResetReaderTypeComboBox;
        TLabel *Label81;
        TCheckBox *ResetRespCheckBox;
        TCheckBox *ResetEnablePWCheckBox;
        TCheckBox *ResetRSSICheckBox;
        TGroupBox *EnableReaderGroupBox;
        TLabel *Label82;
        TLabel *Label83;
        TLabel *Label84;
        TEdit *EnableReaderRepIDEdit;
        TComboBox *EnableReaderIDComboBox;
        TEdit *EnableReaderHostIDEdit;
        TListView *EnableReaderListView;
        TBitBtn *EnableReaderClearBitBtn;
        TCheckBox *EnableReaderBroadcastRdrCheckBox;
        TCheckBox *EnableReaderBroadcastReptCheckBox;
        TGroupBox *DisableReaderGroupBox;
        TLabel *Label85;
        TLabel *Label86;
        TLabel *Label87;
        TEdit *DisableReaderRepIDEdit;
        TComboBox *DisableReaderIDComboBox;
        TEdit *DisableReaderHostIDEdit;
        TListView *DisableReaderListView;
        TBitBtn *DisableReaderClearBitBtn;
        TCheckBox *DisableReaderBroadcastRdrCheckBox;
        TCheckBox *DisableReaderBroadcastReptCheckBox;
        TGroupBox *QueryReaderGroupBox;
        TLabel *Label25;
        TLabel *Label26;
        TLabel *Label29;
        TEdit *QueryReaderRepIDEdit;
        TComboBox *QueryReaderIDComboBox;
        TEdit *QueryReaderHostIDEdit;
        TListView *QueryReaderListView;
        TBitBtn *QueryReaderClearBitBtn;
        TCheckBox *QueryReaderBroadcastRdrCheckBox;
        TCheckBox *QueryReaderBroadcastReptCheckBox;
        TGroupBox *AssignReaderGroupBox;
        TEdit *AssignReaderReptIDEdit;
        TListView *AssignReaderListView;
        TBitBtn *AssignReaderClearBitBtn;
        TEdit *AssignReaderNewReptIDEdit;
        TGroupBox *GroupBox18;
        TCheckBox *AssignReaderBroadcastCheckBox;
        TCheckBox *AssignReaderEnableCheckBox;
        TCheckBox *AssignReaderRSSICheckBox;
        TCheckBox *AssignReaderNoChangeCheckBox;
        TCheckBox *AssignReaderBroadcastRdrCheckBox;
        TCheckBox *AssignReaderBroadcastReptCheckBox;
        TGroupBox *RelayGroupBox;
        TLabel *Label94;
        TLabel *Label95;
        TLabel *Label96;
        TEdit *Edit2;
        TComboBox *RelayReaderIDComboBox;
        TEdit *RelayHostIDEdit;
        TGroupBox *EnableTagGroupBox;
        TLabel *Label100;
        TLabel *Label101;
        TLabel *Label102;
        TEdit *EnableTagReptIDEdit;
        TComboBox *EnableTagIDComboBox;
        TEdit *EnableTagHostIDEdit;
        TGroupBox *GroupBox35;
        TGroupBox *GroupBox36;
        TRadioButton *EnableAnyTagIDRadioButton;
        TRadioButton *EnableTagIDRadioButton;
        TEdit *EnableTagIDEdit;
        TListView *EnableTagListView;
        TBitBtn *EnableTagClearBitBtn;
        TLabel *Label103;
        TCheckBox *EnableTagBroadcastRdrCheckBox;
        TGroupBox *GroupBox7;
        TGroupBox *GroupBox8;
        TPanel *Panel4;
        TRadioButton *RNShortRadioButton;
        TRadioButton *RNLongRadioButton;
        TRadioButton *RNChangeRadioButton;
        TRadioButton *RNNoChangeRadioButton;
        TGroupBox *GroupBox11;
        TLabel *EnableTagType01Label;
        TLabel *EnableTagType03Name;
        TLabel *EnableTagType03Label;
        TLabel *EnableTagType01Name;
        TLabel *EnableTagType02Name;
        TLabel *EnableTagType02Label;
        TLabel *Label114;
        TLabel *EnableTagTotalLabel;
        TCheckBox *EnableTagKeepListCheckBox;
        TGroupBox *DisableTagGroupBox;
        TLabel *Label106;
        TLabel *Label108;
        TLabel *Label109;
        TLabel *Label110;
        TEdit *DisableTagReptIDEdit;
        TComboBox *DisableTagIDComboBox;
        TEdit *DisableTagHostIDEdit;
        TGroupBox *GroupBox13;
        TRadioButton *DisableAnyTagIDRadioButton;
        TRadioButton *DisableTagIDRadioButton;
        TEdit *DisableTagIDEdit;
        TGroupBox *GroupBox14;
        TListView *DisableTagListView;
        TBitBtn *DisableTagClearBitBtn;
        TCheckBox *DisableTagBroadcastRdrCheckBox;
        TGroupBox *GroupBox15;
        TGroupBox *GroupBox34;
        TLabel *DisableTagType01Label;
        TLabel *DisableTagType03Name;
        TLabel *DisableTagType03Label;
        TLabel *DisableTagType01Name;
        TLabel *DisableTagType02Name;
        TLabel *DisableTagType02Label;
        TLabel *Label120;
        TLabel *DisableTagTotalLabel;
        TCheckBox *DisableTagKeepListCheckBox;
        TGroupBox *QueryTagGroupBox;
        TLabel *Label113;
        TLabel *Label116;
        TLabel *Label119;
        TEdit *QueryTagReptIDEdit;
        TComboBox *QueryTagReaderIDComboBox;
        TEdit *QueryTagHostIDEdit;
        TGroupBox *GroupBox37;
        TRadioButton *QueryAnyTagIDRadioButton;
        TRadioButton *QueryTagIDRadioButton;
        TEdit *QueryTagIDEdit;
        TGroupBox *GroupBox38;
        TCheckBox *QueryTagBroadcastRdrCheckBox;
        TGroupBox *GroupBox39;
        TLabel *Label121;
        TLabel *Label122;
        TLabel *Label123;
        TGroupBox *CallTagGroupBox;
        TLabel *Label124;
        TLabel *Label125;
        TLabel *Label126;
        TEdit *CallTagReptIDEdit;
        TComboBox *CallTagReaderIDComboBox;
        TEdit *CallTagHostIDEdit;
        TGroupBox *GroupBox40;
        TRadioButton *CallTagAnyTagIDRadioButton;
        TRadioButton *CallTagIDRadioButton;
        TEdit *CallTagIDEdit;
        TGroupBox *GroupBox41;
        TCheckBox *CallTagBroadcastRdrCheckBox;
        TGroupBox *GroupBox42;
        TToolButton *ReaderFgenToolButton;
        TBitBtn *ConfigTxTimeBitBtn;
        TStaticText *ConfigTxTimeStaticText;
        TRadioButton *CallTagRNShortRadioButton;
        TRadioButton *CallTagRNLongRadioButton;
        TRadioButton *QueryTagRNShortRadioButton;
        TRadioButton *QueryTagRNLongRadioButton;
        TRadioButton *DisableTagRNShortRadioButton;
        TRadioButton *DisableTagRNLongRadioButton;
        TRadioButton *EnableTagRNShortRadioButton;
        TRadioButton *EnableTagRNLongRadioButton;
        TGroupBox *WriteMemoryGroupBox;
        TLabel *Label129;
        TLabel *Label130;
        TLabel *Label131;
        TEdit *WriteMemReptIDEdit;
        TComboBox *WriteMemoryReaderIDComboBox;
        TEdit *WriteMemoryHostIDEdit;
        TGroupBox *GroupBox47;
        TRadioButton *WriteMemoryAnyTagIDRadioButton;
        TRadioButton *WriteMemoryTagIDRadioButton;
        TEdit *WriteMemoryTagIDEdit;
        TGroupBox *GroupBox48;
        TCheckBox *WriteMemoryBroadcastRdrCheckBox;
        TGroupBox *GroupBox49;
        TRadioButton *WriteMemoryTagRNShortRadioButton;
        TRadioButton *WriteMemoryTagRNLongRadioButton;
        TLabel *Label132;
        TEdit *WriteMemoryStartAddrEdit;
        TLabel *Label133;
        TEdit *WriteMemoryNumByteEdit;
        TStringGrid *WriteMemoryStringGrid;
        TPanel *Panel3;
        TRadioButton *WriteMemoryHexRadioButton;
        TRadioButton *WriteMemDecRadioButton;
        TRadioButton *WriteMemCharRadioButton;
        TLabel *Label134;
        TLabel *Label135;
        TLabel *Label136;
        TLabel *Label137;
        TLabel *Label138;
        TEdit *ReadMemReptIDEdit;
        TComboBox *ReadMemoryReaderIDComboBox;
        TEdit *ReadMemoryHostIDEdit;
        TGroupBox *GroupBox50;
        TRadioButton *ReadMemoryAnyTagIDRadioButton;
        TRadioButton *ReadMemoryTagIDRadioButton;
        TEdit *ReadMemoryTagIDEdit;
        TGroupBox *GroupBox51;
        TCheckBox *ReadMemoryBroadcastRdrCheckBox;
        TGroupBox *GroupBox52;
        TRadioButton *ReadMemoryTagRNShortRadioButton;
        TRadioButton *ReadMemoryTagRNLongRadioButton;
        TEdit *ReadMemoryStartAddrEdit;
        TEdit *ReadMemoryNumByteEdit;
        TStringGrid *ReadMemoryStringGrid;
        TPanel *Panel6;
        TRadioButton *ReadMemoryHexRadioButton;
        TRadioButton *ReadMemDecRadioButton;
        TRadioButton *ReadMemCharRadioButton;
        TGroupBox *ReadMemoryGroupBox;
        TBitBtn *WriteMemoryClearBitBtn;
        TLabel *Label149;
        TLabel *Label150;
        TLabel *Label151;
        TLabel *Label152;
        TGroupBox *TagTempListGroupBox;
        TListView *TagTempListView;
        TBitBtn *ClearTagTempListBitBtn;
        TRadioButton *TagTempListCdegRadioButton;
        TRadioButton *TagTempListFdegRadioButton;
    TLabel *MaxWDataLabel;
        TLabel *MaxReadLabel;
        TLabel *Label157;
        TLabel *Label158;
        TListView *RelayListView;
        TCheckBox *RelayBroadcastRdrCheckBox;
        TCheckBox *RelayKeepListCheckBox;
        TBitBtn *RelayClearBitBtn;
        TGroupBox *InputGroupBox;
        TLabel *Label159;
        TLabel *Label160;
        TLabel *Label161;
        TEdit *InputRepeaterIDEdit;
        TComboBox *InputReaderIDComboBox;
        TEdit *InputHostIDEdit;
        TListView *InputListView;
        TCheckBox *InputBroadCastCheckBox;
        TCheckBox *InputKeepItemsCheckBox;
        TBitBtn *InputClearBitBtn;
        TGroupBox *GroupBox2;
        TRadioButton *Input1NoReportRadioButton;
        TRadioButton *Input1ReportRadioButton;
        TGroupBox *GroupBox16;
        TRadioButton *Input2NoReportRadioButton;
        TRadioButton *Input2ReportRadioButton;
        TGroupBox *EnableFGenGroupBox;
        TLabel *Label162;
        TLabel *Label163;
        TLabel *Label164;
        TEdit *Edit4;
        TComboBox *EnableFGenReaderIDComboBox;
        TEdit *EnableFGenHostIDEdit;
        TGroupBox *GroupBox60;
        TRadioButton *EnableFGenANYRadioButton;
        TRadioButton *EnableFGenACCRadioButton;
        TRadioButton *EnableFGenASSRadioButton;
        TRadioButton *EnableFGenINVRadioButton;
        TRadioButton *Input1NoChangeRadioButton;
        TRadioButton *Input2NoChangeRadioButton;
        TCheckBox *Input1SupervisedCheckBox;
        TCheckBox *Input2SupervisedCheckBox;
        TGroupBox *TagTempGroupBox;
        TLabel *Label139;
        TLabel *Label140;
        TLabel *Label141;
        TEdit *TagTempReptIDEdit;
        TComboBox *TagTempReaderIDComboBox;
        TEdit *TagTempHostIDEdit;
        TGroupBox *GroupBox53;
        TRadioButton *TagTempAnyTagIDRadioButton;
        TRadioButton *TagTempTagIDRadioButton;
        TEdit *TagTempTagIDEdit;
        TGroupBox *GroupBox54;
        TCheckBox *TagTempBroadcastRdrCheckBox;
        TGroupBox *GroupBox55;
        TRadioButton *TagTempTagRNShortRadioButton;
        TRadioButton *TagTempTagRNLongRadioButton;
        TGroupBox *GroupBox46;
        TLabel *Label142;
        TLabel *Label143;
        TLabel *Label144;
        TLabel *Label145;
        TLabel *Label146;
        TLabel *TagTempCalibValueLabel;
        TRadioButton *TagTempLimitCdegRadioButton;
        TRadioButton *TagTempLimitFdegRadioButton;
        TEdit *TagTempNewUpLimitEdit;
        TEdit *TagTempCurrUpLimitEdit;
        TEdit *TagTempCurrLowLimitEdit;
        TEdit *TagTempNewLowLimitEdit;
        TCheckBox *TagTempChangeUpLimitCheckBox;
        TCheckBox *TagTempChangeLowLimitCheckBox;
        TEdit *TagTempCurrUpCalibLimitEdit;
        TEdit *TagTempCurrLowCalibLimitEdit;
        TGroupBox *GroupBox56;
        TLabel *Label147;
        TLabel *Label148;
        TCheckBox *TagTempRepLowLimitCheckBox;
        TCheckBox *TagTempRepPeriodCheckBox;
        TCheckBox *TagTempRepUpLimitCheckBox;
        TComboBox *TagTempNumReadAveComboBox;
        TEdit *TagTempPeriodRepTimeEdit;
        TRadioButton *TagTempPeriodRepTimeMinRadioButton;
        TRadioButton *TagTempPeriodRepTimeHourRadioButton;
        TCheckBox *TagTempChangeReportCheckBox;
        TGroupBox *GroupBox57;
        TRadioButton *TagTempCdegRadioButton;
        TRadioButton *TagTempFdegRadioButton;
        TEdit *TagTempTempValueEdit;
        TBitBtn *TagTempReadTempValueBitBtn;
        TStaticText *TempStatusStaticText;
        TBitBtn *TagTempRefreshBitBtn;
        TCheckBox *TagTempDisplayListCheckBox;
        TGroupBox *ConfigTagGroupBox;
        TLabel *Label97;
        TLabel *Label98;
        TComboBox *ConfigTagReaderIDComboBox;
        TEdit *ConfigTagHostIDEdit;
        TGroupBox *GroupBox28;
        TGroupBox *GroupBox26;
        TLabel *TagNewIDLabel;
        TEdit *ConfigTagNewIDEdit;
        TGroupBox *GroupBox31;
        TComboBox *ConfigTagTIFComboBox;
        TCheckBox *ConfigTagTIFCheckBox;
        TComboBox *ConfigTagGCComboBox;
        TGroupBox *GroupBox32;
        TGroupBox *GroupBox30;
        TComboBox *ConfigTagDurationComboBox;
        TCheckBox *ConfigTagEnableTimeCheckBox;
        TRadioButton *ConfigTagMSRadioButton;
        TRadioButton *ConfigTagSecRadioButton;
        TRadioButton *ConfigTagMinRadioButton;
        TRadioButton *ConfigTagHourRadioButton;
        TGroupBox *GroupBox25;
        TCheckBox *ConfigTagFactoryIDCheckBox;
        TRadioButton *ConfigTagRNShortRadioButton;
        TRadioButton *ConfigTagRNLongRadioButton;
        TCheckBox *ConfigTagModifyRNCheckBox;
        TRadioButton *ConfigTagRNChangeRadioButton;
        TRadioButton *ConfigTagRNNoChangeRadioButton;
        TRadioButton *ConfigTagNoChangeRadioButton;
        TBitBtn *ConfigTagGetConfigBitBtn;
        TCheckBox *ConfigTagNewTagIDCheckBox;
        TGroupBox *GroupBox27;
        TCheckBox *ConfigTagNewTagTypeCheckBox;
        TGroupBox *GroupBox33;
        TGroupBox *TamperGroupBox;
        TRadioButton *ConfigTagReportStatusRadioButton;
        TRadioButton *ConfigTagReportHistoryRadioButton;
        TCheckBox *ConfigTagModifyTamperCheckBox;
        TRadioButton *ConfigTagNoReportRadioButton;
        TEdit *ConfigTagTagIDEdit;
        TRadioButton *ConfigTagTagIDRadioButton;
        TRadioButton *ConfigTagAnyTagIDRadioButton;
        TCheckBox *ConfigTagGCCheckBox;
        TLabel *Label105;
        TGroupBox *FGenResetGroupBox;
        TLabel *FGenResetReaderLabel;
        TLabel *Label174;
        TLabel *FGenResetIDLabel;
        TComboBox *FGenResetReaderIDComboBox;
        TEdit *FGenResetHostIDEdit;
        TListView *FGenResetListView;
        TBitBtn *FGenResetClearBitBtn;
        TCheckBox *FGenResetBroadcastRdrCheckBox;
        TComboBox *FGenResetIDComboBox;
        TCheckBox *FGenResetBroadcastSmartFGenCheckBox;
        TGroupBox *QueryFGenGroupBox;
        TLabel *Label60;
        TLabel *Label61;
        TLabel *Label62;
        TEdit *QueryFGenIDEdit;
        TEdit *QueryFGenHostIDEdit;
        TEdit *QueryFGenRepeaterIDEdit;
        TListView *QueryFGenListView;
        TBitBtn *QueryFGenClearBitBtn;
        TRadioButton *QueryFGenRdrRadioButton;
        TRadioButton *QueryFGenFgRadioButton;
        TCheckBox *QueryFGenCheckBox;
        TListView *QueryFGenProcListView;
        TCheckBox *QueryFGenSmartFGenBroadcastCheckBox;
        TBitBtn *GetProcRevDateBitBtn;
        TBitBtn *QueryFGenClearRevListBitBtn;
        TCheckBox *QueryFGenKeepRevListCheckBox;
        TRadioButton *QueryFGenSmartFGRadioButton;
        TCheckBox *QueryFGenSmartFGenBroadcastRdrCheckBox;
        TLabel *Label104;
        TLabel *Label173;
        TLabel *Label175;
        TGroupBox *ConfigFGenGroupBox;
        TLabel *Label57;
        TLabel *Label169;
        TGroupBox *GroupBox22;
        TLabel *Label58;
        TLabel *Label54;
        TComboBox *FGenConfigTxTimeComboBox;
        TCheckBox *FGenConfigTxTimeModifyCheckBox;
        TGroupBox *GroupBox23;
        TLabel *Label59;
        TComboBox *FGenConfigWaitTimeComboBox;
        TRadioButton *FGenConfigWaitTimeSecRadioButton;
        TRadioButton *FGenConfigWaitTimeMinRadioButton;
        TRadioButton *FGenConfigWaitTimeHourRadioButton;
        TCheckBox *FGenConfigWaitTimeModifyCheckBox;
        TEdit *FGenConfigFieldGenIDEdit;
        TEdit *FGenConfigReaderIDEdit;
        TCheckBox *FGenConfigDefaultTimeCheckBox;
        TGroupBox *GroupBox12;
        TCheckBox *FGenConfigTagTypeModifyCheckBox;
        TGroupBox *GroupBox43;
        TRadioButton *FGenConfigTagIDRadioButton;
        TEdit *FGenConfigTagIDEdit;
        TCheckBox *FGenConfigTagIDModifyCheckBox;
        TRadioButton *FGenConfigAnyTagIDRadioButton;
        TGroupBox *GroupBox44;
        TLabel *Label127;
        TEdit *FGenConfigAssignedReaderIDEdit;
        TCheckBox *FGenConfigAssignedReaderIDModifyCheckBox;
        TGroupBox *GroupBox45;
        TLabel *Label128;
        TEdit *FGenConfigFGenIDEdit;
        TCheckBox *FGenConfigFGenIDModifyCheckBox;
        TGroupBox *Tag;
        TCheckBox *FGenConfigRaRnModifyCheckBox;
        TBitBtn *ConfigFGenUpdateBitBtn;
        TEdit *FGenConfigHostIDEdit;
        TRadioButton *FGenConfigSmartFgenRadioButton;
        TRadioButton *FGenConfigStandFgenRadioButton;
        TRadioButton *FGenConfigRNShortRadioButton;
        TRadioButton *FGenConfigRALongRadioButton;
        TGroupBox *GroupBox29;
        TCheckBox *FGenConfigMDCheckBox;
        TRadioButton *FGenConfigMDActiveHiRadioButton;
        TRadioButton *FGenConfigMDActiveLoRadioButton;
        TCheckBox *FGenConfigMonitorPIRCheckBox;
        TCheckBox *FGenConfigActivePIRCheckBox;
        TCheckBox *FGenConfigMDEnableCheckBox;
        TGroupBox *SmartFGenGroupBox;
        TLabel *Label166;
        TLabel *Label167;
        TLabel *Label168;
        TCheckBox *SmartFGenBroadcastAllRdrCheckBox;
        TEdit *SmartFGenHostIDEdit;
        TGroupBox *GroupBox59;
        TRadioButton *SmartFGenAnyTagIDRadioButton;
        TRadioButton *SmartFGenTagIDRadioButton;
        TEdit *SmartFGenTagIDEdit;
        TGroupBox *GroupBox61;
        TGroupBox *SmartFGenRNShortRadioButton;
        TRadioButton *SmartFGenShortRNRadioButton;
        TRadioButton *SmartFGenLongRNRadioButton;
        TGroupBox *GroupBox63;
        TLabel *Label165;
        TLabel *Label172;
        TLabel *Label171;
        TLabel *Label56;
        TLabel *SmartFGenDPotValueLabel;
        TUpDown *SmartFGenPotentioUpDown;
        TEdit *SmartFGenNewDPotEdit;
        TComboBox *SmartFGenReaderIDComboBox;
        TComboBox *SmartFGenIDComboBox;
        TCheckBox *SmartFGenBroadcastAllCheckBox;
        TBitBtn *SmartFGenGetDPotBitBtn;
        TTimer *PollTimer;
        TClientSocket *ClientSocket;
        TComboBox *AssignReaderTypeComboBox;
        TLabel *AssinReaderTypeLabel;
        TRadioButton *ReaderCMDRadioButton;
        TRadioButton *FGenCMDRadioButton;
        TRadioButton *SFGenCMDRadioButton;
        TRadioButton *TagCMDRadioButton;
        TPanel *SmartFGenCMDPanel;
        TLabel *Label176;
        TBitBtn *FGenResetBitBtn;
        TStaticText *FGenResetStaticText;
        TBitBtn *SmartFGenBitBtn;
        TStaticText *SmartFGenStaticText;
        TBitBtn *ResetCMDSFGenBitBtn;
        TBitBtn *ConfigSFGenBitBtn;
        TBitBtn *QuerySFGenBitBtn;
        TStaticText *ConfigSFGenStaticText;
        TPanel *ReaderCMDPanel;
        TLabel *Label93;
        TBitBtn *ResetDeviceBitBtn;
        TStaticText *ResetReaderStaticText;
        TBitBtn *EnableReaderBitBtn;
        TStaticText *EnableReaderStaticText;
        TBitBtn *DisableReaderBitBtn;
        TStaticText *DisableReaderStaticText;
        TBitBtn *QueryReaderBitBtn;
        TStaticText *QueryReaderStaticText;
        TBitBtn *AssignReaderBitBtn;
        TStaticText *AssignReaderStaticText;
        TBitBtn *ReaderVersion;
        TStaticText *ReaderVersionStaticText;
        TBitBtn *EnableFGenBitBtn;
        TStaticText *EnableRdrFGenStaticText;
        TBitBtn *RelayBitBtn;
        TStaticText *RelayStaticText;
        TBitBtn *InputsBitBtn;
        TStaticText *InputsStaticText;
        TPanel *TagCMDPanel;
        TLabel *Label177;
        TBitBtn *EnableTagBitBtn;
        TBitBtn *DisableTagBitBtn;
        TBitBtn *QueryTagBitBtn;
        TBitBtn *CallTagBitBtn;
        TBitBtn *AssignTagRdrBitBtn;
        TBitBtn *ConfigTagRNDBitBtn;
        TBitBtn *WriteMemoryBitBtn;
        TBitBtn *ReadMemoryBitBtn;
        TBitBtn *TagTempBitBtn;
        TStaticText *EnableTagStaticText;
        TStaticText *DisableTagStaticText;
        TStaticText *QueryTagStaticText;
        TStaticText *CallTagStaticText;
        TStaticText *AssignTagRdrStaticText;
        TStaticText *ConfigTagRNDStaticText;
        TStaticText *ReadMemoryStaticText;
        TStaticText *WriteMemoryStaticText;
        TStaticText *TagTempStaticText;
        TBitBtn *ConfigTagBitBtn;
        TStaticText *ConfigTagStaticText;
        TPanel *FGenCMDPanel;
        TLabel *Label170;
        TBitBtn *ConfigFGenBitBtn;
        TStaticText *ConfigFGenStaticText;
        TBitBtn *QueryFGenBitBtn;
        TStaticText *QueryFGenStaticText;
        TBitBtn *ResetCMDReaderBitBtn;
        TBitBtn *ResetCMDTagBitBtn;
        TBitBtn *ResetCMDFGenBitBtn;
        TGroupBox *QuerySFGenGroupBox;
        TLabel *Label178;
        TLabel *Label179;
        TLabel *Label180;
        TLabel *Label182;
        TLabel *Label183;
        TLabel *Label184;
        TEdit *QuerySFGenHostIDEdit;
        TEdit *Edit7;
        TListView *QuerySFGenListView;
        TBitBtn *QuerySFGenClearBitBtn;
        TCheckBox *QuerySFGenCheckBox;
        TListView *QuerySFGenProcListView;
        TCheckBox *QuerySFGenSmartFGenBroadcastCheckBox;
        TBitBtn *GetSFGenProcRevDateBitBtn;
        TBitBtn *QuerySFGenClearRevListBitBtn;
        TCheckBox *QuerySFGenKeepRevListCheckBox;
        TCheckBox *QuerySFGenSmartFGenBroadcastRdrCheckBox;
        TComboBox *QueryFGenSmartFGenIDComboBox;
        TLabel *QueryFGenRdrIDLabel;
        TComboBox *QueryFGenSmartFGenRdrIDComboBox;
        TGroupBox *ConfigSFGenGroupBox;
        TLabel *Label181;
        TLabel *Label185;
        TLabel *Label186;
        TGroupBox *GroupBox64;
        TLabel *Label187;
        TLabel *Label188;
        TComboBox *SFGenConfigTxTimeComboBox;
        TCheckBox *SFGenConfigTxTimeModifyCheckBox;
        TGroupBox *GroupBox65;
        TLabel *Label189;
        TComboBox *SFGenConfigWaitTimeComboBox;
        TRadioButton *SFGenConfigWaitTimeSecRadioButton;
        TRadioButton *SFGenConfigWaitTimeMinRadioButton;
        TRadioButton *SFGenConfigWaitTimeHourRadioButton;
        TCheckBox *SFGenConfigWaitTimeModifyCheckBox;
        TCheckBox *CheckBox3;
        TGroupBox *GroupBox66;
        TCheckBox *SFGenConfigTagTypeModifyCheckBox;
        TGroupBox *GroupBox67;
        TRadioButton *SFGenConfigTagIDRadioButton;
        TEdit *SFGenConfigTagIDEdit;
        TCheckBox *SFGenConfigTagIDModifyCheckBox;
        TRadioButton *SFGenConfigAnyTagIDRadioButton;
        TGroupBox *GroupBox68;
        TLabel *Label190;
        TEdit *SFGenConfigAssignedReaderIDEdit;
        TCheckBox *SFGenConfigAssignedReaderIDModifyCheckBox;
        TGroupBox *GroupBox69;
        TLabel *Label191;
        TEdit *SFGenConfigFGenIDEdit;
        TCheckBox *SFGenConfigFGenIDModifyCheckBox;
        TGroupBox *GroupBox70;
        TCheckBox *SFGenConfigRaRnModifyCheckBox;
        TRadioButton *SFGenConfigRNShortRadioButton;
        TRadioButton *SFGenConfigRALongRadioButton;
        TBitBtn *ConfigSFGenUpdateBitBtn;
        TEdit *SFGenConfigHostIDEdit;
        TGroupBox *GroupBox72;
        TCheckBox *SFGenConfigMDCheckBox;
        TRadioButton *SFGenConfigMDActiveHiRadioButton;
        TRadioButton *SFGenConfigMDActiveLoRadioButton;
        TCheckBox *SFGenConfigMDEnableCheckBox;
        TCheckBox *CheckBox12;
        TCheckBox *CheckBox13;
        TComboBox *FGenConfigSmartFieldGenIDComboBox;
        TComboBox *FGenConfigSmartFGenReaderIDComboBox;
        TStaticText *QuerySFGenStaticText;
        TRadioButton *QuerySFGenAnyRdrRadioButton;
        TRadioButton *QuerySFGenSpecificRdrRadioButton;
        TGroupBox *SetFStrengthGroupBox;
        TLabel *Label55;
        TLabel *Label192;
        TComboBox *ReaderFStrengthReaderComboBox;
        TEdit *ReaderFStrengthHostEdit;
        TListView *ReaderFStrengthListView;
        TBitBtn *ReaderFStrengthClearBitBtn;
        TCheckBox *ReaderFStrengthBroadcastCheckBox;
        TBitBtn *SetFStrengthBitBtn;
        TStaticText *SetFStrengthStaticText;
        TGroupBox *GroupBox62;
        TLabel *Label194;
        TLabel *Label195;
        TLabel *Label196;
        TLabel *Label197;
        TLabel *ReaderFStrengthLabel;
        TUpDown *ReaderFStrengthUpDown;
        TEdit *ReaderFStrengthEdit;
        TBitBtn *ReaderGetFStrengthBitBtn;
        TComboBox *ReaderModifyTXFComboBox;
        TLabel *Label193;
        TLabel *Label199;
        TCheckBox *ReaderINCDECTxFieldCheckBox;
        TCheckBox *ReaderSetABSTxFieldCheckBox;
        TGroupBox *ReaderRangeGroupBox;
        TRadioButton *ReaderShortRangeRadioButton;
        TRadioButton *ReaderLongRangeRadioButton;
        TBitBtn *ReaderIncreaseTXBitBtn;
        TBitBtn *ReaderDecreaseTXBitBtn;
        TLabel *Label154;
        TLabel *TagTempRepLowLimitLabel;
        TLabel *TagTempRepUpLimitLabel;
        TLabel *TagTempRepPeriodLabel;
        TLabel *TagTempPeriodRepTimeHourLabel;
        TLabel *TagTempPeriodRepTimeMinLabel;
        TLabel *Label153;
        TLabel *Label198;
        TLabel *Label200;
        TLabel *Label204;
        TLabel *Label205;
        TLabel *Label206;
        TLabel *Label207;
        TLabel *Label208;
        TLabel *Label209;
        TLabel *Label210;
        TLabel *Label211;
        TLabel *Label212;
        TLabel *Label213;
        TLabel *Label214;
        TLabel *Label215;
        TLabel *Label216;
        TLabel *Label217;
        TLabel *Label218;
        TGroupBox *ConfigTagLEDGroupBox;
        TLabel *Label226;
        TLabel *Label227;
        TEdit *ConfigTagLEDReptIDEdit;
        TComboBox *ConfigTagLEDReaderIDComboBox;
        TEdit *ConfigTagLEDHostIDEdit;
        TGroupBox *GroupBox74;
        TRadioButton *ConfigTagLEDAnyTagIDRadioButton;
        TRadioButton *ConfigTagLEDTagIDRadioButton;
        TEdit *ConfigTagLEDTagIDEdit;
        TGroupBox *GroupBox75;
        TCheckBox *ConfigTagLEDBroadcastRdrCheckBox;
        TGroupBox *GroupBox76;
        TRadioButton *ConfigTagLEDRNShortRadioButton;
        TRadioButton *ConfigTagLEDRNLongRadioButton;
        TGroupBox *GroupBox77;
        TLabel *Label229;
        TBitBtn *ConfigTagLEDBitBtn;
        TStaticText *ConfigTagLEDStaticText;
        TBitBtn *ConfigTagLEDGetBitBtn;
        TEdit *ConfigTagLEDNumCyclesEdit;
        TLabel *Label223;
        TGroupBox *GroupBox71;
        TRadioButton *CallTagDisableLEDRadioButton;
        TRadioButton *CallTagEnableLEDRadioButton;
        TGroupBox *GroupBox73;
        TRadioButton *QueryTagDisableLEDRadioButton;
        TRadioButton *QueryTagEnableLEDRadioButton;
        TGroupBox *GroupBox78;
        TRadioButton *DisableTagDisableLEDRadioButton;
        TRadioButton *DisableTagEnableLEDRadioButton;
        TGroupBox *GroupBox79;
        TRadioButton *EnableTagDisableLEDRadioButton;
        TRadioButton *EnableTagEnableLEDRadioButton;
        TBitBtn *DownloadRdrBitBtn;
        TStaticText *DownloadRdrStaticText;
        TGroupBox *DownloadRdrGroupBox;
        TLabel *Label236;
        TLabel *Label237;
        TComboBox *DownloadRdrReaderComboBox;
        TEdit *DownloadRdrHostEdit;
        TGroupBox *GroupBox80;
        TRadioButton *DownloadRdrProcessCRadioButton;
        TRadioButton *DownloadRdrProcessDRadioButton;
        TRadioButton *DownloadRdrProcessERadioButton;
        TGroupBox *GroupBox81;
        TEdit *DownloadRdrFileNameEdit;
        TLabel *Label238;
        TBitBtn *DownloadRdrGetFileBitBtn;
        TProgressBar *DownloadRdrProgressBarE;
        TGroupBox *GroupBox82;
        TLabel *Label239;
        TLabel *Label240;
        TLabel *DownloadRdrPCVerLabel;
        TLabel *Label242;
        TLabel *DownloadRdrPCDateLabel;
        TLabel *Label244;
        TLabel *Label245;
        TLabel *DownloadRdrPDVerLabel;
        TLabel *DownloadRdrPDDateLabel;
        TLabel *Label248;
        TLabel *Label249;
        TLabel *Label250;
        TLabel *DownloadRdrPEVerLabel;
        TLabel *DownloadRdrPEDateLabel;
        TLabel *Label253;
        TBitBtn *DownloadRdrBootQueryBitBtn;
        TProgressBar *DownloadRdrProgressBarD;
        TProgressBar *DownloadRdrProgressBarC;
        TLabel *Label254;
        TLabel *Label255;
        TLabel *Label256;
        TGroupBox *GroupBox83;
        TLabel *Label257;
        TLabel *Label258;
        TLabel *Label259;
        TLabel *DownloadLabelC;
        TLabel *DownloadLabelD;
        TLabel *DownloadLabelE;
        TOpenDialog *DownloadRdrOpenDialog;
        TBitBtn *DownloadSFGenBitBtn;
        TStaticText *DownloadSmartFGenStaticText;
        TLabel *DownloadRdrFileNameLabel;
        TTimer *TimeTimer1;
        TToolButton *EncryptToolButton;
        TGroupBox *EncryptGroupBox;
        TGroupBox *GroupBox85;
        TRadioButton *EncryptPCRadioButton;
        TRadioButton *EncryptPDRadioButton;
        TRadioButton *EncryptPERadioButton;
        TGroupBox *GroupBox86;
        TLabel *Label252;
        TLabel *EncryptFileNameLabel;
        TEdit *EncryptFileNameEdit;
        TBitBtn *EncryptGetFileBitBtn;
        TGroupBox *GroupBox88;
        TLabel *Label276;
        TLabel *EncryptStatusCLabel;
        TRadioButton *EncryptPJRadioButton;
        TRadioButton *EncryptPKRadioButton;
        TLabel *Label241;
        TLabel *EncryptFNameCLabel;
        TBitBtn *EncryptClearBitBtn;
        TLabel *EncryptStatusDLabel;
        TLabel *Label247;
        TLabel *Label251;
        TLabel *EncryptFNameDLabel;
        TLabel *EncryptStatusELabel;
        TLabel *Label263;
        TLabel *Label264;
        TLabel *EncryptFNameELabel;
        TLabel *EncryptStatusJLabel;
        TLabel *Label267;
        TLabel *Label268;
        TLabel *EncryptFNameJLabel;
        TLabel *EncryptStatusKLabel;
        TLabel *Label275;
        TLabel *Label277;
        TLabel *EncryptFNameKLabel;
        TBitBtn *EncryptBitBtn;
        TGroupBox *DownloadSFGenGroupBox;
        TLabel *Label243;
        TLabel *Label246;
        TLabel *Label260;
        TLabel *Label261;
        TComboBox *DownloadSFGenReaderComboBox;
        TEdit *DownloadSFGenHostEdit;
        TGroupBox *GroupBox87;
        TRadioButton *DownloadSFGenProcessJRadioButton;
        TRadioButton *DownloadSFGenProcessKRadioButton;
        TGroupBox *GroupBox89;
        TLabel *Label265;
        TLabel *DownloadSFGenFileNameLabel;
        TEdit *DownloadSFGenFileNameEdit;
        TBitBtn *DownloadSFGenGetFileBitBtn;
        TGroupBox *GroupBox90;
        TLabel *Label269;
        TLabel *Label270;
        TLabel *DownloadSFGenPJVerLabel;
        TLabel *Label272;
        TLabel *DownloadSFGenPJDateLabel;
        TLabel *Label274;
        TLabel *Label278;
        TLabel *DownloadSFGenPKVerLabel;
        TLabel *DownloadSFGenPKDateLabel;
        TLabel *Label281;
        TBitBtn *DownloadSFGenBootQueryBitBtn;
        TProgressBar *DownloadSFGenProgressBarK;
        TProgressBar *DownloadSFGenProgressBarJ;
        TGroupBox *GroupBox91;
        TLabel *Label287;
        TLabel *Label288;
        TLabel *DownloadLabelJ;
        TLabel *DownloadLabelK;
        TLabel *Label293;
        TComboBox *DownloadSFGenIDComboBox;
        TGroupBox *GroupBox84;
        TCheckBox *AssignReaderModifyTXCheckBox;
        TComboBox *AssignReaderTXComboBox;
        TLabel *Label89;
        TLabel *Label92;
        TGroupBox *AssignReaderWTGroupBox;
        TLabel *AssignReaderTimeLabel;
        TComboBox *AssignReaderWTComboBox;
        TRadioButton *AssignReaderWTSecRadioButton;
        TRadioButton *AssignReaderWTMinRadioButton;
        TRadioButton *AssignReaderWTHourRadioButton;
        TCheckBox *AssignReaderModifyWTCheckBox;
        TLabel *AssignReaderSecLabel;
        TLabel *AssignReaderMinLabel;
        TLabel *AssignReaderHourLabel;
        TGroupBox *GroupBox93;
        TEdit *AssignReaderNewIDEdit;
        TCheckBox *AssignReaderNewRdrCheckBox;
        TGroupBox *GroupBox94;
        TLabel *Label30;
        TEdit *AssignReaderHostIDEdit;
        TEdit *AssignReaderNewHostIDEdit;
        TCheckBox *AssignReaderNewHostCheckBox;
        TBitBtn *AssignReaderGetConfigBitBtn;
        TGroupBox *AssignReaderMDGroupBox;
        TCheckBox *AssignReaderEnableMDCheckBox;
        TRadioButton *AssignReaderMDActiveHiRadioButton;
        TRadioButton *AssignReaderMDActiveLoRadioButton;
        TLabel *AssignReaderEnableLabel;
        TLabel *AssignReaderActiveHiLabel;
        TLabel *AssignReaderActiveLoLabel;
        TCheckBox *AssignReaderModifyMDCheckBox;
        TCheckBox *AssignReaderBroadcastReaderCheckBox;
        TBitBtn *AssignReaderSetConfigBitBtn;
        TLabel *TestLabel;
        TLabel *test2;
        TLabel *test3;
        TCheckBox *DownloadRdrTestCheckBox;
        TEdit *EncryptPCVerEdit;
        TLabel *Label234;
        TEdit *EncryptPDVerEdit;
        TEdit *EncryptPEVerEdit;
        TEdit *EncryptPJVerEdit;
        TEdit *EncryptPKVerEdit;
        TListView *QueryTagListView;
        TButton *QueryTagClearListButton;
        TCheckBox *QueryTagKeepItemsCheckBox;
        TGroupBox *GroupBox97;
        TGroupBox *GroupBox98;
        TRadioButton *EnableRelay1RadioButton;
        TRadioButton *DisableRelay1RadioButton;
        TRadioButton *EnableRelay2RadioButton;
        TRadioButton *DisableRelay2RadioButton;
        TLabel *Label235;
        TLabel *RxBatteryLabel;
        TLabel *Label262;
        TLabel *RxRSSILabel;
        TLabel *Label266;
        TLabel *Label271;
        TLabel *Label273;
        TGroupBox *GroupBox99;
        TLabel *Label279;
        TEdit *ConfigTagLEDSpeakerEdit;
        TLabel *Label280;
        TRadioButton *ConfigLEDRadioButton;
        TRadioButton *ConfigLEDSpeakerRadioButton;
        TGroupBox *GroupBox100;
        TRadioButton *CallTagDisableSpeakerRadioButton;
        TRadioButton *CallTagEnableSpeakerRadioButton;
        TGroupBox *GroupBox101;
        TRadioButton *QueryTagDisableSpeakerRadioButton;
        TRadioButton *QueryTagEnableSpeakerRadioButton;
        TGroupBox *GroupBox102;
        TRadioButton *DisableTagDisableSpeakerRadioButton;
        TRadioButton *DisableTagEnableSpeakerRadioButton;
        TGroupBox *GroupBox103;
        TRadioButton *EnableTagDisableSpeakerRadioButton;
        TRadioButton *EnableTagEnableSpeakerRadioButton;
        TLabel *Label88;
        TLabel *EnableTagFactoryLabel;
        TLabel *Label91;
        TLabel *DisableTagTypeFactoryName;
        TLabel *DisableTagFactoryLabel;
        TCheckBox *RdrCodeVerBroadcastRdrCheckBox;
        TListView *RdrCodeVerListView;
        TCheckBox *ConfigTagLEDBroadcastReaderCheckBox;
        TListView *ConfigTagSpeakerListView;
        TListView *ConfigTagLEDListView;
        TBitBtn *ConfigTagSpeakerGetBitBtn;
        TBitBtn *ConfigTagSpeakerClearListBitBtn;
        TCheckBox *ConfigTagSpeakerKeepListCheckBox;
        TBitBtn *ConfigTagLEDClearListBitBtn;
        TCheckBox *ConfigTagLEDKeepListCheckBox;
        TGroupBox *GroupBox92;
        TCheckBox *FGenConfigTagRdrIDModifyCheckBox;
        TCheckBox *FGenConfigTagRdrIDCheckBox;
        TLabel *Label42;
        TCheckBox *Input1ModifyCheckBox;
        TCheckBox *Input2ModifyCheckBox;
        TLabel *Label43;
        TLabel *Label48;
        TLabel *Label224;
        TLabel *Label225;
        TLabel *Label228;
        TLabel *Label230;
        TRadioButton *EnableTagIDRangeRadioButton;
        TComboBox *EnableTagIDRangeComboBox;
        TRadioButton *DisableTagIDRangeRadioButton;
        TComboBox *DisableTagIDRangeComboBox;
        TComboBox *CallTagIDRangeComboBox;
        TRadioButton *CallTagIDRangeRadioButton;
        TComboBox *QueryTagIDRangeComboBox;
        TRadioButton *QueryTagIDRangeRadioButton;
        TRadioButton *ConfigTagIDRangeRadioButton;
        TComboBox *ConfigTagIDRangeComboBox;
        TRadioButton *TagTempTagIDRangeRadioButton;
        TComboBox *TagTempTagIDRangeComboBox;
        TRadioButton *ReadMemoryTagIDRangeRadioButton;
        TComboBox *ReadMemoryTagIDRangeComboBox;
        TRadioButton *WriteMemoryTagIDRangeRadioButton;
        TComboBox *WriteMemoryTagIDRangeComboBox;
        TGroupBox *GroupBox95;
        TGroupBox *GroupBox96;
        TEdit *AssignTagRdrTagIDEdit;
        TGroupBox *GroupBox104;
        TRadioButton *AssignTagRdrLongRNDRadioButton;
        TRadioButton *AssignTagRdrShortRNDRadioButton;
        TRadioButton *AssignTagRdrTagIDRadioButton;
        TRadioButton *AssignTagRdrTagIDRangeRadioButton;
        TComboBox *AssignTagRdrTagIDRangeComboBox;
        TRadioButton *AssignTagRdrAnyTagIDRadioButton;
        TGroupBox *GroupBox105;
        TGroupBox *GroupBox106;
        TLabel *Label74;
        TLabel *Label72;
        TEdit *ConfigTagRNDShortUpEdit;
        TLabel *Label78;
        TEdit *ConfigTagRNDShortLowEdit;
        TEdit *ConfigTagRNDLongUpEdit;
        TLabel *Label75;
        TEdit *ConfigTagRNDLongLowEdit;
        TLabel *Label65;
        TLabel *Label73;
        TGroupBox *GroupBox107;
        TEdit *ConfigTagRNDTagIDEdit;
        TRadioButton *ConfigTagRNDTagIDRadioButton;
        TRadioButton *ConfigTagRNDTagIDRangeRadioButton;
        TComboBox *ConfigTagRNDTagIDRangeComboBox;
        TRadioButton *ConfigTagRNDAnyTagIDRadioButton;
        TGroupBox *GroupBox108;
        TRadioButton *ConfigTagRNDLongRespRadioButton;
        TRadioButton *ConfigTagRNDShortRespRadioButton;
        TRadioButton *ConfigTagLEDTagIDRangeRadioButton;
        TComboBox *ConfigTagLEDTagIDRangeComboBox;
        TGroupBox *GroupBox109;
        TRadioButton *SmartFGenLEDDisableRadioButton;
        TRadioButton *SmartFGenLEDEnableRadioButton;
        TGroupBox *GroupBox110;
        TRadioButton *SmartFGenSpkDisableRadioButton;
        TRadioButton *SmartFGenSpkEnableRadioButton;
    TTimer *GeneralTimer;
    TCheckBox *LargeDataCheckBox;
    TLabel *BytesWrittenLabel;
    TLabel *WriteTimeLabel;
    TTimer *WriteTimer;
    TLabel *WriteNumPKtLabel;
    TLabel *WriteNumRetryLabel;
    TTimer *ReadTimer;
    TTimer *RetryTimer;
    TLabel *ReadNumRetryLabel;
        TCheckBox *ReadTagLargeDataCheckBox;
        TLabel *ReadTagReadLabel;
        TLabel *ReadTimeLabel;
        TTimer *ReadLargDataTimer;
        TCheckBox *DisplayDataCheckBox;
        TBitBtn *ReadMemLargeDataStopBitBtn;
        TGroupBox *GroupBox111;
        TCheckBox *SFGenConfigFSRangeModifyCheckBox;
        TRadioButton *SFGenConfigShortRangeRadioButton;
        TRadioButton *SFGenConfigLongRangeRadioButton;
        TComboBox *FGenConfigPotentiComboBox;
        TLabel *FGenConfigFSLabel;
        TBitBtn *FGenConfigPotBitBtn;
        TCheckBox *FGenConfigPotentiModifyCheckBox;
        TComboBox *EnableTagTypeComboBox;
        TComboBox *CallTagTypeComboBox;
        TComboBox *ConfigTagTypeComboBox;
        TComboBox *ConfigTagNewTagTypeComboBox;
        TComboBox *TagTempTagTypeComboBox;
        TComboBox *WriteMemoryTagTypeComboBox;
        TComboBox *ReadMemoryTagTypeComboBox;
        TComboBox *FGenConfigTagTypeComboBox;
        TComboBox *ConfigTagRNDTagTypeComboBox;
        TComboBox *AssignTagRdrTagTypeComboBox;
        TComboBox *ConfigTagLEDTagTypeComboBox;
        TComboBox *SmartFGenTagTypeComboBox;
        TComboBox *QueryTagTypeComboBox;
        TComboBox *DisableTagTypeComboBox;
        TComboBox *EnableFGenTagTypeComboBox;
        TComboBox *SFGenConfigTagTypeComboBox;
        TLabel *DisableTagType04Name;
        TLabel *DisableTagType04Label;
        TLabel *DisableTagType06Name;
        TLabel *DisableTagType06Label;
        TLabel *DisableTagType05Name;
        TLabel *DisableTagType05Label;
        TLabel *ReportType05Label;
        TLabel *ReportType5Label;
        TLabel *ReportType06Label;
        TLabel *ReportType6Label;
        TLabel *Label115;
        TLabel *ReportFACTLabel;
        TLabel *EnableTagType04Name;
        TLabel *EnableTagType04Label;
        TLabel *EnableTagType05Name;
        TLabel *EnableTagType05Label;
        TLabel *EnableTagType06Name;
        TLabel *EnableTagType06Label;
        TCheckBox *FGenConfigEnableISCheckBox;
        TCheckBox *FGenConfigLEDCheckBox;
        TLabel *Label27;
        TCheckBox *FGenConfigSPKCheckBox;
        TLabel *Label28;
        TLabel *FGenConfigEnableISLabel;
        TRadioButton *SmartFGenShortRangeRadioButton;
        TRadioButton *SmartFGenLongRangeRadioButton;
        TCheckBox *TagTempLoggingCheckBox;
        TLabel *Label32;
        TCheckBox *TagTempWarpAroundCheckBox;
        TLabel *Label66;
        TBitBtn *TagTempTimeStampBitBtn;
        TComboBox *AssignReaderIDComboBox;
        TLabel *FGenConfigHTimeLabel;
        TComboBox *FGenConfigHoldTimeComboBox;
        TLabel *FGenConfigHoldTimeLabel;
        TLabel *FGenConfigWaitTimeLabel;
        TCheckBox *FGenConfigWaitTimeCheckBox;
        TCheckBox *FGenConfigHoldTimeCheckBox;
        //void __fastcall Timer1Timer(TObject *Sender);
        void __fastcall Timer2Timer(TObject *Sender);
        void __fastcall MainStatusBarDrawPanel(TStatusBar *StatusBar,
          TStatusPanel *Panel, const TRect &Rect);
        void __fastcall FormActivate(TObject *Sender);
        void __fastcall RecClearClick(TObject *Sender);
        void __fastcall TxClearClick(TObject *Sender);
        void __fastcall FormClose(TObject *Sender, TCloseAction &Action);
        //void __fastcall WriteBitBtnClick(TObject *Sender);
        //void __fastcall EnableTagBitBtnClick(TObject *Sender);
        //void __fastcall DisableTagBitBtnClick(TObject *Sender);
        void __fastcall DecRadioButtonClick(TObject *Sender);
        void __fastcall HexRadioButtonClick(TObject *Sender);
        //void __fastcall EnableTimeCheckBoxClick(TObject *Sender);
        void __fastcall ClearMsgBitBtnClick(TObject *Sender);
        //void __fastcall CommConfigToolButton1Click(TObject *Sender);
        void __fastcall CommToolButtonClick(TObject *Sender);
        void __fastcall DiagnosticsBitBtnClick(TObject *Sender);
        void __fastcall DebugDisplayBitBtnClick(TObject *Sender);
        void __fastcall RxClearBitBtnClick(TObject *Sender);
        void __fastcall TxClearBitBtnClick(TObject *Sender);
        void __fastcall FormDestroy(TObject *Sender);
        void __fastcall ResetDeviceBitBtnClick(TObject *Sender);
        void __fastcall EnableReaderBitBtnClick(TObject *Sender);
        void __fastcall DisableReaderBitBtnClick(TObject *Sender);
        void __fastcall ConfigTagBitBtnClick(TObject *Sender);
        void __fastcall EnableTagBitBtnClick(TObject *Sender);
        void __fastcall DisableTagBitBtnClick(TObject *Sender);
        void __fastcall QueryTagBitBtnClick(TObject *Sender);
        void __fastcall Timer3Timer(TObject *Sender);
        void __fastcall StopGoRXBitBtnClick(TObject *Sender);
        void __fastcall StopGoTXBitBtnClick(TObject *Sender);
        void __fastcall AccessCtrlRadioButtonClick(TObject *Sender);
        void __fastcall AssetCtrlRadioButtonClick(TObject *Sender);
        void __fastcall InvetRadioButtonClick(TObject *Sender);
        void __fastcall CarRadioButtonClick(TObject *Sender);
        void __fastcall RecordToolButtonClick(TObject *Sender);
        void __fastcall DecRadioButtonMouseUp(TObject *Sender,
          TMouseButton Button, TShiftState Shift, int X, int Y);
        void __fastcall HexRadioButtonMouseUp(TObject *Sender,
          TMouseButton Button, TShiftState Shift, int X, int Y);
        void __fastcall AnyTagRadioButtonClick(TObject *Sender);
        void __fastcall TagIDRadioButtonClick(TObject *Sender);
        void __fastcall TagIDRadioButtonMouseDown(TObject *Sender,
          TMouseButton Button, TShiftState Shift, int X, int Y);
        void __fastcall FactoryIDRadioButtonMouseDown(TObject *Sender,
          TMouseButton Button, TShiftState Shift, int X, int Y);
        void __fastcall CallTagBitBtnClick(TObject *Sender);
        void __fastcall TextDisplayBitBtnClick(TObject *Sender);
        void __fastcall DebugDisplayToolButtonClick(TObject *Sender);
        void __fastcall TextDisplayToolButtonClick(TObject *Sender);
        void __fastcall HelpToolButtonClick(TObject *Sender);
        void __fastcall StopRecToolButtonClick(TObject *Sender);
        void __fastcall StartRecToolButtonClick(TObject *Sender);
        void __fastcall CloseToolButtonClick(TObject *Sender);
        void __fastcall ClearTagListBitBtnClick(TObject *Sender);
        void __fastcall ConfigToolButtonClick(TObject *Sender);
        void __fastcall DetectedTagListViewDrawItem(
          TCustomListView *Sender, TListItem *Item, TRect &Rect,
          TOwnerDrawState State);
        void __fastcall DetectedTagListViewDblClick(TObject *Sender);
        void __fastcall RelayBitBtnClick(TObject *Sender);
        void __fastcall AssignReaderBitBtnClick(TObject *Sender);
        void __fastcall QueryReaderBitBtnClick(TObject *Sender);
        void __fastcall DetectedTagListViewColumnClick(TObject *Sender,
          TListColumn *Column);
        void __fastcall DetectedTagListViewCompare(TObject *Sender,
          TListItem *Item1, TListItem *Item2, int Data, int &Compare);
        void __fastcall NewReaderIDCheckBoxClick(TObject *Sender);
        void __fastcall NewHostIDCheckBoxClick(TObject *Sender);
        void __fastcall NewRepeaterIDCheckBoxClick(TObject *Sender);
        //void __fastcall ReaderIDEditChange(TObject *Sender);
        //void __fastcall ReaderIDEditExit(TObject *Sender);
        void __fastcall NewReaderIDEditChange(TObject *Sender);
        void __fastcall NewReaderIDEditExit(TObject *Sender);
        void __fastcall HostIDEditChange(TObject *Sender);
        void __fastcall RdrCodeVerHostIDEdit(TObject *Sender);
        void __fastcall RepeaterIDEditChange(TObject *Sender);
        void __fastcall RepeaterIDEditExit(TObject *Sender);
        void __fastcall FieldGenIDEditChange(TObject *Sender);
        void __fastcall FieldGenIDEditExit(TObject *Sender);
        void __fastcall NewHostIDEditChange(TObject *Sender);
        void __fastcall NewHostIDEditExit(TObject *Sender);
        void __fastcall NewRepeaterIDEditChange(TObject *Sender);
        void __fastcall NewRepeaterIDEditExit(TObject *Sender);
        void __fastcall NewListItemCheckBoxClick(TObject *Sender);
        void __fastcall ChinaDemoToolButtonClick(TObject *Sender);
        void __fastcall ClearMsgClick(TObject *Sender);
        void __fastcall ReaderIDComboBoxChange(TObject *Sender);
        void __fastcall ReaderIDComboBoxExit(TObject *Sender);
        void __fastcall TagIDEditChange(TObject *Sender);
        void __fastcall TagIDEditExit(TObject *Sender);
        void __fastcall ReaderVersionClick(TObject *Sender);
        void __fastcall TxTimeSecRadioButtonClick(TObject *Sender);
        void __fastcall WaitTimeSecRadioButtonClick(TObject *Sender);
        void __fastcall WaitTimeMinRadioButtonClick(TObject *Sender);
        void __fastcall WaitTimeHourRadioButtonClick(TObject *Sender);
        void __fastcall TxTimeAllRadioButtonClick(TObject *Sender);
        void __fastcall WaitTimeAllRadioButtonClick(TObject *Sender);
        void __fastcall ConfigTxTimeBitBtnClick(TObject *Sender);
        void __fastcall ConfigTxTimeReaderRadioButtonClick(
          TObject *Sender);
        void __fastcall ConfigTxTimeFieldGenRadioButtonClick(
          TObject *Sender);
        void __fastcall ConfigFGenBitBtnClick(TObject *Sender);
        void __fastcall FGenConfigTxTimeSecRadioButtonClick(TObject *Sender);
        void __fastcall FGenConfigWaitTimeSecRadioButtonClick(TObject *Sender);
        void __fastcall FGenConfigWaitTimeMinRadioButtonClick(TObject *Sender);
        void __fastcall FGenConfigWaitTimeHourRadioButtonClick(TObject *Sender);
        void __fastcall QueryFGenFgRadioButtonClick(TObject *Sender);
        void __fastcall QueryFGenRdrRadioButtonClick(TObject *Sender);
        void __fastcall QueryFGenSmartFGRadioButtonClick(TObject *Sender);
        void __fastcall QueryFGenBitBtnClick(TObject *Sender);
        //void __fastcall QueryFGenBroadcastCheckBoxClick(TObject *Sender);
        void __fastcall QueryFGenClearBitBtnClick(TObject *Sender);
        void __fastcall QueryFGenListViewColumnClick(TObject *Sender,
          TListColumn *Column);
        void __fastcall QueryFGenListViewCompare(TObject *Sender,
          TListItem *Item1, TListItem *Item2, int Data, int &Compare);
        void __fastcall AssignTagRdrBitBtnClick(TObject *Sender);
        void __fastcall AssignTagRdrClearBitBtnClick(TObject *Sender);
        void __fastcall AssignTagRdrBroadcastRdrCheckBoxClick(
          TObject *Sender);
        void __fastcall ConfigTagRNDRadioButtonClick(TObject *Sender);
        void __fastcall ConfigTagRNDBitBtnClick(TObject *Sender);
        void __fastcall ConfigTagRNDClearBitBtnClick(TObject *Sender);
        void __fastcall ConfigTagRNDBroadcastCheckBoxClick(
          TObject *Sender);
        void __fastcall ResetReaderStaticTextClick(TObject *Sender);
        void __fastcall EnableReaderStaticTextClick(TObject *Sender);
        void __fastcall DisableReaderStaticTextClick(TObject *Sender);
        void __fastcall QueryReaderStaticTextClick(TObject *Sender);
        void __fastcall AssignReaderStaticTextClick(TObject *Sender);
        void __fastcall ReaderVersionStaticTextClick(TObject *Sender);
        void __fastcall ConfigTxTimeStaticTextClick(TObject *Sender);
        void __fastcall RelayStaticTextClick(TObject *Sender);
        void __fastcall ConfigFGenStaticTextClick(TObject *Sender);
        void __fastcall QueryFGenStaticTextClick(TObject *Sender);
        void __fastcall ConfigTagStaticTextClick(TObject *Sender);
        void __fastcall EnableTagStaticTextClick(TObject *Sender);
        void __fastcall DisableTagStaticTextClick(TObject *Sender);
        void __fastcall QueryTagStaticTextClick(TObject *Sender);
        void __fastcall CallTagStaticTextClick(TObject *Sender);
        void __fastcall AssignTagRdrStaticTextClick(TObject *Sender);
        void __fastcall ConfigTagRNDStaticTextClick(TObject *Sender);
        void __fastcall ResetBroadcastReaderCheckBoxClick(TObject *Sender);
        void __fastcall ResetBroadcastRepeaterCheckBoxClick(
          TObject *Sender);
        //void __fastcall ResetModifyReaderCheckBoxClick(TObject *Sender);
        void __fastcall ResetClearBitBtnClick(TObject *Sender);
        void __fastcall EnableReaderBroadcastRdrCheckBoxClick(
          TObject *Sender);
        void __fastcall EnableReaderGroupBoxClick(TObject *Sender);
        void __fastcall DisableReaderClearBitBtnClick(TObject *Sender);
        void __fastcall DisableReaderBroadcastRdrCheckBoxClick(
          TObject *Sender);
        void __fastcall QueryReaderBroadcastRdrCheckBoxClick(
          TObject *Sender);
        void __fastcall AssignReaderBroadcastRdrCheckBoxClick(
          TObject *Sender);
        void __fastcall ConfigTagNewTagTypeCheckBoxClick(TObject *Sender);
        void __fastcall ConfigTagTIFCheckBoxClick(TObject *Sender);
        void __fastcall ConfigTagGCCheckBoxClick(TObject *Sender);
        void __fastcall ConfigTagEnableTimeCheckBoxClick(TObject *Sender);
        void __fastcall ConfigTagTagIDRadioButtonClick(TObject *Sender);
        void __fastcall ConfigTagNewTagIDCheckBoxClick(TObject *Sender);
        void __fastcall ConfigTagAnyTagIDRadioButtonClick(TObject *Sender);
        void __fastcall ConfigTagFactoryIDCheckBoxClick(TObject *Sender);
        void __fastcall ConfigTagMSRadioButtonClick(TObject *Sender);
        void __fastcall ConfigTagHourRadioButtonClick(TObject *Sender);
        void __fastcall ConfigTagMinRadioButtonClick(TObject *Sender);
        void __fastcall ConfigTagSecRadioButtonClick(TObject *Sender);
        void __fastcall RNChangeRadioButtonClick(TObject *Sender);
        void __fastcall RNNoChangeRadioButtonClick(TObject *Sender);
        void __fastcall ConfigTagRNChangeRadioButtonClick(TObject *Sender);
        void __fastcall ConfigTagRNNoChangeRadioButtonClick(
          TObject *Sender);
        void __fastcall EnableTagListViewColumnClick(TObject *Sender,
          TListColumn *Column);
        void __fastcall EnableTagListViewCompare(TObject *Sender,
          TListItem *Item1, TListItem *Item2, int Data, int &Compare);
        void __fastcall EnableTagClearBitBtnClick(TObject *Sender);
        void __fastcall DisableAnyTagIDRadioButtonClick(TObject *Sender);
        void __fastcall DisableTagIDRadioButtonClick(TObject *Sender);
        void __fastcall EnableTagIDRadioButtonClick(TObject *Sender);
        void __fastcall EnableAnyTagIDRadioButtonClick(TObject *Sender);
        void __fastcall DisableTagClearBitBtnClick(TObject *Sender);
        void __fastcall AssignReaderListViewColumnClick(TObject *Sender,
          TListColumn *Column);
        void __fastcall AssignReaderListViewCompare(TObject *Sender,
          TListItem *Item1, TListItem *Item2, int Data, int &Compare);
        void __fastcall QueryReaderListViewColumnClick(TObject *Sender,
          TListColumn *Column);
        void __fastcall QueryReaderListViewCompare(TObject *Sender,
          TListItem *Item1, TListItem *Item2, int Data, int &Compare);
        void __fastcall DisableReaderListViewColumnClick(TObject *Sender,
          TListColumn *Column);
        void __fastcall DisableReaderListViewCompare(TObject *Sender,
          TListItem *Item1, TListItem *Item2, int Data, int &Compare);
        void __fastcall EnableReaderListViewColumnClick(TObject *Sender,
          TListColumn *Column);
        void __fastcall EnableReaderListViewCompare(TObject *Sender,
          TListItem *Item1, TListItem *Item2, int Data, int &Compare);
        void __fastcall ResetListViewColumnClick(TObject *Sender,
          TListColumn *Column);
        void __fastcall ResetListViewCompare(TObject *Sender,
          TListItem *Item1, TListItem *Item2, int Data, int &Compare);
        void __fastcall ConfigTagRNDListViewColumnClick(TObject *Sender,
          TListColumn *Column);
        void __fastcall ConfigTagRNDListViewCompare(TObject *Sender,
          TListItem *Item1, TListItem *Item2, int Data, int &Compare);
        void __fastcall AssignTagRdrListViewColumnClick(TObject *Sender,
          TListColumn *Column);
        void __fastcall AssignTagRdrListViewCompare(TObject *Sender,
          TListItem *Item1, TListItem *Item2, int Data, int &Compare);
        void __fastcall QueryAnyTagIDRadioButtonClick(TObject *Sender);
        void __fastcall QueryTagIDRadioButtonClick(TObject *Sender);
        void __fastcall CallTagAnyTagIDRadioButtonClick(TObject *Sender);
        void __fastcall AssignReaderClearBitBtnClick(TObject *Sender);
        void __fastcall EnableReaderClearBitBtnClick(TObject *Sender);
        void __fastcall DisableTagListViewColumnClick(TObject *Sender,
          TListColumn *Column);
        void __fastcall DisableTagListViewCompare(TObject *Sender,
          TListItem *Item1, TListItem *Item2, int Data, int &Compare);
        void __fastcall QueryReaderClearBitBtnClick(TObject *Sender);
        void __fastcall ReaderFgenToolButtonClick(TObject *Sender);
        //void __fastcall FGenConfigReaderIDModifyCheckBoxClick(
        //TObject *Sender);
        void __fastcall FGenConfigFGenIDModifyCheckBoxClick(
          TObject *Sender);
        void __fastcall FGenConfigTagTypeModifyCheckBoxClick(
          TObject *Sender);
        void __fastcall FGenConfigTagIDModifyCheckBoxClick(
          TObject *Sender);
        void __fastcall FGenConfigTxTimeModifyCheckBoxClick(
          TObject *Sender);
        void __fastcall FGenConfigWaitTimeModifyCheckBoxClick(
          TObject *Sender);
        void __fastcall FGenConfigTagIDRadioButtonClick(TObject *Sender);
        void __fastcall FGenConfigAnyTagIDRadioButtonClick(
          TObject *Sender);
        void __fastcall FGenConfigWaitTimeSecRadioButtonMouseUp(
          TObject *Sender, TMouseButton Button, TShiftState Shift, int X,
          int Y);
        void __fastcall FGenConfigWaitTimeHourRadioButtonMouseUp(
          TObject *Sender, TMouseButton Button, TShiftState Shift, int X,
          int Y);
        void __fastcall FGenConfigWaitTimeMinRadioButtonMouseUp(
          TObject *Sender, TMouseButton Button, TShiftState Shift, int X,
          int Y);
        void __fastcall FGenConfigRaRnModifyCheckBoxClick(TObject *Sender);
        //void __fastcall FGenConfigReaderIDEditChange(TObject *Sender);
        void __fastcall FGenConfigFGenIDEditChange(TObject *Sender);
        void __fastcall FGenConfigTagIDEditChange(TObject *Sender);
        void __fastcall ConfigFGenUpdateBitBtnClick(TObject *Sender);
        void __fastcall EnableRdrFGenStaticTextClick(TObject *Sender);
        void __fastcall EnableFGenBitBtnClick(TObject *Sender);
        void __fastcall WriteMemoryStaticTextClick(TObject *Sender);
        void __fastcall WriteMemoryBitBtnClick(TObject *Sender);
        //void __fastcall WriteMemoryNumByteEditChange(TObject *Sender);
        //void __fastcall WriteMemoryStringGridGetEditText(TObject *Sender,
          //int ACol, int ARow, AnsiString &Value);
        //void __fastcall WriteMemoryStringGridSetEditText(TObject *Sender,
          //int ACol, int ARow, const AnsiString Value);
        void __fastcall WriteMemoryTagIDRadioButtonClick(TObject *Sender);
        void __fastcall WriteMemoryAnyTagIDRadioButtonClick(
          TObject *Sender);
        void __fastcall WriteMemoryBroadcastRdrCheckBoxClick(
          TObject *Sender);
        void __fastcall WriteMemoryHexRadioButtonClick(TObject *Sender);
        void __fastcall WriteMemDecRadioButtonClick(TObject *Sender);
        void __fastcall WriteMemCharRadioButtonClick(TObject *Sender);
        void __fastcall ReadMemoryStaticTextClick(TObject *Sender);
        void __fastcall ReadMemoryBitBtnClick(TObject *Sender);
        void __fastcall ReadMemoryTagIDRadioButtonClick(TObject *Sender);
        void __fastcall ReadMemoryAnyTagIDRadioButtonClick(
          TObject *Sender);
        void __fastcall ReadMemoryHexRadioButtonClick(TObject *Sender);
        void __fastcall ReadMemDecRadioButtonClick(TObject *Sender);
        void __fastcall ReadMemCharRadioButtonClick(TObject *Sender);
        void __fastcall ReadMemoryNumByteEditChange(TObject *Sender);
        void __fastcall WriteMemoryClearBitBtnClick(TObject *Sender);
        void __fastcall TagTempRepLowLimitCheckBoxClick(TObject *Sender);
        void __fastcall TagTempRepUpLimitCheckBoxClick(TObject *Sender);
        void __fastcall TagTempRepPeriodCheckBoxClick(TObject *Sender);
        void __fastcall TagTempChangeReportCheckBoxClick(TObject *Sender);
        void __fastcall TagTempChangeUpLimitCheckBoxClick(TObject *Sender);
        void __fastcall TagTempChangeLowLimitCheckBoxClick(
          TObject *Sender);
        void __fastcall TagTempStaticTextClick(TObject *Sender);
        void __fastcall TagTempBitBtnClick(TObject *Sender);
        void __fastcall TagTempTagIDRadioButtonClick(TObject *Sender);
        void __fastcall TagTempAnyTagIDRadioButtonClick(TObject *Sender);
        void __fastcall TagTempLimitCdegRadioButtonClick(TObject *Sender);
        void __fastcall TagTempLimitFdegRadioButtonClick(TObject *Sender);
        void __fastcall TagTempRefreshBitBtnClick(TObject *Sender);
        void __fastcall TagTempReadTempValueBitBtnClick(TObject *Sender);
        void __fastcall TagTempCdegRadioButtonClick(TObject *Sender);
        void __fastcall TagTempFdegRadioButtonClick(TObject *Sender);
        void __fastcall TagTempPeriodRepTimeHourRadioButtonClick(
          TObject *Sender);
        void __fastcall TagTempPeriodRepTimeMinRadioButtonClick(
          TObject *Sender);
        void __fastcall TagTempPeriodRepTimeHourRadioButtonMouseDown(
          TObject *Sender, TMouseButton Button, TShiftState Shift, int X,
          int Y);
        void __fastcall TagTempPeriodRepTimeMinRadioButtonMouseDown(
          TObject *Sender, TMouseButton Button, TShiftState Shift, int X,
          int Y);
        void __fastcall TagTempRepLowLimitCheckBoxMouseDown(
          TObject *Sender, TMouseButton Button, TShiftState Shift, int X,
          int Y);
        void __fastcall TagTempRepUpLimitCheckBoxMouseDown(TObject *Sender,
          TMouseButton Button, TShiftState Shift, int X, int Y);
        void __fastcall TagTempRepPeriodCheckBoxMouseDown(TObject *Sender,
          TMouseButton Button, TShiftState Shift, int X, int Y);
        void __fastcall TagTempDisplayListCheckBoxClick(TObject *Sender);
        void __fastcall ClearTagTempListBitBtnClick(TObject *Sender);
        void __fastcall TagTempListViewColumnClick(TObject *Sender,
          TListColumn *Column);
        void __fastcall TagTempListViewCompare(TObject *Sender,
          TListItem *Item1, TListItem *Item2, int Data, int &Compare);
        void __fastcall TagTempListCdegRadioButtonClick(TObject *Sender);
        void __fastcall TagTempListFdegRadioButtonClick(TObject *Sender);
        void __fastcall TagTempListViewCustomDrawItem(
          TCustomListView *Sender, TListItem *Item, TCustomDrawState State,
          bool &DefaultDraw);
        void __fastcall FGenConfigAccessRadioButtonClick(TObject *Sender);
        void __fastcall FGenConfigAccessRadioButtonMouseDown(
          TObject *Sender, TMouseButton Button, TShiftState Shift, int X,
          int Y);
        void __fastcall FGenConfigAssetRadioButtonClick(TObject *Sender);
        void __fastcall FGenConfigInventoryRadioButtonClick(
          TObject *Sender);
        void __fastcall FGenConfigAnyTypeRadioButtonClick(TObject *Sender);
        void __fastcall FGenConfigAssetRadioButtonMouseDown(
          TObject *Sender, TMouseButton Button, TShiftState Shift, int X,
          int Y);
        void __fastcall FGenConfigInventoryRadioButtonMouseDown(
          TObject *Sender, TMouseButton Button, TShiftState Shift, int X,
          int Y);
        void __fastcall FGenConfigAnyTypeRadioButtonMouseDown(
          TObject *Sender, TMouseButton Button, TShiftState Shift, int X,
          int Y);
        void __fastcall FGenConfigActivePIRCheckBoxClick(TObject *Sender);
        void __fastcall FGenConfigActivePIRCheckBoxMouseDown(
          TObject *Sender, TMouseButton Button, TShiftState Shift, int X,
          int Y);
        void __fastcall EnableRelayCheckBoxMouseDown(TObject *Sender,
          TMouseButton Button, TShiftState Shift, int X, int Y);
        void __fastcall DisableRelayCheckBoxMouseDown(TObject *Sender,
          TMouseButton Button, TShiftState Shift, int X, int Y);
        void __fastcall RelayListViewCustomDrawItem(
          TCustomListView *Sender, TListItem *Item, TCustomDrawState State,
          bool &DefaultDraw);
        void __fastcall RelayListViewCustomDrawSubItem(
          TCustomListView *Sender, TListItem *Item, int SubItem,
          TCustomDrawState State, bool &DefaultDraw);
        void __fastcall RelayClearBitBtnClick(TObject *Sender);
        void __fastcall InputsStaticTextClick(TObject *Sender);
        void __fastcall InputsBitBtnClick(TObject *Sender);
        void __fastcall InputClearBitBtnClick(TObject *Sender);
        void __fastcall InputListViewColumnClick(TObject *Sender,
          TListColumn *Column);
        void __fastcall InputListViewCompare(TObject *Sender,
          TListItem *Item1, TListItem *Item2, int Data, int &Compare);
        void __fastcall QueryFGenSmartFGenBroadcastRdrCheckBoxClick(
          TObject *Sender);
        void __fastcall SmartFGenStaticTextClick(TObject *Sender);
        void __fastcall FGenResetStaticTextClick(TObject *Sender);
        void __fastcall FGenResetBitBtnClick(TObject *Sender);
        void __fastcall FGenConfigAssignedReaderIDModifyCheckBoxClick(
          TObject *Sender);
        void __fastcall FGenConfigAssignedReaderIDEditChange(
          TObject *Sender);
        void __fastcall SmartFGenBitBtnClick(TObject *Sender);
        void __fastcall SmartFGenNewDPotEditChange(TObject *Sender);
        void __fastcall SmartFGenBroadcastAllCheckBoxClick(
          TObject *Sender);
        void __fastcall SmartFGenTagIDRadioButtonClick(TObject *Sender);
        void __fastcall SmartFGenAnyTagIDRadioButtonClick(TObject *Sender);
        void __fastcall ConfigTagModifyRNCheckBoxClick(TObject *Sender);
        void __fastcall ConfigTagGetConfigBitBtnClick(TObject *Sender);
        void __fastcall ConfigTagModifyTamperCheckBoxClick(
          TObject *Sender);
        void __fastcall FGenResetClearBitBtnClick(TObject *Sender);
        void __fastcall QueryFGenSmartFGenBroadcastCheckBoxClick(
          TObject *Sender);
        void __fastcall GetProcRevDateBitBtnClick(TObject *Sender);
        void __fastcall QueryFGenClearRevListBitBtnClick(TObject *Sender);
        void __fastcall FGenConfigStandFgenRadioButtonClick(
          TObject *Sender);
        void __fastcall FGenConfigSmartFgenRadioButtonClick(
          TObject *Sender);
        void __fastcall FGenConfigMDCheckBoxClick(TObject *Sender);
        void __fastcall FGenConfigPotentiModifyCheckBoxClick(
          TObject *Sender);
        void __fastcall FGenConfigPotBitBtnClick(TObject *Sender);
        void __fastcall SmartFGenPotentioUpDownClick(TObject *Sender,
          TUDBtnType Button);
        void __fastcall SmartFGenGetDPotBitBtnClick(TObject *Sender);
        void __fastcall SmartFGenPotentioUpDownChangingEx(TObject *Sender,
          bool &AllowChange, short NewValue, TUpDownDirection Direction);
        void __fastcall PollTimerTimer(TObject *Sender);
        void __fastcall ClientSocketError(TObject *Sender,
          TCustomWinSocket *Socket, TErrorEvent ErrorEvent,
          int &ErrorCode);
        void __fastcall ClientSocketConnect(TObject *Sender,
          TCustomWinSocket *Socket);
        void __fastcall ClientSocketRead(TObject *Sender,
          TCustomWinSocket *Socket);
        void __fastcall AssignReaderNewRdrCheckBoxClick(TObject *Sender);
        void __fastcall AssignReaderNewHostCheckBoxClick(TObject *Sender);
        void __fastcall AssignReaderNoChangeCheckBoxClick(TObject *Sender);
        void __fastcall ReaderCMDRadioButtonClick(TObject *Sender);
        void __fastcall FGenCMDRadioButtonClick(TObject *Sender);
        void __fastcall SFGenCMDRadioButtonClick(TObject *Sender);
        void __fastcall TagCMDRadioButtonClick(TObject *Sender);
        void __fastcall QuerySFGenStaticTextClick(TObject *Sender);
        void __fastcall QuerySFGenBitBtnClick(TObject *Sender);
        void __fastcall ConfigSFGenStaticTextClick(TObject *Sender);
        void __fastcall ConfigSFGenBitBtnClick(TObject *Sender);
        void __fastcall SFGenConfigAssignedReaderIDModifyCheckBoxClick(
          TObject *Sender);
        void __fastcall SFGenConfigFGenIDModifyCheckBoxClick(
          TObject *Sender);
        void __fastcall SFGenConfigTagTypeModifyCheckBoxClick(
          TObject *Sender);
        void __fastcall SFGenConfigTagIDModifyCheckBoxClick(
          TObject *Sender);
        void __fastcall SFGenConfigRaRnModifyCheckBoxClick(
          TObject *Sender);
        void __fastcall SFGenConfigTxTimeModifyCheckBoxClick(
          TObject *Sender);
        void __fastcall SFGenConfigWaitTimeModifyCheckBoxClick(
          TObject *Sender);
        void __fastcall SFGenConfigMDCheckBoxClick(TObject *Sender);
        void __fastcall ConfigSFGenUpdateBitBtnClick(TObject *Sender);
        void __fastcall ResetCMDReaderBitBtnClick(TObject *Sender);
        void __fastcall CMDEnableTimerTimer(TObject *Sender);
        void __fastcall ResetCMDFGenBitBtnClick(TObject *Sender);
        void __fastcall ResetCMDSFGenBitBtnClick(TObject *Sender);
        void __fastcall ResetCMDTagBitBtnClick(TObject *Sender);
        void __fastcall TagTempListViewDblClick(TObject *Sender);
        void __fastcall QuerySFGenClearRevListBitBtnClick(TObject *Sender);
        void __fastcall QuerySFGenSmartFGenBroadcastCheckBoxClick(
          TObject *Sender);
        void __fastcall FGenConfigWaitTimeComboBoxClick(TObject *Sender);
        void __fastcall SFGenConfigWaitTimeSecRadioButtonClick(
          TObject *Sender);
        void __fastcall SFGenConfigWaitTimeMinRadioButtonClick(
          TObject *Sender);
        void __fastcall SFGenConfigWaitTimeHourRadioButtonClick(
          TObject *Sender);
        void __fastcall SFGenConfigWaitTimeSecRadioButtonMouseDown(
          TObject *Sender, TMouseButton Button, TShiftState Shift, int X,
          int Y);
        void __fastcall SFGenConfigWaitTimeMinRadioButtonMouseDown(
          TObject *Sender, TMouseButton Button, TShiftState Shift, int X,
          int Y);
        void __fastcall SFGenConfigWaitTimeHourRadioButtonMouseDown(
          TObject *Sender, TMouseButton Button, TShiftState Shift, int X,
          int Y);
        void __fastcall QuerySFGenClearBitBtnClick(TObject *Sender);
        void __fastcall SetFStrengthStaticTextClick(TObject *Sender);
        void __fastcall ReaderINCDECTxFieldCheckBoxClick(TObject *Sender);
        void __fastcall ReaderSetABSTxFieldCheckBoxClick(TObject *Sender);
        void __fastcall ReaderSetABSTxFieldCheckBoxMouseDown(
          TObject *Sender, TMouseButton Button, TShiftState Shift, int X,
          int Y);
        void __fastcall ReaderINCDECTxFieldCheckBoxMouseDown(
          TObject *Sender, TMouseButton Button, TShiftState Shift, int X,
          int Y);
        void __fastcall SetFStrengthBitBtnClick(TObject *Sender);
        void __fastcall ReaderGetFStrengthBitBtnClick(TObject *Sender);
        void __fastcall ReaderFStrengthUpDownChangingEx(TObject *Sender,
          bool &AllowChange, short NewValue, TUpDownDirection Direction);
        void __fastcall ReaderIncreaseTXBitBtnClick(TObject *Sender);
        void __fastcall ReaderDecreaseTXBitBtnClick(TObject *Sender);
        void __fastcall FormClick(TObject *Sender);
        void __fastcall FGenConfigWaitTimeHourRadioButtonMouseDown(
          TObject *Sender, TMouseButton Button, TShiftState Shift, int X,
          int Y);
        void __fastcall FGenConfigWaitTimeMinRadioButtonMouseDown(
          TObject *Sender, TMouseButton Button, TShiftState Shift, int X,
          int Y);
        void __fastcall FGenConfigWaitTimeSecRadioButtonMouseDown(
          TObject *Sender, TMouseButton Button, TShiftState Shift, int X,
          int Y);
        void __fastcall ConfigTagLEDStaticTextClick(TObject *Sender);
        void __fastcall ConfigTagLEDBitBtnClick(TObject *Sender);
        void __fastcall ConfigTagLEDNumCycleComboBoxChange(
          TObject *Sender);
        void __fastcall ConfigTagLEDTagIDRadioButtonClick(TObject *Sender);
        void __fastcall ConfigTagLEDAnyTagIDRadioButtonClick(
          TObject *Sender);
        void __fastcall ConfigTagLEDGetBitBtnClick(TObject *Sender);
        void __fastcall ConfigTagLEDEnableCheckBoxMouseDown(
          TObject *Sender, TMouseButton Button, TShiftState Shift, int X,
          int Y);
        void __fastcall ConfigTagLEDBroadcastRdrCheckBoxClick(
          TObject *Sender);
        void __fastcall DownloadRdrStaticTextClick(TObject *Sender);
        void __fastcall DownloadRdrGetFileBitBtnClick(TObject *Sender);
        void __fastcall DownloadRdrProcessCRadioButtonClick(
          TObject *Sender);
        void __fastcall DownloadRdrBitBtnClick(TObject *Sender);
        void __fastcall DownloadRdrBootQueryBitBtnClick(TObject *Sender);
        void __fastcall DownloadRdrProcessDRadioButtonClick(
          TObject *Sender);
        void __fastcall DownloadRdrProcessERadioButtonClick(
          TObject *Sender);
        void __fastcall TimeTimer1Timer(TObject *Sender);
        void __fastcall EncryptPCRadioButtonClick(TObject *Sender);
        void __fastcall EncryptPDRadioButtonClick(TObject *Sender);
        void __fastcall EncryptPERadioButtonClick(TObject *Sender);
        void __fastcall EncryptPJRadioButtonClick(TObject *Sender);
        void __fastcall EncryptPKRadioButtonClick(TObject *Sender);
        void __fastcall EncryptGetFileBitBtnClick(TObject *Sender);
        void __fastcall EncryptClearBitBtnClick(TObject *Sender);
        void __fastcall EncryptToolButtonClick(TObject *Sender);
        void __fastcall EncryptBitBtnClick(TObject *Sender);
        void __fastcall DownloadSmartFGenStaticTextClick(TObject *Sender);
        void __fastcall DownloadSFGenGetFileBitBtnClick(TObject *Sender);
        void __fastcall DownloadSFGenBitBtnClick(TObject *Sender);
        void __fastcall AssignReaderModifyTXCheckBoxClick(TObject *Sender);
        void __fastcall AssignReaderModifyWTCheckBoxClick(TObject *Sender);
        void __fastcall AssignReaderModifyMDCheckBoxClick(TObject *Sender);
        void __fastcall AssignReaderGetConfigBitBtnClick(TObject *Sender);
        void __fastcall AssignReaderListViewDblClick(TObject *Sender);
        void __fastcall AssignReaderSetConfigBitBtnClick(TObject *Sender);
        void __fastcall CallTagIDRadioButtonClick(TObject *Sender);
        void __fastcall BootloadTimerTimer(TObject *Sender);
        void __fastcall DownloadRdrTestCheckBoxClick(TObject *Sender);
        void __fastcall QueryTagClearListButtonClick(TObject *Sender);
        void __fastcall QueryTagListViewColumnClick(TObject *Sender,
          TListColumn *Column);
        void __fastcall QueryTagListViewCompare(TObject *Sender,
          TListItem *Item1, TListItem *Item2, int Data, int &Compare);
        void __fastcall QueryTagListViewCustomDrawItem(
          TCustomListView *Sender, TListItem *Item, TCustomDrawState State,
          bool &DefaultDraw);
        void __fastcall EnableRelay1RadioButtonClick(TObject *Sender);
        void __fastcall DisableRelay1RadioButtonClick(TObject *Sender);
        void __fastcall EnableRelay2RadioButtonClick(TObject *Sender);
        void __fastcall DisableRelay2RadioButtonClick(TObject *Sender);
        void __fastcall ConfigLEDRadioButtonClick(TObject *Sender);
        void __fastcall ConfigLEDSpeakerRadioButtonClick(TObject *Sender);
        void __fastcall ConfigTagSpeakerGetBitBtnClick(TObject *Sender);
        void __fastcall AssignReaderIDComboBoxChange(TObject *Sender);
        void __fastcall AssignReaderBroadcastReaderCheckBoxClick(
          TObject *Sender);
        void __fastcall RelayBroadcastRdrCheckBoxClick(TObject *Sender);
        void __fastcall InputBroadCastCheckBoxClick(TObject *Sender);
        void __fastcall EnableTagBroadcastRdrCheckBoxClick(
          TObject *Sender);
        void __fastcall DisableTagBroadcastRdrCheckBoxClick(
          TObject *Sender);
        void __fastcall QueryTagBroadcastRdrCheckBoxClick(TObject *Sender);
        void __fastcall CallTagBroadcastRdrCheckBoxClick(TObject *Sender);
        void __fastcall ReadMemoryBroadcastRdrCheckBoxClick(
          TObject *Sender);
        void __fastcall ConfigTagLEDBroadcastReaderCheckBoxClick(
          TObject *Sender);
        void __fastcall SmartFGenBroadcastAllRdrCheckBoxClick(
          TObject *Sender);
        void __fastcall TagTempBroadcastRdrCheckBoxClick(TObject *Sender);
        void __fastcall FGenResetBroadcastRdrCheckBoxClick(
          TObject *Sender);
        void __fastcall FGenResetBroadcastSmartFGenCheckBoxClick(
          TObject *Sender);
        void __fastcall ConfigTagSpeakerClearListBitBtnClick(
          TObject *Sender);
        void __fastcall ConfigTagLEDClearListBitBtnClick(TObject *Sender);
        void __fastcall ConfigTagSpeakerListViewColumnClick(
          TObject *Sender, TListColumn *Column);
        void __fastcall ConfigTagSpeakerListViewCompare(TObject *Sender,
          TListItem *Item1, TListItem *Item2, int Data, int &Compare);
        void __fastcall ConfigTagLEDListViewCompare(TObject *Sender,
          TListItem *Item1, TListItem *Item2, int Data, int &Compare);
        void __fastcall ConfigTagLEDListViewColumnClick(TObject *Sender,
          TListColumn *Column);
        void __fastcall ConfigTagSpeakerListViewCustomDrawItem(
          TCustomListView *Sender, TListItem *Item, TCustomDrawState State,
          bool &DefaultDraw);
        void __fastcall ConfigTagLEDListViewCustomDrawItem(
          TCustomListView *Sender, TListItem *Item, TCustomDrawState State,
          bool &DefaultDraw);
        void __fastcall ConfigTagSpeakerListViewDblClick(TObject *Sender);
        void __fastcall ConfigTagLEDListViewDblClick(TObject *Sender);
        void __fastcall FGenConfigTagRdrIDModifyCheckBoxClick(
          TObject *Sender);
        void __fastcall Input1ModifyCheckBoxClick(TObject *Sender);
        void __fastcall Input2ModifyCheckBoxClick(TObject *Sender);
        void __fastcall EnableTagIDRangeRadioButtonClick(TObject *Sender);
        void __fastcall EnableTagIDRangeComboBoxExit(TObject *Sender);
        void __fastcall DisableTagIDRangeRadioButtonClick(TObject *Sender);
        void __fastcall CallTagIDRangeRadioButtonClick(TObject *Sender);
        void __fastcall CallTagIDRangeComboBoxExit(TObject *Sender);
        void __fastcall QueryTagIDRangeRadioButtonClick(TObject *Sender);
        void __fastcall ConfigTagIDRangeRadioButtonClick(
          TObject *Sender);
        void __fastcall ConfigTagIDRangeComboBoxExit(TObject *Sender);
        void __fastcall QueryTagIDRangeComboBoxExit(TObject *Sender);
        void __fastcall TagTempTagIDRangeComboBoxExit(TObject *Sender);
        void __fastcall TagTempTagIDRangeRadioButtonClick(TObject *Sender);
        void __fastcall ReadMemoryTagIDRangeComboBoxExit(TObject *Sender);
        void __fastcall ReadMemoryTagIDRangeRadioButtonClick(
          TObject *Sender);
        void __fastcall WriteMemoryTagIDRangeRadioButtonClick(
          TObject *Sender);
        void __fastcall WriteMemoryTagIDRangeComboBoxExit(TObject *Sender);
        void __fastcall AssignTagRdrTagIDRadioButtonClick(TObject *Sender);
        void __fastcall AssignTagRdrTagIDRangeRadioButtonClick(
          TObject *Sender);
        void __fastcall AssignTagRdrAnyTagIDRadioButtonClick(
          TObject *Sender);
        void __fastcall AssignTagRdrTagIDRangeComboBoxExit(
          TObject *Sender);
        void __fastcall ConfigTagRNDTagIDRangeComboBoxExit(
          TObject *Sender);
        void __fastcall ConfigTagRNDTagIDRadioButtonClick(TObject *Sender);
        void __fastcall ConfigTagRNDTagIDRangeRadioButtonClick(
          TObject *Sender);
        void __fastcall ConfigTagRNDAnyTagIDRadioButtonClick(
          TObject *Sender);
        void __fastcall ConfigTagLEDTagIDRangeComboBoxExit(
          TObject *Sender);
        void __fastcall ConfigTagLEDTagIDRangeRadioButtonClick(
          TObject *Sender);
    void __fastcall GeneralTimerTimer(TObject *Sender);
    void __fastcall LargeDataCheckBoxClick(TObject *Sender);
    void __fastcall WriteTimerTimer(TObject *Sender);
    void __fastcall ReadTimerTimer(TObject *Sender);
    void __fastcall RetryTimerTimer(TObject *Sender);
        void __fastcall ReadTagLargeDataCheckBoxClick(TObject *Sender);
        void __fastcall ReadLargDataTimerTimer(TObject *Sender);
        void __fastcall ReadMemLargeDataStopBitBtnClick(TObject *Sender);
        void __fastcall TagTempLoggingCheckBoxClick(TObject *Sender);
        void __fastcall TagTempLoggingCheckBoxMouseDown(TObject *Sender,
          TMouseButton Button, TShiftState Shift, int X, int Y);
        void __fastcall FGenConfigWaitTimeCheckBoxClick(TObject *Sender);
        void __fastcall FGenConfigHoldTimeCheckBoxClick(TObject *Sender);
        void __fastcall TagTempTimeStampBitBtnClick(TObject *Sender);
        //void __fastcall ReaderOffLineTimerTimer(TObject *Sender);
        //void __fastcall ChinaTimer1Timer(TObject *Sender);
        //void __fastcall ChinaTimer2Timer(TObject *Sender);
private:	// User declarations
public:		// User declarations
        __fastcall TProgStationForm(TComponent* Owner);
        bool __fastcall OpenSerial(UINT port, UINT baud);
        bool __fastcall InitComPort(int port, int baud);
        int __fastcall OnCommNotify();
        void __fastcall PacketParser(int len, const int sockIndex);
        void __fastcall DisplayRecPackets(char buf[260], int len, bool frameFlag, bool crcFlag, AnsiString ip);
        bool __fastcall WriteRS232Comm(unsigned int Command, int len, unsigned char* buf, unsigned int pktID);
        bool __fastcall WriteTCPIPComm(unsigned int Command, int Len, unsigned char* buf, unsigned int pktID, SOCKET activeSocket, sockaddr_in* activePeer, int encrypIndx);
        void __fastcall ClosePort();
        bool __fastcall CheckCRC(unsigned int PktLen, Byte* buf);
        void __fastcall Generate_CRC(char c);
        int __fastcall BuildPacket(unsigned int Command, unsigned int Len, unsigned char* buf, unsigned int pktID, int* reader);
        void __fastcall DisplayTransmitPackets(char buf[64], int len, AnsiString ip);
        int __fastcall AsciiToInteger(char *c, int n);
        bool __fastcall LoadProgramByteAndTagID();
        unsigned int comPort;
        unsigned int baudRate;
        unsigned int lastBaudrate;
        unsigned int curBaudrate;
        unsigned int dataBits;
        unsigned int stopBits;
        unsigned int parity;
        unsigned int FlowCtrl;
        UINT LastCommand;
        COMMTIMEOUTS oldTimeouts;
        bool portClosed;
        String TxCommandStr;
        //String comStatusStr;
        bool packetOK;

        //Tag type definition
        //========================
        AnsiString tagTypes[7];
        AnsiString tagTypesAbr[7];
        unsigned short numTagTypes;
        bool tagTypesUpdated;

        //read large Data Params
        //========================
        bool gotFF;
        int startAddress;
        int lastNumReadLargeData;
        int MaxReadLargeData;
        int numReadLargeData;
        int readLargeDataCounter;
        unsigned char rPktID;
        unsigned char lastRPktID;
        int noResponseCounter;
        int readBufIndex;
        bool displayElapsedTime;
        TTagReadLargDataForm* largeDataDlg;

        //bool comPortOK;
        bool comPanelRect;
        TRect comLEDRect;
        bool LEDon;
        int recNum;
        bool Activated;
        int index;
        bool PortOpen;
        bool HexFormat;
        //unsigned int RecCounter;
        unsigned int txRxCounter;
        bool StopTX;
        bool StopRX;
        char ProgramByte;
        int TimePeriod[3];
        int NewTagID[4];
        bool TagTypeAckQue[4]; //index 0=acc 1=ast 2=inv 3=car
        bool ProgammingMode;
        AnsiString TagIDStr;
        bool DecButtonClicked;
        bool HexButtonClicked;
        int __fastcall TProgStationForm::GetTimePeriodUnit();
        int __fastcall TProgStationForm::GetTimePeriod();
        int __fastcall HexToInt(char * buf, int size);
        void __fastcall TxClearTagCtrls();
        void __fastcall RxClearTagCtrls();
        void __fastcall FactoryIDCtrlEnabled(bool b);
        int __fastcall GetTimeTIF();
        int __fastcall GetTimeTIFComboBox();
        int __fastcall GetGC();
        int __fastcall GetGCComboBox();
        LastProgTagStruct LastProgTag;
        short int timerCount;
        bool MultiTagType;
        bool displayRx;
        bool displayTx;
        AnsiString progStr;
        AnsiString tagTypeStr;
        bool programming;
        bool resetPowerup;
        FILE* fileHandle;
        bool recording;
        AnsiString recDebugStr;
        AnsiString txDebugStr;
        AnsiString recDisplayStr;
        unsigned int lineCounter;
        bool tagIDMouseDown;
        bool prevIDMouseDown;
        bool factoryIDMouseDown;
        tagDetectedArrayStruct tagDetectedArray[MAX_TAG_DETECTED];
        tagDetectedArrayStruct tagTempArray[MAX_TAG_DETECTED];
        tagArrayStruct tagArray[MAX_TAG];
        tagArrayStruct enabledTagArray[MAX_TAG];
        tagArrayStruct disabledTagArray[MAX_TAG];
        unsigned short tagDetectCount;
        unsigned short tagTempCount;
        /*unsigned short accTagCount;
        unsigned short assTagCount;
        unsigned short invTagCount;
        unsigned short carTagCount;*/

        unsigned short type1Count;
        unsigned short type2Count;
        unsigned short type3Count;
        unsigned short type4Count;
        unsigned short type5Count;
        unsigned short type6Count;
        unsigned short typeFacCount;
        AnsiString selectedType;
        AnsiString selectedId;
        int columnToSort;
        bool alreadyDisplayed;
        bool alreadyDisplayed1;
        unsigned int lastReaderID;
        unsigned int lastHostID;
        bool readerOffLine;
        bool readerOnLine;
        unsigned short tgType;
        bool global;
        unsigned short int lastLenBytesSent;
        bool newListItem;
        unsigned int arrayIndex;
        unsigned int nDuplicates;
        void __fastcall DisplayStatusByte(int len, unsigned int cmd);
        void __fastcall ClearTimer3Que();
        void __fastcall BuildCRC(int index);
        void __fastcall SendMultiTagTypePkt();
        void __fastcall TransmiLastPacket(int Len);
        void __fastcall UpdateTimer3Que();
        void __fastcall EnableAllConfigCtrls(bool b);
        bool __fastcall IsMSecDataValid(AnsiString str);
        bool __fastcall IsSecDataValid(AnsiString str);
        bool __fastcall IsMinDataValid(AnsiString str);
        bool __fastcall IsHourDataValid(AnsiString str);
        AnsiString __fastcall BuildRecPktString(char* buf, int len, bool frameFlag, bool crcFlag);
        AnsiString __fastcall BuildTxRecordPktStr(char* buf, int len);
        void __fastcall DisplayTagInListView(unsigned int index, unsigned char fGenID, bool gID, bool* fGenFlag, bool* gIDFlag, unsigned short rssi, AnsiString Stat);
        void __fastcall DisplayTagInListView(unsigned int id, unsigned short type);
        //bool __fastcall CheckSameTag(unsigned int id, unsigned short type, unsigned char fGenID, bool gID, bool* fgenFlag, bool* gIDFlag, unsigned short* dIndex);
        bool __fastcall CheckDuplicatedTag(unsigned int id, unsigned short type);
        bool __fastcall CheckDuplicatedEnabledTag(unsigned int id, unsigned short type);
        bool __fastcall CheckDuplicatedDisabledTag(unsigned int id, unsigned short type);
        bool __fastcall CheckDuplicatedReader(unsigned int id);
        void __fastcall UpdateTagDetectedArray(unsigned short readerID, unsigned short fGenID, unsigned int tagID, unsigned short tagType, int configIndex);
        bool __fastcall CheckTagTimeInterval(unsigned int id, unsigned short type, int index);
        void __fastcall LoadStatusByteToQue(int index, unsigned int cmd, int queIndex);
        AnsiString __fastcall GetItem(TStrings* str, int itemNum);
        void __fastcall doTagID(__int64 n, int *index);
        bool clicked;
        bool modifyComm;
        bool msgDisplayed;
        int PwUpHostAddr;
        int pwUpReaderAddr;
        int pwUplastSFGenAddr;
        unsigned int readerIDList[128];
        int fgenIDList[256];
        unsigned short newRID;
        unsigned short newHID;
        unsigned int numReaderIDList;
        unsigned int numFgenIDList;
        unsigned int numSFgenIDList;
        unsigned short eTagType01Count;
        unsigned short eTagType02Count;
        unsigned short eTagType03Count;
        unsigned short eTagType04Count;
        unsigned short eTagType05Count;
        unsigned short eTagType06Count;
        unsigned short eTagFactCount;
        unsigned short eTagTotalCount;
        unsigned short arrayIndexEnabled;
        unsigned short dTagType01Count;
        unsigned short dTagType02Count;
        unsigned short dTagType03Count;
        unsigned short dTagType04Count;
        unsigned short dTagType05Count;
        unsigned short dTagType06Count;
        //unsigned short dTagAccCount;
        //unsigned short dTagAssCount;
        //unsigned short dTagInvCount;
        unsigned short dTagFactCount;
        unsigned short dTagTotalCount;
        unsigned short arrayIndexDisabled;
        bool receivedResponse;
        bool uninitReader;
        bool waitingForRespone;
        bool waitResetRdrACK;
        bool displayed;
        DWORD sendTime;
        unsigned short int assignedTagRdrID;
        bool fGenRedDisplay;
        bool readerFgenButtonActivated;
        AnsiString newFGID;
        void __fastcall DisplayReadersInViewList(AnsiString type, AnsiString id);
        void __fastcall ClearListView();
        void __fastcall DisplayQueryFGenListView(int ID, int tType, int tTime, int wType, int wTime, int rdrID, AnsiString tagType, AnsiString tagID, bool PIR, bool high, int hTime, int motionSens
        );
        void __fastcall DisplayConfigRdrListView(int rID, int hID, int tTime, int wType, int wTime, bool mdEnable, bool high, int dpot);
        void __fastcall DisplayQueryReadersInViewList(AnsiString id, AnsiString type, bool broadcast, bool Enable, bool RSSI);
        void __fastcall GotoConfigFGenPage();
        void __fastcall EnableAllCommands(bool b);
        bool FGenConfigWaitTimeSecClicked;
        bool FGenConfigWaitTimeMinClicked;
        bool FGenConfigWaitTimeHourClicked;
        bool SFGenConfigWaitTimeSecClicked;
        bool SFGenConfigWaitTimeMinClicked;
        bool SFGenConfigWaitTimeHourClicked;
        SOCKET __fastcall OpenSocket(char * hostname, int port, int type, struct sockaddr_in * peer);
        //bool __fastcall ConnectToNetwork(char * hostname, int port, int type, struct sockaddr_in * peer);
        //bool __fastcall ConnectToNetwork(int port);
        void __fastcall CloseSocket();
        //int __fastcall main_loop(LTX_SOCKET sock, struct sockaddr_in * from, int encryption);
        int __fastcall main_loop(int encryption);
        int __fastcall ReadSocket(SOCKET sock, char * rbuf, int len, struct sockaddr_in * from, int sockIndex);
        int __fastcall ClearReadSocket(SOCKET sock, struct sockaddr_in * from, int loop);
        int __fastcall WriteSocket(SOCKET sock, char * sbuf, int len, struct sockaddr_in * to);
        AnsiString hostName;
        int portID;
        AnsiString lastHostName;
        AnsiString lastComm;
        int lastPortID;
        AnsiString lastIPAddress;
        AnsiString lastCommType;
        //bool RS232On;
        //bool networkOn;
        AnsiString version;
        AnsiString timeStr;
        AnsiString hostIDStr;
        AnsiString item1Str;
        AnsiString sysStr;
        AnsiString temp;
        //AnsiString comConnectStr;
        bool justStartedUp[MAX_DESCRIPTOR];
        int sysHostID;
        WriteThread *txWriteThread;
        int pktLenToTransmit;
        bool allHostID;
        bool displayLowBattery;
        unsigned int pktCommandToTransmit;

        void __fastcall serial_write(char * plaintext, int len);
        int __fastcall serial_read(char * plaintext, int len, int command);
        void __fastcall CloseNetworkConnection();
        void __fastcall DisplayDumpedPackets(char buf[1002], int len);
        void __fastcall DisplayTxInfo();
        bool __fastcall CheckHostID();
        void __fastcall UpdateNewHostIDFields(int id);
        //int numIpSelected;
        //unsigned short int numOpenedSocket;
        AnsiString ipAddrs[10];
        int numIps;
        AnsiString writeFormat;
        AnsiString readFormat;
        bool mouseClicked;
        unsigned char tagTempPeriodByte;
        unsigned int tagTempCtrlByte;
        float tagTempHiLimit;
        float tagTempLoLimit;
        float tagTemperature;
        bool tempDisplayed;
        int __fastcall GetIpAddressIndex(int reader);
        bool __fastcall CheckSameTag(unsigned int id, unsigned short type, unsigned char fGenID, bool gID, bool* fgenFlag, bool* gIDFlag, unsigned short* dIndex, unsigned short rdr, unsigned int cmd, unsigned short rssi);
        void __fastcall UpdateTemperatureScreen();
        void __fastcall DisplayTagTemperature(unsigned char status);
        void __fastcall DisplayTagTempInListView(unsigned int tagID, float temp, unsigned char status, char type, unsigned int rdr);
        bool __fastcall CheckSameTagTemp(unsigned int id, unsigned short type, bool gID, bool* gIDFlag, unsigned short* dIndex, unsigned int rdr);
        void __fastcall UpdateTagTempArray(unsigned short readerID, unsigned int tagID, unsigned short tagType, int configIndex);
        DWORD __fastcall GetNowTimeValue();
        bool readTemp;
        unsigned short int lastFieldGenID;
        unsigned short int lastSmartFieldGenID;
        bool fieldGenOffLine;
        bool fieldGenOnLine;
        unsigned short fgenTagtype;
        //float tagTempCalib;
        float tagTempCalibC;
        float tagTempCalibF;
        float tagTempCalibDiff;
        AnsiString __fastcall GetDate(unsigned char day, unsigned char my);
        void __fastcall SaveTagTempCalibToTag();
        void __fastcall UpdateTagTempScreen();
        //int __fastcall ScanNetwork(LTX_SOCKET sock, cbxentry ** cbxlist, unsigned long oem_id);
        //long __fastcall interface_up(LTX_SOCKET sock, struct sockaddr_in *peerAddr, LTX_INTERFACE_INFO *ifo);
        bool __fastcall GetAllReadersIPAddr();
        int __fastcall GetIpAddressIndex(AnsiString ip);
        //struct networkInfoStruct *networkInfo; //[MAX_DESCRIPTOR];
        HINSTANCE hDLL;
        bool first;
        unsigned short int numItems;
        unsigned int numSockPoll;
        unsigned int numSockConnected;
        //int listViewItemCount;
        unsigned int sockQueIndex;
        unsigned int sockConnectQueIndex;
        unsigned int numSelectedSockConnect;
        bool updateIpList;
        bool ConnectingSockets;
        sockConnectQueStruct sockConnectQue[MAX_DESCRIPTOR];
        //AnsiString sockPollQue[MAX_DESCRIPTOR];
        sockPollQueStruct sockPollQue[MAX_DESCRIPTOR];
        int __fastcall TCPEncrypt(char* encryText, int len, char* plaintext, int method, int index);
        int __fastcall TCPDecrypt(char* encryText, int len, char*  plaintext, int method, int index);
        void __fastcall DisplayRelayListView(int rID, int relID, bool enable);
        void __fastcall DisplayInputListView(unsigned short rdr, unsigned char conByte);
        void __fastcall EnableAllStaticTextCommands();
        void __fastcall DisableAllStaticTextCommands();
        AnsiString __fastcall GetMonth(unsigned short m);
        unsigned short __fastcall GetSFGenConfigCount();
        void __fastcall CloseSingleSocket(int index);
        int __fastcall GetSockConnectQueIndex(AnsiString ip);
        int __fastcall GetSockPollQueIndex(AnsiString ip);
        //ListViewInfoStruct listViewInfo[MAX_DESCRIPTOR];  //jul 27, 06
        ListViewInfoStruct* listViewInfo;
        int maxConnectRetry;
        int broadcastNum;
        bool __fastcall BuildTxSockets(unsigned int Command, int Len, unsigned char* buf, unsigned int pktID, AnsiString ip);
        bool __fastcall WriteSockets(int index);
        int __fastcall GetSockPollQueIndex(int reader);
        bool __fastcall ConnectSocket(AnsiString ip);
        bool __fastcall DisconnectSocket(AnsiString ip);
        int __fastcall GetSocketIndex(AnsiString ip);
        bool __fastcall WriteAWSocket(unsigned int Command, int Len, unsigned char* buf, unsigned int pktID, char type, unsigned int selectList[MAX_DESCRIPTOR], int numEntry, int sockIndex);
        int __fastcall GetSocketIndex(int reader);
        int __fastcall GetBroadcastNum();
        void __fastcall StartBootLoader();
        void __fastcall imbed_the_proc(char *str);
        void __fastcall open_hex_file(void);
        void __fastcall OnClrBtn();
        void __fastcall build_diag_string(char *src, UINT len, UINT rcvd);
        void __fastcall fill_image_buf(void);
        void __fastcall send_a_bload_cmd(void);
        UINT __fastcall convert_image2_packet(void);
        void __fastcall add2_send_queue(UINT count);
        UINT __fastcall convert_record2_packet(UINT byte_len);
        UINT __fastcall bl_packet_prelims(UINT bpp_len);
        void finish_the_packet(UINT *x, bool pid, bool add2q);
        bool __fastcall WriteToRS232Port(unsigned int bytes);
        void __fastcall DisplayQueryBootData(UINT proc);
        bool __fastcall IsDecryptFileOK(UINT p);
        bool __fastcall open_ctrl_file(void);
        bool bootloading;
        bool sendBootQuery;
        void __fastcall EncryptHexFile(unsigned short pType, AnsiString fName, int ver);
        bool __fastcall DecryptHexFile(unsigned short pType, AnsiString fName);
        void Hex2Char(char const* szHex, unsigned char& rch);
        void CharStr2HexStr(unsigned char const* pucCharStr, char* pszHexStr, int iSize);
        void Char2Hex(unsigned char ch, char* szHex);
        void HexStr2CharStr(char const* pszHexStr, unsigned char* pucCharStr, int iSize);
        AnsiString __fastcall IsEncryptFileOK(UINT p);
        void __fastcall DisableSmartFGenStaticCommands();
        void __fastcall DisableStdFGenStaticCommands();
        void __fastcall DisableReaderStaticCommands();
        void __fastcall DisableTagStaticCommands();
        void __fastcall EnableSmartFGenStaticCommands();
        void __fastcall EnableStdFGenStaticCommands();
        void __fastcall EnableReaderStaticCommands();
        void __fastcall EnableTagStaticCommands();
        void __fastcall ShowSmartFGenStaticCommands();
        void __fastcall ShowStdFGenStaticCommands();
        void __fastcall ShowReaderStaticCommands();
        void __fastcall ShowTagStaticCommands();
        void __fastcall HideAllCommandBtns();
        void __fastcall EnableAllBitbtnCommands();
        bool getRdrConfig;
        void __fastcall DisplayTagInfo(unsigned int id, int index);
        bool __fastcall IsItemInList(unsigned int item);
        unsigned short listCount;
        void __fastcall ChangeConfigReaderSTDControls(bool b);
        bool __fastcall IsInSpkList(unsigned int id, AnsiString type);
        bool __fastcall IsInLEDList(unsigned int id, AnsiString type);
        bool rdrConfigFlag;
        bool fGenConfigFlag;
        bool rdrConfigTxFlag;
        unsigned short stdFGenType;
        unsigned short spkListCount;
        unsigned short ledListCount;
        bool __fastcall IsInRange(unsigned short range);
        unsigned char __fastcall GetRangeIndex(unsigned short range);
        bool __fastcall CheckValidWrite(int len);
        void __fastcall UpdateTagTypesComboBox();
        bool __fastcall CheckVaildTagType(AnsiString type);
        bool __fastcall CheckVaildTagTypeAbr(AnsiString type);
        int __fastcall GetTagTypeValue(int index);
        int __fastcall GetTagTypeComboBoxIndex(unsigned char byte);
        int __fastcall GetIndexTagTypeAbr(AnsiString type);
        int __fastcall GetIndexTagType(AnsiString type);
        int __fastcall GetTagTypesIndex(unsigned char byte);
        int __fastcall GetTagTypeComboBoxIndex(AnsiString abr);
        int retry;
        int __fastcall GetTagTypeComboBoxIndex(int typeVal);
        int __fastcall GetNetworkInfoIndex(int reader);
        int __fastcall IsReaderOffline(int rdr);
        bool __fastcall IsReaderOffline();
        bool __fastcall IsAllReaderOffline();
        void __fastcall InitializeListViewInfo();
        void __fastcall DisconnectAllSockets();
        void __fastcall InitializeAWSockets();
        int __fastcall GetListViewInfoIndex(int reader);
        int __fastcall nBitTwosComplement(int data, int numBits);
        int totByteWritten;
        unsigned char lastWritePktID;
        unsigned char lastReadPktID;
        unsigned char writePktID;
        bool writeTagData;
        int callingReaderID;
        int lastRdrOfflineIndex;
        bool rdrWentOffline;
        int noResponseReaderID[128];
        bool powerupQueryFGen;

        //China Demo-------------------------
        /*TChinaDemoForm* chinaDemoDlg;
        bool closeChinaDemo;
        bool chinaDemoON;
        bool relay1On;
        bool relay2On;
        short int chinaTagIndex;
        void __fastcall ChinaDemoCallTag();
        unsigned int timeCheckTags;
        unsigned int timeSendStatus;
        void __fastcall ClearForGlobalChina();*/
        //------------------------------------
};
//---------------------------------------------------------------------------
extern PACKAGE TProgStationForm *ProgStationForm;
//---------------------------------------------------------------------------
#endif
