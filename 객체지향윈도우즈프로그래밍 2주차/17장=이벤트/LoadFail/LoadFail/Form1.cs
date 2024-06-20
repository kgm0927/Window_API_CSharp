using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LoadFail
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
			//if (true /*어떤 실패 조건*/)
			//{
			//	MessageBox.Show("초기화에 실패하여 실행을 계속할 수 없습니다.",
			//		"어머나 이걸 어째");
			//	Close();
			//}
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			if (true /*어떤 실패 조건*/)
			{
				MessageBox.Show("초기화에 실패하여 실행을 계속할 수 없습니다.",
					"어머나 이걸 어째");
				Close();
			}
		}
	}
}
