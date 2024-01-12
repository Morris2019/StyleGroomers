using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Refit;
using UrbanStyleGroomers.Model;
using UrbanStyleGroomers.ModelsServer;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace UrbanStyleGroomers.ViewPages
{
    public partial class CompletedJobsPage : ContentPage
    {

        private readonly HttpClient _client = new HttpClient();
        private ObservableCollection<Urbanuserbookings> UsersBookingsItems;
        public string UserID { get; set; }

        public CompletedJobsPage()
        {
            InitializeComponent();

        }
        protected async override void OnAppearing()
        {
            await LooadCompleteddJobs();
            base.OnAppearing();
        }
        private async void BackButton(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainGroomersPage());
        }
        async void CompletedRefresh_Refreshing(object sender, EventArgs e)
        {
           await LooadCompleteddJobs();
            CompletedRefresh.IsRefreshing = false;
        }
        async void CompletedReload(object sender, EventArgs e)
        {

            await LooadCompleteddJobs();
            CompletedRefresh.IsRefreshing = false;
        }
        private async Task LooadCompleteddJobs()
        {

            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.Internet)
            {
                UserID = await SecureStorage.GetAsync("UbanUsersId");

                string UrbanUrl = "http://20.87.12.160/urbanstyle/bookings?groomerid=" + UserID + "&status=completed";

                CompletedRefresh.IsRefreshing = true;
      
                try
                {
                    CompletedRefresh.IsRefreshing = true;

                    var content = await _client.GetStringAsync(UrbanUrl);
                    var deserializedPosts = JsonConvert.DeserializeObject<List<Urbanuserbookings>>(content);
                    UsersBookingsItems = new ObservableCollection<Urbanuserbookings>(deserializedPosts);

                    if (UsersBookingsItems.Count < 1)
                    {
                        CompletedRefresh.IsVisible = false;
                        CompletedRefresh.IsRefreshing = false;
                        //  CompletedRefresh.IsEnabled = false;
                        emptyListLayout.IsVisible = true;
                    }
                    else
                    {
                        UranActivity.ItemsSource = UsersBookingsItems;
                        CompletedRefresh.IsRefreshing = false;
                    }

                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert($"Data Error", ex.ToString(), "OK");
                    CompletedRefresh.IsRefreshing = false;
                }

                //CompletedRefresh.IsRefreshing = false;
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert($"Data Error", "Please check network and try again", "OK");
            }
        }
        async void UranActivity_SelectionChanged(System.Object sender, Xamarin.Forms.SelectionChangedEventArgs e)
        {
            var bookitems = e.CurrentSelection.FirstOrDefault() as Urbanuserbookings;
            if (bookitems == null)
                return;

            await Navigation.PushAsync(new CompletedDetailsPage(bookitems.date_time, bookitems.id, bookitems.tax_name));

            ((CollectionView)sender).SelectedItem = null;
        }
    }
}
