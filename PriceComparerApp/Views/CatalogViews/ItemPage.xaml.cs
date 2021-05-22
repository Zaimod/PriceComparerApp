using PriceComparerApp.Models.DataTransferObjects;
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
    public partial class ItemPage : ContentPage
    {
        public CatalogDto catalogDto { get; private set; }
        public CatalogListViewModel viewModel { get; private set; }
        public ItemPage(CatalogDto catalogDto, CatalogListViewModel viewModel)
        {
            InitializeComponent();
            this.catalogDto = catalogDto;
            this.viewModel = viewModel;
            this.BindingContext = this;
        }
    }
}