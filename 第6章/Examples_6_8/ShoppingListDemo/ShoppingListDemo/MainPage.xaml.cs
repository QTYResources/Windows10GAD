using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ShoppingListDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            Loaded += MainPage_Loaded;
        }

        async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            Files.Items.Clear();
            StorageFolder storage = await ApplicationData.Current.LocalFolder.CreateFolderAsync("ShoppingList", CreationCollisionOption.OpenIfExists);
            var files = await storage.GetFilesAsync();
            {
                foreach (StorageFile file in files)
                {
                    Grid a = new Grid();
                    ColumnDefinition col = new ColumnDefinition();
                    col.Width = GridLength.Auto;
                    a.ColumnDefinitions.Add(col);
                    ColumnDefinition col2 = new ColumnDefinition();
                    col2.Width = GridLength.Auto;
                    a.ColumnDefinitions.Add(col2);
                    TextBlock txbx = new TextBlock();
                    txbx.Text = file.DisplayName;
                    txbx.HorizontalAlignment = HorizontalAlignment.Center;
                    Grid.SetColumn(txbx, 0);
                    HyperlinkButton btn = new HyperlinkButton();
                    btn.Content = "查看详细";
                    btn.Name = file.DisplayName;
                    btn.Click += (s, ea) =>
                    {
                        Frame.Navigate(typeof(DisplayPage), file);
                    };
                    Grid.SetColumn(btn, 1);

                    a.Children.Add(txbx);
                    a.Children.Add(btn);

                    Files.Items.Add(a);
                }
            }
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddItem));
        }
    }
}
