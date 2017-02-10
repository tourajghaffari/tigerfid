//---------------------------------------------------------------------------------------
//    Version 42
//---------------------------------------------------------------------------------------

// TestAPIDlg.cpp : implementation file
 
#include "stdafx.h"
#include <math.h>
#include "TestAPI.h"
#include "TestAPIDlg.h"
#include "AWI_API.h"
#include "RelayDialog.h"
#include "Debug.h"
#include ".\testapidlg.h"
#include "CommPage.h"
#include "ReaderPage.h"
#include "TagPage.h"
#include "SFGENPage.h"
#include "STDFgenPage.h"


#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#ifdef _DEBUG_ACTIVEWAVE
long DebugEventFn( rfDebugEvent_t* debugEvent);
CDebug* debugWin = NULL;
#endif
#endif

long ReaderEvent(long status,
                 HANDLE funcId,
                 rfReaderEvent_t* readerEvent,
                 void* userArg);

long TagEvent(long status,
              HANDLE funcId,
              rfTagEvent_t* tagEvent,
              void* userArg);

CWnd* msgBoxSend;
CWnd* listBox;
CCommPage* comPage = NULL;
CReaderPage* readerPage = NULL;
CSFGENPage* fgenSmartPage = NULL;
STDFGenPage* fgenSTDPage = NULL;
CTagPage* tagPage = NULL;
bool readerRegistered = false;
bool tagRegistered = false;

CWnd* listIPBox;
CWnd* scanButton;
CWnd* app;
rfTagSelect_t* tagSelect = NULL;
rfNewTagConfig_t* newTagCfg = NULL;
CTestAPIDlg* apiDlg;
HANDLE* hConn = NULL;
int pktID = 0;
int pktCounter = 0;
bool configSFGenDlg = false;

unsigned short relayID = 0;
bool enable = false;

void DisplayRecPackets(char buf[260], int len, char ip[20], bool frameFlag, bool crcFlag);
void DisplaySendPackets(char buf[260], int len, char ip[2], bool frameFlag, bool crcFlag);
int HexToInt(char * buf, int size);
int count = 0;
rfTagTemp_t* tagTemp = NULL;

unsigned short config;
unsigned short config2;
bool supervised;
/////////////////////////////////////////////////////////////////////////////
// CAboutDlg dialog used for App About

class CAboutDlg : public CDialog
{
public:
	CAboutDlg();

// Dialog Data
	//{{AFX_DATA(CAboutDlg)
	enum { IDD = IDD_ABOUTBOX };
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CAboutDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:
	//{{AFX_MSG(CAboutDlg)
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

CAboutDlg::CAboutDlg() : CDialog(CAboutDlg::IDD)
{
	//{{AFX_DATA_INIT(CAboutDlg)
	//}}AFX_DATA_INIT
}

void CAboutDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CAboutDlg)
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CAboutDlg, CDialog)
	//{{AFX_MSG_MAP(CAboutDlg)
		// No message handlers
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CTestAPIDlg dialog

CTestAPIDlg::CTestAPIDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CTestAPIDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CTestAPIDlg)
		// NOTE: the ClassWizard will add member initialization here
	//}}AFX_DATA_INIT
	// Note that LoadIcon does not require a subsequent DestroyIcon in Win32
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CTestAPIDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CTestAPIDlg)
	// NOTE: the ClassWizard will add DDX and DDV calls here
	//}}AFX_DATA_MAP
	DDX_Control(pDX, IDC_TAB_API, m_apiTestTab);
}

