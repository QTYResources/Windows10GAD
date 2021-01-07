using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
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

namespace BackgroundLocationDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            // 设置地图的ServiceToken
            myMap.MapServiceToken = "AuthenticationToken";
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }
        // 注册的后台任务对象
        private IBackgroundTaskRegistration _geolocTask = null;
        // 任务的取消信号对象
        private CancellationTokenSource _cts = null;
        // 后台任务的名字
        private const string SampleBackgroundTaskName = "SampleLocationBackgroundTask";
        // 后台任务的入口
        private const string SampleBackgroundTaskEntryPoint = "BackgroundTask.LocationBackgroundTask";
        // 页面的进入事件处理程序
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            Geolocator geolocator = new Geolocator();
            Geoposition pos = await geolocator.GetGeopositionAsync();
            // 获取后台任务是否已经注册，如果已经注册则获取其注册对象
            foreach (var cur in BackgroundTaskRegistration.AllTasks)
            {
                if (cur.Value.Name == SampleBackgroundTaskName)
                {
                    _geolocTask = cur.Value;
                    break;
                }
            }
            if (_geolocTask != null)
            {
                // 已注册后台任务，则监控其Completed事件，获取最新的位置信息
                _geolocTask.Completed += new BackgroundTaskCompletedEventHandler(OnCompleted);
            }
            else
            {
                // 如果为注册后台任务，则注册后台任务
                RegisterBackgroundTask();
            }
        }

        // 离开当前页面的时间处理程序
        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            CancelGetGeoposition();
            if (_geolocTask != null)
            {
                _geolocTask.Completed -= new BackgroundTaskCompletedEventHandler(OnCompleted);
            }

            base.OnNavigatingFrom(e);
        }
        // 如果获取地理位置还在进行中，则将其取消
        private void CancelGetGeoposition()
        {
            if (_cts != null)
            {
                _cts.Cancel();
                _cts = null;
            }
        }

        // 添加跟踪位置的后台任务的注册
        private async void RegisterBackgroundTask()
        {
            try
            {
                var access = await BackgroundExecutionManager.RequestAccessAsync();
                if (access == BackgroundAccessStatus.Denied)
                {
                    await new MessageDialog("后台任务被禁止!").ShowAsync();
                    return;
                }

                //  注册一个在锁屏上定时15分钟执行的后台任务
                BackgroundTaskBuilder geolocTaskBuilder = new BackgroundTaskBuilder();
                geolocTaskBuilder.Name = SampleBackgroundTaskName;
                geolocTaskBuilder.TaskEntryPoint = SampleBackgroundTaskEntryPoint;
                var trigger = new TimeTrigger(15, false);
                geolocTaskBuilder.SetTrigger(trigger);
                _geolocTask = geolocTaskBuilder.Register();
                // 注册后台任务完成事件
                _geolocTask.Completed += new BackgroundTaskCompletedEventHandler(OnCompleted);
            }
            catch (Exception ex)
            {

            }
        }

        // 获取地理位置
        async private void GetGeopositionAsync()
        {
            try
            {
                // 创建任务的取消信号对象，可用来取消异步请求
                _cts = new CancellationTokenSource();
                CancellationToken token = _cts.Token;
                Geolocator geolocator = new Geolocator();
                // 获取当前的位置
                Geoposition pos = await geolocator.GetGeopositionAsync().AsTask(token);
                // 设置地图的中心
                myMap.Center = pos.Coordinate.Point;
                // 纬度信息
                tbLatitude.Text = "纬度:" + pos.Coordinate.Point.Position.Latitude;
                // 经度信息
                tbLongitude.Text = "经度:" + pos.Coordinate.Point.Position.Longitude;
                // 准确性信息
                tbAccuracy.Text = "准确性:" + pos.Coordinate.Accuracy;
            }
            catch (UnauthorizedAccessException)
            {
                // 访问异常
            }
            catch (TaskCanceledException)
            {
                // 请求被取消
            }
            catch (Exception ex)
            {
                // 其他异常
            }
            finally
            {
                _cts = null;
            }
        }
        // 后台任务完成事件，用以更新地理位置信息
        async private void OnCompleted(IBackgroundTaskRegistration sender, BackgroundTaskCompletedEventArgs e)
        {
            if (sender != null)
            {
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    try
                    {
                        // 检查是否出现异常
                        e.CheckResult();

                        // 获取存储设置，在后台任务中把地理位置信息存储在里面
                        var settings = ApplicationData.Current.LocalSettings;
                        if (settings.Values["Status"] != null)
                        {
                            // 状态信息
                            tbStatus.Text = "状态改变信息:" + settings.Values["Status"].ToString();
                        }
                        bool latitude = false;
                        if (settings.Values["Latitude"] != null)
                        {
                            // 纬度信息
                            tbLatitude.Text = "纬度:" + settings.Values["Latitude"].ToString();
                            latitude = true;
                        }
                        else
                        {
                            tbLatitude.Text = "纬度:" + "No data";
                        }
                        bool longitude = false;
                        if (settings.Values["Longitude"] != null)
                        {
                            // 经度信息
                            tbLongitude.Text = "经度:" + settings.Values["Longitude"].ToString();
                            longitude = true;
                        }
                        else
                        {
                            tbLongitude.Text = "经度:" + "No data";
                        }
                        if (settings.Values["Accuracy"] != null)
                        {
                            // 准确性信息
                            tbAccuracy.Text = "准确性:" + settings.Values["Accuracy"].ToString();
                        }
                        else
                        {
                            tbAccuracy.Text = "准确性:" + "No data";
                        }
                        if (latitude & longitude)
                        {
                            // 设置地图的中心
                            myMap.Center = new Geopoint(new BasicGeoposition
                            {
                                Altitude = 0,
                                Latitude = double.Parse(settings.Values["Latitude"].ToString()),
                                Longitude = double.Parse(settings.Values["Longitude"].ToString())
                            });
                        }

                    }
                    catch (Exception ex)
                    {

                    }
                });
            }
        }
    }
}
