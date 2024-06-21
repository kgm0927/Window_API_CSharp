#include <WinSock2.h>
#include <stdlib.h>
#include <stdio.h>
#include <Windows.h>


#define BURSIZE 512

int buf1;
int buf2;

LRESULT CALLBACK WndProc(HWND, UINT, WPARAM, LPARAM);
HINSTANCE g_hInst;
HWND hWndMain;
LPCTSTR lpszClass = TEXT("Client");


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

void err_quit(char* msg){
	LPVOID IpMsgBuf;
	FormatMessage(
		FORMAT_MESSAGE_ALLOCATE_BUFFER |
		FORMAT_MESSAGE_FROM_SYSTEM,
		NULL, WSAGetLastError(),
		MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT),
		(LPTSTR)&IpMsgBuf, 0, NULL
		);

	printf("[%s] %s", msg, (LPCTSTR)IpMsgBuf);

	LocalFree(IpMsgBuf);

}

// 소켓 함수 오류출력

void err_display(char *msg){
	LPVOID IpMsgBuf;
	FormatMessage(
		FORMAT_MESSAGE_ALLOCATE_BUFFER |
		FORMAT_MESSAGE_FROM_SYSTEM,
		NULL, WSAGetLastError(),
		MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT),
		(LPTSTR)&IpMsgBuf, 0, NULL);
	printf("[%s] %s", msg, (LPCTSTR)IpMsgBuf);
	LocalFree(IpMsgBuf);
}



// 사용자 정의 데이터 수신 함수
int recvn(SOCKET s, char *buf, int len, int flags){

	int received;
	char *ptr = buf;
	int left = len;

	while (left > 0){
		received = recv(s, ptr, left, flags);
		if (received == SOCKET_ERROR)
			return SOCKET_ERROR;

		else if (received == 0)
			break;

		left -= received;
		ptr += received;
	}

	return (len - left);
}

int Initialize_WSADATA(WSADATA *wsa){
	return WSAStartup(MAKEWORD(2, 2), wsa);
}

void Initialize_SOCKET(SOCKET *sock){
	*sock = socket(AF_INET, SOCK_STREAM, 0);
}


void Initialize_SOCKADDR_IN(SOCKADDR_IN &serveraddr, SOCKET &sock, int &retval)
{
	ZeroMemory((&serveraddr), sizeof(serveraddr));
	serveraddr.sin_family = AF_INET;
	serveraddr.sin_port = htons(5000);
	serveraddr.sin_addr.s_addr = inet_addr("127.0.0.1");

	retval = connect(sock, (SOCKADDR*)&serveraddr, sizeof(serveraddr));
	if (retval == SOCKET_ERROR) err_quit("connect()");

}

DWORD WINAPI ThreadFunc(LPVOID temp){
	int retval;

	WSADATA wsa;
	if (Initialize_WSADATA(&wsa) != 0)
		return -1;


	SOCKET sock;
	Initialize_SOCKET(&sock);


	SOCKADDR_IN serveraddr;

	Initialize_SOCKADDR_IN(serveraddr, sock, retval);

	while (1)
	{
		retval = send(sock, (char*)&buf1, sizeof(int), 0);
		retval = send(sock, (char*)&buf2, sizeof(int), 0);

		if (retval == SOCKET_ERROR)
		{
			err_display("send()");
			break;
		}

		break;
	}

	closesocket(sock);

	WSACleanup();

	return 0;


}
int mouse_handling(HWND& hWnd, int&x, int&y, LPARAM& lParam, HDC&hdc, HBRUSH &hBrush, HBRUSH &oldBrush, HANDLE &hThread, DWORD &ThreadID){
	x = LOWORD(lParam);
	y = HIWORD(lParam);

	buf1 = x;
	buf2 = y;

	hdc = GetDC(hWnd);

	hBrush = CreateSolidBrush(RGB(255, 0, 0));
	oldBrush = (HBRUSH)SelectObject(hdc, hBrush);
	Rectangle(hdc, x - 8, y - 8, x + 8, y + 8);
	SelectObject(hdc, oldBrush);
	ReleaseDC(hWnd, hdc);

	hThread = CreateThread(NULL, 0, ThreadFunc, NULL, 0, &ThreadID);
	CloseHandle(hThread);

	return 0;

}

LRESULT CALLBACK WndProc(HWND hWnd, UINT iMessage, WPARAM wParam, LPARAM lParam)
{

	DWORD ThreadID;
	HANDLE hThread;

	HDC hdc;
	PAINTSTRUCT ps;


	int x, y;
	HBRUSH hBrush, oldBrush;

	switch (iMessage)
	{
	case WM_CREATE:
		return 0;

	case WM_LBUTTONDOWN:
		mouse_handling(hWnd, x, y, lParam, hdc, hBrush, oldBrush, hThread, ThreadID);
		return 0;

	case WM_PAINT:
		hdc = BeginPaint(hWnd, &ps);
		EndPaint(hWnd, &ps);
		return 0;

	case WM_DESTROY:
		PostQuitMessage(0);
		return 0;
	}
	return(DefWindowProc(hWnd, iMessage, wParam, lParam));
}