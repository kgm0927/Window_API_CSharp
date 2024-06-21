using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sixth_week
{
    public partial class Form2 : Form

    {

        public int iDialogPenWidth { get; set; }
        public Color DialogPenColor { get; set; }


        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            int[] arr = new  int [] { 1, 2, 4, 8, 16, 32 };
            for (int i = 0; i < arr.Length; i++)
            {
                comboBox1.Items.Add(arr[i]);
            }
            comboBox1.Text = iDialogPenWidth.ToString();

            hScrollBar1.Value = DialogPenColor.R;
            hScrollBar2.Value = DialogPenColor.G;
            hScrollBar3.Value = DialogPenColor.B;

            textBox1.Text = hScrollBar1.Value.ToString();
            textBox2.Text = hScrollBar2.Value.ToString();
            textBox3.Text = hScrollBar3.Value.ToString();



        }

        private void label5_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawLine(new Pen(DialogPenColor, iDialogPenWidth), 0, label5.Height / 2, label5.Width, label5.Height / 2);
        
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            DialogPenColor = Color.FromArgb(hScrollBar1.Value, hScrollBar2.Value, hScrollBar3.Value);
            textBox1.Text = hScrollBar1.Value.ToString();
            textBox2.Text = hScrollBar2.Value.ToString();
            textBox3.Text = hScrollBar3.Value.ToString();
            label5.Invalidate();
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            iDialogPenWidth = int.Parse(comboBox1.Text);
            label5.Invalidate();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            iDialogPenWidth = int.Parse(comboBox1.SelectedItem.ToString());
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            change_Color_by_typing(e, textBox1, hScrollBar1);
        }

        private void change_Color_by_typing(KeyEventArgs e,TextBox tb,HScrollBar sb){
            if (e.KeyData == Keys.Enter)
            {
                sb.Value = int.Parse(tb.Text);
                DialogPenColor = Color.FromArgb(hScrollBar1.Value, hScrollBar2.Value, hScrollBar3.Value);
                label5.Invalidate();
            }
        }
    }
}