BEGIN_MESSAGE_MAP(CTestAPIDlg, CDialog)
	//{{AFX_MSG_MAP(CTestAPIDlg)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_WM_CLOSE()
	//}}AFX_MSG_MAP
	ON_WM_SHOWWINDOW()
	ON_NOTIFY(TCN_SELCHANGE, IDC_TAB_API, OnTcnSelchangeTabApi)
	ON_BN_CLICKED(IDC_BUTTON_CLEAR_MSG_LIST, OnBnClickedButtonClearMsgList)
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
//------------------------------------------------------------------------------
long ReaderEvent(long status, HANDLE funcId,
                 rfReaderEvent_t* readerEvent, void* userArg)
{
    CString str;
	CString str1;
	char buf[30];

	if (readerEvent->errorStatus != RF_E_NO_ERROR)
	{
       itoa(++pktCounter, buf, 10);
	   str = buf;
	   str += "- ";

	   itoa(readerEvent->errorStatus, buf, 10);
       str += "    Error Code  = ";
       str += buf;

	   itoa(readerEvent->eventStatus, buf, 10);
       str += " ,   Event Status = ";
	   str += buf;

	   itoa(readerEvent->eventType, buf, 10);
       str += " ,   Event Type  = ";
       str += buf;

       itoa(readerEvent->cmdType, buf, 10);
       str += " ,   Command Type  = ";
       str += buf;

	   str += " ,    pktID = ";
	   itoa(readerEvent->pktID, buf, 10);
	   str += buf;

	   ((CListBox *)listBox)->InsertString(0, str);
	   MessageBeep(MB_OK);
	}
    else if (readerEvent->eventType == RF_READER_RESET)
    {
	   itoa(++pktCounter, buf, 10);
	   str = buf;
	   str += "- ";

       str += "   rfResetReader()";
       
	   str += " ,   readerID = ";
	   itoa(readerEvent->reader, buf, 10);
       str += buf;

	   str += " ,    pktID = ";
	   itoa(readerEvent->pktID, buf, 10);
	   str += buf;
       
	   ((CListBox *)listBox)->InsertString(0, str);
	   MessageBeep(MB_OK);
	   
    }
	else if (readerEvent->eventType == RF_GET_READER_VERSION) 	     
    {
	   itoa(++pktCounter, buf, 10);
	   str = buf;
	   str += "- ";

       str += "   rfGetReaderVersion()";
       
	   str += "   readerID = ";
	   itoa(readerEvent->reader, buf, 10);
       str += buf;

	   str += " ,    pktID = ";
	   itoa(readerEvent->pktID, buf, 10);
	   str += buf;
       
	   ((CListBox *)listBox)->InsertString(0, str);

	   str = "Data Code (C) = ";
	   itoa(readerEvent->versionInfo.dataCodeVer, buf, 10);
	   str += buf;
	   ((CListBox *)listBox)->InsertString(0, str);

	   str = "Program Code (B) = ";
	   itoa(readerEvent->versionInfo.progCodeVer, buf, 10);
	   str += buf;

	   ((CListBox *)listBox)->InsertString(0, str);

	   str = "Host Code (E) = ";
	   itoa(readerEvent->versionInfo.hostCodeVer, buf, 10);
	   str += buf;
	   ((CListBox *)listBox)->InsertString(0, str);

	   MessageBeep(MB_OK);  
    }
	else if (readerEvent->eventType == RF_STD_FGEN_POWERUP)
    {
	   itoa(++pktCounter, buf, 10);
	   str = buf;
	   str += "- ";

       str += "   Powerup STD FGen";
       
	   str += " ,   FGenID = ";
	   itoa(readerEvent->fGenerator, buf, 10);
       str += buf;

	   str += " ,    pktID = ";
	   itoa(readerEvent->pktID, buf, 10);
	   str += buf;

	   fgenSTDPage->SetDlgItemInt(IDC_EDIT_FGEN, readerEvent->fGenerator, false);
	   fgenSTDPage->SetDlgItemInt(IDC_EDIT_HOST, readerEvent->host, false);
	   
	   ((CListBox *)listBox)->InsertString(0, str);
	    MessageBeep(MB_OK);
	   
    }
	else if (readerEvent->eventType == RF_SMART_FGEN_POWERUP)
    {
	   itoa(++pktCounter, buf, 10);
	   str = buf;
	   str += "- ";

       str += "   Powerup Smart FGen";
       
	   str += " ,   Smart FGenID = ";
	   itoa(readerEvent->fGenerator, buf, 10);
       str += buf;

	   str += " ,    pktID = ";
	   itoa(readerEvent->pktID, buf, 10);
	   str += buf;

	   fgenSmartPage->SetDlgItemInt(IDC_EDIT_FGEN, readerEvent->fGenerator, false);
	   
	   ((CListBox *)listBox)->InsertString(0, str);
	   MessageBeep(MB_OK);
	   
    } 
	else if (readerEvent->eventType == RF_QUERY_STD_FGEN)
    {
		itoa(++pktCounter, buf, 10);
		str = buf;
		str += "- ";

		str += "   rfQueryFieldGenerator()";
		
		str += " ,    pktID = ";
		itoa(readerEvent->pktID, buf, 10);
		str += buf;
		str += " ,   readerID = ";
		itoa(readerEvent->reader, buf, 10);
		str += buf;

		((CListBox *)listBox)->InsertString(0, str);

		str = " ,    FGen Reader ID = ";
		itoa(readerEvent->smartFgen.readerID, buf, 10);
		str += buf;
		((CListBox *)listBox)->InsertString(0, str);
		fgenSTDPage->SetDlgItemInt(IDC_EDIT_READER, readerEvent->smartFgen.readerID, false);

		str = " ,    Tag ID = ";
		itoa(readerEvent->smartFgen.tagID, buf, 10);
		str += buf;
		if (readerEvent->smartFgen.tagID == 0)
			fgenSTDPage->CheckDlgButton(IDC_RADIO_ANYTYPE, 1);
		else
			fgenSTDPage->SetDlgItemInt(IDC_EDIT_TAG, readerEvent->smartFgen.tagID, false);

		str += " ,    Tag Type = ";
		if (readerEvent->smartFgen.tagType == ALL_TAGS)
		{
			str += "Any Type";
			fgenSTDPage->CheckDlgButton(IDC_RADIO_ANYTYPE, 1);
		}
		else if (readerEvent->smartFgen.tagType == ACCESS_TAG)
		{
			str += "Access";
			fgenSTDPage->CheckDlgButton(IDC_RADIO_ACCESS, 1);
		}
		else if (readerEvent->smartFgen.tagType == ASSET_TAG)
		{
			str += "Asset";
			fgenSTDPage->CheckDlgButton(IDC_RADIO_ASSET, 1);
		}
		else
		{
			str += "Inventory";
		}
		((CListBox *)listBox)->InsertString(0, str);

		str = "     Transmit Time = ";
		itoa(readerEvent->smartFgen.txTime, buf, 10);
		str += buf;
		str += " sec";
		((CListBox *)listBox)->InsertString(0, str);
		fgenSTDPage->SetDlgItemInt(IDC_EDIT_TX, readerEvent->smartFgen.txTime, false);

		str = "     Wait Time = ";
		itoa(readerEvent->smartFgen.waitTime, buf, 10);
		str += buf;
		if (readerEvent->smartFgen.wTimeType = RF_TIME_SECOND)
		{
			str += " sec";
			fgenSTDPage->CheckDlgButton(IDC_RADIO_SEC, 1);
		}
		else if (readerEvent->smartFgen.wTimeType = RF_TIME_MINUTE)
		{
			str += " min";
			fgenSTDPage->CheckDlgButton(IDC_RADIO_MIN, 1);
		}
		else
		{
			str += " hour";	
			fgenSTDPage->CheckDlgButton(IDC_RADIO_HOUR, 1);
		}
		((CListBox *)listBox)->InsertString(0, str);
		fgenSTDPage->SetDlgItemInt(IDC_EDIT_WT, readerEvent->smartFgen.waitTime, false);

		str = "    Motion Detector Enable = ";
		if (readerEvent->smartFgen.mDetectStatus)
		{
			str += "True";
			fgenSTDPage->CheckDlgButton(IDC_RADIO_ENABLE_MD, 1);
		}
		else
		{
			str += "False";
			fgenSTDPage->CheckDlgButton(IDC_RADIO_DISABLE_MD, 1);
		}
		((CListBox *)listBox)->InsertString(0, str);

		str = "    Motion Detector Active ";
		if (readerEvent->smartFgen.mDetectActive)
		{
			str += "HIGH";
			fgenSTDPage->CheckDlgButton(IDC_RADIO_MD_ACTIVE_HIGH, 1);
		}
		else
		{
			str += "LOW";
			fgenSTDPage->CheckDlgButton(IDC_RADIO_MD_ACTIVE_LOW, 1);
		}
		((CListBox *)listBox)->InsertString(0, str);

	   str = "    Assign Reader = ";
	   if (readerEvent->smartFgen.assignRdr)
	   {
	      str += "True"; 
		  fgenSTDPage->CheckDlgButton(IDC_CHECK_ASSIGN_READER, 1);
	   }
	   else
	   {
	      str += "False";
		  fgenSTDPage->CheckDlgButton(IDC_CHECK_ASSIGN_READER, 0);
	   }
	   ((CListBox *)listBox)->InsertString(0, str);

	   str = "    Long Interval = ";
	   if (readerEvent->smartFgen.longInterval)
	   {
	      str += "True";
		  fgenSTDPage->CheckDlgButton(IDC_RADIO_LONG_RND, 1);
	   }
	   else
	   {
	      str += "False";
		  fgenSTDPage->CheckDlgButton(IDC_RADIO_SHORT_RND, 1);
	   }
	   ((CListBox *)listBox)->InsertString(0, str);

	   MessageBeep(MB_OK);
	   
  }
	else if ((readerEvent->eventType == RF_GET_READER_CONFIG) ||
		     (readerEvent->eventType == RF_GET_READER_CONFIG_ALL))

    {
	   itoa(++pktCounter, buf, 10);
	   str = buf;
	   str += "- ";

       str += "   rfGetReaderConfig()";
       
	   str += "   readerID = ";
	   itoa(readerEvent->reader, buf, 10);
       str += buf;

	   str += " ,    pktID = ";
	   itoa(readerEvent->pktID, buf, 10);
	   str += buf;

	   str += " ,    TX Time = ";
	   itoa(readerEvent->smartFgen.txTime, buf, 10);
	   str += buf;

       readerPage->SetDlgItemInt(IDC_EDIT_READER_TX_TIME, readerEvent->smartFgen.txTime, false);
			     
	   str += " ,    Field Strength = ";
	   itoa(readerEvent->smartFgen.fsValue, buf, 10);
	   str += buf;
	   ((CListBox *)listBox)->InsertString(0, str);

	   itoa(pktCounter, buf, 10);
	   str = buf;
	   str += "- ";
	   str += "    Wait Time = ";
	   itoa(readerEvent->smartFgen.waitTime, buf, 10);
	   str += buf;
		 readerPage->SetDlgItemInt(IDC_EDIT_READER_WT_TIME, readerEvent->smartFgen.waitTime, false);
     if (readerEvent->smartFgen.wTimeType == RF_TIME_HOUR) 
        ((CButton *)readerPage->GetDlgItem(IDC_RADIO_RDR_WT_HR))->SetCheck(true);
		 else if (readerEvent->smartFgen.wTimeType == RF_TIME_MINUTE)
			  ((CButton *)readerPage->GetDlgItem(IDC_RADIO_RDR_WT_MINUTE))->SetCheck(true);
		 else
			  ((CButton *)readerPage->GetDlgItem(IDC_RADIO_RDR_WT_SECOND))->SetCheck(true);

		 str += " ,   Motion Detect = ";
	   if(readerEvent->smartFgen.mDetectActive)
		 {
	      str += "Active High";
				((CButton *)readerPage->GetDlgItem(IDC_CHECK_RDR_MD_HIGH))->SetCheck(true);
		 }
		 else
		 {
			 str += "Active Low";
			 ((CButton *)readerPage->GetDlgItem(IDC_CHECK_RDR_MD_HIGH))->SetCheck(false);
		 }

		 if(readerEvent->smartFgen.mDetectStatus)
		 {
	        str += " ,   Enabled";
			((CButton *)readerPage->GetDlgItem(IDC_CHECK_RDR_MD_ENABLE))->SetCheck(true);
		 }
		 else
		 {
			  str +=  " ,   Disabled";
			((CButton *)readerPage->GetDlgItem(IDC_CHECK_RDR_MD_ENABLE))->SetCheck(false);
		 }
      
	   ((CListBox *)listBox)->InsertString(0, str);
	   MessageBeep(MB_OK);
	   
  } //GET_READER_CONFIG	
  else if ((readerEvent->eventType == RF_READER_CONFIG) ||
	  (readerEvent->eventType == RF_READER_CONFIG_ALL))

  {
	   itoa(++pktCounter, buf, 10);
	   str = buf;
	   str += "- ";

       str += "   rfConfigureReader()";
       
	   str += " ,   readerID = ";
	   itoa(readerEvent->reader, buf, 10);
       str += buf;

	   str += " ,    pktID = ";
	   itoa(readerEvent->pktID, buf, 10);
	   str += buf;
      
	   ((CListBox *)listBox)->InsertString(0, str);
	   MessageBeep(MB_OK);
	   
  }	
  else if (readerEvent->eventType == RF_CONFIG_STD_FGEN)
  {
	   itoa(++pktCounter, buf, 10);
	   str = buf;
	   str += "- ";

       str += "   rfConfigSTDFGen()";
       
	   str += " ,   fGenID = ";
	   itoa(readerEvent->fGenerator, buf, 10);
       str += buf;

	   str += " ,    pktID = ";
	   itoa(readerEvent->pktID, buf, 10);
	   str += buf;
      
	   ((CListBox *)listBox)->InsertString(0, str);
	   MessageBeep(MB_OK);
	   
  }
  else if (readerEvent->eventType == RF_SET_CONFIG_SMART_FGEN)
  {
	   itoa(++pktCounter, buf, 10);
	   str = buf;
	   str += "- ";

       str += "   rfSetConfigSmartFGen()";
       
	   str += " ,    pktID = ";
	   itoa(readerEvent->pktID, buf, 10);
	   str += buf;
       str += " ,   readerID = ";
	   itoa(readerEvent->reader, buf, 10);
       str += buf;
	   str += " ,   smartFGenID = ";
	   itoa(readerEvent->smartFgen.ID, buf, 10);
       str += buf;
      
	   ((CListBox *)listBox)->InsertString(0, str);
	   MessageBeep(MB_OK);
	   
    }
	else if (readerEvent->eventType == RF_CALL_TAG_SMART_FGEN)
    {
	   itoa(++pktCounter, buf, 10);
	   str = buf;
	   str += "- ";

       str += "   rfCallTagSmartFGen()";
       
	   str += " ,    pktID = ";
	   itoa(readerEvent->pktID, buf, 10);
	   str += buf;
       str += " ,   readerID = ";
	   itoa(readerEvent->reader, buf, 10);
       str += buf;
	   str += "  ,   smartFGenID = ";
	   itoa(readerEvent->smartFgen.ID, buf, 10);
       str += buf;
      
	   ((CListBox *)listBox)->InsertString(0, str);
	   MessageBeep(MB_OK);
	   
    }
	else if ((readerEvent->eventType == RF_SET_FS_SMART_FGEN) || 
           (readerEvent->eventType == RF_SET_FS_SMART_FGEN_ALL))
    {
	   itoa(++pktCounter, buf, 10);
	   str = buf;
	   str += "- ";

       str += "   rfSetSmartFGenFS()";
       
	   str += " ,    pktID = ";
	   itoa(readerEvent->pktID, buf, 10);
	   str += buf;
       str += " ,   rdrID = ";
	   itoa(readerEvent->reader, buf, 10);
       str += buf;
	   str += " ,   SmartFGenID = ";
	   itoa(readerEvent->smartFgen.ID, buf, 10);
       str += buf;
       
       str += " ,    FS = ";
	   itoa(readerEvent->smartFgen.fsValue, buf, 10);
	   str += buf;

	   ((CListBox *)listBox)->InsertString(0, str);

	   MessageBeep(MB_OK);   
    }
	else if ((readerEvent->eventType == RF_GET_FS_SMART_FGEN) || 
           (readerEvent->eventType == RF_GET_FS_SMART_FGEN_ALL))
    {
	   itoa(++pktCounter, buf, 10);
	   str = buf;
	   str += "- ";

       str += "   rfGetSmartFGenFS()";
       
	   str += " ,    pktID = ";
	   itoa(readerEvent->pktID, buf, 10);
	   str += buf;
       str += " ,   rdrID = ";
	   itoa(readerEvent->reader, buf, 10);
       str += buf;
	   str += " ,   SmartFGenID = ";
	   itoa(readerEvent->smartFgen.ID, buf, 10);
       str += buf;
       
	   ((CListBox *)listBox)->InsertString(0, str);
	   
       str = "Field Strength = ";
	   itoa(readerEvent->smartFgen.fsValue, buf, 10);
	   str += buf;
	   ((CListBox *)listBox)->InsertString(0, str);

	   str = "Assigned Reader ID = ";
	   itoa(readerEvent->smartFgen.readerID, buf, 10);
	   str += buf;
	   ((CListBox *)listBox)->InsertString(0, str);

	   str = "Transmit Time = ";
	   itoa(readerEvent->smartFgen.txTime, buf, 10);
	   str += buf;
	   str += " sec";
	   ((CListBox *)listBox)->InsertString(0, str);

	   str = "Wait Time = ";
	   itoa(readerEvent->smartFgen.waitTime, buf, 10);
	   str += buf;
	   if (readerEvent->smartFgen.wTimeType == RF_TIME_HOUR)
	      str += " hour";
	   else if (readerEvent->smartFgen.wTimeType == RF_TIME_MINUTE)
	      str += " min";
	   else 
	      str += " sec";
	   ((CListBox *)listBox)->InsertString(0, str);

	   str = "Motion Detector Status = ";
	   if (readerEvent->smartFgen.mDetectStatus)
	      str += "Enabled";
	   else
	      str += "Disabled";
	   ((CListBox *)listBox)->InsertString(0, str);

	   str = "Motion Detector Active High = ";
	   if (readerEvent->smartFgen.mDetectActive)
	      str += "True";
	   else
	      str += "False";
	   ((CListBox *)listBox)->InsertString(0, str);

	   str = "Assign Rdr ID to Tag = ";
	   if (readerEvent->smartFgen.assignRdr)
	      str += "True";
	   else
	      str += "False";
	   ((CListBox *)listBox)->InsertString(0, str);

	   str = "Tag ID = ";
	   if (readerEvent->smartFgen.tagID == 0)
          str += "Any Tag ID";
	   else
	   {
	      itoa(readerEvent->smartFgen.tagID, buf, 10);
	      str += buf;
	   }
	   ((CListBox *)listBox)->InsertString(0, str);

	   str = "Tag Type = ";
	   if (readerEvent->smartFgen.tagType == ACCESS_TAG)
          str += "Access";
	   else if (readerEvent->smartFgen.tagType == ASSET_TAG)
          str += "Asset";
	   else if (readerEvent->smartFgen.tagType == INVENTORY_TAG)
          str += "Inventory";
	   else
	      str += "Any Type";
	   ((CListBox *)listBox)->InsertString(0, str);

	   MessageBeep(MB_OK);   
    }
	else if ((readerEvent->eventType == RF_QUERY_SMART_FGEN) || 
           (readerEvent->eventType == RF_QUERY_SMART_FGEN_ALL))
    {
	   itoa(++pktCounter, buf, 10);
	   str = buf;
	   str += "- ";

       str += "   rfQuerySmartFGen()";
       
	   str += " ,    pktID = ";
	   itoa(readerEvent->pktID, buf, 10);
	   str += buf;
       str += " ,   readerID = ";
	   itoa(readerEvent->reader, buf, 10);
       str += buf;
	   str += " ,   SmartFGenID = ";
	   itoa(readerEvent->smartFgen.ID, buf, 10);
       str += buf;
       app->SetDlgItemInt(IDC_EDIT_FGEN_ID, readerEvent->smartFgen.ID, false);
	   
	   ((CListBox *)listBox)->InsertString(0, str);

       str = "    Assigned Reader ID = ";
	   itoa(readerEvent->smartFgen.readerID, buf, 10);
	   str += buf;
	   ((CListBox *)listBox)->InsertString(0, str);

	   str = "    Tag ID = ";
	   itoa(readerEvent->smartFgen.tagID, buf, 10);
	   str += buf;
	   str += "    Tag Type = ";
	   if (readerEvent->smartFgen.tagType == ALL_TAGS)
		   str += "Any Type";
	   else if (readerEvent->smartFgen.tagType == ACCESS_TAG)
		   str += "Access";
	   else if (readerEvent->smartFgen.tagType == ASSET_TAG)
		   str += "Asset";
	   else
		   str += "Inventory";
	   ((CListBox *)listBox)->InsertString(0, str);

	   str = "    Transmit Time = ";
	   itoa(readerEvent->smartFgen.txTime, buf, 10);
	   str += buf;
	   str += " sec";
	   ((CListBox *)listBox)->InsertString(0, str);

	   str = "    Wait Time = ";
	   itoa(readerEvent->smartFgen.waitTime, buf, 10);
	   str += buf;
	   if (readerEvent->smartFgen.wTimeType = RF_TIME_SECOND)
          str += " sec";
	   else if (readerEvent->smartFgen.wTimeType = RF_TIME_MINUTE)
          str += " min";
	   else
          str += " hour";	   
	   ((CListBox *)listBox)->InsertString(0, str);

       str = "    Field Strength = ";
	   itoa(readerEvent->smartFgen.fsValue, buf, 10);
	   str += buf;
	   ((CListBox *)listBox)->InsertString(0, str);

	   str = "    Motion Detector Enable = ";
	   if (readerEvent->smartFgen.mDetectStatus)
	      str += "True";
	   else
	      str += "False";
	   ((CListBox *)listBox)->InsertString(0, str);

	   str = "    Motion Detector Active ";
	   if (readerEvent->smartFgen.mDetectActive)
	      str += "HIGH";
	   else
	      str += "LOW";
	   ((CListBox *)listBox)->InsertString(0, str);

	   MessageBeep(MB_OK);
	   
    }
	else if ((readerEvent->eventType == RF_RESET_SMART_FGEN) || 
             (readerEvent->eventType == RF_RESET_SMART_FGEN_ALL))
    {
	   itoa(++pktCounter, buf, 10);
	   str = buf;
	   str += "- ";

       str += "   rfResetSmartFGen()";
       
	   str += " ,    pktID = ";
	   itoa(readerEvent->pktID, buf, 10);
	   str += buf;
       str += " ,   readerID = ";
	   itoa(readerEvent->reader, buf, 10);
       str += buf;
	   str += " ,   SmartFGenID = ";
	   itoa(readerEvent->smartFgen.ID, buf, 10);
       str += buf;
       app->SetDlgItemInt(IDC_EDIT_FGEN_ID, readerEvent->smartFgen.ID, false);
	   
	   ((CListBox *)listBox)->InsertString(0, str);
	   MessageBeep(MB_OK);
	   
    }
	else if ((readerEvent->eventType == RF_GET_RDR_FS) ||
		     (readerEvent->eventType == RF_GET_RDR_FS_ALL))

    {
	   itoa(++pktCounter, buf, 10);
	   str = buf;
	   str += "- ";

       str += "   rfGetReaderFS()";
       
	   str += " ,    pktID = ";
	   itoa(readerEvent->pktID, buf, 10);
	   str += buf;
       str += " ,   readerID = ";
	   itoa(readerEvent->reader, buf, 10);
       str += buf;
      
	   str += " ,   FS = ";
	   itoa(readerEvent->smartFgen.fsValue, buf, 10);
	   str += buf;

	   str += " ,   Range = ";
	   if (readerEvent->smartFgen.longDistance)
	     str += "Long";
	   else
	     str += "Short";
	   
	   ((CListBox *)listBox)->InsertString(0, str);
	   MessageBeep(MB_OK);
	   
    }
	else if (readerEvent->eventType == RF_SET_RDR_FS)
    {
	   itoa(++pktCounter, buf, 10);
	   str = buf;
	   str += "- ";

       str += "   rfSetReaderFS()";
       
	   str += " ,    pktID = ";
	   itoa(readerEvent->pktID, buf, 10);
	   str += buf;
       str += " ,   readerID = ";
	   itoa(readerEvent->reader, buf, 10);
       str += buf;
      
	   str += " ,   FS = ";
	   itoa(readerEvent->data[0], buf, 10);
	   str += buf;
	   ((CListBox *)listBox)->InsertString(0, str);
	   MessageBeep(MB_OK);
	   
    }
	else if (readerEvent->eventType == RF_GET_INPUT_STATUS)
    {
	   itoa(++pktCounter, buf, 10);
	   str = buf;
	   str += "- ";

       str += "   rfGetInputPortStatus()";
       
	   str += " ,    pktID = ";
	   itoa(readerEvent->pktID, buf, 10);
	   str += buf;
       str += " ,   readerID = ";
	   itoa(readerEvent->reader, buf, 10);
       str += buf;
	   
	   str += " ,   Input1 = ";
	   if (readerEvent->data[0] == NORMAL_CLOSED)
           str += "Normal Closed";
	   else if (readerEvent->data[0] == NORMAL_OPEN)
           str += "Normal Open";
	   else if (readerEvent->data[0] == FAULTY_CLOSED)
           str += "Faulty Closed";
	   else if (readerEvent->data[0] == FAULTY_OPEN)
           str += "Faulty Open";

	   str += " ,   Input2 = ";
	   if (readerEvent->data[1] == NORMAL_CLOSED)
           str += "Normal Closed";
	   else if (readerEvent->data[1] == NORMAL_OPEN)
           str += "Normal Open";
	   else if (readerEvent->data[1] == FAULTY_CLOSED)
           str += "Faulty Closed";
	   else if (readerEvent->data[1] == FAULTY_OPEN)
           str += "Faulty Open";

	   ((CListBox *)listBox)->InsertString(0, str);
	   MessageBeep(MB_OK);
	   
    }
	else if ((readerEvent->eventType == RF_CONFIG_INPUT_PORT) ||
             (readerEvent->eventType == RF_CONFIG_INPUT_PORT_ALL))
    {
	   itoa(++pktCounter, buf, 10);
	   str = buf;
	   str += "- ";

       str += "   rfConfigInputPort()";
       
	   str += " ,    pktID = ";
	   itoa(readerEvent->pktID, buf, 10);
	   str += buf;
       str += " ,   readerID = ";
	   itoa(readerEvent->reader, buf, 10);
       str += buf;
	   
	   ((CListBox *)listBox)->InsertString(0, str);
	   MessageBeep(MB_OK);
	   
    }
	else if (readerEvent->eventType == RF_END_OF_BROADCAST)
	{
       itoa(++pktCounter, buf, 10);
	   str = buf;
	   str += "- ";

       str += "   End Of Broadcast";
       
	   str += " ,   readerID = ";
	   itoa(readerEvent->reader, buf, 10);
       str += buf;

	   str += " ,    pktID = ";
	   itoa(readerEvent->pktID, buf, 10);
	   str += buf;

	   ((CListBox *)listBox)->InsertString(0, str);
	   MessageBeep(MB_OK);
	}
	else if ((readerEvent->eventType == RF_RELAY_ENABLE) ||
	        (readerEvent->eventType == RF_RELAY_DISABLE))
    {
	   itoa(++pktCounter, buf, 10);
	   str = buf;
	   str += "- ";
       if (readerEvent->eventType == RF_RELAY_ENABLE)
          str += "   rfEnableRelay()";
	   else
          str += "   rfDisableRelay()";
	   str += " ,    pktID = ";
	   itoa(readerEvent->pktID, buf, 10);
	   str += buf;
       str += " ,   readerID = ";
	   itoa(readerEvent->reader, buf, 10);
       str += buf;
      
	   ((CListBox *)listBox)->InsertString(0, str);
	   MessageBeep(MB_OK);
	   
    }
	else if (readerEvent->eventType == RF_OPEN_SOCKET)
    {
	   itoa(++pktCounter, buf, 10);
	   str = buf;
	   str += "- ";

       str += "   rfOpenSocket()";
       
	   str += " ,    pktID = ";
	   itoa(readerEvent->pktID, buf, 10);
	   str += buf;
       
	   if (readerEvent->ip != NULL)
	   {
	      str += " ,    IP = ";
	      strcpy(buf, (char*)readerEvent->ip);
	      str += buf;
	   }

	   ((CListBox *)listBox)->InsertString(0, str);
	   MessageBeep(MB_OK);
	   
    }
	else if (readerEvent->eventType == RF_CLOSE_SOCKET)
    {
	   itoa(++pktCounter, buf, 10);
	   str = buf;
	   str += "- ";

       str += "   rfCloseSocket()";
       
	   str += " ,    pktID = ";
	   itoa(readerEvent->pktID, buf, 10);
	   str += buf;
       
	   if (readerEvent->ip != NULL)
	   {
	      str += " ,    IP = ";
	      strcpy(buf, (char*)readerEvent->ip);
	      str += buf;
	   }

	   ((CListBox *)listBox)->InsertString(0, str);
	   MessageBeep(MB_OK);
	   
    }
	else if (readerEvent->eventType == RF_SCAN_NETWORK)
    {
	   itoa(++pktCounter, buf, 10);
	   str = buf;
	   str += "- ";

       str += "   rfScanNetwork()";
       
	   str += " ,    pktID = ";
	   itoa(readerEvent->pktID, buf, 10);
	   str += buf;
       
	   if (readerEvent->ip[0] != NULL)
	   {
	      str += "    IP = ";
	      strcpy(buf, (char*)readerEvent->ip);
	      str += buf;
	      str1 = buf;
		  comPage->m_ipList.AddString(str1);
	   }

	   ((CListBox *)listBox)->InsertString(0, str);
	   MessageBeep(MB_OK);
	   
    }
	else if (readerEvent->eventType == RF_SCAN_IP)
    {
	   itoa(++pktCounter, buf, 10);
	   str = buf;
	   str += "- ";

       str += "   rfScanIP()";
       
	   str += " ,    pktID = ";
	   itoa(readerEvent->pktID, buf, 10);
	   str += buf;

	   

	  if (readerEvent->ip[0] != NULL)
	  {
	    str += "    IP = ";
	    strcpy(buf, (char*)readerEvent->ip);
	    str += buf;
	    
		//if (((CListBox *)IDC_LIST_IP)->FindString(0, (char*)readerEvent->ip) == LB_ERR)
		if (comPage->m_ipList.FindString(0, (char*)readerEvent->ip) == LB_ERR)
		{
			str1 = buf;
		    comPage->m_ipList.AddString(str1);
		}
	  }

      ((CListBox *)listBox)->InsertString(0, str); 
	   MessageBeep(MB_OK);
     
    }
	else if ((readerEvent->eventType == CFG_READER_NEW) ||
		     (readerEvent->eventType == CFG_READER_INIT))
    {
	   itoa(++pktCounter, buf, 10);
	   str = buf;
	   str += "- ";

       str += "   rfConfigureReader() = ";
       
	   str += " ,    pktID = ";
	   itoa(readerEvent->pktID, buf, 10);
	   str += buf;
       str += " ,   readerID = ";
	   itoa(readerEvent->reader, buf, 10);
       str += buf;
      
	   ((CListBox *)listBox)->InsertString(0, str);
	   MessageBeep(MB_OK);
	   
    }
	else if (readerEvent->eventType == RF_READER_PING)
    {
	   itoa(++pktCounter, buf, 10);
	   str = buf;
	   str += "- ";

       str += "   rfQueryReader() = ";
	   itoa(readerEvent->errorStatus, buf, 10);
	   str += buf;

	   str += " ,    pktID = ";
	   itoa(readerEvent->pktID, buf, 10);
	   str += buf;
       str += " ,   readerID = ";
	   itoa(readerEvent->reader, buf, 10);
       str += buf;
      
	   ((CListBox *)listBox)->InsertString(0, str);
	   MessageBeep(MB_OK);
	   
    }
	else if ((readerEvent->eventType == CFG_READER_NEW) ||
		     (readerEvent->eventType == CFG_READER_INIT))
    {
	   itoa(++pktCounter, buf, 10);
	   str = buf;
	   str += "- ";

       str += "   rfConfigureReader()";
       
	   str += " ,   readerID = ";
	   itoa(readerEvent->reader, buf, 10);
       str += buf;

	   str += " ,    pktID = ";
	   itoa(readerEvent->pktID, buf, 10);
	   str += buf;
       
	   ((CListBox *)listBox)->InsertString(0, str);
	   MessageBeep(MB_OK);   
    }
	else if ((readerEvent->eventType == RF_READER_ENABLE) ||
	         (readerEvent->eventType == RF_READER_DISABLE))
    {
	   itoa(++pktCounter, buf, 10);
	   str = buf;
	   str += "- ";
       if (readerEvent->eventType == RF_READER_ENABLE)
          str += "   rfEnableReader()";
	   else
          str += "   rfDisableReader()";
	   str += " ,    pktID = ";
	   itoa(readerEvent->pktID, buf, 10);
	   str += buf;
       str += " ,   readerID = ";
	   itoa(readerEvent->reader, buf, 10);
       str += buf;
      
	   ((CListBox *)listBox)->InsertString(0, str);
	   MessageBeep(MB_OK);	   
    }
    else if (readerEvent->eventType == RF_READER_POWERUP)
    {
       itoa(++pktCounter, buf, 10);
	   CString str = buf;
	   str += "- ";
       str += "   rfPowerupReader()";
       str += " ,    readerID = ";
	   itoa(readerEvent->reader, buf, 10);
       str += buf;
       str += " ,   pktID = ";
	   itoa(pktID, buf, 10);
       str += buf;
	   
	   ((CListBox *)listBox)->InsertString(0, str);

	   app->SetDlgItemInt( IDC_EDIT_READER_ID, readerEvent->reader, false);
	   app->SetDlgItemInt( IDC_EDIT_HOST_ID, readerEvent->host, false);
	   comPage->SetDlgItemInt( IDC_EDIT_COMM_HOST_ID, readerEvent->host, false);
	   readerPage->SetDlgItemInt( IDC_EDIT_READER_ID, readerEvent->reader, false);
	   readerPage->SetDlgItemInt( IDC_EDIT_HOST_ID, readerEvent->host, false);
	   tagPage->SetDlgItemInt( IDC_EDIT_RDR_ID, readerEvent->reader, false);
	   tagPage->SetDlgItemInt( IDC_EDIT_HOST_ID, readerEvent->host, false);
	   fgenSTDPage->SetDlgItemInt( IDC_EDIT_HOST, readerEvent->host, false);
	   fgenSmartPage->SetDlgItemInt( IDC_EDIT_SFGEN_HOST, readerEvent->host, false);
	   fgenSmartPage->SetDlgItemInt( IDC_EDIT_SFGEN_READER, readerEvent->reader, false);

	   MessageBeep(MB_OK);
    }
	
    return (0);
}
//------------------------------------------------------------------------------
long TagEvent(long status, HANDLE funcId,
              rfTagEvent_t* tagEvent, void* userArg)
{
    CString str;
	CString s;
	int fg = 0;
    char buf[100];

	if (tagEvent->errorStatus != RF_E_NO_ERROR)
	{
       itoa(++pktCounter, buf, 10);
	   str = buf;
	   str += "- ";

	   itoa(tagEvent->errorStatus, buf, 10);
       str += "    Error Code  = ";
       str += buf;

	   itoa(tagEvent->eventStatus, buf, 10);
       str += " ,   Event Status = ";
	   str += buf;

	   itoa(tagEvent->eventType, buf, 10);
       str += " ,   Event Type  = ";
       str += buf;

       itoa(tagEvent->cmdType, buf, 10);
       str += " ,   Command Type  = ";
       str += buf;

	   str += " ,    pktID = ";
	   itoa(tagEvent->pktID, buf, 10);
	   str += buf;

	   ((CListBox *)listBox)->InsertString(0, str);
	   MessageBeep(MB_OK);
	}
    else if ((tagEvent->eventType == RF_TAG_DETECTED) || 
			 (tagEvent->eventType == RF_TAG_DETECTED_RSSI) ||
             (tagEvent->eventType == RF_TAG_DETECTED_SANI))
    {
        itoa(++pktCounter, buf, 10);
		str = buf;
		str += "-    ";
        if (tagEvent->eventType == RF_TAG_DETECTED_SANI)
            str += "Sani ";
        str += "Tag Detected";
		str += " ,  pktID = ";
		itoa(tagEvent->pktID, buf, 10);
        str += buf;
        str += " ,  ID = ";
		itoa(tagEvent->tag.id, buf, 10);
        str += buf;
        str += " ,  Type = ";
        if (tagEvent->tag.tagType == 1)
           str += "Access";
        else if (tagEvent->tag.tagType == 2)
           str += "Inventory";
        else if (tagEvent->tag.tagType == 3)
           str += "Asset";
        else if (tagEvent->tag.tagType == 4)
           str += "Car";
		else if (tagEvent->tag.tagType == 7)
           str += "Factory";
        if (tagEvent->tag.status.tamperSwitch)
           str += " ,  Tamper Switch = Active";
		str += " ,  Rdr ID = ";
		itoa(tagEvent->reader, buf, 10);
		str += buf;
		str += " ,  FG = ";
		itoa(tagEvent->fGenerator, buf, 10);
		str += buf;
		if ((tagEvent->eventType == RF_TAG_DETECTED_RSSI) ||
            (tagEvent->eventType == RF_TAG_DETECTED_SANI))
		{
		   str += " ,  FS = ";
           itoa(tagEvent->RSSI, buf, 10);
		   str += buf;
		}
        if (tagEvent->eventType == RF_TAG_DETECTED_SANI)
        {
            str += "  Unit = ";
            switch (tagEvent->tag.sani.UnitType) {
                case NoUnit:
                     str += "NoUnit";
                     break;
                case Door:
                     str += "Door";
                     break;
                case Faucet:
                     str += "Faucet";
                     break;
                case Sanitization:
                     str += "Sanitization";
                     break;
                case Contamination:
                     str += "Contamination";
                     break;
                case Bed:
                     str += "Bed";
                     break;
            }
            str += "  Status = ";
            switch (tagEvent->tag.sani.Status) {
                case Violation:
                     str += "Violation";
                     break;
                case ContaminatedOther:
                     str += "Contaminated Other";
                     break;
                case ContaminatedPatient:
                     str += "Contaminated Patient";
                     break;
                case EngagingPatient:
                     str += "Engaging Patient";
                     break;
                case ContaminatedBathroom:
                     str += "Contaminated Bathroom";
                     break;
                case Clean:
                     str += "Clean";
                     break;
                case AlcoholClean:
                     str += "Alcohol Clean";
                     break;
                case SoapClean:
                     str += "Soap Clean";
                     break;
            }
        }
        ((CListBox *)listBox)->InsertString(0, str);
		int index = ((CListBox *)listBox)->GetCount();
		MessageBeep(MB_OK);
    }
	else if (tagEvent->eventType == RF_TAG_ASSIGN_READER)
    {
        itoa(++pktCounter, buf, 10);
		str = buf;
		str += "- ";
		str += "   rfAssignTagReader()";
		str += " ,   pktID = ";
		itoa(tagEvent->pktID, buf, 10);
		str += buf;
		str += " ,   ID = ";
		itoa(tagEvent->tag.id, buf, 10);
		str += buf;
		
		str += " ,   Type = ";
        if (tagEvent->tag.tagType == 1)
           str += "Access";
        else if (tagEvent->tag.tagType == 2)
           str += "Inventory";
        else if (tagEvent->tag.tagType == 3)
           str += "Asset";
		
		((CListBox *)listBox)->InsertString(0, str);

		MessageBeep(MB_OK);
    }//RF_TAG_ASSIGN_READER
	  else if (tagEvent->eventType == RF_SET_TAG_LED_CONFIG)
    {
        itoa(++pktCounter, buf, 10);
		str = buf;
		str += "- ";
		str += "   rfSetTagLEDConfig";
		str += " ,   pktID = ";
		itoa(tagEvent->pktID, buf, 10);
        str += buf;
        str += " ,   ID = ";
		itoa(tagEvent->tag.id, buf, 10);
        str += buf;
        
		str += " ,   Type = ";
        if (tagEvent->tag.tagType == 1)
           str += "Access";
        else if (tagEvent->tag.tagType == 2)
           str += "Inventory";
        else if (tagEvent->tag.tagType == 3)
           str += "Asset";
		else if (tagEvent->tag.tagType == 7)
           str += "Factory";

		((CListBox *)listBox)->InsertString(0, str);

		MessageBeep(MB_OK);
    }//SET_TAG_LED_CONFIG
	else if (tagEvent->eventType == RF_SET_TAG_SPEAKER_CONFIG)
    {
        itoa(++pktCounter, buf, 10);
		str = buf;
		str += "- ";
		str += "   rfSetTagSpeakerConfig";
		str += " ,   pktID = ";
		itoa(tagEvent->pktID, buf, 10);
        str += buf;
        str += " ,   ID = ";
		itoa(tagEvent->tag.id, buf, 10);
        str += buf;
        
		str += " ,   Type = ";
        if (tagEvent->tag.tagType == 1)
           str += "Access";
        else if (tagEvent->tag.tagType == 2)
           str += "Inventory";
        else if (tagEvent->tag.tagType == 3)
           str += "Asset";
		else if (tagEvent->tag.tagType == 7)
           str += "Factory";

		((CListBox *)listBox)->InsertString(0, str);

		MessageBeep(MB_OK);
    }//SET_TAG_SPEAKER_CONFIG
	else if (tagEvent->eventType == RF_GET_TAG_LED_CONFIG)
    {
        itoa(++pktCounter, buf, 10);
		str = buf;
		str += "- ";
		str += "   rfGetTagLedConfig";
		str += " ,   pktID = ";
		itoa(tagEvent->pktID, buf, 10);
        str += buf;
        str += " ,   ID = ";
		itoa(tagEvent->tag.id, buf, 10);
        str += buf;
        
		str += " ,   Type = ";
        if (tagEvent->tag.tagType == 1)
           str += "Access";
        else if (tagEvent->tag.tagType == 2)
           str += "Inventory";
        else if (tagEvent->tag.tagType == 3)
           str += "Asset";
		else if (tagEvent->tag.tagType == 7)
           str += "Factory";

		str += " ,   LED = ";
		app->SetDlgItemInt(IDC_EDIT_TAG_LED, tagEvent->tag.data[0], false);
		str += itoa(tagEvent->tag.data[0], buf, 10);
		
		((CListBox *)listBox)->InsertString(0, str);

		MessageBeep(MB_OK);
    }
	else if (tagEvent->eventType == RF_GET_TAG_SPEAKER_CONFIG)
    {
        itoa(++pktCounter, buf, 10);
		str = buf;
		str += "- ";
		str += "   rfGetSpeakerLedConfig";
		str += " ,   pktID = ";
		itoa(tagEvent->pktID, buf, 10);
        str += buf;
        str += " ,   Tag ID = ";
		itoa(tagEvent->tag.id, buf, 10);
        str += buf;
        
		str += " ,   Type = ";
        if (tagEvent->tag.tagType == 1)
           str += "Access";
        else if (tagEvent->tag.tagType == 2)
           str += "Inventory";
        else if (tagEvent->tag.tagType == 3)
           str += "Asset";
		else if (tagEvent->tag.tagType == 7)
           str += "Factory";

		str += " ,   Speaker = ";
		app->SetDlgItemInt(IDC_EDIT_TAG_SPEAKER, tagEvent->tag.data[0], false);
		str += itoa(tagEvent->tag.data[0], buf, 10);

		((CListBox *)listBox)->InsertString(0, str);

		MessageBeep(MB_OK);
    }
	else if (tagEvent->eventType == RF_TAG_QUERY)
    {
		if ((tagEvent->tag.selectType == RF_SELECT_FIELD) &&
		    (tagEvent->eventStatus ==  RF_S_DONE) &&
			(tagEvent->errorStatus == RF_E_NO_ERROR))
		{
		   str = "   rfQueryTag() command completed";
		   ((CListBox *)listBox)->InsertString(0, str);
		   MessageBeep(MB_OK);
		   return (0);
		}

        itoa(++pktCounter, buf, 10);
		str = buf;
		str += "- ";
        str += "   rfQueryTag";
		str += " ,  pktID = ";
		itoa(tagEvent->pktID, buf, 10);
        str += buf;
        str += " ,  ID = ";
		itoa(tagEvent->tag.id, buf, 10);
        str += buf;
        str += " ,  Type = ";
        if (tagEvent->tag.tagType == 1)
           str += "Access";
        else if (tagEvent->tag.tagType == 2)
           str += "Inventory";
        else if (tagEvent->tag.tagType == 3)
           str += "Asset";
		else if (tagEvent->tag.tagType == 7)
           str += "Factory";

		((CListBox *)listBox)->InsertString(0, str);

		if (tagEvent->tag.status.batteryLow)
			((CListBox *)listBox)->InsertString(0, "  Battery LOW");
		else
			((CListBox *)listBox)->InsertString(0, "  Battery OK");

		if (tagEvent->tag.status.enabled)
			((CListBox *)listBox)->InsertString(0, "  Tag is enabled");
		else
			((CListBox *)listBox)->InsertString(0, "  Tag is disabled");

		if (tagEvent->tag.status.tamperSwitch)
			((CListBox *)listBox)->InsertString(0, "  Tag is Tampered");
		
		int n = tagEvent->tag.version;
		itoa(n, buf, 10);
		str = "   Tag Version is ";
		str += buf;
		((CListBox *)listBox)->InsertString(0, str);

		str = "   Tag Group Count is ";
		n = tagEvent->tag.groupCount;
		itoa(n, buf, 10);
		str += buf;
		((CListBox *)listBox)->InsertString(0, str);

		str = "  Tag Resend Time is ";
		n = tagEvent->tag.resendTime;
		itoa(n, buf, 10);
		str += buf;
		if (tagEvent->tag.resendTimeType == RF_TIME_HOUR)
           str += " hour";
		else if (tagEvent->tag.resendTimeType == RF_TIME_MINUTE)
           str += " min";
		else if (tagEvent->tag.resendTimeType == RF_TIME_SECOND)
           str += " sec";
		((CListBox *)listBox)->InsertString(0, str);

		str = "  Tag TimeInField is ";
		n =  tagEvent->tag.timeInField;
		itoa(n, buf, 10);
		str += buf;
		((CListBox *)listBox)->InsertString(0, str);
		
		MessageBeep(MB_OK);
    }
	else if (tagEvent->eventType == RF_GET_TAG_TEMP)
    {
        itoa(++pktCounter, buf, 10);
		str = buf;
		str += "- ";
		str += "   rfGetTagTemp";
		str += ",  ID = ";
		itoa(tagEvent->tag.id, buf, 10);
        str += buf;

		str += ",  Type = ";
        if (tagEvent->tag.tagType == 1)
            str += "Access";
        else if (tagEvent->tag.tagType == 2)
            str += "Inventory";
        else if (tagEvent->tag.tagType == 3)
            str += "Asset";
        else if (tagEvent->tag.tagType == 4)
            str += "Car";  
		else if (tagEvent->tag.tagType == 7)
            str += "Factory";
		
		((CListBox *)listBox)->InsertString(0, str);
		
		MessageBeep(MB_OK);
    }
	else if (tagEvent->eventType == RF_TAG_TEMP)
    {
        itoa(++pktCounter, buf, 10);
		str = buf;
		str += "- ";
		tagTemp->temperature = tagEvent->tag.temp.temperature;
		str += "   Tag Temperature = ";
		sprintf(buf, "%3.3f", tagTemp->temperature);
		str += buf;
		tagTemp->status = tagEvent->tag.temp.status;
		if (tagTemp->status == RF_TAG_TEMP_NORM)
            str += "   NORMAL";
		else if (tagTemp->status == RF_TAG_TEMP_LOW)
            str += "   LOW";
		else if (tagTemp->status == RF_TAG_TEMP_HIGH)
            str += "   HIGH";

		str += ",  ID = ";
		itoa(tagEvent->tag.id, buf, 10);
        str += buf;

		str += ",  Type = ";
        if (tagEvent->tag.tagType == 1)
            str += "Access";
        else if (tagEvent->tag.tagType == 2)
            str += "Inventory";
        else if (tagEvent->tag.tagType == 3)
            str += "Asset";
        else if (tagEvent->tag.tagType == 4)
            str += "Car";  
		else if (tagEvent->tag.tagType == 7)
            str += "Factory";
		
		((CListBox *)listBox)->InsertString(0, str);
		
		MessageBeep(MB_OK);
    }
	else if (tagEvent->eventType == RF_GET_TAG_TEMP_CONFIG)
    {
        itoa(++pktCounter, buf, 10);
		str = buf;
		str += "- ";
        str += "   rfGetTagTempConfig";
		str += " ,  pktID = ";
		itoa(tagEvent->pktID, buf, 10);
        str += buf;
        str += " ,  ID = ";
		itoa(tagEvent->tag.id, buf, 10);
        str += buf;
        
		str += " ,  Type = ";
        if (tagEvent->tag.tagType == 1)
           str += "Access";
        else if (tagEvent->tag.tagType == 2)
           str += "Inventory";
        else if (tagEvent->tag.tagType == 3)
           str += "Asset";
        else if (tagEvent->tag.tagType == 4)
           str += "Car";
		else if (tagEvent->tag.tagType == 7)
           str += "Factory";
        
		str += " ,  FG = ";
		itoa(tagEvent->fGenerator, buf, 10);
		str += buf;

        ((CListBox *)listBox)->InsertString(0, str);
		int index = ((CListBox *)listBox)->GetCount();
        
		str = "       ";
		tagTemp->rptUnderLowerLimit = tagEvent->tag.temp.rptUnderLowerLimit;
		if (tagEvent->tag.temp.rptUnderLowerLimit)
		   str += "Report Under Low Temp Limit = Yes";
		else
           str += "Report Under Low Temp Limit = No";
		((CListBox *)listBox)->InsertString(0, str);

		str = "       ";
		tagTemp->rptOverUpperLimit = tagEvent->tag.temp.rptOverUpperLimit; 
		if (tagEvent->tag.temp.rptOverUpperLimit)
		   str += "Report Over Upper Temp Limit = Yes";
		else
           str += "Report Over Upper Temp Limit = No";
		((CListBox *)listBox)->InsertString(0, str);

		str = "       ";
		tagTemp->rptPeriodicRead = tagEvent->tag.temp.rptPeriodicRead;
		if (tagEvent->tag.temp.rptPeriodicRead)
		   str += "Report Periodic Read = Yes";
		else
           str += "Report Periodic Read = No";
		((CListBox *)listBox)->InsertString(0, str);

		str = "       ";
		tagTemp->numReadAve = tagEvent->tag.temp.numReadAve;
		itoa(tagEvent->tag.temp.numReadAve, buf, 10);
		str += "Number of Reads per Average = ";
		str += buf;
		((CListBox *)listBox)->InsertString(0, str);

		str = "       ";
		tagTemp->periodicRptTime = tagEvent->tag.temp.periodicRptTime;
		itoa(tagEvent->tag.temp.periodicRptTime, buf, 10);
		str += "Periodic Report Time = ";
		str += buf;
		if (tagEvent->tag.temp.periodicTimeType == RF_TIME_HOUR)
			str += "  Hour";
		else
			str += "  Minute";
		((CListBox *)listBox)->InsertString(0, str);

		tagTemp->lowerLimitTemp = tagEvent->tag.temp.lowerLimitTemp;
		str.Format("%7.3f", tagEvent->tag.temp.lowerLimitTemp);
		str = "        Lower Limit Temp = " + str;
		((CListBox *)listBox)->InsertString(0, str);

		tagTemp->upperLimitTemp = tagEvent->tag.temp.upperLimitTemp;
		str.Format("%7.3f", tagEvent->tag.temp.upperLimitTemp);
		str = "        Upper Limit Temp = " + str;
		((CListBox *)listBox)->InsertString(0, str);

		if (tagEvent->tag.temp.enableTempLogging)
		   str = "Temp Logging Enabled";
		else
           str = "Temp Logging Disabled";
		((CListBox *)listBox)->InsertString(0, str);

		if (tagEvent->tag.temp.enableTempLogging)
		   str = "Tag IS Logging The Temperature";
		else
           str = "Tag Is NOT Logging The Temperature";
		((CListBox *)listBox)->InsertString(0, str);

		if (tagEvent->tag.temp.wrapAround)
		   str = "Tag Logging temp warp-around is Enabled.";
		else
           str = "Tag Logging temp warp-around is Disabled.";
		((CListBox *)listBox)->InsertString(0, str);
		
		MessageBeep(MB_OK);
    }
	else if (tagEvent->eventType == RF_SET_TAG_TEMP_CONFIG)
    {
        itoa(++pktCounter, buf, 10);
		str = buf;
		str += "- ";
        str += "   rfSetTagTempConfig";
		str += " ,  pktID = ";
		itoa(tagEvent->pktID, buf, 10);
        str += buf;
        str += " ,  ID = ";
		itoa(tagEvent->tag.id, buf, 10);
        str += buf;
        
		str += " ,  Type = ";
        if (tagEvent->tag.tagType == 1)
           str += "Access";
        else if (tagEvent->tag.tagType == 2)
           str += "Inventory";
        else if (tagEvent->tag.tagType == 3)
           str += "Asset";
        else if (tagEvent->tag.tagType == 4)
           str += "Car";
		else if (tagEvent->tag.tagType == 7)
           str += "Factory";

		((CListBox *)listBox)->InsertString(0, str);
        

		MessageBeep(MB_OK);
    }
	else if (tagEvent->eventType == RF_SET_TAG_TEMP_LOG_TIMESTAMP)
    {
        itoa(++pktCounter, buf, 10);
		str = buf;
		str += "- ";
        str += "   rfSetTagTempLogTimestamp";
		str += " ,  pktID = ";
		itoa(tagEvent->pktID, buf, 10);
        str += buf;
        str += " ,  ID = ";
		itoa(tagEvent->tag.id, buf, 10);
        str += buf;
        
		str += " ,  Type = ";
        if (tagEvent->tag.tagType == 1)
           str += "Access";
        else if (tagEvent->tag.tagType == 2)
           str += "Inventory";
        else if (tagEvent->tag.tagType == 3)
           str += "Asset";
        else if (tagEvent->tag.tagType == 4)
           str += "Car";
		else if (tagEvent->tag.tagType == 7)
           str += "Factory";

		((CListBox *)listBox)->InsertString(0, str);
        

		MessageBeep(MB_OK);
    }
	else if (tagEvent->eventType == RF_GET_TAG_TEMP_LOG_TIMESTAMP)
    {
        itoa(++pktCounter, buf, 10);
		str = buf;
		str += "- ";
        str += "   rfGetTagTempLogTimestamp";
		str += " ,  pktID = ";
		itoa(tagEvent->pktID, buf, 10);
        str += buf;
        str += " ,  ID = ";
		itoa(tagEvent->tag.id, buf, 10);
        str += buf;
        
		str += " ,  Type = ";
        if (tagEvent->tag.tagType == 1)
           str += "Access";
        else if (tagEvent->tag.tagType == 2)
           str += "Inventory";
        else if (tagEvent->tag.tagType == 3)
           str += "Asset";
        else if (tagEvent->tag.tagType == 4)
           str += "Car";
		else if (tagEvent->tag.tagType == 7)
           str += "Factory";

		str += " ,  Year:";
		itoa(tagEvent->logTimestamp.year, buf, 10);
        str += buf;

		str += ", Month:";
		itoa(tagEvent->logTimestamp.month, buf, 10);
        str += buf;

		str += ", Day:";
		itoa(tagEvent->logTimestamp.day, buf, 10);
        str += buf;

		str += ", Hour:";
		itoa(tagEvent->logTimestamp.hour, buf, 10);
        str += buf;

		str += ", Min:";
		itoa(tagEvent->logTimestamp.min, buf, 10);
        str += buf;

		str += ", Sec:";
		itoa(tagEvent->logTimestamp.sec, buf, 10);
        str += buf;

		((CListBox *)listBox)->InsertString(0, str);
        

		MessageBeep(MB_OK);
    }
	else if (tagEvent->eventType == RF_TAG_READ)
    {
		if ((tagEvent->tag.selectType == RF_SELECT_FIELD) &&
		    (tagEvent->eventStatus ==  RF_S_DONE) &&
			(tagEvent->errorStatus == RF_E_NO_ERROR))
		{
		   str = "   rfReadTags() command completed";
		   ((CListBox *)listBox)->InsertString(0, str);
		   MessageBeep(MB_OK);
		   return (0);
		}
			
		itoa(++pktCounter, buf, 10);
		str = buf;
		str += "- ";
		str += "   rfReadTags";
		str += " ,  pktID = ";
		itoa(tagEvent->pktID, buf, 10);
		str += buf;

		//Testing remove
		if (tagEvent->tag.id == 0)
			int stop = 0;

		str += " ,  ID = ";
		itoa(tagEvent->tag.id, buf, 10);
		str += buf;
	
		str += " ,  Type = ";
		if (tagEvent->tag.tagType == 1)
			str += "Access";
		else if (tagEvent->tag.tagType == 2)
			str += "Inventory";
		else if (tagEvent->tag.tagType == 3)
			str += "Asset";
		else if (tagEvent->tag.tagType == 4)
			str += "Car";
		else if (tagEvent->tag.tagType == 7)
			str += "Factory";
	
		str += " ,  FG = ";
		itoa(tagEvent->fGenerator, buf, 10);
		str += buf;

		((CListBox *)listBox)->InsertString(0, str);
		int index = ((CListBox *)listBox)->GetCount();
	
		str = "data: ";
		//strncpy(buf, (char*)tagEvent->tag.data, tagEvent->tag.dataLen);
		for (int i=0; i<tagEvent->tag.dataLen; i++)
		{
			s.Format("0x%02x", tagEvent->tag.data[i]);
			str += s;
			str += "  ";
		}
		
		((CListBox *)listBox)->InsertString(0, str);
	    MessageBeep(MB_OK);
    }
	else if (tagEvent->eventType == RF_TAG_WRITE)
    {
        itoa(++pktCounter, buf, 10);
		str = buf;
		str += "- ";
        str += "   rfWriteTag";
		str += " ,  pktID = ";
		itoa(tagEvent->pktID, buf, 10);
        str += buf;
        str += " ,  ID = ";
		itoa(tagEvent->tag.id, buf, 10);
        str += buf;
        
		str += " ,  Type = ";
        if (tagEvent->tag.tagType == 1)
           str += "Access";
        else if (tagEvent->tag.tagType == 2)
           str += "Inventory";
        else if (tagEvent->tag.tagType == 3)
           str += "Asset";
        else if (tagEvent->tag.tagType == 4)
           str += "Car";
		else if (tagEvent->tag.tagType == 7)
           str += "Factory";
        
		str += " ,  FG = ";
		itoa(tagEvent->fGenerator, buf, 10);
		str += buf;

        ((CListBox *)listBox)->InsertString(0, str);
		MessageBeep(MB_OK);
    }
	else if (tagEvent->eventType == RF_TAG_CALL)
    {
        itoa(++pktCounter, buf, 10);
		str = buf;
		str += "- ";
        str += "   Tag Call";
		str += " ,   pktID = ";
	    itoa(tagEvent->pktID, buf, 10);
	    str += buf;
        str += " ,  ID = ";
		itoa(tagEvent->tag.id, buf, 10);
        str += buf;
        str += " ,  Type = ";
        if (tagEvent->tag.tagType == 1)
           str += "Access";
        else if (tagEvent->tag.tagType == 2)
           str += "Inventory";
        else if (tagEvent->tag.tagType == 3)
           str += "Asset";
        else if (tagEvent->tag.tagType == 4)
           str += "Car";
		else if (tagEvent->tag.tagType == 7)
           str += "Factory";

		str += " ,  FG = ";
		itoa(tagEvent->fGenerator, buf, 10);
		str += buf;

        if (tagEvent->tag.status.tamperSwitch)
           str += " ,  Tamper Switch = Active";
		
        ((CListBox *)listBox)->InsertString(0, str);
		int index = ((CListBox *)listBox)->GetCount();
		MessageBeep(MB_OK);
    }
	else if ((tagEvent->eventType == RF_TAG_ENABLE) ||
		    (tagEvent->eventType == RF_TAG_DISABLE))
    {
		if ((tagEvent->tag.selectType == RF_SELECT_FIELD) &&
		    (tagEvent->eventStatus ==  RF_S_DONE) &&
			(tagEvent->errorStatus == RF_E_NO_ERROR))
		{
			if (tagEvent->eventType == RF_TAG_ENABLE)
		      str = "   rfEnableTags() command completed";
		    else
			  str = "   rfDisableTags() command completed";

		   ((CListBox *)listBox)->InsertString(0, str);
		   MessageBeep(MB_OK);
		   return (0);
		}
        itoa(++pktCounter, buf, 10);
		str = buf;
		str += "- ";
		if (tagEvent->eventType == RF_TAG_ENABLE)
           str += "   Tag Enabled";
		else
		   str += "   Tag Disabled";

		str += " ,   pktID = ";
	    itoa(tagEvent->pktID, buf, 10);
	    str += buf;
        str += " ,  ID = ";
		itoa(tagEvent->tag.id, buf, 10);
        str += buf;
        str += " ,  Type = ";

        if (tagEvent->tag.tagType == 1)
           str += "Access";
        else if (tagEvent->tag.tagType == 2)
           str += "Inventory";
        else if (tagEvent->tag.tagType == 3)
           str += "Asset";
		else if (tagEvent->tag.tagType == 7)
           str += "Factory";
        
		str += " ,  FG = ";
		itoa(tagEvent->fGenerator, buf, 10);
		str += buf;

        if (tagEvent->tag.status.tamperSwitch)
           str += " ,  Tamper Switch = Active";
		
        ((CListBox *)listBox)->InsertString(0, str);
		int index = ((CListBox *)listBox)->GetCount();
		MessageBeep(MB_OK);
    }
	
    return (0);
}

