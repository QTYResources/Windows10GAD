using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.System.Threading;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace DelayLoadDemo
{
    public class Data : INotifyPropertyChanged
    {
        public string Name { get; set; }

        public Page Page { get; set; }

        private Uri imageUri;
        public Uri ImageUri
        {
            get
            {
                return imageUri;
            }
            set
            {
                if (imageUri == value)
                {
                    return;
                }
                imageUri = value;
                bitmapImage = null;
            }
        }
        WeakReference bitmapImage;

        public ImageSource ImageSource
        {

            get
            {
                if (bitmapImage != null)
                {
                    if (bitmapImage.IsAlive)
                        return (ImageSource)bitmapImage.Target;
                    else
                        Debug.WriteLine("数据已经被回收");
                }
                if (imageUri != null)
                {
                    Task.Factory.StartNew(() => { DownloadImage(imageUri); });
                }
                return null;
            }
        }

        async void DownloadImage(Uri uri)
        {

            List<Byte> allBytes = new List<byte>();
            Stream streamForUI;
            using (var response = await HttpWebRequest.Create(uri).GetResponseAsync())
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    byte[] buffer = new byte[4000];
                    int bytesRead = 0;
                    while ((bytesRead = await responseStream.ReadAsync(buffer, 0, 4000)) > 0)
                    {
                        allBytes.AddRange(buffer.Take(bytesRead));
                    }
                }
            }
            streamForUI = new MemoryStream((int)allBytes.Count);
            streamForUI.Write(allBytes.ToArray(), 0, allBytes.Count);
            streamForUI.Seek(0, SeekOrigin.Begin);


            await Page.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                BitmapImage bm = new BitmapImage();
                bm.SetSource(streamForUI.AsRandomAccessStream());

                if (bitmapImage == null)
                    bitmapImage = new WeakReference(bm);
                else
                    bitmapImage.Target = bm;
                //触发UI的改变
                OnPropertyChanged("ImageSource");
            });
        }

        async void OnPropertyChanged(string property)
        {
            var hander = PropertyChanged;
            if (Page == null) return;
            await Page.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                if (hander != null)
                    hander(this, new PropertyChangedEventArgs(property));
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
