using System;
using System.IO;
using System.Text;
using System.Data;
using System.Net.Sockets;
using System.Threading;

namespace Server
{
  public class TCPSocketListener
  {
    // 클라이언트의 별칭을 나타내는 변수
    private string _nick = "";

    // 서버의 인스턴스
    private Server _server = null;

    // 클라이언트를 연결하고 있는 소켓
    private Socket _clientSocket = null;

    // 클라이언트응답을 계속 받아줄 쓰레드 
    private Thread _listenerThread = null;

    // 클라이언트와의 통신을 중지할것인가를 알려주는 플래그값
    private bool _isStopClient = false;

    // 현재 통신하고 있는 클라이언트를 지우기 위한 플래그값
    private bool _isMarkedForDeletion = false;

    // network 소켓으로 부터 가져올 Network stream 
    private NetworkStream _netStream = null;

    // 클라이언트와의 통신에서 읽기를 쉽게할 읽기 스트림
    private StreamReader _stmReader = null;

    // 클라이언트와의 통신에서 쓰기를 쉽게할 쓰기 스트림 
    private StreamWriter _stmWriter = null;


    // 연결된 소켓과 서버를 인자로 한 생성자. 
    public TCPSocketListener(Socket clientSocket, Server server)
    {
      // 넘겨온 소켓 값을 내부의 전역 으로 받음 
      _clientSocket = clientSocket;

      // 소켓으로 부터 네트워크 스트림을 생성 
      _netStream = new NetworkStream(_clientSocket);

      // 네트워크 스트림으로 부터 읽기 스트림을 생성 
      _stmReader = new StreamReader(_netStream);

      // 네트워크 스트림으로 부터 쓰기 스트림을 생성 
      _stmWriter = new StreamWriter(_netStream);

      // 서버에 필요한 메소드를 접근하기 위해 서버 객체를 받음 
      _server = server;
    }

    // 소멸자 
    ~TCPSocketListener()
    {
      // 소켓통신을 종료하는데 필요한 모든 처리를하는 메소드
      StopSocketListener();
    }

    // 별칭 속성값
    public string NickName
    {
      get { return _nick; }
    }

    // 클라이언트와 통신한 쓰레드를 시작할 메소드
    public void StartSocketListener()
    {
      // 소켓값이 정상적인지 간단한 확인
      if (_clientSocket != null)
      {
        // 쓰레드를 생성합니다. 
        _listenerThread = new Thread(new ThreadStart(SocketListenerThreadStart));
        // 쓰레드를 시작합니다. 
        _listenerThread.Start();
      }
    }


    // 실제로 쓰레드 처리될 메소드 
    private void SocketListenerThreadStart()
    {
      try
      {
        // 소켓을 통해 만든 읽기 리더를 통해 클라이언트가 보낸 문자를 받는다.
        string initString = _stmReader.ReadLine();

        // 처음 보내지는 메세지 문자열중 []안의 별칭을 얻어 냅니다.
        _nick = initString.Substring(initString.IndexOf('['), initString.IndexOf(']') - initString.IndexOf('[') + 1);

        // 받은 메세지를 서버의 창에 로그로 남깁니다.
        _server.LogWrite(initString);

        // 접속한 모든 클라이언트에게 메세지를 보냅니다.
        _server.BroadcastMsg(initString);

        // 클라이언트가 연결되어져 있다면 연결을 끝기 까지 
        // 메세지를 받아서 서버의 로그에 남기는 처리를 반복합니다.
        while (!_isStopClient)
        {
          // 클라이언트로 온 메세지를 받습니다.
          string rcvString = _stmReader.ReadLine();

          // 받은 메세지를 서버의 창에 로그로 남깁니다.
          _server.LogWrite(rcvString);

          // 접속한 모든 클라이언트에게 메세지를 보냅니다.
          _server.BroadcastMsg(rcvString);

          // 잠시 쉽니다.
          Thread.Sleep(300);
        }
      }
      catch (Exception se)
      {
      }
      finally
      {
        // while 문을 빠져 나왔다면 종료된것으로 중지 플래그값으로 변경합니다.
        _isStopClient = true;
        _isMarkedForDeletion = true;
      }
    }


    // 소켓 통신 중지에 필요한 처리를 합니다.
    public void StopSocketListener()
    {
      // 객체가 null 인지 확인 합니다.
      if (_clientSocket != null)
      {
        // 더이상 클라이언트의 값을 받지 않도록 설정  
        _isStopClient = true;
        // 서버에서 삭제 처리할수 있도록 마킹합니다.
        _isMarkedForDeletion = true;
        // 읽기 스트림을 닫습니다.
        _stmReader.Close();
        // 쓰기 스트림을 닫습니다. 
        _stmWriter.Close();
        // 네트워크 스트림을 닫습니다. 
        _netStream.Close();
        // 소켓을 닫습니다. 
        _clientSocket.Close();

        // 읽기 스트림을 해지합니다.  
        _stmReader = null;
        // 쓰기 스트림을 해지합니다. 
        _stmWriter = null;
        // 네트워크 스트림을 해지합니다. 
        _netStream = null;
        // 클라이언트 소켓을 해지합니다.
        _clientSocket = null;

        // 중지하기 위한 시간을 넉넉히 1초정도 줍니다.
        _listenerThread.Join(1000);

        // 살아 있다면 중지 시킵니다. 
        if (_listenerThread.IsAlive)
        {
          // 스레드를 중지하도록 합니다. 
          _listenerThread.Abort();
        }
        _listenerThread = null;
      }
    }

    // 삭제 마킹에 대한 속성 값입니다. 
    public bool IsMarkedForDeletion()
    {
      return _isMarkedForDeletion;
    }

    // 쓰기스트림을 통해 클라이언트에게 메세지를 보냅니다. 
    public void SendMessage(string msg)
    {
      if (_stmWriter != null)
      {
        // 쓰기 스트림에 메세지를 씁니다.
        _stmWriter.WriteLine(msg);
        // 메세지를 방출합니다. 
        _stmWriter.Flush();
      }
    }


  }// end TCPSocketListener 
}
