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
            MessageBox.Show("btnAccount_Click");
        }

        private void btnText_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnImage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRun_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
