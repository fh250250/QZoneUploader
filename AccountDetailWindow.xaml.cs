using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace QZoneUploader
{
    public partial class AccountDetailWindow : Window
    {
        public AccountDetailWindow()
        {
            InitializeComponent();
        }

        public void LoadData(Account account)
        {
            Title += $" [{account.Username}] - {Account.StatusToString(account.Status)}";

            account.Logs.ForEach(log => TboxLogs.AppendText(log + Environment.NewLine));

            if (account.Screenshot != null)
            {
                var bmp = new BitmapImage();

                bmp.BeginInit();
                bmp.StreamSource = new MemoryStream(account.Screenshot);
                bmp.EndInit();

                ImgScreenshot.Source = bmp;
            }
        }
    }
}
