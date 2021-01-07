using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization.Json;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace JsonDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ApplicationDataContainer _appSettings;
        private const string UserDataKey = "UserDataKey";
        public MainPage()
        {
            this.InitializeComponent();
            _appSettings = ApplicationData.Current.LocalSettings;
        }

        private async void save_Click(object sender, RoutedEventArgs e)
        {
            if (userName.Text == "" || userAge.Text == "")
            {
                await new MessageDialog("请输入完整的信息").ShowAsync();
                return;
            }
            ObservableCollection<School> education = new ObservableCollection<School>();
            if (school1.IsChecked == true)
            {
                education.Add(new School { Id = "id001", Name = school1.Content.ToString() });
            }
            if (school2.IsChecked == true)
            {
                education.Add(new School { Id = "id002", Name = school2.Content.ToString() });
            }
            User user = new User { Education = education, Id = Guid.NewGuid().ToString(), Name = userName.Text, Age = Int32.Parse(userAge.Text), Verified = false };
            //string json=ToJsonData(user);
            string json = user.Stringify();
            info.Text = json;
            _appSettings.Values[UserDataKey] = json;
            await new MessageDialog("保存成功").ShowAsync();
        }

        private async void get_Click(object sender, RoutedEventArgs e)
        {
            if (!_appSettings.Values.ContainsKey(UserDataKey))
            {
                await new MessageDialog("未保存信息").ShowAsync();
                return;
            }
            string json = _appSettings.Values[UserDataKey].ToString();
            //User user = DataContractJsonDeSerializer<User>(json);
            User user = new User(json);
            string userInfo = "";
            userInfo = "Id:" + user.Id + " Name:" + user.Name + " Age:" + user.Age;
            foreach (var item in user.Education)
            {
                userInfo += " Education:" + "Id:" + item.Id + " Name:" + item.Name;
            }
            await new MessageDialog(userInfo).ShowAsync();
        }

        public T DataContractJsonDeSerializer<T>(string jsonString)
        {
            var ds = new DataContractJsonSerializer(typeof(T));
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            T obj = (T)ds.ReadObject(ms);
            ms.Dispose();
            return obj;
        }

        public string ToJsonData(object item)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(item.GetType());
            string result = String.Empty;
            using (MemoryStream ms = new MemoryStream())
            {
                serializer.WriteObject(ms, item);
                ms.Position = 0;
                using (StreamReader reader = new StreamReader(ms))
                {
                    result = reader.ReadToEnd();
                }
            }
            return result;
        }
    }
}
