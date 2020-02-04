using LibVLCSharp.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using XiaoHeitu.ZPlayer.WinForm.Apis;

namespace XiaoHeitu.ZPlayer.WinForm.Forms
{
    public partial class MainForm : BaseForm
    {
        BufferedGraphicsContext MyBufferedGraphics = new BufferedGraphicsContext();
        OpenFileDialog openFD = new OpenFileDialog();
        LibVLC _libVLC;
        MediaPlayer _mediaPlayer;

        /// <summary>
        /// Initializes a new instance of the Form1 class
        /// </summary>
        public MainForm()
        {
            this.InitializeComponent();

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

            //this.videoView1.MediaPlayer = this._mediaPlayer;

            this._mediaPlayer.Hwnd = this.pMoiveHost.Handle;

        }

        private void _mediaPlayer_Stopped(object sender, EventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                this.btnPlay.Visible = true;
                this.btnPause.Visible = false;
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
                this._mediaPlayer.Play(new Media(this._libVLC, this.GetStream(this.openFD.FileName)));
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
            throw new Exception("暂不支持ZIP文件！");
            //Stream result = null;
            //FileStream zipToOpen = File.OpenRead(filename);
            //ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Read);
            //var entry = archive.Entries[0];
            //result = entry.Open();
            //return result;
        }




        private void btnPlay_Click(object sender, EventArgs e)
        {
            this._mediaPlayer.Play();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            this._mediaPlayer.Pause();
        }
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            Win32Api.ReleaseCapture();
            Win32Api.SendMessage(this.Handle, Win32Api.WM_SYSCOMMAND, Win32Api.SC_MOVE + Win32Api.HTCAPTION, 0);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            Win32Api.ReleaseCapture();
            Win32Api.SendMessage(this.Handle, Win32Api.WM_SYSCOMMAND, Win32Api.SC_MOVE + Win32Api.HTCAPTION, 0);
        }



        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (this._mediaPlayer.IsPlaying)
            {
                this.btnPause_Click(sender, e);
            }
            else
            {
                this.btnPlay_Click(sender, e);
            }
        }

        private void panel1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this._mediaPlayer.Fullscreen = !this._mediaPlayer.Fullscreen;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            this._mediaPlayer.Stop();
        }
    }
}
