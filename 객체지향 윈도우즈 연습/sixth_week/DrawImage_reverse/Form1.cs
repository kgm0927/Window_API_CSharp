using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawImage_reverse
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
                    Color color=B.GetPixel(x,y);

                    byte r = (byte)~color.R;
                    byte g = (byte)~color.G;
                    byte b = (byte)~color.B;


                    B.SetPixel(x, y,Color.FromArgb(r, g, b));

            
                }

            }
            e.Graphics.DrawImage(B, 100, 100);


        }
    }
}
