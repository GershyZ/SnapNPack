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
    public partial class ImagePreviewPage : ContentPage
    {
        public ImagePreviewPage(string imgpath)
        {
            InitializeComponent();

            Debug.WriteLine("Image Path: " + imgpath);
            //Create a layout
            StackLayout layout = new StackLayout();
            //Add a regular image control
            Image img = new Image();
            img.VerticalOptions = LayoutOptions.FillAndExpand;
            img.HorizontalOptions = LayoutOptions.FillAndExpand;
            //set the path
            img.Source = imgpath;

            //Add the child
            layout.Children.Add(img);

            //Set the content
            Content = layout;
        }
    }
}