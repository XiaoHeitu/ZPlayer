using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vlc.DotNet.Core;
using Vlc.DotNet.Core.Interops;
using Vlc.DotNet.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        OpenFileDialog openFD = new OpenFileDialog();

        // List of controls used to store those controls that will be playing
        private readonly VlcControl vlcControl = null;

        // Extra parameters to pass to the viewer media. I found a 2 seconds buffer cache makes playing much more stable.
        private const string StreamParams = ":network-caching=2000";

        // My path to VLC folder. You can Set your own.
        private string vlcLibraryPath;

        // Count number of clicks on any player.
        //private int numberOfClicks;

        /// <summary>
        /// Initializes a new instance of the Form1 class
        /// </summary>
        public Form1()
        {
            this.InitializeComponent();

            var currentAssembly = Assembly.GetEntryAssembly();
            var currentDirectory = new FileInfo(currentAssembly.Location).DirectoryName;
            // Default installation path of VideoLAN.LibVLC.Windows
            this.vlcLibraryPath = Path.Combine(currentDirectory, "libvlc", IntPtr.Size == 4 ? "win-x86" : "win-x64");


            this.vlcControl = new VlcControl();
            this.vlcControl.BeginInit();
            this.vlcControl.VlcLibDirectory = new DirectoryInfo(this.vlcLibraryPath);
            this.vlcControl.Size = new Size(681, 400);
            this.vlcControl.Location = new Point(13, 13);
            this.vlcControl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.vlcControl.EndInit();
            this.Controls.Add(this.vlcControl);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.openFD.ShowDialog() == DialogResult.OK)
            {
                this.vlcControl.SetMedia(this.openFD.FileName, ":network-caching=2000");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.vlcControl.Play();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.vlcControl.Pause();
        }
    }
}
