using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawImage
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Image I = Image.FromFile("아기.jpg");
            e.Graphics.DrawImage(I, new Point(0, 0));


            Rectangle r = new Rectangle(70, 20, 300, 320);
            Point[] pts = { new Point(0,0),new Point(300,0),new Point(100,200)};
        }
    }
}
