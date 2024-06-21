#include <WinSock2.h>
#include <stdlib.h>
#include <stdio.h>
#include <fcntl.h>
#include <time.h>


#define PARAM_SIZE 4
#define WAIT_TIME 5
#define LSOCKADDR struct sockaddr*

int IsAlivePort(char *host, int port, int m_sec);
void ErrParameter();
//naver test : [ 223.130.195.95 0 500 ]  [속성-디버깅-명령인수]에 입력되어 있음


int Initialize_WSADATA(WSADATA* wsa){
	return WSAStartup(MAKEWORD(2, 2), wsa);
}

int main(int argc,char* argv[]){
	if (argc != PARAM_SIZE)ErrParameter();

	int start_port = atoi(argv[2]), end_port = atoi(argv[3]);

	// 윈속 초기화
	WSADATA wsa;
	if (Initialize_WSADATA(&wsa)!=0) return -1;

	// Port Scanning
	for (int i = start_port; i < start_port; i++)
	{
		if (IsAlivePort(argv[1], i, WAIT_TIME)) printf("%5d - 0N\n", i);
		//printf("%5d - 0N\n", i);
	}

	// 윈속 종료
	WSACleanup();
	return 0;
}


int IsAlivePort(char *host, int port, int m_sec){
	TIMEVAL Timeout = {0,m_sec};


	struct sockaddr_in addr;
	SOCKET sock = socket(AF_INET,SOCK_STREAM,0);
	addr.sin_addr.s_addr = inet_addr(host);
	addr.sin_port = htons(port);
	addr.sin_family = AF_INET;

	// Non-blocking 
	unsigned long mode = 1;

	int res = ioctlsocket(sock, FIONBIO, &mode);

	if (res != NO_ERROR) printf("ioctlsocket failed: %ld\n", res);

	if (connect(sock, (LSOCKADDR)&addr, sizeof(addr)) == false) return FALSE;


	fd_set Read, Err;


	FD_ZERO(&Read);
	FD_ZERO(&Err);
	FD_SET(sock, &Read);		//  connect()  호출 후 sock을 통한 응답(Read)을 기다린다.
	FD_SET(sock, &Err);

	// Check socket status

	int ret = select(0, NULL, &Read, &Err, &Timeout);
	if (ret == 0) return FALSE;

	else if (FD_ISSET(sock, &Read)) return TRUE;



}


void ErrParameter(){
	LPVOID IpMsgBuf;
	FormatMessage(
		FORMAT_MESSAGE_ALLOCATE_BUFFER |
		FORMAT_MESSAGE_FROM_SYSTEM,
		NULL, WSAGetLastError(),
		MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT),
		(LPTSTR)&IpMsgBuf, 0, NULL
		);
	MessageBox(NULL, TEXT("Input Parameter"), TEXT("Parameter Error : <ip> <start_port> <end_port>"), MB_ICONERROR);

	LocalFree(IpMsgBuf);
	exit(-1);
}