using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;

namespace BiliSpirit.Common
{
    public static class ImageRadius
    {
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.RegisterAttached("CornerRadius",
            typeof(int), typeof(ImageRadius),
            new FrameworkPropertyMetadata(0, OnCornerRadiusPropertyChanged));

        static int rad = 0;
        private static void OnCornerRadiusPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            Image image = sender as Image;
            image.SizeChanged += Image_SizeChanged;
            rad = (int)e.NewValue;

           
        }

        private static void Image_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            RectangleGeometry geometry = new RectangleGeometry();
            geometry.Rect = new Rect(0, 0, e.NewSize.Width, e.NewSize.Height);
            geometry.RadiusX = rad;
            geometry.RadiusY = rad;

            if (sender is Image image)
            {
                image.Clip = geometry;
            }
        }

        public static int GetCornerRadius(DependencyObject dp)
        {
            return (int)dp.GetValue(CornerRadiusProperty);
        }

        public static void SetCornerRadius(DependencyObject dp, int value)
        {
            dp.SetValue(CornerRadiusProperty, value);
        }
    }
}
