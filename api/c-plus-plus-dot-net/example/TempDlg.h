#pragma once


// CTempDlg dialog

class CTempDlg : public CDHtmlDialog
{
	DECLARE_DYNCREATE(CTempDlg)

public:
	CTempDlg(CWnd* pParent = NULL);   // standard constructor
	virtual ~CTempDlg();
// Overrides
	HRESULT OnButtonOK(IHTMLElement *pElement);
	HRESULT OnButtonCancel(IHTMLElement *pElement);

// Dialog Data
	enum { IDD = IDD_DIALOG_TEMP, IDH = IDR_HTML_TEMPDLG };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support
	virtual BOOL OnInitDialog();

	DECLARE_MESSAGE_MAP()
	DECLARE_DHTML_EVENT_MAP()
public:
	afx_msg void OnBnClickedOk();
};
