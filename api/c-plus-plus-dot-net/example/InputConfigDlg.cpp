// InputConfigDlg.cpp : implementation file
//

#include "stdafx.h"
#include "TestAPI.h"
#include "InputConfigDlg.h"
#include "ActiveWAveLib.h"
#include ".\inputconfigdlg.h"


extern unsigned short config;
extern unsigned short config2;
extern bool supervised;

// CInputConfigDlg dialog

IMPLEMENT_DYNAMIC(CInputConfigDlg, CDialog)
CInputConfigDlg::CInputConfigDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CInputConfigDlg::IDD, pParent)
{
}

CInputConfigDlg::~CInputConfigDlg()
{
}

void CInputConfigDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
}


BEGIN_MESSAGE_MAP(CInputConfigDlg, CDialog)
	ON_BN_CLICKED(IDOK, OnBnClickedOk)
END_MESSAGE_MAP()


// CInputConfigDlg message handlers

void CInputConfigDlg::OnBnClickedOk()
{
	// TODO: Add your control notification handler code here
	
	if(((CButton *)GetDlgItem(IDC_RADIO_IGNOR_INPUT1))->GetCheck() == BST_CHECKED)
	   config = IGNOR_INPUT_CHANGE;
	else if(((CButton *)GetDlgItem(IDC_RADIO_REPORT_INPUT1))->GetCheck() == BST_CHECKED)
	   config = REPORT_INPUT_CHANGE;
	else
	   config = NO_CHANGE_INPUT;

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

	OnOK();
}
