using LibVLCSharp.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using XiaoHeitu.ZPlayer.WinForm.Apis;
using XiaoHeitu.ZPlayer.WinForm.Controls;
using XiaoHeitu.ZPlayer.WinForm.Events;
using XiaoHeitu.ZPlayer.WinForm.Properties;

namespace XiaoHeitu.ZPlayer.WinForm.Forms
{
    public partial class MainForm : BaseForm
    {
        BufferedGraphicsContext MyBufferedGraphics = new BufferedGraphicsContext();
        OpenFileDialog openFD = new OpenFileDialog();
        LibVLC _libVLC;
        MediaPlayer _mediaPlayer;
        MediaPlayer _preview;

        /// <summary>
        /// Initializes a new instance of the Form1 class
        /// </summary>
        public MainForm()
        {
            this.InitializeComponent();
            this.InitZContainer();

            var currentAssembly = Assembly.GetEntryAssembly();
            var currentDirectory = new FileInfo(currentAssembly.Location).DirectoryName;
            // Default installation path of VideoLAN.LibVLC.Windows
            var libDirectory = new DirectoryInfo(System.IO.Path.Combine(currentDirectory, "libvlc", IntPtr.Size == 4 ? "win-x86" : "win-x64"));


            Core.Initialize(libDirectory.FullName);

            this._libVLC = new LibVLC();
            this._mediaPlayer = new MediaPlayer(this._libVLC);
            this._mediaPlayer.EnableMouseInput = true;

            this._mediaPlayer.Paused += this._mediaPlayer_Paused;
            this._mediaPlayer.Playing += this._mediaPlayer_Playing;
            this._mediaPlayer.Stopped += this._mediaPlayer_Stopped;
            this._mediaPlayer.EndReached += this._mediaPlayer_EndReached;
            this._mediaPlayer.PositionChanged += this._mediaPlayer_PositionChanged;

            //this.videoView1.MediaPlayer = this._mediaPlayer;

            this._mediaPlayer.Hwnd = this.pMoiveHost.Handle;

            this._preview = new MediaPlayer(this._libVLC);
            this._preview.Volume = 0;
            this._preview.Hwnd = this.pPreviewHost.Handle;
        }


        ZImageButton btnPlay;
        ZImageButton btnPause;
        ZImageButton btnStop;
        ZSlider sldProgress;

