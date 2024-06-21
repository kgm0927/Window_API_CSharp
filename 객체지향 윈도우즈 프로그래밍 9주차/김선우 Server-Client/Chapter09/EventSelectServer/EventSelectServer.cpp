#include <winsock2.h>
#include <stdlib.h>
#include <stdio.h>

#define BUFSIZE 512

// 소켓 정보 저장을 위한 구조체
struct SOCKETINFO
{
	SOCKET sock;
	char buf[BUFSIZE+1];
	int recvbytes;
	int sendbytes;
};

int nTotalSockets = 0;
SOCKETINFO *SocketInfoArray[WSA_MAXIMUM_WAIT_EVENTS];
WSAEVENT EventArray[WSA_MAXIMUM_WAIT_EVENTS];

// 소켓 관리 함수
BOOL AddSocketInfo(SOCKET sock);
void RemoveSocketInfo(int nIndex);
// 오류 출력 함수
void err_quit(char *msg);
void err_display(char *msg);
void err_display(int errcode);

int main(int argc, char* argv[])
{
	int retval;

	// 윈속 초기화
	WSADATA wsa;
	if(WSAStartup(MAKEWORD(2,2), &wsa) != 0)
		return -1;

	// socket()
	SOCKET listen_sock = socket(AF_INET, SOCK_STREAM, 0);
	if(listen_sock == INVALID_SOCKET) err_quit("socket()");

	// 소켓 정보 추가
	if(AddSocketInfo(listen_sock) == FALSE)
		return -1;

	// WSAEventSelect()
	retval = WSAEventSelect(listen_sock, EventArray[nTotalSockets-1], 
		 FD_ACCEPT|FD_CLOSE);
	if(retval == SOCKET_ERROR) err_quit("WSAEventSelect()");

	// bind()
	SOCKADDR_IN serveraddr;
	ZeroMemory(&serveraddr, sizeof(serveraddr));
	serveraddr.sin_family = AF_INET;
	serveraddr.sin_port = htons(9000);
	serveraddr.sin_addr.s_addr = htonl(INADDR_ANY);
	retval = bind(listen_sock, (SOCKADDR *)&serveraddr, sizeof(serveraddr));
	if(retval == SOCKET_ERROR) err_quit("bind()");
	
	// listen()
	retval = listen(listen_sock, SOMAXCONN);
	if(retval == SOCKET_ERROR) err_quit("listen()");

	// 데이터 통신에 사용할 변수
	int index;
	WSANETWORKEVENTS NetworkEvents;
	SOCKET client_sock;
	SOCKADDR_IN clientaddr;
	int addrlen;

	while(1){
		// 이벤트 객체 관찰
		index = WSAWaitForMultipleEvents(nTotalSockets, EventArray, 
			FALSE, WSA_INFINITE, FALSE);
		if(index == WSA_WAIT_FAILED){
			err_display("WSAWaitForMultipleEvents()");
			continue;
		}
		index -= WSA_WAIT_EVENT_0;

		// 구체적인 네트워크 이벤트 알아내기
		retval = WSAEnumNetworkEvents(SocketInfoArray[index]->sock, 
			EventArray[index], &NetworkEvents);
		if(retval == SOCKET_ERROR){
			err_display("WSAEnumNetworkEvents()");
			continue;
		}

		// FD_ACCEPT 이벤트 처리
		if(NetworkEvents.lNetworkEvents & FD_ACCEPT){
			if(NetworkEvents.iErrorCode[FD_ACCEPT_BIT] != 0){
				err_display(NetworkEvents.iErrorCode[FD_ACCEPT_BIT]);
				continue;
			}

			addrlen = sizeof(clientaddr);
			client_sock = accept(SocketInfoArray[index]->sock, 
				(SOCKADDR *)&clientaddr, &addrlen);
			if(client_sock == INVALID_SOCKET){
				err_display("accept()");
				continue;
			}
			printf("[TCP 서버] 클라이언트 접속: IP 주소=%s, 포트 번호=%d\n", 
				inet_ntoa(clientaddr.sin_addr), ntohs(clientaddr.sin_port));

			if(nTotalSockets >= WSA_MAXIMUM_WAIT_EVENTS){
				printf("[오류] 더 이상 접속을 받아들일 수 없습니다!\n");
				closesocket(client_sock);
				continue;
			}

			if(AddSocketInfo(client_sock) == FALSE)
				continue;

			retval = WSAEventSelect(client_sock, EventArray[nTotalSockets-1], 
				FD_READ|FD_WRITE|FD_CLOSE);
			if(retval == SOCKET_ERROR) err_quit("WSAEventSelect()");
		}

		// FD_READ, FD_WRITE 이벤트 처리
		if(NetworkEvents.lNetworkEvents & FD_READ 
			|| NetworkEvents.lNetworkEvents & FD_WRITE)
		{
			if(NetworkEvents.lNetworkEvents & FD_READ 
				&& NetworkEvents.iErrorCode[FD_READ_BIT] != 0)
			{
				err_display(NetworkEvents.iErrorCode[FD_READ_BIT]);
				continue;
			}
			if(NetworkEvents.lNetworkEvents & FD_WRITE 
				&& NetworkEvents.iErrorCode[FD_WRITE_BIT] != 0)
			{
				err_display(NetworkEvents.iErrorCode[FD_WRITE_BIT]);
				continue;
			}
			
			SOCKETINFO *ptr = SocketInfoArray[index];

			if(ptr->recvbytes == 0){
				// 데이터 받기
				retval = recv(ptr->sock, ptr->buf, BUFSIZE, 0);
				if(retval == SOCKET_ERROR){
					if(WSAGetLastError() != WSAEWOULDBLOCK){
						err_display("recv()");
						RemoveSocketInfo(index);
					}
					continue;
				}
				ptr->recvbytes = retval;
				// 받은 데이터 출력
				ptr->buf[retval] = '\0';
				addrlen = sizeof(clientaddr);
				getpeername(ptr->sock, (SOCKADDR *)&clientaddr, &addrlen);
				printf("[TCP/%s:%d] %s\n", inet_ntoa(clientaddr.sin_addr),
					ntohs(clientaddr.sin_port), ptr->buf);
			}

			if(ptr->recvbytes > ptr->sendbytes){
				// 데이터 보내기
				retval = send(ptr->sock, ptr->buf + ptr->sendbytes, 
					ptr->recvbytes - ptr->sendbytes, 0);
				if(retval == SOCKET_ERROR){
					if(WSAGetLastError() != WSAEWOULDBLOCK){
						err_display("send()");
						RemoveSocketInfo(index);
					}
					continue;
				}
				ptr->sendbytes += retval;
				// 받은 데이터를 모두 보냈는지 체크
				if(ptr->recvbytes == ptr->sendbytes)
					ptr->recvbytes = ptr->sendbytes = 0;
			}
		}

		// FD_CLOSE 이벤트 처리
		if(NetworkEvents.lNetworkEvents & FD_CLOSE){
			if(NetworkEvents.iErrorCode[FD_CLOSE_BIT] != 0)
				err_display(NetworkEvents.iErrorCode[FD_CLOSE_BIT]);
			RemoveSocketInfo(index);
		}
	}

	// 윈속 종료
	WSACleanup();
	return 0;
}

