using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.Proximity;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace NFCMessageDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        private ProximityDevice _proximityDevice;
        private long _publishedMessageId = -1;
        private long _subscribedMessageId = -1;
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            _proximityDevice = ProximityDevice.GetDefault();
            if (_proximityDevice != null)
            {
                _proximityDevice.DeviceArrived += _device_DeviceArrived;
                _proximityDevice.DeviceDeparted += _device_DeviceDeparted;
            }
            else
            {
                await new MessageDialog("你的设备不支持NFC功能").ShowAsync();
            }
        }

        async void _device_DeviceDeparted(ProximityDevice sender)
        {
            await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => state.Text = "当前不处于NFC通信的范围内");
        }

        async void _device_DeviceArrived(ProximityDevice sender)
        {
            await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => state.Text = "当前处于NFC通信的范围内");
        }

        private async void btSend_Click_1(object sender, RoutedEventArgs e)
        {
            if (_proximityDevice == null)
            {
                await new MessageDialog("你的设备不支持NFC功能").ShowAsync();
                return;
            }
            if (_publishedMessageId == -1)
            {
                String publishText = tbSendMessage.Text;
                tbSendMessage.Text = "";
                if (publishText.Length > 0)
                {
                    _publishedMessageId = _proximityDevice.PublishMessage("Windows.SampleMessageType", publishText);
                    await new MessageDialog("消息已经发送，可以接触其他的设备来进行接收消息").ShowAsync();
                }
                else
                {
                    await new MessageDialog("发送的消息不能为空").ShowAsync();
                }
            }
            else
            {
                await new MessageDialog("消息已经发送，请接收").ShowAsync();
            }
        }

        private async void btReceive_Click_1(object sender, RoutedEventArgs e)
        {
            if (_proximityDevice == null)
            {
                await new MessageDialog("你的设备不支持NFC功能").ShowAsync();
                return;
            }
            if (_subscribedMessageId == -1)
            {
                _subscribedMessageId = _proximityDevice.SubscribeForMessage("Windows.SampleMessageType", MessageReceived);
                await new MessageDialog("订阅NFC接收的消息成功").ShowAsync();
            }
            else
            {
                await new MessageDialog("已订阅NFC接收的消息").ShowAsync();
            }
        }

        async void MessageReceived(ProximityDevice proximityDevice, ProximityMessage message)
        {
            await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => tbReceiveMessage.Text = message.DataAsString);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            if (_proximityDevice != null)
            {
                if (_publishedMessageId != -1)
                {
                    _proximityDevice.StopPublishingMessage(_publishedMessageId);
                    _publishedMessageId = -1;
                }
                if (_subscribedMessageId != -1)
                {
                    _proximityDevice.StopSubscribingForMessage(_subscribedMessageId);
                    _subscribedMessageId = 1;
                }
            }
        }

    }
}
