using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace second_week_making_dot
{
    public partial class Form1 : Form
    {
        public ArrayList ar;
        public Form1()
        {
            InitializeComponent();
            ar=new ArrayList();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            Graphics g=CreateGraphics();
            CMyData c=new CMyData();
            set_cmydata(e, c);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Paint_line(e);
        }
           private void set_cmydata(MouseEventArgs e,CMyData c_ref){
               Graphics g=CreateGraphics();

               if(e.Button==MouseButtons.Left){
                   CMyData c = new CMyData();
                   c_ref=c;

                   c.PenCol=Color.Red;
                   c.BrushCol=Color.Blue;
                   c.Size=5;
                   c.point=new Point(e.X,e.Y);
                   ar.Add(c);

           
                   Pen pen = new Pen(c.PenCol);
                SolidBrush brc = new SolidBrush(c.BrushCol);
                g.DrawEllipse(pen, new Rectangle(c.point.X, c.point.Y, c.Size, c.Size));
                g.FillEllipse(brc, new Rectangle(c.point.X, c.point.Y, c.Size, c.Size));

               }
           }

               private void Paint_line(PaintEventArgs e){
                   Graphics g=e.Graphics;
                   foreach(CMyData dot in ar){
                       Pen p=new Pen(dot.PenCol);
                       SolidBrush sb=new SolidBrush(dot.BrushCol);


                       g.DrawEllipse(p,dot.point.X,dot.point.Y,dot.Size,dot.Size);
                       g.FillEllipse(sb,dot.point.X,dot.point.Y,dot.Size,dot.Size);
                   }
               }
               class CMyData
               {
                   private Color bCol;
                   private Color pCol;
                   private int size;



                   public Color BrushCol
                   {
                       get { return bCol; }
                       set { bCol = value; }
                   }

                   public Color PenCol
                   {
                       get { return pCol; }
                       set { pCol = value; }
                   }

                   public Point point
                   {
                       get;
                       set;
                   }

                   public int Size
                   {
                       get { return size; }
                       set { size = value; }


                   }

               }

}
    }
   

  