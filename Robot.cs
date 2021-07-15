using System;
using System.Net;
using System.Threading.Tasks;
using PuppeteerSharp;

namespace QZoneUploader
{
    public class Robot
    {
        private static readonly BrowserFetcherOptions browserFetcherOptions = new BrowserFetcherOptions()
        {
            Path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ".local_browser"),
            Product = Product.Chrome,
            Host = "https://npm.taobao.org/mirrors",
        };

        public static bool IsBrowserDownloaded()
        {
            var browserFetcher = new BrowserFetcher(browserFetcherOptions);
            var revisionInfo = browserFetcher.RevisionInfo(BrowserFetcher.DefaultChromiumRevision);
            return revisionInfo.Local;
        }

        public static Task<RevisionInfo> DownloadBrowserAsync(DownloadProgressChangedEventHandler onProgress)
        {
            var browserFetcher = new BrowserFetcher(browserFetcherOptions);
            browserFetcher.DownloadProgressChanged += onProgress;
            return browserFetcher.DownloadAsync(BrowserFetcher.DefaultChromiumRevision);
        }
    }
}
