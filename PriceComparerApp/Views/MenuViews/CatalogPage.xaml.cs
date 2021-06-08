using PriceComparerApp.ViewModels.CatalogViewModel;
using PriceComparerApp.Views.SignViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PriceComparerApp.Views.MenuViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CatalogPage : ContentPage
    {
        CatalogViewModel catalogViewModel;
        public CatalogPage()
        {
            InitializeComponent();
            catalogViewModel = new CatalogViewModel() { Navigation = this.Navigation };
            BindingContext = catalogViewModel;

        }

        protected override async void OnAppearing()
        {
            await catalogViewModel.GetItems();
            base.OnAppearing();
        }

        private void searchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var _container = BindingContext as CatalogViewModel;
            collectView.BatchBegin();

            if (string.IsNullOrWhiteSpace(e.NewTextValue))
                collectView.ItemsSource = _container.items;
            else
                collectView.ItemsSource = _container.items.Where(i => i.Name.IndexOf(e.NewTextValue, StringComparison.OrdinalIgnoreCase) >= 0);

            collectView.BatchCommit();
        }

        private void ClickSort_Clicked(object sender, EventArgs e)
        {

        }

        private async void Logout_Clicked(object sender, EventArgs e)
        {
            Preferences.Set("token", "");
            await Navigation.PushAsync(new SignInPage(), true);
        }

        private void Settings_Clicked(object sender, EventArgs e)
        {

        }
    }
}