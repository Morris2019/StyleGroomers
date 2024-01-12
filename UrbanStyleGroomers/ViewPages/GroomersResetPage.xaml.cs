using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace UrbanStyleGroomers.ViewPages
{
    public partial class GroomersResetPage : ContentPage
    {
        public GroomersResetPage()
        {
            InitializeComponent();
        }
        private async void PassReset(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GroomerLoginPage());
        }
        private async void UserRegister(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GroomersRegisterPage());
        }
    }
}
