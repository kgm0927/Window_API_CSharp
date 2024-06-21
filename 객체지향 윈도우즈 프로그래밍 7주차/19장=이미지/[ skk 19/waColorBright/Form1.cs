using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace waColorBright
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btOpen;
		private System.Windows.Forms.Button btConvert;
		private System.Windows.Forms.OpenFileDialog ofdOpen;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		Image image;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
			this.btOpen = new System.Windows.Forms.Button();
			this.btConvert = new System.Windows.Forms.Button();
			this.ofdOpen = new System.Windows.Forms.OpenFileDialog();
			this.SuspendLayout();
			// 
			// btOpen
			// 
			this.btOpen.Location = new System.Drawing.Point(144, 8);
			this.btOpen.Name = "btOpen";
			this.btOpen.Size = new System.Drawing.Size(144, 23);
			this.btOpen.TabIndex = 0;
			this.btOpen.Text = "RGB 영상파일 읽기";
			this.btOpen.Click += new System.EventHandler(this.btOpen_Click);
			// 
			// btConvert
			// 
			this.btConvert.Location = new System.Drawing.Point(432, 8);
			this.btConvert.Name = "btConvert";
			this.btConvert.Size = new System.Drawing.Size(88, 23);
			this.btConvert.TabIndex = 1;
			this.btConvert.Text = "밝게 변환";
			this.btConvert.Click += new System.EventHandler(this.btConvert_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(672, 318);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.btConvert,
																		  this.btOpen});
			this.Name = "Form1";
			this.Text = "RGB영상을 밝게 처리";
			this.ResumeLayout(false);

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

		private void btOpen_Click(object sender, System.EventArgs e)
		{
			Graphics gr = CreateGraphics();
			ofdOpen.Title = "영상파일 열기";
			ofdOpen.Filter = "All Files(*.*)|*.*|Bitmap File(*.bmp)|*.bmp|Gif File(*.gif)|*.gif|Jpeg File(*.jpg)|*.jpg";
			if(ofdOpen.ShowDialog() == DialogResult.OK)
			{
				string strFilename = ofdOpen.FileName;
				image = Image.FromFile(strFilename);
				gr.DrawImage(image, 10,50, image.Width, image.Height);
			}
			gr.Dispose();
		}

		private void btConvert_Click(object sender, System.EventArgs e)
		{
			int x, y, z;
			int r, g, b;
			const int ADD_VALUE = 50;
			Color color;

			Graphics gr = CreateGraphics();
			Bitmap gBitmap = new Bitmap(image);
			for(y=0; y<image.Height; y++)
				for(x=0; x<image.Width; x++)
				{
					color = gBitmap.GetPixel(x,y);
					r = color.R+ADD_VALUE; if(r > 255) r = 255;
					g = color.G+ADD_VALUE; if(g > 255) g = 255;
					b = color.B+ADD_VALUE; if(b > 255) b = 255;
					color = Color.FromArgb(r,g,b);
					gBitmap.SetPixel(x, y, color);
				}
			gr.DrawImage(gBitmap, 20+gBitmap.Width, 50, gBitmap.Width, gBitmap.Height);
			gr.Dispose();
		}
	}
}
