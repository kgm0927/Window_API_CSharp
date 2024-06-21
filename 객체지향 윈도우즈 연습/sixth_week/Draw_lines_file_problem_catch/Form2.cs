using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Draw_lines_file_problem_catch
{
    public partial class Form2 : Form
    {
        public int PenWidth { get; set; }
        public Color PenColor { get; set; }
        public int selected_combo_index ;


        public Form2(int choosed)
        {
            InitializeComponent();
            int[] arr = new int[] { 1, 2, 4, 8, 16, 32 };
            for (int i = 0; i < arr.Length; i++)
            {
                comboBox1.Items.Add(arr[i]);
            }
            comboBox1.SelectedIndex = choosed;
            comboBox1.Text = choosed.ToString();
            selected_combo_index=comboBox1.Items.IndexOf(comboBox1.SelectedItem);

            hScrollBar1.Value = PenColor.R;
            hScrollBar2.Value = PenColor.G;
            hScrollBar3.Value = PenColor.B;


            textBox1.Text = hScrollBar1.Value.ToString();
            textBox2.Text = hScrollBar2.Value.ToString();
            textBox3.Text = hScrollBar3.Value.ToString();



        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            PenColor = Color.FromArgb(hScrollBar1.Value, hScrollBar2.Value, hScrollBar3.Value);
            label5.Invalidate();
        }

        private void label5_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawLine(new Pen(PenColor, PenWidth), new Point(0,label5.Height/2), new Point(label5.Right,label5.Height/2));
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            selected_combo_index = comboBox1.Items.IndexOf(comboBox1.SelectedItem);
            label5.Invalidate();
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            PenWidth = int.Parse(comboBox1.Text);
            label5.Invalidate();
        }
    }
}
