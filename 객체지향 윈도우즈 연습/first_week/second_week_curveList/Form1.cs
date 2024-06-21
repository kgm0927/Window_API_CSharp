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

namespace second_week_curveList
{
    public partial class Form1 : Form
    {
        private int x, y;
        private ArrayList ar;

        public Form1()
        {
            ar = new ArrayList();
            InitializeComponent();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                x = e.X;
                y = e.Y;
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            memorized_line(e);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Pen p = new Pen(Color.Black);

            drawline_in_arrayList( ar, e, p);
        }

        private void drawline_in_arrayList( ArrayList ar, PaintEventArgs e,Pen p)
        {
            foreach (Rectangle R in ar)
            {
                e.Graphics.DrawLine(p, R.Left, R.Top, R.Right, R.Bottom);
            }
        }

        private void memorized_line(MouseEventArgs e)
        {
            if (Capture && e.Button == MouseButtons.Left)
            {
                Graphics G = CreateGraphics();
                G.DrawLine(Pens.Black, x, y, e.X, e.Y);
                ar.Add(Rectangle.FromLTRB(x, y, e.X, e.Y));
                x = e.X;
                y = e.Y;
                G.Dispose();
            }

        }

    }
}
