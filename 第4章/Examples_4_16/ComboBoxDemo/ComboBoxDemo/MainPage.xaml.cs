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

namespace ComboBoxDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            List<Man> datas = new List<Man>{
                new Man{ Name="张三", Age=20},
                new Man{ Name="李四", Age=34},
                new Man{ Name="黎明", Age=43},
                new Man{ Name="刘德华", Age=33},
                new Man{ Name="张学友", Age=44},
            };
            comboBox2.ItemsSource = datas;
        }

        private void comboBox2_DropDownClosed(object sender, object e)
        {
            if (comboBox2.SelectedItem != null)
            {
                Man man = comboBox2.SelectedItem as Man;
                Info.Text = "name:" + man.Name + "  age:" + man.Age;
            }
        }
    }

    public class Man
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
