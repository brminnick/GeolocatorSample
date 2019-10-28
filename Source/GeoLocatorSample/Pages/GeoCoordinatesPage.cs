using System;
using AsyncAwaitBestPractices;
using Xamarin.Forms;

namespace GeoLocatorSample
{
    public class GeoCoordinatesPage : BaseContentPage<GeoCoordinatesViewModel>
    {
        public GeoCoordinatesPage()
        {
            var currentLocationTitleLabel = new TitleLabel("Lat/Long");

            var currentLocationValueLabel = new CenteredTextLabel();
            currentLocationValueLabel.SetBinding(Label.TextProperty, nameof(GeoCoordinatesViewModel.LatLongText));

            var latLongAccuracyTitleLabel = new TitleLabel("Lat/Long Accuracy");

            var latLongAccruacyValueLabel = new CenteredTextLabel();
            latLongAccruacyValueLabel.SetBinding(Label.TextProperty, nameof(GeoCoordinatesViewModel.LatLongAccuracyText));

            var altitudeTitleLabel = new TitleLabel("Altitude");

            var altitudeValueLabel = new CenteredTextLabel();
            altitudeValueLabel.SetBinding(Label.TextProperty, nameof(GeoCoordinatesViewModel.AltitudeText));

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
                    altitudeValueLabel
                }
            };

            GeolocationService.GeolocationFailed += HandleGeolocationFailed;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (ViewModel.StartUpdatingLocationCommand.CanExecute(null))
                ViewModel.StartUpdatingLocationCommand.ExecuteAsync().SafeFireAndForget();
        }

        void HandleGeolocationFailed(object sender, Exception exception)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                switch (exception)
                {
                    case Exception javaLangException when javaLangException.Message.Contains("requestPermissions"):
                    case Xamarin.Essentials.PermissionException permissionException:
                        var shouldOpenSettings = await DisplayAlert("Geoloation Failed", "Geolocation Permission Disabled", "Open Settings", "Ignore");

                        if (shouldOpenSettings)
                            Xamarin.Essentials.AppInfo.ShowSettingsUI();
                        break;

                    default:
                        await DisplayAlert("Geolocation Failed", exception.Message, "OK");
                        break;
                }
            });
        }

        class TitleLabel : CenteredTextLabel
        {
            public TitleLabel(in string text)
            {
                Text = text;
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
    }
}
