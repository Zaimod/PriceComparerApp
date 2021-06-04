using PriceComparerApp.ApiServices;
using PriceComparerApp.Behaviors;
using PriceComparerApp.Models;
using PriceComparerApp.Services;
using PriceComparerApp.ViewModels.HomeViewModels;
using PriceComparerApp.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PriceComparerApp.ViewModels.SignViewModels
{
    public class SignInViewModel : INotifyPropertyChanged
    {
        private readonly IMessageService messageService;
        public SignService signService;
        public Action DisplayInvalidLoginPrompt;
        public Action DisplayInvalidCheckEmailPrompt;
        public Action DisplaySuccessLoginPrompt; //test
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        NavigationPage catalogListpage;

        private string login;
        public string Login
        {
            get { return login; }
            set
            {
                login = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Login"));
            }
        }
        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Password"));
            }
        }
        public ICommand SubmitCommand { set; get; }
        public SignInViewModel()
        {
            this.messageService = DependencyService.Get<IMessageService>();
            signService = new SignService();
            SubmitCommand = new Command(OnSubmit);
        }
        public async void OnSubmit()
        {
            var response = await signService.SignIn(login, password);
            if (string.IsNullOrEmpty(response.token))
            {            
                DisplayInvalidLoginPrompt();
            }
            else
            {
                if (response.emailConfirmed)
                {
                    GetCatalogListPage(response);
                }
                else
                {
                    var responseCode = await signService.SendVerificationCode(login);
                    string resultPopUpCode =  await messageService.ShowAsync();
                    
                    if (resultPopUpCode != null)
                    {
                        var responseChangeEmailConfirmed = await signService.ChangeEmailConfirmed(resultPopUpCode, login);
                        if (responseCode && responseChangeEmailConfirmed)
                        {
                            GetCatalogListPage(response);
                        }
                        else
                            DisplayInvalidCheckEmailPrompt();
                    }
                }             
            }
        }
        
        public void GetCatalogListPage(TokenResponse response)
        {
            catalogListpage = new NavigationPage(new MenuPage());
            Preferences.Set("login", login);
            Preferences.Set("password", password);
            Preferences.Set("token", response.token);
            Application.Current.MainPage = catalogListpage;
        }
    }
}
