using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WindowsRuntimeComponent1;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace WindRTAsyncDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        WindowsPhoneRuntimeComponent windowsPhoneRuntimeComponent;

        Progress<double> myProgress;
        CancellationTokenSource cancellationTokenSource;

        public MainPage()
        {
            InitializeComponent();

            windowsPhoneRuntimeComponent = new WindowsPhoneRuntimeComponent();
            windowsPhoneRuntimeComponent.currentValue += windowsPhoneRuntimeComponent_currentValue;
            myProgress = new Progress<double>();
            myProgress.ProgressChanged += myProgress_ProgressChanged;
        }

        async void windowsPhoneRuntimeComponent_currentValue(int __param0)
        {
            await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                progressBar.Value = __param0 * 10;
            });
            Debug.WriteLine("事件汇报的进度：" + __param0.ToString());
        }

        void myProgress_ProgressChanged(object sender, double e)
        {
            Debug.WriteLine("当前处理进度：" + e.ToString());
            if (e == 8)
            {
                cancellationTokenSource.Cancel();
            }
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int sum = windowsPhoneRuntimeComponent.Add(1, 10);
            await new MessageDialog("结果：" + sum).ShowAsync();
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            int sum = await windowsPhoneRuntimeComponent.AddAdync(1, 10);
            await new MessageDialog("结果：" + sum).ShowAsync();
        }

        private async void Button_Click_3(object sender, RoutedEventArgs e)
        {
            try
            {
                cancellationTokenSource = new CancellationTokenSource();
                //int sum = await windowsPhoneRuntimeComponent.AddWithProgressAsync(1, 10).AsTask(myProgress);
                int sum = await windowsPhoneRuntimeComponent.AddWithProgressAsync(1, 10).AsTask(cancellationTokenSource.Token, myProgress);
                await new MessageDialog("结果：" + sum).ShowAsync();
                Debug.WriteLine("结果：" + sum);
            }
            catch (TaskCanceledException)
            {
                Debug.WriteLine("任务被取消了");
            }
            catch (Exception)
            {

            }
        }
    }
}
