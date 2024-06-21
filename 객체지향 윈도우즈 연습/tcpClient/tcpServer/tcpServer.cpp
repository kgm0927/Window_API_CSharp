#define _WINSOCK_DEPRECATED_NO_WARNINGS
#include <winsock2.h>
#include <stdlib.h>
#include <stdio.h>
#include <iostream>

#define BUFSIZE 512

using namespace std;

void err_quit(char *msg){
	LPVOID lpMsgBuf;
	FormatMessage(
		FORMAT_MESSAGE_ALLOCATE_BUFFER |
		FORMAT_MESSAGE_FROM_SYSTEM,
		NULL, WSAGetLastError(),
		MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT),
		(LPTSTR)&lpMsgBuf, 0, NULL);
	MessageBox(NULL, (LPCTSTR)lpMsgBuf, (LPCWSTR)msg, MB_ICONERROR);
	LocalFree(lpMsgBuf);
	exit(-1);

}


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


int Set_WSADATA(WSADATA *wsa){
	if (WSAStartup(MAKEWORD(2, 2), wsa) != 0){
		return -1;
	}
	return 1;
}

void Make_socket(SOCKET* listen_sock){
	*listen_sock = socket(AF_INET, SOCK_STREAM, 0);
}


void Binding_serveraddr(SOCKADDR_IN *serveraddr, int len, int &retval, SOCKET* listen_sock,char* port){
	ZeroMemory(serveraddr, len);										// serveraddr ����ü�� �ʱ�ȭ�Ѵ�.
	serveraddr->sin_family = AF_INET;													// �ּ� �йи��� ipv4�� �����Ѵ�.
	serveraddr->sin_port = htons(atoi(port));													// ������ ���ε��� ��Ʈ�� �����Ѵ�.
	serveraddr->sin_addr.s_addr = htonl(INADDR_ANY);									// htonl �Լ��� ȣ��Ʈ ����Ʈ ������ ��Ʈ��ũ ����Ʈ ������ ��ȯ�Ѵ�. INADDR_ANY�� �ý��ۿ� ��� ������ ��� ��Ʈ��ũ �������̽��κ��� ������ ������ ������ ��Ÿ����.
	retval = bind(*listen_sock, (SOCKADDR*)serveraddr, len);				// ���Ͽ� �ּҸ� ���ε��ϴ� �κ��Դϴ�. bind �Լ��� ����Ͽ� ���Ͽ� IP �ּҿ� ��Ʈ�� �Ҵ��մϴ�.

	/**
	SOCKET listen_sock = socket(AF_INET, SOCK_STREAM, 0);: TCP ������ �����ϴ� �κ��Դϴ�.
	AF_INET�� IPv4 �ּ� �йи��� ����ϰ�, SOCK_STREAM�� TCP ������ ��Ÿ���ϴ�.
	socket �Լ��� �����ϸ� ���� ��ũ����(���� �ڵ�)�� ��ȯ�ϰ�, �����ϸ� INVALID_SOCKET�� ��ȯ�մϴ�.

	if (listen_sock == INVALID_SOCKET) err_quit("socket()");: ���� ������ ������ ��츦 Ȯ���ϴ� �κ��Դϴ�.
	������ ��� err_quit �Լ��� ȣ���Ͽ� ������ ó���մϴ�.

	SOCKADDR_IN serveraddr;: ���� �ּҸ� ������ ����ü�� �����մϴ�

	ZeroMemory(&serveraddr, sizeof(serveraddr));: serveraddr ����ü�� �ʱ�ȭ�մϴ�.
	�̸� ���� ����ü�� �ִ� ��� ��� ������ 0���� �����մϴ�.

	serveraddr.sin_family = AF_INET;: �ּ� �йи��� IPv4�� �����մϴ�.

	serveraddr.sin_port = htons(9000);: ������ ���ε��� ��Ʈ�� �����մϴ�.

	htons �Լ��� ȣ��Ʈ ����Ʈ ������ ��Ʈ��ũ ����Ʈ ������ ��ȯ�մϴ�.

	serveraddr.sin_addr.s_addr = htonl(INADDR_ANY);: ������ ���ε��� IP �ּҸ� �����մϴ�.
	INADDR_ANY�� �ý��ۿ� ��� ������ ��� ��Ʈ��ũ �������̽��κ��� ������ ������ ������ ��Ÿ���ϴ�.


	retval = bind(listen_sock, (SOCKADDR*)&serveraddr, sizeof(serveraddr));: ���Ͽ� �ּҸ� ���ε��ϴ� �κ��Դϴ�. bind �Լ��� ����Ͽ� ���Ͽ� IP �ּҿ� ��Ʈ�� �Ҵ��մϴ�.

	*
	*/

}

