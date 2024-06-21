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
    // �⺻ �����Ǹ� ������ ���� ���� 
    private string _svrIP = "";
    // �⺻ ��Ʈ�� ������ ������ ��Ʈ ����  
   // private int _svrPort = 2005;
   // private int _svrPort = 9190;  //������
    private int _svrPort = 9000;  //�輱��
    // ���� ����� �� Ŭ���̾�Ʈ ���� 
    private TcpClient _tcpClient = null;
    // ��Ī 
    private string _nick = "user";

    // ��Ʈ��ũ ��Ʈ�� ��ü�� �����մϴ�. 
    private NetworkStream _netStream = null;
    // �б� ��Ʈ�� ��ü ���� 
    private StreamReader _stmReader = null;
    // ���� ��Ʈ�� ��ü ���� 
    private StreamWriter _stmWriter = null;

    // ����� �ߴ��ߴ����� ��Ÿ���� �÷��� 
    private bool _isStop = false;

    // ������ �� ���ø����̼ǿ� �α׸� ������� ������ delegate 
    public delegate void LogWriteDelegate(string msg);

    // ������ .
    public Client()
    {
      InitializeComponent();

      // ���� ��ǻ���� �����Ǹ� �⺻������ �����ݴϴ�.
      IPHostEntry localHostEntry = Dns.GetHostByName(Dns.GetHostName());
      // ���� ��ǻ���� �����Ǹ� �⺻ �����Ƿ� ���� 
      _svrIP = localHostEntry.AddressList[0].ToString();
      // ������ ���ڿ� �����ݴϴ�.
      txtIP.Text = _svrIP;
    }

    // �����ϱ� 
    private void btnConn_Click(object sender, EventArgs e)
    {
      // �������� ���� ������� �ʴٸ�  
      if (txtIP.Text != string.Empty)
        // ���� ������ �����Ǹ� ������ ������ �����Ƿ� ���� 
        _svrIP = txtIP.Text;
      else
        // ����ִٸ� �Է��ϵ��� �޼����� ������ 
        MessageBox.Show("�ּҸ� �Է� �Ͽ� �ֽʽÿ�");

      // ��Ī �Է� ���ڰ� ������� �ʴٸ� 
      if (txtNick.Text != string.Empty)
        // �Է��� ������ ��Ī ��� 
        _nick = txtNick.Text;
      else
        // �������� �ʾҴٸ� �����Ǹ� ��ġ���� ��� 
        _nick = _svrIP;

      // ������ �޼����� ���� �����带 �����մϴ�.
      Thread _clitThread = new Thread(new ThreadStart(ClientReceive));
      // ��� ������ �� �ֵ��� ��׶���� ���� �մϴ�. 
      _clitThread.IsBackground = true;
      // �����带 ���� �մϴ�. 
      _clitThread.Start();
    }


    // ���ø� ���̼��� �����忡 �����ϱ� ���� ��������Ʈ �̿�
    public void MessageWrite(string msg)
    {
      // ���� ������� ���ø����̼� ������� �浹���� �ʵ��� ��������Ʈ �̿� 
      LogWriteDelegate deleLogWirte = new LogWriteDelegate(AppendMsg);
      // ���ø����̼��� Invoke�� ����Ͽ� ��������Ʈ�� ���� 
      this.Invoke(deleLogWirte, new object[] { msg });
    }


    // ȭ���� ��ȭâ�� �޼����� ����մϴ�. 
    public void AppendMsg(string msg)
    {
      // �޼��� �߰��� �Բ� ����ǵ��� �Ѵ�. 
      txtOutput.AppendText(msg + "\r\n");
      // �޼���â�� ��Ŀ���� �� 
      txtOutput.Focus();
      // �޼��� �߰��� �κб��� ��ũ�ѽ��� ������ 
      txtOutput.ScrollToCaret();
      // �ٽ� �Է��Ҽ� �ֵ��� �޼��� �Է� ���ڿ� ��Ŀ��
      txtSend.Focus();
    }


    // ���� ��Ʈ������ �޼����� �����մϴ�. 
    public void SendMessage(string msg)
    {
      // ���� ��Ʈ���� ��ȿ������ üũ�մϴ�. 
      if (_stmWriter != null)
      {
        // �������´����� �޼��� ������ �����Ͽ� ���� ��Ʈ���� ���ϴ�. 
        _stmWriter.WriteLine("[" + _nick + "] " + msg);
        // ���� ��Ʈ���� �ִ� ������ �����մϴ�. 
        _stmWriter.Flush();
      }
    }


    // �����忡�� ����� ������ �Դϴ�.
    private void ClientReceive()
    {
      try
      {
        // Ŭ���̾�Ʈ ������ ����, �����մϴ�. 
        _tcpClient = new TcpClient(_svrIP, _svrPort);

        // Ŭ���̾�Ʈ�� ���� �Ǿ��ٸ� 
        if (_tcpClient.Connected)
        {
          // ��Ʈ��ũ ��Ʈ���� �����մϴ�. 
          _netStream = _tcpClient.GetStream();
          // �б� ��Ʈ���� �����մϴ�.
          _stmReader = new StreamReader(_netStream);
          // ���� ��Ʈ���� �����մϴ�.
          _stmWriter = new StreamWriter(_netStream);
          SendMessage("���� �����Ͽ����ϴ�.");

          while (!_isStop)
          {
            // �б� ��Ʈ������ ���� �޼����� �о�帲
            string rcvMsg = _stmReader.ReadLine();
            // �α�â�� �����κ��� ���� �޼����� �߰��մϴ�. 
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
        // ������ �������� �˸��ϴ�.
        MessageWrite("������ ������ϴ�.");
      }
      finally
      {
        // �б� ��Ʈ���� �ݽ��ϴ�. 
        _stmReader.Close();
        // ���� ��Ʈ���� �ݽ��ϴ�. 
        _stmWriter.Close();
        // ��Ʈ��ũ ��Ʈ���� �ݽ��ϴ�. 
        _netStream.Close();
        // Ŭ���̾�Ʈ ������ �ݽ��ϴ�. 
        _tcpClient.Close();
      }
    }


    // �Է»��ڿ��� ���͸� ġ�� �޼����� �����մϴ�. 
    private void txtSend_KeyPress(object sender, KeyPressEventArgs e)
    {
      // ���͸� �Է��Ͽ��ٸ� 
      if (e.KeyChar == '\r')
      {
        // �Է»��ڿ� �Է��� ��Ʈ���� �޽����.
        string msg = txtSend.Text;
        // ������ �޼����� �����մϴ�. 
        SendMessage(msg);
        // �Է»����� ������ ����ϴ�.
        txtSend.Clear();
      }
    }



  }// end Form1


}