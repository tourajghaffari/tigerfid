// Seal Host Environment.h : main header file for the PROJECT_NAME application
//

#pragma once

#ifndef __AFXWIN_H__
	#error "include 'stdafx.h' before including this file for PCH"
#endif

#include "resource.h"		// main symbols
#include <ctime>

#define _TIMEOUT	5
#define _DELAY		1000

// CSealHostEnvironmentApp:
// See Seal Host Environment.cpp for the implementation of this class
//

class CSealHostEnvironmentApp : public CWinApp
{
public:
	CSealHostEnvironmentApp();

// Overrides
	public:
	virtual BOOL InitInstance();

// Implementation

	DECLARE_MESSAGE_MAP()
};

extern CSealHostEnvironmentApp theApp;