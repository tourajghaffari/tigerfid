// ReaderPage.cpp : implementation file
//
//---------------------------------------------------------------------------------------
// change history:
//
// [jp_02/24/2010]: changed to use ALL_RS232_READERS instead of GLOBAL_READERS
//---------------------------------------------------------------------------------------

#include "stdafx.h"
#include "TestAPI.h"
#include "ReaderPage.h"
#include "TestAPIDlg.h"
#include "AWI_API.h"
#include ".\readerpage.h"

extern CWnd* listBox;
extern CTestAPIDlg* apiDlg;
extern CReaderPage* readerPage;
extern int pktCounter;
extern int pktID;

// CReaderPage dialog

IMPLEMENT_DYNAMIC(CReaderPage, CDialog)
CReaderPage::CReaderPage(CWnd* pParent /*=NULL*/)
	: CDialog(CReaderPage::IDD, pParent)
{
   readerPage = this;
}

CReaderPage::~CReaderPage()
{
}

void CReaderPage::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
}


BEGIN_MESSAGE_MAP(CReaderPage, CDialog)
	ON_BN_CLICKED(IDC_RADIO_NEW_CONFIG, OnBnClickedRadioNewConfig)
	ON_BN_CLICKED(IDC_RADIO_INPUT, OnBnClickedRadioInput)
	ON_BN_CLICKED(IDC_RADIO_FS, OnBnClickedRadioFs)
	ON_BN_CLICKED(IDC_RADIO_RELAY, OnBnClickedRadioRelay)
	ON_BN_CLICKED(IDC_RADIO_ENABLE_DISABLE, OnBnClickedRadioEnableDisable)
	ON_BN_CLICKED(IDC_BUTTON_RF_REG_READER, OnBnClickedButtonRfRegReader)
	ON_BN_CLICKED(IDC_BUTTON_ENABLE_READER, OnBnClickedButtonEnableReader)
	ON_BN_CLICKED(IDC_BUTTON_ENABLE_RELAY, OnBnClickedButtonEnableRelay)
	ON_BN_CLICKED(IDC_BUTTON_GET_RDR_FS, OnBnClickedButtonGetRdrFs)
	ON_BN_CLICKED(IDC_BUTTON_SET_RDR_FS, OnBnClickedButtonSetRdrFs)
	ON_BN_CLICKED(IDC_BUTTON_GET_INPUT, OnBnClickedButtonGetInput)
	ON_BN_CLICKED(IDC_BUTTON_CONFIG_INPUT_PORT, OnBnClickedButtonConfigInputPort)
    ON_WM_CHILDACTIVATE()
    ON_BN_CLICKED(IDC_CHECK_RDR_CFG_HOST_ID, OnBnClickedCheckRdrCfgHostId)
    ON_BN_CLICKED(IDC_CHECK_RDR_CFG_RDR_ID, OnBnClickedCheckRdrCfgRdrId)
    ON_BN_CLICKED(IDC_BUTTON_CONFIG_NEW_RDR, OnBnClickedButtonConfigNewRdr)
    ON_BN_CLICKED(IDC_BUTTON_GET_READER_VERSION, OnBnClickedButtonGetReaderVersion)
	ON_WM_CLOSE()
END_MESSAGE_MAP()


// CReaderPage message handlers

void CReaderPage::OnBnClickedRadioNewConfig()
{
   EnableNewConfigGroupBox(true);
   EnableInputGroupBox(false);
   EnableFSGroupBox(false);
   EnableRelayGroupBox(false);
   EnableEnableRdrGroupBox(false);
}

void CReaderPage::EnableNewConfigGroupBox(bool b)
{
   ((CWnd *)GetDlgItem(IDC_CHECK_RDR_CFG_HOST_ID))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_CHECK_RDR_CFG_RDR_ID))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_GROUP_NEW_RDR_CONFIG))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_BUTTON_CONFIG_NEW_RDR))->EnableWindow(b);	   
}

