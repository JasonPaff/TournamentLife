using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Tournament_Life.Converters
{
    public class BoolToVisibleConverter : IValueConverter
    {
        public BoolToVisibleConverter()
        {
            TrueValue = Visibility.Visible;
            FalseValue = Visibility.Collapsed;
        }

        public Visibility TrueValue { get; set; }

        public Visibility FalseValue { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool flag = false;
            if (value is bool boolean)
            {
                flag = boolean;
            }
            else if (value is bool?)
            {
                bool? nullable = (bool?)value;
                flag = nullable ?? false;
            }
            return flag ? TrueValue : FalseValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (Equals(value, TrueValue))
                return true;
            if (Equals(value, FalseValue))
                return false;
            return null;
        }

    }
}