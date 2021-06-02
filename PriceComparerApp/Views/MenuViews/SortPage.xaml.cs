using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PriceComparerApp.Views.MenuViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SortPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        string filterName = null;
        public SortPage()
        {

            InitializeComponent();
        }

        private void IsAscendingAlhabet_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            filterName = "byAscending";
            MessagingCenter.Send(App.Current as App, "SortItemsByFilter", filterName);
            Navigation.PopPopupAsync();
        }

        private void IsDescendingAlhabet_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            filterName = "byDescending";
            MessagingCenter.Send(App.Current as App, "SortItemsByFilter", filterName);
            Navigation.PopPopupAsync();
        }

        private void isPopularityDesc_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            filterName = "byPopularityDesc";
            MessagingCenter.Send(App.Current as App, "SortItemsByFilter", filterName);
            Navigation.PopPopupAsync();
        }

        private void isPopularityAsc_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            filterName = "byPopularityAsc";
            MessagingCenter.Send(App.Current as App, "SortItemsByFilter", filterName);
            Navigation.PopPopupAsync();
        }
    }
}