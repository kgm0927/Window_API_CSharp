using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace _190530_TcpServerExWinForm
{
    public partial class Form1 : Form
    {
        private delegate void SafeCallDelegate(string text);
        IPEndPoint serverEndPoint;
        TcpListener listener;
        TcpClient client;
        Thread t1;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text.Equals("열기"))
            {
                int port = int.Parse(textBox2.Text);
                serverEndPoint = new IPEndPoint(IPAddress.Parse(textBox1.Text), port);
                listener = new TcpListener(IPAddress.Any, port);
                TcpClient client = new TcpClient();

                listener.Start();

                while (true)
                {
                    client = listener.AcceptTcpClient();
                    // Server 가 여기서 Block 된다. 리스트 박스에 항목 추가 
                    // 메시지를 처리 하지 못한다...
                    if (client.Connected)
                    {
                        t1 = new Thread(new ParameterizedThreadStart(threadedWorks));
                        t1.Start(client);
                    }
                    else
                    {
                        Console.WriteLine("이전에 소켓이 연결되지 않았습니다. ");
                    }
                }

                button1.Text = "닫기";
            }
            else if (button1.Text.Equals("닫기"))
            {
                button1.Text = "열기";
                listener.Stop();
            }

        }

        private void threadedWorks(Object o)
        {
            TcpClient client = (TcpClient)o;
            Thread t;
            //listBox1.Items.Add("[TCP 서버] 서버가 시작되었습니다.");
            //SendMessage(listbox1,LB_Addstring.....);
            // 위의 SendMessage 처리가 Primary Thread에 의해 저리된 후
            // return 되어야 하는 데 client = listener.AcceptTcpClient();에서
            // block되어 처리하지 못하기 때문에 client와 통신하는 이 Thread도 여기서 
            // 연쇄적으로 Block 된다. 주석처리하면 아래의 client와 통신은 실행된다.
            try
            {
                if (client.Connected)
                {
                    Socket s = client.Client;
                    string address = s.RemoteEndPoint.ToString();
                    string[] array = address.Split(new char[] { ':' });
                    byte[] recevbyte = new byte[513];

                    NetworkStream ns = client.GetStream();
                    Encoding ASCII = Encoding.ASCII;

                   // listBox1AccessInThread("[TCP 서버] 클라이언트 접속 : IP 주소 = " + array[0] + " 포트번호 = " + array[1]);
                    while (true)
                    {
                        try
                        {
                            int bytes = ns.Read(recevbyte, 0, recevbyte.Length);
                            String strbyte = ASCII.GetString(recevbyte, 0, bytes);
                            if (bytes == 0)
                            {
                                ns.Write(recevbyte, 0, bytes);
                               // listBox1.Items.Add("[TCP 서버] 클라이언트 연결 해제 : IP 주소 = " + array[0] + " 포트번호 = " + array[1]);
                                break;
                            }
                            else
                            {
                               // listBox1.Items.Add("[TCP/" + array[0] + ":" + array[1] + "] " + strbyte);
                                ns.Write(recevbyte, 0, bytes);
                            }
                        }
                        catch (Exception ex)
                        {
                            //listBox1.Items.Add("[TCP 서버] 클라이언트 연결 해제 : IP 주소 = " + array[0] + " 포트번호 = " + array[1]);
                            ex.ToString();
                            break;
                        }
                    }
                    client.Close();
                }
                else
                {
                    //listBox1.Items.Add("이전에 소켓이 연결되지 않았습니다. ");
                }
            }
            catch (Exception ex)
            {
               // listBox1.Items.Add("Except Thrown: " + ex.ToString());
               // listBox1.Items.Add("[TCP 서버] 서버가 닫혔습니다.");
            }
            finally
            {
                client.Close();
            }
        }

        public void listBox1AccessInThread(string text)
        {
            if (listBox1.InvokeRequired)
            {
                SafeCallDelegate d = new SafeCallDelegate(listBox1AccessInThread);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                listBox1.Items.Add(text);
            }
        }
    }
}
