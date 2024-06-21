#include <windows.h>
#include<stdlib.h>
#include<time.h>
#include<String.h>



LRESULT CALLBACK WndProc(HWND,UINT,WPARAM,LPARAM);
HINSTANCE g_hInst;
HWND hWndMain;
LPCTSTR lpszClass=TEXT("진예은");
struct _myP//좌표 브러쉬 크기
{
	POINT p; //점의좌표 xy자동으로나옴
	int iColor;
	COLORREF color; //COLORREF   쓰는법->data[iCount].color=RGB(GetRvalue,,); GetRvalue(data[iCount].color)
	int iSize;
	//int r;
	
};
_myP P[1000];


int APIENTRY WinMain(HINSTANCE hInstance,HINSTANCE hPrevInstance
					 ,LPSTR lpszCmdParam,int nCmdShow)
{
	HWND hWnd;
	MSG Message;
	WNDCLASS WndClass;
	g_hInst=hInstance;
	
	WndClass.cbClsExtra=0;
	WndClass.cbWndExtra=0;
	WndClass.hbrBackground=(HBRUSH)GetStockObject(WHITE_BRUSH);//배경을 흰색
	WndClass.hCursor=LoadCursor(NULL,IDC_ARROW);
	WndClass.hIcon=LoadIcon(NULL,IDI_APPLICATION);
	WndClass.hInstance=hInstance;
	WndClass.lpfnWndProc=(WNDPROC)WndProc;
	WndClass.lpszClassName=lpszClass;
	WndClass.lpszMenuName=NULL;
	WndClass.style=CS_HREDRAW | CS_VREDRAW;
	RegisterClass(&WndClass);
	
	hWnd=CreateWindow(lpszClass,lpszClass,WS_OVERLAPPEDWINDOW,
		CW_USEDEFAULT,CW_USEDEFAULT,CW_USEDEFAULT,CW_USEDEFAULT,
		NULL,(HMENU)NULL,hInstance,NULL);
	ShowWindow(hWnd,nCmdShow);
	hWndMain=hWnd;
	
	while(GetMessage(&Message,0,0,0)) {
		TranslateMessage(&Message);
		DispatchMessage(&Message);
	}
	return Message.wParam;
}
//*  Memory DC 사용하지 않고 그리기
LRESULT CALLBACK WndProc(HWND hWnd,UINT iMessage,WPARAM wParam,LPARAM lParam)
{
	HBITMAP MyBitmap, OldBitmap;
	HDC hdc,MemDC;
	PAINTSTRUCT ps;
	int i, iSize, x,y;
	HBRUSH hBrush;
	
	switch(iMessage) {
	case WM_CREATE:
		srand((unsigned int)time(NULL));
		return 0;
			
	case WM_PAINT: 
		hdc=BeginPaint(hWnd, &ps);
		for(i=0;i<10000;i++)
		{
			hBrush=(HBRUSH)CreateSolidBrush(RGB(rand()%256,rand()%256,rand()%256));
			SelectObject(hdc,hBrush);	
			iSize=rand()%50+1;
			x=rand()%900;
			y=rand()%600;
			Ellipse(hdc,x-iSize,y-iSize,x+iSize,y+iSize);
		}
	
		EndPaint(hWnd, &ps);
		return 0;

	case WM_DESTROY:
		PostQuitMessage(0);
		return 0;
	}
	return(DefWindowProc(hWnd,iMessage,wParam,lParam));
}
// */

/*  Memory DC 사용
LRESULT CALLBACK WndProc(HWND hWnd,UINT iMessage,WPARAM wParam,LPARAM lParam)
{
	HBITMAP MyBitmap, OldBitmap;
	HDC hdc,MemDC;
	PAINTSTRUCT ps;
	int i, iSize, x,y;
	HBRUSH hBrush;
	RECT rt;
	
	switch(iMessage) {
	case WM_CREATE:
		srand((unsigned int)time(NULL));
		return 0;
		
		
	case WM_PAINT: 
		hdc=BeginPaint(hWnd, &ps);
	    MemDC=CreateCompatibleDC(hdc); 
	   	MyBitmap=CreateCompatibleBitmap(hdc,1000,700); 
		OldBitmap=(HBITMAP)SelectObject(MemDC, MyBitmap);

		SetRect(&rt, 0, 0, 900, 600);
		FillRect(MemDC, &rt, (HBRUSH)GetStockObject(WHITE_BRUSH));

		for(i=0;i<10000;i++)
		{
			hBrush=(HBRUSH)CreateSolidBrush(RGB(rand()%256,rand()%256,rand()%256));
			SelectObject(MemDC,hBrush);	
			iSize=rand()%50+1;
			x=rand()%900;
			y=rand()%600;
			Ellipse(MemDC,x-iSize,y-iSize,x+iSize,y+iSize);
			DeleteObject(SelectObject(MemDC, (HBRUSH)GetStockObject(WHITE_BRUSH)));
		}
		BitBlt(hdc, 0,0,1000,700,MemDC,0,0,SRCCOPY);
		SelectObject(MemDC,OldBitmap);
		DeleteObject(MyBitmap);
		DeleteDC(MemDC);
		EndPaint(hWnd, &ps);
		return 0;

	case WM_DESTROY:
		PostQuitMessage(0);
		return 0;
	}
	return(DefWindowProc(hWnd,iMessage,wParam,lParam));
}
// */
