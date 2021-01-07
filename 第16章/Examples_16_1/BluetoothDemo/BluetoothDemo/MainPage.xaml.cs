using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.Proximity;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;
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

namespace BluetoothDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private StreamSocket _socket = null;
        private DataWriter _dataWriter;
        private DataReader _dataReader;
        public MainPage()
        {
            InitializeComponent();
            Loaded += MainPage_Loaded;
        }

        private async void btFindBluetooth_Click(object sender, RoutedEventArgs e)
        {
            string message = "";
            try
            {
                PeerFinder.Start();
                var peers = await PeerFinder.FindAllPeersAsync();
                if (peers.Count == 0)
                {
                    message = "没有发现对等的蓝牙应用";
                }
                else
                {
                    lbBluetoothApp.ItemsSource = peers;
                }
            }
            catch (Exception ex)
            {
                if ((uint)ex.HResult == 0x8007048F)
                {
                    message = "Bluetooth已关闭请打开的蓝牙开关";
                }
                else
                {
                    message = ex.Message;
                }
            }
            if (message != "")
                await new MessageDialog(message).ShowAsync();
        }

        private async void btConnect_Click(object sender, RoutedEventArgs e)
        {
            Button deleteButton = sender as Button;
            PeerInformation selectedPeer = deleteButton.DataContext as PeerInformation;
            _socket = await PeerFinder.ConnectAsync(selectedPeer);
            _dataReader = new DataReader(_socket.InputStream);
            _dataWriter = new DataWriter(_socket.OutputStream);
            PeerFinder_StartReader();
        }

        async void PeerFinder_StartReader()
        {
            string message = "";
            try
            {
                uint bytesRead = await _dataReader.LoadAsync(sizeof(uint));
                if (bytesRead > 0)
                {
                    uint strLength = (uint)_dataReader.ReadUInt32();
                    bytesRead = await _dataReader.LoadAsync(strLength);
                    if (bytesRead > 0)
                    {
                        string content = _dataReader.ReadString(strLength);
                        await new MessageDialog("获取到消息：" + content).ShowAsync();
                        PeerFinder_StartReader();
                    }
                    else
                    {
                        message = "对方已关闭连接";
                    }
                }
                else
                {
                    message = "对方已关闭连接";
                }
            }
            catch (Exception e)
            {
                message = "读取失败: " + e.Message;
            }
            if (message != "")
                await new MessageDialog(message).ShowAsync();
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            PeerFinder.ConnectionRequested += PeerFinder_ConnectionRequested;
        }

        async void PeerFinder_ConnectionRequested(object sender, ConnectionRequestedEventArgs args)
        {

            await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
            {
                MessageDialog md = new MessageDialog("是否接收" + args.PeerInformation.DisplayName + "连接请求", "蓝牙连接");
                UICommand yes = new UICommand("接收");
                UICommand no = new UICommand("拒绝");
                md.Commands.Add(yes);
                md.Commands.Add(no);
                var result = await md.ShowAsync();
                if (result == yes)
                {
                    ConnectToPeer(args.PeerInformation);
                }
            });
        }

        async void ConnectToPeer(PeerInformation peer)
        {
            _socket = await PeerFinder.ConnectAsync(peer);
            _dataReader = new DataReader(_socket.InputStream);
            _dataWriter = new DataWriter(_socket.OutputStream);
            string message = "测试消息";
            uint strLength = _dataWriter.MeasureString(message);
            _dataWriter.WriteUInt32(strLength);
            _dataWriter.WriteString(message);
            uint numBytesWritten = await _dataWriter.StoreAsync();
        }
    }
}
