namespace Client
{
    partial class Client
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
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.txtSend = new System.Windows.Forms.TextBox();
            this.btnConn = new System.Windows.Forms.Button();
            this.txtNick = new System.Windows.Forms.TextBox();
            this.lblNick = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(12, 64);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ReadOnly = true;
            this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtOutput.Size = new System.Drawing.Size(362, 276);
            this.txtOutput.TabIndex = 0;
            // 
            // txtSend
            // 
            this.txtSend.Location = new System.Drawing.Point(12, 346);
            this.txtSend.Name = "txtSend";
            this.txtSend.Size = new System.Drawing.Size(359, 21);
            this.txtSend.TabIndex = 1;
            this.txtSend.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSend_KeyPress);
            // 
            // btnConn
            // 
            this.btnConn.Location = new System.Drawing.Point(296, 34);
            this.btnConn.Name = "btnConn";
            this.btnConn.Size = new System.Drawing.Size(75, 23);
            this.btnConn.TabIndex = 3;
            this.btnConn.Text = "연결하기";
            this.btnConn.Click += new System.EventHandler(this.btnConn_Click);
            // 
            // txtNick
            // 
            this.txtNick.Location = new System.Drawing.Point(50, 36);
            this.txtNick.Name = "txtNick";
            this.txtNick.Size = new System.Drawing.Size(237, 21);
            this.txtNick.TabIndex = 5;
            // 
            // lblNick
            // 
            this.lblNick.AutoSize = true;
            this.lblNick.Location = new System.Drawing.Point(15, 39);
            this.lblNick.Name = "lblNick";
            this.lblNick.Size = new System.Drawing.Size(28, 12);
            this.lblNick.TabIndex = 6;
            this.lblNick.Text = "별명";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "주소";
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(50, 9);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(237, 21);
            this.txtIP.TabIndex = 9;
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 386);
            this.Controls.Add(this.txtIP);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblNick);
            this.Controls.Add(this.txtNick);
            this.Controls.Add(this.btnConn);
            this.Controls.Add(this.txtSend);
            this.Controls.Add(this.txtOutput);
            this.Name = "Client";
            this.Text = "클라이언트";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtOutput;
		private System.Windows.Forms.TextBox txtSend;
		private System.Windows.Forms.Button btnConn;
        private System.Windows.Forms.TextBox txtNick;
        private System.Windows.Forms.Label lblNick;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtIP;
    }
}

