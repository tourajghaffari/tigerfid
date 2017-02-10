#pragma once
#include "afxwin.h"
#include "ActiveWaveLib.h"


// SetConfigSFgen dialog

class SetConfigSFgen : public CDialog
{
	DECLARE_DYNAMIC(SetConfigSFgen)

public:
	SetConfigSFgen(CWnd* pParent = NULL);   // standard constructor
	virtual ~SetConfigSFgen();

// Dialog Data
	enum { IDD = IDD_DIALOG_SET_CONFIG_SMART_FGEN };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnEnChangeEdit1();
	CEdit m_SFGenID;
	CEdit m_readerID;
	CEdit m_txTime;
	CEdit m_waitTime;
	CEdit m_tagID;
	CEdit m_FieldStrength;
	int m_wTimeSec;
	int pktID;
	void DisplaySFGenData(rfReaderEvent_t* readerEvent);
	void ClearFields();
	void DisplayMessage(CString msg);
	afx_msg void OnBnClickedOk();
	afx_msg void OnBnClickedButtonGet();
	afx_msg void OnBnClickedButtonSet();
	afx_msg void OnActivate(UINT nState, CWnd* pWndOther, BOOL bMinimized);
	afx_msg void OnBnClickedCheckAnyid();
};
