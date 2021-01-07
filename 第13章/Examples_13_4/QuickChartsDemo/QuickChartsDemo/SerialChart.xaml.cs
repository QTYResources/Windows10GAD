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

namespace QuickChartsDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SerialChart : Page
    {
        public SerialChart()
        {
            InitializeComponent();
            this.Loaded += PhoneApplicationPage_Loaded;
        }

        private ObservableCollection<TestDataItem> _data = new ObservableCollection<TestDataItem>()
        {
            new TestDataItem() { cat1 = "cat1", val1=5, val2=15, val3=12},
            new TestDataItem() { cat1 = "cat2", val1=15.2, val2=1.5, val3=2.1M},
            new TestDataItem() { cat1 = "cat3", val1=25, val2=5, val3=2},
            new TestDataItem() { cat1 = "cat4", val1=8.1, val2=1, val3=8},
            new TestDataItem() { cat1 = "cat5", val1=8.1, val2=1, val3=4},
            new TestDataItem() { cat1 = "cat6", val1=8.1, val2=1, val3=10},
        };

        public ObservableCollection<TestDataItem> Data { get { return _data; } }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = this;
        }
    }

    public class TestDataItem
    {
        public string cat1 { get; set; }
        public double val1 { get; set; }
        public double val2 { get; set; }
        public decimal val3 { get; set; }
    }
}
