using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 김경민_20172032
{
    public partial class Form3 : Form
    {
        public int Currentblue;
        public int Currentgreen;
        public int Currentred;
        public int Currentbright;
       public Bitmap bp;
       public Bitmap bp_2;

        public Form2 f2;
        private int[,] smoothing;
        private int[,] edge;




        public Form3(Image img,int r,int g,int b,int gri,Form2 f2)
        {
            InitializeComponent();

        bp = new Bitmap(img);       // 최초로 생성된 이미지 저장
        bp_2 = new Bitmap(img);     // 변화된 이미지를 저장
        label5.Image = this.bp;


        smoothing = new int[,] { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } };
        edge = new int[,] { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };
        Currentred = r;
            Currentblue = b;
            Currentgreen = g;
           
            Currentbright = gri;
            
            hScrollBar4.Value = gri;

            hScrollBar1.Value = r;
            hScrollBar2.Value = g;
            hScrollBar3.Value = b;

            hScrollBar1.LargeChange = 1;
            hScrollBar2.LargeChange = 1;
            hScrollBar3.LargeChange = 1;
            hScrollBar4.LargeChange = 1;

            textBox1.Text = r.ToString();
            textBox2.Text = g.ToString();
            textBox3.Text = b.ToString();


            this.f2 = f2;


        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

            textBox1.Text = hScrollBar1.Value.ToString();
            textBox2.Text = hScrollBar2.Value.ToString();
            textBox3.Text = hScrollBar3.Value.ToString();

            Currentbright = hScrollBar4.Value;
            Currentred = hScrollBar1.Value ;
            Currentgreen = hScrollBar2.Value ;
            Currentblue = hScrollBar3.Value;


            Graphics g = CreateGraphics();
          

            //change_rgb_while_scolling();
              changing_bright();
            change_rgb();
        }


        // 만약 스크롤 바가 움직이는 도중에 그림을 바꾸려면 hScrollBar1_Scroll 함수 안에 이 함수를 추가하면 된다.

        private void change_rgb_while_scolling()
        {
            f2.CurrentR = Currentred;
            f2.CurrentG = Currentgreen;
            f2.CurrentB = Currentblue;
            f2.Currentbrighter = Currentbright;
      
            f2.Invalidate();
           


        }


        private void change_Color_by_typing(KeyEventArgs e, TextBox tb, HScrollBar sb,ref int CurrentColor)
        {
            if (e.KeyData == Keys.Enter)
            {
                sb.Value = int.Parse(tb.Text);
                CurrentColor = sb.Value;
                change_rgb_while_scolling();
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            change_Color_by_typing(e, textBox1, hScrollBar1,ref Currentred);
            
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            change_Color_by_typing(e, textBox2, hScrollBar2,ref Currentgreen);

          

        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            change_Color_by_typing(e, textBox3, hScrollBar3,ref Currentblue);



        }

        private void label5_Paint(object sender, PaintEventArgs e)
        {
         
            
            
        }
        
        private void change_rgb() // 색깔 삼원색 조종
        {
            Bitmap bp = new Bitmap(this.bp_2);
            for (int y = 0; y < bp.Height; y++)
            {
                for (int x = 0; x < bp.Width; x++)
                {
                    Color color = bp.GetPixel(x, y);

                    int newRed = Math.Min(255, Math.Max(0, color.R - (255 - Currentred)));
                    int newGreen = Math.Min(255, Math.Max(0, color.G - (255 - Currentgreen )));
                    int newBlue = Math.Min(255, Math.Max(0, color.B - (255 - Currentblue )));
                    Color newColor = Color.FromArgb(newRed, newGreen, newBlue);



                    bp.SetPixel(x, y, newColor);


                }

            }


          
            
            this.label5.Image = bp;
            this.label5.Invalidate();
        }
        private void change_rgb( Bitmap bp_2)
        {
          
            for (int y = 0; y < bp_2.Height; y++)
            {
                for (int x = 0; x < bp_2.Width; x++)
                {
                    Color color = bp.GetPixel(x, y);

                    int newRed = Math.Min(255, Math.Max(0, color.R - (255 - Currentred)));
                    int newGreen = Math.Min(255, Math.Max(0, color.G - (255 - Currentgreen)));
                    int newBlue = Math.Min(255, Math.Max(0, color.B - (255 - Currentblue)));
                    Color newColor = Color.FromArgb(newRed, newGreen, newBlue);



                    bp_2.SetPixel(x, y, newColor);


                }
            }

            this.label5.Image = bp;
            this.label5.Invalidate();

        }


        private void changing_bright()// 밝기 조종 //현 단계에서는 사용할 방도가 없으나 함수를 바꾸면 사용이 가능함.
        {
     
            Bitmap bp = new Bitmap(this.bp_2);
            for (int y = 0; y < bp.Height; y++)
            {
                for (int x = 0; x < bp.Width; x++)
                {
                    Color color = bp.GetPixel(x, y);

                    int newRed = Math.Min(255, Math.Max(0, color.R - (255 - Currentbright)));
                    int newGreen = Math.Min(255, Math.Max(0, color.G - (255 - Currentbright)));
                    int newBlue = Math.Min(255, Math.Max(0, color.B - (255 - Currentbright)));
                    Color newColor = Color.FromArgb(newRed, newGreen, newBlue);



                    bp.SetPixel(x, y, newColor);


                }

            }

            



            this.label5.Image = bp;
            this.label5.Invalidate();

        }



        private void button3_Click(object sender, EventArgs e)
        {
            make_smoothing();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            make_edging();
        }


        private void makeing_gray(Label lbl, Bitmap bp)     // 먼저 회색으로 만듦
        {

            if (lbl != null)
            {


                if (bp.PixelFormat.ToString() != "Format8bppIndexed")  // 컬러 영상일 때
                {
                    for (int x = 1; x < bp.Width; x++)
                    {
                        for (int y = 1; y < bp.Height; y++)
                        {
                            int color = bp.GetPixel(x, y).R + bp.GetPixel(x, y).G + bp.GetPixel(x, y).B;
                            color /= 3;

                            if (color > 255) color = 255;
                            if (color < 0) color = 0;

                            Color c = Color.FromArgb(color, color, color);
                            bp.SetPixel(x, y, c);
                        }
                    }
                }
            }


        }


        //

        private void finally_smoothing(Bitmap bp, Bitmap bp2, int[,] m)// smoothing 실행
        {

            int sum;

            for (int x = 1; x < bp.Width - 1; x++)
            {
                for (int y = 1; y < bp.Height - 1; y++)
                {
                    sum = 0;
                    for (int r = -1; r < 2; r++)
                    {
                        for (int c = -1; c < 2; c++)
                        {
                            sum += m[r + 1, c + 1] * bp.GetPixel(x + r, y + c).R;
                        }
                    }
                    sum = Math.Abs(sum);
                    sum /= 9;
                    if (sum > 255) sum = 255;
                    if (sum < 0) sum = 0;
                    bp2.SetPixel(x, y, Color.FromArgb(sum, sum, sum));

                }
            }

        }

        private void finally_edging(Bitmap bp, Bitmap bp2, Form2 fm2, int[,] m)
        {
            int sum;
            for (int x = 1; x < bp.Width - 1; x++)
            {
                for (int y = 1; y < bp.Height - 1; y++)
                {
                    sum = 0;
                    for (int r = -1; r < 2; r++)
                    {
                        for (int c = -1; c < 2; c++)
                        {

                            sum += m[r + 1, c + 1] * bp.GetPixel(x + r, y + c).R;

                        }
                    }
                    sum = Math.Abs(sum);
                    if (sum > 255) sum = 255;
                    //if (sum < 0) sum = 0;
                    bp2.SetPixel(x, y, Color.FromArgb(sum, sum, sum));
                }

            }
        }


        private void make_smoothing()
        {
            Bitmap bp = new Bitmap(this.bp_2);
          

            makeing_gray(label5,bp);        // 우선 회색으로 실행 

            Bitmap bp2 = new Bitmap(bp);

            finally_smoothing(bp, bp2,  smoothing);// smoothing 실행
            this.label5.Image = bp2;
            this.bp_2 = bp2;
            this.f2.img = this.bp_2;
            this.f2.Invalidate();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.label5.Image = f2.original_img;
            this.bp_2 = new Bitmap(f2.original_img);
            f2.img = f2.original_img;

            hScrollBar1.Value = 255;
            hScrollBar2.Value = 255;
            hScrollBar3.Value = 255;
            textBox1.Text = hScrollBar1.Value.ToString();
            textBox2.Text = hScrollBar2.Value.ToString();
            textBox3.Text = hScrollBar3.Value.ToString();

            f2.Invalidate();
        }


        private void finally_edging(Bitmap bp, Bitmap bp2,  int[,] m)
        {
            int sum;
            for (int x = 1; x < bp.Width - 1; x++)
            {
                for (int y = 1; y < bp.Height - 1; y++)
                {
                    sum = 0;
                    for (int r = -1; r < 2; r++)
                    {
                        for (int c = -1; c < 2; c++)
                        {

                            sum += m[r + 1, c + 1] * bp.GetPixel(x + r, y + c).R;

                        }
                    }
                    sum = Math.Abs(sum);
                    if (sum > 255) sum = 255;
                    //if (sum < 0) sum = 0;
                    bp2.SetPixel(x, y, Color.FromArgb(sum, sum, sum));
                }

            }
        }
        private void make_edging()
        {
            Bitmap bp = new Bitmap(this.bp_2);


            makeing_gray(label5, bp);           // 먼저 회색으로 변환

            Bitmap bp2 = new Bitmap(bp);

            finally_edging(bp, bp2, edge);      // edge 실행
            this.label5.Image = bp2;
            this.bp_2 = bp2;
            this.f2.img = this.bp_2;
            this.f2.Invalidate();

        }
    }
    

    }

