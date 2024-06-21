using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Drawing.Drawing2D;

namespace ThreadExam
{
    public partial class Form1 : Form
    {
        static public IntPtr FormHandle;
        Thread ThreadOne, ThreadTwo;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormHandle = this.Handle;
            Rectangle rt1 = new Rectangle(5, 5, 100, 50);
            Rectangle rt2 = new Rectangle(5, 60, 100, 50);
            ThreadEx Thread1 = new ThreadEx(rt1);
            ThreadOne = new Thread(new ThreadStart(Thread1.painting));
            ThreadOne.Start();

            ThreadEx Thread2 = new ThreadEx(rt2);
            ThreadTwo = new Thread(new ThreadStart(Thread2.painting));
            ThreadTwo.Start();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ThreadOne.Abort();
            ThreadTwo.Abort();
        }
    }

    class ThreadEx
    {
        private Rectangle rt;
        public ThreadEx(Rectangle rect)
        {
            rt = rect;
        }
        public void painting()
        {
            Byte Blue=255;
           
            while (true)
            {
                Graphics G = Graphics.FromHwnd(Form1.FormHandle);
                Color color=Color.FromArgb(255,0,0,Blue);
                G.FillRectangle(new SolidBrush(color), rt);
                Thread.Sleep(10);
                Blue++;
                G.Dispose();
            }
          
        }

    }
}