void listening(int *retval, SOCKET *listen_sock){
	*retval = listen(*listen_sock, SOMAXCONN);

}

void Make_client_sock(){

}
int Check_cmd(int argc, char *argv[]){
	if (argc != 3) {
		cout << argv[0] << "<IP �ּ�> " << "<��Ʈ ��ȣ>" << endl;
		exit(1);
	}



	return 0;
}


int main(int argc, char* argv[]){

	int retval;


	Check_cmd( argc, argv);

	// ���� �ʱ�ȭ
	/*
	"WSADATA"�� Windows Sockets ������ ����ü�� ��Ÿ���ϴ�.
	Windows Sockets�� Windows � ü������ ��Ʈ��ũ �����
	�����ϱ� ���� API(���� ���α׷� ���α׷��� �������̽�)�Դϴ�.
	�� ������ ����ü�� Windows Sockets ����� �ʱ�ȭ�ϰ� �����ϴ� �� ���˴ϴ�.
	"wsa"�� �Ϲ������� Windows Sockets ����� �ʱ�ȭ�� �Ŀ� ���Ǵ� ���� �̸��Դϴ�.*/

	WSADATA wsa;
	Set_WSADATA(&wsa);

	//socket()
	SOCKET listen_sock;
	Make_socket(&listen_sock);
	if (listen_sock == INVALID_SOCKET)err_quit("socket()");



	//bind()
	SOCKADDR_IN serveraddr;																// ���� �ּҸ� ������ ����ü�� �����Ѵ�.
	Binding_serveraddr(&serveraddr, sizeof(serveraddr), retval, &listen_sock,argv[2]);
	// ����


	// listen()
	retval = listen(listen_sock, SOMAXCONN);
	//listening(&retval, &listen_sock);
	if (retval == SOCKET_ERROR)err_quit("listen()");

	//retval = listen(listen_sock, SOMAXCONN);
	//if (retval == SOCKET_ERROR)err_quit("listen()");



	// ������ ��ſ� ����� ����
	SOCKET client_sock;
	SOCKADDR_IN clientaddr;
	int addrlen;
	char buf[BUFSIZE + 1];

	while (1){
		// accept()
		addrlen = sizeof(clientaddr);
		client_sock = accept(listen_sock, (SOCKADDR*)&clientaddr, &addrlen);
		if (client_sock == INVALID_SOCKET){
			err_display("accept()");
			break;
		}
		printf("\n[TCP ����] Ŭ���̾�Ʈ ����: IP �ּ� =%s, ��Ʈ ��ȣ =%d \n", inet_ntoa(clientaddr.sin_addr), ntohs(clientaddr.sin_port));


		// Ŭ���̾�Ʈ�� ������ ���
		while (1){
			// ������ �ޱ�
			retval = recv(client_sock, buf, BUFSIZE, 0);// block
			if (retval == SOCKET_ERROR){
				err_display("recv()");
				break;
			}
			else if (retval == 0)
				break;


			// ���� ������ ���
			buf[retval] = '\0';
			printf("[TCP %s: %d] %s\n", inet_ntoa(clientaddr.sin_addr), ntohs(clientaddr.sin_port), buf);


			// ������ ������
			retval = send(client_sock, buf, retval, 0);
			if (retval == SOCKET_ERROR){
				err_display("send()");
				break;
			}
		}

		closesocket(client_sock);
		printf("[TCP ����] Ŭ���̾�Ʈ ����: IP �ּ�=%s, ��Ʈ ��ȣ=%d \n", inet_ntoa(clientaddr.sin_addr), ntohs(clientaddr.sin_port));


	}
	// closesocket()
	closesocket(listen_sock);

	// ���� ���� 
	WSACleanup();
	return 0;

}