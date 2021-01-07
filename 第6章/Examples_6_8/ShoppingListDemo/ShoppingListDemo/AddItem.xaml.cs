using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Linq;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ShoppingListDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddItem : Page
    {
        public AddItem()
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

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private async void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (nameTxt.Text == "" || quanTxt.Text == "" || nameTxt.Text == "")
                    await new MessageDialog("请输入完整的信息").ShowAsync();
                StorageFolder storage = await ApplicationData.Current.LocalFolder.GetFolderAsync("ShoppingList");
                XmlDocument _doc = new XmlDocument();
                XmlElement _item = _doc.CreateElement(nameTxt.Text);
                _item.SetAttribute("price", priceTxt.Text);
                _item.SetAttribute("quantity", quanTxt.Text);
                _doc.AppendChild(_item);
                StorageFile file = await storage.CreateFileAsync(nameTxt.Text + ".xml", CreationCollisionOption.ReplaceExisting);
                await _doc.SaveToFileAsync(file);
                Frame.GoBack();
            }
            catch (Exception exe)
            {

            }        
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
            }
        }
    }

}
