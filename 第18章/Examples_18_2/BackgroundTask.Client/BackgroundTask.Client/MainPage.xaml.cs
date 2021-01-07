using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Background;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace BackgroundTask.Client
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

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            var access = await BackgroundExecutionManager.RequestAccessAsync();
            if (access == BackgroundAccessStatus.Denied)
            {
                await new MessageDialog("后台任务已经被禁止了").ShowAsync();
            }
            else
            {
                RegisterBackgroundTask();
            }
        }

        private void RegisterBackgroundTask()
        {
            bool isRegistered = BackgroundTaskRegistration.AllTasks.Any(x => x.Value.Name == "Notification task");
            if (!isRegistered)
            {
                BackgroundTaskBuilder builder = new BackgroundTaskBuilder
                {
                    Name = "Notification task",
                    TaskEntryPoint =
                        "BackgroundTask.NotificationTask.NotificationTask"
                };
                MaintenanceTrigger trigger = new MaintenanceTrigger(15, false);
                builder.SetTrigger(trigger);
                BackgroundTaskRegistration task = builder.Register();
            }
        }
    }
}
