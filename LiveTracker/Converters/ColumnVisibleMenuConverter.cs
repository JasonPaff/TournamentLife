using System;
using System.Globalization;
using System.Windows.Data;

namespace Tournament_Life.Converters
{
    public class ColumnVisibleMenuConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is not null && !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is not null && (bool)value;
        }
    }
}
