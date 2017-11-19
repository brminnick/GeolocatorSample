using Plugin.Geolocator;
using System.Windows.Input;
using Xamarin.Forms;
using System;
using System.Threading.Tasks;
using Plugin.Geolocator.Abstractions;

namespace GeoLocatorSample
{
    public class GeoCoordinatesViewModel : BaseViewModel
    {
        #region Fields
        string _latLongText, _latLongAccuracyText, _altitudeText, _altitudeAccuracyText;
        ICommand _startListeningForGeoloactionUpdatesCommand;
        #endregion

        #region Constructors
        public GeoCoordinatesViewModel() => StartListeningForGeoloactionUpdatesCommand?.Execute(null);
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

        ICommand StartListeningForGeoloactionUpdatesCommand => _startListeningForGeoloactionUpdatesCommand ??
            (_startListeningForGeoloactionUpdatesCommand = new Command(async () => await ExecuteStartListeningForGeoloactionUpdatesCommand()));

        bool IsGeolocationAvailable => CrossGeolocator.IsSupported
                                        && CrossGeolocator.Current.IsGeolocationAvailable
                                        && CrossGeolocator.Current.IsGeolocationEnabled;
        #endregion

        #region Methods
        Task ExecuteStartListeningForGeoloactionUpdatesCommand()
        {
            if (IsGeolocationAvailable)
            {
                CrossGeolocator.Current.PositionChanged += HandlePositionChanged;
                return CrossGeolocator.Current.StartListeningAsync(TimeSpan.FromSeconds(1), 10);
            }

            LatLongText = "Geolocation Unavailable";
            return Task.CompletedTask;
        }

        void HandlePositionChanged(object sender, PositionEventArgs e)
        {
            AltitudeAccuracyText = $"{ConvertDoubleToString(e.Position.AltitudeAccuracy, 0)}m";
            AltitudeText = $"{ConvertDoubleToString(e.Position.Altitude, 2)}m";
            LatLongAccuracyText = $"{ConvertDoubleToString(e.Position.Accuracy, 0)}m";
            LatLongText = $"{ConvertDoubleToString(e.Position.Latitude, 2)}, {ConvertDoubleToString(e.Position.Longitude, 2)}";

            string ConvertDoubleToString(double number, int decimalPlaces) => number.ToString($"F{decimalPlaces}");
        }
        #endregion
    }
}
