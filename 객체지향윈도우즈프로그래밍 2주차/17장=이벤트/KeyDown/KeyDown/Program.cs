﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace KeyDown
{
	static class Program
	{
		/// <summary>
		/// 해당 응용 프로그램의 주 진입점입니다.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());
        // run는 메시지 루핑(looping)을 하는 것과 같다.
		}
	}
}
