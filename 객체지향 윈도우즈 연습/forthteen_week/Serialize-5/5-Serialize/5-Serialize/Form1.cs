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

namespace _5_Serialize
{
    public partial class Form1 : Form
    {
        tcp_assemble tcpa;

        public Form1()
        {
            InitializeComponent();
            tcpa = new tcp_assemble(new ArrayList(), new TcpClient(), new object());
        }

        private void painting_dot(ref Graphics g, CMyData c)
        {
            g.FillEllipse(Brushes.Red, c.Point.X, c.Point.Y, c.Size, c.Size);
            g.DrawEllipse(Pens.Black, c.Point.X, c.Point.Y, c.Size, c.Size);
        }

        private void socketSendReceive()
        {
            TcpListener listener = new TcpListener(IPAddress.Any, 9000);
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

                    lock (tcpa.Forlock)
                    {
                        tcpa.ar.Add(c);
                   //     Graphics g = CreateGraphics();
                   //     painting_dot(ref g,  c);
                        Invalidate();
                    }

                }
            }
        }

        private void clicking_and_draw_dot(ref MouseEventArgs e,ref tcp_assemble tcpa)
        {
            Random random = new Random();
            if (e.Button == MouseButtons.Left)
            {
                CMyData c = new CMyData();
                c.Size = (int)random.Next(30, 100);
                c.Point = new Point(e.X, e.Y);

                lock (tcpa.Forlock)
                {
                    tcpa.ar.Add(c);
                    Graphics g = CreateGraphics();
                    painting_dot(ref g,  c);
                    Invalidate();

                    if (tcpa.tclient.Connected)
                    {
                        NetworkStream ns = tcpa.tclient.GetStream();
                        BinaryFormatter bf = new BinaryFormatter();
                        bf.Serialize(ns, c);
                    }

                }
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            clicking_and_draw_dot(ref e, ref tcpa);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tcpa.tclient=new TcpClient("127.0.0.1",8000);
            if (tcpa.tclient.Connected)
            {
                label1.Text = "연결 성공";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tcpa.th1 = new Thread(new ThreadStart(socketSendReceive));
            tcpa.th1.IsBackground = true;
            tcpa.th1.Start();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {

            Graphics g=CreateGraphics();
            lock (tcpa.Forlock)
            {
                foreach (CMyData c in tcpa.ar) //ar안에 CMyData 추가.
                {
                    painting_dot(ref g,  c);
                }
            }
        }



     


        private void painting_dot_while_moving(ref MouseEventArgs e,ref tcp_assemble tcpa)
        {
            Random random = new Random();
            if (true)
            {
                CMyData c = new CMyData();
                c.Size = (int)random.Next(30, 100);
                c.Point = new Point(e.X, e.Y);

                lock (tcpa.Forlock)
                {
                    tcpa.ar.Add(c);
                    Graphics g = CreateGraphics();
                    painting_dot(ref g, c);
                    Invalidate();

                    if (tcpa.tclient.Connected)
                    {
                        NetworkStream ns = tcpa.tclient.GetStream();
                        BinaryFormatter bf = new BinaryFormatter();
                        bf.Serialize(ns, c);
                    }

                }

            }
        }


        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            painting_dot_while_moving(ref e, ref tcpa);
        }

     
    }
    public class tcp_assemble
    {
        public ArrayList ar;
        public TcpClient tclient;
        public Thread th1;
        public readonly object Forlock;

        public tcp_assemble(ArrayList ar, TcpClient tclient, object Forlock)
        {
            this.ar = ar;
            this.tclient = tclient;
            this.Forlock = Forlock;
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
