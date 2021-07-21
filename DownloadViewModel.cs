using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace QZoneUploader
{
    public class DownloadViewModel : ObservableObject
    {
        private int _progress = 0;
        public int Progress
        {
            get => _progress;
            set => SetProperty(ref _progress, value);
        }
    }
}
