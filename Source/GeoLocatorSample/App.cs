using Xamarin.Forms;

namespace GeoLocatorSample
{
	public class App : Application
	{
		#region Constant Fields
		readonly GeoCoordinatesPage _geoCoordinatesPage = new GeoCoordinatesPage();
		#endregion

		#region Constructors
		public App() => MainPage = _geoCoordinatesPage;
		#endregion

		#region Methods
		protected override void OnResume()
		{
			base.OnResume();

			var geoCoordinatesViewModel = _geoCoordinatesPage?.BindingContext as GeoCoordinatesViewModel;
			geoCoordinatesViewModel?.StartUpdatingLocationCommand?.Execute(null);
		}
		#endregion
	}
}
