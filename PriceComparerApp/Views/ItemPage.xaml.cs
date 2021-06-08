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
        public ItemPage(ProductDto _selectedItem)
        {
            InitializeComponent();
            selectedItem = _selectedItem;
            itemViewModel = new ItemViewModel() { Navigation = this.Navigation, ProductId = selectedItem.Id };
            BindingContext = itemViewModel;
            Subscribe();
        }
        protected override async void OnAppearing()
        {
            await itemViewModel.GetItems();
            base.OnAppearing();
        }

        private void Subscribe()
        {
            MessagingCenter.Subscribe<App, string>(App.Current, "SortItemsByFilter", (snd, arg) =>
            {
                Sorting(arg);
            });
        }

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

        private void ClickSort_Clicked(object sender, EventArgs e)
        {
            Navigation.PushPopupAsync(new SortItemPage());
        }
    }
}