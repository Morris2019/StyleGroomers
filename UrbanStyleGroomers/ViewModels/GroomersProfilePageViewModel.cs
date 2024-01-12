using System;
using System.Collections.ObjectModel;
using UrbanStyleGroomers.Model;
using Xamarin.Forms;

namespace UrbanStyleGroomers.ViewModels
{
    public class GroomersProfilePageViewModel : BindableObject
    {
        ObservableCollection<GroomersProfileModel> ProfileList;
        public GroomersProfilePageViewModel()
        {
            LoadMenu();
        }

        public ObservableCollection<GroomersProfileModel> profileList
        {
            get { return ProfileList; }
            set
            {
                ProfileList = value;
                OnPropertyChanged();
            }
        }

        void LoadMenu()
        {
            profileList = new ObservableCollection<GroomersProfileModel>() {
                
               
            };


        }
    }
}
