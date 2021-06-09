using PriceComparerApp.Models.DataTransferObjects;
using PriceComparerApp.ViewModels;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PriceComparerApp.Views.MenuViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        CabinetPageViewModel cabinetPageViewModel;
        UserDto dtoUser;

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsPage"/> class.
        /// </summary>
        /// <param name="user">The user.</param>
        public SettingsPage(UserDto user)
        {
            cabinetPageViewModel = new CabinetPageViewModel();
            dtoUser = new UserDto();
            InitializeComponent();
            Initialize(user);
            dtoUser.Email = user.Email;
        }

        /// <summary>
        /// Initializes the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        private void Initialize(UserDto user)
        {
            entryUserName.Text = user.UserName;
            entryFirstName.Text = user.FirstName;
            entryLastName.Text =  user.LastName;
            entryPhone.Text = user.PhoneNumber;
            if (user.Birthday != null)
            {
                dateBirth.Date = user.Birthday.Value;
            }
        }

        /// <summary>
        /// Handles the Clicked event of the Button control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void Button_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(App.Current as App, "SaveNewInformation", "_saving");
            InitializeDtoForSaving();
            await cabinetPageViewModel.SaveUserInformation(dtoUser);
            await PopupNavigation.Instance.PopAsync();
        }

        /// <summary>
        /// Initializes the dto for saving.
        /// </summary>
        private void InitializeDtoForSaving()
        {
            dtoUser.UserName = entryUserName.Text;
            dtoUser.FirstName = entryFirstName.Text;
            dtoUser.LastName = entryLastName.Text;
            dtoUser.Birthday = dateBirth.Date;
            dtoUser.PhoneNumber = entryPhone.Text;
            dtoUser.Sex = "";
            dtoUser.City = "";
        }
    }
}