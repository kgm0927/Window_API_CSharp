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
    // �⺻ ��Ʈ 
    public static int _svrPort = 2005;

    // ���� TCP ������ 
    private TcpListener _server = null;

    // �����带 �����ϰ��� �Ҷ� true �Դϴ�. 
    private bool _isStop = false;

    // Ŭ���̾�Ʈ�� ���� �����带 ����������� �����մϴ�. 
    private bool _isStopRemove = false;

    // Ŭ���̾�Ʈ�� ������ �޾Ƶ��� ������ �Դϴ�. 
    private Thread _serverThread = null;

    // ������ ���� ����ڸ� �����ϴ� ������ 
    private Thread _removeThread = null;

    // ���ӵ� Ŭ���̾�Ʈ�� ��� ���� ����Ʈ 
    private ArrayList _arrClientLister = null;

    // ������ �� ���ø����̼ǿ� �α׸� ������� ������ delegate 
    public delegate void LogWriteDelegate(string msg);

    // ���� ������.
    public Server()
    {
      InitializeComponent();
      Init();
    }

    // ������ TCP �����ʸ� �ʱ�ȭ �մϴ�. 
    private void Init()
    {
      try
      {
        // ������ �����ϴ� ��ǻ���� �����Ǹ� ã�� ������ �����մϴ�. 
        IPHostEntry localHostEntry = Dns.GetHostByName(Dns.GetHostName());
        // TcpListener �� ���� ��ü�� �����մϴ�. 
        _server = new TcpListener(localHostEntry.AddressList[0], _svrPort);
      }
      catch (Exception ex)
      {
        _server = null;
      }
    }

    // ���� ��ư�� ������ ������ �����մϴ�.
    private void btnStart_Click(object sender, EventArgs e)
    {
      // ���� ���¸� false �� �����մϴ�. 		
      _isStop = false;
      // ������ ���� Ŭ���̾�Ʈ ���� �۾��� ������ �ƴ��� �����մϴ�.
      _isStopRemove = false;
      // �α�â�� ������ �˸��ϴ�. 
      LogWrite("������ �����մϴ�.");
      // �����带 ������ �� �ֵ��� �޼ҵ带 ȣ���մϴ�. 
      this.StartServer();
    }

    // ���� ��ư�� ������ ������ ���񽺸� �����մϴ�. 
    private void btnStop_Click(object sender, EventArgs e)
    {
      // â�� ���� ������ �˸��ϴ�. 
      LogWrite("������ �����մϴ�.");
      // ������ ������ �޼ҵ带 ȣ���մϴ�. 
      this.StopServer();
    }


    // ���ø� ���̼��� �����忡 ���ԵǱ� ���� ��������Ʈ �̿�
    public void LogWrite(string msg)
    {
      // ���Ͽ� ���õ� �����尡 ���� �����Ƿ� application ���� �浹�� ���ϱ����� ��������Ʈ�� �̿��մϴ�.
      LogWriteDelegate deleLogWirte = new LogWriteDelegate(AppendLog);
      // ������ ��������Ʈ�� �̿��Ͽ� Invoke �� �����մϴ�. 
      this.Invoke(deleLogWirte, new object[] { msg });
    }

    // �α׸� ��� ��ũ�ѵ� ��ġ�� ��ġ�ϵ��� �մϴ�. 
    public void AppendLog(string msg)
    {
      try
      {
        // �޼����� �߰��ϰ� ������ �մϴ�. 
        txtLog.AppendText(msg + "\r\n");
        // �α׻����� ��Ŀ���� �����մϴ�.
        txtLog.Focus();
        // �߰��� ���� �þ ���α��� ���������� �մϴ�.
        txtLog.ScrollToCaret();
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());
      }
    }


    public void StartServer()
    {
      // ������ ����� ������ �Ǿ��ٸ� 
      if (_server != null)
      {
        // �������� ����� ���� ArrayList
        _arrClientLister = new ArrayList();

        // Ŭ���̾�Ʈ�� ��û�� �޴� �����带 �����մϴ�. 
        _server.Start();

        // Ŭ���̾�Ʈ�� ������ �޾Ƶ��ϼ� �ִ� �����带 ���� 
        _serverThread = new Thread(new ThreadStart(ServerThreadStart));
        // �����带 ��׶���� �����մϴ�.
        _serverThread.IsBackground = true;
        // �����带 ������ŵ�ϴ�. 
        _serverThread.Start();
        // �����Ҷ� �ణ�� ������ �ݴϴ�. 
        Thread.Sleep(300);

        //������ ���� Ŭ���̾�Ʈ ������ �����ϴ� ������ 
        _removeThread = new Thread(new ThreadStart(RemoveThreadStart));
        // �����带 ��׶���� �����մϴ�.
        _removeThread.IsBackground = true;
        // �����带 ������ŵ�ϴ�. 
        _removeThread.Start();
        // �����Ҷ� �ణ�� ������ �ݴϴ�. 
        Thread.Sleep(300);
      }
    }


    public void StopServer()
    {
      // ������ �������̶�� 
      if (_server != null)
      {
        // ������ �������� �˸��ϴ�. 
        _isStop = true;
        // �������� �����մϴ�. 
        _server.Stop();

        // �����尡 ���⶧���� 1������ ��ٸ��ϴ�.
        _serverThread.Join(1000);

        // �����尡 ��� �ִٸ� ���� ��ŵ�ϴ�.
        if (_serverThread.IsAlive)
        {
          // �����尡 ����ǵ��� �մϴ�. 
          _serverThread.Abort();
        }
        // �����带 ������ �����մϴ�. 
        _serverThread = null;

        // ���� �����带 ������ ǥ���ϱ����� _isStopRemove�� true�� 
        _isStopRemove = true;

        // ������ ������ ���� 1�� 
        _removeThread.Join(1000);

        // �����尡 ��� �ִٸ� ���� ��Ŵ 
        if (_removeThread.IsAlive)
        {
          // �����尡 ����ǵ��� �մϴ�. 
          _removeThread.Abort();
        }
        // �����带 ������ �����մϴ�. 
        _removeThread = null;

        // Stop All clients.
        StopAllSocketListers();
      }
    }


    private void StopAllSocketListers()
    {
      // Ŭ���̾�Ʈ�� �����ϰ� �ִ� ��� �����带 ������ŵ�ϴ�. 
      foreach (TCPSocketListener socketListener in _arrClientLister)
      {
        // ������ �����ʸ� ������Ű�� ���� �޼ҵ带 ���� 
        socketListener.StopSocketListener();
      }

      // ��� Ŭ���̾�Ʈ ��ϸ� ���� �մϴ�. 
      _arrClientLister.Clear();
      // ��� ���� ����Ʈ�� �����մϴ�. 
      _arrClientLister = null;
    }


    private void ServerThreadStart()
    {
      // Ŭ���̾�Ʈ ���ϰ�ü�� �����մϴ�. 
      Socket clientSocket = null;
      // ���� ����Ʈ ��ü�� �����մϴ�. 
      TCPSocketListener socketListener = null;

      while (!_isStop)
      {
        try
        {
          // ������ ���ӵ� Ŭ���̾�Ʈ ������ �޽��ϴ�.
          clientSocket = _server.AcceptSocket();
          // ����� ���ϰ����� Ŭ���̾�Ʈ�� ������ ���� �����ʸ� �����մϴ�.
          socketListener = new TCPSocketListener(clientSocket, this);
          // ������� ���� �̴ϴ� 
          lock (_arrClientLister)
          {
            // ���Ḯ��Ʈ�� ���ο� �����ʸ� �߰���ŵ�ϴ�.
            _arrClientLister.Add(socketListener);
          }

          // Ŭ���̾�Ʈ�� ����� �����带 ���� ��ŵ�ϴ�.
          socketListener.StartSocketListener();
          // ����� �α׸� ����ϴ�.
          LogWrite("[" + clientSocket.RemoteEndPoint.ToString() + "]" + "�� ����Ǿ����ϴ�.");

        }
        catch (SocketException se)
        {
          _isStop = true;
        }
      }
    }



    private void RemoveThreadStart()
    {
      // �����ϱ� ���� ������� �ƴ϶�� �����ϴ� �۾��� ��� �մϴ�.
      while (!_isStopRemove)
      {

        // ���� ��Ͽ��� ������ ����Ʈ�� ���� ����Ʈ ��ü ����
        ArrayList deleteList = new ArrayList();

        // ������ ����Ʈ�� ������ ������ ���� �ɾ� �ݴϴ�. 
        lock (_arrClientLister)
        {
          foreach (TCPSocketListener socketListener in _arrClientLister)
          {
            // ����Ǿ� ���� �ʾ� ���� ǥ�ð� �� �����ʸ� ã�� ���� ������ŵ�ϴ�.
            if (socketListener.IsMarkedForDeletion())
            {
              // ��ŷ �Ǿ��ٸ� ���� ����Ʈ�� �߰���ŵ�ϴ�.
              deleteList.Add(socketListener);
              // ���Ͽ��� �����ϰ� �ִ� �����ʸ� ���� ��ŵ�ϴ�.
              socketListener.StopSocketListener();
              // �����ϴµ��� ��� ���ϴ�.
              Thread.Sleep(300);
            }

          }
          // ��ŷ�� ����Ʈ�� �����ŵ�ϴ�.
          foreach (TCPSocketListener delListener in deleteList)
          {
            // ����� �޼����� �α׿� ����ϴ�.
            LogWrite(delListener.NickName + " ���� ���� ����Ǿ����ϴ�.");
            // ���� ��Ͽ��� ���� �մϴ� .
            _arrClientLister.Remove(delListener);
          }
        }
        Thread.Sleep(300);

        deleteList = null;
      }// end while
    }


    // ����Ǿ� �ִ� ��� Ŭ���̾�Ʈ���� �޼����� �����ϴ�. 
    public void BroadcastMsg(string msg)
    {
      // ������ ����Ʈ���� ���� ���� �������� �޼����� �������� �մϴ�.
      lock (_arrClientLister)
      {
        // ������ Ŭ���̾�Ʈ���� üũ�ϵ��� �մϴ�. 
        foreach (TCPSocketListener client in _arrClientLister)
        {
          try
          {
            // ������ Ŭ���ξ�Ʈ�� �޼ҵ� ���� ���� �޼����� �����ϴ�.
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