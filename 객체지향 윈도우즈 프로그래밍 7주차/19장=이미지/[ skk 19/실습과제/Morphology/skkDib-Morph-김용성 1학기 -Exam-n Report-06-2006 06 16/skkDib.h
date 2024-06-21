// skkDib.h : main header file for the SKKDIB application
//

#if !defined(AFX_SKKDIB_H__D9D48445_7452_11D3_B809_0000C0B11065__INCLUDED_)
#define AFX_SKKDIB_H__D9D48445_7452_11D3_B809_0000C0B11065__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#include "resource.h"       // main symbols

/////////////////////////////////////////////////////////////////////////////
// CSkkDibApp:
// See skkDib.cpp for the implementation of this class
//

class CSkkDibApp : public CWinApp
{
public:
	CSkkDibApp();

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CSkkDibApp)
	public:
	virtual BOOL InitInstance();
	//}}AFX_VIRTUAL

// Implementation
	//{{AFX_MSG(CSkkDibApp)
	afx_msg void OnAppAbout();
	afx_msg void OnFileOpen();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};


/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_SKKDIB_H__D9D48445_7452_11D3_B809_0000C0B11065__INCLUDED_)
