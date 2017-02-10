// SetConfigSFgen.cpp : implementation file
//

#include "stdafx.h"
#include "TestAPI.h"
#include "SetConfigSFgen.h"
#include "TestAPIDlg.h"
#include ".\setconfigsfgen.h"


// SetConfigSFgen dialog
extern rfSmartFGen_t* smartFGen;
extern CWnd* app;
extern CTestAPIDlg* apiDlg;
extern SetConfigSFgen* CfgSFGenDlg;
extern bool configSFGenDlg;

IMPLEMENT_DYNAMIC(SetConfigSFgen, CDialog)
SetConfigSFgen::SetConfigSFgen(CWnd* pParent /*=NULL*/)
	: CDialog(SetConfigSFgen::IDD, pParent)
	//, m_wTimeSec(0)
{
	CfgSFGenDlg = this;
	pktID = 1;
}

SetConfigSFgen::~SetConfigSFgen()
{
}

void SetConfigSFgen::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_EDIT1, m_SFGenID);
	DDX_Control(pDX, IDC_EDIT2, m_readerID);
	DDX_Control(pDX, IDC_EDIT3, m_txTime);
	DDX_Control(pDX, IDC_EDIT4, m_waitTime);
	DDX_Control(pDX, IDC_EDIT5, m_tagID);
	DDX_Control(pDX, IDC_EDIT6, m_FieldStrength);
}


BEGIN_MESSAGE_MAP(SetConfigSFgen, CDialog)
	ON_EN_CHANGE(IDC_EDIT1, OnEnChangeEdit1)
	ON_BN_CLICKED(IDOK, OnBnClickedOk)
	ON_BN_CLICKED(ID_BUTTON_GET, OnBnClickedButtonGet)
	ON_BN_CLICKED(ID_BUTTON_SET, OnBnClickedButtonSet)
	ON_WM_ACTIVATE()
	ON_BN_CLICKED(IDC_CHECK_ANYID, OnBnClickedCheckAnyid)
END_MESSAGE_MAP()


// SetConfigSFgen message handlers

void SetConfigSFgen::OnEnChangeEdit1()
{
	// TODO:  If this is a RICHEDIT control, the control will not
	// send this notification unless you override the CDialog::OnInitDialog()
	// function and call CRichEditCtrl().SetEventMask()
	// with the ENM_CHANGE flag ORed into the mask.

	// TODO:  Add your control notification handler code here
}

void SetConfigSFgen::OnBnClickedOk()
{
	OnOK();
}

void SetConfigSFgen::OnBnClickedButtonGet()
{
   unsigned short fType;
   UInt16 bType = 0;
   bool fGenBcast;
   int ret = 0;

   int rdrID = app->GetDlgItemInt(IDC_EDIT_READER_ID, NULL, true);
   int hostID = app->GetDlgItemInt(IDC_EDIT_HOST_ID, NULL, true);
   int fgenID = GetDlgItemInt(IDC_EDIT1, NULL, true); 

   fType = SPECIFIC_READER;
   fGenBcast = true;
   bType = RESPOND_ANY_RDR;
   
   ClearFields();

   SetDlgItemText(IDC_STATIC_MSG, "Message: Waiting for data .....");
   if (configSFGenDlg)
      ret = rfQuerySmartFGen(hostID, rdrID, 0, fgenID, fType, fGenBcast, bType, ++pktID);
   else
      ret = rfQuerySTDFGen(hostID, fgenID, ++pktID);
   MessageBeep(0xFFFFFFFF);
}

