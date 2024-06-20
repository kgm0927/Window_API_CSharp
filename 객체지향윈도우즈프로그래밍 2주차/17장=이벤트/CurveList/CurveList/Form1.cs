using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace CurveList
{
	public partial class Form1 : Form
	{
		private int x, y;
		private ArrayList ar;

		public Form1()
		{
			ar = new ArrayList();
			InitializeComponent();
		}

		private void Form1_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				x = e.X;
				y = e.Y;
			}
		}

		private void Form1_MouseMove(object sender, MouseEventArgs e)
		{
			if (Capture && e.Button == MouseButtons.Left)
			{
				Graphics G = CreateGraphics();
				G.DrawLine(Pens.Black, x, y, e.X, e.Y);
				ar.Add(Rectangle.FromLTRB(x, y, e.X, e.Y));
				x = e.X;
				y = e.Y;
				G.Dispose();
			}
		}

		private void Form1_Paint(object sender, PaintEventArgs e)
		{
			foreach (Rectangle R in ar)
			{
				e.Graphics.DrawLine(Pens.Black, R.Left, R.Top, R.Right, R.Bottom);
			}
		}
	}
}
