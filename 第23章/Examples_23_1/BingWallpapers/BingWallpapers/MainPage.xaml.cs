using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace BingWallpapers
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private string TodayPictureUri = "http://appserver.m.bing.net/BackgroundImageService/TodayImageService.svc/GetTodayImage?dateOffset=-0&urlEncodeHeaders=true&osName=windowsphone&osVersion=8.10&orientation=480x800&deviceName=WP8Device&mkt=zh-CN";
        private string TodayPictureUri_Big = "http://appserver.m.bing.net/BackgroundImageService/TodayImageService.svc/GetTodayImage?dateOffset=-0&urlEncodeHeaders=true&osName=windowsphone&osVersion=8.10&orientation=1024x768&deviceName=WP8Device&mkt=zh-CN";
        public MainPage()
        {
            this.InitializeComponent();

            Window.Current.SizeChanged += Current_SizeChanged;
        }

        private void Current_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            if (e.Size.Width <= 500)
            {
                background.UriSource = new Uri(TodayPictureUri);
            }
            else
            {
                background.UriSource = new Uri(TodayPictureUri_Big);
            }
        }

        // 进入当前的页面
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            background.UriSource = new Uri(TodayPictureUri);
        }
        // 查看今天壁纸
        private void today_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(DayPicturesPage), 0);
        }
        // 查看昨天壁纸
        private void yesterday_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(DayPicturesPage), 1);
        }
        // 查看两天前壁纸
        private void twodayago_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(DayPicturesPage), 2);
        }
        // 查看三天前壁纸
        private void threedayago_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(DayPicturesPage), 3);
        }
        // 查看更早壁纸
        private void other_Click(object sender, RoutedEventArgs e)
        {
            showMorePicture.Begin();
        }
        // 查看更早壁纸，减少天数的图标按钮事件
        private void minus_bar_Click(object sender, RoutedEventArgs e)
        {
            int day = Int32.Parse(dayNumber.Text);
            if (day > 0)
            {
                day--;
                dayNumber.Text = day.ToString();
            }
        }
        // 查看更早壁纸，增加天数的图标按钮事件
        private void plus_bar_Click(object sender, RoutedEventArgs e)
        {
            int day = Int32.Parse(dayNumber.Text);
            day++;
            dayNumber.Text = day.ToString();

        }
        // 前往查看自定义天数的壁纸
        private void go_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(DayPicturesPage), Int32.Parse(dayNumber.Text));
        }
    }
}
