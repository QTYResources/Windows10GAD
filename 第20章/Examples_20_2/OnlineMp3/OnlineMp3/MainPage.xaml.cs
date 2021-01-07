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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace OnlineMp3
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ApplicationDataContainer _appSettings;
        private const string KEY = "mp3Name";
        private string fileList = "";
        public MainPage()
        {
            InitializeComponent();
            _appSettings = ApplicationData.Current.LocalSettings;
            if (_appSettings.Values.ContainsKey(KEY))
            {
                fileList = _appSettings.Values[KEY].ToString();
                listBox1.ItemsSource = fileList.Split('|');
            }
            else
            {
                _appSettings.Values.Add(KEY, fileList);
            }
        }
        // 保存播放的历史记录
        private void savehistory()
        {
            // 提取文件名
            string fileName = System.IO.Path.GetFileName(mp3Uri.Text);
            if (!fileList.Contains(fileName))
            {
                fileList += "|" + fileName;
                _appSettings.Values[KEY] = fileList;
                listBox1.ItemsSource = fileList.Split('|');
            }
        }

        private async void play_Click(object sender, RoutedEventArgs e)
        {
            string erro = "";
            try
            {
                if (!string.IsNullOrEmpty(mp3Uri.Text))
                {
                    media.Source = new Uri(mp3Uri.Text, UriKind.Absolute);
                    media.Play();
                    savehistory();
                }
                else
                {
                    erro = "请输入mp3的网络地址！";
                }

            }
            catch (Exception)
            {
                erro = "无法播放！";
            }
            if (erro != "")
            {
                await new MessageDialog(erro).ShowAsync();
            }
        }

        private void stop_Click(object sender, RoutedEventArgs e)
        {
            media.Stop();
        }

    }
}
