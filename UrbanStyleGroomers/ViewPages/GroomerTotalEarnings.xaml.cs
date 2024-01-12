using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UrbanStyleGroomers.Model;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace UrbanStyleGroomers.ViewPages
{
    public partial class GroomerTotalEarnings : ContentPage
    {
        //private const string UrbanUrl = "http://20.87.12.160/urbanstyle/bookings?groomerid=10&status=completed";
        private readonly HttpClient _client = new HttpClient();
        private ObservableCollection<Urbanuserbookings> UsersBookingspayments;
        public string UserID { get; set; }
        public string begindate{get;set;}
        public string laterdate { get; set; }
        //public double groomertotal { get; set; }
        public GroomerTotalEarnings()
        {
            InitializeComponent();
            
        }
        protected async override void OnAppearing()
        {
            await LooadPayments();
            base.OnAppearing();
        }
            private async void BackButton(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainGroomersPage());
        }
        private async Task LooadPayments()
        {

            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.Internet)
            {
                UserID = await SecureStorage.GetAsync("UbanUsersId");

                string UrbanUrl = "http://20.87.12.160/urbanstyle/bookings?groomerid=" + UserID + "&status=completed";

                if (Payrefrresh.IsRefreshing)
                    return;
                try
                {
                    Urbantotal.Text = string.Empty;

                    var content = await _client.GetStringAsync(UrbanUrl);
                    var deserializedPosts = JsonConvert.DeserializeObject<List<Urbanuserbookings>>(content);
                    UsersBookingspayments = new ObservableCollection<Urbanuserbookings>(deserializedPosts);

                    if(UsersBookingspayments.Count < 1)
                    {
                        Payrefrresh.IsVisible = false;
                        Payrefrresh.IsEnabled = false;
                        emptyList.IsVisible = true;
                    }
                    else
                    {
                        LoadPayemt.ItemsSource = UsersBookingspayments;

                        List<double> totalprices = new List<double>();


                        double totalprice = UsersBookingspayments.Sum(m => m.amount_to_pay);
                        double numberdivide = totalprice / 2;

                        Urbantotal.Text = numberdivide.ToString();

                        Payrefrresh.IsRefreshing = false;
                    }
                    
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert($"Data Error", ex.ToString(), "OK");
                    Payrefrresh.IsRefreshing = false;
                }

               
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert($"Data Error", "Please check network and try again", "OK");
            }
        }
       async void Payrefrresh_Refreshing(object sender, EventArgs e)
        {
            await LooadPayments();
        }
        async void EaringsReload(object sender, EventArgs e)
        {
            Payrefrresh.IsRefreshing = true;

            await LooadPayments();
        }
        async void LoadPayemt_SelectionChanged(System.Object sender, Xamarin.Forms.SelectionChangedEventArgs e)
        {
            var earingins = e.CurrentSelection.FirstOrDefault() as Urbanuserbookings;
            if (earingins == null)
                return;

           await Navigation.PushAsync(new CompletedDetailsPage(earingins.date_time, earingins.id, earingins.tax_name));

            ((CollectionView)sender).SelectedItem = null;
        }

        async void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            begindate = startDate.Date.ToString("yyyy-MM-dd");
            laterdate = EndDate.Date.ToString("yyyy-MM-dd");
            Payrefrresh.IsRefreshing = true;

            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.Internet)
            {
                LoadPayemt.ItemsSource = null;
               

                UserID = await SecureStorage.GetAsync("UbanUsersId");

                string UrbanUrl = "http://20.87.12.160/urbanstyle/bookings?groomerid=" + UserID + "&status=completed&startDate="+ begindate + "&endDate="+ laterdate ;

                try
                {
                    Urbantotal.Text = string.Empty;

                    var content = await _client.GetStringAsync(UrbanUrl);
                    var deserializedPosts = JsonConvert.DeserializeObject<List<Urbanuserbookings>>(content);
                    UsersBookingspayments = new ObservableCollection<Urbanuserbookings>(deserializedPosts);

                    if (UsersBookingspayments.Count < 1)
                    {
                        Payrefrresh.IsVisible = false;
                        Payrefrresh.IsEnabled = false;
                        emptyList.IsVisible = true;
                    }
                    else
                    {
                        Payrefrresh.IsVisible = true;
                        Payrefrresh.IsEnabled = true;

                        LoadPayemt.ItemsSource = UsersBookingspayments;

                        List<double> totalprices = new List<double>();


                        double totalprice = UsersBookingspayments.Sum(m => m.amount_to_pay);
                        double numberdivide = totalprice / 2;

                        Urbantotal.Text = numberdivide.ToString();

                        Payrefrresh.IsRefreshing = false;
                    }

                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert($"Data Error", ex.ToString(), "OK");
                    Payrefrresh.IsRefreshing = false;
                }


            }
            else
            {
                await Application.Current.MainPage.DisplayAlert($"Data Error", "Please check network and try again", "OK");
            }

        }
    }
}
