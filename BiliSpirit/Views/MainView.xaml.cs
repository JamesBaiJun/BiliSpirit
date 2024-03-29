﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BiliSpirit.Views
{
    /// <summary>
    /// MainView.xaml 的交互逻辑
    /// </summary>
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
        }

        private void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.VerticalOffset > 0)
            {
                topGrid.Visibility = Visibility.Collapsed;
                scolledTopGrid.Visibility = Visibility.Visible;
            }
            else if (e.VerticalOffset == 0)
            {
                topGrid.Visibility = Visibility.Visible;
                scolledTopGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AnimaScroll.ScrollToVerticalOffset(0);
        }
    }
}
