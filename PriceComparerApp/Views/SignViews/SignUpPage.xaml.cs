using PriceComparerApp.ViewModels.SignViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PriceComparerApp.Views.SignViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage : ContentPage
    {
        SignUpViewModel vm = new SignUpViewModel();
        public SignUpPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            
            BindingContext = vm;
            vm.DisplayInvalidRegisterPrompt += () => DisplayAlert("Error", "Invalid Register, try again", "OK");
            vm.DisplaySuccessRegisterPrompt += () => DisplayAlert("Success", "You registrated!", "OK");
            vm.DisplayInvalidCheckEmailPrompt += () => DisplayAlert("Error", "Wrong verification code", "OK");

            FirstName.Completed += (object sender, EventArgs e) =>
            {  
                LastName.Focus();       
            };

            LastName.Completed += (object sender, EventArgs e) =>
            {
                UserName.Focus();
            };

            UserName.Completed += (object sender, EventArgs e) =>
            {
                Email.Focus();
            };

            Email.Completed += (object sender, EventArgs e) =>
            {
                Password.Focus();
            };
            Password.Completed += (object sender, EventArgs e) =>
            {
                vm.SubmitCommand.Execute(null);
            };
        }
        private async void OnCloseButtonClicked(object sender, EventArgs args)
        {
            await Navigation.PopModalAsync();
        }

        private void ClickSignUp_Clicked(object sender_click, EventArgs e_click)
        {
            if (FirstName.TextColor.R == 1 || LastName.TextColor.R == 1 || UserName.TextColor.R == 1 ||
                Email.TextColor.R == 1 || Password.TextColor.R == 1 || ConfirmPassword.TextColor.R == 1)
            {
                if (FirstName.Text == null || LastName.Text == null || UserName.Text == null ||
                    Email.Text == null || Password.Text == null || ConfirmPassword.Text == null)
                {
                    DisplayAlert("Error", "Put all forms", "OK");
                }
                else
                    DisplayAlert("Error", "Wrong, red text, please put text, again", "OK");
            }
            else
            {
                vm.OnSubmit();
            }
            
        }
    }
}