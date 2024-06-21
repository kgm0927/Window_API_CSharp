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

namespace Third_week_curve
{
    public partial class Form1 : Form
    {
        private LinkedList<ArrayList> lines;
        private ArrayList line;
        private Point passed_p;


        public Form1()
        {
            lines = new LinkedList<ArrayList>();
            InitializeComponent();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button==MouseButtons.Left)
            {
               
                Point p = new Point(e.X, e.Y);
                passed_p = p;
                line = new ArrayList();
                line.Add(p);


                Invalidate();

            }



        }
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Capture && e.Button == MouseButtons.Left)
            {
                Graphics g=CreateGraphics();
                Point p=new Point(e.X, e.Y);
                
                
                g.DrawLine(Pens.Black, passed_p, p);
                passed_p = p;
                line.Add(p);

            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach (ArrayList line in lines)
            {
                for (int i = 1; i < line.Count; i++)// 1에서부터 시작함.
                {
                    e.Graphics.DrawLine(Pens.Black, (Point)line[i-1], (Point)line[i]);
                }
            }

        }

     
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            lines.AddLast(line);
           
        }
    }

   

}
