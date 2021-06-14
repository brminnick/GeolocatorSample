using Android.App;
using Android.Content.PM;

namespace GeoLocatorSample.Droid
{
    [Activity(Label = "GeoLocatorSample.Droid", Icon = "@drawable/Icon", RoundIcon = "@drawable/Icon_Round", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    [IntentFilter(new[] { Microsoft.Maui.Essentials.Platform.Intent.ActionAppAction }, Categories = new[] { global::Android.Content.Intent.CategoryDefault })]
    public class AppDelegate : Microsoft.Maui.MauiAppCompatActivity
    {
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            Microsoft.Maui.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
