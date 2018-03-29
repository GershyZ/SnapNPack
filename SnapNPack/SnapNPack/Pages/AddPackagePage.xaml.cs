using Newtonsoft.Json;
using SnapNPack.Helpers;
using SnapNPack.Models;
using SnapNPack.ViewModels;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SnapNPack.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddPackagePage : ContentPage
	{
        int _ContainerId;
        PackagePageVM _ViewModel;

		public AddPackagePage (int containerid)
		{
			InitializeComponent ();
            createBtn.IsVisible = false;           
            //Save the container id
            _ContainerId = containerid;
            _ViewModel = new PackagePageVM();
            BindingContext = _ViewModel;
		}

        private async void picBtn_Clicked(object sender, EventArgs e)
        {
            //Take photo. Same as in AddContainerPage
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                return;
            }

            Random rand = new Random();

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "SnapNPack",
                Name = "package" + rand.Next(1, 10000).ToString() + ".jpg"
            });

            if (file == null)
                return;
            createBtn.IsVisible = true;
            _ViewModel.ImageSrc = file.Path;
        }

        private async void createBtn_Clicked(object sender, EventArgs e)
        {
            var rand = new Random();

            //Assemble our package
            var temp = new PackageModel();
            temp.Description = _ViewModel.Description;
            temp.Fragile = _ViewModel.Fragile;
            temp.ImageFile = _ViewModel.ImageSrc;
            temp.Id = rand.Next(0, 10000);

            try
            {
                //Get our list from json
                var list = JsonConvert.DeserializeObject<List<ContainerModel>>(Settings.ContainerSetting);

                foreach (var c in list)
                {
                    //find out container
                    if (c.Id == _ContainerId)
                    {
                        //add the packages to the container
                        c.Packages.AddFirst(temp);                        
                        createBtn.IsVisible = false;
                    }
                }
                //save the modified list
                var json = JsonConvert.SerializeObject(list);
                Settings.ContainerSetting = json;
                createBtn.IsVisible = false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }

            //Destroy and go back to previous 
            await Navigation.PopAsync();
        }
    }
}