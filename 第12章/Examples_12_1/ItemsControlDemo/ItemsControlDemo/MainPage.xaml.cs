using System;
using System.Collections.Generic;
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

namespace ItemsControlDemo
{

    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            List<Item> items = new List<Item>();
            for (int i = 0; i < 100; i++)
            {
                items.Add(new Item { FirstName = "Li" + i, LastName = "Lei" + i });
            }
            itemsControl.ItemsSource = items;
        }
    }
    public class Item
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
