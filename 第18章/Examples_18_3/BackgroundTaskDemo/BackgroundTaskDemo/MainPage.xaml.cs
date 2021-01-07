using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Background;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
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

namespace BackgroundTaskDemo
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


        private string _taskName = "MyTask";
        private string _taskEntryPoint = "MyBackgroundTask.MyTask";
        private bool _taskRegistered = false;
        private string _taskProgress = "";

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            foreach (KeyValuePair<Guid, IBackgroundTaskRegistration> task in BackgroundTaskRegistration.AllTasks)
            {
                if (task.Value.Name == _taskName)
                {
                    AttachProgressAndCompletedHandlers(task.Value);
                    _taskRegistered = true;
                    break;
                }
            }
            UpdateUI();
        }

        private async void btnRegister_Click_1(object sender, RoutedEventArgs e)
        {
            var access = await BackgroundExecutionManager.RequestAccessAsync();
            if (access == BackgroundAccessStatus.Denied)
            {
                await new MessageDialog("后台任务已经被禁止了").ShowAsync();
                return;
            }
            BackgroundTaskBuilder builder = new BackgroundTaskBuilder();
            builder.Name = _taskName;
            builder.TaskEntryPoint = _taskEntryPoint;
            builder.SetTrigger(new SystemTrigger(SystemTriggerType.TimeZoneChange, false));
            BackgroundTaskRegistration task = builder.Register();
            AttachProgressAndCompletedHandlers(task);
            _taskRegistered = true;
        }

        private void btnUnregister_Click_1(object sender, RoutedEventArgs e)
        {
            foreach (KeyValuePair<Guid, IBackgroundTaskRegistration> task in BackgroundTaskRegistration.AllTasks)
            {
                if (task.Value.Name == _taskName)
                {
                    task.Value.Unregister(true);
                    break;
                }
            }
            _taskRegistered = false;
        }

        private void AttachProgressAndCompletedHandlers(IBackgroundTaskRegistration task)
        {

            task.Progress += new BackgroundTaskProgressEventHandler(OnProgress);
            task.Completed += new BackgroundTaskCompletedEventHandler(OnCompleted);
        }

        private void OnProgress(IBackgroundTaskRegistration task, BackgroundTaskProgressEventArgs args)
        {
            _taskProgress = args.Progress.ToString() + "%";

            UpdateUI();
        }

        private void OnCompleted(IBackgroundTaskRegistration task, BackgroundTaskCompletedEventArgs args)
        {

            _taskProgress = "done";
            try
            {
                args.CheckResult();
            }
            catch (Exception ex)
            {
                _taskProgress = ex.ToString();
            }

            UpdateUI();
        }

        private async void UpdateUI()
        {

            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                btnRegister.IsEnabled = !_taskRegistered;
                btnUnregister.IsEnabled = _taskRegistered;
                if (_taskProgress != "")
                    progressInfo.Text = "Progress：" + _taskProgress;
                var settings = ApplicationData.Current.LocalSettings;
                if (settings.Values.ContainsKey("MyTask"))
                {
                    statusInfo.Text = "Status:" + settings.Values["MyTask"].ToString();
                }
                if (settings.Values.ContainsKey("BackgroundWorkCost"))
                {
                    workCostInfo.Text = "BackgroundWorkCost:" + settings.Values["BackgroundWorkCost"].ToString();
                }
            });
        }
    }

}
