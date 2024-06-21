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

namespace four_week_drawlines
{
    public partial class Form1 : Form
    {
        private LinkedList<CMyData> total_lines;
        private CMyData data;
        private Point passed_p;
        private Color CurrentPenColor;
        public Form1()
        {
            InitializeComponent();
            total_lines = new LinkedList<CMyData>();
            CurrentPenColor = Color.Red;
        }

        private void 선색깔ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            빨강ToolStripMenuItem.Checked = (CurrentPenColor == Color.Red);
            파랑ToolStripMenuItem.Checked=(CurrentPenColor == Color.Blue);
            초ToolStripMenuItem.Checked=(CurrentPenColor== Color.Green);

        }

        private void 빨강ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentPenColor = Color.Red;
        }

        private void 파랑ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentPenColor = Color.Blue;
        }

        private void 초ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentPenColor = Color.Green;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
           //사용하지 말아야 함.
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Capture && e.Button == MouseButtons.Left)
            {
                Graphics G = CreateGraphics();
                Pen p = new Pen(data.color, 1);

                G.DrawLine(p, passed_p, new Point(e.X, e.Y));
                passed_p = new Point(e.X, e.Y);

                data.AR.Add(new Point(e.X, e.Y));
                G.Dispose();

            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach (CMyData item in total_lines)
            {
                Pen p=new Pen(item.color, 1);
                for (int i = 0; i < item.AR.Count; i++)
                {
                    e.Graphics.DrawLine(p, (Point)item.AR[i - 1], (Point)item.AR[1]);
                }
                
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            total_lines.AddLast(data);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button==MouseButtons.Left)
            {
                Point p=new Point(e.X, e.Y);
                passed_p = p;

                data = new CMyData();
                data.color = CurrentPenColor;

                data.AR.Add(p);

            }
        }
    }

    class CMyData
    {
        private ArrayList ar;

        public CMyData()
        {
            ar = new ArrayList();

        }

        public Color color
        {
            get;
            set;
        }

        public int Width
        {
            get;
            set;
        }

        public ArrayList AR
        {
            get { return ar; }
        }
    }
}