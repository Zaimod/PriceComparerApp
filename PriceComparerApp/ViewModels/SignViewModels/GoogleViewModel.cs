using PriceComparerApp.Models;
using PriceComparerApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PriceComparerApp.ViewModels.SignViewModels
{
    public class GoogleViewModel : INotifyPropertyChanged
    {
        private GoogleProfile _googleProfile;
        private readonly GoogleServices _googleServices;

        /// <summary>
        /// Gets or sets the google profile.
        /// </summary>
        /// <value>
        /// The google profile.
        /// </value>
        public GoogleProfile GoogleProfile
        {
            get { return _googleProfile; }
            set
            {
                _googleProfile = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GoogleViewModel"/> class.
        /// </summary>
        public GoogleViewModel()
        {
            _googleServices = new GoogleServices();
        }

        /// <summary>
        /// Gets the access token asynchronous.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        public async Task<string> GetAccessTokenAsync(string code)
        {

            var accessToken = await _googleServices.GetAccessTokenAsync(code);

            return accessToken;
        }

        /// <summary>
        /// Sets the google user profile asynchronous.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        public async Task SetGoogleUserProfileAsync(string accessToken)
        {

            GoogleProfile = await _googleServices.GetGoogleUserProfileAsync(accessToken);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
