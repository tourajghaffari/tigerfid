// SFGENPage.cpp : implementation file
//

#include "stdafx.h"
#include "TestAPI.h"
#include "SFGENPage.h"
#include "TestAPIDlg.h"
#include "AWI_API.h"
#include ".\sfgenpage.h"

extern CSFGENPage* fgenSmartPage;
extern CTestAPIDlg* apiDlg;
extern rfTagSelect_t* tagSelect;
extern int pktCounter;
extern int pktID;

// CSFGENPage dialog

IMPLEMENT_DYNAMIC(CSFGENPage, CDialog)
CSFGENPage::CSFGENPage(CWnd* pParent /*=NULL*/)
	: CDialog(CSFGENPage::IDD, pParent)
{
	fgenSmartPage = this;
}

CSFGENPage::~CSFGENPage()
{
}

void CSFGENPage::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
}


BEGIN_MESSAGE_MAP(CSFGENPage, CDialog)
	ON_BN_CLICKED(IDC_RADIO_QUERY, OnBnClickedRadioQuery)
	ON_BN_CLICKED(IDC_RADIO_CALLTAG, OnBnClickedRadioCalltag)
	ON_BN_CLICKED(IDC_RADIO_FS, OnBnClickedRadioFs)
	ON_BN_CLICKED(IDC_BUTTON_RESET_SFGEN, OnBnClickedButtonResetSfgen)
	ON_WM_CHILDACTIVATE()
	ON_BN_CLICKED(IDC_BUTTON_QUERY_SFGEN, OnBnClickedButtonQuerySfgen)
	ON_BN_CLICKED(IDC_CHECK_QUERY_SFGEN_BCAST, OnBnClickedCheckQuerySfgenBcast)
	ON_BN_CLICKED(IDC_BUTTON_CALL_TAG_SFGEN, OnBnClickedButtonCallTagSfgen)
	ON_BN_CLICKED(IDC_CHECK_ANYID_CALL, OnBnClickedCheckAnyidCall)
	ON_BN_CLICKED(IDC_BUTTON_SET_FS_SFGEN, OnBnClickedButtonSetFsSfgen)
END_MESSAGE_MAP()


// CSFGENPage message handlers

void CSFGENPage::OnBnClickedRadioQuery()
{
	EnableQueryGroupBox(true);
	EnableCallTagGroupBox(false);
	EnableFSGroupBox(false);
}

void CSFGENPage::EnableQueryGroupBox(bool b)
{
   ((CWnd *)GetDlgItem(IDC_GROUP_QUERY))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_BUTTON_QUERY_SFGEN))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_CHECK_QUERY_SFGEN_BCAST))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_RADIO_RESP_SPECIFIC_RDR))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_RADIO_RESP_ANY_RDR))->EnableWindow(b);
}

void CSFGENPage::OnBnClickedRadioCalltag()
{
	EnableQueryGroupBox(false);
	EnableCallTagGroupBox(true);
	EnableFSGroupBox(false);
}

void CSFGENPage::OnBnClickedCheckQuerySfgenBcast()
{
	if (((CButton *)GetDlgItem(IDC_CHECK_QUERY_SFGEN_BCAST))->GetCheck() == BST_CHECKED)
	{
	   ((CWnd *)GetDlgItem(IDC_RADIO_RESP_SPECIFIC_RDR))->EnableWindow(true);
	   ((CWnd *)GetDlgItem(IDC_RADIO_RESP_ANY_RDR))->EnableWindow(true);
         
	}
	else
	{
       ((CWnd *)GetDlgItem(IDC_RADIO_RESP_SPECIFIC_RDR))->EnableWindow(false);
	   ((CWnd *)GetDlgItem(IDC_RADIO_RESP_ANY_RDR))->EnableWindow(false);
	}
}

void CSFGENPage::OnBnClickedCheckAnyidCall()
{
	if (((CButton *)GetDlgItem(IDC_CHECK_ANYID_CALL))->GetCheck() == BST_CHECKED)
       ((CWnd *)GetDlgItem(IDC_EDIT_TAG_CALL))->EnableWindow(false);
	else
	   ((CWnd *)GetDlgItem(IDC_EDIT_TAG_CALL))->EnableWindow(true);
}

