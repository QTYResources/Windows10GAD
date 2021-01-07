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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace LocalSettingsDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ApplicationDataContainer _appSettings;
        public MainPage()
        {
            InitializeComponent();
            _appSettings = ApplicationData.Current.LocalSettings;
            BindKeyList();
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(txtKey.Text))
            {
                _appSettings.Values[txtKey.Text] = txtValue.Text;
                BindKeyList();
            }
            else
            {
                await new MessageDialog("请输入key值").ShowAsync();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lstKeys.SelectedIndex > -1)
            {
                _appSettings.Values.Remove(lstKeys.SelectedItem.ToString());
                BindKeyList();
            }
        }

        private void lstKeys_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                string key = e.AddedItems[0].ToString();
                if (_appSettings.Values.ContainsKey(key))
                {
                    txtKey.Text = key;
                    txtValue.Text = _appSettings.Values[key].ToString();
                }
            }
        }

        private void BindKeyList()
        {
            lstKeys.Items.Clear();
            foreach (string key in _appSettings.Values.Keys)
            {
                lstKeys.Items.Add(key);
            }
            txtKey.Text = "";
            txtValue.Text = "";
        }
        private void deleteall_Click(object sender, RoutedEventArgs e)
        {
            _appSettings.Values.Clear();
            BindKeyList();
        }
    }
}
