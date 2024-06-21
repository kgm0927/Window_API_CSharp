using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace waHistogramProcess
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
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.MenuItem strectchingItem;
		private System.Windows.Forms.MenuItem stretching2Item;
		private System.Windows.Forms.MenuItem equalizationItem;
		private System.Windows.Forms.MenuItem specificationItem;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		Graphics grBm;
		Image image;
		Bitmap bitmap;
		int [,] grayArray;
		const int HISTO_WIDTH  = 256;
		const int HISTO_HEIGHT = 240;


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
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.strectchingItem = new System.Windows.Forms.MenuItem();
			this.stretching2Item = new System.Windows.Forms.MenuItem();
			this.equalizationItem = new System.Windows.Forms.MenuItem();
			this.specificationItem = new System.Windows.Forms.MenuItem();
			this.ofdOpen = new System.Windows.Forms.OpenFileDialog();
			this.sfdSave = new System.Windows.Forms.SaveFileDialog();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem1,
																					  this.menuItem6});
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
			// menuItem6
			// 
			this.menuItem6.Index = 1;
			this.menuItem6.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.strectchingItem,
																					  this.stretching2Item,
																					  this.equalizationItem,
																					  this.specificationItem});
			this.menuItem6.Text = "Histogram";
			// 
			// strectchingItem
			// 
			this.strectchingItem.Index = 0;
			this.strectchingItem.Text = "Stretching";
			this.strectchingItem.Click += new System.EventHandler(this.strectchingItem_Click);
			// 
			// stretching2Item
			// 
			this.stretching2Item.Index = 1;
			this.stretching2Item.Text = "Strectching2";
			this.stretching2Item.Click += new System.EventHandler(this.stretching2Item_Click);
			// 
			// equalizationItem
			// 
			this.equalizationItem.Index = 2;
			this.equalizationItem.Text = "Equalization";
			this.equalizationItem.Click += new System.EventHandler(this.equalizationItem_Click);
			// 
			// specificationItem
			// 
			this.specificationItem.Index = 3;
			this.specificationItem.Text = "Specification";
			this.specificationItem.Click += new System.EventHandler(this.specificationItem_Click);
			// 
			// sfdSave
			// 
			this.sfdSave.FileName = "doc1";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(504, 496);
			this.Menu = this.mainMenu1;
			this.Name = "Form1";
			this.Text = "히스토그램을 이용한 영상처리";
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
				this.ClientSize = new System.Drawing.Size(image.Width+HISTO_WIDTH+10,
								       (image.Height>HISTO_HEIGHT)?2*image.Height+10:2*HISTO_HEIGHT+10);
				setShadowBitmap();
				grBm.DrawImage(image, 0,0, image.Width, image.Height);
				copyBitmap2Array();
				viewHistogram(image.Width+10, 0, grayArray);
			}
			Invalidate();
		}
		void copyBitmap2Array()
		{
			int x,y,brightness;
			Color color;
			grayArray = new int[bitmap.Height, bitmap.Width];
			for(y=0; y<bitmap.Height; y++)
				for(x=0; x<bitmap.Width; x++)
				{
					color = bitmap.GetPixel(x,y);
					brightness = (int)(255.0*color.GetBrightness());
					grayArray[y,x] = brightness;
				}
		}
		private void Form1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			Graphics gr = e.Graphics;

			gr.DrawImage(bitmap, 0,0);
		}
		void viewHistogram(int leftTopX, int leftTopY, int[,] histoArray)
		{
			int x,y;
			Color color;
			Bitmap histoBitmap = new Bitmap(HISTO_WIDTH, HISTO_HEIGHT);
			int[] histogram = new int[256];
			histogram.Initialize();
			for (y=0; y<image.Height; y++)  // 각 픽셀의 종류별 count
				for (x=0; x<image.Width; x++) 
					histogram[histoArray[y,x]]++;  
			int max_cnt=0;   // 가장 많은 픽셀
			for(x=0; x<HISTO_WIDTH; x++)
				if(histogram[x]>max_cnt) max_cnt=histogram[x];
			for (x=0; x<HISTO_WIDTH; x++) 
				for (y=0; y<HISTO_HEIGHT; y++) 
				{
					color = Color.FromArgb(125,125,125);
					histoBitmap.SetPixel(x,y,color);
				}
			for (x=0; x<HISTO_WIDTH; x++) 
			{
				double dHeight = (double)histogram[x]*(HISTO_HEIGHT-1)/(double)max_cnt;
				for (y=0; y<(int)dHeight; y++) 
				{
					color = Color.FromArgb(0,0,0);
					histoBitmap.SetPixel(x,(HISTO_HEIGHT-1)-y,color);
				}
			}
			grBm.DrawImage(histoBitmap, leftTopX,leftTopY);
			Invalidate();
		}
		private void strectchingItem_Click(object sender, System.EventArgs e)
		{
			int x,y;
			int alpha=0, beta=255;
			int[] histogram = new int[256];
            int[] LUT = new int[256];   
			histogram.Initialize();
			for (y=0; y<image.Height; y++)  // 각 픽셀의 종류별 count
				for (x=0; x<image.Width; x++) 
					histogram[grayArray[y,x]]++;  
			for(x=0; x<256; x++)
				if(histogram[x] != 0)  
				{
					alpha = x;
					break;
				}

			for(x=255; x>0; x--)
				if(histogram[x] != 0)  
				{
					beta = x;
					break;
				}
			for(x=0; x<alpha; x++)    LUT[x]=0;
			for(x=255; x>beta; x--) LUT[x]=255;

			for(x=alpha; x<=beta; x++)
				LUT[x]=(int)((x - alpha) * 255.0 / (beta - alpha));

			/* transfer new image */
			for (y=0; y<image.Height; y++)  
				for (x=0; x<image.Width; x++) 
					grayArray[y,x]=LUT[grayArray[y,x]];  
			displayArray(0, image.Height+10, grayArray);
			viewHistogram(image.Width+10, (image.Height>HISTO_HEIGHT)?image.Height+10:HISTO_HEIGHT+10, grayArray);
			Invalidate();
		}

		private void stretching2Item_Click(object sender, System.EventArgs e)
		{
			int    x,y;
			int    alpha=0, beta=255;
			double alphaPercent=3.0, betaPercent=3.0; // %
			int    histoSum;
			int[]  histogram = new int[256];
			int[]  LUT = new int[256];   
			int    numberOfPixels;

			histogram.Initialize();
			for (y=0; y<image.Height; y++)  // 각 픽셀의 종류별 count
				for (x=0; x<image.Width; x++) 
					histogram[grayArray[y,x]]++;  
			numberOfPixels = image.Width * image.Height;
			histoSum = 0;
			for(x=0; x<256; x++)
			{
				histoSum += histogram[x];
				if(histoSum*100.0/numberOfPixels >= alphaPercent)  
				{
					alpha = x;
					break;
				}
			}

			histoSum = 0;
			for(x=255; x>0; x--)
			{
				histoSum += histogram[x];
				if(histoSum*100.0/numberOfPixels >= betaPercent)  
				{
					beta = x;
					break;
				}
			}

			for(x=0; x<alpha; x++)    LUT[x]=0;
			for(x=255; x>beta; x--) LUT[x]=255;

			for(x=alpha; x<=beta; x++)
				LUT[x]=(int)((x - alpha) * 255.0 / (beta - alpha));

			/* transfer new image */
			for (y=0; y<image.Height; y++)  
				for (x=0; x<image.Width; x++) 
					grayArray[y,x]=LUT[grayArray[y,x]];  
			displayArray(0, image.Height+10, grayArray);
			viewHistogram(image.Width+10, (image.Height>HISTO_HEIGHT)?image.Height+10:HISTO_HEIGHT+10, grayArray);
			Invalidate();
		}

		private void equalizationItem_Click(object sender, System.EventArgs e)
		{
			int    x,y;
			int    sum;
			double scale;
			int[]  histogram    = new int[256];
			int[]  LUT = new int[256];   
			int    numberOfPixels;

			histogram.Initialize();
			for (y=0; y<image.Height; y++)  // 각 픽셀의 종류별 count
				for (x=0; x<image.Width; x++) 
					histogram[grayArray[y,x]]++;  
			numberOfPixels = image.Width * image.Height;
			sum = 0;
			scale = 255.0/numberOfPixels;
			for(x=0; x<256; x++)
			{
				sum += histogram[x];
				LUT[x]=(int)(sum*scale+0.5);
			}

			/* transfer new image */
			for (y=0; y<image.Height; y++)  
				for (x=0; x<image.Width; x++) 
					grayArray[y,x]=LUT[grayArray[y,x]];  
			displayArray(0, image.Height+10, grayArray);
			viewHistogram(image.Width+10, (image.Height>HISTO_HEIGHT)?image.Height+10:HISTO_HEIGHT+10, grayArray);
			Invalidate();
		}

		private void specificationItem_Click(object sender, System.EventArgs e)
		{
		
		}
		private void saveItem_Click(object sender, System.EventArgs e)
		{
			int x,y;
			Color color;
			sfdSave.Title = "Save Image as ...";
			sfdSave.OverwritePrompt = true;
			sfdSave.Filter = "Bitmap File(*.bmp)|*.bmp|GIF File(*.gif)|*.gif|JPEG File(*.jpg)|*.jpg|PNG file(*.png)|*.png|TIFF(*.tif)|*.tif";
			if(sfdSave.ShowDialog() == DialogResult.OK)
			{
				string strFilename = sfdSave.FileName;
				string strLowFilename = strFilename.ToLower();
				Bitmap gBitmap = new Bitmap(image.Width, image.Height);

				for(y=0; y<image.Height; y++)
					for(x=0; x<image.Width; x++)
					{
						color = Color.FromArgb(grayArray[y,x], grayArray[y,x], grayArray[y,x]);
						gBitmap.SetPixel(x, y, color);
					}
				gBitmap.Save(strLowFilename);
			}
		}
		private void closeItem_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		void displayArray(int leftTopX, int leftTopY, int[,] grayA)
		{
			int x,y;
			Color color;
			Bitmap gBitmap = new Bitmap(image.Width, image.Height);

			for(y=0; y<image.Height; y++)
				for(x=0; x<image.Width; x++)
				{
					color = Color.FromArgb(grayA[y,x], grayA[y,x], grayA[y,x]);
					gBitmap.SetPixel(x, y, color);
				}
			grBm.DrawImage(gBitmap, leftTopX,leftTopY, gBitmap.Width, gBitmap.Height);
			Invalidate();
		}

	}
}
