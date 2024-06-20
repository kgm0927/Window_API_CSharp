using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections; //배열, 배열 리스트, 해시 테이블, 큐 등을 담고 있을 ArrayList를 사용하기 위한 선언



// 속성에서 doublebuffered를 클릭하면 깜박거리지 않는다.
namespace First
{
    public partial class Form1 : Form  //Form1은 Form 상속 받음
    {
        ArrayList ar;
        public Form1()
        {
            InitializeComponent();
            ar = new ArrayList(); //ar리스트 생성/  ArrayList 객체 생성
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)  //마우스를 클릭할 때  만약 왼쪽 버튼을 눌렀을 때
        {
            Random Random = new Random(); // 

            if (e.Button == MouseButtons.Left) // 왼쪽 버튼을 눌렀을 때 1048576
            {
                CMyData c = new CMyData(); //
                c.Shape = (int)Random.Next(2); //랜덤으로 0  or 1 설정, 원인지 사각형인지 결정하여 저장  / 교재 p.388
                c.Size = (int)Random.Next(50, 200); // 50-199 중에 랜덤으로 받아서 크기 설정 후 저장
                c.Point = new Point(e.X, e.Y); // 클릭한 마우스 위치 저장 //set을 사용함.
                c.bColor = Color.FromArgb(Random.Next(256), Random.Next(256), Random.Next(256)); //r,g,b 255 // static이므로 객체를 생성하지 않음
                c.pColor = Color.FromArgb(Random.Next(256), Random.Next(256), Random.Next(256));  // r,g,b 255 
                ar.Add(c); // CMydata  마우스를 클릭하며 랜덤으로 생성된 도형의 정보를 저장
               
                SolidBrush brc = new SolidBrush(c.bColor); // brc  P.660
                Pen p = new Pen(c.pColor); // p P.671 
                Graphics g = CreateGraphics();
                g.DrawEllipse(Pens.Red, c.Point.X, c.Point.Y, c.Size, c.Size);
                g.FillEllipse(brc, c.Point.X, c.Point.Y, c.Size, c.Size); // 내부 색 채우기



              //  this.Invalidate();
              //  Rectangle rc = new Rectangle(c.Point.X,c.Point.Y,c.Size,c.Size);
              //  Invalidate(rc); // paint 시행하여 칠하기// 배경 모두 지우고 다시 그려라 ... // 이러한 이유로 깜박거리는 결과가 나온다. //현재 파라미터가 rc 이므로 이 부분만 다시 그리게 한다. (무효 영역만 그린다는 의미.)

            }
         
        }

        private void Form1_Paint(object sender, PaintEventArgs e)  //윈도우에 칠하는 것 (윈도우를 다시 띄울때 저장된 것들을 다시 칠하는 것 포함)
        {
            foreach (CMyData c in ar) // P.155  저장된 도형들을 차례대로 칠하기
            {
                SolidBrush brc = new SolidBrush(c.bColor); // brc  P.660
                Pen p = new Pen(c.pColor); // p P.671 
                if (c.Shape == 1) // 1은 원 도형
                {
                    Graphics g = e.Graphics;
                    g.DrawEllipse(p, c.Point.X, c.Point.Y, c.Size, c.Size); // 테두리 그리기 // e 안에 담아 있어서 Graphics 라는 프로퍼티를 사용한다.
                   g.FillEllipse(brc, c.Point.X, c.Point.Y, c.Size, c.Size); // 내부 색 채우기
                }
                else  //0은 직사각형
                {
                    e.Graphics.DrawRectangle(p, c.Point.X, c.Point.Y, c.Size, c.Size);
                    e.Graphics.FillRectangle(brc, c.Point.X, c.Point.Y, c.Size, c.Size);
                }
            }
        }
    }
    public class CMyData // p.210 // 프로퍼티 사용
    {
      //  private Point point;// 마우스 클릭 좌표 저장
        private Color penCol;// 도형의 테두리 색 저장
        private Color brushCol;// 도형의 내부 색 저장
        private int size, shape;// 도형의 크기, 모양 저장
       
        public Point Point//    // Property
        { get; set; }// 한번에 필드 정의그리고 Property 정의 // 아래의 비슷한 내용을 아예 get; set; 으로 줄인 것이다.
        
        public Color pColor//
        {
            get { return penCol; }  // Rvalue  저장된 색 정보 배출
            set { penCol = value; } // Lvalue  색 저장
        }
        public Color bColor//
        {
            get { return brushCol; }   // 저장된 색 정보 배출
            set { brushCol = value; } // 색 저장
        }
       
        public int Size// 
        {
            get { return size; }
            set { size = value; }
        }
        public int Shape//
        {
            get { return shape; }
            set { shape = value; }
        }
    }
}
