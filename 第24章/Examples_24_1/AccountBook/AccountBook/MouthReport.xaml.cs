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

namespace AccountBook
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MouthReport : Page
    {

        public MouthReport()
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
        // 当前记录的月份
        private int mouth;
        // 当前记录的年份
        private int year;
        // 导航进入界面的事件处理程序
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            mouth = DateTime.Now.Month;
            year = DateTime.Now.Year;
            DisplayVoucherData();
        }

        // 处理菜单栏单击事件
        private void ApplicationBarIconButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                switch ((sender as AppBarButton).Label)
                {
                    case "上一月":
                        this.mouth--;
                        if (this.mouth <= 0)
                        {
                            this.year--;
                            this.mouth = 12;
                        }
                        break;
                    case "下一月":
                        this.mouth++;
                        if (this.mouth >= 12)
                        {
                            this.year++;
                            this.mouth = 1;
                        }
                        break;
                }
                DisplayVoucherData();
            }
            catch
            {
            }
        }
        // 展现记账的数据
        private async void DisplayVoucherData()
        {
            //本月的收入
            double inSum = await Common.GetMouthSummaryIncome(mouth, year);
            //本月的支出
            double exSum = await Common.GetMouthSummaryExpenses(mouth, year);
            //显示本月收入
            inTB.Text = "收入:" + inSum;
            //显示本月支出
            exTB.Text = "支出:" + exSum;
            //显示本月结余
            balanceTB.Text = "结余:" + (inSum - exSum);
            //绑定当前月份的记账记录
            listMouthReport.ItemsSource = await Common.GetThisMonthAllRecords(mouth, year);
            PageTitle.Text = year + "年" + mouth + "月";
        }
    }
}
