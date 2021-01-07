using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace BingWallpapers
{
    public class CountryNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string countryName = value.ToString();
            switch (value.ToString())
            {
                case "zh-CN":
                    countryName = "中国";
                    break;
                case "fr-FR":
                    countryName = "法国";
                    break;
                case "de-DE":
                    countryName = "德国";
                    break;
                case "en-US":
                    countryName = "美国";
                    break;
                case "ja-JP":
                    countryName = "日本";
                    break;
                case "en-GB":
                    countryName = "英国";
                    break;
            }
            return countryName;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
