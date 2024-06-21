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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        public Image image { get; set; }

        //번개질
        private void Form2_Load(object sender, EventArgs e)
        {
            //this.ClientSize = new Size(image.Width, image.Height);
            //그림크기와 동일한 윈도우 크기 지정
            this.ClientSize = image.Size;
        }

        //번개질
        private void Form2_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(image, 0, 0, image.Width, image.Height);
           // e.Graphics.DrawImage(image, 0, 0); // 이렇게도 가능하다.
            //그림의 크기도 인자로 넘김. 그림크기에 맞추어 윈도우 열기 
        }
    }
}
