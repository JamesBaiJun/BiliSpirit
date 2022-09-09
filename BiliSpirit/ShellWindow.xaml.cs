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
    /// ShellWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ShellWindow : Window
    {
        public ShellWindow()
        {
            InitializeComponent();
            Closed += (s, e) => { Application.Current.Shutdown(); };
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void minBtn_Click(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;

        private void cloBtn_Click(object sender, RoutedEventArgs e) => Close();

        private void Window_MouseLeave(object sender, MouseEventArgs e)
        {
            if (Top == 0)
            {
                //DoubleAnimation da = new DoubleAnimation(-Height + 10, new Duration(TimeSpan.FromMilliseconds(150)));
                //BeginAnimation(TopProperty, da);
                //da.Completed += (s, e) =>
                //{
                Top = -Height + 10;
                //};
            }
        }

        private void Window_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Top == -Height + 10)
            {
                Top = 0;
            }
        }

        private void miniBtn_Click(object sender, RoutedEventArgs e)
        {
            Topmost = (bool)miniBtn.IsChecked;
        }
    }
}
