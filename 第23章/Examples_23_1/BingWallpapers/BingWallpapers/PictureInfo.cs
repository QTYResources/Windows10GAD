using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace BingWallpapers
{
    /// <summary>
    /// 壁纸图片信息类
    /// </summary>
    public class PictureInfo
    {
        // 壁纸的热点说明信息
        public List<string> hotspot { get; set; }
        // 壁纸的主题
        public string imgTitle { get; set; }
        // 壁纸图片的地址
        public Uri imageUri { get; set; }
        // 壁纸位图对象
        public BitmapImage image { get; set; }
        // 国家代码
        public string countryCode { get; set; }
        // 壁纸图片信息类的初始化
        public PictureInfo(string _countryCode, string _imgTitle, string _imgUri)
        {
            countryCode = _countryCode;
            imgTitle = _imgTitle;
            imageUri = new Uri(_imgUri);
        }
    }
}
