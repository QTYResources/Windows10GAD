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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ObservableCollectionDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        ObservableCollection<OrderModel> OrderModels = new ObservableCollection<OrderModel>();
        public MainPage()
        {
            this.InitializeComponent();
            list.ItemsSource = OrderModels;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Random random = new Random();
            OrderModels.Add(new OrderModel { OrderID = random.Next(1000), OrderName = "OrderName" + random.Next(1000) });
        }

        private void DelButton_Click(object sender, RoutedEventArgs e)
        {
            if (list.SelectedItem != null)
            {
                OrderModel orderModel = list.SelectedItem as OrderModel;
                if (OrderModels.Contains(orderModel))
                {
                    OrderModels.Remove(orderModel);
                }
            }
        }
    }
}
