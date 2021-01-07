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

namespace StarFlowDemo
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

        private async void button_Click_1(object sender, RoutedEventArgs e)
        {
            if ((bool)myCanvas.GetValue(StarBehavior.AttachStarFlakeProperty) == false)
            {
                if (myCanvas.Children.Count > 0)
                {
                    await new MessageDialog("星星动画未完全结束").ShowAsync();
                    return;
                }
                myCanvas.SetValue(StarBehavior.AttachStarFlakeProperty, true);
                button.Content = "停止新增星星";
            }
            else
            {
                myCanvas.SetValue(StarBehavior.AttachStarFlakeProperty, false);
                button.Content = "开始星星飘落";
            }
        }
    }
}
