using System;
using System.Globalization;
using System.Windows.Data;

namespace Tournament_Life.Converters
{
    public class DateToTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null)
                return "12:00 AM";

            var a = DateTime.Parse(value.ToString(), new CultureInfo("en-US"));
            var b = new DateTime(1001, 1, 1, a.Hour, a.Minute, 0);

            return b.ToString("h:mm tt", new CultureInfo("en-US"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (DateTime)value;
        }
    }
}
