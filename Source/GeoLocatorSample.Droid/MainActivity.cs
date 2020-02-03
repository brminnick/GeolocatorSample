using Android.OS;
using Android.App;
using Android.Content.PM;
using Comet.Android;

namespace GeoLocatorSample.Droid
{
    [Activity(Label = "GeoLocatorSample.Droid", Icon = "@drawable/Icon", RoundIcon = "@drawable/Icon_Round", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : CometActivity
    {
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
#if DEBUG
            global::Comet.Reload.Init();
#endif
            global::Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this, savedInstanceState);

            Page = new GeoCoordinatesPage();
        }
    }
}
