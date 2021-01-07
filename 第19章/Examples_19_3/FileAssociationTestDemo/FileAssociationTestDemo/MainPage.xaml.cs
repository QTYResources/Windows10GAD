using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace FileAssociationTestDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        private async void OnCreateFileButtonClicked(object sender, RoutedEventArgs e)
        {

            await WriteToFile("文件内容，日志信息test test test test test test", "rss.log");
            await new MessageDialog("创建成功").ShowAsync();
        }


        public async Task WriteToFile(string contents, string fileName)
        {
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            var file = await folder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            using (var fs = await file.OpenAsync(FileAccessMode.ReadWrite))
            {
                using (var outStream = fs.GetOutputStreamAt(0))
                {
                    using (var dataWriter = new DataWriter(outStream))
                    {
                        if (contents != null)
                            dataWriter.WriteString(contents);

                        await dataWriter.StoreAsync();
                        dataWriter.DetachStream();
                    }

                    await outStream.FlushAsync();
                }
            }
        }


        private async void OnOpenFileButtonClicked(object sender, RoutedEventArgs e)
        {
            string message = "";
            try
            {
                StorageFile file = await ApplicationData.Current.LocalFolder.GetFileAsync("rss.log");
                bool success = await Windows.System.Launcher.LaunchFileAsync(file);
                if (success)
                {
                    message = "文件打开成功";
                }
                else
                {
                    message = "文件打开失败";
                }
            }
            catch (Exception exc)
            {
                message = exc.ToString();
            }
            await new MessageDialog(message).ShowAsync();
        }
    }
}
