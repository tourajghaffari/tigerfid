// TestAPITab.cpp : implementation file
//

#include "stdafx.h"
#include "TestAPI.h"
#include "TestAPITab.h"
#include "CommPage.h"
#include ".\testapitab.h"


// TestAPITab dialog

IMPLEMENT_DYNAMIC(TestAPITab, CTabCtrl)
TestAPITab::TestAPITab(CWnd* pParent /*=NULL*/)
	: CTabCtrl(TestAPITab::IDD, pParent)
{
	m_DialogID[0] = IDD_DIALOG_COMM;
}

TestAPITab::~TestAPITab()
{
}

void TestAPITab::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
}


BEGIN_MESSAGE_MAP(TestAPITab, CDialog)
	ON_NOTIFY(TCN_SELCHANGE, IDC_TAB_TEST_API, OnTcnSelchangeTabTestApi)
END_MESSAGE_MAP()


// TestAPITab message handlers

void TestAPITab::ActivateTabDialogs()
{
   int nSel = GetCurSel();
   if(m_DialogFocus.m_hWnd)
       m_DialogFocus.DestroyWindow();
//Create the dialog on the CTabCtrl
   m_DialogFocus.Create(m_DialogID[nSel],GetParent());

   CRect l_rectClient;
   CRect l_rectWnd;

   GetClientRect(l_rectClient);
   AdjustRect(FALSE,l_rectClient);
   GetWindowRect(l_rectWnd);
   GetParent()->ScreenToClient(l_rectWnd);
   l_rectClient.OffsetRect(l_rectWnd.left,l_rectWnd.top);
   m_DialogFocus.SetWindowPos(&wndTop, l_rectClient.left, l_rectClient.top, l_rectClient.Width(), l_rectClient.Height(), SWP_SHOWWINDOW);
}
void TestAPITab::OnTcnSelchangeTabTestApi(NMHDR *pNMHDR, LRESULT *pResult)
{
	// TODO: Add your control notification handler code here
	ActivateTabDialogs();
	*pResult = 0;
}
