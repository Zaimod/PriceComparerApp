using PriceComparerApp.ApiServices;
using PriceComparerApp.Models.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PriceComparerApp.ViewModels
{
    public class CabinetPageViewModel : INotifyPropertyChanged
    {
        private bool initialized = false;

        public event PropertyChangedEventHandler PropertyChanged;

        UserService userService = new UserService();

        public UserDto items { get; set; }

        public ICommand BackCommand { protected set; get; }
        public INavigation Navigation { get; set; }

        public CabinetPageViewModel()
        {
            items = new UserDto();
            BackCommand = new Command(Back);
        }

        private bool isBusy;
        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;
                OnPropertyChanged("IsBusy");
                OnPropertyChanged("IsLoaded");
            }
        }

        private bool _isRefreshing = false;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }

        public bool IsLoaded
        {
            get { return !isBusy; }
        }

        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        private void Back()
        {
            Navigation.PopAsync();
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsRefreshing = true;

                    await GetUser(true);

                    IsRefreshing = false;
                });
            }
        }

        public async Task GetUser(bool refresh = false)
        {       
            IsBusy = true;

            UserDto userDto = await userService.GetUser(Preferences.Get("login", "").ToString());

            items = userDto;

            IsBusy = false;
            initialized = true;
        }

        public async Task SaveUserInformation(UserDto items)
        {
            await userService.UpdateUser(items);
        }
    }
}
