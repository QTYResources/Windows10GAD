using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.PersonalInformation;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ContactStoreDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            bool IsPersonalInformationAPIPresent =
Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.PersonalInformation.ContactStore");

            if (!IsPersonalInformationAPIPresent)
            {
                this.IsEnabled = false;
            }
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string message = "";
            if (name.Text != "" && phone.Text != "")
            {
                try
                {
                    ContactInformation conInfo = new ContactInformation();
                    var properties = await conInfo.GetPropertiesAsync();
                    properties.Add(KnownContactProperties.Telephone, phone.Text);
                    properties.Add(KnownContactProperties.GivenName, name.Text);
                    ContactStore conStore = await ContactStore.CreateOrOpenAsync();
                    StoredContact storedContact = new StoredContact(conStore, conInfo);
                    await storedContact.SaveAsync();
                    message = "保存成功";
                }
                catch (Exception ex)
                {
                    message = "保存失败，错误信息：" + ex.Message;
                }
            }
            else
            {
                message = "名字或电话不能为空";
            }
            await new MessageDialog(message).ShowAsync();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //(Window.Current.Content as Frame).Navigate(typeof(ContactsList));
            this.Frame.Navigate(typeof(ContactsList));
        }

    }
}
