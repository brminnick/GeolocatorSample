#pragma warning disable IDE0051 // Remove unused private members
using Microsoft.Maui.Devices.Sensors;
using Microsoft.Maui.Dispatching;
using Microsoft.Maui.Hosting;

namespace GeoLocatorSample;

public class App : CometApp
{
	[Body]
	View View() => new GeoCoordinatesPage(new GeolocationService(Dispatcher.GetForCurrentThread(), Geolocation.Default));

	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder.UseCometApp<App>()
			.ConfigureFonts(fonts => {
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
		//-:cnd
#if DEBUG
		builder.EnableHotReload();
#endif
		//+:cnd
		return builder.Build();
	}
}

#pragma warning restore IDE0051 // Remove unused private members