using SnapNPack.iOS;
using SnapNPack.Views;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(FastImageView), typeof(FastImageViewRenderer))]
namespace SnapNPack.iOS
{
    public class FastImageViewRenderer : ViewRenderer<FastImageView, UIButton>
    {
        UIButton _ImageView;

        protected override void OnElementChanged(ElementChangedEventArgs<FastImageView> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                _ImageView = new UIButton();
                SetNativeControl(_ImageView);
            }
            if (e.OldElement != null)
            {
                _ImageView.TouchDown -= _ImageView_TouchDown;
                _ImageView.Dispose();
            }
            if (e.NewElement != null)
            {
                _ImageView.TouchDown += _ImageView_TouchDown;

                Task.Run(() =>
                {
                    _ImageView.SetImage(UIImage.FromFile(Element.ImageSrc), UIControlState.Normal);
                });
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if(e.PropertyName == FastImageView.ImageSrcProperty.PropertyName)
            {
                Task.Run(() =>
                {
                    _ImageView.SetImage(UIImage.FromFile(Element.ImageSrc), UIControlState.Normal);
                });
            }
        }

        private void _ImageView_TouchDown(object sender, EventArgs e)
        {
            Element.ExecuteClicked();
        }
    }
}