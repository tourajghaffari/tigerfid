// STDFGenPage.cpp : implementation file
//

#include "stdafx.h"
#include "TestAPI.h"
#include "STDFGenPage.h"
#include "AWI_API.h"
#include "TestAPIDlg.h"
#include ".\stdfgenpage.h"

extern STDFGenPage* fgenSTDPage;
extern CTestAPIDlg* apiDlg;
extern int pktID;

// STDFGenPage dialog

IMPLEMENT_DYNAMIC(STDFGenPage, CDialog)
STDFGenPage::STDFGenPage(CWnd* pParent /*=NULL*/)
	: CDialog(STDFGenPage::IDD, pParent)
{
	fgenSTDPage = this;
}

STDFGenPage::~STDFGenPage()
{
}

void STDFGenPage::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
}


BEGIN_MESSAGE_MAP(STDFGenPage, CDialog)
	ON_BN_CLICKED(IDC_BUTTON_QUERY_FGEN, OnBnClickedButtonQueryFgen)
END_MESSAGE_MAP()


// STDFGenPage message handlers
void STDFGenPage::OnBnClickedButtonQueryFgen()
{
	CString s = "rfQueryFieldGenerator()";

	int fgenID = GetDlgItemInt(IDC_EDIT_FGEN, NULL,true);
	int hostID = GetDlgItemInt(IDC_EDIT_HOST, NULL,true);
    
	SetDlgItemText(IDC_EDIT_READER, "");
	SetDlgItemText(IDC_EDIT_TX, "");
	CheckDlgButton(IDC_RADIO_ENABLE_MD, 0);
	CheckDlgButton(IDC_RADIO_DISABLE_MD, 0);	
	CheckDlgButton(IDC_RADIO_MD_ACTIVE_HIGH, 0);
	CheckDlgButton(IDC_RADIO_MD_ACTIVE_LOW, 0);
	SetDlgItemText(IDC_EDIT_WT, "");
	CheckDlgButton(RF_TIME_SECOND, 0);
	CheckDlgButton(RF_TIME_MINUTE, 0);
	CheckDlgButton(RF_TIME_HOUR, 0);
	CheckDlgButton(IDC_CHECK_ANYID, 0);
	SetDlgItemText(IDC_EDIT_TAG, "");
	CheckDlgButton(IDC_RADIO_LONG_RND, 0);
	CheckDlgButton(IDC_RADIO_SHORT_RND, 0);  
	CheckDlgButton(IDC_RADIO_ACCESS, 0);
	CheckDlgButton(IDC_RADIO_ASSET, 0);
	CheckDlgButton(IDC_RADIO_INVENTORY, 0);
	CheckDlgButton(IDC_RADIO_ANYTYPE, 0);
	CheckDlgButton(IDC_CHECK_ASSIGN_READER, 0);

	int ret = rfQuerySTDFGen(hostID, fgenID, ++pktID);
	apiDlg->Display(s, pktID, ret);
	MessageBeep(0xFFFFFFFF);
}
