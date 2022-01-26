using System;
using System.Globalization;
using System.Windows.Data;

namespace LiveTracker.Converters
{
    public class BoolToYesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is true)
                return "Yes";

            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
