using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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

namespace AnimationUITestDemo
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // 阻塞UI线程2秒钟
            Task.Delay(2000).Wait();
        }

        private void heightAnimationButton_Click_1(object sender, RoutedEventArgs e)
        {
            // 播放改变高度属性的动画，高度有100变成200
            scaleTransformStoryboard.Stop();
            heightStoryboard.Begin();
        }

        private void scaleTransformAnimationButton_Click_1(object sender, RoutedEventArgs e)
        {
            // 播放改变变换属性的动画，举行沿着X轴放大2倍
            heightStoryboard.Stop();
            scaleTransformStoryboard.Begin();
        }
    }
}
