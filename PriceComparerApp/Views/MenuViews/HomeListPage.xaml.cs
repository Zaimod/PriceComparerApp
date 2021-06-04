using Android.Webkit;
using PriceComparerApp.ViewModels.HomeViewModels;
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
using Rg.Plugins.Popup.Extensions;

namespace PriceComparerApp.Views.MenuViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeListPage : ContentPage
    {
        HomeListViewModel homeViewModel;
        //bool isLoading;
        //Page page;
        public int categoryId = 0;

        public HomeListPage()
        {            
            InitializeComponent();
            homeViewModel = new HomeListViewModel() { Navigation = this.Navigation };
            BindingContext = homeViewModel;
            Subscribe();
        }

        protected override async void OnAppearing()
        {
            if (categoryId != 0)
                await homeViewModel.GetItems(categoryId: categoryId);
            else
                await homeViewModel.GetItems();
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

        private void Subscribe()
        {
            MessagingCenter.Subscribe<App, string>(App.Current, "SortItemsByFilter", (snd, arg) =>
            {
                Sorting(arg);
            });
        }

        private async void ClickLogout_Clicked(object sender, EventArgs e)
        {
            Preferences.Set("token", "");
            await Navigation.PushAsync(new SignInPage(), true);
        }

        private void ClickFilterList_Clicked(object sender, EventArgs e)
        {
            Navigation.PushPopupAsync(new FilterListPage());
        }

        private void ClickSort_Clicked(object sender, EventArgs e)
        {
            Navigation.PushPopupAsync(new SortPage());
        }

        private void Sorting(string type = null)
        {
            string filterText = type;

            if (filterText != "" || filterText != null)
            {
                var _container = BindingContext as HomeListViewModel;
                homeList.BeginRefresh();

                if (filterText == "byAscending")
                    homeList.ItemsSource = _container.items.OrderBy(t => t.title);
                else if (filterText == "byDescending")
                    homeList.ItemsSource = _container.items.OrderByDescending(t => t.title);
                else if(filterText == "byPopularityDesc")
                    homeList.ItemsSource = _container.items.OrderByDescending(t => t.numbReviews);
                else if (filterText == "byPopularityAsc")
                    homeList.ItemsSource = _container.items.OrderBy(t => t.numbReviews);

                homeList.EndRefresh();
            }
        }

        private void searchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var _container = BindingContext as HomeListViewModel;
            homeList.BeginRefresh();

            if (string.IsNullOrWhiteSpace(e.NewTextValue))
                homeList.ItemsSource = _container.items;
            else
                homeList.ItemsSource = _container.items.Where(i => i.title.IndexOf(e.NewTextValue, StringComparison.OrdinalIgnoreCase) >= 0);

            homeList.EndRefresh();
        }

        private void catalogList_Refreshing(object sender, EventArgs e)
        {
            var _container = BindingContext as HomeListViewModel;
            homeList.BeginRefresh();
            homeList.ItemsSource = _container.items;
            homeList.EndRefresh();
        }

        private async void catalogList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            
        }
    }
}