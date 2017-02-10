#pragma once
#include "afxwin.h"


// CCommPage dialog

class CCommPage : public CDialog
{
	DECLARE_DYNAMIC(CCommPage)

public:
	CCommPage(CWnd* pParent = NULL);   // standard constructor
	virtual ~CCommPage();

// Dialog Data
	enum { IDD = IDD_DIALOG_COMM };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

	DECLARE_MESSAGE_MAP()
public:
	void EnableRS232Group(bool b);
	void EnableNetworkGroup(bool b);
	void CloseDebugWindow();
	afx_msg void OnBnClickedButtonRfRegReader();
	afx_msg void OnBnClickedRadioRs232();
	afx_msg void OnBnClickedRadioNetwork();
	afx_msg void OnBnClickedButtonRfRegTag();
	CStatic m_commGroup;
	CButton m_rs232Button;
	CWnd* listIPBox;
	afx_msg void OnBnClickedButtonRfOpen();
	afx_msg void OnBnClickedButtonRfClose();
//	afx_msg void OnBnClickedButtonRfRegDebugEvent();
	afx_msg void OnChildActivate();
//	afx_msg void OnBnClickedButtonStartDebug();
	CListBox m_ipList;
	afx_msg void OnBnClickedButtonRfOpenSocket();
	afx_msg void OnBnClickedButtonRfCloseSocket();
//	afx_msg void OnBnClickedButtonResetReaderSocket();
	afx_msg void OnActivateApp(BOOL bActive, DWORD dwThreadID);
//	afx_msg void OnBnClickedButtonChangeIp();
	afx_msg void OnBnClickedButtonScanNetwork();
	afx_msg void OnBnClickedButtonRfCloseSocket2();
	afx_msg void OnBnClickedButtonRfScanIp();
	CEdit m_ip;
	afx_msg void OnBnClickedRadioRadioSpecificIp();
	afx_msg void OnBnClickedRadioAllIps();
	afx_msg void OnBnClickedButtonRfResetReaderSocket();
};
