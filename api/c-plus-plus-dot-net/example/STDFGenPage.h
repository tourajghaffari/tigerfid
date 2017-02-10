#pragma once
#include "afxwin.h"


// STDFGenPage dialog

class STDFGenPage : public CDialog
{
	DECLARE_DYNAMIC(STDFGenPage)

public:
	STDFGenPage(CWnd* pParent = NULL);   // standard constructor
	virtual ~STDFGenPage();

// Dialog Data
	enum { IDD = IDD_DIALOG_STDFGEN };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnBnClickedButtonQueryFgen();
	afx_msg void OnBnClickedCheckAnyid();
};
