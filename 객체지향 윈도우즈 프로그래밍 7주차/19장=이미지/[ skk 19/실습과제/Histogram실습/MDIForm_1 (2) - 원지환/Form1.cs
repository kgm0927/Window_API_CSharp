using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;

using System.Runtime.Serialization.Formatters.Binary;

namespace MDIForm
{
    public partial class Form1 : Form
    {
        Bitmap bit;
        Image image;
        int[] greenArray_x;
        int[] greenArray_y;

        public Form1()
        {
            InitializeComponent();
        }

        private void 불러오기NToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "영상파일 열기";
            openFileDialog1.InitialDirectory = "C:Users";
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "All Files|*.*";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                image = Image.FromFile(openFileDialog1.FileName);

                bit = new Bitmap(image);

                Form2 Child = new Form2();
                Child.MdiParent = this;
                Child.show_bitmap = bit;
                Child.Show();
            }
        }

        private void 저장하기CToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Title = "다른 이름으로 저장";
            saveFileDialog1.FileName = "";
            saveFileDialog1.Filter = "Bitmap File|*.bmp";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Form2 Child = (Form2)ActiveMdiChild;

                if (Child != null)
                {
                    image = Child.show_bitmap;
                    image.Save(saveFileDialog1.FileName, ImageFormat.Jpeg);
                }
            }
        }

        private void 밝게ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 Child = (Form2)ActiveMdiChild;
            image = Child.show_bitmap;

            int x, y;
            int r, g, b;
            const int ADD_VALUE = 50;
            Color color;

            Graphics gr = CreateGraphics();
            Bitmap gBitmap = new Bitmap(image);
            for (y = 0; y < image.Height; y++)
                for (x = 0; x < image.Width; x++)
                {
                    color = gBitmap.GetPixel(x, y);
                    r = color.R + ADD_VALUE; if (r > 255) r = 255;
                    g = color.G + ADD_VALUE; if (g > 255) g = 255;
                    b = color.B + ADD_VALUE; if (b > 255) b = 255;
                    color = Color.FromArgb(r, g, b);
                    gBitmap.SetPixel(x, y, color);
                }
            Form2 dlg = new Form2();
            dlg.MdiParent = this;
            dlg.show_bitmap = gBitmap;
            dlg.Show();
            gr.Dispose();
        }

        private void 어둡게ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 Child = (Form2)ActiveMdiChild;
            image = Child.show_bitmap;

            int x, y;
            int r, g, b;
            const int ADD_VALUE = 50;
            Color color;

            Graphics gr = CreateGraphics();
            Bitmap gBitmap = new Bitmap(image);
            for (y = 0; y < image.Height; y++)
                for (x = 0; x < image.Width; x++)
                {
                    color = gBitmap.GetPixel(x, y);
                    r = color.R - ADD_VALUE; if (r < 0) r = 0;
                    g = color.G - ADD_VALUE; if (g < 0) g = 0;
                    b = color.B - ADD_VALUE; if (b < 0) b = 0;
                    color = Color.FromArgb(r, g, b);
                    gBitmap.SetPixel(x, y, color);
                }
            Form2 dlg = new Form2();
            dlg.MdiParent = this;
            dlg.show_bitmap = gBitmap;
            dlg.Show();
            gr.Dispose();
        }

        private void 번호판출력ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int x, y;
            int r, g, b;
            int green_count;
            Color color;

            Bitmap gBitmap = new Bitmap(image);
            greenArray_x = new int[image.Height];

            for (y = 0; y < image.Height; y++)
            {
                green_count = 0;

                for (x = 0; x < image.Width; x++)
                {
                    color = gBitmap.GetPixel(x, y);
                    r = color.R;
                    g = color.G;
                    b = color.B;

                    if ((r >= 0 && r <= 100) && (g >= 100 && g <= 255) && (b >= 0 && b <= 100))
                        green_count++;
                }
                greenArray_x[y] = green_count;
            }

            Bitmap gBitmap_x = new Bitmap(image);

            for (y = 0; y < image.Height; y++)
            {
                for (x = 0; x < image.Width; x++)
                {
                    if (x <= greenArray_x[y])
                        gBitmap_x.SetPixel(x, y, Color.Green);
                    else
                        gBitmap_x.SetPixel(x, y, Color.White);
                }
            }          

            greenArray_y = new int[image.Width];

            for (x = 0; x < image.Width; x++)
            {
                green_count = 0;

                for (y = 0; y < image.Height; y++)
                {
                    color = gBitmap.GetPixel(x, y);
                    r = color.R;
                    g = color.G;
                    b = color.B;

                    if ((r >= 0 && r <= 100) && (g >= 100 && g <= 255) && (b >= 0 && b <= 100))
                        green_count++;
                }
                greenArray_y[x] = green_count;
            }

            Bitmap gBitmap_y = new Bitmap(image);

            for (y = 0; y < image.Width; y++)
            {
                for (x = 0; x < image.Height; x++)
                {
                    if (x <= greenArray_y[y])
                        gBitmap_y.SetPixel(y, x, Color.Green);
                    else
                        gBitmap_y.SetPixel(y, x, Color.White);
                }
            }

            Form2 Child = new Form2();
            Child.MdiParent = this;
            Child.show_bitmap = gBitmap_x;
            Child.Show();

            Form2 Child2 = new Form2();
            Child2.MdiParent = this;
            Child2.show_bitmap = gBitmap_y;
            Child2.Show();
        }
    }
}
