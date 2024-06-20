using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TimerTimeTest
{
	public class MyTime
	{
		private int hour, min, sec;
		public int Hour
		{
			set { hour = value; }
			get { return hour; }
		}
		public int Min
		{
			set { min = value; }
			get { return min; }
		}
		public int Sec
		{
			set { sec = value; }
			get { return sec; }
		}

		public MyTime() { }
		public string GetTime()
		{
			return hour + "시" + min + "분" + sec + "초";
		}
		public static MyTime operator ++(MyTime A)
		{
			MyTime T = new MyTime();
			T.hour = A.hour;
			T.min = A.min;
			T.sec = A.sec;

			T.sec++;
			T.min += T.sec / 60;
			T.sec %= 60;
			T.hour += T.min / 60;
			T.min %= 60;

			return T;
		}
	}
	public partial class Form1 : Form
	{
		private MyTime tick_time;
		private string tick_time_string;

		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			Timer T = new Timer();
			T.Interval = 1000;

			DateTime dt = DateTime.Now;
			tick_time = new MyTime();
			tick_time.Hour = dt.Hour;
			tick_time.Min = dt.Minute;
			tick_time.Sec = dt.Second;

			T.Tick += new EventHandler(T_Tick);
			T.Start();
		}

		void T_Tick(object sender, EventArgs e)
		{
			tick_time++;
			Invalidate();
		}

		private void Form1_Paint(object sender, PaintEventArgs e)
		{
			DateTime dt = DateTime.Now;
			string real_time = dt.Hour + "시" + dt.Minute + "분" + dt.Second + "초";
			e.Graphics.DrawString("실제 시간 : " + real_time, Font, Brushes.Black, 10, 10);
			e.Graphics.DrawString("타이머 시간 : " + tick_time.GetTime(), Font, Brushes.Black, 10, 30);
		}
	}
}
