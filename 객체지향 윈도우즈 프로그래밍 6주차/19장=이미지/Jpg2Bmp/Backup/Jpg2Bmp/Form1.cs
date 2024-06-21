﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;

namespace Jpg2Bmp
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				string OldFile = openFileDialog1.FileName;
				string NewFile = Path.ChangeExtension(OldFile, "bmp");
				Image Jpg = Image.FromFile(OldFile);
				Jpg.Save(NewFile, ImageFormat.Bmp);
				MessageBox.Show(OldFile + "을 BMP로 변환했습니다.", "알림");
			}
		}
	}
}
