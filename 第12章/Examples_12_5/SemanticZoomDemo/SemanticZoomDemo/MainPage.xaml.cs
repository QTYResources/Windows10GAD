using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SemanticZoomDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            List<Item> mainItem = new List<Item>();
            for (int i = 0; i < 10; i++)
            {
                mainItem.Add(new Item { Content = "A类别", Title = "Test A" + i });
                mainItem.Add(new Item { Content = "B类别", Title = "Test B" + i });
                mainItem.Add(new Item { Content = "C类别", Title = "Test C" + i });
            }
            List<ItemInGroup> Items = (from item in mainItem group item by item.Content into newItems select new ItemInGroup { Key = newItems.Key, ItemContent = newItems.ToList() }).ToList();
            this.itemcollectSource.Source = Items;
            // 分别对两个视图进行绑定 
            outView.ItemsSource = itemcollectSource.View.CollectionGroups;
            inView.ItemsSource = itemcollectSource.View;
        }
    }

    public class ItemInGroup
    {
        public string Key { get; set; }

        public List<Item> ItemContent { get; set; }
    }

    public class Item
    {
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