void SetConfigSFgen::DisplaySFGenData(rfReaderEvent_t* readerEvent)
{
   SetDlgItemText(IDC_STATIC_MSG, "Message: Data Received.");
   SetDlgItemInt(IDC_EDIT1, readerEvent->smartFgen.ID, false);
   SetDlgItemInt(IDC_EDIT2, readerEvent->smartFgen.readerID, false);
   
   SetDlgItemInt(IDC_EDIT3, readerEvent->smartFgen.txTime, false);
   SetDlgItemInt(IDC_EDIT4, readerEvent->smartFgen.waitTime, false);
   if (readerEvent->smartFgen.wTimeType == RF_TIME_SECOND)
     ((CButton *)GetDlgItem(IDC_RADIO_SEC))->SetCheck(true);
   else if (readerEvent->smartFgen.wTimeType == RF_TIME_MINUTE)
     ((CButton *)GetDlgItem(IDC_RADIO_MIN))->SetCheck(true);
   else if (readerEvent->smartFgen.wTimeType == RF_TIME_HOUR)
     ((CButton *)GetDlgItem(IDC_RADIO_HOUR))->SetCheck(true);
   
   if (readerEvent->smartFgen.tagID == 0)
      ((CButton *)GetDlgItem(IDC_CHECK_ANYID))->SetCheck(true); 
   else
      SetDlgItemInt(IDC_EDIT5, readerEvent->smartFgen.tagID, false);
   if (readerEvent->smartFgen.tagType == ACCESS_TAG)
     ((CButton *)GetDlgItem(IDC_RADIO_ACCESS))->SetCheck(true);
   else if (readerEvent->smartFgen.tagType == ASSET_TAG)
     ((CButton *)GetDlgItem(IDC_RADIO_ASSET))->SetCheck(true);
   else if (readerEvent->smartFgen.tagType == INVENTORY_TAG)
     ((CButton *)GetDlgItem(IDC_RADIO_INVENTORY))->SetCheck(true);
   else
     ((CButton *)GetDlgItem(IDC_RADIO_ANYTYPE))->SetCheck(true);

   SetDlgItemInt(IDC_EDIT6, readerEvent->smartFgen.fsValue, false);
   
   if (readerEvent->smartFgen.longDistance)
      ((CButton *)GetDlgItem(IDC_RADIO_LONG_INT))->SetCheck(true);
   else
	  ((CButton *)GetDlgItem(IDC_RADIO_SHORT_INT))->SetCheck(true);

   if (readerEvent->smartFgen.mDetectStatus)
      ((CButton *)GetDlgItem(IDC_RADIO_MD_ENABLE))->SetCheck(true);
   else
	  ((CButton *)GetDlgItem(IDC_RADIO_MD_DISABLE))->SetCheck(true);

   if (readerEvent->smartFgen.mDetectActive)
      ((CButton *)GetDlgItem(IDC_RADIO_MD_ACTIVE_HIGH))->SetCheck(true);
   else
      ((CButton *)GetDlgItem(IDC_RADIO_MD_ACTIVE_LOW))->SetCheck(true);

   if (readerEvent->smartFgen.assignRdr)
      ((CButton *)GetDlgItem(IDC_CHECK_ASSIGN_READER))->SetCheck(true);
   else
      ((CButton *)GetDlgItem(IDC_CHECK_ASSIGN_READER))->SetCheck(false);

   if (readerEvent->smartFgen.longInterval)
   {
      ((CButton *)GetDlgItem(IDC_RADIO_LONG_RND))->SetCheck(true);
	  ((CButton *)GetDlgItem(IDC_RADIO_SHORT_RND))->SetCheck(false);
   }
   else
   {
      ((CButton *)GetDlgItem(IDC_RADIO_LONG_RND))->SetCheck(false);
	  ((CButton *)GetDlgItem(IDC_RADIO_SHORT_RND))->SetCheck(true);
   }
}

