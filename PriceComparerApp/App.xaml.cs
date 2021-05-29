using PriceComparerApp.Views;
using PriceComparerApp.Views.SignViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PriceComparerApp
{
    public partial class App : Application
    {
        public App()
        {
            DependencyService.Register<Services.IMessageService, Services.MessageService>();
            InitializeComponent();
            if(Preferences.Get("token", "").ToString().Equals(""))
                MainPage = new NavigationPage(new SignInPage());
            else
                MainPage = new NavigationPage(new CatalogListPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
