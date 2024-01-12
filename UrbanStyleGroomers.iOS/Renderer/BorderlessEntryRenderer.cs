using System;
using System.ComponentModel;
using UIKit;
using UrbanStyleGroomers.iOS.Renderer;
using UrbanStyleGroomers.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(BorderlessEntry), typeof(BorderlessEntryRenderer))]
namespace UrbanStyleGroomers.iOS.Renderer
{
    public class BorderlessEntryRenderer : EntryRenderer
    {
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (Control == null) return;
            Control.Layer.BorderWidth = 0;
            Control.BorderStyle = UITextBorderStyle.None;

        }
    }
}
