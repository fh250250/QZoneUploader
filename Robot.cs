using System;
using System.Net;
using System.Threading.Tasks;
using PuppeteerSharp;

namespace QZoneUploader
{
    public class Robot
    {
        public Account Account { get; }
        private MainViewModel MainViewModel { get; }
        private Browser browser;
        private Page page;

        public event EventHandler<string> MessageComming;
        protected virtual void OnMessage(string message, bool isPublic = true)
        {
            Account.AppendLog(message);
            
            if (isPublic)
            {
                MessageComming?.Invoke(this, message);
            }
        }

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

        public Robot(Account account, MainViewModel mainViewModel)
        {
            Account = account;
            MainViewModel = mainViewModel;
        }

        public async Task Run(bool isDebug)
        {
            var revisionInfo = new BrowserFetcher(browserFetcherOptions).RevisionInfo(BrowserFetcher.DefaultChromiumRevision);
            var launchOptions = new LaunchOptions()
            {
                Headless = !isDebug,
                ExecutablePath = revisionInfo.ExecutablePath,
                DefaultViewport = new ViewPortOptions()
                {
                    Width = 1280,
                    Height = 768,
                }
            };

            OnMessage("启动浏览器");

            browser = await Puppeteer.LaunchAsync(launchOptions);
            page = await browser.NewPageAsync();

            OnMessage("开始执行");
            Account.Status = AccountStatus.RUNNING;

            try
            {
                await Login();
                await UploadImage();

                OnMessage("处理完成");
                Account.Status = AccountStatus.SUCCESS;
            }
            catch (Exception e)
            {
                OnMessage("失败");
                OnMessage(e.Message, false);
                Account.Status = AccountStatus.FAILURE;
                Account.Screenshot = await page.ScreenshotDataAsync();
            }
            finally
            {
                await browser.CloseAsync();
            }
        }

        private async Task Login()
        {
            OnMessage("登录");

            _ = await page.GoToAsync("https://i.qq.com");
            
            var loginFrame = await page.WaitForFrameAsync("login_frame");

            await (await loginFrame.WaitForSelectorAsync("#switcher_plogin")).ClickAsync();
            await (await loginFrame.WaitForSelectorAsync("#u")).TypeAsync(Account.Username);
            await (await loginFrame.WaitForSelectorAsync("#p")).TypeAsync(Account.Password);
            await (await loginFrame.WaitForSelectorAsync("#login_button")).ClickAsync();

            _ = await page.WaitForNavigationAsync();
        }

        private async Task UploadImage()
        {
            OnMessage("点击相册");
            await (await page.WaitForSelectorAsync("a[title=\"相册\"]")).ClickAsync();

            var tphotoFrame = await page.WaitForFrameAsync("tphoto", "app_canvas_frame");

            OnMessage("打开上传对话框");
            await (await tphotoFrame.WaitForSelectorAsync(".j-uploadentry-photo")).ClickAsync();
            var uploadFrame = await page.WaitForFrameAsync("photoUploadDialog");

            OnMessage($"延时{MainViewModel.DelaySec}s");
            await page.WaitForTimeoutAsync((int)MainViewModel.DelaySec);

            var imagePath = MainViewModel.RandomImage;
            OnMessage($"选择随机文件 {imagePath}");
            var fileChooserTask = page.WaitForFileChooserAsync();
            await Task.WhenAll(fileChooserTask, uploadFrame.ClickAsync(".btn-select-img"));
            await fileChooserTask.Result.AcceptAsync(imagePath.Replace(@"\", @"\\"));

            OnMessage("上传文件");
            await (await uploadFrame.WaitForSelectorAsync(".op-btn.btn-upload")).ClickAsync();
            _ = await page.WaitForSelectorAsync("#photoUploadDialog", new WaitForSelectorOptions() { Hidden = true });
            _ = await tphotoFrame.WaitForSelectorAsync("#desc_all");

            OnMessage($"延时{MainViewModel.DelaySec}s");
            await page.WaitForTimeoutAsync((int)MainViewModel.DelaySec);

            OnMessage("填写随机文本");
            await tphotoFrame.TypeAsync("#desc_all", MainViewModel.RandomText);

            OnMessage($"延时{MainViewModel.DelaySec}s");
            await page.WaitForTimeoutAsync((int)MainViewModel.DelaySec);

            await (await tphotoFrame.WaitForSelectorAsync("#back_btn_md")).ClickAsync();
            _ = await tphotoFrame.WaitForNavigationAsync();
        }
    }
}
