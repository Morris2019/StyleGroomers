using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Windows.Input;
using UrbanStyleGroomers.Model;
using UrbanStyleGroomers.ViewPages;
using Xamarin.Forms;

namespace UrbanStyleGroomers.ViewModels
{
    public class MenuViewModel : BindableObject
    {
        private const string UrbanUrl = "https://urbanstyleapi.azurewebsites.net/Bookings/GetUserBooking/237";
        private readonly HttpClient _client = new HttpClient();
        private ObservableCollection<Booking> UsersBookingsItems;

        private ObservableCollection<MenuListModel> MenuList;
        public MenuViewModel()
        {
           
            LoadMenu();
        }
        public ObservableCollection<MenuListModel> menulist
        {
            get { return MenuList; }
            set
            {
                MenuList = value;
                OnPropertyChanged();
            }
        }
        public MenuListModel SelectedItem { get; set; }

        INavigation Navigation => Application.Current.MainPage.Navigation;

        public ICommand ItemSelectedCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (SelectedItem == null)
                        return;

                    if (SelectedItem.MenuId == 1)
                    {
                        await Navigation.PushAsync(new GroomersProfilePage());
                    }
                    else if (SelectedItem.MenuId == 2)
                    {
                        await Navigation.PushAsync(new GroomerTotalEarnings());
                    }
                    else if (SelectedItem.MenuId == 4)
                    {
                        await Navigation.PushAsync(new GroomersUploadPage());
                    }
                    SelectedItem = null;
                });
            }
        }
        void LoadMenu()
        {
            menulist = new ObservableCollection<MenuListModel>() {
                 new MenuListModel
                 {
                     Name = "Profile ",
                     MenuId=1
                 },
                 new MenuListModel
                 {
                     Name = "Total Earnings",
                     MenuId=2
                 },
                 new MenuListModel
                 {
                     Name = "Upload Images",
                     MenuId=4
                 },
            };

        }
    }
}
