namespace ZiegelCAN
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Windows.Data;

    public class LastItemConverter : IMultiValueConverter
    {
        public LastItemConverter() { }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            IEnumerable<Message> items = values[0] as IEnumerable<Message>;
            if (items != null)
            {
                return items.LastOrDefault();
            }

            return Binding.DoNothing;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
