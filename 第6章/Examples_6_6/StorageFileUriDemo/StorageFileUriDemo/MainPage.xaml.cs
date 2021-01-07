using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace StorageFileUriDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private string fileName = "testfile.txt";
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void bt_save_Click(object sender, RoutedEventArgs e)
        {
            if (info.Text != "")
            {
                IStorageFolder applicationFolder = ApplicationData.Current.LocalFolder;
                IStorageFile storageFile = await applicationFolder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);
                await FileIO.WriteTextAsync(storageFile, info.Text);
                await new MessageDialog("保存成功，文件的路径：" + storageFile.Path).ShowAsync();
            }
            else
            {
                await new MessageDialog("内容不能为空").ShowAsync();
            }
        }

        private async void bt_read_Click(object sender, RoutedEventArgs e)
        {
            string text;
            try
            {
                var storageFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appdata:///local/" + fileName));
                IRandomAccessStream accessStream = await storageFile.OpenReadAsync();
                using (StreamReader streamReader = new StreamReader(accessStream.AsStreamForRead((int)accessStream.Size)))
                {
                    text = streamReader.ReadToEnd();
                }
            }
            catch (Exception exce)
            {
                text = "文件读取错误：" + exce.Message;
            }
            await new MessageDialog(text).ShowAsync();
        }
    }
}
