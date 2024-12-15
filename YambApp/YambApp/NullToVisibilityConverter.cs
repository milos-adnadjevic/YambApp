using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;
using FsCheck;
using NUnit.Core;

namespace YambApp
{
    public class NullToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Check if value is null or empty
            if (value == null || string.IsNullOrEmpty(value as string))
            {
                return Visibility.Collapsed;  // Hide if value is null or empty
            }
            else
            {
                return Visibility.Visible;    // Show if value is not null or empty
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();     
        }
    }

}
