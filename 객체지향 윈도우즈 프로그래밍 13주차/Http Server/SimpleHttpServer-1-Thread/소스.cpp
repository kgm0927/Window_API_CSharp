
#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <winsock2.h>
char    szBuf[2048];

// 클라이언트와 데이터 통신
DWORD WINAPI ProcessClient(LPVOID arg)
{
	char    szInBuf[2048];
	SOCKET client_sock = (SOCKET)arg;
	memset(szInBuf, 0, sizeof(szInBuf));

	recv(client_sock, szInBuf, sizeof(szInBuf), 0);
	// 클라이언트로부터 http packet 받기
	// 한개의 접속 Web Browser client가 
	// Keep-alive 하면 여기서 블럭된다(?). 소켓을 닫았는데..(?)

	printf("%s", szInBuf);
	// 클라이언트에 테스트용 HTTP 메세지 송신
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

	// 윈속 초기화
	if (WSAStartup(MAKEWORD(2, 0), &wsaData) != 0)
	{
		return 1;
	}
	
	// 소켓 만들기
	sockSvr = socket(AF_INET, SOCK_STREAM, 0);
	if (sockSvr == INVALID_SOCKET)
	{
		printf("Socket Error No : %d", WSAGetLastError());
		return 1;
	}

	// 소켓 설정
	addrSockSvr.sin_family = AF_INET;
	addrSockSvr.sin_port = htons(52273);
	addrSockSvr.sin_addr.S_un.S_addr = INADDR_ANY;

	// 소켓 옵션 설정
	setsockopt(sockSvr, SOL_SOCKET, SO_REUSEADDR, (const char *)&bValid, sizeof(bValid));
	if (bind(sockSvr, (struct sockaddr *)&addrSockSvr, sizeof(addrSockSvr)) != 0)
	{
		printf("Bind Error No : %d", WSAGetLastError());
		return 1;
	}

	// TCP클라이언트로 부터 접속 요구를 대기
	if (listen(sockSvr, 20) != 0)
	{
		printf("Listen Error No : %d", WSAGetLastError());
		return 1;
	}

	// 응답용 HTTP 메세지 작성
	memset(szBuf, 0, sizeof(szBuf));
	_snprintf(szBuf, sizeof(szBuf),
		"HTTP/1.0 200 OK\r\n"
		"Content-Length: 25\r\n"         //길이가 정확하지 않으면...
		"Content-Type: text/html\r\n"    //text/plain
		"Connection: Close\r\n"			//(HTTP/1.1의 default인)Keep-Alive해제, but.....
		"\r\n"
		"<h1>초간단 HTTP 서버</h1>\\n");

	while (1) {
		// TCP클라이언트로 부터 접속 요구 받기\tab   
		nlen = sizeof(addrSockclt);
		client_sock = accept(sockSvr, (struct sockaddr *)&addrSockclt, &nlen);
		if (client_sock == INVALID_SOCKET)
		{
			printf("Accept Error No : %d", WSAGetLastError());
			return 1;
		}

		printf("[TCP 서버] 클라이언트 접속: IP 주소=%s, 포트 번호=%d\n",
			inet_ntoa(addrSockclt.sin_addr), ntohs(addrSockclt.sin_port));
		
		// 스레드 생성
		hThread = CreateThread(NULL, 0, ProcessClient,
			(LPVOID)client_sock, 0, &ThreadId);
		if (hThread == NULL)
			printf("[오류] 스레드 생성 실패!\n");
		else
			CloseHandle(hThread);
		
		
	}

	closesocket(sockSvr);
	// 윈속 종료
	WSACleanup();
	return 0;
}

