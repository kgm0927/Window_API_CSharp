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
using System.IO;

namespace 랜덤_원_사각형_생성
{
    public partial class Form1 : Form 
    {
        public ArrayList ar; 

        public Form1()
        {
            InitializeComponent();
            ar = new ArrayList(); 
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            Graphics g = CreateGraphics();

            if (e.Button == MouseButtons.Left) 
            {
                CMyData c = new CMyData();
                c.Point = new Point(e.X, e.Y);
                ar.Add(c);
                
                //SolidBrush brc = new SolidBrush(Color.Red); 
                //Pen p = new Pen(Color.Red);
                g.DrawEllipse(Pens.Red, e.X, e.Y, 26, 26);
                g.FillEllipse(Brushes.Blue, e.X, e.Y, 26, 26);
                
                
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach (CMyData c in ar)
            {
                //SolidBrush brc = new SolidBrush(Color.Red); 
                //Pen p = new Pen(Color.Red);
                e.Graphics.DrawEllipse(Pens.Red, c.Point.X, c.Point.Y, 26, 26);
                e.Graphics.FillEllipse(Brushes.Blue, c.Point.X, c.Point.Y, 26, 26);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void 파일ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FileStream fs = new FileStream(@"c:\temp\Kim.bin", FileMode.Create,
               FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(ar.Count);
            foreach (CMyData c in ar)
            {
                c.Write(bw);
            }
            fs.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int count=0;
            FileStream fs = new FileStream(@"c:\temp\Kim.bin", FileMode.Open,
                FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            count=br.ReadInt32();
            ar = new ArrayList(); 
            for (int i = 0; i < count; i++)
            {
                //ar.Add(new CMyData(br.ReadInt32(), br.ReadInt32())); // OK
                CMyData t=CMyData.Read(br);  // 이건 책과 동일하게
                ar.Add(t);
                
            }
            fs.Close();
            Invalidate();
            
        }
    }

    public class CMyData
    {
        public Point Point { get; set; }
        public CMyData(){}
        public CMyData(int x, int y)
        {
            this.Point = new Point(x, y);
        }

        public void Write(BinaryWriter bw)
        {
            bw.Write(Point.X);
            bw.Write(Point.Y);
        }
        public static CMyData Read(BinaryReader br)
        {
            return new CMyData(br.ReadInt32(), br.ReadInt32());
        }
    }
}
