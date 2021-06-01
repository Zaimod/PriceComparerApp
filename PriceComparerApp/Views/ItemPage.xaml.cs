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
        public ItemPage(string title, string Description, string img)
        {
            InitializeComponent();

            ItemNameShow.Text = title;         
            DescriptionItemShow.Text = Description;
            ImageCall.Source = new UriImageSource()
            {
                Uri = new Uri(img)
            };
        }
    }
}