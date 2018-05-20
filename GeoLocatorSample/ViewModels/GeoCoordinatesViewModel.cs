using System;
using System.Windows.Input;

using Xamarin.Essentials;
using Xamarin.Forms;

namespace GeoLocatorSample
{
	public class GeoCoordinatesViewModel : BaseViewModel
	{
		#region Fields
		string _latLongText, _latLongAccuracyText, _altitudeText, _altitudeAccuracyText;
		ICommand _startListeningForGeoloactionUpdatesCommand;
		#endregion

		#region Constructors
		public GeoCoordinatesViewModel() => Device.StartTimer(TimeSpan.FromSeconds(1), () => { UpdatePosition(); return true; });
		#endregion

		#region Events
		public event EventHandler<string> GeolocationFailed;
		#endregion

		#region Properties
		public string LatLongText
		{
			get => _latLongText;
			set => SetProperty(ref _latLongText, value);
		}

		public string LatLongAccuracyText
		{
			get => _latLongAccuracyText;
			set => SetProperty(ref _latLongAccuracyText, value);
		}

		public string AltitudeText
		{
			get => _altitudeText;
			set => SetProperty(ref _altitudeText, value);
		}

		public string AltitudeAccuracyText
		{
			get => _altitudeAccuracyText;
			set => SetProperty(ref _altitudeAccuracyText, value);
		}
		#endregion

		#region Methods
		async void UpdatePosition()
		{
			try
			{
				var request = new GeolocationRequest(GeolocationAccuracy.Best);
				var location = await Geolocation.GetLocationAsync(request).ConfigureAwait(false);

				LatLongAccuracyText = $"{ConvertDoubleToString(location.Accuracy, 0)}m";
				LatLongText = $"{ConvertDoubleToString(location.Latitude, 3)}, {ConvertDoubleToString(location.Longitude, 3)}";
			}
			catch (FeatureNotSupportedException e)
			{
				OnGeolocationFailed(e.Message);
			}
			catch (PermissionException e)
			{
				OnGeolocationFailed(e.Message);
			}
			catch (Exception e)
			{
				OnGeolocationFailed(e.Message);
			}

			string ConvertDoubleToString(double? number, int decimalPlaces) => (number ?? 0).ToString($"F{decimalPlaces}");
		}

		void OnGeolocationFailed(string message) => GeolocationFailed?.Invoke(this, message);
		#endregion
	}
}
