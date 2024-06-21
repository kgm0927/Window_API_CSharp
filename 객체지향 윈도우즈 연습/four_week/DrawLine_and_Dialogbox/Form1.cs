using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawLine_and_Dialogbox
{
    public partial class Form1 : Form
    {
        private LinkedList<CMyData> total_lines;
        private CMyData data;
        private Color CurrentPenColor;
        private Point passed_p;
        public Form1()
        {
            total_lines= new LinkedList<CMyData>(); 
            InitializeComponent();
            CurrentPenColor = Color.Red;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point p = new Point(e.X, e.Y);
                 passed_p = p;

                Graphics g =CreateGraphics();
                data = new CMyData();
               
                data.Color = CurrentPenColor;
                g.DrawLine(new Pen(data.Color), passed_p, p);

                // 여기에 passed_p=p가 쓰이면 선이 서로 연결된다.

                data.AR.Add(p);
                g.Dispose();

            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            total_lines.AddLast(data);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Capture&&e.Button==MouseButtons.Left)
            {
                Graphics g = CreateGraphics();
                Point p=new Point(e.X, e.Y);

                g.DrawLine(new Pen(data.Color,1),passed_p,p);
                passed_p = p;
                data.AR.Add(p);
                g.Dispose();
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach (CMyData item in total_lines)
            {
                Pen pen = new Pen(item.Color,1);
                Graphics g=CreateGraphics();

                for (int i = 1; i < item.AR.Count; i++)
                {
                    g.DrawLine(pen, (Point)item.AR[i - 1], (Point)item.AR[i]);
                }
                g.Dispose();
            }
        }

        private void menuStrip1_MenuActivate(object sender, EventArgs e)
        {
            빨강ToolStripMenuItem.Checked = (CurrentPenColor == Color.Red);
            파랑ToolStripMenuItem.Checked = (CurrentPenColor == Color.Blue);
            초록ToolStripMenuItem.Checked=(CurrentPenColor == Color.Green);

        }

        private void 빨강ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentPenColor = Color.Red;
        }

        private void 파랑ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentPenColor = Color.Blue;
        }

        private void 초록ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentPenColor= Color.Green;   
        }

        private void 대화상자ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
           
            f2.PenColor = CurrentPenColor;

            if(f2.ShowDialog() == DialogResult.OK)
            {
                CurrentPenColor = f2.PenColor;
            }
            f2.Dispose();

        }
    }

    class CMyData
    {
        private ArrayList Ar;
        public CMyData()
        {
            Ar= new ArrayList();
        }

        public Color Color
        { set;
            get;
        }

        public int Width
        {
            get;
            set;
        }
        public ArrayList AR
        {
            get { return Ar; }
        }

    }
}
