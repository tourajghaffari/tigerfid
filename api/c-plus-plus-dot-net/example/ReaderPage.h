#pragma once


// CReaderPage dialog

class CReaderPage : public CDialog
{
	DECLARE_DYNAMIC(CReaderPage)

public:
	CReaderPage(CWnd* pParent = NULL);   // standard constructor
	virtual ~CReaderPage();

// Dialog Data
	enum { IDD = IDD_DIALOG_READER };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnBnClickedRadioNewConfig();
	afx_msg void OnBnClickedRadioInput();
	afx_msg void OnBnClickedRadioFs();
	afx_msg void OnBnClickedRadioRelay();
	afx_msg void OnBnClickedRadioEnableDisable();
	void EnableNewConfigGroupBox(bool b);
	void EnableInputGroupBox(bool b);
	void EnableFSGroupBox(bool b);
	void EnableRelayGroupBox(bool b);
	void EnableEnableRdrGroupBox(bool b);
	afx_msg void OnBnClickedButtonRfRegReader();
	afx_msg void OnBnClickedButtonEnableReader();
	afx_msg void OnBnClickedButtonEnableRelay();
	afx_msg void OnBnClickedButtonGetRdrFs();
	afx_msg void OnBnClickedButtonSetRdrFs();
	afx_msg void OnBnClickedButtonGetInput();
	afx_msg void OnBnClickedButtonConfigInputPort();
//	afx_msg void OnBnClickedButtonGetReaderCfg();
	afx_msg void OnChildActivate();
	afx_msg void OnBnClickedCheckRdrCfgHostId();
	afx_msg void OnBnClickedCheckRdrCfgRdrId();
//	afx_msg void OnBnClickedCheckRdrCfgOptions();
	afx_msg void OnBnClickedCheckRdrCfgTx();
	afx_msg void OnBnClickedCheckRdrCfgFgen();
	afx_msg void OnBnClickedButtonConfigNewRdr();
//	afx_msg void OnBnClickedCheckRdrCfgType();
	afx_msg void OnBnClickedButtonGetReaderVersion();
	afx_msg void OnClose();
};
