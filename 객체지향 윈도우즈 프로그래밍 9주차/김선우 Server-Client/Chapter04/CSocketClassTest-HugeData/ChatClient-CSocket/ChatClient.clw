; CLW file contains information for the MFC ClassWizard

[General Info]
Version=1
LastClass=CChatClientDlg
LastTemplate=CAsyncSocket
NewFileInclude1=#include "stdafx.h"
NewFileInclude2=#include "ChatClient.h"

ClassCount=3
Class1=CChatClientApp
Class2=CChatClientDlg
Class3=CAboutDlg

ResourceCount=3
Resource1=IDD_ABOUTBOX
Resource2=IDR_MAINFRAME
Resource3=IDD_CHATCLIENT_DIALOG

[CLS:CChatClientApp]
Type=0
HeaderFile=ChatClient.h
ImplementationFile=ChatClient.cpp
Filter=N
LastObject=CChatClientApp

[CLS:CChatClientDlg]
Type=0
HeaderFile=ChatClientDlg.h
ImplementationFile=ChatClientDlg.cpp
Filter=D
LastObject=ID_FILEVIEW
BaseClass=CDialog
VirtualFilter=dWC

[CLS:CAboutDlg]
Type=0
HeaderFile=ChatClientDlg.h
ImplementationFile=ChatClientDlg.cpp
Filter=D

[DLG:IDD_ABOUTBOX]
Type=1
Class=CAboutDlg
ControlCount=4
Control1=IDC_STATIC,static,1342177283
Control2=IDC_STATIC,static,1342308480
Control3=IDC_STATIC,static,1342308352
Control4=IDOK,button,1342373889

[DLG:IDD_CHATCLIENT_DIALOG]
Type=1
Class=CChatClientDlg
ControlCount=9
Control1=IDC_MESSAGE_LIST,listbox,1352728833
Control2=IDC_SEND_BUTTON,button,1342242817
Control3=IDCANCEL,button,1342242816
Control4=IDC_MESSAGE_EDIT,edit,1350631552
Control5=IDC_RECEIVEDBYTES,edit,1350631552
Control6=IDC_STATIC,static,1342308352
Control7=IDC_RECEIVEDCOUNT,edit,1350631552
Control8=IDC_STATIC,static,1342308352
Control9=ID_FILEVIEW,button,1342242816

