using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Background;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace BackgroundTaskTestDemo
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

            bool taskRegistered = false;
            string exampleTaskName = "ExampleBackgroundTask";
            taskRegistered = BackgroundTaskRegistration.AllTasks.Any(x => x.Value.Name == exampleTaskName);

            if (!taskRegistered)
            {
                var access = await BackgroundExecutionManager.RequestAccessAsync();
                if (access == BackgroundAccessStatus.Denied)
                {
                    await new MessageDialog("后台任务已经被禁止了").ShowAsync();
                }
                else
                {
                    var builder = new BackgroundTaskBuilder();
                    builder.Name = exampleTaskName;
                    builder.TaskEntryPoint = "Tasks.ExampleBackgroundTask";
                    builder.SetTrigger(new SystemTrigger(SystemTriggerType.InternetAvailable, false));

                    builder.AddCondition(new SystemCondition(SystemConditionType.UserPresent));

                    BackgroundTaskRegistration task = builder.Register();
                    task.Completed += task_Completed;
                }

            }
            else
            {
                var cur = BackgroundTaskRegistration.AllTasks.FirstOrDefault(x => x.Value.Name == exampleTaskName);
                BackgroundTaskRegistration task = (BackgroundTaskRegistration)(cur.Value);
                task.Completed += task_Completed;
            }

        }

        async void task_Completed(BackgroundTaskRegistration sender, BackgroundTaskCompletedEventArgs args)
        {
            string text = "";
            IStorageFolder applicationFolder = ApplicationData.Current.LocalFolder;
            IStorageFile storageFile = await applicationFolder.GetFileAsync("test.txt");
            IRandomAccessStream accessStream = await storageFile.OpenReadAsync();
            using (StreamReader streamReader = new StreamReader(accessStream.AsStreamForRead((int)accessStream.Size)))
            {
                text = streamReader.ReadToEnd();
            }
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                info.Text = text;
            });
        }
    }
}