#ifdef _DEBUG_ACTIVEWAVE
long DebugEventFn(rfDebugEvent_t* debugEvent)
{        
	if (debugWin != NULL)
	{
       if (debugEvent->eventType == RF_DEBUG_RECV)
	   {
	      debugWin->DisplayRecPackets(debugEvent->recv, debugEvent->recvLen, (char*)debugEvent->ip, false, false);  
	   }
	   else if (debugEvent->eventType == RF_DEBUG_SEND)
	   {
	      debugWin->DisplaySendPackets(debugEvent->send, debugEvent->sendLen, (char*)debugEvent->ip, false, false);  
	   }
	}
    return(0);
}
#endif

// CTestAPIDlg message handlers

BOOL CTestAPIDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	// Add "About..." menu item to system menu.

	// IDM_ABOUTBOX must be in the system command range.
	ASSERT((IDM_ABOUTBOX & 0xFFF0) == IDM_ABOUTBOX);
	ASSERT(IDM_ABOUTBOX < 0xF000);

	CMenu* pSysMenu = GetSystemMenu(FALSE);
	if (pSysMenu != NULL)
	{
		CString strAboutMenu;
		strAboutMenu.LoadString(IDS_ABOUTBOX);
		if (!strAboutMenu.IsEmpty())
		{
			pSysMenu->AppendMenu(MF_SEPARATOR);
			pSysMenu->AppendMenu(MF_STRING, IDM_ABOUTBOX, strAboutMenu);
		}
	}

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon
	
	// TODO: Add extra initialization here
	hConn = NULL;
	comType = 0; //0=none, 1=RS232, 2=Network
	listBox = (CListBox *)GetDlgItem(IDC_LIST);
	
	pktCounter = 0;
	app = this;
	apiDlg = this;
	configSFGenDlg = false;
	tagTemp = new rfTagTemp_t;
	readerRegistered = false;
	tagRegistered = false;
	m_apiTestTab.InsertItem(0, "Communication");
	m_apiTestTab.InsertItem(1, "Reader");
	m_apiTestTab.InsertItem(2, "Tag");
	m_apiTestTab.InsertItem(3, "Smart FGen");
	m_apiTestTab.InsertItem(4, "Standard FGen");

	m_tabPages[0] = new CCommPage;
    m_tabPages[1] = new CReaderPage;
    m_tabPages[2] = new CTagPage;
	m_tabPages[3] = new CSFGENPage;
    m_tabPages[4] = new STDFGenPage;

    m_nNumberOfPages=5;
	InitTabs();

	return TRUE;  // return TRUE  unless you set the focus to a control
}

