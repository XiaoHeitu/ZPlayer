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
            this.components = new System.ComponentModel.Container();
            this.pMoiveHost = new System.Windows.Forms.Panel();
            this.cmsReightMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miFile = new System.Windows.Forms.ToolStripMenuItem();
            this.miOpenFile = new System.Windows.Forms.ToolStripMenuItem();
            this.miHardwareDecoding = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.miExit = new System.Windows.Forms.ToolStripMenuItem();
            this.miSubtitle = new System.Windows.Forms.ToolStripMenuItem();
            this.zContainer1 = new XiaoHeitu.ZPlayer.WinForm.Controls.ZContainer();
            this.pPreviewHost = new System.Windows.Forms.Panel();
            this.miLoadSubtitleFile = new System.Windows.Forms.ToolStripMenuItem();
            this.miSelectSubtitle = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsReightMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // pMoiveHost
            // 
            this.pMoiveHost.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pMoiveHost.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.pMoiveHost.BackgroundImage = global::XiaoHeitu.ZPlayer.WinForm.Properties.Resources.Logo;
            this.pMoiveHost.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pMoiveHost.ContextMenuStrip = this.cmsReightMenu;
            this.pMoiveHost.Location = new System.Drawing.Point(2, 2);
            this.pMoiveHost.Name = "pMoiveHost";
            this.pMoiveHost.Size = new System.Drawing.Size(640, 461);
            this.pMoiveHost.TabIndex = 9;
            this.pMoiveHost.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pMoiveHost_MouseDown);
            this.pMoiveHost.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pMoiveHost_MouseMove);
            // 
            // cmsReightMenu
            // 
            this.cmsReightMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miFile,
            this.miSubtitle,
            this.miHardwareDecoding,
            this.toolStripSeparator1,
            this.miExit});
            this.cmsReightMenu.Name = "cmsReightMenu";
            this.cmsReightMenu.Size = new System.Drawing.Size(181, 120);
            this.cmsReightMenu.Opening += new System.ComponentModel.CancelEventHandler(this.cmsReightMenu_Opening);
            // 
            // miFile
            // 
            this.miFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miOpenFile});
            this.miFile.Name = "miFile";
            this.miFile.Size = new System.Drawing.Size(180, 22);
            this.miFile.Text = "文件(&F)";
            // 
            // miOpenFile
            // 
            this.miOpenFile.Name = "miOpenFile";
            this.miOpenFile.Size = new System.Drawing.Size(142, 22);
            this.miOpenFile.Text = "打开文件(&O)";
            this.miOpenFile.Click += new System.EventHandler(this.miOpenFile_Click);
            // 
            // miHardwareDecoding
            // 
            this.miHardwareDecoding.Name = "miHardwareDecoding";
            this.miHardwareDecoding.Size = new System.Drawing.Size(180, 22);
            this.miHardwareDecoding.Text = "使用硬件解码(&H)";
            this.miHardwareDecoding.Click += new System.EventHandler(this.miHardwareDecoding_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // miExit
            // 
            this.miExit.Name = "miExit";
            this.miExit.Size = new System.Drawing.Size(180, 22);
            this.miExit.Text = "退出(X)";
            this.miExit.Click += new System.EventHandler(this.miExit_Click);
            // 
            // miSubtitle
            // 
            this.miSubtitle.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miLoadSubtitleFile,
            this.miSelectSubtitle});
            this.miSubtitle.Name = "miSubtitle";
            this.miSubtitle.Size = new System.Drawing.Size(180, 22);
            this.miSubtitle.Text = "字幕";
            // 
            // zContainer1
            // 
            this.zContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.zContainer1.BackColor = System.Drawing.SystemColors.Control;
            this.zContainer1.BackgroundEdgeInset = new System.Windows.Forms.Padding(2, 2, 2, 29);
            this.zContainer1.BackgroundImage = global::XiaoHeitu.ZPlayer.WinForm.Properties.Resources.Background;
            this.zContainer1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.zContainer1.ContextMenuStrip = this.cmsReightMenu;
            this.zContainer1.Location = new System.Drawing.Point(2, 462);
            this.zContainer1.Name = "zContainer1";
            this.zContainer1.Size = new System.Drawing.Size(640, 29);
            this.zContainer1.TabIndex = 0;
            this.zContainer1.Text = "zContainer1";
            this.zContainer1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.zContainer1_MouseDown);
            this.zContainer1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.zContainer1_MouseMove);
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
            // miLoadSubtitleFile
            // 
            this.miLoadSubtitleFile.Name = "miLoadSubtitleFile";
            this.miLoadSubtitleFile.Size = new System.Drawing.Size(180, 22);
            this.miLoadSubtitleFile.Text = "加载字幕文件";
            this.miLoadSubtitleFile.Click += new System.EventHandler(this.miLoadSubtitleFile_Click);
            // 
            // miSelectSubtitle
            // 
            this.miSelectSubtitle.Name = "miSelectSubtitle";
            this.miSelectSubtitle.Size = new System.Drawing.Size(180, 22);
            this.miSelectSubtitle.Text = "选择字幕";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::XiaoHeitu.ZPlayer.WinForm.Properties.Resources.Background;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(644, 494);
            this.ContextMenuStrip = this.cmsReightMenu;
            this.Controls.Add(this.pPreviewHost);
            this.Controls.Add(this.zContainer1);
            this.Controls.Add(this.pMoiveHost);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.MinimumSize = new System.Drawing.Size(480, 330);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.cmsReightMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pMoiveHost;
        private System.Windows.Forms.Panel pPreviewHost;
        private Controls.ZContainer zContainer1;
        private System.Windows.Forms.ContextMenuStrip cmsReightMenu;
        private System.Windows.Forms.ToolStripMenuItem miFile;
        private System.Windows.Forms.ToolStripMenuItem miExit;
        private System.Windows.Forms.ToolStripMenuItem miOpenFile;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem miHardwareDecoding;
        private System.Windows.Forms.ToolStripMenuItem miSubtitle;
        private System.Windows.Forms.ToolStripMenuItem miLoadSubtitleFile;
        private System.Windows.Forms.ToolStripMenuItem miSelectSubtitle;
    }
}

