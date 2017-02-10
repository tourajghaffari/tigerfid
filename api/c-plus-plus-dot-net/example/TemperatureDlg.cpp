// TemperatureDlg.cpp : implementation file
//

#include "stdafx.h"
#include "TestAPI.h"
#include "TemperatureDlg.h"
#include "ActiveWaveLib.h"
#include ".\temperaturedlg.h"

extern rfTagTemp_t* tagTemp;

// CTemperatureDlg dialog

IMPLEMENT_DYNAMIC(CTemperatureDlg, CDialog)
CTemperatureDlg::CTemperatureDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CTemperatureDlg::IDD, pParent)
{
}

CTemperatureDlg::~CTemperatureDlg()
{
}

void CTemperatureDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
}


BEGIN_MESSAGE_MAP(CTemperatureDlg, CDialog)
	ON_BN_CLICKED(IDOK, OnBnClickedOk)
	ON_WM_ACTIVATE()
END_MESSAGE_MAP()

void CTemperatureDlg::OnBnClickedOk()
{
	if(((CButton *)GetDlgItem(IDC_CHECK_REPORT_UNDER))->GetCheck())
	   tagTemp->rptUnderLowerLimit = true;
	else
	   tagTemp->rptUnderLowerLimit = false;

	if(((CButton *)GetDlgItem(IDC_CHECK_REPORT_UPPER))->GetCheck())
	   tagTemp->rptOverUpperLimit = true;
	else
	   tagTemp->rptOverUpperLimit = false;

	if(((CButton *)GetDlgItem(IDC_CHECK_REPORT_PERIODIC))->GetCheck())
	   tagTemp->rptPeriodicRead = true;
	else
	   tagTemp->rptPeriodicRead = false;

	tagTemp->numReadAve = GetDlgItemInt(IDC_COMBO_NUM_READ, NULL, false);

	tagTemp->periodicRptTime = GetDlgItemInt(IDC_EDIT_PERIODIC_REPORT_TIME, NULL, false);
   
	if(((CButton *)GetDlgItem(IDC_RADIO_TIME_HOUR))->GetCheck())
       tagTemp->periodicTimeType = RF_TIME_HOUR;
	else
	   tagTemp->periodicTimeType = RF_TIME_MINUTE;
		
	char buf[11];
	GetDlgItemText(IDC_EDIT_UP_LIMIT_TEMP, buf, 10);
	tagTemp->upperLimitTemp = (float)atof(buf);
	GetDlgItemText(IDC_EDIT_LOW_LIMIT_TEMP, buf, 10);
	tagTemp->lowerLimitTemp = (float)atof(buf);

	OnOK();
}

void CTemperatureDlg::OnActivate(UINT nState, CWnd* pWndOther, BOOL bMinimized)
{
	CDialog::OnActivate(nState, pWndOther, bMinimized);

   ((CButton *)GetDlgItem(IDC_CHECK_REPORT_UNDER))->SetCheck(tagTemp->rptUnderLowerLimit);
   ((CButton *)GetDlgItem(IDC_CHECK_REPORT_UPPER))->SetCheck(tagTemp->rptOverUpperLimit);
   ((CButton *)GetDlgItem(IDC_CHECK_REPORT_PERIODIC))->SetCheck(tagTemp->rptPeriodicRead);
   char buf[10];
   sprintf(buf, "%3.3f", tagTemp->lowerLimitTemp);
   SetDlgItemText(IDC_EDIT_LOW_LIMIT_TEMP, buf);
   sprintf(buf, "%3.3f", tagTemp->upperLimitTemp);
   SetDlgItemText(IDC_EDIT_UP_LIMIT_TEMP, buf);
   SetDlgItemInt(IDC_COMBO_NUM_READ, tagTemp->numReadAve, false);
   SetDlgItemInt(IDC_EDIT_PERIODIC_REPORT_TIME, tagTemp->periodicRptTime, false);
   if (tagTemp->periodicTimeType == RF_TIME_HOUR)
     ((CButton *)GetDlgItem(IDC_RADIO_TIME_HOUR))->SetCheck(true);
   else
     ((CButton *)GetDlgItem(IDC_RADIO_TIME_MINUTE))->SetCheck(true);
}
