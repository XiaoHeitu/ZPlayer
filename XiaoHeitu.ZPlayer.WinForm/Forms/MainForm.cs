using LibVLCSharp.Shared;
using SharpGL;
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
using System.Threading;
using System.Threading.Tasks;
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


        private readonly static object _MyLock = new object();

        FormWindowState tempWindowState = FormWindowState.Normal;
        bool isFullscreen = false;

        float replayPosition = 0;

        IntPtr buffer = IntPtr.Zero;
        uint videoWidth = 0;
        uint videoHeight = 0;


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
            this._mediaPlayer.EnableMouseInput = false;
            this._mediaPlayer.EnableKeyInput = false;

            this._mediaPlayer.Paused += this._mediaPlayer_Paused;
            this._mediaPlayer.Playing += this._mediaPlayer_Playing;
            this._mediaPlayer.Stopped += this._mediaPlayer_Stopped;
            this._mediaPlayer.EndReached += this._mediaPlayer_EndReached;
            this._mediaPlayer.PositionChanged += this._mediaPlayer_PositionChanged;
            this._mediaPlayer.LengthChanged += this._mediaPlayer_LengthChanged;
            //this._mediaPlayer.Hwnd = this.pMoiveHost.Handle;
            this._mediaPlayer.EnableHardwareDecoding = false;


            this._preview = new MediaPlayer(this._libVLC);
            this._preview.EnableMouseInput = false;
            this._preview.EnableKeyInput = false;
            this._preview.Mute = true;
            this._preview.Volume = 0;
            this._preview.EnableHardwareDecoding = true;
            this._preview.Hwnd = this.pPreviewHost.Handle;

            //初始值
            this.sldVolume.Value = this._mediaPlayer.Volume / 100f;

        }


        public MainForm(string fileName) : this()
        {
            if (!string.IsNullOrWhiteSpace(fileName))
            {
                this.Play(fileName);
            }
        }
        #region 初始化Z容器
        private ZImageButton btnPlay;
        private ZImageButton btnPause;
        private ZImageButton btnStop;
        private ZSlider sldProgress;
        private ZLabel labProgress;
        private ZImageButton btnVolume;
        private ZSlider sldVolume;
        private ZImageButton btnFullScreen;

        public void InitZContainer()
        {
            this.btnPlay = new ZImageButton();
            this.btnPause = new ZImageButton();
            this.btnStop = new ZImageButton();
            this.sldProgress = new ZSlider();
            this.labProgress = new ZLabel();
            this.btnVolume = new ZImageButton();
            this.sldVolume = new ZSlider();
            this.btnFullScreen = new ZImageButton();



            this.btnPlay.BeginInit();
            this.btnPause.BeginInit();
            this.btnStop.BeginInit();
            this.sldProgress.BeginInit();
            this.labProgress.BeginInit();
            this.btnVolume.BeginInit();
            this.sldVolume.BeginInit();
            this.btnFullScreen.BeginInit();

            // 
            // btnPlay
            // 
            this.btnPlay.Anchor = ((AnchorStyles)((AnchorStyles.Top | AnchorStyles.Left)));
            this.btnPlay.HoverImage = Resources.Play;
            this.btnPlay.Location = new Point(7, 7);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.NormalImage = Resources.Play_OnPress;
            this.btnPlay.PressImage = Resources.Play_OnPress;
            this.btnPlay.Size = new Size(17, 19);
            this.btnPlay.Click += new EventHandler(this.btnPlay_Click);
            // 
            // btnPause
            // 
            this.btnPause.Anchor = ((AnchorStyles)((AnchorStyles.Top | AnchorStyles.Left)));
            this.btnPause.HoverImage = Resources.Pause;
            this.btnPause.Location = new Point(7, 7);
            this.btnPause.Name = "btnPause";
            this.btnPause.NormalImage = Resources.Pause_OnPress;
            this.btnPause.PressImage = Resources.Pause_OnPress;
            this.btnPause.Size = new Size(17, 19);
            this.btnPause.Visible = false;
            this.btnPause.Click += new EventHandler(this.btnPause_Click);
            // 
            // btnStop
            // 
            this.btnStop.Anchor = ((AnchorStyles)((AnchorStyles.Top | AnchorStyles.Left)));
            this.btnStop.HoverImage = Resources.Stop;
            this.btnStop.Location = new Point(29, 9);
            this.btnStop.NormalImage = Resources.Stop_OnPress;
            this.btnStop.PressImage = Resources.Stop_OnPress;
            this.btnStop.Size = new Size(15, 15);
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            //
            // sldProgress
            //
            this.sldProgress.Anchor = ((AnchorStyles)((AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right)));
            this.sldProgress.DraggerEdgeInset = new Padding(0);
            this.sldProgress.DraggerSize = new Size(19, 19);
            this.sldProgress.HoverDraggerImage = Resources.Slider_Dragger;
            this.sldProgress.LoaderEdgeInset = new Padding(5, 4, 5, 4);
            this.sldProgress.LoaderImage = Resources.Slider_Loader;
            this.sldProgress.Location = new Point(50, 6);
            this.sldProgress.Name = "sldProgress";
            this.sldProgress.NormalDraggerImage = Resources.Slider_Dragger_OnPress;
            this.sldProgress.PressDraggerImage = Resources.Slider_Dragger_OnPress;
            this.sldProgress.RailEdgeInset = new Padding(6, 5, 6, 5);
            this.sldProgress.RailImage = Resources.Slider_Rail;
            this.sldProgress.RailPadding = new Padding(0);
            this.sldProgress.RailWidth = 10;
            this.sldProgress.Size = new Size(375, 20);
            this.sldProgress.Value = 0F;
            this.sldProgress.ValueChanged += new ValueChangedEventHandler(this.sldProgress_ValueChanged);
            this.sldProgress.Hover += new HoverEventHandler(this.sldProgress_Hover);
            this.sldProgress.MouseLeave += new EventHandler(this.sldProgress_MouseLeave);
            // 
            // labProgress
            // 
            this.labProgress.Anchor = ((AnchorStyles)((AnchorStyles.Top | AnchorStyles.Right)));
            this.labProgress.Location = new Point(422, 8);
            this.labProgress.Size = new Size(85, 16);
            this.labProgress.Text = "0:00/0:00";
            this.labProgress.TextColor = Color.White;
            this.labProgress.Font = new Font("Microsoft YaHei UI", 9);
            // 
            // btnVolume
            // 
            this.btnVolume.Anchor = ((AnchorStyles)((AnchorStyles.Top | AnchorStyles.Right)));
            this.btnVolume.HoverImage = Resources.Volume_3;
            this.btnVolume.Location = new Point(517, 6);
            this.btnVolume.NormalImage = Resources.Volume_3_OnPress;
            this.btnVolume.PressImage = Resources.Volume_3_OnPress;
            this.btnVolume.Size = new Size(20, 20);
            this.btnVolume.Click += new System.EventHandler(this.btnVolume_Click);
            // 
            // sldVolume
            // 
            this.sldVolume.Anchor = ((AnchorStyles)((AnchorStyles.Top | AnchorStyles.Right)));
            this.sldVolume.DraggerEdgeInset = new Padding(0);
            this.sldVolume.DraggerSize = new Size(17, 15);
            this.sldVolume.HoverDraggerImage = Resources.Volume_Dragger;
            this.sldVolume.Location = new Point(537, 10);
            this.sldVolume.Name = "sldVolume";
            this.sldVolume.NormalDraggerImage = Resources.Volume_Dragger_OnPress;
            this.sldVolume.PressDraggerImage = Resources.Volume_Dragger_OnPress;
            this.sldVolume.RailEdgeInset = new Padding(5, 4, 5, 4);
            this.sldVolume.RailImage = Resources.Volume_Rail;
            this.sldVolume.RailWidth = 9;
            this.sldVolume.Size = new Size(70, 15);
            this.sldVolume.ValueChanged += this.SldVolume_ValueChanged;
            // 
            // btnFullScreen
            // 
            this.btnFullScreen.Anchor = ((AnchorStyles)((AnchorStyles.Top | AnchorStyles.Right)));
            this.btnFullScreen.HoverImage = Resources.Full_Screen;
            this.btnFullScreen.Location = new Point(615, 8);
            this.btnFullScreen.NormalImage = Resources.Full_Screen_OnPress;
            this.btnFullScreen.PressImage = Resources.Full_Screen_OnPress;
            this.btnFullScreen.Size = new Size(15, 17);
            this.btnFullScreen.Click += new System.EventHandler(this.btnFullScreen_Click);



            this.zContainer1.ZControls.Add(this.btnPlay);
            this.zContainer1.ZControls.Add(this.btnPause);
            this.zContainer1.ZControls.Add(this.btnStop);
            this.zContainer1.ZControls.Add(this.sldProgress);
            this.zContainer1.ZControls.Add(this.labProgress);
            this.zContainer1.ZControls.Add(this.btnVolume);
            this.zContainer1.ZControls.Add(this.sldVolume);
            this.zContainer1.ZControls.Add(this.btnFullScreen);


            this.btnPlay.EndInit();
            this.btnPause.EndInit();
            this.btnStop.EndInit();
            this.sldProgress.EndInit();
            this.labProgress.EndInit();
            this.btnVolume.EndInit();
            this.sldVolume.EndInit();
            this.btnFullScreen.EndInit();
        }

        #endregion


        protected override void OnSizeChanged(EventArgs e)
        {
            if (this.WindowState != FormWindowState.Maximized)
            {
                this.isFullscreen = false;
            }
            base.OnSizeChanged(e);
        }

        #region MediaPlayer事件
        private void _mediaPlayer_LengthChanged(object sender, MediaPlayerLengthChangedEventArgs e)
        {
            var timeLength = TimeSpan.FromMilliseconds(e.Length);
            this.labProgress.Text = $"0:00/{(int)timeLength.TotalMinutes}:{timeLength.Seconds:00}";
            this._mediaPlayer.Size(0, ref this.videoWidth, ref this.videoHeight);
            this._mediaPlayer.SetVideoCallbacks(
                new MediaPlayer.LibVLCVideoLockCb((IntPtr opaque, IntPtr planes) =>
                {
                    Monitor.Enter(MainForm._MyLock);
                    System.Runtime.InteropServices.Marshal.FreeHGlobal(this.buffer);//释放缓冲区
                    this.buffer = System.Runtime.InteropServices.Marshal.AllocHGlobal((int)this.videoWidth * (int)this.videoHeight * 3);
                    System.Runtime.InteropServices.Marshal.WriteIntPtr(planes, this.buffer);
                    return IntPtr.Zero;
                }),
                new MediaPlayer.LibVLCVideoUnlockCb((IntPtr opaque, IntPtr picture, IntPtr planes) =>
                {
                    Monitor.Exit(MainForm._MyLock);
                }), new MediaPlayer.LibVLCVideoDisplayCb((IntPtr opaque, IntPtr picture) =>
                {
                    this.Invoke(new Action(this.openGLControl1.DoRender));
                }));
            this._mediaPlayer.SetVideoFormat("RV24", this.videoWidth, this.videoHeight, this.videoWidth * 3);
        }

        private void _mediaPlayer_PositionChanged(object sender, MediaPlayerPositionChangedEventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                this.sldProgress.Value = e.Position;

                var timeLength = TimeSpan.FromMilliseconds(this._mediaPlayer.Length);
                var timePosition = TimeSpan.FromMilliseconds(this._mediaPlayer.Time);


                this.labProgress.Text = $"{(int)timePosition.TotalMinutes}:{timePosition.Seconds:00}/{(int)timeLength.TotalMinutes}:{timeLength.Seconds:00}";


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
        object osdLock = new object();
        Bitmap osdMessage;
        BackgroundWorker osdDelayTask;
        int osdDelay = 2000;
        private void ShowOSD(string message)
        {
            if (this.osdDelayTask != null)
            {
                this.osdDelayTask.CancelAsync();
            }

            lock (this.osdLock)
            {
                if (this.osdMessage != null)
                {
                    this.osdMessage.Dispose();
                    this.osdMessage = null;
                }

                var ff = new FontFamily("Microsoft YaHei UI");
                var f = new Font(ff, 24, FontStyle.Regular, GraphicsUnit.Point);
                var gp = new System.Drawing.Drawing2D.GraphicsPath();
                gp.AddString(message, ff, (int)FontStyle.Regular, 18, Point.Empty, StringFormat.GenericDefault);
                var gpb = gp.GetBounds();

                Bitmap text = new Bitmap((int)gpb.Width + 10, (int)gpb.Height + 10);
                using (var tg = Graphics.FromImage(text))
                {
                    tg.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    tg.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                    tg.FillPath(Brushes.Yellow, gp);
                    tg.Dispose();
                }

                this.osdMessage = text;
            }
            this.Invoke(new Action(this.openGLControl1.DoRender));

            this.osdDelayTask = new BackgroundWorker();
            this.osdDelayTask.WorkerSupportsCancellation = true;
            this.osdDelayTask.DoWork += (sender, e) =>
            {
                Thread.Sleep(this.osdDelay);
                if (((BackgroundWorker)sender).CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
            };
            this.osdDelayTask.RunWorkerCompleted += (sender, e) =>
            {
                if (e.Cancelled)
                {
                    return;
                }
                lock (this.osdLock)
                {
                    if (this.osdMessage != null)
                    {
                        this.osdMessage.Dispose();
                        this.osdMessage = null;
                    }
                }
                this.Invoke(new Action(this.openGLControl1.DoRender));
            };
            this.osdDelayTask.RunWorkerAsync();
        }

        private void _mediaPlayer_Paused(object sender, EventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                this.btnPlay.Visible = true;
                this.btnPause.Visible = false;
            }));

        }


        #endregion

        #region 控制器事件

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
            var timeLength = TimeSpan.FromMilliseconds(this._mediaPlayer.Length);
            var timePosition = TimeSpan.FromMilliseconds(this._mediaPlayer.Length * e.Value);
            this.labProgress.Text = $"{(int)timePosition.TotalMinutes}:{timePosition.Seconds:00}/{(int)timeLength.TotalMinutes}:{timeLength.Seconds:00}";
            this._mediaPlayer.Position = e.Value;
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

        private void btnFullScreen_Click(object sender, EventArgs e)
        {
            this.ToggleFullscreen();
        }

        private void SldVolume_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            this._mediaPlayer.Volume = (int)(e.Value * 100);
            this.ShowOSD($"音量：{e.Value:P0}");
            this.ChangebtnVolumeImage();
        }

        private void btnVolume_Click(object sender, EventArgs e)
        {
            this._mediaPlayer.ToggleMute();
            this.ChangebtnVolumeImage();
        }

        private void zContainer1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Win32Api.ReleaseCapture();
                Win32Api.SendMessage(this.Handle, Win32Api.WM_SYSCOMMAND, Win32Api.SC_MOVE + Win32Api.HTCAPTION, 0);
            }
        }

        private void pMoiveHost_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.TogglePlayOrPause();
                if (e.Clicks == 2)
                {
                    this.ToggleFullscreen();
                }
                Win32Api.ReleaseCapture();
                Win32Api.SendMessage(this.Handle, Win32Api.WM_SYSCOMMAND, Win32Api.SC_MOVE + Win32Api.HTCAPTION, 0);
            }
        }


        Point lastLocation = Point.Empty;
        BackgroundWorker lastWorker = null;
        private void pMoiveHost_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.isFullscreen && this.lastLocation != e.Location)
            {
                Cursor.Show();
                this.zContainer1.Visible = true;
                if (this.lastWorker != null)
                {
                    this.lastWorker.CancelAsync();
                }
                this.lastWorker = new BackgroundWorker();
                this.lastWorker.WorkerSupportsCancellation = true;
                this.lastWorker.DoWork += (ss, ee) =>
                {
                    for (int i = 0; i < 2000; i++)
                    {
                        Thread.Sleep(1);
                        if (((BackgroundWorker)ss).CancellationPending)
                        {
                            ee.Cancel = true;
                            return;
                        }
                    }
                };
                this.lastWorker.RunWorkerCompleted += (ss, ee) =>
                {
                    if (ee.Cancelled || !this.isFullscreen)
                    {
                        return;
                    }
                    this.Invoke(new Action(() =>
                    {
                        Cursor.Hide();
                        this.zContainer1.Visible = false;
                    }));
                };
                this.lastWorker.RunWorkerAsync();
            }

            this.lastLocation = e.Location;
        }

        private void zContainer1_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.lastWorker != null)
            {
                this.lastWorker.CancelAsync();
            }
        }
        #endregion

        #region 菜单事件

        private void miExit_Click(object sender, EventArgs e)
        {
            this._mediaPlayer.Stop();
            Application.Exit();
        }

        private void miOpenFile_Click(object sender, EventArgs e)
        {
            if (this.openFD.ShowDialog() == DialogResult.OK)
            {
                this.Play(this.openFD.FileName);
            }
        }

        private void miHardwareDecoding_Click(object sender, EventArgs e)
        {
            this._mediaPlayer.EnableHardwareDecoding = !this._mediaPlayer.EnableHardwareDecoding;

            this.Replay();
        }

        private void cmsReightMenu_Opening(object sender, CancelEventArgs e)
        {
            this.InitSoundTrackMenuItem();
            this.InitSubtitleMenuItem();
            this.InitHardwareDecodingMenuItem();
        }

        private void InitHardwareDecodingMenuItem()
        {
            this.miHardwareDecoding.Checked = this._mediaPlayer.EnableHardwareDecoding;
        }

        private void miLoadSubtitleFile_Click(object sender, EventArgs e)
        {
            var result = this.openFD.ShowDialog();
            if (result != DialogResult.OK)
            {
                return;
            }

            this._mediaPlayer.AddSlave(MediaSlaveType.Subtitle, this.PathToUri(this.openFD.FileName), true);

        }


        private void miLoadSoundTrackFile_Click(object sender, EventArgs e)
        {
            var result = this.openFD.ShowDialog();
            if (result != DialogResult.OK)
            {
                return;
            }

            this._mediaPlayer.AddSlave(MediaSlaveType.Audio, this.PathToUri(this.openFD.FileName), true);
        }
        #endregion

        /// <summary>
        /// 加载字幕菜单
        /// </summary>
        private void InitSubtitleMenuItem()
        {
            var spuCount = this._mediaPlayer.SpuCount;
            this.miSelectSubtitle.DropDownItems.Clear();
            this.miSelectSubtitle.Enabled = (spuCount > 0);
            if (!this.miSubtitle.Enabled)
            {
                return;
            }


            for (int i = 0; i < spuCount; i++)
            {
                var spu = this._mediaPlayer.SpuDescription[i];


                var mi = new ToolStripMenuItem();

                mi.Text = spu.Name;
                mi.Tag = spu.Id;
                mi.Click += (ss, ee) =>
                {
                    var ssmi = (ToolStripMenuItem)ss;
                    this._mediaPlayer.SetSpu((int)ssmi.Tag);
                };
                mi.Checked = (spu.Id == this._mediaPlayer.Spu);

                this.miSelectSubtitle.DropDownItems.Add(mi);
            }
        }

        /// <summary>
        /// 加载音轨
        /// </summary>
        private void InitSoundTrackMenuItem()
        {
            var stCount = this._mediaPlayer.AudioTrackCount;
            this.miSelectSoundTrack.DropDownItems.Clear();
            this.miSelectSoundTrack.Enabled = (stCount > 0);
            if (!this.miSubtitle.Enabled)
            {
                return;
            }


            for (int i = 0; i < stCount; i++)
            {
                var st = this._mediaPlayer.AudioTrackDescription[i];
                var mi = new ToolStripMenuItem();
                mi.Text = st.Name;
                mi.Tag = st.Id;
                mi.Click += (ss, ee) =>
                {
                    var ssmi = (ToolStripMenuItem)ss;
                    this._mediaPlayer.SetAudioTrack((int)ssmi.Tag);
                };
                mi.Checked = (st.Id == this._mediaPlayer.AudioTrack);

                this.miSelectSoundTrack.DropDownItems.Add(mi);
            }
        }

        #region 方法


        private void Replay()
        {
            this.replayPosition = this._mediaPlayer.Position;
            this._mediaPlayer.Stop();

            this._mediaPlayer.Play();
            this._mediaPlayer.Position = this.replayPosition;
        }
        private void Play(string fileName)
        {
            this._preview.Media = new Media(this._libVLC, this.GetStream(fileName));
            this._mediaPlayer.Media = new Media(this._libVLC, this.GetStream(fileName));
            //this._mediaPlayer.Play();
        }
        /// <summary>
        /// 改变音量按钮图标
        /// </summary>
        private void ChangebtnVolumeImage()
        {
            if (this._mediaPlayer.Mute)
            {
                this.btnVolume.NormalImage = Resources.Volume_Mute_OnPress;
                this.btnVolume.HoverImage = Resources.Volume_Mute;
                this.btnVolume.PressImage = Resources.Volume_Mute_OnPress;
            }
            else
            {
                var index = (int)((this._mediaPlayer.Volume / 100f) / (1f / 3f)) + 1;
                if (index > 3)
                {
                    index = 3;
                }

                this.btnVolume.NormalImage = (Bitmap)Resources.ResourceManager.GetObject($"Volume_{index}_OnPress");
                this.btnVolume.HoverImage = (Bitmap)Resources.ResourceManager.GetObject($"Volume_{index}");
                this.btnVolume.PressImage = (Bitmap)Resources.ResourceManager.GetObject($"Volume_{index}_OnPress");
            }
        }

        /// <summary>
        /// 切换全屏
        /// </summary>
        private void ToggleFullscreen()
        {
            if (this.isFullscreen)
            {
                this.WindowState = this.tempWindowState;
                this.MaximumSize = Screen.FromHandle(this.Handle).WorkingArea.Size;
                this.isFullscreen = false;

                this.openGLControl1.SetBounds(2, 2, this.ClientSize.Width - 4, this.Height - this.zContainer1.Height - 4);
                this.zContainer1.Visible = true;
            }
            else
            {
                this.tempWindowState = this.WindowState;
                this.MaximumSize = Size.Empty;
                this.WindowState = FormWindowState.Maximized;
                this.isFullscreen = true;



                this.openGLControl1.Bounds = this.ClientRectangle;
                this.zContainer1.Visible = false;
            }
        }

        /// <summary>
        /// 切换播放暂停
        /// </summary>
        private void TogglePlayOrPause()
        {
            if (this._mediaPlayer.IsPlaying)
            {
                this._mediaPlayer.Pause();
            }
            else
            {
                this._mediaPlayer.Play();
            }
        }

        /// <summary>
        /// 获取媒体流
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        private Stream GetStream(string filename)
        {
            Stream result = null;
            var ext = System.IO.Path.GetExtension(filename);
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

        /// <summary>
        /// 从ZIP包中获取媒体流
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        private Stream GetZipStream(string filename)
        {
            Stream result = null;
            FileStream zipToOpen = File.OpenRead(filename);
            ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Read);
            var entry = archive.Entries[0];
            result = entry.Open();
            return result;
        }
        #endregion


        private string PathToUri(string path)
        {
            var a = new UriBuilder();
            a.Path = path;
            a.Scheme = Uri.UriSchemeFile;
            return a.Uri.ToString();
        }

        private void openGLControl1_OpenGLDraw(object sender, SharpGL.RenderEventArgs args)
        {
            SharpGL.OpenGL gl = this.openGLControl1.OpenGL;

            //清除深度缓存 
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            gl.MatrixMode(OpenGL.GL_PROJECTION);
            //重置当前指定的矩阵为单位矩阵,将当前的用户坐标系的原点移到了屏幕中心
            gl.LoadIdentity();

            //显示视频画面
            Monitor.Enter(MainForm._MyLock);
            try
            {
                if (this.buffer != IntPtr.Zero)
                {
                    var xz = (float)this.openGLControl1.Width / (float)this.videoWidth;
                    var yz = (float)this.openGLControl1.Height / (float)this.videoHeight;
                    var xyz = 1f;
                    xyz = Math.Min(xz, yz);
                    if (xz < yz)
                    {
                        gl.RasterPos(-1f, (xyz / yz));
                    }
                    else
                    {
                        gl.RasterPos((xyz / xz), -1f);
                    }
                    gl.PixelZoom(xyz, -xyz);
                    gl.DrawPixels((int)this.videoWidth, (int)this.videoHeight, OpenGL.GL_RGB, OpenGL.GL_UNSIGNED_BYTE, this.buffer);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                Monitor.Exit(MainForm._MyLock);
            }

            lock (this.osdLock)
            {
                if (this.osdMessage != null)
                {
                    var textData = this.osdMessage.LockBits(new Rectangle(0, 0, this.osdMessage.Width, this.osdMessage.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);


                    //显示OSD
                    var x = (2f / this.openGLControl1.Width) * 50f - 1f;
                    var y = (2f / this.openGLControl1.Height) * (this.openGLControl1.Height - 50f) - 1f;
                    gl.RasterPos(x, y);
                    gl.PixelZoom(1, -1);
                    gl.DrawPixels(this.osdMessage.Width, this.osdMessage.Height, OpenGL.GL_BGRA, OpenGL.GL_UNSIGNED_BYTE, textData.Scan0);
                    this.osdMessage.UnlockBits(textData);


                    //gl.DrawText(50, this.openGLControl1.Height - 18, 1, 1, 0, "Microsoft YaHei UI", 24, this.osdMessage);
                }
            }

            gl.Flush();   //强制刷新
        }

        private void openGLControl1_OpenGLInitialized(object sender, EventArgs e)
        {
            OpenGL gl = this.openGLControl1.OpenGL;
            gl.Enable(OpenGL.GL_BLEND);
            gl.BlendFunc(OpenGL.GL_SRC_ALPHA, OpenGL.GL_ONE_MINUS_SRC_ALPHA);
            gl.ClearColor(0, 0, 0, 0);
        }

        private void openGLControl1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.TogglePlayOrPause();
                if (e.Clicks == 2)
                {
                    this.ToggleFullscreen();
                }
                Win32Api.ReleaseCapture();
                Win32Api.SendMessage(this.Handle, Win32Api.WM_SYSCOMMAND, Win32Api.SC_MOVE + Win32Api.HTCAPTION, 0);
            }
        }

        private void openGLControl1_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.isFullscreen && this.lastLocation != e.Location)
            {
                Cursor.Show();
                this.zContainer1.Visible = true;
                if (this.lastWorker != null)
                {
                    this.lastWorker.CancelAsync();
                }
                this.lastWorker = new BackgroundWorker();
                this.lastWorker.WorkerSupportsCancellation = true;
                this.lastWorker.DoWork += (ss, ee) =>
                {
                    for (int i = 0; i < 2000; i++)
                    {
                        Thread.Sleep(1);
                        if (((BackgroundWorker)ss).CancellationPending)
                        {
                            ee.Cancel = true;
                            return;
                        }
                    }
                };
                this.lastWorker.RunWorkerCompleted += (ss, ee) =>
                {
                    if (ee.Cancelled || !this.isFullscreen)
                    {
                        return;
                    }
                    this.Invoke(new Action(() =>
                    {
                        Cursor.Hide();
                        this.zContainer1.Visible = false;
                    }));
                };
                this.lastWorker.RunWorkerAsync();
            }

            this.lastLocation = e.Location;
        }
    }
}
