using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
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
    public sealed partial class AddAccount : Page
    {
        public AddAccount()
        {
            this.InitializeComponent();
            AddListPickerItems();
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
        // 添加下拉框的数据
        private void AddListPickerItems()
        {
            // 支出类别的信息
            listPickerExpenses.Items.Add("房租");
            listPickerExpenses.Items.Add("娱乐");
            listPickerExpenses.Items.Add("餐饮");
            listPickerExpenses.Items.Add("交通");
            listPickerExpenses.Items.Add("其他");
            listPickerExpenses.SelectedIndex = 0;
            // 收入类别的信息
            listPickerIncome.Items.Add("工资");
            listPickerIncome.Items.Add("股票");
            listPickerIncome.Items.Add("投资");
            listPickerIncome.Items.Add("其他");
            listPickerIncome.SelectedIndex = 0;
        }
        // 导航到当前页面的时间处理程序
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // 根据传递进来的参数，判断是显示收入页面（0）还是支出页面（1）
            if (e.Parameter != null)
            {
                if (e.Parameter.ToString() == "0")
                {
                    pivot.SelectedIndex = 0;
                }
                else
                {
                    pivot.SelectedIndex = 1;
                }
            }
        }
        //新增一条记账记录
        private async void appbar_buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            //用于隐藏软键盘
            pivot.Focus(FocusState.Pointer);
            await SaveVoucher();
        }
        //新增一条记账记录并返回
        private async void appbar_buttonFinish_Click(object sender, RoutedEventArgs e)
        {
            if (await SaveVoucher())
            {
                //保存成功则返回上一页
                Frame.GoBack();
            }
        }
        //返回
        private void appbar_buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
        // 保存记账数据
        private async Task<bool> SaveVoucher()
        {
            string erro = "";
            try
            {
                if (pivot.SelectedIndex == 0)
                {//收入
                    if (this.textBox_Income.Text.Trim() == "")
                    {
                        await new MessageDialog("金额不能为空").ShowAsync();
                        return false;
                    }
                    else
                    {
                        //一条记账记录的对象
                        Voucher voucher = new Voucher
                        {
                            Money = double.Parse(this.textBox_Income.Text),
                            Desc = this.textBox_IncomeDesc.Text,
                            DT = DatePickerIncome.Date.Date.Add(TimePickerIncome.Time),
                            Category = listPickerIncome.SelectedItem.ToString(),
                            Type = 0
                        };
                        //添加一条记录
                        App.voucherHelper.AddNew(voucher);
                    }
                }
                else
                {//支出
                    if (this.textBox_Expenses.Text.Trim() == "")
                    {
                        await new MessageDialog("金额不能为空").ShowAsync();
                        return false;
                    }
                    else
                    {
                        //一条记账记录的对象
                        Voucher voucher = new Voucher
                        {
                            Money = double.Parse(this.textBox_Expenses.Text),
                            Desc = this.textBox_ExpensesDesc.Text,
                            DT = DatePickerExpenses.Date.Date.Add(TimePickerExpenses.Time),
                            Category = listPickerExpenses.SelectedItem.ToString(),
                            Type = 1
                        };
                        //添加一条记录
                        App.voucherHelper.AddNew(voucher);
                    }
                }
            }
            catch (Exception ee)
            {
                erro = ee.Message;
            }
            if (erro != "")
            {
                await new MessageDialog(erro).ShowAsync();
                return false;
            }
            else
            {
                // 保存数据
                App.voucherHelper.SaveToFile();
                await new MessageDialog("保存成功").ShowAsync();
                return true;
            }
        }
    }
}
