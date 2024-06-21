using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace MetaTest
{
	public partial class Form1 : Form
	{
		Metafile M;

		public Form1()
		{
			InitializeComponent();
			M = new Metafile("buttrfly.wmf");
			SetStyle(ControlStyles.ResizeRedraw, true);
		}

		private void Form1_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.DrawImage(M, ClientRectangle);
		}
	}
}
