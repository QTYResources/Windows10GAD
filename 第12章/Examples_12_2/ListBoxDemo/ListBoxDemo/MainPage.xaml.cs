using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ListBoxDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public ObservableCollection<Item> Items { get; set; }
        public MainPage()
        {
            this.InitializeComponent();
            Items = new ObservableCollection<Item>();
            for (int i = 0; i < 5; i++)
            {
                Items.Add(new Item { FirstName = "Li" + i, LastName = "Lei" + i });
            }
            this.DataContext = this;
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int count = Items.Count;
            for (int i = count; i < count + 5; i++)
            {
                Items.Add(new Item { FirstName = "Li" + i, LastName = "Lei" + i });
            }
        }

        private async void ListBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            string selectInfo = "";
            foreach (var item in e.AddedItems)
            {
                selectInfo += (item as Item).FirstName + (item as Item).LastName;
            }
            await new MessageDialog(selectInfo).ShowAsync();
        }
    }

    public class Item
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