void CReaderPage::OnChildActivate()
{
	CDialog::OnChildActivate();

	((CComboBox *)GetDlgItem(IDC_COMB_READER_FUNC_TYPE))->SetCurSel(0);
	SetDlgItemInt(IDC_EDIT_HOST_ID, 1, false);
}

void CReaderPage::OnBnClickedRadioInput()
{
	EnableNewConfigGroupBox(false);
    EnableInputGroupBox(true);
	EnableFSGroupBox(false);
	EnableRelayGroupBox(false);
	EnableEnableRdrGroupBox(false);
}

void CReaderPage::EnableInputGroupBox(bool b)
{
   ((CWnd *)GetDlgItem(IDC_GROUP_INPUT))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_GROUP_INPUT1))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_RADIO_IGNOR_INPUT1))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_RADIO_REPORT_INPUT1))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_RADIO_NO_CHANGE_INPUT1))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_GROUP_INPUT2))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_RADIO_IGNOR_INPUT2))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_RADIO_REPORT_INPUT2))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_RADIO_NO_CHANGE_INPUT2))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_CHECK_SUPERVISED))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_BUTTON_GET_INPUT))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_BUTTON_CONFIG_INPUT_PORT))->EnableWindow(b);
}
void CReaderPage::OnBnClickedRadioFs()
{
	EnableNewConfigGroupBox(false);
    EnableInputGroupBox(false);
	EnableFSGroupBox(true);
	EnableRelayGroupBox(false);
	EnableEnableRdrGroupBox(false);
}

void CReaderPage::EnableFSGroupBox(bool b)
{
   ((CWnd *)GetDlgItem(IDC_GROUP_FS))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_GROUP_VALUE))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_RADIO_RDR_FS_DEC))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_RADIO_RDR_FS_INC))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_RADIO_RDR_FS_ABS))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_EDIT_RDR_FS))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_GROUP_RANGE))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_RADIO_RDR_RANGE_LONG))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_RADIO_RDR_RANGE_SHORT))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_BUTTON_GET_RDR_FS))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_BUTTON_SET_RDR_FS))->EnableWindow(b);
}

void CReaderPage::OnBnClickedRadioRelay()
{
	EnableNewConfigGroupBox(false);
    EnableInputGroupBox(false);
	EnableFSGroupBox(false);
	EnableRelayGroupBox(true);
	EnableEnableRdrGroupBox(false);
}

void CReaderPage::EnableRelayGroupBox(bool b)
{
   ((CWnd *)GetDlgItem(IDC_GROUP_RELAY))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_GROUP_RELAY_ADDR))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_RADIO_RELAY1))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_RADIO_RELAY2))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_GROUP_RELAY_ENABLE))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_RADIO_ENABLE_RELAY))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_RADIO_DISABLE_RELAY))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_BUTTON_ENABLE_RELAY))->EnableWindow(b);
}

void CReaderPage::OnBnClickedRadioEnableDisable()
{
	EnableNewConfigGroupBox(false);
    EnableInputGroupBox(false);
	EnableFSGroupBox(false);
	EnableRelayGroupBox(false);
	EnableEnableRdrGroupBox(true);
}

void CReaderPage::EnableEnableRdrGroupBox(bool b)
{
   ((CWnd *)GetDlgItem(IDC_GROUP_ENABLE_RDR))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_BUTTON_ENABLE_READER))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_RADIO_ENABLE_RDR))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_RADIO_DISABLE_RDR))->EnableWindow(b);
}


void CReaderPage::OnBnClickedCheckRdrCfgHostId()
{
	if (((CButton *)GetDlgItem(IDC_CHECK_RDR_CFG_HOST_ID))->GetCheck() == BST_CHECKED)
	{
        ((CWnd *)GetDlgItem(IDC_EDIT_NEW_HOST_ID))->EnableWindow(true);
	    ((CWnd *)GetDlgItem(IDC_STATIC_01))->EnableWindow(true);
	}
	else
	{
        ((CWnd *)GetDlgItem(IDC_EDIT_NEW_HOST_ID))->EnableWindow(false);
	    ((CWnd *)GetDlgItem(IDC_STATIC_01))->EnableWindow(false);
	}	
}

