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

        public CatalogViewModel()
        {
            items = new ObservableCollection<CategoryDto>();
            BackCommand = new Command(Back);
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

        CategoryDto selectedItem;
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

        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        private void Back()
        {
            Navigation.PopAsync();
        }

        private async void HandleSelectedItem()
        {
            HomeListPage home = new HomeListPage() { categoryId = selectedItem.Id };

            await Navigation.PushAsync(home);
        }

        public async Task GetItems()
        {
            if (initialized == true)
                return;
            IsBusy = true;

            IEnumerable<CategoryDto> categoryDto = await categoryService.Get();
            categoryDto = categoryDto.OrderBy(cd => cd.Id);
            while (items.Any())
                items.RemoveAt(items.Count - 1);

            foreach (CategoryDto f in categoryDto)
                items.Add(f);

            IsBusy = false;
            initialized = true;
        }
    }
}
