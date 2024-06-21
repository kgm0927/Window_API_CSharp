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
//naver test : [ 223.130.195.95 0 500 ]  [�Ӽ�-�����-����μ�]�� �ԷµǾ� ����
int main(int argc, char* argv[])
{
	if (argc != PARAM_SIZE) ErrParameter();

	int	start_port = atoi(argv[2]), end_port = atoi(argv[3]);
	
	// ���� �ʱ�ȭ
	WSADATA wsa;
	if (WSAStartup(MAKEWORD(2, 2), &wsa) != 0) return -1;

	//Port Scanning
	for (int i = start_port; i <= end_port; i++){
		if (IsAlivePort(argv[1], i, WAIT_TIME)) printf("%5d - ON\n", i);
		//else printf("%5d - OFF\n", i);
	}
	// ���� ����
	WSACleanup();
	return 0;
}

// connect()ȣ�� �� �����ð��� ������ port �� �������� ���������� ����.
// ��ٸ��� �����ð��� �����ϱ����� select() �𵨻��.
// PortScannerEx7 ���������� ��ٸ��� �ð��� ������ �� ����.  
// �ᱹ 5msec ���ð��� ������ port scanner 

int IsAlivePort(char *host, int port, int m_sec)
{
	TIMEVAL Timeout = { 0, m_sec }; 
	// 5 msec ��� �� �����ȣ(TCPIP���л�:p.226,Three-way handshaking)�� ������
	// �� port�� �������� �ʴ�.

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

	fd_set Read, Err;  //Read , Write�� ������ �ǹ̾���. Ȱ���ϱ� ����

	FD_ZERO(&Read);
	FD_ZERO(&Err);
	FD_SET(sock, &Read);  //connet() ȣ�� �� sock�� ���� ����(Read)�� ��ٸ���.
	FD_SET(sock, &Err);

	//Check socket status

	int ret = select(0, NULL, &Read, &Err, &Timeout);	// Ÿ�Ӿƿ��� ������ �� �ִ�.
	if (ret == 0) return FALSE;  //in case of timeout, select() returns 0.
	//else return TRUE;  // 0�� port�� Err�߻��� port�� ����Ѵ�.
	else if (FD_ISSET(sock, &Read)) return TRUE; 
	//Read ���տ� �־�� (�����Ϸ��� port�� ���� ������) socket ���� Ȯ��
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