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
            Form2 Child = (Form2)this.ActiveMdiChild; 
            if (Child != null)
            {
                
                Image I = Child.image;

                Bitmap B = new Bitmap(I);
                 for (int y = 0; y < B.Height; y+=3)
                     for (int x = 0; x < B.Width; x+=3)
                     {
                        int sum=0;
                         


                         for (int s_x = 0; s_x < 3; s_x++)
                         {
                             for (int s_y = 0; s_y < 3; s_y++)
                             {
                                 Color color = B.GetPixel(x + s_x, y + s_y);

                                 byte r = (byte)~color.R;
                                 byte g = (byte)~color.G;
                                 byte b = (byte)~color.B;

                                 sum = ((int)r + (int)g + (int)b) / 9;
                                 B.SetPixel(x, y, Color.FromArgb((byte)sum,(byte)sum,(byte)sum));


                             }
                         }
                     
                     
                     }
            }
        }

     }
}
