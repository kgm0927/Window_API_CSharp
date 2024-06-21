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

namespace Drawine_just_dlg
{
    public partial class Form1 : Form
    {
        private LinkedList<CMyData<Point>> total_lines;
        CMyData<Point> data;
        private int x, y;
        private Color CurrentPenColor;
        private int iCurrentPenWidth;

        public Form1()
        {
            total_lines = new LinkedList<CMyData<Point>>();
            CurrentPenColor = Color.Black;
            iCurrentPenWidth = 2;
            InitializeComponent();
        }

      

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                x = e.X;
                y = e.Y;
                data = new CMyData<Point>();
                data.Color = CurrentPenColor;
                data.Width = iCurrentPenWidth;
                //ar = new ArrayList();
                //ar.Add(new Point(x, y));
                data.AR.Add(new Point(x, y));
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            total_lines.AddLast(data);

        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Capture && e.Button == MouseButtons.Left)
            {
                Graphics G = CreateGraphics();
                //펜 설정하기(색깔과 굵기) 
                Pen p = new Pen(data.Color, data.Width);
                G.DrawLine(p, x, y, e.X, e.Y);
                x = e.X;
                y = e.Y;
                data.AR.Add(new Point(x, y));
                G.Dispose();
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach (CMyData<Point> line in total_lines)
            {
                Pen p = new Pen(line.Color, line.Width);
                for (int i = 1; i < line.AR.Count; i++)
                {
                    e.Graphics.DrawLine(p, (Point)line.AR[i - 1], (Point)line.AR[i]);
                }
            }
        }

        private void 대화상자ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 dlg = new Form2();
            dlg.DialogPenColor = CurrentPenColor;
            dlg.iDialogPenWidth = iCurrentPenWidth;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                CurrentPenColor = dlg.DialogPenColor;      //get
                iCurrentPenWidth = dlg.iDialogPenWidth;
            }
            dlg.Dispose();
        }
    }

    class CMyData<T>
    {
        private int width;
        private Color pencol;
        private ArrayList ar;

        public CMyData()
        {
            ar = new ArrayList();
        }


        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        public Color Color
        {
            get { return pencol; }
            set { pencol = value; }
        }

        public ArrayList AR
        {
            get { return ar; }
        }

        public void insert_ar(T dot)
        {

            AR.Add(dot);


        }

        public T output_ar(int cnt)
        {
            return (T)ar[cnt];
        }


    }

}
