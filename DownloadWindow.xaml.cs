using System.Net;
using System.Threading.Tasks;
using System.Windows;

namespace QZoneUploader
{
    public partial class DownloadWindow : Window
    {
        public DownloadWindow()
        {
            InitializeComponent();
        }

        public DownloadViewModel ViewModel => (DownloadViewModel)DataContext;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _ = DownloadAsync();
        }

        private async Task DownloadAsync()
        {
            try
            {
                _ = await Robot.DownloadBrowserAsync(DownloadBrowser_Progress);
                DownloadBrowser_Success();
            }
            catch
            {
                DownloadBrowser_Failure();
            }
        }

        private void DownloadBrowser_Failure()
        {
            _ = MessageBox.Show("下载失败");
            Close();
        }

        private void DownloadBrowser_Success()
        {
            new MainWindow().Show();
            Close();
        }

        private void DownloadBrowser_Progress(object sender, DownloadProgressChangedEventArgs e)
        {
            ViewModel.Progress = e.ProgressPercentage;
        }
    }
}
