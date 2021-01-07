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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ScrollViewerDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private DispatcherTimer tmrDown;
        private DispatcherTimer tmrUp;
        public MainPage()
        {
            InitializeComponent();
            for (int i = 0; i <= 30; i++)
            {
                Image imgItem = new Image();
                imgItem.Width = 200;
                imgItem.Height = 200;
                if (i % 4 == 0)
                {
                    imgItem.Source = (new BitmapImage(new Uri("ms-appx:///a.jpg", UriKind.RelativeOrAbsolute)));
                }
                else if (i % 4 == 1)
                {
                    imgItem.Source = (new BitmapImage(new Uri("ms-appx:///b.jpg", UriKind.RelativeOrAbsolute)));

                }
                else if (i % 4 == 2)
                {
                    imgItem.Source = (new BitmapImage(new Uri("ms-appx:///c.jpg", UriKind.RelativeOrAbsolute)));

                }
                else
                {
                    imgItem.Source = (new BitmapImage(new Uri("ms-appx:///d.jpg", UriKind.RelativeOrAbsolute)));

                }
                this.stkpnlImage.Children.Add(imgItem);
            }

            tmrDown = new DispatcherTimer();
            tmrDown.Interval = new TimeSpan(500);
            tmrDown.Tick += tmrDown_Tick;
            tmrUp = new DispatcherTimer();
            tmrUp.Interval = new TimeSpan(500);
            tmrUp.Tick += tmrUp_Tick;
        }

        void tmrUp_Tick(object sender, object e)
        {
            scrollViewer1.ScrollToVerticalOffset(scrollViewer1.VerticalOffset - 10);
        }

        void tmrDown_Tick(object sender, object e)
        {
            tmrUp.Stop();
            scrollViewer1.ScrollToVerticalOffset(scrollViewer1.VerticalOffset + 10);
        }

        private void btnUp_Click(object sender, RoutedEventArgs e)
        {
            tmrDown.Stop();
            tmrUp.Start();
        }

        private void btnDown_Click(object sender, RoutedEventArgs e)
        {
            tmrDown.Start();
        }

        private void stop_Click(object sender, RoutedEventArgs e)
        {
            tmrUp.Stop();
            tmrDown.Stop();
        }
    }
}
