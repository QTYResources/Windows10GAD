using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.PushNotifications;
using Windows.UI.Core;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace PushNotificationDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void bt_open_Click(object sender, RoutedEventArgs e)
        {
            PushNotificationChannel channel = await PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync();
            String uri = channel.Uri;
            Debug.WriteLine(uri);
            channel.PushNotificationReceived += channel_PushNotificationReceived;
        }

        async void channel_PushNotificationReceived(PushNotificationChannel sender, PushNotificationReceivedEventArgs args)
        {
            switch (args.NotificationType)
            {
                case PushNotificationType.Badge:
                    BadgeUpdateManager.CreateBadgeUpdaterForApplication().Update(args.BadgeNotification);
                    break;
                case PushNotificationType.Raw:
                    string msg = args.RawNotification.Content;
                    await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => info.Text = msg);
                    break;
                case PushNotificationType.Tile:
                    TileUpdateManager.CreateTileUpdaterForApplication().Update(args.TileNotification);
                    break;
                case PushNotificationType.Toast:
                    ToastNotificationManager.CreateToastNotifier().Show(args.ToastNotification);
                    break;
                default:
                    break;
            }
        }
    }
}
