/*
 * event.c
 * Written by SW. YOON
 */

#include <stdio.h>
#include <stdlib.h>
#include <windows.h>
#include <process.h>    /* _beginthreadex, _endthreadex */

DWORD WINAPI NumberOfA(void *arg);
DWORD WINAPI NumberOfOthers(void *arg);

void ErrorHandling(char *message);
char String[100];
HANDLE hEvent;

int main(int argc, char **argv) 
{	
	HANDLE  hThread1, hThread2;
	DWORD dwThreadID1, dwThreadID2;

	hEvent = CreateEvent(NULL, TRUE, FALSE, NULL);
	if(hEvent==NULL){
		puts("Event ������Ʈ ���� ����");
		exit(1);
	}
	
	hThread1 = (HANDLE)_beginthreadex(NULL, 0, NumberOfA, NULL, 0, (unsigned *)&dwThreadID1);
	hThread2 = (HANDLE)_beginthreadex(NULL, 0, NumberOfOthers, NULL, 0, (unsigned *)&dwThreadID2);

	if(hThread1==0 || hThread2==0) {
		puts("������ ���� ����");
		exit(1);
	}	

	fputs("���ڿ��� �Է� �ϼ��� : ", stdout); 
	fgets(String, 30, stdin);
	SetEvent(hEvent);

	if(WaitForSingleObject(hThread1, INFINITE)==WAIT_FAILED)
		ErrorHandling("������ wait ����");
		
	if(WaitForSingleObject(hThread2, INFINITE)==WAIT_FAILED)
		ErrorHandling("������ wait ����");

 	CloseHandle(hEvent); //Event ������Ʈ �Ҹ�
    return 0;
}

DWORD WINAPI NumberOfA(void *arg) 
{
  int i;
  int count=0;
  
  WaitForSingleObject(hEvent, INFINITE); //Event�� ��´�.
  for(i=0; String[i]!=0; i++) {
	  if(String[i]=='A')
		  count++;
  }

  printf("A ������ �� : %d\n", count);
  return 0;
}

DWORD WINAPI NumberOfOthers(void *arg) 
{
  int i;
  int count=0;
  
  WaitForSingleObject(hEvent, INFINITE); //Event�� ��´�.
  for(i=0; String[i]!=0; i++) {
	  if(String[i]!='A')
		  count++;
  }

  printf("A �̿��� ���� �� : %d\n", count-1);
  return 0;
}

void ErrorHandling(char *message)
{
  fputs(message, stderr);
  fputc('\n', stderr);
  exit(1);
}
