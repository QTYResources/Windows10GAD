using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Cryptography;
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
using Windows.Web.Http;
using Windows.Web.Http.Filters;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace HttpClientDemo
{
    public sealed partial class MainPage : Page
    {

        private string server = "http://localhost/default.aspx";
        private HttpClient httpClient;
        private CancellationTokenSource cts;
        public MainPage()
        {
            this.InitializeComponent();
            httpClient = new HttpClient();
            cts = new CancellationTokenSource();
        }

        private async void HttpRequestAsync(Func<Task<string>> httpRequestFuncAsync)
        {
            string responseBody;
            waiting.Visibility = Visibility.Visible;
            try
            {
                responseBody = await httpRequestFuncAsync();
                cts.Token.ThrowIfCancellationRequested();
            }
            catch (TaskCanceledException)
            {
                responseBody = "请求被取消";
            }
            catch (Exception ex)
            {
                responseBody = "异常消息" + ex.Message;
            }
            finally
            {
                waiting.Visibility = Visibility.Collapsed;
            }
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
            {
                await new MessageDialog(responseBody).ShowAsync();
            });
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            HttpRequestAsync(async () =>
            {
                string resourceAddress = server + "?cacheable=1";
                HttpResponseMessage response = await httpClient.GetAsync(new Uri(resourceAddress)).AsTask(cts.Token);
                string responseBody = await response.Content.ReadAsStringAsync().AsTask(cts.Token);
                return responseBody;
            });

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            HttpRequestAsync(async () =>
            {
                string resourceAddress = server + "?extraData=2000";
                StringBuilder responseBody = new StringBuilder();
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, new Uri(resourceAddress));
                HttpResponseMessage response = await httpClient.SendRequestAsync(
                    request,
                    HttpCompletionOption.ResponseHeadersRead).AsTask(cts.Token);

                using (Stream responseStream = (await response.Content.ReadAsInputStreamAsync()).AsStreamForRead())
                {
                    int read = 0;
                    byte[] responseBytes = new byte[1000];
                    do
                    {
                        read = await responseStream.ReadAsync(responseBytes, 0, responseBytes.Length);
                        responseBody.AppendFormat("Bytes read from stream: {0}", read);
                        responseBody.AppendLine();
                        IBuffer responseBuffer = CryptographicBuffer.CreateFromByteArray(responseBytes);
                        responseBuffer.Length = (uint)read;
                        responseBody.AppendFormat(CryptographicBuffer.EncodeToHexString(responseBuffer));
                        responseBody.AppendLine();
                    } while (read != 0);
                }
                return responseBody.ToString();
            });
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            HttpRequestAsync(async () =>
            {
                string resourceAddress = server;
                string responseBody;
                HttpResponseMessage response = await httpClient.PostAsync(new Uri(resourceAddress),
                    new HttpStringContent("hello Windows Phone")).AsTask(cts.Token);
                responseBody = await response.Content.ReadAsStringAsync().AsTask(cts.Token);
                return responseBody;
            });
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            HttpRequestAsync(async () =>
            {
                string resourceAddress = server;
                string responseBody;
                const int contentLength = 1000;
                Stream stream = GenerateSampleStream(contentLength);
                HttpStreamContent streamContent = new HttpStreamContent(stream.AsInputStream());
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, new Uri(resourceAddress));
                request.Content = streamContent;
                HttpResponseMessage response = await httpClient.SendRequestAsync(request).AsTask(cts.Token);
                responseBody = await response.Content.ReadAsStringAsync().AsTask(cts.Token);
                return responseBody;
            });
        }

        private static MemoryStream GenerateSampleStream(int size)
        {
            byte[] subData = new byte[size];
            for (int i = 0; i < subData.Length; i++)
            {
                subData[i] = 40;
            }
            return new MemoryStream(subData);
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            HttpRequestAsync(async () =>
            {
                string resourceAddress = server;
                string responseBody;
                const uint streamLength = 1000000;
                HttpStreamContent streamContent = new HttpStreamContent(new SlowInputStream(streamLength));
                streamContent.Headers.ContentLength = streamLength;
                IProgress<HttpProgress> progress = new Progress<HttpProgress>(ProgressHandler);
                HttpResponseMessage response = await httpClient.PostAsync(new Uri(resourceAddress), streamContent).AsTask(cts.Token, progress);
                responseBody = "完成";
                return responseBody;
            });
        }
        private void ProgressHandler(HttpProgress progress)
        {
            string infoString = "";
            infoString = progress.Stage.ToString();
            ulong totalBytesToSend = 0;
            if (progress.TotalBytesToSend.HasValue)
            {
                totalBytesToSend = progress.TotalBytesToSend.Value;
                infoString += "发送数据:" + totalBytesToSend.ToString(CultureInfo.InvariantCulture);
            }
            ulong totalBytesToReceive = 0;
            if (progress.TotalBytesToReceive.HasValue)
            {
                totalBytesToReceive = progress.TotalBytesToReceive.Value;
                infoString += "接收数据:" + totalBytesToReceive.ToString(CultureInfo.InvariantCulture);
            }
            double requestProgress = 0;
            if (progress.Stage == HttpProgressStage.SendingContent && totalBytesToSend > 0)
            {
                requestProgress = progress.BytesSent * 50 / totalBytesToSend;
                infoString += "发送进度：";
            }
            else if (progress.Stage == HttpProgressStage.ReceivingContent)
            {
                requestProgress += 50;
                if (totalBytesToReceive > 0)
                {
                    requestProgress += progress.BytesReceived * 50 / totalBytesToReceive;
                }
                infoString += "接收进度：";
            }
            else
            {
                return;
            }
            infoString += requestProgress;
            infomation.Text = infoString;
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            HttpRequestAsync(async () =>
            {
                string resourceAddress = server;
                string responseBody;
                HttpCookie cookie = new HttpCookie("id", "localhost", "/");
                cookie.Value = "123456";
                cookie.Expires = null;
                HttpBaseProtocolFilter filter = new HttpBaseProtocolFilter();
                bool replaced = filter.CookieManager.SetCookie(cookie, false);
                HttpResponseMessage response = await httpClient.PostAsync(new Uri(resourceAddress),
                   new HttpStringContent("hello Windows Phone")).AsTask(cts.Token);
                responseBody = await response.Content.ReadAsStringAsync().AsTask(cts.Token);
                return responseBody;
            });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            HttpRequestAsync(async () =>
            {
                string resourceAddress = server + "?setCookies=1";
                string responseBody;
                HttpResponseMessage response = await httpClient.GetAsync(new Uri(resourceAddress)).AsTask(cts.Token);
                responseBody = await response.Content.ReadAsStringAsync().AsTask(cts.Token);
                cts.Token.ThrowIfCancellationRequested();
                HttpBaseProtocolFilter filter = new HttpBaseProtocolFilter();
                HttpCookieCollection cookieCollection = filter.CookieManager.GetCookies(new Uri(resourceAddress));
                responseBody = cookieCollection.Count + " cookies :";
                foreach (HttpCookie cookie in cookieCollection)
                {
                    responseBody += "Name: " + cookie.Name + " ";
                    responseBody += "Domain: " + cookie.Domain + " ";
                    responseBody += "Path: " + cookie.Path + " ";
                    responseBody += "Value: " + cookie.Value + " ";
                    responseBody += "Expires: " + cookie.Expires + " ";
                    responseBody += "Secure: " + cookie.Secure + " ";
                    responseBody += "HttpOnly: " + cookie.HttpOnly + " ";
                }
                return responseBody;
            });
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            cts.Cancel();
            cts.Dispose();
            cts = new CancellationTokenSource();
        }
    }
}
