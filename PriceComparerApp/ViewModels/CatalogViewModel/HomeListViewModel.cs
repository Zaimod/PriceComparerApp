using PriceComparerApp.Models.DataTransferObjects;
using PriceComparerApp.ApiServices;
using PriceComparerApp.Views.MenuViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using PriceComparerApp.Views;

namespace PriceComparerApp.ViewModels.CatalogViewModel
{
    public class HomeListViewModel : INotifyPropertyChanged
    {
        private bool initialized = false;

        ProductService productService = new ProductService();
        
        public ObservableCollection<ProductDto> items { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand BackCommand { protected set; get; }      
        public INavigation Navigation { get; set; }

       
        public HomeListViewModel()
        {
            items = new ObservableCollection<ProductDto>();
            BackCommand = new Command(Back);
        }

        private bool _isRefreshing = false;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }

        private bool isBusy;
        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;
                OnPropertyChanged("IsBusy");
                OnPropertyChanged("IsLoaded");
            }
        }

        public bool IsLoaded
        {
            get { return !isBusy; }
        }

        ProductDto selectedItem;
        public ProductDto SelectedItem
        {
            get { return selectedItem; }
            set
            {
                if (selectedItem != value)
                {
                    selectedItem = value;
                    //Navigation.PushAsync(new ItemPage());
                    HandleSelectedItem();
                }
            }
        }

        private async void HandleSelectedItem()
        {
            await Navigation.PushAsync(new ItemPage(selectedItem));
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsRefreshing = true;

                    await GetItems(true);
                    
                    IsRefreshing = false;
                });
            }
        }

        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        private void Back()
        {
            Navigation.PopAsync();
        }

        public async Task GetItems(bool refresh = false, int categoryId = 0)
        {

            if (initialized == true && refresh == false) 
                return;
            IsBusy = true;

            IEnumerable<ProductDto> catalogDto;

            if (categoryId != 0)
                catalogDto = await productService.GetProductsByCategory(categoryId);
            else
                catalogDto = await productService.GetProducts();

            while (items.Any())
                items.RemoveAt(items.Count - 1);

            foreach (ProductDto f in catalogDto)
                items.Add(f);

            IsBusy = false;
            initialized = true;
        }
    }
}
