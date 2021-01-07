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

namespace CommandBarDemo
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

        private void CommandBar_Opened(object sender, object e)
        {
            info.Text = "菜单栏打开了";
        }

        private void CommandBar_Closed(object sender, object e)
        {
            info.Text = "菜单栏关闭了";
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            info.Text = "单击了菜单栏：" + (sender as AppBarButton).Label;
        }
    }
}
