using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace BingWallpapers
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DayPicturesPage : Page
    {
        public DayPicturesPage()
        {
            this.InitializeComponent();
            if (App.IsHardwareButtonsAPIPresent)
            {
                backButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                backButton.Visibility = Visibility.Visible;
            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
            }
        }

        // 进入页面即开始加载网络的壁纸图片和信息
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null && e.Parameter is int)
            {
                // 订阅进度事件的处理程序
                WallpapersService.Current.GetOneDayWallpapersProgressEvent += OnOneDayWallpapersProgressEvent;
                // 调用壁纸请求服务类来获取壁纸信息
                WallpapersService.Current.GetOneDayWallpapers((int)e.Parameter);
            }
        }
        // 离开当前的页面则移除订阅的进度事件
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            WallpapersService.Current.GetOneDayWallpapersProgressEvent -= OnOneDayWallpapersProgressEvent;
            base.OnNavigatedFrom(e);
        }
        // 进度事件的处理程序
        private async void OnOneDayWallpapersProgressEvent(object sender, ProgressEventArgs e)
        {
            await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                if (e.IsException)
                {
                    // 如果发生异常则显示异常信息
                    info.Text = "获取图片异常：" + e.ExceptionInfo;
                }
                else
                {
                    // 正常返回设置进度条的值
                    progress.Value = e.ProgressValue;
                    // 进度完整
                    if (e.Complete)
                    {
                        // 把显示进度的面板隐藏
                        tips.Visibility = Visibility.Collapsed;
                        // 把壁纸信息绑定到列表中
                        pictureList.ItemsSource = e.Pictures;
                        Debug.WriteLine("e.Pictures.Count:" + e.Pictures.Count);
                    }
                }
            });

        }
        // 查看按钮的事件处理程序
        private async void view_Click(object sender, RoutedEventArgs e)
        {
            PictureInfo pictureInfo = (sender as AppBarButton).DataContext as PictureInfo;
            // 在浏览器打开壁纸
            await Launcher.LaunchUriAsync(pictureInfo.imageUri);
        }
        // 保存按钮的事件处理程序
        private async void save_Click(object sender, RoutedEventArgs e)
        {
            PictureInfo pictureInfo = (sender as AppBarButton).DataContext as PictureInfo;
            List<Byte> allBytes = new List<byte>();
            // 把壁纸的图片文件保存到当前的应用文件里面
            using (var response = await HttpWebRequest.Create(pictureInfo.imageUri).GetResponseAsync())
            {
                using (Stream responseStream = response.GetResponseStream())
                {

                    byte[] buffer = new byte[4000];
                    int bytesRead = 0;
                    while ((bytesRead = await responseStream.ReadAsync(buffer, 0, 4000)) > 0)
                    {
                        allBytes.AddRange(buffer.Take(bytesRead));
                    }
                }
            }
            var file = await ApplicationData.Current.LocalFolder.CreateFileAsync(
                        "bingPicture" + DateTime.Now.Ticks + ".jpg", CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteBytesAsync(file, allBytes.ToArray());
        }
    }
}