void CSFGENPage::EnableCallTagGroupBox(bool b)
{
   ((CWnd *)GetDlgItem(IDC_GROUP_CALL_TAG))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_STATIC_00))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_COMB_SELECT_TYPE))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_STATIC_01))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_EDIT_TAG_CALL))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_CHECK_ANYID_CALL))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_RADIO_ACCESS_CALL))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_RADIO_ASSET_CALL))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_RADIO_INVENTORY_CALL))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_RADIO_ANYTYPE_CALL))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_BUTTON_CALL_TAG_SFGEN))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_CHECK_SFGEN_CALLTAG_BCAST))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_GROUP_INTERVAL_CALL))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_RADIO_Long_INTERVAL))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_RADIO_SHORT_INTERVAL))->EnableWindow(b);	   
}

void CSFGENPage::OnBnClickedRadioFs()
{
	EnableQueryGroupBox(false);
	EnableCallTagGroupBox(false);
	EnableFSGroupBox(true);
}

void CSFGENPage::EnableFSGroupBox(bool b)
{
   ((CWnd *)GetDlgItem(IDC_GROUP_FS))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_BUTTON_INC_FS_SFGEN))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_BUTTON_DEC_FS_SFGEN))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_BUTTON_ABS_FS_SFGEN))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_EDIT_ABS_FS_SFGEN))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_STATIC_02))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_BUTTON_SET_FS_SFGEN))->EnableWindow(b);
   ((CWnd *)GetDlgItem(IDC_CHECK_SFGEN_SETFS_BCAST))->EnableWindow(b);
}

void CSFGENPage::OnBnClickedButtonResetSfgen()
{
	char buf[64];
   unsigned short fType;
   CString s;
   UInt16 actType = 0;
   Byte value = 0;
   Boolean range = false;
   bool fGenBcast;

   int rdrID = GetDlgItemInt(IDC_EDIT_SFGEN_READER, NULL,true);
   int hostID = GetDlgItemInt(IDC_EDIT_SFGEN_HOST, NULL,true);
   int sfgenID = GetDlgItemInt(IDC_EDIT_FGEN, NULL,true);

   GetDlgItemText(IDC_COMB_READER_FUNC_TYPE, buf, 50);
   if (strcmp(buf, "SPECIFIC_READER") == 0)
   {
      fType = SPECIFIC_READER;
	  s = "rfResetSmartFGen()   Reader = ";
	  itoa(rdrID, buf, 10);
	  s += buf;
   }
   else if (strcmp(buf, "ALL_READERS") == 0)
   {
      fType = ALL_READERS;
	  s = "rfResetSmartFGen()  ALL_READERS";
   }
   else
   {
	  MessageBox("No Reader Function Type", "API", MB_ICONHAND);
      return;
   }

   
   if (((CButton *)GetDlgItem(IDC_CHECK_SFGEN_BCAST))->GetCheck() == BST_CHECKED)
   {
      fGenBcast = true;
      sfgenID = 0;
   }
   else
      fGenBcast = false;

   int ret = rfResetSmartFGen(hostID, rdrID, 0, sfgenID, fType, fGenBcast, ++pktID);
   apiDlg->Display(s, pktID, ret);
   MessageBeep(0xFFFFFFFF);
}

void CSFGENPage::OnChildActivate()
{
	CDialog::OnChildActivate();

	((CComboBox *)GetDlgItem(IDC_COMB_READER_FUNC_TYPE))->SetCurSel(1);
}

