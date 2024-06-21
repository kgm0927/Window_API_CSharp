using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StringResource
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Paint(object sender, PaintEventArgs e)
		{
			//string Mes = "안녕하세요";
			string Mes = Properties.Resources.String1;
			e.Graphics.DrawString(Mes, Font, Brushes.Black, 10, 10);
		}
	}
}
