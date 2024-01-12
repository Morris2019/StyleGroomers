using System;
using System.Collections.Generic;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace UrbanStyleGroomers.ViewPages
{
    public partial class ErrorMessages : Popup
    {
        public ErrorMessages(string message, string messageTitle)
        {
            InitializeComponent();
            ErrorMessage.Text = message;
            ErrorTitle.Text = messageTitle;
        }

        async void ReturnEroorMsg(object sender, EventArgs e)
        {
            Dismiss("OK");
        }
    }
}
