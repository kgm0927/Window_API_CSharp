using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Draw_lines
{
    public partial class Form2 : Form
    {

        private Color DialogPenColor;
        private int SizePen;
        public int combo_index;

        public Color PenCol
        {
            get { return DialogPenColor; }
            set { DialogPenColor = value; }
        }

        public int SIZE
        {
            get { return SizePen; }
            set { SizePen = value; }
        }


        public Color Color
        {
            get
            {
                if (radioButton1.Checked) DialogPenColor = Color.Red;
                if (radioButton2.Checked) DialogPenColor = Color.Green;
                if (radioButton3.Checked) DialogPenColor = Color.Blue;
                return DialogPenColor;
            }
            set
            {
                DialogPenColor = value;
                if (DialogPenColor == Color.Red) radioButton1.Checked = true;
                if (DialogPenColor == Color.Green) radioButton2.Checked = true;
                if (DialogPenColor == Color.Blue) radioButton3.Checked = true;

            }
        }



        public Form2(int num)
        {
            InitializeComponent();
            comboBox1.Items.Add("1");
            comboBox1.Items.Add("2");
            comboBox1.Items.Add("4");
            comboBox1.Items.Add("8");
            comboBox1.Items.Add("16");
            comboBox1.Items.Add("32");
            comboBox1.SelectedIndex = num;


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Text = Convert.ToString(comboBox1.SelectedIndex);
            SIZE = int.Parse(comboBox1.Text);
            combo_index = comboBox1.SelectedIndex;
            
        }
    }
}
