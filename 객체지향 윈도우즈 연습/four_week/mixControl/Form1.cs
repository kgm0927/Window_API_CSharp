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
namespace mixControl
{
    public partial class Form1 : Form
    {


        private LinkedList<CMyData> LL;
        private int CurrentShape;
        private int iCurrentWidth = 1;

        private CMyData mydata;
        private Color CurrentColor;
        private Point passed_p;
        public Form1()
        {
            CurrentColor = Color.Red;
            LL = new LinkedList<CMyData>();
            InitializeComponent();
        }

        private void 사각형ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentShape = 1;
            Invalidate();
        }

        private void 원형ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentShape = 0;
            Invalidate();
        }

        private void 자유곡선ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentShape = 2;
            Invalidate();
        }

        private void menuStrip1_MenuActivate(object sender, EventArgs e)
        {
        사각형ToolStripMenuItem.Checked=(CurrentShape==1);
        원형ToolStripMenuItem.Checked = (CurrentShape == 0);
        자유곡선ToolStripMenuItem.Checked = (CurrentShape == 2);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
               
                mydata = new CMyData();
                mydata.Shape = CurrentShape;
                mydata.Color = CurrentColor;
                mydata.Width=CurrentShape;
                if (mydata.Shape != 2)
                {
                    mydata.Width = 10;
                }

                passed_p = new Point(e.X, e.Y);

                mydata.AR.Add(passed_p);

                SolidBrush b = new SolidBrush(CurrentColor);
                Graphics g = this.CreateGraphics();
                if (CurrentShape == 0)
                {
                    g.FillRectangle(b, e.X, e.Y, 10 * iCurrentWidth, 10 * iCurrentWidth);
                    g.DrawRectangle(new Pen(Color.Black), e.X, e.Y, 10 * iCurrentWidth, 10 * iCurrentWidth);
                }
                if (CurrentShape == 1)
                {
                    g.FillEllipse(b, e.X, e.Y, 10 * iCurrentWidth, 10 * iCurrentWidth);
                    g.DrawEllipse(new Pen(Color.Black), e.X, e.Y, 10 * iCurrentWidth, 10 * iCurrentWidth);
                }


            }
            
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
           if((Capture&&e.Button==MouseButtons.Left) &&mydata.Shape==2){

               Graphics g = CreateGraphics();
               Point p=new Point(e.X,e.Y);

               g.DrawLine(new Pen(mydata.Color), passed_p, p);

               passed_p = p;
               mydata.AR.Add(passed_p);

               g.Dispose();
            
           }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            LL.AddLast(mydata);
          
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {

            foreach (CMyData draw in LL)
            {
                Graphics g = e.Graphics;
                Pen pen=new Pen(Color.Black);
                if (draw.Shape != 2)
                {
                    Rectangle rect = new Rectangle(((Point)draw.AR[0]).X, ((Point)draw.AR[0]).Y, draw.Width, draw.Width);
                    SolidBrush sb = new SolidBrush(draw.Color);

                    if (draw.Shape == 0) // 원 생성
                    {
                       
                        g.DrawEllipse(pen, rect);
                        g.FillEllipse(sb, rect);
                    }
                    else if (draw.Shape == 1)
                    {
                        g.DrawRectangle(pen, rect);
                        g.DrawRectangle(pen, rect);
                    }

                }
                else
                {
                    Pen line_p = new Pen(draw.Color);

                    for (int i = 1; i < draw.AR.Count; i++)
                    {
                       
                        g.DrawLine(line_p,(Point)draw.AR[i-1],(Point)draw.AR[i]);
                    }
                }
           

            }

            
        }
    }


    class CMyData
    {
        private int shape; // 0 원 // 1 사각형 // 2 선
        private Color col;
        private ArrayList ar;
        private int width;


        public CMyData()
        {
            ar = new ArrayList();
        }

        public int Shape
        {
            get { return shape; }
            set{shape=value;}
        }

        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        public Color Color
        {
            get { return col; }
            set { col = value; }
        }
        public ArrayList AR
        {
            get { return ar; }
        }


    }
}
