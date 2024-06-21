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
namespace Drawline_use_crollbar
{
    public partial class Form1 : Form
    {
        private Color CurrenPentColor;
        private LinkedList<CMyData<Point>> LL;
        private CMyData<Point> cmd;
        private int pensize;
        private Point passed_p;
        private int combo_index;


        public Form1()
        {
            LL = new LinkedList<CMyData<Point>>();
            combo_index = 0;
            pensize = 1;
            InitializeComponent();
            CurrenPentColor = Color.Red;
        }

        private void menuStrip1_MenuActivate(object sender, EventArgs e)
        {
            빨강ToolStripMenuItem.Checked = (CurrenPentColor == Color.Red);
            파랑ToolStripMenuItem.Checked = (CurrenPentColor == Color.Blue);
            초록ToolStripMenuItem.Checked = (CurrenPentColor == Color.Green);
        }

        private void 빨강ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrenPentColor = Color.Red;

        }

        private void 초록ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrenPentColor = Color.Green;

        }

        private void 파랑ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrenPentColor = Color.Blue;
        }
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Graphics g = CreateGraphics();
                cmd = new CMyData<Point>();
                cmd.PenCol = CurrenPentColor;
                cmd.Width = pensize;
                Point p = new Point(e.X, e.Y);
                Pen pen=new Pen(CurrenPentColor,pensize);

                cmd.AR.Add(p);
                passed_p = p;
                g.DrawLine(pen, passed_p, p);
            }
        }


        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Capture && e.Button == MouseButtons.Left)
            {
                Graphics g = CreateGraphics();
                Point p = new Point(e.X, e.Y);
                Pen pen=new Pen(cmd.PenCol,cmd.Width);
                cmd.AR.Add(p);
                g.DrawLine(pen, passed_p, p);
                passed_p = p;
                g.Dispose();
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            LL.AddLast(cmd);
        }

     


        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            foreach (CMyData<Point> line in LL)
            {
                

                Pen pen = new Pen(line.PenCol,line.Width);

                for(int i=1;i<line.AR.Count;i++){
                    g.DrawLine(pen, line.output_ar(i-1), line.output_ar(i));
                }

            }
        }

        private void 대화상자ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 dlg = new Form2();
            dlg.Color = CurrenPentColor;
            dlg.Width = pensize;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                CurrenPentColor = dlg.Color;
                pensize = dlg.Width;
            }
            dlg.Dispose();
        }
    }


    class CMyData<T>
    {
        private int width;
        private Color pencol;
        private ArrayList ar;

        public CMyData(){
        ar=new ArrayList();
    }


        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        public Color PenCol
        {
            get { return pencol; }
            set { pencol = value; }
        }

        public ArrayList AR
        {
            get { return ar; }
        }

        public void insert_ar(T dot){
        
     AR.Add(dot);
    
    
    }

        public T output_ar(int cnt)
        {
            return (T)ar[cnt];
        }


    }

    }

