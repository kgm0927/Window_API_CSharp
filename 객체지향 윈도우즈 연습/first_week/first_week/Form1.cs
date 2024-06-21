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
using System.Configuration;


namespace first_week
{
    public partial class Form1 : Form
    {
        ArrayList ar;
        public Form1()
        {
            InitializeComponent();
            ar = new ArrayList();

        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            Random random = new Random();
            if (e.Button == MouseButtons.Left)
            {
                CMyData c = new CMyData();
                c.Shape = (int)random.Next(2);
                c.Size = (int)random.Next(50, 200);
                c.Point = new Point(e.X, e.Y);

                c.bColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256), random.Next(256));
                c.pColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256), random.Next(256));

                ar.Add(c);  // CMydata

            }
            Invalidate();// paint
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach (CMyData c in ar)
            {
                SolidBrush brc = new SolidBrush(c.bColor);
                Pen p = new Pen(c.pColor);
                if (c.Shape == 1)
                {
                    Graphics g = e.Graphics;
                    g.DrawEllipse(p, new Rectangle(c.Point.X, c.Point.Y, c.Size, c.Size));
                    g.FillEllipse(brc, new Rectangle(c.Point.X, c.Point.Y, c.Size, c.Size));
                }
                else
                {
                    Graphics g = e.Graphics;
                    g.DrawRectangle(p, new Rectangle(c.Point.X, c.Point.Y, c.Size, c.Size));
                    g.FillRectangle(brc, new Rectangle(c.Point.X, c.Point.Y, c.Size, c.Size));
                }
            }
        }

        public class CMyData
        {
            private Point point;
            private Color brushCol;
            private Color penCol;
            private int size, shape;


            public Color pColor
            {
                get { return penCol; }
                set { penCol = value; }
            }

            public Color bColor
            {
                get { return brushCol; }
                set { brushCol = value; }
            }


            public Point Point
            {
                get;
                set;
            }

            public int Size
            {
                get { return size; }
                set { size = value; }
            }

            public int Shape
            {
                get { return shape; }
                set { shape = value; }
            }


        }
    }
}