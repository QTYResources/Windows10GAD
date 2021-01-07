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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DateTimeDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            info.Text = "时间：" + time.Time.ToString() + " 日期：" + date.Date.ToString();
        }
        private void date_DateChanged(object sender, DatePickerValueChangedEventArgs e)
        {
            info.Text = "时间：" + time.Time.ToString() + " 日期改变为：" + date.Date.ToString();
        }

        private void time_TimeChanged(object sender, TimePickerValueChangedEventArgs e)
        {
            info.Text = "时间改变为：" + time.Time.ToString() + " 日期：" + date.Date.ToString();
        }
    }
}
