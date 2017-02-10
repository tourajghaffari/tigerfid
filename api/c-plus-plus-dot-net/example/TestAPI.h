// TestAPI.h : main header file for the TESTAPI application
//

#if !defined(AFX_TESTAPI_H__8048DCB8_9361_456E_97F7_4CA0A2291334__INCLUDED_)
#define AFX_TESTAPI_H__8048DCB8_9361_456E_97F7_4CA0A2291334__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#include "resource.h"		// main symbols

#define itoa _itoa
/////////////////////////////////////////////////////////////////////////////
// CTestAPIApp:
// See TestAPI.cpp for the implementation of this class
//

class CTestAPIApp : public CWinApp
{
public:
	CTestAPIApp();
	

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CTestAPIApp)
	public:
	virtual BOOL InitInstance();
	//}}AFX_VIRTUAL

// Implementation

	//{{AFX_MSG(CTestAPIApp)
		// NOTE - the ClassWizard will add and remove member functions here.
		//    DO NOT EDIT what you see in these blocks of generated code !
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};


/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_TESTAPI_H__8048DCB8_9361_456E_97F7_4CA0A2291334__INCLUDED_)
