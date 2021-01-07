using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Windows.Web.Syndication;

namespace ReadRssItemsSample
{

    public static class RssService
    {

        public static void GetRssItems(string rssFeed, Action<IEnumerable<RssItem>> onGetRssItemsCompleted = null, Action<string> onError = null, Action onFinally = null)
        {
            var request = HttpWebRequest.Create(rssFeed);
            request.Method = "GET";

            request.BeginGetResponse((result) =>
            {
                try
                {
                    HttpWebRequest httpWebRequest = (HttpWebRequest)result.AsyncState;
                    WebResponse webResponse = httpWebRequest.EndGetResponse(result);
                    using (Stream stream = webResponse.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            string content = reader.ReadToEnd();
                            List<RssItem> rssItems = new List<RssItem>();
                            SyndicationFeed feeds = new SyndicationFeed();
                            feeds.Load(content);
                            foreach (SyndicationItem f in feeds.Items)
                            {
                                RssItem rssItem = new RssItem(f.Title.Text, f.Summary.Text, f.PublishedDate.ToString(), f.Links[0].Uri.AbsoluteUri);
                                rssItems.Add(rssItem);
                            }
                            if (onGetRssItemsCompleted != null)
                            {
                                onGetRssItemsCompleted(rssItems);
                            }
                        }

                    }
                }
                catch (WebException webEx)
                {
                    string exceptionInfo = "";
                    switch (webEx.Status)
                    {
                        case WebExceptionStatus.ConnectFailure:
                            exceptionInfo = "ConnectFailure:远程服务器连接失败。";
                            break;
                        case WebExceptionStatus.MessageLengthLimitExceeded:
                            exceptionInfo = "MessageLengthLimitExceeded:网路请求的消息长度受到限制。";
                            break;
                        case WebExceptionStatus.Pending:
                            exceptionInfo = "Pending:内部异步请求挂起。";
                            break;
                        case WebExceptionStatus.RequestCanceled:
                            exceptionInfo = "RequestCanceled:该请求将被取消。";
                            break;
                        case WebExceptionStatus.SendFailure:
                            exceptionInfo = "SendFailure:发送失败，未能将完整请求发送到远程服务器。";
                            break;
                        case WebExceptionStatus.UnknownError:
                            exceptionInfo = "UnknownError:未知错误。";
                            break;
                        case WebExceptionStatus.Success:
                            exceptionInfo = "Success:请求成功。";
                            break;
                        default:
                            exceptionInfo = "未知网络异常。";
                            break;
                    }
                    if (onError != null)
                    {
                        onError(exceptionInfo);
                    }
                }
                catch (Exception e)
                {
                    if (onError != null)
                    {
                        onError("异常：" + e.Message);
                    }
                }
                finally
                {
                    if (onFinally != null)
                    {
                        onFinally();
                    }
                }
            }, request);

        }
    }
}
