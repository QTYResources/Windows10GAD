using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ListViewDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public ObservableCollection<Item> Items { get; set; }
        public bool IsLoading = false;
        private object o = new object();

        public MainPage()
        {
            this.InitializeComponent();
            Items = new ObservableCollection<Item>();
            for (int i = 0; i < 100; i++)
            {
                Items.Add(new Item { FirstName = "Li" + i, LastName = "Lei" + i });
            }
            this.DataContext = this;
            listView.ContainerContentChanging += listView_ContainerContentChanging;
        }

        void listView_ContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            lock (o)
            {
                if (!IsLoading)
                {
                    if (args.ItemIndex == listView.Items.Count - 1)
                    {
                        IsLoading = true;
                        Task.Factory.StartNew(async () =>
                        {
                            await Task.Delay(3000);
                            await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                            {
                                int count = Items.Count;
                                for (int i = count; i < count + 50; i++)
                                {
                                    Items.Add(new Item { FirstName = "Li" + i, LastName = "Lei" + i });
                                }
                                IsLoading = false;
                            });

                        });
                    }
                }
            }
        }

    }
    public class Item
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
