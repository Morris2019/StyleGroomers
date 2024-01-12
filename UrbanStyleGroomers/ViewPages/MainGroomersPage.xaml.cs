using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UrbanStyleGroomers.Model;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace UrbanStyleGroomers.ViewPages
{
    public partial class MainGroomersPage : ContentPage
    {
        //private const ;
        private readonly HttpClient _client = new HttpClient();
        private ObservableCollection<UrbanUsers> UsersdataDetail;
        public string UrbanuserMail { get; set; }
        public string UserImageProfile { get; set; }
        public string UsersMobbile { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        CancellationTokenSource cts;
        private double Lang { get; set; }
        private double Long { get; set; }
        private readonly Xamarin.Forms.Maps.Geocoder _geocoder = new Xamarin.Forms.Maps.Geocoder();

        public MainGroomersPage()
        {
            InitializeComponent();


        }
        protected async override void OnAppearing()
        {

            await GetCurrentLocation();

            UrbanuserMail = await SecureStorage.GetAsync("UserEmail");

            string UrbanUrl = "http://20.87.12.160/urbanstyle/accounts/" + UrbanuserMail;

            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.Internet)
            {

                try
                {
                    var content = await _client.GetStringAsync(UrbanUrl);
                    var deserializedPosts = JsonConvert.DeserializeObject<List<UrbanUsers>>(content);
                    UsersdataDetail = new ObservableCollection<UrbanUsers>(deserializedPosts);


                    foreach (var usersidinfo in UsersdataDetail)
                    {
                        UserID = usersidinfo.id;
                        UsersMobbile = usersidinfo.mobile;
                        UserName = usersidinfo.name;
                        UserEmail = usersidinfo.email;
                    }
                    UsernameField.Text = UserName;

                    await SecureStorage.SetAsync("UbanUsersId", UserID);

                    if (UserID != null)
                    {
                        string imageurl = "http://20.87.12.160/images/groomer/" + UserID + ".jpeg";

                        ProfileImages.Source = imageurl;

                    }
                }
                catch (Exception ex)
                {
                   
                }

            }
            else
            {
                await DisplayAlert($"Data Error", "Please check network and try again", "OK");
            }
            base.OnAppearing();
        }
        void Userlogout(object sender, EventArgs e)
        {
            SecureStorage.RemoveAll();

            Application.Current.MainPage = new NavigationPage(new SplashScreen());
        }
        private async void menuButton(object sender, EventArgs e)
        {
            OpenMenu();
        }
        private void OpenMenu()
        {
            MenuGrid.IsVisible = true;

            Action<double> callback = input => MenuView.TranslationX = input;
            MenuView.Animate("anim", callback, -260, 0, 16, 300, Easing.CubicInOut);
        }

        private void CloseMenu()
        {
            Action<double> callback = input => MenuView.TranslationX = input;
            MenuView.Animate("anim", callback, 0, -260, 16, 300, Easing.CubicInOut);

            MenuGrid.IsVisible = false;
        }
        private void OverlayTapped(object sender, EventArgs e)
        {
            CloseMenu();
        }
        private async void PendingJobs(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PendingJobsPage());
        }
        private async void CompletedJobs(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CompletedJobsPage());
        }
        private async void OpenMaps(object sender, EventArgs e)
        {
            await Xamarin.Essentials.Map.OpenAsync(Lang, Long, new MapLaunchOptions
            {
                NavigationMode = NavigationMode.Default
            });

        }

        async Task GetCurrentLocation()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                cts = new CancellationTokenSource();
                var location = await Geolocation.GetLocationAsync(request, cts.Token);

                if (location != null)
                {
                    Lang = location.Latitude;
                    Long = location.Longitude;

                    Xamarin.Forms.Maps.Position position = new Xamarin.Forms.Maps.Position(Lang, Long);
                    var UserCurrentLoacationn = ($" {location.Latitude}, {location.Longitude}");
                    //ClockLocation.Text = UserCurrentLoacationn;
                    UrbanMap.MoveToRegion(Xamarin.Forms.Maps.MapSpan.FromCenterAndRadius(position, Xamarin.Forms.Maps.Distance.FromMeters(10)));
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }
        }
        protected override void OnDisappearing()
        {
            if (cts != null && !cts.IsCancellationRequested)
                cts.Cancel();
            base.OnDisappearing();
        }
    }
}
