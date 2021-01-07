using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace LauncherDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }
        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (web.Text != "")
            {
                //打开网页
                await Windows.System.Launcher.LaunchUriAsync(new Uri(web.Text));
            }

        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (phone.Text != "")
            {
                //呼叫手机 
                await Windows.System.Launcher.LaunchUriAsync(new Uri("tel:" + phone.Text));
            }

        }

        private async void Button_Click_3(object sender, RoutedEventArgs e)
        {
            string app = (comboBox.SelectedItem as ComboBoxItem).Content.ToString();
            switch (app)
            {
                case "飞行模式设置":
                    await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings-airplanemode:"));
                    break;
                case "蓝牙设置":
                    await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings-bluetooth:"));
                    break;
                case "手机网络设置":
                    await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings-cellular:"));
                    break;
                case "电子邮件和帐户设置":
                    await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings-emailandaccounts:"));
                    break;
                case "位置设置":
                    await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings-location:"));
                    break;
                case "锁屏设置":
                    await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings-lock:"));
                    break;
                case "Wi-Fi设置":
                    await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings-wifi:"));
                    break;
                case "屏幕旋转设置":
                    await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings-screenrotation:"));
                    break;
                case "节电模式设置":
                    await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings-power:"));
                    break;
            }

        }

    }
}
