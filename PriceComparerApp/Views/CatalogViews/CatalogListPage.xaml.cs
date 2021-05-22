using PriceComparerApp.ViewModels.CatalogViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}