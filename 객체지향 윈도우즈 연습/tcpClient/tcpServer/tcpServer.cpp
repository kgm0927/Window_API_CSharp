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
	ZeroMemory(serveraddr, len);										// serveraddr 구조체를 초기화한다.
	serveraddr->sin_family = AF_INET;													// 주소 패밀리를 ipv4로 설정한다.
	serveraddr->sin_port = htons(atoi(port));													// 서버가 바인딩될 포트를 설정한다.
	serveraddr->sin_addr.s_addr = htonl(INADDR_ANY);									// htonl 함수는 호스트 바이트 순서를 네트워크 바이트 순서로 반환한다. INADDR_ANY는 시스템에 사용 가능한 모든 네트워크 인터페이스로부터 연결을 수락할 것임을 나타낸다.
	retval = bind(*listen_sock, (SOCKADDR*)serveraddr, len);				// 소켓에 주소를 바인딩하는 부분입니다. bind 함수를 사용하여 소켓에 IP 주소와 포트를 할당합니다.

	/**
	SOCKET listen_sock = socket(AF_INET, SOCK_STREAM, 0);: TCP 소켓을 생성하는 부분입니다.
	AF_INET은 IPv4 주소 패밀리를 사용하고, SOCK_STREAM은 TCP 소켓을 나타냅니다.
	socket 함수는 성공하면 소켓 디스크립터(소켓 핸들)를 반환하고, 실패하면 INVALID_SOCKET을 반환합니다.

	if (listen_sock == INVALID_SOCKET) err_quit("socket()");: 소켓 생성이 실패한 경우를 확인하는 부분입니다.
	실패한 경우 err_quit 함수를 호출하여 오류를 처리합니다.

	SOCKADDR_IN serveraddr;: 서버 주소를 저장할 구조체를 정의합니다

	ZeroMemory(&serveraddr, sizeof(serveraddr));: serveraddr 구조체를 초기화합니다.
	이를 통해 구조체에 있는 모든 멤버 변수를 0으로 설정합니다.

	serveraddr.sin_family = AF_INET;: 주소 패밀리를 IPv4로 설정합니다.

	serveraddr.sin_port = htons(9000);: 서버가 바인딩될 포트를 설정합니다.

	htons 함수는 호스트 바이트 순서를 네트워크 바이트 순서로 변환합니다.

	serveraddr.sin_addr.s_addr = htonl(INADDR_ANY);: 서버가 바인딩될 IP 주소를 설정합니다.
	INADDR_ANY는 시스템에 사용 가능한 모든 네트워크 인터페이스로부터 연결을 수락할 것임을 나타냅니다.


	retval = bind(listen_sock, (SOCKADDR*)&serveraddr, sizeof(serveraddr));: 소켓에 주소를 바인딩하는 부분입니다. bind 함수를 사용하여 소켓에 IP 주소와 포트를 할당합니다.

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
		cout << argv[0] << "<IP 주소> " << "<포트 번호>" << endl;
		exit(1);
	}



	return 0;
}


int main(int argc, char* argv[]){

	int retval;


	Check_cmd( argc, argv);

	// 윈속 초기화
	/*
	"WSADATA"는 Windows Sockets 데이터 구조체를 나타냅니다.
	Windows Sockets은 Windows 운영 체제에서 네트워크 통신을
	구현하기 위한 API(응용 프로그램 프로그래밍 인터페이스)입니다.
	이 데이터 구조체는 Windows Sockets 기능을 초기화하고 관리하는 데 사용됩니다.
	"wsa"는 일반적으로 Windows Sockets 기능을 초기화한 후에 사용되는 변수 이름입니다.*/

	WSADATA wsa;
	Set_WSADATA(&wsa);

	//socket()
	SOCKET listen_sock;
	Make_socket(&listen_sock);
	if (listen_sock == INVALID_SOCKET)err_quit("socket()");



	//bind()
	SOCKADDR_IN serveraddr;																// 서버 주소를 저장할 구조체를 정의한다.
	Binding_serveraddr(&serveraddr, sizeof(serveraddr), retval, &listen_sock,argv[2]);
	// 문제


	// listen()
	retval = listen(listen_sock, SOMAXCONN);
	//listening(&retval, &listen_sock);
	if (retval == SOCKET_ERROR)err_quit("listen()");

	//retval = listen(listen_sock, SOMAXCONN);
	//if (retval == SOCKET_ERROR)err_quit("listen()");



	// 데이터 통신에 사용할 변수
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
		printf("\n[TCP 서버] 클라이언트 접속: IP 주소 =%s, 포트 번호 =%d \n", inet_ntoa(clientaddr.sin_addr), ntohs(clientaddr.sin_port));


		// 클라이언트와 데이터 통신
		while (1){
			// 데이터 받기
			retval = recv(client_sock, buf, BUFSIZE, 0);// block
			if (retval == SOCKET_ERROR){
				err_display("recv()");
				break;
			}
			else if (retval == 0)
				break;


			// 받은 데이터 출력
			buf[retval] = '\0';
			printf("[TCP %s: %d] %s\n", inet_ntoa(clientaddr.sin_addr), ntohs(clientaddr.sin_port), buf);


			// 데이터 보내기
			retval = send(client_sock, buf, retval, 0);
			if (retval == SOCKET_ERROR){
				err_display("send()");
				break;
			}
		}

		closesocket(client_sock);
		printf("[TCP 서버] 클라이언트 종료: IP 주소=%s, 포트 번호=%d \n", inet_ntoa(clientaddr.sin_addr), ntohs(clientaddr.sin_port));


	}
	// closesocket()
	closesocket(listen_sock);

	// 원속 종료 
	WSACleanup();
	return 0;

}