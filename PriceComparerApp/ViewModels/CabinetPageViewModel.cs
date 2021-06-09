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

        /// <summary>
        /// Initializes a new instance of the <see cref="CabinetPageViewModel"/> class.
        /// </summary>
        public CabinetPageViewModel()
        {
            items = new UserDto();
            BackCommand = new Command(Back);
        }

        private bool isBusy;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is busy.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is busy; otherwise, <c>false</c>.
        /// </value>
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

        /// <summary>
        /// Gets or sets a value indicating whether this instance is refreshing.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is refreshing; otherwise, <c>false</c>.
        /// </value>
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is loaded.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is loaded; otherwise, <c>false</c>.
        /// </value>
        public bool IsLoaded
        {
            get { return !isBusy; }
        }

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="propName">Name of the property.</param>
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        /// <summary>
        /// Backs this instance.
        /// </summary>
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

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="refresh">if set to <c>true</c> [refresh].</param>
        public async Task GetUser(bool refresh = false)
        {       
            IsBusy = true;

            UserDto userDto = await userService.GetUser(Preferences.Get("login", "").ToString());

            items = userDto;

            IsBusy = false;
            initialized = true;
        }

        /// <summary>
        /// Saves the user information.
        /// </summary>
        /// <param name="items">The items.</param>
        public async Task SaveUserInformation(UserDto items)
        {
            await userService.UpdateUser(items);
        }
    }
}
