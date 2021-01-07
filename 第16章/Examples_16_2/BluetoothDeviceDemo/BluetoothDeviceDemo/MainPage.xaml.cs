using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.Proximity;
using Windows.Networking.Sockets;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace BluetoothDeviceDemo
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

        StreamSocket streamSocket;

        private async void btFindBluetooth_Click(object sender, RoutedEventArgs e)
        {
            string errMessage = "";
            try
            {
                PeerFinder.AlternateIdentities["Bluetooth:SDP"] = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX";
                var pairedDevices = await PeerFinder.FindAllPeersAsync();
                if (pairedDevices.Count == 0)
                {
                    await new MessageDialog("没有找到相关的蓝牙设备").ShowAsync();
                }
                else
                {
                    PeerInformation selectedPeer = pairedDevices[0];
                    StreamSocket socket = new StreamSocket();
                    await socket.ConnectAsync(selectedPeer.HostName, selectedPeer.ServiceName);
                    await new MessageDialog("连接上了HostName:" + selectedPeer.HostName + "ServiceName:" + selectedPeer.ServiceName).ShowAsync();
                }
            }
            catch (Exception ex)
            {
                if ((uint)ex.HResult == 0x8007048F)
                {
                    errMessage = "Bluetooth is turned off";
                }
                else
                {
                    errMessage = ex.Message;
                }
            }
            if (errMessage != "")
            {
                await new MessageDialog(errMessage).ShowAsync();
            }

        }

        private async void btFindAllBluetooth_Click(object sender, RoutedEventArgs e)
        {
            string errMessage = "";
            try
            {
                PeerFinder.AlternateIdentities["Bluetooth:Paired"] = "";
                var pairedDevices = await PeerFinder.FindAllPeersAsync();
                if (pairedDevices.Count == 0)
                {
                    await new MessageDialog("没有找到相关的蓝牙设备").ShowAsync();
                }
                else
                {
                    await new MessageDialog("HostName:" + pairedDevices[0].HostName).ShowAsync();
                    streamSocket = new StreamSocket();
                    await streamSocket.ConnectAsync(pairedDevices[0].HostName, "1");

                }
            }
            catch (Exception ex)
            {
                if ((uint)ex.HResult == 0x8007048F)
                {
                    errMessage = "Bluetooth is turned off";
                }
                else
                {
                    errMessage = ex.Message;
                }
            }
            if (errMessage != "")
            {
                await new MessageDialog(errMessage).ShowAsync();
            }
        }
    }
}
