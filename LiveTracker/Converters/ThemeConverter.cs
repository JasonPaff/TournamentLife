using System;
using System.Globalization;
using System.Windows.Data;

namespace Tournament_Life.Converters
{
    public class ThemeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null)
                return "MaterialDark";

            if (value is false)
                return "Office2013LightGray";

            return "MaterialDark";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