void CReaderPage::OnBnClickedCheckRdrCfgRdrId()
{
	if (((CButton *)GetDlgItem(IDC_CHECK_RDR_CFG_RDR_ID))->GetCheck() == BST_CHECKED)
	{
        ((CWnd *)GetDlgItem(IDC_EDIT_NEW_READER_ID))->EnableWindow(true);
	    ((CWnd *)GetDlgItem(IDC_STATIC_00))->EnableWindow(true);
	}
	else
	{
        ((CWnd *)GetDlgItem(IDC_EDIT_NEW_READER_ID))->EnableWindow(false);
	    ((CWnd *)GetDlgItem(IDC_STATIC_00))->EnableWindow(false);
	}	
}

void CReaderPage::OnBnClickedButtonRfRegReader()
{
   char buf[64];
   unsigned short fType;

   int rdrID = GetDlgItemInt(IDC_EDIT_READER_ID, NULL,true);
   int hostID = GetDlgItemInt(IDC_EDIT_HOST_ID, NULL,true);
   GetDlgItemText(IDC_COMB_READER_FUNC_TYPE, buf, 50);
   if (strcmp(buf, "SPECIFIC_READER") == 0)
      fType = SPECIFIC_READER;
   else if (strcmp(buf, "ALL_READERS") == 0)
      fType = ALL_READERS;
   else if (strcmp(buf, "ALL_RS232_READERS") == 0)	// [jp_02/24/2010]
      fType = ALL_RS232_READERS;
   else
   {
	  MessageBox("No Reader Function Type", "API", MB_ICONHAND);
      return;
   }
   int ret = rfResetReader(hostID, rdrID, 0, fType, ++pktID);
   apiDlg->Display("rfResetReader()", pktID, ret);
   MessageBeep(0xFFFFFFFF);
}

void CReaderPage::OnBnClickedButtonEnableReader()
{
   char buf[64];
   unsigned short fType;
   bool enable = false;
   CString s;

   int rdrID = GetDlgItemInt(IDC_EDIT_READER_ID, NULL,true);
   int hostID = GetDlgItemInt(IDC_EDIT_HOST_ID, NULL,true);
   GetDlgItemText(IDC_COMB_READER_FUNC_TYPE, buf, 50);
   if (strcmp(buf, "SPECIFIC_READER") == 0)
   {
      fType = SPECIFIC_READER;
	  s = "rfEnableReader()   Reader = ";
	  itoa(rdrID, buf, 10);
	  s += buf;
   }
   else if (strcmp(buf, "ALL_READERS") == 0)
   {
      fType = ALL_READERS;
	  s = "rfEnableReader()  ALL_READERS";
   }
   else
   {
	  MessageBox("No Reader Function Type", "API", MB_ICONHAND);
      return;
   }

   if(((CButton *)GetDlgItem(IDC_RADIO_ENABLE_RDR))->GetCheck() == BST_CHECKED)
	   enable = true;
   else if(((CButton *)GetDlgItem(IDC_RADIO_DISABLE_RDR))->GetCheck() == BST_CHECKED)
       enable = false;
   else
   {
	   MessageBox("Check Enable or Disable Button.", "API", MB_ICONHAND);
       return;
   }

   //------------------------------------------------------------------------------
   // a way to test for rfQueryReader from C++ testAPI
   int ret = rfEnableReader(hostID, rdrID, 0, enable, fType, ++pktID);
   //int ret = rfQueryReader(hostID, rdrID, 0, fType, ++pktID);
   //------------------------------------------------------------------------------

   apiDlg->Display(s, pktID, ret);
   MessageBeep(0xFFFFFFFF);
}

