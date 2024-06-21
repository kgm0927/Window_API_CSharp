using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;

namespace PlaySound
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			SoundPlayer P = new SoundPlayer();
			//P.SoundLocation = "dingdong.wav";
			P.Stream = Properties.Resources.dingdong;
			P.Play();

			//SystemSounds.Asterisk.Play();
		}
	}
}
