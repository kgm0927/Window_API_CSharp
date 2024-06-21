using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageProcess
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        public Image image { get; set; }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.ClientSize = image.Size;
          
        }

        private void Form2_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(image,0,0,image.Width, image.Height);
        }
    }
}
