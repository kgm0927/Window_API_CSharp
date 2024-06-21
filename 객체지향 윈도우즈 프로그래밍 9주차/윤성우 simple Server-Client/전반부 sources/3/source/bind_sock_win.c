/*
 * bind_sock_win.c
 * Written by SW. YOON
 */

#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <winsock2.h>

void ErrorHandling(char *message);

int main(int argc, char **argv)
{
	WSADATA	wsaData;

	SOCKET hServSock;
	char *servIP="127.0.0.1";
	char *servPort="9190";
	SOCKADDR_IN  servAddr;  /* struct sockaddr_in servAddr; */
	
	if(WSAStartup(MAKEWORD(2, 2), &wsaData) != 0) /* Load Winsock 2.2 DLL */
		ErrorHandling("WSAStartup() error!");

	hServSock=socket(PF_INET, SOCK_STREAM, 0);   
	if(hServSock == INVALID_SOCKET)
		ErrorHandling("socket() error");
  
	memset(&servAddr, 0, sizeof(servAddr));
	servAddr.sin_family=AF_INET;
	servAddr.sin_addr.s_addr=inet_addr(servIP);
	servAddr.sin_port=htons(atoi(servPort));

	if( bind(hServSock, (SOCKADDR*) &servAddr, sizeof(servAddr))==SOCKET_ERROR ) /* ���Ͽ� �ּҸ� �Ҵ� */
		ErrorHandling("bind() error");
  
	printf("���� ��ũ���� %d �� ���Ͽ� �ּ� �Ҵ���� �Ϸ�!\n\n", hServSock);

  	WSACleanup();
	return 0;
}

void ErrorHandling(char *message)
{
	fputs(message, stderr);
	fputc('\n', stderr);
	exit(1);
}
