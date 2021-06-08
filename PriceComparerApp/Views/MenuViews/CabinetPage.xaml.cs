using Android.Views;
using PriceComparerApp.ViewModels;
using PriceComparerApp.Views.SignViews;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PriceComparerApp.Views.MenuViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CabinetPage : ContentPage
    {
        CabinetPageViewModel cabinetPageViewModel;
        public CabinetPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            cabinetPageViewModel = new CabinetPageViewModel() { Navigation = this.Navigation };
            Subscribe();
        }

        protected override async void OnAppearing()
        {
            
            await cabinetPageViewModel.GetUser();
            base.OnAppearing();
            Initialize();
        }

        private void Subscribe()
        {
            MessagingCenter.Subscribe<App, string>(App.Current, "SaveNewInformation",  (snd, arg) =>
            {
                OnAppearing();
            });
        }

        private void Initialize()
        {
            entryUserName.Text = cabinetPageViewModel.items.UserName;
            entryFullName.Text = $"{cabinetPageViewModel.items.FirstName} {cabinetPageViewModel.items.LastName}";
            entryPhone.Text = cabinetPageViewModel.items.PhoneNumber;
            entryEmail.Text = cabinetPageViewModel.items.Email;
            if(cabinetPageViewModel.items.Birthday != null)
            {
                dateBirth.Date = cabinetPageViewModel.items.Birthday.Value;
            }
        }

        private async void Logout_Clicked(object sender, EventArgs e)
        {
            Preferences.Set("token", "");
            await Navigation.PushAsync(new SignInPage(), true);
        }

        private void Settings_Clicked(object sender, EventArgs e)
        {
            Navigation.PushPopupAsync(new SettingsPage(cabinetPageViewModel.items));
        }
    }
}