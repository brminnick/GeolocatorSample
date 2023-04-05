#pragma warning disable IDE0051 // Remove unused private members
using System.Threading.Tasks;
using AsyncAwaitBestPractices;
using Microsoft.Maui.Devices.Sensors;

namespace GeoLocatorSample;

public class GeoCoordinatesPage : View
{
    readonly State<Location> _locationState = new(new Location());
    readonly GeolocationService _geolocationService;

    public GeoCoordinatesPage(GeolocationService geolocationService)
    {
        _geolocationService = geolocationService;
        StartLocationServices().SafeFireAndForget();
    }

    static string ConvertDoubleToString(in double? number, in int decimalPlaces, in string appendString = "") => number is null ? "Unknown" : number.Value.ToString($"F{decimalPlaces}") + appendString;

    [Body]
    View GenerateBody() => new VStack(LayoutAlignment.Center, 2)
    {
        new TitleText("Lat/Long"),
        new LabelText(() => $"{ConvertDoubleToString(_locationState.Value.Latitude, 3)}, {ConvertDoubleToString(_locationState.Value.Longitude, 3)}"),
        new TitleText("Altitude"),
        new LabelText(() => $"{ConvertDoubleToString(_locationState.Value.Altitude, 2,"m")}"),
        new TitleText("Accuracy"),
        new LabelText(() => $"{ConvertDoubleToString(_locationState.Value.Accuracy, 0,"m")}"),
    }.FillHorizontal().FillVertical();

    async Task StartLocationServices()
    {
        while (true)
        {
            _locationState.Value = await _geolocationService.GetLocation().ConfigureAwait(false);

            await Task.Delay(1000).ConfigureAwait(false);
        }
    }

    class TitleText : Text
    {
        public TitleText(Binding<string> value) : base(value)
        {
            this.Background(Colors.Pink);
            this.FillHorizontal();
            this.FontWeight(FontWeight.Bold);
            this.Margin(new Thickness(0, 15, 0, 0));
            this.VerticalTextAlignment(TextAlignment.Center);
            this.HorizontalTextAlignment(TextAlignment.Center);
            this.Color(ColorConstants.TitleTextColor);
        }
    }

    class LabelText : Text
    {
        public LabelText(Func<string> value) : base(value)
        {this.Background(Colors.Pink);
            this.FillHorizontal();
            this.Color(ColorConstants.TextColor);
            this.VerticalTextAlignment(TextAlignment.Center);
            this.HorizontalTextAlignment(TextAlignment.Center);
        }
    }
}

#pragma warning restore IDE0051 // Remove unused private members