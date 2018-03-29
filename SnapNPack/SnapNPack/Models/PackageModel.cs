using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SnapNPack.Models
{
    //Same thing as ContainerModel.cs but for packages
    public class PackageModel : INotifyPropertyChanged
    {
        private string _Description = "";
        public string Description
        {
            get { return _Description; }
            set
            {
                _Description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public string DescAndFrag
        {
            get
            {
                if(Fragile)
                {
                    return Description + " | Fragile";
                }
                else
                {
                    return Description;
                }
            }
        }

        private bool _Fragile = false;
        public bool Fragile
        {
            get { return _Fragile; }
            set
            {
                _Fragile = value;
                OnPropertyChanged(nameof(_Fragile));
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
