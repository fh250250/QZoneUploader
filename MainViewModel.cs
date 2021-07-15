using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace QZoneUploader
{
    public class MainViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<Account> _accounts;
        public ObservableCollection<Account> Accounts
        {
            get => _accounts;
            set
            {
                if (value != _accounts)
                {
                    _accounts = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private ObservableCollection<string> _texts;
        public ObservableCollection<string> Texts
        {
            get => _texts;
            set
            {
                if (value != _texts)
                {
                    _texts = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private ObservableCollection<string> _images;
        public ObservableCollection<string> Images
        {
            get => _images;
            set
            {
                if (value != _images)
                {
                    _images = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private bool _isDebug = false;
        public bool IsDebug
        {
            get => _isDebug;
            set
            {
                if (value != _isDebug)
                {
                    _isDebug = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }
}
