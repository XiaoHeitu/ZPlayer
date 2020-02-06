namespace XiaoHeitu.ZPlayer.WinForm.Forms
{
    partial class MainForm
    {

        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.pMoiveHost = new System.Windows.Forms.Panel();
            this.zContainer1 = new XiaoHeitu.ZPlayer.WinForm.Controls.ZContainer();
            this.pPreviewHost = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(28, 377);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(113, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "打开文件[&O]";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pMoiveHost
            // 
            this.pMoiveHost.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pMoiveHost.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.pMoiveHost.BackgroundImage = global::XiaoHeitu.ZPlayer.WinForm.Properties.Resources.Logo;
            this.pMoiveHost.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pMoiveHost.Location = new System.Drawing.Point(2, 2);
            this.pMoiveHost.Name = "pMoiveHost";
            this.pMoiveHost.Size = new System.Drawing.Size(640, 462);
            this.pMoiveHost.TabIndex = 9;
            this.pMoiveHost.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pMoiveHost_MouseDown);
            // 
            // zContainer1
            // 
            this.zContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.zContainer1.BackColor = System.Drawing.SystemColors.Control;
            this.zContainer1.BackgroundEdgeInset = new System.Windows.Forms.Padding(2, 2, 2, 29);
            this.zContainer1.BackgroundImage = global::XiaoHeitu.ZPlayer.WinForm.Properties.Resources.Background;
            this.zContainer1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.zContainer1.Location = new System.Drawing.Point(2, 462);
            this.zContainer1.Name = "zContainer1";
            this.zContainer1.Size = new System.Drawing.Size(640, 29);
            this.zContainer1.TabIndex = 0;
            this.zContainer1.Text = "zContainer1";
            this.zContainer1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.zContainer1_MouseDown);
            // 
            // pPreviewHost
            // 
            this.pPreviewHost.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.pPreviewHost.Location = new System.Drawing.Point(-500, -500);
            this.pPreviewHost.Name = "pPreviewHost";
            this.pPreviewHost.Size = new System.Drawing.Size(91, 54);
            this.pPreviewHost.TabIndex = 0;
            this.pPreviewHost.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::XiaoHeitu.ZPlayer.WinForm.Properties.Resources.Background;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(644, 494);
            this.Controls.Add(this.pPreviewHost);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pMoiveHost);
            this.Controls.Add(this.zContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainForm";
            this.Text = "Form1";
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button1;
        //private Controls.ZImageButton btnPlay;
        //private Controls.ZImageButton btnPause;
        private System.Windows.Forms.Panel pMoiveHost;
        //private Controls.ZImageButton btnStop;
        //private Controls.ZSlider sldProgress;
        private System.Windows.Forms.Panel pPreviewHost;
        private Controls.ZContainer zContainer1;
    }
}

