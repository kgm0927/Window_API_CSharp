using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace 김경민_20172032
{
    public partial class Form1 : Form
    {
        private int[,] smoothing;
        private int[,] edge;



        public Form1()
        {
            InitializeComponent();
            smoothing = new int[,] { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } };
            edge = new int[,] { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };
        }

        private void 열기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "PNG 파일|*.png|JPEG 파일|*.jpeg|BMP 파일|*.bmp|모든 파일|*.*"; ;
            open_the_image_file(op);
        }
        private void open_the_image_file(OpenFileDialog op)
        {
            if (op.ShowDialog() == DialogResult.OK)
            {
                Bitmap I = new Bitmap(op.FileName);
                Image image = Image.FromFile(op.FileName);

                Bitmap bitmap = (Bitmap)image;

                Image image2 = (Image)bitmap;


                Form2 child = new Form2();
                child.img = image2;
                child.original_img=image2;
                child.MdiParent = this;
                child.Show();
            }
        }


        private void open_file_image()
        {
            OpenFileDialog op = new OpenFileDialog();

            if (op.ShowDialog() == DialogResult.OK)
            {
                object I = new Bitmap(op.FileName);

                Form2 child = new Form2();
                child.img = (Bitmap)I;
                child.original_img = (Bitmap)I;
                child.MdiParent = this;
                child.Show();
            }
        }

        private void brighter(Image img, ref Bitmap bp, ref Color color)
        {
            bp = new Bitmap(img);

            for (int y = 0; y < bp.Height; y++)
            {
                for (int x = 0; x < bp.Width; x++)
                {
                    color = bp.GetPixel(x, y);

                    int r = color.R;
                    int g = color.G;
                    int b = color.B;
                    r = r + 50 > 255 ? 255 : r + 50;
                    g = g + 50 > 255 ? 255 : g + 50;
                    b = b + 50 > 255 ? 255 : b + 50;
                    bp.SetPixel(x, y, Color.FromArgb(r, g, b));


                }
            }
        }

        private void darker(Image img, ref Bitmap bp, ref Color color)
        {
            bp = new Bitmap(img);
            for (int y = 0; y < bp.Height; y++)
            {
                for (int x = 0; x < bp.Width; x++)
                {
                    color = bp.GetPixel(x, y);

                    int r = color.R;
                    int g = color.G;
                    int b = color.B;

                    r = r - 50 < 0 ? 0 : r - 50;
                    g = g - 50 < 0 ? 0 : g - 50;
                    b = b - 50 < 0 ? 0 : b - 50;

                    bp.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }
        }

        private void coloring(int act, ref Form2 Child)
        {
            // 만약 1이면 밝아지게, -1이면 어둡게
            Image img = Child.img;
            Bitmap bp = new Bitmap(img);
            Color col = new Color();
            if (Child != null)
            {


                if (act == 1)
                {
                    brighter(img, ref bp, ref col);
                }
                else if (act == -1)
                {
                    darker(img, ref bp, ref col);
                }
            }
            Child = new Form2();
            Child.img = bp;
            Child.MdiParent = this;
            Child.Show();

        }

        private void 밝게하기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 child = (Form2)this.ActiveMdiChild;
            coloring(1, ref child);
        }

        private void 어둡게하기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 child = (Form2)this.ActiveMdiChild;
            coloring(-1, ref child);
        }

        private void 대화상자ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 f2 = ((Form2)this.ActiveMdiChild);

            Form3 dlg = new Form3(f2.img,f2.CurrentR,f2.CurrentG,f2.CurrentB,f2.Currentbrighter,f2);
           

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                f2.CurrentR = dlg.Currentred;
                f2.CurrentG = dlg.Currentgreen;
                f2.CurrentB = dlg.Currentblue;
                f2.Currentbrighter = dlg.Currentbright;
                f2.img = dlg.bp_2;
                f2.Invalidate();
            }
            // 아예 대화상자로 내용을 바꿀 때마다 새로운 창을 만들어야 하는 것인가.
            // 아니면 계속 썼던 대로 해야 하는 것인가?
            
         
            
        }





        private void 저장ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            how_to_save();
        }


        private void how_to_save()
        {
            Form2 f2 = (Form2)this.ActiveMdiChild;
            // SaveFileDialog 객체 생성
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            // 저장할 파일의 기본 제목 설정
            saveFileDialog.FileName = "image.png";

            // 저장할 파일의 초기 디렉토리 설정 (선택사항)
            saveFileDialog.InitialDirectory = @"C:\temp";

            // 파일 필터 설정 - 이미지 파일 형식만 허용
            saveFileDialog.Filter = "PNG 파일|*.png|JPEG 파일|*.jpeg|BMP 파일|*.bmp|모든 파일|*.*";

            // 사용자가 대화 상자에서 저장을 클릭했는지 확인
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // 사용자가 선택한 파일 경로 가져오기
                string filePath = saveFileDialog.FileName;
                            
                try
                {
                    // 이미지를 파일로 저장

             

                    MessageBox.Show("파일이 성공적으로 저장되었습니다.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("파일을 저장하는 도중 오류가 발생했습니다: " + ex.Message);
                }
            }
        }

    


    }

    
}
