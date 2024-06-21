/*
 * low_read.c
 * Written by SW. YOON
 */

#include <stdio.h>
#include <unistd.h>
#include <sys/types.h>
#include <sys/stat.h>
#include <fcntl.h>

#define BUFSIZE 100

void error_handling(char* message);

int main(void)
{
	int fildes;
	char buf[BUFSIZE];
	
	fildes=open("data.txt", O_RDONLY);  /* data.txt��� �̸��� ���� ���� */
	if( fildes==-1)
		error_handling("open() error!");
	
	printf("���� �� ������ ���� ��ũ���ʹ� %d �Դϴ�.\n" , fildes);
	
	if(read(fildes, buf, sizeof(buf))==-1)  /* ���Ͽ� �����ϴ� ������ buf�� �о� ���δ�. */
		error_handling("read() error!");

	printf("������ ���� : %s", buf);
	
	close(fildes);
	return 0;
}

void error_handling(char* message)
{
	fputs(message, stderr);
	fputc('\n', stderr);
	exit(1);
}
