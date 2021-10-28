using System;
using Xamarin.CommunityToolkit.Markup;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GeoLocatorSample
{
    public class GeoCoordinatesPage : BaseContentPage<GeoCoordinatesViewModel>
    {
        public GeoCoordinatesPage()
        {
            Content = new StackLayout
            {
                Spacing = 2,
                Children =
                {
                    new TitleLabel("Lat/Long"),

                    new CenteredTextLabel()
                        .Bind(Label.TextProperty, nameof(GeoCoordinatesViewModel.LatLongText)),

                    new TitleLabel("Lat/Long Accuracy"),

                    new CenteredTextLabel()
                        .Bind(Label.TextProperty, nameof(GeoCoordinatesViewModel.LatLongAccuracyText)),

                    new TitleLabel("Altitude"),

                    new CenteredTextLabel()
                        .Bind(Label.TextProperty, nameof(GeoCoordinatesViewModel.AltitudeText))
                }
            }.Center();

            GeolocationService.GeolocationFailed += HandleGeolocationFailed;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (ViewModel.StartUpdatingLocationCommand.CanExecute(null))
                ViewModel.StartUpdatingLocationCommand.Execute(null);
        }

        void HandleGeolocationFailed(object sender, Exception exception)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                switch (exception)
                {
                    case Exception javaLangException when javaLangException.Message.Contains("requestPermissions"):
                    case PermissionException permissionException:
                        var shouldOpenSettings = await DisplayAlert("Geoloation Failed", "Geolocation Permission Disabled", "Open Settings", "Ignore");

                        if (shouldOpenSettings)
                            AppInfo.ShowSettingsUI();
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
