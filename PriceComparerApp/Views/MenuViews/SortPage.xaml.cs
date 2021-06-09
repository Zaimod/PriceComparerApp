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

        /// <summary>
        /// Handles the CheckedChanged event of the IsAscendingAlhabet control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CheckedChangedEventArgs"/> instance containing the event data.</param>
        private void IsAscendingAlhabet_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            filterName = "byAscending";
            MessagingCenter.Send(App.Current as App, "SortItemsByFilter", filterName);
            Navigation.PopPopupAsync();
        }

        /// <summary>
        /// Handles the CheckedChanged event of the IsDescendingAlhabet control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CheckedChangedEventArgs"/> instance containing the event data.</param>
        private void IsDescendingAlhabet_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            filterName = "byDescending";
            MessagingCenter.Send(App.Current as App, "SortItemsByFilter", filterName);
            Navigation.PopPopupAsync();
        }

        /// <summary>
        /// Handles the CheckedChanged event of the isPopularityDesc control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CheckedChangedEventArgs"/> instance containing the event data.</param>
        private void isPopularityDesc_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            filterName = "byPopularityDesc";
            MessagingCenter.Send(App.Current as App, "SortItemsByFilter", filterName);
            Navigation.PopPopupAsync();
        }

        /// <summary>
        /// Handles the CheckedChanged event of the isPopularityAsc control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CheckedChangedEventArgs"/> instance containing the event data.</param>
        private void isPopularityAsc_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            filterName = "byPopularityAsc";
            MessagingCenter.Send(App.Current as App, "SortItemsByFilter", filterName);
            Navigation.PopPopupAsync();
        }
    }
}