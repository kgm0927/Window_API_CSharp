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


namespace Drawline_and_network
{
    public partial class Form1 : Form
    {
     //   private LinkedList<CMyData<Point>> LL;


        private List<CMyData> LL;
        private int PenWidth;
        private Color CurrentPenColor;

        private CMyData passed_data;
        tcp_assemble tcpa;


        public Form1()
        {
        
            CurrentPenColor = Color.Red;
            PenWidth = 1;
            LL = new List<CMyData>();
            tcpa = new tcp_assemble(new TcpClient(),new object());
            InitializeComponent();
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
                   
                    Graphics g = CreateGraphics();
                    BinaryFormatter bf = new BinaryFormatter();
    
                    CMyData c = new CMyData();
                    CMyData c2 = new CMyData();

              

                    c = (CMyData)bf.Deserialize(ns);
                    c2 = (CMyData)bf.Deserialize(ns);
                    lock (tcpa.Forlock)
                    {

                        Painting(ref c,ref g,ref c2);
            
                 
                        LL.Add(c);
                        LL.Add(c2);
                    }


                }
            }
        }

        private void 대화상자ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Form2 dlg = new Form2();
            dlg.iDialogPenWidth = PenWidth;
            dlg.DialogPenColor = CurrentPenColor;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                PenWidth = dlg.iDialogPenWidth;
                CurrentPenColor = dlg.DialogPenColor;

            }
            dlg.Dispose();

        }


        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            lock (tcpa.Forlock) { 
                Painting(ref LL, ref g);
                }
        }
        private void Painting(ref List<CMyData> ll,ref Graphics g)
        {
            if (ll == null || ll.Count < 2)
                return;  // 리스트가 비어있거나 점이 2개 미만일 경우 아무것도 그리지 않음

            for (int i = 1; i < ll.Count; i++)
            {
                CMyData c1 = ll[i - 1];
                CMyData c2 = ll[i];

                Pen pen = new Pen(c1.PenColor, c1.PenWidth);
                
                    g.DrawLine(pen, c1.p, c2.p);
                
            }
        }

        private void make_dot_first_Set(ref MouseEventArgs e, ref tcp_assemble tcpa, ref List<CMyData> ll, ref CMyData before_data)
        {
          
            if (e.Button == MouseButtons.Left)
            {
                Point p = new Point(e.X, e.Y);


            

                lock (tcpa.Forlock)
                {
                    before_data = new CMyData();
                    before_data.PenColor = CurrentPenColor;
                    before_data.PenWidth = PenWidth;
                    before_data.p = p;

             

                    ll.Add(before_data);         
                } 
            }
        }


        private void Painting(ref CMyData current,ref Graphics g,ref CMyData before)
        {
            g.DrawLine(new Pen(current.PenColor,current.PenWidth),before.p,current.p);
        }


        private void make_dot_drawing(ref MouseEventArgs e, ref tcp_assemble tcpa, ref List<CMyData> ll,ref CMyData before_data)
        {
         

            if (e.Button == MouseButtons.Left)
            {
                Point p = new Point(e.X, e.Y);

           

                lock (tcpa.Forlock)
                {
                   
                    Graphics g = CreateGraphics();
                    CMyData c=new CMyData();
                    c.p=p;
                    c.PenColor=CurrentPenColor;
                    c.PenWidth=PenWidth;
                    Painting(ref c,ref g,ref before_data);


                    if (tcpa.tclient.Connected)
                    {
                        NetworkStream ns = tcpa.tclient.GetStream();
                        BinaryFormatter bf = new BinaryFormatter();
                        bf.Serialize(ns, before_data);
                        bf.Serialize(ns, c);


                    }

       
                    ll.Add(c);

                    before_data = c;
                }
            }
        }


        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            make_dot_first_Set(ref e, ref tcpa, ref LL, ref passed_data);
     
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {//
            make_dot_drawing(ref e, ref tcpa, ref LL, ref passed_data);
  
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            make_dot_drawing(ref e, ref tcpa, ref LL, ref passed_data);

        }

        private void 연결ToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
      
            tcpa.tclient = new TcpClient("127.0.0.1", 8000);
            if (tcpa.tclient.Connected)
            {
                label1.Text = "연결 성공!";
            }
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            tcpa.th1 = new Thread(new ThreadStart(socketSendReceive));
            tcpa.th1.IsBackground = true;
            tcpa.th1.Start();
     
        }
    }


    public class tcp_assemble
    {
        public TcpClient tclient;
        public Thread th1;
        public readonly object Forlock;


        public tcp_assemble(TcpClient tclient, object forlock)
        {
            this.tclient = tclient;
            this.Forlock = forlock;
        }
    }

    [Serializable]
    class CMyData
    {
        private int penWidth;
        private Color col;
        private Point point;

      
        public int PenWidth
        {
            get { return penWidth; }
            set { penWidth = value; }
        }

        public Color PenColor
        {
            get { return col; }
            set { col = value; }
        }

        public Point p
        {
            get { return point; }
            set { point = value; }
        }





    }
}
