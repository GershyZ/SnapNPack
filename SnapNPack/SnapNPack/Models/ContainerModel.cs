using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace SnapNPack.Models
{
    //Our container model for storing data about our containers, It implements the INotifyPropertyChanged interface
    //So it notifies the ui when properties are changed. More about that here https://developer.xamarin.com/guides/xamarin-forms/xaml/xaml-basics/data_bindings_to_mvvm/
    public class ContainerModel : INotifyPropertyChanged
    {
        private string _Destination = "";
        public string Destination
        {
            get { return _Destination; }
            set
            {
                _Destination = value;
                OnPropertyChanged(nameof(Destination));
            }
        }

        private LinkedList<PackageModel> _Packages = new LinkedList<PackageModel>();
        public LinkedList<PackageModel> Packages
        {
            get { return _Packages; }
            set
            {
                _Packages = value;
                OnPropertyChanged(nameof(_Packages));
            }
        }

        private string _ImageFile = "";
        public string ImageFile
        {
            get { return _ImageFile; }
            set
            {
                _ImageFile = value;
                OnPropertyChanged(nameof(ImageFile));
            }
        }

        public int Id { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(String info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }
    }
}
