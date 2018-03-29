using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms.Platform.Android;
using SnapNPack.Views;
using Android.Graphics;
using System.Threading.Tasks;
using System.ComponentModel;
using Xamarin.Forms;
using SnapNPack.Droid;

//Register our class as a renderer for FastImageView
[assembly: ExportRenderer(typeof(FastImageView), typeof(FastImageViewRenderer))]
namespace SnapNPack.Droid
{
    //The android implementation of the fastimageview renderer. More on custom renderer here https://developer.xamarin.com/guides/xamarin-forms/application-fundamentals/custom-renderer/
    public class FastImageViewRenderer : ViewRenderer<FastImageView, ImageView>
    {
        ImageView _ImageView;

        //Image 
        BitmapFactory.Options _Options = new BitmapFactory.Options
        {
            //Make the image for times smaller
            InSampleSize = 10
        };

        //Called when the xamarin forms element changes
        protected override void OnElementChanged(ElementChangedEventArgs<FastImageView> e)
        {
            base.OnElementChanged(e);

            //If the control has not been created. Then create it
            if(Control == null)
            {
                _ImageView = new ImageView(Context);
                //Sets the native control
                SetNativeControl(_ImageView);
            }
            //e.OldElement will not be null when a control is about to be destroy
            if(e.OldElement != null)
            {
                //Dispose the image and de-hook the events
                _ImageView.Click -= _ImageView_Click;
                _ImageView.Dispose();
            }
            //e.NewElement is not null when the element has just been created
            if(e.NewElement != null)
            {
                _ImageView.Click += _ImageView_Click;

                Bitmap result = BitmapFactory.DecodeFile(Element.ImageSrc, _Options);
                _ImageView.SetImageBitmap(result);
                _ImageView.Invalidate();
            }
        }

        private void _ImageView_Click(object sender, EventArgs e)
        {
            //The image has been clicked
            Element.ExecuteClicked();
        }

        //Is called when a property is changed on the element
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            //If our image property has been changed
            if(e.PropertyName == FastImageView.ImageSrcProperty.PropertyName)
            {
                //Image property changed load the new image
                System.Diagnostics.Debug.WriteLine(Element.ImageSrc);
                Bitmap result = BitmapFactory.DecodeFile(Element.ImageSrc, _Options);
                _ImageView.SetImageBitmap(result);
                _ImageView.Invalidate();
            }
        }
    }
}