using Microsoft.Win32;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;

namespace QZoneUploader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel vm = new MainViewModel();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = vm;
        }

        private void btnAccount_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog()
            {
                Filter = "文本文件(*.txt)|*.txt",
            };

            if (dlg.ShowDialog() != true) return;

            #region 解析账号数据
            vm.Accounts.Clear();
            foreach (var line in File.ReadAllLines(dlg.FileName))
            {
                var str = line.Trim();

                if (string.IsNullOrEmpty(str)) continue;

                var data = str.Split("----");

                if (data.Length != 2) continue;

                vm.Accounts.Add(new Account(data[0], data[1]));
            }
            #endregion
        }

        private void btnText_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog()
            {
                Filter = "文本文件(*.txt)|*.txt",
            };

            if (dlg.ShowDialog() != true) return;

            #region 解析文本数据
            vm.Texts.Clear();
            foreach (var line in File.ReadAllLines(dlg.FileName))
            {
                var str = line.Trim();

                if (string.IsNullOrEmpty(str)) continue;

                vm.Texts.Add(str);
            }
            #endregion
        }

        private void btnImage_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog()
            {
                Multiselect = true,
                Filter = "图像文件(*.png, *.jpg)|*.png;*.jpg",
            };

            if (dlg.ShowDialog() != true) return;

            vm.Images.Clear();
            foreach (var filename in dlg.FileNames)
            {
                vm.Images.Add(filename);
            }
        }

        private void btnRun_Click(object sender, RoutedEventArgs e)
        {
            if (vm.Accounts.Count == 0)
            {
                MessageBox.Show("没有账号数据", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (vm.Texts.Count == 0)
            {
                MessageBox.Show("没有文本数据", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (vm.Images.Count == 0)
            {
                MessageBox.Show("没有图片数据", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
        }
    }
}
