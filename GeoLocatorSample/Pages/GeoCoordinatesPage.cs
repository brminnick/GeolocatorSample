using System;
using Xamarin.Forms;

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

			Content = new StackLayout
			{
				Spacing = 2,
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center,
				Children = {
					currentLocationTitleLabel,
					currentLocationValueLabel,
					latLongAccuracyTitleLabel,
					latLongAccruacyValueLabel
				}
			};
		}
		#endregion

		#region Methods
		protected override void OnAppearing()
		{
			base.OnAppearing();

			ViewModel.GeolocationFailed += HandleGeolocationFailed;
		}

		protected override void OnDisappearing()
		{
			base.OnAppearing();

			ViewModel.GeolocationFailed -= HandleGeolocationFailed;
		}

		void HandleGeolocationFailed(object sender, string message) =>
			Device.BeginInvokeOnMainThread(async () => await DisplayAlert("Geolocation Failed", message, "OK"));
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
