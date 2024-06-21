using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace ImageAttr
{
	public partial class Form1 : Form
	{
		Image Src = Image.FromFile("오솔길.jpg");
		ImageAttributes Attr = new ImageAttributes();
		float Threshold = 0.5f;
		float Gamma = 1.0f;
		float Bright = 0.0f;

		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.DrawImage(Src, new Rectangle(0, 0, Src.Width, Src.Height),
				0, 0, Src.Width, Src.Height, GraphicsUnit.Pixel, Attr);
		}

		private void Form1_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				// 리셋
				case Keys.Space:
					Attr = new ImageAttributes();
					Src = Image.FromFile("오솔길.jpg");
					Threshold = 0.5f;
					Gamma = 1.0f;
					Bright = 0.0f;
					break;
				// 시계 방향 회전
				case Keys.Right:
					Src.RotateFlip(RotateFlipType.Rotate90FlipNone);
					break;
				// 반시계 방향 회전
				case Keys.Left:
					Src.RotateFlip(RotateFlipType.Rotate270FlipNone);
					break;
				// 수평 뒤집기
				case Keys.Up:
					Src.RotateFlip(RotateFlipType.RotateNoneFlipX);
					break;
				// 수직 뒤집기
				case Keys.Down:
					Src.RotateFlip(RotateFlipType.RotateNoneFlipY);
					break;
				// 스레시 홀드 증감
				case Keys.Q:
					if (Threshold < 1) Threshold += 0.1f;
					Attr.SetThreshold(Threshold);
					break;
				case Keys.W:
					if (Threshold > 0) Threshold -= 0.1f;
					Attr.SetThreshold(Threshold);
					break;
				// 감마 증감
				case Keys.E:
					if (Gamma < 5.0) Gamma += 0.1f;
					Attr.SetGamma(Gamma);
					break;
				case Keys.R:
					if (Gamma > 0.1) Gamma -= 0.1f;
					Attr.SetGamma(Gamma);
					break;
				// 밝게
				case Keys.D1:
					if (Bright < 1.0f) Bright += 0.1f;
					float[][] M1 = {
						new float[] {1.0f, 0.0f, 0.0f, 0.0f, 0.0f},
						new float[] {0.0f, 1.0f, 0.0f, 0.0f, 0.0f},
						new float[] {0.0f, 0.0f, 1.0f, 0.0f, 0.0f},
						new float[] {0.0f, 0.0f, 0.0f, 1.0f, 0.0f},
						new float[] {Bright, Bright, Bright, 0.0f, 1.0f},
					};
					ColorMatrix Mat1 = new ColorMatrix(M1);
					Attr.SetColorMatrix(Mat1);
					break;
				// 어둡게
				case Keys.D2:
					if (Bright > -1.0f) Bright -= 0.1f;
					float[][] M2 = {
						new float[] {1.0f, 0.0f, 0.0f, 0.0f, 0.0f},
						new float[] {0.0f, 1.0f, 0.0f, 0.0f, 0.0f},
						new float[] {0.0f, 0.0f, 1.0f, 0.0f, 0.0f},
						new float[] {0.0f, 0.0f, 0.0f, 1.0f, 0.0f},
						new float[] {Bright, Bright, Bright, 0.0f, 1.0f},
					};
					ColorMatrix Mat2 = new ColorMatrix(M2);
					Attr.SetColorMatrix(Mat2);
					break;
				// 반전
				case Keys.D3:
					float[][] M3 = {
						new float[] {-1.0f, 0.0f, 0.0f, 0.0f, 0.0f},
						new float[] {0.0f, -1.0f, 0.0f, 0.0f, 0.0f},
						new float[] {0.0f, 0.0f, -1.0f, 0.0f, 0.0f},
						new float[] {0.0f, 0.0f, 0.0f, 1.0f, 0.0f},
						new float[] {1.0f, 1.0f, 1.0f, 0.0f, 1.0f},
					};
					ColorMatrix Mat3 = new ColorMatrix(M3);
					Attr.SetColorMatrix(Mat3);
					break;
				// 그레이 스케일
				case Keys.D4:
					float[][] M4 = {
						new float[] {0.299f, 0.299f, 0.299f, 0.0f, 0.0f},
						new float[] {0.587f, 0.587f, 0.587f, 0.0f, 0.0f},
						new float[] {0.114f, 0.114f, 0.114f, 0.0f, 0.0f},
						new float[] {0.0f, 0.0f, 0.0f, 1.0f, 0.0f},
						new float[] {0.0f, 0.0f, 0.0f, 0.0f, 1.0f},
					};
					ColorMatrix Mat4 = new ColorMatrix(M4);
					Attr.SetColorMatrix(Mat4);
					break;
				case Keys.D5:
					ColorMap[] Map = new ColorMap[1];
					Map[0] = new ColorMap();
					Map[0].OldColor = Color.White;
					Map[0].NewColor = Color.Blue;
					Attr.SetRemapTable(Map);
					break;
			}
			Invalidate();
		}
	}
}
