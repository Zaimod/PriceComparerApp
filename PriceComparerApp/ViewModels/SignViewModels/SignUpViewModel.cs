using PriceComparerApp.ApiServices;
using PriceComparerApp.Models.DataTransferObjects;
using PriceComparerApp.ViewModels.CatalogViewModels;
using PriceComparerApp.Views.SignViews;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PriceComparerApp.ViewModels.SignViewModels
{
    public class SignUpViewModel : INotifyPropertyChanged
    {
        public SignService signService;
        public Action DisplayInvalidRegisterPrompt;
        public Action DisplaySuccessRegisterPrompt; //test
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        NavigationPage signInpage;

        #region Properties
        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                PropertyChanged(this, new PropertyChangedEventArgs("FirstName"));
            }
        }

        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                PropertyChanged(this, new PropertyChangedEventArgs("LastName"));
            }
        }

        private string userName;
        public string UserName
        {
            get { return userName; }
            set
            {
                userName = value;
                PropertyChanged(this, new PropertyChangedEventArgs("UserName"));
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

        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Email"));
            }
        }
        private string confirmPassword;
        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set
            {
                confirmPassword = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ConfirmPassword"));
            }
        }
        #endregion

        public ICommand SubmitCommand { set; get; }

        public SignUpViewModel()
        {
            signService = new SignService();
            SubmitCommand = new Command(OnSubmit);
        }
        public void OnSubmit()
        {
            UserForSignUpDto dto = new UserForSignUpDto()
            {
                FirstName = FirstName,
                LastName = LastName,
                UserName = UserName,
                Email = Email,
                Password = Password,
                PhoneNumber = "",
                Roles = new List<string>() { "User" }
            };
            var response = signService.SignUp(dto).Result;
            if (response)
            {
                signInpage = new NavigationPage(new SignInPage());
                Application.Current.MainPage = signInpage;
            }
            else
            {
                DisplayInvalidRegisterPrompt();
            }
        }
    }
}
