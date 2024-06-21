using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageProcess
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void 열기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            if (op.ShowDialog() == DialogResult.OK)
            {
                Image I = Image.FromFile(op.FileName);

                Form2 Child = new Form2();
                Child.image = I;
                Child.MdiParent = this;
                Child.Show();
            }
        }

        private void 밝게하기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 Child = (Form2)this.ActiveMdiChild;
            if (Child != null)
            {
                Image I = Child.image;

                Bitmap B = new Bitmap(I);
                for (int y = 0; y < B.Height; y++)
                    for (int x = 0; x < B.Width; x++)
                    {
                        Color color = B.GetPixel(x, y);
                        int r = color.R;
                        int g = color.G;
                        int b = color.B;
                        // Saturation

                        if (((ToolStripMenuItem)sender).Text.Equals("밝게하기"))
                        {
                            r = r + 50 > 255 ? 255 : r + 50;
                            g = g + 50 > 255 ? 255 : g + 50;
                            b = b + 50 > 255 ? 255 : b + 50;
                        }
                        else
                        {
                            r = r - 50 < 0 ? 0 : r - 50;
                            g = g - 50 < 0 ? 0 : g - 50;
                            b = b - 50 < 0 ? 0 : b - 50;
                        }
                        B.SetPixel(x, y, Color.FromArgb(r, g, b));
                    }

                Child = new Form2();
                Child.image = B;
                Child.MdiParent = this;
                Child.Show();
            }

        }

        private void smoothingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 original = ActiveMdiChild as Form2;
            if (original != null)
            {
                Bitmap gBitmap = new Bitmap(original.image);
                //https://docs.microsoft.com/ko-kr/dotnet/api/system.drawing.imaging.pixelformat?view=netframework-4.8
                if (gBitmap.PixelFormat.ToString() != "Format8bppIndexed")
                {
                    for (int i = 0; i < gBitmap.Height; i++)
                    {
                        for (int j = 0; j < gBitmap.Width; j++)
                        {
                            int color = gBitmap.GetPixel(j, i).R + gBitmap.GetPixel(j, i).G + gBitmap.GetPixel(j, i).B;
                            color /= 3;
                            Color c = Color.FromArgb(color, color, color);
                            gBitmap.SetPixel(j, i, c);
                        }
                    }

                }
                Bitmap Smoothing = new Bitmap(original.image);
                int[,] m = { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } };
                int sum;
                for (int x = 1; x < gBitmap.Width - 1; x++)
                {
                    for (int y = 1; y < gBitmap.Height - 1; y++)
                    {
                        sum = 0;
                        for (int r = -1; r < 2; r++)
                        {
                            for (int c = -1; c < 2; c++)
                            {
                                sum += m[r + 1, c + 1] * gBitmap.GetPixel(x + r, y + c).R;
                            }
                        }
                        sum = Math.Abs(sum);
                        sum /= 9;
                        if (sum > 255) sum = 255;   // 절대치가 있어야 한다.
                        if (sum < 0) sum = 0;
                        Smoothing.SetPixel(x, y, Color.FromArgb(sum, sum, sum));
                    }
                }
                Form2 child = new Form2();
                child.image = Smoothing;
                child.MdiParent = this;
                child.Show();
            }
        }

        private void edgeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 original = ActiveMdiChild as Form2;

            if (original != null)
            {
                Bitmap gBitmap = new Bitmap(original.image);
                //https://docs.microsoft.com/ko-kr/dotnet/api/system.drawing.imaging.pixelformat?view=netframework-4.8
                if (gBitmap.PixelFormat.ToString() != "Format8bppIndexed")
                {
                    for (int i = 0; i < gBitmap.Height; i++)
                    {
                        for (int j = 0; j < gBitmap.Width; j++)
                        {
                            int color = gBitmap.GetPixel(j, i).R + gBitmap.GetPixel(j, i).G + gBitmap.GetPixel(j, i).B;
                            color /= 3;
                            Color c = Color.FromArgb(color, color, color);
                            gBitmap.SetPixel(j, i, c);
                        }
                    }

                }
                Bitmap Edge = new Bitmap(gBitmap);
                int[,] m = { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };
                int sum;
                for (int x = 1; x < gBitmap.Width - 1; x++)
                {
                    for (int y = 1; y < gBitmap.Height - 1; y++)
                    {
                        sum = 0;
                        for (int r = -1; r < 2; r++)
                        {
                            for (int c = -1; c < 2; c++)
                            {

                                sum += m[r + 1, c + 1] * gBitmap.GetPixel(x + r, y + c).R;

                            }
                        }
                        sum = Math.Abs(sum);
                        if (sum > 255) sum = 255;
                        //if (sum < 0) sum = 0;
                        Edge.SetPixel(x, y, Color.FromArgb(sum, sum, sum));
                    }

                }
                Form2 child = new Form2();
                child.image = Edge;
                child.MdiParent = this;
                child.Show();
            }
        }

     }
}
