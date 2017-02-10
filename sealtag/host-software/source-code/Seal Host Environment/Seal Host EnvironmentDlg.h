// Seal Host EnvironmentDlg.h : header file
//

#pragma once
#include "afxwin.h"


// CSealHostEnvironmentDlg dialog
class CSealHostEnvironmentDlg : public CDialog
{
// Construction
public:
	CSealHostEnvironmentDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	enum { IDD = IDD_SEALHOSTENVIRONMENT_DIALOG };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support


// Implementation
protected:
	HICON m_hIcon;

	// Generated message map functions
	virtual BOOL OnInitDialog();
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	DECLARE_MESSAGE_MAP()
private:
	int getNumber(unsigned char, unsigned char);
	CString getWDay(int);
	CString getCmd(int);
	CString getCmdStatus(int);
	CString getTamperStatus(int);
public:
	afx_msg void OnBnClickedButton1();
	afx_msg void OnBnClickedButton2();
	afx_msg void OnBnClickedButton3();
	CEdit statusText;
	CString m_statusText;
	CComboBox comboBox1;
	CComboBox comboBox2;
	CComboBox comboBox3;
	CComboBox comboBox4;
	CString m_comboBox1;
	CString m_comboBox2;
	CString m_comboBox3;
	CString m_comboBox4;
	CButton btnRead;
	CButton btnClose;
	CButton btnOpen;
	CButton btnWrite;
	afx_msg void OnBnClickedButton4();
	CComboBox comboBox5;
	CString m_comboBox5;
	CComboBox comboBox6;
	CString m_comboBox6;
	CComboBox comboBox7;
	CString m_comboBox7;
	CComboBox comboBox8;
	CString m_comboBox8;
	CComboBox comboBoxCommand;
	CString m_comboBoxCommand;
	afx_msg void OnBnClickedButton5();
	CComboBox comboBox9;
	CString m_comboBox9;
	CComboBox comboBox10;
	CString m_comboBox10;
	CComboBox comboBox11;
	CString m_comboBox11;
	CComboBox comboBox12;
	CString m_comboBox12;
	afx_msg void OnBnClickedButtonStartTimer();
	afx_msg void OnBnClickedButtonStopTimer();
	CButton btnStartTimer;
	CButton btnStopTimer;
	CButton btnRead5;
	CButton btnRead1;
	afx_msg void OnBnClickedButtonRead5();
	afx_msg void OnBnClickedButtonRead1();
	CButton btnFlush;
	afx_msg void OnBnClickedButtonFlush();
	CEdit edReset;
	CString m_edReset;
	CButton btnHistory;
	CEdit edHistory;
	CString m_edHistory;
	afx_msg void OnBnClickedButtonReadHistory();
	CButton btnHistory2;
	afx_msg void OnBnClickedButtonHistory2();
	void initApp(void);
	afx_msg void OnBnClickedButtonReadAllHistory();
	CButton btnAllHistory;
	CStatic logoGTP;
	CStatic logoHunter;
	afx_msg void OnStnDblclickRuntime();
	afx_msg void OnStnClickedRuntime();
};
