using System;
using System.Threading.Tasks;
using AsyncAwaitBestPractices;
using Comet;
using Xamarin.Essentials;

namespace GeoLocatorSample
{
    public class GeoCoordinatesPage : View
    {
        readonly State<Location?> _locationState = new State<Location?>();

        public GeoCoordinatesPage() => StartLocationServices().SafeFireAndForget();

        static string ConvertDoubleToString(in double? number, in int decimalPlaces) => $"{number?.ToString($"F{decimalPlaces}")}" ?? "Unknown";

#pragma warning disable IDE0051 // Remove unused private members
        [Body]
        View GenerateBody() => new VStack(HorizontalAlignment.Center, 2)
        {
            new TitleText("Lat/Long"),
            new LabelText(() => $"{ConvertDoubleToString(_locationState.Value?.Latitude, 3)}, {ConvertDoubleToString(_locationState.Value?.Longitude, 3)}"),
            new TitleText("Altitude"),
            new LabelText(() => $"{ConvertDoubleToString(_locationState.Value?.Altitude, 2)}m"),
            new TitleText("Accuracy"),
            new LabelText(() => $"{ConvertDoubleToString(_locationState.Value?.Accuracy, 0)}m"),
        };
#pragma warning restore IDE0051 // Remove unused private members

        async Task StartLocationServices()
        {
            while (true)
            {
                _locationState.Value = await GeolocationService.GetLocation().ConfigureAwait(false);

                await Task.Delay(1000).ConfigureAwait(false);
            }
        }

        class TitleText : Text
        {
            public TitleText(Binding<string>? value = null) : base(value)
            {
                this.Color(ColorConstants.TitleTextColor);
                this.FontWeight(Weight.Bold);
                this.TextAlignment(TextAlignment.Center);
                this.Margin(new Thickness(0, 15, 0, 0));
            }
        }

        class LabelText : Text
        {
            public LabelText(Func<string> value) : base(value)
            {
                this.Color(ColorConstants.TextColor);
                this.TextAlignment(TextAlignment.Center);
            }
        }
    }
}
