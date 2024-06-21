#include <windows.h>
#include <stdio.h>

struct Point3D
{
	int x, y, z;
};

DWORD WINAPI MyThread(LPVOID arg)
{
	Point3D *pt = (Point3D *)arg;
	while(1){
		printf("Running another thread: %d, %d, %d\n", 
			pt->x, pt->y, pt->z);
		Sleep(1000);
	}

	return 0;
}

int main()
{
	// 첫 번째 스레드 생성
	Point3D pt1 = {10, 20, 30};
	DWORD ThreadId1;
	HANDLE hThread1 = CreateThread(NULL, 0, MyThread, 
		(LPVOID)&pt1, 0, &ThreadId1);
	if(hThread1 == NULL) return -1;
	CloseHandle(hThread1);

	// 두 번째 스레드 생성
	Point3D pt2 = {40, 50, 60};
	DWORD ThreadId2;
	HANDLE hThread2 = CreateThread(NULL, 0, MyThread, 
		(LPVOID)&pt2, 0, &ThreadId2);
	if(hThread2 == NULL) return -1;
	CloseHandle(hThread2);

	while(1){
		printf("Running primary thread...\n");
		Sleep(1000);
	}

	return 0;
}