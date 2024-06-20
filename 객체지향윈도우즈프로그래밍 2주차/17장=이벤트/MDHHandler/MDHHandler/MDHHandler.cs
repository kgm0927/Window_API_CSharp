using System;
using System.Drawing;
using System.Windows.Forms;

//* 정적 메서드
class MDHHandler : Form
{
	public static void Main()
	{
		MDHHandler MyForm = new MDHHandler();
		MyForm.Paint += new PaintEventHandler(MyPaint);
		Application.Run(MyForm);
	}

	static void MyPaint(Object sender, PaintEventArgs e)
	{
		e.Graphics.DrawEllipse(Pens.Blue, 10, 10, 200, 200);
	}
}
//*/

/* 인스턴스 메서드
class MDHHandler : Form
{
	public static void Main()
	{
		MDHHandler MyForm = new MDHHandler();
		MyForm.Paint += new PaintEventHandler(MyForm.MyPaint);
		Application.Run(MyForm);
	}

	void MyPaint(Object sender, PaintEventArgs e)
	{
		e.Graphics.DrawEllipse(Pens.Blue, 10, 10, 200, 200);
	}
}
//*/

/* 가상 함수 재정의
class MDHHandler : Form
{
	public static void Main()
	{
		MDHHandler MyForm = new MDHHandler();
		Application.Run(MyForm);
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		e.Graphics.DrawEllipse(Pens.Blue, 10, 10, 200, 200);
	}
}
//*/

/* 메서드 추가와 가상 함수 재정의
class MDHHandler : Form
{
	public static void Main()
	{
		MDHHandler MyForm = new MDHHandler();
		MyForm.Paint += new PaintEventHandler(MyForm.MyPaint); 
		Application.Run(MyForm);
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		//base.OnPaint(e);
		e.Graphics.DrawString("OnPaint 메서드 호출", Font, Brushes.Black, 10, 10);
	}

	void MyPaint(Object sender, PaintEventArgs e)
	{
		e.Graphics.DrawString("Paint 이벤트 핸들러 호출", Font, Brushes.Black, 10, 30);
	}
}
//*/