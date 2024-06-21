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

namespace Draw_lines
{
    public partial class Form1 : Form
    {
        private LinkedList<CMyData> LL;
        private int combo_index;
        private Color CurrentPenColor;
        private int CurrentPenSize;
        private Point passed_p; private CMyData cmd;
        public Form1()
        {
            LL = new LinkedList<CMyData>();
            CurrentPenColor = Color.Red;
            combo_index = 0;
            InitializeComponent();
        }

        private void 초록ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentPenColor = Color.Red;
        }

        private void 파랑ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentPenColor = Color.Blue;
        }

        private void 빨강ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentPenColor = Color.Red;
        }

        private void 대화상자ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 dlg = new Form2(combo_index);
            dlg.Color = CurrentPenColor;
            dlg.SIZE = CurrentPenSize;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                CurrentPenColor = dlg.Color;
                CurrentPenSize = dlg.SIZE;
                combo_index = dlg.combo_index;

            }

            dlg.Dispose();

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach (CMyData line in LL)
            {
                Pen pen = new Pen(line.PenCol); 

                for (int i = 1; i < line.AR.Count; i++)
                {
                    e.Graphics.DrawLine(pen, (Point)line.AR[i - 1], (Point)line.AR[i]);
                }
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point p = new Point(e.X, e.Y);
                 cmd = new CMyData();
                cmd.PenCol = CurrentPenColor;
                cmd.Width = CurrentPenSize;
                passed_p = p;


                cmd.AR.Add(p);

                
                

            }





        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            LL.AddLast(cmd);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Capture && e.Button == MouseButtons.Left)
            {
                Graphics g = CreateGraphics();
                Pen p = new Pen(CurrentPenColor,CurrentPenSize);
                Point pit = new Point(e.X, e.Y);

                g.DrawLine(p, passed_p, pit);
                passed_p = pit;
                cmd.AR.Add(pit);

                g.Dispose();

            }
        }

        private void menuStrip1_MenuActivate(object sender, EventArgs e)
        {
            빨강ToolStripMenuItem.Checked = (CurrentPenColor == Color.Red);
            파랑ToolStripMenuItem.Checked = (CurrentPenColor == Color.Blue);
            초록ToolStripMenuItem.Checked = (CurrentPenColor == Color.Green);
        }
    }

    class CMyData
    {
        private ArrayList ar;
        private int width;
        private Color pencol;


        public Color PenCol
        {
            get { return pencol; }
            set { pencol = value; }
        }

        public int Width
        {
            get { return width; }
            set { width = value; }
        }


        public CMyData()
        {
            ar = new ArrayList();
        }

        public ArrayList AR
        {
            get { return ar; }
        }

    }


}
