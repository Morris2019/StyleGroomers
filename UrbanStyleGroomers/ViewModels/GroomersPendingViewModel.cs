using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UrbanStyleGroomers.Model;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace UrbanStyleGroomers.ViewModels
{
    public class GroomersPendingViewModel : BindableObject
    {
        string noPending = string.Empty;
        private readonly HttpClient _client = new HttpClient();
        private ObservableCollection<Urbanuserbookings> UbanPendingItems;
        public string UserID { get; set; }

        public Command PendingRefreshCommand { get; }
        public Command EmptyPending { get; }

        public GroomersPendingViewModel()
        {
            LooadPendingJobs();
            PendingRefreshCommand = new Command(async () => await PendingRefreshAsync());
            EmptyPending = new Command(async () => await EmptyPendingAsync());
        }
        public ObservableCollection<Urbanuserbookings> PendingItems
        {
            get { return UbanPendingItems; }
            set
            {
                UbanPendingItems = value;
                OnPropertyChanged();
            }
        }
        public string NoPending
        {
            get => noPending;
            set
            {
                if (noPending == value)
                    return;
                noPending = value;
                OnPropertyChanged(nameof(noPending));
                OnPropertyChanged(nameof(ListeError));
            }
        }
        public string ListeError => $"No Pending Jobs Available";
        async Task EmptyPendingAsync()
        {
            await LooadPendingJobs();
        }
        async Task PendingRefreshAsync()
        {
            await LooadPendingJobs();
        }
        private async Task LooadPendingJobs()
        {

            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.Internet)
            {
                if (IsRefreshing)
                    return;

                UserID = await SecureStorage.GetAsync("UbanUsersId");

                string UrbanUrl = "http://20.87.12.160/urbanstyle/bookings?groomerid=" + "13" + "&status=pending";

                try
                {
                    IsRefreshing = true;

                    var content = await _client.GetStringAsync(UrbanUrl);
                    var deserializedPosts = JsonConvert.DeserializeObject<List<Urbanuserbookings>>(content);
                    //PendingItems = new ObservableCollection<Urbanuserbookings>(deserializedPosts);

                   if (deserializedPosts.Count < 1)
                   {
                      string ListeError = $"No Pending Jobs Available";

                       IsRefreshing = false;
                       
                   }
                   else
                   {
                        PendingItems = new ObservableCollection<Urbanuserbookings>(deserializedPosts);
//
                        IsRefreshing = false;
                    }

                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert($"Data Error", ex.ToString(), "OK");
                    IsRefreshing = false;
                }

            }
            else
            {
                await Application.Current.MainPage.DisplayAlert($"Data Error", "Please check network and try again", "OK");
            }
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
    }
}
