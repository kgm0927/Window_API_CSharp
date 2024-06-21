using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MDIForm
{
    public partial class Form3 : Form
    {
        int[] greenarray;
        Bitmap bit;
        int x;


        public Form3()
        {
            InitializeComponent();
        }

        public int[] show_array
        {
            get { return greenarray; }

            set { greenarray = value;
            for (x = 0; x < greenarray.Count(); x++)
                {
                    label1.Text += greenarray[x];
                }
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //this.Size = new Size(bit.Width + 10, bit.Height + 30);
        }

        private void Form2_Paint(object sender, PaintEventArgs e)
        {
            /*int x, y;

            for (x = 0; x < greenarray.Count(); x++)
            {
                for (y = 0; y < greenarray[x]; y++)
                {
                    bit.SetPixel(x, y, Color.Black);
                }
            }
            e.Graphics.DrawImage(bit, 0, 0, bit.Width, bit.Height);*/
        }
    }
}
