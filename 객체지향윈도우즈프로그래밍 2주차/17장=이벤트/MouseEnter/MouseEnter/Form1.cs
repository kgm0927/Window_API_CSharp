using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MouseEnter
{
	public partial class Form1 : Form
	{
		private int count;
		private string Status;
		public Form1()
		{
			Status = "알수 없음";
			InitializeComponent();
		}

		private void Form1_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.Clear(BackColor);
			e.Graphics.DrawString(Status, Font, Brushes.Black, 10, 10);
			e.Graphics.DrawString("카운트 : " + count.ToString(),
				Font, Brushes.Black, 10, 30);
		}

		private void Form1_MouseEnter(object sender, EventArgs e)
		{
			Status = "컨트롤 안으로 들어왔다.";
			Invalidate();
		}

		private void Form1_MouseHover(object sender, EventArgs e)
		{
			count++;
			Invalidate();
		}

		private void Form1_MouseLeave(object sender, EventArgs e)
		{
			Status = "컨트롤 바깥으로 나갔다.";
			Invalidate();
		}
	}
}
