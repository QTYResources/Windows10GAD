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
    public sealed partial class YearReport : Page
    {

        public YearReport()
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
        private int year;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            year = DateTime.Now.Year;
            DisplayVoucherData();
        }

        private void ApplicationBarIconButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                switch ((sender as AppBarButton).Label)
                {
                    case "上一年":
                        this.year--;
                        break;
                    case "下一年":
                        this.year++;
                        break;
                }
                DisplayVoucherData();
            }
            catch
            {
            }
        }

        private async void DisplayVoucherData()
        {
            double inSum = await Common.GetYearSummaryIncome(year);
            double exSum = await Common.GetYearSummaryExpenses(year);
            inTB.Text = "收入:" + inSum;
            exTB.Text = "支出:" + exSum;
            balanceTB.Text = "结余:" + (inSum - exSum);
            listYearReport.ItemsSource = await Common.GetThisYearAllRecords(year);
            PageTitle.Text = year + "年";
        }
    }
}
