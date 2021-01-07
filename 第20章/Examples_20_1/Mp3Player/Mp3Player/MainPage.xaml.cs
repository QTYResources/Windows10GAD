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

namespace Mp3Player
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

        private void play_Click(object sender, RoutedEventArgs e)
        {
            if (radioButton1.IsChecked == true)
            {
                sound.Source = new Uri("ms-appx:///TouchMyHeart.mp3", UriKind.Absolute);
            }
            else if (radioButton2.IsChecked == true)
            {
                sound.Source = new Uri("ms-appx:///2.mp3", UriKind.Absolute);
            }
            else if (radioButton3.IsChecked == true)
            {
                sound.Source = new Uri("ms-appx:///3.mp3", UriKind.Absolute);
            }
            else if (radioButton4.IsChecked == true)
            {
                sound.Source = new Uri("ms-appx:///4.mp3", UriKind.Absolute);
            }
            else if (radioButton5.IsChecked == true)
            {
                sound.Source = new Uri("ms-appx:///5.mp3", UriKind.Absolute);
            }
            else
            {
                sound.Source = new Uri("ms-appx:///TouchMyHeart.mp3", UriKind.Absolute);
            }
            sound.Play();
        }

        private void pause_Click(object sender, RoutedEventArgs e)
        {
            sound.Pause();
        }

        private void stop_Click(object sender, RoutedEventArgs e)
        {
            sound.Stop();
        }

    }
}
