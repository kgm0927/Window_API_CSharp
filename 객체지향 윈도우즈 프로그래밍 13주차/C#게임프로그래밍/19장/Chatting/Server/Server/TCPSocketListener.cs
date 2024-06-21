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
    // Ŭ���̾�Ʈ�� ��Ī�� ��Ÿ���� ����
    private string _nick = "";

    // ������ �ν��Ͻ�
    private Server _server = null;

    // Ŭ���̾�Ʈ�� �����ϰ� �ִ� ����
    private Socket _clientSocket = null;

    // Ŭ���̾�Ʈ������ ��� �޾��� ������ 
    private Thread _listenerThread = null;

    // Ŭ���̾�Ʈ���� ����� �����Ұ��ΰ��� �˷��ִ� �÷��װ�
    private bool _isStopClient = false;

    // ���� ����ϰ� �ִ� Ŭ���̾�Ʈ�� ����� ���� �÷��װ�
    private bool _isMarkedForDeletion = false;

    // network �������� ���� ������ Network stream 
    private NetworkStream _netStream = null;

    // Ŭ���̾�Ʈ���� ��ſ��� �б⸦ ������ �б� ��Ʈ��
    private StreamReader _stmReader = null;

    // Ŭ���̾�Ʈ���� ��ſ��� ���⸦ ������ ���� ��Ʈ�� 
    private StreamWriter _stmWriter = null;


    // ����� ���ϰ� ������ ���ڷ� �� ������. 
    public TCPSocketListener(Socket clientSocket, Server server)
    {
      // �Ѱܿ� ���� ���� ������ ���� ���� ���� 
      _clientSocket = clientSocket;

      // �������� ���� ��Ʈ��ũ ��Ʈ���� ���� 
      _netStream = new NetworkStream(_clientSocket);

      // ��Ʈ��ũ ��Ʈ������ ���� �б� ��Ʈ���� ���� 
      _stmReader = new StreamReader(_netStream);

      // ��Ʈ��ũ ��Ʈ������ ���� ���� ��Ʈ���� ���� 
      _stmWriter = new StreamWriter(_netStream);

      // ������ �ʿ��� �޼ҵ带 �����ϱ� ���� ���� ��ü�� ���� 
      _server = server;
    }

    // �Ҹ��� 
    ~TCPSocketListener()
    {
      // ��������� �����ϴµ� �ʿ��� ��� ó�����ϴ� �޼ҵ�
      StopSocketListener();
    }

    // ��Ī �Ӽ���
    public string NickName
    {
      get { return _nick; }
    }

    // Ŭ���̾�Ʈ�� ����� �����带 ������ �޼ҵ�
    public void StartSocketListener()
    {
      // ���ϰ��� ���������� ������ Ȯ��
      if (_clientSocket != null)
      {
        // �����带 �����մϴ�. 
        _listenerThread = new Thread(new ThreadStart(SocketListenerThreadStart));
        // �����带 �����մϴ�. 
        _listenerThread.Start();
      }
    }


    // ������ ������ ó���� �޼ҵ� 
    private void SocketListenerThreadStart()
    {
      try
      {
        // ������ ���� ���� �б� ������ ���� Ŭ���̾�Ʈ�� ���� ���ڸ� �޴´�.
        string initString = _stmReader.ReadLine();

        // ó�� �������� �޼��� ���ڿ��� []���� ��Ī�� ��� ���ϴ�.
        _nick = initString.Substring(initString.IndexOf('['), initString.IndexOf(']') - initString.IndexOf('[') + 1);

        // ���� �޼����� ������ â�� �α׷� ����ϴ�.
        _server.LogWrite(initString);

        // ������ ��� Ŭ���̾�Ʈ���� �޼����� �����ϴ�.
        _server.BroadcastMsg(initString);

        // Ŭ���̾�Ʈ�� ����Ǿ��� �ִٸ� ������ ���� ���� 
        // �޼����� �޾Ƽ� ������ �α׿� ����� ó���� �ݺ��մϴ�.
        while (!_isStopClient)
        {
          // Ŭ���̾�Ʈ�� �� �޼����� �޽��ϴ�.
          string rcvString = _stmReader.ReadLine();

          // ���� �޼����� ������ â�� �α׷� ����ϴ�.
          _server.LogWrite(rcvString);

          // ������ ��� Ŭ���̾�Ʈ���� �޼����� �����ϴ�.
          _server.BroadcastMsg(rcvString);

          // ��� ���ϴ�.
          Thread.Sleep(300);
        }
      }
      catch (Exception se)
      {
      }
      finally
      {
        // while ���� ���� ���Դٸ� ����Ȱ����� ���� �÷��װ����� �����մϴ�.
        _isStopClient = true;
        _isMarkedForDeletion = true;
      }
    }


    // ���� ��� ������ �ʿ��� ó���� �մϴ�.
    public void StopSocketListener()
    {
      // ��ü�� null ���� Ȯ�� �մϴ�.
      if (_clientSocket != null)
      {
        // ���̻� Ŭ���̾�Ʈ�� ���� ���� �ʵ��� ����  
        _isStopClient = true;
        // �������� ���� ó���Ҽ� �ֵ��� ��ŷ�մϴ�.
        _isMarkedForDeletion = true;
        // �б� ��Ʈ���� �ݽ��ϴ�.
        _stmReader.Close();
        // ���� ��Ʈ���� �ݽ��ϴ�. 
        _stmWriter.Close();
        // ��Ʈ��ũ ��Ʈ���� �ݽ��ϴ�. 
        _netStream.Close();
        // ������ �ݽ��ϴ�. 
        _clientSocket.Close();

        // �б� ��Ʈ���� �����մϴ�.  
        _stmReader = null;
        // ���� ��Ʈ���� �����մϴ�. 
        _stmWriter = null;
        // ��Ʈ��ũ ��Ʈ���� �����մϴ�. 
        _netStream = null;
        // Ŭ���̾�Ʈ ������ �����մϴ�.
        _clientSocket = null;

        // �����ϱ� ���� �ð��� �˳��� 1������ �ݴϴ�.
        _listenerThread.Join(1000);

        // ��� �ִٸ� ���� ��ŵ�ϴ�. 
        if (_listenerThread.IsAlive)
        {
          // �����带 �����ϵ��� �մϴ�. 
          _listenerThread.Abort();
        }
        _listenerThread = null;
      }
    }

    // ���� ��ŷ�� ���� �Ӽ� ���Դϴ�. 
    public bool IsMarkedForDeletion()
    {
      return _isMarkedForDeletion;
    }

    // ���⽺Ʈ���� ���� Ŭ���̾�Ʈ���� �޼����� �����ϴ�. 
    public void SendMessage(string msg)
    {
      if (_stmWriter != null)
      {
        // ���� ��Ʈ���� �޼����� ���ϴ�.
        _stmWriter.WriteLine(msg);
        // �޼����� �����մϴ�. 
        _stmWriter.Flush();
      }
    }


  }// end TCPSocketListener 
}
