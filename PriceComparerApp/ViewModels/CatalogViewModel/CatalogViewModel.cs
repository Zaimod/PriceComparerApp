using PriceComparerApp.ApiServices;
using PriceComparerApp.Models.DataTransferObjects;
using PriceComparerApp.Views;
using PriceComparerApp.Views.MenuViews;
using PriceComparerApp.Views.SignViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PriceComparerApp.ViewModels.CatalogViewModel
{
    public class CatalogViewModel : INotifyPropertyChanged
    {
        private bool initialized = false;
        public event PropertyChangedEventHandler PropertyChanged;

        CategoryService categoryService = new CategoryService();

        public ObservableCollection<CategoryDto> items { get; set; }

        public ICommand BackCommand { protected set; get; }
        public INavigation Navigation { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CatalogViewModel"/> class.
        /// </summary>
        public CatalogViewModel()
        {
            items = new ObservableCollection<CategoryDto>();
            BackCommand = new Command(Back);
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

        CategoryDto selectedItem;
        /// <summary>
        /// Gets or sets the selected item.
        /// </summary>
        /// <value>
        /// The selected item.
        /// </value>
        public CategoryDto SelectedItem
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
        /// Handles the selected item.
        /// </summary>
        private async void HandleSelectedItem()
        {
            HomeListPage home = new HomeListPage() { categoryId = selectedItem.Id };

            await Navigation.PushAsync(home);
        }

        /// <summary>
        /// Gets the items.
        /// </summary>
        public async Task GetItems()
        {
            if (initialized == true)
                return;
            IsBusy = true;

            IEnumerable<CategoryDto> categoryDto = await categoryService.Get();
            while (items.Any())
                items.RemoveAt(items.Count - 1);

            foreach (CategoryDto f in categoryDto)
                items.Add(f);

            IsBusy = false;
            initialized = true;
        }
    }
}
