using PriceComparerApp.ApiServices;
using PriceComparerApp.Behaviors;
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
        public SignService signService;
        public Action DisplayInvalidLoginPrompt;
        public Action DisplaySuccessLoginPrompt; //test
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

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
                Preferences.Set("login", login);
                Preferences.Set("password", password);
                Preferences.Set("token", response.token);
                DisplaySuccessLoginPrompt();
            }
        }
         
    }
}
