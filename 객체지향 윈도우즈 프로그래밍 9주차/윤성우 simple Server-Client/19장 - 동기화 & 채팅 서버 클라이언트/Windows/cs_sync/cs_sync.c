/*
 * cs_sync.c
 * Written by SW. YOON
 */

#include <stdio.h>
#include <stdlib.h>
#include <windows.h>
#include <process.h>    /* _beginthreadex, _endthreadex */

DWORD WINAPI ThreadSummation(void *arg);
void ErrorHandling(char *message);

double sum=0;
int sum1[]={1, 50000};
int sum2[]={50001, 100000};

CRITICAL_SECTION cs;

int main(int argc, char **argv) 
{
	HANDLE hThread1, hThread2;
	DWORD dwThreadID1, dwThreadID2;

	InitializeCriticalSection(&cs);

    hThread1 = (HANDLE)_beginthreadex(NULL, 0, ThreadSummation, (long*)sum1, 0, (unsigned *)&dwThreadID1);
	hThread2 = (HANDLE)_beginthreadex(NULL, 0, ThreadSummation, (long*)sum2, 0, (unsigned *)&dwThreadID2);
    if(hThread1==0 || hThread2==0) 
		ErrorHandling("쓰레드 생성 오류");

	if(WaitForSingleObject(hThread1, INFINITE)==WAIT_FAILED)
		ErrorHandling("쓰레드 wait 오류");
		
	if(WaitForSingleObject(hThread2, INFINITE)==WAIT_FAILED)
		ErrorHandling("쓰레드 wait 오류");

	printf("main함수 종료, sum = %lf \n", sum);
	DeleteCriticalSection(&cs);

    return 0;
}

DWORD WINAPI ThreadSummation(void *arg) 
{
  int start=((int*)arg)[0];
  int end=((int*)arg)[1];

  for( ; start<=end; start++) {
	  EnterCriticalSection(&cs);
	  sum+=start;
	  LeaveCriticalSection(&cs);
  }
  return 0;
}

void ErrorHandling(char *message)
{
  fputs(message, stderr);
  fputc('\n', stderr);
  exit(1);
}
