using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace PackageDemo
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
        }

        private async void btGetFile_Click(object sender, RoutedEventArgs e)
        {
            lbFolder.Items.Clear();
            StorageFolder localFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            foreach (StorageFolder folder in await localFolder.GetFoldersAsync())
            {
                ListBoxItem item = new ListBoxItem();
                item.Content = "应用程序目录" + folder.Name;
                item.DataContext = folder;
                lbFolder.Items.Add(item);
            }
            lbFile.Items.Clear();
            foreach (StorageFile file in await localFolder.GetFilesAsync())
            {
                ListBoxItem item3 = new ListBoxItem();
                item3.Content = "文件：" + file.Name;
                item3.DataContext = file;
                lbFile.Items.Add(item3);
            }
        }

        private async void open_Click(object sender, RoutedEventArgs e)
        {
            if (lbFolder.SelectedIndex == -1)
            {
                await new MessageDialog("请选择一个文件夹").ShowAsync();
            }
            else
            {
                ListBoxItem item = lbFolder.SelectedItem as ListBoxItem;
                StorageFolder folder = item.DataContext as StorageFolder;
                lbFolder.Items.Clear();
                foreach (StorageFolder folder2 in await folder.GetFoldersAsync())
                {
                    ListBoxItem item2 = new ListBoxItem();
                    item2.Content = "文件夹：" + folder2.Name;
                    item2.DataContext = folder;
                    lbFolder.Items.Add(item2);
                }
                lbFile.Items.Clear();
                foreach (StorageFile file in await folder.GetFilesAsync())
                {
                    ListBoxItem item3 = new ListBoxItem();
                    item3.Content = "文件：" + file.Name;
                    item3.DataContext = file;
                    lbFile.Items.Add(item3);
                }

            }
        }

        private async void create_Click(object sender, RoutedEventArgs e)
        {
            if (lbFolder.SelectedIndex == -1)
            {
                await new MessageDialog("请选择一个文件夹").ShowAsync();
            }
            else
            {
                ListBoxItem item = lbFolder.SelectedItem as ListBoxItem;
                StorageFolder folder = item.DataContext as StorageFolder;
                StorageFile file = await folder.CreateFileAsync(DateTime.Now.Millisecond.ToString() + ".txt");
                ListBoxItem item3 = new ListBoxItem();
                item3.Content = "文件：" + file.Name;
                item3.DataContext = file;
                lbFile.Items.Add(item3);
                await new MessageDialog("创建文件成功").ShowAsync();
            }
        }

        private async void delete_Click(object sender, RoutedEventArgs e)
        {
            if (lbFile.SelectedIndex == -1)
            {
                await new MessageDialog("请选择一个文件夹").ShowAsync();
            }
            else
            {
                ListBoxItem item = lbFile.SelectedItem as ListBoxItem;
                StorageFile file = item.DataContext as StorageFile;
                await file.DeleteAsync();
                lbFile.Items.Remove(item);
                await new MessageDialog("删除成功").ShowAsync();
            }
        }

    }
}