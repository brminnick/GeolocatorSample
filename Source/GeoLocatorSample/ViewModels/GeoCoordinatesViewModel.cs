using System;
using System.Threading.Tasks;
using AsyncAwaitBestPractices.MVVM;
using Xamarin.Essentials;

namespace GeoLocatorSample
{
    public class GeoCoordinatesViewModel : BaseViewModel
    {
        bool _isPollingGeolocation;

        IAsyncCommand? _startUpdatingLocationCommand;

        string _latLongText = string.Empty,
            _latLongAccuracyText = string.Empty,
            _altitudeText = string.Empty,
            _altitudeAccuracyText = string.Empty;

        public IAsyncCommand StartUpdatingLocationCommand =>
            _startUpdatingLocationCommand ??= new AsyncCommand(() => StartUpdatingLocation(TimeSpan.FromSeconds(1)), _ => !_isPollingGeolocation);

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

        async Task<bool> UpdatePosition()
        {
            try
            {
                var location = await GeolocationService.GetLocation().ConfigureAwait(false);

                AltitudeText = $"{convertDoubleToString(location.Altitude, 2)}m";
                LatLongAccuracyText = $"{convertDoubleToString(location.Accuracy, 0)}m";
                LatLongText = $"{convertDoubleToString(location.Latitude, 3)}, {convertDoubleToString(location.Longitude, 3)}";

                return true;
            }
            catch
            {
                return false;
            }

            static string convertDoubleToString(in double? number, in int decimalPlaces, in string appendString = "") =>
                number is null ? "Unknown" : number.Value.ToString($"F{decimalPlaces}") + appendString;
        }

        async Task StartUpdatingLocation(TimeSpan pollingTimeSpan)
        {
            await SetIsPollingGeolocation(true).ConfigureAwait(false);

            bool isUpdatePositionSuccessful;

            do
            {
                isUpdatePositionSuccessful = await UpdatePosition().ConfigureAwait(false);
                await Task.Delay(pollingTimeSpan).ConfigureAwait(false);
            } while (isUpdatePositionSuccessful);

            await SetIsPollingGeolocation(false).ConfigureAwait(false);
        }

        async ValueTask SetIsPollingGeolocation(bool isPollingLocation)
        {
            if (isPollingLocation != _isPollingGeolocation)
            {
                _isPollingGeolocation = isPollingLocation;
                await MainThread.InvokeOnMainThreadAsync(StartUpdatingLocationCommand.RaiseCanExecuteChanged).ConfigureAwait(false);
            }
        }
    }
}
