using System;
using System.Globalization;
using System.Windows.Data;

namespace Tournament_Life.Converters
{
    public class DateToDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null) return "1/1/1111";

            var a = DateTime.Parse(value.ToString(), new CultureInfo("en-US"));
            var b = new DateTime(a.Year, a.Month, a.Day, 0, 0, 0);

            return b.ToShortDateString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (DateTime)value;
        }
    }
}
