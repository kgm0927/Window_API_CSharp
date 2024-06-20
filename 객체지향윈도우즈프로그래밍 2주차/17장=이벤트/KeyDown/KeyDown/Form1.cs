using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KeyDown
{
	public partial class Form1 : Form   // 닷넷 소속이다. Form클래스는// partial 키워드는 하나의 클래스를 나눠서 작성한다는 의미
	{
		private int x, y;

		public Form1()
		{
			InitializeComponent();
			x = 10;
			y = 10;
		}

		private void Form1_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.FillEllipse(Brushes.Black, x - 8, y - 8, 16, 16);
		}

		private void Form1_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.Left:
					x -= 10;
					Invalidate();   // this 키워드를 붙일 수도 있으나 넣지 않을 수도 있다.// 인스턴스 자체를 가리킨다.
					break;
				case Keys.Right:
					x += 10;
					Invalidate();
					break;
				case Keys.Up:
					y -= 10;
					Invalidate();
					break;
				case Keys.Down:
					y += 10;
					Invalidate();
					break;
			}
		}
	}
}
