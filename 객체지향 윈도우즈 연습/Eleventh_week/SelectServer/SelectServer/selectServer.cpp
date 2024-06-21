#define _WINSOCK_DEPRECATED_NO_WARNINGS

#include <winsock2.h>
#include <stdlib.h>
#include <stdio.h>


#define BUFSIZE 512

struct SOCKETINFO
{
	SOCKET sock;
	char buf[BUFSIZE + 1];
	int recvbytes;
	int sendbytes;
};

int nTotalSockets = 0;
SOCKETINFO* SocketInfoArray[FD_SETSIZE];

// ���� ���� �Լ�
BOOL AddSocketInfo(SOCKET sock) {
	if (nTotalSockets >= (FD_SETSIZE - 1)) {
		printf("[����] ���� ������ �߰��� �� �����ϴ�.");
		return FALSE;
	}

	SOCKETINFO* ptr = new SOCKETINFO;
	if (ptr == NULL) {
		printf("[����] �޸𸮰� �����մϴ�!\n");
		return FALSE;
	}
	ptr->sock = sock;
	ptr->recvbytes = 0;
	ptr->sendbytes = 0;
	SocketInfoArray[nTotalSockets++] = ptr;

	return TRUE;
}


// ���� ���� ����
void RemoveSocketInfo(int nIndex) {

	// Ŭ���̾�Ʈ ���� ���

	SOCKETINFO* ptr = SocketInfoArray[nIndex];

	// Ŭ���̾�Ʈ ���� ���
	SOCKADDR_IN clientaddr;
	int addrlen = sizeof(clientaddr);
	getpeername(ptr->sock, (SOCKADDR*)&clientaddr, &addrlen);

	printf("[TCP ����] Ŭ���̾�Ʈ ����: IP �ּ�=%s, ��Ʈ ��ȣ=%d\n",
		inet_ntoa(clientaddr.sin_addr), ntohs(clientaddr.sin_port));


	closesocket(ptr->sock);
	delete ptr;

	for (int i = 0; i < nTotalSockets; i++)
	{
		SocketInfoArray[i] = SocketInfoArray[i + 1];
	}
	nTotalSockets--;


}


// ���� �Լ� ���� ��� �� ����
void err_quit(const char* msg)
{
	LPVOID lpMsgBuf;
	FormatMessage(
		FORMAT_MESSAGE_ALLOCATE_BUFFER |
		FORMAT_MESSAGE_FROM_SYSTEM,
		NULL, WSAGetLastError(),
		MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT),
		(LPTSTR)&lpMsgBuf, 0, NULL);
	MessageBox(NULL, (LPCTSTR)lpMsgBuf, (LPCTSTR)msg, MB_ICONERROR);
	LocalFree(lpMsgBuf);
	exit(-1);
}

// ���� �Լ� ���� ���
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

int Initialize_WSADATA(WSADATA* wsa) {
	return WSAStartup(MAKEWORD(2, 2), wsa);
}

int Initialize_SOCKET(SOCKET* sock) {
	*sock = socket(AF_INET, SOCK_STREAM, 0);
	if (*sock==INVALID_SOCKET)
	{
		err_quit("socket()");
		return -1;
	}
	return 1;
}

// bind(
int Initialize_SOCKADDR_IN(SOCKADDR_IN* serveraddr,int serveraddr_size,SOCKET* sock,int &retval) {
	ZeroMemory(serveraddr, sizeof(serveraddr));
	serveraddr->sin_family = AF_INET;
	serveraddr->sin_port = htons(9000);
	serveraddr->sin_addr.s_addr = htonl(INADDR_ANY);
	retval = bind(*sock, (SOCKADDR*)serveraddr, serveraddr_size);
	if (retval == SOCKET_ERROR) {
		err_quit("listen()");
		return -1;
	}
	return 1;
}


int listening_and_set(SOCKET* sock,int &retval,u_long &on) {
	retval = listen(*sock,SOMAXCONN);
	if (retval==SOCKET_ERROR)
	{
		err_quit("listen()");
		return -1;
	}

	// �� ���ŷ �������� ��ȯ

	on = TRUE;
	retval = ioctlsocket(*sock, FIONBIO, &on);
	if (retval==SOCKET_ERROR)
	{
		err_display("ioctlsocket()");
		return -1;
	}

	return 1;
}




typedef struct var_for_data_communication {
	FD_SET rset;
	FD_SET wset;
	SOCKET client_sock;
	SOCKADDR_IN clientaddr;
	int addrlen=sizeof(clientaddr);
};

