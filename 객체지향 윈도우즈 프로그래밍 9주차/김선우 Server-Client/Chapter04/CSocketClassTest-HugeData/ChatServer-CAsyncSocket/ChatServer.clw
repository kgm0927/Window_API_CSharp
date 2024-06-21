; CLW file contains information for the MFC ClassWizard

[General Info]
Version=1
LastClass=CChatServerDlg
LastTemplate=CAsyncSocket
NewFileInclude1=#include "stdafx.h"
NewFileInclude2=#include "ChatServer.h"

ClassCount=5
Class1=CChatServerApp
Class2=CChatServerDlg
Class3=CAboutDlg

ResourceCount=3
Resource1=IDD_ABOUTBOX
Resource2=IDR_MAINFRAME
Class4=CServerAsyncSocket
Class5=CAgentAsyncSocket
Resource3=IDD_CHATSERVER_DIALOG

[CLS:CChatServerApp]
Type=0
HeaderFile=ChatServer.h
ImplementationFile=ChatServer.cpp
Filter=N

[CLS:CChatServerDlg]
Type=0
HeaderFile=ChatServerDlg.h
ImplementationFile=ChatServerDlg.cpp
Filter=D
LastObject=IDC_SEND_BUTTON
BaseClass=CDialog
VirtualFilter=dWC

[CLS:CAboutDlg]
Type=0
HeaderFile=ChatServerDlg.h
ImplementationFile=ChatServerDlg.cpp
Filter=D

[DLG:IDD_ABOUTBOX]
Type=1
Class=CAboutDlg
ControlCount=4
Control1=IDC_STATIC,static,1342177283
Control2=IDC_STATIC,static,1342308480
Control3=IDC_STATIC,static,1342308352
Control4=IDOK,button,1342373889

[DLG:IDD_CHATSERVER_DIALOG]
Type=1
Class=CChatServerDlg
ControlCount=8
Control1=IDC_SEND_BUTTON,button,1342242817
Control2=IDCANCEL,button,1342242816
Control3=IDC_MESSAGE_LIST,listbox,1352728833
Control4=IDC_MESSAGE_EDIT,edit,1350631552
Control5=IDC_RECEIVEDBYTES,edit,1350631552
Control6=IDC_STATIC,static,1342308352
Control7=IDC_RECEIVEDCOUNT,edit,1350631552
Control8=IDC_STATIC,static,1342308352

[CLS:CServerAsyncSocket]
Type=0
HeaderFile=ServerAsyncSocket.h
ImplementationFile=ServerAsyncSocket.cpp
BaseClass=CAsyncSocket
Filter=N
LastObject=CServerAsyncSocket
VirtualFilter=q

[CLS:CAgentAsyncSocket]
Type=0
HeaderFile=AgentAsyncSocket.h
ImplementationFile=AgentAsyncSocket.cpp
BaseClass=CAsyncSocket
Filter=N
VirtualFilter=q
LastObject=CAgentAsyncSocket

