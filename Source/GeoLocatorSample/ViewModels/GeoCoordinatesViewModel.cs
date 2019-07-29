using System;
using System.Threading.Tasks;
using System.Windows.Input;

using AsyncAwaitBestPractices.MVVM;

namespace GeoLocatorSample
{
    public class GeoCoordinatesViewModel : BaseViewModel
    {
        #region Fields
        string _latLongText, _latLongAccuracyText, _altitudeText, _altitudeAccuracyText;
        ICommand _startUpdatingLocationCommand;
        #endregion

        #region Properties
        public ICommand StartUpdatingLocationCommand => _startUpdatingLocationCommand ??
            (_startUpdatingLocationCommand = new AsyncCommand(StartUpdatingLocation));

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
            try
            {
                var location = await GeolocationService.GetLocation().ConfigureAwait(false);

                AltitudeText = $"{convertDoubleToString(location?.Altitude, 2)}m";
                LatLongAccuracyText = $"{convertDoubleToString(location?.Accuracy, 0)}m";
                LatLongText = $"{convertDoubleToString(location?.Latitude, 3)}, {convertDoubleToString(location?.Longitude, 3)}";

                return true;
            }
            catch
            {
                return false;
            }

            string convertDoubleToString(in double? number, in int decimalPlaces) => number?.ToString($"F{decimalPlaces}") ?? "Unknown";
        }

        async Task StartUpdatingLocation()
        {
            bool isUpdatePositionSuccessful;

            do
            {
                isUpdatePositionSuccessful = await UpdatePosition().ConfigureAwait(false);
                await Task.Delay(TimeSpan.FromSeconds(1)).ConfigureAwait(false);
            } while (isUpdatePositionSuccessful);
        }
        #endregion
    }
}
