using PriceComparerApp.Models.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PriceComparerApp.ViewModels.CatalogViewModels
{
    public class ItemViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        CatalogListViewModel catalogListViewModel;

        public CatalogDto catalogDto { get; private set; }

        public ItemViewModel()
        {
            catalogDto = new CatalogDto();
        }

        public CatalogListViewModel ListViewModel
        {
            get { return catalogListViewModel; }
            set
            {
                if (catalogListViewModel != value)
                {
                    catalogListViewModel = value;
                    OnPropertyChanged("ListViewModel");
                }
            }
        }

        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