void CReaderPage::OnBnClickedButtonEnableRelay()
{
   char buf[64];
   unsigned short cmdType;
   CString s;

   int rdrID = GetDlgItemInt(IDC_EDIT_READER_ID, NULL,true);
   int hostID = GetDlgItemInt(IDC_EDIT_HOST_ID, NULL,true);
   GetDlgItemText(IDC_COMB_READER_FUNC_TYPE, buf, 50);
   if (strcmp(buf, "SPECIFIC_READER") == 0)
   {
      cmdType = SPECIFIC_READER;
	  s = "rfEnableReader()   Reader = ";
	  itoa(rdrID, buf, 10);
	  s += buf;
   }
   else if (strcmp(buf, "ALL_READERS") == 0)
   {
      cmdType = ALL_READERS;
	  s = "rfEnableReader()  ALL_READERS";
   }
   else
   {
	  MessageBox("No Reader Function Type", "API", MB_ICONHAND);
      return;
   }

   int relayID = 0;
   if (((CButton *)GetDlgItem(IDC_RADIO_RELAY1))->GetCheck() == BST_CHECKED)
	   relayID = 1;
   else if (((CButton *)GetDlgItem(IDC_RADIO_RELAY2))->GetCheck() == BST_CHECKED)
	   relayID = 2;
   else
   {
	   MessageBox("No relay ID", "API", MB_ICONHAND);
	   return;
   }

   bool enable = false;
   if (((CButton *)GetDlgItem(IDC_RADIO_ENABLE_RELAY))->GetCheck() == BST_CHECKED)
	   enable = true;
   else if (((CButton *)GetDlgItem(IDC_RADIO_DISABLE_RELAY))->GetCheck() == BST_CHECKED)
	   enable = false;
   else
   {
	   MessageBox("Check Enable or Disable button.", "API", MB_ICONHAND);
	   return;
   }

   GetDlgItemText(IDC_COMB_READER_FUNC_TYPE, buf, 50);
   if (strcmp(buf, "SPECIFIC_READER") == 0)
   {
      cmdType = SPECIFIC_READER;
	  if (enable)
	  {
	     if (relayID == 0x01)
	        s = "rfEnableRelay()   Relay = 1   Reader = ";
	     else
	        s = "rfEnableRelay()   Relay = 2   Reader = ";
	  }
	  else
	  {
         if (relayID == 0x01)
	        s = "rfDisableRelay()   Relay = 1   Reader = ";
	     else
	        s = "rfDisableRelay()   Relay = 2   Reader = ";
	  }
	  itoa(rdrID, buf, 10);
	  s += buf;
   }
   else if (strcmp(buf, "ALL_READERS") == 0)
   {
      cmdType = ALL_READERS;
	  if (enable)
	  {
	     if (relayID == 0x01)
	        s = "rfEnableRelay()   Relay = 1   ALL_READERS";
	     else
	        s = "rfEnableRelay()   Relay = 2   ALL_READERS";
	  }
	  else
	  {
         if (relayID == 0x01)
	        s = "rfDisableRelay()   Relay = 1   ALL_READERS";
	     else
	        s = "rfDisableRelay()   Relay = 2   ALL_READERS";
	  }
   }
   else
   {
	  MessageBox("No Reader Function Type", "API", MB_ICONHAND);
      return;
   }

   int ret = rfEnableRelay(hostID, rdrID, 0, relayID, enable, cmdType, ++pktID);
   apiDlg->Display(s, pktID, ret);
   MessageBeep(0xFFFFFFFF);
}

