#pragma once


// CTemperatureDlg dialog

class CTemperatureDlg : public CDialog
{
	DECLARE_DYNAMIC(CTemperatureDlg)

public:
	CTemperatureDlg(CWnd* pParent = NULL);   // standard constructor
	virtual ~CTemperatureDlg();

// Dialog Data
	enum { IDD = IDD_DIALOG_TEMPERATURE };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnBnClickedCheckReportUnder();
	afx_msg void OnBnClickedOk();
	afx_msg void OnActivate(UINT nState, CWnd* pWndOther, BOOL bMinimized);
};
