using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Sharpnado.HorizontalListView.Droid;
using Android;
using Android.Content;

namespace UrbanStyleGroomers.Droid
{
    [Activity(Label = "UrbanStyleGroomers", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        const int RequestLocationId = 0;
        readonly string[] LocatioPermisions =
        {
            Manifest.Permission.AccessCoarseLocation,
            Manifest.Permission.AccessFineLocation
        };
        protected override void OnStart()
        {
            base.OnStart();

            if ((int)Build.VERSION.SdkInt >= 23)
            {
                if (CheckSelfPermission(Manifest.Permission.AccessFineLocation) != Permission.Granted)
                {
                    RequestPermissions(LocatioPermisions, RequestLocationId);
                }
                else
                {
                    // Permissions already granted - display a message.
                }
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            NativeMedia.Platform.Init(this, savedInstanceState);
            SharpnadoInitializer.Initialize();
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(true);
            Xamarin.FormsMaps.Init(this, savedInstanceState);

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (NativeMedia.Platform.CheckCanProcessResult(requestCode, resultCode, data))
            {
                NativeMedia.Platform.OnActivityResult(requestCode, resultCode, data);
            }

            base.OnActivityResult(requestCode, resultCode, data);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            if (requestCode == RequestLocationId)
            {
                if ((grantResults.Length == 1) && (grantResults[0] == (int)Permission.Granted))
                {
                    // Permissions granted - display a message.
                }
                else
                {
                    // Permissions denied - display a message.
                }

            }
            else
            {
                Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

                base.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            }
        }
    }
}