void CTestAPIDlg::OnSysCommand(UINT nID, LPARAM lParam)
{
	if ((nID & 0xFFF0) == IDM_ABOUTBOX)
	{
		CAboutDlg dlgAbout;
		dlgAbout.DoModal();
	}
	else
	{
		CDialog::OnSysCommand(nID, lParam);
	}
}

// If you add a minimize button to your dialog, you will need the code below
//  to draw the icon.  For MFC applications using the document/view model,
//  this is automatically done for you by the framework.

void CTestAPIDlg::OnPaint() 
{
	if (IsIconic())
	{
		CPaintDC dc(this); // device context for painting

		SendMessage(WM_ICONERASEBKGND, (WPARAM) dc.GetSafeHdc(), 0);

		// Center icon in client rectangle
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// Draw the icon
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialog::OnPaint();
	}
}

// The system calls this to obtain the cursor to display while the user drags
//  the minimized window.
HCURSOR CTestAPIDlg::OnQueryDragIcon()
{
	return (HCURSOR) m_hIcon;
}

void CTestAPIDlg::OnOK() 
{
	if (comType  == 1)
	{
	   if (hConn)
	   {
          rfClose(hConn);
	      delete hConn;
	      hConn = NULL;
	   }
	}

	CDialog::OnOK();
}

