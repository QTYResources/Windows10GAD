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
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace StreamSocketDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        StreamSocketListener listener;
        StreamSocket socket;
        DataWriter writer;
        public MainPage()
        {
            InitializeComponent();
        }

        private async void btStartListener_Click(object sender, RoutedEventArgs e)
        {
            if (listener != null)
            {
                await new MessageDialog("监听已经启动了").ShowAsync();
                return;
            }
            listener = new StreamSocketListener();
            listener.ConnectionReceived += OnConnection;
            try
            {
                await listener.BindServiceNameAsync("22112");//5556 22112
                await new MessageDialog("正在监听中").ShowAsync();
            }
            catch (Exception exception)
            {
                listener = null;
                if (SocketError.GetStatus(exception.HResult) == SocketErrorStatus.Unknown)
                {
                    throw;
                }

            }
        }

        private async void OnConnection(StreamSocketListener sender, StreamSocketListenerConnectionReceivedEventArgs args)
        {
            DataReader reader = new DataReader(args.Socket.InputStream);
            try
            {
                while (true)
                {
                    uint sizeFieldCount = await reader.LoadAsync(sizeof(uint));
                    if (sizeFieldCount != sizeof(uint))
                    {
                        return;
                    }
                    uint stringLength = reader.ReadUInt32();
                    uint actualStringLength = await reader.LoadAsync(stringLength);
                    if (stringLength != actualStringLength)
                    {
                        return;
                    }
                    string msg = reader.ReadString(actualStringLength);
                    await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                    {
                        TextBlock tb = new TextBlock { Text = msg, FontSize = 20 };
                        lbMsg.Children.Add(tb);
                    });

                }
            }
            catch (Exception exception)
            {
                if (SocketError.GetStatus(exception.HResult) == SocketErrorStatus.Unknown)
                {
                    throw;
                }
            }
        }

        private async void btConnectSocket_Click(object sender, RoutedEventArgs e)
        {
            if (socket != null)
            {
                await new MessageDialog("已经连接了Socket").ShowAsync();
                return;
            }
            HostName hostName = null;
            string message = "";
            try
            {
                hostName = new HostName("localhost");
            }
            catch (ArgumentException)
            {
                message = "主机名不可用";
            }
            if (message != "")
            {
                await new MessageDialog(message).ShowAsync();
                return;
            }
            socket = new StreamSocket();
            try
            {
                await socket.ConnectAsync(hostName, "22112");
                await new MessageDialog("连接成功").ShowAsync();
            }
            catch (Exception exception)
            {
                if (SocketError.GetStatus(exception.HResult) == SocketErrorStatus.Unknown)
                {
                    throw;
                }
            }

        }

        private async void btSendMsg_Click(object sender, RoutedEventArgs e)
        {
            if (listener == null)
            {
                await new MessageDialog("监听未启动").ShowAsync();
                return;
            }
            if (socket == null)
            {
                await new MessageDialog("未连接Socket").ShowAsync();
                return;
            }
            if (writer == null)
            {
                writer = new DataWriter(socket.OutputStream);
            }
            string stringToSend = tbMsg.Text;
            writer.WriteUInt32(writer.MeasureString(stringToSend));
            writer.WriteString(stringToSend);
            try
            {
                await writer.StoreAsync();
                await new MessageDialog("发送成功").ShowAsync();
            }
            catch (Exception exception)
            {
                if (SocketError.GetStatus(exception.HResult) == SocketErrorStatus.Unknown)
                {
                    throw;
                }
            }
        }

        private void btClose_Click(object sender, RoutedEventArgs e)
        {
            if (writer != null)
            {
                writer.DetachStream();
                writer.Dispose();
                writer = null;
            }
            if (socket != null)
            {
                socket.Dispose();
                socket = null;
            }
            if (listener != null)
            {
                listener.Dispose();
                listener = null;
            }
        }
    }
}
