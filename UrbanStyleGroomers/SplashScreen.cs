using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UrbanStyleGroomers.Model;
using UrbanStyleGroomers.ViewPages;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace UrbanStyleGroomers
{
    public class SplashScreen : ContentPage
    {
        private ObservableCollection<UrbanUsers> UsersdataDetail;
        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public string UserPhone { get; set; }
        public string UserPassword { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
        Image SplashImage;
        ActivityIndicator progressIndicator;
        public SplashScreen()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            var sub = new AbsoluteLayout();
            SplashImage = new Image
            {
                Source = "groomerapplogo.png",
                WidthRequest = 140,
                HeightRequest = 140,
            };
            progressIndicator = new ActivityIndicator
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.End,
                WidthRequest = 50,
                HeightRequest = 50,
                Color = Color.FromHex("#043C7C")
            };

            AbsoluteLayout.SetLayoutFlags(SplashImage, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(SplashImage, new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));

            AbsoluteLayout.SetLayoutFlags(progressIndicator, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(progressIndicator, new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));

            sub.Children.Add(SplashImage);
            sub.Children.Add(progressIndicator);
            this.BackgroundColor = Color.FromHex("#FFFFFF");
            this.Content = sub;
            NavigationPage.SetHasNavigationBar(this, false);

        }
        protected override async void OnAppearing()
        {
            await SplashImage.ScaleTo(1, 1500, Easing.Linear);

            await CheckUserValid();

            base.OnAppearing();
        }
        async Task CheckUserValid()
        {
            UserPassword = await SecureStorage.GetAsync("UrbanUserPassword");
            UserPhone = await SecureStorage.GetAsync("Phonenumber");

            if(UserPassword != null && UserPhone != null)
            {
                Application.Current.MainPage = new NavigationPage(new MainGroomersPage());
            }
            else
            {
                Application.Current.MainPage = new NavigationPage(new GroomerLoginPage());
            }
     
        }
    }
}