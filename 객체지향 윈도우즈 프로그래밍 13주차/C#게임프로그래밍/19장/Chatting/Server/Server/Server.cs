using System;
using System.IO;
using System.Net;
using System.Data;
using System.Text;
using System.Drawing;
using System.Net.Sockets;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Server
{
  public partial class Server : Form
  {
    // 기본 포트 
    public static int _svrPort = 2005;

    // 서버 TCP 리스너 
    private TcpListener _server = null;

    // 쓰레드를 중지하고자 할때 true 입니다. 
    private bool _isStop = false;

    // 클라이언트를 삭제 쓰레드를 멈출것인지를 결정합니다. 
    private bool _isStopRemove = false;

    // 클라이언트의 접속을 받아들일 쓰레드 입니다. 
    private Thread _serverThread = null;

    // 접속이 끊긴 사용자를 제거하는 쓰레드 
    private Thread _removeThread = null;

    // 접속된 클라이언트를 담아 놓을 리스트 
    private ArrayList _arrClientLister = null;

    // 생성한 폼 어플리케이션에 로그를 찍기위해 선언한 delegate 
    public delegate void LogWriteDelegate(string msg);

    // 서버 생성자.
    public Server()
    {
      InitializeComponent();
      Init();
    }

    // 서버의 TCP 리스너를 초기화 합니다. 
    private void Init()
    {
      try
      {
        // 서버를 실행하는 컴퓨터의 아이피를 찾아 종점을 생성합니다. 
        IPHostEntry localHostEntry = Dns.GetHostByName(Dns.GetHostName());
        // TcpListener 로 서버 객체를 생성합니다. 
        _server = new TcpListener(localHostEntry.AddressList[0], _svrPort);
      }
      catch (Exception ex)
      {
        _server = null;
      }
    }

    // 시작 버튼을 누르면 서버를 시작합니다.
    private void btnStart_Click(object sender, EventArgs e)
    {
      // 멈춤 상태를 false 로 설정합니다. 		
      _isStop = false;
      // 접속이 끊긴 클라이언트 삭제 작업이 멈춤이 아님을 설정합니다.
      _isStopRemove = false;
      // 로그창에 시작을 알립니다. 
      LogWrite("서버를 시작합니다.");
      // 쓰레드를 시작할 수 있도록 메소드를 호출합니다. 
      this.StartServer();
    }

    // 중짐 버튼을 누르면 서버의 서비스를 중지합니다. 
    private void btnStop_Click(object sender, EventArgs e)
    {
      // 창에 서버 중지를 알립니다. 
      LogWrite("서버를 중지합니다.");
      // 서버를 중지할 메소드를 호출합니다. 
      this.StopServer();
    }


    // 어플리 케이션의 쓰레드에 포함되기 위해 델리게이트 이용
    public void LogWrite(string msg)
    {
      // 소켓에 관련된 쓰레드가 돌고 있으므로 application 과의 충돌을 피하기위해 델리게이트를 이용합니다.
      LogWriteDelegate deleLogWirte = new LogWriteDelegate(AppendLog);
      // 생성한 델리게이트를 이용하여 Invoke 를 실행합니다. 
      this.Invoke(deleLogWirte, new object[] { msg });
    }

    // 로그를 찍고 스크롤된 위치에 위치하도록 합니다. 
    public void AppendLog(string msg)
    {
      try
      {
        // 메세지를 추가하고 개행을 합니다. 
        txtLog.AppendText(msg + "\r\n");
        // 로그상제에 포커스를 설정합니다.
        txtLog.Focus();
        // 추가로 인해 늘어난 라인까지 보여지도록 합니다.
        txtLog.ScrollToCaret();
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());
      }
    }


    public void StartServer()
    {
      // 서버가 제대로 생성이 되었다면 
      if (_server != null)
      {
        // 리스너의 목록을 갖는 ArrayList
        _arrClientLister = new ArrayList();

        // 클라이언트의 요청을 받는 쓰레드를 시작합니다. 
        _server.Start();

        // 클라이언트의 접속을 받아들일수 있는 쓰레드를 생성 
        _serverThread = new Thread(new ThreadStart(ServerThreadStart));
        // 쓰레드를 백그라운드로 설정합니다.
        _serverThread.IsBackground = true;
        // 쓰레드를 가동시킵니다. 
        _serverThread.Start();
        // 가동할때 약간의 여유를 줍니다. 
        Thread.Sleep(300);

        //접속이 끊긴 클라이언트 소켓을 삭제하는 쓰레드 
        _removeThread = new Thread(new ThreadStart(RemoveThreadStart));
        // 쓰레드를 백그라운드로 설정합니다.
        _removeThread.IsBackground = true;
        // 쓰레드를 가동시킵니다. 
        _removeThread.Start();
        // 가동할때 약간의 여유를 줍니다. 
        Thread.Sleep(300);
      }
    }


    public void StopServer()
    {
      // 서버가 정상적이라면 
      if (_server != null)
      {
        // 서버가 정지임을 알립니다. 
        _isStop = true;
        // 리스닝을 중지합니다. 
        _server.Stop();

        // 쓰레드가 멈출때까지 1초정도 기다립니다.
        _serverThread.Join(1000);

        // 쓰레드가 살아 있다면 중지 시킵니다.
        if (_serverThread.IsAlive)
        {
          // 쓰레드가 종료되도록 합니다. 
          _serverThread.Abort();
        }
        // 쓰레드를 완전히 해제합니다. 
        _serverThread = null;

        // 제거 쓰레드를 중지를 표시하기위해 _isStopRemove을 true로 
        _isStopRemove = true;

        // 쓰레드 중지를 위한 1초 
        _removeThread.Join(1000);

        // 쓰레드가 살아 있다면 중지 시킴 
        if (_removeThread.IsAlive)
        {
          // 쓰레드가 종료되도록 합니다. 
          _removeThread.Abort();
        }
        // 쓰레드를 완전히 해제합니다. 
        _removeThread = null;

        // Stop All clients.
        StopAllSocketListers();
      }
    }


    private void StopAllSocketListers()
    {
      // 클라이언트와 접속하고 있는 모든 쓰레드를 중지시킵니다. 
      foreach (TCPSocketListener socketListener in _arrClientLister)
      {
        // 각각의 리스너를 중지시키기 위한 메소드를 실행 
        socketListener.StopSocketListener();
      }

      // 모든 클라이언트 목록를 제거 합니다. 
      _arrClientLister.Clear();
      // 목록 관리 리스트를 해제합니다. 
      _arrClientLister = null;
    }


    private void ServerThreadStart()
    {
      // 클라이언트 소켓객체를 선언합니다. 
      Socket clientSocket = null;
      // 소켓 리스트 객체를 선언합니다. 
      TCPSocketListener socketListener = null;

      while (!_isStop)
      {
        try
        {
          // 서버에 접속된 클라이언트 소켓을 받습니다.
          clientSocket = _server.AcceptSocket();
          // 연결된 소켓값으로 클라이언트에 대응할 소켓 리스너를 생성합니다.
          socketListener = new TCPSocketListener(clientSocket, this);
          // 목록으로 락을 겁니다 
          lock (_arrClientLister)
          {
            // 연결리스트에 새로운 리스너를 추가시킵니다.
            _arrClientLister.Add(socketListener);
          }

          // 클라이언트와 통신할 쓰레드를 실행 시킵니다.
          socketListener.StartSocketListener();
          // 연결된 로그를 남깁니다.
          LogWrite("[" + clientSocket.RemoteEndPoint.ToString() + "]" + "이 연결되었습니다.");

        }
        catch (SocketException se)
        {
          _isStop = true;
        }
      }
    }



    private void RemoveThreadStart()
    {
      // 종료하기 위해 멈춘것이 아니라면 삭제하는 작업을 계속 합니다.
      while (!_isStopRemove)
      {

        // 연결 목록에서 제거할 리스트를 담을 리스트 객체 선언
        ArrayList deleteList = new ArrayList();

        // 현재의 리스트가 변동이 없도록 락을 걸어 줍니다. 
        lock (_arrClientLister)
        {
          foreach (TCPSocketListener socketListener in _arrClientLister)
          {
            // 연결되어 있지 않아 삭제 표시가 된 리스너를 찾아 내고 중지시킵니다.
            if (socketListener.IsMarkedForDeletion())
            {
              // 마킹 되었다면 삭제 리스트에 추가시킵니다.
              deleteList.Add(socketListener);
              // 소켓에서 실행하고 있던 리스너를 종료 시킵니다.
              socketListener.StopSocketListener();
              // 종료하는동안 잠시 쉽니다.
              Thread.Sleep(300);
            }

          }
          // 마킹된 리스트를 종료시킵니다.
          foreach (TCPSocketListener delListener in deleteList)
          {
            // 종료된 메세지를 로그에 남깁니다.
            LogWrite(delListener.NickName + " 님이 접속 종료되었습니다.");
            // 연결 목록에서 삭제 합니다 .
            _arrClientLister.Remove(delListener);
          }
        }
        Thread.Sleep(300);

        deleteList = null;
      }// end while
    }


    // 연결되어 있는 모든 클라이언트에게 메세지를 보냅니다. 
    public void BroadcastMsg(string msg)
    {
      // 현재의 리스트값이 변동 없는 조건으로 메세지를 보내도록 합니다.
      lock (_arrClientLister)
      {
        // 각각의 클라이언트마다 체크하도록 합니다. 
        foreach (TCPSocketListener client in _arrClientLister)
        {
          try
          {
            // 각각의 클라인언트의 메소드 값을 통해 메세지를 보냅니다.
            client.SendMessage(msg);
          }
          catch (Exception ex)
          {
            LogWrite("[error]" + ex.ToString());
          }
        }// end foreach
      }// end lock
    }// end method 

  }// end class 
}// end namespace