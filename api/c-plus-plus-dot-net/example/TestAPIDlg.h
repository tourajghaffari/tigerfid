//---------------------------------------------------------------------------------------
//     Version 39
//---------------------------------------------------------------------------------------

// TestAPIDlg.h : header file
//

#include "afxcmn.h"
#if !defined(AFX_TESTAPIDLG_H__57FB3032_2AA2_4E19_803D_B000405A9441__INCLUDED_)
#define AFX_TESTAPIDLG_H__57FB3032_2AA2_4E19_803D_B000405A9441__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

/////////////////////////////////////////////////////////////////////////////
// CTestAPIDlg dialog

class CTestAPIDlg : public CDialog
{
// Construction
public:
	unsigned short comType; //0=none, 1=RS232, 2=Network
	bool readerRegistered;
	bool tagRegistered;
	CTestAPIDlg(CWnd* pParent = NULL);	// standard constructor
	void Display(CString cmd, int pID, int ret);
	void DisplayTag(CString cmd, int pID, int ret, int tagID, CString type);

// Dialog Data
	//{{AFX_DATA(CTestAPIDlg)
	enum { IDD = IDD_TESTAPI_DIALOG };
		// NOTE: the ClassWizard will add data members here
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CTestAPIDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:
	HICON m_hIcon;

	// Generated message map functions
	//{{AFX_MSG(CTestAPIDlg)
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	virtual void OnOK();
	afx_msg void OnClose();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnShowWindow(BOOL bShow, UINT nStatus);
	afx_msg void OnTcnSelchangeTab2(NMHDR *pNMHDR, LRESULT *pResult);
	afx_msg void OnTcnSelchangeTabTestApi(NMHDR *pNMHDR, LRESULT *pResult);
	afx_msg void OnTcnSelchangeTabApi(NMHDR *pNMHDR, LRESULT *pResult);
	CTabCtrl m_apiTestTab;
	//Array to hold the list of dialog boxes/tab pages for CTabCtrl
    CDialog* m_tabPages[5];
    //Variable to hold the dialog in focus
    CDialog m_DialogFocus;
    //Function to activate the tab dialog boxes 
    void InitTabs();
	void SetRectangle();
	int m_tabCurrent;
	int m_nNumberOfPages;
	afx_msg void OnBnClickedButtonClearMsgList();
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_TESTAPIDLG_H__57FB3032_2AA2_4E19_803D_B000405A9441__INCLUDED_)
