#include <winsock2.h>
#include <stdio.h>

int main(int argc, char* argv[])
{
	WSADATA wsa;
	if(WSAStartup(MAKEWORD(2, 2), &wsa) != 0)
		return -1;

	// 원래의 IP 주소 출력
	char *ipaddr = "147.46.114.70";
	printf("IP 주소 = %s\n", ipaddr);

	// inet_addr() 함수 연습
	printf("IP 주소(변환 후) = 0x%x\n", inet_addr(ipaddr));

	// inet_ntoa() 함수 연습
	IN_ADDR temp;
	temp.s_addr = inet_addr(ipaddr);
	printf("IP 주소(변환 후) = %s\n", inet_ntoa(temp));

	WSACleanup();
	return 0;
}