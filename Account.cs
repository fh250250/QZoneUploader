using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

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

        public static string StatusToString(AccountStatus status)
        {
            return status switch
            {
                AccountStatus.IDLE => "等待中",
                AccountStatus.RUNNING => "运行中",
                AccountStatus.SUCCESS => "成功",
                AccountStatus.FAILURE => "失败",
                _ => "",
            };
        }
    }

    public enum AccountStatus
    {
        IDLE,
        RUNNING,
        SUCCESS,
        FAILURE,
    }

    [ValueConversion(typeof(AccountStatus), typeof(string))]
    public class AccountStatusToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Account.StatusToString((AccountStatus)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
