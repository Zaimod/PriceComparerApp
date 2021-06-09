using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PriceComparerApp.Views.MenuViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FilterListPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilterListPage"/> class.
        /// </summary>
        public FilterListPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Clicked event of the Button control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PopPopupAsync();
        }

        /// <summary>
        /// Handles the CheckedChanged event of the cbAllCategories control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CheckedChangedEventArgs"/> instance containing the event data.</param>
        private void cbAllCategories_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {

        }
    }
}