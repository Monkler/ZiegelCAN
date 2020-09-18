namespace ZiegelCAN
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Windows;
    using System.Windows.Data;

    public class CountToVisibleConverter : IValueConverter
    {
        public CountToVisibleConverter() { }

        public object Convert(object values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values is int count)
            {
                if (count == 0)
                {
                    return Visibility.Collapsed;
                }
                else
                {
                    return Visibility.Visible;
                }
            }

            return Binding.DoNothing;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