void CReaderPage::OnBnClickedButtonGetRdrFs()
{
   char buf[64];
   unsigned short fType;
   CString s;
   Byte value = 0;
   Boolean range = false;

   int rdrID = GetDlgItemInt(IDC_EDIT_READER_ID, NULL,true);
   int hostID = GetDlgItemInt(IDC_EDIT_HOST_ID, NULL,true);
   GetDlgItemText(IDC_COMB_READER_FUNC_TYPE, buf, 50);
   if (strcmp(buf, "SPECIFIC_READER") == 0)
   {
      fType = SPECIFIC_READER;
	  s = "rfGetReaderFS()   Reader = ";
	  itoa(rdrID, buf, 10);
	  s += buf;
   }
   else if (strcmp(buf, "ALL_READERS") == 0)
   {
      fType = ALL_READERS;
	  s = "rfGetReaderFS()  ALL_READERS";
   }
   else
   {
	  MessageBox("No Reader Function Type", "API", MB_ICONHAND);
      return;
   }

   int ret = rfGetReaderFS(hostID, rdrID, 0, fType, ++pktID);
   apiDlg->Display(s, pktID, ret);
   MessageBeep(0xFFFFFFFF);
}

void CReaderPage::OnBnClickedButtonSetRdrFs()
{
   char buf[64];
   unsigned short fType;
   CString s;
   UInt16 actType = 0;
   Byte value = 0;
   Boolean range = false;

   int rdrID = GetDlgItemInt(IDC_EDIT_READER_ID, NULL,true);
   int hostID = GetDlgItemInt(IDC_EDIT_HOST_ID, NULL,true);
   GetDlgItemText(IDC_COMB_READER_FUNC_TYPE, buf, 50);
   if (strcmp(buf, "SPECIFIC_READER") == 0)
   {
      fType = SPECIFIC_READER;
	  s = "rfSetReaderFS()   Reader = ";
	  itoa(rdrID, buf, 10);
	  s += buf;
   }
   else if (strcmp(buf, "ALL_READERS") == 0)
   {
      fType = ALL_READERS;
	  s = "rfSetReaderFS()  ALL_READERS";
   }
   else
   {
	  MessageBox("No Reader Function Type", "API", MB_ICONHAND);
      return;
   }

   if (((CButton *)GetDlgItem(IDC_RADIO_RDR_FS_ABS))->GetCheck() == BST_CHECKED)
   {
       actType = RF_ABSOLUTE;
	   value = (Byte)GetDlgItemInt(IDC_EDIT_RDR_FS, NULL,true);
   }
   else if (((CButton *)GetDlgItem(IDC_RADIO_RDR_FS_INC))->GetCheck() == BST_CHECKED)
       actType = RF_INCREMENT;
   else if (((CButton *)GetDlgItem(IDC_RADIO_RDR_FS_DEC))->GetCheck() == BST_CHECKED)
       actType = RF_DECREMENT;
   else if (((CButton *)GetDlgItem(IDC_RADIO_RDR_FS_RANGE))->GetCheck() == BST_CHECKED)
       actType = RF_RANGE;
   else
   {
	  MessageBox("No Action Type for Reader Field Strength", "API", MB_ICONHAND);
      return;
   }

   if (((CButton *)GetDlgItem(IDC_RADIO_RDR_RANGE_LONG))->GetCheck() == BST_CHECKED)
      range = true;
   else
	  range = false;

   int ret = rfSetReaderFS(hostID, rdrID, 0, actType, value, range, fType, ++pktID);
   apiDlg->Display(s, pktID, ret);
   MessageBeep(0xFFFFFFFF);
}

