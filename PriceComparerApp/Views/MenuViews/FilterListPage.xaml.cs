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
        public FilterListPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PopPopupAsync();
        }

        private void cbAllCategories_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {

        }
    }
}