void CTestAPIDlg::Display(CString cmd, int pID, int ret)
{
   char buf[10];
   CString str;

   itoa(++pktCounter, buf, 10);
   str = buf;
   str += " -";
   str += "   " + cmd;
   str += "   pktID = ";
   itoa(pID, buf, 10);
   str += buf;
   str += "    return Code = ";
   itoa(ret, buf, 10);
   str += buf;
   ((CListBox *)listBox)->InsertString(0, str); 
}

void CTestAPIDlg::DisplayTag(CString cmd, int pID, int ret, int tagID, CString type)
{
   char buf[10];
   CString str; 

   itoa(++pktCounter, buf, 10);
   str = buf;
   str += " -";
   str += "   " + cmd;
   str += "  pktID=";
   itoa(pID, buf, 10);
   str += buf;
   str += "   ID=";
   str += itoa(tagID, buf, 10);
   str += "   Type=" + type;
   str += "    return Code = ";
   itoa(ret, buf, 10);
   str += buf;
   ((CListBox *)listBox)->InsertString(0, str); 
}
void CTestAPIDlg::OnClose() 
{
	//if (comType  == 1)
	{
	   if (hConn)
	   {
          rfClose(hConn);
	      delete hConn;
	      hConn = NULL;
	   }
	}
	//else if (comType == 2)
	{
       rfCloseSocket(NULL, ALL_IPS);
	}

	//delete tagSelect;

	delete tagTemp;

#ifdef _DEBUG_ACTIVEWAVE
	if (debugWin)
	   delete debugWin;
#endif

	for (int nCount=0; nCount < m_nNumberOfPages; nCount++) 
      delete m_tabPages[nCount];

	CDialog::OnClose();
}

