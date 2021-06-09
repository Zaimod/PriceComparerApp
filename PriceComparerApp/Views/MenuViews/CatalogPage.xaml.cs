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

        /// <summary>
        /// Initializes a new instance of the <see cref="CatalogPage"/> class.
        /// </summary>
        public CatalogPage()
        {
            InitializeComponent();
            catalogViewModel = new CatalogViewModel() { Navigation = this.Navigation };
            BindingContext = catalogViewModel;

        }

        /// <summary>
        /// When overridden, allows application developers to customize behavior immediately prior to the <see cref="T:Xamarin.Forms.Page" /> becoming visible.
        /// </summary>
        /// <remarks>
        /// To be added.
        /// </remarks>
        protected override async void OnAppearing()
        {
            await catalogViewModel.GetItems();
            base.OnAppearing();
        }

        /// <summary>
        /// Handles the TextChanged event of the searchBar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="TextChangedEventArgs"/> instance containing the event data.</param>
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

        /// <summary>
        /// Handles the Clicked event of the ClickSort control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ClickSort_Clicked(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the Clicked event of the Logout control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void Logout_Clicked(object sender, EventArgs e)
        {
            Preferences.Set("token", "");
            await Navigation.PushAsync(new SignInPage(), true);
        }

        /// <summary>
        /// Handles the Clicked event of the Settings control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Settings_Clicked(object sender, EventArgs e)
        {

        }
    }
}