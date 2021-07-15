using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace QZoneUploader
{
    public class Account
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _username;
        public string Username => _username;

        private string _password;
        public string Password => _password;

        private AccountStatus _status;
        public AccountStatus Status
        {
            get => _status;
            set
            {
                if (value != _status)
                {
                    _status = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Account(string username, string password)
        {
            _username = username;
            _password = password;
            _status = AccountStatus.IDLE;
        }
    }

    public enum AccountStatus
    {
        IDLE,
        RUNNING,
        SUCCESS,
        FAILURE,
    }
}
