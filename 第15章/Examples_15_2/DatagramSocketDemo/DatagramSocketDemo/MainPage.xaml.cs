using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace DatagramSocketDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        public MainPage()
        {
            InitializeComponent();
        }
        DatagramSocket datagramSocket;
        DataWriter writer;
        private async void listener_Click(object sender, RoutedEventArgs e)
        {
            DatagramSocket datagramSocket = new DatagramSocket();
            datagramSocket.MessageReceived += datagramSocket_MessageReceived;
            try
            {
                await datagramSocket.BindServiceNameAsync("22112");
                msgList.Children.Add(new TextBlock { Text = "监听成功", FontSize = 20 });
            }
            catch (Exception err)
            {
                if (SocketError.GetStatus(err.HResult) == SocketErrorStatus.AddressAlreadyInUse)
                {
                    // 异常消息，使用SocketErrorStatus枚举来判断Socket的异常类型
                }
            }
        }

        async void datagramSocket_MessageReceived(DatagramSocket sender, DatagramSocketMessageReceivedEventArgs args)
        {
            DataReader dataReader = args.GetDataReader();
            uint length = dataReader.UnconsumedBufferLength;
            string content = dataReader.ReadString(length);
            await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            msgList.Children.Add(new TextBlock { Text = "服务器收到的消息：" + content, FontSize = 20 }));
            IOutputStream outputStream = await sender.GetOutputStreamAsync(
                   args.RemoteAddress,
                   args.RemotePort);
            DataWriter writer = new DataWriter(outputStream);
            writer.WriteString(content + "（服务器发送）");
            try
            {
                await writer.StoreAsync();
                await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                    msgList.Children.Add(new TextBlock { Text = "服务器发送的消息：" + content + "（服务器发送）", FontSize = 20 }));
            }
            catch (Exception err)
            {
                if (SocketError.GetStatus(err.HResult) == SocketErrorStatus.AddressAlreadyInUse)
                {
                    // 异常消息，使用SocketErrorStatus枚举来判断Socket的异常类型
                }
            }
        }


        async void datagramSocket_MessageReceived2(DatagramSocket sender, DatagramSocketMessageReceivedEventArgs args)
        {
            try
            {
                DataReader dataReader = args.GetDataReader();
                uint length = dataReader.UnconsumedBufferLength;
                string content = dataReader.ReadString(length);
                await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
               msgList.Children.Add(new TextBlock { Text = "客户端收到的消息：" + content, FontSize = 20 }));
            }
            catch (Exception exception)
            {
                SocketErrorStatus socketError = SocketError.GetStatus(exception.HResult);
                if (socketError == SocketErrorStatus.ConnectionResetByPeer)
                {
                    // 异常消息，使用SocketErrorStatus枚举来判断Socket的异常类型
                }
                else if (socketError != SocketErrorStatus.Unknown)
                {

                }
                else
                {

                }
            }
        }

        private async void send_Click(object sender, RoutedEventArgs e)
        {
            if (writer == null)
            {
                if (datagramSocket == null)
                {
                    HostName hostName = new HostName("localhost");
                    datagramSocket = new DatagramSocket();
                    datagramSocket.MessageReceived += datagramSocket_MessageReceived2;
                    IOutputStream outputStream = await datagramSocket.GetOutputStreamAsync(hostName, "22112");
                    writer = new DataWriter(outputStream);
                }
                else
                {
                    writer = new DataWriter(datagramSocket.OutputStream);
                }
            }


            writer.WriteString("test");
            try
            {
                await writer.StoreAsync();
                msgList.Children.Add(new TextBlock { Text = "客户端发送的消息：" + "test", FontSize = 20 });
            }
            catch (Exception err)
            {
                if (SocketError.GetStatus(err.HResult) == SocketErrorStatus.AddressAlreadyInUse)
                {
                    // 异常消息，使用SocketErrorStatus枚举来判断Socket的异常类型
                }
            }
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            if (datagramSocket != null)
            {
                datagramSocket.Dispose();
                datagramSocket = null;
            }
            if (writer != null)
            {
                writer.Dispose();
                writer = null;
            }
        }
    }
}
