using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ZPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        OpenFileDialog open = new OpenFileDialog();
        public MainWindow()
        {
            
            this.InitializeComponent();

            var currentAssembly = Assembly.GetEntryAssembly();
            var currentDirectory = new FileInfo(currentAssembly.Location).DirectoryName;
            // Default installation path of VideoLAN.LibVLC.Windows
            var libDirectory = new DirectoryInfo(System.IO.Path.Combine(currentDirectory, "libvlc", IntPtr.Size == 4 ? "win-x86" : "win-x64"));

            this.vlcControl1.SourceProvider.CreatePlayer(libDirectory/* pass your player parameters here */);
            this.vlcControl1.SourceProvider.MediaPlayer.Play(new Uri("http://download.blender.org/peach/bigbuckbunny_movies/big_buck_bunny_480p_surround-fix.avi"));
        }

        public void LoadFile()
        {            
            //var result = open.ShowDialog();
            //if ((result ?? false))
            //{
            //    this.MyElement.Source = new Uri($"http://localhost:20813/ZFile/Open?file={System.Web.HttpUtility.UrlEncode(open.FileName)}");
                
            //}
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.LoadFile();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {            
            //this.MyElement.Play();
        }

        private void Window_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            //var cur = this.MyElement.Volume;
            //var value = cur + ((double)e.Delta / 10000 * 5);
            //if (value <= 0)
            //{
            //    value = 0;
            //}
            //if(value >= 1)
            //{
            //    value = 1;
            //}
            //this.MyElement.Volume = value;
        }

        private void MyElement_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            //MessageBox.Show(e.ErrorException.Message, "出错了", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
