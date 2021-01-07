using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.BackgroundTransfer;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace TransferDownloadDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BackgroundTransferList : Page
    {
        public BackgroundTransferList()
        {
            this.InitializeComponent();
        }
        private List<DownloadOperation> activeDownloads;
        private ObservableCollection<TransferModel> transfers = new ObservableCollection<TransferModel>();
        private CancellationTokenSource cancelToken = new CancellationTokenSource();
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            StorageFolder fd = await ApplicationData.Current.LocalFolder.CreateFolderAsync("shared", CreationCollisionOption.OpenIfExists);
            StorageFolder fd2 = await fd.CreateFolderAsync("transfers", CreationCollisionOption.OpenIfExists);
            var files = await fd2.GetFilesAsync();
            TransferList.ItemsSource = transfers;
            FileList.ItemsSource = files;
            await DiscoverActiveDownloadsAsync();
        }


        private async Task DiscoverActiveDownloadsAsync()
        {
            activeDownloads = new List<DownloadOperation>();

            IReadOnlyList<DownloadOperation> downloads = null;
            try
            {
                downloads = await BackgroundDownloader.GetCurrentDownloadsAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return;
            }


            if (downloads.Count > 0)
            {
                List<Task> tasks = new List<Task>();
                foreach (DownloadOperation download in downloads)
                {
                    Debug.WriteLine(String.Format(CultureInfo.CurrentCulture,
                        "Discovered background download: {0}, Status: {1}", download.Guid,
                        download.Progress.Status));

                    tasks.Add(HandleDownloadAsync(download, false));
                }

                await Task.WhenAll(tasks);
            }
        }

        private async Task HandleDownloadAsync(DownloadOperation download, bool start)
        {
            try
            {
                TransferModel transfer = new TransferModel();
                transfer.DownloadOperation = download;
                transfer.Source = download.RequestedUri.ToString();
                transfer.Destination = download.ResultFile.Path;
                transfer.BytesReceived = download.Progress.BytesReceived;
                transfer.TotalBytesToReceive = download.Progress.TotalBytesToReceive;
                transfer.Progress = 0;

                transfers.Add(transfer);
                Progress<DownloadOperation> progressCallback = new Progress<DownloadOperation>(DownloadProgress);
                await download.AttachAsync().AsTask(cancelToken.Token, progressCallback);

                ResponseInformation response = download.GetResponseInformation();
                Debug.WriteLine(String.Format(CultureInfo.CurrentCulture, "Completed: {0}, Status Code: {1}",
                    download.Guid, response.StatusCode));
            }
            catch (TaskCanceledException)
            {
                Debug.WriteLine("Canceled: " + download.Guid);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            finally
            {
                transfers.Remove(transfers.First(p => p.DownloadOperation == download));
                activeDownloads.Remove(download);
            }
        }

        private void DownloadProgress(DownloadOperation download)
        {
            try
            {
                TransferModel transfer = transfers.First(p => p.DownloadOperation == download);
                transfer.Progress = (int)((download.Progress.BytesReceived * 100) / download.Progress.TotalBytesToReceive);
                transfer.BytesReceived = download.Progress.BytesReceived;
                transfer.TotalBytesToReceive = download.Progress.TotalBytesToReceive;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {

            foreach (TransferModel transfer in transfers)
            {
                if (transfer.DownloadOperation.Progress.Status == BackgroundTransferStatus.Running)
                {
                    transfer.DownloadOperation.Pause();
                }
            }
        }

        private void btnResume_Click(object sender, RoutedEventArgs e)
        {

            foreach (TransferModel transfer in transfers)
            {
                if (transfer.DownloadOperation.Progress.Status == BackgroundTransferStatus.PausedByApplication)
                {
                    transfer.DownloadOperation.Resume();
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            cancelToken.Cancel();
            cancelToken.Dispose();

            cancelToken = new CancellationTokenSource();
        }
    }
}
