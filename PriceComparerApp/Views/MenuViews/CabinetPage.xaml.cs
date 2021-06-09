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

        /// <summary>
        /// Initializes a new instance of the <see cref="CabinetPage"/> class.
        /// </summary>
        public CabinetPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            cabinetPageViewModel = new CabinetPageViewModel() { Navigation = this.Navigation };
            Subscribe();
        }

        /// <summary>
        /// When overridden, allows application developers to customize behavior immediately prior to the <see cref="T:Xamarin.Forms.Page" /> becoming visible.
        /// </summary>
        /// <remarks>
        /// To be added.
        /// </remarks>
        protected override async void OnAppearing()
        {
            
            await cabinetPageViewModel.GetUser();
            base.OnAppearing();
            Initialize();
        }

        /// <summary>
        /// Subscribes this instance.
        /// </summary>
        private void Subscribe()
        {
            MessagingCenter.Subscribe<App, string>(App.Current, "SaveNewInformation",  (snd, arg) =>
            {
                OnAppearing();
            });
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
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

        /// <summary>
        /// Handles the Clicked event of the Logout control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void Logout_Clicked(object sender, EventArgs e)
        {
            Preferences.Set("token", "");
            await Navigation.PushAsync(new SignInPage(), true);
        }

        /// <summary>
        /// Handles the Clicked event of the Settings control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Settings_Clicked(object sender, EventArgs e)
        {
            Navigation.PushPopupAsync(new SettingsPage(cabinetPageViewModel.items));
        }
    }
}