void CSFGENPage::OnBnClickedButtonQuerySfgen()
{
   char buf[64];
   unsigned short fType;
   CString s;
   UInt16 bType = 0;
   Byte value = 0;
   Boolean range = false;
   bool fGenBcast;

   int rdrID = GetDlgItemInt(IDC_EDIT_SFGEN_READER, NULL,true);
   int hostID = GetDlgItemInt(IDC_EDIT_SFGEN_HOST, NULL,true);
   int sfgenID = GetDlgItemInt(IDC_EDIT_FGEN, NULL,true);

   GetDlgItemText(IDC_COMB_READER_FUNC_TYPE, buf, 50);
   if (strcmp(buf, "SPECIFIC_READER") == 0)
   {
      fType = SPECIFIC_READER;
	  s = "rfQuerySmartFGen()   Reader = ";
	  itoa(rdrID, buf, 10);
	  s += buf;
   }
   else if (strcmp(buf, "ALL_READERS") == 0)
   {
      fType = ALL_READERS;
	  s = "rfQuerySmartFGen()  ALL_READERS";
   }
   else
   {
	  MessageBox("No Reader Function Type", "API", MB_ICONHAND);
      return;
   }

   
   if (((CButton *)GetDlgItem(IDC_CHECK_QUERY_SFGEN_BCAST))->GetCheck() == BST_CHECKED)
   {
      fGenBcast = true;
      if (((CButton *)GetDlgItem(IDC_RADIO_RESP_SPECIFIC_RDR))->GetCheck() == BST_CHECKED)
         bType = RESPOND_SPEC_RDR;
	  else
	     bType = RESPOND_ANY_RDR;
   }
   else
      fGenBcast = false;
   if (pktID >= 224)
	   pktID = 1;
   int ret = rfQuerySmartFGen(hostID, rdrID, 0, sfgenID, fType, fGenBcast, bType, ++pktID);
   apiDlg->Display(s, pktID, ret);
   MessageBeep(0xFFFFFFFF);
}

void CSFGENPage::OnBnClickedButtonCallTagSfgen()
{
	char buf[64];
    unsigned short cmdType;
	int size = 0;
	unsigned int numTag = 0;
	bool setTxTimeInt;
	bool timeInt;
	bool fGenBcast;

  	int rdrID = GetDlgItemInt(IDC_EDIT_SFGEN_READER, NULL,true);
    int hostID = GetDlgItemInt(IDC_EDIT_SFGEN_HOST, NULL,true);
	int sfgenID = GetDlgItemInt(IDC_EDIT_FGEN, NULL,true);

	CString str;
	GetDlgItemText(IDC_COMB_SELECT_TYPE, str);
	if (str.IsEmpty())
	{
       MessageBox("No Tag Function Type", "API", MB_ICONHAND);
	   return;
	}

	GetDlgItemText(IDC_COMB_READER_FUNC_TYPE, buf, 50);
    if (strcmp(buf, "SPECIFIC_READER") == 0)
      cmdType = SPECIFIC_READER;
    else if (strcmp(buf, "ALL_READERS") == 0)
      cmdType = ALL_READERS;
    else
    {
	  MessageBox("No Reader Function Type", "API", MB_ICONHAND);
      return;
    }

	if (((CButton *)GetDlgItem(IDC_CHECK_SFGEN_CALLTAG_BCAST))->GetCheck() == BST_CHECKED)
      fGenBcast = true;
    else
      fGenBcast = false;

	if(((CButton *)GetDlgItem(IDC_RADIO_Long_INTERVAL))->GetCheck() == BST_CHECKED)
	{
		setTxTimeInt = true;
		timeInt = true; //long
	}
	else if(((CButton *)GetDlgItem(IDC_RADIO_SHORT_INTERVAL))->GetCheck() == BST_CHECKED)
	{
		setTxTimeInt = true;
		timeInt = false; //short
	}
	else
	{
       setTxTimeInt = false;
	   timeInt = false; 
	}
	
	tagSelect = new rfTagSelect_t;
	if (str == "RF_SELECT_FIELD")
	{
		tagSelect->selectType = RF_SELECT_FIELD;
	    int ret = rfCallTagSmartFGen(hostID, rdrID, 0, sfgenID, cmdType, fGenBcast, tagSelect, setTxTimeInt, timeInt, ++pktID);
		apiDlg->Display("rfCallTagSmartFgen()", pktID, ret);
		delete tagSelect;
		return;
	}
	else
	{ 
	   if (str == "RF_SELECT_TAG_ID")
	   {
          tagSelect->selectType = RF_SELECT_TAG_ID;
		  if (((CButton *)GetDlgItem(IDC_CHECK_ANYID_CALL))->GetCheck() == BST_CHECKED)
		  {
			  tagSelect->tagList[0] = 0x00;  //Any tag ID
	          numTag = 0;
		  }
		  else
		  {
             UINT tagID = GetDlgItemInt(IDC_EDIT_TAG_CALL, NULL, false); 
	         if (tagID == 0)
	         {
                MessageBox("No Tag ID", "API", MB_ICONHAND);
			    delete tagSelect;
	            return;
	         }
	         else
	         {
			    tagSelect->tagList[0] = tagID;
	            numTag++;
	         }
		  }

	      tagSelect->numTags = numTag;
	   }
	   else
	      tagSelect->selectType = RF_SELECT_TAG_TYPE;


	   if (((CButton *)GetDlgItem(IDC_RADIO_ACCESS_CALL))->GetCheck() == BST_CHECKED)
	       tagSelect->tagType = ACCESS_TAG;
	   else if (((CButton *)GetDlgItem(IDC_RADIO_ASSET_CALL))->GetCheck() == BST_CHECKED)
           tagSelect->tagType = ASSET_TAG;
	   else if (((CButton *)GetDlgItem(IDC_RADIO_INVENTORY_CALL))->GetCheck() == BST_CHECKED)
           tagSelect->tagType = INVENTORY_TAG;
	   else if (((CButton *)GetDlgItem(IDC_RADIO_ANYTYPE_CALL))->GetCheck() == BST_CHECKED)
           tagSelect->tagType = ALL_TAGS;
	   else
	   {
          MessageBox("No Tag Type", "API", MB_ICONHAND);
		  delete tagSelect;
	      return;
	   }
	}
  
	int ret = rfCallTagSmartFGen(hostID, rdrID, 0, sfgenID, cmdType, fGenBcast, tagSelect, setTxTimeInt, timeInt, ++pktID);

	apiDlg->Display("rfCallTagSmartFgen()", pktID, ret);
	delete tagSelect;
}

