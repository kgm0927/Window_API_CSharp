#include <winsock2.h>
#include <stdlib.h>
#include <stdio.h>
#include <iostream>
#include <time.h>

#define BUFSIZE 512

// ���� �Լ� ���� ��� �� ����
void err_quit(char *msg)
{
	LPVOID lpMsgBuf;
	FormatMessage(
		FORMAT_MESSAGE_ALLOCATE_BUFFER |
		FORMAT_MESSAGE_FROM_SYSTEM,
		NULL, WSAGetLastError(),
		MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT),
		(LPTSTR)&lpMsgBuf, 0, NULL);
	MessageBox(NULL, (LPCTSTR)lpMsgBuf,(LPCTSTR)msg, MB_ICONERROR);
	LocalFree(lpMsgBuf);
	exit(-1);
}

// ���� �Լ� ���� ���
void err_display(char *msg)
{
	LPVOID lpMsgBuf;
	FormatMessage(
		FORMAT_MESSAGE_ALLOCATE_BUFFER |
		FORMAT_MESSAGE_FROM_SYSTEM,
		NULL, WSAGetLastError(),
		MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT),
		(LPTSTR)&lpMsgBuf, 0, NULL);
	printf("[%s] %s\n\n", msg, (LPCTSTR)lpMsgBuf);
	LocalFree(lpMsgBuf);
}

// ����� ���� ������ ���� �Լ�
int recvn(SOCKET s, char *buf, int len, int flags)
{
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

int make_wsa(WSADATA* wsa){
	return WSAStartup(MAKEWORD(2, 2), wsa);
}

void connecting_sockaddr(SOCKADDR_IN* serveraddr,int size,char* ip){
	ZeroMemory(serveraddr, size);
	serveraddr->sin_family = AF_INET;
	serveraddr->sin_addr.s_addr = inet_addr(ip);
}


int searching_ip(int port_num,SOCKET *sock,SOCKADDR_IN *serveraddr,int &retval){

	for (int portnum = port_num; portnum < port_num+3; portnum++){
		printf("port num : %d Test!!!\n", portnum);
		serveraddr->sin_port = htons(portnum);
		retval = connect(*sock, (SOCKADDR *)&serveraddr, sizeof(serveraddr)); // ������ ���� ���� �� �ִ� ���� �ѹ� ����� ���ƾ� �Ѵ�.
		if (retval != SOCKET_ERROR)
		{
			printf("port num : %d \n", portnum);
			closesocket(*sock);
		}

		else {
			err_display("connect()");
			continue;
		}
	}
	return 0;
}

int Check_cmd(int argc, char *argv[]){
	if (argc != 2) {
		std::cout << argv[0] << "<IP �ּ�> "  << std::endl;
		exit(1);
	}



	return 0;
}

int main(int argc, char* argv[]){
	int retval;

	WSADATA wsa;

	Check_cmd(argc, argv);
	printf("%s", argv[1]);

	if (make_wsa(&wsa) != 0)
		return -1;


	SOCKET sock = socket(AF_INET, SOCK_STREAM, 0);
	if (sock==INVALID_SOCKET)
	{
		err_quit("socket()");
	}
	SOCKADDR_IN serveraddr;
	connecting_sockaddr(&serveraddr, sizeof(serveraddr), argv[1]);


//	searching_ip(79,&sock, &serveraddr, retval);
	for (int portnum = 79; portnum < 82; portnum++){
		printf("port num : %d Test!!!\n", portnum);
		serveraddr.sin_port = htons(portnum);
		retval = connect(sock, (SOCKADDR *)&serveraddr, sizeof(serveraddr)); // ������ ���� ���� �� �ִ� ���� �ѹ� ����� ���ƾ� �Ѵ�.




		if (retval != SOCKET_ERROR)
		{
			printf("\n\n");
			printf("���� ��!!!:\n");
			printf("port num : %d \n", portnum);
			printf("ip num:%s", argv[1]);
			printf("\n\n");

			closesocket(sock);
			continue;

		}

		else {
			err_display("connect()");
			
		}
	}


}