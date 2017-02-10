#pragma once


// CInputConfigDlg dialog

class CInputConfigDlg : public CDialog
{
	DECLARE_DYNAMIC(CInputConfigDlg)

public:
	CInputConfigDlg(CWnd* pParent = NULL);   // standard constructor
	virtual ~CInputConfigDlg();

// Dialog Data
	enum { IDD = IDD_DIALOG_INPUT_CONFIG };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnBnClickedOk();
};
