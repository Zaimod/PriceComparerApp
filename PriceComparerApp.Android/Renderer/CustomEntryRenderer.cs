using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PriceComparerApp.CustomRenderer;
using PriceComparerApp.Android.Renderer;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;

namespace PriceComparerApp.Android.Renderer
{
    public class CustomEntryRenderer : EntryRenderer
    {
        public CustomEntryRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.SetBackgroundColor(Color.FromHex("#7601DC").ToAndroid());
            }
        }
    }
}