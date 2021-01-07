using System.Net;
using System.Text.RegularExpressions;

namespace ReadRssItemsSample
{

    public class RssItem
    {

        public RssItem(string title, string summary, string publishedDate, string url)
        {
            Title = title;
            Summary = summary;
            PublishedDate = publishedDate;
            Url = url;
            PlainSummary = WebUtility.HtmlDecode(Regex.Replace(summary, "<[^>]+?>", ""));
        }

        public string Title { get; set; }

        public string Summary { get; set; }

        public string PublishedDate { get; set; }

        public string Url { get; set; }

        public string PlainSummary { get; set; }
    }
}
