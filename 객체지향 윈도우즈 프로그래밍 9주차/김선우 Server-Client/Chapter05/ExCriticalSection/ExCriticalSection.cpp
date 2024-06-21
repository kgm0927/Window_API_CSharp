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
	// �Ӱ迵�� �ʱ�ȭ
	InitializeCriticalSection(&cs);

	// �� ���� ������ ����
	HANDLE hThread[2];
	DWORD ThreadId[2];
	hThread[0] = CreateThread(NULL, 0, MyThread1,
		NULL, 0, &ThreadId[0]);
	hThread[1] = CreateThread(NULL, 0, MyThread2,
		NULL, 0, &ThreadId[1]);

	// ������ ���� ���
	WaitForMultipleObjects(2, hThread, TRUE, INFINITE);

	// �Ӱ迵�� ����
	DeleteCriticalSection(&cs);

	// ��� ���
	for(int i=0; i<100; i++){
		printf("%d ", A[i]);
	}
	printf("\n");
	
	return 0;
}