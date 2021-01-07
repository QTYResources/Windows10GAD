using Windows.ApplicationModel.Background;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace BackgroundTask.NotificationTask
{
    public sealed class NotificationTask : IBackgroundTask
    {
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            SendNotification("This is a toast notification");
        }

        private void SendNotification(string text)
        {
            XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText01);

            XmlNodeList elements = toastXml.GetElementsByTagName("text");
            foreach (IXmlNode node in elements)
            {
                node.InnerText = text;
            }

            ToastNotification notification = new ToastNotification(toastXml);
            ToastNotificationManager.CreateToastNotifier().Show(notification);
        }
    }
}
