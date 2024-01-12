using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Refit;
using UrbanStyleGroomers.Model;
using UrbanStyleGroomers.ModelsServer;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace UrbanStyleGroomers.ViewPages
{
    public partial class GroomerLoginPage : ContentPage
    {
        public string UserId { get; set; }
        public string UserSEmail { get; set; }
        public string UserPhone { get; set; }
        public string UserSPassword { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
        private ObservableCollection<UrbanUsers> UsersdataDetail;
        private ObservableCollection<ResponseModel> userLogin;
        private bool emailValid;

        public string messages { get; set; }
        public string messageTitle { get; set; }
        public bool EmailValid
        {
            get => emailValid;
            set
            {
                emailValid = value;
                OnPropertyChanged();
            }
        }

        public GroomerLoginPage()
        {
            InitializeComponent();
            BindingContext = this;
        }
        private async void forgotPassword(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GroomersResetPage());
        }
        public async void LoginClick(object sender, EventArgs e)
        {
            var current = Connectivity.NetworkAccess;

            if (string.IsNullOrWhiteSpace(UrbanUserTell.Text))
            {
                messages = "Please enter your email address to proceed";
                messageTitle = "Entry Field Error";
                await Navigation.ShowPopupAsync(new ErrorMessages(messages, messageTitle));
             
            }
            else if (!myMultiValidation.IsValid)
            {
                messages = "Please enter valid email address to proceed";

                messageTitle = "Email Validation Error";
                await Navigation.ShowPopupAsync(new ErrorMessages(messages, messageTitle));
            }
            else if (string.IsNullOrEmpty(UserPassword.Text))
            {
                messages = "Please enter your password to proceed";

                messageTitle = "Entry Field Error";
                await Navigation.ShowPopupAsync(new ErrorMessages(messages, messageTitle));
            }
            else
            {
                //List<string> Response = new List<string>();
                if (current == NetworkAccess.Internet)
                {
                    UrbanUserTell.IsReadOnly = true;
                    UserPassword.IsReadOnly = true;
                    try
                    {
                        RegIndicator.IsRunning = true;

                        var login = new UsersInsert
                        {
                            email = UrbanUserTell.Text,
                            password = UserPassword.Text
                        };
                        string UrbanUrl = "http://20.87.12.160/urbanstyle/accounts/authenticate";
                        HttpClient _client = new HttpClient();
                        string serializedLogin = JsonConvert.SerializeObject(login);
                        StringContent content = new StringContent(serializedLogin, Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await _client.PostAsync(UrbanUrl, content);
                        string result = await response.Content.ReadAsStringAsync();
                        ResponseModel responsemodel = JsonConvert.DeserializeObject<ResponseModel>(result);

                        if (responsemodel.success == true)
                        {

                            //  await SecureStorage.SetAsync("UrbanUserID", UserID);
                            // await SecureStorage.SetAsync("UserEmail", UserSEmail);
                            // await SecureStorage.SetAsync("UrbanFullname", UserName);
                            await SecureStorage.SetAsync("Phonenumber", UrbanUserTell.Text);
                            await SecureStorage.SetAsync("UrbanUserPassword", UserPassword.Text);

                            UrbanUserTell.IsReadOnly = false;
                            UserPassword.IsReadOnly = false;

                            Application.Current.MainPage = new NavigationPage(new MainGroomersPage());
                            RegIndicator.IsRunning = false;

                        }
                        else
                        {
                            UrbanUserTell.IsReadOnly = false;
                            UserPassword.IsReadOnly = false;

                            messages = "Please check your credentials and try again";

                            messageTitle = "Signin Error";
                            await Navigation.ShowPopupAsync(new ErrorMessages(messages, messageTitle));

                            RegIndicator.IsRunning = false;

                        }
                     }
                    catch (Exception ex)
                    {
                        UrbanUserTell.IsReadOnly = false;
                        UserPassword.IsReadOnly = false;

                        messages = "Please check your credentials and try again";

                        messageTitle = "Signin Error";
                        await Navigation.ShowPopupAsync(new ErrorMessages(messages, messageTitle));

                        RegIndicator.IsRunning = false;
                    }
                }
                
            }
        }
        public async void UserCreateAccountTap(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GroomersRegisterPage());
        }

    }
}
