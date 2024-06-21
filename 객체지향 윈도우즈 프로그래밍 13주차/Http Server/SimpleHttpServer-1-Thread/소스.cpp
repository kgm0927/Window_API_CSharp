
#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <winsock2.h>
char    szBuf[2048];

// Ŭ���̾�Ʈ�� ������ ���
DWORD WINAPI ProcessClient(LPVOID arg)
{
	char    szInBuf[2048];
	SOCKET client_sock = (SOCKET)arg;
	memset(szInBuf, 0, sizeof(szInBuf));

	recv(client_sock, szInBuf, sizeof(szInBuf), 0);
	// Ŭ���̾�Ʈ�κ��� http packet �ޱ�
	// �Ѱ��� ���� Web Browser client�� 
	// Keep-alive �ϸ� ���⼭ ���ȴ�(?). ������ �ݾҴµ�..(?)

	printf("%s", szInBuf);
	// Ŭ���̾�Ʈ�� �׽�Ʈ�� HTTP �޼��� �۽�
	send(client_sock, szBuf, (int)strlen(szBuf), 0);
	
	closesocket(client_sock);
	// closesocket()
	return 0;
}

int main()
{
	WSADATA wsaData;
	SOCKET    sockSvr;
	SOCKET client_sock;
	int        nlen;
	struct    sockaddr_in    addrSockSvr;
	struct    sockaddr_in    addrSockclt;
	long    nRet;
	BOOL    bValid = 1;
	//char    szBuf[2048];
	//char    szInBuf[2048];

	HANDLE hThread;
	DWORD ThreadId;

	// ���� �ʱ�ȭ
	if (WSAStartup(MAKEWORD(2, 0), &wsaData) != 0)
	{
		return 1;
	}
	
	// ���� �����
	sockSvr = socket(AF_INET, SOCK_STREAM, 0);
	if (sockSvr == INVALID_SOCKET)
	{
		printf("Socket Error No : %d", WSAGetLastError());
		return 1;
	}

	// ���� ����
	addrSockSvr.sin_family = AF_INET;
	addrSockSvr.sin_port = htons(52273);
	addrSockSvr.sin_addr.S_un.S_addr = INADDR_ANY;

	// ���� �ɼ� ����
	setsockopt(sockSvr, SOL_SOCKET, SO_REUSEADDR, (const char *)&bValid, sizeof(bValid));
	if (bind(sockSvr, (struct sockaddr *)&addrSockSvr, sizeof(addrSockSvr)) != 0)
	{
		printf("Bind Error No : %d", WSAGetLastError());
		return 1;
	}

	// TCPŬ���̾�Ʈ�� ���� ���� �䱸�� ���
	if (listen(sockSvr, 20) != 0)
	{
		printf("Listen Error No : %d", WSAGetLastError());
		return 1;
	}

	// ����� HTTP �޼��� �ۼ�
	memset(szBuf, 0, sizeof(szBuf));
	_snprintf(szBuf, sizeof(szBuf),
		"HTTP/1.0 200 OK\r\n"
		"Content-Length: 25\r\n"         //���̰� ��Ȯ���� ������...
		"Content-Type: text/html\r\n"    //text/plain
		"Connection: Close\r\n"			//(HTTP/1.1�� default��)Keep-Alive����, but.....
		"\r\n"
		"<h1>�ʰ��� HTTP ����</h1>\\n");

	while (1) {
		// TCPŬ���̾�Ʈ�� ���� ���� �䱸 �ޱ�\tab   
		nlen = sizeof(addrSockclt);
		client_sock = accept(sockSvr, (struct sockaddr *)&addrSockclt, &nlen);
		if (client_sock == INVALID_SOCKET)
		{
			printf("Accept Error No : %d", WSAGetLastError());
			return 1;
		}

		printf("[TCP ����] Ŭ���̾�Ʈ ����: IP �ּ�=%s, ��Ʈ ��ȣ=%d\n",
			inet_ntoa(addrSockclt.sin_addr), ntohs(addrSockclt.sin_port));
		
		// ������ ����
		hThread = CreateThread(NULL, 0, ProcessClient,
			(LPVOID)client_sock, 0, &ThreadId);
		if (hThread == NULL)
			printf("[����] ������ ���� ����!\n");
		else
			CloseHandle(hThread);
		
		
	}

	closesocket(sockSvr);
	// ���� ����
	WSACleanup();
	return 0;
}

