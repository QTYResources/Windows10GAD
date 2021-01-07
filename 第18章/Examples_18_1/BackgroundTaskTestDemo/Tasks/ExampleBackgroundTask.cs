using System;
using Windows.ApplicationModel.Background;
using Windows.Storage;

namespace Tasks
{
    public sealed class ExampleBackgroundTask : IBackgroundTask
    {
        public async void Run(IBackgroundTaskInstance taskInstance)
        {

            BackgroundTaskDeferral deferral = taskInstance.GetDeferral();

            IStorageFolder applicationFolder = ApplicationData.Current.LocalFolder;
            IStorageFile storageFile = await applicationFolder.CreateFileAsync("test.txt", CreationCollisionOption.OpenIfExists);
            await FileIO.WriteTextAsync(storageFile, DateTime.Now.ToString());

            deferral.Complete();
        }
    }
}