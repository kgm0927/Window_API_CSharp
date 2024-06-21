﻿#include <winsock2.h>
#include <stdlib.h>
#include <stdio.h>
#include <windows.h>

#define BUFSIZE 512
SOCKET listen_sock;

POINT p[1000];
int iCount;

LRESULT CALLBACK WndProc(HWND, UINT, WPARAM, LPARAM);
HINSTANCE g_hInst;
HWND hWndMain;
LPCTSTR lpszClass = TEXT("Class");

int APIENTRY WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance
	, LPSTR lpszCmdParam, int nCmdShow)
{
	HWND hWnd;
	MSG Message;
	WNDCLASS WndClass;
	g_hInst = hInstance;

	WndClass.cbClsExtra = 0;
	WndClass.cbWndExtra = 0;
	WndClass.hbrBackground = (HBRUSH)GetStockObject(WHITE_BRUSH);
	WndClass.hCursor = LoadCursor(NULL, IDC_ARROW);
	WndClass.hIcon = LoadIcon(NULL, IDI_APPLICATION);
	WndClass.hInstance = hInstance;
	WndClass.lpfnWndProc = (WNDPROC)WndProc;
	WndClass.lpszClassName = lpszClass;
	WndClass.lpszMenuName = NULL;
	WndClass.style = CS_HREDRAW | CS_VREDRAW;
	RegisterClass(&WndClass);

	hWnd = CreateWindow(lpszClass, lpszClass, WS_OVERLAPPEDWINDOW,
		CW_USEDEFAULT, CW_USEDEFAULT, CW_USEDEFAULT, CW_USEDEFAULT,
		NULL, (HMENU)NULL, hInstance, NULL);
	ShowWindow(hWnd, nCmdShow);
	hWndMain = hWnd;

	while (GetMessage(&Message, 0, 0, 0)) {
		TranslateMessage(&Message);
		DispatchMessage(&Message);
	}
	return Message.wParam;
}

void err_quit(const char* msg) {
	LPVOID IpMsgBuf;
	FormatMessage(
		FORMAT_MESSAGE_ALLOCATE_BUFFER |
		FORMAT_MESSAGE_FROM_SYSTEM,
		NULL, WSAGetLastError(),
		MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT),
		(LPTSTR)&IpMsgBuf, 0, NULL);

	MessageBox(NULL, (LPCTSTR)IpMsgBuf, (LPCTSTR)msg, MB_ICONERROR);
	LocalFree(IpMsgBuf);
	exit(-1);
}


void err_display(const char* msg)
{
	LPVOID lpMsgBuf;
	FormatMessage(
		FORMAT_MESSAGE_ALLOCATE_BUFFER |
		FORMAT_MESSAGE_FROM_SYSTEM,
		NULL, WSAGetLastError(),
		MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT),
		(LPTSTR)&lpMsgBuf, 0, NULL);
	MessageBox(NULL, (LPCTSTR)lpMsgBuf, (LPCTSTR)msg, MB_ICONERROR);
	LocalFree(lpMsgBuf);
	exit(-1);
}


int Initialize_WSADATA(WSADATA& wsa) {
	return WSAStartup(MAKEWORD(2, 2), &wsa);
}

int Initialize_SOCKET(SOCKET &sock) {
	sock = socket(AF_INET, SOCK_STREAM, 0);
	return 0;
}


void Initialize_SOCKADDR_IN(SOCKADDR_IN& serveraddr,SOCKET& sock,int& retval ) {
	ZeroMemory(&serveraddr, sizeof(serveraddr));
	serveraddr.sin_family = AF_INET;
	serveraddr.sin_port = htons(6000);
	serveraddr.sin_addr.s_addr = htonl(INADDR_ANY);
	retval = bind(sock,(SOCKADDR*)&serveraddr,sizeof(serveraddr));

	if (retval==SOCKET_ERROR)
	{
		err_quit("bind()");
	}
	
}

void Initialize_SOCKADDR_IN(SOCKADDR_IN& serveraddr, SOCKET& sock, int& retval,const char* sentence) {
	ZeroMemory(&serveraddr, sizeof(serveraddr));
	serveraddr.sin_family = AF_INET;
	serveraddr.sin_port = htons(5000);
	serveraddr.sin_addr.s_addr = inet_addr("127.0.0.1");
	retval = connect(sock, (SOCKADDR*)&serveraddr, sizeof(serveraddr));

	if (retval == SOCKET_ERROR)
	{
		err_quit(sentence);
	}

}

void Listening(int& retval, SOCKET& sock) {
	retval = listen(sock, SOMAXCONN);

}


// 데이터 통신에 사용할 변수
typedef struct var_to_communication {
	SOCKET client_sock;
	SOCKADDR_IN clientaddr;
	int addrlen = sizeof(clientaddr);
	int buf1;
	int buf2;
};

