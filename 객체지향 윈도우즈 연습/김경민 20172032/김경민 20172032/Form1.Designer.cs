﻿namespace 김경민_20172032
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
            this.파일ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.열기ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.저장ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.명암ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.밝게하기ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.어둡게하기ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.convolutionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.대화상자ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.파일ToolStripMenuItem,
            this.명암ToolStripMenuItem,
            this.convolutionToolStripMenuItem,
            this.대화상자ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(842, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 파일ToolStripMenuItem
            // 
            this.파일ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.열기ToolStripMenuItem,
            this.저장ToolStripMenuItem});
            this.파일ToolStripMenuItem.Name = "파일ToolStripMenuItem";
            this.파일ToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.파일ToolStripMenuItem.Text = "파일 ";
            // 
            // 열기ToolStripMenuItem
            // 
            this.열기ToolStripMenuItem.Name = "열기ToolStripMenuItem";
            this.열기ToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.열기ToolStripMenuItem.Text = "열기";
            this.열기ToolStripMenuItem.Click += new System.EventHandler(this.열기ToolStripMenuItem_Click);
            // 
            // 저장ToolStripMenuItem
            // 
            this.저장ToolStripMenuItem.Name = "저장ToolStripMenuItem";
            this.저장ToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.저장ToolStripMenuItem.Text = "저장";
            this.저장ToolStripMenuItem.Click += new System.EventHandler(this.저장ToolStripMenuItem_Click);
            // 
            // 명암ToolStripMenuItem
            // 
            this.명암ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.밝게하기ToolStripMenuItem,
            this.어둡게하기ToolStripMenuItem});
            this.명암ToolStripMenuItem.Name = "명암ToolStripMenuItem";
            this.명암ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.명암ToolStripMenuItem.Text = "명암";
            // 
            // 밝게하기ToolStripMenuItem
            // 
            this.밝게하기ToolStripMenuItem.Name = "밝게하기ToolStripMenuItem";
            this.밝게하기ToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.밝게하기ToolStripMenuItem.Text = "밝게 하기";
            this.밝게하기ToolStripMenuItem.Click += new System.EventHandler(this.밝게하기ToolStripMenuItem_Click);
            // 
            // 어둡게하기ToolStripMenuItem
            // 
            this.어둡게하기ToolStripMenuItem.Name = "어둡게하기ToolStripMenuItem";
            this.어둡게하기ToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.어둡게하기ToolStripMenuItem.Text = "어둡게 하기";
            this.어둡게하기ToolStripMenuItem.Click += new System.EventHandler(this.어둡게하기ToolStripMenuItem_Click);
            // 
            // convolutionToolStripMenuItem
            // 
            this.convolutionToolStripMenuItem.Name = "convolutionToolStripMenuItem";
            this.convolutionToolStripMenuItem.Size = new System.Drawing.Size(83, 20);
            this.convolutionToolStripMenuItem.Text = "convolution";
            // 
            // 대화상자ToolStripMenuItem
            // 
            this.대화상자ToolStripMenuItem.Name = "대화상자ToolStripMenuItem";
            this.대화상자ToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.대화상자ToolStripMenuItem.Text = "대화상자";
            this.대화상자ToolStripMenuItem.Click += new System.EventHandler(this.대화상자ToolStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(842, 469);
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
        private System.Windows.Forms.ToolStripMenuItem 파일ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 열기ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 저장ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 명암ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem convolutionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 대화상자ToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem 밝게하기ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 어둡게하기ToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}

