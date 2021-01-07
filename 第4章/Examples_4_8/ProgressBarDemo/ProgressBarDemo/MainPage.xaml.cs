using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ProgressBarDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            progressBar1.Visibility = Visibility.Collapsed;
        }

        private void begin_Click(object sender, RoutedEventArgs e)
        {
            progressBar1.Visibility = Visibility.Visible;

            if (radioButton1.IsChecked == true)
            {
                progressBar1.IsIndeterminate = false;
                DispatcherTimer timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(1);
                timer.Tick += timer_Tick;
                timer.Start();
            }
            else
            {
                progressBar1.Value = 0;
                progressBar1.IsIndeterminate = true;

            }
        }
        async void timer_Tick(object sender, object e)
        {
            if (progressBar1.Value < 100)
            {
                progressBar1.Value += 10;
            }
            else
            {
                (sender as DispatcherTimer).Tick -= timer_Tick;
                (sender as DispatcherTimer).Stop();
                await new MessageDialog("进度完成").ShowAsync();
            }

        }
        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            progressBar1.Visibility = Visibility.Collapsed;
        }

    }
}
