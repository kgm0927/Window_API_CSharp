// ClientSocket.cpp : implementation file
//

#include "stdafx.h"
#include "ChatClient.h"
#include "ClientSocket.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CClientSocket

CClientSocket::CClientSocket()
{
}

CClientSocket::~CClientSocket()
{
}


// Do not edit the following lines, which are needed by ClassWizard.
#if 0
BEGIN_MESSAGE_MAP(CClientSocket, CSocket)
	//{{AFX_MSG_MAP(CClientSocket)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()
#endif	// 0

/////////////////////////////////////////////////////////////////////////////
// CClientSocket member functions

#include "ChatClientDlg.h"
int count=0, Received=0; 

#define FILESIZE 1000000
char TotalReceivedData[FILESIZE]; 

void CClientSocket::OnReceive(int nErrorCode) 
{
	// TODO: Add your specialized code here and/or call the base class
	char Data[FILESIZE+1];
	int ret;
	ret=Receive((LPSTR)Data, FILESIZE);

	CString szTmp, szReceivedBytes, szReceivedCallCount;
	szTmp.Format("Received Byte = %d,  CallCount = %d", ret, count+1);
	((CChatClientDlg*)AfxGetMainWnd())
		->m_MessageList.AddString(szTmp);

	count++;
	Received+=ret;
	szReceivedBytes.Format("%d", Received);
	((CChatClientDlg*)AfxGetMainWnd())
		->m_ctrlReceivedBytes.SetWindowText(szReceivedBytes);
	szReceivedCallCount.Format("%d", count);
	((CChatClientDlg*)AfxGetMainWnd())
		->m_ctrlReceivedCount.SetWindowText(szReceivedCallCount);

	Data[ret]=0;
	strcat(TotalReceivedData,Data);
	CSocket::OnReceive(nErrorCode);
}


void CClientSocket::OnClose(int nErrorCode) 
{
	// TODO: Add your specialized code here and/or call the base class
	Close();

	((CChatClientDlg*)AfxGetMainWnd())
		->m_MessageList.AddString("서버가 종료되었습니다.");
	
	CSocket::OnClose(nErrorCode);
}
