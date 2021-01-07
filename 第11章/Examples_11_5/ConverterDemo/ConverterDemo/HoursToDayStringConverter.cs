using System;
using System.Globalization;
using Windows.UI.Xaml.Data;

namespace ConverterDemo
{
    public class HoursToDayStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {

            if (Int16.Parse(value.ToString()) < 12)
            {
                return "尊敬的用户，上午好。";
            }
            else if (Int16.Parse(value.ToString()) > 12)
            {
                return "尊敬的用户，下午好。";
            }
            else
            {
                return "尊敬的用户，中午好。";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return DateTime.Now.Hour;
        }
    }
}
