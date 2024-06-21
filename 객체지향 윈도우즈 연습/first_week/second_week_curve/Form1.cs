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

namespace second_week_curve
{
    public partial class Form1 : Form
    {
     
        private Point passed_p;

        public Form1()
        {
            
            InitializeComponent();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left )
            {
               passed_p=new Point(e.X,e.Y);
             
            }
            

        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Capture && e.Button == MouseButtons.Left)
            {
                draw_line(ref e);
            }
        }


        private void draw_line(ref MouseEventArgs e)
        {
            if (Capture && e.Button == MouseButtons.Left)
            {
                Graphics g = CreateGraphics();
                Point p = new Point(e.X, e.Y);

                g.DrawLine(Pens.Black, passed_p, p);
                passed_p = p;
                g.Dispose();
            }
        }
        

    }
}