void CSFGENPage::OnBnClickedButtonSetFsSfgen()
{
   char buf[64];
   unsigned short fType;
   CString s;
   UInt16 bType = 0;
   Byte value = 0;
   Boolean range = false;
   bool fGenBcast;

   int rdrID = GetDlgItemInt(IDC_EDIT_SFGEN_READER, NULL,true);
   int hostID = GetDlgItemInt(IDC_EDIT_SFGEN_HOST, NULL,true);
   int sfgenID = GetDlgItemInt(IDC_EDIT_FGEN, NULL,true);

   GetDlgItemText(IDC_COMB_READER_FUNC_TYPE, buf, 50);
   if (strcmp(buf, "SPECIFIC_READER") == 0)
   {
      fType = SPECIFIC_READER;
	  s = "rfSetSmartFGenFS()   Reader = ";
	  itoa(rdrID, buf, 10);
	  s += buf;
   }
   else if (strcmp(buf, "ALL_READERS") == 0)
   {
      fType = ALL_READERS;
	  s = "rfSetFSSmartFGen()  ALL_READERS";
   }
   else
   {
	  MessageBox("No Reader Function Type", "API", MB_ICONHAND);
      return;
   }

   if (((CButton *)GetDlgItem(IDC_CHECK_SFGEN_SETFS_BCAST))->GetCheck() == BST_CHECKED)
      fGenBcast = true;
   else
      fGenBcast = false;

   UInt16 actType;

   if (((CButton *)GetDlgItem(IDC_BUTTON_INC_FS_SFGEN))->GetCheck() == BST_CHECKED)
     actType = RF_INC_FS;
   else if (((CButton *)GetDlgItem(IDC_BUTTON_DEC_FS_SFGEN))->GetCheck() == BST_CHECKED)
     actType = RF_DEC_FS;
   else
   {
     actType = RF_ABS_FS;
	 value = GetDlgItemInt(IDC_EDIT_ABS_FS_SFGEN, NULL,true);
   }

   int ret = rfSetSmartFGenFS(hostID, rdrID, 0, sfgenID, fType, fGenBcast, actType, value, ++pktID);
   apiDlg->Display(s, pktID, ret);
   MessageBeep(0xFFFFFFFF);
}