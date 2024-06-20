using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace 객체_윈도우즈_2주차
{
    public partial class Form1 : Form
    {
        ArrayList ar;
        public Form1()
        {
            InitializeComponent();
            ar = new ArrayList();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button==MouseButtons.Left){
                CMyData c = new CMyData();
                c.point = new Point(e.X, e.Y);

                ar.Add(c);// CMydata 마우스를 클릭하여 입력
            }
            Invalidate();// paint 시행하여 칠하기
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach (CMyData c in ar) // P.155  저장된 도형들을 차례대로 칠하기
            {
                SolidBrush brc = new SolidBrush(c.bColor); // brc  P.660
                Pen p = new Pen(c.pColor); // p P.671 

                e.Graphics.DrawEllipse(p, c.point.X, c.point.Y, c.Size, c.Size); // 테두리 그리기
                e.Graphics.FillEllipse(brc, c.point.X, c.point.Y, c.Size, c.Size); // 내부 색 채우기


               
            }
        }
    }

    public class CMyData // p.210 // 프로퍼티 사용
    {
      //  private Point point;// 마우스 클릭 좌표 저장
        private Color penCol;// 도형의 테두리 색 저장
        private Color brushCol;// 도형의 내부 색 저장
        private int size;// 도형의 크기, 모양 저장
        




        public Point point
        {
            get;
            set;
        }

        public Color bColor
        {
            get { return brushCol; }
            set { brushCol = value; }
        }
        public Color pColor//
        {
            get { return penCol; }  // Rvalue  저장된 색 정보 배출
            set { penCol = value; } // Lvalue  색 저장
        }

        public CMyData()
        {
            penCol = Color.Yellow;
            brushCol = Color.Red;
            size = 10;
        }

        public int Size// 
        {
            get { return size; }
            set { size = value; }
        }
      

    }

}
