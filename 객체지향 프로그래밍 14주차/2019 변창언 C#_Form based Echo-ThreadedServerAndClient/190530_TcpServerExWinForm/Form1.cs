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
        //private delegate void SafeCallDelegate(string text);
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

                t1 = new Thread(new ParameterizedThreadStart(waitForClient));
                t1.Start(port);

                button1.Text = "닫기";
            }
            else if (button1.Text.Equals("닫기"))
            {
                button1.Text = "열기";
                listener.Stop();
                t1.Abort();
            }

        }

        private void waitForClient(Object o)
        {
            int port = (int)o;
            listener = new TcpListener(IPAddress.Any, port);
            client = new TcpClient();

            listener.Start();
            Thread t;
            listBox1.Items.Add("[TCP 서버] 서버가 시작되었습니다.");
            try
            {
                while (true)
                {
                    client = listener.AcceptTcpClient();

                    if (client.Connected)
                    {
                        t = new Thread(new ParameterizedThreadStart(threadedWorks)); // 스레드 생성
                        t.IsBackground = true;
                        t.Start(client);
                    }
                    else
                    {
                        listBox1.Items.Add("이전에 소켓이 연결되지 않았습니다. ");
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                listBox1.Items.Add("Except Thrown: " + ex.ToString());
                listBox1.Items.Add("[TCP 서버] 서버가 닫혔습니다.");
            }
            finally
            {
                client.Close();
            }
        }

        private void threadedWorks(Object o)
        {
            TcpClient client = (TcpClient)o;
            Socket s = client.Client;
            string address = s.RemoteEndPoint.ToString();
            string[] array = address.Split(new char[] { ':' });
            byte[] recevbyte = new byte[513];

            NetworkStream ns = client.GetStream();
            Encoding ASCII = Encoding.ASCII;

            listBox1.Items.Add("[TCP 서버] 클라이언트 접속 : IP 주소 = " + array[0] + " 포트번호 = " + array[1]);
            while (true)
            {
                try
                {
                    int bytes = ns.Read(recevbyte, 0, recevbyte.Length);
                    String strbyte = ASCII.GetString(recevbyte, 0, bytes);
                    if (bytes == 0)
                    {
                        ns.Write(recevbyte, 0, bytes);
                        listBox1.Items.Add("[TCP 서버] 클라이언트 연결 해제 : IP 주소 = " + array[0] + " 포트번호 = " + array[1]);
                        break;
                    }
                    else
                    {
                        listBox1.Items.Add("[TCP/" + array[0] + ":" + array[1] + "] " + strbyte);
                        ns.Write(recevbyte, 0, bytes);
                    }
                }
                catch (Exception ex)
                {
                    listBox1.Items.Add("[TCP 서버] 클라이언트 연결 해제 : IP 주소 = " + array[0] + " 포트번호 = " + array[1]);
                    ex.ToString();
                    break;
                }
            }
            client.Close();
        }
        ////  아래는 불필요...
        //public void listBox1AccessInThread(string text)
        //{
        //    if (listBox1.InvokeRequired)
        //    {
        //        SafeCallDelegate d = new SafeCallDelegate(listBox1AccessInThread);
        //        this.Invoke(d, new object[] { text });
        //    }
        //    else
        //    {
        //        listBox1.Items.Add(text);
        //    }
        //}

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            listener.Stop();
        }
    }
}