void CTestAPIDlg::OnShowWindow(BOOL bShow, UINT nStatus)
{
	CDialog::OnShowWindow(bShow, nStatus);
}

int HexToInt(char * buf, int size)
{
   double IntNum = 0;
   int num = 0;
   int y = 0;

   buf = buf + size - 1;
   for (int i=size; i>0; i--)
   {
      if ((*buf == 'a') || (*buf == 'A'))
        num = 10;
      else if ((*buf == 'b') || (*buf == 'B'))
        num = 11;
      else if ((*buf == 'c') || (*buf == 'C'))
        num = 12;
      else if ((*buf == 'd') || (*buf == 'D'))
        num = 13;
      else if ((*buf == 'e') || (*buf == 'E'))
        num = 14;
      else if ((*buf == 'f') || (*buf == 'F'))
        num = 15;
      else if (*buf == '0')
        num = 0;
      else if (*buf == '1')
        num = 1;
      else if (*buf == '2')
        num = 2;
      else if (*buf == '3')
        num = 3;
      else if (*buf == '4')
        num = 4;
      else if (*buf == '5')
        num = 5;
      else if (*buf == '6')
        num = 6;
      else if (*buf == '7')
        num = 7;
      else if (*buf == '8')
        num = 8;
      else if (*buf == '9')
        num = 9;

      IntNum += num * pow((DOUBLE)16, y++);

      buf--;
   }

   return ((int)IntNum);
}

