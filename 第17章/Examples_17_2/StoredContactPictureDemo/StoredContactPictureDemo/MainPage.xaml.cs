using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.PersonalInformation;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace StoredContactPictureDemo
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
                add.IsEnabled = false;
            }

        }


        private async void bt_add_Click(object sender, RoutedEventArgs e)
        {
            ContactStore contactStore = await ContactStore.CreateOrOpenAsync(ContactStoreSystemAccessMode.ReadWrite, ContactStoreApplicationAccessMode.ReadOnly);
            ContactInformation contactInformation = new ContactInformation();
            var properties = await contactInformation.GetPropertiesAsync();
            properties.Add(KnownContactProperties.FamilyName, "test");
            properties.Add(KnownContactProperties.Telephone, "12345678");
            StoredContact storedContact = new StoredContact(contactStore, contactInformation);
            StorageFile imagefile = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFileAsync("image.png");
            var stream = await imagefile.OpenStreamForReadAsync();
            IInputStream inputStream = stream.AsInputStream();
            await storedContact.SetDisplayPictureAsync(inputStream);
            await storedContact.SaveAsync();

            IRandomAccessStream raStream = await storedContact.GetDisplayPictureAsync();
            BitmapImage bi = new BitmapImage();
            bi.SetSource(raStream);
            image.Source = bi;
        }
    }
}
