#pragma once


// TestAPITab dialog

class TestAPITab : public CTabCtrl
{
	DECLARE_DYNAMIC(TestAPITab)

public:
	TestAPITab(CWnd* pParent = NULL);   // standard constructor
	virtual ~TestAPITab();

// Dialog Data
	enum { IDD = IDD_TESTAPI_DIALOG };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

public:
    int m_DialogID[2];
    CDialog m_DialogFocus;
    void ActivateTabDialogs();

	DECLARE_MESSAGE_MAP()
	afx_msg void OnTcnSelchangeTabTestApi(NMHDR *pNMHDR, LRESULT *pResult);
};
