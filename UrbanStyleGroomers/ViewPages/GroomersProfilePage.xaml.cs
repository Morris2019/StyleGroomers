using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using NativeMedia;
using Newtonsoft.Json;
using Tobinco.EmbeddedFont;
using UrbanStyleGroomers.Model;
using UrbanStyleGroomers.Renderer;
using Xamarin.Essentials;
using Xamarin.Forms;

using System.Diagnostics;
using System.ComponentModel;
using System.Text;

namespace UrbanStyleGroomers.ViewPages
{
    public partial class GroomersProfilePage : ContentPage
    {
        private readonly HttpClient _client = new HttpClient();
        private ObservableCollection<UrbanUsers> userItems;
        private ObservableCollection<Urbanuserbookings> UsersBookingspayments;
        public string UserImageProfile { get; set; }
        public string UrbanuserMail { get; set; }
         public string UsersMobbile { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        int ratingPoint = 0;

        static readonly Color[] Colors = new Color[] { Color.Red, Color.Aqua, Color.Yellow, Color.Green };

        public GroomersProfilePage()
        {
            InitializeComponent();
            MyStar1.Text = MaterialIconClass.StarOutline;
            MyStar2.Text = MaterialIconClass.StarOutline;
            MyStar3.Text = MaterialIconClass.StarOutline;
            MyStar4.Text = MaterialIconClass.StarOutline;
            MyStar5.Text = MaterialIconClass.StarOutline;
        }
    
        protected async override void OnAppearing()
        {
            base.OnAppearing();


            UsersMobbile = await SecureStorage.GetAsync("Phonenumber");

            string UrbanUrl = "http://20.87.12.160/urbanstyle/accounts/" + UsersMobbile;

            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.Internet)
            {

                try
                {
                    var content = await _client.GetStringAsync(UrbanUrl);
                    var deserializedPosts = JsonConvert.DeserializeObject<List<UrbanUsers>>(content);
                    userItems = new ObservableCollection<UrbanUsers>(deserializedPosts);


                    foreach (var usersidinfo in userItems)
                    {
                        UserID = usersidinfo.id;
                        UsersMobbile = usersidinfo.mobile;
                        UserName = usersidinfo.name;
                        UserEmail = usersidinfo.email;
                    }
                    UrbanupUsername.Text = UserName;
                  
                    await SecureStorage.SetAsync("UbanUsersId", UserID);

                    if(UserID != null)
                    {
                        string imageurl = "http://20.87.12.160/images/groomer/" + UserID + ".jpeg";

                        ProfileImage.Source = imageurl;

                    }


                }
                catch (Exception ex)
                {
                    await DisplayAlert($"Data Error", ex.ToString(), "OK");
                }

            }
            else
            {
                await DisplayAlert($"Data Error", "Please check network and try again", "OK");
            }
            await LoadGroomersWork();
        }
        public async Task LoadGroomersWork()
        {
            string Completedworks = "http://20.87.12.160/urbanstyle/bookings?groomerid=" + UserID + "&status=completed";
           // Urbantotal.Text = string.Empty;

            var content = await _client.GetStringAsync(Completedworks);
            var deserializedPosts = JsonConvert.DeserializeObject<List<Urbanuserbookings>>(content);
            UsersBookingspayments = new ObservableCollection<Urbanuserbookings>(deserializedPosts);

            if (UsersBookingspayments.Count > 0)
            {
                GroomerCompleted.Text = UsersBookingspayments.Count.ToString();
            }
            else
            {
                GroomerCompleted.Text = UsersBookingspayments.Count.ToString();
            }
        }

        async void BackButton(object sender, EventArgs e)
        {
             //SecureStorage.RemoveAll();

            await Navigation.PushAsync(new MainGroomersPage());
        }
        async void ImageUploads(object sender, EventArgs e)
        {

        }

        private async void EditImage(object sender, EventArgs e)
        {
            try
            {
                var imagefilePicker = await FilePicker.PickAsync(
                    new PickOptions
                    {
                        FileTypes = FilePickerFileType.Images
                    });
                if (imagefilePicker == null)
                    return;

                var stream = await imagefilePicker.OpenReadAsync();
                //userProfille.Source = ImageSource.FromStream(() => stream);

                if (stream != null)
                {
                    byte[] filebytearray = new byte[stream.Length];
                    stream.Read(filebytearray, 0, (int)stream.Length);
                    UserImageProfile = Convert.ToBase64String(filebytearray);

                    try
                    {
                        var groomimage = new GroomersImage
                        {
                            imageByte = UserImageProfile,
                            imageType = "groomer",
                            mobileNumber = UserID
                        };
                       
                        string profileUrbanUrl = "http://20.87.12.160/urbanstyle/accounts/signup/addimage/";
                            
                        HttpClient profileclient = new HttpClient();
                        var serializedLogin = JsonConvert.SerializeObject(groomimage);
                        HttpResponseMessage profileresponse = await profileclient.PostAsync(profileUrbanUrl, new StringContent(serializedLogin, Encoding.UTF8, "application/json"));
                        String responseString = await profileresponse.Content.ReadAsStringAsync();

                        if(responseString != null)
                        {
                            ProfileImage.Source = responseString;
                        }
                       

                    }
                    catch(Exception ex)
                    {
                        await DisplayAlert("Image Upload catch", ex.ToString(), "Ok");
                    }
                }
                else
                {
                    await DisplayAlert("Image Upload Failed", "Please try again", "Ok");
                }
              

            }
            catch (Exception ex)
            {
                await DisplayAlert("Image Upload Failed", "Please try again", "Ok");
            }
            
        }
        void Rating1(object sender, EventArgs e)
        {
            MyStar1.Text = MaterialIconClass.Star;
            MyStar2.Text = MaterialIconClass.StarOutline;
            MyStar3.Text = MaterialIconClass.StarOutline;
            MyStar4.Text = MaterialIconClass.StarOutline;
            MyStar5.Text = MaterialIconClass.StarOutline;
            ratingPoint = 1;
        }
        void Rating2(object sender, EventArgs e)
        {
            MyStar1.Text = MaterialIconClass.Star;
            MyStar2.Text = MaterialIconClass.Star;
            MyStar3.Text = MaterialIconClass.StarOutline;
            MyStar4.Text = MaterialIconClass.StarOutline;
            MyStar5.Text = MaterialIconClass.StarOutline;
            ratingPoint = 2;
        }
        void Rating3(object sender, EventArgs e)
        {
            MyStar1.Text = MaterialIconClass.Star;
            MyStar2.Text = MaterialIconClass.Star;
            MyStar3.Text = MaterialIconClass.Star;
            MyStar4.Text = MaterialIconClass.StarOutline;
            MyStar5.Text = MaterialIconClass.StarOutline;
            ratingPoint = 3;
        }
        void Rating4(object sender, EventArgs e)
        {
            MyStar1.Text = MaterialIconClass.Star;
            MyStar2.Text = MaterialIconClass.Star;
            MyStar3.Text = MaterialIconClass.Star;
            MyStar4.Text = MaterialIconClass.Star;
            MyStar5.Text = MaterialIconClass.StarOutline;
            ratingPoint = 4;
        }
        void Rating5(object sender,EventArgs e)
        {
            MyStar1.Text = MaterialIconClass.Star;
            MyStar2.Text = MaterialIconClass.Star;
            MyStar3.Text = MaterialIconClass.Star;
            MyStar4.Text = MaterialIconClass.Star;
            MyStar5.Text = MaterialIconClass.Star;
            ratingPoint = 5;
        }

        async void UpdateUrbanProfile(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GroomerProfileUpdate());
           
        }
    }
}
