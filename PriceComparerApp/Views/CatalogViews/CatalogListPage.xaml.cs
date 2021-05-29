using Android.Webkit;
using PriceComparerApp.ViewModels.CatalogViewModels;
using PriceComparerApp.Views.SignViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PriceComparerApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CatalogListPage : ContentPage
    {
        CatalogListViewModel catalogListViewModel;
        public CatalogListPage()
        {
            InitializeComponent();
            catalogListViewModel = new CatalogListViewModel() { Navigation = this.Navigation };
            BindingContext = catalogListViewModel;
        }

        protected override async void OnAppearing()
        {
            await catalogListViewModel.GetItems();
            base.OnAppearing();
        }

        private async void ClickLogout_Clicked(object sender, EventArgs e)
        {
            Preferences.Set("token", "");
            Navigation.InsertPageBefore(new SignInPage(), this);
            await Navigation.PopAsync();
        }
    }
}