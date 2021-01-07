using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace PackageFileDemo
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


        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            ApplicationData appData = ApplicationData.Current;
            StorageFile imgFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/StoreLogo.png"));
            await imgFile.CopyAsync(appData.TemporaryFolder, imgFile.Name, NameCollisionOption.ReplaceExisting);
            appImage.Source = new BitmapImage(new Uri("ms-appdata:///temp/StoreLogo.png"));
            packageImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/StoreLogo.png"));
        }
    }
}
