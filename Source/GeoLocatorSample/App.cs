using Xamarin.Forms;

namespace GeoLocatorSample
{
    public class App : Application
    {
        readonly GeoCoordinatesPage _geoCoordinatesPage;

        public App()
        {
            Device.SetFlags(new[] { "Markup_Experimental" });

            MainPage = _geoCoordinatesPage = new GeoCoordinatesPage();
        }

        protected override void OnResume()
        {
            base.OnResume();

            var geoCoordinatesViewModel = (GeoCoordinatesViewModel)_geoCoordinatesPage.BindingContext;

            if (geoCoordinatesViewModel.StartUpdatingLocationCommand.CanExecute(null))
                geoCoordinatesViewModel.StartUpdatingLocationCommand.Execute(null);
        }
    }
}
