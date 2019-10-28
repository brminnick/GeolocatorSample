using AsyncAwaitBestPractices;
using Xamarin.Forms;

namespace GeoLocatorSample
{
    public class App : Application
    {
        readonly GeoCoordinatesPage _geoCoordinatesPage = new GeoCoordinatesPage();

        public App() => MainPage = _geoCoordinatesPage;

        protected override void OnResume()
        {
            base.OnResume();

            var geoCoordinatesViewModel = (GeoCoordinatesViewModel)_geoCoordinatesPage.BindingContext;

            if (geoCoordinatesViewModel.StartUpdatingLocationCommand.CanExecute(null))
                geoCoordinatesViewModel.StartUpdatingLocationCommand.ExecuteAsync().SafeFireAndForget();
        }
    }
}