void SetConfigSFgen::ClearFields()
{
   SetDlgItemText(IDC_EDIT1, "");
   SetDlgItemText(IDC_EDIT2, "");
   
   SetDlgItemText(IDC_EDIT3, "");
   SetDlgItemText(IDC_EDIT4, "");
   ((CButton *)GetDlgItem(IDC_RADIO_SEC))->SetCheck(false);
   ((CButton *)GetDlgItem(IDC_RADIO_MIN))->SetCheck(false);
   ((CButton *)GetDlgItem(IDC_RADIO_HOUR))->SetCheck(false);
   SetDlgItemText(IDC_EDIT5, "");
   ((CButton *)GetDlgItem(IDC_RADIO_ACCESS))->SetCheck(false);
   ((CButton *)GetDlgItem(IDC_RADIO_ASSET))->SetCheck(false);
   ((CButton *)GetDlgItem(IDC_RADIO_INVENTORY))->SetCheck(false);
   ((CButton *)GetDlgItem(IDC_RADIO_ANYTYPE))->SetCheck(false);
   SetDlgItemText(IDC_EDIT6, "");
   ((CButton *)GetDlgItem(IDC_RADIO_MD_ENABLE))->SetCheck(false);
   ((CButton *)GetDlgItem(IDC_RADIO_MD_DISABLE))->SetCheck(false);
   ((CButton *)GetDlgItem(IDC_RADIO_MD_ACTIVE_HIGH))->SetCheck(false);
   ((CButton *)GetDlgItem(IDC_RADIO_MD_ACTIVE_LOW))->SetCheck(false);
   ((CButton *)GetDlgItem(IDC_CHECK_ANYID))->SetCheck(false);
   ((CButton *)GetDlgItem(IDC_RADIO_LONG_INT))->SetCheck(false);
   ((CButton *)GetDlgItem(IDC_RADIO_SHORT_INT))->SetCheck(false); 
}
void SetConfigSFgen::OnBnClickedButtonSet()
{
	int pktID = 0x0A;
	int rdrID = 0;
	if (configSFGenDlg)
	   rdrID = app->GetDlgItemInt(IDC_EDIT_READER_ID, NULL,true);
    int hostID = app->GetDlgItemInt(IDC_EDIT_HOST_ID, NULL,true);
	int fGenID = app->GetDlgItemInt(IDC_EDIT_FGEN_ID, NULL,true);
	unsigned short cmdType = SPECIFIC_READER;
	unsigned short configType = ALL_PARAMS;
	
	rfSmartFGen_t* smartFGen = new rfSmartFGen_t;
	
	smartFGen->ID = GetDlgItemInt(IDC_EDIT1, NULL, false); 
	smartFGen->readerID = GetDlgItemInt(IDC_EDIT2, NULL, false);
	smartFGen->txTime = GetDlgItemInt(IDC_EDIT3, NULL, false);
    smartFGen->waitTime = GetDlgItemInt(IDC_EDIT4, NULL, false);
	
	if (((CButton *)GetDlgItem(IDC_CHECK_ANYID))->GetCheck() == BST_CHECKED)
	   smartFGen->tagID = 0x00;
	else
	   smartFGen->tagID = GetDlgItemInt(IDC_EDIT5, NULL, false);
	
	if (configSFGenDlg)
	{
	    smartFGen->fsValue = GetDlgItemInt(IDC_EDIT6, NULL, false);
    
	    if (((CButton *)GetDlgItem(IDC_RADIO_LONG_INT))->GetCheck() == BST_CHECKED)
		   smartFGen->longDistance = true;
	    else
           smartFGen->longDistance = false;
	}

	if (((CButton *)GetDlgItem(IDC_RADIO_ACCESS))->GetCheck() == BST_CHECKED)
	   smartFGen->tagType = ACCESS_TAG;
	else if (((CButton *)GetDlgItem(IDC_RADIO_ASSET))->GetCheck() == BST_CHECKED)
	   smartFGen->tagType = ASSET_TAG;
	else if (((CButton *)GetDlgItem(IDC_RADIO_INVENTORY))->GetCheck() == BST_CHECKED)
	   smartFGen->tagType = INVENTORY_TAG;
	else
	   smartFGen->tagType = ALL_TAGS;

	if (((CButton *)GetDlgItem(IDC_RADIO_MD_ENABLE))->GetCheck() == BST_CHECKED)
	   smartFGen->mDetectStatus = true;
	else
	   smartFGen->mDetectStatus = false;

	if (((CButton *)GetDlgItem(IDC_CHECK_ASSIGN_READER))->GetCheck() == BST_CHECKED)
	   smartFGen->assignRdr = true;
	else
	   smartFGen->assignRdr = false;

	if (((CButton *)GetDlgItem(IDC_RADIO_LONG_RND))->GetCheck() == BST_CHECKED)
	   smartFGen->longInterval = true;
	else
	   smartFGen->longInterval = false;

	if (((CButton *)GetDlgItem(IDC_RADIO_MD_ACTIVE_HIGH))->GetCheck() == BST_CHECKED)
	   smartFGen->mDetectActive = true;
	else
	   smartFGen->mDetectActive = false;

	if (((CButton *)GetDlgItem(IDC_RADIO_SEC))->GetCheck() == BST_CHECKED)
	   smartFGen->wTimeType = RF_TIME_SECOND;
	else if (((CButton *)GetDlgItem(IDC_RADIO_MIN))->GetCheck() == BST_CHECKED)
	   smartFGen->wTimeType = RF_TIME_MINUTE;
	else if (((CButton *)GetDlgItem(IDC_RADIO_HOUR))->GetCheck() == BST_CHECKED)
	   smartFGen->wTimeType = RF_TIME_HOUR;

	SetDlgItemText(IDC_STATIC_MSG, "Message: Waiting for ack .....");

	int ret=0;
	if (configSFGenDlg)
	{
	   ret = rfSetConfigSmartFGen(hostID, rdrID, 0, fGenID, cmdType, smartFGen, pktID);
	   apiDlg->Display("rfSetConfigSmartFGen()", pktID, ret);
	}
	else
	{
      ret = rfConfigSTDFGen(hostID, fGenID, configType, smartFGen, pktID);
      apiDlg->Display("rfConfigSTDFGen()", pktID, ret);
	}
	

	delete smartFGen;

}

