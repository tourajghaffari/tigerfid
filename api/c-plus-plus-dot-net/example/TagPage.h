#pragma once


// CTagPage dialog

class CTagPage : public CDialog
{
	DECLARE_DYNAMIC(CTagPage)

public:
	CTagPage(CWnd* pParent = NULL);   // standard constructor
	virtual ~CTagPage();

// Dialog Data
	enum { IDD = IDD_DIALOG_TAG };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

	DECLARE_MESSAGE_MAP()
public:
	void EnableTempGroupBox(bool b);
	void EnableTempCalibGroupBox(bool b);
	void EnableLEDGroupBox(bool b);
	void EnableWriteTagGroupBox(bool b);
	void EnableReadTagGroupBox(bool b);
	afx_msg void OnBnClickedRadioTemp();
	afx_msg void OnBnClickedRadioTempCalib();
	afx_msg void OnBnClickedRadioLedConfig();
	afx_msg void OnBnClickedButtonCallTag();
	afx_msg void OnBnClickedButtonQueryTag();
	afx_msg void OnBnClickedButtonEnableTag();
	afx_msg void OnBnClickedButtonDisableTag();
	afx_msg void OnBnClickedButtonGetTempConfig();
	afx_msg void OnBnClickedButtonGetTagTempCalib();
	afx_msg void OnBnClickedButtonSetTagTempCalib();
	afx_msg void OnBnClickedButtonGetTagLedConfig();
	afx_msg void OnBnClickedButtonSetTagLedConfig();
	
	afx_msg void OnChildActivate();
	afx_msg void OnBnClickedButtonSetTempConfig();
	afx_msg void OnBnClickedButtonGetTagTemp();
	afx_msg void OnBnClickedButtonReadTag();
	afx_msg void OnBnClickedRadioWriteTag();
	afx_msg void OnBnClickedRadioReadTag();
	afx_msg void OnBnClickedButtonWriteTag();
//	afx_msg void OnBnClickedCheckModifyId();
//	afx_msg void OnBnClickedRadioConfig();
//	afx_msg void OnBnClickedCheckModifyType();
//	afx_msg void OnBnClickedCheckModifySendTime();
//	afx_msg void OnBnClickedCheckModifyRptTamper();
//	afx_msg void OnBnClickedCheckModifyFactory();
//	afx_msg void OnBnClickedButtonConfigTag();
	afx_msg void OnBnClickedCheckAnyId();
//	afx_msg void OnBnClickedCheckModifyTifgc();
	virtual BOOL OnInitDialog();
//	afx_msg void OnBnHotItemChangeButtonWriteTag(NMHDR *pNMHDR, LRESULT *pResult);
	afx_msg void OnBnClickedButtonSetTempLogTimestamp();
	afx_msg void OnBnClickedButtonGetTempLogTimestamp2();
};