// 소켓 정보 추가
BOOL AddSocketInfo(SOCKET sock)
{
	if(nTotalSockets >= WSA_MAXIMUM_WAIT_EVENTS){
		printf("[오류] 소켓 정보를 추가할 수 없습니다!\n");
		return FALSE;
	}

	SOCKETINFO *ptr = new SOCKETINFO;
	if(ptr == NULL){
		printf("[오류] 메모리가 부족합니다!\n");
		return FALSE;
	}

	WSAEVENT hEvent = WSACreateEvent();
	if(hEvent == WSA_INVALID_EVENT){
		err_display("WSACreateEvent()");
		return FALSE;
	}

	ptr->sock = sock;
	ptr->recvbytes = 0;
	ptr->sendbytes = 0;
	SocketInfoArray[nTotalSockets] = ptr;
	EventArray[nTotalSockets] = hEvent;
	nTotalSockets++;

	return TRUE;
}

// 소켓 정보 삭제
void RemoveSocketInfo(int nIndex)
{
	SOCKETINFO *ptr = SocketInfoArray[nIndex];

	// 클라이언트 정보 얻기
	SOCKADDR_IN clientaddr;
	int addrlen = sizeof(clientaddr);
	getpeername(ptr->sock, (SOCKADDR *)&clientaddr, &addrlen);
	printf("[TCP 서버] 클라이언트 종료: IP 주소=%s, 포트 번호=%d\n", 
		inet_ntoa(clientaddr.sin_addr), ntohs(clientaddr.sin_port));

	closesocket(ptr->sock);
	delete ptr;
	WSACloseEvent(EventArray[nIndex]);

	for(int i=nIndex; i<nTotalSockets; i++){
		SocketInfoArray[i] = SocketInfoArray[i+1];
		EventArray[i] = EventArray[i+1];
	}
	nTotalSockets--;
}

// 소켓 함수 오류 출력 후 종료
void err_quit(char *msg)
{
	LPVOID lpMsgBuf;
	FormatMessage( 
		FORMAT_MESSAGE_ALLOCATE_BUFFER|
		FORMAT_MESSAGE_FROM_SYSTEM,
		NULL, WSAGetLastError(),
		MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT),
		(LPTSTR)&lpMsgBuf, 0, NULL);
	MessageBox(NULL, (LPCTSTR)lpMsgBuf, msg, MB_ICONERROR);
	LocalFree(lpMsgBuf);
	exit(-1);
}

// 소켓 함수 오류 출력
void err_display(char *msg)
{
	LPVOID lpMsgBuf;
	FormatMessage( 
		FORMAT_MESSAGE_ALLOCATE_BUFFER|
		FORMAT_MESSAGE_FROM_SYSTEM,
		NULL, WSAGetLastError(),
		MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT),
		(LPTSTR)&lpMsgBuf, 0, NULL);
	printf("[%s] %s", msg, (LPCTSTR)lpMsgBuf);
	LocalFree(lpMsgBuf);
}

// 소켓 함수 오류 출력
void err_display(int errcode)
{
	LPVOID lpMsgBuf;
	FormatMessage( 
		FORMAT_MESSAGE_ALLOCATE_BUFFER|
		FORMAT_MESSAGE_FROM_SYSTEM,
		NULL, errcode,
		MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT),
		(LPTSTR)&lpMsgBuf, 0, NULL);
	printf("[오류] %s", (LPCTSTR)lpMsgBuf);
	LocalFree(lpMsgBuf);
}