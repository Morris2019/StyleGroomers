using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using NativeMedia;
using Newtonsoft.Json;
using UrbanStyleGroomers.Model;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace UrbanStyleGroomers.ViewPages
{
    public partial class GroomerProfileUpdate : ContentPage
    {
        private const string UrbanUrl = "http://20.87.12.160/urbanstyle/accounts";
        private readonly HttpClient _client = new HttpClient();
        public string UserImageProfile { get; set; }
        public string UsersMobbile { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        private ObservableCollection<UrbanUsers> UsersdataDetail;

        private bool emailValid;

        public bool EmailValid
        {
            get => emailValid;
            set
            {
                emailValid = value;
                OnPropertyChanged();
            }
        }

        public GroomerProfileUpdate()
        {
            InitializeComponent();

        }
        protected async override void OnAppearing()
        {
            await LoadGroomerData();
            base.OnAppearing();
        }
        async void UploadUserImage(object sender, EventArgs e)
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
                }
                else
                {
                    await DisplayAlert("Image Upload Failed", "Please try again", "Ok");
                }
                var imagestreamm = await imagefilePicker.OpenReadAsync();
                userProfille.Source = ImageSource.FromStream(() => imagestreamm);

            }
            catch (Exception ex)
            {
                await DisplayAlert("Image Upload Failed", "Please try again", "Ok");
            }
        }
        public async Task LoadGroomerData()
        {


            UsersMobbile = await SecureStorage.GetAsync("Phonenumber");

            string UrbanUrl = "http://20.87.12.160/urbanstyle/accounts/" + UsersMobbile;

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
                    UrbanupUsername.Text = UserName;
                    UrbanupUsermail.Text = UserEmail;
                    UserupPhoneNumber.Text = UsersMobbile;
                    await SecureStorage.SetAsync("UbanUsersId", UserID);

                    if (UserID != null)
                    {
                        string imageurl = "http://20.87.12.160/images/groomer/" + UserID + ".jpeg";

                        userProfille.Source = imageurl;

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


        }
        public async void UpdateProfile(object sender, EventArgs e)
        {
            var current = Connectivity.NetworkAccess;

            if (string.IsNullOrEmpty(UrbanupUsername.Text))
            {
                await DisplayAlert("Account error", "Please enter your Username", "OK");
            }
            else if (string.IsNullOrWhiteSpace(UrbanupUsermail.Text))
            {
                await DisplayAlert("Account error", "Please enter your email", "OK");
            }
            else if (!myMultiValidation.IsValid)
            {
                await DisplayAlert("Error", "Email not valid", "OK");
            }
            else if (string.IsNullOrWhiteSpace(UserupPhoneNumber.Text))
            {
                await DisplayAlert("Account error", "Please your Phone number", "OK");
            }

            else
            {
                if (current == NetworkAccess.Internet)
                {
                    UrbanupUsername.IsReadOnly = true;
                    UrbanupUsermail.IsReadOnly = true;
                    UserupPhoneNumber.IsReadOnly = true;

                    try
                    {
                        RegIndicator.IsRunning = true;

                        var login = new UsersInsert
                        {
                            name = UrbanupUsername.Text,
                            email = UrbanupUsermail.Text,
                            mobile = UserupPhoneNumber.Text,
                        };

                        HttpClient _client = new HttpClient();
                        string serializedLogin = JsonConvert.SerializeObject(login);
                        StringContent content = new StringContent(serializedLogin, Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await _client.PutAsync("http://20.87.12.160/urbanstyle/accounts/"+UserID, content);
                        string result = await response.Content.ReadAsStringAsync();
                        ResponseModel responsemodel = JsonConvert.DeserializeObject<ResponseModel>(result);

                        if (responsemodel.success == true)
                        {
                            
                            await Navigation.PushAsync (new GroomersProfilePage());
                            RegIndicator.IsRunning = false;


                        }
                        else
                        {
                            UrbanupUsername.IsReadOnly = false;
                            UrbanupUsermail.IsReadOnly = false;
                            UserupPhoneNumber.IsReadOnly = false;


                            await DisplayAlert("Login error", "Try again", "OK");
                            RegIndicator.IsRunning = false;
                        }
                       
                    }

                    catch (Exception ex)
                    {
                        UrbanupUsername.IsReadOnly = false;
                        UrbanupUsermail.IsReadOnly = false;
                        UserupPhoneNumber.IsReadOnly = false;

                        await DisplayAlert("Update Account", "Account failed to update", "Ok");
                        RegIndicator.IsRunning = false;
                    }
                }
                else
                {
                    await DisplayAlert($"Network Error", "Please check network and try again", "OK");
                }
            }
        }
        async void BackButton(object sender, EventArgs e)
        {
            //SecureStorage.RemoveAll();

            await Navigation.PushAsync(new GroomersProfilePage());
        }
    }
}
