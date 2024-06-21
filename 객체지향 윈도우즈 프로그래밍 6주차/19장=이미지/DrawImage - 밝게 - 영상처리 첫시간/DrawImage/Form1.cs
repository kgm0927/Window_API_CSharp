using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DrawImage
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Paint(object sender, PaintEventArgs e)
		{
			Image I = Image.FromFile("아기.jpg");
            Bitmap B = new Bitmap(I);
            for(int y=0; y<B.Height; y++)
                for (int x = 0; x<B.Width; x++)
                {
                    Color color = B.GetPixel(x,y);
                    
                    //B.GetPixel(x, y).R = (byte)(B.GetPixel(x, y).R + 50);  
                    //Error. field 직접접근 불가
                   /* //*/ //Wrap 
                   // int r = color.R + 50; 
                  //  int g = color.G + 50; 
                  //  int b = color.B + 50; 
                    //B.SetPixel(x,y, Color.FromArgb(r, g, b)); //Error
                  //  B.SetPixel(x,y, Color.FromArgb((byte)r, (byte)g, (byte)b)); // 밝은 곳에서는 스택 오버플로우가 일어남.
                    //*/
                    
                    //* // Saturation
                    int r = (int)(color.R + 50); if (r > 255) r = 255;
                    int g = color.G + 50; if (g > 255) g = 255;
                    int b = color.B + 50; if (b > 255) b = 255;
                    B.SetPixel(x, y, Color.FromArgb(r, g, b));
                    //*/
                   
                }
             
            e.Graphics.DrawImage(B, 0, 0);
			
		}
	}
}
