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

namespace WebServiceDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        // 获取归属地按钮事件处理程序
        private async void search_Click(object sender, RoutedEventArgs e)
        {
            //实例化一个web service代理的对象
            MobileCodeServiceReference.MobileCodeWSSoapClient proxy = new MobileCodeServiceReference.MobileCodeWSSoapClient();
            // 调用web service的方法获取电话号码的归属地
            information.Text = await proxy.getMobileCodeInfoAsync(No.Text, "");
        }

    }
}
