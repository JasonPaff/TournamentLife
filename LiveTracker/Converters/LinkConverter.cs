using System;
using System.Globalization;
using System.Windows.Data;

namespace Tournament_Life.Converters
{
    public class LinkConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string a = "";

            if (value is null) return a;

            if (value.ToString().Length > 0) a = "Yes";

            return a;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
