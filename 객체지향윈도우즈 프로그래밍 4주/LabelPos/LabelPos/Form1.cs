using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LabelPos
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 dlg = new Form2();
            dlg.LabelX = label1.Left;   // 좌측상단 꼭지점
            dlg.LabelY = label1.Top;
            dlg.LabelText = label1.Text;// 캡션

            // 현재 등식에 나타나있는 모든 것은 프로퍼티이다.


            if (dlg.ShowDialog() == DialogResult.OK)
            {   // 레이블 컨트롤의 새로운 위치와 캡션 설정 OK를 누른다면
                label1.Left = dlg.LabelX;
                label1.Top = dlg.LabelY;
                label1.Text = dlg.LabelText;
            }
            dlg.Dispose();
        }
    }
}