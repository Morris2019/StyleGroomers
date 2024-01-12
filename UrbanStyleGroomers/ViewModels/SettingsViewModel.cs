using System;
using System.Collections.ObjectModel;
using UrbanStyleGroomers.Model;
using Xamarin.Forms;

namespace UrbanStyleGroomers.ViewModels
{
    public class SettingsViewModel : BindableObject
    {
        private ObservableCollection<MenuListModel> MenuList;
        public SettingsViewModel()
        {
            LoadAccounts();
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

        void LoadAccounts()
        {
            menulist = new ObservableCollection<MenuListModel>() {
                 new MenuListModel
                 {
                     Name = "Edit my account information ",
                     MenuId=1
                 },
                 new MenuListModel
                 {
                     Name = "Why is my account blocked",
                     MenuId=2
                 },
                 new MenuListModel
                 {
                     Name = "Recieving unwanted marketing messages",
                     MenuId=3
                 },
                 new MenuListModel
                 {
                     Name = "Delete my account",
                     MenuId=4
                 },
            };

        }
    }

}