using BiliSpirit.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace BiliSpirit.Converters
{
    internal class HisCoverConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is HistoryList history)
            {
                if (string.IsNullOrEmpty(history.cover))
                {
                    if (history.covers != null)
                    {
                        return history.covers[0];
                    }
                    else
                    {
                        return null;
                    }
                }

                return history.cover;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
