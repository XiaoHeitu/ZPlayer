using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            Microsoft.DirectX.AudioVideoPlayback.Video video = Microsoft.DirectX.AudioVideoPlayback.Video.FromUrl(new Uri("http://localhost:20813/ZFile/Open?file=d:\\1.zip"));
            video.Owner = this.panel1;
            video.Play();
        }
    }
}
