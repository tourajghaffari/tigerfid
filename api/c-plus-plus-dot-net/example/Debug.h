#pragma once
#include "afxwin.h"


// CDebug dialog

class CDebug : public CDialog
{
	DECLARE_DYNAMIC(CDebug)

public:
	CDebug(CWnd* pParent = NULL);   // standard constructor
	virtual ~CDebug();

// Dialog Data
	enum { IDD = IDD_DIALOG_DEBUG };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

	DECLARE_MESSAGE_MAP()
public:
	CListBox m_recvList;
	CListBox m_sendList;
	int count;
	void DisplaySendPackets(char buf[260], int len, char ip[20], bool frameFlag, bool crcFlag);
	void DisplayRecPackets(char buf[260], int len, char ip[20], bool frameFlag, bool crcFlag);
	afx_msg void OnBnClickedButtonRxClear();
	afx_msg void OnBnClickedButtonTxClear();
	afx_msg void OnBnClickedButtonRxStop();
	CButton m_rxStop;
	CButton m_txStop;
	bool rxStopDebug;
	bool txStopDebug;
	afx_msg void OnBnClickedButtonTxStop();
//	afx_msg void OnClose();
	afx_msg void OnBnClickedButtonDebugClose();
};
