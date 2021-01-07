using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Devices.Geolocation;
using Windows.Storage;

namespace BackgroundTask
{
    public sealed class LocationBackgroundTask : IBackgroundTask
    {
        CancellationTokenSource cts = null;

        async void IBackgroundTask.Run(IBackgroundTaskInstance taskInstance)
        {
            BackgroundTaskDeferral deferral = taskInstance.GetDeferral();
            try
            {
                taskInstance.Canceled += new BackgroundTaskCanceledEventHandler(OnCanceled);
                if (cts == null)
                {
                    cts = new CancellationTokenSource();
                }
                CancellationToken token = cts.Token;
                Geolocator geolocator = new Geolocator();
                Geoposition pos = await geolocator.GetGeopositionAsync().AsTask(token);
                DateTime currentTime = DateTime.Now;
                WriteStatusToAppdata("Time: " + currentTime.ToString());
                WriteGeolocToAppdata(pos);
            }
            catch (UnauthorizedAccessException)
            {
                WriteStatusToAppdata("Disabled");
                WipeGeolocDataFromAppdata();
            }
            catch (Exception ex)
            {
                // 超时异常
                const int WaitTimeoutHResult = unchecked((int)0x80070102);

                if (ex.HResult == WaitTimeoutHResult)
                {
                    WriteStatusToAppdata("An operation requiring location sensors timed out. Possibly there are no location sensors.");
                }
                else
                {
                    WriteStatusToAppdata(ex.ToString());
                }

                WipeGeolocDataFromAppdata();
            }
            finally
            {
                cts = null;
                deferral.Complete();
            }
        }

        private void WriteGeolocToAppdata(Geoposition pos)
        {
            var settings = ApplicationData.Current.LocalSettings;
            settings.Values["Latitude"] = pos.Coordinate.Point.Position.Latitude.ToString();
            settings.Values["Longitude"] = pos.Coordinate.Point.Position.Longitude.ToString();
            settings.Values["Accuracy"] = pos.Coordinate.Accuracy.ToString();
        }

        private void WipeGeolocDataFromAppdata()
        {
            var settings = ApplicationData.Current.LocalSettings;
            settings.Values["Latitude"] = "";
            settings.Values["Longitude"] = "";
            settings.Values["Accuracy"] = "";
        }

        private void WriteStatusToAppdata(string status)
        {
            var settings = ApplicationData.Current.LocalSettings;
            settings.Values["Status"] = status;
        }

        private void OnCanceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {
            if (cts != null)
            {
                cts.Cancel();
                cts = null;
            }
        }
    }
}
