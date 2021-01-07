using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
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

namespace MapLocationDemo
{

    public sealed partial class MainPage : Page
    {
        Geolocator geolocator = null;
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
            geolocator = new Geolocator();
            myMap.MapServiceToken = "AuthenticationToken";
        }


        private async void getlocation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Geoposition pos = await geolocator.GetGeopositionAsync();
                myMap.Center = pos.Coordinate.Point;
                tbLatitude.Text = "纬度:" + pos.Coordinate.Point.Position.Latitude;
                tbLongitude.Text = "经度:" + pos.Coordinate.Point.Position.Longitude;
                tbAccuracy.Text = "准确性:" + pos.Coordinate.Accuracy;
            }
            catch (System.UnauthorizedAccessException)
            {
                tbLatitude.Text = "No data";
                tbLongitude.Text = "No data";
                tbAccuracy.Text = "No data";
            }
            catch (TaskCanceledException)
            {
                tbLatitude.Text = "Cancelled";
                tbLongitude.Text = "Cancelled";
                tbAccuracy.Text = "Cancelled";
            }
        }
    }
}
