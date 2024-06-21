using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace Client
{
  public partial class Client : Form
  {
    // 기본 아이피를 설정할 변수 설정 
    private string _svrIP = "";
    // 기본 포트를 설정할 변수와 포트 설정  
   // private int _svrPort = 2005;
   // private int _svrPort = 9190;  //윤성우
    private int _svrPort = 9000;  //김선우
    // 소켓 통신을 할 클라이언트 소켓 
    private TcpClient _tcpClient = null;
    // 별칭 
    private string _nick = "user";

    // 네트워크 스트림 객체를 선언합니다. 
    private NetworkStream _netStream = null;
    // 읽기 스트림 객체 선언 
    private StreamReader _stmReader = null;
    // 쓰기 스트림 객체 선언 
    private StreamWriter _stmWriter = null;

    // 통신을 중단했는지를 나타내는 플래그 
    private bool _isStop = false;

    // 생성한 폼 어플리케이션에 로그를 찍기위해 선언한 delegate 
    public delegate void LogWriteDelegate(string msg);

    // 생성자 .
    public Client()
    {
      InitializeComponent();

      // 현재 컴퓨터의 아이피를 기본적으로 보여줍니다.
      IPHostEntry localHostEntry = Dns.GetHostByName(Dns.GetHostName());
      // 현재 컴퓨터의 아이피를 기본 아이피로 설정 
      _svrIP = localHostEntry.AddressList[0].ToString();
      // 아이피 상자에 보여줍니다.
      txtIP.Text = _svrIP;
    }

    // 연결하기 
    private void btnConn_Click(object sender, EventArgs e)
    {
      // 아이피의 값이 비어있지 않다면  
      if (txtIP.Text != string.Empty)
        // 현재 설정된 아이피를 서버에 접속할 아이피로 설정 
        _svrIP = txtIP.Text;
      else
        // 비어있다면 입력하도록 메세지를 보여줌 
        MessageBox.Show("주소를 입력 하여 주십시요");

      // 별칭 입력 상자가 비어있지 않다면 
      if (txtNick.Text != string.Empty)
        // 입력한 값으로 별칭 사용 
        _nick = txtNick.Text;
      else
        // 설정하지 않았다면 아이피를 별치으로 사용 
        _nick = _svrIP;

      // 서버로 메세지를 받을 쓰레드를 실행합니다.
      Thread _clitThread = new Thread(new ThreadStart(ClientReceive));
      // 즉시 중지할 수 있도록 백그라운드로 실행 합니다. 
      _clitThread.IsBackground = true;
      // 쓰레드를 실행 합니다. 
      _clitThread.Start();
    }


    // 어플리 케이션의 쓰레드에 포함하기 위해 델리게이트 이용
    public void MessageWrite(string msg)
    {
      // 소켓 쓰레드와 어플리케이션 쓰레드와 충돌되지 않도록 데리게이트 이용 
      LogWriteDelegate deleLogWirte = new LogWriteDelegate(AppendMsg);
      // 어플리케이션의 Invoke를 사용하여 델리게이트를 실행 
      this.Invoke(deleLogWirte, new object[] { msg });
    }


    // 화면의 대화창에 메세지를 출력합니다. 
    public void AppendMsg(string msg)
    {
      // 메세지 추가와 함께 개행되도록 한다. 
      txtOutput.AppendText(msg + "\r\n");
      // 메세지창에 포커스를 줌 
      txtOutput.Focus();
      // 메세지 추가된 부분까지 스크롤시켜 보여줌 
      txtOutput.ScrollToCaret();
      // 다시 입력할수 있도록 메세지 입력 상자에 포커스
      txtSend.Focus();
    }


    // 쓰기 스트림으로 메세지를 전송합니다. 
    public void SendMessage(string msg)
    {
      // 쓰기 스트림이 유효한지를 체크합니다. 
      if (_stmWriter != null)
      {
        // 누가보냈는지와 메세지 내용을 조합하여 쓰기 스트림에 씁니다. 
        _stmWriter.WriteLine("[" + _nick + "] " + msg);
        // 쓰기 스트림에 있는 내용을 방출합니다. 
        _stmWriter.Flush();
      }
    }


    // 쓰레드에서 실행된 쓰레드 입니다.
    private void ClientReceive()
    {
      try
      {
        // 클라이언트 소켓을 생성, 연결합니다. 
        _tcpClient = new TcpClient(_svrIP, _svrPort);

        // 클라이언트가 연결 되었다면 
        if (_tcpClient.Connected)
        {
          // 네트워크 스트림을 생성합니다. 
          _netStream = _tcpClient.GetStream();
          // 읽기 스트림을 생성합니다.
          _stmReader = new StreamReader(_netStream);
          // 쓰기 스트림을 생성합니다.
          _stmWriter = new StreamWriter(_netStream);
          SendMessage("님이 접속하였습니다.");

          while (!_isStop)
          {
            // 읽기 스트림으로 부터 메세지를 읽어드림
            string rcvMsg = _stmReader.ReadLine();
            // 로그창에 서버로부터 받은 메세지를 추가합니다. 
            MessageWrite(rcvMsg);
          }
        }
        else
        {
          return;
        }
      }
      catch (Exception ex)
      {
        // 접속이 끊겼음을 알립니다.
        MessageWrite("접속이 끊겼습니다.");
      }
      finally
      {
        // 읽기 스트림을 닫습니다. 
        _stmReader.Close();
        // 쓰기 스트림을 닫습니다. 
        _stmWriter.Close();
        // 네트워크 스트림을 닫습니다. 
        _netStream.Close();
        // 클라이언트 소켓을 닫습니다. 
        _tcpClient.Close();
      }
    }


    // 입력상자에서 엔터를 치면 메세지를 전송합니다. 
    private void txtSend_KeyPress(object sender, KeyPressEventArgs e)
    {
      // 엔터를 입력하였다면 
      if (e.KeyChar == '\r')
      {
        // 입력상자에 입력한 스트링을 받스빈다.
        string msg = txtSend.Text;
        // 서버로 메세지를 전송합니다. 
        SendMessage(msg);
        // 입력상자의 내용을 지웁니다.
        txtSend.Clear();
      }
    }



  }// end Form1


}