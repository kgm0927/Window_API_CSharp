#define _WINSOCK_DEPRECATED_NO_WARNINGS
#include <winsock2.h>
#include <stdlib.h>
#include <stdio.h>

#define BUFSIZE 512

// 소켓 함수 오류 출력 후 종료
void err_quit(char* msg)
{
	LPVOID lpMsgBuf;
	FormatMessage(
		FORMAT_MESSAGE_ALLOCATE_BUFFER |
		FORMAT_MESSAGE_FROM_SYSTEM,
		NULL, WSAGetLastError(),
		MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT),
		(LPTSTR)&lpMsgBuf, 0, NULL);
	MessageBox(NULL, (LPCTSTR)lpMsgBuf, msg, MB_ICONERROR);
	LocalFree(lpMsgBuf);
	exit(-1);
}

// 소켓 함수 오류 출력
void err_display(char* msg)
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

// 사용자 정의 데이터 수신 함수
int recvn(SOCKET s, char* buf, int len, int flags)
{
	int received;
	char* ptr = buf;
	int left = len;

	while (left > 0) {
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

int Making_wsa(WSADATA* wsa) {
	return WSAStartup(MAKEWORD(2, 2), wsa);
}

void Making_socket(SOCKET* sock) {
	*sock = socket(AF_INET, SOCK_STREAM, 0);
}

void Making_socketaddr(SOCKADDR_IN *serveraddr,SOCKET* sock ,int len,int *retval) {
	ZeroMemory(serveraddr, len);
	serveraddr->sin_family = AF_INET;
	serveraddr->sin_port = htons(9000);
	serveraddr->sin_addr.s_addr = inet_addr("192.168.35.213"); // 루프백 어드레스

	*retval = connect(*sock, (SOCKADDR*)serveraddr, len);
}


void Set_buf(char* buf,int len) {
	ZeroMemory(buf, len);
	printf("\n[보낼 데이터] ");
}

int Remove_enter(char* buf,int &len) {
	
	if (buf[len - 1] == '\n')
	{
		buf[len - 1] = '\0';
		return 1;
	}

	if (strlen(buf) == 0)
		return 0;
}

void Send_Message(SOCKET *sock,int &retval,char* buf,int len,int flag) {
	retval = send(*sock, buf, len, flag);

}


void Receive_Message(SOCKET* sock,char* buf,int& retval,int flag) {
	retval = recvn(*sock, buf, retval, flag);
}


int main(int argc, char* argv[])
{
	int retval;
	WSADATA wsa;

	// 원속 초기화
	if (Making_wsa(&wsa) != 0)
		return -1;

	// socket()
	SOCKET sock;
	Making_socket(&sock);
	if (sock == INVALID_SOCKET)err_quit("socket()");

	// connect()
	SOCKADDR_IN serveraddr;
	Making_socketaddr(&serveraddr, &sock, sizeof(serveraddr), &retval);
	if (retval == SOCKET_ERROR)err_quit("connect()");

	// 데이터 통신에 사용할 변수
	char buf[BUFSIZE + 1];
	int len;

	while (1) {
		// 데이터 입력
		Set_buf(buf, sizeof(buf));
		if (fgets(buf, BUFSIZE + 1, stdin) == NULL)
			break;

		// '\n' 문자 제거
		len = strlen(buf);
		if (Remove_enter(buf, len) == 0)
			break;

		// 데이터 보내기
		Send_Message(&sock,retval, buf, strlen(buf), 0);
		if (retval == SOCKET_ERROR) {
			err_display("send()");
			break;
		}
		printf("[TCP 클라이언트] %d바이트를 보냈습니다.\n", retval);

		// 데이터 받기
		Receive_Message(&sock, buf, retval, 0);

		if (retval == SOCKET_ERROR) {
			err_display("recv()");
			break;
		}
		else if (retval == 0)
			break;

		// 받은 데이터 출력
		buf[retval] = '\0';
		printf("[TCP 클라이언트] %d바이트를 받았습니다.\n", retval);
		printf("[받은 데이터] %s\n", buf);
	}

	// closesocket()
	closesocket(sock);

	// 윈속 종료
	WSACleanup();
	return 0;


	
}