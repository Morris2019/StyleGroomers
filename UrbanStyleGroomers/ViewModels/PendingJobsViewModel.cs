using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Refit;
using UrbanStyleGroomers.Model;
using UrbanStyleGroomers.ModelsServer;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace UrbanStyleGroomers.ViewModels
{
    public class PendingJobsViewModel : BindableObject
    {
        private readonly HttpClient _client = new HttpClient();
        public string UserID { get; set; }
        private ObservableCollection<Urbanuserbookings> UsersBookingsItems;

        public Command GetBookingsCommand { get; }

        public PendingJobsViewModel()
        {
            LooadPendingJobs();

            GetBookingsCommand = new Command(async () => await GetBookingsAsync());
            // BookItemSelected = new Command(async () => await GetSelectedItemAsync());
        }
        bool isRefreshing;
        public bool IsRefreshing
        {
            get => isRefreshing;
            set
            {
                isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }

        async Task GetBookingsAsync()
        {
            await LooadPendingJobs();
        }
        //INavigation Navigation => Application.Current.MainPage.Navigation;

        public ObservableCollection<Urbanuserbookings> bookinItems
        {
            get { return UsersBookingsItems; }
            set
            {
                UsersBookingsItems = value;
                OnPropertyChanged();
            }
        }
        private async Task LooadPendingJobs()
        {

            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.Internet)
            {
                UserID = await SecureStorage.GetAsync("UbanUsersId");

                string UrbanUrl = "http://20.87.12.160/urbanstyle/bookings?groomerid=" + UserID + "&status=completed";

                if (IsRefreshing)
                    return;
                try
                {
                    IsRefreshing = true;

                    var content = await _client.GetStringAsync(UrbanUrl);
                    var deserializedPosts = JsonConvert.DeserializeObject<List<Urbanuserbookings>>(content);
                    bookinItems = new ObservableCollection<Urbanuserbookings>(deserializedPosts); //Converting the List to ObservableCollection of Post

                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert($"Data Error", ex.ToString(), "OK");
                }

                IsRefreshing = false;
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert($"Data Error", "Please check network and try again", "OK");
            }
        }

    }
}