using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace StyleCodeDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            Style style = new Style(typeof(Button));
            style.Setters.Add(new Setter(Button.HeightProperty, 70));
            style.Setters.Add(new Setter(Button.WidthProperty, 300));
            style.Setters.Add(new Setter(Button.ForegroundProperty, new SolidColorBrush(Colors.White)));
            style.Setters.Add(new Setter(Button.BackgroundProperty, new SolidColorBrush(Colors.Blue)));

            buton1.Style = style;
        }

        private void buton1_Click(object sender, RoutedEventArgs e)
        {
            buton1.Style = (Style)this.Resources["style2"];
            // buton1.Style = (Style)Application.Current.Resources["style2"];
        }
    }
}
