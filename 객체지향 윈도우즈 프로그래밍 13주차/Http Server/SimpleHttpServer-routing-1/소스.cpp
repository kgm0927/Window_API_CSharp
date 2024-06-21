
#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <winsock2.h>
#include <locale.h>
#include <wchar.h>

char* ConvertWCtoC(wchar_t* str) //tchar_t -> char
{
	//반환할 char* 변수 선언
	char* pStr;

	//입력받은 wchar_t 변수의 길이를 구함
	int strSize = WideCharToMultiByte(CP_ACP, 0, str, -1, NULL, 0, NULL, NULL);

	//char* 메모리 할당
	pStr = new char[strSize];

	//형 변환
	WideCharToMultiByte(CP_ACP, 0, str, -1, pStr, strSize, 0, 0);

	return pStr;
}

int main()
{
	WSADATA wsaData;
	SOCKET sockSvr;
	SOCKET sockSS;
	int nlen;
	struct sockaddr_in addrSockSvr;
	struct sockaddr_in addrSockclt;
	long nRet;
	BOOL bValid = 1;
	char szBuf[1024];
	char szInBuf[1024];


	///////////////////////////////////////////////////
	FILE* f = NULL; //HTML 파일을 불러오기 위한 FILE
	//HTML 데이터를 조합하기 전에 구분
	char header[1024]; //웹 헤더 문자열 저장
	char path[1024]; //리퀘스트 경로 문자열 저장
	char fileName[1024]; //파일 이름 문자열 저장
	char temp[1024]; //웹데이터 문자열 임시 저장
	wchar_t html[1024]; //HTML 데이터 (멀티바이트 지원)
	///////////////////////////////////////////////////

	setlocale(LC_ALL, "ko-KR"); //인코딩 설정
	
	
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
	addrSockSvr.sin_port = htons(80);
	addrSockSvr.sin_addr.S_un.S_addr = INADDR_ANY;

	// 소켓 옵션 설정
	setsockopt(sockSvr, SOL_SOCKET, SO_REUSEADDR, (const char *)&bValid, sizeof(bValid));
	if (bind(sockSvr, (struct sockaddr *)&addrSockSvr, sizeof(addrSockSvr)) != 0)
	{
		printf("Bind Error No : %d", WSAGetLastError());
		return 1;
	}

	// TCP클라이언트로 부터 접속 요구를 대기
	if (listen(sockSvr, 5) != 0)
	{
		printf("Listen Error No : %d", WSAGetLastError());
		return 1;
	}

	// 응답용 HTTP 메세지 작성


	memset(header, 0, sizeof(header));

	_snprintf(header, sizeof(header), 
		"HTTP/1.0 200 OK\r\n"
		"Content-Length: 2048\r\n"
		"Content-Type: text/html\r\n"
		"\r\n");

	while (1) {
		// TCP클라이언트로 부터 접속 요구 받기
		nlen = sizeof(addrSockclt);
		sockSS = accept(sockSvr, (struct sockaddr *)&addrSockclt, &nlen);
		if (sockSS == INVALID_SOCKET)
		{
			printf("Accept Error No : %d", WSAGetLastError());
			return 1;
		}

		memset(szBuf, 0, sizeof(szBuf)); //버퍼 초기화
		memset(szInBuf, 0, sizeof(szInBuf)); //버퍼 초기화
		memset(path, 0, sizeof(path));

		recv(sockSS, szInBuf, sizeof(szInBuf), 0);
		// 클라이언트로부터 http packet 받기

		// 리퀘스트 정보 파싱
		for (int i = 0; i < strlen(szInBuf); i++) {
			if (szInBuf[i] == 'G' && szInBuf[i + 1] == 'E' && szInBuf[i + 2] == 'T' && szInBuf[i + 3] == ' ') { //GET일 경우
				for (int j = 0; szInBuf[i + 4 + j] != ' '; j++) {
					path[j] = szInBuf[i + 4 + j];
					path[j + 1] = '\0';
				}
				break;
			}
			else if (szInBuf[i] == 'P' && szInBuf[i + 1] == 'O' && szInBuf[i + 2] == 'S' && szInBuf[i + 3] == 'T' && szInBuf[i + 4] == ' ') { //POST일 경우
				for (int j = 0; szInBuf[i + 4 + j] != ' '; j++) {
					path[j] = szInBuf[i + 4 + j];
					path[j + 1] = '\0';
				}
				break;
			}
		}
		printf("request: %s \n", path);

		memset(fileName, 0, sizeof(fileName));

		// 파싱한 후, 해당 정보에 맞는 html 데이터를 전송하기 위해 파일 이름 지정.
		///*
		if (strcmp(path, "/") == 0) {
			strcpy(fileName, ".\\html\\index.html");
		}
		else if (strcmp(path, "/http") == 0) {
			strcpy(fileName, ".\\html\\http.html");
		}
		else if (strcmp(path, "/univ") == 0) {
			strcpy(fileName, ".\\html\\univ.html");
		}
		else if (strcmp(path, "/js") == 0) {
			strcpy(fileName, ".\\html\\js.html");
		}
		//*/
		
		// 필요한 파일을 연다.
		//f = fopen("C:\\Users\\PC\\Desktop\\NodeJS\\HttpServer-C\\SimpleHttpServer-file-input-html-router\\html\\index.html", "rt+,ccs=UTF-8");
		f = fopen(fileName, "rt+,ccs=UTF-8");
		memset(html, 0, sizeof(html));

		if (f == NULL) {
			printf("File Can't Load (%s)\n", fileName);
			break;
		}
		else {
			printf("File Load (%s)\n", fileName);
			while (feof(f) == 0) {
				wchar_t buf[1024];
				fgetws(buf, sizeof(buf), f);
				wcscat(html, buf);
			}
			fclose(f);
		}
		
		memset(temp, 0, sizeof(temp));
		
		// 웹 데이터 조합
		strcat(temp, header);
		strcat(temp, ConvertWCtoC(html));

		_snprintf(szBuf, sizeof(szBuf), temp);
		

		// 전송, 에러가 뜰 경우 웹소켓 종료
		if (send(sockSS, szBuf, (int)strlen(szBuf), 0) < 1)
		{
			printf("send : %d\n", WSAGetLastError());
			printf("Error");
			break;
		}

		printf("%s", szInBuf);
		// 클라이언트에 테스트용 HTTP 메세지 송신
		//send(sockSS, szBuf, (int)strlen(szBuf), 0);
		closesocket(sockSS);
	}
	// 윈속 종료
	WSACleanup();
	return 0;
}
