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

namespace AccountBook
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ChartPage : Page
    {
        public ChartPage()
        {
            this.InitializeComponent();
            if (App.IsHardwareButtonsAPIPresent)
            {
                backButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                backButton.Visibility = Visibility.Visible;
            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
            }
        }

        // 导航进入界面的事件处理程序
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            // 创建图表的数据源对象
            ObservableCollection<ChartData> collecion = new ObservableCollection<ChartData>();
            // 获取所有的记账记录
            IEnumerable<Voucher> allRecords = await Common.GetAllRecords();
            // 获取所有记账记录里面的类别
            IEnumerable<string> enumerable2 = (from c in allRecords select c.Category).Distinct<string>();
            // 按照类别来统计记账的数目
            foreach (var item in enumerable2)
            {
                // 获取该类别下的钱的枚举集合
                IEnumerable<double> enumerable3 = from c in allRecords.Where<Voucher>(c => c.Category == item) select c.Money;
                // 添加一条图表的数据
                ChartData data = new ChartData
                {
                    Sum = enumerable3.Sum(),
                    TypeName = item
                };
                collecion.Add(data);
            }
            // 设置饼图的数据源
            pie1.DataSource = collecion;
            // 设置柱形图形的数据源
            chart1.DataSource = collecion;
        }
    }
}
