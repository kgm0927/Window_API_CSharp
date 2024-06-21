#include <windows.h>
#include <stdio.h>

DWORD WINAPI MyThread(LPVOID arg)
{
	while(1)
		printf("Running MyThread()\n");

	return 0;
}

int main()
{
	// 스레드 생성
	DWORD ThreadId;
	HANDLE hThread = CreateThread(NULL, 0, MyThread, NULL, 0, &ThreadId);
	if(hThread == NULL) return -1;

	// 우선순위 변경
	SetThreadPriority(hThread, THREAD_PRIORITY_ABOVE_NORMAL);

	while(1)
		printf("Running main()....................\n");

	return 0;
}