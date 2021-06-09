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


        /// <summary>
        /// Initializes a new instance of the <see cref="HomeListViewModel"/> class.
        /// </summary>
        public HomeListViewModel()
        {
            items = new ObservableCollection<ProductDto>();
            BackCommand = new Command(Back);
        }

        private bool _isRefreshing = false;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is refreshing.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is refreshing; otherwise, <c>false</c>.
        /// </value>
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
        /// <summary>
        /// Gets or sets a value indicating whether this instance is busy.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is busy; otherwise, <c>false</c>.
        /// </value>
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

        /// <summary>
        /// Gets a value indicating whether this instance is loaded.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is loaded; otherwise, <c>false</c>.
        /// </value>
        public bool IsLoaded
        {
            get { return !isBusy; }
        }

        ProductDto selectedItem;
        /// <summary>
        /// Gets or sets the selected item.
        /// </summary>
        /// <value>
        /// The selected item.
        /// </value>
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

        /// <summary>
        /// Handles the selected item.
        /// </summary>
        private async void HandleSelectedItem()
        {
            await Navigation.PushAsync(new ItemPage(selectedItem));
        }

        /// <summary>
        /// Gets the refresh command.
        /// </summary>
        /// <value>
        /// The refresh command.
        /// </value>
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

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="propName">Name of the property.</param>
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        /// <summary>
        /// Backs this instance.
        /// </summary>
        private void Back()
        {
            Navigation.PopAsync();
        }

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <param name="refresh">if set to <c>true</c> [refresh].</param>
        /// <param name="categoryId">The category identifier.</param>
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
