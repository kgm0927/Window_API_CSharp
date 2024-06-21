using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Drawline_and_network
{
    public partial class Form2 : Form
    {
        public int iDialogPenWidth;
        public Color DialogPenColor;
        public Form2()
        {
            InitializeComponent();
            hScrollBar1.Minimum = 0;
            hScrollBar1.Maximum = 255;
            hScrollBar2.Minimum = 0;
            hScrollBar2.Maximum = 255;
            hScrollBar3.Minimum = 0;
            hScrollBar3.Maximum = 255;
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            Load_everything(ref comboBox1,ref iDialogPenWidth,ref DialogPenColor,ref hScrollBar1,ref hScrollBar2,ref hScrollBar3,ref textBox1,ref textBox2,ref textBox3);


        }
        private void Load_everything(ref ComboBox combo1,ref int idialogPenwidth,ref Color pencolor,ref HScrollBar scroll1,ref HScrollBar scroll2,ref HScrollBar scroll3,ref TextBox text1,ref TextBox text2,ref TextBox text3)
        {
          int [] arr=new int[]{1,2,4,8,16,32};
            for(int i=0;i<arr.Length;i++){
                comboBox1.Items.Add(arr[i]);
            }

            combo1.Text = idialogPenwidth.ToString();


            scroll1.Value = pencolor.R;
            scroll2.Value = pencolor.G;
            scroll3.Value = pencolor.B;

            text1.Text = scroll3.Value.ToString();
            text2.Text = scroll3.Value.ToString();
            text3.Text = scroll3.Value.ToString();
            // 반드시 HScrollBar의 속성 중에서 LargeChange는 0이어야 한다. 그렇지 않으면 maximum 값은 255가 아닌 246이 된다. (HScrollBar 인스터스의 속성을 확인해 볼 것)

        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            iDialogPenWidth = int.Parse(comboBox1.SelectedItem.ToString());
            label5.Invalidate();
        }

        private void Scrolling(ref Color pencolor,ref HScrollBar scroll1,ref HScrollBar scroll2,ref HScrollBar scroll3,ref TextBox text1,ref TextBox text2,ref TextBox text3,ref Label label){
            DialogPenColor = Color.FromArgb(scroll1.Value, scroll2.Value, scroll3.Value);
            //  DialogPenColor = Color.FromArgb((int)(2.55 * scroll1.Value), (int)(2.55 * scroll2.Value), (int)(2.55 * scroll3.Value)); //만약 스크롤 바의 범위가 0~100일 경우 이 코드를 사용해야 한다.

            text1.Text = scroll1.Value.ToString();
            text2.Text = scroll2.Value.ToString();
            text3.Text = scroll3.Value.ToString();
            label5.Invalidate();
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            Scrolling(ref DialogPenColor, ref hScrollBar1, ref hScrollBar2, ref hScrollBar3, ref textBox1, ref textBox2, ref textBox3, ref label5);
        }

        private void label5_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawLine(new Pen(DialogPenColor, iDialogPenWidth), 0, label5.Height / 2, label5.Width, label5.Height / 2);
        

        }

       
    }
}
