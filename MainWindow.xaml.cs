using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;

namespace QZoneUploader
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public MainViewModel ViewModel => (MainViewModel)DataContext;

        private void BtnAccount_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog()
            {
                Filter = "文本文件(*.txt)|*.txt",
            };

            if (dlg.ShowDialog() != true) return;

            #region 解析账号数据
            ViewModel.Accounts.Clear();
            foreach (var line in File.ReadAllLines(dlg.FileName))
            {
                var str = line.Trim();

                if (string.IsNullOrEmpty(str)) continue;

                var data = str.Split("----");

                if (data.Length != 2) continue;

                ViewModel.Accounts.Add(new Account(data[0], data[1]));
            }
            #endregion
        }

        private void BtnText_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog()
            {
                Filter = "文本文件(*.txt)|*.txt",
            };

            if (dlg.ShowDialog() != true) return;

            #region 解析文本数据
            ViewModel.Texts.Clear();
            foreach (var line in File.ReadAllLines(dlg.FileName))
            {
                var str = line.Trim();

                if (string.IsNullOrEmpty(str)) continue;

                ViewModel.Texts.Add(str);
            }
            #endregion
        }

        private void BtnImage_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog()
            {
                Multiselect = true,
                Filter = "图像文件(*.png, *.jpg)|*.png;*.jpg",
            };

            if (dlg.ShowDialog() != true) return;

            ViewModel.Images.Clear();
            foreach (var filename in dlg.FileNames)
            {
                ViewModel.Images.Add(filename);
            }
        }

        private void BtnRun_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.Accounts.Count == 0)
            {
                MessageBox.Show("没有账号数据", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (ViewModel.Texts.Count == 0)
            {
                MessageBox.Show("没有文本数据", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (ViewModel.Images.Count == 0)
            {
                MessageBox.Show("没有图片数据", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Run();
        }

        private void BtnShowDetail_Click(object sender, RoutedEventArgs e)
        {
            if (LstAccounts.SelectedItem == null) return;

            Account account = (Account)LstAccounts.SelectedItem;

            MessageBox.Show(account.Username);
        }

        private async void Run()
        {
            TbLogs.Clear();
            ViewModel.IsRunning = true;

            foreach (var acc in ViewModel.Accounts)
            {
                var robot = new Robot(acc, ViewModel);
                robot.MessageComming += Robot_MessageComming;
                await robot.Run(ViewModel.IsDebug);
            }

            ViewModel.IsRunning = false;
        }

        private void Robot_MessageComming(object sender, string e)
        {
            var account = ((Robot)sender).Account;
            AppendLog($"[{account.Username}] {e}");
        }

        private void AppendLog(string msg)
        {
            TbLogs.AppendText(msg);
            TbLogs.AppendText(Environment.NewLine);
            TbLogs.ScrollToEnd();
        }
    }
}
