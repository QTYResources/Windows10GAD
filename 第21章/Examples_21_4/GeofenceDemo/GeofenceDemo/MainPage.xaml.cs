using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.Devices.Enumeration;
using Windows.Devices.Geolocation;
using Windows.Devices.Geolocation.Geofencing;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Globalization;
using Windows.Globalization.DateTimeFormatting;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace GeofenceDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        // 地理围栏ID
        string fenceKey = "fenceKey";
        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
            // 添加地理围栏状态改变事件
            GeofenceMonitor.Current.GeofenceStateChanged += Current_GeofenceStateChanged;
        }
        // 创建地理围栏的按钮事件处理程序
        private async void creatGeofence_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (var geo in GeofenceMonitor.Current.Geofences)
                {
                    if (geo.Id == fenceKey)
                    {
                        await new MessageDialog("该地理围栏已经添加").ShowAsync();
                        return;
                    }
                }
                // 创建一个Geolocator对象
                Geolocator geolocator = new Geolocator();
                // 获取当前的地理位置信息
                Geoposition pos = await geolocator.GetGeopositionAsync();
                // 获取日历对象，并设置为当前的时间
                Calendar calendar = new Calendar();
                calendar.SetToNow();
                // 获取当前的日期时间
                DateTimeOffset nowDateTime = calendar.GetDateTime();
                // 使用经纬度来创建一个BasicGeoposition对象
                BasicGeoposition position;
                position.Latitude = pos.Coordinate.Point.Position.Latitude;
                position.Longitude = pos.Coordinate.Point.Position.Longitude;
                position.Altitude = 0.0;
                double radius = 20;
                // 创建围栏的圆形区域
                Geocircle geocircle = new Geocircle(position, radius);
                // 表示监控地理围栏3种状态的改变
                MonitoredGeofenceStates mask = 0;
                mask |= MonitoredGeofenceStates.Entered;
                mask |= MonitoredGeofenceStates.Exited;
                mask |= MonitoredGeofenceStates.Removed;
                // 在进入状态改变事件之前，你需要持续在地理围栏内（外）的持续时间
                TimeSpan dwellTime = TimeSpan.FromSeconds(3);
                // 该地理围栏持续10个小时
                TimeSpan duration = TimeSpan.FromHours(10);
                // 创建围栏对象
                Geofence geofence = new Geofence(fenceKey, geocircle, mask, true, dwellTime, nowDateTime, duration);
                GeofenceMonitor.Current.Geofences.Add(geofence);
                // 添加成功
                await new MessageDialog("创建成功").ShowAsync();
            }
            catch (Exception)
            {

            }
        }
        // 地理围栏状态改变事件处理程序
        async void Current_GeofenceStateChanged(GeofenceMonitor sender, object args)
        {
            var reports = sender.ReadReports();

            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                foreach (GeofenceStateChangeReport report in reports)
                {
                    GeofenceState state = report.NewState;
                    // 获取地理围栏
                    Geofence geofence = report.Geofence;
                    if (state == GeofenceState.Removed)
                    {
                        info.Text = "地理围栏已经被移除";
                        GeofenceMonitor.Current.Geofences.Remove(geofence);
                    }
                    else if (state == GeofenceState.Entered)
                    {
                        info.Text = "进入地理围栏";
                    }
                    else if (state == GeofenceState.Exited)
                    {
                        info.Text = "退出地理围栏";
                    }
                }
            });
        }
    }
}