void Sock_Set_Initialize(var_for_data_communication* vfdc,SOCKET* sock) {
	FD_ZERO(&(vfdc->rset));
	FD_ZERO(&(vfdc->wset));
	FD_SET(*sock, &(vfdc->rset));		// ���� ��� ���� ����

	for (int i = 0; i < nTotalSockets; i++) {
		if (SocketInfoArray[i]->recvbytes>SocketInfoArray[i]->sendbytes)
		{
			FD_SET(SocketInfoArray[i]->sock, &(vfdc->wset));
		}
		else {
			FD_SET(SocketInfoArray[i]->sock, &(vfdc->rset));

		}
	}
}


int Selecting(var_for_data_communication* vfdc,int& retval) {

	retval = select(0, &(vfdc->rset), &(vfdc->wset), NULL, NULL);
	if (retval == SOCKET_ERROR)
	{
		err_quit("select()");
		return -1;
	}
	return 1;
}


// ���� �� �˻�(1): Ŭ���̾�Ʈ ���� ����
void Socket_set(var_for_data_communication* vfdc,SOCKET*listen_sock) {

	if (FD_ISSET(*listen_sock, &(vfdc->rset))) {
		vfdc->client_sock = accept(*listen_sock, (SOCKADDR*)&(vfdc->clientaddr), &(vfdc->addrlen));
		if (vfdc->client_sock == INVALID_SOCKET) {
			if (WSAGetLastError() != WSAEWOULDBLOCK)
				err_display("accept()");

		}

		else {
			printf("[TCP ����] Ŭ���̾�Ʈ ����: IP �ּ�=%s, ��Ʈ��ȣ=%d\n",
				inet_ntoa(vfdc->clientaddr.sin_addr), ntohs(vfdc->clientaddr.sin_port));

			// ���� ���� �߰�
			if (AddSocketInfo(vfdc->client_sock) == FALSE) {
				printf("[TCP ����] Ŭ���̾�Ʈ ������ �����մϴ�!\n");
				closesocket(vfdc->client_sock);

			}
		}
	}
}

// ���� �� �˻�(2): ������ ���

void Communicate_with_client(var_for_data_communication* vfdc,int &retval) {
	for (int i = 0; i < nTotalSockets; i++)
	{
		SOCKETINFO* ptr = SocketInfoArray[i];
		if (FD_ISSET(ptr->sock,&(vfdc->rset)))
		{
			retval = recv(ptr->sock,ptr->buf,BUFSIZE,0);
			if (retval == SOCKET_ERROR){
				if (WSAGetLastError() != WSAEWOULDBLOCK){
					err_display("recv()");
					RemoveSocketInfo(i);
				
				}
				continue;
			}
			else if (retval == 0){
				RemoveSocketInfo(i);
				continue;
			}

			ptr->recvbytes = retval;

			getpeername(ptr->sock, (SOCKADDR*)&(vfdc->clientaddr), &(vfdc->addrlen));
			ptr->buf[retval] = '\0';
			printf("[TCP/%s:%d] %s\n", inet_ntoa(vfdc->clientaddr.sin_addr), ntohs(vfdc->clientaddr.sin_port), ptr->buf);

		}
		if (FD_ISSET(ptr->sock,&(vfdc->wset)))
		{// ������ ������
			retval = send(ptr->sock, ptr->buf + ptr->sendbytes, ptr->recvbytes - ptr->sendbytes, 0);

			if (retval==SOCKET_ERROR)
			{
				if (WSAGetLastError() != WSAEWOULDBLOCK){
					err_display("send()");
					RemoveSocketInfo(i);
					
				}
				continue;
			}
			ptr->sendbytes += retval;
			if (ptr->recvbytes==ptr->sendbytes)
			{
				ptr->recvbytes = ptr->sendbytes = 0;
			}
		}

	}
	
}


int main() {

	int retval;
	
	WSADATA wsa;
	if (Initialize_WSADATA(&wsa) != 0)
		return -1; 

	// socket
	SOCKET listen_sock;
	Initialize_SOCKET(&listen_sock);

	// bind()
	SOCKADDR_IN serveraddr;
	Initialize_SOCKADDR_IN(&serveraddr, sizeof(serveraddr), &listen_sock, retval);

	u_long on=NULL;
	listening_and_set(&listen_sock, retval, on);

	var_for_data_communication vfdc;

	while (1)
	{
		Sock_Set_Initialize(&vfdc, &listen_sock);

		Selecting(&vfdc, retval);

		Socket_set(&vfdc, &listen_sock);

		Communicate_with_client(&vfdc, retval);
	}

	WSACleanup();
	return 0;

}