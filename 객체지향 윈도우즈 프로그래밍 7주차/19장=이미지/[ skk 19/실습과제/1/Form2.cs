using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MdiProject
{
    public partial class Form2 : Form
    {
        Image image;
        public Form2()
        {
            InitializeComponent();
        }

        public Image Image
        {
            get { return image; }
            set { image = value; }
        }

        private void Form2_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(image,0,0, image.Width, image.Height);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.Size = new Size(image.Width+10, image.Height+30);
        }
    }
}
