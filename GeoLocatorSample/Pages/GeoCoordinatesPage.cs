using Xamarin.Forms;
using System.Threading.Tasks;
using System.Threading;

namespace GeoLocatorSample
{
    public class GeoCoordinatesPage : BaseContentPage<GeoCoordinatesViewModel>
    {
        #region Constructors
        public GeoCoordinatesPage()
        {
            var currentLocationTitleLabel = new TitleLabel { Text = "Lat/Long" };

            var currentLocationValueLabel = new CenteredTextLabel();
            currentLocationValueLabel.SetBinding(Label.TextProperty, nameof(ViewModel.LatLongText));

            var latLongAccuracyTitleLabel = new TitleLabel { Text = "Lat/Long Accuracy" };

            var latLongAccruacyValueLabel = new CenteredTextLabel();
            latLongAccruacyValueLabel.SetBinding(Label.TextProperty, nameof(ViewModel.LatLongAccuracyText));

            var altitudeTitleLabel = new TitleLabel { Text = "Altitude" };

            var altitudeValueLabel = new CenteredTextLabel();
            altitudeValueLabel.SetBinding(Label.TextProperty, nameof(ViewModel.AltitudeText));

            var altitudeAccuracyTitleLabel = new TitleLabel { Text = "Altitude Accuracy" };

            var altitudeAccuracyValueLabel = new CenteredTextLabel();
            altitudeAccuracyValueLabel.SetBinding(Label.TextProperty, nameof(ViewModel.AltitudeAccuracyText));

            Content = new StackLayout
            {
                Spacing = 2,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Children = {
                    currentLocationTitleLabel,
                    currentLocationValueLabel,
                    latLongAccuracyTitleLabel,
                    latLongAccruacyValueLabel,
                    altitudeTitleLabel,
                    altitudeValueLabel,
                    altitudeAccuracyTitleLabel,
                    altitudeAccuracyValueLabel
                }
            };
        }
        #endregion

        #region Classes
        class TitleLabel : CenteredTextLabel
        {
            public TitleLabel()
            {
                TextColor = ColorConstants.TitleTextColor;
                FontAttributes = FontAttributes.Bold;
                Margin = new Thickness(0, 15, 0, 0);
            }
        }

        class CenteredTextLabel : Label
        {
            public CenteredTextLabel()
            {
                TextColor = ColorConstants.TextColor;
                HorizontalTextAlignment = TextAlignment.Center;
            }
        }
        #endregion
    }
}
