using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace FileAssociationDemo
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
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            FileActivatedEventArgs fileEvent = e.Parameter as FileActivatedEventArgs;
            if (fileEvent != null)
            {
                foreach (StorageFile file in fileEvent.Files)
                {
                    fileName.Text += file.Name;

                    var fileStream = await file.OpenReadAsync();
                    var stream = fileStream.AsStreamForRead();
                    byte[] content = new byte[stream.Length];
                    await stream.ReadAsync(content, 0, content.Length);
                    string text = Encoding.UTF8.GetString(content, 0, content.Length);

                    fileContent.Text += fileContent.Text + text;
                }

            }

        }
    }
}
