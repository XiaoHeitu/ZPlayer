using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
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
            InitializeComponent();

            this.LoadFile();
        }

        public void LoadFile()
        {
            var result = open.ShowDialog();
            if ((result ?? false))
            {

                this.MyElement.Source = new Uri($"http://localhost:56153/ZFile/Open?file={System.Web.HttpUtility.UrlEncode(open.FileName)}");
            }
        }
    }
}
