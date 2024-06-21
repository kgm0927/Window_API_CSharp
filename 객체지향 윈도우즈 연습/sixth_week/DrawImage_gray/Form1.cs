using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawImage_gray
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {

            Image img = Image.FromFile("아기.jpg");
            Bitmap B = new Bitmap(img);
            for (int y = 0; y < B.Height; y++)
            {
                for (int x = 0; x < B.Width; x++)
                {
                    Color color = B.GetPixel(x, y);
                    byte gray = (byte)((color.R + color.B + color.G) / 3);

                    B.SetPixel(x, y, Color.FromArgb(gray, gray, gray));

                }

            } e.Graphics.DrawImage(B, 0, 0);

        }
    }
}
