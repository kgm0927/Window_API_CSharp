// ServerAsyncSocket.cpp : implementation file
//

#include "stdafx.h"
#include "ChatServer.h"
#include "ServerAsyncSocket.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CServerAsyncSocket

CServerAsyncSocket::CServerAsyncSocket()
{
}

CServerAsyncSocket::~CServerAsyncSocket()
{
}


// Do not edit the following lines, which are needed by ClassWizard.
#if 0
BEGIN_MESSAGE_MAP(CServerAsyncSocket, CAsyncSocket)
	//{{AFX_MSG_MAP(CServerAsyncSocket)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()
#endif	// 0

/////////////////////////////////////////////////////////////////////////////
// CServerAsyncSocket member functions

void CServerAsyncSocket::OnAccept(int nErrorCode) 
{
	// TODO: Add your specialized code here and/or call the base class
//	AfxMessageBox("클라이언트로부터 연결 요청이 들어왔습니다!");
	Accept(m_Agent);
	
	CAsyncSocket::OnAccept(nErrorCode);
}
