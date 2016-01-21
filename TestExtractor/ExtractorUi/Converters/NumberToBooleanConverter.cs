using System;
using System.Windows.Data;

namespace TestExtractor.ExtractorUi.Converters
{
    internal class NumberToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var number = 0;

            try
            {
                number = int.Parse(string.Format("{0}", value));
            }
            catch
            {
            }

            if (number == 0)
            {
                return false;

            }
            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException("Not immplemented Exception");
        }
    }
}
