using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;
using Unosquare.FFME.Common;

namespace BiliSpirit.Windows
{
    /// <summary>
    /// PlayerWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PlayerWindow : Window
    {
        public PlayerWindow(IMediaInputStream stream)
        {
            InitializeComponent();
            VideoStream = stream;
            Loaded += PlayerWindow_Loaded;
        }

        private async void PlayerWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //await Media.Open(new Uri(VideoAdress));
            await Media.Open(VideoStream);
            Media.Play();
        }

        public IMediaInputStream VideoStream { get; set; }
    }
}
