using System;
using System.Globalization;
using System.Windows.Data;

namespace TestExtractor.ExtractorUi.Converters
{
    /// <summary>
    ///     A Number to Boolean Converter Class
    ///     Implements Interface : <see cref="IValueConverter" />
    /// </summary>
    internal class NumberToBooleanConverter : IValueConverter
    {
        /// <summary>
        ///     Implements <see cref="IValueConverter.Convert" />
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int number = 0;

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

        /// <summary>
        ///     Implements <see cref="IValueConverter.ConvertBack" />
        /// </summary>
        /// <remarks>
        ///     Throws a <see cref="NotImplementedException" />
        /// </remarks>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("Not immplemented Exception");
        }
    }
}