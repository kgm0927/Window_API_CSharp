
#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <winsock2.h>
#include <locale.h>
#include <wchar.h>

char* ConvertWCtoC(wchar_t* str) //tchar_t -> char
{
	//��ȯ�� char* ���� ����
	char* pStr;

	//�Է¹��� wchar_t ������ ���̸� ����
	int strSize = WideCharToMultiByte(CP_ACP, 0, str, -1, NULL, 0, NULL, NULL);

	//char* �޸� �Ҵ�
	pStr = new char[strSize];

	//�� ��ȯ
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
	FILE* f = NULL; //HTML ������ �ҷ����� ���� FILE
	//HTML �����͸� �����ϱ� ���� ����
	char header[1024]; //�� ��� ���ڿ� ����
	char path[1024]; //������Ʈ ��� ���ڿ� ����
	char fileName[1024]; //���� �̸� ���ڿ� ����
	char temp[1024]; //�������� ���ڿ� �ӽ� ����
	wchar_t html[1024]; //HTML ������ (��Ƽ����Ʈ ����)
	///////////////////////////////////////////////////

	setlocale(LC_ALL, "ko-KR"); //���ڵ� ����
	
	
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
	addrSockSvr.sin_port = htons(80);
	addrSockSvr.sin_addr.S_un.S_addr = INADDR_ANY;

	// ���� �ɼ� ����
	setsockopt(sockSvr, SOL_SOCKET, SO_REUSEADDR, (const char *)&bValid, sizeof(bValid));
	if (bind(sockSvr, (struct sockaddr *)&addrSockSvr, sizeof(addrSockSvr)) != 0)
	{
		printf("Bind Error No : %d", WSAGetLastError());
		return 1;
	}

	// TCPŬ���̾�Ʈ�� ���� ���� �䱸�� ���
	if (listen(sockSvr, 5) != 0)
	{
		printf("Listen Error No : %d", WSAGetLastError());
		return 1;
	}

	// ����� HTTP �޼��� �ۼ�


	memset(header, 0, sizeof(header));

	_snprintf(header, sizeof(header), 
		"HTTP/1.0 200 OK\r\n"
		"Content-Length: 2048\r\n"
		"Content-Type: text/html\r\n"
		"\r\n");

	while (1) {
		// TCPŬ���̾�Ʈ�� ���� ���� �䱸 �ޱ�
		nlen = sizeof(addrSockclt);
		sockSS = accept(sockSvr, (struct sockaddr *)&addrSockclt, &nlen);
		if (sockSS == INVALID_SOCKET)
		{
			printf("Accept Error No : %d", WSAGetLastError());
			return 1;
		}

		memset(szBuf, 0, sizeof(szBuf)); //���� �ʱ�ȭ
		memset(szInBuf, 0, sizeof(szInBuf)); //���� �ʱ�ȭ
		memset(path, 0, sizeof(path));

		recv(sockSS, szInBuf, sizeof(szInBuf), 0);
		// Ŭ���̾�Ʈ�κ��� http packet �ޱ�

		// ������Ʈ ���� �Ľ�
		for (int i = 0; i < strlen(szInBuf); i++) {
			if (szInBuf[i] == 'G' && szInBuf[i + 1] == 'E' && szInBuf[i + 2] == 'T' && szInBuf[i + 3] == ' ') { //GET�� ���
				for (int j = 0; szInBuf[i + 4 + j] != ' '; j++) {
					path[j] = szInBuf[i + 4 + j];
					path[j + 1] = '\0';
				}
				break;
			}
			else if (szInBuf[i] == 'P' && szInBuf[i + 1] == 'O' && szInBuf[i + 2] == 'S' && szInBuf[i + 3] == 'T' && szInBuf[i + 4] == ' ') { //POST�� ���
				for (int j = 0; szInBuf[i + 4 + j] != ' '; j++) {
					path[j] = szInBuf[i + 4 + j];
					path[j + 1] = '\0';
				}
				break;
			}
		}
		printf("request: %s \n", path);

		memset(fileName, 0, sizeof(fileName));

		// �Ľ��� ��, �ش� ������ �´� html �����͸� �����ϱ� ���� ���� �̸� ����.
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
		
		// �ʿ��� ������ ����.
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
		
		// �� ������ ����
		strcat(temp, header);
		strcat(temp, ConvertWCtoC(html));

		_snprintf(szBuf, sizeof(szBuf), temp);
		

		// ����, ������ �� ��� ������ ����
		if (send(sockSS, szBuf, (int)strlen(szBuf), 0) < 1)
		{
			printf("send : %d\n", WSAGetLastError());
			printf("Error");
			break;
		}

		printf("%s", szInBuf);
		// Ŭ���̾�Ʈ�� �׽�Ʈ�� HTTP �޼��� �۽�
		//send(sockSS, szBuf, (int)strlen(szBuf), 0);
		closesocket(sockSS);
	}
	// ���� ����
	WSACleanup();
	return 0;
}
