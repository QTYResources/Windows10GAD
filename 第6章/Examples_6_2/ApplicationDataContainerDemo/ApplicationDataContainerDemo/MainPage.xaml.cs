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

namespace ApplicationDataContainerDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        ApplicationDataContainer localSettings = null;
        const string containerName = "exampleContainer";
        const string settingName = "exampleSetting";
        public MainPage()
        {
            this.InitializeComponent();
            localSettings = ApplicationData.Current.LocalSettings;
            DisplayOutput();
        }

        void CreateContainer_Click(Object sender, RoutedEventArgs e)
        {
            ApplicationDataContainer container = localSettings.CreateContainer(containerName, ApplicationDataCreateDisposition.Always);
            DisplayOutput();
        }

        void DeleteContainer_Click(Object sender, RoutedEventArgs e)
        {
            localSettings.DeleteContainer(containerName);
            DisplayOutput();
        }

        void WriteSetting_Click(Object sender, RoutedEventArgs e)
        {
            if (localSettings.Containers.ContainsKey(containerName))
            {
                localSettings.Containers[containerName].Values[settingName] = "Hello World";
            }
            DisplayOutput();
        }

        void DeleteSetting_Click(Object sender, RoutedEventArgs e)
        {
            if (localSettings.Containers.ContainsKey(containerName))
            {
                localSettings.Containers[containerName].Values.Remove(settingName);
            }
            DisplayOutput();
        }

        void DisplayOutput()
        {
            bool hasContainer = localSettings.Containers.ContainsKey(containerName);
            bool hasSetting = hasContainer ? localSettings.Containers[containerName].Values.ContainsKey(settingName) : false;
            String output = String.Format("Container Exists: {0}\n" +
                                          "Setting Exists: {1}",
                                          hasContainer ? "true" : "false",
                                          hasSetting ? "true" : "false");
            OutputTextBlock.Text = output;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }
    }
}
