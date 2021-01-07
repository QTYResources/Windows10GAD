using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FlyoutDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            listPickerFlyout.ItemsSource = new List<string> { "诺基亚", "三星", "HTC", "苹果", "华为" };
        }

        private async void PickerFlyout_Confirmed(PickerFlyout sender, PickerConfirmedEventArgs args)
        {
            await new MessageDialog("你点击了确定").ShowAsync();
        }

        private async void TimePickerFlyout_TimePicked(TimePickerFlyout sender, TimePickedEventArgs args)
        {
            await new MessageDialog(args.NewTime.ToString()).ShowAsync();
        }

        private async void DatePickerFlyout_DatePicked(DatePickerFlyout sender, DatePickedEventArgs args)
        {
            await new MessageDialog(args.NewDate.ToString()).ShowAsync();
        }

        private void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            menuFlyoutButton.Content = (sender as MenuFlyoutItem).Text;
        }

        private void TextBlock_Tapped(object sender, TappedRoutedEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;
            if (element != null)
            {
                FlyoutBase.ShowAttachedFlyout(element);
            }
        }
        private async void listPickerFlyout_ItemsPicked(ListPickerFlyout sender, ItemsPickedEventArgs args)
        {
            if (sender.SelectedItem != null)
            {
                await new MessageDialog("你选择的是：" + sender.SelectedItem.ToString()).ShowAsync();
            }
        }
    }
}
