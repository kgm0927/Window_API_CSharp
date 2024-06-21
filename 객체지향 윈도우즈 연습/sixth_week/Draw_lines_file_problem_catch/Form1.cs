using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
namespace Draw_lines_file_problem_catch
{
    public partial class Form1 : Form
    {
        private LinkedList<CMyData<Point>> LL;
        private Color currentPenColor;
        private int penWidth;
        private Point passed_p;
        private CMyData<Point> data;


        public Form1()
        {
            InitializeComponent();
        }

        private void menuStrip1_MenuActivate(object sender, EventArgs e)
        {
            redToolStripMenuItem.Checked = (currentPenColor == Color.Red);
            greenToolStripMenuItem.Checked = (currentPenColor == Color.Green);
            blueToolStripMenuItem.Checked = (currentPenColor == Color.Blue);
        }

        private void redToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentPenColor = Color.Red;
        }

        private void greenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentPenColor = Color.Green;
        }

        private void blueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentPenColor = Color.Red;

        }

        private void 대화상자ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 파일ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 저장하기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            how_to_save();
        }

        private void 불러오기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            how_to_open();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach(CMyData<Point> line in LL){
                Pen pen = new Pen(line.PenCurrentColor, line.PenWidth);

                for (int i = 1; i < line.AR_size; i++)
                {
                    e.Graphics.DrawLine(pen, line.output_ar(i - 1), line.output_ar(1));

                }

            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point p = new Point(e.X,e.Y);
                data = new CMyData<Point>();
                data.PenCurrentColor = currentPenColor;
                data.PenWidth = this.penWidth;
                passed_p = p;
                data.input_ar(p);
                

            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Capture && e.Button == MouseButtons.Left)
            {
                
                Point p = new Point(e.X, e.Y);
                CreateGraphics().DrawLine(new Pen(data.PenCurrentColor,data.PenWidth), passed_p, p);
                data.input_ar(p);
                passed_p = p;

                CreateGraphics().Dispose();
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            LL.AddLast(data);
        }
        private void how_to_save()
        {
            saveFileDialog1.InitialDirectory = @"C:\temp";
            saveFileDialog1.Title = "파일 저장하기";
            saveFileDialog1.Filter = "Bin 파일|*.bin|모든 파일|*.*";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = new FileStream(saveFileDialog1.FileName, FileMode.CreateNew, FileAccess.Write);
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, LL);
                fs.Close();
            }

        }

        private void how_to_open()
        {
            openFileDialog1.InitialDirectory = @"C:\temp";
            openFileDialog1.Title = "파일 불러오기";
            openFileDialog1.Filter = "Bin 파일|*.bin|모든 파일|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    FileStream fs = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read);
                    BinaryFormatter bf = new BinaryFormatter();

                    LL = (LinkedList<CMyData<Point>>)bf.Deserialize(fs);
                    fs.Close();
                }
                catch (FileNotFoundException)
                {
                    MessageBox.Show("지정한 파일이 없습니다.");
                }
            }
        }


    }

    [Serializable]
    class CMyData<T>
    {


        private ArrayList ar;

        private int penSize;
        private Color penColor;


        public CMyData(){
            ar=new ArrayList();
        }


        public int PenWidth
        {
            get{return penSize;}
            set{penSize=value;}
        }
        public Color PenCurrentColor{
            get{return penColor;}
            set{penColor=value;}
        }
        public ArrayList AR{
            get{return ar;}
        }
        public void input_ar(T point){
            AR.Add(point);

        }
        public T output_ar(int i){
            return (T)AR[i];
        }

        public int AR_size{
            get{return AR.Count;}
        }


    }

}
