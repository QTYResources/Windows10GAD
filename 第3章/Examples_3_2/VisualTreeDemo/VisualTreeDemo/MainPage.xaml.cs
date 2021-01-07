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

namespace VisualTreeDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        string visulTreeStr = "";
        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            visulTreeStr = "";
            GetChildType(stackPanel);
            MessageDialog messageDialog = new MessageDialog(visulTreeStr);
            await messageDialog.ShowAsync();
        }

        public void GetChildType(DependencyObject reference)
        {
            int count = VisualTreeHelper.GetChildrenCount(reference);
            if (count > 0)
            {
                for (int i = 0; i <= count - 1; i++)
                {
                    var child = VisualTreeHelper.GetChild(reference, i);
                    visulTreeStr += child.GetType().ToString() + count + "  ";
                    GetChildType(child);
                }
            }
        }
    }
}
