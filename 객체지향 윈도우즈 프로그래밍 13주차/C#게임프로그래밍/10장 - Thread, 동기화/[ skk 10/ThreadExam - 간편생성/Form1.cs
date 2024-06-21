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
        static public Thread Thread1, Thread2;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormHandle = this.Handle;
            Rectangle rt1 = new Rectangle(5, 5, 100, 50);
            Rectangle rt2 = new Rectangle(5, 60, 100, 50);

            Thread1 = new Thread(new ParameterizedThreadStart(ThreadEx));
            Thread1.Start(rt1);

            Thread2 = new Thread(new ParameterizedThreadStart(ThreadEx));
            Thread2.Start(rt2);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Thread1.Abort();
            Thread2.Abort();
        }

        private void ThreadEx(Object p)
        {
            Rectangle rt = (Rectangle)p;
            Byte Blue = 255;

            while (true)
            {
                Graphics G = Graphics.FromHwnd(Form1.FormHandle);
                Color color = Color.FromArgb(255, 0, 0, Blue);
                G.FillRectangle(new SolidBrush(color), rt);
                Thread.Sleep(10);
                Blue++;
                G.Dispose();
            }

        }
    }

}
