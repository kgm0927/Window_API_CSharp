namespace DrawLine_and_Dialogbox
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
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.새ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.빨강ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.파랑ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.초록ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.대화상자ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.새ToolStripMenuItem,
            this.대화상자ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.MenuActivate += new System.EventHandler(this.menuStrip1_MenuActivate);
            // 
            // 새ToolStripMenuItem
            // 
            this.새ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.빨강ToolStripMenuItem,
            this.파랑ToolStripMenuItem,
            this.초록ToolStripMenuItem});
            this.새ToolStripMenuItem.Name = "새ToolStripMenuItem";
            this.새ToolStripMenuItem.Size = new System.Drawing.Size(68, 24);
            this.새ToolStripMenuItem.Text = "선색깔";
            // 
            // 빨강ToolStripMenuItem
            // 
            this.빨강ToolStripMenuItem.Name = "빨강ToolStripMenuItem";
            this.빨강ToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.빨강ToolStripMenuItem.Text = "빨강";
            this.빨강ToolStripMenuItem.Click += new System.EventHandler(this.빨강ToolStripMenuItem_Click);
            // 
            // 파랑ToolStripMenuItem
            // 
            this.파랑ToolStripMenuItem.Name = "파랑ToolStripMenuItem";
            this.파랑ToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.파랑ToolStripMenuItem.Text = "파랑";
            this.파랑ToolStripMenuItem.Click += new System.EventHandler(this.파랑ToolStripMenuItem_Click);
            // 
            // 초록ToolStripMenuItem
            // 
            this.초록ToolStripMenuItem.Name = "초록ToolStripMenuItem";
            this.초록ToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.초록ToolStripMenuItem.Text = "초록";
            this.초록ToolStripMenuItem.Click += new System.EventHandler(this.초록ToolStripMenuItem_Click);
            // 
            // 대화상자ToolStripMenuItem
            // 
            this.대화상자ToolStripMenuItem.Name = "대화상자ToolStripMenuItem";
            this.대화상자ToolStripMenuItem.Size = new System.Drawing.Size(83, 24);
            this.대화상자ToolStripMenuItem.Text = "대화상자";
            this.대화상자ToolStripMenuItem.Click += new System.EventHandler(this.대화상자ToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 새ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 빨강ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 파랑ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 초록ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 대화상자ToolStripMenuItem;
    }
}

