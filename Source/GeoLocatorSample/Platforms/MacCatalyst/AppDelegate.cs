using Foundation;
using Microsoft.Maui.Hosting;

namespace GeoLocatorSample;

[Register(nameof(AppDelegate))]
public class AppDelegate : MauiUIApplicationDelegate
{
	protected override MauiApp CreateMauiApp() => App.CreateMauiApp();
}