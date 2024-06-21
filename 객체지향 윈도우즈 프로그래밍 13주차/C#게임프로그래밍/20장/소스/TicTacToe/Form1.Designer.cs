namespace TicTacToe
{
    partial class frmTicTacToe
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTicTacToe));
            this.btnNewGame = new System.Windows.Forms.Button();
            this.btnServer = new System.Windows.Forms.Button();
            this.btnClient = new System.Windows.Forms.Button();
            this.btnDisconn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pic11 = new System.Windows.Forms.PictureBox();
            this.pic12 = new System.Windows.Forms.PictureBox();
            this.pic13 = new System.Windows.Forms.PictureBox();
            this.pic21 = new System.Windows.Forms.PictureBox();
            this.pic22 = new System.Windows.Forms.PictureBox();
            this.pic23 = new System.Windows.Forms.PictureBox();
            this.pic33 = new System.Windows.Forms.PictureBox();
            this.pic32 = new System.Windows.Forms.PictureBox();
            this.pic31 = new System.Windows.Forms.PictureBox();
            this.lblMsg = new System.Windows.Forms.Label();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.ilSign = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pic11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic21)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic22)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic23)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic33)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic32)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic31)).BeginInit();
            this.SuspendLayout();
            // 
            // btnNewGame
            // 
            this.btnNewGame.Location = new System.Drawing.Point(8, 8);
            this.btnNewGame.Name = "btnNewGame";
            this.btnNewGame.Size = new System.Drawing.Size(80, 23);
            this.btnNewGame.TabIndex = 0;
            this.btnNewGame.Text = "새 게임";
            this.btnNewGame.Click += new System.EventHandler(this.btnNewGame_Click);
            // 
            // btnServer
            // 
            this.btnServer.Location = new System.Drawing.Point(94, 8);
            this.btnServer.Name = "btnServer";
            this.btnServer.Size = new System.Drawing.Size(80, 23);
            this.btnServer.TabIndex = 1;
            this.btnServer.Text = "서버로 시작";
            this.btnServer.Click += new System.EventHandler(this.btnServer_Click);
            // 
            // btnClient
            // 
            this.btnClient.Location = new System.Drawing.Point(181, 8);
            this.btnClient.Name = "btnClient";
            this.btnClient.Size = new System.Drawing.Size(81, 23);
            this.btnClient.TabIndex = 2;
            this.btnClient.Text = "서버에 접속";
            this.btnClient.Click += new System.EventHandler(this.btnClient_Click);
            // 
            // btnDisconn
            // 
            this.btnDisconn.Location = new System.Drawing.Point(268, 8);
            this.btnDisconn.Name = "btnDisconn";
            this.btnDisconn.Size = new System.Drawing.Size(78, 23);
            this.btnDisconn.TabIndex = 3;
            this.btnDisconn.Text = "접속종료";
            this.btnDisconn.Click += new System.EventHandler(this.btnDisconn_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(27, 172);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(295, 10);
            this.label1.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(123, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(10, 265);
            this.label2.TabIndex = 5;
            this.label2.Text = "label2";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(221, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(10, 265);
            this.label3.TabIndex = 6;
            this.label3.Text = "label3";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(29, 252);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(293, 12);
            this.label4.TabIndex = 7;
            // 
            // pic11
            // 
            this.pic11.Location = new System.Drawing.Point(43, 106);
            this.pic11.Name = "pic11";
            this.pic11.Size = new System.Drawing.Size(74, 60);
            this.pic11.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pic11.TabIndex = 10;
            this.pic11.TabStop = false;
            this.pic11.Click += new System.EventHandler(this.pic_Click);
            // 
            // pic12
            // 
            this.pic12.Location = new System.Drawing.Point(140, 106);
            this.pic12.Name = "pic12";
            this.pic12.Size = new System.Drawing.Size(74, 60);
            this.pic12.TabIndex = 18;
            this.pic12.TabStop = false;
            this.pic12.Click += new System.EventHandler(this.pic_Click);
            // 
            // pic13
            // 
            this.pic13.Location = new System.Drawing.Point(238, 107);
            this.pic13.Name = "pic13";
            this.pic13.Size = new System.Drawing.Size(74, 60);
            this.pic13.TabIndex = 19;
            this.pic13.TabStop = false;
            this.pic13.Click += new System.EventHandler(this.pic_Click);
            // 
            // pic21
            // 
            this.pic21.Location = new System.Drawing.Point(43, 188);
            this.pic21.Name = "pic21";
            this.pic21.Size = new System.Drawing.Size(74, 60);
            this.pic21.TabIndex = 20;
            this.pic21.TabStop = false;
            this.pic21.Click += new System.EventHandler(this.pic_Click);
            // 
            // pic22
            // 
            this.pic22.Location = new System.Drawing.Point(139, 188);
            this.pic22.Name = "pic22";
            this.pic22.Size = new System.Drawing.Size(74, 60);
            this.pic22.TabIndex = 21;
            this.pic22.TabStop = false;
            this.pic22.Click += new System.EventHandler(this.pic_Click);
            // 
            // pic23
            // 
            this.pic23.Location = new System.Drawing.Point(237, 189);
            this.pic23.Name = "pic23";
            this.pic23.Size = new System.Drawing.Size(74, 60);
            this.pic23.TabIndex = 22;
            this.pic23.TabStop = false;
            this.pic23.Click += new System.EventHandler(this.pic_Click);
            // 
            // pic33
            // 
            this.pic33.Location = new System.Drawing.Point(238, 270);
            this.pic33.Name = "pic33";
            this.pic33.Size = new System.Drawing.Size(74, 60);
            this.pic33.TabIndex = 25;
            this.pic33.TabStop = false;
            this.pic33.Click += new System.EventHandler(this.pic_Click);
            // 
            // pic32
            // 
            this.pic32.Location = new System.Drawing.Point(140, 269);
            this.pic32.Name = "pic32";
            this.pic32.Size = new System.Drawing.Size(74, 60);
            this.pic32.TabIndex = 24;
            this.pic32.TabStop = false;
            this.pic32.Click += new System.EventHandler(this.pic_Click);
            // 
            // pic31
            // 
            this.pic31.Location = new System.Drawing.Point(44, 269);
            this.pic31.Name = "pic31";
            this.pic31.Size = new System.Drawing.Size(74, 60);
            this.pic31.TabIndex = 23;
            this.pic31.TabStop = false;
            this.pic31.Click += new System.EventHandler(this.pic_Click);
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.ForeColor = System.Drawing.Color.Blue;
            this.lblMsg.Location = new System.Drawing.Point(12, 49);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(124, 12);
            this.lblMsg.TabIndex = 26;
            this.lblMsg.Text = "새 게임을 시작합니다.";
            // 
            // txtIP
            // 
            this.txtIP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtIP.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtIP.Location = new System.Drawing.Point(200, 41);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(100, 21);
            this.txtIP.TabIndex = 27;
            this.txtIP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtIP.Visible = false;
            // 
            // btnOk
            // 
            this.btnOk.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btnOk.Location = new System.Drawing.Point(304, 41);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(42, 20);
            this.btnOk.TabIndex = 28;
            this.btnOk.Text = "확인";
            this.btnOk.Visible = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // ilSign
            // 
            this.ilSign.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilSign.ImageStream")));
            this.ilSign.Images.SetKeyName(0, "Cross.GIF");
            this.ilSign.Images.SetKeyName(1, "Ball.GIF");
            // 
            // frmTicTacToe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(353, 376);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.txtIP);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.pic33);
            this.Controls.Add(this.pic32);
            this.Controls.Add(this.pic31);
            this.Controls.Add(this.pic23);
            this.Controls.Add(this.pic22);
            this.Controls.Add(this.pic21);
            this.Controls.Add(this.pic13);
            this.Controls.Add(this.pic12);
            this.Controls.Add(this.pic11);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnDisconn);
            this.Controls.Add(this.btnClient);
            this.Controls.Add(this.btnServer);
            this.Controls.Add(this.btnNewGame);
            this.KeyPreview = true;
            this.Name = "frmTicTacToe";
            this.Text = "Tic Tac Toe";
            this.Load += new System.EventHandler(this.frmTicTacToe_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pic11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic21)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic22)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic23)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic33)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic32)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic31)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnNewGame;
        private System.Windows.Forms.Button btnServer;
        private System.Windows.Forms.Button btnClient;
        private System.Windows.Forms.Button btnDisconn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pic11;
        private System.Windows.Forms.PictureBox pic12;
        private System.Windows.Forms.PictureBox pic13;
        private System.Windows.Forms.PictureBox pic21;
        private System.Windows.Forms.PictureBox pic22;
        private System.Windows.Forms.PictureBox pic23;
        private System.Windows.Forms.PictureBox pic33;
        private System.Windows.Forms.PictureBox pic32;
        private System.Windows.Forms.PictureBox pic31;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.ImageList ilSign;
    }
}

