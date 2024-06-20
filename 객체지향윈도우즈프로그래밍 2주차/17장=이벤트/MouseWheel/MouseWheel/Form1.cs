using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Windows.Forms;

namespace MouseWheel
{
	public partial class Form1 : Form
	{
		private int x = 10000;
		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_MouseWheel(object sender, MouseEventArgs e)
		{
			x += e.Delta;
			Invalidate();
		}

		private void Form1_Paint(object sender, PaintEventArgs e)
		{
			string str = "현재 휠 값 : " + x.ToString();
			e.Graphics.DrawString(str, Font, Brushes.Black, 10, 10);
			str = "현재 휠 값 : " + (x/120.0).ToString();
			e.Graphics.DrawString(str, Font, Brushes.Black, 10, 30);
		}
	}
}
