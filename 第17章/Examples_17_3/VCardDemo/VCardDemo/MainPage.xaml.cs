using System;
using System.Collections.Generic;
using System.Diagnostics;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace VCardDemo
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
        ContactStore contactStore;
        StoredContact storedContact;
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            contactStore = await ContactStore.CreateOrOpenAsync(ContactStoreSystemAccessMode.ReadWrite, ContactStoreApplicationAccessMode.ReadOnly);
        }

        private async void add_Click(object sender, RoutedEventArgs e)
        {
            ContactInformation contactInformation = new ContactInformation();
            var properties = await contactInformation.GetPropertiesAsync();
            properties.Add(KnownContactProperties.FamilyName, "张");
            properties.Add(KnownContactProperties.GivenName, "三");
            properties.Add(KnownContactProperties.Email, "1111@qq.com");
            properties.Add(KnownContactProperties.CompanyName, "挪鸡鸭");
            properties.Add(KnownContactProperties.Telephone, "12345678");
            storedContact = new StoredContact(contactStore, contactInformation);
            await storedContact.SaveAsync();
            await new MessageDialog("保存成功").ShowAsync();
        }

        private async void getvcard_Click(object sender, RoutedEventArgs e)
        {
            if (storedContact != null)
            {
                var stream = await storedContact.ToVcardAsync(VCardFormat.Version2_1);
                byte[] datas = StreamToBytes(stream.AsStreamForRead());
                string vcard = System.Text.Encoding.UTF8.GetString(datas, 0, datas.Length);
                vcardTb.Text = vcard;
            }
            else
            {
                await new MessageDialog("请先创建联系人").ShowAsync();
            }
        }


        private async void savevcard_Click(object sender, RoutedEventArgs e)
        {
            string message;
            if (vcardTb.Text != "")
            {
                byte[] datas = System.Text.Encoding.UTF8.GetBytes(vcardTb.Text);
                Stream stream = BytesToStream(datas);
                try
                {
                    ContactInformation contactInformation = await ContactInformation.ParseVcardAsync(stream.AsInputStream());
                    storedContact = new StoredContact(contactStore, contactInformation);
                    await storedContact.SaveAsync();
                    message = "保存成功";
                }
                catch (Exception exe)
                {
                    message = "vcard格式有误,异常：" + exe.Message;
                }
            }
            else
            {
                message = "vcard不能为空";
            }
            await new MessageDialog(message).ShowAsync();
        }

        public byte[] StreamToBytes(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            return bytes;
        }

        public Stream BytesToStream(byte[] bytes)
        {
            Stream stream = new MemoryStream(bytes);
            return stream;
        }
    }
}
