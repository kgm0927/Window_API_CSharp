using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Third_week
{
    public partial class Form1 : Form
    {
        private int count = 0;
        private Random rm;





        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            rm = new Random();
            SolidBrush sbh = new SolidBrush(Color.FromArgb(rm.Next(256), rm.Next(256), rm.Next(256)));

            Graphics G = CreateGraphics();
            G.DrawString("Down", Font, sbh, e.X, e.Y);
            G.Dispose();
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            count++;
            Text = count.ToString();
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            Graphics G = CreateGraphics();
            G.DrawString("Click", Font, Brushes.Black, e.X, e.Y);
            G.Dispose();
        }

        private void Form1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Graphics G = CreateGraphics();
            G.DrawString("Double click", Font, Brushes.Black, e.X, e.Y);
            G.Dispose();
        }
    }
}
