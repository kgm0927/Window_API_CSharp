/*
 * semaphore_win.c
 * Written by SW. YOON
 */

#include <stdio.h>
#include <stdlib.h>
#include <windows.h>
#include <process.h>    /* _beginthreadex, _endthreadex */

DWORD WINAPI ThreadSend(void *arg);
DWORD WINAPI ThreadRecv(void *arg);
void ErrorHandling(char *message);

HANDLE hSem;    //세마포어 오브젝트 핸들
int number=0;

char thread1[]="A Thread";
char thread2[]="B Thread";
char thread3[]="C Thread";

int main(int argc, char **argv)
{
  HANDLE hThread1, hThread2, hThread3;
  DWORD dwThreadID1, dwThreadID2, dwThreadID3;

  
  hSem=CreateSemaphore(NULL, 0, 1, NULL); // 바이너리 세마포어 생성.
  if(hSem == NULL) {
    puts("세마포어 객체 생성 실패 실패");
    exit(1);
  }

  hThread1 = (HANDLE)_beginthreadex(NULL, 0, ThreadSend, (void*)thread1, 0, (unsigned *)&dwThreadID1);
  hThread2 = (HANDLE)_beginthreadex(NULL, 0, ThreadRecv, (void*)thread2, 0, (unsigned *)&dwThreadID2);
  hThread3 = (HANDLE)_beginthreadex(NULL, 0, ThreadRecv, (void*)thread3, 0, (unsigned *)&dwThreadID3);
  if(hThread1==0 || hThread2==0 || hThread3==0) {
	  puts("쓰레드 생성 오류");
	  exit(1);
  }

  if(WaitForSingleObject(hThread1, INFINITE)==WAIT_FAILED)
	  ErrorHandling("쓰레드 wait 오류");

  if(WaitForSingleObject(hThread2, INFINITE)==WAIT_FAILED)
	  ErrorHandling("쓰레드 wait 오류");

  if(WaitForSingleObject(hThread3, INFINITE)==WAIT_FAILED)
	  ErrorHandling("쓰레드 wait 오류");
  
  printf("최종 number : %d \n", number);
  CloseHandle(hSem); // 세마포어 오브젝트 소멸!
  return 0;
}

DWORD WINAPI ThreadSend(void * arg)
{
  int i;

  for(i=0; i<4; i++){
    while(number != 0)
      Sleep(1000);
    number++;
    printf("실행 : %s,  number : %d \n", (char*)arg, number);
	ReleaseSemaphore(hSem, 1, NULL); // 세마포어 1 증가!
  }  
  return 0;
}

DWORD WINAPI ThreadRecv(void * arg)
{
  int i;

  for(i=0; i<2; i++){
    WaitForSingleObject(hSem, INFINITE);
    number--;
    printf("실행 : %s, number : %d \n", (char*)arg, number);
  }
  return 0;
}

void ErrorHandling(char *message)
{
  fputs(message, stderr);
  fputc('\n', stderr);
  exit(1);
}

