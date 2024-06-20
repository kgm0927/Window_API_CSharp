using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WizardHandler
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Paint(object sender, PaintEventArgs e)
		{
            Graphics myDC = e.Graphics; //PaintEventArgs.Graphics 속성을 이용하여(PaintEventArgs 클래스는 Graphics 속성을 제공)   
                                        //e 객체가 담고 있는, System.Drawing space의 Graphics class의 instance 를 가져 온다.
			                            
                                        //MFC 에서 CDC class의 instance가 인자 e 에 담겨져 넘어 온다.
										//이제 myDC로  Graphics class의 그리기 method를 호출할 수 있다.

			myDC.DrawEllipse(Pens.Blue, 10, 10, 200, 200); 
			//System.Drawing space의 Pens class의 static property들은 각종색깔의 Pen Class instance를 return 한다.
			//Win32 API에서Stock Pen과 유사.
		}
	}
}
