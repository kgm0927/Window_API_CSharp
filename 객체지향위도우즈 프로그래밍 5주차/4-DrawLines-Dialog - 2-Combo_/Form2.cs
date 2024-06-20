using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawLines
{
    public partial class Form2 : Form
    {
        public Color DialogPenColor { get; set; }
        public int iDialogPenWidth { get; set; }

       
        public Form2()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            iDialogPenWidth = (((ComboBox)sender).SelectedIndex + 1) * 2; // 실제 굵기 값.
            
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
            for (int i = 2; i <= 10; i += 2)
            {
                comboBox1.Items.Add(i);
            }
            comboBox1.Text = iDialogPenWidth.ToString();
         
            //*
            string[] WidthArray = new string[] { "2", "4", "6", "8", "10" }; //comboBox1에 추가할 item 배열
            //comboBox의 item은 string 형이다.
            //comboBox에서 직접 아이템을 넣을 수도 있음.
            for (int i = 0; i < WidthArray.Length; i++)
            {
                comboBox1.Items.Add(WidthArray[i]);
            }

            hScrollBar1.Value = DialogPenColor.R;
            hScrollBar2.Value = DialogPenColor.G;
            hScrollBar3.Value = DialogPenColor.B;

            // */
            textBox1.Text = DialogPenColor.R.ToString();
            textBox2.Text = DialogPenColor.G.ToString();
            textBox3.Text = DialogPenColor.B.ToString();

            // comboBox1.SelectedIndex = iDialogPenWidth / 2 - 1;
            comboBox1.Text = iDialogPenWidth.ToString();
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            iDialogPenWidth = int.Parse(comboBox1.Text);// 바로 숫자로 변환함
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            DialogPenColor= Color.FromArgb(hScrollBar1.Value, 
                hScrollBar2.Value, hScrollBar3.Value);
            textBox1.Text = hScrollBar1.Value.ToString();
            textBox2.Text = hScrollBar2.Value.ToString();
            textBox3.Text = hScrollBar3.Value.ToString();
        }
    }
}
