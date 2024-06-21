using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace waBasicConvolution
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem openItem;
		private System.Windows.Forms.MenuItem saveItem;
		private System.Windows.Forms.MenuItem closeItem;
		private System.Windows.Forms.OpenFileDialog ofdOpen;
		private System.Windows.Forms.SaveFileDialog sfdSave;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		Graphics grBm;
		Image image;
		Bitmap bitmap;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem basicItem;
		private System.Windows.Forms.MenuItem blurItem;
		private System.Windows.Forms.MenuItem sharpenItem;
		private System.Windows.Forms.MenuItem sobelItem;
		private System.Windows.Forms.MenuItem sobelYItem;
		private System.Windows.Forms.MenuItem medianItem;
		private System.Windows.Forms.MenuItem maxItem;
		private System.Windows.Forms.MenuItem minItem;
		private System.Windows.Forms.MenuItem averageItem;
		private System.Windows.Forms.MenuItem laplacianItem;
		private System.Windows.Forms.MenuItem laplacian0CrossItem;
		private System.Windows.Forms.MenuItem logItem;
		private System.Windows.Forms.MenuItem log0CrossItem;
		private System.Windows.Forms.MenuItem cannyItem;
		private System.Windows.Forms.MenuItem sobelXItem;
		int [,] grayArray;


		public Form1()
		{
			InitializeComponent();
			setShadowBitmap();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.openItem = new System.Windows.Forms.MenuItem();
			this.saveItem = new System.Windows.Forms.MenuItem();
			this.closeItem = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.basicItem = new System.Windows.Forms.MenuItem();
			this.blurItem = new System.Windows.Forms.MenuItem();
			this.sharpenItem = new System.Windows.Forms.MenuItem();
			this.sobelItem = new System.Windows.Forms.MenuItem();
			this.sobelYItem = new System.Windows.Forms.MenuItem();
			this.sobelXItem = new System.Windows.Forms.MenuItem();
			this.laplacianItem = new System.Windows.Forms.MenuItem();
			this.laplacian0CrossItem = new System.Windows.Forms.MenuItem();
			this.logItem = new System.Windows.Forms.MenuItem();
			this.log0CrossItem = new System.Windows.Forms.MenuItem();
			this.cannyItem = new System.Windows.Forms.MenuItem();
			this.medianItem = new System.Windows.Forms.MenuItem();
			this.maxItem = new System.Windows.Forms.MenuItem();
			this.minItem = new System.Windows.Forms.MenuItem();
			this.averageItem = new System.Windows.Forms.MenuItem();
			this.ofdOpen = new System.Windows.Forms.OpenFileDialog();
			this.sfdSave = new System.Windows.Forms.SaveFileDialog();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem1,
																					  this.menuItem5});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.openItem,
																					  this.saveItem,
																					  this.closeItem});
			this.menuItem1.Text = "&File";
			// 
			// openItem
			// 
			this.openItem.Index = 0;
			this.openItem.Text = "&Open";
			this.openItem.Click += new System.EventHandler(this.openItem_Click);
			// 
			// saveItem
			// 
			this.saveItem.Index = 1;
			this.saveItem.Text = "&Save";
			this.saveItem.Click += new System.EventHandler(this.saveItem_Click);
			// 
			// closeItem
			// 
			this.closeItem.Index = 2;
			this.closeItem.Text = "E&xit";
			this.closeItem.Click += new System.EventHandler(this.closeItem_Click);
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 1;
			this.menuItem5.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.basicItem,
																					  this.blurItem,
																					  this.sharpenItem,
																					  this.sobelItem,
																					  this.sobelYItem,
																					  this.sobelXItem,
																					  this.laplacianItem,
																					  this.laplacian0CrossItem,
																					  this.logItem,
																					  this.log0CrossItem,
																					  this.cannyItem,
																					  this.medianItem,
																					  this.maxItem,
																					  this.minItem,
																					  this.averageItem});
			this.menuItem5.Text = "&Convolve";
			// 
			// basicItem
			// 
			this.basicItem.Index = 0;
			this.basicItem.Text = "Basic";
			this.basicItem.Click += new System.EventHandler(this.basicItem_Click);
			// 
			// blurItem
			// 
			this.blurItem.Index = 1;
			this.blurItem.Text = "Blurring";
			this.blurItem.Click += new System.EventHandler(this.blurItem_Click);
			// 
			// sharpenItem
			// 
			this.sharpenItem.Index = 2;
			this.sharpenItem.Text = "Sharpening";
			this.sharpenItem.Click += new System.EventHandler(this.sharpenItem_Click);
			// 
			// sobelItem
			// 
			this.sobelItem.Index = 3;
			this.sobelItem.Text = "Sobel";
			this.sobelItem.Click += new System.EventHandler(this.sobelItem_Click);
			// 
			// sobelYItem
			// 
			this.sobelYItem.Index = 4;
			this.sobelYItem.Text = "SobelY";
			this.sobelYItem.Click += new System.EventHandler(this.sobelYItem_Click);
			// 
			// sobelXItem
			// 
			this.sobelXItem.Index = 5;
			this.sobelXItem.Text = "SobelX";
			this.sobelXItem.Click += new System.EventHandler(this.sobelXItem_Click);
			// 
			// laplacianItem
			// 
			this.laplacianItem.Index = 6;
			this.laplacianItem.Text = "Laplacian";
			this.laplacianItem.Click += new System.EventHandler(this.laplacianItem_Click);
			// 
			// laplacian0CrossItem
			// 
			this.laplacian0CrossItem.Index = 7;
			this.laplacian0CrossItem.Text = "Laplacian0Cross";
			this.laplacian0CrossItem.Click += new System.EventHandler(this.laplacian0CrossItem_Click);
			// 
			// logItem
			// 
			this.logItem.Index = 8;
			this.logItem.Text = "LoG";
			this.logItem.Click += new System.EventHandler(this.logItem_Click);
			// 
			// log0CrossItem
			// 
			this.log0CrossItem.Index = 9;
			this.log0CrossItem.Text = "Log0Cross";
			this.log0CrossItem.Click += new System.EventHandler(this.log0CrossItem_Click);
			// 
			// cannyItem
			// 
			this.cannyItem.Index = 10;
			this.cannyItem.Text = "Canny Operator";
			this.cannyItem.Click += new System.EventHandler(this.cannyItem_Click);
			// 
			// medianItem
			// 
			this.medianItem.Index = 11;
			this.medianItem.Text = "Median Filter";
			this.medianItem.Click += new System.EventHandler(this.medianItem_Click);
			// 
			// maxItem
			// 
			this.maxItem.Index = 12;
			this.maxItem.Text = "Maximum Filter";
			this.maxItem.Click += new System.EventHandler(this.maxItem_Click);
			// 
			// minItem
			// 
			this.minItem.Index = 13;
			this.minItem.Text = "Minimum Filter";
			this.minItem.Click += new System.EventHandler(this.minItem_Click);
			// 
			// averageItem
			// 
			this.averageItem.Index = 14;
			this.averageItem.Text = "Average Filter";
			this.averageItem.Click += new System.EventHandler(this.averageItem_Click);
			// 
			// sfdSave
			// 
			this.sfdSave.FileName = "doc1";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(464, 337);
			this.Menu = this.mainMenu1;
			this.Name = "Form1";
			this.Text = "회선";
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		void setShadowBitmap()
		{
			bitmap    = new Bitmap(ClientSize.Width, ClientSize.Height);
			grBm      = Graphics.FromImage(bitmap);
			grBm.Clear(BackColor);
		}
		private void openItem_Click(object sender, System.EventArgs e)
		{
			ofdOpen.Title = "Open image";
			ofdOpen.Filter = "All Files(*.*)|*.*|Bitmap File(*.bmp)|GIF file(*.gif)|*.gif|JPEG File(*.jpg)|PNG file(*.png)|*.png|TIFF(*.tif)|*.tif";
			if(ofdOpen.ShowDialog() == DialogResult.OK)
			{
				string strFilename = ofdOpen.FileName;
				image = Image.FromFile(strFilename);
				this.ClientSize = new System.Drawing.Size(image.Width, image.Height);
				setShadowBitmap();
				grBm.DrawImage(image, 0,0, image.Width, image.Height);
				copyBitmap2Array();
			}
			Invalidate();
		}
		void copyBitmap2Array()
		{
			int x,y,brightness;
			int low=255, high=0;
			Color color;
			grayArray = new int[bitmap.Height, bitmap.Width];
			for(y=0; y<bitmap.Height; y++)
				for(x=0; x<bitmap.Width; x++)
				{
					color = bitmap.GetPixel(x,y);
					brightness = (int)(0.299*color.R+0.587*color.G+0.114*color.B);
					if(brightness>high) high = brightness;
					if(brightness<low)  low  = brightness;
					grayArray[y,x] = brightness;
				}
			int[] LUT = new int[256];
			double scale = 255.0 / (high-low);
			for(x=low; x<=high; x++)
				LUT[x] = (byte)((x-low)*scale);
			for(y=0; y<bitmap.Height; y++)
				for(x=0; x<bitmap.Width; x++)
					grayArray[y,x] = LUT[grayArray[y,x]];

		}
		private void Form1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			Graphics gr = e.Graphics;

			gr.DrawImage(bitmap, 0,0);
		}
		private void saveItem_Click(object sender, System.EventArgs e)
		{
			sfdSave.Title = "Save Image as ...";
			sfdSave.OverwritePrompt = true;
			sfdSave.Filter = "Bitmap File(*.bmp)|*.bmp|GIF File(*.gif)|*.gif|JPEG File(*.jpg)|*.jpg|PNG file(*.png)|*.png|TIFF(*.tif)|*.tif";
			if(sfdSave.ShowDialog() == DialogResult.OK)
			{
				string strFilename = sfdSave.FileName;
				string strLowFilename = strFilename.ToLower();
				bitmap.Save(strLowFilename);
			}
		}
		private void closeItem_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		void displayArray()
		{
			int x,y;
			Color color;
			Bitmap gBitmap = new Bitmap(bitmap.Width, bitmap.Height);

			for(y=0; y<bitmap.Height; y++)
				for(x=0; x<bitmap.Width; x++)
				{
					color = Color.FromArgb(grayArray[y,x], grayArray[y,x], grayArray[y,x]);
					gBitmap.SetPixel(x, y, color);
				}
			grBm.DrawImage(gBitmap, 0,0, gBitmap.Width, gBitmap.Height);
			Invalidate();
		}

		void displayResultArray(int[,] resultArray, int Width, int Height)
		{
			int x,y;
			Color color;
			Bitmap gBitmap = new Bitmap(Width, Height);

			for(y=0; y<Height; y++)
				for(x=0; x<Width; x++)
				{
					color = Color.FromArgb(resultArray[y,x], resultArray[y,x], resultArray[y,x]);
					gBitmap.SetPixel(x, y, color);
				}
			grBm.DrawImage(gBitmap, 0,0, gBitmap.Width, gBitmap.Height);
			Invalidate();
		}

		int[,] convolve(int[,] G, int Width, int Height, double[,] M, int maskCol, int maskRow, int biasValue)
		{
			int [,] R = new int[Height,Width];
			int x, y;
			int r, c;
			int xPad = maskCol / 2;
			int yPad = maskRow / 2;
			double sum;

			for(y=0; y<Height-2*yPad; y++)
				for(x=0; x<Width-2*xPad; x++)
				{
					sum = 0.0;
					for(r=0; r<maskRow; r++)
						for(c=0; c<maskCol; c++)
							sum += G[y+r, x+c] * M[r, c];
					sum += biasValue;
					if(sum>255.0) sum = 255.0;
					if(sum<0.0) sum = 0.0;
					R[y+yPad,x+xPad] = (int)sum;
				}
			for(y=0; y<yPad; y++)
			{
				for(x=xPad; x<Width-xPad; x++)
				{
					R[y, x] = R[yPad, x];
					R[Height-1-y,x] = R[Height-1-yPad, x];
				}
			}
			for(x=0; x<xPad; x++)
			{
				for(y=0; y<Height; y++)
				{
					R[y, x] = R[y, xPad];
					R[y, Width-1-x] = R[y, Width-1-xPad];
				}
			}
			return R;
		}

		int[,] convolveEdge(int[,] G, int Width, int Height, double[,] M, int maskCol, int maskRow)
		{
			int [,] R = new int[Height,Width];
			int x, y;
			int r, c;
			int xPad = maskCol / 2;
			int yPad = maskRow / 2;
			double sum;

			for(y=0; y<Height-2*yPad; y++)
				for(x=0; x<Width-2*xPad; x++)
				{
					sum = 0.0;
					for(r=0; r<maskRow; r++)
						for(c=0; c<maskCol; c++)
							sum += G[y+r, x+c] * M[r, c];
					sum = Math.Abs(sum);
					if(sum>255.0) sum = 255.0;
					if(sum<0.0) sum = 0.0;
					R[y+yPad,x+xPad] = (int)sum;
				}
			for(y=0; y<yPad; y++)
			{
				for(x=xPad; x<Width-xPad; x++)
				{
					R[y, x] = R[yPad, x];
					R[Height-1-y,x] = R[Height-1-yPad, x];
				}
			}
			for(x=0; x<xPad; x++)
			{
				for(y=0; y<Height; y++)
				{
					R[y, x] = R[y, xPad];
					R[y, Width-1-x] = R[y, Width-1-xPad];
				}
			}
			return R;
		}
		int[,] convolveXY(int[,] G,int Width,int Height, double[,] M1,double[,] M2,int maskCol,int maskRow)
		{
			int [,] R = new int[Height,Width];
			int x, y;
			int r, c;
			int xPad = maskCol / 2;
			int yPad = maskRow / 2;
			double sum,sum1,sum2;

			for(y=0; y<Height-2*yPad; y++)
				for(x=0; x<Width-2*xPad; x++)
				{
					sum1=sum2 = 0.0;
					for(r=0; r<maskRow; r++)
						for(c=0; c<maskCol; c++)
						{
							sum1 += G[y+r, x+c] * M1[r, c];
							sum2 += G[y+r, x+c] * M2[r, c];
						}
					sum =Math.Abs(sum1)+Math.Abs(sum2);
					if(sum>255.0) sum = 255.0;
					if(sum<0.0) sum = 0.0;
					R[y+yPad,x+xPad] = (int)sum;
				}
			for(y=0; y<yPad; y++)
			{
				for(x=xPad; x<Width-xPad; x++)
				{
					R[y, x] = R[yPad, x];
					R[Height-1-y,x] = R[Height-1-yPad, x];
				}
			}
			for(x=0; x<xPad; x++)
			{
				for(y=0; y<Height; y++)
				{
					R[y, x] = R[y, xPad];
					R[y, Width-1-x] = R[y, Width-1-xPad];
				}
			}
			return R;
		}
		int[,] convolveXY2(int[,] G,int Width,int Height, double[,] M1,double[,] M2,int maskCol,int maskRow, int bias)
		{
			int [,] R = new int[Height,Width];
			int x, y;
			int r, c;
			int xPad = maskCol / 2;
			int yPad = maskRow / 2;
			double sum,sum1,sum2;

			for(y=0; y<Height-2*yPad; y++)
				for(x=0; x<Width-2*xPad; x++)
				{
					sum1=sum2 = 0.0;
					for(r=0; r<maskRow; r++)
						for(c=0; c<maskCol; c++)
						{
							sum1 += G[y+r, x+c] * M1[r, c];
							sum2 += G[y+r, x+c] * M2[r, c];
						}
					sum =sum1+sum2+bias;
					if(sum>255.0) sum = 255.0;
					if(sum<0.0) sum = 0.0;
					R[y+yPad,x+xPad] = (int)sum;
				}
			for(y=0; y<yPad; y++)
			{
				for(x=xPad; x<Width-xPad; x++)
				{
					R[y, x] = R[yPad, x];
					R[Height-1-y,x] = R[Height-1-yPad, x];
				}
			}
			for(x=0; x<xPad; x++)
			{
				for(y=0; y<Height; y++)
				{
					R[y, x] = R[y, xPad];
					R[y, Width-1-x] = R[y, Width-1-xPad];
				}
			}
			return R;
		}

		int[,] convolveLp(int[,] G, int Width, int Height, int maskCol, int maskRow)
		{
			int [,] R = new int[Height,Width];
			int x, y;
			int r, c;
			int xPad = maskCol / 2;
			int yPad = maskRow / 2;
			int[] target = new int[maskRow*maskCol];

			for(y=0; y<Height-2*yPad; y++)
				for(x=0; x<Width-2*xPad; x++)
				{
					int index = 0;
					for(r=0; r<maskRow; r++)
						for(c=0; c<maskCol; c++)
							target[index++]= G[y+r, x+c];
					R[y+yPad,x+xPad] = zeroCrossing(target, index);
				}
			for(y=0; y<yPad; y++)
			{
				for(x=xPad; x<Width-xPad; x++)
				{
					R[y, x] = R[yPad, x];
					R[Height-1-y,x] = R[Height-1-yPad, x];
				}
			}
			for(x=0; x<xPad; x++)
			{
				for(y=0; y<Height; y++)
				{
					R[y, x] = R[y, xPad];
					R[y, Width-1-x] = R[y, Width-1-xPad];
				}
			}
			return R;
		}
		int zeroCrossing(int[] target, int tsize)
		{
			int mid = tsize / 2;
			if(target[mid]*target[1]<0 || target[mid]*target[mid-1]<0) // x,y 방향에서 부호변화가 생기면
			{
					return 255;
			}
			return 0;
		}

		int[,] convolveNoBias(int[,] G, int Width, int Height, double[,] M, int maskCol, int maskRow)
		{
			int [,] R = new int[Height,Width];
			int x, y;
			int r, c;
			int xPad = maskCol / 2;
			int yPad = maskRow / 2;
			double sum;

			for(y=0; y<Height-2*yPad; y++)
				for(x=0; x<Width-2*xPad; x++)
				{
					sum = 0.0;
					for(r=0; r<maskRow; r++)
						for(c=0; c<maskCol; c++)
							sum += G[y+r, x+c] * M[r, c];
					R[y+yPad,x+xPad] = (int)sum;
				}
			for(y=0; y<yPad; y++)
			{
				for(x=xPad; x<Width-xPad; x++)
				{
					R[y, x] = R[yPad, x];
					R[Height-1-y,x] = R[Height-1-yPad, x];
				}
			}
			for(x=0; x<xPad; x++)
			{
				for(y=0; y<Height; y++)
				{
					R[y, x] = R[y, xPad];
					R[y, Width-1-x] = R[y, Width-1-xPad];
				}
			}
			return R;
		}

		int[,] convolveCannyNonMax(double[,] A, double[,] G, int Width, int Height, int maskCol, int maskRow)
		{
			int [,] R = new int[Height,Width];
			int x, y;
			int r, c;
			int xPad = maskCol / 2;
			int yPad = maskRow / 2;
			double[] targetA = new double[maskRow*maskCol];
			double[] targetG = new double[maskRow*maskCol];

			for(y=0; y<Height-2*yPad; y++)
				for(x=0; x<Width-2*xPad; x++)
				{
					int index = 0;
					for(r=0; r<maskRow; r++)
						for(c=0; c<maskCol; c++)
						{
							targetA[index]= A[y+r, x+c];
							targetG[index++]= G[y+r, x+c];
						}
					R[y+yPad,x+xPad] = nonMaxSup(targetA, targetG, index);
				}
			for(y=0; y<yPad; y++)
			{
				for(x=xPad; x<Width-xPad; x++)
				{
					R[y, x] = R[yPad, x];
					R[Height-1-y,x] = R[Height-1-yPad, x];
				}
			}
			for(x=0; x<xPad; x++)
			{
				for(y=0; y<Height; y++)
				{
					R[y, x] = R[y, xPad];
					R[y, Width-1-x] = R[y, Width-1-xPad];
				}
			}
			return R;
		}
		int nonMaxSup(double[] targetA, double[] targetG, int tsize)
		{
			int mid = tsize / 2;
			double iAngle,iAngle2,iAngle3,iAngle4,cAngle;
			double range= Math.PI/8.0; // 오차범위: -23.5도 ~ + 23.5도
			for(int i = 0; i<mid; i++)
			{
				if(targetG[mid] > targetG[i] && targetG[mid] >targetG[tsize-1-i])
				{
					iAngle = Math.PI/4.0*(i+1);
					iAngle2 = Math.PI/4.0*(i+1)+Math.PI;
					iAngle3 = Math.PI/4.0*i - 3.0*Math.PI/4.0 ;
					iAngle4 = Math.PI/4.0*i - 3.0*Math.PI/4.0 -Math.PI;
					cAngle = targetA[mid];
					if((cAngle- range)<iAngle && iAngle<(cAngle+range))
						return (int)targetG[mid];
					else if((cAngle- range)<iAngle2 && iAngle2<(cAngle+range))
						return (int)targetG[mid];
					else if((cAngle- range)<iAngle3 && iAngle3<(cAngle+range))
						return (int)targetG[mid];
					else if((cAngle- range)<iAngle4 && iAngle4<(cAngle+range))
						return (int)targetG[mid];
				}
			}
			return 0;
		}

		int[,] convolveCannyThreshold(int[,] G, int Width, int Height, int maskCol, int maskRow, int lower, int upper)
		{
			int [,] R = new int[Height,Width];
			int x, y;
			int r, c;
			int xPad = maskCol / 2;
			int yPad = maskRow / 2;
			int[] target = new int[maskRow*maskCol];

			for(y=0; y<Height-2*yPad; y++)
				for(x=0; x<Width-2*xPad; x++)
				{
					int index = 0;
					for(r=0; r<maskRow; r++)
						for(c=0; c<maskCol; c++)
							target[index++]= G[y+r, x+c];
					R[y+yPad,x+xPad] = thresholdLimit(target, index, lower, upper);
				}
			for(y=0; y<yPad; y++)
			{
				for(x=xPad; x<Width-xPad; x++)
				{
					R[y, x] = R[yPad, x];
					R[Height-1-y,x] = R[Height-1-yPad, x];
				}
			}
			for(x=0; x<xPad; x++)
			{
				for(y=0; y<Height; y++)
				{
					R[y, x] = R[y, xPad];
					R[y, Width-1-x] = R[y, Width-1-xPad];
				}
			}
			return R;
		}
		int thresholdLimit(int[] target, int tsize, int lower, int upper)
		{
			int mid = tsize / 2;
			if(target[mid] > upper)
				return 255;
			else if(target[mid]<lower)
				return 0;
			else
			{
				for(int i = 0; i<tsize; i++)
				{
					if(target[i] >= target[mid])
						return 255;
				}
			}
			return 0;
		}


		int[,] convolveMedian(int[,] G,int Width,int Height, int winCol,int winRow)
		{
			int [,] R = new int[Height,Width];
			int x, y;
			int r, c;
			int xPad = winCol / 2;
			int yPad = winRow / 2;
			int winSize = winRow*winCol;
			int[] target = new int[winSize];

			for(y=0; y<Height-2*yPad; y++)
				for(x=0; x<Width-2*xPad; x++)
				{
					int index = 0;
					for(r=0; r<winRow; r++)
						for(c=0; c<winCol; c++)
							target[index++] = G[y+r, x+c];
					R[y+yPad,x+xPad] = median(target, winSize);
				}
			for(y=0; y<yPad; y++)
			{
				for(x=xPad; x<Width-xPad; x++)
				{
					R[y, x] = R[yPad, x];
					R[Height-1-y,x] = R[Height-1-yPad, x];
				}
			}
			for(x=0; x<xPad; x++)
			{
				for(y=0; y<Height; y++)
				{
					R[y, x] = R[y, xPad];
					R[y, Width-1-x] = R[y, Width-1-xPad];
				}
			}
			return R;
		}
		int median(int[] target, int tsize)
		{
			int i,j,t;
			for(i=0; i<tsize-1; i++)
			{
				for(j=i+1; j<tsize; j++)
				{
					if(target[i]>target[j])
					{
						t=target[i]; 
						target[i]=target[j]; 
						target[j]=t;
					}
				}
			}
			return (target[tsize/2]);
		}
		int[,] convolveMin(int[,] G,int Width,int Height, int winCol,int winRow)
		{
			int [,] R = new int[Height,Width];
			int x, y;
			int r, c;
			int xPad = winCol / 2;
			int yPad = winRow / 2;
			int winSize = winRow*winCol;
			int[] target = new int[winSize];

			for(y=0; y<Height-2*yPad; y++)
				for(x=0; x<Width-2*xPad; x++)
				{
					int index = 0;
					for(r=0; r<winRow; r++)
						for(c=0; c<winCol; c++)
							target[index++] = G[y+r, x+c];
					R[y+yPad,x+xPad] = minimum(target, winSize);
				}
			for(y=0; y<yPad; y++)
			{
				for(x=xPad; x<Width-xPad; x++)
				{
					R[y, x] = R[yPad, x];
					R[Height-1-y,x] = R[Height-1-yPad, x];
				}
			}
			for(x=0; x<xPad; x++)
			{
				for(y=0; y<Height; y++)
				{
					R[y, x] = R[y, xPad];
					R[y, Width-1-x] = R[y, Width-1-xPad];
				}
			}
			return R;
		}
		int minimum(int[] target, int tsize)
		{
            int min = target[0];
			int i;
			for(i=1; i<tsize; i++)
			{
				if(target[i]<min) min=target[i];

			}
			return min;
		}
		int[,] convolveMax(int[,] G,int Width,int Height, int winCol,int winRow)
		{
			int [,] R = new int[Height,Width];
			int x, y;
			int r, c;
			int xPad = winCol / 2;
			int yPad = winRow / 2;
			int winSize = winRow*winCol;
			int[] target = new int[winSize];

			for(y=0; y<Height-2*yPad; y++)
				for(x=0; x<Width-2*xPad; x++)
				{
					int index = 0;
					for(r=0; r<winRow; r++)
						for(c=0; c<winCol; c++)
							target[index++] = G[y+r, x+c];
					R[y+yPad,x+xPad] = maximum(target, winSize);
				}
			for(y=0; y<yPad; y++)
			{
				for(x=xPad; x<Width-xPad; x++)
				{
					R[y, x] = R[yPad, x];
					R[Height-1-y,x] = R[Height-1-yPad, x];
				}
			}
			for(x=0; x<xPad; x++)
			{
				for(y=0; y<Height; y++)
				{
					R[y, x] = R[y, xPad];
					R[y, Width-1-x] = R[y, Width-1-xPad];
				}
			}
			return R;
		}
		int maximum(int[] target, int tsize)
		{
			int max = target[0];
			int i;
			for(i=1; i<tsize; i++)
			{
				if(target[i]>max) max=target[i];

			}
			return max;
		}

		int[,] convolveAverage(int[,] G,int Width,int Height, int winCol,int winRow)
		{
			int [,] R = new int[Height,Width];
			int x, y;
			int r, c;
			int xPad = winCol / 2;
			int yPad = winRow / 2;
			int winSize = winRow*winCol;
			int[] target = new int[winSize];

			for(y=0; y<Height-2*yPad; y++)
				for(x=0; x<Width-2*xPad; x++)
				{
					int index = 0;
					for(r=0; r<winRow; r++)
						for(c=0; c<winCol; c++)
							target[index++] = G[y+r, x+c];
					R[y+yPad,x+xPad] = average(target, winSize);
				}
			for(y=0; y<yPad; y++)
			{
				for(x=xPad; x<Width-xPad; x++)
				{
					R[y, x] = R[yPad, x];
					R[Height-1-y,x] = R[Height-1-yPad, x];
				}
			}
			for(x=0; x<xPad; x++)
			{
				for(y=0; y<Height; y++)
				{
					R[y, x] = R[y, xPad];
					R[y, Width-1-x] = R[y, Width-1-xPad];
				}
			}
			return R;
		}
		int average(int[] target, int tsize)
		{
			int sum =0;
			int i;
			for(i=1; i<tsize; i++)
			{
				sum += target[i];

			}
			return (int)((double)sum/(double)tsize + 0.5);
		}
		private void basicItem_Click(object sender, System.EventArgs e)
		{
			double [,] mask = {{0.0, -1.0, 0.0}, {-1.0, 0.0, 1.0}, {0.0, 1.0, 0.0}};	//볼록 마스크

			int[,] ResultArray = convolve(grayArray, image.Width, image.Height, mask, 3, 3, 128);
			displayResultArray(ResultArray, image.Width, image.Height);
		}

		private void blurItem_Click(object sender, System.EventArgs e)
		{
			double mVal = 1.0/25.0;
			double [,] mask = {{mVal,mVal,mVal,mVal,mVal},{mVal,mVal,mVal,mVal,mVal},{mVal,mVal,mVal,mVal,mVal},{mVal,mVal,mVal,mVal,mVal},{mVal,mVal,mVal,mVal,mVal}};	

			int[,] ResultArray = convolve(grayArray, image.Width, image.Height, mask, 5, 5, 0);
			displayResultArray(ResultArray, image.Width, image.Height);
		}

		private void sharpenItem_Click(object sender, System.EventArgs e)
		{
			double [,] mask = {{0.0, -1.0, 0.0}, {-1.0, 5.0, -1.0}, {0.0, -1.0, 0.0}};	

			int[,] ResultArray = convolve(grayArray, image.Width, image.Height, mask, 3, 3, 0);
			displayResultArray(ResultArray, image.Width, image.Height);
		}

		private void sobelItem_Click(object sender, System.EventArgs e)
		{
			double [,] mask1 = {{-1.0, 0.0, 1.0}, {-2.0, 0.0, 2.0}, {-1.0, 0.0, 1.0}};	
			double [,] mask2 = {{-1.0, -2.0, -1.0}, {0.0, 0.0, 0.0}, {1.0, 2.0, 1.0}};	

			int[,] ResultArray = convolveXY(grayArray, image.Width, image.Height, mask1,mask2, 3, 3);
			displayResultArray(ResultArray, image.Width, image.Height);
		}

		private void sobelYItem_Click(object sender, System.EventArgs e)
		{
			double [,] mask = {{-1.0, -2.0, -1.0}, {0.0, 0.0, 0.0}, {1.0, 2.0, 1.0}};	

			int[,] ResultArray = convolveEdge(grayArray, image.Width, image.Height, mask, 3, 3);
			displayResultArray(ResultArray, image.Width, image.Height);
		}

		private void sobelXItem_Click(object sender, System.EventArgs e)
		{
			double [,] mask = {{-1.0, 0.0, 1.0}, {-2.0, 0.0, 2.0}, {-1.0, 0.0, 1.0}};	

			int[,] ResultArray = convolveEdge(grayArray, image.Width, image.Height, mask, 3, 3);
			displayResultArray(ResultArray, image.Width, image.Height);
		}

		private void laplacianItem_Click(object sender, System.EventArgs e)
		{
			double [,] mask = {{-1.0, -1.0, -1.0}, {-1.0, 8.0, -1.0}, {-1.0, -1.0, -1.0}};	

			int[,] ResultArray = convolveEdge(grayArray, image.Width, image.Height, mask, 3, 3);
			displayResultArray(ResultArray, image.Width, image.Height);
		}

		private void laplacian0CrossItem_Click(object sender, System.EventArgs e)
		{
			double [,] mask = {{-1.0, -1.0, -1.0}, {-1.0, 8.0, -1.0}, {-1.0, -1.0, -1.0}};	
			double [,] gmask = {{2.0/115.0,  4.0/115.0,  5.0/115.0,  4.0/115.0, 2.0/115.0}, 
								{4.0/115.0,  9.0/115.0, 12.0/115.0,  9.0/115.0, 4.0/115.0},
								{5.0/115.0, 12.0/115.0, 15.0/115.0, 12.0/115.0, 5.0/115.0}, 
								{4.0/115.0,  9.0/115.0, 12.0/115.0,  9.0/115.0, 4.0/115.0},
								{2.0/115.0,  4.0/115.0,  5.0/115.0,  4.0/115.0, 2.0/115.0}};

			int[,] blurArray = convolveEdge(grayArray, image.Width, image.Height, gmask, 5, 5);
			int[,] LaplacianArray = convolveNoBias(blurArray, image.Width, image.Height, mask, 3, 3);

			int[,] ResultArray =  convolveLp(LaplacianArray, image.Width, image.Height, 3, 3);
			displayResultArray(ResultArray, image.Width, image.Height);
		}

		private void logItem_Click(object sender, System.EventArgs e)
		{
			double [,] mask = {
				   {0.0, 0.0, 3.0,   2.0,   2.0,   2.0, 3.0, 0.0, 0.0}, 
				   {0.0, 2.0, 3.0,   5.0,   5.0,   5.0, 3.0, 2.0, 0.0}, 
				   {3.0, 3.0, 5.0,   3.0,   0.0,   3.0, 5.0, 3.0, 3.0}, 
				   {2.0, 5.0, 3.0, -12.0, -23.0, -12.0, 3.0, 5.0, 2.0}, 
				   {2.0, 5.0, 0.0, -23.0, -40.0, -23.0, 0.0, 5.0, 2.0}, 
				   {2.0, 5.0, 3.0, -12.0, -23.0, -12.0, 3.0, 5.0, 2.0}, 
				   {3.0, 3.0, 5.0,   3.0,   0.0,   3.0, 5.0, 3.0, 3.0}, 
				   {0.0, 2.0, 3.0,   5.0,   5.0,   5.0, 3.0, 2.0, 0.0}, 
				   {0.0, 0.0, 3.0,   2.0,   2.0,   2.0, 3.0, 0.0, 0.0}
			};

			int[,] LaplacianArray = convolveNoBias(grayArray, image.Width, image.Height, mask, 9, 9);
			int[,] tLp = new int[image.Height, image.Width];
			int x, y;
			int min = int.MaxValue;
			int max = int.MinValue;
			for(y=0; y<image.Height; y++)
				for(x=0; x<image.Width; x++)
				{
					if(LaplacianArray[y,x] < min) min=LaplacianArray[y,x];
					if(LaplacianArray[y,x] > max) max=LaplacianArray[y,x];
				}
			double scale1=-127.0/min;
			double scale2=127.0/max;
			for(y=0; y<image.Height; y++)
				for(x=0; x<image.Width; x++)
				{
					tLp[y,x] = LaplacianArray[y,x];
					if(tLp[y,x] < 0) tLp[y,x] = (int)(scale1*tLp[y,x])+127;
					else if (tLp[y,x] == 0) tLp[y,x] = 128;
					else tLp[y,x] = (int)(scale2*tLp[y,x])+128;
				}
			displayResultArray(tLp, image.Width, image.Height);
		}

		private void log0CrossItem_Click(object sender, System.EventArgs e)
		{
			double [,] mask = {
				   {0.0, 0.0, 3.0,   2.0,   2.0,   2.0, 3.0, 0.0, 0.0}, 
				   {0.0, 2.0, 3.0,   5.0,   5.0,   5.0, 3.0, 2.0, 0.0}, 
				   {3.0, 3.0, 5.0,   3.0,   0.0,   3.0, 5.0, 3.0, 3.0}, 
				   {2.0, 5.0, 3.0, -12.0, -23.0, -12.0, 3.0, 5.0, 2.0}, 
				   {2.0, 5.0, 0.0, -23.0, -40.0, -23.0, 0.0, 5.0, 2.0}, 
				   {2.0, 5.0, 3.0, -12.0, -23.0, -12.0, 3.0, 5.0, 2.0}, 
				   {3.0, 3.0, 5.0,   3.0,   0.0,   3.0, 5.0, 3.0, 3.0}, 
				   {0.0, 2.0, 3.0,   5.0,   5.0,   5.0, 3.0, 2.0, 0.0}, 
				   {0.0, 0.0, 3.0,   2.0,   2.0,   2.0, 3.0, 0.0, 0.0}
			};

			int[,] LaplacianArray = convolveNoBias(grayArray, image.Width, image.Height, mask, 9, 9);
			int[,] ResultArray =  convolveLp(LaplacianArray, image.Width, image.Height, 3, 3);
			displayResultArray(ResultArray, image.Width, image.Height);
		}

		private void cannyItem_Click(object sender, System.EventArgs e)
		{
			double [,] mask = { {1.0/273.0,  4.0/273.0,  7.0/273.0,  4.0/273.0, 1.0/273.0}, 
								{4.0/273.0, 16.0/273.0, 16.0/273.0, 16.0/273.0, 4.0/273.0},
								{7.0/273.0, 26.0/273.0, 41.0/273.0, 26.0/273.0, 7.0/273.0}, 
								{4.0/273.0, 16.0/273.0, 16.0/273.0, 16.0/273.0, 4.0/273.0},
								{1.0/273.0,  4.0/273.0,  7.0/273.0,  4.0/273.0, 1.0/273.0}};	
			double [,] mask1 = {{-1.0, 0.0, 1.0}, {-2.0, 0.0, 2.0}, {-1.0, 0.0, 1.0}};	
			double [,] mask2 = {{-1.0, -2.0, -1.0}, {0.0, 0.0, 0.0}, {1.0, 2.0, 1.0}};	

			int[,] GaussArray = convolveEdge(grayArray, image.Width, image.Height, mask, 5, 5);
			int[,] SobelXArray = convolveNoBias(GaussArray, image.Width, image.Height, mask1, 3, 3);
			int[,] SobelYArray = convolveNoBias(GaussArray, image.Width, image.Height, mask2, 3, 3);
			int[,] tLp = new int[image.Height, image.Width];
			double[,] angleArray = new double[image.Height, image.Width];
			double[,] magnitudeArray = new double[image.Height, image.Width];
			int x, y;
			for(y=0; y<image.Height; y++)
				for(x=0; x<image.Width; x++)
				{
					angleArray[y,x] = Math.Atan2(SobelYArray[y,x],SobelXArray[y,x]);
					magnitudeArray[y,x] = Math.Sqrt(SobelXArray[y,x]*SobelXArray[y,x]+SobelYArray[y,x]*SobelYArray[y,x]);
					tLp[y,x] = (int)magnitudeArray[y,x];
					//if(tLp[y,x]>255) tLp[y,x]= 255;
				}
			//displayResultArray(tLp, image.Width, image.Height);
			//MessageBox.Show("잠깐");
			int[,] NonMaxArray = convolveCannyNonMax(angleArray, magnitudeArray, image.Width, image.Height, 3, 3);
			//displayResultArray(NonMaxArray, image.Width, image.Height);
			//MessageBox.Show("잠깐");
			//int[,] ThresholdArray = convolveCannyThreshold(NonMaxArray, image.Width, image.Height, 3, 3, 50, 200);
			int[,] ThresholdArray = convolveCannyThreshold(NonMaxArray, image.Width, image.Height, 3, 3, 30, 200);
			displayResultArray(ThresholdArray, image.Width, image.Height);
		}

		private void medianItem_Click(object sender, System.EventArgs e)
		{
			int[,] ResultArray = convolveMedian(grayArray, image.Width, image.Height, 3, 3);
			displayResultArray(ResultArray, image.Width, image.Height);
		}

		private void maxItem_Click(object sender, System.EventArgs e)
		{
			int[,] ResultArray = convolveMax(grayArray, image.Width, image.Height, 3, 3);
			displayResultArray(ResultArray, image.Width, image.Height);
		}

		private void minItem_Click(object sender, System.EventArgs e)
		{
			int[,] ResultArray = convolveMin(grayArray, image.Width, image.Height, 3, 3);
			displayResultArray(ResultArray, image.Width, image.Height);
		}

		private void averageItem_Click(object sender, System.EventArgs e)
		{
			int[,] ResultArray = convolveAverage(grayArray, image.Width, image.Height, 3, 3);
			displayResultArray(ResultArray, image.Width, image.Height);
		}
	}
}
