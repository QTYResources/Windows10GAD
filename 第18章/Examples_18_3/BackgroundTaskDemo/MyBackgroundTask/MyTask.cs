using System;
using System.Diagnostics;
using Windows.ApplicationModel.Background;
using Windows.Storage;
using Windows.System.Threading;

namespace MyBackgroundTask
{
    public sealed class MyTask : IBackgroundTask
    {
        BackgroundTaskCancellationReason _cancelReason = BackgroundTaskCancellationReason.Abort;
        volatile bool _cancelRequested = false;
        BackgroundTaskDeferral _deferral = null;
        ThreadPoolTimer _periodicTimer = null;
        uint _progress = 0;
        IBackgroundTaskInstance _taskInstance = null;
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            Debug.WriteLine("Background " + taskInstance.Task.Name + " Starting...");
            var cost = BackgroundWorkCost.CurrentBackgroundWorkCost;
            var settings = ApplicationData.Current.LocalSettings;
            settings.Values["BackgroundWorkCost"] = cost.ToString();
            taskInstance.Canceled += new BackgroundTaskCanceledEventHandler(OnCanceled);
            _deferral = taskInstance.GetDeferral();
            _taskInstance = taskInstance;
            _periodicTimer = ThreadPoolTimer.CreatePeriodicTimer(new TimerElapsedHandler(PeriodicTimerCallback), TimeSpan.FromSeconds(1));
        }

        private void OnCanceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {
            _cancelRequested = true;
            _cancelReason = reason;
            Debug.WriteLine("Background " + sender.Task.Name + " Cancel Requested...");
        }

        private void PeriodicTimerCallback(ThreadPoolTimer timer)
        {
            if ((_cancelRequested == false) && (_progress < 100))
            {
                _progress += 10;
                _taskInstance.Progress = _progress;
            }
            else
            {

                _periodicTimer.Cancel();

                var settings = ApplicationData.Current.LocalSettings;
                var key = _taskInstance.Task.Name;
                settings.Values[key] = (_progress < 100) ? "Canceled with reason: " + _cancelReason.ToString() : "Completed";
                Debug.WriteLine("Background " + _taskInstance.Task.Name + settings.Values[key]);
                _deferral.Complete();
            }
        }
    }
}
