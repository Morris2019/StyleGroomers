using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UrbanStyleGroomers.Model;
using Xamarin.Forms;
using System.Net.Http;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace UrbanStyleGroomers.ViewPages
{
    public partial class PendingJobsPage : ContentPage
    {
        private readonly HttpClient _client = new HttpClient();
        private ObservableCollection<Urbanuserbookings> UsersBookingsItems;
        public string UserID { get; set; }

        public PendingJobsPage()
        {
            InitializeComponent();

        }
        protected async override void OnAppearing()
        {
            await LooadPendingJobs();
            base.OnAppearing();
        }
        private async void BackButton(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainGroomersPage());
        }
        private async Task LooadPendingJobs()
        {

            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.Internet)
            {
                UserID = await SecureStorage.GetAsync("UbanUsersId");

                string UrbanUrl = "http://20.87.12.160/urbanstyle/bookings?groomerid=" + UserID + "&status=pending";

                PendingRefresh.IsVisible = true;

                try
                {
                    PendingRefresh.IsRefreshing = true;

                    var content = await _client.GetStringAsync(UrbanUrl);
                    var deserializedPosts = JsonConvert.DeserializeObject<List<Urbanuserbookings>>(content);
                    UsersBookingsItems = new ObservableCollection<Urbanuserbookings>(deserializedPosts);

                    if (UsersBookingsItems.Count < 1)
                    {
                        PendingRefresh.IsVisible = false;
                        PendingRefresh.IsRefreshing = false;
                        //  CompletedRefresh.IsEnabled = false;
                        emptyListLayout.IsVisible = true;
                    }
                    else
                    {
                        PendingCollection.ItemsSource = UsersBookingsItems;
                        PendingRefresh.IsRefreshing = false;
                    }

                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert($"Data Error", ex.ToString(), "OK");
                    PendingRefresh.IsRefreshing = false;
                }

                //PendingRefresh.IsRefreshing = false;
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert($"Data Error", "Please check network and try again", "OK");
            }
        }
        async void PendingReload(object sender, EventArgs e)
        {
            await LooadPendingJobs();
            PendingRefresh.IsRefreshing = false;
        }
        async void PendingList_Refreshing(object sender, EventArgs e)
        {
            await LooadPendingJobs();
            PendingRefresh.IsRefreshing = false;
        }
        async void PendingCollection_SelectionChanged(System.Object sender, Xamarin.Forms.SelectionChangedEventArgs e)
        {
            var jobList = e.CurrentSelection.FirstOrDefault() as Urbanuserbookings;
            if (jobList == null)
                return;

            await Navigation.PushAsync(new GroomersJobDetals(jobList.tax_name, jobList.id, jobList.date_time));

            ((CollectionView)sender).SelectedItem = null;
        }

    }
}
