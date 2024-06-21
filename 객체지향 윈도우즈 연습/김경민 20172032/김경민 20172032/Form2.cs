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
    public partial class Form2 : Form
    {
        public Image img;           // 화면에 나타날 사진을 저장함.
        public Image original_img;  // 최초로 생성된 사진 값을 저장함
        Bitmap copy_b;

        public int Currentbrighter;
        public int CurrentR;
        public int CurrentG;
        public int CurrentB;



        public Form2()
        {
            InitializeComponent();
            CurrentB = 255;
            CurrentG = 255;
            CurrentR = 255;
            Currentbrighter = 255;
        }

        

        private void Form2_Load(object sender, EventArgs e)
        {
            this.ClientSize = img.Size;

        }

        private void Form2_Paint(object sender, PaintEventArgs e)
        {


           // change_rgb(e);
           // change_rgb(e);

            changing_bright(e);

       //     change_rgb(e);
           
        }

        private void change_rgb(PaintEventArgs e)
        {
            copy_b = new Bitmap(img);
            Bitmap bp = new Bitmap(img);
            for (int y = 0; y < bp.Height; y++)
            {
                for (int x = 0; x < bp.Width; x++)
                {
                    Color color = bp.GetPixel(x, y);

                    int newRed = Math.Min(255, Math.Max(0, color.R - (255 - CurrentR)));
                    int newGreen = Math.Min(255, Math.Max(0, color.G - (255 - CurrentG)));
                    int newBlue = Math.Min(255, Math.Max(0, color.B - (255 - CurrentB)));
                    Color newColor = Color.FromArgb(newRed, newGreen, newBlue);



                    bp.SetPixel(x, y, newColor);


                }

            }

            e.Graphics.DrawImage(bp, 0, 0, img.Width, img.Height);
        }


        private void changing_bright(PaintEventArgs e)
        {
     
            Bitmap bp = new Bitmap(img);
            for (int y = 0; y < bp.Height; y++)
            {
                for (int x = 0; x < bp.Width; x++)
                {
                    Color color = bp.GetPixel(x, y);

                    int newRed = Math.Min(255, Math.Max(0, color.R - (255 - Currentbrighter)));
                    int newGreen = Math.Min(255, Math.Max(0, color.G - (255 - Currentbrighter)));
                    int newBlue = Math.Min(255, Math.Max(0, color.B - (255 - Currentbrighter)));
                    Color newColor = Color.FromArgb(newRed, newGreen, newBlue);



                    bp.SetPixel(x, y, newColor);


                }

            }

            e.Graphics.DrawImage(bp, 0, 0, img.Width, img.Height);
        }

        
    }



}
