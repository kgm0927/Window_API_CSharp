#include <WinSock2.h>
#include <stdlib.h>
#include <stdio.h>
#include <Windows.h>



#define BUFSIZE 512

SOCKET listen_sock;

POINT p[1000];
int iCount;

LRESULT CALLBACK WndProc(HWND, UINT, WPARAM, LPARAM);
HINSTANCE g_hInst;
HWND hWndMain;
LPCTSTR lpszClass = TEXT("Server");


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


// 소켓 함수 오류 출력 후 종료

void err_quit(char *msg){
	LPVOID lpMsgBuf;
	FormatMessage(
		FORMAT_MESSAGE_ALLOCATE_BUFFER |
		FORMAT_MESSAGE_FROM_SYSTEM,
		NULL, WSAGetLastError(),
		MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT),
		(LPTSTR)&lpMsgBuf, 0, NULL);
	MessageBox(NULL, (LPCTSTR)lpMsgBuf,(LPCTSTR) msg, MB_ICONERROR);
	LocalFree(lpMsgBuf);
	exit(-1);
}


// 소켓 함수 오류 출력
void err_display(char *msg)
{
	LPVOID lpMsgBuf;
	FormatMessage(
		FORMAT_MESSAGE_ALLOCATE_BUFFER |
		FORMAT_MESSAGE_FROM_SYSTEM,
		NULL, WSAGetLastError(),
		MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT),
		(LPTSTR)&lpMsgBuf, 0, NULL);
	printf("[%s] %s", msg, (LPCTSTR)lpMsgBuf);
	LocalFree(lpMsgBuf);
}

int Initialize_WSAStartup(WSADATA &wsa){
	return WSAStartup(MAKEWORD(2, 2), &wsa);
}

void Initialize_SOCKET(SOCKET* sock){
	*sock = socket(AF_INET, SOCK_STREAM, 0);

}


void Initialize_SOCKADDR_IN(SOCKADDR_IN *serveraddr,int serveraddr_len,int &retval,SOCKET &sock){
	ZeroMemory(serveraddr, serveraddr_len);
	serveraddr->sin_family = AF_INET;
	serveraddr->sin_port = htons(5000);
	serveraddr->sin_addr.s_addr = htonl(INADDR_ANY);	// 어떤 ip건 받아들이겠다는 의미.
	retval = bind(sock, (SOCKADDR*)serveraddr, serveraddr_len);

}


void Listening(int&retval, SOCKET *sock){
	retval = listen(*sock, SOMAXCONN);

}


// 데이터 통신에 사용할 변수


typedef struct var_to_communication{
	SOCKET client_sock;
	SOCKADDR_IN clientaddr;
	int addrlen=sizeof(clientaddr);
	int buf1;
	int buf2;
};




DWORD WINAPI ThreadFunc(LPVOID temp)
{
	int retval;

	WSADATA wsa;
	if (Initialize_WSAStartup(wsa) != 0)
		return -1;

	SOCKET listen_sock;
	Initialize_SOCKET(&listen_sock);
	if (listen_sock == INVALID_SOCKET)err_quit("socket()");


	//bind()

	SOCKADDR_IN serveraddr;
	Initialize_SOCKADDR_IN(&serveraddr, sizeof(serveraddr), retval, listen_sock);
	if (retval == SOCKET_ERROR)err_quit("bind()");

	// listen()
	Listening(retval, &listen_sock);
	if (retval == SOCKET_ERROR)err_quit("listen()");


	// 데이터 통신에 사용할 변수
	 var_to_communication vtc;
	 var_to_communication *pvtc = &vtc;
	while (1)
	{
		vtc.client_sock = accept(listen_sock, (SOCKADDR*)&pvtc->clientaddr, &pvtc->addrlen);
		if (vtc.client_sock == INVALID_SOCKET){
			err_display("accept()");
			continue;
		}

		printf("\n[TCP 서버] 클라이언트 접속: IP 주소=%s, 포트 번호=%d\n"
			, inet_ntoa(pvtc->clientaddr.sin_addr), ntohs(pvtc->clientaddr.sin_port));

		// 클라이언트와 데이터 통신
		while (1){
			retval = recv(pvtc->client_sock, (char*)&(pvtc->buf1), sizeof(int), 0);
			retval = recv(pvtc->client_sock, (char*)&(pvtc->buf2), sizeof(int), 0);
			if (retval == SOCKET_ERROR){
				err_display("recv()");
				break;
			}
			else if (retval == 0)
				break;

			p[iCount].x = pvtc->buf1;
			p[iCount++].y = pvtc->buf2;
			InvalidateRect(hWndMain, NULL, TRUE);

		}
		closesocket(pvtc->client_sock);
		printf("[TCP 서버] 클라이언트 종료: IP 주소=%s, 포트 번호=%d\n",
			inet_ntoa(pvtc->clientaddr.sin_addr), ntohs(pvtc->clientaddr.sin_port));

	}

	closesocket(listen_sock);

	WSACleanup();
	return 0;
}

void Creating_thread(HANDLE* hThread,DWORD *ThreadID){
	*hThread = CreateThread(NULL, 0, ThreadFunc, NULL, 0, ThreadID);
	CloseHandle(*hThread);
	
}

int Pointing(HWND* hWnd,LPARAM &lParam){
	p[iCount].x = LOWORD(lParam);
	p[iCount++].y = HIWORD(lParam);
	InvalidateRect(*hWnd, NULL, FALSE);
	
	return 0;  
}

int Painting(HWND& hWnd, HDC &hdc, PAINTSTRUCT& ps,HBRUSH& hBrush ,HBRUSH &oldBrush){
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

	return 0;
}

LRESULT CALLBACK WndProc(HWND hWnd, UINT iMessage, WPARAM wParam, LPARAM lParam)
{
	static DWORD ThreadID;
	static HANDLE hThread;

	HDC hdc;
	PAINTSTRUCT ps;

	int i;

	HBRUSH hBrush, oldBrush;

	switch (iMessage) {
	case WM_CREATE:
		hThread = CreateThread(NULL, 0, ThreadFunc, NULL, 0, &ThreadID);
		CloseHandle(hThread);
		return 0;
	case WM_LBUTTONDOWN:
		Pointing(&hWnd,  lParam);
		return 0;


	case WM_PAINT:
		Painting(hWnd, hdc, ps, hBrush, oldBrush);
		return 0;
	case WM_DESTROY:
		PostQuitMessage(0);
		return 0;
	}
	return(DefWindowProc(hWnd, iMessage, wParam, lParam));
}