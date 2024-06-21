// ChatServerDlg.h : header file
//

#if !defined(AFX_CHATSERVERDLG_H__CC848527_C8AD_11D2_BFE5_0020E00EF9DE__INCLUDED_)
#define AFX_CHATSERVERDLG_H__CC848527_C8AD_11D2_BFE5_0020E00EF9DE__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

/////////////////////////////////////////////////////////////////////////////
// CChatServerDlg dialog

#include "ServerAsyncSocket.h"

class CChatServerDlg : public CDialog
{
// Attributes
public:
	CServerAsyncSocket m_ServerSocket;

// Construction
public:
	CChatServerDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	//{{AFX_DATA(CChatServerDlg)
	enum { IDD = IDD_CHATSERVER_DIALOG };
	CEdit	m_ctrlReceivedCount;
	CEdit	m_ctrlReceivedBytes;
	CListBox	m_MessageList;
	CString	m_szMessage;
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CChatServerDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:
	HICON m_hIcon;

	// Generated message map functions
	//{{AFX_MSG(CChatServerDlg)
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	afx_msg void OnLButtonDown(UINT nFlags, CPoint point);
	afx_msg void OnSendButton();
	afx_msg void OnClose();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_CHATSERVERDLG_H__CC848527_C8AD_11D2_BFE5_0020E00EF9DE__INCLUDED_)
