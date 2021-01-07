using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace PositionChangedDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Geolocator geolocator = null;
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
            geolocator = new Geolocator();
            geolocator.MovementThreshold = 5;
            geolocator.StatusChanged += geolocator_StatusChanged;
            geolocator.PositionChanged += geolocator_PositionChanged;
            myMap.MapServiceToken = "AuthenticationToken";
        }

        private async void getlocation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Geoposition pos = await geolocator.GetGeopositionAsync();
                myMap.Center = pos.Coordinate.Point;
                tbLatitude.Text = "纬度:" + pos.Coordinate.Point.Position.Latitude;
                tbLongitude.Text = "经度:" + pos.Coordinate.Point.Position.Longitude;
                tbAccuracy.Text = "准确性:" + pos.Coordinate.Accuracy;
            }
            catch (System.UnauthorizedAccessException)
            {
                // 服务被禁用异常
                tbLatitude.Text = "No data";
                tbLongitude.Text = "No data";
                tbAccuracy.Text = "No data";
            }
            catch (TaskCanceledException)
            {
                // 请求被取消
                tbLatitude.Text = "Cancelled";
                tbLongitude.Text = "Cancelled";
                tbAccuracy.Text = "Cancelled";
            }
        }

        async void geolocator_StatusChanged(Geolocator sender, StatusChangedEventArgs args)
        {
            await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                // 状态信息
                tbStatus.Text = "状态改变信息:" + GetStatusString(args.Status);

            });
        }


        private string GetStatusString(PositionStatus status)
        {
            var strStatus = "";

            switch (status)
            {
                case PositionStatus.Ready:
                    strStatus = "地理位置信息可用";
                    break;

                case PositionStatus.Initializing:
                    strStatus = "地理位置服务正在初始化";
                    break;

                case PositionStatus.NoData:
                    strStatus = "地理位置服务不可用";
                    break;

                case PositionStatus.Disabled:
                    strStatus = "地理位置服务被用户禁用";
                    break;

                case PositionStatus.NotInitialized:
                    strStatus = "未请求地理位置信息";
                    break;

                case PositionStatus.NotAvailable:
                    strStatus = "设备不支持地理位置服务";
                    break;

                default:
                    strStatus = "未知状态";
                    break;
            }

            return (strStatus);

        }

        async void geolocator_PositionChanged(Geolocator sender, PositionChangedEventArgs args)
        {
            await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                Geoposition pos = args.Position;
                myMap.Center = pos.Coordinate.Point;
                tbLatitude.Text = DateTime.Now.ToString() + " 纬度:" + pos.Coordinate.Point.Position.Latitude;
                tbLongitude.Text = DateTime.Now.ToString() + " 经度:" + pos.Coordinate.Point.Position.Longitude;
                tbAccuracy.Text = DateTime.Now.ToString() + " 准确性:" + pos.Coordinate.Accuracy;

            });

        }
    }
}
