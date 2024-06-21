namespace MDIForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.파일FToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.불러오기NToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.저장하기CToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.밝기변경ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.밝게ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.어둡게ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.히스토그램ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.번호판출력ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.파일FToolStripMenuItem,
            this.밝기변경ToolStripMenuItem,
            this.히스토그램ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(584, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 파일FToolStripMenuItem
            // 
            this.파일FToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.불러오기NToolStripMenuItem,
            this.저장하기CToolStripMenuItem});
            this.파일FToolStripMenuItem.Name = "파일FToolStripMenuItem";
            this.파일FToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.파일FToolStripMenuItem.Text = "파일";
            // 
            // 불러오기NToolStripMenuItem
            // 
            this.불러오기NToolStripMenuItem.Name = "불러오기NToolStripMenuItem";
            this.불러오기NToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.불러오기NToolStripMenuItem.Text = "불러오기";
            this.불러오기NToolStripMenuItem.Click += new System.EventHandler(this.불러오기NToolStripMenuItem_Click);
            // 
            // 저장하기CToolStripMenuItem
            // 
            this.저장하기CToolStripMenuItem.Name = "저장하기CToolStripMenuItem";
            this.저장하기CToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.저장하기CToolStripMenuItem.Text = "저장하기";
            this.저장하기CToolStripMenuItem.Click += new System.EventHandler(this.저장하기CToolStripMenuItem_Click);
            // 
            // 밝기변경ToolStripMenuItem
            // 
            this.밝기변경ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.밝게ToolStripMenuItem,
            this.어둡게ToolStripMenuItem});
            this.밝기변경ToolStripMenuItem.Name = "밝기변경ToolStripMenuItem";
            this.밝기변경ToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.밝기변경ToolStripMenuItem.Text = "밝기변경";
            // 
            // 밝게ToolStripMenuItem
            // 
            this.밝게ToolStripMenuItem.Name = "밝게ToolStripMenuItem";
            this.밝게ToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.밝게ToolStripMenuItem.Text = "밝게";
            this.밝게ToolStripMenuItem.Click += new System.EventHandler(this.밝게ToolStripMenuItem_Click);
            // 
            // 어둡게ToolStripMenuItem
            // 
            this.어둡게ToolStripMenuItem.Name = "어둡게ToolStripMenuItem";
            this.어둡게ToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.어둡게ToolStripMenuItem.Text = "어둡게";
            this.어둡게ToolStripMenuItem.Click += new System.EventHandler(this.어둡게ToolStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // 히스토그램ToolStripMenuItem
            // 
            this.히스토그램ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.번호판출력ToolStripMenuItem});
            this.히스토그램ToolStripMenuItem.Name = "히스토그램ToolStripMenuItem";
            this.히스토그램ToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
            this.히스토그램ToolStripMenuItem.Text = "히스토그램";
            // 
            // 번호판출력ToolStripMenuItem
            // 
            this.번호판출력ToolStripMenuItem.Name = "번호판출력ToolStripMenuItem";
            this.번호판출력ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.번호판출력ToolStripMenuItem.Text = "번호판 출력";
            this.번호판출력ToolStripMenuItem.Click += new System.EventHandler(this.번호판출력ToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 562);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 파일FToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 불러오기NToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 저장하기CToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 밝기변경ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 밝게ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 어둡게ToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem 히스토그램ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 번호판출력ToolStripMenuItem;
    }
}

