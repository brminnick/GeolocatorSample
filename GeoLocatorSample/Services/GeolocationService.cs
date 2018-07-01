using System;
using System.Threading.Tasks;

using Xamarin.Essentials;

namespace GeoLocatorSample
{
	public static class GeolocationService
	{
		#region Constant Fields
		static readonly Lazy<GeolocationRequest> _geolocationRequestHolder = new Lazy<GeolocationRequest>(() => new GeolocationRequest(GeolocationAccuracy.Best));
		#endregion

		#region Events
		public static event EventHandler<Exception> GeolocationFailed;
		#endregion

		#region Properties
		static GeolocationRequest GeolocationRequestHolder => _geolocationRequestHolder.Value;
		#endregion

		#region Methods
		public static async Task<Location> GetLocation()
		{
			try
			{
				var location = await Geolocation.GetLocationAsync(GeolocationRequestHolder).ConfigureAwait(false);

				return location;
			}
			catch (Exception e)
			{
				OnGeolocationFailed(e);
				throw
			}
		}

		static void OnGeolocationFailed(Exception exception) => GeolocationFailed?.Invoke(null, exception);
		#endregion
	}
}