DWORD WINAPI ThreadFunc(LPVOID temp) {
	int retval;

	// 윈속 초기화
	WSADATA wsa;
	
	if (Initialize_WSADATA(wsa) != 0)
		return -1;

	// socket()
	SOCKET listen_sock;
	Initialize_SOCKET(listen_sock);
	if (listen_sock == INVALID_SOCKET)err_quit("socket()");

	// bind()
	SOCKADDR_IN serveraddr;
	Initialize_SOCKADDR_IN(serveraddr, listen_sock, retval);

	Listening(retval, listen_sock);

	var_to_communication vtc;

	while (1)
	{
		vtc.client_sock = accept(listen_sock, (SOCKADDR*)&(vtc.clientaddr), &vtc.addrlen);
		if (vtc.client_sock == INVALID_SOCKET) {
			err_display("accept()");
			continue;
		}

		printf("\n[TCP 서버] 클라이언트 접속: IP 주소=%s, 포트번호=%d \n"
			, inet_ntoa(vtc.clientaddr.sin_addr), ntohs(vtc.clientaddr.sin_port));

		while (1) {
			retval = recv(vtc.client_sock, (char*)&vtc.buf1, sizeof(int), 0);
			retval = recv(vtc.client_sock, (char*)&vtc.buf2, sizeof(int), 0);

			if (retval == SOCKET_ERROR) {
				err_display("recv()");
				break;
			}
			else if (retval == 0)
				break;

			p[iCount].x = vtc.buf1;
			p[iCount++].y = vtc.buf2;

			InvalidateRect(hWndMain, NULL, TRUE);

		}
		closesocket(vtc.client_sock);
		printf("[TCP 서버] 클라이언트 종료: IP 주소=%s, 포트 번호=%d\n",
			inet_ntoa(vtc.clientaddr.sin_addr), ntohs(vtc.clientaddr.sin_port));
	}

	closesocket(listen_sock);

	// 윈속 종료
	WSACleanup();
	return 0;
}

SOCKET sock;

int Creating_thread(HWND &hWnd,DWORD& ThreadID,HANDLE& hThread) {
	hThread = CreateThread(NULL, 0, ThreadFunc, NULL, 0, &ThreadID);
	CloseHandle(hThread);
	CreateWindow(TEXT("button"), TEXT("연결"), WS_CHILD | WS_VISIBLE |
		BS_PUSHBUTTON, 20, 20, 100, 25, hWnd, (HMENU)0, g_hInst, NULL);
	return 0;
}

int Organize_command(int &retval,HWND &hWnd,WPARAM &wParam) {

	switch (LOWORD(wParam))
	{
	case 0:
		WSADATA wsa;
		if (Initialize_WSADATA(wsa) != 0)
			return -1;

		// socket()
		Initialize_SOCKET(sock);
		if (sock == INVALID_SOCKET)err_quit("socket()");

		// connect()
		SOCKADDR_IN serveraddr;
		Initialize_SOCKADDR_IN(serveraddr, sock, retval, "connect()");

		MessageBox(hWnd, TEXT("연결되었습니다."), TEXT("Button"), MB_OK);

		break;
	}

	return 0;


}

void Pointing(int&x, int&y, int &retval, LPARAM& lParam){
	x = LOWORD(lParam);
	y = HIWORD(lParam);
	retval = send(sock, (char*)&x, sizeof(int), 0);
	retval = send(sock, (char*)&y, sizeof(int), 0);
	if (retval == SOCKET_ERROR){
		err_display("send()");
		return;
	}
}

void Setting_brush(HWND &hWnd,HDC &hdc,HBRUSH &hBrush,HBRUSH &oldBrush,LPARAM &lParam,int &x,int &y){
	hdc = GetDC(hWnd);
	hBrush = CreateSolidBrush(RGB(255, 0, 0));
	oldBrush = (HBRUSH)SelectObject(hdc, hBrush);

	Rectangle(hdc, x - 8, y - 8, x + 8, y + 8);
	SelectObject(hdc, oldBrush);
	ReleaseDC(hWnd,hdc);

	p[iCount].x = LOWORD(lParam);
	p[iCount++].y = HIWORD(lParam);
	InvalidateRect(hWnd, NULL, FALSE);
	return;

}

void Painting(HDC &hdc, HWND&hWnd, PAINTSTRUCT &ps, HBRUSH &hBrush, HBRUSH &oldBrush){

	hdc = BeginPaint(hWnd, &ps);
	hBrush = CreateSolidBrush(RGB(255, 0, 0));
	oldBrush = (HBRUSH)SelectObject(hdc, hBrush);
	for (int i = 0; i < iCount; i++)
	{
		Rectangle(hdc, p[i].x - 8, p[i].y - 8, p[i].x + 8, p[i].y + 8);
	}

	SelectObject(hdc, oldBrush);
	DeleteObject(hBrush);
	EndPaint(hWnd, &ps);
}


LRESULT CALLBACK WndProc(HWND hWnd, UINT iMessage, WPARAM wParam, LPARAM lParam)
{
	DWORD ThreadID;
	HANDLE hThread;

	HDC hdc;
	PAINTSTRUCT ps;


	int x, y;
	int retval;
	HBRUSH hBrush, oldBrush;

	switch (iMessage)
	{
	case WM_CREATE:
		Creating_thread(hWnd, ThreadID, hThread);
		return 0;

	case WM_COMMAND:
		Organize_command(retval, hWnd, wParam);

		return 0;

	case WM_LBUTTONDOWN:
		Pointing(x, y, retval, lParam);
		Setting_brush(hWnd, hdc, hBrush, oldBrush, lParam, x, y);
		return 0;

	case WM_PAINT:
		Painting(hdc, hWnd, ps, hBrush, oldBrush);
		return 0;
	case WM_DESTROY:

		closesocket(sock);
		// 윈속 종료
		WSACleanup();
		PostQuitMessage(0);
		return 0;

	}

	return(DefWindowProc(hWnd, iMessage, wParam, lParam));


}