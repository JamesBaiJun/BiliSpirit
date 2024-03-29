﻿using BiliSpirit.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace BiliSpirit.Converters
{
    internal class HisCoverConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is HistoryList history)
            {
                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.DecodePixelWidth = 120; // 设置解码后图像的宽度，图像变小，解析变快

                if (string.IsNullOrEmpty(history.cover))
                {
                    if (history.covers != null)
                    {
                        bitmapImage.UriSource = new Uri(history.covers[0], UriKind.Absolute);
                        bitmapImage.EndInit();

                        return bitmapImage;
                    }
                    else
                    {
                        return null;
                    }
                }

                bitmapImage.UriSource = new Uri(history.cover, UriKind.Absolute);
                bitmapImage.EndInit();

                return bitmapImage;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
