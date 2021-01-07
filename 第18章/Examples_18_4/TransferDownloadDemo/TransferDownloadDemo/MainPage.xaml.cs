using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.BackgroundTransfer;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Web;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace TransferDownloadDemo
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

        private async void downloadButton_Click(object sender, RoutedEventArgs e)
        {
            if (fileUrl.Text == "")
            {
                info.Text = "文件地址不能为空";
                return;
            }
            string transferFileName = fileUrl.Text;
            string downloadFile = DateTime.Now.Ticks + transferFileName.Substring(transferFileName.LastIndexOf("/") + 1);
            Uri transferUri;
            try
            {
                transferUri = new Uri(Uri.EscapeUriString(transferFileName), UriKind.RelativeOrAbsolute);
            }
            catch (Exception ex)
            {
                info.Text = "文件地址不符合格式";
                return;
            }
            StorageFile destinationFile;
            StorageFolder fd = await ApplicationData.Current.LocalFolder.CreateFolderAsync("shared", CreationCollisionOption.OpenIfExists);
            StorageFolder fd2 = await fd.CreateFolderAsync("transfers", CreationCollisionOption.OpenIfExists);
            destinationFile = await fd2.CreateFileAsync(downloadFile, CreationCollisionOption.ReplaceExisting);
            BackgroundDownloader backgroundDownloader = new BackgroundDownloader();
            DownloadOperation download = backgroundDownloader.CreateDownload(transferUri, destinationFile);
            await download.StartAsync();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(BackgroundTransferList));
        }
    }
}
