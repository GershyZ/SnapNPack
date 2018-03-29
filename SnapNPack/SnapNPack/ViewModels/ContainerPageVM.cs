using System;
using System.ComponentModel;

namespace SnapNPack.ViewModels
{
    public class ContainerPageVM : INotifyPropertyChanged
    {
        private string _ImageSrc = "";
        public string ImageSrc
        {
            get { return _ImageSrc; }
            set
            {
                _ImageSrc = value;
                NotifyPropertyChanged(nameof(ImageSrc));
            }
        }

        private string _Destination = "";
        public string Destination
        {
            get { return _Destination; }
            set
            {
                _Destination = value;
                NotifyPropertyChanged(nameof(Destination));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
