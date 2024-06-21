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
    public partial class Form2 : Form
    {
        Bitmap bit;

        public Form2()
        {
            InitializeComponent();
        }

        public Bitmap show_bitmap
        {
            get { return bit; }
            set { bit = value; }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.Size = new Size(bit.Width + 10, bit.Height + 30);
        }

        private void Form2_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(bit, 0, 0, bit.Width, bit.Height);
        }
    }
}
