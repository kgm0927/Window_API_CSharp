#include <windows.h>
#include <stdio.h>
LRESULT CALLBACK WndProc (HWND, UINT, WPARAM, LPARAM) ;

int WINAPI WinMain (HINSTANCE hInstance, HINSTANCE hPrevInstance,
                    PSTR szCmdLine, int iCmdShow)
{
     static TCHAR szAppName[] = TEXT ("Test 2002-2-1") ;
     HWND         hwnd ;
     MSG          msg ;
     WNDCLASS     wndclass ;

     wndclass.style         = CS_HREDRAW | CS_VREDRAW ;
     wndclass.lpfnWndProc   = WndProc ;
     wndclass.cbClsExtra    = 0 ;
     wndclass.cbWndExtra    = 0 ;
     wndclass.hInstance     = hInstance ;
     wndclass.hIcon         = LoadIcon (NULL, IDI_APPLICATION) ;
     wndclass.hCursor       = LoadCursor (NULL, IDC_ARROW) ;
     wndclass.hbrBackground = (HBRUSH) GetStockObject (WHITE_BRUSH) ;
     wndclass.lpszMenuName  = NULL ;
     wndclass.lpszClassName = szAppName ;

     if (!RegisterClass (&wndclass))
     {
          MessageBox (NULL, TEXT ("This program requires Windows NT!"), 
                      szAppName, MB_ICONERROR) ;
          return 0 ;
     }
     
     hwnd = CreateWindow (szAppName,                  // window class name
                          TEXT ("Test 2002-2-1"), // window caption
                          WS_OVERLAPPEDWINDOW,        // window style
                          CW_USEDEFAULT,              // initial x position
                          CW_USEDEFAULT,              // initial y position
                          CW_USEDEFAULT,              // initial x size
                          CW_USEDEFAULT,              // initial y size
                          NULL,                       // parent window handle
                          NULL,                       // window menu handle
                          hInstance,                  // program instance handle
                          NULL) ;                     // creation parameters
     
     ShowWindow (hwnd, iCmdShow) ;
     UpdateWindow (hwnd) ;
     
     while (GetMessage (&msg, NULL, 0, 0))
     {
          TranslateMessage (&msg) ;
          DispatchMessage (&msg) ;
     }
     return msg.wParam ;
}

FILE * fpin;

LRESULT CALLBACK WndProc (HWND hwnd, UINT message, WPARAM wParam, LPARAM lParam)
{
     HDC         hdc, MemDC;
	 HBITMAP MyBitmap, OldBitmap;
     PAINTSTRUCT ps ;
	 int i,j, temp;

	 static int image[256][256];
	      
     switch (message)
     {
     case WM_CREATE:
		 if((fpin=fopen("data.txt", "r"))==NULL)
		 {
			MessageBox(hwnd, "Read File open failed", "Fiel Open Error", MB_OK);
		 }

		 for(i=0; i<256; i++)
			 for(j=0; j<256; j++)
			 {
				 fscanf(fpin, "%d", &image[i][j]);
			 }

		 fclose(fpin);
		 return 0 ;
	 
     
	 case WM_PAINT:
          hdc = BeginPaint (hwnd, &ps) ;
		  MemDC=CreateCompatibleDC(hdc);
		  MyBitmap=CreateCompatibleBitmap(hdc,256,256);
		  OldBitmap=(HBITMAP)SelectObject(MemDC, MyBitmap);

		  for(i=0; i<256; i++)
			  for(j=0; j<256; j++)
			  {
				    temp=image[i][j];
					SetPixel(MemDC,i,j,RGB(temp,temp,temp));
			  }
		  
		  BitBlt(hdc, 0,0,256,256,MemDC,0,0,SRCCOPY);
		  SelectObject(MemDC,OldBitmap);
		  DeleteObject(MyBitmap);
		  DeleteDC(MemDC);
		  EndPaint (hwnd, &ps) ;
          return 0 ;
          
     case WM_DESTROY:
          PostQuitMessage (0) ;
          return 0 ;
     }
     return DefWindowProc (hwnd, message, wParam, lParam) ;
}