using Android.App;
using Android.Content;
using PriceComparerApp.Helpers;
using PriceComparerApp.Services;
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
    public partial class SignInPage : ContentPage
    {
        SignInViewModel vm = new SignInViewModel();
        private readonly GoogleViewModel _googleViewModel;
        public SignInPage()
        {         
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();

            _googleViewModel = BindingContext as GoogleViewModel;
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
            if (Login.TextColor.R == 1 || Password.TextColor.R == 1)
            {
                if (Login.Text == null || Password.Text == null)
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

        private void TapSignGoogle_Tapped(object sender, EventArgs e)
        {          
          
            var authRequest =
                  "https://accounts.google.com/o/oauth2/v2/auth"
                  + "?response_type=code"
                  + "&scope=openid"
                  + "&redirect_uri=" + GoogleServices.RedirectUri
                  + "&client_id=" + GoogleServices.ClientId;

            var webView = new WebView
            {
                Source = authRequest,
                HeightRequest = 1
            };

            webView.Navigated += WebViewOnNavigated;

            Content = webView;
        }

        private async void WebViewOnNavigated(object sender, WebNavigatedEventArgs e)
        {

            var code = ExtractCodeFromUrl(e.Url);

            if (code != "")
            {

                var accessToken = await _googleViewModel.GetAccessTokenAsync(code);

                await _googleViewModel.SetGoogleUserProfileAsync(accessToken);

            }
        }

        private string ExtractCodeFromUrl(string url)
        {
            if (url.Contains("code="))
            {
                var attributes = url.Split('&');

                var code = attributes.FirstOrDefault(s => s.Contains("code=")).Split('=')[1];

                return code;
            }

            return string.Empty;
        }
    }
}