        public void InitZContainer()
        {
            this.btnPlay = new ZImageButton();
            this.btnPause = new ZImageButton();
            this.btnStop = new ZImageButton();
            this.sldProgress = new ZSlider();


            this.btnPlay.BeginInit();
            this.btnPause.BeginInit();
            this.btnStop.BeginInit();
            this.sldProgress.BeginInit();

            // 
            // btnPlay
            // 
            this.btnPlay.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Left)));
            this.btnPlay.HoverImage = Resources.Play;
            this.btnPlay.Location = new Point(15, 7);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.NormalImage = Resources.Play_OnPress;
            this.btnPlay.PressImage = Resources.Play_OnPress;
            this.btnPlay.Size = new Size(17, 19);
            //this.btnPlay.TabIndex = 6;
            //this.btnPlay.Text = "zButton1";
            this.btnPlay.Click += new EventHandler(this.btnPlay_Click);
            // 
            // btnPause
            // 
            this.btnPause.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Left)));
            this.btnPause.HoverImage = Resources.Pause;
            this.btnPause.Location = new Point(16, 8);
            this.btnPause.Name = "btnPause";
            this.btnPause.NormalImage = Resources.Pause_OnPress;
            this.btnPause.PressImage = Resources.Pause_OnPress;
            this.btnPause.Size = new Size(14, 17);
            //this.btnPause.TabIndex = 8;
            //this.btnPause.Text = "zButton1";
            this.btnPause.Visible = false;
            this.btnPause.Click += new EventHandler(this.btnPause_Click);
            // 
            // btnStop
            // 
            this.btnStop.HoverImage = Resources.Stop;
            this.btnStop.Location = new Point(36, 9);
            //this.btnStop.Name = "btnStop";
            this.btnStop.NormalImage = Resources.Stop_OnPress;
            this.btnStop.PressImage = Resources.Stop_OnPress;
            this.btnStop.Size = new Size(15, 15);
            //this.btnStop.TabIndex = 0;
            //this.btnStop.Text = "zImageButton1";
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            //
            // sldProgress
            //
            this.sldProgress.Anchor = ((AnchorStyles)((AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right)));
            this.sldProgress.DraggerEdgeInset = new Padding(0);
            this.sldProgress.DraggerSize = new Size(19, 19);
            this.sldProgress.HoverDraggerImage = Resources.Slider_Dragger;
            this.sldProgress.LoaderEdgeInset = new Padding(5, 4, 5, 4);
            this.sldProgress.LoaderImage = Resources.Slider_Loader;
            this.sldProgress.LoaderValue = 0.5F;
            this.sldProgress.Location = new Point(68, 6);
            this.sldProgress.Name = "sldProgress";
            this.sldProgress.NormalDraggerImage = Resources.Slider_Dragger;
            this.sldProgress.PressDraggerImage = Resources.Slider_Dragger_OnPress;
            this.sldProgress.RailEdgeInset = new Padding(6, 5, 6, 5);
            this.sldProgress.RailImage = Resources.Slider_Rail;
            this.sldProgress.RailPadding = new Padding(0);
            this.sldProgress.RailWidth = 10;
            this.sldProgress.Size = new Size(240, 20);
            //this.sldProgress.TabIndex = 0;
            //this.sldProgress.Text = "zSlider1";
            this.sldProgress.Value = 0F;
            this.sldProgress.ValueChanged += new ValueChangedEventHandler(this.sldProgress_ValueChanged);
            this.sldProgress.Hover += new HoverEventHandler(this.sldProgress_Hover);
            this.sldProgress.MouseLeave += new EventHandler(this.sldProgress_MouseLeave);

            this.zContainer1.ZControls.Add(this.btnPlay);
            this.zContainer1.ZControls.Add(this.btnPause);
            this.zContainer1.ZControls.Add(this.btnStop);
            this.zContainer1.ZControls.Add(this.sldProgress);


            this.btnPlay.EndInit();
            this.btnPause.EndInit();
            this.btnStop.EndInit();
            this.sldProgress.EndInit();
        }

        private void _mediaPlayer_PositionChanged(object sender, MediaPlayerPositionChangedEventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                this.sldProgress.Value = e.Position;
            }));
        }
        private void _mediaPlayer_EndReached(object sender, EventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                this.btnPlay.Visible = true;
                this.btnPause.Visible = false;
                this.sldProgress.Value = 0;
            }));
        }


        private void _mediaPlayer_Stopped(object sender, EventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                this.btnPlay.Visible = true;
                this.btnPause.Visible = false;
                this.sldProgress.Value = 0;
            }));
        }

        private void _mediaPlayer_Playing(object sender, EventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                this.btnPause.Visible = true;
                this.btnPlay.Visible = false;
            }));
        }

        private void _mediaPlayer_Paused(object sender, EventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                this.btnPlay.Visible = true;
                this.btnPause.Visible = false;
            }));

        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (this.openFD.ShowDialog() == DialogResult.OK)
            {
                this._mediaPlayer.Media = new Media(this._libVLC, this.GetStream(this.openFD.FileName));
                this._preview.Media = new Media(this._libVLC, this.GetStream(this.openFD.FileName), "--noaudio");

                //this._mediaPlayer.Play(new Media(this._libVLC, this.GetStream(this.openFD.FileName)));
            }
        }


        private Stream GetStream(string filename)
        {
            Stream result = null;
            var ext = Path.GetExtension(filename);
            switch (ext.ToUpper())
            {
                case ".ZIP":
                    {
                        result = this.GetZipStream(filename);
                        break;
                    }
                default:
                    {
                        result = File.OpenRead(filename);
                        break;
                    }
            }

            return result;
        }

        private Stream GetZipStream(string filename)
        {
            Stream result = null;
            FileStream zipToOpen = File.OpenRead(filename);
            ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Read);
            var entry = archive.Entries[0];
            result = entry.Open();
            return result;
        }


        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (this._mediaPlayer.State == VLCState.Ended)
            {
                this._mediaPlayer.Position = 0;
            }
            this._mediaPlayer.Play();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            this._mediaPlayer.Pause();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            this._mediaPlayer.Stop();
        }

        private void sldProgress_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            this._mediaPlayer.Position = this.sldProgress.Value;
        }

        private void sldProgress_Hover(object sender, HoverEventArgs e)
        {
            if (this._mediaPlayer.State != VLCState.Playing && this._mediaPlayer.State != VLCState.Paused)
            {
                return;
            }
            ZControl c = (ZControl)sender;
            this.pPreviewHost.Location = new Point((this.zContainer1.Location.X + c.Location.X + e.MouseLocation.X - this.pPreviewHost.Size.Width / 2), (this.zContainer1.Location.Y + c.Location.Y - this.pPreviewHost.Size.Height + 2));
            this._preview.Position = e.HoverValue;
            this._preview.Play();
            this.pPreviewHost.Visible = true;
        }

        private void sldProgress_MouseLeave(object sender, EventArgs e)
        {
            this._preview.Pause();
            this.pPreviewHost.Visible = false;
        }

        //private void zContainer1_MouseDown(object sender, MouseEventArgs e)
        //{
        //    Win32Api.ReleaseCapture();
        //    Win32Api.SendMessage(this.Handle, Win32Api.WM_SYSCOMMAND, Win32Api.SC_MOVE + Win32Api.HTCAPTION, 0);
        //}

        //private void pMoiveHost_MouseDown(object sender, MouseEventArgs e)
        //{
        //    Win32Api.ReleaseCapture();
        //    Win32Api.SendMessage(this.Handle, Win32Api.WM_SYSCOMMAND, Win32Api.SC_MOVE + Win32Api.HTCAPTION, 0);
        //}
    }
}
