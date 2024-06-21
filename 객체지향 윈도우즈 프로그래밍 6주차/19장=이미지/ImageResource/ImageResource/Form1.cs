using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ImageResource
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Paint(object sender, PaintEventArgs e)
		{
			Bitmap B = Properties.Resources.MyCar;
			e.Graphics.DrawImage(B, 10, 10);

			Bitmap B2 = Properties.Resources.Image1;
			e.Graphics.DrawImage(B2, 20, 20);

			Icon I = Properties.Resources.Icon1;
			e.Graphics.DrawIcon(I, 100, 20);
		}
	}
}
