using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.PersonalInformation;
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
    public sealed partial class ContactsList : Page
    {
        //联系人存储
        private ContactStore conStore;
        public ContactsList()
        {
            InitializeComponent();
        }
        // 进入页面事件
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            GetContacts();
        }
        // 获取联系人列表
        async private void GetContacts()
        {
            conStore = await ContactStore.CreateOrOpenAsync();
            ContactQueryResult conQueryResult = conStore.CreateContactQuery();
            // 查询联系人
            IReadOnlyList<StoredContact> conList = await conQueryResult.GetContactsAsync();
            List<Item> list = new List<Item>();
            foreach (StoredContact storCon in conList)
            {
                var properties = await storCon.GetPropertiesAsync();
                list.Add(
                    new Item
                    {
                        Name = storCon.FamilyName + storCon.GivenName,
                        Id = storCon.Id,
                        Phone = properties[KnownContactProperties.Telephone].ToString()
                    });
            }
            conListBox.ItemsSource = list;
        }
        // 删除联系人事件处理
        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Button deleteButton = sender as Button;
            Item deleteItem = deleteButton.DataContext as Item;
            await conStore.DeleteContactAsync(deleteItem.Id);
            GetContacts();
        }
        // 跳转到编辑联系人页面
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Button deleteButton = sender as Button;
            Item editItem = deleteButton.DataContext as Item;
            (Window.Current.Content as Frame).Navigate(typeof(EditContact), editItem.Id);
        }

    }
    // 自定义绑定的联系人数据对象
    class Item
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public string Phone { get; set; }
    }
}
