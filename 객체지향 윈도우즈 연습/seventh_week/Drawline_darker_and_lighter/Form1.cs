using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Drawline_darker_and_lighter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void 열기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            if (of.ShowDialog() == DialogResult.OK)
            {
                Image img = Image.FromFile(of.FileName);

                Form2 child = new Form2();
                child.image = img;
                child.MdiParent = this;
                child.Show();
                
            }
        }

        private void 밝게ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 Child = (Form2)this.ActiveMdiChild;
            coloring(1,ref Child);
        }

        private void 어둡게ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 child = (Form2)this.ActiveMdiChild;
            coloring(-1,ref child);

        }

        private void brighter( Image img,ref Bitmap bp,ref Color color)
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

        private void darker( Image img,ref Bitmap bp,ref Color color){
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

        private void coloring(int act,ref Form2 Child)
        {
            // 만약 1이면 밝아지게, -1이면 어둡게
            Image img = Child.image;
            Bitmap bp = new Bitmap(img);
            Color col = new Color();
            if (Child != null)
            {
             

                if (act == 1)
                {
                    brighter( img, ref bp, ref col);
                }
                else if (act == -1)
                {
                    darker( img, ref bp, ref col);
                }
            }
            Child = new Form2();
            Child.image = bp;
            Child.MdiParent = this;
            Child.Show();

        }

        private void smoothingToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


    }
}
