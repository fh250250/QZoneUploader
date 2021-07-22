using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Collections.Generic;

namespace QZoneUploader
{
    public class Account : ObservableObject
    {
        public string Username { get; }
        public string Password { get; }
        public List<string> Logs { get; } = new List<string>();
        public byte[] Screenshot { get; set; }

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

        public void AppendLog(string log)
        {
            Logs.Add(log);
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
