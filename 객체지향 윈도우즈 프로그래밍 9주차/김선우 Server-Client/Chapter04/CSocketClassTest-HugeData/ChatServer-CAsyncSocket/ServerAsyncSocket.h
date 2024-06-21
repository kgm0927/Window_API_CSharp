#if !defined(AFX_SERVERASYNCSOCKET_H__CC84853D_C8AD_11D2_BFE5_0020E00EF9DE__INCLUDED_)
#define AFX_SERVERASYNCSOCKET_H__CC84853D_C8AD_11D2_BFE5_0020E00EF9DE__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000
// ServerAsyncSocket.h : header file
//

#include "AgentAsyncSocket.h"

/////////////////////////////////////////////////////////////////////////////
// CServerAsyncSocket command target

class CServerAsyncSocket : public CAsyncSocket
{
// Attributes
public:
	CAgentAsyncSocket m_Agent;

// Operations
public:
	CServerAsyncSocket();
	virtual ~CServerAsyncSocket();

// Overrides
public:
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CServerAsyncSocket)
	public:
	virtual void OnAccept(int nErrorCode);
	//}}AFX_VIRTUAL

	// Generated message map functions
	//{{AFX_MSG(CServerAsyncSocket)
		// NOTE - the ClassWizard will add and remove member functions here.
	//}}AFX_MSG

// Implementation
protected:
};

/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_SERVERASYNCSOCKET_H__CC84853D_C8AD_11D2_BFE5_0020E00EF9DE__INCLUDED_)
