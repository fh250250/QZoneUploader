using System;
using System.Net;
using System.Threading.Tasks;
using System.Windows;

namespace QZoneUploader
{
    /// <summary>
    /// DownloadWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DownloadWindow : Window
    {
        private DownloadViewModel vm = new DownloadViewModel();

        public DownloadWindow()
        {
            InitializeComponent();
            DataContext = vm;
        }

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
            MessageBox.Show("下载失败");
            Close();
        }

        private void DownloadBrowser_Success()
        {
            new MainWindow().Show();
            Close();
        }

        private void DownloadBrowser_Progress(object sender, DownloadProgressChangedEventArgs e)
        {
            vm.Progress = e.ProgressPercentage;
        }
    }
}
