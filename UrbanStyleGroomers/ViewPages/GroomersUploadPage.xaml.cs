using System;
using System.Collections.Generic;
using NativeMedia;
using Xamarin.Forms;

namespace UrbanStyleGroomers.ViewPages
{
    public partial class GroomersUploadPage : ContentPage
    {
        public GroomersUploadPage()
        {
            InitializeComponent();
        }
        public async void BackButton(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainGroomersPage());
        }
        private async void ImageUploads(object sender, EventArgs e)
        {
            var mediaFilePicked = await MediaGallery.PickAsync(10, MediaFileType.Image, MediaFileType.Video);
            if (mediaFilePicked?.Files == null)
            {
                return;
            }
            else
            {
                foreach (var mediaFiles in mediaFilePicked.Files)
                {
                    var fileName = mediaFiles.NameWithoutExtension;
                    var extension = mediaFiles.Extension;
                    var contentType = mediaFiles.ContentType;

                    // await DisplayAlert(fileName, $"Extension: {extension}, Content-type: {contentType}", "OK");
                }
            }
        }
    }
}
