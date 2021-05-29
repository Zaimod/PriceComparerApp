﻿using PriceComparerApp.ViewModels.SignViewModels;
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
    public partial class SignInPage : ContentPage
    {
        public SignInPage()
        {         
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();

            SignInViewModel vm = new SignInViewModel();
            BindingContext = vm;
            vm.DisplayInvalidLoginPrompt += () => DisplayAlert("Error", "Invalid Login, try again", "OK");
            vm.DisplaySuccessLoginPrompt += () => DisplayAlert("Success", "You autheticated!", "OK");
            vm.DisplayInvalidCheckEmailPrompt += () => DisplayAlert("Error", "Wrong verification code", "OK");

            Login.Completed += (object sender, EventArgs e) =>
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

        private void TapSignUp_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SignUpPage());
        }

        private void ClickSignIn_Clicked(object sender_click, EventArgs e_click)
        {
            
        }

        
    }
}