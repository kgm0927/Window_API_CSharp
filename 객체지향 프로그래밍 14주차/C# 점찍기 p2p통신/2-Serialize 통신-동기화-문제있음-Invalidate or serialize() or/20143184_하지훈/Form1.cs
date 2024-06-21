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

namespace _20143184_하지훈
{
    public partial class Form1 : Form
    {
        ArrayList ar;
        TcpClient tclient;
        Thread th1;
        readonly object ForLock;  //초기화 Form1()

        private void socketSendReceive()
        {
            TcpListener listener = new TcpListener(IPAddress.Any, 8000);
            listener.Start();
            TcpClient serverClient = listener.AcceptTcpClient();
            if (serverClient.Connected)
            {
                NetworkStream ns = serverClient.GetStream();
                Encoding unicode = Encoding.Unicode;
                while (true)
                {
                    CMyData c = new CMyData();
                    BinaryFormatter bf = new BinaryFormatter();
                    c = (CMyData)bf.Deserialize(ns);
                    lock (ForLock)
                    {
                        ar.Add(c);
                        Invalidate();
                        //Updatewindow();
                    }
                }
            }
        }

        public Form1()
        {
            InitializeComponent();
            ar = new ArrayList();
            ForLock = new object();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            Random Random = new Random();
            if (e.Button == MouseButtons.Left)
            {
                CMyData c = new CMyData();
                c.Size = (int)Random.Next(30, 100);
                c.Point = new Point(e.X, e.Y);
                lock (ForLock)
                {
                    ar.Add(c); //배열에 저장
                    
                               
                    if (tclient.Connected)
                    {
                        NetworkStream ns = tclient.GetStream();
                        BinaryFormatter bf = new BinaryFormatter();
                        bf.Serialize(ns, c);
                    }
                    Invalidate();
                }
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach (CMyData c in ar) //ar안에 CMyData 추가.
            {
                e.Graphics.FillEllipse(Brushes.Red, c.Point.X, c.Point.Y, c.Size, c.Size);
                e.Graphics.DrawEllipse(Pens.Black, c.Point.X, c.Point.Y, c.Size, c.Size);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tclient = new TcpClient("127.0.0.1", 9000);
            if (tclient.Connected)
            {
                label1.Text = "연결 성공!";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            th1 = new Thread(new ThreadStart(socketSendReceive));
            th1.IsBackground = true;
            th1.Start();
        }
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
