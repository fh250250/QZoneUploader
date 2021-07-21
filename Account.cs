using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace QZoneUploader
{
    public class Account : ObservableObject
    {
        public string Username { get; }
        public string Password { get; }

        private AccountStatus _status = AccountStatus.IDLE;
        public AccountStatus Status
        {
            get => _status;
            set => SetProperty(ref _status, value);
        }

        public Account(string username, string password)
        {
            Username = username;
            Password = password;
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
