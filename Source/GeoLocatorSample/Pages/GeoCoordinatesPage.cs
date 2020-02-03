using System.Threading.Tasks;
using AsyncAwaitBestPractices;
using Comet;
using Xamarin.Essentials;

namespace GeoLocatorSample
{
    public class GeoCoordinatesPage : View
    {
        [State]
        readonly State<Location?> _locationState = new State<Location?>();

        public GeoCoordinatesPage() => StartLocationServices().SafeFireAndForget();

        [Body]
        View body() => new VStack
        {
            new Text("Latitude"),
            new Text(()=> $"{ConvertDoubleToString(_locationState.Value?.Latitude,2)}"),
            new Text("Longitude"),
            new Text(()=>$"{ConvertDoubleToString(_locationState.Value?.Longitude,2)}"),
            new Text("Altitude"),
            new Text(()=> $"{ConvertDoubleToString(_locationState.Value?.Altitude,3)}"),
        };

        async Task StartLocationServices()
        {
            while (true)
            {
                _locationState.Value = await GeolocationService.GetLocation().ConfigureAwait(false);

                await Task.Delay(1000).ConfigureAwait(false);
            }
        }

        static string ConvertDoubleToString(in double? number, in int decimalPlaces) => $"{number?.ToString($"F{decimalPlaces}")}m" ?? "Unknown";

        //class TitleLabel : CenteredTextLabel
        //{
        //    public TitleLabel(in string text)
        //    {
        //        Text = text;
        //        TextColor = ColorConstants.TitleTextColor;
        //        FontAttributes = FontAttributes.Bold;
        //        Margin = new Thickness(0, 15, 0, 0);
        //    }
        //}

        //class CenteredTextLabel : Label
        //{
        //    public CenteredTextLabel()
        //    {
        //        TextColor = ColorConstants.TextColor;
        //        HorizontalTextAlignment = TextAlignment.Center;
        //    }
        //}
    }
}
