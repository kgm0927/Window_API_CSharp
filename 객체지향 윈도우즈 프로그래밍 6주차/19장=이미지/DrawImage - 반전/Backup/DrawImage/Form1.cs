using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
            Bitmap B = new Bitmap(I);
           //* 
            for(int y=0; y<B.Height; y++)
                for (int x = 0; x<B.Width; x++)
                {
                    Color color = B.GetPixel(x,y);
                    byte r = (byte)~color.R;
                    byte g = (byte)~color.G;
                    byte b = (byte)~color.B;
                    B.SetPixel(x,y, Color.FromArgb(r, g, b));
                }
            // */
            e.Graphics.DrawImage(B, 0, 0);
			
		}
	}
}
