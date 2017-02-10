//---------------------------------------------------------------------------

#ifndef ComConfigPageH
#define ComConfigPageH
//---------------------------------------------------------------------------
#include <Classes.hpp>
#include <Controls.hpp>
#include <StdCtrls.hpp>
#include <Forms.hpp>
#include <Buttons.hpp>
#include <ComCtrls.hpp>
#include <ExtCtrls.hpp>

#include "ScanUnit.h"
#include "TCPIPUnit.h"
#include <ActnList.hpp>

struct activeIPsStruct
{
   AnsiString ip;
   int reader;
   int host;
   bool selected;
   bool active;
};

//---------------------------------------------------------------------------
class TCommConfigDlg : public TForm
{
__published:	// IDE-managed Components
        TBitBtn *cancelBtn;
        TPageControl *CommPageControl;
        TTabSheet *RS232TabSheet;
        TGroupBox *GroupBox1;
        TLabel *Label1;
        TLabel *Label2;
        TLabel *Label3;
        TLabel *Label4;
        TLabel *Label5;
        TLabel *Label6;
        TComboBox *BaudRateComboBox;
        TComboBox *DataBitsComboBox;
        TComboBox *ParityComboBox;
        TComboBox *StopBitsComboBox;
        TComboBox *FlowCtrlComboBox;
        TComboBox *CommPortComboBox;
        TGroupBox *GroupBox2;
        TRadioButton *RS232RadioButton;
        TRadioButton *TCPIPRadioButton;
        TLabel *Label12;
        TTabSheet *TCPIPTabSheet;
        TLabel *Label7;
        TEdit *ComPortIDEdit;
        TListView *IPListView;
        TBitBtn *ClearListBitBtn;
        TBitBtn *SearchIPBitBtn;
        TLabel *Label8;
        TGroupBox *GroupBox3;
        TBitBtn *AssignIPBitBtn;
        TLabel *Label9;
        TEdit *IPEdit1;
        TEdit *IPEdit2;
        TEdit *IPEdit3;
        TEdit *IPEdit4;
        TBitBtn *ResetReadersBitBtn;
        TCheckBox *SelectAllCheckBox;
        TGroupBox *GroupBox4;
        TRadioButton *NoEncryptRadioButton;
        TRadioButton *RijndaelEncryptRadioButton;
        TLabel *ConnectLabel;
        TBitBtn *ConnectBitBtn;
        TBitBtn *DisconnectBitBtn;
        TCheckBox *KeepListItemCheckBox;
        TBitBtn *RemoveIPBitBtn;
        TGroupBox *GroupBox5;
        TRadioButton *UseSearchIPRadioButton;
        TRadioButton *UseSpecificIPRadioButton;
        TEdit *IPSpecEdit1;
        TEdit *IPSpecEdit2;
        TEdit *IPSpecEdit3;
        TEdit *IPSpecEdit4;
        TTimer *ConnectTimer;
        TLabel *Msg;
        TBitBtn *AddIPBitBtn;
        void __fastcall cancelBtnClick(TObject *Sender);
        void __fastcall BaudRateComboBoxChange(TObject *Sender);
        void __fastcall DataBitsComboBoxChange(TObject *Sender);
        void __fastcall ParityComboBoxChange(TObject *Sender);
        void __fastcall StopBitsComboBoxChange(TObject *Sender);
        void __fastcall ConnectBitBtnClick(TObject *Sender);
        void __fastcall RS232RadioButtonClick(TObject *Sender);
        void __fastcall TCPIPRadioButtonClick(TObject *Sender);
        void __fastcall CommPageControlChange(TObject *Sender);
        void __fastcall FormActivate(TObject *Sender);
        void __fastcall FormClose(TObject *Sender, TCloseAction &Action);
        void __fastcall SearchIPBitBtnClick(TObject *Sender);
        void __fastcall ClearListBitBtnClick(TObject *Sender);
        void __fastcall IPListViewDblClick(TObject *Sender);
        void __fastcall ResetReadersBitBtnClick(TObject *Sender);
        void __fastcall IPListViewCustomDrawItem(TCustomListView *Sender,
          TListItem *Item, TCustomDrawState State, bool &DefaultDraw);
        void __fastcall SelectAllCheckBoxClick(TObject *Sender);
        void __fastcall AssignIPBitBtnClick(TObject *Sender);
        void __fastcall IPListViewClick(TObject *Sender);
        void __fastcall RijndaelEncryptRadioButtonClick(TObject *Sender);
        void __fastcall NoEncryptRadioButtonClick(TObject *Sender);
        void __fastcall IPListViewColumnClick(TObject *Sender,
          TListColumn *Column);
        void __fastcall IPListViewCompare(TObject *Sender,
          TListItem *Item1, TListItem *Item2, int Data, int &Compare);
        void __fastcall UseSearchIPRadioButtonClick(TObject *Sender);
        void __fastcall UseSpecificIPRadioButtonClick(TObject *Sender);
        void __fastcall DisconnectBitBtnClick(TObject *Sender);
        void __fastcall InitNetworkBitBtnClick(TObject *Sender);
        void __fastcall ConnectTimerTimer(TObject *Sender);
        void __fastcall AddIPBitBtnClick(TObject *Sender);
        void __fastcall RemoveIPBitBtnClick(TObject *Sender);
private:	// User declarations
public:		// User declarations
        unsigned int comPort;
        unsigned int baudRate;
        unsigned int dataBits;
        unsigned int stopBits;
        unsigned int parity;
        unsigned int FlowCtrl;
        bool PortOpen;
        TScanForm* sacnForm;
        __fastcall TCommConfigDlg(TComponent* Owner);
        AnsiString __fastcall GetIpAddress(short n1, short n2, short n3, short n4, short index);
        int __fastcall GetIpAddressIndex(int reader);
        void __fastcall AddIpToList(AnsiString s, bool active);
        void __fastcall ClearIPListView();
        AnsiString __fastcall GetItem(TStrings* str, int itemNum);
        void __fastcall UpdateIPListViewPowerup(int indx);
        int __fastcall GetIpAddressIndex(AnsiString ip);
        TTCPIPForm* tcpipForm;
        //void GetActiveIpSeleceted();
        void UpdateSocketStatus(int indx);
        int columnToSort;
        int myListViewItemCount;
        int connectQueEntry;
        bool scanDisconnect;
        void __fastcall AddConnectedIpToActiveList(AnsiString connectedIp, int indx);
        int __fastcall GetActiveIpAddressIndex(AnsiString ip);
        void __fastcall GetAllSelectedItems();
        void __fastcall UpdateIPListView();
};
//---------------------------------------------------------------------------
extern PACKAGE TCommConfigDlg *CommConfigDlg;
//---------------------------------------------------------------------------
#endif