void CReaderPage::OnBnClickedButtonGetInput()
{
   char buf[64];
   unsigned short cmdType;
   CString s;

   int rdrID = GetDlgItemInt(IDC_EDIT_READER_ID, NULL,true);
   int hostID = GetDlgItemInt(IDC_EDIT_HOST_ID, NULL,true);
   GetDlgItemText(IDC_COMB_READER_FUNC_TYPE, buf, 50);
   if (strcmp(buf, "SPECIFIC_READER") == 0)
   {
      cmdType = SPECIFIC_READER;
	  s = "rfEnableReader()   Reader = ";
	  itoa(rdrID, buf, 10);
	  s += buf;
   }
   else if (strcmp(buf, "ALL_READERS") == 0)
   {
      cmdType = ALL_READERS;
	  s = "rfEnableReader()  ALL_READERS";
   }
   else
   {
	  MessageBox("No Reader Function Type", "API", MB_ICONHAND);
      return;
   }

   GetDlgItemText(IDC_COMB_READER_FUNC_TYPE, buf, 50);
   if (strcmp(buf, "SPECIFIC_READER") == 0)
   {
      cmdType = SPECIFIC_READER;
	  
	  s = "rfGetInputPortStatus()   Reader = ";
	  itoa(rdrID, buf, 10);
	  s += buf;
   }
   else if (strcmp(buf, "ALL_READERS") == 0)
   {
      cmdType = ALL_READERS;
	  s = "rfGetInputPortStatus()   ALL_READERS"; 
   }
   else
   {
	  MessageBox("No Reader Function Type", "API", MB_ICONHAND);
      return;
   }

   int ret = rfGetInputPortStatus(hostID, rdrID, 0, cmdType, ++pktID);
   apiDlg->Display(s, pktID, ret);
   MessageBeep(0xFFFFFFFF);
}

void CReaderPage::OnBnClickedButtonConfigInputPort()
{
   char buf[64];
   unsigned short cmdType;
   CString s;

   int rdrID = GetDlgItemInt(IDC_EDIT_READER_ID, NULL,true);
   int hostID = GetDlgItemInt(IDC_EDIT_HOST_ID, NULL,true);
   GetDlgItemText(IDC_COMB_READER_FUNC_TYPE, buf, 50);
   if (strcmp(buf, "SPECIFIC_READER") == 0)
   {
      cmdType = SPECIFIC_READER;
	  s = "rfEnableReader()   Reader = ";
	  itoa(rdrID, buf, 10);
	  s += buf;
   }
   else if (strcmp(buf, "ALL_READERS") == 0)
   {
      cmdType = ALL_READERS;
	  s = "rfEnableReader()  ALL_READERS";
   }
   else
   {
	  MessageBox("No Reader Function Type", "API", MB_ICONHAND);
      return;
   }
 
   unsigned short config1 = 0;
   unsigned short config2 = 0;
   bool supervised = false;

   if(((CButton *)GetDlgItem(IDC_RADIO_IGNOR_INPUT1))->GetCheck() == BST_CHECKED)
	   config1 = IGNOR_INPUT_CHANGE;
	else if(((CButton *)GetDlgItem(IDC_RADIO_REPORT_INPUT1))->GetCheck() == BST_CHECKED)
	   config1 = REPORT_INPUT_CHANGE;
	else
	   config1 = NO_CHANGE_INPUT;

	if(((CButton *)GetDlgItem(IDC_RADIO_IGNOR_INPUT2))->GetCheck() == BST_CHECKED)
	   config2 = IGNOR_INPUT_CHANGE;
	else if(((CButton *)GetDlgItem(IDC_RADIO_REPORT_INPUT2))->GetCheck() == BST_CHECKED)
	   config2 = REPORT_INPUT_CHANGE;
	else
	   config2 = NO_CHANGE_INPUT;

    if(((CButton *)GetDlgItem(IDC_CHECK_SUPERVISED))->GetCheck() == BST_CHECKED)
	   supervised = true;
	else
       supervised = false;

   GetDlgItemText(IDC_COMB_READER_FUNC_TYPE, buf, 50);
   if (strcmp(buf, "SPECIFIC_READER") == 0)
   {
      cmdType = SPECIFIC_READER;
	  
	  s = "rfConfigInputPort()   Reader = ";
	  itoa(rdrID, buf, 10);
	  s += buf;
   }
   else if (strcmp(buf, "ALL_READERS") == 0)
   {
      cmdType = ALL_READERS;
	  s = "rfConfigInputPort()   ALL_READERS"; 
   }
   else
   {
	  MessageBox("No Reader Function Type", "API", MB_ICONHAND);
      return;
   }

   int ret = rfConfigInputPort(hostID, rdrID, 0, config1, config2, supervised, cmdType, ++pktID);
   apiDlg->Display(s, pktID, ret);
   MessageBeep(0xFFFFFFFF);
}

