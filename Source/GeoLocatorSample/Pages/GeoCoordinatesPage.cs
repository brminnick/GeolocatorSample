#pragma warning disable IDE0051 // Remove unused private members
using System;
using System.Threading.Tasks;
using AsyncAwaitBestPractices;
using Comet;
using Microsoft.Maui;
using Microsoft.Maui.Essentials;

namespace GeoLocatorSample
{
    public class GeoCoordinatesPage : View
    {
        readonly State<Location> _locationState = new State<Location>(new Location());

        public GeoCoordinatesPage() => StartLocationServices().SafeFireAndForget();

        static string ConvertDoubleToString(in double? number, in int decimalPlaces, in string appendString = "") => number is null ? "Unknown" : number.Value.ToString($"F{decimalPlaces}") + appendString;

        [Body]
        View GenerateBody() => new VStack(Comet.HorizontalAlignment.Center, 2)
        {
            new TitleText("Lat/Long"),
            new LabelText(() => $"{ConvertDoubleToString(_locationState.Value?.Latitude, 3)}, {ConvertDoubleToString(_locationState.Value.Longitude, 3)}"),
            new TitleText("Altitude"),
            new LabelText(() => $"{ConvertDoubleToString(_locationState.Value?.Altitude, 2,"m")}"),
            new TitleText("Accuracy"),
            new LabelText(() => $"{ConvertDoubleToString(_locationState.Value?.Accuracy, 0,"m")}"),
        }.FillHorizontal().FillVertical();

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
            public TitleText(Binding<string> value) : base(value)
            {
                this.FillHorizontal();
                this.FontWeight(FontWeight.Bold);
                this.Margin(new Thickness(0, 15, 0, 0));
                this.TextAlignment(TextAlignment.Center);
                this.Color(ColorConstants.TitleTextColor);
            }
        }

        class LabelText : Text
        {
            public LabelText(Func<string> value) : base(value)
            {
                this.FillHorizontal();
                this.Color(ColorConstants.TextColor);
                this.TextAlignment(TextAlignment.Center);
            }
        }
    }
}

#pragma warning restore IDE0051 // Remove unused private members