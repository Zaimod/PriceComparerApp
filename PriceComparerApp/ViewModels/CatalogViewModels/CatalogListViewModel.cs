using PriceComparerApp.Models.DataTransferObjects;
using PriceComparerApp.ApiServices;
using PriceComparerApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PriceComparerApp.ViewModels.CatalogViewModels
{
    public class CatalogListViewModel : INotifyPropertyChanged
    {
        bool initialized = false;
        private bool isBusy;
        public ObservableCollection<CatalogDto> items { get; set; }
        CatalogService catalogService = new CatalogService();
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand BackCommand { protected set; get; }      
        public INavigation Navigation { get; set; }

        CatalogDto selectedItem;
        public CatalogListViewModel()
        {
            items = new ObservableCollection<CatalogDto>();
            BackCommand = new Command(Back);
        }

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

        public CatalogDto SelectedItem
        {
            get { return selectedItem; }
            set
            {
                if (selectedItem != value)
                {
                    CatalogDto tempItem = value;
                    selectedItem = null;
                    OnPropertyChanged("SelectedItem");
                    Navigation.PushAsync(new ItemPage(tempItem, this));
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

        public async Task GetItems()
        {
            if (initialized == true) 
                return;
            IsBusy = true;
            IEnumerable<CatalogDto> catalogDto = await catalogService.Get();

            while (items.Any())
                items.RemoveAt(items.Count - 1);

            foreach (CatalogDto f in catalogDto)
                items.Add(f);

            IsBusy = false;
            initialized = true;
        }
    }
}
