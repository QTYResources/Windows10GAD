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
    public sealed partial class EditContact : Page
    {
        // 联系人数据存储
        private ContactStore conStore;
        // 联系人对象
        private StoredContact storCon;
        // 联系人属性字典
        private IDictionary<string, object> properties;
        public EditContact()
        {
            InitializeComponent();
        }
        // 进入页面事件处理
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // 通过联系人的id获取联系人的信息
            if (e.Parameter != null)
                GetContact(e.Parameter.ToString());
        }

        // 保存编辑的联系人
        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (name.Text != "" && phone.Text != "")
            {
                storCon.GivenName = name.Text;
                properties[KnownContactProperties.Telephone] = phone.Text;
                await storCon.SaveAsync();//保存联系人
                //返回上一个页面
                (Window.Current.Content as Frame).GoBack();
            }
            else
            {
                await new MessageDialog("名字或者电话不能为空").ShowAsync();
            }
        }
        // 获取需要编辑的联系人信息
        async private void GetContact(string id)
        {
            conStore = await ContactStore.CreateOrOpenAsync();
            storCon = await conStore.FindContactByIdAsync(id);
            properties = await storCon.GetPropertiesAsync();
            name.Text = storCon.GivenName;
            phone.Text = properties[KnownContactProperties.Telephone].ToString();
        }
    }
}
