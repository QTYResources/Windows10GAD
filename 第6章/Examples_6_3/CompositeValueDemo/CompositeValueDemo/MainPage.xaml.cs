using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CompositeValueDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        ApplicationDataContainer roamingSettings = null;
        const string settingName = "exampleCompositeSetting";
        const string settingName1 = "one";
        const string settingName2 = "two";
        public MainPage()
        {
            this.InitializeComponent();
            roamingSettings = ApplicationData.Current.RoamingSettings;
            DisplayOutput();
        }

        void WriteCompositeSetting_Click(Object sender, RoutedEventArgs e)
        {
            ApplicationDataCompositeValue composite = new ApplicationDataCompositeValue();
            composite[settingName1] = 1;
            composite[settingName2] = "world";
            roamingSettings.Values[settingName] = composite;
            DisplayOutput();
        }

        void DeleteCompositeSetting_Click(Object sender, RoutedEventArgs e)
        {
            roamingSettings.Values.Remove(settingName);
            DisplayOutput();
        }

        void DisplayOutput()
        {
            ApplicationDataCompositeValue composite = (ApplicationDataCompositeValue)roamingSettings.Values[settingName];
            String output;
            if (composite == null)
            {
                output = "复合设置信息为空";
            }
            else
            {
                output = String.Format("复合设置: {{{0} = {1}, {2} = \"{3}\"}}", settingName1, composite[settingName1], settingName2, composite[settingName2]);
            }
            OutputTextBlock.Text = output;
        }
    }
}
