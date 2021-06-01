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
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace PriceComparerApp.Views.MenuViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CatalogListPage : ContentPage
    {
        CatalogListViewModel catalogListViewModel;
        //bool isLoading;
        //Page page;
        Xamarin.Forms.ListView listview = new Xamarin.Forms.ListView();
        public CatalogListPage()
        {
            InitializeComponent();
            catalogListViewModel = new CatalogListViewModel() { Navigation = this.Navigation };
            BindingContext = catalogListViewModel;
        }

        protected override async void OnAppearing()
        {
            await catalogListViewModel.GetItems();
            //catalogList.ItemAppearing += (sender, e) =>
            //{
            //    if (isLoading || catalogListViewModel.items.Count == 0)
            //        return;

            //    if(e.Item.ToString() == catalogListViewModel.items[catalogListViewModel.items.Count - 1].ToString())
            //    {
            //        LoadItems();
            //    }
            //};

            //page = new ContentPage
            //{
            //    Content = new StackLayout
            //    {
            //        VerticalOptions = LayoutOptions.Center,
            //        Children = {
            //            listview
            //        }
            //    }
            //};

            //LoadItems();
            base.OnAppearing();
        }

        //private async Task LoadItems()
        //{
        //    isLoading = true;
        //    page.Title = "Loading";

        //    //simulator delayed load
        //    Device.StartTimer(TimeSpan.FromSeconds(2), () => {
        //        for (int i = 0; i < 20; i++)
        //        {
                     
        //        }
        //        page.Title = "Done";
        //        isLoading = false;
        //        //stop timer
        //        return false;
        //    });
        //}

        private async void ClickLogout_Clicked(object sender, EventArgs e)
        {
            Preferences.Set("token", "");
            await Navigation.PushAsync(new SignInPage(), true);
        }

        private void ClickFilterList_Clicked(object sender, EventArgs e)
        {

        }

        private void ClickSort_Clicked(object sender, EventArgs e)
        {

        }

        private void searchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var _container = BindingContext as CatalogListViewModel;
            catalogList.BeginRefresh();

            if (string.IsNullOrWhiteSpace(e.NewTextValue))
                catalogList.ItemsSource = _container.items;
            else
                catalogList.ItemsSource = _container.items.Where(i => i.title.IndexOf(e.NewTextValue, StringComparison.OrdinalIgnoreCase) >= 0);

            catalogList.EndRefresh();
        }

        private void catalogList_Refreshing(object sender, EventArgs e)
        {
            var _container = BindingContext as CatalogListViewModel;
            catalogList.BeginRefresh();

            catalogList.ItemsSource = _container.items;

            catalogList.EndRefresh();
        }

        private async void catalogList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            
        }
    }
}