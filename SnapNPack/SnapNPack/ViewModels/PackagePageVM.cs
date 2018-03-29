using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnapNPack.ViewModels
{
    public class PackagePageVM : INotifyPropertyChanged
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

        private string _Description = "";
        public string Description
        {
            get { return _Description; }
            set
            {
                _Description = value;
                NotifyPropertyChanged(nameof(Description));
            }
        }

        private bool _Fragile = false;
        public bool Fragile
        {
            get { return _Fragile; }
            set
            {
                _Fragile = value;
                NotifyPropertyChanged(nameof(Fragile));
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
