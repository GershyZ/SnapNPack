using Newtonsoft.Json;
using SnapNPack.Helpers;
using SnapNPack.Models;
using SnapNPack.ViewModels;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Media.Abstractions;

namespace SnapNPack.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddContainerPage : ContentPage
	{
        private ContainerPageVM _ViewModel;

		public AddContainerPage ()
		{
			InitializeComponent ();
            createBtn.IsVisible = false;
            //Set out viewmodel
            _ViewModel = new ContainerPageVM();
            BindingContext = _ViewModel;
		}

        private async void picBtn_Clicked(object sender, EventArgs e)
        {
            //https://montemagno.com/cross-platform-photos-with-media-plugin/
            if (!CrossMedia.IsSupported)
            {
                await DisplayAlert("CrossMedia not supported", "CrossMedia is not installed properly", "OK");
                return;
            }
            await CrossMedia.Current.Initialize();
            if(!(CrossMedia.Current.IsCameraAvailable && CrossMedia.Current.IsTakePhotoSupported))
            {
                var errmsg = (CrossMedia.Current.IsCameraAvailable ? "Taking pictures is not supported" : "Camera Unavailable");
                await DisplayAlert("Unable to take picture", errmsg, "OK");
                return;
            }
            //New random for our containers file name
            Random rand = new Random();

            var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                Directory = "SnapNPack",
                Name = "container" + rand.Next(1, 10000).ToString() + ".jpg"
            });

            var photo = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;
            });
            //_ViewModel.ImageSrc = photo;
            createBtn.IsVisible = true;
        }

        private async void createBtn_Clicked(object sender, EventArgs e)
        {
            Random rand = new Random();
            //Assemble our new container
            var temp = new ContainerModel();
            temp.Destination = _ViewModel.Destination;
            temp.ImageFile = _ViewModel.ImageSrc;
            temp.Id = rand.Next(0, 10000);

            try
            {
                //Get our list from json
                var list = JsonConvert.DeserializeObject<List<ContainerModel>>(Settings.ContainerSetting);

                //Add the new one
                list.Insert(0, temp);
                //Reserialize
                var json = JsonConvert.SerializeObject(list);
                Debug.WriteLine(json);
                //Save
                Settings.ContainerSetting = json;
                createBtn.IsVisible = false;    

            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }

            //Destroy the page and go back to previous.
            await Navigation.PopAsync();
        }
    }
}