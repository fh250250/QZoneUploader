using System.Windows;

namespace QZoneUploader
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if (Robot.IsBrowserDownloaded())
            {
                new MainWindow().Show();
            }
            else
            {
                new DownloadWindow().Show();
            }
        }
    }
}
