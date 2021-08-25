using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.ObjectModel;

namespace QZoneUploader
{
    public class MainViewModel : ObservableObject
    {
        private ObservableCollection<Account> _accounts = new ObservableCollection<Account>();
        public ObservableCollection<Account> Accounts
        {
            get => _accounts;
            set => SetProperty(ref _accounts, value);
        }

        private ObservableCollection<string> _texts = new ObservableCollection<string>();
        public ObservableCollection<string> Texts
        {
            get => _texts;
            set => SetProperty(ref _texts, value);
        }

        private ObservableCollection<string> _images = new ObservableCollection<string>();
        public ObservableCollection<string> Images
        {
            get => _images;
            set => SetProperty(ref _images, value);
        }

        private bool _isDebug = false;
        public bool IsDebug
        {
            get => _isDebug;
            set => SetProperty(ref _isDebug, value);
        }

        private bool _isRunning = false;
        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public uint DelaySec
        {
            get => Properties.Settings.Default.RunDelaySec;
            set
            {
                OnPropertyChanging();
                Properties.Settings.Default.RunDelaySec = value;
                Properties.Settings.Default.Save();
                OnPropertyChanged();
            }
        }

        public string RandomImage => Images[new Random().Next(0, Images.Count)];
        public string RandomText => Texts[new Random().Next(0, Texts.Count)];
    }
}
