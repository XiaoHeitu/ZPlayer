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

        // Extra parameters to pass to the viewer media. I found a 2 seconds buffer cache makes playing much more stable.
        private const string StreamParams = ":network-caching=2000";

        // My path to VLC folder. You can Set your own.
        //private string vlcLibraryPath;

        // Count number of clicks on any player.
        //private int numberOfClicks;

        /// <summary>
        /// Initializes a new instance of the Form1 class
        /// </summary>
        public Form1()
        {
            this.InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.openFD.ShowDialog() == DialogResult.OK)
            {
                this.vlcControl1.SetMedia(this.GetStream(this.openFD.FileName), ":network-caching=2000");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.vlcControl1.Play();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.vlcControl1.Pause();
        }

        private void vlcControl1_VlcLibDirectoryNeeded(object sender, VlcLibDirectoryNeededEventArgs e)
        {
            var currentAssembly = Assembly.GetEntryAssembly();
            var currentDirectory = new FileInfo(currentAssembly.Location).DirectoryName;
            // Default installation path of VideoLAN.LibVLC.Windows
            var vlcLibraryPath = Path.Combine(currentDirectory, "libvlc", IntPtr.Size == 4 ? "win-x86" : "win-x64");
            e.VlcLibDirectory = new DirectoryInfo(vlcLibraryPath);
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
    }
}
