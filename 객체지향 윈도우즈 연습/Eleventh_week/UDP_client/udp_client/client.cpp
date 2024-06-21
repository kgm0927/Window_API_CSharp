#define _WINSOCK_DEPRECATED_NO_WARNINGS

#include <WinSock2.h>
#include <stdlib.h>
#include <stdio.h>
#include <winsock.h>
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

void err_display(const char* msg) {
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

int initialize_WSADATA(WSADATA *wsa) {
	if (WSAStartup(MAKEWORD(2, 2), wsa) != 0) {
		return -1;
	}
	else
	{
		return 1;
	}
}
int Create_Socket(SOCKET *sock) {
	*sock = socket(AF_INET, SOCK_DGRAM, 0);
	if (*sock == INVALID_SOCKET) {
		err_quit("Socket()");
		return -1;
	}
	return 1;
}


int Initialize_sock_struct(SOCKADDR_IN *sock_struct,int sock_size,const char* ip) {
	ZeroMemory(sock_struct, sock_size);
	sock_struct->sin_family = AF_INET;
	sock_struct->sin_port = htons(9000);
	sock_struct->sin_addr.s_addr = inet_addr(ip);

	return 1;

}

int Delete_enter(char* buf,int* buf_len) {
	
	
	if (buf[(*buf_len) - 1] == '\n')
		buf[*(buf_len)-1] = '\0';
	if (strlen(buf) == 0)
		return - 1;
}

// 데이터 보내기
void Send_data(int &retval,SOCKET* sock,char* buf,int buf_len,SOCKADDR_IN* serveraddr,int serveraddr_len){

		retval = sendto(*sock, buf, buf_len, 0, (SOCKADDR*)serveraddr, serveraddr_len);
		if (retval == SOCKET_ERROR) {
			err_display("sendto()");
		}
		printf("[UDP 클라이언트] %d 바이트를 보냈습니다. \n", retval);

}

int receive_data(int& retval, char* buf, int* addrlen,SOCKET* sock, SOCKADDR_IN* peeraddr, int* peeraddr_len) {

	*addrlen = *peeraddr_len;
	retval = recvfrom(*sock,buf,BUFSIZE,0,(SOCKADDR*)peeraddr,addrlen);
	if (retval==SOCKET_ERROR)
	{
		err_display("recvfrom()");
		return -1;
	}
	return 1;


}
void Connection_with_server(char* buf, int* buf_len, int& retval, SOCKET * sock, SOCKADDR_IN * serveraddr, int serveraddr_len,SOCKADDR_IN* peeraddr,int* peeraddr_len,int* addrlen) {

	bool True_or_False = true;
	while (True_or_False)
	{
		printf("\n[보낼 데이터] ");
		if (fgets(buf, BUFSIZE + 1, stdin) == NULL)break;

		if (-1 == Delete_enter(buf,buf_len)) {
			break;
		}

		Send_data(retval, sock, buf, *buf_len, serveraddr, serveraddr_len);

		if (-1 == receive_data(retval, buf, addrlen, sock, peeraddr, peeraddr_len)) {
			break;
		}
		if (memcmp(peeraddr, serveraddr, *peeraddr_len)) {
			printf("[오류] 잘못된 데이터입니다!\n");
			continue;
		}

		buf[retval] = '\0';
		printf("[UDP 클라이언트] %d바이트를 받았습니다.\n", retval);
		printf("[받은 데이터] %s\n", buf);

	
	}
}



int main(int argc, char* argv[]){


	if (argc < 2) {
		printf("Usage:포트 번호를 쓰시기 바랍니다.");
		return 1;
	}
	char* ip = argv[1];


	int retval;
	

	WSADATA wsa;

	initialize_WSADATA(&wsa);

	SOCKET sock;
	Create_Socket(&sock);


	// 소켓 주소 구조체 초기화
	SOCKADDR_IN serveraddr;
	Initialize_sock_struct(&serveraddr, sizeof(serveraddr), ip);


	// 데이터 통신에 사용할 변수
	SOCKADDR_IN peeraddr;
	int peeraddr_len = sizeof(peeraddr);
	int addrlen;
	char buf[BUFSIZE + 1];
	int len = strlen(buf);
	
	Connection_with_server(buf, &len, retval, &sock, &serveraddr, sizeof(serveraddr), &peeraddr,&peeraddr_len , &addrlen);

	closesocket(sock);

	WSACleanup();
	return 0;
}

