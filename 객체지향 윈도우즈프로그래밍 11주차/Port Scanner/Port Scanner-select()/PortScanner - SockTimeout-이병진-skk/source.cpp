#include <winsock2.h>
#include <stdlib.h>
#include <stdio.h>
#include <fcntl.h>
#include <time.h>

#define PARAM_SIZE 4
#define WAIT_TIME 5  // 2
#define LSOCKADDR struct sockaddr *

int IsAlivePort(char *host, int port, int m_sec);
void ErrParameter();
//naver test : [ 223.130.195.95 0 500 ]  [속성-디버깅-명령인수]에 입력되어 있음
int main(int argc, char* argv[])
{
	if (argc != PARAM_SIZE) ErrParameter();

	int	start_port = atoi(argv[2]), end_port = atoi(argv[3]);
	
	// 윈속 초기화
	WSADATA wsa;
	if (WSAStartup(MAKEWORD(2, 2), &wsa) != 0) return -1;

	//Port Scanning
	for (int i = start_port; i <= end_port; i++){
		if (IsAlivePort(argv[1], i, WAIT_TIME)) printf("%5d - ON\n", i);
		//else printf("%5d - OFF\n", i);
	}
	// 윈속 종료
	WSACleanup();
	return 0;
}

// connect()호출 후 일정시간이 지나면 port 가 열려있지 않은것으로 간주.
// 기다리는 일정시간을 적용하기위해 select() 모델사용.
// PortScannerEx7 예제에서는 기다리는 시간을 적용할 수 없다.  
// 결국 5msec 대기시간을 적용한 port scanner 

int IsAlivePort(char *host, int port, int m_sec)
{
	TIMEVAL Timeout = { 0, m_sec }; 
	// 5 msec 대기 후 응답신호(TCPIP교학사:p.226,Three-way handshaking)가 없으면
	// 이 port는 열려있지 않다.

	struct sockaddr_in addr;
	SOCKET sock = socket(AF_INET, SOCK_STREAM, 0);
	addr.sin_addr.s_addr = inet_addr(host);
	addr.sin_port = htons(port);
	addr.sin_family = AF_INET;

	//Non-blocking
	unsigned long mode = 1;
	int res = ioctlsocket(sock, FIONBIO, &mode);

	if (res != NO_ERROR) printf("ioctlsocket failed : %ld\n", res);

	if (connect(sock, (LSOCKADDR)&addr, sizeof(addr)) == false) return FALSE;

	fd_set Read, Err;  //Read , Write의 구분은 의미없다. 활용하기 나름

	FD_ZERO(&Read);
	FD_ZERO(&Err);
	FD_SET(sock, &Read);  //connet() 호출 후 sock을 통한 응답(Read)을 기다린다.
	FD_SET(sock, &Err);

	//Check socket status

	int ret = select(0, NULL, &Read, &Err, &Timeout);	// 타임아웃을 설정할 수 있다.
	if (ret == 0) return FALSE;  //in case of timeout, select() returns 0.
	//else return TRUE;  // 0번 port는 Err발생한 port로 출력한다.
	else if (FD_ISSET(sock, &Read)) return TRUE; 
	//Read 집합에 넣어둔 (조사하려는 port를 위해 생성한) socket 인지 확인
}

void ErrParameter()
{
	LPVOID lpMsgBuf;
	FormatMessage(
		FORMAT_MESSAGE_ALLOCATE_BUFFER |
		FORMAT_MESSAGE_FROM_SYSTEM,
		NULL, WSAGetLastError(),
		MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT),
		(LPTSTR)&lpMsgBuf, 0, NULL);
	MessageBox(NULL, TEXT("Input Parameter"), TEXT("Parameter Error : <ip> <start_port> <end_port>"), MB_ICONERROR);
	LocalFree(lpMsgBuf);
	exit(-1);
}