void SetConfigSFgen::DisplayMessage(CString msg)
{
	SetDlgItemText(IDC_STATIC_MSG, msg);
}

void SetConfigSFgen::OnActivate(UINT nState, CWnd* pWndOther, BOOL bMinimized)
{
	CDialog::OnActivate(nState, pWndOther, bMinimized);

	SetDlgItemInt(IDC_STATIC_READER_ID, app->GetDlgItemInt(IDC_EDIT_READER_ID, NULL,true), false);
	SetDlgItemInt(IDC_STATIC_HOST_ID, app->GetDlgItemInt(IDC_EDIT_HOST_ID, NULL,true), false);
	SetDlgItemInt(IDC_STATIC_SFGEN_ID, app->GetDlgItemInt(IDC_EDIT_FGEN_ID, NULL,true), false);
	SetDlgItemInt(IDC_EDIT1, app->GetDlgItemInt(IDC_EDIT_FGEN_ID, NULL,true), false);

	if (!configSFGenDlg)
	{
	   ((CWnd *)GetDlgItem(IDC_STATIC_FS))->ShowWindow(false);
       ((CWnd *)GetDlgItem(IDC_EDIT6))->ShowWindow(false);
	   ((CWnd *)GetDlgItem(IDC_RADIO_LONG_INT))->ShowWindow(false);
	   ((CWnd *)GetDlgItem(IDC_RADIO_SHORT_INT))->ShowWindow(false);
	}
}

void SetConfigSFgen::OnBnClickedCheckAnyid()
{
	if (((CButton *)GetDlgItem(IDC_CHECK_ANYID))->GetCheck() == BST_CHECKED)
		((CWnd *)GetDlgItem(IDC_EDIT5))->EnableWindow(false);
	else
		((CWnd *)GetDlgItem(IDC_EDIT5))->EnableWindow(true);
}
