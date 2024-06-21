#include <windows.h>
#include <stdio.h>

int A[100];
CRITICAL_SECTION cs;

DWORD WINAPI MyThread1(LPVOID arg)
{
	EnterCriticalSection(&cs);
	for(int i=0; i<100; i++){
		A[i] = 3;
		Sleep(10);
	}
	LeaveCriticalSection(&cs);

	return 0;
}

DWORD WINAPI MyThread2(LPVOID arg)
{
	EnterCriticalSection(&cs);
	for(int i=99; i>=0; i--){
		A[i] = 4;
		Sleep(10);
	}
	LeaveCriticalSection(&cs);

	return 0;
}

int main()
{
	// 임계영역 초기화
	InitializeCriticalSection(&cs);

	// 두 개의 스레드 생성
	HANDLE hThread[2];
	DWORD ThreadId[2];
	hThread[0] = CreateThread(NULL, 0, MyThread1,
		NULL, 0, &ThreadId[0]);
	hThread[1] = CreateThread(NULL, 0, MyThread2,
		NULL, 0, &ThreadId[1]);

	// 스레드 종료 대기
	WaitForMultipleObjects(2, hThread, TRUE, INFINITE);

	// 임계영역 제거
	DeleteCriticalSection(&cs);

	// 결과 출력
	for(int i=0; i<100; i++){
		printf("%d ", A[i]);
	}
	printf("\n");
	
	return 0;
}