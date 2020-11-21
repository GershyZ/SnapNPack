using Newtonsoft.Json;
using SnapNPack.Helpers;
using SnapNPack.Models;
using SnapNPack.Pages;
using SnapNPack.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;

namespace SnapNPack
{
    public partial class MainPage : ContentPage
    {
        String no_containers = "Create a container first with the icon with the green plus";
        MainPageVM _ViewModel;

        public MainPage()
        {
            InitializeComponent();
            //Set the main page viewmodel . Defined in ViewModels/MainPageVM
            _ViewModel = new MainPageVM();
            BindingContext = _ViewModel;
        }

        private async void addContainer_Clicked(object sender, EventArgs e)
        {
            //Show the new container page
            await Navigation.PushAsync(new AddContainerPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //Load the items when the page appears
            _ViewModel.LoadItems();
        }

        #region unused
        private void removeContainer_Clicked(object sender, EventArgs e)
        {
            //Load the json string we have saved of our containers
            var temp = JsonConvert.DeserializeObject<List<ContainerModel>>(Settings.ContainerSetting);

            try
            {
                //Try to cast the selected container item in the container carousel to a container model
                var item = CarouselContainers.CurrentItem as ContainerModel;

                Debug.WriteLine(item.Id);

                foreach (var c in temp)
                {
                    //Loop through all containers until we find the container with same id as the selected one
                    if (c.Id == item.Id)
                    {
                        Debug.WriteLine("Item ID: " + c.Id);
                        Debug.WriteLine("Wanted Item ID: " + item.Id);
                        //Remove it
                        temp.Remove(c);
                        break;
                    }
                }

                //Serialize the modefied list into json and overwrite the setting
                var json = JsonConvert.SerializeObject(temp);
                Debug.WriteLine(json);
                Settings.ContainerSetting = json;

                //Reload items
                _ViewModel.LoadItems();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
        }
        #endregion
        private async void addPackage_Clicked(object sender, EventArgs e)
        {
            //Make sure we actually have a container
            if (CarouselContainers.ItemsSource.OfType<ContainerModel>().Count() > 0)
            {
                //get the container reference
                var container = CarouselContainers.CurrentItem as ContainerModel;
                //open add package page
                await Navigation.PushAsync(new AddPackagePage(container.Id));
            }
        }


        private async void removePackage_Clicked(object sender, EventArgs e)
        {
            //same here. retrive our list from json
            var temp = JsonConvert.DeserializeObject<List<ContainerModel>>(Settings.ContainerSetting);
            if (temp == null || temp.Count == 0)
            {
                await DisplayAlert("Unable to unpack", no_containers, "OK");
                return;
            }
            try
            {
                //cast the current container to containermodel
                var container = CarouselContainers.CurrentItem as ContainerModel;
                //cast the current package to packagemodel
                if (container.Packages.Count == 0)
                {
                    await DisplayAlert("Unable to unpack", "This container is empty. Press the button with the green arrow pointing in to add stuff in here", "OK");
                    return;
                }
                var package = CarouselPackages.CurrentItem as PackageModel;
                Debug.WriteLine("Action: Delete Package: " + package.Id);
                var found = false;
                foreach (var c in temp)
                {
                    //loop through containers
                    if (c.Id == container.Id)
                    {
                        //found the container we are look for
                        foreach (var p in c.Packages)
                        {
                            //loop through that containers packages
                            if (p.Id == package.Id)
                            {
                                found = true;
                                //find the package we are looking for and remove it then break out of the loop
                                var confirm = await DisplayAlert("Unpacking Stuff", "Are you unpacking this stuff?", "Unpack", "Cancel");
                                if (confirm)
                                {
                                    c.Packages.Remove(p);
                                    if (c.Packages.Count == 0 && c.Id != App.MISCELLANEOUS_ITEM_BOX)
                                    {
                                        Debug.WriteLine("Empty Container Id: " + c.Id);
                                        var deletecontainer = await DisplayAlert("Empty Container", "This container is empty, are you done with it?", "Yes", "No");
                                        if (deletecontainer)
                                        {
                                            Debug.WriteLine("Container Deleted");
                                            temp.Remove(c);
                                            break;
                                        }
                                    }
                                    if (found)
                                    {
                                        break;
                                    }
                                }
                                Debug.WriteLine("Package Deleted?: " + confirm);
                            }
                        }
                    }
                    if (found)
                    {
                        break;
                    }
                }

                //Serizalize our new list
                var json = JsonConvert.SerializeObject(temp);
                Debug.WriteLine(json);
                //Replace the old json
                Settings.ContainerSetting = json;
                _ViewModel.LoadItems();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
        }


        private void packageImageView_Clicked(object sender, EventArgs e)
        {
            //get our current package
            var p = CarouselPackages.CurrentItem as PackageModel;
            //open image preview page with our image file path
            Navigation.PushAsync(new ImagePreviewPage(p.ImageFile));
        }

        private void containerPackageView_Clicked(object sender, EventArgs e)
        {
            //same like packageImageView_Clicked() but for containers
            var c = CarouselContainers.CurrentItem as ContainerModel;
            Navigation.PushAsync(new ImagePreviewPage(c.ImageFile));
        }

        private void TitleLabel_Tapped(object sender, EventArgs e)
        {
            var title = (Label)sender;
            title.BackgroundColor = (title.BackgroundColor == Color.Navy ? Color.White : Color.Navy);
            if (title.Id == containerLabel.Id)
            {
                functionContainers.IsVisible = !functionContainers.IsVisible;
            }
            else if (title.Id == packageLabel.Id)
            {
                functionPackages.IsVisible = !functionPackages.IsVisible;
            }
        }
    }
}
