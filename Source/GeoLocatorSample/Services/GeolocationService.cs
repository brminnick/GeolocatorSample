using System;
using System.Threading.Tasks;
using AsyncAwaitBestPractices;
using Xamarin.Essentials;

namespace GeoLocatorSample
{
    public static class GeolocationService
    {
        static readonly WeakEventManager<Exception> _geolocationFailedWeakEventManager = new WeakEventManager<Exception>();
        static readonly Lazy<GeolocationRequest> _geolocationRequestHolder = new Lazy<GeolocationRequest>(() => new GeolocationRequest(GeolocationAccuracy.Best));

        public static event EventHandler<Exception> GeolocationFailed
        {
            add => _geolocationFailedWeakEventManager.AddEventHandler(value);
            remove => _geolocationFailedWeakEventManager.RemoveEventHandler(value);
        }

        static GeolocationRequest GeolocationRequest => _geolocationRequestHolder.Value;
 
        public static async Task<Location> GetLocation()
        {
            try
            {
                var location = await Geolocation.GetLocationAsync(GeolocationRequest).ConfigureAwait(false);
                return location;
            }
            catch (PermissionException e) when (e.Message.ToLower().Contains("main thread"))
            {
                var location = await MainThread.InvokeOnMainThreadAsync(() => Geolocation.GetLocationAsync(GeolocationRequest)).ConfigureAwait(false);
                return location;
            }
            catch (Exception e)
            {
                OnGeolocationFailed(e);
                throw;
            }
        }

        static void OnGeolocationFailed(Exception exception) => _geolocationFailedWeakEventManager.RaiseEvent(null, exception, nameof(GeolocationFailed));
    }
}
