using PriceComparerApp.ApiServices;
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
        public Action DisplayInvalidLoginPrompt;
        public Action DisplaySuccessLoginPrompt; //test
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

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

        public ICommand SubmitCommand { set; get; }

        public SignUpViewModel()
        {
            signService = new SignService();
            SubmitCommand = new Command(OnSubmit);
        }
        public void OnSubmit()
        {
             
        }
    }
}
