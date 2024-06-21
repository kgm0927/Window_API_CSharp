using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageProcess_4
{
    public partial class Form1 : Form
    {


       private int[,] smoothing;
       private int[,] edge;
        public Form1()
        {
            InitializeComponent();
                smoothing=new int[,]{ { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } };
            edge=new int [,]{ { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };
        }

        private void 열기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            open_the_image_file(op);
       

        }

        private void open_the_image_file(OpenFileDialog op)
        {
            if (op.ShowDialog() == DialogResult.OK)
            {
                Image I = Image.FromFile(op.FileName);
                Form2 child = new Form2();
                child.image = I;
                child.MdiParent = this;
                child.Show();
            }
        }

        private void 밝게하기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 child = (Form2)this.ActiveMdiChild;
            coloring(1,ref child);
        }

        private void 어둡게하기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 child = (Form2)this.ActiveMdiChild;
            coloring(-1, ref child);
        }

        private void brighter(Image img, ref Bitmap bp, ref Color color)
        {
            bp = new Bitmap(img);

            for (int y = 0; y < bp.Height; y++)
            {
                for (int x = 0; x < bp.Width; x++)
                {
                    color = bp.GetPixel(x, y);

                    int r = color.R;
                    int g = color.G;
                    int b = color.B;
                    r = r + 50 > 255 ? 255 : r + 50;
                    g = g + 50 > 255 ? 255 : g + 50;
                    b = b + 50 > 255 ? 255 : b + 50;
                    bp.SetPixel(x, y, Color.FromArgb(r, g, b));


                }
            }
        }

        private void darker(Image img, ref Bitmap bp, ref Color color)
        {
            bp = new Bitmap(img);
            for (int y = 0; y < bp.Height; y++)
            {
                for (int x = 0; x < bp.Width; x++)
                {
                    color = bp.GetPixel(x, y);

                    int r = color.R;
                    int g = color.G;
                    int b = color.B;

                    r = r - 50 < 0 ? 0 : r - 50;
                    g = g - 50 < 0 ? 0 : g - 50;
                    b = b - 50 < 0 ? 0 : b - 50;

                    bp.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }
        }

        private void coloring(int act, ref Form2 Child)
        {
            // 만약 1이면 밝아지게, -1이면 어둡게
            Image img = Child.image;
            Bitmap bp = new Bitmap(img);
            Color col = new Color();
            if (Child != null)
            {


                if (act == 1)
                {
                    brighter(img, ref bp, ref col);
                }
                else if (act == -1)
                {
                    darker(img, ref bp, ref col);
                }
            }
            Child = new Form2();
            Child.image = bp;
            Child.MdiParent = this;
            Child.Show();

        }


        private void makeing_gray(Form2 original, Bitmap bp)     // 먼저 회색으로 만듦
        {

            if (original != null)
            {


                if (bp.PixelFormat.ToString() != "Format8bppIndexed")  // 컬러 영상일 때
                {
                    for (int x = 1; x < bp.Width; x++)
                    {
                        for (int y = 1; y < bp.Height; y++)
                        {
                            int color = bp.GetPixel(x, y).R + bp.GetPixel(x, y).G + bp.GetPixel(x, y).B;
                            color /= 3;

                            if (color > 255) color = 255;
                            if (color < 0) color = 0;

                            Color c = Color.FromArgb(color, color, color);
                            bp.SetPixel(x, y, c);
                        }
                    }
                }
            }


        }


        //

        private void finally_smoothing(Bitmap bp, Bitmap bp2, Form2 fm2, int[,] m)
        {

            int sum;

            for (int x = 1; x < bp.Width - 1; x++)
            {
                for (int y = 1; y < bp.Height - 1; y++)
                {
                    sum = 0;
                    for (int r = -1; r < 2; r++)
                    {
                        for (int c = -1; c < 2; c++)
                        {
                            sum += m[r + 1, c + 1] * bp.GetPixel(x + r, y + c).R;
                        }
                    }
                    sum = Math.Abs(sum);
                    sum /= 9;
                    if (sum > 255) sum = 255;
                    if (sum < 0) sum = 0;
                    bp2.SetPixel(x, y, Color.FromArgb(sum, sum, sum));

                }
            }

        }

        private void finally_edging(Bitmap bp, Bitmap bp2, Form2 fm2, int[,] m)
        {
            int sum;
            for (int x = 1; x < bp.Width - 1; x++)
            {
                for (int y = 1; y < bp.Height - 1; y++)
                {
                    sum = 0;
                    for (int r = -1; r < 2; r++)
                    {
                        for (int c = -1; c < 2; c++)
                        {

                            sum += m[r + 1, c + 1] * bp.GetPixel(x + r, y + c).R;

                        }
                    }
                    sum = Math.Abs(sum);
                    if (sum > 255) sum = 255;
                    //if (sum < 0) sum = 0;
                    bp2.SetPixel(x, y, Color.FromArgb(sum, sum, sum));
                }

            }
        }


        private void smoothingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 original = this.ActiveMdiChild as Form2;
            Bitmap bp = new Bitmap(original.image);


            makeing_gray(original, bp);

            Bitmap bp2 = new Bitmap(original.image);

            finally_smoothing(bp, bp2, original, smoothing);


            Form2 child = new Form2();
            child.image = bp2;
            child.MdiParent = this;
            child.Show();
        }

        private void edgeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 original = this.ActiveMdiChild as Form2;
            Bitmap bp = new Bitmap(original.image);


            makeing_gray(original, bp);

            Bitmap bp2 = new Bitmap(original.image);

            finally_edging(bp, bp2, original, edge);


            Form2 child = new Form2();
            child.image = bp2;
            child.MdiParent = this;
            child.Show();
        }




    }




}
