#if !defined(AFX_AGENTASYNCSOCKET_H__ED2F2A81_CC10_11D2_BFE5_0020E00EF9DE__INCLUDED_)
#define AFX_AGENTASYNCSOCKET_H__ED2F2A81_CC10_11D2_BFE5_0020E00EF9DE__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000
// AgentAsyncSocket.h : header file
//



/////////////////////////////////////////////////////////////////////////////
// CAgentAsyncSocket command target

class CAgentAsyncSocket : public CAsyncSocket
{
// Attributes
public:

// Operations
public:
	CAgentAsyncSocket();
	virtual ~CAgentAsyncSocket();

// Overrides
public:
	void SendDataForEcho();
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CAgentAsyncSocket)
	public:
	virtual void OnReceive(int nErrorCode);
	virtual void OnClose(int nErrorCode);
	//}}AFX_VIRTUAL

	// Generated message map functions
	//{{AFX_MSG(CAgentAsyncSocket)
		// NOTE - the ClassWizard will add and remove member functions here.
	//}}AFX_MSG

// Implementation
protected:
};

/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_AGENTASYNCSOCKET_H__ED2F2A81_CC10_11D2_BFE5_0020E00EF9DE__INCLUDED_)
