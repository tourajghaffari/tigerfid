#pragma once


// CSFGENPage dialog

class CSFGENPage : public CDialog
{
	DECLARE_DYNAMIC(CSFGENPage)

public:
	CSFGENPage(CWnd* pParent = NULL);   // standard constructor
	virtual ~CSFGENPage();

// Dialog Data
	enum { IDD = IDD_DIALOG_SFGEN };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

	DECLARE_MESSAGE_MAP()
public:
	void EnableQueryGroupBox(bool b);
	void EnableCallTagGroupBox(bool b);
	void EnableFSGroupBox(bool b);
	void EnableConfigGroupBox(bool b);
	afx_msg void OnBnClickedRadioQuery();
	afx_msg void OnBnClickedRadioCalltag();
	afx_msg void OnBnClickedRadioFs();
//	afx_msg void OnBnClickedRadioConfig();
	afx_msg void OnBnClickedButtonResetSfgen();
	afx_msg void OnChildActivate();
	afx_msg void OnBnClickedButtonQuerySfgen();
	afx_msg void OnBnClickedCheckQuerySfgenBcast();
	afx_msg void OnBnClickedButtonCallTagSfgen();
	afx_msg void OnBnClickedCheckAnyidCall();
	afx_msg void OnBnClickedButtonSetFsSfgen();
//	afx_msg void OnBnClickedButtonGetFsSfgen();
//	afx_msg void OnBnClickedButtonSetConfigSfgen();
//	afx_msg void OnBnClickedCheckAnyid();
};
