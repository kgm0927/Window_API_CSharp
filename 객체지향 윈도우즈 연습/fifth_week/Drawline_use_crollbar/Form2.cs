using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Drawline_use_crollbar
{
    public partial class Form2 : Form
    {
        private Color color;
        private int width;
        private int combo_index;

        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        public Form2()
        {

       
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            string[] arr = new string[] { "1", "2", "4", "8", "16" };
            for (int i = 0; i < arr.Length; i++)
            {
                comboBox1.Items.Add(arr[i]);

            }
            comboBox1.Text = width.ToString();

            hScrollBar1.Value = color.R;
            hScrollBar2.Value = color.G;
            hScrollBar3.Value = color.B;

            textBox1.Text = color.R.ToString();
            textBox2.Text = color.G.ToString();
            textBox3.Text = color.B.ToString();

            comboBox1.Text = width.ToString();
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            color = Color.FromArgb(hScrollBar1.Value, hScrollBar2.Value, hScrollBar3.Value);
            textBox1.Text = hScrollBar1.Value.ToString();
            textBox2.Text = hScrollBar2.Value.ToString();
            textBox3.Text = hScrollBar3.Value.ToString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            width = int.Parse(comboBox1.Text);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            width = int.Parse(comboBox1.SelectedItem.ToString());

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

      
    }
}