void CReaderPage::OnBnClickedButtonConfigNewRdr()
{
	char buf[64];
   CString s;
   UInt16 rdrID = 0;
   UInt16 hostID = 0;
   UInt16 newRdrID = 0;
   UInt16 newHostID = 0;
   UInt16 cmdType = 0;
   UInt16 rdrType = 0;
   UInt32 cfgType = 0;
   UInt16 param1 = 0;
   UInt16 param2 = 0;
   UInt16 param3 = 0;
   UInt16 param4 = 0;
   bool configure = false;

   rdrID = GetDlgItemInt(IDC_EDIT_READER_ID, NULL,true);
   hostID = GetDlgItemInt(IDC_EDIT_HOST_ID, NULL,true);
   
   GetDlgItemText(IDC_COMB_READER_FUNC_TYPE, buf, 50);
   if (strcmp(buf, "SPECIFIC_READER") == 0)
   {
      cmdType = SPECIFIC_READER;
      s = "rfConfigureReader()   readerID = ";
	  s += itoa(rdrID, buf, 10);
   }
   else if (strcmp(buf, "ALL_READERS") == 0)
   {
      cmdType = ALL_READERS;
      s = "rfConfigureReader()   readerID = ALL_READERS";
   }
   else
   {
	  MessageBox("No Reader Command Type", "API", MB_ICONHAND);
      return;
   }

   param1 = hostID;
   param2 = rdrID;
   param3 = RF_RDR_STANDARD;

   if (((CButton *)GetDlgItem(IDC_CHECK_RDR_CFG_HOST_ID))->GetCheck() == BST_CHECKED)
   {
      cfgType |= CFG_HOST_ID;
	  param1 = GetDlgItemInt(IDC_EDIT_NEW_HOST_ID, NULL,true);
	  configure = true;
   }

   if (((CButton *)GetDlgItem(IDC_CHECK_RDR_CFG_RDR_ID))->GetCheck() == BST_CHECKED)
   {
      cfgType |= CFG_READER_ID;
	  param2 = GetDlgItemInt(IDC_EDIT_NEW_READER_ID, NULL,true);
	  configure = true;
   }

   if (!configure)
   {
	  MessageBox("No Reader Config Type", "API", MB_ICONHAND);
      return;
   }

   int ret = rfConfigureReader(hostID, rdrID, 0, cmdType, cfgType, param1, param2, param3, param4, ++pktID);
   apiDlg->Display(s, pktID, ret);
   MessageBeep(0xFFFFFFFF);
}

void CReaderPage::OnBnClickedButtonGetReaderVersion()
{
   char buf[64];
   unsigned short fType;

   int rdrID = GetDlgItemInt(IDC_EDIT_READER_ID, NULL,true);
   int hostID = GetDlgItemInt(IDC_EDIT_HOST_ID, NULL,true);
   GetDlgItemText(IDC_COMB_READER_FUNC_TYPE, buf, 50);
   if (strcmp(buf, "SPECIFIC_READER") == 0)
      fType = SPECIFIC_READER;
   else if (strcmp(buf, "ALL_READERS") == 0)
      fType = ALL_READERS;
   else
   {
	  MessageBox("No Reader Function Type", "API", MB_ICONHAND);
      return;
   }
   int ret = rfGetReaderVersion(hostID, rdrID, 0, fType, ++pktID);
   apiDlg->Display("rfGetReaderVersion()", pktID, ret);
   MessageBeep(0xFFFFFFFF);
}

void CReaderPage::OnClose()
{
	// TODO: Add your message handler code here and/or call default
    
	CDialog::OnClose();
}
