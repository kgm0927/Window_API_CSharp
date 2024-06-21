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
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace sixth_week
{
    public partial class Form1 : Form
    {
        private LinkedList<CMyData<Point>> LL;
        private int PenWidth;
        private Color CurrentPenColor;
        private Point passed_p;
        private CMyData<Point> data;

        public Form1()
        {
            LL = new LinkedList<CMyData<Point>>();
            CurrentPenColor = Color.Red;
            PenWidth = 1;
            InitializeComponent();
        }

        private void menuStrip1_MenuActivate(object sender, EventArgs e)
        {
            redToolStripMenuItem.Checked = (CurrentPenColor == Color.Red);
            greenToolStripMenuItem.Checked = (CurrentPenColor == Color.Green);
            blueToolStripMenuItem.Checked = (CurrentPenColor == Color.Blue);
        }

        private void 저장하기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            how_to_save();
        }

        private void 불러오기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            how_to_open();
        }
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point p = new Point(e.X, e.Y);


                data = new CMyData<Point>();
                data.PenColor = CurrentPenColor;
                data.PenWidth = PenWidth;

                passed_p = p;

                data.AR.Add(p);


            }
        }



        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Capture && e.Button == MouseButtons.Left)
            {
                Graphics g = CreateGraphics();
                Point p = new Point(e.X, e.Y);
                Pen pen = new Pen(data.PenColor,data.PenWidth);
                g.DrawLine(pen, passed_p, p);

                passed_p = p;

                data.AR.Add(p);
                g.Dispose();

            }
        }
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            LL.AddLast(data);
        }

        private void redToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentPenColor = Color.Red;

        }

        private void greenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentPenColor = Color.Green;
        }

        private void blueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentPenColor = Color.Blue;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach (CMyData<Point> line in LL)
            {
                Graphics g = e.Graphics;
                Pen pen = new Pen(line.PenColor, line.PenWidth);
                for (int i = 1; i < line.AR.Count; i++)
                {
                    g.DrawLine(pen, line.output_ar(i-1), line.output_ar(i ));
                }
            }

        }

        [Serializable]
        class CMyData<T>
        {
            private int penWidth;
            private Color col;
            private ArrayList ar;

            public CMyData()
            {
                ar = new ArrayList();
            }
            public int PenWidth
            {
                get { return penWidth; }
                set { penWidth = value; }
            }

            public Color PenColor
            {
                get { return col; }
                set { col = value; }
            }

            public ArrayList AR
            {
                get { return ar; }
            }

            public T output_ar(int i)
            {
                return (T)ar[i];
            }




        }

        private void 대화상자ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 dlg = new Form2();
            dlg.iDialogPenWidth=PenWidth;
            dlg.DialogPenColor = CurrentPenColor;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                PenWidth = dlg.iDialogPenWidth;
                CurrentPenColor = dlg.DialogPenColor;
            }
            dlg.Dispose();
        }

        private void how_to_save(){
            saveFileDialog1.InitialDirectory = @"C:Wtemp";
            saveFileDialog1.Title = "파일 저장하기";
            saveFileDialog1.Filter="Bin 파일|*.bin|모든 파일|*.*";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = new FileStream(saveFileDialog1.FileName, FileMode.Create, FileAccess.Write);
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, LL);
                fs.Close();
            }

        }

        private void how_to_open()
        {
            openFileDialog1.InitialDirectory = @"c:Wtemp";
            openFileDialog1.Title = "파일 불러오기";
            openFileDialog1.Filter = "Bin 파일|*.bin|모든 파일|*.*"; 

            if(openFileDialog1.ShowDialog()==DialogResult.OK){
                FileStream fs=new FileStream(openFileDialog1.FileName,FileMode.Open,FileAccess.Read);
                BinaryFormatter bf=new BinaryFormatter();
                LL = (LinkedList<CMyData<Point>>)bf.Deserialize(fs);
                fs.Close();
                Invalidate();

            }

        }


    }
}
