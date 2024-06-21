/*
 * make_socket.c
 * Written by SW. YOON
 */

#include <stdio.h>
#include <stdlib.h>
#include <arpa/inet.h>
#include <sys/socket.h>

void error_handling(char *message);

int main(int argc, char **argv)
{
	int tcp_socket;
	int udp_socket;
	
	/* �������� TCP ���� ���� */
	tcp_socket=socket(PF_INET, SOCK_STREAM, IPPROTO_TCP);
	if(tcp_socket==-1)
		error_handling("socket() error");
	
	/* �� �������� UDP ���� ���� */
	udp_socket=socket(PF_INET, SOCK_DGRAM, IPPROTO_UDP);
	if(udp_socket==-1)
		error_handling("socket() error");
	
	printf("������ TCP ������ ���� ��ũ���� : %d \n", tcp_socket);
	printf("������ UDP ������ ���� ��ũ���� : %d \n", udp_socket);
	
	close(tcp_socket);
	close(udp_socket);
	return 0;
}

void error_handling(char *message)
{
	fputs(message, stderr);
	fputc('\n', stderr);
	exit(1);
}
