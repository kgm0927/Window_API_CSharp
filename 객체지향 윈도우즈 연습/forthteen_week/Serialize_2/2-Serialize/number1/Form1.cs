using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
namespace number1
{
    public partial class Form1 : Form
    {
        tcp_assemble tcpa;
        readonly object Forlock;

        public Form1()
        {
            InitializeComponent();
            tcpa = new tcp_assemble();
            tcpa.ar = new ArrayList();
            Forlock = new object();

        }

        private void socketSendReceive()
        {
            try
            {
                TcpListener listener = new TcpListener(IPAddress.Any, 9000);
                listener.Start();

                TcpClient serverClient = listener.AcceptTcpClient();
                if (serverClient.Connected)
                {
                    NetworkStream ns = serverClient.GetStream();
                    while (true)
                    {
                        try
                        {
                            CMyData c = new CMyData();
                            BinaryFormatter bf = new BinaryFormatter();
                            c = (CMyData)bf.Deserialize(ns);
                            lock (Forlock)
                            {
                                tcpa.ar.Add(c);
                                Invalidate();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("데이터 수신 중 오류 발생: " + ex.Message);
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("소켓 송수신 설정 중 오류 발생: " + ex.Message);
            }
        }

        private void click_and_connecting(ref tcp_assemble tcpa)
        {
            try
            {
                tcpa.tclient = new TcpClient("127.0.0.1", 8000);
                if (tcpa.tclient.Connected)
                {
                    label1.Text = "연결 성공!";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("서버 연결 중 오류 발생: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            click_and_connecting(ref tcpa);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                tcpa.th1 = new Thread(new ThreadStart(socketSendReceive));
                tcpa.th1.IsBackground = true;
                tcpa.th1.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("폼 로드 중 오류 발생 : " + ex.Message);
            }
        }




        private void painting_dot(ref MouseEventArgs e, ref tcp_assemble tcpa_p)
        {
            try
            {
                Random random = new Random();
                if (e.Button == MouseButtons.Left)
                {
                    CMyData c = new CMyData();
                    c.Size = random.Next(30, 100);
                    c.Point = new Point(e.X, e.Y);

                    lock (Forlock)
                    {
                        tcpa_p.ar.Add(c);

                        if (tcpa_p.tclient != null && tcpa_p.tclient.Connected)
                        {
                            try
                            {
                                NetworkStream ns = tcpa_p.tclient.GetStream();
                                BinaryFormatter bf = new BinaryFormatter();
                                bf.Serialize(ns, c);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("데이터 전송 중 오류 발생: " + ex.Message);
                            }
                        }
                        Invalidate();
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("마우스 클릭 처리 중 오류 발생: " + ex.Message);
            }



        }




        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            painting_dot(ref e, ref tcpa);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            lock (Forlock)
            {
                foreach (CMyData c in tcpa.ar)
                {
                    e.Graphics.FillEllipse(Brushes.Red, c.Point.X, c.Point.Y, c.Size, c.Size);
                    e.Graphics.DrawEllipse(Pens.Black, c.Point.X, c.Point.Y, c.Size, c.Size);
                }
            }
        }
    }
    public class tcp_assemble
    {
        public ArrayList ar;
        public TcpClient tclient;
        public Thread th1;
    }

    [Serializable]
    public class CMyData
    {
        private Point point;
        private int size;

        public Point Point
        {
            get { return point; }
            set { point = value; }
        }
        public int Size
        {
            get { return size; }
            set { size = value; }
        }
    }
}