using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SnapNPack.Views
{
    /*You may be asking. Why are we not using the regular xamarin image view. Well there is a performance problem. When we take images with our camera 
     Depending on your reselution of your camera images can get really really big. And displaying more than two or three of these images at once will take up
     a lot of memory and speed. So we solve this by implementing our own view which modifies the size and quality of the image to lower the performance hit*/
    public class FastImageView : View
    {
        //A eventhandler for forwarding that the image was clicked
        public event EventHandler Clicked;
        public void ExecuteClicked()
        {
            Clicked?.Invoke(this, EventArgs.Empty);
        }

        //A Bindable property which is the image's path
        public static readonly BindableProperty ImageSrcProperty =
            BindableProperty.Create("ImageSrc", typeof(string), typeof(FastImageView), "");

        //Our image source property 
        public string ImageSrc
        {
            get { return (string)GetValue(ImageSrcProperty); }
            set {
                SetValue(ImageSrcProperty, value);
                OnPropertyChanged(nameof(ImageSrc));
            }
        }
    }
}
