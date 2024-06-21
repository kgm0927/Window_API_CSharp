// AgentAsyncSocket.cpp : implementation file
//

#include "stdafx.h"
#include "ChatServer.h"
#include "AgentAsyncSocket.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CAgentAsyncSocket

CAgentAsyncSocket::CAgentAsyncSocket()
{
}

CAgentAsyncSocket::~CAgentAsyncSocket()
{
}


// Do not edit the following lines, which are needed by ClassWizard.
#if 0
BEGIN_MESSAGE_MAP(CAgentAsyncSocket, CAsyncSocket)
	//{{AFX_MSG_MAP(CAgentAsyncSocket)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()
#endif	// 0

/////////////////////////////////////////////////////////////////////////////
// CAgentAsyncSocket member functions

#include "ChatServerDlg.h"
int count=0, Received=0; 

#define FILESIZE 1000000
char TotalReceivedData[FILESIZE]; 

void CAgentAsyncSocket::OnReceive(int nErrorCode) 
{
	// TODO: Add your specialized code here and/or call the base class
	char Data[1001];
	int ret;
	
	ret=Receive((LPSTR)Data, 1000);
	CString szTmp, szReceivedBytes, szReceivedCallCount;
	szTmp.Format("%s", Data);
	((CChatServerDlg*)AfxGetMainWnd())
		->m_MessageList.AddString(szTmp);

	Data[ret]=0;
	strcat(TotalReceivedData,Data);
	
	count++;
	Received+=ret;
	szReceivedBytes.Format("%d", Received);
	((CChatServerDlg*)AfxGetMainWnd())
		->m_ctrlReceivedBytes.SetWindowText(szReceivedBytes);
	szReceivedCallCount.Format("%d", count);
	((CChatServerDlg*)AfxGetMainWnd())
		->m_ctrlReceivedCount.SetWindowText(szReceivedCallCount);

	if(Received==FILESIZE) 
	{ 
		SendDataForEcho();
	}
	
	CAsyncSocket::OnReceive(nErrorCode);
}

void CAgentAsyncSocket::SendDataForEcho()
{
	Send((LPSTR)TotalReceivedData, FILESIZE);
}

void CAgentAsyncSocket::OnClose(int nErrorCode) 
{
	// TODO: Add your specialized code here and/or call the base class
	Close();
	((CChatServerDlg*)AfxGetMainWnd())
		->m_MessageList.AddString("클라이언트가 종료되었습니다.");
	
	CAsyncSocket::OnClose(nErrorCode);
}


