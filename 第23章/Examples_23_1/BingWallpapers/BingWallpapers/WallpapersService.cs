using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BingWallpapers
{
    /// <summary>
    /// 获取壁纸信息的服务类
    /// </summary>
    public class WallpapersService
    {
        // 距离今天的天数，表示获取壁纸的时间
        private int selectedDay;
        // 国家代码
        private List<string> countries = new List<string>(new string[] {
            "zh-CN", "fr-FR",  "de-DE","en-US",  "ja-JP","en-GB"});
        // 需要获取的请求次数
        private int http_times;
        // 总的请求次数
        private int http_times_all;
        // 是否已经开始下载了
        private bool downloading = false;
        // 已经下载的图片信息
        private Dictionary<int, List<PictureInfo>> allHaveDownloadPictures;
        // 完成进度事件
        public EventHandler<ProgressEventArgs> GetOneDayWallpapersProgressEvent;
        // 触发完成进度事件的方法
        private void OnGetOneDayWallpapersProgressEvent(ProgressEventArgs progressEventArgs)
        {
            if (GetOneDayWallpapersProgressEvent != null)
            {
                GetOneDayWallpapersProgressEvent.Invoke(this, progressEventArgs);
            }
        }
        private static WallpapersService _Current;
        // 单例对象
        public static WallpapersService Current
        {
            get
            {
                if (_Current == null)
                    _Current = new WallpapersService();
                return _Current;
            }
        }
        // 初始化对象
        private WallpapersService()
        {
            allHaveDownloadPictures = new Dictionary<int, List<PictureInfo>>();
        }
        /// <summary>
        /// 获取所选时间的图片
        /// </summary>
        /// <param name="selectedDay">表示距离今天的时间，0表示今天，1表示昨天……</param>
        public void GetOneDayWallpapers(int day)
        {
            // 如果当前正在下载图片则跳出该方法的调用
            if (downloading) return;
            // 把当前的服务标志位正在下载
            downloading = true;
            selectedDay = day;
            // 如果当前并没有该日期的数据则需要在字典对象里面添加上
            if (!allHaveDownloadPictures.Keys.Contains(selectedDay))
            {
                allHaveDownloadPictures.Add(selectedDay, new List<PictureInfo>());
            }
            // 拼接网络请求地址
            string format = "http://appserver.m.bing.net/BackgroundImageService/TodayImageService.svc/GetTodayImage?dateOffset=-{0}&urlEncodeHeaders=true&osName=windowsPhone&osVersion=8.10&orientation=480x800&deviceName=WP8Device&mkt={1}";
            http_times_all = http_times = countries.Count<string>();
            foreach (string country in countries)
            {
                // 判断该请求是否已经请求过,通过其所对应的日期和国家
                if (allHaveDownloadPictures[selectedDay].Select(item => item.countryCode == country).Count() == 0)
                {
                    //图片下载url
                    string bingUrlFmt = string.Format(format, selectedDay, country);
                    //开始下载
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(bingUrlFmt);
                    request.Method = "GET";
                    request.BeginGetResponse(result => this.responseHandler(result, request, country, bingUrlFmt), null);
                }
                else
                {
                    // 如果壁纸信息已经获取过则不要再重复获取，直接返回进度信息
                    SetProgress();
                }
            }
        }
        // 获取的壁纸图片信息回调方法
        private void responseHandler(IAsyncResult asyncResult, HttpWebRequest request, string myloc, string _imgUri)
        {
            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)request.EndGetResponse(asyncResult);
            }
            catch (Exception e)
            {
                downloading = false;
                // 返回异常信息
                OnGetOneDayWallpapersProgressEvent(
                    new ProgressEventArgs
                    {
                        IsException = true,
                        Complete = false,
                        ExceptionInfo = e.Message,
                        ProgressValue = 0,
                        Pictures = null
                    });
                return;
            }
            if (request.HaveResponse)
            {
                // 图片的热点说明信息
                List<string> _hotspot = new List<string>();
                string _imgTitle = "";
                // 通过HTTP请求头传输图片相关的信息
                foreach (string str in response.Headers.AllKeys)
                {
                    string str2 = str;
                    string str3 = response.Headers[str];
                    // 获取图片的说明信息和版权信息
                    if (str2.Contains("Image-Info-Credit"))
                    {
                        _imgTitle = WebUtility.UrlDecode(str3);
                    }
                    // 获取图片的热点介绍信息
                    else if (str2.Contains("Image-Info-Hotspot-"))
                    {
                        string[] strArray = WebUtility.UrlDecode(str3).Replace(" ", "").Split(new char[] { ';' });
                        _hotspot.AddRange(strArray);
                    }
                }
                PictureInfo info = new PictureInfo(myloc, _imgTitle, _imgUri);
                info.hotspot = _hotspot;
                allHaveDownloadPictures[selectedDay].Add(info);
            }
            Debug.WriteLine("allHaveDownloadPictures[selectedDay].Count:" + allHaveDownloadPictures[selectedDay].Count);
            // 返回进度信息
            SetProgress();
        }
        // 返回进度的信息
        private void SetProgress()
        {
            http_times--;
            bool finish = http_times == 0;
            if (finish)
                downloading = false;
            // 返回结果
            OnGetOneDayWallpapersProgressEvent(
           new ProgressEventArgs
           {
               IsException = false,
               Complete = finish,
               ExceptionInfo = "",
               ProgressValue = (int)(((float)(http_times_all - http_times) / (float)http_times_all) * 100),
               Pictures = allHaveDownloadPictures[selectedDay]
           });
        }
    }
}
