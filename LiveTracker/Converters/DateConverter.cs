using System;
using System.Globalization;
using System.Windows.Data;

namespace Tournament_Life.Converters
{
    public class DateConverter : IValueConverter
    {
        public string Category { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime result;
            string MyString = value.ToString();
            DateTime.TryParse(MyString, out result);

            if (Category == "Year")
            {
                if ((DateTime.Compare(new DateTime(2019, 01, 01), result)) <= 0 && (DateTime.Compare(new DateTime(2019, 12, 31), result)) >= 0)
                {
                    return "Year 2019";
                }
                if ((DateTime.Compare(new DateTime(2020, 01, 01), result)) <= 0 && (DateTime.Compare(new DateTime(2020, 12, 31), result)) >= 0)
                {
                    return "Year 2020";
                }
                else if ((DateTime.Compare(new DateTime(2021, 01, 01), result)) <= 0 && (DateTime.Compare(new DateTime(2021, 12, 31), result)) >= 0)
                {
                    return "Year 2021";
                }
                else if ((DateTime.Compare(new DateTime(2022, 01, 01), result)) <= 0 && (DateTime.Compare(new DateTime(2022, 12, 31), result)) >= 0)
                {
                    return "Year 2022";
                }
                else if ((DateTime.Compare(new DateTime(2023, 01, 01), result)) <= 0 && (DateTime.Compare(new DateTime(2023, 12, 31), result)) >= 0)
                {
                    return "Year 2023";
                }
                else if ((DateTime.Compare(new DateTime(2024, 01, 01), result)) <= 0 && (DateTime.Compare(new DateTime(2024, 12, 31), result)) >= 0)
                {
                    return "Year 2024";
                }
                else if ((DateTime.Compare(new DateTime(2025, 01, 01), result)) <= 0 && (DateTime.Compare(new DateTime(2025, 12, 31), result)) >= 0)
                {
                    return "Year 2025";
                }
            }

            if (Category == "Month")
            {
                switch(result.Month)
                {
                        case 1:
                        return "January";
                        case 2:
                        return "February";
                        case 3:
                        return "March";
                        case 4:
                        return "April";
                        case 5:
                        return "May";
                        case 6:
                        return "June";
                        case 7:
                        return "July";
                        case 8:
                        return "August";
                        case 9:
                        return "September";
                        case 10:
                        return "October";
                        case 11:
                        return "November";
                        case 12:
                        return "December";
                }
            }

            return "Old Ass Date";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
