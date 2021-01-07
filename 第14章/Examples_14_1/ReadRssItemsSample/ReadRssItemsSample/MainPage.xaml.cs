using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace ReadRssItemsSample
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

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (rssURL.Text != "")
            {
                RssService.GetRssItems(
                     rssURL.Text,
                   async (items) =>
                   {
                       await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                       {
                           listbox.ItemsSource = items;
                       });
                   },
                   async (exception) =>
                   {
                       await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
                       {
                           await new MessageDialog(exception).ShowAsync();
                       });

                   },
                    null
                    );
            }
            else
            {
                await new MessageDialog("请输入RSS地址").ShowAsync();
            }
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listbox.SelectedItem == null)
                return;
            var template = (RssItem)listbox.SelectedItem;
            Frame.Navigate(typeof(DetailPage), template);
            listbox.SelectedItem = null;
        }
    }
}
