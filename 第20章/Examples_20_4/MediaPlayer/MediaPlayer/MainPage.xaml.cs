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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MediaPlayer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        // 使用定时器来处理视频播放的进度条
        DispatcherTimer currentPosition = new DispatcherTimer();
        // 页面的初始化
        public MainPage()
        {
            InitializeComponent();
            //定义多媒体流可用并被打开时触发的事件
            myMediaElement.MediaOpened += new RoutedEventHandler(myMediaElement_MediaOpened);
            //定义多媒体停止时触发的事件
            myMediaElement.MediaEnded += new RoutedEventHandler(myMediaElement_MediaEnded);
            //定义多媒体播放状态改变时触发的事件
            myMediaElement.CurrentStateChanged += new RoutedEventHandler(myMediaElement_CurrentStateChanged);
            // 定时器时间间隔
            currentPosition.Interval = new TimeSpan(1000);
            //定义定时器触发的事件
            currentPosition.Tick += currentPosition_Tick;
            //设置多媒体控件的网络视频资源
            myMediaElement.Source = new Uri("ms-appx:///123.wmv", UriKind.Absolute);
        }
        //视频状态改变时的处理事件
        void myMediaElement_CurrentStateChanged(object sender, RoutedEventArgs e)
        {

            if (myMediaElement.CurrentState == MediaElementState.Playing)
            {//播放视频时各菜单的状态
                currentPosition.Start();
                play.IsEnabled = false; // 播放
                pause.IsEnabled = true;  // 暂停
                stop.IsEnabled = true;  // 停止
            }
            else if (myMediaElement.CurrentState == MediaElementState.Paused)
            { //暂停视频时各菜单的状态
                currentPosition.Stop();
                play.IsEnabled = true;
                pause.IsEnabled = false;
                stop.IsEnabled = true;
            }
            else
            {//停止视频时各菜单的状态
                currentPosition.Stop();
                play.IsEnabled = true;
                pause.IsEnabled = false;
                stop.IsEnabled = false;
            }
        }
        //多媒体停止时触发的事件
        void myMediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            //停止播放
            myMediaElement.Stop();
        }
        //多媒体流可用并被打开时触发的事件
        void myMediaElement_MediaOpened(object sender, RoutedEventArgs e)
        {
            //获取多媒体视频的总时长来设置进度条的最大值
            pbVideo.Maximum = (int)myMediaElement.NaturalDuration.TimeSpan.TotalMilliseconds;
            //播放视频
            myMediaElement.Play();
        }
        //定时器触发的事件
        private void currentPosition_Tick(object sender, object e)
        {
            //获取当前视频播放了的时长来设置进度条的值
            pbVideo.Value = (int)myMediaElement.Position.TotalMilliseconds;
        }

        // 播放视频菜单事件
        private void play_Click(object sender, RoutedEventArgs e)
        {
            myMediaElement.Play();
        }
        // 暂停视频菜单事件
        private void pause_Click(object sender, RoutedEventArgs e)
        {
            myMediaElement.Pause();
        }
        // 停止视频菜单事件
        private void stop_Click(object sender, RoutedEventArgs e)
        {
            myMediaElement.Stop();
        }
    }
}
