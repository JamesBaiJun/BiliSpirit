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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BiliSpirit
{
    /// <summary>
    /// SideWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SideWindow : Window
    {
        public SideWindow()
        {
            InitializeComponent();

            Height = SystemParameters.WorkArea.Height;
            Left = SystemParameters.WorkArea.Width - Width;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Owner.Show();
            Hide();
        }

        private void refreBtn_Click(object sender, RoutedEventArgs e)
        {
            scroll.ScrollToVerticalOffset(0);
        }

        private void Window_MouseEnter(object sender, MouseEventArgs e)
        {
            DoubleAnimation da = new DoubleAnimation(SystemParameters.WorkArea.Width - Width, new Duration(TimeSpan.FromMilliseconds(150)));
            BeginAnimation(LeftProperty, da);
            DoubleAnimation daOpacity = new DoubleAnimation(1, new Duration(TimeSpan.FromMilliseconds(150)));
            BeginAnimation(OpacityProperty, daOpacity);
            //Left = SystemParameters.WorkArea.Width - Width;
        }

        private void Window_MouseLeave(object sender, MouseEventArgs e)
        {
            DoubleAnimation da = new DoubleAnimation(SystemParameters.WorkArea.Width - 20, new Duration(TimeSpan.FromMilliseconds(250)));
            BeginAnimation(LeftProperty, da);
            DoubleAnimation daOpacity = new DoubleAnimation(0.2, new Duration(TimeSpan.FromMilliseconds(250)));
            BeginAnimation(OpacityProperty, daOpacity);

            //Opacity = 0.2;
            //Left = SystemParameters.WorkArea.Width - 30;
        }
    }
}
