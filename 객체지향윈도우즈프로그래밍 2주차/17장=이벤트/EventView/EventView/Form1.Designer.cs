namespace EventView
{
	partial class Form1
	{
		/// <summary>
		/// 필수 디자이너 변수입니다.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 사용 중인 모든 리소스를 정리합니다.
		/// </summary>
		/// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form 디자이너에서 생성한 코드

		/// <summary>
		/// 디자이너 지원에 필요한 메서드입니다.
		/// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
		/// </summary>
		private void InitializeComponent()
		{
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(12, 12);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(261, 240);
			this.textBox1.TabIndex = 0;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(306, 63);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(101, 23);
			this.button1.TabIndex = 1;
			this.button1.Text = "모두 지우기";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// checkBox1
			// 
			this.checkBox1.AutoSize = true;
			this.checkBox1.Location = new System.Drawing.Point(306, 110);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(76, 16);
			this.checkBox1.TabIndex = 2;
			this.checkBox1.Text = "감시 중지";
			this.checkBox1.UseVisualStyleBackColor = true;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(445, 264);
			this.Controls.Add(this.checkBox1);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.textBox1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Deactivate += new System.EventHandler(this.Form1_Deactivate);
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ClientSizeChanged += new System.EventHandler(this.Form1_ClientSizeChanged);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
			this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
			this.Shown += new System.EventHandler(this.Form1_Shown);
			this.Activated += new System.EventHandler(this.Form1_Activated);
			this.Layout += new System.Windows.Forms.LayoutEventHandler(this.Form1_Layout);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
			this.Move += new System.EventHandler(this.Form1_Move);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
			this.Resize += new System.EventHandler(this.Form1_Resize);
			this.LocationChanged += new System.EventHandler(this.Form1_LocationChanged);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.CheckBox checkBox1;
	}
}

