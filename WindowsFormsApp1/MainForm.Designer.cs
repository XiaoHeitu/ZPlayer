namespace WindowsFormsApp1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.button1 = new System.Windows.Forms.Button();
            this.btnPlay = new WindowsFormsApp1.Controls.ZImageButton();
            this.btnPause = new WindowsFormsApp1.Controls.ZImageButton();
            this.pMoiveHost = new System.Windows.Forms.Panel();
            this.btnStop = new WindowsFormsApp1.Controls.ZImageButton();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(28, 393);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(113, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "打开文件[&O]";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPlay.HoverImage = global::WindowsFormsApp1.Properties.Resources.Play;
            this.btnPlay.Location = new System.Drawing.Point(15, 489);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.NormalImage = global::WindowsFormsApp1.Properties.Resources.Play_OnPress;
            this.btnPlay.PressImage = global::WindowsFormsApp1.Properties.Resources.Play_OnPress;
            this.btnPlay.Size = new System.Drawing.Size(13, 15);
            this.btnPlay.TabIndex = 6;
            this.btnPlay.Text = "zButton1";
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnPause
            // 
            this.btnPause.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPause.HoverImage = global::WindowsFormsApp1.Properties.Resources.Pause;
            this.btnPause.Location = new System.Drawing.Point(16, 490);
            this.btnPause.Name = "btnPause";
            this.btnPause.NormalImage = global::WindowsFormsApp1.Properties.Resources.Pause_Onpress;
            this.btnPause.PressImage = global::WindowsFormsApp1.Properties.Resources.Pause_Onpress;
            this.btnPause.Size = new System.Drawing.Size(10, 13);
            this.btnPause.TabIndex = 8;
            this.btnPause.Text = "zButton1";
            this.btnPause.Visible = false;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // pMoiveHost
            // 
            this.pMoiveHost.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pMoiveHost.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.pMoiveHost.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pMoiveHost.BackgroundImage")));
            this.pMoiveHost.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pMoiveHost.Location = new System.Drawing.Point(0, 0);
            this.pMoiveHost.Name = "pMoiveHost";
            this.pMoiveHost.Size = new System.Drawing.Size(640, 482);
            this.pMoiveHost.TabIndex = 9;
            this.pMoiveHost.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseClick);
            this.pMoiveHost.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDoubleClick);
            this.pMoiveHost.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            // 
            // btnStop
            // 
            this.btnStop.HoverImage = global::WindowsFormsApp1.Properties.Resources.Stop;
            this.btnStop.Location = new System.Drawing.Point(36, 491);
            this.btnStop.Name = "btnStop";
            this.btnStop.NormalImage = global::WindowsFormsApp1.Properties.Resources.Stop_Onpress;
            this.btnStop.PressImage = global::WindowsFormsApp1.Properties.Resources.Stop_Onpress;
            this.btnStop.Size = new System.Drawing.Size(11, 11);
            this.btnStop.TabIndex = 0;
            this.btnStop.Text = "zImageButton1";
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundEdgeInset = new System.Windows.Forms.Padding(1, 1, 30, 29);
            this.BackgroundImage = global::WindowsFormsApp1.Properties.Resources.bg;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(640, 510);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pMoiveHost);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainForm";
            this.Text = "Form1";
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private Controls.ZImageButton btnPlay;
        private Controls.ZImageButton btnPause;
        private System.Windows.Forms.Panel pMoiveHost;
        private Controls.ZImageButton btnStop;
    }
}

