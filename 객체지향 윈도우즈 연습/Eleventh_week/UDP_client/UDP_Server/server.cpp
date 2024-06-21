#define _WINSOCK_DEPRECATED_NO_WARNINGS


#include <WinSock2.h>
#include <stdlib.h>
#include <stdio.h>


#define BUFSIZE 512

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
	printf("[%s] %s", (LPCTSTR)msg, (LPCTSTR)lpMsgBuf);
	LocalFree(lpMsgBuf);
}

int Initialize_WSADATA(WSADATA *wsa) {
	if (WSAStartup(MAKEWORD(2, 2), wsa) != 0) {
		return -1;
	}
	return 1;
}


int Initialize_socket(SOCKET* sock) {
	*sock = socket(AF_INET, SOCK_DGRAM, 0);
	if (*sock == INVALID_SOCKET) {
		err_quit("socket()");
		return -1;
	}
	return 1;
}

int Initialize_SOCKADDR_IN(SOCKADDR_IN *serveraddr,int serveraddr_len,int &retval,SOCKET* sock) {
	ZeroMemory(serveraddr, serveraddr_len);
	serveraddr->sin_family = AF_INET;		// 여기서 자꾸 문제가 됨
	serveraddr->sin_port = htons(9000);
	serveraddr->sin_addr.s_addr = htonl(INADDR_ANY);
	retval = bind(*sock, (SOCKADDR*)serveraddr, serveraddr_len);
	if (retval == SOCKET_ERROR)
	{
		err_quit("bind()");
		return -1;
	}
	return 1;


}

// 데이터 통신에 사용할 변수
typedef struct ClientAddr  {
	SOCKADDR_IN clientaddr;
	int addrlen=sizeof(clientaddr);
	char buf[BUFSIZE + 1]="";
};

void Receive_data(ClientAddr* ca,SOCKET *sock,int& retval,char* buf) {
	retval = recvfrom(*sock, buf, BUFSIZE, 0, (SOCKADDR*)&(ca->clientaddr), &(ca->addrlen));

	if (retval==SOCKET_ERROR)
	{
		err_display("recvfrom()");
	}
}

// 모든 데이터 읽기
void Show_data(ClientAddr* ca,char* buf,int& retval) {
	buf[retval] = '\0';
	printf("[UDP/ %s: %d] %s \n", inet_ntoa(ca->clientaddr.sin_addr), ntohs(ca->clientaddr.sin_port), buf);
}


void Send_data(ClientAddr*ca,int& retval,SOCKET* sock,char* buf, int addr_size) {

	retval = sendto(*sock, buf, retval, 0, (SOCKADDR*)&(ca->clientaddr), addr_size);

	if (retval == SOCKET_ERROR) {
		err_display("sendto()");
	
	}
}

void Communicate_with_client(ClientAddr* ca,char* buf,int& retval,SOCKET* sock,int serveraddr_size) {
	
		Receive_data(ca, sock, retval, buf);

		Show_data(ca, buf, retval);

		Send_data(ca, retval, sock, buf, serveraddr_size);
	
}

int main(int argc, char* argv[]) {
	int retval;

	WSADATA wsa;
	Initialize_WSADATA(&wsa);


	//socket()
	SOCKET sock;
	Initialize_socket(&sock);

	SOCKADDR_IN serveraddr;
	int addrlen = sizeof(serveraddr);
	char buf[BUFSIZE + 1];

	Initialize_SOCKADDR_IN(&serveraddr,sizeof(serveraddr),retval,&sock);

	ClientAddr ca;
	
	while (1)
	{

	
		Communicate_with_client(&ca, buf, retval, &sock, addrlen);
	}

	closesocket(sock);

	WSACleanup();
	return 0;


}