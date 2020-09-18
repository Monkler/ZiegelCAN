namespace ZiegelCAN
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Windows;
    using System.Windows.Data;

    public class DataToStringConverter : IValueConverter
    {
        public DataToStringConverter() { }

        public object Convert(object values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values is byte[] data)
            {
                string res = "";
                foreach (byte element in data)
                {
                    res += $"{element:X2} ";
                }

                return res;
            }

            return Binding.DoNothing;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
