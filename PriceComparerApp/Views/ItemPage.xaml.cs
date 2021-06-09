using PriceComparerApp.Models.DataTransferObjects;
using PriceComparerApp.ViewModels.CatalogViewModel;
using Rg.Plugins.Popup.Extensions;
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
    public partial class ItemPage : ContentPage
    {
        ItemViewModel itemViewModel;
        ProductDto selectedItem;

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemPage"/> class.
        /// </summary>
        /// <param name="_selectedItem">The selected item.</param>
        public ItemPage(ProductDto _selectedItem)
        {
            InitializeComponent();
            selectedItem = _selectedItem;
            itemViewModel = new ItemViewModel() { Navigation = this.Navigation, ProductId = selectedItem.Id };
            BindingContext = itemViewModel;
            Subscribe();
        }

        /// <summary>
        /// When overridden, allows application developers to customize behavior immediately prior to the <see cref="T:Xamarin.Forms.Page" /> becoming visible.
        /// </summary>
        /// <remarks>
        /// To be added.
        /// </remarks>
        protected override async void OnAppearing()
        {
            await itemViewModel.GetItems();
            base.OnAppearing();
        }

        /// <summary>
        /// Subscribes this instance.
        /// </summary>
        private void Subscribe()
        {
            MessagingCenter.Subscribe<App, string>(App.Current, "SortItemsByFilter", (snd, arg) =>
            {
                Sorting(arg);
            });
        }

        /// <summary>
        /// Sortings the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        private void Sorting(string type = null)
        {
            string filterText = type;

            if (filterText != "" || filterText != null)
            {
                var _container = BindingContext as ItemViewModel;
                homeList.BeginRefresh();

                if (filterText == "byAscending")
                    homeList.ItemsSource = _container.items.OrderBy(t => t.Name);
                else if (filterText == "byDescending")
                    homeList.ItemsSource = _container.items.OrderByDescending(t => t.Name);
                else if (filterText == "byPopularityDesc")
                    homeList.ItemsSource = _container.items.OrderByDescending(t => t.NumbReviews);
                else if (filterText == "byPopularityAsc")
                    homeList.ItemsSource = _container.items.OrderBy(t => t.NumbReviews);
                else if (filterText == "byPriceDesc")
                    homeList.ItemsSource = _container.items.OrderByDescending(t => t.NewPrice);
                else if (filterText == "byPriceAsc")
                    homeList.ItemsSource = _container.items.OrderBy(t => t.NewPrice);

                homeList.EndRefresh();
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the searchBar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="TextChangedEventArgs"/> instance containing the event data.</param>
        private void searchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var _container = BindingContext as ItemViewModel;
            homeList.BeginRefresh();

            if (string.IsNullOrWhiteSpace(e.NewTextValue))
                homeList.ItemsSource = _container.items;
            else
                homeList.ItemsSource = _container.items.Where(i => i.Name.IndexOf(e.NewTextValue, StringComparison.OrdinalIgnoreCase) >= 0);

            homeList.EndRefresh();
        }

        private async void homeList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Handles the Clicked event of the ClickSort control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ClickSort_Clicked(object sender, EventArgs e)
        {
            Navigation.PushPopupAsync(new SortItemPage());
        }
    }
}