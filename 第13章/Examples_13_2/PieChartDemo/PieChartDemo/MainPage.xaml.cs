using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace PieChartDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            List<PieDataItem> datas = new List<PieDataItem>();
            datas.Add(new PieDataItem { Value = 30, Brush = new SolidColorBrush(Colors.Red) });
            datas.Add(new PieDataItem { Value = 40, Brush = new SolidColorBrush(Colors.Orange) });
            datas.Add(new PieDataItem { Value = 50, Brush = new SolidColorBrush(Colors.Blue) });
            datas.Add(new PieDataItem { Value = 30, Brush = new SolidColorBrush(Colors.LightGray) });
            datas.Add(new PieDataItem { Value = 20, Brush = new SolidColorBrush(Colors.Purple) });
            datas.Add(new PieDataItem { Value = 40, Brush = new SolidColorBrush(Colors.Green) });
            piePlotter.DataContext = datas;
            piePlotter.ShowPie();
        }
    }
}
