using Newtonsoft.Json;
using SnapNPack.Helpers;
using SnapNPack.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SnapNPack.ViewModels
{
    public class MainPageVM : INotifyPropertyChanged
    {
        private ObservableCollection<ContainerModel> _Containers = new ObservableCollection<ContainerModel>();
        public ObservableCollection<ContainerModel> Containers
        {
            get { return _Containers; }
            set
            {
                _Containers = value;
                OnPropertyChanged(nameof(Containers));
            }
        }

        public MainPageVM()
        {
            Containers = new ObservableCollection<ContainerModel>();
            LoadItems();
        }

        public void LoadItems()
        {
            Task.Run(() =>
            {
                try
                {
                    var temp = JsonConvert.DeserializeObject<List<ContainerModel>>(Settings.ContainerSetting);

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        if (temp != null && temp.Count > 0)
                        {
                            foreach (ContainerModel model in temp)
                            {
                                Containers.Add(model);
                            }
                        }
                    });
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.StackTrace);
                }
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(String info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }
    }
}
