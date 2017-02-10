#pragma once


// CRelayDialog dialog

class CRelayDialog : public CDialog
{
	DECLARE_DYNAMIC(CRelayDialog)

public:
	CRelayDialog(CWnd* pParent = NULL);   // standard constructor
	virtual ~CRelayDialog();

// Dialog Data
	enum { IDD = IDD_DIALOG_RELAY };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnActivate(UINT nState, CWnd* pWndOther, BOOL bMinimized);
	afx_msg void OnBnClickedOk();
};
