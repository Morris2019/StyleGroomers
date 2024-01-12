using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UrbanStyleGroomers.Model;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace UrbanStyleGroomers.ViewPages
{
    public partial class GroomersRegisterPage : ContentPage
    {
        private readonly HttpClient usersclient = new HttpClient();
        private ObservableCollection<UrbanUsers> UsersdataDetail;

        public string UrbanuserMail { get; set; }
        public string UserID { get; set; }
        public string UserEmail { get; set; }
        public string UsersMobbile { get; set; }
        public string messages { get; set; }
        public string messageTitle { get; set; }
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

        public GroomersRegisterPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        async void SignUpUsers(object sender, EventArgs e)
        {
            var current = Connectivity.NetworkAccess;

            if (string.IsNullOrEmpty(UrbanUsername.Text))
            {
                messages = "Please enter your name to proceed";

                messageTitle = "Entry Field Error";
                await Navigation.ShowPopupAsync(new ErrorMessages(messages, messageTitle));
            }
            else if (string.IsNullOrWhiteSpace(UrbanUsermail.Text))
            {
                messages = "Please enter your email address to proceed";

                messageTitle = "Entry Field Error";
                await Navigation.ShowPopupAsync(new ErrorMessages(messages, messageTitle));
            }
            else if (!myMultiValidation.IsValid)
            {
                messages = "Please enter valid email address to proceed";

                messageTitle = "Email validation Error";
                await Navigation.ShowPopupAsync(new ErrorMessages(messages, messageTitle));
            }
            else if (string.IsNullOrWhiteSpace(UrbanUserPhoneNumber.Text))
            {
                messages = "Please enter your phone number to proceed";

                messageTitle = "Entry Field Error";
                await Navigation.ShowPopupAsync(new ErrorMessages(messages, messageTitle));
            }
            else if (string.IsNullOrWhiteSpace(UrbanUserPassword.Text))
            {
                messages = "Please enter your password to proceed";

                messageTitle = "Entry Field Error";
                await Navigation.ShowPopupAsync(new ErrorMessages(messages, messageTitle));
            }
            else if (string.IsNullOrWhiteSpace(UrbanPassconfirm.Text))
            {
                messages = "Please enter your password again to confirm";

                messageTitle = "Entry Field Error";
                await Navigation.ShowPopupAsync(new ErrorMessages(messages, messageTitle));
            }
            else if (UrbanPassconfirm.Text != UrbanUserPassword.Text)
            {
                messages = "Passwords do not match, please try again";

                messageTitle = "Entry Field Error";
                await Navigation.ShowPopupAsync(new ErrorMessages(messages, messageTitle));
            }
            else
            {
                RegIndicator.IsRunning = true;
                string phoneNumberVerify = "http://20.87.12.160/urbanstyle/accounts/" + UrbanUserPhoneNumber;

                var content = await usersclient.GetStringAsync(phoneNumberVerify);
                var deserializedPosts = JsonConvert.DeserializeObject<List<UrbanUsers>>(content);
                UsersdataDetail = new ObservableCollection<UrbanUsers>(deserializedPosts);

                if (UsersdataDetail.Count > 1)
                {
                    messages = "Account already exist";
                    messageTitle = "Account Creation";
                    await Navigation.ShowPopupAsync(new ErrorMessages(messages, messageTitle));
                    UrbanUsername.IsReadOnly = false;
                    UrbanUsermail.IsReadOnly = false;
                    UrbanUserPhoneNumber.IsReadOnly = false;
                    UrbanUserPassword.IsReadOnly = false;
                    RegIndicator.IsRunning = false;
                }
                else
                {
                   await UrbanSignUp();
                }

            }
        }
        async Task UrbanSignUp()
        {
            UrbanUsername.IsReadOnly = true;
            UrbanUsermail.IsReadOnly = true;
            UrbanUserPhoneNumber.IsReadOnly = true;
            UrbanUserPassword.IsReadOnly = true;

            RegIndicator.IsRunning = true;

            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.Internet)
            {
                try
                {
                    var login = new UsersInsert
                    {
                        name = UrbanUsername.Text,
                        email = UrbanUsermail.Text,
                        mobile = UrbanUserPhoneNumber.Text,
                        password = UrbanUserPassword.Text
                    };

                    string UrbanUrl = "http://20.87.12.160/urbanstyle/accounts/";
                    HttpClient _client = new HttpClient();
                    string serializedLogin = JsonConvert.SerializeObject(login);
                    StringContent signcontent = new StringContent(serializedLogin, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await _client.PostAsync(UrbanUrl, signcontent);
                    string result = await response.Content.ReadAsStringAsync();
                    ResponseModel responsemodel = JsonConvert.DeserializeObject<ResponseModel>(result);

                    if (responsemodel.success == true)
                    {
                        SecureStorage.RemoveAll();

                        await SecureStorage.SetAsync("UserEmail", UrbanUsermail.Text);
                        await SecureStorage.SetAsync("UserFullname", UrbanUsername.Text);
                        await SecureStorage.SetAsync("UrbanUserPassword", UrbanUserPassword.Text);
                        await SecureStorage.SetAsync("Phonenumber", UrbanUserPhoneNumber.Text);


                        Application.Current.MainPage = new NavigationPage(new MainGroomersPage());
                        RegIndicator.IsRunning = false;
                    }
                    else
                    {
                        UrbanUsername.IsReadOnly = false;
                        UrbanUsermail.IsReadOnly = false;
                        UrbanUserPhoneNumber.IsReadOnly = false;
                        UrbanUserPassword.IsReadOnly = false;

                        messages = "Please check your credentials and try again";

                        messageTitle = "Account Creation";
                        await Navigation.ShowPopupAsync(new ErrorMessages(responsemodel.message, messageTitle));
                        RegIndicator.IsRunning = false;
                    }
                }

                catch (Exception ex)
                {
                    UrbanUsername.IsReadOnly = false;
                    UrbanUsermail.IsReadOnly = false;
                    UrbanUserPhoneNumber.IsReadOnly = false;
                    UrbanUserPassword.IsReadOnly = false;

                    messages = "Please check your credentials and try again";

                    await Navigation.ShowPopupAsync(new ErrorMessages(messages, messageTitle));
                    RegIndicator.IsRunning = false;
                }
            }
            else
            {
                await DisplayAlert("Network", "Please check your internet and try again", "Ok");
            }
        }
        async void SignInPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GroomerLoginPage());
        }
    }
}