using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UrbanStyleGroomers.Model;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace UrbanStyleGroomers.ViewPages
{
    public partial class GroomersJobDetals : ContentPage
    {
        private readonly HttpClient _client = new HttpClient();

        CancellationTokenSource cts;
        public string CustomerAddress { get; set; }
        private double Lang { get; set; }
        private double Long { get; set; }
        private readonly Xamarin.Forms.Maps.Geocoder _geocoder = new Xamarin.Forms.Maps.Geocoder();

        private string ItemId { get; set; }
        public GroomersJobDetals(string tax_name, string id, DateTime date_time)
        {
            InitializeComponent();

            ItemId = id;
            JobService.Text = tax_name;
            JobDate.Text = date_time.ToLongDateString();

        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await GetCurrentLocation();
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
                    var addresses = await _geocoder.GetAddressesForPositionAsync(position);
                    //jobAddress.Text
                     CustomerAddress = addresses.ToString();
                    //await DisplayAlert("Address", CustomerAddress, "OK");
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
        
        protected async void OpenMaps(object sender, EventArgs e)
        {
            await Map.OpenAsync(Lang, Long, new MapLaunchOptions
            {
                NavigationMode = NavigationMode.Default
            });
        }
        public async void BackButton(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PendingJobsPage());
            //base.OnBackButtonPressed();//
            //Application.Current.MainPage = new NavigationPage(new PendingJobsPage()); 

        }
        public async void AcceptJobButton(object sender, EventArgs e)
        {
        }
        public async void DeclineJobButton(object sender, EventArgs e)
        {
        }
    }
}
