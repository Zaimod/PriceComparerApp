using PriceComparerApp.ApiServices;
using PriceComparerApp.Models.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PriceComparerApp.ViewModels.CatalogViewModel
{
    public class ItemViewModel : INotifyPropertyChanged
    {
        private bool initialized = false;
        
        CatalogService catalogService = new CatalogService();


        public ObservableCollection<CatalogDto> items { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand BackCommand { protected set; get; }
        public INavigation Navigation { get; set; }


        public ItemViewModel()
        {
            items = new ObservableCollection<CatalogDto>();
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

        private int productId;
        public int ProductId
        {
            get { return productId; }
            set
            {
                productId = value;
                OnPropertyChanged(nameof(ProductId));
            }
        }

        CatalogDto selectedItem;
        public CatalogDto SelectedItem
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
            //await Navigation.PushAsync(new ItemPage(selectedItem.Id));
            await Browser.OpenAsync(new Uri(selectedItem.Url), BrowserLaunchMode.SystemPreferred);
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

        public async Task GetItems(bool refresh = false)
        {

            if (initialized == true && refresh == false)
                return;
            IsBusy = true;

            IEnumerable<CatalogDto> catalogDto = await catalogService.GetByProductId(productId);

            
            while (items.Any())
                items.RemoveAt(items.Count - 1);

            foreach (CatalogDto f in catalogDto)
            {
                f.UrlImageShop = $"store{f.StoreId}";
                f.NewPriceStr = f.NewPrice + "₴";
                items.Add(f);
            }
            IsBusy = false;
            initialized = true;
        }
    }
}
