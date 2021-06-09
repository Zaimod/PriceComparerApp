using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using Android.Graphics;
using Plugin.CurrentActivity;

namespace PriceComparerApp.Android
{
    [Activity(Label = "PriceComparerApp", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        /// <summary>
        /// Called when [create].
        /// </summary>
        /// <param name="bundle">The bundle.</param>
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            Rg.Plugins.Popup.Popup.Init(this);
            //Window.SetFlags(WindowManagerFlags.LayoutNoLimits, WindowManagerFlags.LayoutNoLimits);
            Window.SetStatusBarColor(Color.Rgb(165, 214, 167));
            Window.SetNavigationBarColor(Color.Rgb(165, 214, 167));
            Xamarin.Essentials.Platform.Init(this, bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);      
            global::Xamarin.Forms.FormsMaterial.Init(this, bundle);
            
            ImageCircle.Forms.Plugin.Droid.ImageCircleRenderer.Init();
            CrossCurrentActivity.Current.Init(this.Application);
            LoadApplication(new App());
        }

        /// <summary>
        /// Called when [back pressed].
        /// </summary>
        public override void OnBackPressed()
        {
            Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed);
        }

        /// <summary>
        /// To be added.
        /// </summary>
        /// <param name="requestCode">To be added.</param>
        /// <param name="permissions">To be added.</param>
        /// <param name="grantResults">To be added.</param>
        /// <remarks>
        /// Portions of this page are modifications based on work created and shared by the <format type="text/html"><a href="https://developers.google.com/terms/site-policies" title="Android Open Source Project">Android Open Source Project</a></format> and used according to terms described in the <format type="text/html"><a href="https://creativecommons.org/licenses/by/2.5/" title="Creative Commons 2.5 Attribution License">Creative Commons 2.5 Attribution License.</a></format>
        /// </remarks>
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
         
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

