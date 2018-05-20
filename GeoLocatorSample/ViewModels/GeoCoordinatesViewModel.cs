using System;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Essentials;
using Xamarin.Forms;

namespace GeoLocatorSample
{
	public class GeoCoordinatesViewModel : BaseViewModel
	{

		#region Fields
		string _latLongText, _latLongAccuracyText, _altitudeText, _altitudeAccuracyText;
		ICommand _startUpdatingLocationCommand;
		#endregion

		#region Properties
		public ICommand StartUpdatingLocationCommand => _startUpdatingLocationCommand ?? (_startUpdatingLocationCommand = new Command(async () =>
		{
			bool isUpdatePositionSuccessful = false;

			do
			{
				isUpdatePositionSuccessful = await UpdatePosition().ConfigureAwait(false);
				await Task.Delay(TimeSpan.FromSeconds(1)).ConfigureAwait(false);
			} while (isUpdatePositionSuccessful);
		}));

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
		async Task<bool> UpdatePosition()
		{
			var location = await GeolocationService.GetLocation().ConfigureAwait(false);

			if (location is null) 
				return false;

			LatLongAccuracyText = $"{ConvertDoubleToString(location?.Accuracy, 0)}m";
			LatLongText = $"{ConvertDoubleToString(location?.Latitude, 3)}, {ConvertDoubleToString(location?.Longitude, 3)}";

			return true;

			string ConvertDoubleToString(double? number, int decimalPlaces) => number?.ToString($"F{decimalPlaces}") ?? "Unknown";
		}
		#endregion
	}
}
