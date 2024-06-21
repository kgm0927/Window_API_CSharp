using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lebel_Pos
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.LabelX = label1.Left;
            f2.LabelY = label1.Top;
            f2.LabelText = label1.Text;

            if (f2.ShowDialog() == DialogResult.OK)
            {
                label1.Left = f2.LabelX;
                label1.Top = f2.LabelY;
                label1.Text = f2.LabelText;
            }
        }
    }
}
