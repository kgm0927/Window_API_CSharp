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
                
                //SolidBrush brc = new SolidBrush(Color.Red); // Stock object
                //Pen p = new Pen(Color.Red);
                g.DrawEllipse(Pens.Red, e.X, e.Y, 6, 6);
                g.FillEllipse(Brushes.Blue, e.X, e.Y, 6, 6);
                
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach (CMyData c in ar)
            {
                //SolidBrush brc = new SolidBrush(Color.Red); 
                //Pen p = new Pen(Color.Red);
                e.Graphics.DrawEllipse(Pens.Red, c.Point.X, c.Point.Y, 6, 6);
                e.Graphics.FillEllipse(Brushes.Blue, c.Point.X, c.Point.Y, 6, 6);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }

    public class CMyData
    {
        public Point Point { get; set; } 
    }
}