void CTestAPIDlg::OnTcnSelchangeTabApi(NMHDR *pNMHDR, LRESULT *pResult)
{
	// TODO: Add your control notification handler code here
	if(m_tabCurrent != m_apiTestTab.GetCurFocus())
	{
       m_tabPages[m_tabCurrent]->ShowWindow(SW_HIDE);
       m_tabCurrent = m_apiTestTab.GetCurFocus();
       m_tabPages[m_tabCurrent]->ShowWindow(SW_SHOW);
       m_tabPages[m_tabCurrent]->SetFocus();
    }

	*pResult = 0;
}

void CTestAPIDlg::InitTabs()
{
  m_tabCurrent=0;

  m_tabPages[0]->Create(IDD_DIALOG_COMM, &m_apiTestTab);
  m_tabPages[1]->Create(IDD_DIALOG_READER, &m_apiTestTab);
  m_tabPages[2]->Create(IDD_DIALOG_TAG, &m_apiTestTab);
  m_tabPages[3]->Create(IDD_DIALOG_SFGEN, &m_apiTestTab);
  m_tabPages[4]->Create(IDD_DIALOG_STDFGEN, &m_apiTestTab);

  m_tabPages[0]->ShowWindow(SW_SHOW);
  m_tabPages[1]->ShowWindow(SW_HIDE);
  m_tabPages[2]->ShowWindow(SW_HIDE);
  m_tabPages[3]->ShowWindow(SW_HIDE);
  m_tabPages[4]->ShowWindow(SW_HIDE);

  SetRectangle();
}

void CTestAPIDlg::SetRectangle()
{
   CRect tabRect, itemRect;
   int nX, nY, nXc, nYc;

   m_apiTestTab.GetClientRect(&tabRect);
   m_apiTestTab.GetItemRect(0, &itemRect);

   nX=itemRect.left;
   nY=itemRect.bottom+1;
   nXc=tabRect.right-itemRect.left-1;
   nYc=tabRect.bottom-nY-1;

   m_tabPages[0]->SetWindowPos(&wndTop, nX, nY, nXc, nYc, SWP_SHOWWINDOW);
   for(int nCount=1; nCount < m_nNumberOfPages; nCount++)
     m_tabPages[nCount]->SetWindowPos(&wndTop, nX, nY, nXc, nYc, SWP_HIDEWINDOW);
}

void CTestAPIDlg::OnBnClickedButtonClearMsgList()
{
	((CListBox *)listBox)->ResetContent();
	pktCounter = 0;
	pktID = 0;
}
