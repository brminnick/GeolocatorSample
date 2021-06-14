using Comet;
using Microsoft.Maui.Hosting;

namespace GeoLocatorSample
{
    public class App : CometApp
    {
        [Body]
        protected View MainPage() => new GeoCoordinatesPage();

        public override void Configure(IAppHostBuilder appBuilder)
        {
            base.Configure(appBuilder);

            appBuilder.UseMauiApp<App>();
#if DEBUG
            appBuilder.EnableHotReload();
#endif

        }
    }
}
