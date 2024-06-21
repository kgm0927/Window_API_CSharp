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
			e.Graphics.DrawImage(I, 0, 0);
			//e.Graphics.DrawImage(I, new Point(0, 0));

			//e.Graphics.DrawImage(I, new Rectangle(0, 0, 200, 169));
			//e.Graphics.DrawImage(I, 0, 0, 200, 169);
			//e.Graphics.DrawImage(I, 0, 0, 100, 169);
			//e.Graphics.DrawImage(I, 0, 0, 200, 85);

			//e.Graphics.DrawImage(I, 10, 10, new Rectangle(70, 20, 300, 320), GraphicsUnit.Pixel);
			//e.Graphics.DrawImage(I, new Rectangle(10,10,200,200), 70, 20, 300, 300, GraphicsUnit.Pixel);

			Rectangle R = new Rectangle(70, 20, 300, 320);
			Point[] pts = { new Point(0, 0), new Point(300, 0), new Point(100, 200) };
			//e.Graphics.DrawImage(I, pts, R, GraphicsUnit.Pixel);
		}
	}
}
