// RelayDialog.cpp : implementation file
//

#include "stdafx.h"
#include "TestAPI.h"
#include "RelayDialog.h"
#include ".\relaydialog.h"

extern unsigned short relayID;
extern bool enable;

// CRelayDialog dialog

IMPLEMENT_DYNAMIC(CRelayDialog, CDialog)
CRelayDialog::CRelayDialog(CWnd* pParent /*=NULL*/)
	: CDialog(CRelayDialog::IDD, pParent)
{
}

CRelayDialog::~CRelayDialog()
{
}

void CRelayDialog::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
}


BEGIN_MESSAGE_MAP(CRelayDialog, CDialog)
	ON_WM_ACTIVATE()
	ON_BN_CLICKED(IDOK, OnBnClickedOk)
END_MESSAGE_MAP()

void CRelayDialog::OnActivate(UINT nState, CWnd* pWndOther, BOOL bMinimized)
{
	CDialog::OnActivate(nState, pWndOther, bMinimized);	
}

void CRelayDialog::OnBnClickedOk()
{
	if (((CButton *)GetDlgItem(IDC_RADIO_RELAY1))->GetCheck())
	   relayID = 0x01;
	else 
	   relayID = 0x02;

	if (((CButton *)GetDlgItem(IDC_RADIO_ENABLE))->GetCheck())
	   enable = true;
	else 
	   enable = false;

	OnOK();
}
